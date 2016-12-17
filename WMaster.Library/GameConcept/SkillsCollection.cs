using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;

namespace WMaster.GameConcept
{
    /// <summary>
    /// Represent the Skills collection of an entity (player, girl, gang, rival...)
    /// </summary>
    public class SkillsCollection : IReadOnlyCollection<EntitySkill>
    {
        /// <summary>
        /// Internal read only collection representing all skills of an entity.
        /// </summary>
        private ReadOnlyCollection<EntitySkill> _collection;

        /// <summary>
        /// Get specific <see cref="EntitySkill"/>.
        /// </summary>
        /// <param name="skill"><see cref="Skills"/> of <see cref="EntitySkill"/> to retrive</param>
        /// <returns><see cref="EntitySkill"/> of <see cref="Skills"/>.</returns>
        public EntitySkill this[EnumSkills skill]
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
            List<EntitySkill> listSkills = new List<EntitySkill>();
            foreach (EnumSkills item in System.Enum.GetValues(typeof(EnumSkills)))
            {
                listSkills.Add(
                    new EntitySkill()
                    {
                        Caracteristique = item,
                        CurrentValue = 0,
                        LastWeekValue = 0,
                        LastYearValue = 0
                    }
                );
            }
            this._collection = new ReadOnlyCollection<EntitySkill>(listSkills);
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
        public IEnumerator<EntitySkill> GetEnumerator()
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
