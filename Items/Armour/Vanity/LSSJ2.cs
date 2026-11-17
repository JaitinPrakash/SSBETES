using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace SSBETES.Items.Armour.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    internal class LSSJ2Hair : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(copper: 5);
            Item.rare = ItemRarityID.Yellow;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            Mod DBT = ModLoader.GetMod("DBZMODPORT");
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Cobweb);
            recipe.AddTile(DBT.Find<ModTile>("ZTable"));
            recipe.Register();
        }
    }
}