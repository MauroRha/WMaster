using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster
{
    [Obsolete("Dont use this class, need to initialise ans save configuration data outside project execution", false)]
    public class TempConfiguration
    {
        public System.Collections.Specialized.NameValueCollection ResourcesText = new System.Collections.Specialized.NameValueCollection()
        {
            {"Gang[GangName]IsAttackingRivals", "Gang   [[:GangName:]]   is attacking rivals."},
            {"TheyFailedToFindAnyEnemyAssetsToHit", "They failed to find any enemy assets to hit."},
            {"ScoutedTheCityInVainSeekingWouldBeChallengersToYourDominance.", "Scouted the city in vain, seeking would-be challengers to your dominance."},
            {"YourMenRunIntoAGangFrom[GangName]AndABrawlBreaksOut", "Your men run into a gang from [[:GangName:]] and a brawl breaks out."},
            {"YourGang[GangName]FailsToReportBackFromTheirSabotageMissionLaterYouLearnThatTheyWereWipedOutToTheLastMan.", "Your gang [[:GangName:]] fails to report back from their sabotage mission.[[:NewLine:]]Later you learn that they were wiped out to the last man."},
            {"YourMenLostTheLoneSurvivorFightsHisWayBackToFriendlyTerritory", "Your men lost. The lone survivor fights his way back to friendly territory."},
            {"YourMenLostThe[GangMemberNum]SurvivorsFightTheirWayBackToFriendlyTerritory", "Your men lost. The [[:GangMemberNum:]] survivors fight their way back to friendly territory."},
            {"YourMenWin", "Your men win."},
            {"TheEnemyGangIsDestroyed[RivalGangName]HasNoMoreGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has no more gangs left!"},
            {"TheEnemyGangIsDestroyed[RivalGangName]HasAFewGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has a few gangs left."},
            {"TheEnemyGangIsDestroyed[RivalGangName]HasALotOfGangsLeft", "The enemy gang is destroyed. [[:RivalName:]] has a lot of gangs left."},
            {"YourMenEncounterNoResistanceWhenyougoAfter[RivalGangName]", "Your men encounter no resistance when you go after [[:RivalGangName:]]."},
            {"YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveNoMoreBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have no more businesses left!"},
            {"YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveAFewBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have a few businesses left."},
            {"YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveALotOfBusinessesLeft", "Your men destroy [[:Number:]] of their businesses. [[:RivalName:]] have a lot of businesses left."},
            {"[RivalName]HaveNoBusinessesToAttack", "[[:RivalName:]] have no businesses to attack."},
            {"YourMenSteal[GoldAmount]GoldFromThemMuhahahaha[RivalName]IsPennilessNow", "Your men steal [[:GoldAmount:]] gold from them. Mu-hahahaha!  [[:RivalName:]] is penniless now!"},
            {"YourMenSteal[GoldAmount]GoldFromThem[RivalName]IsLookingPrettyPoor", "Your men steal [[:GoldAmount:]] gold from them. [[:RivalName:]] is looking pretty poor."},
            {"YourMenSteal[GoldAmount]GoldFromThemItLooksLike[RivalName]StillHasALotOfGold", "Your men steal [[:GoldAmount:]] gold from them. It looks like [[:RivalName:]] still has a lot of gold."},
            {"AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAGoldBearerBondWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops a Gold Bearer Bond worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke."},
            {"AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAStackOfGoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops a stack of Gold Bearer Bonds worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke."},
            {"AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDrops[BurnedBonds]GoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke", "As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops [[:BurnedBonds:]] Gold Bearer Bonds worth 10k gold each. [[:BurnedBondsCost:]] gold just went up in smoke."},
            {"AsTheyAreBeingChasedThroughTheStreetsBy[RivalName]sPeopleOneOfYourGangMembersCutsOpenASackOfGoldSpillingItsContentsInTheStreetAsThThrongsOfCiviliansStreamInToCollectTheCoinsTheBlockThePursuersAndAllowYouMenToGetAwaySafely", "As they are being chased through the streets by [[:RivalName:]]'s people, one of your gang members cuts open a sack of gold spilling its contents in the street. As the throngs of civilians stream in to collect the coins, they block the pursuers and allow you men to get away safely."},
            {"AsYourGangLeaveYourRivalsTerritoryOnTheWayBackToYourBrothelTheyComeUponABandOfLocalPoliceThatAreHuntingThemTheirBossDemands[BribePercent]OfWhatYourGangIsCarryingInOrderToLetThemGoTheyPayThem[Bribe]GoldAndContinueOnHome", "As your gang leave your rival's territory on the way back to your brothel, they come upon a band of local police that are hunting them. Their boss demands [[:BribePercent:]]% of what your gang is carrying in order to let them go. They pay them [[:Bribe:]] gold and continue on home."},
            {"[GangName]ReturnsWith[GoldAmount]Gold", "[[:GangName:]] returns with [[:GoldAmount:]] gold."},
            {"TheLosersHaveNoGoldToTake", "The losers have no gold to take."},
            {"YourMenStealAnItemFromThemOne[ItemName]", "Your men steal an item from them, one [[:ItemName:]]."},
            {"YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasNoBrothelsLeft", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] has no Brothels left."},
            {"YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]IsInControlOfVeryFewBrothels", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] is in control of very few Brothels."},
            {"YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasManyBrothelsLeft", "Your men burn down one of [[:RivalName:]]'s Brothels. [[:RivalName:]] has many Brothels left."},
            {"YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasNoGamblingHallsLeft", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] has no Gambling Halls left."},
            {"YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]IsInControlOfVeryFewGamblingHalls", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] is in control of very few Gambling Halls."},
            {"YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasManyGamblingHallsLeft", "Your men burn down one of [[:RivalName:]]'s Gambling Halls. [[:RivalName:]] has many Gambling Halls left."},
            {"YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasNoBarsLeft", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] has no Bars left."},
            {"YourMenBurnDownOneOf[RivalName]sBars[RivalName]IsInControlOfVeryFewBars", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] is in control of very few Bars"},
            {"YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasManyBarsLeft", "Your men burn down one of [[:RivalName:]]'s Bars. [[:RivalName:]] has many Bars left"},
            {"Gang[GangName]IsLookingForEscapedGirls", "Gang   [[:GangName:]]   is looking for escaped girls."},
            {"ThereAreNoneOfYourGirlsWhoHaveRunAwaySoTheyHaveNooneToLookFor", "There are none of your girls who have run away, so they have noone to look for."},
            {"YourGoonsFind[GirlName]AndSheComesQuietlyWithoutPuttingUpAFight", "Your goons find [[:GirlName:]] and she comes quietly without putting up a fight."},
            {"YourGoonsFind[GirlName]AndTheyTryToCatchHerInTheirNets", "Your goons find [[:GirlName:]] and they try to catch her in their nets."},
            {"[GirlName]WasRecapturedBy[GangName]SheGaveUpWithoutAFight", "[[:GirlName:]] was recaptured by [[:GangName:]]. She gave up without a fight."},
            {"[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in the tattered remains of their last net."},
            {"[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their last net."},
            {"[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets", "[[:GirlName:]] managed to damage [[:Number:]] of their nets before they finally caught her in their nets."},
            {"SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow", "She struggles against the net your men use, but it is pointless. She is in your dungeon now."},
            {"[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]", "[[:GirlName:]] was captured in a net and dragged back to the dungeon by [[:GangName:]]."},
            {"[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay", "[[:GirlName:]] managed to damage all of their nets so they have to do things the hard way."},
            {"[GangName]AttemptToRecaptureHer", "[[:GangName:]] attempt to recapture her."},
            {"SheFightsBackButYourMenSucceedInCapturingHer", "She fights back but your men succeed in capturing her."},
            {"[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon", "[[:GirlName:]] fought with [[:GangName:]] but lost. She was dragged back to the dungeon."},
            {"TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets", "The girl fights back and defeats your men before escaping into the streets."},
            {"[GangName]RecaptureHerSuccessfullyWithoutAFussSheIsInYourDungeonNow", "[[:GangName:]] recapture her successfully without a fuss. She is in your dungeon now."},
            {"[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without a fight."},
            {"AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer", "After dodging all of their nets, she gives up when they pull out their weapons and prepare to kill her."},
            {"[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight", "[[:GirlName:]] was surrounded by [[:GangName:]] and gave up without anymore of a fight."}
        };
    }
}
