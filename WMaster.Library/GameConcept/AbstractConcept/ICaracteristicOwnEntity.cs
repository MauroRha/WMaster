using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.GameConcept.AbstractConcept
{
    /// <summary>
    /// Represent an entity who have set of evolutiv caracteristics.
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
