using DBZMODPORT.Dusts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ObjectInteractions;
using rail;

namespace SSBETES.Items.BlueRelic
{
    internal class SaiyanRelicTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Mod dbzmod = ModLoader.GetMod("DBZMODPORT");
            Main.tileSolidTop[(int)base.Type] = false;
            Main.tileFrameImportant[(int)base.Type] = true;
            Main.tileNoAttach[(int)base.Type] = true;
            Main.tileTable[(int)base.Type] = false;
            Main.tileLavaDeath[(int)base.Type] = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData((Terraria.Enums.AnchorType)11, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16
            };
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.CoordinatePadding = 2;
            LocalizedText name = base.CreateMapEntryName();
            base.AddMapEntry(new Color(219, 255, 234), name);
            base.DustType = ModContent.DustType<MetalDust>();
            TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
            base.AdjTiles = new int[]
            {
                (int)dbzmod.Find<ModTile>("KaiTable").Type,
                (int)dbzmod.Find<ModTile>("ZTable").Type
            };
            TileObjectData.addTile((int)base.Type);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = (fail ? 1 : 3);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(null, i * 16, j * 16, 32, 16, ModContent.ItemType<SaiyanRelic>(), 1, false, 0, false, false);
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Main.mouseRightRelease = false;
            player.CloseSign();
            player.SetTalkNPC(0);
            NPC.FindFirstNPC(ModContent.NPCType<SaiyanRelicAI>());
            Main.npcChatCornerItem = 0;
            Main.npcChatText = "hello n";
            return true;
        }

    }
}
