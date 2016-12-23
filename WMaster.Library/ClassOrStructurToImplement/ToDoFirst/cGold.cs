using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cGold : cGoldBase
    {
        private SortedDictionary<int, cGoldBase> brothels = new SortedDictionary<int, cGoldBase>();
        private cGoldBase find_brothel_account(int id)
        {
            cGoldBase ac_pt = brothels[id];
            if (ac_pt == null)
            {
                ac_pt = new cGoldBase(0);
                brothels[id] = ac_pt;
            }
            return ac_pt;
        }
        public cGold()
            : base()
        {
        }
        public cGold(int initial)
            : base(initial)
        {
        }
        /*
         *	take the brothel's cGold field (m_Finance - replaces
         *	m_Upkeep and m_Income) and add it to the record
         *	Passing the brothel struct so we can record the 
         *	transactions against the brothel ID
         *
         *	(Better would be to pass m_Finance and ID separately
         *	and loosen the module coupling)
         */
        void brothel_accounts(cGold g, int brothel_id)
        { throw new NotImplementedException(); }
        void week_end()
        { throw new NotImplementedException(); }
        int total_income()
        { throw new NotImplementedException(); }
        int total_upkeep()
        { throw new NotImplementedException(); }
        /*
         *	I'm not sure whether it's easier to make the total funcs
         *	virtual, or just to repeat the inlines for the base class
         *
         *	as long as it's just the two, repeating the inlines
         *	might be better
         */
        int total_earned()
        { throw new NotImplementedException(); }//    { return m_income + m_cash_in; }
        public int total_profit()
        {
            return (int)(total_earned() - total_upkeep());
        }
        void gen_report(int month)
        { throw new NotImplementedException(); }
    }

}
