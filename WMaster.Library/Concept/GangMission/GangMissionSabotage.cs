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
//  <copyright file="GangMissionSabotage.cs" company="The Pink Petal Devloment Team">
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
    using WMaster.Entity.Item;
    using WMaster.Entity.Living;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Performe a gang sabotage mission against player's rival.
    /// <remarks><para>`J` mission returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
    /// </summary>
    public class GangMissionSabotage : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionSabotage(Gang gang)
            : base(EnuGangMissions.Sabotage, gang)
        {
        }

        // TODO : Methode is too complex, Cyclomatic indicator to high. Need to fractionat it
        /// <summary>
        /// Performe a gang sabotage mission against player's rival.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString sabotageEvent = new LocalString();
            sabotageEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsAttackingRivals",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
            /*
            *	See if they can find any enemy assets to attack
            *
            *	I'd like to add a little more intelligence to this.
            *	Modifiers based on gang intelligence, for instance
            *	Allow a "scout" activity for gangs that improves the
            *	chances of a raid. That sort of thing.
            */
            if (!WMRand.Percent(Math.Min(90, this.GangCible.Intelligence)))
            {

                this.GangCible.m_Events.AddMessage(
                    LocalString.GetString(LocalString.ResourceStringCategory.Global, "TheyFailedToFindAnyEnemyAssetsToHit"),
                    ImageTypes.PROFILE, EventType.GANG);
                return false;
            }
            /*
            *	if yes then do damage to a random rival
            *
            *	Something else to consider: rival choice should be
            *	weighted by number of territories controlled
            *	(or - if we go with the ward idea - by territories
            *	controlled in the ward in question
            *
            *	of course, if there is no rival, it's academic
            */
            cRival rival = Game.Rivals.GetRandomRivalToSabotage();
            Gang rivalGang;
            if (rival == null)
            {
                this.GangCible.m_Events.AddMessage(
                    LocalString.GetString(LocalString.ResourceStringCategory.Global, "ScoutedTheCityInVainSeekingWouldBeChallengersToYourDominance"),
                    ImageTypes.PROFILE, EventType.GANG);
                return false;
            }

            if (rival.m_NumGangs > 0)
            {
                rivalGang = GangManager.GetTempGang(rival.m_Power);
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenRunIntoAGangFrom[GangName]AndABrawlBreaksOut",
                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
                if (GangManager.GangBrawl(this.GangCible, rivalGang, false) == false)
                {
                    rivalGang = null;
                    if (this.GangCible.MemberNum == 0)
                    {
                        // TODO : Check if event is shown when gang was disband
                        sabotageEvent.AppendFormat(
                            LocalString.ResourceStringCategory.Global,
                            "YourGang[GangName]FailsToReportBackFromTheirSabotageMissionLaterYouLearnThatTheyWereWipedOutToTheLastMan",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
                    }
                    else if (this.GangCible.MemberNum == 1)
                    {
                        sabotageEvent.Append(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenLostTheLoneSurvivorFightsHisWayBackToFriendlyTerritory");
                    }
                    else
                    {
                        sabotageEvent.AppendFormat(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenLostThe[GangMemberNum]SurvivorsFightTheirWayBackToFriendlyTerritory",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangMemberNum", this.GangCible.MemberNum) });
                    }
                    this.GangCible.m_Events.AddMessage(sabotageEvent.ToString(), ImageTypes.PROFILE, EventType.DANGER);
                    return false;
                }
                else
                {
                    sabotageEvent.Append(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenWin");
                }
                if (rivalGang.MemberNum <= 0) // clean up the rival gang
                {
                    rival.m_NumGangs--;

                    if (rival.m_NumGangs == 0)
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasNoMoreGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                    else if (rival.m_NumGangs <= 3)
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasAFewGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                    else
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasALotOfGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                }
                rivalGang = null;
            }
            else
            {
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenEncounterNoResistanceWhenyougoAfter[RivalGangName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalGangName", rival.m_Name) });
            }

            // if we had an objective to attack a rival we just achieved it
            if (Game.Brothels.GetObjective() != null && Game.Brothels.GetObjective().m_Objective == (int)Objectives.LAUNCHSUCCESSFULATTACK)
            {
                Game.Brothels.PassObjective();
            }

            // If the rival has some businesses under his control he's going to lose some of them
            if (rival.m_BusinessesExtort > 0)
            {
                // mod: brighter goons do better damage they need 100% to be better than before however
                int spread = this.GangCible.Intelligence / 4;
                int num = 1 + WMRand.Random(spread); // get the number of businesses lost
                if (rival.m_BusinessesExtort < num) // Can't destroy more businesses than they have
                {
                    num = rival.m_BusinessesExtort;
                }
                rival.m_BusinessesExtort -= num;

                if (rival.m_BusinessesExtort == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveNoMoreBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_BusinessesExtort <= 10)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveAFewBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveALotOfBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            else
            {
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "[RivalName]HaveNoBusinessesToAttack",
                    new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
            }

            if (rival.m_Gold > 0)
            {
                // mod: brighter goons are better thieves
                // `J` changed it // they need 100% to be better than before however	
                // `J` now based on rival's gold
                // `J` bookmark - your gang sabotage mission gold taken
                int gold = WMRand.Random((int)(((double)this.GangCible.Intelligence / 2000.0) * (double)rival.m_Gold)) + WMRand.Random((this.GangCible.Intelligence / 5) * this.GangCible.MemberNum); // plus (int/5)*num -  0-5% of rival's gold
                if (gold > rival.m_Gold)
                {
                    gold = (int)rival.m_Gold;
                }
                rival.m_Gold -= gold;

                // some of the money taken 'dissappears' before the gang reports it.
                if (WMRand.Percent(20) && gold > 1000)
                {
                    gold -= WMRand.Random() % 1000;
                }

                if (rival.m_Gold == 0)
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThemMuhahahaha[RivalName]IsPennilessNow",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_Gold <= 10000)
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThem[RivalName]IsLookingPrettyPoor",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThemItLooksLike[RivalName]StillHasALotOfGold",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
                }

                /*
                `J` zzzzzz - need to add more and better limiters
                Suggestions from Whitetooth:
                I'm guessing those factors are based on there skills which make sense. For Example:
                Men - Overall number of people able to carry gold after sabotage.
                Combat - total amount of gold each man can hold.
                Magic - Amount of extra gold the gang can carry with magic not relying on combat or men. Magic could be bonus gold that can't be dropped, bribed, or stolen on the way back.
                Intel - Could be a overall factor to check if the gang knows what is valuable and what isn't.
                Agility - Could be a check for clumsiness of the gang; they could drop valuables on the way back.
                Tough - Checks if there tough enough to intimidate any guards or protect the money they have.
                Charisma - Factors how much gold they have to bribe to guards if they get caught and can't intimidate them.
                The order of checks could be -> Intel -> Magic -> Men - > Combat -> Agility -> Tough -> Charisma
                */

                // `J` bookmark - limit gold taken by gang sabotage
                bool limit = false;
                if (gold > 15000)
                {
                    limit = true;
                    int burnedbonds = (gold / 10000);
                    int bbcost = burnedbonds * 10000;
                    gold -= bbcost;

                    if (burnedbonds == 1)
                    {
                        sabotageEvent.AppendLineFormat(
                      LocalString.ResourceStringCategory.Global,
                      "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAGoldBearerBondWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                      new List<FormatStringParameter>() { new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }
                    else if (burnedbonds > 4)
                    {
                        sabotageEvent.AppendLineFormat(
                       LocalString.ResourceStringCategory.Global,
                       "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAStackOfGoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                       new List<FormatStringParameter>() { new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }
                    else
                    {
                        sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDrops[BurnedBonds]GoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                        new List<FormatStringParameter>() { new FormatStringParameter("BurnedBonds", burnedbonds), new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }

                }
                if (gold > 5000 && WMRand.Percent(50))
                {
                    limit = true;
                    int spill = (WMRand.Random() % 4500) + 500;
                    gold -= spill;
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsTheyAreBeingChasedThroughTheStreetsBy[RivalName]sPeopleOneOfYourGangMembersCutsOpenASackOfGoldSpillingItsContentsInTheStreetAsThThrongsOfCiviliansStreamInToCollectTheCoinsTheBlockThePursuersAndAllowYouMenToGetAwaySafely",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }

                if (gold > 5000)
                {
                    limit = true;
                    int bribeperc = ((WMRand.Random() % 15) * 5) + 10;
                    int bribe = (int)(gold * ((double)bribeperc / 100.0));
                    gold -= bribe;
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsYourGangLeaveYourRivalsTerritoryOnTheWayBackToYourBrothelTheyComeUponABandOfLocalPoliceThatAreHuntingThemTheirBossDemands[BribePercent]OfWhatYourGangIsCarryingInOrderToLetThemGoTheyPayThem[Bribe]GoldAndContinueOnHome",
                        new List<FormatStringParameter>() { new FormatStringParameter("BribePercent", bribeperc), new FormatStringParameter("Bribe", bribe) });
                }

                if (limit)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]ReturnsWith[GoldAmount]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name), new FormatStringParameter("GoldAmount", gold) });
                }
                Game.Gold.plunder(gold);
            }
            else
            {
                sabotageEvent.AppendLine(
                    LocalString.ResourceStringCategory.Global,
                    "TheLosersHaveNoGoldToTake");
            }

            if (rival.m_NumInventory > 0 && WMRand.Percent(Math.Min(75, this.GangCible.Intelligence)))
            {
                cRivalManager r = new cRivalManager();
                int num = r.GetRandomRivalItemNum(rival);
                sInventoryItem item = r.GetRivalItem(rival, num);
                if (item != null)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenStealAnItemFromThemOne[ItemName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("ItemName", item.Name) });

                    r.RemoveRivalInvByNumber(rival, num);
                    Game.Brothels.AddItemToInventory(item);
                }
            }

            if (rival.m_NumBrothels > 0 && WMRand.Percent(this.GangCible.Intelligence / Math.Min(3, 11 - rival.m_NumBrothels)))
            {
                rival.m_NumBrothels--;
                rival.m_Power--;
                if (rival.m_NumBrothels == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasNoBrothelsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumBrothels <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]IsInControlOfVeryFewBrothels",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasManyBrothelsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if (rival.m_NumGamblingHalls > 0 && WMRand.Percent(this.GangCible.Intelligence / Math.Min(1, 9 - rival.m_NumGamblingHalls)))
            {
                rival.m_NumGamblingHalls--;

                if (rival.m_NumGamblingHalls == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasNoGamblingHallsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumGamblingHalls <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]IsInControlOfVeryFewGamblingHalls",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasManyGamblingHallsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if (rival.m_NumBars > 0 && WMRand.Percent(this.GangCible.Intelligence / Math.Min(1, 7 - rival.m_NumBars)))
            {
                rival.m_NumBars--;

                if (rival.m_NumBars == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasNoBarsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumBars <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]IsInControlOfVeryFewBars",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasManyBarsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }

            GangManager.BoostGangSkill(this.GangCible.Stats[EnumStats.INTELLIGENCE], 2);
            this.GangCible.m_Events.AddMessage(sabotageEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);

            // See if the rival is eliminated:  If 4 or more are zero or less, the rival is eliminated
            int VictoryPoints = 0;
            if (rival.m_Gold <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumGangs <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_BusinessesExtort <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumBrothels <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumGamblingHalls <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumBars <= 0)
            {
                VictoryPoints++;
            }

            if (VictoryPoints >= 4)
            {
                LocalString ssVic = new LocalString();
                ssVic.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YouHaveDealt[RivalName]AFatalBlowTheirCriminalOrganizationCrumblesToNothingBeforeYou",
                    new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                Game.Brothels.m_Rivals.RemoveRival(rival);
                this.GangCible.m_Events.AddMessage(ssVic.ToString(), ImageTypes.PROFILE, EventType.GOODNEWS);
            }
            return true;
        }

        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.Global, "GangMissionSabotage");
        }
    }
}
