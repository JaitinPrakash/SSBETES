using Terraria;
using Terraria.ModLoader;

namespace SSBETES.Buffs.Debuffs
{
    internal class FPCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {      
            base.Update(npc, ref buffIndex);
        }
    }
}
