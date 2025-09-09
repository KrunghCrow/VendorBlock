using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("Vendor Block" , "Krungh Crow" , "2.0.1")]
    [Description("Disables interaction with NPC vendors and mission npc's with config and permissions")]
    class VendorBlock : RustPlugin
    {
        private Dictionary<string , VendorSettings> vendorSettings = new Dictionary<string , VendorSettings>();

        private readonly Dictionary<string , string> defaultVendors = new Dictionary<string , string>
        {
            //Vehicles
            { "bandit_conversationalist", "vendorblock.heli" },//updated
            { "boat_shopkeeper", "vendorblock.boat" },//updated
            { "stables_shopkeeper", "vendorblock.horse" },//updated
            //Missions
            { "missionprovider_fishing_b", "vendorblock.divemaster"},//updated
            { "missionprovider_fishing_a", "vendorblock.fisherman"},//updated
            { "missionprovider_bandit_a", "vendorblock.lumberjack"},//updated
            { "missionprovider_bandit_b", "vendorblock.miner"},//updated
            { "missionprovider_stables_a", "vendorblock.stablehand"},//updated
            { "missionprovider_stables_b", "vendorblock.hunter"},//updated
            { "missionprovider_outpost_a", "vendorblock.scientist"},//updated
            { "missionprovider_outpost_b", "vendorblock.vagebond"}//updated
        };

        private class VendorSettings
        {
            [JsonProperty("Enabled")]
            public bool Enabled { get; set; }

            [JsonProperty("Permission")]
            public string Permission { get; set; }
        }

        #region Config

        protected override void LoadDefaultConfig()
        {
            Config.Clear();
            vendorSettings.Clear();

            foreach (var entry in defaultVendors)
            {
                vendorSettings[entry.Key] = new VendorSettings
                {
                    Enabled = true ,
                    Permission = entry.Value
                };
            }

            Config.WriteObject(vendorSettings , true);
            SaveConfig();
        }

        protected override void LoadConfig()
        {
            try
            {
                base.LoadConfig();
                vendorSettings = Config.ReadObject<Dictionary<string , VendorSettings>>();

                if (vendorSettings == null)
                {
                    PrintWarning("Config was empty or invalid, regenerating default config...");
                    LoadDefaultConfig();
                    return;
                }

                bool updated = false;

                foreach (var entry in defaultVendors)
                {
                    if (!vendorSettings.ContainsKey(entry.Key))
                    {
                        vendorSettings[entry.Key] = new VendorSettings
                        {
                            Enabled = true ,
                            Permission = entry.Value
                        };
                        updated = true;
                    }
                }

                if (updated)
                {
                    PrintWarning("Missing vendors added to config.");
                    Config.WriteObject(vendorSettings , true);
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                PrintError($"Config error: {ex.Message}, regenerating...");
                LoadDefaultConfig();
            }
        }

        #endregion

        #region Language

        protected override void LoadDefaultMessages()
        {
            var messages = new Dictionary<string , string>();
            foreach (var vendor in defaultVendors.Keys)
            {
                messages[$"VendorReply.{vendor}"] = $"Using the {vendor.Replace("_" , " ")} vendor is disabled on this server!";
            }
            lang.RegisterMessages(messages , this);
        }

        #endregion

        #region Hooks

        void Init()
        {
            foreach (var vendor in vendorSettings.Values)
                permission.RegisterPermission(vendor.Permission , this);

            PrintWarning("VendorBlock initialized and permissions registered.");
        }

        object OnNpcConversationStart(object npcObj , BasePlayer player , ConversationData data)
        {
            if (player == null || npcObj == null || data == null)
                return null;

            var entity = npcObj as BaseEntity;
            if (entity == null)
                return null;

            string key = entity.ShortPrefabName;
            Puts(key);
            if (vendorSettings.TryGetValue(key , out var setting))
            {
                bool hasPerm = permission.UserHasPermission(player.UserIDString , setting.Permission);
                if (!setting.Enabled && !hasPerm)
                {
                    player.ChatMessage(lang.GetMessage($"VendorReply.{key}" , this , player.UserIDString));
                    var method = npcObj.GetType().GetMethod("EndConversation");
                    method?.Invoke(npcObj , new object[] { player });
                    Puts($"Blocked {player.displayName} from NPC '{key}' via conversation hook.");

                    return false;
                }
            }
            return null;
        }

        #endregion
    }
}
