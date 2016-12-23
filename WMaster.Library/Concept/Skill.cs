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
    /// Skill value of en entity (player, girl, gang, rival...)
    /// <remarks><para>Class used to fix <see cref="EntityAttribute&lt;EnumSkills&gt"/> to <see cref="EnumSkills"/>;</para></remarks>
    /// </summary>
    public class Skill : Attribute<EnumSkills>
    {
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        private const int LOWER_BOUND_LIMIT = 0;
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        public override int LowerBoundLimit
        {
            get { return Skill.LOWER_BOUND_LIMIT; }
        }

        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        private const int UPPER_BOUND_LIMIT = 100;
        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        public override int UpperBoundLimit
        {
            get { return Skill.UPPER_BOUND_LIMIT; }
        }
    }
}
