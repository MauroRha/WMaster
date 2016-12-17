using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // Keeps track of girls in the dungeon
    public class sDungeonGirl : System.IDisposable
    {
        sDungeonGirl()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public bool m_Feeding; // are you feeding them
        public int m_Reason; // the reason they are here
        public int m_Weeks; // the number of weeks they have been here

        // customer data
        public sGirl m_Girl;
        public sDungeonGirl m_Next;
        public sDungeonGirl m_Prev;

        void OutputGirlDetailString(string Data, string detailName)
        { throw new NotImplementedException(); }
    }
}
