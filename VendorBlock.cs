using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core;

#region Changelogs and ToDo
/**********************************************************************
* 1.0.0 :   Release
* 1.0.1 :   Simplified permcheck
**********************************************************************/
#endregion

namespace Oxide.Plugins
{
    [Info("Vendor Block", "Krungh Crow", "1.0.1")]
    [Description("Disables interaction with the airwolf/boat/stables vendor npc")]
    class VendorBlock : RustPlugin
    {
        #region Variables
        const string Heli_Perm = "vendorblock.heli";
        const string Boat_Perm = "vendorblock.boat";
        const string Horse_Perm = "vendorblock.horse";

        #endregion

        #region LanguageAPI
        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["VendorReplyAirwolf"] = "Using the Airwolf Vendor is disabled on this server!",
                ["VendorReplyBoat"] = "Using the Boat Vendor is disabled on this server!",
                ["VendorReplyStables"] = "Using the Horse Vendor is disabled on this server!",
            }, this);
        }
        #endregion

        #region Oxide hooks

        void Init()
        {
            permission.RegisterPermission(Heli_Perm, this);
            permission.RegisterPermission(Boat_Perm, this);
            permission.RegisterPermission(Horse_Perm, this);
        }

        bool? OnNpcConversationStart(VehicleVendor vendor, BasePlayer player, ConversationData conversationData)
        {
            if (conversationData.shortname == "airwolf_heli_vendor" && !HasPerm(player, Heli_Perm))
            {
                player.ChatMessage(lang.GetMessage("VendorReplyAirwolf", this, player.UserIDString));
                return false;
            }
            if (conversationData.shortname == "boatvendor" && !HasPerm(player, Boat_Perm))
            {
                player.ChatMessage(lang.GetMessage("VendorReplyBoat", this, player.UserIDString));
                return false;
            }
            if (conversationData.shortname == "stablesvendor" && !HasPerm(player, Horse_Perm))
            {
                player.ChatMessage(lang.GetMessage("VendorReplyStables", this, player.UserIDString));
                return false;
            }
            return null;
        }
        #endregion

        #region Helpers
        bool HasPerm(BasePlayer player , string perm) { return HasPerm(player , perm); }
        #endregion
    }
}