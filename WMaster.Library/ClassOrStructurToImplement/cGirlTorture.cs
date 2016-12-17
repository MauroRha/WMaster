using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    public class cGirlTorture : System.IDisposable
    {
        private sGirl m_Girl;
        private sGirl m_Torturer;
        private sDungeonGirl m_DungeonGirl;
        private cDungeon m_Dungeon;
        private string m_Message;
        private bool m_Fight;
        private bool m_TorturedByPlayer;

        bool girl_escapes()
        { throw new NotImplementedException(); }
        void AddTextPlayer()
        { throw new NotImplementedException(); }
        void AddTextTorturerGirl()
        { throw new NotImplementedException(); }
        void UpdateStats()
        { throw new NotImplementedException(); }
        void UpdateTraits()
        { throw new NotImplementedException(); }
        void add_trait(string trait, int pc)
        { throw new NotImplementedException(); }
        bool IsGirlInjured(uint unModifier)
        { throw new NotImplementedException(); } // Based on cGirls::GirlInjured()
        void MakeEvent(string sMsg)
        { throw new NotImplementedException(); }
        void DoTorture()
        { throw new NotImplementedException(); }

        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        cGirlTorture(sGirl pGirl)
        { throw new NotImplementedException(); } // Torture Girl by player
        cGirlTorture(sDungeonGirl pGirl)
        { throw new NotImplementedException(); } // Torture Dungeon girl by player
        cGirlTorture(sDungeonGirl pGirl, sGirl pTourturer)
        { throw new NotImplementedException(); } // Tortured by Torture job girl

    }
}
