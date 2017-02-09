namespace WMaster.ClassOrStructurToImplement
{
    using System;
    using WMaster.Entity.Living.GangMission;
    using WMaster.Entity.Living;
    using WMaster.Concept.Attributs;

    /// <summary>
    /// Provinf accessor for all tarifs.
    /// </summary>
    public class Tariff
    {
        /// <summary>
        /// Get slave base price.
        /// </summary>
        /// <param name="girl"><see cref="sGirl"/> to check.</param>
        /// <returns>Price of slave.</returns>
        public double SlaveBasePrice(sGirl girl)
        {
            // The ask price is the base price for the girl. It changes with her stats, so we need to refresh it
            double cost;
            Game.Girls.CalculateAskPrice(girl, false);
            cost = girl.askprice() * 15; // base price is the girl's ask price stat

            for (int i = 0; i < EnumExtensions.Count<EnumSkills>(); i++) // add to that the sum of her skills
            {
                cost += (uint)girl.m_Skills[i];
            }
            if (Game.Girls.CheckVirginity(girl))
            {
                cost *= 1.5; // virgins fetch a premium
            }
            WMLog.Trace(string.Format("CTariff: base price for slave '{0}' = {1:0.00}", girl.Name, cost), WMLog.TraceLog.INFORMATION);
            return cost;
        }

        public Tariff()
        {
        }

        /// <summary>
        /// Cost to upgrad gang weapon.
        /// </summary>
        /// <param name="level">Level weapon to upgrad.</param>
        /// <returns>Cost of upgrading.</returns>
        public int GoonWeaponUpgrade(int level)
        {
            return (int)((level + 1) * 1200 * Configuration.OutgoingFactors.ItemCost);
        }
        /// <summary>
        /// Get gang mission cost.
        /// </summary>
        /// <param name="mission"><see cref="EnuGangMissions"/> type mission.</param>
        /// <returns>Cost of the mission.</returns>
        public int GoonMissionCost(EnuGangMissions mission)
        {

            double cost = 0.0;
            double factor = Configuration.OutgoingFactors.GoonWages;
            cost = factor * GangMissionBase.GetCostFactor(mission);

            return (int)cost;
        }

        /// <summary>
        /// Get price for <paramref name="number"/> healing pot.
        /// </summary>
        /// <param name="number">Number of potions.</param>
        /// <returns>Price for <paramref name="number"/> healing pot.</returns>
        public int HealingPrice(int number)
        {
            return (int)(number * Constants.UNITARY_HEALING_PRICE * Configuration.OutgoingFactors.Consumables);
        }
        /// <summary>
        /// Get price for <paramref name="number"/> nets.
        /// </summary>
        /// <param name="number">Number of nets.</param>
        /// <returns>Price for <paramref name="number"/> nets.</returns>
        public int NetsPrice(int number)
        {
            return (int)(number * Constants.UNITARY_NETS_PRICE * Configuration.OutgoingFactors.Consumables);
        }
        public int AntiPregPrice(int n)
        {
            return (int)(n * Constants.UNITARY_ANTI_PREG_PRICE * Configuration.OutgoingFactors.Consumables);
        }

        /// <summary>
        /// Get the strip bar price.
        /// </summary>
        /// <returns>Strip bar price.</returns>
        public int StripBarPrice()
        {
            return (int)(Constants.STRIP_BAR_TARIF * Configuration.OutgoingFactors.CasinoCost);
        }
        /// <summary>
        /// Get the gambling hall price.
        /// </summary>
        /// <returns>Gambling hall price.</returns>
        public int GamblingHallPrice()
        {
            return (int)(Constants.GAMBLING_HALL_TARIF * Configuration.OutgoingFactors.CasinoCost);
        }
        /// <summary>
        /// Get the cost of movie.
        /// </summary>
        /// <returns>Cost of movie.</returns>
        public int MovieCost()
        {
            return (int)(Constants.BASE_MOVIE_COST * Configuration.OutgoingFactors.MovieCost);
        }
        /*
         *	let's have matron wages go up as skill level increases.
         *	`J` this is no longer used
         */
        /// <summary>
        /// let's have matron wages go up as skill level increases.
        /// `J` this is no longer used
        /// </summary>
        /// <param name="level">Level of mattron.</param>
        /// <param name="numgirls">Num of girls mattron have to survey.</param>
        /// <returns>Cost of mattron wages.</returns>
        [Obsolete("This method musn't be use ('J')", true)]
        private int MatronWages(int level, int numgirls)
        {
            int basePrice = (level * 2) + numgirls * 2;
            return (int)(basePrice * Configuration.OutgoingFactors.MatronWages);
        }
        /// <summary>
        /// Get bar staff wages amount.
        /// </summary>
        /// <returns>Bar staff wages amount.</returns>
        public int BarStaffWages()
        {
            return (int)(Constants.BASE_BAS_STAFF_WAGES * Configuration.OutgoingFactors.BarCost);
        }
        /// <summary>
        /// Cost for all empty room in brothel.
        /// </summary>
        /// <param name="brothel"></param>
        /// <returns>Cost for all empty room in brothel.</returns>
        public int EmptyRoomCost(sBrothel brothel)
        {

            double cost;
            /*
            *	basic cost is number of empty rooms
            *	nominal cost is 2 gold per
            *	modified by brothel support multiplier
            */
            cost = brothel.NumRooms - brothel.NumGirls;
            cost *= 2;
            cost *= Configuration.OutgoingFactors.BrothelSupport;
            return (int)cost;
        }
        /// <summary>
        /// Cost for empty bar.
        /// </summary>
        /// <returns>Empty bar cost.</returns>
        public int EmptyBarCost()
        {
            return (int)(Constants.BASE_EMPTY_BAR_COST * Configuration.OutgoingFactors.BarCost);
        }

        // TODO : Find level and shifts descriptions.
        public int ActiveBarCost(int level, double shifts)
        {
            if (shifts > 2.0)
            {
                shifts = 2.0;
            }
            shifts /= 2.0;
            double cost = Constants.BASE_ACTIVE_BAR_COST * level / shifts;
            return (int)(cost * Configuration.OutgoingFactors.BarCost);
        }
        // TODO : Find level description.
        public int EmptyCasinoCost(int level)
        {
            return (int)(Constants.BASE_EMPTY_CASINO_COST * level * Configuration.OutgoingFactors.CasinoCost);
        }
        // TODO : Find level and shifts descriptions.
        public int ActiveCasinoCost(int level, double shifts)
        {
            if (shifts > 2.0)
            {
                shifts = 2.0;
            }
            shifts /= 2.0;
            double cost = Constants.BASE_ACTIVE_CASINO_COST * level / shifts;
            return (int)(cost * Configuration.OutgoingFactors.CasinoCost);
        }
        //  TODO : Using CasinoCost instead of staff wages??
        public int CasinoStaffWages()
        {
            //g_LogFile.ss() << "casino wages: cfg factor = " << cfg.out_fact.staff_wages();
            //g_LogFile.ssend();
            return (int)(Constants.BASE_EMPTY_CASINO_COST * Configuration.OutgoingFactors.CasinoCost);
        }
        /// <summary>
        /// Cost of advert for <paramref name="budget"/> set.
        /// </summary>
        /// <param name="budget">Setting of advert amount.</param>
        /// <returns>real cost of advert.</returns>
        public int AdvertisingCosts(int budget)
        {
            return (int)(budget * Configuration.OutgoingFactors.Advertising);
        }
        /// <summary>
        /// Get cost of adding <paramref name="numberOfRoomToAdd"/> new rooms.
        /// </summary>
        /// <param name="numberOfRoomToAdd">Number of rooms to add.</param>
        /// <returns>Cost of new rooms.</returns>
        public int AddRoomCost(int numberOfRoomToAdd)
        {
            return (int)(numberOfRoomToAdd * Constants.BASE_ROOM_COST * Configuration.OutgoingFactors.BrothelCost);
        }

        /// <summary>
        /// Get price for slave.
        /// </summary>
        /// <param name="girl">Girl to check.</param>
        /// <param name="buying"><b>True</b> for get buying price, <b>False</b> for selling price.</param>
        /// <returns></returns>
        public double SlavePrice(sGirl girl, bool buying)
        {
            if (buying)
            {
                return SlaveBuyPrice(girl);
            }
            return SlaveSellPrice(girl);
        }
        /// <summary>
        /// Get slave buy price.
        /// </summary>
        /// <param name="girl"><see cref="sGirl"/> to check.</param>
        /// <returns>Buy price of slave.</returns>
        public int SlaveBuyPrice(sGirl girl)
        {

            double cost = SlaveBasePrice(girl);
            double factor = Configuration.OutgoingFactors.SlaveCost;
            WMLog.Trace(string.Format("CTariff: buy price config factor '{0}' = {1}", girl.Name, factor), WMLog.TraceLog.INFORMATION);

            cost *= Configuration.OutgoingFactors.SlaveCost; // multiply by the config factor for buying slaves
            WMLog.Trace(string.Format("CTariff: buy price for slave '{0}' = {1:0.00}", girl.Name, cost), WMLog.TraceLog.INFORMATION);
            return (int)cost;
        }
        /// <summary>
        /// Get slave sell price.
        /// </summary>
        /// <param name="girl"><see cref="sGirl"/> to check.</param>
        /// <returns>Slavesell price.</returns>
        public int SlaveSellPrice(sGirl girl)
        {

            double cost = SlaveBasePrice(girl);
            return (int)(cost * Configuration.IncomeFactors.SlaveSales); // multiply by the config factor for buying slaves
        }

        /// <summary>
        /// Get randomize sell price for male slave.
        /// </summary>
        /// <returns>Price of male slave to sell.</returns>
        public int MaleSlaveSales()
        {
            return WMRand.Random(300) + Constants.BASE_MALE_SLAVE_SELL_PRICE;
        }
        /// <summary>
        /// Get randomize sell price for creature.
        /// </summary>
        /// <returns></returns>
        public int CreatureSales()
        {
            return WMRand.Random(2000) + Constants.BASE_CREATURE_SELL_PRICE;
        }
        /// <summary>
        /// Get cost of training girl.
        /// </summary>
        /// <returns>Cost of training girl.</returns>
        public int GirlTraining()
        {
            return (int)(Configuration.OutgoingFactors.Training * Constants.BASE_GIRL_TRAINING_COST);
        }
        /*
         *	really should do this by facility and match on name
         *
         *	that said...
         */
        /// <summary>
        /// Get price of facility.
        /// </summary>
        /// <param name="basePrice">Base price of facility.</param>
        /// <returns>Price of facility.</returns>
        [Obsolete("Must be translate to specific facility (building).")]
        public int BuyFacility(int basePrice)
        {
            return (int)(Configuration.OutgoingFactors.BrothelCost * basePrice);
        }
    }
}
