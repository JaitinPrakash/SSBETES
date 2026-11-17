using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace SSBETES.Changes
{

    [Label("Server Settings")]
    internal class ConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static ConfigServer Instance;

        [Label("Rebalanced Stats (Mod Reload Required)")]
        [Tooltip("Adjusts transformation stats to fit DBT:Rebalance")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool SSJTweaks;

        [Label("Bring Back False SSJ")]
        [Tooltip("Brings back False Super Saiyan to the panel")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool FalsePrework;
    }

    [Label("Client Settings")]
    internal class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static Config Instance;

        [Label("Remove DB Enchantment Aura")]
        [Tooltip("Dragon Ball accessory will give its buffs without extra transformation")]
        [DefaultValue(false)]
        public bool DBAura;

        [DefaultValue(true)]
        public bool FalseBaseHair;

    }
}