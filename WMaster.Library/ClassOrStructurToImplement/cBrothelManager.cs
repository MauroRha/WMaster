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
    using WMaster.Entity.Living;
    using WMaster.Entity.Living.GangMission;
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
                        UpdateAllGirlsStat(current, EnumStats.PCFear, 2);
                        UpdateAllGirlsStat(current, EnumStats.PCHate, 1);
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
                        Game.Girls.GetStat(current, EnumStats.Health) >= 80 && Game.Girls.GetStat(current, EnumStats.Tiredness) <= 20)
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

                brothel.Fame += Game.Girls.GetStat(current, EnumStats.Fame);

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
                        int hpay = (int)Math.Round((double)totalPay * (double)(current.m_Stats[(int)EnumStats.House] * 0.01));
                        int gpay = totalPay - hpay;
                        summary.Dot();
                        summary.NewLine();
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "SheKeepsThe[TotalTips]SheGotInTipsAndHerCut[AmountPercent]OfThePaymentAmountingTo[Gole]Gold",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("TotalTips", totalTips),
                        new FormatStringParameter("AmountPercent", 100 - current.m_Stats[(int)EnumStats.House]),
                        new FormatStringParameter("Gold", gpay) });
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                            "YouGot[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", hpay),
                        new FormatStringParameter("AmountPercent", current.m_Stats[(int)EnumStats.House]) });
                    }
                    else
                    {
                        int hpay = (int)Math.Round(((double)totalGold * (double)(current.m_Stats[(int)EnumStats.House] * 0.01)));
                        int gpay = totalGold - hpay;
                        summary.Dot();
                        summary.NewLine();
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "SheKeeps[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", gpay),
                        new FormatStringParameter("AmountPercent", 100 - current.m_Stats[(int)EnumStats.House]) });
                        summary.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                            "YouKeep[Gold]Gold[AmountPercent]",
                            new List<FormatStringParameter>() {
                        new FormatStringParameter("Gold", hpay),
                        new FormatStringParameter("AmountPercent", current.m_Stats[(int)EnumStats.House]) });
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

                if (Game.Girls.GetStat(current, EnumStats.Tiredness) > 80)
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
                                Game.Girls.UpdateStat(current, (int)EnumStats.Tiredness, -5);
                            }
                        }
                    }
                    else
                    {
                        matronWarningMsg.AppendLine(LocalString.ResourceStringCategory.Girl, "CAUTIONThisGirlDesparatlyNeedRestGiveHerSomeFreeTime");
                    }
                }

                if (Game.Girls.GetStat(current, EnumStats.Happiness) < 40 && matron && WMRand.Percent(70))
                {
                    matronMsg.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "YourMatronHelpsCheerUp[GirlName]AfterSheFeelsSad",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    Game.Girls.UpdateStat(current, (int)EnumStats.Happiness, 5);
                }

                if (Game.Girls.GetStat(current, (int)EnumStats.Health) < 40)
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
                            Game.Girls.UpdateStat(current, (int)EnumStats.Health, 5);
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
                    Game.Girls.UpdateStat(current, (int)EnumStats.Health, 2, false);
                    Game.Girls.UpdateStat(current, (int)EnumStats.Tiredness, -2, false);
                }

                // Level the girl up if nessessary
                Game.Girls.LevelUp(current);



            }

            //// WD: Finished Processing Shift set flag
            m_ProcessingShift = -1;

        }

        // TODO : UpdateCustomers - Emptyfunction was call ???
        public void UpdateCustomers(sBrothel brothel, DayShift Day0Night1)
        { 
        }

        // MYR: Start of my automation functions
        // TODO : REFACTORING - Game.Girls.HasTrait(cur, "Construct") - If trait is hard coded it must be fixed in structure like derived class, instance of trait class or enum !! If trait is add from imported file, script or external source it will be uniquely informative our use LUA Script to operate.
        // TODO : UsePlayersItems(sGirl cur) - Must change logic to use item caracteristics instead of item name. To have function working with imported items.
        [Obsolete("Must change logic to use item caracteristics instead of item name. To have function working with imported items.", false)]
        public void UsePlayersItems(sGirl cur)
        {
            int has = 0, has2 = 0, Die = 0, PolishCount = 0;

            /* Automatic item use - to stop the monotonous work.
            (I started writing this for my test game where I had 6 brothels with
            125+ girls in each. 16 of them were full time catacombs explorers.)

            Food type items are forced. Actual pieces of equipment are not.
            The players equipment choices should always be respected.

            There are a number of things this function specifically DOES NOT do:
            1. Use skill raising items.
            2. Cure diseases like aids and syphilis.
            3. Cure addictions like shroud and fairy dust.
            4. Use temporary items.
            5. Use items related to pregnancy, insemenation or children

            I should qualify this by saying, "It doesn't directly raise stats, cure
            diseases and addictions." They can happen indirectly as a piece of equipment
            equipped for a stat boost or trait may also raise skills. Similarily a
            item used to cure some condition (like an Elixir of Ultimate Regeneration
            curing one-eye or scars)  may also cure a disease or addiction as well.

            The way this is currently written it shouldn't be released as part
            of the game. It makes too many choices for the player. Perhaps we can
            make it into a useful game function somehow. Regardless, this can be
            disabled by commenting out a single line in UpdateGirls.
            */

            // ------------ Part 1: Stats -------------

            #region automation_stats
            // Health

            // Healing items are wasted on constructs as the max. 4% applies to both damage and
            // healing
            // TODO : WARNING !!! String use as identifiant (item name). Not secure thing. Try to change!!!
            has = Game.Brothels.HasItem("Healing Salve (L)");
            // TODO : WARNING !!! String use as identifiant (girl trait). Not secure thing. Try to change!!!
            if (Game.Girls.GetStat(cur, EnumStats.Health) <= 25 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedALargeHealingSalveToStayHealthy"));
            }

            has = Game.Brothels.HasItem("Healing Salve (M)");
            if (Game.Girls.GetStat(cur, EnumStats.Health) <= 50 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAMediumHealingSalveToStayHealthy"));
            }

            has = Game.Brothels.HasItem("Healing Salve (S)");
            if (Game.Girls.GetStat(cur, EnumStats.Health) <= 75 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedASmallHealingSalveToStayHealthy"));
            }

            // Tiredness/fatigue
            has = Game.Brothels.HasItem("Incense of Serenity (L)");
            if (Game.Girls.GetStat(cur, EnumStats.Tiredness) >= 75 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedALargeIncenseOfSerenityToStayAwake"));
            }

            has = Game.Brothels.HasItem("Incense of Serenity (M)");
            if (Game.Girls.GetStat(cur, EnumStats.Tiredness) >= 50 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAMediumIncenseOfSerenityToStayAwake"));
            }

            has = Game.Brothels.HasItem("Incense of Serenity (S)");
            if (Game.Girls.GetStat(cur, EnumStats.Tiredness) >= 25 && !Game.Girls.HasTrait(cur, "Construct") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedASmallIncenseOfSerenityToStayAwake"));
            }

            // Mana

            // Set threshold at 20 as that is what is required to charm a customer to sleep with a girl
            has = Game.Brothels.HasItem("Mana Crystal");
            if (Game.Girls.GetStat(cur, EnumStats.Mana) < 20 && has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAManaCrystalToRestore25Mana"));
                }
            }
            has = Game.Brothels.HasItem("Eldritch Cookie");
            if (Game.Girls.GetStat(cur, EnumStats.Mana) < 20 && has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAnEldritchCookieToRestore30Mana"));
                }
            }
            has = Game.Brothels.HasItem("Mana Potion");
            if (Game.Girls.GetStat(cur, EnumStats.Mana) < 20 && has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAManaPotionToRestore100Mana"));
                }
            }

            // Libido - ordered big to small

            // Succubus Milk [100 pts]
            has = Game.Brothels.HasItem("Succubus Milk");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) < 5 && has != -1) // Lower threshold
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedSuccubusMilkToRestore100Libido"));
            }

            // Sinspice [75 pts]
            has = Game.Brothels.HasItem("Sinspice");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) < 10 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedSinspiceToRestore75Libido"));
            }

            //Empress' New Clothes [50 pts] (Piece of equipment)  (This is a tossup between charisma & libido)
            has = Game.Brothels.HasItem("Empress' New Clothes");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnTheEmpressNewClothesToGetHerLibidoUp"));
            }

            // Red Rose Extravaganza [50 pts?]
            has = Game.Brothels.HasItem("Red Rose Extravaganza");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) < 10 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "GaveHerARedRoseExtravaganzaToGetHerLibidoGoingAgain"));
            }

            // Ring of the Horndog [50 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Ring of the Horndog");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && Game.Girls.HasItem(cur, "Minor Ring of the Horndog") == -1 && Game.Girls.HasItem(cur, "Ring of the Horndog") == -1 && Game.Girls.HasItem(cur, "Organic Lingerie") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerEquipARingOfTheHorndogToBetterServeHerCustomersLibidoUp"));
            }

            // Gemstone Dress [42 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Gemstone Dress");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnAGemstoneDressForThatMilliondollarFeelingLibidoUp"));
            }

            // Silken Dress [34 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Silken Dress");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnASilkenDressToBetterSlideWithHerCustomersLibidoUp"));
            }

            // Minor Ring of the Horndog [30 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Minor Ring of the Horndog");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && Game.Girls.HasItem(cur, "Minor Ring of the Horndog") == -1 && Game.Girls.HasItem(cur, "Ring of the Horndog") == -1 && Game.Girls.HasItem(cur, "Organic Lingerie") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SheWasLookinALittleListlessSoYouHadHerEquipAMinorRingOfTheHorndogLibidoUp"));
            }

            // Velvet Dress [34 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Velvet Dress");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnAVelvetDressToFeelEvenMoreSexyLibidoUp"));
            }

            // Designer Lingerie [20 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Designer Lingerie");
            if (Game.Girls.GetStat(cur, EnumStats.Libido) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnDesignerLingerieToFeelMoreAtHomeLibidoUp"));
            }

            // Charisma 

            //Ring of Charisma [50 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Ring of Charisma");
            if (Game.Girls.GetStat(cur, EnumStats.Charisma) <= 50 && Game.Girls.HasItem(cur, "Ring of Charisma") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnARingOfCharismaToOvercomeHerSpeakingDifficulties"));
            }

            // Minor Ring of Charisma [30 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Minor Ring of Charisma");
            if (Game.Girls.GetStat(cur, EnumStats.Charisma) <= 70 && Game.Girls.HasItem(cur, "Minor Ring of Charisma") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "PutOnAMinorRingOfCharisma"));
            }

            // Beauty

            // Ring of Beauty [50 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Ring of Beauty");
            if (Game.Girls.GetStat(cur, EnumStats.Beauty) <= 50 && Game.Girls.HasItem(cur, "Ring of Beauty") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnARingOfBeautyToOvercomeHerUglystickDisadvantage"));
            }

            // Minor Ring of Beauty [30 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Minor Ring of Beauty");
            if (Game.Girls.GetStat(cur, EnumStats.Beauty) <= 70 && Game.Girls.HasItem(cur, "Minor Ring of Beauty") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnAMinorRingOfBeautyToCoverSomeFlaws"));
            }

            // Rainbow Ring [15 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Rainbow Ring");
            if (Game.Girls.GetStat(cur, EnumStats.Beauty) <= 85 && Game.Girls.HasItem(cur, "Rainbow Ring") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnARainbowRingToMatchHerRainbowPersonality"));
            }

            // Happiness - ordered from big values to small

            // Heaven-and-Earth Cake [100 pts]
            has = Game.Brothels.HasItem("Heaven-and-Earth Cake");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 10 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadAHeavenandearthCakeToStaveOffSeriousDepression"));
            }

            // Eldritch cookie [70 pts]
            has = Game.Brothels.HasItem("Eldritch Cookie");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 30 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadAnEldritchCookieToImproveHerMood"));
            }

            // Expensive Chocolates [50 pts]
            has = Game.Brothels.HasItem("Expensive Chocolates");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 50 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadSomeExpensiveChocolatesToImproveHerMood"));
            }

            // Apple Tart [30 pts]
            has = Game.Brothels.HasItem("Apple Tart");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 70 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadAnAppleTartToImproveHerMood"));
            }

            // Honeypuff Scones [30 pts]
            has = Game.Brothels.HasItem("Honeypuff Scones");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 70 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadAHoneypuffSconeForLunch"));
            }

            // Fancy breath mints [10 pts]
            has = Game.Brothels.HasItem("Fancy Breath Mints");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 90 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadFancyBreathMintsWhyNotTheyWereLyingAround"));
            }

            // Exotic Bouquet [10 pts]
            has = Game.Brothels.HasItem("Exotic Bouquet");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 90 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouGaveHerAnExoticBouquetForWorkWellDone"));
            }

            // Wild Flowers [5 pts]
            has = Game.Brothels.HasItem("Wild Flowers");
            if (Game.Girls.GetStat(cur, EnumStats.Happiness) <= 95 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouGaveHerSomeWildFlowers"));
            }

            // Age

            //Do this before boobs b/c lolly wand makes them small
            // My arbitrary rule is, once they hit 30, make 'em young again.

            // To prevent using an elixir, then a wand, set an arbitrary upper age limit of 35 for elixirs
            has = Game.Brothels.HasItem("Elixir of Youth");
            if ((Game.Girls.GetStat(cur, EnumStats.Age) >= 30) && (Game.Girls.GetStat(cur, EnumStats.Age) <= 35) && (has != -1))
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAElixirOfYouthToRemoveTenYearsOfAge"));
            }

            has = Game.Brothels.HasItem("Lolita Wand");
            if (Game.Girls.GetStat(cur, EnumStats.Age) >= 30 && Game.Girls.GetStat(cur, EnumStats.Age) <= 80 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedALolitaWandToBecomeSeventeenAgain"));
            }

            // XP: Nuts & tomes & mangos of knowledge, etc...

            // `J` xp can now be above 255 so removing restriction
            has = Game.Brothels.HasItem("Nut of Knowledge");
            if (has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedASmallNutOfKnowledge"));
                }
            }
            has = Game.Brothels.HasItem("Mango of Knowledge");
            if (has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SheAteAMangoOfKnowledge"));
                }
            }
            has = Game.Brothels.HasItem("Watermelon of Knowledge");
            if (has != -1)
            {
                if (WMRand.Percent(5))
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SheHadAWatermelonOfKnowledgeForLunch"));
                }
            }

            // Constitution (Items in reverse order. That is, the items offering the largest increases are first)

            // Ring of the Schwarzenegger [50 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Ring of the Schwarzenegger");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 50 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnARingOfTheSchwarzeneggerForTheConstitutionBoost"));
            }

            // Bracer of Toughness [40 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Bracer of Toughness");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 60 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnABracerOfToughnessForTheConstitutionBoost"));
            }

            // Minor Ring of the Schwarzenegger [30 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Minor Ring of the Schwarzenegger");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 70 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnAMinorRingOfTheSchwarzeneggerForTheConstitutionBoost"));
            }

            // Necklace of Pain Reversal [25 pts net: +40 for masochist -15 on necklace] (Piece of equipment)
            has = Game.Brothels.HasItem("Necklace of Pain Reversal");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 75 && !Game.Girls.HasTrait(cur, "Masochist") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnANecklaceOfPainReversalForTheConstitutionBoost"));
            }

            // Tiger Leotard [20 pts] (Piece of equipment)
            has = Game.Brothels.HasItem("Tiger Leotard");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 80 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ShePutOnATigerLeotardToFeelItsStrengthAndPower"));
            }

            // Manual of health [10 pts] (Piece of equipment, but slotless)
            // Lets be reasonable and only allow only one of each slotless item to be given to a girl.
            // (Having 8 stripper poles in a girl's inventory looks silly IMO.)
            has = Game.Brothels.HasItem("Manual of Health");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 90 && Game.Girls.GetStat(cur, EnumStats.Strength) <= 90 && Game.Girls.HasItem(cur, "Manual of Health") == -1 && has != -1)
            {
                AutomaticSlotlessItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouGaveHerAManualOfHealthToRead"));
            }

            // Free Weights [10 pts] (Piece of equipment, but slotless)
            has = Game.Brothels.HasItem("Free Weights");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 90 && Game.Girls.GetStat(cur, EnumStats.Strength) <= 90 && Game.Girls.HasItem(cur, "Free Weights") == -1 && has != -1)
            {
                AutomaticSlotlessItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouGaveHerFreeWeightsToWorkWith"));
            }

            // Stripper Pole [5 pts] (Piece of equipment, but slotless)
            has = Game.Brothels.HasItem("Stripper Pole");
            if (Game.Girls.GetStat(cur, EnumStats.Constitution) <= 95 && Game.Girls.GetStat(cur, EnumStats.Strength) <= 95 && Game.Girls.HasItem(cur, "Stripper Pole") == -1 && has != -1)
            {
                AutomaticSlotlessItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouGaveHerAStripperPoleToPracticeWith"));
            }

            // Obedience

            // Necklace of Control (piece of equipment)
            has = Game.Brothels.HasItem("Necklace of Control");
            if (Game.Girls.GetStat(cur, EnumStats.Obedience) <= 10 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HerObedienceIsAProblemSoYouHadHerPutOnANecklaceOfControl"));
            }

            has = Game.Brothels.HasItem("Disguised Slave Band");
            if (Game.Girls.GetStat(cur, EnumStats.Obedience) <= 50 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnADisguisedSlaveBandClaimingItWasSomethingElse"));
            }

            has = Game.Brothels.HasItem("Slave Band");
            if (Game.Girls.GetStat(cur, EnumStats.Obedience) <= 50 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouDealthWithHerObedienceProblemsByForcingHerToWearASlaveBand"));
            }

            has = Game.Brothels.HasItem("Willbreaker Spice");
            if (Game.Girls.GetStat(cur, EnumStats.Obedience) <= 90 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouSlippedSomeWillbreakerSpiceInToHerFood"));
            }
            #endregion

            // ---------- Part 2: Traits ----------------

            #region automation_traits
            // Perfection. This is an uber-valuable I put in. Ideally it should be Catacombs01, not Catacombs15.
            // It changes so many traits that it's hard to decide on a rule. In the end I kept it simple.
            // (Players will justifiably hate me if I made this decision for them.)
            // Do this first as it covers/replaces 90% of what follows
            has = Game.Brothels.HasItem("Perfection");
            if (cur.m_NumTraits <= 8 && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedPerfectionToBecomeANearPerfectBeing"));
            }

            // Tough

            // Aoshima beef
            has = Game.Brothels.HasItem("Aoshima BEEF!!");
            if (!Game.Girls.HasTrait(cur, "Tough") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "BulkedUpOnAoshimaBeefToGetTheToughTrait"));
            }

            // Oiran Dress (Piece of equipment)
            has = Game.Brothels.HasItem("Oiran Dress");
            if (!Game.Girls.HasTrait(cur, "Tough") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "Put on an Oiran Dress."));
            }

            // Nymphomaniac

            // Do this before quick learner b/c taking the shroud cola gives the girl the slow learner trait
            has = Game.Brothels.HasItem("Shroud Cola");
            has2 = Game.Brothels.HasItem("Cure for Shroud Addiction");
            if (!Game.Girls.HasTrait(cur, "Nymphomaniac") && (has != -1 && has2 != -1))
            {
                // If one succeeds, the other should too
                // Note the order is important here: Shroud cola has to be first
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerDownAShroundColaForTheNymphomaniacSideeffectUnfortunatelySheAlsoGainsTheSlowlearnerTrait"));
                AutomaticFoodItemUse(cur, has2, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerTakeTheShroudAddictionCure"));
            }

            // Quick learner

            // Scroll of transcendance
            has = Game.Brothels.HasItem("Scrolls of Transcendance");
            if (!Game.Girls.HasTrait(cur, "Quick Learner") && !Game.Girls.HasTrait(cur, "Optimist") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ReadAScrollOfTranscendenceToGainTheQuickLearnerAndOptimistTraits"));
            }

            // Book of enlightenment
            has = Game.Brothels.HasItem("Book of Enlightenment");
            if (!Game.Girls.HasTrait(cur, "Quick Learner") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ReadABookOfEnlightenmentForTheQuickLearnerTrait"));
            }

            // Ring of Enlightenment
            has = Game.Brothels.HasItem("Ring of Enlightenment");
            if (!Game.Girls.HasTrait(cur, "Quick Learner") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "PutOnARingOfEnlightenmentForTheQuickLearnerTrait"));
            }

            // Amulet of the Cunning Linguist
            has = Game.Brothels.HasItem("Amulet of the Cunning Linguist");
            if (!Game.Girls.HasTrait(cur, "Quick Learner") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "PutOnAnAmuletOfTheCunningLinguistForTheQuickLearnerTrait"));
            }

            // Optimist: Good fortune, leprechaun biscuit, chatty flowers, etc...

            // Good Fortune
            has = Game.Brothels.HasItem("Good Fortune");
            if (!Game.Girls.HasTrait(cur, "Optimist") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "ReadAGoodFortuneAndFeelsMoreOptimisticForIt"));
            }

            // Leprechaun Biscuit
            has = Game.Brothels.HasItem("Leprechaun Biscuit");
            if (!Game.Girls.HasTrait(cur, "Optimist") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HadALeprechaunBiscuitAndFeelsMoreOptimisticForIt"));
            }

            // Chatty Flowers
            has = Game.Brothels.HasItem("Chatty Flowers");
            if (!Game.Girls.HasTrait(cur, "Optimist") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "TalkedWithTheChattyFlowersAndFeelsMoreOptimisticForIt"));
            }

            // Glass shoes (piece of equipment)
            has = Game.Brothels.HasItem("Glass Shoes");
            if (!Game.Girls.HasTrait(cur, "Optimist") && Game.Girls.HasItem(cur, "Sandals of Mercury") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SlippedOnGlassShoesForTheOptimistTrait"));
            }

            // Elegant (Obsidian Choker, piece of equipment)

            has = Game.Brothels.HasItem("Obsidian Choker");
            if (!Game.Girls.HasTrait(cur, "Elegant") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "PutOnAnObsidianChokerForTheElegantTrait"));
            }

            // Fleet of foot (Sandals of Mercury, piece of equipment)

            has = Game.Brothels.HasItem("Sandals of Mercury");
            if (!Game.Girls.HasTrait(cur, "Fleet of Foot") && Game.Girls.HasItem(cur, "Glass Shoes") == -1 && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "PutOnSandalsOfMercuryForTheFleetOfFootTrait"));
            }

            // Fast Orgasms & Nymphomaniac (Organic Lingerie, piece of equipment)

            has = Game.Brothels.HasItem("Organic Lingerie");
            if (!Game.Girls.HasTrait(cur, "Fast orgasms") && !Game.Girls.HasTrait(cur, "Fast Orgasms") && !Game.Girls.HasTrait(cur, "Nymphomaniac") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerWearOrganicLingerie"));
            }

            // Fast Orgasms (Ring of Pleasure, piece of equipment)

            has = Game.Brothels.HasItem("Ring of Pleasure");
            if (!Game.Girls.HasTrait(cur, "Fast orgasms") && !Game.Girls.HasTrait(cur, "Fast Orgasms") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnARingOfPleasureForTheFastOrgasmsTrait"));
            }

            // Lets try and cure mind fucked & retarted
            // The amulet of the sex elemental gives you the mind fucked trait. It can be "cured" until the amulet is taken off and put on again.
            // Regardless, we'll not try to cure the amulet case.
            has = Game.Brothels.HasItem("Refined Mandragora Extract");
            if (((Game.Girls.HasTrait(cur, "Mind Fucked") && Game.Girls.HasItem(cur, "Amulet of the Sex Elemental") == -1) || Game.Girls.HasTrait(cur, "Retarded")) && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerUseRefinedMandragoraExtractToRemoveMentalDamage"));
            }

            // Malformed

            //	has = g_Brothels.HasItem("Elixir of Ultimate Regeneration");
            //	if (g_Girls.HasTrait(cur, "Malformed") && has != -1)
            //		AutomaticFoodItemUse(cur, has, gettext("Used an elixir of ultimate regeneration to cure her malformities."));

            // Tsundere & yandere

            has = Game.Brothels.HasItem("Attitude Reajustor");
            if ((Game.Girls.HasTrait(cur, "Yandere") || Game.Girls.HasTrait(cur, "Tsundere")) && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerTakeAnAttitudeReajustorPill"));
            }

            // Eyes

            has = Game.Brothels.HasItem("Eye Replacement Candy");
            if ((Game.Girls.HasTrait(cur, "One Eye") || Game.Girls.HasTrait(cur, "Eye Patch")) && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAnEyeReplacementCandyToRestoreHerEye"));
            }

            // Last ditch eye check.  Use the big guns if you don't have anything else.
            //	has = g_Brothels.HasItem("Elixir of Ultimate Regeneration");
            //	if ((g_Girls.HasTrait(cur, "One Eye") || g_Girls.HasTrait(cur, "Eye Patch")) && has != -1)
            //		AutomaticFoodItemUse(cur, has, gettext("Used an elixir of ultimate regeneration to restore her eye."));

            // Scars - start with the least powerful cures and work up
            has = Game.Brothels.HasItem("Oil of Lesser Scar Removing");
            if ((Game.Girls.HasTrait(cur, "Small Scars") || Game.Girls.HasTrait(cur, "Cool Scars")) && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAnOilOfLesserScarRemovalToRemoveWorkrelatedDamage"));
            }

            has = Game.Brothels.HasItem("Oil of Greater Scar Removing");
            if ((Game.Girls.HasTrait(cur, "Small Scars") || Game.Girls.HasTrait(cur, "Cool Scars") || Game.Girls.HasTrait(cur, "Horrific Scars")) && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedAnOilOfGreaterScarRemovalToRemoveHerScars"));
            }

            //	has = g_Brothels.HasItem("Elixir of Ultimate Regeneration");
            //	if ((g_Girls.HasTrait(cur, "Small Scars") || g_Girls.HasTrait(cur, "Cool Scars") || g_Girls.HasTrait(cur, "Horrific Scars")) && has != -1)
            //		AutomaticFoodItemUse(cur, has, gettext("Used an elixir of ultimate regeneration to remove her scars."));

            // Big boobs

            has = Game.Brothels.HasItem("Oil of Extreme Breast Growth");
            if (!Game.Girls.HasTrait(cur, "Big Boobs") && !Game.Girls.HasTrait(cur, "Abnormally Large Boobs") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SheUsesAnOilOfExtremeBreastGrowthToGainTheAbnormallyLargeBoobsTrait"));
            }

            has = Game.Brothels.HasItem("Oil of Greater Breast Growth");
            if (!Game.Girls.HasTrait(cur, "Big Boobs") && !Game.Girls.HasTrait(cur, "Abnormally Large Boobs") && has != -1)
            {
                AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "SheUsesAnOilOfGreaterBreastGrowthToGainTheBigBoobsTrait"));
            }

            // Nipple Rings of Pillowy Softness (piece of [ring slot] equipment)
            has = Game.Brothels.HasItem("Nipple Rings of Pillowy Softness");
            if (!Game.Girls.HasTrait(cur, "Big Boobs") && !Game.Girls.HasTrait(cur, "Abnormally Large Boobs") && has != -1)
            {
                AutomaticSlotlessItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnANippleRingsOfPillowySoftness"));
            }

            // Nipple Rings of Breast Expansion, (piece of [ring slot] equipment)
            has = Game.Brothels.HasItem("Nipple Rings of Breast Expansion");
            if (!Game.Girls.HasTrait(cur, "Big Boobs") && !Game.Girls.HasTrait(cur, "Abnormally Large Boobs") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHadHerPutOnNippleRingsOfBreastExpansionForTheBigBoobsTrait"));
            }

            // Polish
            has = Game.Brothels.HasItem("Polish");
            if (has != -1)
            {
                // If the girl doesn't have 4 of these 5 traits she will use polish
                if (!Game.Girls.HasTrait(cur, "Good Kisser"))
                {
                    PolishCount++;
                }
                if (!Game.Girls.HasTrait(cur, "Great Figure"))
                {
                    PolishCount++;
                }
                if (!Game.Girls.HasTrait(cur, "Great Arse"))
                {
                    PolishCount++;
                }
                if (!Game.Girls.HasTrait(cur, "Long Legs"))
                {
                    PolishCount++;
                }
                if (!Game.Girls.HasTrait(cur, "Puffy Nipples"))
                {
                    PolishCount++;
                }

                if (PolishCount >= 4)
                {
                    AutomaticFoodItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "UsedPolishToMakeHerselfMoreAttractiveToClients"));
                }
            }

            // Masochist

            // Put this at the bottom as there are better neck slot items that could be equipped above
            // Unlike the case of raising the constitution score in part one, we're only concerned with the trait here
            has = Game.Brothels.HasItem("Necklace of Pain Reversal");
            if (!Game.Girls.HasTrait(cur, "Masochist") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouHaveThisThingForMasochismSoYouHadHerPutOnANecklaceOfPainReversal"));
            }

            // Iron Will

            // Disguised Slave band (piece of equipment)
            // (Statuses like 'controlled' on the Disguised Slave Band (amongst others) don't appear to do anything.)
            has = Game.Brothels.HasItem("Disguised Slave Band");
            if (Game.Girls.HasTrait(cur, "Iron Will") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HerIronWillIsAProblemSoYouHadHerPutOnADisguisedSlaveBandClaimingItWasSomethingElse"));
            }

            has = Game.Brothels.HasItem("Slave Band");
            if (Game.Girls.HasTrait(cur, "Iron Will") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "YouDealthWithHerIronWillByForcingHerToWearASlaveBand"));
            }

            // Necklace of Control (piece of equipment)
            has = Game.Brothels.HasItem("Necklace of Control");
            if (Game.Girls.HasTrait(cur, "Iron Will") && has != -1)
            {
                AutomaticItemUse(cur, has, LocalString.GetString(LocalString.ResourceStringCategory.Items, "HerIronWillIsAProblemSoYouHadHerPutOnANecklaceOfControl"));
            }
            #endregion
        }

        public bool AutomaticItemUse(sGirl girl, int InvNum, string message)
        {
            int EquipSlot = -1;

            EquipSlot = Game.Girls.AddInv(girl, m_Inventory[InvNum]);
            if (EquipSlot != -1)
            {
                if (Game.Inventory.equip_singleton_ok(girl, EquipSlot, false)) // Don't force equipment
                {
                    RemoveItemFromInventoryByNumber(InvNum); // Remove from general inventory
                    Game.Inventory.Equip(girl, EquipSlot, false);
                    girl.Events.AddMessage(message, ImageType.PROFILE, EventType.Warning);
                    return true;
                }
                else
                {
                    Game.Girls.RemoveInvByNumber(girl, EquipSlot); // Remove it from the girl's inventory if they can't equip
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AutomaticSlotlessItemUse(sGirl girl, int InvNum, string message)
        {
            // Slotless items include manuals, stripper poles, free weights, etc...
            int equipSlot = -1;

            equipSlot = Game.Girls.AddInv(girl, m_Inventory[InvNum]);
            if (equipSlot != -1)
            {
                RemoveItemFromInventoryByNumber(InvNum); // Remove from general inventory
                Game.Inventory.Equip(girl, equipSlot, false);
                girl.Events.AddMessage(message, ImageType.DEATH, EventType.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AutomaticFoodItemUse(sGirl girl, int InvNum, string message)
        {
            int EquipSlot = -1;

            EquipSlot = Game.Girls.AddInv(girl, m_Inventory[InvNum]);
            if (EquipSlot != -1)
            {
                RemoveItemFromInventoryByNumber(InvNum);
                Game.Inventory.Equip(girl, EquipSlot, true);
                girl.Events.AddMessage(message, ImageType.DEATH, EventType.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveItemFromInventoryByNumber(int Pos)
        {
            bool removed = false;

            if (Game.Brothels.m_Inventory[Pos] != null)
            {
                if (Game.Brothels.m_NumItem[Pos] > 0)
                {
                    removed = true;
                    Game.Brothels.m_NumItem[Pos]--;

                    // We may reduce the stack size to zero
                    if (Game.Brothels.m_NumItem[Pos] == 0)
                    {
                        Game.Brothels.m_Inventory[Pos] = null;
                        Game.Brothels.m_EquipedItems[Pos] = 0;
                        Game.Brothels.m_NumInventory--;
                    }
                } // if num > 0
            } // Inventory type not null

            SortInventory();
            return removed;
        }
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
        // ----- Inventory
        public void SortInventory()
        {
            //	qu_sort(0,299,m_Inventory);
        }

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
        {
            // no pay or tips, no need to continue
            if (girl.Pay <= 0 && girl.Tips <= 0)
            {
                girl.Pay = girl.Tips = 0;
                return;
            }

            if (girl.Tips > 0) // `J` check tips first
            {
                if ((Configuration.Initial.GirlsKeepTips && !girl.is_slave()) || (Configuration.Initial.SlaveKeepTips && girl.is_slave())) // if slaves tips are counted sepreatly from pay -  if free girls tips are counted sepreatly from pay
                {
                    girl.Money += girl.Tips; // give her the tips directly
                }
                else // otherwise add tips into pay
                {
                    girl.Pay += girl.Tips;
                }
            }
            girl.Tips = 0;
            // no pay, no need to continue
            if (girl.Pay <= 0)
            {
                girl.Pay = 0;
                return;
            }

            // if the house takes nothing		or if it is a player paid job and she is not a slave
            if (girl.m_Stats[(int)EnumStats.House] == 0 || (cJobManager.is_job_Paid_Player(Job) && !girl.is_slave()) || (cJobManager.is_job_Paid_Player(Job) && girl.is_slave() && Configuration.Initial.SlavePayOutOfPocket))
            {
                // or if it is a player paid job	and she is a slave		but you pay slaves out of pocket.
                girl.Money += girl.Pay; // she gets it all
                girl.Pay = 0;
                return;
            }

            // so now we are to the house percent.
            float houseFactor = (float)girl.m_Stats[(int)EnumStats.House] / 100.0f;

            // work out how much gold (if any) she steals
            double stealFactor = calc_pilfering(girl);
            int stolen = (int)(stealFactor * girl.Pay);
            girl.Pay -= stolen;
            girl.Money += stolen;


            int house = (int)(houseFactor * girl.Pay); // the house takes its cut of whatever's left
            if (house > girl.Pay)
            {
                house = girl.Pay; // this shouldn't happen. That said...
            }

            girl.Money += girl.Pay - house; // The girl collects her part of the pay
            brothel.m_Finance.BrothelWork(house); // and add the rest to the brothel finances
            girl.Pay = 0; // clear pay
            if (girl.Money < 0)
            {
                girl.Money = 0; // Not sure how this could happen - suspect it's just a sanity check
            }

            if (stolen == 0)
            {
                return; // If she didn't steal anything, we're done
            }
            Gang gang = Game.Gangs.GetGangOnMission(EnuGangMissions.SpyGirls); // if no-one is watching for theft, we're done
            if (gang == null)
            {
                return;
            }
            int catch_pc = Game.Gangs.ChanceToCatch(girl); // work out the % chance that the girl gets caught
            if (!WMRand.Percent(catch_pc))
            {
                return; // if they don't catch her, we're done
            }

            // OK: she got caught. Tell the player
            LocalString gmess = new LocalString();
            gmess.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                "YourGoonsSpotted[GirlName]TakingMoreGoldThenSheReported",
                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            gang.Events.AddMessage(gmess.ToString(), ImageType.PROFILE, EventType.Gang);
        }

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
        {
            // We look for an item in the range of countFrom to MAXNUM_INVENTORY.
            // Either the index of the item or -1 is returned.

            if (countFrom == -1)
            {
                countFrom = 0;
            }

            if (countFrom >= Constants.MAXNUM_INVENTORY)
            {
                return -1;
            }

            for (int i = countFrom; i < Constants.MAXNUM_INVENTORY; i++)
            {
                if (m_Inventory[i] != null)
                {
                    if (m_Inventory[i].Name == name)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

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

        public int GetNumberOfItemsOfType(int type, bool splitsubtype)
        {
            if (m_NumInventory < 1)
            {
                return 0;
            }

            int num = 0;
            int found = 0; // to reduce loops
            for (int i = 0; i < Constants.MAXNUM_INVENTORY && found < m_NumInventory; i++)
            {
                if (m_Inventory[i] != null)
                {
                    found++;
                    if ((int)m_Inventory[i].ItemType == type)
                    {
                        num++;
                    }
                    // if we are looking for consumables (INVFOOD) but we are not splitting subtypes, accept INVMAKEUP as INVFOOD.
                    if (type == (int)ItemType.FOOD && !splitsubtype && (int)m_Inventory[i].ItemType == (int)ItemType.MAKEUP)
                    {
                        num++;
                    }
                }
            }
            return num;
        }



        public long GetBribeRate()
        { return m_BribeRate; }
        public void SetBribeRate(long rate)
        { m_BribeRate = rate; }

        // TODO : REFACTORING - Change chained list rivale to collection
        public void UpdateBribeInfluence()
        {
            m_Influence = GetBribeRate();
            cRival rival = GetRivals();
            if (rival != null)
            {
                long total = m_BribeRate;
                total += Constants.TOWN_OFFICIALSWAGES; // this is the amount the government controls

                while (rival != null) // get the total for all bribes
                {
                    total += rival.m_BribeRate;
                    rival = rival.m_Next;
                }

                rival = GetRivals();
                while (rival != null) // get the total for all bribes
                {
                    if (rival.m_BribeRate > 0 && total != 0)
                    {
                        rival.m_Influence = (int)(((float)rival.m_BribeRate / (float)total) * 100.0f);
                    }
                    else
                    {
                        rival.m_Influence = 0;
                    }
                    rival = rival.m_Next;
                }

                if (m_BribeRate != 0 && total != 0)
                {
                    m_Influence = (int)(((float)m_BribeRate / (float)total) * 100.0f);
                }
                else
                {
                    m_Influence = 0;
                }
            }
            else
            {
                if (m_BribeRate <= 0)
                {
                    m_Influence = 0;
                }
                else
                {
                    m_Influence = (int)(((float)m_BribeRate / (float)((float)Constants.TOWN_OFFICIALSWAGES + (float)m_BribeRate)) * 100.0f);
                }
            }
        }

        public long GetInfluence()
        { return m_Influence; }

        public cRival GetRivals()
        { return m_Rivals.GetRivals(); }
        [Obsolete("RivalManager may move to GameEngine", false)]
        public cRivalManager GetRivalManager()
        { return m_Rivals; }

        // ----- Bank & money
        // TODO : REFACTORING - Create bank master class to extent functionality...
        public void WithdrawFromBank(int amount)
        {
            if (m_Bank - amount >= 0)
            {
                m_Bank -= amount;
            }
        }
        public void DepositInBank(int amount)
        {
            if (amount > 0)
            {
                m_Bank += amount;
            }
        }
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

        // updates an objective and checks for compleation
        [Obsolete("Game objectives must be move to Game instance or as default to player instance", false)]
        public void UpdateObjective()
        {
            if (m_Objective != null)
            {
                if (m_Objective.m_Limit > -1)
                {
                    m_Objective.m_Limit--;
                }

                switch (m_Objective.Objective)
                {
                    case Objectives.REACHGOLDTARGET:
                        if (Game.Brothels.GetBankMoney() >= m_Objective.Target)
                        {
                            PassObjective(); // `J` changed to bank instead of cash to clear up issues
                        }
                        break;
                    case Objectives.HAVEXGOONS:
                        if (Game.Gangs.GetNumGangs() >= m_Objective.Target)
                        {
                            PassObjective();
                        }
                        break;
                    case Objectives.STEALXAMOUNTOFGOLD:
                    case Objectives.CAPTUREXCATACOMBGIRLS:
                    case Objectives.KIDNAPXGIRLS:
                    case Objectives.EXTORTXNEWBUSINESS:
                        if (m_Objective.SoFar >= m_Objective.Target)
                        {
                            PassObjective();
                        }
                        break;
                    case Objectives.HAVEXMONSTERGIRLS:
                        if (GetTotalNumGirls(true) >= m_Objective.Target)
                        {
                            PassObjective();
                        }
                        break;
                    case Objectives.HAVEXAMOUNTOFGIRLS:
                        if (GetTotalNumGirls() >= m_Objective.Target)
                        {
                            PassObjective();
                        }
                        break;

                    // note that OBJECTIVE_GETNEXTBROTHEL has PassObjective() call in cScreenTown when passed.
                }

                // `J` moved to the end and fixed so if the objective is passed (thus deleted), failure is not returned
                if (m_Objective != null && m_Objective.m_Limit == 0)
                {
                    LocalString repportObjective = new LocalString();
                    if (m_Objective.Text.Length.Equals(0))
                    {
                        repportObjective.AppendLine(LocalString.ResourceStringCategory.Player, "YouHaveFailedAnObjective");
                    }
                    else
                    {
                        repportObjective.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                            "YouHaveFailedYourObjectiveTo[Objective]",
                        new List<FormatStringParameter>() { new FormatStringParameter("Objective", m_Objective.Text) });
                    }
                    Game.MessageQue.Enqueue(repportObjective.ToString(), MessageCategory.Red);
                    m_Objective = null;
                }
            }
        }
        /// <summary>
        /// Returns the objective
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use property", true)]
        public sObjective GetObjective()
        { return m_Objective; }
        /// <summary>
        /// Creates a new objective.
        /// </summary>
        [Obsolete("move objective to Game class or Player", false)]
        public void CreateNewObjective()
        {
            if (m_Objective != null)
            {
                m_Objective = null;
            }

            m_Objective = new sObjective();
            if (m_Objective != null) // Why if?
            {
                LocalString objectiveText = new LocalString();
                LocalString objectiveRepport = new LocalString();

                objectiveRepport.Append(LocalString.ResourceStringCategory.Player, "YouHaveANewObjectiveYouMust");
                bool done = false;
                m_Objective.m_Difficulty = Game.GameEngine.Year - 1209;
                m_Objective.SoFar = 0;
                m_Objective.m_Reward = WMRand.Random((int)Rewards.NUM_REWARDS);
                m_Objective.m_Limit = -1;
                m_Objective.Target = 0;
                m_Objective.Text = "";

                while (!done)
                {
                    m_Objective.Objective = (Objectives)WMRand.Random((int)Objectives.NUM_OBJECTIVES);
                    switch (m_Objective.Objective)
                    {
                        case Objectives.REACHGOLDTARGET:
                            {
                                if (m_Objective.m_Difficulty >= 3)
                                {
                                    m_Objective.m_Limit = WMRand.Random(20) + 10;
                                    m_Objective.Target = m_Objective.m_Limit * 1000;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Acquire[Amount]GoldWithin[Number]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Amount", m_Objective.Target), new FormatStringParameter("Number", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = (WMRand.Random(20) + 1) * 200;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Acquire[Amount]Gold",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Amount", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.LAUNCHSUCCESSFULATTACK:
                            {
                                if (Game.Rivals.GetNumRivals() > 0)
                                {
                                    m_Objective.m_Limit = (m_Objective.m_Difficulty >= 3 ? WMRand.Random(5) + 3 : WMRand.Random(10) + 10);
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "LaunchASuccessfulAttackMissionWithin[Number]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.m_Limit) });
                                    done = true;
                                }
                            } break;

                        case Objectives.HAVEXGOONS:
                            {
                                if (Game.Gangs.GetNumGangs() < Game.Gangs.GetMaxNumGangs())
                                {
                                    m_Objective.Target = Game.Gangs.GetNumGangs() + (WMRand.Random(3) + 1);
                                    if (m_Objective.Target > Game.Gangs.GetMaxNumGangs()) m_Objective.Target = Game.Gangs.GetMaxNumGangs();
                                    m_Objective.m_Limit = (m_Objective.m_Difficulty >= 3 ? WMRand.Random(4) + 3 : WMRand.Random(7) + 6);
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                       "Have[GangNumber]GangsWithin[Number]Weeks",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GangNumber", m_Objective.Target), new FormatStringParameter("Number", m_Objective.m_Limit) });
                                    done = true;
                                }
                            } break;

                        case Objectives.STEALXAMOUNTOFGOLD:
                            {
                                if (m_Objective.m_Difficulty >= 2)
                                {
                                    m_Objective.m_Limit = WMRand.Random(20) + 13;
                                    m_Objective.Target = m_Objective.m_Limit * 1300;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Steal[Amount]GoldWithin[Number]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Amount", m_Objective.Target), new FormatStringParameter("Number", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = (WMRand.Random(20) + 1) * 200;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Steal[Amount]Gold",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Amount", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.CAPTUREXCATACOMBGIRLS:
                            {
                                if (m_Objective.m_Difficulty >= 2)
                                {
                                    m_Objective.m_Limit = WMRand.Random(5) + 1;
                                    m_Objective.Target = WMRand.Random(m_Objective.m_Limit - 1) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Capture[Number]GirlsFromTheCatacombsWithin[Limite]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target), new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = WMRand.Random(5) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Capture[Number]GirlsFromTheCatacombs",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.HAVEXMONSTERGIRLS:
                            {
                                if (m_Objective.m_Difficulty >= 2)
                                {
                                    m_Objective.m_Limit = WMRand.Random(8) + 3;
                                    m_Objective.Target = GetTotalNumGirls(true) + WMRand.Random(m_Objective.m_Limit - 1) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "HaveATotalOf[Number]MonsterNonhumanGirlsWithin[Limite]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target), new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = GetTotalNumGirls(true) + WMRand.Random(8) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "HaveATotalOf[Number]MonsterNonhumanGirls",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.KIDNAPXGIRLS:
                            {
                                if (m_Objective.m_Difficulty >= 2)
                                {
                                    m_Objective.m_Limit = WMRand.Random(5) + 1;
                                    m_Objective.Target = WMRand.Random(m_Objective.m_Limit - 1) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Kidnap[Number]GirlsFromTheStreetsWithin[Limite]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target), new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = WMRand.Random(5) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "Kidnap[Number]GirlsFromTheStreets",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.EXTORTXNEWBUSINESS:
                            {	// `J` if there are not enough available businesses, don't use this one
                                if (Constants.TOWN_NUMBUSINESSES > GangManager.NumBusinessExtorted + 5)
                                {
                                    if (m_Objective.m_Difficulty >= 2)
                                    {
                                        m_Objective.m_Limit = WMRand.Random(5) + 1;
                                        m_Objective.Target = WMRand.Random(m_Objective.m_Limit - 1) + 1;
                                        objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                            "GainControlOf[Number]NewBusinessesWithin[Limite]Weeks",
                                            new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target), new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                    }
                                    else
                                    {
                                        m_Objective.Target = WMRand.Random(5) + 1;
                                        objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                            "GainControlOf[Number]NewBusinesses",
                                            new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target) });
                                    }
                                    done = true;
                                }
                            } break;

                        case Objectives.HAVEXAMOUNTOFGIRLS:
                            {
                                if (m_Objective.m_Difficulty >= 2)
                                {
                                    m_Objective.m_Limit = WMRand.Random(8) + 3;
                                    m_Objective.Target = GetTotalNumGirls() + WMRand.Random(m_Objective.m_Limit - 1) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "HaveATotalOf[Number]GirlsWithin[Limite]Weeks",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target), new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                }
                                else
                                {
                                    m_Objective.Target = GetTotalNumGirls() + WMRand.Random(8) + 1;
                                    objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                        "HaveATotalOf[Number]Girls",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Number", m_Objective.Target) });
                                }
                                done = true;
                            } break;

                        case Objectives.GETNEXTBROTHEL:
                            {
                                if (GetNumBrothels() < 6)
                                {
                                    if (m_Objective.m_Difficulty >= 2)
                                    {
                                        m_Objective.m_Limit = WMRand.Random(10) + 10;
                                        objectiveText.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                                           "PurchaseANewBrothelWithin[Limite]Weeks",
                                           new List<FormatStringParameter>() { new FormatStringParameter("Limite", m_Objective.m_Limit) });
                                    }
                                    else
                                    {
                                        objectiveText.AppendLine(LocalString.ResourceStringCategory.Player, "PurchaseANewBrothel");
                                    }
                                    done = true;
                                }
                            } break;
                    }

                }
                objectiveRepport.AppendLitteral(objectiveText.ToString());
                m_Objective.Text = objectiveText.ToString();

                if (objectiveRepport.HasMessage())
                {
                    Game.MessageQue.Enqueue(objectiveRepport.ToString(), MessageCategory.Darkblue);
                    Game.Brothels.GetBrothel(0).m_Events.AddMessage(objectiveRepport.ToString(), ImageType.PROFILE, EventType.GoodNews);
                }
            }

        }
        
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
        {
            bool added = false;
            int curI = Game.Brothels.HasItem(item.Name, -1);

            bool loop = true;
            while (loop)
            {
                if (curI != -1)
                {
                    if (Game.Brothels.m_NumItem[curI] >= 999)
                    {
                        curI = Game.Brothels.HasItem(item.Name, curI + 1);
                    }
                    else
                    {
                        loop = false;
                    }
                }
                else
                {
                    loop = false;
                }
            }

            if (Game.Brothels.m_NumInventory < Constants.MAXNUM_INVENTORY || curI != -1)
            {
                if (curI != -1)
                {
                    added = true;
                    Game.Brothels.m_NumItem[curI]++;

                }
                else
                {
                    for (int j = 0; j < Constants.MAXNUM_INVENTORY; j++)
                    {
                        if (Game.Brothels.m_Inventory[j] == null)
                        {
                            added = true;
                            Game.Brothels.m_Inventory[j] = item;
                            Game.Brothels.m_EquipedItems[j] = 0;
                            Game.Brothels.m_NumInventory++;
                            Game.Brothels.m_NumItem[j]++;
                            break;
                        }
                    }
                }
            }

            SortInventory();
            return added;
        }

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
            long pc = Game.Player.suspicion() - m_Influence;
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
        {
            double taxRate = Configuration.Tax.Rate; // normal tax rate is 6%
            if (m_Influence > 0) // can you influence it lower
            {
                int lowerBy = (int)m_Influence / 20;
                float amount = (float)(lowerBy / 100);
                taxRate -= amount;
                if (taxRate < Configuration.Tax.Minimum)
                {
                    taxRate = Configuration.Tax.Minimum;
                }
            }
            // check for money laundering and apply tax
            int earnings = Game.Gold.TotalEarned();

            if (earnings <= 0)
            {
                Game.MessageQue.Enqueue(LocalString.GetString(LocalString.ResourceStringCategory.Player, "YouDidntEarnAnyMoneySoDidntGetTaxed"), MessageCategory.Blue);
                return;
            }
            /*
            *	money laundering: nice idea - I had no idea it was
            *	in the game.
            *
            *	Probably we should make the player work for this.
            *	invest a little in businesses to launder through.
            */
            int laundry = WMRand.Random((int)(earnings * Configuration.Tax.Laundry));
            int tax = (int)((earnings - laundry) * taxRate);
            /*
            *	this should not logically happen unless we
            *	do something very clever with the money laundering
            */
            if (tax <= 0)
            {
                Game.MessageQue.Enqueue(LocalString.GetString(LocalString.ResourceStringCategory.Player, "ThanksToACleverAccountantNoneOfYourIncomeTurnsOutToBeTaxable"), MessageCategory.Blue);
                return;
            }
            Game.Gold.Tax(tax);
            LocalString taxeReport = new LocalString();
            /*
            *	Let's report the laundering, at least.
            *	Otherwise, it just makes the tax rate wobble a bit
            */
            taxeReport.AppendLineFormat(LocalString.ResourceStringCategory.Player,
                "YouWereTaxed[Tax]GoldYouManagedToLaunder[Laundry]ThroughVariousLocalBusinesses",
                new List<FormatStringParameter>() { new FormatStringParameter("Tax", tax), new FormatStringParameter("Laundry", laundry) });
            Game.MessageQue.Enqueue(taxeReport.ToString(), MessageCategory.Blue);
        }

        public void do_daily_items(sBrothel brothel, sGirl girl)
        {
            if (girl.m_NumInventory < 1)
            {
                return; // no items so skip it
            }

            LocalString effectItemsReport = new LocalString();
            string girlName = girl.Realname;
            bool masturbate = false;
            bool striptease = false;
            bool combat = false;
            bool formal = false;
            bool swim = false;
            bool cook = false;
            bool maid = false;

            // `J` zzzzzz - This list needs to be sorted into groups

            // unrestricted use items

            if (Game.Girls.HasItemJ(girl, "Android, Assistance") != -1 && WMRand.Percent(50))
            {
                effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "HerAssistanceAndroidSweptUpAndTookOutTheTrashForHer");
                brothel.Filthiness -= 5;
            }
            if (Game.Girls.HasItemJ(girl, "Room Decorations") != -1 && WMRand.Percent(3))
            {
                effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheLooksAroundAtHerRoomDecorationsAndSmilesSheReallyLikesThatHerRoomIsALittleBetterThenMostTheOtherGirls");
                girl.happiness(5);
            }
            if (Game.Girls.HasItemJ(girl, "Journal") != -1 && WMRand.Percent(15))
            {
                if (Game.Girls.HasTrait(girl, "Nerd") && WMRand.Percent(50))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheDecideToWriteOnHerNovelSomeToday");
                    girl.happiness(WMRand.Random(2));
                    girl.intelligence(WMRand.Random(2));
                }
                else if (Game.Girls.HasTrait(girl, "Bimbo") && WMRand.Percent(50))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheDoodledSillyPicturesInHerJournal");
                    girl.happiness(WMRand.Random(3));
                }
                else
                {
                    string thoughts = string.Empty;
                    switch (WMRand.Random(20))
                    {
                        case 0:
                            girl.happiness(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtHappy");
                            break;
                        case 1:
                            girl.happiness(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtSad");
                            break;
                        case 2:
                            girl.happiness(1 + WMRand.Random(3));
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtFun");
                            break;
                        case 3:
                            girl.intelligence(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtInteresting");
                            break;
                        case 4:
                            girl.spirit(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtPositive");
                            break;
                        case 5:
                            girl.spirit(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtNegative");
                            break;
                        case 6:
                            girl.obedience(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtHelpful");
                            break;
                        case 7:
                            girl.obedience(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtAnoying");
                            break;
                        case 8:
                            girl.pclove(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtLoving");
                            break;
                        case 9:
                            girl.pclove(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtUnloving");
                            break;
                        case 10:
                            girl.pchate(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtHateful");
                            break;
                        case 11:
                            girl.pchate(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtCarefree");
                            break;
                        case 12:
                            girl.pcfear(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtFearful");
                            break;
                        case 13:
                            girl.pcfear(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtFearless");
                            break;
                        case 14:
                            girl.dignity(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtProper");
                            break;
                        case 15:
                            girl.dignity(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtSlutty");
                            break;
                        case 16:
                            girl.libido(-1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtTame");
                            break;
                        case 17:
                            girl.libido(1);
                            thoughts = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThoughtSexy");
                            break;
                        default:
                            break;
                    }
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "SheUsedHerJournalToWriteSomeOfHer[Thoughts]ThoughtsDownToday",
                        new List<FormatStringParameter>() { new FormatStringParameter("Thoughts", thoughts) });
                }
            }
            // Dream Orbs

            if (Game.Girls.HasItemJ(girl, "Nightmare Orb") != -1 && WMRand.Percent(50))
            {
                if (girl.pclove() > girl.pcfear() && WMRand.Percent(girl.pclove()))
                {
                    if (WMRand.Percent(50))
                    {
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]ComesToYouAndTellsYouSheHadAScaryDreamAboutYou",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    }
                    girl.pcfear(WMRand.Random(2));
                    girl.happiness(-1);
                    girl.tiredness(1);
                }
                else if (girl.has_trait("Masochist") || girl.has_trait("Twisted")) // she liked it
                {
                    girl.pcfear(WMRand.Random(2));
                    girl.happiness(WMRand.Random(3));
                    girl.tiredness(WMRand.Random(2));
                }
                else // everyone else
                {
                    girl.pcfear(WMRand.Random(3));
                    girl.happiness(-WMRand.Random(3));
                    girl.tiredness(WMRand.Random(4));
                }
            }
            if (Game.Girls.HasItemJ(girl, "Lovers Orb") != -1 && WMRand.Percent(50))
            {
                if (girl.pclove() > girl.pchate() && WMRand.Percent(girl.pclove()))
                {
                    girl.pclove(1 + WMRand.Random(3));
                    girl.pcfear(-WMRand.Random(3));
                    girl.pchate(-WMRand.Random(3));
                    girl.happiness(3 + WMRand.Random(3));
                    girl.tiredness(1 + WMRand.Random(3));
                    girl.npclove(-(1 + WMRand.Random(3)));
                    if (WMRand.Percent(50))
                    {
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]ComesToYouAndTellsYouSheHadASexyDreamAboutYou",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });

                        // TODO : REFACTORING - change "Game.Player.Gender() >= Gender.HERMFULL"
                        if (girl.has_trait("Lesbian") && Game.Player.Gender() >= Gender.HERMFULL && WMRand.Percent(girl.pclove() / 10))
                        {
                            Game.Girls.RemoveTrait(girl, "Lesbian");
                            Game.Girls.AddTrait(girl, "Bisexual");
                            effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "NormallyIDontLikeMenButForYouIllMakeAnException");
                        }
                        // TODO : REFACTORING - change "Game.Player.Gender() <= Gender.FUTAFULL"
                        if (girl.has_trait("Straight") && Game.Player.Gender() <= Gender.FUTAFULL && WMRand.Percent(girl.pclove() / 10))
                        {
                            Game.Girls.RemoveTrait(girl, "Straight");
                            Game.Girls.AddTrait(girl, "Bisexual");
                            effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "NormallyIDontLikeWomenButForYouIllMakeAnException");
                        }
                        effectItemsReport.NewLine();
                    }
                }
                else // everyone else
                {
                    girl.pclove(WMRand.Random(3));
                    girl.pcfear(-WMRand.Random(2));
                    girl.pchate(-WMRand.Random(2));
                    girl.happiness(1 + WMRand.Random(3));
                    girl.tiredness(1 + WMRand.Random(3));
                }
            }
            if (Game.Girls.HasItemJ(girl, "Happy Orb") != -1 && WMRand.Percent(50))
            {
                if (girl.pclove() > girl.pcfear() && WMRand.Percent(girl.pclove()))
                {
                    if (WMRand.Percent(50))
                    {
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]ComesToYouAndTellsYouSheHadAHappyDreamAboutYou",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    }
                    girl.happiness(4 + WMRand.Random(5));
                    girl.pclove(WMRand.Random(2));
                    girl.pcfear(-WMRand.Random(2));
                    girl.pchate(-WMRand.Random(2));
                }
                else // everyone else
                {
                    girl.happiness(3 + WMRand.Random(3));
                }
            }

            // Items ONLY useable if resting
            if (cGirls.is_she_resting(girl))
            {
                if (Game.Girls.HasItemJ(girl, "Free Weights") != -1 && WMRand.Percent(15))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]DecideToSpendHerTimeWorkingOutWithHerFreeWeights",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
                    if (WMRand.Percent(5))
                    {
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Beauty, 1); // working out will help her look better
                    }
                    if (WMRand.Percent(10))
                    {
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Constitution, 1); // working out will make her healthier
                    }
                    if (WMRand.Percent(50))
                    {
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Strength, 1); // working out will make her stronger
                    }
                }

                // Books and reading materials

                // first list all books to see if she has any
                if (Game.Girls.HasItemJ(girl, "Manual of Sex") != -1 || Game.Girls.HasItemJ(girl, "Manual of Bondage") != -1 || Game.Girls.HasItemJ(girl, "Manual of Two Roses") != -1 || Game.Girls.HasItemJ(girl, "Manual of Arms") != -1 || Game.Girls.HasItemJ(girl, "Manual of the Dancer") != -1 || Game.Girls.HasItemJ(girl, "Manual of Magic") != -1 || Game.Girls.HasItemJ(girl, "Manual of Health") != -1 || Game.Girls.HasItemJ(girl, "Library Card") != -1)
                {
                    int numbooks = girl.intelligence() / 30; // how many books can she read?
                    if (Game.Girls.HasTrait(girl, "Blind"))
                    {
                        numbooks = 1;
                    }
                    else
                    {
                        if (Game.Girls.HasTrait(girl, "Nerd"))
                        {
                            numbooks += 1;
                        }
                        if (Game.Girls.HasTrait(girl, "Quick Learner"))
                        {
                            numbooks += 1;
                        }
                        if (Game.Girls.HasTrait(girl, "Slow Learner"))
                        {
                            numbooks -= 2;
                        }
                        if (Game.Girls.HasTrait(girl, "Bimbo"))
                        {
                            numbooks -= 1;
                        }
                    }
                    if (numbooks < 1)
                    {
                        numbooks = 1;
                    }

                    // then see if she wants to read any
                    if (Game.Girls.HasItemJ(girl, "Manual of Sex") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfSex");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.NormalSex, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of Bondage") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfBondage");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.BDSM, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of Two Roses") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfTwoRoses");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.Lesbian, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of Arms") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfArms");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.Combat, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of the Dancer") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfTheDacer");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.Striptease, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of Magic") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfMagic");
                        Game.Girls.UpdateSkill(girl, (int)EnumSkills.Magic, 2);
                        numbooks--;
                    }
                    if (Game.Girls.HasItemJ(girl, "Manual of Health") != -1 && WMRand.Percent(5) && numbooks > 0)
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffReadingHerManualOfHealth");
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Constitution, 1);
                        numbooks--;
                    }

                    // She may go to the library if she runs out of books to read
                    if (Game.Girls.HasItemJ(girl, "Library Card") != -1 && WMRand.Percent(15) && numbooks > 0)
                    {
                        if (Game.Girls.HasTrait(girl, "Nymphomaniac"))
                        {
                            effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheSpentTheDayAtTheLibraryLookingAtPornMakingHerBecomeHorny");
                            Game.Girls.UpdateStatTemp(girl, (int)EnumStats.Libido, 15);
                        }
                        else
                        {
                            effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheSpentHerFreeTimeAtTheLibraryReading");
                            if (WMRand.Percent(5))
                            {
                                Game.Girls.UpdateStat(girl, (int)EnumStats.Intelligence, 1);
                            }
                            if (WMRand.Percent(5))
                            {
                                // TODO : REFACTORING - Get Skill instance instead of Random number for skill to get skill name
                                int upskill = WMRand.Random((int)EnumSkills.NUM_SKILLS);
                                int upskillg = WMRand.Random(4) - 1;
                                if (upskillg > 0)
                                {
                                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                        "SheFoundABookOn[Skill]AndGained[Number]PointsInIt",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Skill", sGirl.skill_names[upskill]), new FormatStringParameter("Number", upskillg) });
                                }
                                else
                                {
                                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                                        "SheFoundABookOn[Skill]ButDidntFindItVeryUseful",
                                        new List<FormatStringParameter>() { new FormatStringParameter("Skill", sGirl.skill_names[upskill]) });
                                }
                            }
                            effectItemsReport.NewLine();
                        }
                    }
                }

                // End Books and reading materials

            }

            ////////////////////
            // Unsorted items //
            ////////////////////


            if (Game.Girls.HasItemJ(girl, "Television Set") != -1)
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]SpentMostOfHerDayLoungingInFrontOfHerTelevisionSet",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
                    girl.tiredness(-5);
                    if (WMRand.Percent(5))
                    {
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Intelligence, 1);
                    }
                }
                else
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "AtTheEndOfHerLongDay[GirlName]FloppedDownInFrontOfHerTelevisionSetAndRelaxed",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    girl.tiredness(-3);
                }
            }
            if (Game.Girls.HasItemJ(girl, "Appreciation Trophy") != -1 && cGirls.is_she_cleaning(girl) && WMRand.Percent(5) && girl.pclove() > girl.pchate() - 10)
            {
                effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "WhileCleaning[GirlName]CameAcrossHerAppreciationTrophyAndSmiled",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                girl.pclove(1);
            }
            if (Game.Girls.HasItemJ(girl, "Art Easel") != -1 && WMRand.Percent(2))
            {
                int sale = WMRand.Random(30) + 1;
                effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                   "[GirlName]ManagedToSellOneOfHerPaintingsFor[Amount]Gold",
                   new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Amount", sale) });
                girl.Money += sale;
                girl.happiness(sale / 5);
                girl.fame(1);
            }
            if (Game.Girls.HasItemJ(girl, "Compelling Dildo") != -1)
            {
                if (Game.Girls.GetStat(girl, EnumStats.Libido) > 65 && cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]sLustGotTheBetterOfHerAndSheSpentTheDayUsingHerCompellingDildo",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    Game.Girls.UpdateStatTemp(girl, (int)EnumStats.Libido, -20);
                    masturbate = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Pet Spider") != -1 && WMRand.Percent(15))
            {
                if (Game.Girls.HasTrait(girl, "Meek"))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]sMeekNatureMakesHerCoverHerPetSpidersCageSoItDoesntScareHer",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else if (Game.Girls.HasTrait(girl, "Aggressive"))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]ThrowsInSomeFoodToHerPetSpiderAndSmilesWhileSheWatchsItKillItsPrey",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]PlaysWithHerPetSpider",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
            }
            if (Game.Girls.HasItemJ(girl, "Chrono Bed") != -1)
            {
                effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "ThanksToHerChronoBedSheGotAGreatNightsSleepAndWokeUpFeelingWonderful");
                girl.health(50);
                girl.tiredness(-50);
            }
            else if (Game.Girls.HasItemJ(girl, "Rejuvenation Bed") != -1)
            {
                effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "ThanksToHerRejuvenationBedSheGotAGreatNightsSleepAndWokeUpFeelingBetter");
                girl.health(5);
                girl.tiredness(-5);
            }
            if (Game.Girls.HasItemJ(girl, "Claptrap") != -1 && WMRand.Percent(10))
            {
                effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "ThanksToClaptrapsSenseOfHumorSheIsABetterMood");
                girl.happiness(5);
            }
            if (Game.Girls.HasItemJ(girl, "The Realm of Darthon") != -1 && WMRand.Percent(2))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimePlayingTheRealmOfDarthonWithSomeOfTheOtherGirls");
                    girl.happiness(5);
                }
            }
            if (Game.Girls.HasItemJ(girl, "Stripper Pole") != -1 && WMRand.Percent(10))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffPracticingOnHerStripperPole");
                    Game.Girls.UpdateSkill(girl, (int)EnumSkills.Striptease, 2);
                    striptease = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Android, Combat MK I") != -1 && WMRand.Percent(5))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffTrainingWithHerAndroidCombatMKI");
                    Game.Girls.UpdateSkill(girl, (int)EnumSkills.Combat, 1);
                    combat = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Android, Combat MK II") != -1 && WMRand.Percent(10))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffTrainingWithHerAndroidCombatMKII");
                    Game.Girls.UpdateSkill(girl, (int)EnumSkills.Combat, 2);
                    combat = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Compelling Buttplug") != -1 && WMRand.Percent(10))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffWithHerCompellingButtplugIn");
                    Game.Girls.UpdateSkill(girl, (int)EnumSkills.Anal, 2);
                }
            }
            if (Game.Girls.HasItemJ(girl, "Computer") != -1 && WMRand.Percent(15) && cGirls.is_she_resting(girl))
            {
                if (Game.Girls.HasTrait(girl, "Nymphomaniac"))
                {
                    if (Game.Girls.GetStat(girl, EnumStats.Libido) > 65)
                    {
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]sLustGotTheBetterOfHerWhileSheWasOnTheHerComputerLookingAtPorn",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        Game.Girls.UpdateStatTemp(girl, (int)EnumStats.Libido, -20);
                        masturbate = true;
                    }
                    else
                    {
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheSpentTheDayOnHerComputerLookingAtPornMakingHerBecomeHorny");
                        Game.Girls.UpdateStatTemp(girl, (int)EnumStats.Libido, 15);
                    }
                }
                else
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SheSpentHerFreeTimePlayingOnHerComputer");
                    if (WMRand.Percent(5))
                    {
                        Game.Girls.UpdateStat(girl, (int)EnumStats.Intelligence, 1);
                    }
                }
            }
            if ((Game.Girls.HasItemJ(girl, "Cat") != -1 || Game.Girls.HasItemJ(girl, "Black Cat") != -1) && WMRand.Percent(10))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffWithHerPetCat");
                    girl.happiness(5);
                }
            }
            if (Game.Girls.HasItemJ(girl, "Guard Dog") != -1 && WMRand.Percent(15))
            {
                if (Game.Girls.HasTrait(girl, "Meek"))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                       "[GirlName]sMeekNatureMakesHerScaredOfHerPetGuardDog",
                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else if (Game.Girls.HasTrait(girl, "Aggressive"))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                       "[GirlName]SeeksHerGuardDogOnSomeRandomPatronsAndLaughsWhileTheyRunScared",
                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
                else
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]PlaysWithHerPetGuardDog",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
            }
            if (Game.Girls.HasItemJ(girl, "Noble Gown") != -1)
            {
                effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]WentAroundWearingHerNobleGownTodayMakingHerLookQuiteFormal",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                formal = true;
            }
            if (Game.Girls.HasItemJ(girl, "Fishnet Stocking") != -1)
            {
                effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]WentAroundWearingHerFishnetStockingTodayMakingHerSexyEvenIfItDidMakeHerFeelALitteTrashy",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
            }
            if (WMRand.Percent(15) && (Game.Girls.HasItemJ(girl, "White String Bikini") != -1 || Game.Girls.HasItemJ(girl, "Black String Bikini") != -1))
            {
                int num = 0;
                swim = true;
                if (Game.Girls.HasItemJ(girl, "White String Bikini") != -1 && Game.Girls.HasItemJ(girl, "Black String Bikini") != -1)
                {
                    num = WMRand.Random(2);
                    if (num == 1)
                    {
                        num = 2;
                    }
                }
                else if (Game.Girls.HasItemJ(girl, "White String Bikini") != -1)
                {
                    num = 0;
                }
                else
                {
                    num = 2;
                }
                if (WMRand.Percent(50) && cGirls.is_she_resting(girl))
                {
                    num++;
                    girl.happiness(5);
                }

                switch (num)
                {
                    case 0:
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]WentAroundWearingHerWhiteStringBikiniToday",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        break;
                    case 1:
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffAtTheLocalPoolInHerWhiteStringBikini");
                        break;
                    case 2:
                        effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                            "[GirlName]WentAroundWearingHerBlackStringBikiniToday",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        break;
                    case 3:
                        effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffAtTheLocalPoolInHerBlackStringBikini");
                        break;
                    default:
                        break;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Apron") != -1 && WMRand.Percent(10))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "ShePutOnHerApronAndCookedAMealForSomeOfTheGirls");
                    Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Workcooking, 1);
                    girl.happiness(5);
                    cook = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Maid Uniform") != -1 && WMRand.Percent(5))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "ShePutOnHerMaidUniformAndCleanedUp");
                    brothel.Filthiness -= 5;
                    maid = true;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Disguised Slave Band") != -1)
            {
                effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]WentAroundWearingHerDisguisedSlaveBandHavingNoIdeaOfWhatItReallyDoesToHer",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
            }
            if (Game.Girls.HasItemJ(girl, "Anger Management Tapes") != -1 && WMRand.Percent(2))
            {
                if (cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLine(LocalString.ResourceStringCategory.Girl, "SpentHerTimeOffListenToHerAngerManagementTapes");
                    Game.Girls.UpdateStat(girl, (int)EnumStats.Spirit, -2);
                }
            }
            if (Game.Girls.HasItemJ(girl, "Rainbow Underwear") != -1)
            {
                if (cGirls.is_she_stripping(girl)) //not sure this will work like i want it to might be that i need to added them to the jobs
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]StrippedDownToRevealHerRainbowUnderwearToTheApprovalOfThePatronsWatchingHer",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    brothel.Happiness += 5;
                }
            }
            if (Game.Girls.HasItemJ(girl, "Short Sword") != -1 && WMRand.Percent(5))
            {
                if (Game.Girls.GetStat(girl, EnumStats.Intelligence) > 65 && cGirls.is_she_resting(girl))
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]SharpenedHerShortSwordMakingItMoreReadyForCombat",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    Game.Girls.UpdateSkillTemp(girl, (int)EnumSkills.Combat, 2);
                }
                else
                {
                    effectItemsReport.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                        "[GirlName]TriedToSharpenHerShortSwordButDoesntHaveTheBrainsToDoItRight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                    Game.Girls.UpdateSkillTemp(girl, (int)EnumSkills.Combat, -2);
                }
            }

            if (effectItemsReport.HasMessage()) // only pass the summary if she has any of the items listed
            {
                ImageType imagetype = ImageType.PROFILE;
                /* */
                if (masturbate)
                {
                    imagetype = ImageType.MAST;
                }
                else if (striptease)
                {
                    imagetype = ImageType.STRIP;
                }
                else if (combat)
                {
                    imagetype = ImageType.COMBAT;
                }
                else if (formal)
                {
                    imagetype = ImageType.FORMAL;
                }
                else if (swim)
                {
                    imagetype = ImageType.SWIM;
                }
                else if (cook)
                {
                    imagetype = ImageType.COOK;
                }
                else if (maid)
                {
                    imagetype = ImageType.MAID;
                }

                girl.Events.AddMessage(effectItemsReport.ToString(), imagetype, EventType.Summary);

            }
        }

        // add the girls accommodation and food costs to the upkeep
        public void do_food_and_digs(sBrothel brothel, sGirl girl)
        {
            // `J` new code for .06.01.18
            LocalString report = new LocalString();

            // Gold per accommodation level
            int gold = (girl.is_slave() ? 5 : 20) * (girl.m_AccLevel + 1);
            brothel.m_Finance.GirlSupport(gold);

            int preferredaccom = Game.Girls.PreferredAccom(girl); // what she wants/expects
            int mod = girl.m_AccLevel - preferredaccom;

            /*   if (acc == 0)	return "Bare Bones";
            else if (acc == 1)	return "Very Poor";
            else if (acc == 2)	return "Poor";
            else if (acc == 3)	return "Adequate";
            else if (acc == 4)	return "Comfortable";
            else if (acc == 5)	return "Nice";
            else if (acc == 6)	return "Good";
            else if (acc == 7)	return "Great";
            else if (acc == 8)	return "Wonderful";
            else if (acc == 9)	return "High Class";
            */

            if (Configuration.Debug.LogExtraDetails)
            {
                string name = girl.Realname;
                while (name.Length < 30)
                {
                    name += " ";
                }
                WMLog.LogMessage.Append(name  + " | P_" + preferredaccom + "-A_" + girl.m_AccLevel + "=M_" + mod);
            }
            // bsin added Sanity for .06.02.30
            // TODO : REFACTORING - Create and use classe interval?
            int hapA = 0; // A should always be lower than B
            int hapB = 0;
            int lovA = 0;
            int lovB = 0;
            int hatA = 0;
            int hatB = 0;
            int feaA = 0;
            int feaB = 0;
            int sanA = 0;
            int sanB = 0;
            mod = Math.Max(Math.Min(mod, 9), -9);

            // TODO : REFACTORING - Call function to set couples
            switch (mod)	// happiness, love, hate, fear
            {
                case -9:
                    hapA = -24;
                    hapB = -7;
                    lovA = -14;
                    lovB = -3;
                    hatA = 6;
                    hatB = 22;
                    feaA = 5;
                    feaB = 12;
                    sanA = -7;
                    sanB = 2;
                    break;
                case -8: hapA = -19; hapB = -6; lovA = -11; lovB = -3; hatA = 5; hatB = 18; feaA = 4; feaB = 9; sanA = -6; sanB = 2;
                    break;
                case -7: hapA = -16; hapB = -5; lovA = -9; lovB = -3; hatA = 4; hatB = 14; feaA = 3; feaB = 7; sanA = -5; sanB = 1;
                    break;
                case -6: hapA = -13; hapB = -4; lovA = -7; lovB = -2; hatA = 4; hatB = 10; feaA = 2; feaB = 5; sanA = -4; sanB = 1;
                    break;
                case -5: hapA = -10; hapB = -3; lovA = -6; lovB = -2; hatA = 3; hatB = 7; feaA = 1; feaB = 4; sanA = -3; sanB = 1;
                    break;
                case -4: hapA = -8; hapB = -2; lovA = -5; lovB = -1; hatA = 2; hatB = 5; feaA = 0; feaB = 3; sanA = -2; sanB = 0;
                    break;
                case -3: hapA = -6; hapB = -1; lovA = -4; lovB = 0; hatA = 1; hatB = 4; feaA = 0; feaB = 2; sanA = -1; sanB = 0;
                    break;
                case -2: hapA = -4; hapB = 0; lovA = -3; lovB = 0; hatA = 0; hatB = 3; feaA = 0; feaB = 1; sanA = 0; sanB = 0;
                    break;
                case -1: hapA = -2; hapB = 1; lovA = -2; lovB = 1; hatA = -1; hatB = 2; feaA = 0; feaB = 0; sanA = 0; sanB = 0;
                    break;
                case 0: hapA = -1; hapB = 3; lovA = -1; lovB = 2; hatA = -1; hatB = 1; feaA = 0; feaB = 0; sanA = 0; sanB = 1;
                    break;
                case 1: hapA = 0; hapB = 5; lovA = -1; lovB = 3; hatA = -1; hatB = 0; feaA = 0; feaB = 0; sanA = 0; sanB = 1;
                    break;
                case 2: hapA = 1; hapB = 8; lovA = 0; lovB = 3; hatA = -3; hatB = 0; feaA = 0; feaB = 0; sanA = 0; sanB = 1;
                    break;
                case 3: hapA = 2; hapB = 11; lovA = 0; lovB = 4; hatA = -5; hatB = -1; feaA = -1; feaB = 0; sanA = 0; sanB = 2;
                    break;
                case 4: hapA = 3; hapB = 14; lovA = 1; lovB = 4; hatA = -6; hatB = -1; feaA = -1; feaB = 0; sanA = 0; sanB = 2;
                    break;
                case 5: hapA = 4; hapB = 16; lovA = 1; lovB = 5; hatA = -7; hatB = -1; feaA = -1; feaB = 0; sanA = 0; sanB = 3;
                    break;
                case 6: hapA = 5; hapB = 18; lovA = 2; lovB = 5; hatA = -7; hatB = -2; feaA = -2; feaB = 0; sanA = -1; sanB = 3;
                    break;
                case 7: hapA = 5; hapB = 19; lovA = 2; lovB = 6; hatA = -8; hatB = -2; feaA = -2; feaB = 0; sanA = -1; sanB = 4;
                    break;
                case 8: hapA = 5; hapB = 20; lovA = 2; lovB = 7; hatA = -9; hatB = -3; feaA = -3; feaB = 0; sanA = -1; sanB = 4;
                    break;
                case 9: hapA = 5; hapB = 21; lovA = 2; lovB = 8; hatA = -10; hatB = -3; feaA = -3; feaB = 0; sanA = -2; sanB = 5;
                    break;
                default: break;
            }
            if (Configuration.Debug.LogExtraDetails)
            {
                WMLog.LogMessage.Append("\t|");
            }

            if (girl.happiness() < 20 - mod)			// if she is unhappy, her mood will go down
            {
                if (Configuration.Debug.LogExtraDetails)
                {
                    WMLog.LogMessage.Append("a");
                }
                
                // TODO : REFACTORING - Call function to update couple.
                if (mod < -6)
                { hapA -= 7;
                    hapB -= 3; lovA -= 4; lovB -= 1; hatA += 2; hatB += 5; feaA += 2; feaB += 5; }
                else if (mod < -3)
                { hapA -= 5;
                    hapB -= 2; lovA -= 2; lovB -= 1; hatA += 1; hatB += 3; feaA += 1; feaB += 3; }
                else if (mod < 0)
                { hapA -= 3;
                    hapB -= 1; lovA -= 1; lovB -= 0; hatA += 0; hatB += 2; feaA += 0; feaB += 2; }
                else if (mod < 1)
                { hapA -= 2;
                    hapB -= 0; lovA -= 1; lovB -= 0; hatA += 0; hatB += 1; feaA += 0; feaB += 1; }
                else if (mod < 4)
                { hapA -= 2;
                    hapB -= 0; lovA -= 1; lovB -= 0; hatA += 0; hatB += 1; feaA -= 1; feaB += 1; }
                else if (mod < 7)
                {
                    hapA -= 1;
                    hapB -= 0; lovA -= 1; lovB -= 0; hatA += 0; hatB += 0; feaA -= 1; feaB += 0;
                }
            }
            else if (!WMRand.Percent(girl.happiness()))	// if she is not happy, her mood may go up or down
            {
                if (Configuration.Debug.LogExtraDetails)
                {
                    WMLog.LogMessage.Append("b");
                }
                // TODO : REFACTORING - Call function to update couple.
                if (mod < -6) { hapA -= 3; hapB += 1; lovA -= 3; lovB += 0; hatA -= 0; hatB += 4; feaA -= 2; feaB += 3; }
                else if (mod < -3) { hapA -= 2; hapB += 1; lovA -= 2; lovB += 0; hatA -= 0; hatB += 3; feaA -= 1; feaB += 2; }
                else if (mod < 0) { hapA -= 1; hapB += 2; lovA -= 1; lovB += 1; hatA -= 1; hatB += 2; feaA -= 1; feaB += 2; }
                else if (mod < 1) { hapA -= 1; hapB += 2; lovA -= 1; lovB += 1; hatA -= 1; hatB += 1; feaA -= 1; feaB += 1; }
                else if (mod < 4) { hapA += 0; hapB += 2; lovA -= 0; lovB += 1; hatA -= 1; hatB += 1; feaA -= 1; feaB += 0; }
                else if (mod < 7) { hapA += 0; hapB += 3; lovA += 0; lovB += 1; hatA -= 1; hatB -= 0; feaA -= 0; feaB += 0; }
            }
            else										// otherwise her mood can go up
            {
                if (Configuration.Debug.LogExtraDetails)
                {
                    WMLog.LogMessage.Append("c");
                }
                // TODO : REFACTORING - Call function to update couple.
                if (mod < -6) { hapA -= 1; hapB += 2; lovA -= 1; lovB += 1; hatA -= 1; hatB -= 1; feaA -= 1; feaB += 1; }
                else if (mod < -3) { hapA += 0; hapB += 2; lovA += 0; lovB += 1; hatA -= 2; hatB -= 0; feaA -= 2; feaB -= 0; }
                else if (mod < 0) { hapA += 0; hapB += 3; lovA += 0; lovB += 1; hatA -= 2; hatB -= 0; feaA -= 2; feaB -= 0; }
                else if (mod < 1) { hapA += 0; hapB += 5; lovA += 0; lovB += 1; hatA -= 2; hatB -= 1; feaA -= 2; feaB -= 0; }
                else if (mod < 4) { hapA += 1; hapB += 7; lovA += 0; lovB += 2; hatA -= 3; hatB -= 1; feaA -= 3; feaB -= 0; }
                else if (mod < 7) { hapA += 2; hapB += 8; lovA += 1; lovB += 3; hatA -= 4; hatB -= 1; feaA -= 3; feaB -= 1; }
            }
            if (girl.health() < 25) // if she is injured she may be scared because of her surroundings
            {
                if (Configuration.Debug.LogExtraDetails)
                {
                    WMLog.LogMessage.Append("d");
                }
                // TODO : REFACTORING - Call function to update couple.
                if (mod < -6)
                {
                    hapA -= 6;
                    hapB -= 2;
                    lovA -= 4;
                    lovB -= 1;
                    hatA += 3;
                    hatB += 4;
                    feaA += 2;
                    feaB += 4;
                    sanA -= 4;
                    sanB -= 2;
                }
                else if (mod < -3)
                {
                    hapA -= 4;
                    hapB -= 1;
                    lovA -= 3;
                    lovB -= 1;
                    hatA += 2;
                    hatB += 3;
                    feaA += 1;
                    feaB += 3;
                    sanA -= 2;
                    sanB -= 1;
                }
                else if (mod < 0)
                {
                    hapA -= 2;
                    hapB -= 1;
                    lovA -= 1;
                    lovB += 0;
                    hatA += 1;
                    hatB += 2;
                    feaA += 0;
                    feaB += 2;
                    sanA -= 1;
                    sanB -= 0;
                }
                else if (mod < 1)
                {
                    hapA -= 1;
                    hapB += 1;
                    lovA -= 0;
                    lovB += 0;
                    hatA -= 0;
                    hatB += 1;
                    feaA -= 1;
                    feaB += 1;
                    sanA += 0;
                    sanB += 1;
                }
                else if (mod < 4)
                {
                    hapA += 0;
                    hapB += 4;
                    lovA += 0;
                    lovB += 1;
                    hatA -= 1;
                    hatB += 0;
                    feaA -= 2;
                    feaB += 1;
                    sanA += 1;
                    sanB += 2;
                }
                else if (mod < 7)
                {
                    hapA += 2;
                    hapB += 8;
                    lovA += 1;
                    lovB += 1;
                    hatA -= 1;
                    hatB += 0;
                    feaA -= 3;
                    feaB += 0;
                    sanA += 2;
                    sanB += 4;
                }
            }
            else if (Configuration.Debug.LogExtraDetails)
            {
                WMLog.LogMessage.Append(" ");
            }
            if (girl.is_slave()) // slaves get half as much from their mods
            {
                if (Configuration.Debug.LogExtraDetails)
                {
                    WMLog.LogMessage.Append("e");
                }
                hapA /= 2;
                hapB /= 2;
                lovA /= 2;
                lovB /= 2;
                hatA /= 2;
                hatB /= 2;
                feaA /= 2;
                feaB /= 2;
            }
            else if (Configuration.Debug.LogExtraDetails)
            {
                WMLog.LogMessage.Append(" ");
            }

            int hap = WMRand.Bell(hapA, hapB);
            int lov = WMRand.Bell(lovA, lovB);
            int hat = WMRand.Bell(hatA, hatB);
            int fea = WMRand.Bell(feaA, feaB);
            int san = WMRand.Bell(sanA, sanB);

            if (Configuration.Debug.LogExtraDetails)
            {
                WMLog.LogMessage.Append("\t| happy:\t" + hapA + "\t" + hapB + "\t=" + hap + "\t| love :\t" + lovA + "\t" + lovB + "\t=" + lov + "\t| hate :\t" + hatA + "\t" + hatB + "\t=" + hat + "\t| fear :\t" + feaA + "\t" + feaB + "\t=" + fea + "\t| sanity :\t" + sanA + "\t" + sanB + "\t=" + san);
                WMLog.TraceBuffer(WMLog.TraceLog.INFORMATION);
            }


            girl.happiness(hap);
            girl.pclove(lov);
            girl.pchate(hat);
            girl.pcfear(fea);
            girl.sanity(san);
            // after all the happy, love fear and hate are done, do some other checks.

            #region false pragma
            //	if (girl->pchate() > girl->pcfear())		// if she hates you more than she fears you, she will disobey more
//	{
//		girl->obedience(g_Dice.bell(mod, 0));
//		girl->spirit(g_Dice.bell(-1, 2));
//	}
//	else										// otherwise she will obey more in hopes of getting an upgrade
//	{
//		girl->obedience(g_Dice.bell(0, -mod));
//		girl->spirit(g_Dice.bell(-2, 1));
            //	}
            #endregion





            int chance = 1 + (mod < 0 ? -mod : mod);
            if (!WMRand.Percent(chance))
            {
                return;
            }
            // Only check if a trait gets modified if mod is far from 0

            bool b_health = WMRand.Percent(girl.health());
            bool b_happiness = WMRand.Percent(girl.happiness());
            bool b_tiredness = WMRand.Percent(girl.tiredness());
            bool b_intelligence = WMRand.Percent(girl.intelligence());
            bool b_confidence = WMRand.Percent(girl.confidence());
            bool b_libido = WMRand.Percent(girl.libido());
            bool b_obedience = WMRand.Percent(girl.obedience());
            bool b_spirit = WMRand.Percent(girl.spirit());
            bool b_pclove = WMRand.Percent(girl.pclove());
            bool b_pcfear = WMRand.Percent(girl.pcfear());
            bool b_pchate = WMRand.Percent(girl.pchate());
            bool b_morality = WMRand.Percent(girl.morality());
            bool b_refinement = WMRand.Percent(girl.refinement());
            bool b_dignity = WMRand.Percent(girl.dignity());

            if (girl.has_trait("Homeless") && b_refinement && b_dignity && b_confidence && mod >= 0 && girl.m_AccLevel >= 5 && WMRand.Percent(girl.m_AccLevel))
            {
                Game.Girls.RemoveTrait(girl, "Homeless", true);
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]HasGottenUsedToBetterSurroundingsAndHasLostTheHomelessTrait",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (girl.has_trait("Masochist") && b_intelligence && b_spirit && b_confidence && mod >= 2 && WMRand.Percent(girl.m_AccLevel - 7))
            {
                Game.Girls.RemoveTrait(girl, "Masochist", true);
                // `J` zzzzzz - needs better text
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]SeemsToBeGettingOverHerMasochisticTendencies",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (!girl.has_trait("Masochist") && !b_dignity && !b_spirit && !b_confidence && mod <= -1 && WMRand.Percent(3 - mod))
            {
                Game.Girls.AddTrait(girl, "Masochist");
                // `J` zzzzzz - needs better text
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName][GirlName]SeemsToBeGettingOverHerMasochisticTendencies",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (WMRand.Percent(90))
            {
            } // `J` - zzzzzz - The rest need work so for now they will be less common

            else if (girl.has_trait("Optimist") && mod < 0 && WMRand.Percent(3))
            {
                Game.Girls.RemoveTrait(girl, "Optimist", true);
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]HasLostTheOptimistTraitSomeoneWriteBetterTextForThis",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (!girl.has_trait("Optimist") && mod > 0 && WMRand.Percent(3)) // `J` - zzzzzz - needs work
            {
                Game.Girls.AddTrait(girl, "Optimist");
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]HasGainedTheOptimistTraitSomeoneWriteBetterTextForThis",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (girl.has_trait("Pessimist") && mod > 0 && WMRand.Percent(3)) // `J` - zzzzzz - needs work
            {
                Game.Girls.RemoveTrait(girl, "Pessimist", true);
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]HasLostThePessimistTraitSomeoneWriteBetterTextForThis",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }
            else if (!girl.has_trait("Pessimist") && mod < 0 && WMRand.Percent(3)) // `J` - zzzzzz - needs work
            {
                Game.Girls.AddTrait(girl, "Pessimist");
                report.AppendLineFormat(LocalString.ResourceStringCategory.Girl,
                    "[GirlName]HasGainedThePessimistTraitSomeoneWriteBetterTextForThis",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.Realname) });
            }



            if (report.HasMessage())
            {
                girl.Events.AddMessage(report.ToString(), ImageType.PROFILE, EventType.GoodNews);
            }

            #region Old code
            //        // old code
    ///*
    //*	add the girls accommodation and food costs to the upkeep
    //*/
    //if (girl->is_slave()) {
    //    /*
    //    *		For a slavegirl, 5 gold per accommodation level
    //    */
    //    brothel->m_Finance.girl_support(5 * (girl->m_AccLevel + 1));
    //    /*
    //    *		accommodation zero is all they expect
    //    */
    //    if (girl->m_AccLevel == 0) return;
    //    /*
    //    *		accommodation over 0 means happier,
    //    *		and maybe liking the PC more
    //    *
    //    *		mod: docclox - made happiness gains greater
    //    *		for nicer digs
    //    */
    //    girl->happiness(4 + girl->m_AccLevel / 2);
    //    // end mod
    //    /*
    //    *		mod - doc - make love and hate change faster
    //    *		with better digs - but not at the same rate as
    //    *		the happiness bonus, so there's a point to the
    //    *		intermediate levels
    //    */
    //    int excess = girl->happiness() - 100;
    //    if (excess >= 0) {
    //        int mod = 1 + excess / 3;
    //        girl->pchate(-mod);
    //        girl->pclove(mod);
    //    }
    //    // end mod
    //    return;
    //}
    ///*
    //*	For a freegirl, 20 gold per accommodation level
    //*	mod - doc - simplified the calculation a bit
    //*/
    //brothel->m_Finance.girl_support(20 * (girl->m_AccLevel + 1));
    ///*
    //*	let's do the simple case
    //*	if her accommodation is greater then her level
    //*		divided by 2		// `J` added
    //*	she'll get happier. That's a mod: it was >=
    //*	before, but this way 0 level girls want level 1 accom
    //*	and it goes up level for level thereafter
    //*/
    //if (girl->m_AccLevel > girl->level() / 2)
    //{
    //    girl->happiness(2 + girl->m_AccLevel / 2);
    //    int excess = girl->happiness() - 100;
    //    if (excess >= 0)
    //    {
    //        int mod = 1 + excess / 3;
    //        girl->pchate(-mod);
    //        girl->pclove(mod);
    //    }
    //    return;
    //}
    ///*
    //*	If we get here, the accommodation level is less
    //*	than a girl of her accomplisments would expect
    //*	However, level 11 (was 6) and greater and her sense of
    //*	professionalism means she doesn't let it affect her
    //*	state of mind
    //*/
    //if (girl->level() >= 11) {
    //    return;
    //}
    ///*
    //*	Failing that, she will be less happy
    //*/
    //// `J` - she will be much less happy with lower accom now
    //int mod, diff = girl->level() - girl->m_AccLevel;
    //mod = diff / 2;	// half the difference, round down
    //mod++;		// and add one
    //girl->happiness(-mod);
    ///*
    //*	and if she gets completely miserable,
    //*	she'll grow to hate the PC
    //*/
    //if (girl->happiness() <= 0) {
    //    girl->pchate(1 + diff / 3);
    //}
    //    // end old code
        #endregion

        }
        public string disposition_text()
        { throw new NotImplementedException(); }
        public string fame_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public string suss_text()
        { throw new NotImplementedException(); }
        public string happiness_text(sBrothel brothel)
        { throw new NotImplementedException(); }
        public double calc_pilfering(sGirl girl)
        {
            double factor = 0.0;
            if (girl.is_addict() && girl.Money < 100) // on top of all other factors, an addict will steal to feed her habit
            {
                factor += (girl.is_addict(true) ? 0.5 : 0.1); // hard drugs will make her steal more
            }
            // let's work out what if she is going steal anything
            if (girl.pclove() >= 50 || girl.obedience() >= 50)
            {
                return factor; // love or obedience will keep her honest
            }
            if (girl.pcfear() > girl.pchate())
            {
                return factor; // if her fear is greater than her hate, she won't dare steal
            }
            // `J` yes they do // if (girl->is_slave()) return factor;					// and apparently, slaves don't steal
            if (girl.pchate() > 40)
            {
                return factor + 0.15; // given all the above, if she hates him enough, she'll steal
            }
            if (girl.confidence() > 70 && girl.spirit() > 50)
            {
                return factor + 0.15; // if she's not motivated by hatred, she needs to be pretty confident
            }
            return factor; // otherwise, she stays honest (aside from addict factored-in earlier)
        }

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
                totalFame += Game.Girls.GetStat(girl, EnumStats.Fame);
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
        private long m_Influence;				// based on the bribe rate this is the percentage of influence you have
        private int m_Dummy;					//a dummy variable
        private long m_Bank;					// how much is stored in the bank

        [Obsolete("Move Objective to Game class or player", false)]
        private sObjective m_Objective;
        /// <summary>
        /// Get the objective
        /// </summary>
        [Obsolete("Move Objective to Game class or player", false)]
        public sObjective CurrentObjective
        {
            get { return this.m_Objective; }
        }

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