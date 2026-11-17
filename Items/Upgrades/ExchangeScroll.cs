using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
using DBZMODPORT;
using Terraria.Audio;

namespace SSBETES.Items.Upgrades
{
    internal class ExchangeScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Saiyan Blue Kaio-Ken UNLOCK");
            // Tooltip.SetDefault("Unleashes new power");
            // Tooltip.SetDefault("");
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
            Item.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override bool? UseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            return BUPPlayer.EvoLock || BUPPlayer.KaioLock;
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.EvoLock = false;
            BUPPlayer.KaioLock = false;
            Main.NewText("I hope you don't regret your choice this time.");
        }
    }
}
