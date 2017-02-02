using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;
using WMaster.Concept;
using WMaster.Concept.Attributs;

namespace WMaster.ClassOrStructurToImplement
{
    public class sCustomer : System.IDisposable
    {
        // Regular Stats
        public byte m_IsWoman; // 0 means a man, 1 means a woman
        public byte m_Amount; // how many customers this represents
        public byte m_Class; // is the person rich, poor or middle class
        public byte m_Official; // is the person an official of the town

        public uint m_Money;

        public bool m_HasAIDS; // `J` Does the customer have AIDS?
        public bool m_HasChlamydia; // `J` Does the customer have Chlamydia?
        public bool m_HasSyphilis; // `J` Does the customer have Syphilis?
        public bool m_HasHerpes; // `J` Does the customer have Herpes?

        public int[] m_Stats = new int[(int)EnumStats.NUM_STATS];
        public int[] m_Skills = new int[(int)EnumSkills.NUM_SKILLS];

        // `J` What the customers want to do in the building
        public byte m_GoalA; // Primary goal
        public byte m_GoalB; // Secondary goal
        public byte m_GoalC; // Backup goal

        public byte m_Fetish; // the customers fetish
        public byte m_SexPref; // their sex preference
        public byte m_SexPrefB; // their secondary sex preference

        public byte m_ParticularGirl; // the id of the girl he wants

        public sCustomer m_Next;
        public sCustomer m_Prev;

        sCustomer()
        { throw new NotImplementedException(); }

        public void Dispose()
        { throw new NotImplementedException(); }

        public int happiness()
        {
            return m_Stats[(int)EnumStats.Happiness];
        }
    }
}
