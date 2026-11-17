using System;
using DBZGoatLib.Handlers;
using DBZMODPORT;
using DBZMODPORT.Util;
using SSBETES.Buffs.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace SSBETES.Assets.Effects
{
    public class TransformEffect : PlayerDrawLayer
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
            if (Main.netMode != NetmodeID.Server)
            {
                Player drawPlayer3 = drawInfo.drawPlayer;
                BUPPlayer modPlayer2 = drawPlayer3.GetModPlayer<BUPPlayer>();
                Mod mod = (Mod)SSBETES.Instance;
                if (drawInfo.shadow == 0f && modPlayer2.isTransformationAnimationPlaying)
                {
                    bool flag = false;
                    if (drawPlayer3.HasBuff<SSJ3FPBuff>())
                    {
                        int frameCounterLimit2 = 4;
                        int numberOfFrames2 = 4;
                        int yOffset3 = -18;
                        drawInfo.DrawDataCache.Add(TransformationAnimation(drawInfo.drawPlayer, drawInfo.Position, "DBZMODPORT/Effects/Animations/Transform/SSJ3", frameCounterLimit2, numberOfFrames2, yOffset3));
                        flag = modPlayer2.isTransformationAnimationPlaying;
                    }
                    if (drawPlayer3.HasBuff<SSBKKBuff>())
                    {
                        int frameCounterLimit2 = 4;
                        int numberOfFrames2 = 4;
                        int yOffset3 = -18;
                        drawInfo.DrawDataCache.Add(TransformationAnimation(drawInfo.drawPlayer, drawInfo.Position, "SSBETES/Assets/Effects/SSBKK", frameCounterLimit2, numberOfFrames2, yOffset3));
                        flag = modPlayer2.isTransformationAnimationPlaying;
                    }
                    if (drawPlayer3.HasBuff<SSBKKx20Buff>())
                    {
                        int frameCounterLimit2 = 4;
                        int numberOfFrames2 = 4;
                        int yOffset3 = -18;
                        drawInfo.DrawDataCache.Add(TransformationAnimation(drawInfo.drawPlayer, drawInfo.Position, "SSBETES/Assets/Effects/SSBKK20", frameCounterLimit2, numberOfFrames2, yOffset3));
                        flag = modPlayer2.isTransformationAnimationPlaying;
                    }
                    if (drawPlayer3.HasBuff<SSBEBuff>())
                    {
                        int frameCounterLimit5 = 6;
                        int numberOfFrames5 = 6;
                        int yOffset6 = 35;
                        drawInfo.DrawDataCache.Add(TransformationAnimation(drawInfo.drawPlayer, drawInfo.Position, "SSBETES/Assets/Effects/SSBE", frameCounterLimit5, numberOfFrames5, yOffset6));
                        flag = modPlayer2.isTransformationAnimationPlaying;
                    }
                    if (!flag)
                    {
                        modPlayer2.isTransformationAnimationPlaying = false;
                    }
                }
            }
        }

        public static DrawData TransformationAnimation(Player drawPlayer, Vector2 position, string transformationSpriteSheet, int frameCounterLimit, int numberOfFrames, int yOffset)
        {
            Mod mod = (Mod)SSBETES.Instance;
            BUPPlayer APlayer = drawPlayer.GetModPlayer<BUPPlayer>();
            int frame = APlayer.transformationFrameTimer / frameCounterLimit;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(transformationSpriteSheet, (AssetRequestMode)2);
            int frameSize = texture.Height / numberOfFrames;
            int drawX = (int)(position.X + (float)drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(position.Y + (float)frameSize + (float)yOffset + (float)drawPlayer.height / 0.6f - Main.screenPosition.Y);
            if (frame == numberOfFrames)
            {
                APlayer.isTransformationAnimationPlaying = false;
            }
            return new DrawData(texture, new Vector2((float)drawX, (float)drawY), new Rectangle?(new Rectangle(0, frameSize * frame, texture.Width, frameSize)), Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), 1f, 0, 0f);
        }
    }
}

