using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DBZGoatLib;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZMODPORT.Util;
using DBZMODPORT;
using DBZMODPORT.Buffs;
using DBZMODPORT.Buffs.SSJBuffs;
using SSBETES.Changes;
using SSBETES.Buffs.Transformations;
using SSBETES.Buffs.Traits;
using SSBETES.Buffs.Debuffs;
using SSBETES.Buffs.Buff;
using SSBETES.Assets.Transformation;

namespace SSBETES.Assets
{
    internal class BUPPlayer : ModPlayer
    {
        #region Variables

        public bool FSSJActive;
        public bool FSSJUnlockMsg;
        public bool FSSJAchieved;

        public bool BERSERKERActive;
        public bool BERSERKERUnlockMsg;
        public bool BERSERKERAchieved;

        public bool ConBERSERKERActive;
        public bool ConBERSERKERUnlockMsg;
        public bool ConBERSERKERAchieved;

        public bool EvilSaiyanActive;
        public bool EvilSaiyanUnlockMsg;
        public bool EvilSaiyanAchieved;

        public bool DBEnchancedActive;

        public bool IKARIActive;
        public bool IKARIUnlockMsg;
        public bool IKARIAchieved;

        public bool FPSSJActive;
        public bool FPSSJUnlockMsg;
        public bool FPSSJAchieved;

        public bool SSJFPActive;
        public bool SSJFPUnlockMsg;
        public bool SSJFPAchieved;

        public bool SSJRGActive;
        public bool SSJRGUnlockMsg;
        public bool SSJRGAchieved;

        public bool PSSBActive;
        public bool PSSBUnlockMsg;
        public bool PSSBAchieved;

        public bool SSBEActive;
        public bool SSBEUnlockMsg;
        public bool SSBEAchieved;

        public bool SSBKKActive;
        public bool SSBKKUnlockMsg;
        public bool SSBKKAchieved;

        public bool SSBKKx20Active;
        public bool SSBKKx20UnlockMsg;
        public bool SSBKKx20Achieved;

        public bool SSJ3FPActive;
        public bool SSJ3FPUnlockMsg;
        public bool SSJ3FPAchieved;

        public bool LimitBreakActive;
        public bool LimitBreakUnlockMsg;
        public bool LimitBreakAchieved;

        public bool EvilSaiyanNearby;

        public bool KaioLock;
        public bool EvoLock;

        public bool isAncient;
        public bool isMajin;
        public bool isMightyFruitAte;
        public bool DragonBallEquipped;
        public bool hasBuffs;
        public bool MP_Unlock;

        public bool SuperScroll;
        public bool SuperFragment;

        public bool CrimsonMaskEqpd;
        //public bool isEvilBossAlive;
        //public bool isWallOfFleshAlive;

        public float IkariTimer;
        public float EvilMultiplier;
        public float EvilPower;
        public float EvilPowerTimer;
        public int RageGain;
        public bool Unlocking;
        public bool RageSign;
        public bool FalseAwakening;
        public bool FalseReaction;
        public bool PreFalseCheck;
        public bool FormCD;

        public int lightningFrameTimer;
        public int transformationFrameTimer;
        public bool isTransformationAnimationPlaying;

        public bool TransformationProcess;

        public static ModKeybind Ability;

        public Texture2D hair;
        public Color? originalEyeColor;
        public Color? originalSkinColor;

        public DateTime? Offset = null;
        public DateTime? PoweringUpTime = null;
        public DateTime? LastPowerUpTick = null;

        private TransformationInfo? Form = null;

        #endregion

        public override void PostUpdate()
        {
            Player player = Main.player[Main.myPlayer];
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();

            bool isEvilBossAlive = false;
            bool isBossActive = false;
            bool FalseAwaken1 = false;

            foreach (NPC npc in Main.npc)
            {
                if (npc.boss)
                {
                    isBossActive = true;
                }

                if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.BrainofCthulhu)
                {
                    isEvilBossAlive = true;
                }
                
                if (npc.type == NPCID.WallofFlesh && npc.life <= 25)
                {
                    if (!BUPPlayer.IKARIAchieved && player.GetModPlayer<GPlayer>().Trait == "Legendary")
                    {
                        BUPPlayer.IKARIAchieved = true;
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("IKARIBuff").Value);
                    }
                }
                
                if (npc.type == NPCID.QueenSlimeBoss && npc.life <= 30 && player.GetModPlayer<GPlayer>().Trait != "GOD" && player.GetModPlayer<GPlayer>().Trait != "Majin" && player.GetModPlayer<GPlayer>().Trait != "Legendary")
                {
                    if (!BUPPlayer.RageSign)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 137,
                            new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;

                            Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 142,
                            new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;
                        }
                        BUPPlayer.RageSign = true;
                        Main.NewText("Cult will open gates to new form!", Color.DeepSkyBlue);
                    }
                }
                if (npc.type == NPCID.CultistBoss && npc.life <= 30)
                {
                    if (!BUPPlayer.SSJRGAchieved && BUPPlayer.RageSign && player.GetModPlayer<MyPlayer>().masteryLevel2 >= 1)
                    {
                        BUPPlayer.SSJRGAchieved = true;
                        BUPPlayer.SSJRageAnimation();
                        BUPPlayer.TransformationProcess = true;
                    }
                }
            }

            if (player.GetModPlayer<GPlayer>().Trait == "Ancient")
            {
                if (NPC.downedBoss1 && !NPC.downedBoss3)
                {
                    Player.AddBuff(ModContent.BuffType<UnknownAncient>(), 120);
                }
                else if (NPC.downedBoss1 && NPC.downedBoss3) // && !NPC.downedMechBossAny
                {
                    Player.ClearBuff(ModContent.BuffType<UnknownAncient>());
                    Player.AddBuff(ModContent.BuffType<AncientBuff>(), 120);
                }
            }

            if (player.GetModPlayer<GPlayer>().Trait == "Majin")
            {
                if (NPC.downedBoss1 && !NPC.downedBoss3)
                {
                    Player.AddBuff(ModContent.BuffType<UnknownMajin>(), 120);
                }
                else if (NPC.downedBoss1 && NPC.downedBoss3)
                {
                    Player.ClearBuff(ModContent.BuffType<UnknownMajin>());
                    Player.AddBuff(ModContent.BuffType<MajinBuff>(), 120);
                }
            }

            if (isEvilBossAlive && ConfigServer.Instance.FalsePrework) //NPC.downedBoss2
            {
                if (!BUPPlayer.FSSJAchieved && (player.GetModPlayer<GPlayer>().Trait != "GOD" && player.GetModPlayer<GPlayer>().Trait != "Majin"))
                {
                    if ((player.statLife <= player.statLifeMax2 / 1.5f) && !FalseAwaken1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 108,
                            new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;
                        }
                        FalseAwaken1 = true;
                    }

                    if ((player.statLife <= player.statLifeMax2 / 4) && FalseAwaken1)
                    {
                        BUPPlayer.FSSJAchieved = true;
                        Player.statLife = Player.statLifeMax2;
                        TransformationHandler.ClearTransformations(Player);
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("FSSJBuff").Value);
                    }
                }
            }

            if (!ConfigServer.Instance.FalsePrework && NPC.downedBoss1 && !NPC.downedBoss3)
            {
                if (player.GetModPlayer<GPlayer>().Trait != "GOD" && player.GetModPlayer<GPlayer>().Trait != "Majin" && !Player.HasBuff<FSSJBuff>() && isBossActive)
                {
                    BUPPlayer.PreFalseCheck = true;
                    if ((player.statLife <= player.statLifeMax2 / 4) && BUPPlayer.FalseReaction)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 108,
                            new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;
                        }
                        BUPPlayer.FalseAwakening = true;           
                    }
                    else if (player.statLife > player.statLifeMax2 / 4)
                    {
                        BUPPlayer.FalseAwakening = false;
                        BUPPlayer.FalseReaction = false;
                    }
                }
            }

            if (NPC.downedBoss3)
            {
                BUPPlayer.PreFalseCheck = false;
            }

            if (EvilSaiyanAchieved)
            {
                EvilMultiplier = 0;
                if (NPC.downedBoss3)
                {
                    EvilMultiplier = 0.5f;
                    if (NPC.downedPlantBoss)
                    {
                        EvilMultiplier = 1.5f;
                        if (NPC.downedMoonlord)
                        {
                            EvilMultiplier = 2.75f;
                        }
                    }
                }
            }

            if (FSSJAchieved && !FSSJUnlockMsg)
            {
                FSSJUnlockMsg = true;
                Main.NewText("The anger fills your body!", Color.DarkGoldenrod);
            }

            if (EvilSaiyanAchieved && !EvilSaiyanUnlockMsg)
            {
                EvilSaiyanUnlockMsg = true;
                if (player.GetModPlayer<GPlayer>().Trait == "Ancient")
                {
                    Main.NewText("Evil Ki overwhelms you!", new Color(137, 29, 35));
                }
            }
            /*
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.BERSERKERBuff>()) >= 0.15f)
            {
                ConBERSERKERAchieved = true;
            }
            if (ConBERSERKERAchieved && !ConBERSERKERUnlockMsg)
            {
                ConBERSERKERUnlockMsg = true;
                Main.NewText("Corrupted ki is under your control!", new Color(10, 24, 131));
            }
            */
            if (IKARIAchieved && !IKARIUnlockMsg)
            {
                IKARIUnlockMsg = true;
                Main.NewText("Your rage is uncontrollable!", Color.LawnGreen); //Rage has opened up new possibilities for you!
            }

            if (player.GetModPlayer<MyPlayer>().masteryLevel1 >= 1f && !SSJFPUnlockMsg)
            {
                SSJFPAchieved = true;
            }

            if (SSJFPAchieved && !SSJFPUnlockMsg && player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSJFPBuff>()) == 0f)
            {
                SSJFPUnlockMsg = true;
                Main.NewText("You have mastered SSJ1.\nHold charge and transform while in USSJ \nto use Super Saiyan Full Power.", Color.Gold);
            }

            if (SSJRGAchieved && !SSJRGUnlockMsg)
            {
                SSJRGUnlockMsg = true;
                //Main.NewText("", Color.Yellow);
            }

            if (SSJ3FPAchieved && !SSJ3FPUnlockMsg)
            {
                SSJ3FPUnlockMsg = true;                
            }

            if (player.GetModPlayer<MyPlayer>().masteryLevelBlue >= 1f && !PSSBUnlockMsg)
            {
                PSSBAchieved = true;
                SSBEAchieved = true;
                SSBKKAchieved = true;
            }

            if (PSSBAchieved && !PSSBUnlockMsg && player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<PSSBBuff>()) == 0f)
            {
                PSSBUnlockMsg = true;
                Main.NewText("You have perfected SSJB.\nHold charge and transform while in SSJB \nto use Perfected Super Saiyan Blue.", Color.LightSkyBlue);
                TransformationHandler.ClearTransformations(Player);
                TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("PSSBBuff").Value);
            }
            /*
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.PSSBBuff>()) >= 1f && (!SSBEUnlockMsg || !SSBKKUnlockMsg))
            {
                SSBKKAchieved = true;
                SSBEAchieved = true;
            }
            */
            if (SSBEAchieved && !SSBEUnlockMsg)
            {
                SSBEUnlockMsg = true;
                //Main.NewText("Your pride unlocked your true potential. Super Saiyan Blue Evolution has been achieved!", new Color(0, 40, 163));
            }

            if (SSBKKAchieved && !SSBKKUnlockMsg)
            {
                SSBKKUnlockMsg = true;
                //Main.NewText("Now you are able to combine your strongest form with old techique!", Color.Aquamarine);
            }

            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSBKKBuff>()) >= 0.5f)
            {
                SSBKKx20Achieved = true;
            }

            if (SSBKKx20Achieved && !SSBKKx20UnlockMsg && player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSBKKx20Buff>()) == 0f)
            {
                SSBKKx20UnlockMsg = true;
                Main.NewText("Now you can use Kaio-Ken x20 with SSJB.\nHold charge and transform while in SSJBKKx10", Color.Aquamarine);
            }

            if (Player.HasBuff<SSBEBuff>() || Player.HasBuff<SSBKKx20Buff>())
            {
                lightningFrameTimer++;
            }
            if (lightningFrameTimer >= 15)
            {
                lightningFrameTimer = 0;
            }
            if (isTransformationAnimationPlaying)
            {
                Player.velocity = new Vector2(0f, Player.velocity.Y);
                transformationFrameTimer++;
                return;
            }
            transformationFrameTimer = 0;

            if (EvilPowerTimer > 0)
            {
                EvilPowerTimer--;
            }
            if (EvilPowerTimer == 0 & EvilPower > 0f)
            {
                EvilPower -= 0.001f;
            }
            if (EvilPower < 0f)
            {
                EvilPower = 0f;
            }
            if (EvilPower > 1f)
            {
                EvilPower = 1f;
            }
        }

        public override void PostUpdateEquips()
        {
            Player player = Main.player[Main.myPlayer];

            if (player.GetModPlayer<GPlayer>().Trait == "Ancient")
            {
                if (Player.HasBuff<SSJGBuff>() || Player.HasBuff<SSJBBuff>() || Player.HasBuff<SSJRBuff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    Main.NewText("Ancient Trait doesn't have access to god forms", Color.Gray);
                }
            }
            /*
            if (isAncient && EvilSaiyanActive)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (!Main.player[i].active || Main.player[i].whoAmI == Player.whoAmI)
                        continue;
                    if ((Main.player[i].Center - Player.Center).Length() <= 2000)
                    {
                        if (!Main.player[i].HasBuff<BERSERKERBuff>())
                        {
                            Main.player[i].AddBuff(ModContent.BuffType<BERSERKERBuff>(), 666666);
                            //TransformationHandler.Transform(Main.player[i], TransformationHandler.GetTransformation("BERSERKERBuff").Value);
                        }
                    }
                    else if (Main.player[i].HasBuff<BERSERKERBuff>())
                    {
                        int index = Main.player[i].FindBuffIndex(ModContent.BuffType<BERSERKERBuff>());
                        Main.player[i].DelBuff(index);
                    }
                }
            }
            */
            if (player.GetModPlayer<GPlayer>().Trait != "Ancient")
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (!Main.player[i].active || Main.player[i].whoAmI == Player.whoAmI)
                        continue;
                    if ((Main.player[i].Center - Player.Center).Length() <= 2000)
                    {
                        if (Main.player[i].HasBuff<EvilSaiyanBuff>() && !player.HasBuff<BERSERKERBuff>())
                        {
                            //Main.player[i].AddBuff(ModContent.BuffType<BERSERKERBuff>(), 666666);
                            TransformationHandler.Transform(player, TransformationHandler.GetTransformation("BERSERKERBuff").Value);
                            EvilSaiyanNearby = true;
                            //TransformationHandler.Transform(Main.player[i], TransformationHandler.GetTransformation("BERSERKERBuff").Value);
                        }
                        else
                        {
                            EvilSaiyanNearby = false;
                        }
                    }
                    else 
                    {
                        if (player.HasBuff<BERSERKERBuff>())
                        {
                            int index = player.FindBuffIndex(ModContent.BuffType<BERSERKERBuff>());
                            player.DelBuff(index);
                        }
                        
                    }
                }
            }

            if (EvilSaiyanAchieved)
            {
                player.statLifeMax2 += 50;
                player.GetModPlayer<MyPlayer>().kiMax2 += 500;
            }
            if (SuperFragment)
            {
                if (player.GetModPlayer<GPlayer>().Trait == "Legendary")
                {
                    player.GetModPlayer<MyPlayer>().kiMax2 += 2000;
                }
                else
                {
                    player.GetModPlayer<MyPlayer>().kiMax2 += 1000;
                }
            }
            if (SuperScroll)
            {
                player.GetModPlayer<MyPlayer>().kiChargeRate += 3;
            }

            if (player.HasBuff<ASSJBuff>())
            {
                player.GetModPlayer<BUPPlayer>().FormCD = true;
            }
            if (player.HasBuff<USSJBuff>())
            {
                player.GetModPlayer<BUPPlayer>().FormCD = false;
            }
            if (!player.HasBuff<IKARIBuff>())
            {
                IkariTimer = 1.0f;
                RageGain = 0;
            }
            if (player.HasBuff<IKARIBuff>() && IkariTimer < 2f)
            {
                RageGain++;
                if (RageGain > 900)
                {
                    SoundEngine.PlaySound(new SoundStyle("DBZMODPORT/Sounds/Explosion").WithVolumeScale(3f).WithPitchOffset(1f), player.position);

                    for (int i = 0; i < 55; i++)
                    {
                        Dust dust;
                        Vector2 position = Main.LocalPlayer.Top;
                        dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 2, 0f, 0f, 0, new Color(0, 255, 44), 1f)];
                        dust.noLight = true;
                    }
                       
                    IkariTimer += 0.1f;
                    RageGain = 0;
                }
            }
            else if (player.HasBuff<IKARIBuff>() && IkariTimer >= 2f && !player.HasBuff<FPCooldown>())
            {
                TransformationHandler.ClearTransformations(Player);
                SoundEngine.PlaySound(new SoundStyle("DBZMODPORT/Sounds/Awakening").WithVolumeScale(2f).WithPitchOffset(0f), player.position);
                TransformationHandler.Transform(player, TransformationHandler.GetTransformation("FPSSJBuff").Value);
                Main.NewText("You have entered Full Power Super Saiyan", Color.DarkGreen);
            }
            if (!player.GetModPlayer<BUPPlayer>().DragonBallEquipped && player.HasBuff<DBEnchancedBuff>())
            {
                int index = player.FindBuffIndex(ModContent.BuffType<DBEnchancedBuff>());
                player.DelBuff(index);
            }
        }

        public override void ResetEffects()
        {
            CrimsonMaskEqpd = false;
            DragonBallEquipped = false;
            FormCD = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Main.myPlayer];

            if (player.HasBuff<SSJ3FPBuff>()) 
            {
                int kiheal = new Random().Next(30, 85);
                if (EvilPower < 1f)
                {
                EvilPower += (float)(new Random().Next(4, 9) / 100.0); //0.06f
                }
                EvilPowerTimer = 420; //7 seconds
                player.GetModPlayer<MyPlayer>().AddKi((float)(kiheal), false, false);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), kiheal, false, false);
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (PreFalseCheck)
            {
                if (Main.rand.NextBool(12))
                {
                    FalseReaction = true;
                }
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            Player player = Main.player[Main.myPlayer];

            if (CrimsonMaskEqpd && !Player.HasBuff<MaskCooldown>()) 
            {
                int healamount = (Player.statLifeMax2)/4;
                Player.statLife += (healamount);
                Player.HealEffect(healamount, true);
                Player.AddBuff(ModContent.BuffType<XenoRegen>(), 2400);
                Player.AddBuff(ModContent.BuffType<MaskCooldown>(), 7200);
                return false; 
            }
            if (player.GetModPlayer<BUPPlayer>().FalseAwakening)
            {
                player.GetModPlayer<BUPPlayer>().FSSJAchieved = true;
                Player.statLife = Player.statLifeMax2 / 3;
                TransformationHandler.ClearTransformations(Player);
                TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("FSSJBuff").Value);
                player.GetModPlayer<BUPPlayer>().FalseAwakening = false;
                player.GetModPlayer<BUPPlayer>().FalseReaction = false;
                return false;
            }
            return true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("isAncient", isAncient);
            tag.Add("isMajin", isMajin);
            tag.Add("hasBuffs", hasBuffs);
            tag.Add("RageSign", RageSign);
            tag.Add("EvilMultiplier", EvilMultiplier);
            tag.Add("IkariTimer", IkariTimer);

            tag.Add("SuperScroll", SuperScroll);
            tag.Add("SuperFragment", SuperFragment);

            tag.Add("SSBETES_FSSJAchieved", FSSJAchieved);
            tag.Add("SSBETES_FSSJUnlockMsg", FSSJUnlockMsg);

            tag.Add("SSBETES_IKARIAchieved", IKARIAchieved);
            tag.Add("SSBETES_IKARIUnlockMsg", IKARIUnlockMsg);

            tag.Add("SSBETES_EvilSaiyanAchieved", EvilSaiyanAchieved);
            tag.Add("SSBETES_EvilSaiyanUnlockMsg", EvilSaiyanUnlockMsg);

            tag.Add("SSBETES_BERSERKERAchieved", BERSERKERAchieved);
            tag.Add("SSBETES_BERSERKERUnlockMsg", BERSERKERUnlockMsg);

            tag.Add("SSBETES_FPSSJAchieved", FPSSJAchieved);
            tag.Add("SSBETES_FPSSJUnlockMsg", FPSSJUnlockMsg);

            tag.Add("SSBETES_SSJFPAchieved", SSJFPAchieved);
            tag.Add("SSBETES_SSJFPUnlockMsg", SSJFPUnlockMsg);

            tag.Add("SSBETES_SSJRGAchieved", SSJRGAchieved);
            tag.Add("SSBETES_SSJRGUnlockMsg", SSJRGUnlockMsg);

            tag.Add("SSBETES_SSJ3FPAchieved", SSJ3FPAchieved);
            tag.Add("SSBETES_SSJ3FPUnlockMsg", SSJ3FPUnlockMsg);

            tag.Add("SSBETES_PSSBAchieved", PSSBAchieved);
            tag.Add("SSBETES_PSSBUnlockMsg", PSSBUnlockMsg);

            tag.Add("SSBETES_SSBEAchieved", SSBEAchieved);
            tag.Add("SSBETES_SSBEUnlockMsg", SSBEUnlockMsg);

            tag.Add("SSBETES_SSBKKAchieved", SSBKKAchieved);
            tag.Add("SSBETES_SSBKKUnlockMsg", SSBKKUnlockMsg);

            if (originalSkinColor != null)
            {
                tag.Add("OriginalSkinColorR", originalSkinColor.Value.R);
                tag.Add("OriginalSkinColorG", originalSkinColor.Value.G);
                tag.Add("OriginalSkinColorB", originalSkinColor.Value.B);
            }
        }

        public override void LoadData(TagCompound tag)
        {

            isAncient = tag.GetBool("isAncient");
            isMajin = tag.GetBool("isMajin");
            hasBuffs = tag.GetBool("hasBuffs");
            RageSign = tag.GetBool("RageSign");
            EvilMultiplier = tag.GetFloat("EvilMultiplier");
            IkariTimer = tag.GetFloat("IkariTimer");

            SuperFragment = tag.GetBool("SuperFragment");
            SuperScroll = tag.GetBool("SuperScroll");

            if (tag.ContainsKey("SSBETES_FSSJAchieved"))
                FSSJAchieved = tag.GetBool("SSBETES_FSSJAchieved");
            if (tag.ContainsKey("SSBETES_FSSJUnlockMsg"))
                FSSJUnlockMsg = tag.GetBool("SSBETES_FSSJUnlockMsg");

            if (tag.ContainsKey("SSBETES_IKARIAchieved"))
                IKARIAchieved = tag.GetBool("SSBETES_IKARIAchieved");
            if (tag.ContainsKey("SSBETES_IKARIUnlockMsg"))
                IKARIUnlockMsg = tag.GetBool("SSBETES_IKARIUnlockMsg");

            if (tag.ContainsKey("SSBETES_BERSERKERAchieved"))
                BERSERKERAchieved = tag.GetBool("SSBETES_BERSERKERAchieved");
            if (tag.ContainsKey("SSBETES_BERSERKERUnlockMsg"))
                BERSERKERUnlockMsg = tag.GetBool("SSBETES_BERSERKERUnlockMsg");

            if (tag.ContainsKey("SSBETES_EvilSaiyanAchieved"))
                EvilSaiyanAchieved = tag.GetBool("SSBETES_EvilSaiyanAchieved");
            if (tag.ContainsKey("SSBETES_EvilSaiyanUnlockMsg"))
                EvilSaiyanUnlockMsg = tag.GetBool("SSBETES_EvilSaiyanUnlockMsg");

            if (tag.ContainsKey("SSBETES_FPSSJAchieved"))
                FPSSJAchieved = tag.GetBool("SSBETES_FPSSJAchieved");
            if (tag.ContainsKey("SSBETES_FPSSJUnlockMsg"))
                FPSSJUnlockMsg = tag.GetBool("SSBETES_FPSSJUnlockMsg");

            if (tag.ContainsKey("SSBETES_SSJFPAchieved"))
                SSJFPAchieved = tag.GetBool("SSBETES_SSJFPAchieved");
            if (tag.ContainsKey("SSBETES_SSJFPUnlockMsg"))
                SSJFPUnlockMsg = tag.GetBool("SSBETES_SSJFPUnlockMsg");

            if (tag.ContainsKey("SSBETES_SSJRGAchieved"))
                SSJRGAchieved = tag.GetBool("SSBETES_SSJRGAchieved");
            if (tag.ContainsKey("SSBETES_SSJRGUnlockMsg"))
                SSJRGUnlockMsg = tag.GetBool("SSBETES_SSJRGUnlockMsg");

            if (tag.ContainsKey("SSBETES_SSJ3FPAchieved"))
                SSJ3FPAchieved = tag.GetBool("SSBETES_SSJ3FPAchieved");
            if (tag.ContainsKey("SSBETES_SSJ3FPUnlockMsg"))
                SSJ3FPUnlockMsg = tag.GetBool("SSBETES_SSJ3FPUnlockMsg");

            if (tag.ContainsKey("SSBETES_PSSBAchieved"))
                PSSBAchieved = tag.GetBool("SSBETES_PSSBAchieved");
            if (tag.ContainsKey("SSBETES_PSSBUnlockMsg"))
                PSSBUnlockMsg = tag.GetBool("SSBETES_PSSBUnlockMsg");

            if (tag.ContainsKey("SSBETES_SSBEAchieved"))
                SSBEAchieved = tag.GetBool("SSBETES_SSBEAchieved");
            if (tag.ContainsKey("SSBETES_SSBEUnlockMsg"))
                SSBEUnlockMsg = tag.GetBool("SSBETES_SSBEUnlockMsg");

            if (tag.ContainsKey("SSBETES_SSBKKAchieved"))
                SSBKKAchieved = tag.GetBool("SSBETES_SSBKKAchieved");
            if (tag.ContainsKey("SSBETES_SSBKKUnlockMsg"))
                SSBKKUnlockMsg = tag.GetBool("SSBETES_SSBKKUnlockMsg"); 
        }

        public override void OnEnterWorld()
        {
            TraitHandler.RollTrait(true);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            Player player = Main.player[Main.myPlayer];
            var CallMyPlayer = player.GetModPlayer<MyPlayer>();
            var CallGPlayer = player.GetModPlayer<GPlayer>();

            if (TransformationHandler.EnergyChargeKey.Current && TransformationHandler.TransformKey.JustPressed)
            {
                if (SSJFPAchieved && !FormCD && !Player.HasBuff<SSJFPBuff>() && Player.HasBuff<USSJBuff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSJFPBuff").Value);
                    FormCD = true;
                }
                if (SSJRGAchieved && !FormCD && !Player.HasBuff<SSJRGBuff>() && Player.HasBuff<SSJ2Buff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSJRGBuff").Value);
                }
                if (SSJ3FPAchieved && !FormCD && !Player.HasBuff<SSJ3FPBuff>() && Player.HasBuff<SSJ3Buff>() && CallGPlayer.Trait == "Ancient")
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSJ3FPBuff").Value);
                }
                if (PSSBAchieved && !FormCD && !Player.HasBuff<PSSBBuff>() && Player.HasBuff<SSJBBuff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("PSSBBuff").Value);
                }
                if (SSBKKx20Achieved && !FormCD && Player.HasBuff<SSBKKBuff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSBKKx20Buff").Value);
                }
            }
            if (TransformationHandler.TransformKey.JustPressed)
            {
                if (CallMyPlayer.masteryLevel1 != 0 && !FormCD && !Player.HasBuff<SSJ1Buff>() && Player.HasBuff<FSSJBuff>())
                {
                    TransformationHandler.ClearTransformations(Player);
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSJ1Buff").Value);
                }
                if ((CallMyPlayer.masteryLevel2 != 0 || CallMyPlayer.masteryLevelLeg != 0) && !FormCD && (!Player.HasBuff<SSJ2Buff>() || !Player.HasBuff<LSSJBuff>()) && Player.HasBuff<SSJFPBuff>())
                {
                    if (CallGPlayer.Trait != "Legendary")
                    {
                        TransformationHandler.ClearTransformations(Player);
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSJ2Buff").Value);
                    }
                    if (CallGPlayer.Trait == "Legendary")
                    {
                        TransformationHandler.ClearTransformations(Player);
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("LSSJBuff").Value);
                    }
                }
                if (!FormCD && (!Player.HasBuff<SSBEBuff>() || !Player.HasBuff<SSBKKBuff>()) && Player.HasBuff<PSSBBuff>())
                {
                    if (SSBEUnlockMsg)
                    {
                        TransformationHandler.ClearTransformations(Player);
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSBEBuff").Value);
                    }
                    if (SSBKKUnlockMsg)
                    {
                        TransformationHandler.ClearTransformations(Player);
                        TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("SSBKKBuff").Value);
                    }
                }
            }

            if (Ability.JustPressed)
            {
                if (CallGPlayer.Trait == "Legendary")
                {
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("LimitBreakerBuff").Value);
                }
                if (CallGPlayer.Trait == "Ancient")
                {
                    TransformationHandler.Transform(Player, TransformationHandler.GetTransformation("EvilSaiyanBuff").Value);
                }
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (hair != null && !drawInfo.fullHair)
            {
                drawInfo.drawPlayer.head = 0;
            }
            
            if (isAncient && !TransformationHandler.IsTransformed(Player))
            {
                ChangeEyeColor(Color.Red);
                return;
            }
            
            if (isMajin)
            {
                ChangeEyeColor(Color.Black);
                ChangeSkinColor(Color.HotPink);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (Player.HasBuff<FSSJBuff>())
            {
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (EvilSaiyanActive)
            {
                ChangeEyeColor(Color.Red);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            /*
            if (BERSERKERActive)
            {
                ChangeEyeColor(Color.White);
                return;
            }
            */
            if (IKARIActive)
            {
                ChangeEyeColor(Color.Yellow);
                return;
            }
            if (FPSSJActive)
            {
                drawInfo.colorHair = Color.Transparent;
                ChangeEyeColor(Color.White);
                return;
            }
            if (SSJFPActive)
            {
                drawInfo.colorHair = Color.Transparent;
                ChangeEyeColor(Color.Turquoise);
                return;
            }
            if (SSJRGActive)
            {
                drawInfo.colorHair = Color.Transparent;
                ChangeEyeColor(Color.DeepSkyBlue);
                return;
            }
            if (SSJ3FPActive)
            {
                ChangeEyeColor(Color.Red);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (PSSBActive)
            {
                ChangeEyeColor(Color.Aqua);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (SSBKKActive)
            {
                ChangeEyeColor(Color.Aquamarine);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (SSBKKx20Active)
            {
                ChangeEyeColor(Color.Aquamarine);
                drawInfo.colorHair = Color.Transparent;
                return;
            }
            if (SSBEActive)
            {
                ChangeEyeColor(Color.Blue);
                drawInfo.colorHair = Color.Transparent;
                return;
            }

            if (originalSkinColor != null && Player.skinColor != originalSkinColor.Value)
            {
                Player.skinColor = originalSkinColor.Value;
            }
        }

        public void ChangeEyeColor(Color eyeColor)
        {
            if (!originalEyeColor.HasValue)
            {
                originalEyeColor = Player.eyeColor;
            }
            Player.eyeColor = eyeColor;
        }

        public void ChangeSkinColor(Color skinColor)
        {
            if (originalSkinColor == null)
            {
                originalSkinColor = new Color?(Player.skinColor);
            }
            Player.skinColor = skinColor;
        }
        public void SSJ3FPAnimation()
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Player.Center.X - 40f, Player.Center.Y + 70f, 0f, 0f, ModContent.ProjectileType<SSJ3FPSequence>(), 0, 0f, Player.whoAmI, 0f, 0f, 0f);
            SoundHelper.PlayCustomSound("DBZMODPORT/Sounds/GroundRumble", Player, 3f, 0f);
        }
        public void SSJRageAnimation()
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Player.Center.X - 40f, Player.Center.Y + 70f, 0f, 0f, ModContent.ProjectileType<SSJRageSequence>(), 0, 0f, Player.whoAmI, 0f, 0f, 0f);
            SoundHelper.PlayCustomSound("DBZMODPORT/Sounds/GroundRumble", Player, 3f, 0f);
        }
        public float GetEvilAmount()
        {
            return EvilMultiplier;
        }
        public float GetEvilPower()
        {
            return EvilPower;
        }
        public float GetIkariTimer()
        {
            return IkariTimer;
        }
        public float RandomFormDrain()
        {
            List<float> random = new List<float> { -0.5f, -0.45f, -0.4f, -0.35f, -0.3f, -0.25f, -0.2f, -0.15f,
                -0.1f, -0.05f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.45f,
                0.5f, 0.55f, 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f, 1.0f, 1.05f,
                1.1f, 1.15f, 1.2f, 1.25f, 1.3f, 1.35f, 1.4f, 1.45f, 1.5f, 1.55f, 1.6f, 1.65f,
                1.7f, 1.75f, 1.8f, 1.85f, 1.9f, 1.95f, 2.0f, 2.05f, 2.1f, 2.15f, 2.2f, 2.25f,
                2.3f, 2.35f, 2.4f, 2.45f, 2.5f, 2.55f, 2.6f, 2.65f, 2.7f, 2.75f, 2.8f, 2.85f,
                2.9f, 2.95f, 3.0f, 3.05f, 3.1f, 3.15f, 3.2f, 3.25f, 3.3f, 3.35f, 3.4f, 3.45f, 3.5f };
            return random[Main.rand.Next(79)];
        }
        public float RandomMasteredFormDrain()
        {
            List<float> random = new List<float> { -1.0f, -0.95f, -0.9f, -0.85f, -0.8f, -0.75f, -0.7f,
                -0.65f, -0.6f, -0.55f, -0.5f, -0.45f, -0.4f, -0.35f, -0.3f,
                -0.25f, -0.2f, -0.15f, -0.1f, -0.05f, 0.05f, 0.1f,
                0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.45f, 0.5f, 0.55f,
                0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f, 1.0f,
                1.05f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f, 1.35f, 1.4f, 1.45f,
                1.5f, 1.55f, 1.6f, 1.65f, 1.7f, 1.75f, 1.8f, 1.85f, 1.9f, 1.95f, 2.0f };
            return random[Main.rand.Next(59)];
        }
        public float RandomAttackDrain()
        {
            List<float> random = new List<float> { 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f, 1f, 1.05f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f, 1.35f };
            return random[Main.rand.Next(13)];
        }
        public float RandomAttackBonus()
        {
            List<float> random = new List<float> { -1f, -0.2f, -0.15f, -0.1f, -0.05f, 0f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.45f };
            return random[Main.rand.Next(13)];
        }

        public class SEPBSSFPanel : TransformationTree
        {
            public override bool Complete() => true;

            public override bool Condition(Player player)
            {
                return !ConfigServer.Instance.FalsePrework;
            }

            public override Connection[] Connections() => new Connection[]
            {
                //new Connection(0.5f, 2, 1.5f, false, new Gradient(Color.DarkGoldenrod).AddStop(0.65f, new Color(247, 232, 31))),
                //new Connection(2, 2, 1, true, new Gradient(Color.Yellow).AddStop(0.65f, new Color(240, 237, 126))),             
                //new Connection(2, 2, 1.5f, false, new Gradient(Color.Yellow).AddStop(0.65f, new Color(71, 164, 219))),
                //new Connection(3.5f, 2, 3f, false, new Gradient(Color.Cyan).AddStop(0.65f, new Color(65,105,225))),
                //new Connection(5f, 1, 2f, true, new Gradient(Color.Blue).AddStop(0.65f, new Color(0, 174, 152))),

                new Connection(0.5f, 2, 1.5f, false, new Gradient(Color.Yellow).AddStop(0.5f, new Color(240, 237, 126))),
                new Connection(2, 2, 1.5f, false, new Gradient(new Color(240, 237, 126)).AddStop(0.85f, new Color(71, 164, 219))),
                new Connection(3.5f, 2, 3f, false, new Gradient(Color.Cyan).AddStop(0.8f, new Color(65,105,225))),
                new Connection(5f, 1, 2f, true, new Gradient(Color.Blue).AddStop(0.5f, new Color(0, 174, 152))),
            };

            public override string Name() => "SSBE - Bonus Forms"; //Bonus Forms
            
            public override Node[] Nodes() => new Node[]
            {
                //new Node(0.5f, 2, "FSSJBuff", "SSBETES/Buffs/Transformations/FSSJBuff", "Fight with world evil so anger opens up new opportunities for you.",UnlockConditionFSSJ,DiscoverConditionFSSJ),
                new Node(0.5f, 2, "SSJ1Buff", "DBZMODPORT/Buffs/SSJBuffs/SSJ1Buff", "Only through failure with a powerful foe will true power awaken.",UnlockConditionSSJ1,DiscoverConditionSSJ1),
                new Node(2, 2, "SSJFPBuff", "SSBETES/Buffs/Transformations/SSJFPBuff", "Master Super Saiyan.",UnlockConditionSSJFP,DiscoverConditionSSJFP),
                new Node(3.5f, 2, "SSJRGBuff", "SSBETES/Buffs/Transformations/SSJRGBuff", "Defeating the lunar cult after slime royalty will show you the path.",UnlockConditionSSJRG,DiscoverConditionSSJRG),
                new Node(5f, 2, "SSJBBuff", "DBZMODPORT/Buffs/SSJBuffs/SSJBBuff", "The experience from battling a galactic being could bring forth this strength.",UnlockConditionSSJB,DiscoverConditionSSJB),          
                new Node(6.5f, 2, "PSSBBuff", "SSBETES/Buffs/Transformations/PSSBBuff", "Perfect your strongest form.",UnlockConditionPSSB,DiscoverConditionPSSB),
                new Node(5f, 1, "SSBEBuff", "SSBETES/Buffs/Transformations/SSBEBuff", "Mastering the divine form will open you to a choice.",UnlockConditionSSBE,DiscoverConditionSSBE), //Only gifted warriors can achieve such strength through training.
                new Node(5f, 3, "SSBKKBuff", "SSBETES/Buffs/Transformations/SSBKKBuff", "Mastering the divine form will open you to a choice.",UnlockConditionSSBKK,DiscoverConditionSSBKK), //Train harder in your god form.
                new Node(1, 0, "IKARIBuff", "SSBETES/Buffs/Transformations/IKARIBuff", "Destroying the seal between light and dark will grant access to the wild power.",UnlockConditionIKARI,DiscoverConditionIKARI),
                new Node(2, 0, "LimitBreakerBuff", "SSBETES/Buffs/Transformations/LimitBreakerBuff", "After losing control, a new power will unveil to you",UnlockConditionnLimitBreak,DiscoverConditionLimitBreak), //Only gifted warriors can achieve such strength through training.
                new Node(5, 0, "EvilSaiyanBuff", "SSBETES/Buffs/Transformations/EvilSaiyanBuff", "Cthulhu creature may reveal an ancient power.",UnlockConditionEvilSaiyan,DiscoverConditionEvilSaiyan), //Train harder in your god form.
                new Node(6, 0, "SSJ3FPBuff", "SSBETES/Buffs/Transformations/SSJ3FPBuff", "Cosmic creatures will push you to the limit",UnlockConditionSSJ3FP,DiscoverConditionSSJ3FP),
            };

            public bool UnlockConditionSSJ1(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<MyPlayer>().masteryLevel1 != 0;
            }

            public bool DiscoverConditionSSJ1(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin";
            }

            public bool UnlockConditionSSJB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0;
            }

            public bool DiscoverConditionSSJB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && player.GetModPlayer<MyPlayer>().masteryLevelGod != 0;
            }

            public bool UnlockConditionFSSJ(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().FSSJAchieved;
            }

            public bool DiscoverConditionFSSJ(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin";
            }

            public bool UnlockConditionIKARI(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary" && player.GetModPlayer<BUPPlayer>().IKARIAchieved;
            }

            public bool DiscoverConditionIKARI(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary";
            }

            public bool UnlockConditionnLimitBreak(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary" && player.GetModPlayer<MyPlayer>().masteryLevelLeg2 >= 1;
            }

            public bool DiscoverConditionLimitBreak(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary";
            }

            public bool UnlockConditionEvilSaiyan(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<BUPPlayer>().EvilSaiyanAchieved;
            }

            public bool DiscoverConditionEvilSaiyan(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient";
            }

            public bool UnlockConditionSSJ3FP(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<BUPPlayer>().SSJ3FPAchieved;
            }

            public bool DiscoverConditionSSJ3FP(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<MyPlayer>().masteryLevel3 != 0;
            }

            public bool UnlockConditionSSJFP(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSJFPAchieved;
            }

            public bool DiscoverConditionSSJFP(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<MyPlayer>().masteryLevel1 != 0;
            }

            public bool UnlockConditionSSJRG(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Ancient" && player.GetModPlayer<BUPPlayer>().SSJRGAchieved;
            }

            public bool DiscoverConditionSSJRG(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Ancient" && (player.GetModPlayer<BUPPlayer>().SSJRGAchieved || player.GetModPlayer<MyPlayer>().masteryLevel2 != 0);
            }

            public bool UnlockConditionPSSB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().PSSBAchieved;
            }

            public bool DiscoverConditionPSSB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || player.GetModPlayer<BUPPlayer>().PSSBAchieved);
            }

            public bool UnlockConditionSSBE(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSBEAchieved && !player.GetModPlayer<BUPPlayer>().KaioLock;
            }

            public bool DiscoverConditionSSBE(Player player)
            {
                var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || BUPPlayer.PSSBAchieved || BUPPlayer.SSBEAchieved);
            }

            public bool UnlockConditionSSBKK(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSBKKAchieved && player.GetModPlayer<MyPlayer>().kaioAchieved && !player.GetModPlayer<BUPPlayer>().EvoLock;
            }

            public bool DiscoverConditionSSBKK(Player player)
            {
                var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || BUPPlayer.PSSBAchieved || BUPPlayer.SSBKKAchieved) && player.GetModPlayer<MyPlayer>().kaioAchieved;
            }

        }

        public class FSSJPanel : TransformationTree
        {
            public override bool Complete() => true;

            public override bool Condition(Player player)
            {
                return ConfigServer.Instance.FalsePrework;
            }

            public override Connection[] Connections() => new Connection[]
            {
                new Connection(0.5f, 2, 1, true, new Gradient(Color.DarkGoldenrod).AddStop(0.5f, new Color(235,196,10))),
                new Connection(0.5f, 2, 1.5f, false, new Gradient(Color.Yellow).AddStop(0.5f, new Color(240, 237, 126))),
                new Connection(2, 2, 1.5f, false, new Gradient(new Color(240, 237, 126)).AddStop(0.85f, new Color(71, 164, 219))),
                new Connection(3.5f, 2, 3f, false, new Gradient(Color.Cyan).AddStop(0.8f, new Color(65,105,225))),
                new Connection(5f, 1, 2f, true, new Gradient(Color.Blue).AddStop(0.5f, new Color(0, 174, 152))),
            };

            public override string Name() => "SSBE - Bonus Forms"; //Bonus Forms

            public override Node[] Nodes() => new Node[]
            {
                new Node(0.5f, 3, "FSSJBuff", "SSBETES/Buffs/Transformations/FSSJBuff", "Fight with world evil so anger opens up new opportunities for you.",UnlockConditionFSSJ,DiscoverConditionFSSJ),
                new Node(0.5f, 2, "SSJ1Buff", "DBZMODPORT/Buffs/SSJBuffs/SSJ1Buff", "Only through failure with a powerful foe will true power awaken.",UnlockConditionSSJ1,DiscoverConditionSSJ1),
                new Node(2, 2, "SSJFPBuff", "SSBETES/Buffs/Transformations/SSJFPBuff", "Master Super Saiyan.",UnlockConditionSSJFP,DiscoverConditionSSJFP),
                new Node(3.5f, 2, "SSJRGBuff", "SSBETES/Buffs/Transformations/SSJRGBuff", "Defeating the lunar cult after slime royalty will show you the path.",UnlockConditionSSJRG,DiscoverConditionSSJRG),
                new Node(5f, 2, "SSJBBuff", "DBZMODPORT/Buffs/SSJBuffs/SSJBBuff", "The experience from battling a galactic being could bring forth this strength.",UnlockConditionSSJB,DiscoverConditionSSJB),
                new Node(6.5f, 2, "PSSBBuff", "SSBETES/Buffs/Transformations/PSSBBuff", "Perfect your strongest form.",UnlockConditionPSSB,DiscoverConditionPSSB),
                new Node(5f, 1, "SSBEBuff", "SSBETES/Buffs/Transformations/SSBEBuff", "Mastering the divine form will open you to a choice.",UnlockConditionSSBE,DiscoverConditionSSBE), //Only gifted warriors can achieve such strength through training.
                new Node(5f, 3, "SSBKKBuff", "SSBETES/Buffs/Transformations/SSBKKBuff", "Mastering the divine form will open you to a choice.",UnlockConditionSSBKK,DiscoverConditionSSBKK), //Train harder in your god form.
                new Node(1, 0, "IKARIBuff", "SSBETES/Buffs/Transformations/IKARIBuff", "Destroying the seal between light and dark will grant access to the wild power.",UnlockConditionIKARI,DiscoverConditionIKARI),
                new Node(2, 0, "LimitBreakerBuff", "SSBETES/Buffs/Transformations/LimitBreakerBuff", "After losing control, a new power will unveil to you",UnlockConditionnLimitBreak,DiscoverConditionLimitBreak), //Only gifted warriors can achieve such strength through training.
                new Node(5, 0, "EvilSaiyanBuff", "SSBETES/Buffs/Transformations/EvilSaiyanBuff", "Cthulhu creature may reveal an ancient power.",UnlockConditionEvilSaiyan,DiscoverConditionEvilSaiyan), //Train harder in your god form.
                new Node(6, 0, "SSJ3FPBuff", "SSBETES/Buffs/Transformations/SSJ3FPBuff", "Cosmic creatures will push you to the limit",UnlockConditionSSJ3FP,DiscoverConditionSSJ3FP),
            };

            public bool UnlockConditionSSJ1(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<MyPlayer>().masteryLevel1 != 0;
            }

            public bool DiscoverConditionSSJ1(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin";
            }

            public bool UnlockConditionSSJB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0;
            }

            public bool DiscoverConditionSSJB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && player.GetModPlayer<MyPlayer>().masteryLevelGod != 0;
            }

            public bool UnlockConditionFSSJ(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().FSSJAchieved;
            }

            public bool DiscoverConditionFSSJ(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin";
            }

            public bool UnlockConditionIKARI(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary" && player.GetModPlayer<BUPPlayer>().IKARIAchieved;
            }

            public bool DiscoverConditionIKARI(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary";
            }

            public bool UnlockConditionnLimitBreak(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary" && player.GetModPlayer<MyPlayer>().masteryLevelLeg2 >= 1;
            }

            public bool DiscoverConditionLimitBreak(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Legendary";
            }

            public bool UnlockConditionEvilSaiyan(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<BUPPlayer>().EvilSaiyanAchieved;
            }

            public bool DiscoverConditionEvilSaiyan(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient";
            }

            public bool UnlockConditionSSJ3FP(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<BUPPlayer>().SSJ3FPAchieved;
            }

            public bool DiscoverConditionSSJ3FP(Player player)
            {
                return player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<MyPlayer>().masteryLevel3 != 0;
            }

            public bool UnlockConditionSSJFP(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSJFPAchieved;
            }

            public bool DiscoverConditionSSJFP(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && player.GetModPlayer<MyPlayer>().masteryLevel1 != 0;
            }

            public bool UnlockConditionSSJRG(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Ancient" && player.GetModPlayer<BUPPlayer>().SSJRGAchieved;
            }

            public bool DiscoverConditionSSJRG(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Majin" && GPlayer.Trait != "Ancient" && (player.GetModPlayer<BUPPlayer>().SSJRGAchieved || player.GetModPlayer<MyPlayer>().masteryLevel2 != 0);
            }

            public bool UnlockConditionPSSB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().PSSBAchieved;
            }

            public bool DiscoverConditionPSSB(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || player.GetModPlayer<BUPPlayer>().PSSBAchieved);
            }

            public bool UnlockConditionSSBE(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSBEAchieved && !player.GetModPlayer<BUPPlayer>().KaioLock;
            }

            public bool DiscoverConditionSSBE(Player player)
            {
                var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || BUPPlayer.PSSBAchieved || BUPPlayer.SSBEAchieved);
            }

            public bool UnlockConditionSSBKK(Player player)
            {
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && player.GetModPlayer<BUPPlayer>().SSBKKAchieved && player.GetModPlayer<MyPlayer>().kaioAchieved && !player.GetModPlayer<BUPPlayer>().EvoLock;
            }

            public bool DiscoverConditionSSBKK(Player player)
            {
                var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                var GPlayer = player.GetModPlayer<GPlayer>();

                return GPlayer.Trait != "Legendary" && GPlayer.Trait != "Primal" && GPlayer.Trait != "GOD" && GPlayer.Trait != "Ancient" && GPlayer.Trait != "Majin" && (player.GetModPlayer<MyPlayer>().masteryLevelBlue != 0 || BUPPlayer.PSSBAchieved || BUPPlayer.SSBKKAchieved) && player.GetModPlayer<MyPlayer>().kaioAchieved;
            }

        }
    }
}


