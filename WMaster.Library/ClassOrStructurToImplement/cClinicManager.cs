/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace WMaster.Game.Manager
{
    using System;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Enum;

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
