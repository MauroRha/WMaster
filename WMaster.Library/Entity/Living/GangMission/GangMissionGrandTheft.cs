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
//  <copyright file="GangMissionGrandTheft.cs" company="The Pink Petal Devloment Team">
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
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Grand theft mission affecte to gang.
    /// </summary>
    public sealed class GangMissionGrandTheft : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionGrandTheft(Gang gang)
            : base(EnuGangMissions.GrandTheft, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.GangMission, "MissionGrandTheft");
        }

        /// <summary>
        /// Performe a gang grand theft mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString grandTheftEvent = new LocalString();
            Game.Player.disposition(-3);
            Game.Player.customerfear(3);
            Game.Player.suspicion(3);
            bool fightRival = false;
            cRival rival = null;
            Gang defenders = null;
            string place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlacePlace");
            int defenceChance = 0;
            int gold = 1;
            int difficulty = Math.Max(0, WMRand.Bell(0, 6) - 2); // 0-4

            if (difficulty <= 0)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlaceSmallShop");
                defenceChance = 10;
                gold += 10 + WMRand.Random(290);
                difficulty = 0;
            }
            if (difficulty == 1)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlaceSmithy");
                defenceChance = 30;
                gold += 50 + WMRand.Random(550);
            }
            if (difficulty == 2)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlaceJeweler");
                defenceChance = 50;
                gold += 200 + WMRand.Random(800);
            }
            if (difficulty == 3)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlaceTradeCaravan");
                defenceChance = 70;
                gold += 500 + WMRand.Random(1500);
            }
            if (difficulty >= 4)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "PlaceBank");
                defenceChance = 90;
                gold += 1000 + WMRand.Random(4000);
                difficulty = 4;
            }

            grandTheftEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.GangMission,
                "Gang[GangName]GoesOutToRobA[ThievePlace]",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name), new FormatStringParameter("ThievePlace", place) });

            // `J` chance of running into a rival gang updated for .06.02.41
            int gangs = Game.Rivals.GetNumRivalGangs();
            int chance = 10 + Math.Max(30, gangs * 2); // 10% base +2% per gang, 40% max

            if (WMRand.Percent(chance))
            {
                rival = Game.Rivals.GetRandomRivalWithGangs();
                if (rival != null && rival.m_NumGangs > 0)
                {
                    fightRival = true;
                    defenders = GangManager.GetTempGang(rival.m_Power);

                    grandTheftEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.GangMission,
                        "The[ThievePlace]IsGuardedByAGangFrom[RivalName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place), new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if ((defenders == null) && WMRand.Percent(defenceChance))
            {
                defenders = GangManager.GetTempGang(difficulty * 3);
                grandTheftEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.GangMission,
                    "The[ThievePlace]HasItsOwnGuards",
                    new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place) });
            }
            if (defenders == null)
            {
                grandTheftEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.GangMission,
                    "The[ThievePlace]IsUnguarded",
                    new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place) });
            }

            if (defenders != null)
            {
                if (!GangManager.GangBrawl(this.GangCible, defenders, false))
                {
                    grandTheftEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "YourMenLoseTheFight");
                    this.GangCible.m_Events.AddMessage(grandTheftEvent.ToString(), ImageType.PROFILE, EventType.Danger);
                    return false;
                }
                grandTheftEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "YourMenWin");
            }

            if (fightRival && defenders.MemberNum <= 0)
            {
                rival.m_NumGangs--;
            }
            defenders = null;

            // rewards
            grandTheftEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.GangMission,
                "TheyGetAwayWith[Number]GoldFromThe[ThievePlace]",
                new List<FormatStringParameter>() { new FormatStringParameter("Number", gold), new FormatStringParameter("ThievePlace", place) });

            // `J` zzzzzz - need to add items


            Game.Player.suspicion(gold / 1000);

            Game.Gold.GrandTheft(gold);
            this.GangCible.m_Events.AddMessage(grandTheftEvent.ToString(), ImageType.PROFILE, EventType.Gang);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
            {
                Game.Brothels.GetObjective().m_SoFar += gold;
            }
            return true;
        }
    }
}
