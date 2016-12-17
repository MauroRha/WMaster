using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cGoldBase
    {
        private class income
        {
            public double brothel_work;

            public double street_work;
            public double movie_income;
            public double bar_income;
            public double gambling_profits;
            public double item_sales;
            public double slave_sales;
            public double creature_sales;
            public double extortion;
            public double plunder;
            public double petty_theft;
            public double grand_theft;
            public double catacomb_loot;
            public double objective_reward;
            public double bank_interest;
            public double misc;
            public double clinic_income;
            public double arena_income;
            public double farm_income;

            //01234567890123456789012345678901234567890123456789012345678901234567890123456789
            // --- Whores ---                              --- Sales --- 
            // Brothel  Street   Movie     Bar  Casino   Items  Monster Loc'Biz   Raids P.Theft G.Theft C'combs  Reward Intr'st    Misc
            //  123456 1234567 1234567 1234567 1234567 1234567  1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 
            public string str(int brothel_no = -1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("  --- Whores ---                              --- Sales ---");
                sb.Append("# Brothel  Street   Movie     Bar  Casino   Items  " + "Monster Loc'Biz   Raids P.Theft G.Theft C'combs  " + "Reward Intr'st    Misc      Clinic		Arena");
                if (brothel_no == -1)
                {
                    sb.Append(brothel_no + " ");
                }
                else
                {
                    sb.Append("  ");
                }
                sb.Append(/* TODO : Fint utility of setw(7)? setw(7) + */ brothel_work + " ");
                sb.Append(/*setw(7) + */street_work + " ");
                sb.Append(/*setw(7) + */movie_income + " ");
                sb.Append(/*setw(7) + */bar_income + " ");
                sb.Append(/*setw(7) + */gambling_profits + " ");
                sb.Append(/*setw(7) + */item_sales + " ");
                sb.Append(/*setw(7) + */slave_sales + " ");
                sb.Append(/*setw(7) + */creature_sales + " ");
                sb.Append(/*setw(7) + */extortion + " ");
                sb.Append(/*setw(7) + */plunder + " ");
                sb.Append(/*setw(7) + */petty_theft + " ");
                sb.Append(/*setw(7) + */grand_theft + " ");
                sb.Append(/*setw(7) + */catacomb_loot + " ");
                sb.Append(/*setw(7) + */objective_reward + " ");
                sb.Append(/*setw(7) + */bank_interest + " ");
                sb.Append(/*setw(7) + */misc + " ");
                sb.Append(/*setw(7) + */clinic_income + " ");
                sb.Append(/*setw(7) + */arena_income + " ");
                sb.Append(/*setw(7) + */farm_income + " ");
                sb.Append("\n");
                return sb.ToString();
            }
        }

        private class outcome
        {
            public double brothel_cost;
            public double slave_cost;
            public double item_cost;
            public double consumable_cost;
            public double movie_cost;
            public double goon_wages;
            public double staff_wages;
            public double girl_support;
            public double girl_training;
            public double building_upkeep;
            public double bar_upkeep;
            public double casino_upkeep;
            public double advertising_costs;
            public double centre_costs;
            public double arena_costs;
            public double bribes;
            public double fines;
            public double tax;
            public double rival_raids;
        }

        private income detail_in = new income();
        private outcome detail_out = new outcome();

        //   -------     Purchase    -------  Making --  Support  --    Girl ------   Upkeep  ------
        // # Brothel  Slaves   Items Consume  Movies    Goon    Girl Train'g Build'g     Bar  Casino Adverts  Bribes   Fines Raiders
        //   1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 

        /*
         *	Two types of transaction here: instant and delayed.
         *	Instant transactions show up in the gold total immediatly.
         *	Delayed transactions are settled at the end of the turn.
         *	Instant transactions are sometimes called cash transactions
         *	because they're effectively things the PC buys with the cash in
         *	his pocket, or where he sells something and pockets the cash
         *
         *	Delayed expenditure gets paid whether you have the gold or not,
         *	possibly driving you into debt. Instant expenditure can only happen
         *	if you have the cash in hand.
         *
         *	we're using doubles here because a lot of prices are going to be
         *	floating point values after config factors are applied: might as well
         *	track the decimals.
         */
        protected double m_initial_value;

        private double m_value;
        public double Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
        }

        private double m_upkeep;
        public double Upkeep
        {
            get { return this.m_upkeep; }
            set { this.m_upkeep = value; }
        }

        private double m_income;
        public double Income
        {
            get { return this.m_income; }
            set { this.m_income = value; }
        }

        /*
         *	cash transactions are applied directly to value
         *	but we need to record how much came in and went out
         *	so we can calculate total earnings and expenditure
         *	for the turn
         */
        protected double m_cash_in;
        public double CashIn
        {
            get { return this.m_cash_in; }
            set { this.m_cash_in = value; }
        }

        protected double m_cash_out;
        public double CashOut
        {
            get { return this.m_cash_out; }
            set { this.m_cash_out = value; }
        }
        /*
         *	this lets us combine the "if pc can afford it" test
         *	with the actual debit, streamlining cash expediture
         */
        bool debit_if_ok(double price, bool force = false)
        { throw new NotImplementedException(); }
        public cGoldBase()
        { throw new NotImplementedException(); }
        public cGoldBase(int initial)
        { throw new NotImplementedException(); }
        /*
         *	use these to save and load
         *	they save "value income upkeep" all on one line by itself
         *
         *	If I expand the variables tracked here, I'll mod the stream
         *	operators accodingly.
         */

        //ORIGINAL LINE: friend istream &operator >>(istream& is, cGoldBase &g);

        //istream operator >>(istream @is, cGoldBase g);
        /*
         *	save and load methods
         */
        IXmlElement saveGoldXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool loadGoldXML(IXmlHandle hGold)
        { throw new NotImplementedException(); }
        /*
         *	type conversion methods
         */
        string sval()
        { throw new NotImplementedException(); }
        int ival()
        { throw new NotImplementedException(); }

        void reset()
        { throw new NotImplementedException(); }
        public void cheat()
        {
            m_value = 999999;
        }
        public void zero()
        {
            m_upkeep = m_income = m_cash_in = m_cash_out = m_value = 0.0;
        }

        /*
         *	these are bools - if you can't afford it,
         *	the transaction will fail and return false
         */
        bool brothel_cost(double price)
        { throw new NotImplementedException(); }
        bool slave_cost(double price)
        { throw new NotImplementedException(); }
        bool item_cost(double cost)
        { throw new NotImplementedException(); }
        public bool consumable_cost(double cost, bool force = false)
        { throw new NotImplementedException(); }
        void movie_cost(double cost)
        { throw new NotImplementedException(); }
        /*
         *	these are paid whether or not you can
         *	afford it. You just go into debt.
         */
        void goon_wages(double cost)
        { throw new NotImplementedException(); }
        void staff_wages(double cost)
        { throw new NotImplementedException(); }
        void girl_support(double cost)
        { throw new NotImplementedException(); }
        void girl_training(double cost)
        { throw new NotImplementedException(); }
        void building_upkeep(double cost)
        { throw new NotImplementedException(); }
        void bar_upkeep(double cost)
        { throw new NotImplementedException(); }
        void casino_upkeep(double cost)
        { throw new NotImplementedException(); }
        void advertising_costs(double cost)
        { throw new NotImplementedException(); }
        void centre_costs(double cost)
        { throw new NotImplementedException(); }
        void arena_costs(double cost)
        { throw new NotImplementedException(); }
        void bribes(double cost)
        { throw new NotImplementedException(); }
        void fines(double cost)
        { throw new NotImplementedException(); }
        void tax(double cost)
        { throw new NotImplementedException(); }
        void rival_raids(double cost)
        { throw new NotImplementedException(); }
        /*
         *	income methods
         */
        void brothel_work(double income)
        { throw new NotImplementedException(); }
        void item_sales(double income)
        { throw new NotImplementedException(); }
        void slave_sales(double income)
        { throw new NotImplementedException(); }
        void creature_sales(double income)
        { throw new NotImplementedException(); }
        void movie_income(double income)
        { throw new NotImplementedException(); }
        void clinic_income(double income)
        { throw new NotImplementedException(); }
        void arena_income(double income)
        { throw new NotImplementedException(); }
        void farm_income(double income)
        { throw new NotImplementedException(); }
        void bar_income(double income)
        { throw new NotImplementedException(); }
        void gambling_profits(double income)
        { throw new NotImplementedException(); }
        void extortion(double income)
        { throw new NotImplementedException(); }
        void objective_reward(double income)
        { throw new NotImplementedException(); }
        public void plunder(double income)
        { throw new NotImplementedException(); } // from raiding rivals
        void petty_theft(double income)
        { throw new NotImplementedException(); }
        void grand_theft(double income)
        { throw new NotImplementedException(); }
        void catacomb_loot(double income)
        { throw new NotImplementedException(); }
        /*
         *	this doesn't get added to the player's
         *	cash in hand - it's just here for
         *	accounting purposes
         */
        void bank_interest(double income)
        { throw new NotImplementedException(); }
        /*
         *	the "misc" methods never get factored
         *	they're for gold that's already in the system
         *	for instance bank transactions, or taking gold
         *	from your girls
         */
        void misc_credit(double amount)
        { throw new NotImplementedException(); }
        bool misc_debit(double amount)
        { throw new NotImplementedException(); }

        private bool afford(double amount)
        {
            return amount <= m_value;
        }

        //C++ TO C# CONVERTER TODO TASK: The -= operator cannot be overloaded in C#:
        //private static cGoldBase operator -= (cGoldBase rhs)
        //{
        //    m_value -= rhs.m_value;
        //    m_income -= rhs.m_income;
        //    m_upkeep -= rhs.m_upkeep;
        //    return this;
        //}

        //C++ TO C# CONVERTER TODO TASK: The += operator cannot be overloaded in C#:
        //private static cGoldBase operator += (cGoldBase rhs)
        //{
        //    m_value += rhs.m_value;
        //    m_income += rhs.m_income;
        //    m_upkeep += rhs.m_upkeep;
        //    return this;
        //}

        /*
         *	some convienience methods
         */
        private int total_income()
        {
            return (int)m_income;
        }
        private int total_upkeep()
        {
            return (int)m_upkeep;
        }
        private int total_earned()
        {
            return (int)(m_income + m_cash_in);
        }
        private int total_profit()
        {
            double d = total_earned() - total_upkeep();
            return (int)d;
        }
    }
}