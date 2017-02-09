namespace WMaster.ClassOrStructurToImplement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WMaster.Manager;
    using WMaster.Enums;
    using WMaster.Concept;

    // defines a single brothel
    public class sBrothel : System.IDisposable
    {
        private static List<Jobs> _listOfBorthelJobs = new List<Jobs>()
        {
            Jobs.RESTING,Jobs.TRAINING,Jobs.CLEANING,Jobs.SECURITY,Jobs.ADVERTISING,
            Jobs.CUSTOMERSERVICE,Jobs.MATRON,Jobs.TORTURER,Jobs.EXPLORECATACOMBS,Jobs.BEASTCARER,
            Jobs.BARMAID,Jobs.WAITRESS,Jobs.SINGER,Jobs.PIANO,Jobs.ESCORT,
            Jobs.BARCOOK,Jobs.DEALER,Jobs.ENTERTAINMENT,Jobs.XXXENTERTAINMENT,Jobs.WHOREGAMBHALL,
            Jobs.SLEAZYBARMAID,Jobs.SLEAZYWAITRESS,Jobs.BARSTRIPPER,Jobs.BARWHORE,Jobs.MASSEUSE,
            Jobs.BROTHELSTRIPPER,Jobs.PEEP,Jobs.WHOREBROTHEL,Jobs.WHORESTREETS

        };
        /// <summary>
        /// Indicate if <paramref name="job"/> is a brothel jobs.
        /// </summary>
        /// <param name="job"><see cref="Jobs"/> test.</param>
        /// <returns><b>True</b> if <paramref name=" job"/> is a brothel jobs.</returns>
        public static bool IsBrothelJob(Jobs job)
        {
            return sBrothel._listOfBorthelJobs.Contains(job);
        }

        public sBrothel()
        {
            this.m_Finance = null;
            m_Next = null;

            m_GirlsList = new List<sGirl>();

            NumRooms = MaxNumRooms = 0;
            SecurityLevel = Filthiness = 0;
            Fame = 0;
            Happiness = 0;

            m_HasGambStaff = m_HasBarStaff = Bar = GamblingHall = 0;

            KeepPotionsStocked = false;
            AntiPregPotions = AntiPregUsed = 0;

            AdvertisingBudget = 0;
            AdvertisingLevel = 0;
            TotalCustomers = RejectCustomersRestrict = RejectCustomersDisease = MiscCustomers = 0;

            m_RestrictAnal = m_RestrictBDSM = m_RestrictBeast = m_RestrictFoot = m_RestrictGroup = m_RestrictHand = m_RestrictLesbian = m_RestrictNormal = m_RestrictOral = m_RestrictStrip = m_RestrictTitty = false;
            for (int i = 0; i < (int)JobFilter.NUMJOBTYPES; i++)
            {
                m_BuildingQuality[i] = 0;
            }

            //movie
            m_ShowTime = m_ShowQuality = 0;
            m_CurrFilm = null;
            NumMovies = 0;
            m_LastMovies = null;
            m_Movies = null;
            MovieRunTime = 0;
        } // constructor
        public void Dispose()
        {
            m_ShowTime = 0;
            m_ShowQuality = 0;

            m_Next = null;
            this.m_GirlsList.Clear();
            //movie
            m_CurrFilm = null;
            NumMovies = 0;
            m_Movies = null;
            m_LastMovies = null;
        }

        public string Name { get; set; }

        /// <summary>
        /// Brothel's identifiant.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// av. % happy customers last week.
        /// </summary>
        public int Happiness { get; set; }
        /// <summary>
        /// the total number of customers for the last week.
        /// </summary>
        public int TotalCustomers { get; set; }
        /// <summary>
        /// How many customers were turned away by your sex restrictions.
        /// </summary>
        public int RejectCustomersRestrict { get; set; }
        /// <summary>
        /// How many customers were turned away because of disease.
        /// </summary>
        public int RejectCustomersDisease { get; set; }
        /// <summary>
        /// Customers used for temp purposes but must still be taken into account.
        /// </summary>
        public int MiscCustomers { get; set; }
        /// <summary>
        /// How famous this brothel is.
        /// </summary>
        public int Fame { get; set; }
        /// <summary>
        /// How many rooms it has.
        /// </summary>
        public int NumRooms { get; set; }
        /// <summary>
        /// How many rooms it can have.
        /// </summary>
        public int MaxNumRooms { get; set; }
        /// <summary>
        /// How many girls are here.
        /// </summary>
        public int NumGirls { get; set; }
        /// <summary>
        /// Level of bar: 0 => none.
        /// </summary>
        public byte Bar { get; set; }
        /// <summary>
        /// Level of cambling: 0 => none.
        /// </summary>
        public byte GamblingHall { get; set; }
        /// <summary>
        /// Budget player has set for weekly advertising.
        /// </summary>
        public ushort AdvertisingBudget { get; set; }
        /// <summary>
        /// Multiplier for how far budget goes, based on girls working in advertising.
        /// </summary>
        public double AdvertisingLevel { get; set; }
        /// <summary>
        /// `J` added so all buildings save their own number of potions.
        /// </summary>
        public int AntiPregPotions { get; set; }
        /// <summary>
        /// `J` number of potions used last turn.
        /// </summary>
        public int AntiPregUsed { get; set; }
        /// <summary>
        /// `J` and if they get restocked.
        /// </summary>
        public bool KeepPotionsStocked { get; set; }
        //	bool UseAntiPreg(bool use, int brothelID);
        //	bool UseAntiPreg(bool use);
        [Obsolete("Flag as unused")]
        public void AddAntiPreg(int amount)
        {
            AntiPregPotions += amount;
            if (AntiPregPotions > 700)
            {
                AntiPregPotions = 700;
            }
        }

        public int GetNumPotions()
        {
            return AntiPregPotions;
        }
        public bool GetPotionRestock()
        {
            return KeepPotionsStocked;
        }

        public bool Control_Girls()
        {
            return ControlGirls;
        }
        public int Girl_Gets_Girls()
        {
            return GirlGetsGirls;
        }
        public int Girl_Gets_Items()
        {
            return GirlGetsItems;
        }
        public int Girl_Gets_Beast()
        {
            return GirlGetsBeast;
        }
        public bool Control_Girls(bool cg)
        {
            return ControlGirls = cg;
        }
        public int Girl_Gets_Girls(int g)
        {
            return GirlGetsGirls = g;
        }
        public int Girl_Gets_Items(int g)
        {
            return GirlGetsItems = g;
        }
        public int Girl_Gets_Beast(int g)
        {
            return GirlGetsBeast = g;
        }

        public bool ControlGirls { get; set; }
        public int GirlGetsGirls { get; set; }
        public int GirlGetsItems { get; set; }
        public int GirlGetsBeast { get; set; }

        // see above, counter for the 7 week effect
        public int MovieRunTime { get; set; }
        public int NumMovies { get; set; }

        public sMovie m_Movies; // the movies currently selling
        public sMovie m_LastMovies;
        public sFilm m_CurrFilm;

        public cBuilding building = new cBuilding();
        public Gold m_Finance = new Gold(); // for keeping track of how well the place is doing (for the last week)

        // For keeping track of any shows currently being produced here
        public int m_ShowTime; // when reaches 0 then the show is ready
        public int m_ShowQuality; // Determined by the average fame and skill of the girls in the show
        public byte m_HasGambStaff; // gambling hall or
        public byte m_HasBarStaff; // Bar staff. Not as good as girls but consistent

        public bool m_RestrictAnal;
        public bool m_RestrictBDSM;
        public bool m_RestrictOral;
        public bool m_RestrictTitty;
        public bool m_RestrictHand;
        public bool m_RestrictBeast;
        public bool m_RestrictGroup;
        public bool m_RestrictNormal;
        public bool m_RestrictLesbian;
        public bool m_RestrictFoot;
        public bool m_RestrictStrip;

        private int m_Filthiness;
        public int Filthiness
        {
            get { return this.m_Filthiness; }
            set { this.m_Filthiness = Math.Max(value, 0); }
        }

        public EventManager m_Events = new EventManager();

        [Obsolete("Convert int[] to List<int>")]
        public int[] m_BuildingQuality = new int[(int)Enums.JobFilter.NUMJOBTYPES];

        /// <summary>
        /// A list of all the girls this place has.
        /// </summary>
        private List<sGirl> m_GirlsList = new List<sGirl>();
        /// <summary>
        /// Get the list of all the girls this place has.
        /// </summary>
        public IEnumerable<sGirl> GirlsList
        {
            get { return this.m_GirlsList; }
        }

        /// <summary>
        /// sorts the list of girls.
        /// </summary>
        public void Sort()
        {
            m_GirlsList.Sort();
        }

        public sBrothel m_Next;

        private int m_SecurityLevel;
        public int SecurityLevel {
            get { return this.m_SecurityLevel; }
            set { this.m_SecurityLevel = Math.Max(value, 0); }
        }

        public IXmlElement SaveBrothelXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        public bool LoadBrothelXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }
        /// <summary>
        /// Get number of free rooms in brothel.
        /// </summary>
        /// <returns>Number of free rooms.</returns>
        public int FreeRooms()
        {
            return NumRooms - NumGirls;
        }

        // ----- Matron  // `J` added building checks
        public bool MatronOnShift(DayShift shift, bool isClinic, bool isStudio, bool isArena, bool isCentre, bool isHouse, bool isFarm, int brothelID)
        {
            if (isArena)
            {
                if (Game.Arena.GetNumGirlsOnJob(0, Jobs.DOCTORE, shift) > 0)
                {
                    return true;
                }
            }
            else if (isStudio)
            {
                if (Game.Studios.GetNumGirlsOnJob(0, Jobs.DIRECTOR, shift) > 0)
                {
                    return true;
                }
            }
            else if (isClinic)
            {
                if (Game.Clinic.GetNumGirlsOnJob(0, Jobs.CHAIRMAN, shift) > 0)
                {
                    return true;
                }
            }
            else if (isCentre)
            {
                if (Game.Centre.GetNumGirlsOnJob(0, Jobs.CENTREMANAGER, shift) > 0)
                {
                    return true;
                }
            }
            else if (isHouse)
            {
                if (Game.House.GetNumGirlsOnJob(0, Jobs.HEADGIRL, shift) > 0)
                {
                    return true;
                }
            }
            else if (isFarm)
            {
                if (Game.Farm.GetNumGirlsOnJob(0, Jobs.FARMMANGER, shift) > 0)
                {
                    return true;
                }
            }
            else
            {
                if (Game.Brothels.GetNumGirlsOnJob(brothelID, Jobs.MATRON, shift) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public int MatronCount(bool isClinic, bool isStudio, bool isArena, bool isCentre, bool isHouse, bool isFarm, int brothelID)
        {
            int i;
            int sum = 0;
            foreach (DayShift dayShift in DayShift.Day.GetAllItems<DayShift>())
            {
                if (isArena)
                {
                    if (Game.Arena.GetNumGirlsOnJob(0, Jobs.DOCTORE, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else if (isStudio)
                {
                    if (Game.Studios.GetNumGirlsOnJob(0, Jobs.DIRECTOR, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else if (isClinic)
                {
                    if (Game.Clinic.GetNumGirlsOnJob(0, Jobs.CHAIRMAN, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else if (isCentre)
                {
                    if (Game.Centre.GetNumGirlsOnJob(0, Jobs.CENTREMANAGER, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else if (isHouse)
                {
                    if (Game.House.GetNumGirlsOnJob(0, Jobs.HEADGIRL, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else if (isFarm)
                {
                    if (Game.Farm.GetNumGirlsOnJob(0, Jobs.FARMMANGER, dayShift) > 0)
                    {
                        sum++;
                    }
                }
                else
                {
                    if (Game.Brothels.GetNumGirlsOnJob(brothelID, Jobs.MATRON, dayShift) > 0)
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
        public void AddGirl(sGirl girl)
        {
            girl.FixFreeTimeJobs();
            this.m_GirlsList.Add(girl);
        }
        public void RemoveGirl(sGirl girl)
        {
            this.m_GirlsList.Remove(girl);
        }
    }
}