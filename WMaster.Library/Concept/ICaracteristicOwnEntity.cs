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
namespace WMaster.Concept
{

    /// <summary>
    /// Represent an entity who have set of evolutive caracteristics.
    /// </summary>
    public interface ICaracteristicOwnEntity
    {
        /// <summary>
        /// Get the <see cref="SkillsCollection"/> of all <see cref="EnumSkills"/> of entity.
        /// </summary>
        SkillsCollection Skills { get; }
        /// <summary>
        /// Adjust entity skill to skill value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="skill">Skill to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        void AdjustSkill(EnumSkills skill, int amount);

        /// <summary>
        /// Get the <see cref="StatsCollection"/> of all <see cref="EnumStats"/> of entity.
        /// </summary>
        StatsCollection Stats { get; }
        /// <summary>
        /// Adjust entity statistic to statistic value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="stat">Stat to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        void AdjustStat(EnumStats stat, int amount);
    }
}
