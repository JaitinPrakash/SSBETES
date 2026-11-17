using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DBZGoatLib;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using SSBETES.Assets;
using Terraria.Audio;

namespace SSBETES.Items.BlueRelic
{
    public class SaiyanRelicAI : ModNPC
    {

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 20;
            NPC.aiStyle = 0;
            NPC.defense = 35;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        }
        SoundStyle Wish = new SoundStyle("DBZMODPORT/Sounds/WishGranted");
        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Blue Saiyan Statue"
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Kaio-Ken";
            button2 = "Evolution";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.player[Main.myPlayer];
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            if (firstButton)
            {
                if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.PSSBBuff>()) >= 1f)
                {
                    if (!BUPPlayer.SSBKKAchieved && !BUPPlayer.SSBEAchieved)
                    {
                        BUPPlayer.SSBKKAchieved = true;
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
                    Main.NewText("The time has not come yet, come back later.");
                }

            }
            else
            {
                if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.PSSBBuff>()) >= 1f)
                {
                    if (!BUPPlayer.SSBEAchieved && !BUPPlayer.SSBKKAchieved)
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
                    Main.NewText("The time has not come yet, come back later.");
                }
            }
        }

        public override string GetChat()
        {
            NPC.FindFirstNPC(ModContent.NPCType<SaiyanRelicAI>());
            switch (Main.rand.Next(2))
            {
                case 0:
                    return "Choose wisely. (You will receive a new form depending on the choice, you can do it only once)";
                case 1:
                    return "What do you want me to teach you? (You will receive a new form depending on the choice, you can do it only once)";
                default:
                    return "What path do you prefer? (You will receive a new form depending on the choice, you can do it only once)";
            }
        }
    }
}