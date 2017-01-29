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
//  <copyright file="GangMissionService.cs" company="The Pink Petal Devloment Team">
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

    /// <summary>
    /// Sercicing mission affecte to gang.
    /// </summary>
    public sealed class GangMissionService : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionService(Gang gang)
            : base(EnuGangMissions.Service, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.GangMission, "MissionService");
        }

        /// <summary>
        /// Performe a gang service mission.
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString serviceMissionEvent = new LocalString();
            serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                "Gang[GangName]SpendTheWeekHelpingOutTheCommunity",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            int suspicion = WMRand.Bell(0, 2);
            int customerFear = WMRand.Bell(0, 2);
            int disposition = WMRand.Bell(0, 3);
            int service = WMRand.Bell(0, 3);
            int charisma = 0;
            int intelligence = 0;
            int agility = 0;
            int magic = 0;
            int gold = 0;
            int security = 0;
            int beasts = 0;
            int percent = Math.Max(10, Math.Min(this.GangCible.MemberNum * 6, this.GangCible.Service));

            for (int i = 0; i < this.GangCible.MemberNum / 2; i++)
            {
                if (WMRand.Percent(percent))
                {
                    switch (WMRand.Random(9))
                    {
                        case 0:
                            suspicion++;
                            break;
                        case 1:
                            customerFear++;
                            break;
                        case 2:
                            disposition++;
                            break;
                        case 3:
                            charisma++;
                            break;
                        case 4:
                            intelligence++;
                            break;
                        case 5:
                            agility++;
                            break;
                        case 6:
                            magic++;
                            break;
                        case 7:
                            gold += WMRand.Random(10) + 1;
                            break;
                        default:
                            service++;
                            break;
                    }
                }
            }

            if (this.GangCible.MemberNum < 15 && WMRand.Percent(Math.Min(50, this.GangCible.Charisma)))
            {
                int addNum = Math.Max(1, WMRand.Bell(-2, 4));
                if (addNum + this.GangCible.MemberNum > 15)
                {
                    addNum = 15 - this.GangCible.MemberNum;
                }
                serviceMissionEvent.NewLine(); ;

                if (addNum <= 1)
                {
                    addNum = 1;
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "ALocalBoyDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                else if (addNum == 2)
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TwoLocalsDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                else
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "SomeLocalsDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                this.GangCible.MemberNum += addNum;
            }

            if (WMRand.Percent(Math.Max(10, Math.Min(this.GangCible.MemberNum * 6, this.GangCible.Intelligence))))
            {
                sBrothel brothel = Game.Brothels.GetRandomBrothel();
                security = Math.Max(5 + WMRand.Random(26), this.GangCible.Intelligence / 4);
                brothel.SecurityLevel += security;
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                    "TheyCleanedUpAround[BrothelName]FixingLightsRemovingDebrisAndMakingSureTheAreaIsSecure",
                    new List<FormatStringParameter>() { new FormatStringParameter("BrothelName", brothel.Name) });
            }
            if (WMRand.Percent(Math.Max(10, Math.Min(this.GangCible.MemberNum * 6, this.GangCible.Intelligence))))
            {
                beasts += (Math.Max(1, WMRand.Bell(-4, 4)));
                if (beasts <= 1)
                {
                    beasts = 1;
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TheyRoundedUpAStrayBeastAndBroughtItToTheBrothel");
                }
                else if (beasts == 2)
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TheyRoundedUpTwoStrayBeastsAndBroughtThemToTheBrothel");
                }
                else
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.GangMission, "TheyRoundedUpSomeStrayBeastsAndBroughtThemToTheBrothel");
                }
            }

            if (security > 0)
            {
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Brothel, "BrothelSecurity");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", security));
            }
            if (beasts > 0)
            {
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "GlobalBeasts");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", beasts));
            }
            if (suspicion > 0)
            {
                Game.Player.suspicion(-suspicion);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Player, "PlayerSuspicion");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", suspicion));
            }
            if (customerFear > 0)
            {
                Game.Player.customerfear(-customerFear);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Player, "PlayerCustomerFear");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", customerFear));
            }
            if (disposition > 0)
            {
                Game.Player.disposition(disposition);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Player, "PlayerDisposition");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", disposition));
            }
            if (service > 0)
            {
                this.GangCible.AdjustSkill(EnumSkills.SERVICE, service);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "AttributService");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", service));
            }
            if (charisma > 0)
            {
                this.GangCible.AdjustStat(EnumStats.CHARISMA, charisma);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "AttributCharisma");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", charisma));
            }
            if (intelligence > 0)
            {
                this.GangCible.AdjustStat(EnumStats.INTELLIGENCE, intelligence);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "AttributIntelligence");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", intelligence));
            }
            if (agility > 0)
            {
                this.GangCible.AdjustStat(EnumStats.AGILITY, agility);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "AttributAgility");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", agility));
            }
            if (magic > 0)
            {
                this.GangCible.AdjustSkill(EnumSkills.MAGIC, magic);
                serviceMissionEvent.Append(LocalString.ResourceStringCategory.Global, "AttributMagic");
                serviceMissionEvent.AppendLitteral(string.Format(" + {0}", magic));
            }
            if (gold > 0)
            {
                Game.Gold.MiscCredit(gold);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                    "TheyRecieved[Number]GoldInTipsFromGratefulPeople",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
            }

            this.GangCible.m_Events.AddMessage(serviceMissionEvent.ToString(), ImageType.PROFILE, EventType.Gang);
            return true;
        }
    }
}
