using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.GameConcept.Item;

namespace WMaster.ClassOrStructurToImplement
{
    /// <summary>
    /// Manages all brothels
    /// <remarks>
    ///     <para>
    ///         Anyone else think this class tries to do too much?
    ///         Yes it does, I am working on reducing it-Delta
    ///    </para>
    ///    <para>Yep, I think so too. I am working on too</para>
    /// </remarks>
    /// </summary>
    public class cBrothelManager : IDisposable
    {
        //public:
        /// <summary>
        /// constructor
        /// </summary>
        public cBrothelManager()
        { throw new NotImplementedException(); }

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
        }

        public void Free()
        { throw new NotImplementedException(); }

        public sGirl GetDrugPossessor()
        { throw new NotImplementedException(); }

        public void AddGirlToPrison(sGirl girl)
        { throw new NotImplementedException(); }

        public void RemoveGirlFromPrison(sGirl girl)
        { throw new NotImplementedException(); }

        public int GetNumInPrison()
        { return m_NumPrison; }

        public void AddGirlToRunaways(sGirl girl)
        { throw new NotImplementedException(); }
        public void RemoveGirlFromRunaways(sGirl girl)
        { throw new NotImplementedException(); }

        public int GetNumRunaways()
        { return m_NumRunaways; }


        public void NewBrothel(int NumRooms, int MaxNumRooms = 200)
        { throw new NotImplementedException(); }
        public void DestroyBrothel(int ID)
        { throw new NotImplementedException(); }
        public void UpdateBrothels()
        { throw new NotImplementedException(); }
        public void UpdateGirls(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }

        public void UpdateCustomers(sBrothel brothel, bool Day0Night1)
        { throw new NotImplementedException(); }

        // MYR: Start of my automation functions
        public void UsePlayersItems(sGirl cur)
        { throw new NotImplementedException(); }
        public bool AutomaticItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool AutomaticSlotlessItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool AutomaticFoodItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool RemoveItemFromInventoryByNumber(int Pos)
        { throw new NotImplementedException(); } // support fn
        // End of automation functions

        public void UpdateAllGirlsStat(sBrothel brothel, int stat, int amount)
        { throw new NotImplementedException(); }
        public void SetGirlStat(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); }

        public sGirl GetPrison()
        { return m_Prison; }
        public int stat_lookup(string stat_name, int brothel_id = -1)
        { throw new NotImplementedException(); }

        // Used by new security guard code
        public int GetGirlsCurrentBrothel(sGirl girl)
        { throw new NotImplementedException(); }
        // Also used by new security code
        public List<sGirl> GirlsOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); }
        public sGirl GetRandomGirlOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); } 	// `J` - added
        public sGirl GetFirstGirlOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); } 	// `J` - added

        /*	// `J` AntiPreg Potions rewriten and moved to individual buildings
            bool UseAntiPreg(bool use, int brothelID);
            bool UseAntiPreg(bool use);
            void AddAntiPreg(int amount);
            int  GetNumPotions()					{ return m_AntiPregPotions; }
            void KeepPotionsStocked(bool stocked)	{ m_KeepPotionsStocked = stocked; }
            bool GetPotionRestock()					{ return m_KeepPotionsStocked; }
        /* */

        public int GetTotalNumGirls(bool monster = false)
        { throw new NotImplementedException(); }
        public int GetFreeRooms(sBrothel brothel)
        { throw new NotImplementedException(); }
        public int GetFreeRooms(int brothelnum = 0)
        { throw new NotImplementedException(); }

        public void UpgradeSupplySheds()
        { m_SupplyShedLevel++; }
        public int GetSupplyShedLevel()
        { return m_SupplyShedLevel; }

        public void AddGirl(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        public void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl = true)
        { throw new NotImplementedException(); }
        public sGirl GetFirstRunaway()
        { throw new NotImplementedException(); }
        public void sort(sBrothel brothel)
        { throw new NotImplementedException(); } 		// sorts the list of girls
        public void SortInventory()
        { throw new NotImplementedException(); }

        public void SetName(int brothelID, string name)
        { throw new NotImplementedException(); }
        public string GetName(int brothelID)
        { throw new NotImplementedException(); }

        // returns true if the bar is staffed 
        public bool CheckBarStaff(sBrothel brothel, int numGirls)
        { throw new NotImplementedException(); }

        // as above but for gambling hall
        public bool CheckGambStaff(sBrothel brothel, int numGirls)
        { throw new NotImplementedException(); }

        public bool FightsBack(sGirl girl)
        { throw new NotImplementedException(); }
        public int GetNumGirls(int brothelID)
        { throw new NotImplementedException(); }
        public string GetGirlString(int brothelID, int girlNum)
        { throw new NotImplementedException(); }
        public int GetNumGirlsOnJob(int brothelID, int jobID, int day = 0)
        { throw new NotImplementedException(); }

        public string GetBrothelString(int brothelID)
        { throw new NotImplementedException(); }

        public sGirl GetGirl(int brothelID, int num)
        { throw new NotImplementedException(); }
        public int GetGirlPos(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        // MYR: Used by new end of turn code in InterfaceProcesses::TurnSummary
        public sGirl GetGirlByName(int brothelID, string name)
        { throw new NotImplementedException(); }

        public sBrothel GetBrothel(int brothelID)
        { throw new NotImplementedException(); }
        public int GetNumBrothels()
        { return m_NumBrothels; }
        public int GetNumBrothelsWithVacancies()
        { throw new NotImplementedException(); }
        public int GetFirstBrothelWithVacancies()
        { throw new NotImplementedException(); }
        public int GetRandomBrothelWithVacancies()
        { throw new NotImplementedException(); }
        public sBrothel GetRandomBrothel()
        { throw new NotImplementedException(); }

        public void CalculatePay(sBrothel brothel, sGirl girl, int Job)
        { throw new NotImplementedException(); }

        // returns true if the girl wins
        public bool PlayerCombat(sGirl girl)
        { throw new NotImplementedException(); }

        public cPlayer GetPlayer()
        { return m_Player; }
        public cDungeon GetDungeon()
        { return m_Dungeon; }

        public int HasItem(string name, int countFrom = -1)
        { throw new NotImplementedException(); }

        // Some public members for ease of use
        public int m_NumInventory;								// current amount of inventory the brothel has
        [Obsolete("Convert sInventoryItem[] to List<sInventoryItem>", false)]
        public sInventoryItem[] m_Inventory = new sInventoryItem[Constants.MAXNUM_INVENTORY];	// List of inventory items they have (3000 max)
        [Obsolete("Convert short[] to List<short>", false)]
        public short[] m_EquipedItems = new short[Constants.MAXNUM_INVENTORY];	// value of > 0 means equipped (wearing) the item
        [Obsolete("Convert int[] to List<int>", false)]
        public int[] m_NumItem = new int[Constants.MAXNUM_INVENTORY];		// the number of items there are stacked
        public cJobManager m_JobManager;						// manages all the jobs

        public int GetNumberOfItemsOfType(int type, bool splitsubtype = false)
        { throw new NotImplementedException(); }



        public long GetBribeRate()
        { return m_BribeRate; }
        public void SetBribeRate(long rate)
        { m_BribeRate = rate; }
        public void UpdateBribeInfluence()
        { throw new NotImplementedException(); }
        public int GetInfluence()
        { return m_Influence; }

        public cRival GetRivals()
        { return m_Rivals.GetRivals(); }
        public cRivalManager GetRivalManager()
        { return m_Rivals; }

        public void WithdrawFromBank(long amount)
        { throw new NotImplementedException(); }
        public void DepositInBank(long amount)
        { throw new NotImplementedException(); }
        public long GetBankMoney()
        { return m_Bank; }
        public int GetNumFood()
        { return m_Food; }
        public int GetNumDrinks()
        { return m_Drinks; }
        public int GetNumBeasts()
        { return m_Beasts; }
        public int GetNumGoods()
        { return m_HandmadeGoods; }
        public int GetNumAlchemy()
        { return m_Alchemy; }
        public void add_to_food(int i)
        { m_Food += i; if (m_Food < 0) m_Food = 0; }
        public void add_to_drinks(int i)
        { m_Drinks += i; if (m_Drinks < 0) m_Drinks = 0; }
        public void add_to_beasts(int i)
        { m_Beasts += i; if (m_Beasts < 0) m_Beasts = 0; }
        public void add_to_goods(int i)
        { m_HandmadeGoods += i; if (m_HandmadeGoods < 0) m_HandmadeGoods = 0; }
        public void add_to_alchemy(int i)
        { m_Alchemy += i; if (m_Alchemy < 0) m_Alchemy = 0; }

        public bool CheckScripts()
        { throw new NotImplementedException(); }

        public void UpdateObjective()
        { throw new NotImplementedException(); } 				// updates an objective and checks for compleation
        public sObjective GetObjective()
        { throw new NotImplementedException(); } 			// returns the objective
        public void CreateNewObjective()
        { throw new NotImplementedException(); } 			// Creates a new objective
        public void PassObjective()
        { throw new NotImplementedException(); } 				// Gives a reward
        public void AddCustomObjective(int limit, int diff, int objective, int reward, int sofar, int target, string text = "")
        { throw new NotImplementedException(); }

        public IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        public bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }

        public bool NameExists(string name)
        { throw new NotImplementedException(); }
        public bool SurnameExists(string name)
        { throw new NotImplementedException(); }

        public bool AddItemToInventory(sInventoryItem item)
        { throw new NotImplementedException(); }

        public void check_druggy_girl(string ss)
        { throw new NotImplementedException(); }
        public void check_raid()
        { throw new NotImplementedException(); }
        public void do_tax()
        { throw new NotImplementedException(); }
        public void do_daily_items(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }
        public void do_food_and_digs(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }
        public string disposition_text()
        { throw new NotImplementedException(); }
        public string fame_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public string suss_text()
        { throw new NotImplementedException(); }
        public string happiness_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public double calc_pilfering(sGirl girl)
        { throw new NotImplementedException(); }

        public bool runaway_check(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }


        // WD: JOB_TORTURER stuff
        public void TortureDone(bool flag)
        { m_TortureDoneFlag = flag; return; }
        public bool TortureDone()
        { return m_TortureDoneFlag; }
        public sGirl WhoHasTorturerJob()
        { throw new NotImplementedException(); }

        // WD: test to check if doing turn processing.  Used to ingnore HOUSE_STAT value in GetRebelValue() if girl gets to keep all her income.
        public bool is_Dayshift_Processing()
        { return m_Processing_Shift == (short)WMaster.Enum.DayShift.Day; }
        public bool is_Nightshift_Processing()
        { return m_Processing_Shift == (short)WMaster.Enum.DayShift.Night; }

        // WD:	Update code of girls stats
        public void updateGirlTurnBrothelStats(sGirl girl)
        { throw new NotImplementedException(); }

        //private:
        private int TotalFame() //sBrothel *);
        { throw new NotImplementedException(); }

        private cPlayer m_Player;				// the stats for the player owning these brothels
        private cDungeon m_Dungeon;				// the dungeon

        private int m_NumBrothels;
        private sBrothel m_Parent;
        private sBrothel m_Last;

        // brothel supplies
        /*	// `J` moved to individual buildings
            bool m_KeepPotionsStocked;
            int  m_AntiPregPotions;			// the number of pregnancy/insimination preventive potions in stock
        */
        private int m_SupplyShedLevel;			// the level of the supply sheds. the higher the level, the more alcohol and antipreg potions can hold

        // brothel resources
        private int m_HandmadeGoods;			// used with the community centre
        private int m_Beasts;					// used for beastiality scenes
        private int m_Food;						// food produced at the farm
        private int m_Drinks;					// drinks produced at the farm
        private int m_Alchemy;

        // brothel resource Reserves - How much will NOT be sold so it can be used by the Brothels 
        private int m_HandmadeGoodsReserves;
        private int m_BeastsReserves;
        private int m_FoodReserves;
        private int m_DrinksReserves;
        private int m_AlchemyReserves;


        private int m_NumPrison;
        private sGirl m_Prison;				// a list of girls kept in prision
        private sGirl m_LastPrison;

        private int m_NumRunaways;          // a list of runaways
        private sGirl m_Runaways;
        private sGirl m_LastRunaway;

        private long m_BribeRate;				// the amount of money spent bribing officials per week
        private int m_Influence;				// based on the bribe rate this is the percentage of influence you have
        private int m_Dummy;					//a dummy variable
        private long m_Bank;					// how much is stored in the bank

        private sObjective m_Objective;

        private cRivalManager m_Rivals;			// all of the players compedators

        private bool m_TortureDoneFlag;			// WD:	Have we got a torturer working today
        // TODO : REFACTORING - Convert to DayShift enum?
        private short m_Processing_Shift;		// WD:	Store Day0Night1 value when processing girls

        private void AddBrothel(sBrothel newBroth)
        { throw new NotImplementedException(); }
    }
}