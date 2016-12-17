using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.GameConcept.Item;

namespace WMaster.ClassOrStructurToImplement
{
    public class cRival : IDisposable
    {
        // TODO : GAME - Use constant or init variable instead of literal value
        /// <summary>
        /// Constructor
        /// </summary>
        public cRival()
        {
            m_Next = m_Prev = null;
            m_Name = "";
            m_Power = 0;					// `J` added
            m_Influence = 0;
            m_BribeRate = 0;
            m_Gold = 5000;
            m_NumBrothels = 1;
            m_NumGangs = 3;
            m_NumGirls = 8;
            m_NumBars = 0;
            m_NumGamblingHalls = 0;
            m_BusinessesExtort = 0;
            m_Inventory = new sInventoryItem[Constants.MAXNUM_RIVAL_INVENTORY];
        }

        public void Dispose()
        {
            if (m_Next != null)
            { m_Next.Dispose(); }
            m_Next = null;
            m_Prev = null;
        }

        // variables
        public string m_Name;
        public int m_Power;						// `J` added
        public int m_NumGangs;
        public int m_NumBrothels;
        public int m_NumGirls;
        public int m_NumBars;
        public int m_NumGamblingHalls;
        public long m_Gold;
        public int m_BusinessesExtort;
        public long m_BribeRate;
        public int m_Influence;	// based on the bribe rate this is the percentage of influence you have
        public int m_NumInventory;										// current amount of inventory the brothel has
        [Obsolete("Replace sInventoryItem[] to List<sInventoryItem>", false)]
        public sInventoryItem[] m_Inventory = new sInventoryItem[Constants.MAXNUM_RIVAL_INVENTORY];	// List of inventory items they have (40 max)

        public cRival m_Next;
        public cRival m_Prev;
    };
}
