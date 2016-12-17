using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.GameConcept.AbstractConcept
{
    /// <summary>
    /// Implement a turn based functionnality.
    /// </summary>
    public interface ITurnable
    {
        /// <summary>
        /// Close current week and do traitement for initialise new week.
        /// </summary>
        void NextWeek();
        /// <summary>
        /// Close current year and do traitement for initialise new year.
        /// </summary>
        void NextYear();
    }
}
