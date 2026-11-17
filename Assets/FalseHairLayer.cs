using System;
using DBZMODPORT;
using DBZMODPORT.Util;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using SSBETES.Buffs.Transformations;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SSBETES.Changes;

namespace SSBETES.Assets {
    public class FalseHairLayer : PlayerDrawLayer {
        public override bool IsHeadLayer
        {
            get
            {
                return true;
            }
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Head);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
           return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.HasBuff<FSSJBuff>() && Config.Instance.FalseBaseHair)
            {
                
                var TransformedHairSize = ModContent.Request<Texture2D>("SSBETES/Assets/Hair/BaseHair", AssetRequestMode.AsyncLoad).Value;
                
                float x = drawInfo.Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + drawPlayer.width / 2;
                float y = drawInfo.Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f;
                Vector2 vector = new Vector2((int)x, (int)y) + drawPlayer.headPosition + drawInfo.hairOffset;
                vector -= CalcRotation(drawPlayer.headRotation);
                
                drawInfo.DrawDataCache.Add(new DrawData(
                    TransformedHairSize, // The texture to render.
                    vector, // Position to render at.
                    new Rectangle?(drawPlayer.bodyFrame), // Source rectangle.
                    drawPlayer.hairColor, // Color.
                    drawPlayer.headRotation, // Rotation.
                    drawInfo.hairOffset, // Origin. Uses the texture's center.
                    1f, // Scale.
                    drawInfo.playerEffect, // SpriteEffects.
                    0 // 'Layer'. This is always 0 in Terraria.
                ));
            }
        }

        private static Vector2 CalcRotation(float rot)
        {
            Vector2 vector = new Vector2(-13f - rot * 4.25f, -13f + rot * 4.25f);
            float num = (float)Math.Sqrt(Math.Pow(vector.X, 2.0) + Math.Pow(vector.Y, 2.0));
            float num2 = (float)Math.Sqrt(Math.Pow(vector.Y, 2.0) + Math.Pow((double)(vector.X + num), 2.0));
            float num3 = (float)Math.Acos((Math.Pow((double)num, 2.0) + Math.Pow((double)num, 2.0) - Math.Pow((double)num2, 2.0)) / (2.0 * Math.Pow((double)num, 2.0)));
            float num4 = vector.X + num * (float)Math.Cos((double)(MathHelper.ToRadians(rot * 90f) + num3));
            float num5 = vector.Y + num * (float)Math.Sin((double)(MathHelper.ToRadians(rot * 90f) + num3));
            return new Vector2(num4, num5);
        }
    }
}

