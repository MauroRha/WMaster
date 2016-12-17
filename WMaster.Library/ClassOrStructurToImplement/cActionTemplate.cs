using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cActionTemplate : System.IDisposable
    {
        private int m_NumActions; // # of actions in template
        private sAction m_ActionParent; // list of template actions

        // Functions for reading text (mainly used in actions)
        // TODO : REFACTORING - File access functions must be translate to specific os application
        bool GetNextQuotedLine(ref string Data, System.IO.FileInfo fp, int MaxSize)
        { throw new NotImplementedException(); }
        bool GetNextWord(ref string Data, System.IO.FileInfo fp, int MaxSize)
        { throw new NotImplementedException(); }

        public cActionTemplate()
        {
            m_ActionParent = null;
        }
        public void Dispose()
        {
            Free();
        }

        // Load and free the action templates
        bool Load()
        { throw new NotImplementedException(); }
        public bool Free()
        {
            if (m_ActionParent != null)
            {
                m_ActionParent = null;
            }
            m_ActionParent = null;
            m_NumActions = 0;
            return true;
        }

        // Get # actions in template, action parent,
        // and specific action structure.
        public int GetNumActions()
        {
            return m_NumActions;
        }
        public sAction GetActionParent()
        {
            return m_ActionParent;
        }
        sAction GetAction(int Num)
        { throw new NotImplementedException(); }

        // Get a specific type of sScript structure
        sScript CreateScriptAction(int Type)
        { throw new NotImplementedException(); }

        // Get info about actions and entries
        int GetNumEntries(int ActionNum)
        { throw new NotImplementedException(); }
        sEntry GetEntry(int ActionNum, int EntryNum)
        { throw new NotImplementedException(); }

        // Expand action text using min/first/true choice values
        bool ExpandDefaultActionText(ref string Buffer, sAction Action)
        { throw new NotImplementedException(); }

        // Expand action text using selections
        bool ExpandActionText(ref string Buffer, sScript Script)
        { throw new NotImplementedException(); }
    }

}
