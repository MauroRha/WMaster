using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;
using WMaster.Concept;
using WMaster.Concept.Attributs;

namespace WMaster.ClassOrStructurToImplement
{
    /// <summary>
    /// Represent the player class
    /// </summary>
    public class Player // TODO : Derive Player, Gang, rivals and Girls from master class Personnage with skills and stats
    {
        public int Limit100(int nStat)
        { return Math.Max(Math.Min(nStat, 100), -100); } // Limit stats to -100 to 100
        public int Scale200(int nValue, int nStat)
        {
            /*
            *	WD	Scale the value n so that if adjusting the value will have
            *		less effect as you approch the max or min values.
            *
            *	eg	if you are EVIL additional evil acts will only subtract 1 but
            *		if you are GOOD an evil act will subtract nVal from Disposition
            *
            *		This will slow down the changes in player stats as you near the
            *		end of the ranges.
            */
            //printf("cPlayer::Scale200 nValue = %d, nStat = %d.\n", nValue, nStat);
            if (nValue == 0)
            {
                return 0; // Sanity check
            }
            bool bSign = nValue >= 0;
            nStat += 100; // set stat to value between 0 and 200
            if (bSign)
            {
                nStat = 200 - nStat; // Adjust for adding or subtraction
            }
            double fRatio = nStat / 200.0;
            int nRetValue = (int)(nValue * fRatio);

            //printf("cPlayer::Scale200 nRetValue = %d, fRatio = %.2f.\n\n", nRetValue, fRatio);
            if (Math.Abs(nRetValue) > 1)
            {
                return nRetValue; // Value is larger than 1
            }
            return (bSign ? 1 : -1);
        }
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

        [Obsolete("Convert to property", false)]
        public string SetTitle(string title)
        {
            m_Title = title;
            return m_Title;
        }
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
        /// <summary>
        /// List of all skills of the player.
        /// </summary>
        private SkillsCollection m_Skills = new SkillsCollection();
        /// <summary>
        /// Get the list of all skills of the player.
        /// </summary>
        public SkillsCollection Skills
        {
            get { return this.m_Skills; }
        }

        /// <summary>
        /// List of all stats of the player.
        /// </summary>
        private StatsCollection m_Stats = new StatsCollection();
        /// <summary>
        /// Get the list of all stats of the player.
        /// </summary>
        public StatsCollection Stats
        {
            get { return this.m_Stats; }
        }

        public Player()
        {
            m_RealName = "";
            m_FirstName = "";
            m_Surname = "";
            m_BirthYear = 1190;
            m_BirthMonth = 0;
            m_BirthDay = 0;
            m_PlayerGender = Enums.Gender.MALE;

            m_Stats[EnumStats.Health].Value = 100;
            m_Stats[EnumStats.Happiness].Value = 100;
            SetToZero();
        }

        public void SetToZero()
        {
            m_CustomerFear = m_Disposition = m_Suspicion = 0;
            m_WinGame = false;
        }

        IXmlElement SavePlayerXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadPlayerXML(IXmlHandle hPlayer)
        { throw new NotImplementedException(); }

        public int disposition()
        {
            return m_Disposition;
        }
        public int disposition(int n)
        {
            n = Scale200(n, m_Disposition);
            m_Disposition = Limit100(m_Disposition + n);
            return m_Disposition;
        }
        public int evil(int n)
        {
            // `J` add check for if harsher torture is set
            if (Configuration.Initial.TortureMod < 0 && n > 0)
            {
                n += n; // `J` double evil if increasing it BUT NOT IF LOWERING IT
            }
            return disposition(-1 * n);
        }
        public int Suspicion()
        {
            return m_Suspicion;
        }
        [Obsolete("Use UpdateSuspicion methode and convert this to property", false)]
        public int suspicion(int n)
        {
            n = Scale200(n, m_Suspicion);
            m_Suspicion = Limit100(m_Suspicion + n);
            return m_Suspicion;
        }
        public int customerfear()
        {
            return m_CustomerFear;
        }
        [Obsolete("Use UpdateCustomerFear methode and convert this to property", false)]
        public int customerfear(int n)
        {
            n = Scale200(n, m_CustomerFear);
            m_CustomerFear = Limit100(m_CustomerFear + n);
            return m_CustomerFear;
        }

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
