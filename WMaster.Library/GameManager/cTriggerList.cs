using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    using System.Collections.Generic;
    using System.IO;

    public class cTriggerList : System.IDisposable
    {
        public cTriggerList()
        {
            m_Triggers = null;
            m_CurrTrigger = null;
            m_Last = null;
            m_NumTriggers = 0;
            m_GirlTarget = null;
        }
        public void Dispose()
        {
            Free();
        }

        void Free()
        { throw new NotImplementedException(); }
        void LoadList(string filename)
        { throw new NotImplementedException(); }
        IXmlElement SaveTriggersXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadTriggersXML(IXmlHandle hTriggers)
        { throw new NotImplementedException(); }
        void LoadTriggersLegacy(Stream ifs)
        { throw new NotImplementedException(); }

        void AddTrigger(cTrigger trigger)
        { throw new NotImplementedException(); }

        void AddToQue(cTrigger trigger)
        { throw new NotImplementedException(); }
        void RemoveFromQue(cTrigger trigger)
        { throw new NotImplementedException(); }
        cTriggerQue GetNextQueItem()
        { throw new NotImplementedException(); }

        cTrigger CheckForScript(int Type, bool trigger, int[] values)
        { throw new NotImplementedException(); }

        void ProcessTriggers()
        { throw new NotImplementedException(); } // function that process the triggers in the list and adds them to the que if the conditions are met
        void ProcessNextQueItem(string fileloc)
        { throw new NotImplementedException(); }

        // set script targets
        public void SetGirlTarget(sGirl girl)
        {
            //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to variables (in C#, the variable no longer points to the original when the original variable is re-assigned):
            //ORIGINAL LINE: m_GirlTarget = girl;
            m_GirlTarget = girl;
        }

        bool HasRun(int num)
        { throw new NotImplementedException(); }

        private cTrigger m_CurrTrigger;
        private cTrigger m_Triggers;
        private cTrigger m_Last;
        private int m_NumTriggers;

        //int m_NumQued;
        //cTriggerQue* m_StartQue;
        //cTriggerQue* m_EndQue;
        private Queue<cTriggerQue> m_TriggerQueue = new Queue<cTriggerQue>(); //mod


        // script targets (things that the script will affect with certain commands)
        private sGirl m_GirlTarget; // if not 0 then the script is affecting a girl
    }

}
