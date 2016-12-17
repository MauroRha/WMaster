using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    /// <summary>
    /// holds an objective and its data
    /// </summary>
    public struct sObjective
    {
        public int m_Objective; // the objective type
        public int m_Reward; // the reward type
        public int m_Target; // the x variable for the objective
        public int m_SoFar; // how much of the x variable has been achieved
        public int m_Limit; // the number of weeks must be done by
        public int m_Difficulty; // a number representing how hard it is
        public string m_Text; // save the text for pass objective report.
    }
}
