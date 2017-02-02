using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;

namespace WMaster.ClassOrStructurToImplement
{
    /// <summary>
    /// holds an objective and its data
    /// </summary>
    public class sObjective
    {
        /// <summary>
        /// Objective type.
        /// </summary>
        public Objectives Objective { get; set; }
        public int m_Reward; // the reward type
        /// <summary>
        /// The x variable for the objective.
        /// </summary>
        public int Target { get; set; }
        /// <summary>
        /// How much of the x variable has been achieved.
        /// </summary>
        public int SoFar;
        public int m_Limit; // the number of weeks must be done by
        public int m_Difficulty; // a number representing how hard it is
        /// <summary>
        ///  Save the text for pass objective report.
        /// </summary>
        // TODO : TRANSLATE - Control Objective text is realy translate.
        public string Text { get; set; }
    }
}
