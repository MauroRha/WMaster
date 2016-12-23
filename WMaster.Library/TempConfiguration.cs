﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster
{
    [Obsolete("Dont use this class, need to initialise ans save configuration data outside project execution", false)]
    public class TempConfiguration
    {
        private System.Collections.Specialized.NameValueCollection ResourcesText = new System.Collections.Specialized.NameValueCollection();

        public void Initialise()
        {
            ResourcesText.Add("Gang[GangName]IsAttackingRivals", "Gang   [[:GangName:]]   is attacking rivals.");
            ResourcesText.Add("TheyFailedToFindAnyEnemyAssetsToHit", "They failed to find any enemy assets to hit.");
            ResourcesText.Add("ScoutedTheCityInVainSeekingWouldBeChallengersToYourDominance.", "Scouted the city in vain, seeking would-be challengers to your dominance.");
            ResourcesText.Add("YourMenRunIntoAGangFrom[GangName]AndABrawlBreaksOut", "Your men run into a gang from [[:GangName:]] and a brawl breaks out.");
            ResourcesText.Add("YourGang[GangName]FailsToReportBackFromTheirSabotageMissionLaterYouLearnThatTheyWereWipedOutToTheLastMan.", "Your gang [[:GangName:]] fails to report back from their sabotage mission.[[:NewLine:]]Later you learn that they were wiped out to the last man.");
            ResourcesText.Add("YourMenLostTheLoneSurvivorFightsHisWayBackToFriendlyTerritory", "Your men lost. The lone survivor fights his way back to friendly territory.");
            ResourcesText.Add("YourMenLostThe[GangMemberNum]SurvivorsFightTheirWayBackToFriendlyTerritory", "Your men lost. The [[:GangMemberNum:]] survivors fight their way back to friendly territory.");
            ResourcesText.Add("YourMenWin", "Your men win.");
            ResourcesText.Add("TheEnemyGangIsDestroyed[RivalGangName]HasNoMoreGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has no more gangs left!");
            ResourcesText.Add("TheEnemyGangIsDestroyed[RivalGangName]HasAFewGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has a few gangs left.");
            ResourcesText.Add("TheEnemyGangIsDestroyed[RivalGangName]HasALotOfGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has a lot of gangs left.");
            ResourcesText.Add("YourMenEncounterNoResistanceWhenyougoAfter[RivalGangName]", "Your men encounter no resistance when you go after [[:RivalGangName:]].");
            ResourcesText.Add("YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveNoMoreBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have no more businesses left!");
            ResourcesText.Add("YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveAFewBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have a few businesses left.");
            ResourcesText.Add("YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveALotOfBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have a lot of businesses left.");
            ResourcesText.Add("[RivalName]HaveNoBusinessesToAttack", "[[:RivalName:]] have no businesses to attack.");
            ResourcesText.Add("YourMenSteal[GoldAmount]GoldFromThemMuhahahaha[RivalName]IsPennilessNow", "Your men steal [[:GoldAmount:]] gold from them. Mu-hahahaha!  [[:RivalName:]] is penniless now!");
            ResourcesText.Add("YourMenSteal[GoldAmount]GoldFromThem[RivalName]IsLookingPrettyPoor", "Your men steal [[:GoldAmount:]] gold from them. [[:RivalName:]] is looking pretty poor.");
            ResourcesText.Add("YourMenSteal[GoldAmount]GoldFromThemItLooksLike[RivalName]StillHasALotOfGold", "Your men steal [[:GoldAmount:]] gold from them. It looks like [[:RivalName:]] still has a lot of gold.");
            ResourcesText.Add("AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAGoldBearerBondWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops a Gold Bearer Bond worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke.");
            ResourcesText.Add("AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAStackOfGoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops a stack of Gold Bearer Bonds worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke.");
            ResourcesText.Add("AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDrops[BurnedBonds]GoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops [[:BurnedBonds:]] Gold Bearer Bonds worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke.");
            ResourcesText.Add("AsTheyAreBeingChasedThroughTheStreetsBy[RivalName]sPeopleOneOfYourGangMembersCutsOpenASackOfGoldSpillingItsContentsInTheStreetAsThThrongsOfCiviliansStreamInToCollectTheCoinsTheBlockThePursuersAndAllowYouMenToGetAwaySafely", "As they are being chased through the streets by [[:RivalName:]]'s people, one of your gang members cuts open a sack of gold spilling its contents in the street. As the throngs of civilians stream in to collect the coins, they block the pursuers and allow you men to get away safely.");
            ResourcesText.Add("AsYourGangLeaveYourRivalsTerritoryOnTheWayBackToYourBrothelTheyComeUponABandOfLocalPoliceThatAreHuntingThemTheirBossDemands[BribePercent]OfWhatYourGangIsCarryingInOrderToLetThemGoTheyPayThem[Bribe]GoldAndContinueOnHome", "As your gang leave your rival's territory on the way back to your brothel, they come upon a band of local police that are hunting them. Their boss demands [[:BribePercent:]]% of what your gang is carrying in order to let them go. They pay them [[:Bribe:]] gold and continue on home.");
            ResourcesText.Add("[GangName]ReturnsWith[GoldAmount]Gold", "[[:GangName:]] returns with [[:GoldAmount:]] gold.");
            ResourcesText.Add("TheLosersHaveNoGoldToTake", "The losers have no gold to take.");
            ResourcesText.Add("YourMenStealAnItemFromThemOne[ItemName]", "Your men steal an item from them, one [[:ItemName:]].");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasNoBrothelsLeft", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] has no Brothels left.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]IsInControlOfVeryFewBrothels", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] is in control of very few Brothels.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasManyBrothelsLeft", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] has many Brothels left.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasNoGamblingHallsLeft", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] has no Gambling Halls left.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]IsInControlOfVeryFewGamblingHalls", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] is in control of very few Gambling Halls.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasManyGamblingHallsLeft", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] has many Gambling Halls left.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasNoBarsLeft", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] has no Bars left.");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBars[RivalName]IsInControlOfVeryFewBars", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] is in control of very few Bars");
            ResourcesText.Add("YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasManyBarsLeft", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] has many Bars left");
            ResourcesText.Add("Gang[GangName]IsLookingForEscapedGirls", "Gang   [[:GangName:]]   is looking for escaped girls.");
            ResourcesText.Add("ThereAreNoneOfYourGirlsWhoHaveRunAwaySoTheyHaveNooneToLookFor", "There are none of your girls who have run away, so they have noone to look for.");
            ResourcesText.Add("YourGoonsFind[GirlName]AndSheComesQuietlyWithoutPuttingUpAFight", "Your goons find [[:GirlName:]] and she comes quietly without putting up a fight.");
            ResourcesText.Add("YourGoonsFind[GirlName]AndTheyTryToCatchHerInTheirNets", "Your goons find [[:GirlName:]] and they try to catch her in their nets.");
            ResourcesText.Add("[GirlName]WasRecapturedBy[GangName]SheGaveUpWithoutAFight", "[[:GirlName:]] was recaptured by [[:GangName:]]. She gave up without a fight.");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in the tattered remains of their last net.");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their last net.");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their nets.");
            ResourcesText.Add("SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow", "She struggles against the net your men use, but it is pointless. She is in your dungeon now.");
            ResourcesText.Add("[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]", "[[:GirlName:]] was captured in a net and dragged back to the dungeon by [[:GangName:]].");
            ResourcesText.Add("[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay", "[[:GirlName:]] managed to damage all of their nets so they have to do things the hard way.");
            ResourcesText.Add("[GangName]AttemptToRecaptureHer", "[[:GangName:]] attempt to recapture her.");
            ResourcesText.Add("SheFightsBackButYourMenSucceedInCapturingHer", "She fights back but your men succeed in capturing her.");
            ResourcesText.Add("[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon", "[[:GirlName:]] fought with [[:GangName:]] but lost. She was dragged back to the dungeon.");
            ResourcesText.Add("TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets", "The girl fights back and defeats your men before escaping into the streets.");
            ResourcesText.Add("[GangName]RecaptureHerSuccessfullyWithoutAFussSheIsInYourDungeonNow", "[[:GangName:]] recapture her successfully without a fuss. She is in your dungeon now.");
            ResourcesText.Add("[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without a fight.");
            ResourcesText.Add("AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer", "After dodging all of their nets, she gives up when they pull out their weapons and prepare to kill her.");
            ResourcesText.Add("[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without anymore of a fight.");
            ResourcesText.Add("Gang[GangName]IsCapturingTerritory", "Gang   [[:GangName:]]   is capturing territory.");
            ResourcesText.Add("TheyFailToGainAnyMoreNeutralTerritories", "They fail to gain any more neutral territories.");
            ResourcesText.Add("YouGainControlOfOneMoreNeutralTerritories", "You gain control of one more neutral territories.");
            ResourcesText.Add("YouGainControlOf[Number]MoreNeutralTerritory", "You gain control of [[:Number:]] more neutral territory.");
            ResourcesText.Add("ThereAreNoMoreUncontrolledBusinessesLeft", "There are no more uncontrolled businesses left.");
            ResourcesText.Add("ThereIsOneUncontrolledBusinessesLeft", "There is one uncontrolled businesses left.");
            ResourcesText.Add("ThereAreUncontrolledBusinessesLeft", "There are uncontrolled businesses left.");
            ResourcesText.Add("TheyStormIntoYourRival[RivalName]sTerritory", "They storm into your rival [[:RivalName:]]'s territory.");
            ResourcesText.Add("YourMenRunIntoOneOfTheirGangsAndABrawlBreaksOut", "Your men run into one of their gangs and a brawl breaks out.");
            ResourcesText.Add("TheyDestroyTheDefendersAnd", "They destroy the defenders and ");
            ResourcesText.Add("TheyDefeatTheDefendersAnd", "They defeat the defenders and ");
            ResourcesText.Add("TheyFacedNoOppositionAsthey", "They faced no opposition as they ");
            ResourcesText.Add("YourGangHasBeenDefeatedAndFailToTakeControlOfAnyNewTerritory", "Your gang has been defeated and fail to take control of any new territory.");
            ResourcesText.Add("TookOverOneOf[RivalName]sTerritory", "took over one of [[:RivalName:]]'s territory.");
            ResourcesText.Add("TookOver[Number]Of[RivalName]sTerritories", "took over [[:Number:]] of [[:RivalName:]]'s territories.");
            ResourcesText.Add("LeftErrorNoTerritoriesGainedButShouldHaveBeen", "left. (Error: no territories gained but should have been)");
            ResourcesText.Add("YouFailToTakeControlOfAnyOfNewTerritories", "You fail to take control of any of new territories.");
            ResourcesText.Add("Gang[GangName]IsPerformingPettyTheft", "Gang   [[:GangName:]]   is performing petty theft.");
            ResourcesText.Add("YourMenRunIntoAGangFrom[RivalName]AndABrawlBreaksOut", "Your men run into a gang from [[:RivalName:]] and a brawl breaks out.");
            ResourcesText.Add("YourMenRunIntoGroupOfThugsFromTheStreetsAndABrawlBreaksOut", "Your men run into group of thugs from the streets and a brawl breaks out.");
            ResourcesText.Add("YourMenLoseTheFight.", "Your men lose the fight.");
            ResourcesText.Add("YourMenAreConfrontedByAMaskedVigilante", "Your men are confronted by a masked vigilante.");
            ResourcesText.Add("SheFightsWellButYourMenStillManageToCaptureHer", "She fights well but your men still manage to capture her");
            ResourcesText.Add("SheFightsYourMenButLosesQuickly", "She fights your men but loses quickly");
            ResourcesText.Add("SheFightsYourMenButTheyTakeHerDownWithOnlyOneCasualty", "She fights your men but they take her down with only one casualty");
            ResourcesText.Add("SheFightsYourMenButTheyTakeHerDownWithOnlyAFewCasualties", "She fights your men but they take her down with only a few casualties");
            ResourcesText.Add("TheyUnmask[GirlName]TakeAllHerGold[Number]FromHerAndDragHerToTheDungeon", "They unmask [[:GirlName:]], take all her gold ([[:Number:]]) from her and drag her to the dungeon.");
            ResourcesText.Add("[GirlName]TriedToStop[GangName]FromComittingPettyTheftButLostSheWasDraggedBackToTheDungeon", "[[:GirlName:]] tried to stop [[:GangName:]] from comitting petty theft but lost. She was dragged back to the dungeon.");
            ResourcesText.Add("SheDefeatsYourMenAndDisappearsBackIntoTheShadows", "She defeats your men and disappears back into the shadows.");
            ResourcesText.Add("ThisGangWasSentToLookForRunawaysButThereAreNoneSoTheyWentLookingForAnyGirlToKidnapInstead", "This gang was sent to look for runaways but there are none so they went looking for any girl to kidnap instead.");
            ResourcesText.Add("ErrorNoMissionSetOrMissionNotFound[MissionName]", "Error: no mission set or mission not found: [[:MissionName:]]");
            ResourcesText.Add("YouHaveDealt[RivalName]AFatalBlowTheirCriminalOrganizationCrumblesToNothingBeforeYou", "You have dealt [[:RivalName:]] a fatal blow. Their criminal organization crumbles to nothing before you.");
            ResourcesText.Add("WhoPeople", "people");
            ResourcesText.Add("WhoKids", "kids");
            ResourcesText.Add("WhoLittleOldLadies", "little old ladies");
            ResourcesText.Add("WhoNobleMenAndWomen", "noble men and women");
            ResourcesText.Add("WhoSmallStalls", "small stalls");
            ResourcesText.Add("WhoTraders", "traders");
            ResourcesText.Add("YourGangRobs[NumberWho][Who]AndGet[NumberGold]GoldFromThem", "Your gang robs [[:NumberWho:]] [[:Who:]] and get [[:NumberGold:]] gold from them.");
            ResourcesText.Add("[GangName]LostOneMan", "[[:GangName:]] lost one man.");
            ResourcesText.Add("[GangName]Lost[Number]Men", "[[:GangName:]] lost [[:Number:]] men.");
            ResourcesText.Add("ThievePlace", "place");
            ResourcesText.Add("ThieveSmallShop", "small shop");
            ResourcesText.Add("ThieveSmithy", "smithy");
            ResourcesText.Add("ThieveJeweler", "jeweler");
            ResourcesText.Add("ThieveTradeCaravan", "trade caravan");
            ResourcesText.Add("ThieveBank", "bank");
            ResourcesText.Add("Gang[GangName]GoesOutToRobA[ThievePlace]", "Gang   [[:GangName:]]   goes out to rob a [[:ThievePlace:]].");
            ResourcesText.Add("The[ThievePlace]IsGuardedBAGangFrom[RivalName]", "The [[:ThievePlace:]] is guarded by a gang from [[:RivalName:]].");
            ResourcesText.Add("The[ThievePlace]HasItsOwnGuards", "The [[:ThievePlace:]] has its own guards.");
            ResourcesText.Add("The[ThievePlace]IsUnguarded", "The [[:ThievePlace:]] is unguarded");
            ResourcesText.Add("TheyGetAwayWith[Number]GoldFromThe[ThievePlace]", "They get away with [[:Number:]] gold from the [[:ThievePlace:]].");
            ResourcesText.Add("Gang[GangName]IsKidnappingGirls", "Gang   [[:GangName:]]   is kidnapping girls.");
            ResourcesText.Add("YourMenFindAGirl[GirlName]AndConvinceHerThatSheShouldWorkForYou", "Your men find a girl, [[:GirlName:]], and convince her that she should work for you.");
            ResourcesText.Add("YourMenFindAGirl[GirlName]AndTryToCatchHerInTheirNets", "Your men find a girl, [[:GirlName:]], and try to catch her in their nets.");
            ResourcesText.Add("YourMenFindAGirl[GirlName]AndAttemptToKidnapHer", "Your men find a girl, [[:GirlName:]], and attempt to kidnap her.");
            ResourcesText.Add("[GirlName]WasTalkedIntoWorkingForYouBy[GangName]", "[[:GirlName:]] was talked into working for you by [[:GangName:]].");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in the tattered remains of their last net.");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their last net.");
            ResourcesText.Add("[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their nets.");
            ResourcesText.Add("SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow", "She struggles against the net your men use, but it is pointless. She is in your dungeon now.");
            ResourcesText.Add("[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]", "[[:GirlName:]] was captured in a net and dragged back to the dungeon by [[:GangName:]].");
            ResourcesText.Add("[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay", "[[:GirlName:]] managed to damage all of their nets so they have to do things the hard way.");
            ResourcesText.Add("SheFightsBackButYourMenSucceedInKidnappingHer", "She fights back but your men succeed in kidnapping her.");
            ResourcesText.Add("[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon", "[[:GirlName:]] fought with [[:GangName:]] but lost. She was dragged back to the dungeon.");
            ResourcesText.Add("TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets.", "The girl fights back and defeats your men before escaping into the streets.");
            ResourcesText.Add("[GangName]KidnapHerSuccessfullyWithoutAFussSheIsInYourDungeonNow", "[[:GangName:]] kidnap her successfully without a fuss. She is in your dungeon now.");
            ResourcesText.Add("[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without a fight.");
            ResourcesText.Add("AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer", "After dodging all of their nets, she gives up when they pull out their weapons and prepare to kill her.");
            ResourcesText.Add("[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without anymore of a fight.");
            ResourcesText.Add("TheyFailedToFindAnyGirlsToKidnap", "They failed to find any girls to kidnap.");
            ResourcesText.Add("Gang[GangName]IsExploringTheCatacombs", "Gang   [[:GangName:]]   is exploring the catacombs.");
            ResourcesText.Add("YouTellThemToGetWhateverTheyCanFind", "You tell them to get whatever they can find.");
            ResourcesText.Add("All[Number]OfThemReturn", "All [[:Number:]] of them return.");
            ResourcesText.Add("[Number]OfThe[GangNumber]WhoWentOutReturn", "[[:Number:]] of the [[:GangNumber:]] who went out return.");
            ResourcesText.Add("TheyBringBackWithThem[Number]Gold", "They bring back with them:   [[:Number:]] gold");
            ResourcesText.Add("YourInventoryIsFull", "Your inventory is full");
            ResourcesText.Add("One[ItemName]", "one [[:ItemName:]]");
            ResourcesText.Add("YourMenAlsoCapturedAGirlNamed[GirlName]", "Your men also captured a girl named [[:GirlName:]]");
            ResourcesText.Add("[GirlName]WasCapturedInTheCatacombsBy[GangName]", "[[:GirlName:]] was captured in the catacombs by [[:GangName:]]");
            ResourcesText.Add("YourMenAlsoCapturedAGirl", "Your men also captured a girl.");
            ResourcesText.Add("YourMenAlsoBringBack[Number]Beasts", "Your men also bring back [[:Number:]] beasts.");
            ResourcesText.Add("YourMenCapturedOneGirl", "Your men captured one girl:");
            ResourcesText.Add("YourMenCaptured[Number]Girls", "Your men captured [[:Number:]] girls:");
            ResourcesText.Add("[UniqueGirlName]Unique", "   [[:UniqueGirlName:]]   (unique)");
            ResourcesText.Add("[GirlName]", "   [[:GirlName:]]");
            ResourcesText.Add("[GirlName] was captured in the catacombs by [GangName]", "[[:GirlName:]] was captured in the catacombs by [[:GangName:]].");
            ResourcesText.Add("YourMenBringBackOneItem", "Your men bring back one item:");
            ResourcesText.Add("YourMenBringBack[Number]Items", "Your men bring back [[:Number:]] items:");
            ResourcesText.Add("[ItemName]", "   [[:ItemName:]]");
            ResourcesText.Add("YourMenBringBack[Number]Beasts", "Your men bring back [[:Number:]] beasts.");
            ResourcesText.Add("YourMenAlsoBringBack[Number]Beasts", "Your men also bring back [[:Number:]] beasts.");
            ResourcesText.Add("Gang[GangName]SpendTheWeekHelpingOutTheCommunity", "Gang   [[:GangName:]]   spend the week helping out the community.");
            ResourcesText.Add("ALocalBoyDecidedToJoinYourGangToHelpOutTheirCommunity", "A local boy decided to join your gang to help out their community.");
            ResourcesText.Add("TwoLocalsDecidedToJoinYourGangToHelpOutTheirCommunity", "Two locals decided to join your gang to help out their community.");
            ResourcesText.Add("SomeLocalsDecidedToJoinYourGangToHelpOutTheirCommunity", "Some locals decided to join your gang to help out their community.");
            ResourcesText.Add("TheyCleanedUpAround[BrothelName]FixingLightsRemovingDebrisAndMakingSureTheAreaIsSecure", "They cleaned up around [[:BrothelName:]] fixing lights, removing debris and making sure the area is secure.");
            ResourcesText.Add("TheyRoundedUpAStrayBeastAndBroughtItToTheBrothel", "They rounded up a stray beast and brought it to the brothel.");
            ResourcesText.Add("TheyRoundedUpTwoStrayBeastsAndBroughtThemToTheBrothel", "They rounded up two stray beasts and brought them to the brothel.");
            ResourcesText.Add("TheyRoundedUpSomeStrayBeastsAndBroughtThemToTheBrothel", "They rounded up some stray beasts and brought them to the brothel.");
            ResourcesText.Add("Security[Number]", "Security + [[:Number:]]");
            ResourcesText.Add("Beasts[Number]", "Beasts + [[:Number:]]");
            ResourcesText.Add("Suspicion[Number]", "Suspicion + [[:Number:]]");
            ResourcesText.Add("CustomerFear[Number]", "Customer Fear + [[:Number:]]");
            ResourcesText.Add("Disposition[Number]", "Disposition + [[:Number:]]");
            ResourcesText.Add("Service[Number]", "Service + [[:Number:]]");
            ResourcesText.Add("Charisma[Number]", "Charisma + [[:Number:]]");
            ResourcesText.Add("Intelligence[Number]", "Intelligence + [[:Number:]]");
            ResourcesText.Add("Agility[Number]", "Agility + [[:Number:]]");
            ResourcesText.Add("Magic[Number]", "Magic + [[:Number:]]");
            ResourcesText.Add("TheyRecieved[Number]GoldInTipsFromGratefulPeople", "They recieved [[:Number:]] gold in tips from grateful people.");
            ResourcesText.Add("Gang[GangName]SpendTheWeekTrainingAndImprovingTheirSkills", "Gang   [[:GangName:]]   spend the week training and improving their skills.");
            ResourcesText.Add("[Number]Combat", "+[[:Number:]] Combat");
            ResourcesText.Add("[Number]Magic", "+[[:Number:]] Magic");
            ResourcesText.Add("[Number]Intelligence", "+[[:Number:]] Intelligence");
            ResourcesText.Add("[Number]Agility", "+[[:Number:]] Agility");
            ResourcesText.Add("[Number]Toughness", "+[[:Number:]] Toughness");
            ResourcesText.Add("[Number]Charisma", "+[[:Number:]] Charisma");
            ResourcesText.Add("[Number]Strength", "+[[:Number:]] Strength");
            ResourcesText.Add("[Number]Service", "+[[:Number:]] Service");
            ResourcesText.Add("Gang[GangName]IsRecruiting", "Gang   [[:GangName:]]   is recruiting.");
            ResourcesText.Add("TheyWereUnableToFindAnyoneToRecruit", "They were unable to find anyone to recruit.");
            ResourcesText.Add("TheyFoundOnePersonToTryToRecruit", "They found one person to try to recruit");
            ResourcesText.Add("TheyFoundPeopleToTryToRecruit", "They found people to try to recruit");
            ResourcesText.Add("AndTheyGotHimToJoin", " and they got him to join.");
            ResourcesText.Add("ButHeDidntWantToJoin", " but he didn't want to join.");
            ResourcesText.Add("ButWereUnableToGetAnyToJoin", " but were unable to get any to join.");
            ResourcesText.Add("AndManagedToGetAllOfThemToJoin", " and managed to get all of them to join.");
            ResourcesText.Add("ButWereOnlyAbleToConvinceOneOfThemToJoin", " but were only able to convince one of them to join.");
            ResourcesText.Add("AndWereAbleToConvince[Number]OfThemToJoin", " and were able to convince [[:Number:]] of them to join.");
            ResourcesText.Add("TheyGotAsManyAsTheyNeededToFillTheirRanks", "They got as many as they needed to fill their ranks.");
            ResourcesText.Add("TheyOnlyHadRoomForOneMoreInTheirGangSoThey", "They only had room for one more in their gang so they ");
            ResourcesText.Add("TheyOnlyHadRoomFor[Number]MoreInTheirGangSoThey", "They only had room for [[:Number:]] more in their gang so they ");
            ResourcesText.Add("SentTheRestToJoin[GangName]", "sent the rest to join [[:GangName:]].");
            ResourcesText.Add("[GangName]SentOneRecruitThatTheyHadNoRoomForTo[ToGangName]", "[[:GangName:]] sent one recruit that they had no room for to [[:ToGangName:]].");
            ResourcesText.Add("[GangName]Sent[Number]RecruitsThatTheyHadNoRoomForTo[ToGangName]", "[[:GangName:]] sent [[:Number:]] recruits that they had no room for to [[:ToGangName:]].");
            ResourcesText.Add("TheyArrived", "They arrived ");
            ResourcesText.Add("TheyAllArrived", "They all arrived ");
            ResourcesText.Add("Only[Number]Arrived", "Only [[:Number:]] arrived ");
            ResourcesText.Add("AndGotAcceptedIntoTheGang", "and got accepted into the gang.");
            ResourcesText.Add("But[GangName]CouldOnlyTake[Number]OfThem", "but [[:GangName:]] could only take [[:Number:]] of them.");
            ResourcesText.Add("ButNoneShowedUp", " but none showed up.");
            ResourcesText.Add("HadToTurnAwayTheRest", " had to turn away the rest.");
            ResourcesText.Add("[GangName]WasLostWhile", "[[:GangName:]] was lost while ");
            ResourcesText.Add("Guarding", "guarding");
            ResourcesText.Add("AttackingYourRivals", "attacking your rivals");
            ResourcesText.Add("SpyingOnYourGirls", "spying on your girls");
            ResourcesText.Add("TryingToRecaptureARunaway", "trying to recapture a runaway");
            ResourcesText.Add("TryingToExtortNewBusinesses", "trying to extort new businesses");
            ResourcesText.Add("PerformingPettyCrimes", "performing petty crimes");
            ResourcesText.Add("PerformingMajorCrimes", "performing major crimes");
            ResourcesText.Add("TryingToKidnapGirls", "trying to kidnap girls");
            ResourcesText.Add("ExploringTheCatacombs", "exploring the catacombs");
            ResourcesText.Add("Training", "training");
            ResourcesText.Add("Recruiting", "recruiting");
            ResourcesText.Add("HelpingTheCommunity", "helping the community");
            ResourcesText.Add("OnAMission", "on a mission");
            ResourcesText.Add("Gang[GangName]WereSetToRecruitDueToLowNumbers", "Gang   [[:GangName:]]   were set to recruit due to low numbers.");
            ResourcesText.Add("Gang[GangName]WerePlacedBackOnTheirPreviousMissionNowThatTheirNumbersAreBackToNormal", "Gang   [[:GangName:]]   were placed back on their previous mission now that their numbers are back to normal.");
            ResourcesText.Add("Gang[GangName]WerePlacedOnGuardDutyFromRecruitmentAsTheirNumbersAreFull", "Gang   [[:GangName:]]   were placed on guard duty from recruitment as their numbers are full.");
            ResourcesText.Add("AllOfTheMenInGang[GangName]HaveDied", "All of the men in gang [[:GangName:]] have died.");
            ResourcesText.Add("Gang[GangName]IsSpyingOnYourGirls", "Gang   [[:GangName:]]   is spying on your girls.");
            ResourcesText.Add("Gang[GangName]IsGuarding", "Gang   [[:GangName:]]   is guarding.");
            ResourcesText.Add("", "");
        }
    }
}
