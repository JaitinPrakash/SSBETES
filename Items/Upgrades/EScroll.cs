using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
using Terraria.Audio;

namespace SSBETES.Items.Upgrades
{
    internal class EScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Saiyan Blue Evolution UNLOCK");
            // Tooltip.SetDefault("Unleashes new power");
        }
        SoundStyle Wish = new SoundStyle("DBZMODPORT/Sounds/WishGranted");
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.potion = false;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.rare = 11;
            base.Item.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override bool? UseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            return !BUPPlayer.SSBEAchieved && !BUPPlayer.SSBKKAchieved;
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.PSSBBuff>()) >= 1f)
            {
                if (!BUPPlayer.SSBKKAchieved && !BUPPlayer.SSBEAchieved)
                {
                    BUPPlayer.SSBEAchieved = true;
                    Main.NewText("Good choice!");
                    SoundEngine.PlaySound(Wish.WithVolumeScale(3f).WithPitchOffset(0f), player.position);
                }
                else if (BUPPlayer.SSBKKAchieved || BUPPlayer.SSBEAchieved)
                {
                    Main.NewText("I hope you don't regret your choice.");
                }
            }
            else
            {
                Main.NewText("The time has not come yet, try to use it later.");
            }
        }
    }
}

