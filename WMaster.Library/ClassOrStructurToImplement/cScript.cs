using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
// General Functions
sScript *LoadScriptFile(string Filename);
sScript *LoadScriptXML(string Filename);
bool SaveScriptFile(const char *Filename, sScript *ScriptRoot);
bool SaveScriptXML(const char *Filename, sScript *ScriptRoot);
void TraverseScript(sScript *pScript);
*/
namespace WMaster.ClassOrStructurToImplement
{
    // Class for processing scripts
    public class cScript : System.IDisposable
    {
        protected int m_NumActions; // # of script actions loaded
        protected sScript m_ScriptParent; // Script linked list

        // Overloadable functions for preparing for script
        // processing and when processing completed
        protected virtual bool Prepare()
        {
            return true;
        }
        protected virtual bool Release()
        {
            return true;
        }

        // Process a single script action
        protected virtual sScript Process(sScript Script)
        {
            return Script.m_Next;
        }

        public cScript()
        {
            m_ScriptParent = null;
        } // Constructor
        public void Dispose()
        {
            if (m_ScriptParent != null)
            {
                m_ScriptParent.Dispose();
            }
            m_ScriptParent = null;
        } // Destructor

        bool Load(string filename)
        { throw new NotImplementedException(); }// Load a script
        bool Free()
        { throw new NotImplementedException(); } // Free loaded script
        /*
         *	no idea if this stub needs to return true or false
         *	picked one at random to silence a compiler warning
         */
        public bool Execute()
        {
            string tempVar = null;
            return Execute(ref tempVar);
        }
        public bool Execute(ref string Filename)
        {
            //Filename;
            return true;
        } // Execute script
    }

}
