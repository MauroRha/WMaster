using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    /*
     * manages the arena
     *
     * extend cBrothelManager
     */
    public class cArenaManager : cBrothelManager, System.IDisposable
    {
        public cArenaManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)
        void UpdateArena()
        { throw new NotImplementedException(); }
        void UpdateGirls(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }
        void Free()
        { throw new NotImplementedException(); }
        public int m_NumArenas;
        public cJobManager m_JobManager = new cJobManager();

        int Num_Jousting(int brothel)
        { throw new NotImplementedException(); }
        bool is_Jousting_Job(int testjob)
        { throw new NotImplementedException(); }
    }

}
