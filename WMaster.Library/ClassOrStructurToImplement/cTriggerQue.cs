using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cTriggerQue : System.IDisposable
    {
        public cTrigger m_Trigger; // the trigger that needs to be triggered
        public cTriggerQue m_Next; // the next one in the que
        public cTriggerQue m_Prev; // the previous one in the que

        public cTriggerQue()
        {
            m_Trigger = null;
            m_Next = m_Prev = null;
        }
        public void Dispose()
        {
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Prev = m_Next = null;
            m_Trigger = null;
        }
    }
}
