using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.GameConcept
{
    /// <summary>
    /// Skill value of en entity (player, girl, gang, rival...)
    /// <remarks><para>Class used to fix EntityAttribute&lt;Skills&gt;</para></remarks>
    /// </summary>
    public class EntityStat : WMaster.GameConcept.AbstractConcept.EntityAttribute<EnumStats>
    {
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        private const int LOWER_BOUND_LIMIT = -100;
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        protected override int LowerBoundLimit
        {
            get { return EntityStat.LOWER_BOUND_LIMIT; }
        }

        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        private const int UPPER_BOUND_LIMIT = 100;
        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        protected override int UpperBoundLimit
        {
            get { return EntityStat.UPPER_BOUND_LIMIT; }
        }
    }
}
