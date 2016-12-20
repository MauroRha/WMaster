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
namespace WMaster.Game.Concept
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represent the Skills collection of an entity (player, girl, gang, rival...)
    /// </summary>
    public class SkillsCollection : IReadOnlyCollection<Skill>
    {
        /// <summary>
        /// Internal read only collection representing all skills of an entity.
        /// </summary>
        private ReadOnlyCollection<Skill> _collection;

        /// <summary>
        /// Get specific <see cref="Skill"/>.
        /// </summary>
        /// <param name="skill"><see cref="Skills"/> of <see cref="Skill"/> to retrive</param>
        /// <returns><see cref="Skill"/> of <see cref="Skills"/>.</returns>
        public Skill this[EnumSkills skill]
        {
            get
            {
                return this._collection
                    .Where(c => c.Caracteristique.Equals(skill))
                    .First();
            }
        }

        /// <summary>
        /// Get a new instance of <see cref="SkillsCollection"/>. List skill is initialised with all skills to 0.
        /// </summary>
        public SkillsCollection()
        {
            List<Skill> listSkills = new List<Skill>();
            foreach (EnumSkills item in System.Enum.GetValues(typeof(EnumSkills)))
            {
                listSkills.Add(
                    new Skill()
                    {
                        Caracteristique = item,
                        CurrentValue = 0,
                        LastWeekValue = 0,
                        LastYearValue = 0
                    }
                );
            }
            this._collection = new ReadOnlyCollection<Skill>(listSkills);
        }

        #region IReadOnlyCollection<EntitySkill> members and wrapper to this._collection
        /// <summary>
        /// Return the total number of elements in the collection.
        /// </summary>
        public int Count
        {
            get { return this._collection.Count(); }
        }

        /// <summary>
        /// Get iterator of collection.
        /// </summary>
        /// <returns>iterator of collection</returns>
        public IEnumerator<Skill> GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        /// <summary>
        /// Get iterator of collection.
        /// </summary>
        /// <returns>iterator of collection</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }
        #endregion
    }
}
