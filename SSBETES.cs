using Terraria;
using Terraria.ModLoader;
using DBZGoatLib.Handlers;
using DBZGoatLib.Model;
using SSBETES.Assets;
using SSBETES.Network;
using System;
using System.IO;
using System.Reflection;

namespace SSBETES
{
	public class SSBETES : Mod
	{
        //public static SSBETES Instance;
        //public static Mod DBTBalance;
        public static DBZMODPORT.Effects.CircleShader circle;
        public static Mod Instance;
        public static Mod DBZMOD;
        public static Mod OOZARU;
        public static Mod GOATLIB;

        public TraitInfo Anc => TraitHandler.GetTraitByName("ancient").GetValueOrDefault();
        public TraitInfo Maj => TraitHandler.GetTraitByName("majin").GetValueOrDefault();

        public override void Load()
        {
            SSBETES.Instance = this;
            TraitHandler.RegisterTrait(Anc);
            TraitHandler.RegisterTrait(Maj);

            BUPPlayer.Ability = KeybindLoader.RegisterKeybind(SSBETES.Instance, "Ability", "P");

            if (ModLoader.TryGetMod("DBZMODPORT", out Mod dbz))
            {
                DBZMOD = dbz;
            }
            if (ModLoader.TryGetMod("oozaru", out Mod ooz))
            {
                OOZARU = ooz;
            }
            if (ModLoader.TryGetMod("DBZGoatLib", out Mod goat))
            {
                GOATLIB = goat;
            }
        }

        public override void Unload()
        {
            SSBETES.Instance = null;
            TraitHandler.UnregisterTrait(Anc);
            TraitHandler.UnregisterTrait(Maj);

            //Instance = null;
            DBZMOD = null;
            GOATLIB = null;
            OOZARU = null;
            SSBETES.DBZMOD = null;
            BUPPlayer.Ability = null;
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
            NetworkHandler.HandlePacket(reader, whoAmI);
        }
        /*
        public bool IsDBTREnabled()
        {
            if (ModLoader.TryGetMod("DBTBalanceRevived", out Mod dbtr))
            {

                BindingFlags AllFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

                var BalanceConfig = dbtr.Code.DefinedTypes.First(x => x.Name.Equals("BalanceConfigServer"));
                dynamic Instance = BalanceConfig.GetField("Instance", AllFlags).GetValue(null);
                return (bool)(Instance?.SSJTweaks ?? false);
            }
            else return false;
        }
        */
    }
}