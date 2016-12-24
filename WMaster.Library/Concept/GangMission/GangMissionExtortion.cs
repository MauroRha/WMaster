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
//  <copyright file="GangMissionExtortion.cs" company="The Pink Petal Devloment Team">
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
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Entity.Living;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Extortion mission affecte to gang.
    /// </summary>
    public sealed class  GangMissionExtortion : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionExtortion(Gang gang)
            : base(EnuGangMissions.Extortion, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.Global, "GangMissionExtortion");
        }

        /// <summary>
        /// Performe a gang extortion mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString extortionEven = new LocalString();
            Game.Player.disposition(-1);
            Game.Player.customerfear(1);
            Game.Player.suspicion(1);
            extortionEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsCapturingTerritory",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            // Case 1:  Neutral businesses still around
            int numB = Game.Rivals.GetNumBusinesses();
            int uncontrolled = Constants.TOWN_NUMBUSINESSES - GangManager.NumBusinessExtort - numB;
            int n = 0;
            int trycount = 1;
            if (uncontrolled > 0)
            {
                trycount += WMRand.Random() % 5; // 1-5
                while (uncontrolled > 0 && trycount > 0)
                {
                    trycount--;
                    if (WMRand.Percent(this.GangCible.Charisma / 2)) // convince
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.customerfear(-1);
                    }
                    else if (WMRand.Percent(this.GangCible.Intelligence / 2)) // outwit
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.disposition(-1);
                    }
                    else if (WMRand.Percent(this.GangCible.Combat / 2)) // threaten
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.disposition(-1);
                        Game.Player.customerfear(2);
                    }
                }

                if (n == 0)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "TheyFailToGainAnyMoreNeutralTerritories");
                }
                else if (n == 1)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YouGainControlOfOneMoreNeutralTerritories");
                }
                else
                {
                    extortionEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YouGainControlOf[Number]MoreNeutralTerritory",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", n) });
                }
                GangManager.NumBusinessExtort += n;
                Game.Gold.extortion(n * 20);

                if (uncontrolled <= 0)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereAreNoMoreUncontrolledBusinessesLeft");
                }
                if (uncontrolled == 1)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereIsOneUncontrolledBusinessesLeft");
                }
                else
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereAreUncontrolledBusinessesLeft");
                }
            }
            else // Case 2: Steal bussinesses away from rival if no neutral businesses left
            {
                cRival rival = Game.Rivals.GetRandomRivalWithBusinesses();
                if (rival != null && rival.m_BusinessesExtort > 0)
                {
                    extortionEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "TheyStormIntoYourRival[[:RivalName:]]sTerritory",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    bool defended = false;
                    if (rival.m_NumGangs > 0)
                    {
                        Gang rival_gang = GangManager.GetTempGang(rival.m_Power);
                        defended = true;
                        extortionEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenRunIntoOneOfTheirGangsAndABrawlBreaksOut");

                        if (GangManager.GangBrawl(this.GangCible, rival_gang, false))
                        {
                            trycount += WMRand.Random() % 3;

                            if (rival_gang.MemberNum <= 0)
                            {
                                extortionEven.Append(
                                    LocalString.ResourceStringCategory.Global,
                                    "TheyDestroyTheDefendersAnd");
                                rival.m_NumGangs--;
                            }
                            else
                            {
                                extortionEven.Append(
                                    LocalString.ResourceStringCategory.Global,
                                    "TheyDefeatTheDefendersAnd");
                            }
                        }
                        else
                        {
                            extortionEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "YourGangHasBeenDefeatedAndFailToTakeControlOfAnyNewTerritory");
                            this.GangCible.m_Events.AddMessage(extortionEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                            return false;
                        }
                        rival_gang = null;
                    }
                    else // Rival has no gangs
                    {
                        extortionEven.Append(
                            LocalString.ResourceStringCategory.Global,
                            "TheyFacedNoOppositionAsthey");
                        trycount += WMRand.Random() % 5;
                    }

                    while (trycount > 0 && rival.m_BusinessesExtort > 0)
                    {
                        trycount--;
                        rival.m_BusinessesExtort--;
                        GangManager.NumBusinessExtort++;
                        n++;
                    }

                    if (n > 0)
                    {
                        if (n == 1)
                        {
                            extortionEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "TookOverOneOf[RivalName]sTerritory",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                        }
                        else
                        {
                            extortionEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "TookOver[Number]Of[RivalName]sTerritories",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", n), new FormatStringParameter("RivalName", rival.m_Name) });
                        }
                    }
                    else
                    {
                        extortionEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "LeftErrorNoTerritoriesGainedButShouldHaveBeen");
                    }
                }
                else
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YouFailToTakeControlOfAnyOfNewTerritories");
                }
            }

            this.GangCible.m_Events.AddMessage(extortionEven.ToString(), ImageTypes.PROFILE, EventType.GANG);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.EXTORTXNEWBUSINESS))
            {
                Game.Brothels.GetObjective().m_SoFar += n;
            }

            return true;
        }
    }
}
