using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;

namespace WMaster.ClassOrStructurToImplement
{
    // The dungeon
    public class cDungeon : System.IDisposable
    {
        private sDungeonGirl m_Girls;
        private sDungeonGirl m_LastDGirl;
        private sDungeonCust m_Custs;
        private sDungeonCust m_LastDCusts;
        private uint m_NumberDied; // the total number of people that have died in the players dungeon
        private int m_NumGirls;
        private int m_NumCusts;

        private int m_NumGirlsTort; //    WD:    Tracking for Torturer
        private int m_NumCustsTort;

        void updateGirlTurnDungeonStats(sDungeonGirl d_girl)
        { throw new NotImplementedException(); }

        cDungeon()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor

        public void Free()
        { throw new NotImplementedException(); }
        IXmlElement SaveDungeonDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }// saves dungeon data
        bool LoadDungeonDataXML(IXmlHandle hDungeon)
        { throw new NotImplementedException(); }

        public void AddGirl(sGirl girl, DungeonReasons reason)
        { throw new NotImplementedException(); }
        void AddCust(int reason, int numDaughters, bool hasWife)
        { throw new NotImplementedException(); }
        void OutputGirlRow(int i, string Data, List<string> columnNames)
        { throw new NotImplementedException(); }
        void OutputCustRow(int i, string Data, List<string> columnNames)
        { throw new NotImplementedException(); }
        public sDungeonGirl GetGirl(int i)
        { throw new NotImplementedException(); }
        sDungeonGirl GetGirlByName(string name)
        { throw new NotImplementedException(); }
        sDungeonCust GetCust(int i)
        { throw new NotImplementedException(); }
        int GetDungeonPos(sGirl girl)
        { throw new NotImplementedException(); }
        sGirl RemoveGirl(sGirl girl)
        { throw new NotImplementedException(); }
        sGirl RemoveGirl(sDungeonGirl girl)
        { throw new NotImplementedException(); } // releases or kills a girl
        void RemoveCust(sDungeonCust cust)
        { throw new NotImplementedException(); } // releases or kills a customer
        void ClearDungeonGirlEvents()
        { throw new NotImplementedException(); }
        void Update()
        { throw new NotImplementedException(); }

        int GetGirlPos(sGirl girl)
        { throw new NotImplementedException(); }

        public int GetNumCusts()
        {
            return m_NumCusts;
        }
        public int GetNumGirls()
        {
            return m_NumGirls;
        }
        public uint GetNumDied()
        {
            return m_NumberDied;
        }

        public int NumGirlsTort()
        {
            return m_NumGirlsTort;
        }
        public int NumGirlsTort(int n)
        {
            m_NumGirlsTort += n;
            return m_NumGirlsTort;
        }
        public int NumCustsTort()
        {
            return m_NumCustsTort;
        }
        public int NumCustsTort(int n)
        {
            m_NumCustsTort += n;
            return m_NumCustsTort;
        }

        // WD:	Torturer tortures dungeon girl. 
        //void doTorturer(sDungeonGirl* d_girl, sGirl* t_girl, string& summary);	{ cGirlTorture::cGirlTorture(d_girl, t_girl) }

        void PlaceDungeonGirl(sDungeonGirl newGirl)
        { throw new NotImplementedException(); }
        void PlaceDungeonCustomer(sDungeonCust newCust)
        { throw new NotImplementedException(); }
    }
}
