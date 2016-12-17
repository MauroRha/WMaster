using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.GameConcept.AbstractConcept
{
    public interface IValuableAttribut
    {
        /// <summary>
        /// Get or ser the current value of Attribute. Just a Wrapper to CurrentValue property.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Get or set the current value of Attribute
        /// </summary>
        int CurrentValue { get; set; }
    }
}
