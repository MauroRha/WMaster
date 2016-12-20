using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Game;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single brothel
    public class sBrothel : System.IDisposable
    {
        public sBrothel()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public string m_Name;

        public int m_id;
        public ushort m_Happiness; // av. % happy customers last week
        public int m_TotalCustomers; // the total number of customers for the last week
        public int m_RejectCustomersRestrict; // How many customers were turned away by your sex restrictions.
        public int m_RejectCustomersDisease; // How many customers were turned away because of disease.
        public int m_MiscCustomers; // customers used for temp purposes but must still be taken into account
        public byte m_Fame; // How famous this brothel is
        public int m_NumRooms; // How many rooms it has
        public int m_MaxNumRooms; // How many rooms it can have
        public int m_NumGirls; // How many girls are here
        public byte m_Bar; // level of bar: 0 => none
        public byte m_GamblingHall; // as above
        public ushort m_AdvertisingBudget; // Budget player has set for weekly advertising
        public double m_AdvertisingLevel; // multiplier for how far budget goes, based on girls working in advertising
        public int m_AntiPregPotions; // `J` added so all buildings save their own number of potions
        public int m_AntiPregUsed; // `J` number of potions used last turn
        public bool m_KeepPotionsStocked; // `J` and if they get restocked
        //	bool UseAntiPreg(bool use, int brothelID);
        //	bool UseAntiPreg(bool use);
        void AddAntiPreg(int amount)
        { throw new NotImplementedException(); }
        public int GetNumPotions()
        {
            return m_AntiPregPotions;
        }
        public void KeepPotionsStocked(bool stocked)
        {
            m_KeepPotionsStocked = stocked;
        }
        public bool GetPotionRestock()
        {
            return m_KeepPotionsStocked;
        }

        public bool Control_Girls()
        {
            return m_Control_Girls;
        }
        public int Girl_Gets_Girls()
        {
            return m_Girl_Gets_Girls;
        }
        public int Girl_Gets_Items()
        {
            return m_Girl_Gets_Items;
        }
        public int Girl_Gets_Beast()
        {
            return m_Girl_Gets_Beast;
        }
        public bool Control_Girls(bool cg)
        {
            return m_Control_Girls = cg;
        }
        public int Girl_Gets_Girls(int g)
        {
            return m_Girl_Gets_Girls = g;
        }
        public int Girl_Gets_Items(int g)
        {
            return m_Girl_Gets_Items = g;
        }
        public int Girl_Gets_Beast(int g)
        {
            return m_Girl_Gets_Beast = g;
        }

        public bool m_Control_Girls;
        public int m_Girl_Gets_Girls;
        public int m_Girl_Gets_Items;
        public int m_Girl_Gets_Beast;

        public int m_MovieRunTime; // see above, counter for the 7 week effect
        public int m_NumMovies;

        public sMovie m_Movies; // the movies currently selling
        public sMovie m_LastMovies;
        public sFilm m_CurrFilm;

        public cBuilding building = new cBuilding();
        public cGold m_Finance = new cGold(); // for keeping track of how well the place is doing (for the last week)

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

        public int m_Filthiness;

        public Events m_Events = new Events();

        [Obsolete("Convert int[] to List<int>")]
        public int[] m_BuildingQuality = new int[(int)Enum.JobFilter.NUMJOBTYPES];

        public sGirl m_Girls; // A list of all the girls this place has
        public sGirl m_LastGirl;
        public sBrothel m_Next;

        public int m_SecurityLevel;

        public IXmlElement SaveBrothelXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        public bool LoadBrothelXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }
        public int free_rooms()
        {
            return m_NumRooms - m_NumGirls;
        }

        public bool matron_on_shift(int shift, bool isClinic = false, bool isStudio = false, bool isArena = false, bool isCentre = false, bool isHouse = false, bool isFarm = false, int BrothelID = 0)
        { throw new NotImplementedException(); } // `J` added building checks
        public int matron_count(bool isClinic, bool isStudio, bool isArena, bool isCentre, bool isHouse, bool isFarm, int BrothelID)
        { throw new NotImplementedException(); }
        public void AddGirl(sGirl pGirl)
        { throw new NotImplementedException(); }
    }
}
