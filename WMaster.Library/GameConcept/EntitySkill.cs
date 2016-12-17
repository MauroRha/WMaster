using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMaster.Enum;

namespace WMaster.GameConcept
{
    /// <summary>
    /// Skill value of en entity (player, girl, gang, rival...)
    /// <remarks><para>Class used to fix <see cref="EntityAttribute&lt;EnumSkills&gt"/> to <see cref="EnumSkills"/>;</para></remarks>
    /// </summary>
    public class EntitySkill : WMaster.GameConcept.AbstractConcept.EntityAttribute<EnumSkills>
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
            get { return EntitySkill.LOWER_BOUND_LIMIT; }
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
            get { return EntitySkill.UPPER_BOUND_LIMIT; }
        }
    }
}
