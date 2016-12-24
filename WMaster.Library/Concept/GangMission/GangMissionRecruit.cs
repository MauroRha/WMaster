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
//  <copyright file="GangMissionRecruit.cs" company="The Pink Petal Devloment Team">
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
    using WMaster.Entity.Living;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Recruiting mission affecte to gang.
    /// </summary>
    public sealed class GangMissionRecruit : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionRecruit(Gang gang)
            : base(EnuGangMissions.Recruit, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.Global, "GangMissionRecruit");
        }

        /// <summary>
        /// Performe a gang training mission.
        /// <remarks><para>`J` - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString gangRecruitingEvent = new LocalString();
            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsRecruiting",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
            int recruit = 0;
            int start = WMRand.Bell(1, 6); // 1-6 people are available for recruitment
            int available = start;
            int add = Math.Max(0, WMRand.Bell(0, 4) - 1); // possibly get 1-3 without having to ask
            start += add;
            int playerDisposition = Game.Player.disposition();
            while (available > 0)
            {
                int chance = this.GangCible.Charisma;
                if (WMRand.Percent(this.GangCible.Magic / 4))
                {
                    chance += this.GangCible.Magic / 10;
                }
                if (WMRand.Percent(this.GangCible.Combat / 4))
                {
                    chance += this.GangCible.Combat / 10;
                }
                if (WMRand.Percent(this.GangCible.Intelligence / 4))
                {
                    chance += this.GangCible.Intelligence / 10;
                }
                if (WMRand.Percent(this.GangCible.Agility / 4))
                {
                    chance += this.GangCible.Agility / 10;
                }
                if (WMRand.Percent(this.GangCible.Constitution / 4))
                {
                    chance += this.GangCible.Constitution / 10;
                }
                if (WMRand.Percent(this.GangCible.Strength / 4))
                {
                    chance += this.GangCible.Strength / 10;
                }

                // less chance of them wanting to work for really evil or really good player
                if (playerDisposition < -50)
                {
                    chance += (playerDisposition + 50) / 2; // -25 for -100 disp
                }
                if (playerDisposition > -20 && playerDisposition < 0)
                {
                    chance += (22 + playerDisposition) / 2; // +1 for -19  to +10 for -2
                }
                if (playerDisposition == 0)
                {
                    chance += 10; // +10 for -2,-1,0,1,2
                }
                if (playerDisposition < 20 && playerDisposition > 0)
                {
                    chance += (22 - playerDisposition) / 2; // +1 for 19   to +10 for 2
                }
                if (playerDisposition > 50)
                {
                    chance -= (playerDisposition - 50) / 3; // -16 for > 98 disp
                }

                if (chance > 90)
                {
                    chance = 90;
                }
                if (chance < 20)
                {
                    chance = 20; // 20-90% chance
                }
                if (WMRand.Percent(chance))
                {
                    add++;
                }
                available--;
            }

            while (add > recruit && this.GangCible.MemberNum < 15)
            {
                recruit++;
                this.GangCible.MemberNum++;
            }
            if (start < 1)
            {
                gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "TheyWereUnableToFindAnyoneToRecruit");
            }
            else
            {
                if (start == 1)
                {
                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyFoundOnePersonToTryToRecruit");
                }
                else
                {
                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyFoundPeopleToTryToRecruit");
                }

                if (start == 1)
                {
                    if (add == start)
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndTheyGotHimToJoin");
                    }
                    else
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButHeDidntWantToJoin");
                    }
                }
                else if (add <= 0)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButWereUnableToGetAnyToJoin");
                }
                else if (add == start)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndManagedToGetAllOfThemToJoin");
                }
                else if (add == 1)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButWereOnlyAbleToConvinceOneOfThemToJoin");
                }
                else
                {
                    gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                        "AndWereAbleToConvince[Number]OfThemToJoin",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", add) });
                }

                if (this.GangCible.MemberNum >= 15 && add == recruit)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "TheyGotAsManyAsTheyNeededToFillTheirRanks");
                }
                else if (this.GangCible.MemberNum >= 15 && add > recruit)
                {
                    this.GangCible.MemberNum = 15;
                    if (recruit == 1)
                    {
                        gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyOnlyHadRoomForOneMoreInTheirGangSoThey");
                    }
                    else
                    {
                        gangRecruitingEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "TheyOnlyHadRoomFor[Number]MoreInTheirGangSoThey",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", recruit) });
                    }
                    int passNum = add - recruit;
                    Gang passTo = GangManager.GetGangRecruitingNotFull(passNum);
                    if (passTo != null)
                    {
                        gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "SentTheRestToJoin[GangName]",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", passTo.Name) });
                        LocalString pss = new LocalString();
                        if (passNum > 1)
                        {
                            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "[GangName]Sent[Number]RecruitsThatTheyHadNoRoomForTo[ToGangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name), new FormatStringParameter("Number", passNum), new FormatStringParameter("ToGangName", passTo.Name) });
                        }
                        else
                        {
                            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "[GangName]SentOneRecruitThatTheyHadNoRoomForTo[ToGangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name), new FormatStringParameter("ToGangName", passTo.Name) });
                        }
                        int passNumGotThere = 0;
                        for (int i = 0; i < passNum; i++)
                        {
                            if (passTo.MissionType == EnuGangMissions.Recruit)
                            {
                                if (WMRand.Percent(75))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Training)
                            {
                                if (WMRand.Percent(50))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.SpyGirls)
                            {
                                if (WMRand.Percent(95))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Guarding)
                            {
                                if (WMRand.Percent(30))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Service)
                            {
                                if (WMRand.Percent(90))
                                {
                                    passNumGotThere++;
                                }
                            }
                        }
                        if (passNumGotThere > 0)
                        {
                            if (passNumGotThere == passNum)
                            {
                                if (passNum > 1)
                                {
                                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyAllArrived");
                                }
                                else
                                {
                                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyArrived");
                                }
                            }
                            else
                            {
                                gangRecruitingEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                    "Only[Number]Arrived",
                                    new List<FormatStringParameter>() { new FormatStringParameter("Number", passNumGotThere) });
                            }
                            if (passTo.MemberNum + passNumGotThere <= 15)
                            {
                                gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndGotAcceptedIntoTheGang");
                            }
                            else
                            {
                                passNumGotThere = 15 - passTo.MemberNum;
                                gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                    "But[GangName]CouldOnlyTake[Number]OfThem",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", passTo.Name), new FormatStringParameter("Number", passNumGotThere) });
                            }
                            passTo.MemberNum += passNumGotThere;
                        }
                        else
                        {
                            gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButNoneShowedUp");
                        }
                        passTo.m_Events.AddMessage(pss.ToString(), ImageTypes.PROFILE, EventType.GANG);
                    }
                    else
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "HadToTurnAwayTheRest");
                    }
                }
            }
            this.GangCible.m_Events.AddMessage(gangRecruitingEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            this.GangCible.HasSeenCombat = true; // though not actually combat, this prevents the automatic +1 member at the end of the week
            return false;
        }
    }
}
