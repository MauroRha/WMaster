using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;
using WMaster.Concept;
using WMaster.Concept.Attributs;

namespace WMaster.ClassOrStructurToImplement
{
    public class cPlayer
    {
        int Limit100(int nStat)
        { throw new NotImplementedException(); } // Limit stats to -100 to 100
        int Scale200(int nStatMod, int nCurrentStatValue)
        { throw new NotImplementedException(); } // Scale stat from 1 to nStatMod
        /*
         *	the suspicion level of the authorities.
         *	-100 means they are on players side
         *	+100 means they will probably raid his brothels
         */
        private int m_Suspicion;
        /*
         *	How good or evil the player is considered to be:
         *	-100 is evil while +100 is a saint
         */
        private int m_Disposition;
        /*
         *	how much the customers fear you:
         *	-100 is not at all while 100 means a lot
         */
        private int m_CustomerFear;


        private int m_BirthYear; // the game starts in year 1209 so default start age is 18
        private int m_BirthMonth; // there are 12 month in the year
        private int m_BirthDay; // there are 30 days in every month

        private string m_Title; // basic title - need to add more titles with more power gained
        private string m_FirstName; // no first name
        private string m_Surname; // basic surname
        private string m_RealName; // m_FirstName + " " + m_Surname

        private Gender m_PlayerGender; // `J` added - not going to be changeable yet but adding it in for later


        public string Title()
        {
            return m_Title;
        }
        public string FirstName()
        {
            return m_FirstName;
        }
        public string Surname()
        {
            return m_Surname;
        }
        public string RealName()
        {
            return m_RealName;
        }

        string SetTitle(string title)
        { throw new NotImplementedException(); }
        string SetFirstName(string firstname)
        { throw new NotImplementedException(); }
        string SetSurname(string surname)
        { throw new NotImplementedException(); }
        string SetRealName(string realname)
        { throw new NotImplementedException(); }

        public Gender Gender()
        {
            return m_PlayerGender;
        }
        void SetGender(int x)
        { throw new NotImplementedException(); }
        void AdjustGender(int male, int female)
        { throw new NotImplementedException(); }
        bool CanImpregnateFemale()
        { throw new NotImplementedException(); }
        bool CanCarryOwnBaby()
        { throw new NotImplementedException(); }
        bool CanCarryNormalBaby()
        { throw new NotImplementedException(); }
        bool HasPenis()
        { throw new NotImplementedException(); }
        bool HasVagina()
        { throw new NotImplementedException(); }
        bool HasTestes()
        { throw new NotImplementedException(); }
        bool HasOvaries()
        { throw new NotImplementedException(); }


        public bool m_WinGame;
        public int[] m_Skills = new int[(int)EnumSkills.NUM_SKILLS];
        public int[] m_Stats = new int[(int)EnumStats.NUM_STATS];

        cPlayer()
        { throw new NotImplementedException(); }// constructor
        void SetToZero()
        { throw new NotImplementedException(); }

        IXmlElement SavePlayerXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadPlayerXML(IXmlHandle hPlayer)
        { throw new NotImplementedException(); }

        public int disposition()
        {
            return m_Disposition;
        }
        public int disposition(int n)
        { throw new NotImplementedException(); }
        int evil(int n)
        { throw new NotImplementedException(); }
        public int suspicion()
        {
            return m_Suspicion;
        }
        public int suspicion(int n)
        { throw new NotImplementedException(); }
        public int customerfear()
        {
            return m_CustomerFear;
        }
        public int customerfear(int n)
        { throw new NotImplementedException(); }

        public int BirthYear()
        {
            return m_BirthYear;
        }
        public int BirthMonth()
        {
            return m_BirthMonth;
        }
        public int BirthDay()
        {
            return m_BirthDay;
        }
        int BirthYear(int n)
        { throw new NotImplementedException(); }
        int BirthMonth(int n)
        { throw new NotImplementedException(); }
        int BirthDay(int n)
        { throw new NotImplementedException(); }
        int SetBirthYear(int n)
        { throw new NotImplementedException(); }
        int SetBirthMonth(int n)
        { throw new NotImplementedException(); }
        int SetBirthDay(int n)
        { throw new NotImplementedException(); }

    }
}
