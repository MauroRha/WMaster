/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

//<!-- -------------------------------------------------------------------------------------------------------------------- -->
//<file>
//  <copyright file="GangMissionPettyTheft.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-13</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster.Concept.GangMission
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Concept;
    using WMaster.Concept.Attributs;
    using WMaster.Entity.Living;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Petty theft mission affecte to gang.
    /// </summary>
    public sealed class GangMissionPettyTheft : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionPettyTheft(Gang gang)
            : base(EnuGangMissions.PettyTheft, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.Global, "GangMissionPettyTheft");
        }

        /// <summary>
        /// Performe a gang petty theft mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString pettyTheftEven = new LocalString();
            pettyTheftEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsPerformingPettyTheft",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            Game.Player.disposition(-1);
            Game.Player.customerfear(1);
            Game.Player.suspicion(1);

            int gangMemberNumStart = this.GangCible.MemberNum;
            int gangMemberNumLost = 0;

            // `J` chance of running into a rival gang updated for .06.02.41
            int gangs = Game.Rivals.GetNumRivalGangs();
            int chance = 5 + Math.Max(20, gangs * 2); // 5% base +2% per gang, 25% max

            if (WMRand.Percent(chance))
            {
                cRival rival = Game.Rivals.GetRandomRivalWithGangs();
                if (rival != null && rival.m_NumGangs > 0)
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenRunIntoAGangFrom[RivalName]AndABrawlBreaksOut",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenRunIntoGroupOfThugsFromTheStreetsAndABrawlBreaksOut");
                }

                Gang rivalGang = GangManager.GetTempGang();
                if (GangManager.GangBrawl(this.GangCible, rivalGang, false))
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenWin");
                }
                else
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenLoseTheFight");
                    this.GangCible.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                    return false;
                }
                if (rival != null && rival.m_NumGangs > 0 && rivalGang.MemberNum <= 0)
                {
                    rival.m_NumGangs--;
                }
                rivalGang = null;

                gangMemberNumLost += gangMemberNumStart - this.GangCible.MemberNum;
            }
            else if (WMRand.Percent(1)) // `J` added for .06.02.41
            {
                sGirl girl = Game.Girls.GetRandomGirl();
                if (girl.has_trait("Incorporeal"))
                {
                    girl = Game.Girls.GetRandomGirl(); // try not to get an incorporeal girl but only 1 check
                }
                if (girl != null)
                {
                    string girlName = girl.m_Realname;
                    LocalString NGmsg = new LocalString();
                    ImageTypes girlImageType = ImageTypes.PROFILE;
                    EventType eventType = EventType.GANG;
                    EventType gangEventType = EventType.GANG;
                    DungeonReasons dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                    int damagedNets = 0;

                    // `J` make sure she is ready for a fight
                    if (girl.combat() < 50)
                    {
                        girl.combat(10 + WMRand.Random() % 30);
                    }
                    if (girl.magic() < 50)
                    {
                        girl.magic(10 + WMRand.Random() % 20);
                    }
                    if (girl.constitution() < 50)
                    {
                        girl.constitution(10 + WMRand.Random() % 20);
                    }
                    if (girl.agility() < 50)
                    {
                        girl.agility(10 + WMRand.Random() % 20);
                    }
                    if (girl.confidence() < 50)
                    {
                        girl.agility(10 + WMRand.Random() % 40);
                    }
                    girl.health(100);
                    girl.tiredness(-100);

                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenAreConfrontedByAMaskedVigilante");
                    if (!GangManager.GangCombat(girl, this.GangCible))
                    {
                        gangMemberNumLost += gangMemberNumStart - this.GangCible.MemberNum;
                        int goldWin = girl.m_Money > 0 ? girl.m_Money : WMRand.Random() % 100 + 1; // take all her money or 1-100 if she has none
                        girl.m_Money = 0;
                        Game.Gold.petty_theft(goldWin);

                        if (gangMemberNumLost > gangMemberNumStart / 2)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsWellButYourMenStillManageToCaptureHer");
                        }
                        else if (gangMemberNumLost == 0)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButLosesQuickly");
                        }
                        else if (gangMemberNumLost == 1)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButTheyTakeHerDownWithOnlyOneCasualty");
                        }
                        else
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButTheyTakeHerDownWithOnlyAFewCasualties");
                        }
                        pettyTheftEven.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheyUnmask[GirlName]TakeAllHerGold[Number]FromHerAndDragHerToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", goldWin) });
                        girlImageType = ImageTypes.DEATH;
                        dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                        girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                        girl.add_trait("Kidnapped", 5 + WMRand.Random() % 11);

                        // TODO : What to do with NGmsg ?!?
                        NGmsg.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GirlName]TriedToStop[GangName]FromComittingPettyTheftButLostSheWasDraggedBackToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.m_Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                        GangManager.BoostGangSkill(this.GangCible.Skills[EnumSkills.COMBAT], 1);

                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
                        {
                            Game.Brothels.GetObjective().m_SoFar += goldWin;
                        }
                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.KIDNAPXGIRLS))
                        {
                            Game.Brothels.GetObjective().m_SoFar++; // `J` You are technically kidnapping her
                        }
                        return true;
                    }
                    else
                    {
                        pettyTheftEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "SheDefeatsYourMenAndDisappearsBackIntoTheShadows");
                        this.GangCible.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                        return false;
                    }
                }
            }

            int difficulty = Math.Max(0, WMRand.Bell(1, 6) - 2); // 0-4
            string who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoPeople");
            int fightBackChance = 0;
            int numberOfTargets = 2 + WMRand.Random() % 9;
            int targetFight = numberOfTargets;
            int gold = 0;
            int goldBase = 1;

            if (difficulty <= 0)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoKids");
                fightBackChance = 50;
                goldBase += 20;
                difficulty = 0;
            }
            if (difficulty == 1)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoLittleOldLadies");
                fightBackChance = 40;
                goldBase += 40;
            }
            if (difficulty == 2)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoNobleMenAndWomen");
                fightBackChance = 30;
                goldBase += 60;
            }
            if (difficulty == 3)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoSmallStalls");
                fightBackChance = 50;
                goldBase += 80;
            }
            if (difficulty >= 4)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoTraders");
                fightBackChance = 70;
                goldBase += 100;
                difficulty = 4;
            }

            for (int i = 0; i < numberOfTargets; i++)
            {
                gold += WMRand.Random() % goldBase;
            }

            if (WMRand.Percent(fightBackChance)) // determine losses if they fight back
            {
                while (this.GangCible.MemberNum > 0 && targetFight > 0) // fight until someone wins
                {
                    if (WMRand.Percent(this.GangCible.Combat))
                    {
                        targetFight--; // you win so lower their numbers
                    }
                    else if (WMRand.Percent(WMRand.Random() % 11 + (difficulty * 10))) // or they win
                    {
                        if (this.GangCible.HealLimit > 0)
                        {
                            this.GangCible.AdjustHealLimit(-1);
                            GangManager.NumHealingPotions--;
                        } // but you heal
                        else
                        {
                            this.GangCible.MemberNum--;
                            gangMemberNumLost++;
                        } // otherwise lower your numbers
                    }
                }
            }

            if (this.GangCible.MemberNum <= 0)
            {
                return false; // they all died so return and the message will be taken care of in the losegang function
            }

            pettyTheftEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "YourGangRobs[NumberWho][Who]AndGet[NumberGold]GoldFromThem",
                new List<FormatStringParameter>() {
                    new FormatStringParameter("NumberWho", numberOfTargets),
                    new FormatStringParameter("Who", who),
                    new FormatStringParameter("NumberGold", gold)
                });
            if (gangMemberNumLost > 0)
            {
                if (gangMemberNumLost == 1)
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]LostOneMan",
                        new List<FormatStringParameter>() {
                            new FormatStringParameter("GangName", this.GangCible.Name)
                        });
                }
                else
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]Lost[Number]Men",
                        new List<FormatStringParameter>() {
                            new FormatStringParameter("GangName", this.GangCible.Name),
                            new FormatStringParameter("Number", gangMemberNumLost),
                        });
                }
            }

            this.GangCible.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);

            Game.Gold.petty_theft(gold);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
            {
                Game.Brothels.GetObjective().m_SoFar += gold;
            }
            return true;
        }
    }
}
