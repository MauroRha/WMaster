/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace WMaster.Concept
{
    using System;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WMaster.Tool;

    /*
    * Two types of transaction here: instant and delayed.
    * Instant transactions show up in the gold total immediatly.
    * Delayed transactions are settled at the end of the turn.
    *
    * They also break down by income and expenditure, which should
    * be obvious.
    *
    * Delayed expenditure gets paid whether you have the gold or not,
    * possibly driving you into debt. Instant expenditure can only happen
    * ifyou have the cash in hand.
    */
    [Serializable()]
    [XmlRoot("Gold")]
    public class GoldBase : ISerialisableEntity
    {
        /// <summary>
        /// Alow conservation of all incom by type.
        /// </summary>
        private class GoldIncome
        {
            public double BrothelWork { get; set; }
            private double street_work;
            public double Movie { get; set; }
            public double Bar { get; set; }
            public double GamblingProfits { get; set; }
            public double ItemSales { get; set; }
            public double SlaveSales { get; set; }
            public double CreatureSales { get; set; }
            public double Extortion { get; set; }
            public double Plunder { get; set; }
            public double PettyTheft { get; set; }
            public double GrandTheft { get; set; }
            public double CatacombLoot { get; set; }
            public double ObjectiveReward { get; set; }
            private double bank_interest;
            public double Misc { get; set; }
            public double Clinic { get; set; }
            public double Arena { get; set; }
            public double Farm { get; set; }

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
                sb.Append(/* TODO : Fint utility of setw(7)? setw(7) + */ BrothelWork + " ");
                sb.Append(/*setw(7) + */street_work + " ");
                sb.Append(/*setw(7) + */Movie + " ");
                sb.Append(/*setw(7) + */Bar + " ");
                sb.Append(/*setw(7) + */GamblingProfits + " ");
                sb.Append(/*setw(7) + */ItemSales + " ");
                sb.Append(/*setw(7) + */SlaveSales + " ");
                sb.Append(/*setw(7) + */CreatureSales + " ");
                sb.Append(/*setw(7) + */Extortion + " ");
                sb.Append(/*setw(7) + */Plunder + " ");
                sb.Append(/*setw(7) + */PettyTheft + " ");
                sb.Append(/*setw(7) + */GrandTheft + " ");
                sb.Append(/*setw(7) + */CatacombLoot + " ");
                sb.Append(/*setw(7) + */ObjectiveReward + " ");
                sb.Append(/*setw(7) + */bank_interest + " ");
                sb.Append(/*setw(7) + */Misc + " ");
                sb.Append(/*setw(7) + */Clinic + " ");
                sb.Append(/*setw(7) + */Arena + " ");
                sb.Append(/*setw(7) + */Farm + " ");
                sb.Append("\n");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Store alloutcom by type.
        /// </summary>
        private class GoldOutcome
        {
            public double BrothelCost { get; set; }
            public double SlaveCost { get; set; }
            public double ItemCost { get; set; }
            public double CconsumableCost { get; set; }
            public double MovieCost { get; set; }
            public double GoonWages { get; set; }
            public double StaffWages { get; set; }
            public double GirlSupport { get; set; }
            public double GirlTraining { get; set; }
            public double BuildingUpkeep { get; set; }
            public double BarUpkeep { get; set; }
            public double CasinoUpkeep { get; set; }
            public double AdvertisingCosts { get; set; }
            public double CentreCosts { get; set; }
            public double ArenaCosts { get; set; }
            public double Bribes { get; set; }
            public double Fines { get; set; }
            public double Tax { get; set; }
            public double RivalRaids { get; set; }
            public double Misc { get; set; }
        }

        private GoldIncome detailIncome = new GoldIncome();
        private GoldOutcome detailOutcome = new GoldOutcome();

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
        protected double m_InitialValue;

        private double m_Value;
        public double Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }

        private double m_Upkeep;
        public double Upkeep
        {
            get { return this.m_Upkeep; }
            set { this.m_Upkeep = value; }
        }

        private double m_Income;
        public double Income
        {
            get { return this.m_Income; }
            set { this.m_Income = value; }
        }

        /*
         *	cash transactions are applied directly to value
         *	but we need to record how much came in and went out
         *	so we can calculate total earnings and expenditure
         *	for the turn
         */
        protected double m_CashIn;
        public double CashIn
        {
            get { return this.m_CashIn; }
            set { this.m_CashIn = value; }
        }

        protected double m_CashOut;
        public double CashOut
        {
            get { return this.m_CashOut; }
            set { this.m_CashOut = value; }
        }

        /// <summary>
        /// Initialise a new instance of <see cref="GoldBase"/> with default initial gold value.
        /// </summary>
        public GoldBase()
            : this(Configuration.Initial.Gold)
        { }
        /// <summary>
        /// Initialise a new instance of <see cref="GoldBase"/> with <paramref name="initialGoldValue"/> initial gold value.
        /// </summary>
        /// <param name="initialGoldValue">Initial gold amounth.</param>
        public GoldBase(int initialGoldValue)
        {
            this.m_InitialValue = initialGoldValue;
            this.Reset();
        }


        /// <summary>
        /// (Re)Initialise gold data.
        /// </summary>
        public void Reset()
        {
            this.m_Value = m_InitialValue;
            this.m_Upkeep = 0;
            this.m_Income = 0;
            this.m_CashIn = 0;
            this.m_CashOut = 0;
        }

        /*
         *	this lets us combine the "if pc can afford it" test
         *	with the actual debit, streamlining cash expediture
         */
        // TODO : flag force may be set inverted.
        /// <summary>
        /// This is for cash purchases. It lets us test and debit in one function call.
        /// <remarks>
        ///     <para>
        ///         Some transactions can be executed as instant or delayed depending on how they happen.
        ///         Healing potions, for instance are cash transactions from the upgrade screen,
        ///         but if they're auto-restocked, that's a delayed cost.
        ///         For such cases the force flag lets us execute this as a delayed transaction.
        ///     </para>
        /// </remarks>
        /// </summary>
        /// <param name="price">Price to substract to cash if cash is greater than price.</param>
        /// <param name="force">If <b>True</b> the transaction is done even if there are not enouth cash.</param>
        /// <returns><b>True</b> if transaction was done. <b>False</b> elsewere</returns>
        protected bool DebitIfOk(double price, bool force = false)
        {
            if ((price > m_Value) && !force) { return false; }

            m_Value -= price;
            m_CashOut -= price;
            return true;
        }
        /*
         *	type conversion methods
         */
        /// <summary>
        /// Return string representation of golds value.
        /// </summary>
        /// <returns>String representation of golds value.</returns>
        public string StringVal()
        { return this.IntVal().ToString(); }
        /// <summary>
        /// Return gold value round to integer.
        /// </summary>
        /// <returns>Gold value round to integer.</returns>
        public int IntVal()
        { return (int)Math.Floor(m_Value); }

        /// <summary>
        /// Apply cheat code, set gold value to 999999.
        /// </summary>
        public void Cheat()
        {
            m_Value = 999999;
        }

        /// <summary>
        /// Set all goldvalue to 0.
        /// </summary>
        public void Zero()
        {
            m_Upkeep = m_Income = m_CashIn = m_CashOut = m_Value = 0.0;
        }

        /*
         *	these are bools - if you can't afford it,
         *	the transaction will fail and return false
         */
        /// <summary>
        /// buying a new building - cash transactions only please.
        /// </summary>
        /// <param name="price">Cost of new brothel.</param>
        /// <returns><b>True</b> if transaction was done.</returns>
        public bool BrothelCost(double price)
        {
            price = Configuration.OutgoingFactors.BrothelCost * price;

            if (DebitIfOk(price))
            {
                detailOutcome.BrothelCost += price;
                return true;
            }
            else
            { return false; }
        }
        /// <summary>
        /// Cost of slave to buy.
        /// </summary>
        /// <param name="price">Cost of slave.</param>
        /// <returns><b>True</b> if transaction was done.</returns>
        public bool SlaveCost(double price)
        {
            price = Configuration.OutgoingFactors.SlaveCost * price;

            if (DebitIfOk(price))
            {
                detailOutcome.SlaveCost += price;
                return true;
            }
            else
            { return false; }
        }
        /// <summary>
        /// Cost of item to buy.
        /// </summary>
        /// <param name="price">Cost of item.</param>
        /// <returns><b>True</b> if transaction was done.</returns>
        public bool ItemCost(double price)
        {
            price = Configuration.OutgoingFactors.ItemCost * price;

            if (DebitIfOk(price))
            {
                detailOutcome.ItemCost += price;
                return true;
            }
            else
            { return false; }
        }
        /// <summary>
        /// Consumables are things like potions, nets and booze which tend to be auto-stockable.
        /// So we have a force flag here for the restock call
        /// </summary>
        /// <param name="price">Cost of consumable.</param>
        /// <param name="force"><b>True</b> to force transaction even if cash wasn't enough.</param>
        /// <returns><b>True</b> if transaction was done.</returns>
        public bool ConsumableCost(double price, bool force = false)
        {
            price = Configuration.OutgoingFactors.Consumables * price;

            if (DebitIfOk(price, force))
            {
                detailOutcome.CconsumableCost += price;
                return true;
            }
            else
            { return false; }
        }
        /// <summary>
        /// Counterpart of MiscCredit - should probably also go away
        /// </summary>
        /// <param name="price">Cost of misc.</param>
        /// <returns><b>True</b> if transaction was done.</returns>
        public bool MiscDebit(double price)
        {
            if (DebitIfOk(price))
            {
                detailOutcome.Misc += price;
                return true;
            }
            else
            { return false; }
        }
        /*
         *	these are paid whether or not you can
         *	afford it. You just go into debt.
         */
        /// <summary>
        /// Cost of movie.
        /// </summary>
        /// <param name="price">Cost of movie.</param>
        public void MovieCost(double price)
        {
            price = Configuration.OutgoingFactors.MovieCost * price;

            m_Upkeep += price;
            detailOutcome.MovieCost += price;
        }
        /// <summary>
        /// Gang wages cost.
        /// </summary>
        /// <param name="price">Gang wages cost.</param>
        public void GoonWages(double price)
        {
            price = Configuration.OutgoingFactors.GoonWages * price;

            m_Upkeep += price;
            detailOutcome.GoonWages += price;
        }
        /// <summary>
        /// Staff wages cost.
        /// </summary>
        /// <param name="price">Staff wages cost.</param>
        public void StaffWages(double price)
        {
            price = Configuration.OutgoingFactors.StaffWages * price;

            m_Upkeep += price;
            detailOutcome.StaffWages += price;
        }
        /// <summary>
        /// Cost of entertaining girl.
        /// </summary>
        /// <param name="cost">Cost of entertaining girl.</param>
        public void GirlSupport(double price)
        {
            price = Configuration.OutgoingFactors.GirlSupport * price;

            m_Upkeep += price;
            detailOutcome.GirlSupport += price;
        }
        /// <summary>
        /// Training is a delayed cost
        /// </summary>
        /// <param name="cost">Costof girl training.</param>
        public void GirlTraining(double price)
        {
            price = Configuration.OutgoingFactors.GirlTraining * price;

            m_Upkeep += price;
            detailOutcome.GirlTraining += price;
        }
        /// <summary>
        /// Costs associated with building maintenance and upgread.
        /// (but not bar and casino costs which have their own cost category).
        /// </summary>
        /// <param name="price">Costs associated with building maintenance.</param>
        public void BuildingUpkeep(double price)
        {
            price = Configuration.OutgoingFactors.BuildingUpkeep * price;

            m_Upkeep += price;
            detailOutcome.BuildingUpkeep += price;
        }
        /// <summary>
        /// Cost of bar upkeep.
        /// </summary>
        /// <param name="price">Cost of bar upkeep.</param>
        public void BarUpkeep(double price)
        {
            price = Configuration.OutgoingFactors.BarCost * price;

            m_Upkeep += price;
            detailOutcome.BarUpkeep += price;
        }
        /// <summary>
        /// Cost of casino upkeep.
        /// </summary>
        /// <param name="price">Cost of casino upkeep.</param>
        public void CasinoUpkeep(double price)
        {
            price = Configuration.OutgoingFactors.CasinoCost * price;

            m_Upkeep += price;
            detailOutcome.CasinoUpkeep += price;
        }
        /// <summary>
        /// Price of advertising.
        /// </summary>
        /// <param name="price">Price of advertising.</param>
        public void AdvertisingCosts(double price)
        {
            price = Configuration.OutgoingFactors.Advertising * price;

            m_Upkeep += price;
            detailOutcome.AdvertisingCosts += price;
        }
        /// <summary>
        /// Cost of center upkeep.
        /// </summary>
        /// <param name="price">Cost of center upkeep.</param>
        public void CentreCosts(double price)
        {
            price = Configuration.OutgoingFactors.CentreCosts * price;

            m_Upkeep += price;
            detailOutcome.CentreCosts += price;
        }
        /// <summary>
        /// Cost of arena upkeep.
        /// </summary>
        /// <param name="price">Cost of arena upkeep.</param>
        public void ArenaCosts(double price)
        {
            price = Configuration.OutgoingFactors.ArenaCosts * price;

            m_Upkeep += price;
            detailOutcome.ArenaCosts += price;
        }
        /// <summary>
        /// Cost ob bribes.
        /// </summary>
        /// <param name="price">Cost ob bribes.</param>
        public void Bribes(double price)
        {
            price = Configuration.OutgoingFactors.Bribes * price;

            m_Upkeep += price;
            detailOutcome.Bribes += price;
        }
        /// <summary>
        /// Cost of fines.
        /// </summary>
        /// <param name="price">Cost of fines.</param>
        public void Fines(double price)
        {
            price = Configuration.OutgoingFactors.Fines * price;

            m_Value -= price;
            m_CashOut -= price;
            detailOutcome.Fines += price;
        }
        /// <summary>
        /// Cost of taxes.
        /// </summary>
        /// <param name="price">Cost of taxes.</param>
        public void Tax(double price)
        {
            price = Configuration.OutgoingFactors.Tax * price;

            m_Value -= price;
            m_CashOut -= price;
            detailOutcome.Tax += price;
        }
        /// <summary>
        /// Cost of rival raids.
        /// </summary>
        /// <param name="price">Cost of rival raids.</param>
        public void RivalRaids(double price)
        {
            price = Configuration.OutgoingFactors.RivalRaids * price;

            m_Value -= price;
            m_CashOut -= price;
            detailOutcome.RivalRaids += price;
        }
        /*
            *	income methods
            */
        /// <summary>
        /// This is for girls working at the brothel - goes into income
        /// </summary>
        /// <param name="income">Girls working at the brothel income.</param>
        public void Brothelork(double income)
        {
            income = Configuration.IncomeFactors.BrothelWork * income;

            m_Income += income;
            detailIncome.BrothelWork += income;
        }
        /// <summary>
        /// For stuff sold in the marketplace - goes straight into the PC's pocket.
        /// </summary>
        /// <param name="income">Income value before Configuration.IncomeFactors.ItemSales application.</param>
        public void ItemSales(double income)
        {
            income = Configuration.IncomeFactors.ItemSales * income;

            m_Value += income;
            m_CashIn += income;
            detailIncome.ItemSales += income;
        }
        /// <summary>
        /// For slave sold in the slave market - goes straight into the PC's pocket.
        /// </summary>
        /// <param name="income">Income value before Configuration.IncomeFactors.sla application.</param>
        public void SlaveSales(double income)
        {
            income = Configuration.IncomeFactors.SlaveSales * income;

            m_Value += income;
            m_CashIn += income;
            detailIncome.SlaveSales += income;
        }
        /// <summary>
        /// For when a girl gives birth to a monster -  The cash goes to the brothel, so we add this to m_income
        /// </summary>
        /// <param name="income">Selling creature amount</param>
        public void CreatureSales(double income)
        {
            income = Configuration.IncomeFactors.CreatureSales * income;

            m_Income += income;
            detailIncome.CreatureSales += income;
        }
        /// <summary>
        /// Income from movie crystals
        /// </summary>
        /// <param name="income">Incom from movie.</param>
        public void MovieIncome(double income)
        {
            income = Configuration.IncomeFactors.Movie * income;

            m_Income += income;
            detailIncome.Movie += income;
        }
        /// <summary>
        /// Income from the clinic.
        /// </summary>
        /// <param name="income">Income from the clinic.</param>
        public void ClinicIncome(double income)
        {
            income = Configuration.IncomeFactors.Clinic * income;

            m_Income += income;
            detailIncome.Clinic += income;
        }
        /// <summary>
        /// Income from the arena.
        /// </summary>
        /// <param name="income">Income from the arena.</param>
        public void ArenaIncome(double income)
        {
            income = Configuration.IncomeFactors.Arena * income;

            m_Income += income;
            detailIncome.Arena += income;
        }
        /// <summary>
        /// Income from the farm.
        /// </summary>
        /// <param name="income">Income from the farm.</param>
        public void FarmIncome(double income)
        {
            income = Configuration.IncomeFactors.Farm * income;

            m_Income += income;
            detailIncome.Farm += income;
        }
        /// <summary>
        /// Income from the bar.
        /// </summary>
        /// <param name="income">Income from the bar.</param>
        public void BarIncome(double income)
        {
            income = Configuration.IncomeFactors.Bar * income;

            m_Income += income;
            detailIncome.Bar += income;
        }
        /// <summary>
        /// Income from gambling halls.
        /// </summary>
        /// <param name="income">Income from gambling halls.</param>
        public void GamblingProfits(double income)
        {
            income = Configuration.IncomeFactors.GamblingProfits * income;

            m_Income += income;
            detailIncome.GamblingProfits += income;
        }
        /// <summary>
        /// Income from businesess under player control.
        /// </summary>
        /// <param name="income">Income from businesess under player control.</param>
        public void Extortion(double income)
        {
            income = Configuration.IncomeFactors.Extortion * income;

            m_Income += income;
            detailIncome.Extortion += income;
        }
        /// <summary>
        /// These happen at end of turn anyway - so let's do them as delayed transactions.
        /// </summary>
        /// <param name="income">Income for reward of objective.</param>
        public void ObjectiveReward(double income)
        {
            income = Configuration.IncomeFactors.ObjectiveReward * income;

            m_Income += income;
            detailIncome.ObjectiveReward += income;
        }
        /// <summary>
        /// Income from plunder.
        /// </summary>
        /// <param name="income">Income from plunder.</param>
        public void Plunder(double income)
        {
            income = Configuration.IncomeFactors.Plunder * income;

            m_Income += income;
            detailIncome.Plunder += income;
        }
        /// <summary>
        /// Income from petty theft.
        /// </summary>
        /// <param name="income">Income from plunder.</param>
        public void PettyTheft(double income)
        {
            income = Configuration.IncomeFactors.PettyTheft * income;

            m_Income += income;
            detailIncome.PettyTheft += income;
        }
        /// <summary>
        /// Income from grand theft.
        /// </summary>
        /// <param name="income">Income from plunder.</param>
        public void GrandTheft(double income)
        {
            income = Configuration.IncomeFactors.GrandTheft * income;

            m_Income += income;
            detailIncome.GrandTheft += income;
        }
        /// <summary>
        /// Income from loot in catacomb.
        /// </summary>
        /// <param name="income">Income from plunder.</param>
        public void CatacombLoot(double income)
        {
            income = Configuration.IncomeFactors.CatacombLoot * income;

            m_Income += income;
            detailIncome.CatacombLoot += income;
        }
        /// <summary>
        /// Untaxed credit.
        /// <remarks>
        ///     <para>
        ///         The "misc" methods never get factored. They're for gold that's already in the system
        ///         for instance bank transactions, or taking gold from your girls
        ///     </para>
        /// </remarks>
        /// </summary>
        /// <param name="income">Income value to credit.</param>
        public void MiscCredit(double income)
        {
            m_Value += income;
            detailIncome.Misc += income;
        }

        public void BrothelWork(double income)
        {
            m_Income += income;
            detailIncome.BrothelWork += income;
        }

        /*
         *	this doesn't get added to the player's
         *	cash in hand - it's just here for
         *	accounting purposes
         */
        /// <summary>
        /// do nothing for now - placeholder until accounting stuff is added
        /// </summary>
        /// <param name="income"></param>
        public void BankInterest(double income)
        {
            // do nothing for now - placeholder until accounting
            // stuff is added
        }

        /// <summary>
        /// Check if gold value is greater to <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">Value to check.</param>
        /// <returns><b>True</b> if gold value is greater to <paramref name="amount"/>.</returns>
        private bool Afford(double amount)
        {
            return amount <= m_Value;
        }

        public static GoldBase operator -(GoldBase gb1, GoldBase gb2)
        {
            GoldBase returnValue = new GoldBase();
            returnValue.Value =         gb1.Value - gb2.Value;
            returnValue.Income = gb1.Income - gb2.Income;
            returnValue.Upkeep = gb1.Upkeep - gb2.Upkeep;
            return returnValue;
        }

        public static GoldBase operator +(GoldBase gb1, GoldBase gb2)
        {
            GoldBase returnValue = new GoldBase();
            returnValue.Value = gb1.Value + gb2.Value;
            returnValue.Income = gb1.Income + gb2.Income;
            returnValue.Upkeep = gb1.Upkeep + gb2.Upkeep;
            return returnValue;
        }

        /*
         *	some convienience methods
         */
        public virtual int TotalIncome()
        {
            return (int)m_Income;
        }
        public virtual int TotalUpkeep()
        {
            return (int)m_Upkeep;
        }
        public virtual int TotalEarned()
        {
            return (int)(m_Income + m_CashIn);
        }
        public virtual int TotalProfit()
        {
            double d = TotalEarned() - TotalUpkeep();
            return (int)d;
        }

        #region Serialisation
        /// <summary>
        /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
        /// </summary>
        /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
        /// <returns><b>True</b> if no error occure.</returns>
        public bool Serialise(XElement data)
        {
            if (data == null)
            { return false; }

            try
            {
                Serialiser.SetInvarientCulture();
                data.Add(new XAttribute("value", this.Value));
                data.Add(new XAttribute("income", this.Income));
                data.Add(new XAttribute("upkeep", this.Upkeep));
                data.Add(new XAttribute("cash_in", this.CashIn));
                data.Add(new XAttribute("cash_out", this.CashOut));

                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
            finally
            { Serialiser.RestoreCurrentCulture(); }
        }

        /// <summary>
        /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
        /// </summary>
        /// <param name="data">Where to find data to deserialise.</param>
        /// <returns><b>True</b> if no error occure.</returns>
        public bool Deserialise(XElement data)
        {
            if (data == null)
            { return false; }

            try
            {
                Serialiser.SetInvarientCulture();

                Serialiser.SetValue(data.Attribute("value").Value, ref this.m_Value);
                Serialiser.SetValue(data.Attribute("income").Value, ref this.m_Income);
                Serialiser.SetValue(data.Attribute("upkeep").Value, ref this.m_Upkeep);
                Serialiser.SetValue(data.Attribute("cash_in").Value, ref this.m_CashIn);
                Serialiser.SetValue(data.Attribute("cash_out").Value, ref this.m_CashOut);
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
            finally
            { Serialiser.RestoreCurrentCulture(); }
        }
        #endregion
    }
}