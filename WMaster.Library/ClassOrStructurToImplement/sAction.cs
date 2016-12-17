using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // Structure that stores a single action and contains
    // a pointer for using linked lists.
    public class sAction : System.IDisposable
    {
        public int m_ID; // Action ID (0 to # actions-1)
        //	char m_Text[256]; // Action text
        public string m_Text;
        public short m_NumEntries; // # of entries in action
        public sEntry[] m_Entries; // Array of entry structures
        public sAction m_Next; // Next action in linked list

        public sAction()
        {
            m_ID = 0; // Set all data to defaults
            //m_Text = StringFunctions.ChangeCharacter(m_Text, 0, 0);
            m_NumEntries = 0;
            m_Entries = null;
            m_Next = null;
        }

        public void Dispose()
        {
            //if (m_Entries != null)
            //{
            //    Arrays.DeleteArray(m_Entries);
            //}
            m_Entries = null; // Free entries array
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Next = null; // Delete next in list
        }
    }
}
