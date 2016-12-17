using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;

namespace WMaster.ClassOrStructurToImplement
{
    /*
     * manages the centre
     *
     * extend cBrothelManager
     */
    public class cCentreManager : cBrothelManager, System.IDisposable
    {
        public cCentreManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public int m_Rehab_Patient_Time = 0; // `J` basically how many Rehab patients the counselors at the centre can take care of

        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)
        void UpdateCentre()
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
        public int m_NumCentres;
        public cJobManager m_JobManager = new cJobManager();
        int GetNumberPatients(DayShift Day0Night1 = DayShift.Day)
        { throw new NotImplementedException(); }

    }
}
