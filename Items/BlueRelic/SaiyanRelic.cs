using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SSBETES.Items.BlueRelic
{
    internal class SaiyanRelic : ModItem
    {
        public override void SetDefaults()
        {
            base.Item.DefaultToPlaceableTile(ModContent.TileType<SaiyanRelicTile>(), 0);
            base.Item.width = 40;
            base.Item.height = 70;
            base.Item.rare = 9;
            base.Item.master = true;
            base.Item.value = Item.buyPrice(0, 0, 0, 0);
        }
    }
}
