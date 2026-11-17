using Terraria;
using Terraria.ModLoader;

namespace SSBETES.Buffs.Buff
{
	public class MaskCooldown : ModBuff
	{
		public override void SetStaticDefaults() 
        {
            Main.debuff[base.Type] = true;
        }
	}
}
