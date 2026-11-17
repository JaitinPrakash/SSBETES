using System;
using DBZMODPORT.Util;
using SSBETES.Buffs.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SSBETES.Assets.Effects
{
    public class EffectLayer : PlayerDrawLayer
    {
        public override PlayerDrawLayer.Position GetDefaultPosition()
        {
            return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Torso);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (Main.netMode != 2)
            {
                Mod mod = (Mod)SSBETES.Instance;
                if (drawInfo.shadow == 0f)
                {
                    Player drawPlayer2 = drawInfo.drawPlayer;
                    if (drawPlayer2.HasBuff<SSBEBuff>())
                    {
                        drawInfo.DrawDataCache.Add(Lightning(drawInfo.drawPlayer, drawInfo.Position, "SSBETES/Assets/Effects/LightningYellow"));
                    }
                    if (drawPlayer2.HasBuff<SSBKKx20Buff>())
                    {
                        drawInfo.DrawDataCache.Add(Lightning(drawInfo.drawPlayer, drawInfo.Position, "SSBETES/Assets/Effects/LightningRed"));
                    }
                }
            }
        }

        public static DrawData Lightning(Player drawPlayer, Vector2 position, string lightningTexture)
        {
            Mod mod = (Mod)SSBETES.Instance;
            int frame = drawPlayer.GetModPlayer<BUPPlayer>().lightningFrameTimer / 5;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(lightningTexture, (AssetRequestMode)2);
            int frameSize = texture.Height / 3;
            int drawX = (int)(position.X + (float)drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(position.Y + (float)drawPlayer.height / 0.6f - Main.screenPosition.Y);
            return new DrawData(texture, new Vector2((float)drawX, (float)drawY), new Rectangle?(new Rectangle(0, frameSize * frame, texture.Width, frameSize)), Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), 1f, 0, 0f);
        }
    }
}
