![VendorBlock icon new2](https://github.com/KrunghCrow/VendorBlock/assets/72466753/d709657c-df6d-4767-9aee-03c522a25736)

## Features
* Permission system
* Support for Airwolf Vendor (Bandit camp)
* Support for Boat Vendor (Fishing village)
* Support for Horse Vendor (Stables)
* Language file included
* Simpleconfiguration
* Lightweight

## Permissions (Assigned will block the vendor if enabled in the cfg)
* Can be set through cfg

## Configuration
Set to false if you want it to be listed as available to get blocked for players having the assigned permissions.
```json
{
  "bandit_conversationalist": {
    "Enabled": true,
    "Permission": "vendorblock.heli"
  },
  "boat_shopkeeper": {
    "Enabled": true,
    "Permission": "vendorblock.boat"
  },
  "missionprovider_bandit_a": {
    "Enabled": true,
    "Permission": "vendorblock.lumberjack"
  },
  "missionprovider_bandit_b": {
    "Enabled": true,
    "Permission": "vendorblock.miner"
  },
  "missionprovider_fishing_a": {
    "Enabled": true,
    "Permission": "vendorblock.fisherman"
  },
  "missionprovider_fishing_b": {
    "Enabled": true,
    "Permission": "vendorblock.divemaster"
  },
  "missionprovider_outpost_a": {
    "Enabled": true,
    "Permission": "vendorblock.scientist"
  },
  "missionprovider_outpost_b": {
    "Enabled": true,
    "Permission": "vendorblock.vagebond"
  },
  "missionprovider_stables_a": {
    "Enabled": true,
    "Permission": "vendorblock.stablehand"
  },
  "missionprovider_stables_b": {
    "Enabled": true,
    "Permission": "vendorblock.hunter"
  },
  "stables_shopkeeper": {
    "Enabled": true,
    "Permission": "vendorblock.horse"
  }
}
```

## Localisation 
```json
{
  "VendorReply.bandit_conversationalist": "Using the bandit conversationalist vendor is disabled on this server!",
  "VendorReply.boat_shopkeeper": "Using the boat shopkeeper vendor is disabled on this server!",
  "VendorReply.stables_shopkeeper": "Using the stables shopkeeper vendor is disabled on this server!",
  "VendorReply.missionprovider_fishing_b": "Using the missionprovider fishing b vendor is disabled on this server!",
  "VendorReply.missionprovider_fishing_a": "Using the missionprovider fishing a vendor is disabled on this server!",
  "VendorReply.missionprovider_bandit_a": "Using the missionprovider bandit a vendor is disabled on this server!",
  "VendorReply.missionprovider_bandit_b": "Using the missionprovider bandit b vendor is disabled on this server!",
  "VendorReply.missionprovider_stables_a": "Using the missionprovider stables a vendor is disabled on this server!",
  "VendorReply.missionprovider_stables_b": "Using the missionprovider stables b vendor is disabled on this server!",
  "VendorReply.missionprovider_outpost_a": "Using the missionprovider outpost a vendor is disabled on this server!",
  "VendorReply.missionprovider_outpost_b": "Using the missionprovider outpost b vendor is disabled on this server!"
}
```
