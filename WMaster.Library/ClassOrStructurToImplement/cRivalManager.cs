using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Entity.Item;
using WMaster.Tool;

namespace WMaster.ClassOrStructurToImplement
{
    public class cRivalManager : IDisposable
    {
        public cRivalManager()
        { throw new NotImplementedException(); }
        public void Dispose()
        {
            Free();
        }
        public void Free()
        {
            if (m_Rivals != null)
            { m_Rivals.Dispose(); }
            m_Rivals = null;
            m_Last = null;
            m_NumRivals = 0;
        }
        public void Update(int NumPlayerBussiness)
        { throw new NotImplementedException(); }
        public cRival GetRandomRival()
        { throw new NotImplementedException(); }
        public cRival GetRandomRivalWithGangs()
        { throw new NotImplementedException(); }
        public cRival GetRandomRivalWithBusinesses()
        { throw new NotImplementedException(); }
        public cRival GetRandomRivalToSabotage()
        { throw new NotImplementedException(); }
        public cRival GetRivals()
        { return m_Rivals; }
        public cRival GetRival(string name)
        { throw new NotImplementedException(); }
        public cRival GetRival(int number)
        { throw new NotImplementedException(); }
        public IXmlElement SaveRivalsXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        public bool LoadRivalsXML(IXmlHandle hRivalManager)
        { throw new NotImplementedException(); }
        public void CreateRival(long bribeRate, int extort, long gold, int bars, int gambHalls, int Girls, int brothels, int gangs, int age)
        { throw new NotImplementedException(); }
        public void AddRival(cRival rival)
        { throw new NotImplementedException(); }
        public void RemoveRival(cRival rival)
        { throw new NotImplementedException(); }
        public void CreateRandomRival()
        { throw new NotImplementedException(); }
        public void check_rivals()
        { throw new NotImplementedException(); }		// `J` moved from cBrothel
        public string new_rival_text()
        { throw new NotImplementedException(); }	// `J` moved from cBrothel
        public void peace_breaks_out()
        { throw new NotImplementedException(); }	// `J` moved from cBrothel


        // `J` New - rival inventory
        public int AddRivalInv(cRival rival, sInventoryItem item)
        { throw new NotImplementedException(); }    // add item
        public bool RemoveRivalInvByNumber(cRival rival, int num)
        { throw new NotImplementedException(); }	// remove item
        public void SellRivalInvItem(cRival rival, int num)
        { throw new NotImplementedException(); }		// sell item
        public sInventoryItem GetRandomRivalItem(cRival rival)
        { throw new NotImplementedException(); }
        public sInventoryItem GetRivalItem(cRival rival, int num)
        { throw new NotImplementedException(); }
        public int GetRandomRivalItemNum(cRival rival)
        { throw new NotImplementedException(); }


        public int GetNumBusinesses()
        { throw new NotImplementedException(); }
        public int GetNumRivals()
        { return m_NumRivals; }
        public int GetNumRivalGangs()
        { throw new NotImplementedException(); }
        public bool NameExists(string name)
        { throw new NotImplementedException(); }
        public bool player_safe()
        { return m_PlayerSafe; }
        public cRival get_influential_rival()
        { throw new NotImplementedException(); }
        public string rivals_plunder_pc_gold(cRival rival)
        { throw new NotImplementedException(); }

        private int m_NumRivals;
        private cRival m_Rivals;
        private cRival m_Last;
        private bool m_PlayerSafe;
        private DoubleNameList names;
    };
}