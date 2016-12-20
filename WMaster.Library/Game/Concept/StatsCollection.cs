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
    /// Represent the Stats collection of an entity (player, girl, gang, rival...)
    /// </summary>
    public class StatsCollection : IReadOnlyCollection<Stat>
    {
        /// <summary>
        /// Internal read only collection representing all stats of an entity.
        /// </summary>
        private ReadOnlyCollection<Stat> _collection;

        /// <summary>
        /// Get specific <see cref="Stat"/>.
        /// </summary>
        /// <param name="stat"><see cref="EnumStats"/> of <see cref="Stat"/> to retrive</param>
        /// <returns><see cref="Stat"/> of <see cref="EnumStats"/>.</returns>
        public Stat this[EnumStats stat]
        {
            get
            {
                return this._collection
                    .Where(c => c.Caracteristique.Equals(stat))
                    .First();
            }
        }

        /// <summary>
        /// Get a new instance of <see cref="StatsCollection"/>. List skill is initialised with all skills to 0.
        /// </summary>
        public StatsCollection()
        {
            List<Stat> listStats = new List<Stat>();
            foreach (EnumStats item in System.Enum.GetValues(typeof(EnumStats)))
            {
                listStats.Add(
                    new Stat()
                    {
                        Caracteristique = item,
                        CurrentValue = 0,
                        LastWeekValue = 0,
                        LastYearValue = 0
                    }
                );
            }
            this._collection = new ReadOnlyCollection<Stat>(listStats);
        }

        #region IReadOnlyCollection<EntityStat> members and wrapper to this._collection
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
        public IEnumerator<Stat> GetEnumerator()
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
