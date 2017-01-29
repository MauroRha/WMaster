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
namespace WMaster.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Concept.Attributs;
    using WMaster.Entity.Item;
    using WMaster.Enums;

    /// <summary>
    /// Manages all brothels
    /// <remarks>
    ///     <para>
    ///         Anyone else think this class tries to do too much?
    ///         Yes it does, I am working on reducing it-Delta
    ///    </para>
    ///    <para>GBN : Yep, I think so too. I am working on too</para>
    /// </remarks>
    /// </summary>
    public class cBrothelManager : IDisposable
    {
        #region Singleton
        /// <summary>
        /// Singleton of <see cref="cBrothelManager"/>.
        /// </summary>
        private static cBrothelManager m_Instance;
        /// <summary>
        /// Get unique instance of <see cref="cBrothelManager"/>.
        /// </summary>
        public static cBrothelManager Instance
        {
            get
            {
                if (cBrothelManager.m_Instance == null)
                { cBrothelManager.m_Instance = new cBrothelManager(); }
                return cBrothelManager.m_Instance;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public cBrothelManager()
        {
            for (int i = 0; i < Constants.MAXNUM_INVENTORY; i++)
            {
                m_Inventory[i] = null;
                m_EquipedItems[i] = 0;
                m_NumItem[i] = 0;
            }
            /* sBrothel */
            m_BrothelList = new List<sBrothel>();
            /* int */
            m_Influence = m_NumInventory = m_HandmadeGoods = m_Beasts = m_Alchemy = 0;
            /* int */
            m_SupplyShedLevel = 1;
            /* int */
            m_HandmadeGoodsReserves = m_AlchemyReserves = 0;
            /* int */
            m_BeastsReserves = 100;
            /* int */
            m_FoodReserves = m_DrinksReserves = 1000;
            /* int */
            m_ProcessingShift = -1;
            /* long */
            m_BribeRate = m_Bank = 0;
            /* sObjective */
            m_Objective = null;
            /* sGirl */
            m_PrisonGirlList = new List<sGirl>();
            m_RunawaysGirlList = new List<sGirl>();
            /* bool */
            m_TortureDoneFlag = false;
            m_JobManager.Setup();
        }
        #endregion

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            Free();
        }

        public void Free()
        {
            /* sGirls */
            m_PrisonGirlList.Clear();
            m_RunawaysGirlList.Clear();

            m_NumInventory = 0;
            for (int i = 0; i < Constants.MAXNUM_INVENTORY; i++)
            {
                m_Inventory[i] = null;
                m_EquipedItems[i] = 0;
                m_NumItem[i] = 0;
            }
            /* long   */
            m_BribeRate = m_Bank = 0;
            /* int    */
            m_Influence = m_HandmadeGoods = m_Beasts = m_Alchemy = 0;
            /* int    */
            m_SupplyShedLevel = 1;
            
            m_Objective = null;

            m_Dungeon.Free();
            m_Rivals.Free();
            
            m_BrothelList.Clear();
        }

        public sGirl GetDrugPossessor()
        { throw new NotImplementedException(); }

        public void AddGirlToPrison(sGirl girl)
        { throw new NotImplementedException(); }

        [Obsolete("Remove from PrisonGrilList", true)]
        public void RemoveGirlFromPrison(sGirl girl)
        { throw new NotImplementedException(); }

        public int GetNumInPrison()
        { return m_PrisonGirlList.Count; }

        public void AddGirlToRunaways(sGirl girl)
        { throw new NotImplementedException(); }
        public void RemoveGirlFromRunaways(sGirl girl)
        { throw new NotImplementedException(); }

        public int GetNumRunaways()
        { return m_RunawaysGirlList.Count; }


        public void NewBrothel(int NumRooms, int MaxNumRooms)
        {
            sBrothel newBroth = new sBrothel();
            newBroth.NumRooms = NumRooms;
            newBroth.MaxNumRooms = MaxNumRooms;

            AddBrothel(newBroth);
        }

        /// <summary>
        /// Remove brothel with id = <paramref name="identifiantBrothel"/>.
        /// </summary>
        /// <param name="identifiantBrothel">Identifiant of brothel to remove.</param>
        public void DestroyBrothel(int identifiantBrothel)
        {
            foreach (sBrothel brothel in m_BrothelList)
            {
                if (brothel.Id == identifiantBrothel)
                {
                    m_BrothelList.Remove(brothel);
                    break;
                }
            }
        }

        // TODO : REFACTORING - Really TO BIG function. Must to split it into responsibility target object.
        // TODO : REFACTORING - Translate repport to specific function -> create report summary object and/or report context.
        public void UpdateBrothels()
        {
            Tariff tariff = new Tariff();
            LocalString updateBrothel = new LocalString();
            Jobs restjob = Jobs.RESTING;
            Jobs matronjob = Jobs.MATRON;
            Jobs firstjob = Jobs.RESTING;
            Jobs lastjob = Jobs.WHORESTREETS;

            m_TortureDoneFlag = false; //WD: Reset flag each day is set in WorkTorture()

            UpdateBribeInfluence();

            foreach (sBrothel current in m_BrothelList)
            {
                // reset the data
                current.Happiness = current.MiscCustomers = current.TotalCustomers = 0;
                current.m_Finance.Zero();
                current.m_Events.Clear();
                current.AntiPregUsed = 0;
                current.RejectCustomersRestrict = current.RejectCustomersDisease = 0;

                bool matron = (GetNumGirlsOnJob(current.Id, matronjob, DayShift.Day) >= 1) ? true : false;


                #region Start of Turn Girl Setup
                //    `J` do all the things that the girls do at the start of the turn
                // TODO : REFACTORING - The girls do -> Move to GirlManager or Girl class
                foreach (sGirl cgirl in current.GirlsList)
                {
                    string girlName = cgirl.Realname;

                    // Remove any dead bodies from last week
                    if (cgirl.is_dead())
                    {
                        sGirl deadGirl = cgirl;
                        // increase all the girls fear and hate of the player for letting her die (weather his fault or not)
                        UpdateAllGirlsStat(current, EnumStats.PCFEAR, 2);
                        UpdateAllGirlsStat(current, EnumStats.PCHATE, 1);
                        // Two messages go into the girl queue...

                        updateBrothel.Clear();
                        updateBrothel.AppendFormat(
                            LocalString.ResourceStringCategory.Player,
                            "[GirlName]HasDiedFromHerInjuriesTheOtherGirlsAllFearAndHateYouALittleMore",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        deadGirl.Events.AddMessage(updateBrothel.ToString(), ImageType.DEATH, EventType.Danger);
                        Game.MessageQue.Enqueue(updateBrothel.ToString(), MessageCategory.Red);

                        updateBrothel.Clear();
                        updateBrothel.AppendFormat(
                            LocalString.ResourceStringCategory.Player,
                            "[GirlName]HasDiedFromHerInjuriesHerBodyWillBeRemovedByTheEndOfTheWeek",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        deadGirl.Events.AddMessage(updateBrothel.ToString(), ImageType.DEATH, EventType.Summary);
                        RemoveGirl(current.Id, deadGirl);
                        updateBrothel.Clear();
                    }
                    else
                    {
                        cgirl.where_is_she = current.Id;
                        cgirl.m_InStudio = cgirl.m_InArena = cgirl.m_InCentre = cgirl.m_InClinic = cgirl.m_InFarm = cgirl.m_InHouse = false;
                        cgirl.Events.Clear();
                        cgirl.Pay = cgirl.Tips = 0;
                        cgirl.m_Tort = false;

                        //TODO : REFACTORING - Rewrite this bloc of code to secure treatment. Translate enum Jobs to Class Jobs or building to store building information.
                        // `J` Check for out of building jobs
                        if (!sBrothel.IsBrothelJob(cgirl.DayJob))
                        {
                            cgirl.DayJob = restjob;
                        }
                        if (!sBrothel.IsBrothelJob(cgirl.NightJob))
                        {
                            cgirl.NightJob = restjob;
                        }
                        if (!sBrothel.IsBrothelJob(cgirl.PrevDayJob))
                        {
                            cgirl.PrevDayJob = Jobs.NotSet;
                        }
                        if (!sBrothel.IsBrothelJob(cgirl.PrevNightJob))
                        {
                            cgirl.PrevNightJob = Jobs.NotSet;
                        }
                        // set yesterday jobs for everyone
                        cgirl.YesterDayJob = cgirl.DayJob;
                        cgirl.YesterNightJob = cgirl.NightJob;
                        cgirl.m_Refused_To_Work_Day = cgirl.m_Refused_To_Work_Night = false;
                        cgirl.m_NumCusts_old = cgirl.m_NumCusts; // prepare for this week
                        string summary = "";

                        Game.Girls.AddTiredness(cgirl); // `J` moved all girls add tiredness to one place
                        do_food_and_digs(current, cgirl); // Brothel only update for girls accommodation level
                        Game.Girls.CalculateGirlType(cgirl); // update the fetish traits
                        Game.Girls.updateGirlAge(cgirl, true); // update birthday counter and age the girl
                        Game.Girls.HandleChildren(cgirl, summary); // handle pregnancy and children growing up
                        Game.Girls.updateSTD(cgirl); // health loss to STD's                NOTE: Girl can die
                        Game.Girls.updateHappyTraits(cgirl); // Update happiness due to Traits    NOTE: Girl can die
                        updateGirlTurnBrothelStats(cgirl); // Update daily stats                Now only runs once per day
                        Game.Girls.updateGirlTurnStats(cgirl); // Stat Code common to Dugeon and Brothel

                        if (cgirl.m_JustGaveBirth) // if she gave birth, let her rest this week
                        {
                            if (cgirl.DayJob != restjob)
                            {
                                cgirl.PrevDayJob = cgirl.DayJob;
                            }
                            if (cgirl.NightJob != restjob)
                            {
                                cgirl.PrevNightJob = cgirl.NightJob;
                            }
                            cgirl.DayJob = cgirl.NightJob = restjob;
                        }
                    }
                }
                #endregion

                //TODO : REFACTORING - Create function HalfDayShift to factorise code oft day & night shift
                #region Day Shift
                // Moved to here so Security drops once per day instead of everytime a girl works security -PP
                current.SecurityLevel -= 10;
                current.SecurityLevel -= current.NumGirls; //`J` m_SecurityLevel is extremely over powered. Reducing it's power a lot.
                if (current.SecurityLevel <= 0)
                {
                    current.SecurityLevel = 0; // crazy added
                }

                // Generate customers for the brothel for the day shift and update girls
                cJobManager.do_advertising(current, DayShift.Day);
                Game.Customers.GenerateCustomers(current, 0);
                current.TotalCustomers += Game.Customers.GetNumCustomers();

#if true
                cJobManager.do_whorejobs(current, DayShift.Day);
                cJobManager.do_custjobs(current, DayShift.Day);
                UpdateGirls(current, DayShift.Day);
#else
		UpdateCustomers(current, DayShift); // `J` replaces the UpdateGirls running through customers instead of the girls.
#endif

                #endregion

                #region Night Shift

                // update the girls and satisfy the customers for this brothel during the night
                cJobManager.do_advertising(current, DayShift.Night);
                Game.Customers.GenerateCustomers(current, DayShift.Night);
                current.TotalCustomers += Game.Customers.GetNumCustomers();
#if true
                cJobManager.do_whorejobs(current, DayShift.Night);
                cJobManager.do_custjobs(current, DayShift.Night);
                UpdateGirls(current, DayShift.Night);
#else
		UpdateCustomers(current, 1); // `J` replaces the UpdateGirls running through customers instead of the girls.
#endif

                #endregion

                #region Shift Summary

                // get the misc customers
                current.TotalCustomers += current.MiscCustomers;

                updateBrothel.Clear();
                updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                    "[TotalCustomers]CustomersVisitedTheBuilding",
                    new List<FormatStringParameter>() { new FormatStringParameter("TotalCustomers", current.TotalCustomers) });
                if (current.RejectCustomersRestrict > 0)
                {
                    updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                        "[RejectCustomersRestrict]WereTurnedAwayBecauseOfYourSexRestrictions",
                        new List<FormatStringParameter>() { new FormatStringParameter("RejectCustomersRestrict", current.RejectCustomersRestrict) });
                }
                if (current.RejectCustomersDisease > 0)
                {
                    updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                        "[RejectCustomersDisease]WereTurnedAwayBecauseTheyHadAnSTD",
                        new List<FormatStringParameter>() { new FormatStringParameter("RejectCustomersDisease", current.RejectCustomersDisease) });
                }
                current.m_Events.AddMessage(updateBrothel.ToString(), ImageType.PROFILE, EventType.Brothel);

                // empty rooms cost 2 gold to maintain
                current.m_Finance.BuildingUpkeep(tariff.EmptyRoomCost(current));

                // update brothel stats
                if (current.NumGirls > 0)
                {
                    current.Fame = (TotalFame(current) / current.NumGirls);
                }
                if (current.Happiness > 0 && Game.Customers.GetNumCustomers() > 0)
                {
                    current.Happiness = Math.Min(100, current.Happiness / current.TotalCustomers);
                }


                // advertising costs are set independently for each brothel
                current.m_Finance.AdvertisingCosts(tariff.AdvertisingCosts(current.AdvertisingBudget));

                updateBrothel.Clear();
                updateBrothel.AppendFormat(LocalString.ResourceStringCategory.Brothel,
                    "YourAdvertisingBudgetForThisBrothelIs[AdvertisingBudget]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("AdvertisingBudget", current.AdvertisingBudget) });
                if (tariff.AdvertisingCosts(current.AdvertisingBudget) != current.AdvertisingBudget)
                {
                    updateBrothel.AppendFormat(LocalString.ResourceStringCategory.Brothel,
                        "HoweverDueToYourConfigurationYouInsteadHadToPay[AdvertisingCosts]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("AdvertisingCosts", tariff.AdvertisingCosts(current.AdvertisingBudget)) });
                }
                current.m_Events.AddMessage(updateBrothel.ToString(), ImageType.PROFILE, EventType.Brothel);

                // `J` include antipreg potions in summary
                updateBrothel.Clear();
                if (current.AntiPregPotions > 0 || current.AntiPregUsed > 0)
                {
                    int antiPregNumber = current.AntiPregPotions;
                    int used = current.AntiPregUsed;
                    bool stocked = current.KeepPotionsStocked;
                    bool hasMatron = (GetNumGirlsOnJob(current.Id, matronjob, DayShift.Day) >= 1);
                    bool skip = false; // to allow easy skipping of unneeded lines
                    bool error = false; // in case there is an error this makes for easier debugging

                    // first line: previous stock
                    if (stocked && antiPregNumber > 0)
                    {
                        updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                            "YouKeepARegularStockOf[Number]AntiPregnancyPotionsInThisBrothel",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", antiPregNumber) });
                    }
                    else if (used > 0)
                    {
                        updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                            "YouHadAStockOf[Number]AntiPregnancyPotionsInThisBrothel",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", (antiPregNumber + used)) });
                    }
                    else if (antiPregNumber > 0)
                    {
                        updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                            "YouHaveAStockOf[Number]AntiPregnancyPotionsInThisBrothel",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", antiPregNumber) });
                    }
                    else
                    {
                        skip = true;
                        updateBrothel.AppendLine(LocalString.ResourceStringCategory.Brothel, "YouHaveNoAntiPregnancyPotionsInThisBrothel");
                    }

                    // second line: number used
                    if (skip)
                    {
                        // skip the rest of the lines
                    }
                    else if (used == 0)
                    {
                        skip = true;
                        updateBrothel.AppendLine(LocalString.ResourceStringCategory.Brothel, "NoneWereUsed");
                    }
                    else if (antiPregNumber == 0)
                    {
                        skip = true;
                        updateBrothel.AppendLine(LocalString.ResourceStringCategory.Brothel, "AllHaveBeenUsed");
                    }
                    else if (used > 0 && stocked)
                    {
                        if (used > antiPregNumber)
                        {
                            updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "[Number]WereNeededThisWeek",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", used) });
                        }
                        else
                        {
                            updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "[Number]WereUsedThisWeek",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", used) });
                        }
                    }
                    else if (used > 0 && antiPregNumber > 0 && !stocked)
                    {
                        updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                            "[Number]WereUsedThisWeekLeaving[AntiPregNumber]InStock",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", used), new FormatStringParameter("AntiPregNumber", antiPregNumber) });
                    }
                    else
                    { // `J` put this in just in case I missed something
                        WMLog.Trace(string.Format("Error code::  BAP02|{0}|{2}| :: Please report it to pinkpetal.org so it can be fixed",
                            current.AntiPregPotions, current.AntiPregUsed, current.KeepPotionsStocked), WMLog.TraceLog.ERROR);
                        error = true;
                    }

                    // third line: budget
                    if (!skip && stocked)
                    {
                        int cost = 0;
                        if (used > antiPregNumber)
                        {
                            updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "[Number]MoreThanWereInStockWereNeededSoAnEmergencyRestockHadToBeMade",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", used - antiPregNumber) });
                            updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "NormallyTheyCost[AntiPregPrice]GoldButOurSupplierChargesFiveTimesTheNormalPriceForUnscheduledDeliveries",
                                new List<FormatStringParameter>() { new FormatStringParameter("AntiPregPrice", tariff.AntiPregPrice(1)) });

                            cost += tariff.AntiPregPrice(antiPregNumber);
                            cost += tariff.AntiPregPrice(used - antiPregNumber) * 5;
                        }
                        else
                        {
                            cost += tariff.AntiPregPrice(used);
                        }

                        updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                            "YourBudgetForAntiPregnancyPotionsForThisBrothelIs[Cost]Gold",
                            new List<FormatStringParameter>() { new FormatStringParameter("Cost", cost) });

                        if (hasMatron && used > antiPregNumber)
                        {
                            int newnum = (((used / 10) + 1) * 10) + 10;

                            current.AddAntiPreg(newnum - antiPregNumber);
                            updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "TheMatronOfThisBrothelHasIncreasedTheQuantityOfAntiPregnancyPotionsForFurtherOrdersTo[AntiPregPotions]",
                                new List<FormatStringParameter>() { new FormatStringParameter("AntiPregPotions", current.AntiPregPotions) });
                        }
                    }
                    //if (error)
                    //{
                    //    g_LogFile.write("\n\n" + updateBrothel.str() + "\n\n");
                    //}
                    current.m_Events.AddMessage(updateBrothel.ToString(), ImageType.PROFILE, EventType.Brothel);
                }

                // update the global cash
                Game.Gold.BrothelAccounts(current.m_Finance, current.Id);

                // Check in property
                //if (current.Filthiness < 0)
                //{
                //    current.Filthiness = 0;
                //}
                //if (current.SecurityLevel < 0)
                //{
                //    current.SecurityLevel = 0;
                //}

                #endregion

                #region End of Shift Girl Shutdown

                foreach (sGirl girl in current.GirlsList)
                {
                    Game.Girls.updateTemp(girl); // update temp stuff
                    Game.Girls.EndDayGirls(current, girl);
                }

                #endregion
            }

            // Update the bribe rate
            Game.Gold.Bribes(m_BribeRate);

            for (int i = m_RunawaysGirlList.Count() - 1; i >= 0; i--)
            {
                sGirl runawayGirl = m_RunawaysGirlList[i];
                if (runawayGirl.m_RunAway > 0)
                {
                    // there is a chance the authorities will catch her if she is branded a slave
                    if (runawayGirl.is_slave() && WMRand.Percent(5))
                    {
                        // girl is recaptured and returned to you
                        m_RunawaysGirlList.Remove(runawayGirl);
                        m_Dungeon.AddGirl(runawayGirl, DungeonReasons.GIRLRUNAWAY);
                        Game.MessageQue.Enqueue(LocalString.GetString(LocalString.ResourceStringCategory.Player, "ARunnawaySlaveHasBeenRecapturedByTheAuthoritiesAndReturnedToYou"), MessageCategory.Green);
                        continue;
                    }
                }
                else // add her back to girls available to reacquire
                {
                    runawayGirl.NightJob = runawayGirl.DayJob = Jobs.RESTING;
                    m_RunawaysGirlList.Remove(runawayGirl);
                    Game.Girls.AddGirl(runawayGirl);
                    continue;
                }
            }

            for (int i = m_PrisonGirlList.Count() - 1; i >= 0; i--)
            {
                if (WMRand.Percent(10)) // 10% chance of someone being released
                {
                    sGirl prisonGirl = m_PrisonGirlList[i];
                    m_PrisonGirlList.Remove(prisonGirl);
                    Game.Girls.AddGirl(prisonGirl);
                }
            }

            // keep gravitating player suspicion to 0
            /* */
            if (Game.Player.suspicion() > 0)
            {
                Game.Player.suspicion(-1);
            }
            else if (Game.Player.suspicion() < 0)
            {
                Game.Player.suspicion(1);
            }
            if (Game.Player.suspicion() > 20)
            {
                CheckRaid(); // is the player under suspision by the authorities
            }

            if (m_Bank > 0) // incraese the bank gold by 02%
            {
                // TODO : Gorl interrest may be store inconfiguration file
                int amount = (int)(m_Bank * 0.002f);
                m_Bank += amount;
                /*
                *		bank iterest isn't added to the gold value
                *		but it can be recorded for reporting purposes
                */
                Game.Gold.BankInterest(amount);
            }

            // get money from currently extorted businesses
            updateBrothel.Clear();
            if (GangManager.NumBusinessExtorted > 0)
            {
                if (WMRand.Percent(6.7))
                {
                    sGirl girl = Game.Girls.CreateRandomGirl(17, false);
                    updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                        "AManCannotPaySoHeSellsYouHisDaughter[GirlRealname]ToClearHisDebtToYou",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlRealname", girl.Realname) });
                    LocalString ssg = new LocalString();
                    ssg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlRealname]sFatherCouldNotPayHisDebtToYouSoHeGaveHerToYouAsPayment",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlRealname", girl.Realname) }); ;
                    girl.Events.AddMessage(ssg.ToString(), ImageType.PROFILE, EventType.Dungeon);
                    m_Dungeon.AddGirl(girl, DungeonReasons.NEWGIRL);
                    GangManager.NumBusinessExtorted -= -1;
                }
                int gold = GangManager.NumBusinessExtorted * Constants.INCOME_BUSINESS;
                updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                    "YouGain[Gold]GoldFromThe[NumBusinessExtorted]BusinessesUnderYourControl",
                    new List<FormatStringParameter>() { new FormatStringParameter("Gold", gold), new FormatStringParameter("NumBusinessExtorted", GangManager.NumBusinessExtorted) });
                Game.Gold.Extortion(gold);
                Game.MessageQue.Enqueue(updateBrothel.ToString(), MessageCategory.Green);
            }

            do_tax();
            Game.Rivals.check_rivals();
            updateBrothel.Clear();
            long totalProfit = Game.Gold.TotalProfit();
            if (totalProfit < 0)
            {
                updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                    "YourBrothelHadAnOverallDeficitOf[Deficit]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("Deficit", -totalProfit) });
                Game.MessageQue.Enqueue(updateBrothel.ToString(), MessageCategory.Red);
            }
            else if (totalProfit > 0)
            {
                updateBrothel.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                    "YouMadeAOverallProfitOf[Profit]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("Profit", totalProfit) });
                Game.MessageQue.Enqueue(updateBrothel.ToString(), MessageCategory.Green);
            }
            else
            {
                updateBrothel.AppendLine(LocalString.ResourceStringCategory.Brothel,
                    "YouAreBreakingEvenMadeAsMuchMoneyAsYouSpent");
                Game.MessageQue.Enqueue(updateBrothel.ToString(), MessageCategory.Darkblue);
            }

            // MYR: I'm really curious about what goes in these if statements

            // DustyDan, 04/08/2013:  This is for future to include inside these ifs, 
            // the actions to take when not enough businesses controlled to support the 
            // number of brothels currently owned (according to formula that allowed original purchase).

            // Suggest future something like not allowing any net profit from the brothels 
            // that are unsupported by enough businesses. 
            // Forcing sale of a brothel would be too drastic; maybe allowing sale if player 
            // wants to would be an option to present.

            // `J` added loss of security if not enough businesses held.

            if (GangManager.NumBusinessExtorted < 40 && GetNumBrothels() >= 2)
            {
                Game.Brothels.GetBrothel(1).SecurityLevel -= (40 - GangManager.NumBusinessExtorted) * 2;
            }

            if (GangManager.NumBusinessExtorted < 70 && GetNumBrothels() >= 3)
            {
                Game.Brothels.GetBrothel(2).SecurityLevel -= (70 - GangManager.NumBusinessExtorted) * 2;
            }

            if (GangManager.NumBusinessExtorted < 100 && GetNumBrothels() >= 4)
            {
                Game.Brothels.GetBrothel(3).SecurityLevel -= (100 - GangManager.NumBusinessExtorted) * 2;
            }

            if (GangManager.NumBusinessExtorted < 140 && GetNumBrothels() >= 5)
            {
                Game.Brothels.GetBrothel(4).SecurityLevel -= (140 - GangManager.NumBusinessExtorted) * 2;
            }

            if (GangManager.NumBusinessExtorted < 170 && GetNumBrothels() >= 6)
            {
                Game.Brothels.GetBrothel(5).SecurityLevel -= (170 - GangManager.NumBusinessExtorted) * 2;
            }

            if (GangManager.NumBusinessExtorted < 220 && GetNumBrothels() >= 7)
            {
                Game.Brothels.GetBrothel(6).SecurityLevel -= (220 - GangManager.NumBusinessExtorted) * 2;
            }

        }

        // End of turn stuff is here
        public void UpdateGirls(sBrothel brothel, DayShift dayShift)
        {
            // `J` added to allow for easier copy/paste to other buildings
            Jobs firstjob = Jobs.RESTING;
            Jobs lastjob = Jobs.WHORESTREETS;
            Jobs restjob = Jobs.RESTING;
            Jobs matronjob = Jobs.MATRON;
            bool matron = (GetNumGirlsOnJob(brothel.Id, matronjob, DayShift.Day) >= 1) ? true : false;
            LocalString matronMsg = new LocalString();
            LocalString matronWarningMsg = new LocalString();
            LocalString updateGirlReport = new LocalString();
            LocalString summary = new LocalString();

            sGirl DeadGirl = null;
            string msg;
            string girlName;
            int totalPay = 0;
            int totalTips = 0;
            int totalGold = 0;

            EventType sum = EventType.Summary;
            Jobs sw = Jobs.NotSet;
            int psw = 0;
            bool refused = false;
            m_ProcessingShift = (short)dayShift; // WD:    Set processing flag to shift type


            /*
            *	handle any girls training during this shift
            */
            cJobManager.do_training(brothel, dayShift);
            /*
            *	as for the rest of them...
            */
            foreach (sGirl current in brothel.GirlsList)
            {
                totalPay = totalTips = totalGold = 0;
                refused = false;
                girlName = current.Realname;
                sum = EventType.Summary;

                /*
                *		ONCE DAILY processing
                *		at start of Day Shift
                */
                if (dayShift == DayShift.Day)
                {
                    // Back to work
                    if (current.NightJob == restjob && current.DayJob == restjob && current.PregCooldown < Configuration.Pregnancy.CoolDown &&
                        Game.Girls.GetStat(current, EnumStats.HEALTH) >= 80 && Game.Girls.GetStat(current, EnumStats.TIREDNESS) <= 20)
                    {
                        if ((matron || current.PrevDayJob == matronjob) // do we have a director, or was she the director and made herself rest?
                            && current.PrevDayJob != Jobs.NotSet && current.PrevNightJob != Jobs.NotSet)	// 255 = nothing, in other words no previous job stored
                        {
                            Game.Brothels.m_JobManager.HandleSpecialJobs(brothel.Id, current, current.PrevDayJob, current.DayJob, DayShift.Day);
                            if (current.DayJob == current.PrevDayJob)  // only update night job if day job passed HandleSpecialJobs
                            { current.NightJob = current.PrevNightJob; }
                            else
                            { current.NightJob = restjob; }
                            current.PrevDayJob = current.PrevNightJob = Jobs.NotSet;
                            matronMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                "TheMatronPuts[GirlName]BackToWork",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                            current.Events.AddMessage(matronMsg.ToString(), ImageType.PROFILE, EventType.BackToWork);
                            matronMsg.Clear();
                        }
                        else
                        {
                            matronWarningMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                "WARNING[GirlName]IsDoingNothing",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                            current.Events.AddMessage(matronWarningMsg.ToString(), ImageType.PROFILE, EventType.Warning);
                            matronMsg.Clear();
                        }
                    }
                }		/*
		*		EVERY SHIFT processing
		*/

                // Sanity check! Don't process dead girls and check that m_Next points to something
                if (current.is_dead())
                {
                    continue;
                }

                Game.Girls.UseItems(current); // Girl uses items she has
                Game.Girls.CalculateAskPrice(current, true); // Calculate the girls asking price

                /*
                *		JOB PROCESSING
                */
                sw = (dayShift == DayShift.Day ? current.DayJob : current.NightJob);

                // do their job
                //	if((sw != JOB_ADVERTISING) && (sw != JOB_WHOREGAMBHALL) && (sw != JOB_WHOREBROTHEL) && (sw != JOB_BARWHORE))		// advertising and whoring are handled earlier.
                // Was not testing for some jobs which were already handled, changed to a switch case statement just for ease of reading, and expansion -PP
                // TODO : Jobs delegate must be translated in Job class (like gangmission)
                if (sw == Jobs.ADVERTISING || sw == Jobs.WHOREGAMBHALL || sw == Jobs.WHOREBROTHEL || sw == Jobs.BARWHORE || sw == Jobs.BARMAID || sw == Jobs.WAITRESS || sw == Jobs.SINGER || sw == Jobs.PIANO || sw == Jobs.DEALER || sw == Jobs.ENTERTAINMENT || sw == Jobs.XXXENTERTAINMENT || sw == Jobs.SLEAZYBARMAID || sw == Jobs.SLEAZYWAITRESS || sw == Jobs.BARSTRIPPER || sw == Jobs.MASSEUSE || sw == Jobs.BROTHELSTRIPPER || sw == Jobs.PIANO || sw == Jobs.PEEP)
                {
                    // these jobs are already done so we skip them
                }
                else
                {
                    refused = m_JobManager.JobFunc[(int)sw](current, brothel, dayShift, summary);
                }


                totalPay += current.Pay;
                totalTips += current.Tips;
                totalGold += current.Pay + current.Tips;

                // work out the pay between the house and the girl
                Game.Brothels.CalculatePay(brothel, current, sw);

                brothel.Fame += Game.Girls.GetStat(current, EnumStats.FAME);

                /*
                *		Summary Messages
                */

                updateGirlReport.Clear();
                if (sw == Jobs.RESTING)
                {
                    summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]WasRestingSoMadeNoMoney",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else if (sw == Jobs.MATRON && dayShift == DayShift.Night)
                {
                    summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]ContinuedToHelpTheOtherGirlsThroughoutTheNight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }

                // `J` temporary -1 until I reflow brothel jobs
                else if (sw == Jobs.TRAINING || sw == Jobs.ADVERTISING)
                {
                    sum = EventType.None;
                }
                // WD:	No night shift summary message needed for Torturer job
                else if (sw == Jobs.TORTURER && dayShift == DayShift.Night)
                {
                    sum = EventType.None;
                }

                // `J` if a slave does a job that is normally paid by you but you don't pay your slaves...
                else if ((current.is_slave() && !Configuration.Initial.SlavePayOutOfPocket) && cJobManager.is_job_Paid_Player(sw))
                // `J` until all jobs have this part added to them, use the individual job list instead of this
                //(
                //sw == JOB_BEASTCARER ||
                //sw == JOB_CLEANING
                //)) 
                { summary.AppendLine(LocalString.ResourceStringCategory.Girl, "YouOwnHerAndYouDontPayYourSlaves"); }
                // WD:	Bad girl did not work. Moved from cJobManager::Preprocessing()
                else if (refused)
                {
                    summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]RefusedToWorkSoMadeNoMoney",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else if (totalGold > 0)
                {
                    summary.AppendFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]EarnedATotalOf[TotalGold]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("TotalGold", totalGold) });
                    Jobs job = (dayShift == DayShift.Day ? current.NightJob : current.DayJob);
                    // if it is a player paid job and she is not a slave
                    if ((cJobManager.is_job_Paid_Player(job) && !current.is_slave()) || (cJobManager.is_job_Paid_Player(job) && current.is_slave() && Configuration.Initial.SlavePayOutOfPocket))
                    {
                        // or if it is a player paid job	and she is a slave		but you pay slaves out of pocket.
                        summary.AppendLine(LocalString.ResourceStringCategory.Girl, "DirectlyFromYouSheGetsToKeepItAll");
                    }
                    else if (current.house() <= 0)
                    {
                        summary.AppendLine(LocalString.ResourceStringCategory.Girl, "AndSheGetsToKeepItAll");
                    }
                    else if (totalTips > 0 && ((Configuration.Initial.GirlsKeepTips && !current.is_slave()) || (Configuration.Initial.SlaveKeepTips && current.is_slave())))
                    {
                        int hpay = (int)Math.Round((double)totalPay * (double)(current.m_Stats[(int)EnumStats.HOUSE] * 0.01));
                        int gpay = totalPay - hpay;
                        summary.Dot();
                        summary.NewLine();
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "SheKeepsThe[TotalTips]SheGotInTipsAndHerCut[AmountPercent]OfThePaymentAmountingTo[Gole]Gold",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("TotalTips", totalTips),
                        new FormatStringParameter("AmountPercent", 100 - current.m_Stats[(int)EnumStats.HOUSE]),
                        new FormatStringParameter("Gold", gpay) });
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                            "YouGot[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", hpay),
                        new FormatStringParameter("AmountPercent", current.m_Stats[(int)EnumStats.HOUSE]) });
                    }
                    else
                    {
                        int hpay = (int)Math.Round(((double)totalGold * (double)(current.m_Stats[(int)EnumStats.HOUSE] * 0.01)));
                        int gpay = totalGold - hpay;
                        summary.Dot();
                        summary.NewLine();
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "SheKeeps[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", gpay),
                        new FormatStringParameter("AmountPercent", 100 - current.m_Stats[(int)EnumStats.HOUSE]) });
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                            "YouKeep[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", hpay),
                        new FormatStringParameter("AmountPercent", current.m_Stats[(int)EnumStats.HOUSE]) });
                    }
                }

                else if (totalGold == 0)
                {
                    summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                       "[GirlName]MadeNoMoney",
                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else if (totalGold < 0)
                {
                    summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                       "ERRORSheHasALossOf[TotalGold]GoldGirlName[Realname]Job[JobName]Pay[Pay]Tips[Tips]Total[TotalGold]",
                       new List<FormatStringParameter>() {
           new FormatStringParameter("TotalGold", totalGold),
           new FormatStringParameter("Realname", current.Realname),
           new FormatStringParameter("JobName", m_JobManager.JobName[(dayShift == DayShift.Day ? (int)current.NightJob : (int)current.DayJob)]),
           new FormatStringParameter("Pay", current.Pay),
           new FormatStringParameter("Tips", current.Tips)});
                    summary.AppendLine(LocalString.ResourceStringCategory.Global,
                       "PleaseReportThisToThePinkPetalDevlomentTeamAtHttppinkpetalorg");
                    sum = EventType.Debug;
                }
                if (summary.HasMessage()) // `J` temporary -1 not to show until I reflow brothel jobs
                {
                    current.Events.AddMessage(summary.ToString(), ImageType.PROFILE, sum);
                }

                summary.Clear();


                // Runaway, Depression & Drug checking
                if (runaway_check(brothel, current))
                {
                    Game.Brothels.RemoveGirl(brothel.Id, current, false);

                    current.run_away();
                    continue;
                }

                /*
                *		MATRON CODE START
                */

                // Lets try to compact multiple messages into one.
                matronMsg.Clear();
                matronWarningMsg.Clear();

                matron = (GetNumGirlsOnJob(brothel.Id, Jobs.MATRON, DayShift.Night) >= 1 || GetNumGirlsOnJob(brothel.Id, Jobs.MATRON, DayShift.Day) >= 1);

                if (Game.Girls.GetStat(current, EnumStats.TIREDNESS) > 80)
                {
                    if (matron)
                    {
                        if (current.PrevNightJob == Jobs.NotSet && current.PrevDayJob == Jobs.NotSet)
                        {
                            current.PrevDayJob = current.DayJob;
                            current.PrevNightJob = current.NightJob;
                            current.DayJob = current.NightJob = Jobs.RESTING;
                            matronWarningMsg.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                "YourMatronTakes[GirlName]OffDutyToRestDueToHerTiredness",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        }
                        else
                        {
                            if (WMRand.Percent(70))
                            {
                                matronMsg.AppendLineFormat(LocalString.ResourceStringCategory.Brothel,
                                    "YourMatronHelps[GirlName]ToRelax",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                                Game.Girls.UpdateStat(current, (int)EnumStats.TIREDNESS, -5);
                            }
                        }
                    }
                    else
                    {
                        matronWarningMsg.AppendLine(LocalString.ResourceStringCategory.Girl, "CAUTIONThisGirlDesparatlyNeedRestGiveHerSomeFreeTime");
                    }
                }

                if (Game.Girls.GetStat(current, EnumStats.HAPPINESS) < 40 && matron && WMRand.Percent(70))
                {
                    matronMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "YourMatronHelpsCheerUp[GirlName]AfterSheFeelsSad",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    Game.Girls.UpdateStat(current, (int)EnumStats.HAPPINESS, 5);
                }

                if (Game.Girls.GetStat(current, (int)EnumStats.HEALTH) < 40)
                {
                    if (matron)
                    {
                        if (current.PrevNightJob == Jobs.NotSet && current.PrevDayJob == Jobs.NotSet)
                        {
                            current.PrevDayJob = current.DayJob;
                            current.PrevNightJob = current.NightJob;
                            current.DayJob = current.NightJob = Jobs.RESTING;
                            matronWarningMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                "[GirlName]IsTakenOffDutyByYourMatronToRestDueToHerLowHealth",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        }
                        else
                        {
                            matronMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                "YourMatronHelpsHeal[GirlName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                            Game.Girls.UpdateStat(current, (int)EnumStats.HEALTH, 5);
                        }
                    }
                    else
                    {
                        matronWarningMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "DANGER[GirlName]sHealthIsVeryLow",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        matronWarningMsg.AppendLine(LocalString.ResourceStringCategory.Global, "SheMustRestOrSheWillDie");
                    }
                }


                if (matronMsg.HasMessage())
                {
                    current.Events.AddMessage(matronMsg.ToString(), ImageType.PROFILE, EventType.DayShift);
                    matronMsg.Clear();
                }

                if (matronWarningMsg.HasMessage())
                {
                    current.Events.AddMessage(matronWarningMsg.ToString(), ImageType.PROFILE, EventType.Warning);
                    matronWarningMsg.Clear();
                }
                /*
                *		MATRON CODE END
                */

                // update girl triggers
                current.m_Triggers.ProcessTriggers();



                // Do item check at the end of the day
                if (dayShift == DayShift.Night)
                {
                    // Myr: Automate the use of a number of different items. See the function itself for more comments.
                    //      Enabled or disabled based on config option.
                    if (Configuration.Initial.AutoUseItems)
                    {
                        UsePlayersItems(current);
                    }

                    // update for girls items that are not used up
                    do_daily_items(brothel, current); // `J` added

                    // Natural healing, 2% health and 2% tiredness per day
                    Game.Girls.UpdateStat(current, (int)EnumStats.HEALTH, 2, false);
                    Game.Girls.UpdateStat(current, (int)EnumStats.TIREDNESS, -2, false);
                }

                // Level the girl up if nessessary
                Game.Girls.LevelUp(current);



            }

            //// WD: Finished Processing Shift set flag
            m_ProcessingShift = -1;

        }

        public void UpdateCustomers(sBrothel brothel, DayShift Day0Night1)
        { throw new NotImplementedException(); }

        // MYR: Start of my automation functions
        public void UsePlayersItems(sGirl cur)
        { throw new NotImplementedException(); }
        public bool AutomaticItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool AutomaticSlotlessItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool AutomaticFoodItemUse(sGirl girl, int InvNum, string message)
        { throw new NotImplementedException(); }
        public bool RemoveItemFromInventoryByNumber(int Pos)
        { throw new NotImplementedException(); } // support fn
        // End of automation functions

        public void UpdateAllGirlsStat(sBrothel brothel, EnumStats stat, int amount)
        { throw new NotImplementedException(); }
        public void SetGirlStat(sGirl girl, int stat, int amount)
        { throw new NotImplementedException(); }

        public sGirl GetPrison()
        { return m_PrisonGirlList.FirstOrDefault(); }
        public int stat_lookup(string stat_name, int brothel_id = -1)
        { throw new NotImplementedException(); }

        // Used by new security guard code
        public int GetGirlsCurrentBrothel(sGirl girl)
        { throw new NotImplementedException(); }
        // Also used by new security code
        public List<sGirl> GirlsOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); }
        public sGirl GetRandomGirlOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); } 	// `J` - added
        public sGirl GetFirstGirlOnJob(int BrothelID, int JobID, bool Day0Night1)
        { throw new NotImplementedException(); } 	// `J` - added

        /*	// `J` AntiPreg Potions rewriten and moved to individual buildings
            bool UseAntiPreg(bool use, int brothelID);
            bool UseAntiPreg(bool use);
            void AddAntiPreg(int amount);
            int  GetNumPotions()					{ return m_AntiPregPotions; }
            void KeepPotionsStocked(bool stocked)	{ m_KeepPotionsStocked = stocked; }
            bool GetPotionRestock()					{ return m_KeepPotionsStocked; }
        /* */

        public int GetTotalNumGirls(bool monster = false)
        { throw new NotImplementedException(); }
        public int GetFreeRooms(sBrothel brothel)
        { throw new NotImplementedException(); }
        public int GetFreeRooms(int brothelnum = 0)
        { throw new NotImplementedException(); }

        public void UpgradeSupplySheds()
        { m_SupplyShedLevel++; }
        public int GetSupplyShedLevel()
        { return m_SupplyShedLevel; }

        /// <summary>
        /// Add a girl to brothel.
        /// </summary>
        /// <param name="brothelID">Identifiant of brothel.</param>
        /// <param name="girl">Girlto add.</param>
        public void AddGirl(int brothelID, sGirl girl)
        {
            // TODO : REFACTORING - Add girl is brothel responsability. Manager may check if girl was not associate to another building.
            if (girl == null)
            {
                return;
            }
            /* */
            if (girl.m_InStudio)
            {
                girl.DayJob = girl.NightJob = Jobs.FILMFREETIME;
            }
            else if (girl.m_InArena)
            {
                girl.DayJob = girl.NightJob = Jobs.ARENAREST;
            }
            else if (girl.m_InCentre)
            {
                girl.DayJob = girl.NightJob = Jobs.CENTREREST;
            }
            else if (girl.m_InClinic)
            {
                girl.DayJob = girl.NightJob = Jobs.CLINICREST;
            }
            else if (girl.m_InHouse)
            {
                girl.DayJob = girl.NightJob = Jobs.HOUSEREST;
            }
            else if (girl.m_InFarm)
            {
                girl.DayJob = girl.NightJob = Jobs.FARMREST;
            }
            else
            {
                girl.DayJob = girl.NightJob = Jobs.RESTING;
            }

            sBrothel brothel = null;
            foreach (sBrothel lBrothel in m_BrothelList)
            {
                if (lBrothel.Id == brothelID)
                {
                    brothel = lBrothel;
                    break;
                }
            }
            Game.Girls.RemoveGirl(girl, false);
            girl.where_is_she = brothelID;

            brothel.AddGirl(girl);
        }

        [Obsolete("bool deleteGirl wasn't use no")]
        public void RemoveGirl(int brothelID, sGirl girl, bool deleteGirl)
        {
            this.RemoveGirl(brothelID, girl);
        }

        /// <summary>
        /// Remove a girl of a Brothel.
        /// </summary>
        /// <param name="brothelID">Identifiant of brothel.</param>
        /// <param name="girl">Girl to remove.</param>
        public void RemoveGirl(int brothelID, sGirl girl)
        {
            if (girl == null)
            {
                return;
            }
            sBrothel brothel = null;
            foreach (sBrothel lBrothel in m_BrothelList)
            {
                if (brothel.Id == brothelID)
                {
                    brothel = lBrothel;
                    break;
                }
            }

            brothel.RemoveGirl(girl);
        }
        
        public sGirl GetFirstRunaway()
        { throw new NotImplementedException(); }
        public void sort(sBrothel brothel)
        { throw new NotImplementedException(); } 		// sorts the list of girls
        public void SortInventory()
        { throw new NotImplementedException(); }

        public void SetName(int brothelID, string name)
        { throw new NotImplementedException(); }
        public string GetName(int brothelID)
        { throw new NotImplementedException(); }

        // returns true if the bar is staffed 
        public bool CheckBarStaff(sBrothel brothel, int numGirls)
        { throw new NotImplementedException(); }

        // as above but for gambling hall
        public bool CheckGambStaff(sBrothel brothel, int numGirls)
        { throw new NotImplementedException(); }

        public bool FightsBack(sGirl girl)
        { throw new NotImplementedException(); }
        public int GetNumGirls(int brothelID)
        { throw new NotImplementedException(); }
        public string GetGirlString(int brothelID, int girlNum)
        { throw new NotImplementedException(); }
        public int GetNumGirlsOnJob(int brothelID, Jobs jobID, DayShift dayShift)
        { throw new NotImplementedException(); }

        public string GetBrothelString(int brothelID)
        { throw new NotImplementedException(); }

        public sGirl GetGirl(int brothelID, int num)
        { throw new NotImplementedException(); }
        public int GetGirlPos(int brothelID, sGirl girl)
        { throw new NotImplementedException(); }
        // MYR: Used by new end of turn code in InterfaceProcesses::TurnSummary
        public sGirl GetGirlByName(int brothelID, string name)
        { throw new NotImplementedException(); }

        public sBrothel GetBrothel(int brothelID)
        { throw new NotImplementedException(); }
        public int GetNumBrothels()
        { return m_BrothelList.Count(); }
        public int GetNumBrothelsWithVacancies()
        { throw new NotImplementedException(); }
        public int GetFirstBrothelWithVacancies()
        { throw new NotImplementedException(); }
        public int GetRandomBrothelWithVacancies()
        { throw new NotImplementedException(); }
        public sBrothel GetRandomBrothel()
        { throw new NotImplementedException(); }

        public void CalculatePay(sBrothel brothel, sGirl girl, Jobs Job)
        { throw new NotImplementedException(); }

        // returns true if the girl wins
        public bool PlayerCombat(sGirl girl)
        { throw new NotImplementedException(); }

        [Obsolete("Player instance must be move to be member of game instance", false)]
        public cPlayer GetPlayer()
        { return m_Player; }
        [Obsolete("Dungeon instance must be move to be member of game instance or in Dungeon manager to provide multiple dungeon ingame.", false)]
        public cDungeon GetDungeon()
        { return m_Dungeon; }

        [Obsolete("Inventory move to player so HasItem move to player too!", false)]
        public int HasItem(string name, int countFrom = -1)
        { throw new NotImplementedException(); }

        // Some public members for ease of use
        [Obsolete("Inventory move to player so m_NumInventory move to player too! m_NumInventory field public !!", false)]
        public int m_NumInventory;								// current amount of inventory the brothel has
        [Obsolete("Convert sInventoryItem[] to List<sInventoryItem>, Inventory move to player", false)]
        public sInventoryItem[] m_Inventory = new sInventoryItem[Constants.MAXNUM_INVENTORY];	// List of inventory items they have (3000 max)
        [Obsolete("Convert short[] to List<short>, integrate to inventory item", false)]
        public short[] m_EquipedItems = new short[Constants.MAXNUM_INVENTORY];	// value of > 0 means equipped (wearing) the item
        [Obsolete("Convert int[] to List<int>, integrate to inventory item", false)]
        public int[] m_NumItem = new int[Constants.MAXNUM_INVENTORY];		// the number of items there are stacked
        public cJobManager m_JobManager;						// manages all the jobs

        public int GetNumberOfItemsOfType(int type, bool splitsubtype = false)
        { throw new NotImplementedException(); }



        public long GetBribeRate()
        { return m_BribeRate; }
        public void SetBribeRate(long rate)
        { m_BribeRate = rate; }
        public void UpdateBribeInfluence()
        { throw new NotImplementedException(); }
        public int GetInfluence()
        { return m_Influence; }

        public cRival GetRivals()
        { return m_Rivals.GetRivals(); }
        [Obsolete("RivalManager may move to GameEngine", false)]
        public cRivalManager GetRivalManager()
        { return m_Rivals; }

        public void WithdrawFromBank(long amount)
        { throw new NotImplementedException(); }
        public void DepositInBank(long amount)
        { throw new NotImplementedException(); }
        public long GetBankMoney()
        { return m_Bank; }
        public int GetNumFood()
        { return m_Food; }
        public int GetNumDrinks()
        { return m_Drinks; }
        [Obsolete("Think about moving Beast information out of Brothel Manager", false)]
        public int GetNumBeasts()
        { return m_Beasts; }
        public int GetNumGoods()
        { return m_HandmadeGoods; }
        public int GetNumAlchemy()
        { return m_Alchemy; }
        public void add_to_food(int i)
        { m_Food += i; if (m_Food < 0) m_Food = 0; }
        public void add_to_drinks(int i)
        { m_Drinks += i; if (m_Drinks < 0) m_Drinks = 0; }
        [Obsolete("Think about moving Beast information out of Brothel Manager", false)]
        public void add_to_beasts(int i)
        { m_Beasts += i; if (m_Beasts < 0) m_Beasts = 0; }
        public void add_to_goods(int i)
        { m_HandmadeGoods += i; if (m_HandmadeGoods < 0) m_HandmadeGoods = 0; }
        public void add_to_alchemy(int i)
        { m_Alchemy += i; if (m_Alchemy < 0) m_Alchemy = 0; }

        public bool CheckScripts()
        {
            throw new NotImplementedException();
            //sBrothel current = m_Parent;
            //DirPath @base = DirPath(cfg.folders.characters().c_str()) << "";
            //while (current != null)
            //{
            //    sGirl girl;
            //    for (girl = current.m_Girls; girl != null; girl = girl.m_Next)
            //    {
            //        // if no trigger for this girl, skip to the next one
            //        if (girl.m_Triggers.GetNextQueItem() == null)
            //        {
            //            continue;
            //        }
            //        string fileloc = @base.c_str();
            //        fileloc += girl.m_Name;
            //        girl.m_Triggers.ProcessNextQueItem(fileloc);
            //        return true;
            //    }
            //    current = current.m_Next;
            //}
            //return false;
        }

        public void UpdateObjective()
        { throw new NotImplementedException(); } 				// updates an objective and checks for compleation
        [Obsolete("Game objectives must be move to Game instance or as default to player instance", false)]
        public sObjective GetObjective()
        { throw new NotImplementedException(); } 			// returns the objective
        public void CreateNewObjective()
        { throw new NotImplementedException(); } 			// Creates a new objective
        public void PassObjective()
        { throw new NotImplementedException(); } 				// Gives a reward
        public void AddCustomObjective(int limit, int diff, int objective, int reward, int sofar, int target, string text = "")
        { throw new NotImplementedException(); }

        public IXmlElement SaveDataXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        public bool LoadDataXML(IXmlHandle hBrothelManager)
        { throw new NotImplementedException(); }

        public bool NameExists(string name)
        { throw new NotImplementedException(); }
        public bool SurnameExists(string name)
        { throw new NotImplementedException(); }

        public bool AddItemToInventory(sInventoryItem item)
        { throw new NotImplementedException(); }

        public void check_druggy_girl(LocalString ls)
        { throw new NotImplementedException(); }
        public void CheckRaid()
        {
            cRival rival = null;
            cRivalManager rivalManager = GetRivalManager();
            /*
            *	If the player's influence can shield him
            *	it only follows that the influence of his rivals
            *	can act to stitch him up
            *
            *	see if there exists a rival with infulence
            */
            if (rivalManager.player_safe() == false)
            {
                rival = rivalManager.get_influential_rival();
            }
            /*
            *	chance is based on how much suspicion is leveled at
            *	the player, less his influence at city hall.
            *
            *	And then modified back upwards by rival influence
            */
            int pc = Game.Player.suspicion() - m_Influence;
            if (rival != null)
            {
                pc += rival.m_Influence / 4;
            }
            /*
            *	pc gives us the % chance of a raid
            *	let's do the "not raided" case first
            */
            if (WMRand.Percent(pc) == false)
            {
                /*
                *		you are clearly a model citizen, sir
                *		and are free to go
                */
                return;
            }
            /*
            *	OK, the raid is on. Start formatting a message
            */
            LocalString raidReport = new LocalString();
            raidReport.Append(LocalString.ResourceStringCategory.Player,  "TheLocalAuthoritiesPerformABustOnYourOperations");
            /*
            *	if we make our influence check, the guard captain will be under
            *	orders from the mayor to let you off.
            *
            *	Let's make sure the player can tell
            */
            if (WMRand.Percent(m_Influence))
            {
                raidReport.AppendLine(LocalString.ResourceStringCategory.Player, "TheGuardCaptainLecturesYouOnTheImportanceOfCrimePreventionWhilstAlsoPassingOnTheMayorsHeartfeltBestWishes");
                Game.Player.suspicion(-5);
                Game.MessageQue.Enqueue(raidReport.ToString(), MessageCategory.Green);
                return;
            }
            /*
            *	if we have a rival influencing things, it might not matter
            *	if the player is squeaky clean
            */

            if (rival != null && Game.Player.disposition() > 0 && WMRand.Percent(rival.m_Influence / 2))
            {
                int fine = WMRand.Random(1000) + 150;
                Game.Gold.Fines(fine);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheGuardCaptainCondemnsYourOperationAsAHotbedOfCriminalActivityAndFinesYou[Fine]GoldForLivingWithoutDueCareAndAttention",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine) });
                /*
                *		see if there's a girl using drugs he can nab
                */
                check_druggy_girl(raidReport);
                /*
                *		make sure the player knows why the captain is
                *		being so blatantly unfair
                */
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "OnHisWayOutTheCaptainSmilesAndSaysThatThe[RivalName]SendTheirRegards",
                    new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) }); ;
                Game.MessageQue.Enqueue(raidReport.ToString(), MessageCategory.Red);
                return;
            }
            /*
            *	if the player is basically a goody-goody type
            *	he's unlikely to have anything incriminating on
            *	the premises. 20 disposition should see him
            */
            if (WMRand.Percent(Game.Player.disposition() * 5))
            {
                raidReport.AppendLine(LocalString.ResourceStringCategory.Player, "TheyPronounceYourOperationToBeEntirelyInAccordanceWithTheLaw");
                Game.Player.suspicion(-5);
                Game.MessageQue.Enqueue(raidReport.ToString(), MessageCategory.Green);
                return;
            }
            int nPlayer_Disposition = Game.Player.disposition();
            if (nPlayer_Disposition > -10)
            {
                int fine = WMRand.Random(100) + 20;
                Game.Gold.Fines(fine);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheyFindYouInTechnicalViolationOfSomeHealthAndSafetyOrdinancesAndTheyFineYou[Fine]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine) });
            }
            else if (nPlayer_Disposition > -30)
            {
                int fine = WMRand.Random(300) + 40;
                Game.Gold.Fines(fine);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheyFindSomeMinorCriminalitiesAndFineYou[Fine]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine) });
            }
            else if (nPlayer_Disposition > -50)
            {
                int fine = WMRand.Random(600) + 100;
                Game.Gold.Fines(fine);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheyFindEvidenceOfDodgyDealingsAndFineYou[Fine]Gold",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine) });
            }
            else if (nPlayer_Disposition > -70)
            {
                int fine = WMRand.Random(1000) + 150;
                int bribe = WMRand.Random(300) + 100;
                Game.Gold.Fines(fine + bribe);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheyFindALotOfIllegalActivitiesAndFineYou[Fine]GoldItAlsoCostsYouAnExtra[Bribe]ToPayThemOffFromArrestingYou",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine), new FormatStringParameter("Bribe", bribe) });
            }
            else if (nPlayer_Disposition > -90)
            {
                int fine = WMRand.Random(1500) + 200;
                int bribe = WMRand.Random(600) + 100;
                Game.Gold.Fines(fine + bribe);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheyFindEnoughDirtToPutYouBehindBarsForLifeItCostsYou[Bribe]ToStayOutOfPrisonPlusAnother[Fine]InFinesOnTopOfThat",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine), new FormatStringParameter("Bribe", bribe) });
            }
            else
            {
                int fine = WMRand.Random(2000) + 400;
                int bribe = WMRand.Random(800) + 150;
                Game.Gold.Fines(fine + bribe);
                raidReport.AppendLineFormat(
                    LocalString.ResourceStringCategory.Player,
                    "TheCaptainDeclaresYourPremisesToBeASinkholeOfTheUtmostViceAndDepravityAndItIsOnlyWithDifficultyThatYouDissuadeHimFromSeizingAllYourPropertyOnTheSpotYouPay[Fine]GoldInFinesButOnlyAfterSlippingTheCaptain[Bribe]NotToDragYouOffToPrison",
                    new List<FormatStringParameter>() { new FormatStringParameter("Fine", fine), new FormatStringParameter("Bribe", bribe) });
            }
            /*
            *	check for a drug-using girl they can arrest
            */
            check_druggy_girl(raidReport);
            Game.MessageQue.Enqueue(raidReport.ToString(), MessageCategory.Red);
        }
        public void do_tax()
        { throw new NotImplementedException(); }
        public void do_daily_items(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }
        public void do_food_and_digs(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }
        public string disposition_text()
        { throw new NotImplementedException(); }
        public string fame_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public string suss_text()
        { throw new NotImplementedException(); }
        public string happiness_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public double calc_pilfering(sGirl girl)
        { throw new NotImplementedException(); }

        public bool runaway_check(sBrothel brothel, sGirl girl)
        { throw new NotImplementedException(); }


        // WD: JOB_TORTURER stuff
        public void TortureDone(bool flag)
        { m_TortureDoneFlag = flag; return; }
        public bool TortureDone()
        { return m_TortureDoneFlag; }
        public sGirl WhoHasTorturerJob()
        { throw new NotImplementedException(); }

        // WD: test to check if doing turn processing.  Used to ingnore HOUSE_STAT value in GetRebelValue() if girl gets to keep all her income.
        public bool is_Dayshift_Processing()
        { return m_ProcessingShift == (short)WMaster.Enums.DayShift.Day; }
        public bool is_Nightshift_Processing()
        { return m_ProcessingShift == (short)WMaster.Enums.DayShift.Night; }

        // WD:	Update code of girls stats
        public void updateGirlTurnBrothelStats(sGirl girl)
        { throw new NotImplementedException(); }

        //private:
        /// <summary>
        /// Get total fame of brothel. It's the summe of all brothel's girl fame.
        /// </summary>
        /// <param name="brothel">Brothel to calculate fame.</param>
        /// <returns>Global fame of brothel.</returns>
        private int TotalFame(sBrothel brothel)
        {
            int totalFame = 0;
            // TODO : REFACTORING - sGirl -> List<Girl>. Updatecode.
            foreach (sGirl girl in brothel.GirlsList)
            {
                totalFame += Game.Girls.GetStat(girl, EnumStats.FAME);
            }
            return totalFame;
        }

        private cPlayer m_Player;				// the stats for the player owning these brothels
        private cDungeon m_Dungeon;				// the dungeon

        private List<sBrothel> m_BrothelList = new List<sBrothel>();

        // brothel supplies
        /*	// `J` moved to individual buildings
            bool m_KeepPotionsStocked;
            int  m_AntiPregPotions;			// the number of pregnancy/insimination preventive potions in stock
        */
        private int m_SupplyShedLevel;			// the level of the supply sheds. the higher the level, the more alcohol and antipreg potions can hold

        // brothel resources
        private int m_HandmadeGoods;			// used with the community centre
        [Obsolete("Think about moving Beast information out of Brothel Manager", false)]
        private int m_Beasts;					// used for beastiality scenes
        private int m_Food;						// food produced at the farm
        private int m_Drinks;					// drinks produced at the farm
        private int m_Alchemy;

        // brothel resource Reserves - How much will NOT be sold so it can be used by the Brothels 
        private int m_HandmadeGoodsReserves;
        private int m_BeastsReserves;
        private int m_FoodReserves;
        private int m_DrinksReserves;
        private int m_AlchemyReserves;

        /// <summary>
        /// A list of girls kept in prision.
        /// </summary>
        private List<sGirl> m_PrisonGirlList = new List<sGirl>();
        /// <summary>
        /// Get the list of girls kept in prision.
        /// </summary>
        public IEnumerable<sGirl> PrisonGirlList
        {
            get { return this.m_PrisonGirlList; }
        }
        //private int m_NumPrison;
        //private sGirl m_Prison;				// a list of girls kept in prision
        //private sGirl m_LastPrison;

        /// <summary>
        /// A list of runaways girls.
        /// </summary>
        private List<sGirl> m_RunawaysGirlList = new List<sGirl>();
        /// <summary>
        /// Get the list of runaways girls.
        /// </summary>
        public IEnumerable<sGirl> RunawaysGirlList
        {
            get { return this.m_RunawaysGirlList; }
        }

        //private int m_NumRunaways;          // a list of runaways
        //private sGirl m_Runaways;
        //private sGirl m_LastRunaway;

        private long m_BribeRate;				// the amount of money spent bribing officials per week
        private int m_Influence;				// based on the bribe rate this is the percentage of influence you have
        private int m_Dummy;					//a dummy variable
        private long m_Bank;					// how much is stored in the bank

        private sObjective m_Objective;

        [Obsolete("RivalManager may move to GameEngine", false)]
        public cRivalManager m_Rivals;			// all of the players compedators

        private bool m_TortureDoneFlag;			// WD:	Have we got a torturer working today
        // TODO : REFACTORING - Convert to DayShift enum?
        private short m_ProcessingShift;		// WD:	Store Day0Night1 value when processing girls

        public void AddBrothel(sBrothel brothel)
        {
            // TODO : Check if brothel not already in list.
            if (brothel == null)
            { return; }

            m_BrothelList.Add(brothel);
        }

        [Obsolete("Move treatment into specific building or building manager. Building musn know if isClinic, isArena...")]
        private bool UseAntiPreg(bool use, bool isClinic, bool isStudio, bool isArena, bool isCentre, bool isHouse, bool isFarm, int whereisshe)
        {
            if (!use)
            {
                return false;
            }
            /*
            *	anti-preg potions, we probably should allow
            *	on-the-fly restocks. You can imagine someone
            *	noticing things are running low and
            *	sending a girl running to the shops to get
            *	a restock
            *
            *	that said, there's a good argument here for
            *	making this the matron's job, and giving it a
            *	chance dependent on skill level. Could have a
            *	comedy event where the matron forgets, or the
            *	girl forgets (or disobeys) and half a dozen
            *	girls get knocked up.
            *
            *	'course, we could do that anyway.. :)
            *
            *	`J` adjusted it so it uses your existing stock first
            *	before it buys extras at a higher cost as emergency stock
            *
            */
            Tariff tariff = new Tariff();
            int cost = tariff.AntiPregPrice(1);
            if (isClinic)
            {
                if (Game.Clinic.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.Clinic.GetBrothel(0).AntiPregPotions < Game.Clinic.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Clinic.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Clinic.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.Clinic.GetBrothel(0).AntiPregUsed++;
                    Game.Clinic.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }

            }
            else if (isStudio)
            {
                if (Game.Studios.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.Studios.GetBrothel(0).AntiPregPotions < Game.Studios.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Studios.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Studios.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.Studios.GetBrothel(0).AntiPregUsed++;
                    Game.Studios.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            else if (isArena)
            {
                if (Game.Arena.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.Arena.GetBrothel(0).AntiPregPotions < Game.Arena.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Arena.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Arena.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.Arena.GetBrothel(0).AntiPregUsed++;
                    Game.Arena.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            else if (isCentre)
            {
                if (Game.Centre.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.Centre.GetBrothel(0).AntiPregPotions < Game.Centre.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Centre.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Centre.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.Centre.GetBrothel(0).AntiPregUsed++;
                    Game.Centre.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            else if (isHouse)
            {
                if (Game.House.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.House.GetBrothel(0).AntiPregPotions < Game.House.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.House.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.House.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.House.GetBrothel(0).AntiPregUsed++;
                    Game.House.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            else if (isFarm)
            {
                if (Game.Farm.GetBrothel(0).KeepPotionsStocked)
                {
                    if (Game.Farm.GetBrothel(0).AntiPregPotions < Game.Farm.GetBrothel(0).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Farm.GetBrothel(0).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Farm.GetBrothel(0).AntiPregPotions > 0)
                {
                    Game.Farm.GetBrothel(0).AntiPregUsed++;
                    Game.Farm.GetBrothel(0).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                if (Game.Brothels.GetBrothel(whereisshe).KeepPotionsStocked)
                {
                    if (Game.Brothels.GetBrothel(whereisshe).AntiPregPotions < Game.Brothels.GetBrothel(whereisshe).AntiPregUsed)
                    {
                        cost *= 5;
                    }
                    Game.Gold.ConsumableCost(cost);
                    Game.Brothels.GetBrothel(whereisshe).AntiPregUsed++;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
                if (Game.Brothels.GetBrothel(whereisshe).AntiPregPotions > 0)
                {
                    Game.Brothels.GetBrothel(whereisshe).AntiPregUsed++;
                    Game.Brothels.GetBrothel(whereisshe).AntiPregPotions--;
                    if (WMRand.Percent(Configuration.Pregnancy.AntiPregnancyFailure))
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}