using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    /*
    * manages the house
    *
    * extend cBrothelManager
    */
    public class cHouseManager : cBrothelManager, System.IDisposable
    {
        public cHouseManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)
        void UpdateHouse()
        { throw new NotImplementedException(); }
        void UpdateGirls(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }
        //void	AddBrothel(sBrothel* newBroth);
        IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }
        void Free()
        { throw new NotImplementedException(); }
        public int m_NumHouses;
        public cJobManager m_JobManager = new cJobManager();
    }

}