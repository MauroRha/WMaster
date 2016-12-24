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
//  <copyright file="GangMissionCaptureGirls.cs" company="The Pink Petal Devloment Team">
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
    using WMaster.Concept;
    using WMaster.Concept.Attributs;
    using WMaster.Entity.Living;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Performe a gang capturing girls mission against player's rival.
    /// <remarks><para>`J` mission returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
    /// </summary>
    public class GangMissionRecaptureGirls : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionRecaptureGirls(Gang gang)
            : base(EnuGangMissions.RecaptureGirls, gang)
        {
        }

        /// <summary>
        /// Check if there is an escaped girl to recapture before launching mission.
        /// </summary>
        /// <returns><b>True</b> if there are girls to recapture, <b>False</b> if not but send a kidnap mission.</returns>
        protected override bool BeforePerformingMission()
        {
            if (!Game.Brothels.RunawaysGirlList.Count.Equals(0))
            { return true; }

            // No girls to recapture... go to KidnapMission for this turn.
            this.GangCible.m_Events.AddMessage(
                LocalString.GetStringLine(LocalString.ResourceStringCategory.Global, "ThisGangWasSentToLookForRunawaysButThereAreNoneSoTheyWentLookingForAnyGirlToKidnapInstead"),
                ImageTypes.PROFILE, EventType.GANG);

            GangMissionBase.SetGangMission(EnuGangMissions.KidnappGirls, this.GangCible);
            bool returnValue = this.GangCible.CurrentMission.DoTheJob();
            this.GangCible.CurrentMission = this;
            return false;
        } 

        // TODO : Methode is too complex, Cyclomatic indicator to high. Need to fractionat it
        /// <summary>
        /// Performe a gang recapturing mission against escaped player's girl.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString recaptureEven = new LocalString();
            recaptureEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsLookingForEscapedGirls",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            // check if any girls have run away, if no runnaway then the gang continues on as normal
            sGirl runnaway = Game.Brothels.GetFirstRunaway();
            if (runnaway == null) // `J` this should have been replaced by a check in the gang mission list
            {
                recaptureEven.AppendLine(
                    LocalString.ResourceStringCategory.Global,
                    "ThereAreNoneOfYourGirlsWhoHaveRunAwaySoTheyHaveNooneToLookFor");
                this.GangCible.m_Events.AddMessage(recaptureEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                return false;
            }

            LocalString RGmsg = new LocalString();
            string girlName = runnaway.m_Realname;
            bool captured = false;
            int damagedNets = 0;
            ImageTypes girlImageType = ImageTypes.PROFILE;
            EventType gangEventType = EventType.GANG;

            if (!Game.Brothels.FightsBack(runnaway))
            {
                recaptureEven.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourGoonsFind[GirlName]AndSheComesQuietlyWithoutPuttingUpAFight",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                RGmsg.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "[GirlName]WasRecapturedBy[GangName]SheGaveUpWithoutAFight",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                captured = true;
            }
            if (!captured && this.GangCible.NetLimit > 0) // try to capture using net
            {
                recaptureEven.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourGoonsFind[GirlName]AndTheyTryToCatchHerInTheirNets",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                int tries = 0;
                while (this.GangCible.NetLimit > 0 && !captured)
                {
                    int damagechance = 40;
                    if (WMRand.Percent(this.GangCible.Combat)) // hit her with the net
                    {
                        if (!WMRand.Percent((double)(runnaway.agility() - tries) / 2.0)) // she can't avoid or get out of the net
                        {
                            captured = true;
                        }
                        else
                        {
                            damagechance = 60;
                        }
                    }

                    if (WMRand.Percent(damagechance))
                    {
                        damagedNets++;
                        this.GangCible.AdjustNetLimit(-1);
                        GangManager.UseANet();
                    }
                    tries++;
                }
                if (captured)
                {
                    if (damagedNets > 0)
                    {
                        if (this.GangCible.NetLimit == 0)
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                        else if (this.GangCible.NetLimit == 1)
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                        else
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                    }
                    recaptureEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow");
                    girlImageType = ImageTypes.DEATH;
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                    GangManager.BoostGangSkill(this.GangCible.Stats[EnumStats.INTELLIGENCE], 2);
                }
                else
                {
                    recaptureEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
            }
            if (!captured)
            {
                if (Game.Brothels.FightsBack(runnaway)) // kidnap her
                {
                    if (damagedNets == 0)
                    {
                        recaptureEven.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GangName]AttemptToRecaptureHer",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
                    }
                    if (!GangManager.GangCombat(runnaway, this.GangCible))
                    {
                        girlImageType = ImageTypes.DEATH;
                        recaptureEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "SheFightsBackButYourMenSucceedInCapturingHer");
                        RGmsg.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                        GangManager.BoostGangSkill(this.GangCible.Skills[EnumSkills.COMBAT], 1);
                        captured = true;
                    }
                    else
                    {
                        recaptureEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets");
                        gangEventType = EventType.DANGER;
                    }
                }
                else if (damagedNets == 0)
                {
                    recaptureEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]RecaptureHerSuccessfullyWithoutAFussSheIsInYourDungeonNow",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                    captured = true;
                }
                else
                {
                    recaptureEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer");
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", this.GangCible.Name) });
                    captured = true;
                }
            }

            this.GangCible.m_Events.AddMessage(recaptureEven.ToString(), ImageTypes.PROFILE, gangEventType);
            if (captured)
            {
                runnaway.m_Events.AddMessage(RGmsg.ToString(), girlImageType, EventType.GANG);
                runnaway.m_RunAway = 0;
                Game.Brothels.RemoveGirlFromRunaways(runnaway);
                Game.Dungeon.AddGirl(runnaway, DungeonReasons.GIRLRUNAWAY);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.Global, "GangMissionRecaptureGirls");
        }
    }
}
