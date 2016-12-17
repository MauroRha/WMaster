using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;

namespace WMaster.ClassOrStructurToImplement
{
    /*
    * manages the clinic
    *
    * extend cBrothelManager
    */
    public class cClinicManager : cBrothelManager, System.IDisposable
    {
        public cClinicManager()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public int m_Doctor_Patient_Time = 0; // `J` basically how many patients the doctors at the clinic can take care of
        public int m_Nurse_Patient_Time = 0; // `J` basically how many patients the nurses at the clinic can take care of

        void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = false)
        { throw new NotImplementedException(); } // Removes a girl from the list (only used with editor where all girls are available)
        void UpdateClinic()
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
        public int m_NumClinics;
        public cJobManager m_JobManager = new cJobManager();
        //Aika Edit
        //sGirl* GetGirl(int brothelID, int num);
        //sClinic* m_Parent;

        bool is_Surgery_Job(int testjob)
        { throw new NotImplementedException(); }
        bool DoctorNeeded()
        { throw new NotImplementedException(); }
        int GetNumberPatients(DayShift Day0Night1 = DayShift.Day)
        { throw new NotImplementedException(); }

    }

}
