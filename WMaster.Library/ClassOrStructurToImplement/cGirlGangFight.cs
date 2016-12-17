using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    public class cGirlGangFight
    {
        private sGirl m_girl;
        //private CLog l = new CLog();

        private int m_girl_stats;
        private int m_goon_stats;
        private int m_max_goons;
        //	double	m_ratio;
        //	int		m_dead_goons;

        private bool m_girl_fights;
        private bool m_girl_wins;
        private bool m_player_wins;
        private bool m_wipeout;
        private bool m_unopposed;

        private double m_odds;

        void lose_vs_own_gang(Gang gang)
        { throw new NotImplementedException(); }
        void win_vs_own_gang(Gang gang)
        { throw new NotImplementedException(); }
        int use_potions(Gang gang, int casualties)
        { throw new NotImplementedException(); }
        cGirlGangFight(sGirl girl)
        { throw new NotImplementedException(); }

        public bool girl_fights()
        {
            return m_girl_fights;
        }
        public bool girl_submits()
        {
            return !m_girl_fights;
        }
        public bool girl_won()
        {
            return m_girl_wins;
        }
        public bool girl_lost()
        {
            return !m_girl_wins;
        }
        public bool player_won()
        {
            return m_player_wins;
        }

        public bool wipeout()
        {
            return m_wipeout;
        }

        //	int dead_goons()	{ return m_dead_goons; }
    }

}
