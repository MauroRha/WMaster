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
//  <copyright file="GangMissionTraining.cs" company="The Pink Petal Devloment Team">
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
    using System.Collections.Generic;
    using WMaster.Concept.Attributs;
    using WMaster.Enums;
    using WMaster.Manager;

    /// <summary>
    /// Training mission affecte to gang.
    /// </summary>
    public sealed class GangMissionTraining : GangMissionBase
    {
        /// <summary>
        /// Initialise base instantce of gang mission.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> who perform the mission.</param>
        internal GangMissionTraining(Gang gang)
            : base(EnuGangMissions.Training, gang)
        {
        }

        /// <summary>
        /// Return localized mission name.
        /// <remarks><para>Will be use like "GangName is [...]."</para></remarks>
        /// </summary>
        /// <returns>The name of mission.</returns>
        public override string GetMissionName()
        {
            return LocalString.GetString(LocalString.ResourceStringCategory.GangMission, "MissionTraining");
        }

        /// <summary>
        /// Performe a gang training mission.
        /// <remarks><para>`J` - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <returns><b>True</b> if mission is a sucess</returns>
        protected override bool PerformingMission()
        {
            LocalString gangTrainingEvent = new LocalString();
            gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.GangMission,
                "Gang[GangName]SpendTheWeekTrainingAndImprovingTheirSkills",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", this.GangCible.Name) });

            int oldCombat = this.GangCible.Combat;
            int oldMagic = this.GangCible.Magic;
            int oldIntelligence = this.GangCible.Intelligence;
            int oldAgility = this.GangCible.Agility;
            int oldConstitution = this.GangCible.Constitution;
            int oldCharisma = this.GangCible.Charisma;
            int oldStrength = this.GangCible.Strength;
            int oldService = this.GangCible.Service;

            List<IValuableAttribut> possibleSkills = new List<IValuableAttribut>();
            possibleSkills.Add(this.GangCible.Skills[EnumSkills.Combat]);
            possibleSkills.Add(this.GangCible.Skills[EnumSkills.Magic]);
            possibleSkills.Add(this.GangCible.Stats[EnumStats.Intelligence]);
            possibleSkills.Add(this.GangCible.Stats[EnumStats.Agility]);
            possibleSkills.Add(this.GangCible.Stats[EnumStats.Constitution]);
            possibleSkills.Add(this.GangCible.Stats[EnumStats.Charisma]);
            possibleSkills.Add(this.GangCible.Stats[EnumStats.Strength]);
            possibleSkills.Add(this.GangCible.Skills[EnumSkills.Service]);

            int count = WMRand.Random(3) + 2; // get 2-4 potential skill/stats to boost
            for (int i = 0; i < count; i++)
            {
                int boostCount = WMRand.Random(3) + 1; // boost each 1-3 times
                GangManager.BoostGangRandomSkill(possibleSkills, 1, boostCount);
            }
            possibleSkills.Clear();

            if (this.GangCible.Skills[EnumSkills.Combat].Value > oldCombat)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Skills[EnumSkills.Combat].Value - oldCombat));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeCombat");
            }
            if (this.GangCible.Skills[EnumSkills.Magic].Value > oldMagic)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Skills[EnumSkills.Magic].Value - oldMagic));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeMagic");
            }
            if (this.GangCible.Stats[EnumStats.Intelligence].Value > oldIntelligence)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Stats[EnumStats.Intelligence].Value - oldIntelligence));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeIntelligence");
            }
            if (this.GangCible.Stats[EnumStats.Agility].Value > oldAgility)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Stats[EnumStats.Agility].Value - oldAgility));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeAgility");
            }
            if (this.GangCible.Stats[EnumStats.Constitution].Value > oldConstitution)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Stats[EnumStats.Constitution].Value - oldConstitution));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeToughness");
            }
            if (this.GangCible.Stats[EnumStats.Charisma].Value > oldCharisma)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Stats[EnumStats.Charisma].Value - oldCharisma));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeCharisma");
            }
            if (this.GangCible.Stats[EnumStats.Strength].Value > oldStrength)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Stats[EnumStats.Strength].Value - oldStrength));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeStrength");
            }
            if (this.GangCible.Skills[EnumSkills.Service].Value > oldService)
            {
                gangTrainingEvent.AppendLitteral(string.Format("{0} ", this.GangCible.Skills[EnumSkills.Service].Value - oldService));
                gangTrainingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AttributeService");
            }

            this.GangCible.Events.AddMessage(gangTrainingEvent.ToString(), ImageType.PROFILE, EventType.Gang);
            this.GangCible.HasSeenCombat = false;
            return false;
        }
    }
}
