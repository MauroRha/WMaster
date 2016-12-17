using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class sScript : System.IDisposable
    {
        public int m_Type; // 0 to (number of actions-1)
        public int m_NumEntries; // # entries in this script action
        public sScriptEntry m_Entries; // Array of entries
        public sScript m_Prev; // Prev in linked list
        public sScript m_Next; // Next in linked list

        public sScript()
        {
            m_Type = 0; // Clear to defaults
            m_NumEntries = 0;
            m_Entries = null;
            m_Prev = m_Next = null;
        }

        public void Dispose()
        {
            //		if (m_Entries) delete[] m_Entries;
            m_Entries = null; // Delete entry array
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Prev = m_Next = null; // Delete next in linked list
        }
    }

}
