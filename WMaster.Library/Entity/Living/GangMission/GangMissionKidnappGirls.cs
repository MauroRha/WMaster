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
//  <copyright file="GangMissionKidnappGirls.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Entity.Living.GangMission
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Concept.Attributs;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// !kidnaping mission affecte to gang.
    /// </summary>
    public sealed class GangMissionKidnappGirls : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionKidnappGirls(Gang gang)
            : base(EnuGangMissions.KidnappGirls, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.GangMission, "MissionKidnappGirls");
        }

        /// <summary>
        /// Performe a gang kidnapping mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString kidnappMissionEvent = new LocalString();
            kidnappMissionEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.GangMission,
                "Gang[GangName]IsKidnappingGirls",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
            bool captured = false;

            if (WMRand.Percent(Math.Min(75, this.GangCible.Intelligence))) // chance to find a girl to kidnap
            {
                sGirl girl = Game.Girls.GetRandomGirl();
                if (girl != null)
                {
                    int[] v = { -1, -1 };
                    if (girl.m_Triggers.CheckForScript((int)Constants.TRIGGER_KIDNAPPED, true, v) != null)
                    {
                        return true; // not sure if they got the girl from the script but assume they do.
                    }

                    string girlName = girl.Realname;
                    LocalString NGmsg = new LocalString();
                    ImageType girlImageType = ImageType.PROFILE;
                    EventType eventType = EventType.Gang;
                    EventType gangEventType = EventType.Gang;
                    DungeonReasons dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                    int damagedNets = 0;


                    /* MYR: For some reason I can't figure out, a number of girl's house percentages
                    are at zero or set to zero when they are sent to the dungeon. I'm not sure
                    how to fix it, so I'm explicitly setting the percentage to 60 here */
                    // TODO : Remove house stat fixing when bug set to 0 when enter dungeon as fixed
                    girl.m_Stats[(int)EnumStats.HOUSE] = 60;

                    if (WMRand.Percent(Math.Min(75, this.GangCible.Charisma))) // convince her
                    {
                        kidnappMissionEvent.AppendLineFormat(
                           LocalString.ResourceStringCategory.GangMission,
                           "YourMenFindAGirl[GirlName]AndConvinceHerThatSheShouldWorkForYou",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        NGmsg.AppendLineFormat(
                           LocalString.ResourceStringCategory.Girl,
                           "[GirlName]WasTalkedIntoWorkingForYouBy[GangName]",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                        dungeonReason = DungeonReasons.NEWGIRL;
                        GangManager.BoostGangSkill(this.GangCible.Stats[EnumStats.CHARISMA], 3);
                        captured = true;
                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.KIDNAPXGIRLS))
                        {
                            Game.Brothels.GetObjective().m_SoFar++; // `J` Added to make Charisma Kidnapping count
                            if (WMRand.Percent(Game.Brothels.GetObjective().m_Target * 10)) // but possibly reduce the reward to gold only
                            {
                                Game.Brothels.GetObjective().m_Reward = (int)Rewards.GOLD;
                            }
                        }
                    }
                    if (!captured && this.GangCible.NetLimit > 0) // try to capture using net
                    {
                        kidnappMissionEvent.AppendLineFormat(
                           LocalString.ResourceStringCategory.GangMission,
                           "YourMenFindAGirl[GirlName]AndTryToCatchHerInTheirNets",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        int tries = 0;
                        while (this.GangCible.NetLimit > 0 && !captured)
                        {
                            int damageChance = 40;
                            if (WMRand.Percent(this.GangCible.Combat)) // hit her with the net
                            {
                                if (!WMRand.Percent((double)(girl.agility() - tries) / 2.0)) // she can't avoid or get out of the net
                                {
                                    captured = true;
                                }
                                else
                                {
                                    damageChance = 60;
                                }
                            }

                            if (WMRand.Percent(damageChance))
                            {
                                damagedNets++;
                                this.GangCible.AdjustNetLimit(-1);
                                GangManager.NumNets--;
                            }
                            tries++;
                        }
                        if (captured)
                        {
                            if (damagedNets > 0)
                            {

                                if (this.GangCible.NetLimit == 0)
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.GangMission,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                                else if (this.GangCible.NetLimit == 1)
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.GangMission,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                                else
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.GangMission,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                            }
                            kidnappMissionEvent.AppendLine(
                                LocalString.ResourceStringCategory.GangMission,
                                "SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow");
                            girlImageType = ImageType.DEATH;
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                            girl.add_trait("Kidnapped", 5 + WMRand.Random(11));
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.GangMission,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Girl,
                                "[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                            GangManager.BoostGangSkill(this.GangCible.Stats[EnumStats.INTELLIGENCE], 2);
                        }
                        else
                        {
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.GangMission,
                                "[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        }
                    }
                    if (!captured)
                    {
                        if (Game.Brothels.FightsBack(girl)) // kidnap her
                        {
                            if (damagedNets == 0)
                            {
                                kidnappMissionEvent.AppendLineFormat(
                                    LocalString.ResourceStringCategory.GangMission,
                                    "YourMenFindAGirl[GirlName]AndAttemptToKidnapHer",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                            }
                            if (!GangManager.GangCombat(girl, this.GangCible))
                            {
                                girlImageType = ImageType.DEATH;
                                dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                                girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                                girl.add_trait("Kidnapped", 10 + WMRand.Random(11));
                                kidnappMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "SheFightsBackButYourMenSucceedInKidnappingHer");
                                NGmsg.AppendLineFormat(
                                    LocalString.ResourceStringCategory.Girl,
                                    "[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                                GangManager.BoostGangSkill(this.GangCible.Skills[EnumSkills.COMBAT], 1);
                                captured = true;
                            }
                            else
                            {
                                kidnappMissionEvent.AppendLine(
                                    LocalString.ResourceStringCategory.GangMission,
                                    "TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets");
                                gangEventType = EventType.Danger;
                            }
                        }
                        else if (damagedNets == 0)
                        {
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.add_trait("Kidnapped", 3 + WMRand.Random(8));
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.GangMission,
                                "[GangName]KidnapHerSuccessfullyWithoutAFussSheIsInYourDungeonNow",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Girl,
                                "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                            captured = true;
                        }
                        else
                        {
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.add_trait("Kidnapped", 5 + WMRand.Random(8));
                            kidnappMissionEvent.AppendLine(
                                LocalString.ResourceStringCategory.GangMission,
                                "AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer");
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Girl,
                                "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname), new FormatStringParameter("GangName", this.GangCible.Name) });
                            captured = true;
                        }
                    }

                    if (captured)
                    {
                        girl.Events.AddMessage(NGmsg.ToString(), girlImageType, eventType);
                        Game.Dungeon.AddGirl(girl, dungeonReason);
                        GangManager.BoostGangSkill(this.GangCible.Stats[EnumStats.INTELLIGENCE], 1);
                    }
                    this.GangCible.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageType.PROFILE, gangEventType);
                }
                else
                {
                    kidnappMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TheyFailedToFindAnyGirlsToKidnap");
                    this.GangCible.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageType.PROFILE, EventType.Gang);
                }
            }
            else
            {
                kidnappMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TheyFailedToFindAnyGirlsToKidnap");
                this.GangCible.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageType.PROFILE, EventType.Gang);
            }
            return captured;
        }
    }
}
