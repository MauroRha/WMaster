using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // Keeps track of customers in the dungeon
    public class sDungeonCust : System.IDisposable
    {
        sDungeonCust()
        { throw new NotImplementedException(); }// constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public bool m_Feeding; // are you feeding them
        public bool m_Tort; // if true then have already tortured today
        public int m_Reason; // the reason they are here
        public int m_Weeks; // the number of weeks they have been here

        // customer data
        public int m_NumDaughters;
        public bool m_HasWife;
        public sDungeonCust m_Next;
        public sDungeonCust m_Prev;
        public int m_Health;

        void OutputCustDetailString(string Data, string detailName)
        { throw new NotImplementedException(); }
    }

}
