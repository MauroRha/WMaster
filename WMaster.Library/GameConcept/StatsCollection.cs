using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.GameConcept
{
    /// <summary>
    /// Represent the Stats collection of an entity (player, girl, gang, rival...)
    /// </summary>
    public class StatsCollection : IReadOnlyCollection<EntityStat>
    {
        /// <summary>
        /// Internal read only collection representing all stats of an entity.
        /// </summary>
        private ReadOnlyCollection<EntityStat> _collection;

        /// <summary>
        /// Get specific <see cref="EntityStat"/>.
        /// </summary>
        /// <param name="stat"><see cref="EnumStats"/> of <see cref="EntityStat"/> to retrive</param>
        /// <returns><see cref="EntityStat"/> of <see cref="EnumStats"/>.</returns>
        public EntityStat this[EnumStats stat]
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
            List<EntityStat> listStats = new List<EntityStat>();
            foreach (EnumStats item in System.Enum.GetValues(typeof(EnumStats)))
            {
                listStats.Add(
                    new EntityStat()
                    {
                        Caracteristique = item,
                        CurrentValue = 0,
                        LastWeekValue = 0,
                        LastYearValue = 0
                    }
                );
            }
            this._collection = new ReadOnlyCollection<EntityStat>(listStats);
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
        public IEnumerator<EntityStat> GetEnumerator()
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
