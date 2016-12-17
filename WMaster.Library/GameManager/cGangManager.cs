using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;
using WMaster.GameConcept;
using WMaster.GameManager;
using WMaster.Tool;

namespace WMaster.ClassOrStructurToImplement
{
    /// <summary>
    /// Manages all the player gangs
    /// </summary>
    public class cGangManager : System.IDisposable
    {
        /// <summary>
        /// Number of businesses under player control.
        /// <remarks><para>TODO : May translate to player class</para></remarks>
        /// </summary>
        private int m_BusinessesExtort;

        private bool m_Control_Gangs;
        private int m_Gang_Gets_Girls;
        private int m_Gang_Gets_Items;
        private int m_Gang_Gets_Beast;

        private int m_MaxNumGangs;
        private int m_NumGangNames;
        //private int m_NumGangs;
        //private Gang m_GangStart; // the start and end of the list of gangs under the players employment
        //private Gang m_GangEnd;
        /// <summary>
        /// List of gangs under the players employment.
        /// </summary>
        private List<Gang> m_PlayerGangList = new List<Gang>();
        //private int m_NumHireableGangs;
        //private Gang m_HireableGangStart; // the start and end of the list of gangs which are available for hire
        //private Gang m_HireableGangEnd;
        /// <summary>
        /// List of gangs which are available for hire.
        /// </summary>
        private List<Gang> m_HireableGangList = new List<Gang>();

        // gang armory
        // mod - changing the keep stocked flag to an int
        // so we can record the level at which to maintain
        // the stock - then we can restock at turn end
        // to prevent squads becoming immortal by
        // burning money
        private int m_KeepHealStocked;
        private int m_NumHealingPotions;
        private int m_SwordLevel;
        private int m_KeepNetsStocked;
        private int m_NumNets;

        public cGangManager()
        {
            m_NumGangNames = 0;
            //ifstream in = new ifstream();
            //// WD: Typecast to resolve ambiguous call in VS 2010
            //DirPath dp = DirPath() << "Resources" << "Data" << "HiredGangNames.txt";
            //in.open(dp.c_str());
            //in >> m_NumGangNames;
            //in.close();

            m_BusinessesExtort = 0;
            m_NumHealingPotions = m_NumNets = m_SwordLevel = 0;
            m_KeepHealStocked = m_KeepNetsStocked = 0;
            m_Control_Gangs = false;
            m_Gang_Gets_Girls = m_Gang_Gets_Items = m_Gang_Gets_Beast = 0;
        }


        public void Dispose()
        {
            Free();
        }

        /// <summary>
        /// Free GangManager datas
        /// </summary>
        public void Free()
        {
            m_PlayerGangList.Clear();
            m_HireableGangList.Clear();
            m_BusinessesExtort = 0;
            m_NumHealingPotions = m_SwordLevel = m_NumNets = 0;
            m_KeepHealStocked = m_KeepNetsStocked = 0;
            m_Control_Gangs = false;
            m_Gang_Gets_Girls = m_Gang_Gets_Items = m_Gang_Gets_Beast = 0;
        }


        /// <summary>
        /// Adds a new randomly generated gang to the recruitable list
        /// </summary>
        /// <param name="boosted"><b>True</b> to boos number of gang member, skills and statistic.</param>
        public void AddNewGang(bool boosted)
        {
            Gang newGang = new Gang();

            int maxMembers = cConfig.Instance.Gangs.InitMemberMax;
            int minMembers = cConfig.Instance.Gangs.InitMemberMin;
            newGang.MemberNum = minMembers + WMRandom.Next() % (maxMembers + 1 - minMembers);
            if (boosted)
            {
                newGang.MemberNum = Math.Min(15, newGang.MemberNum + 5);
            }

            int new_val;
            foreach (EntitySkill skill in newGang.Skills)
            {
                new_val = (WMRandom.Next() % 30) + 21;
                if (WMRandom.Next() % 5 == 1)
                {
                    new_val += 1 + WMRandom.Next() % 10;
                }
                if (boosted)
                {
                    new_val += 10 + WMRandom.Next() % 11;
                }
                skill.Value = new_val;
            }

            foreach (EntityStat item in newGang.Stats)
            {
                new_val = (WMRandom.Next() % 30) + 21;
                if (WMRandom.Next() % 5 == 1)
                {
                    new_val += WMRandom.Next() % 10;
                }
                if (boosted)
                {
                    new_val += 10 + WMRandom.Next() % 11;
                }
                item.Value = new_val;
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;

            #region Get random gang name
            //string buffer = new string(new char[256]);
            //bool done = false;

            //ifstream in = new ifstream();
            //// WD: Typecast to resolve ambiguous call in VS 2010
            //DirPath dp = DirPath() << "Resources" << "Data" << "HiredGangNames.txt";
            //in.open(dp.c_str());
            ////in.open(DirPath() <<	"Resources" << "Data" << "HiredGangNames.txt");
            //while (!done)
            //{
            //    in.seekg(0);
            //    int name = g_Dice % m_NumGangNames;
            //    in >> m_NumGangNames; // ignore the first line
            //    for (int i = 0; i <= name; i++)
            //    {
            //        if (in.peek() == '\n')
            //        {
            //            in.ignore(1, '\n');
            //        }
            //        in.getline(buffer, sizeof(sbyte), '\n');
            //    }
            //    done = true;
            //    sGang curr = m_GangStart;
            //    while (curr != null)
            //    {
            //        if (curr.m_Name == buffer)
            //        {
            //            done = false;
            //            break;
            //        }
            //        curr = curr.m_Next;
            //    }
            //    curr = m_HireableGangStart;
            //    while (curr != null)
            //    {
            //        if (curr.m_Name == buffer)
            //        {
            //            done = false;
            //            break;
            //        }
            //        curr = curr.m_Next;
            //    }
            //}
            //newGang.m_Name = buffer;
            //in.close();
            #endregion

            this.m_HireableGangList.Add(newGang);
        }

        #region Player gang
        /// <summary>
        /// Add a gang to player gang list.
        /// </summary>
        /// <param name="newGang"><see cref="Gang"/> to add.</param>
        public void AddGang(Gang newGang)
        {
            // TODO : Control newGang dosen't hire by player / rivals
            if ((newGang != null) && !this.m_PlayerGangList.Contains(newGang))
            { this.m_PlayerGangList.Add(newGang); }
        }

        /// <summary>
        /// Get a player gang.
        /// </summary>
        /// <param name="gangID">Position of the gang into the list.</param>
        /// <returns>HJirable <see cref="Gang"/> in gangID position into the list.</returns>
        public Gang GetGang(int gangID)
        {
            // No gang in player list or less gang than requested number?
            if (this.m_PlayerGangList.Count.Equals(0) || (this.m_PlayerGangList.Count <= gangID))
            { return null; }

            return this.m_PlayerGangList[gangID];
        }

        /// <summary>
        /// Removed a controlled gang completely from service
        /// </summary>
        /// <param name="gangID">Position of gang to remove in list.</param>
        public void RemoveGang(int gangID)
        {
            this.RemoveGang(this.GetGang(gangID));
        }
        /// <summary>
        /// Removed a controlled gang completely from service
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to remove.</param>
        public void RemoveGang(Gang gang)
        {
            if (gang != null)
            {
                this.m_PlayerGangList.Remove(gang);
            }
        }

        /// <summary>
        /// Hired a recruitable gang, so add it to player gangs
        /// </summary>
        /// <param name="gangID">Hire gang position into hire gang list</param>
        public void HireGang(int gangID)
        {
            this.HireGang(this.GetHireableGang(gangID));
        }

        /// <summary>
        /// Hired a recruitable gang, so add it to player gangs
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to hire.</param>
        public void HireGang(Gang gang)
        {
            if (gang != null && this.m_HireableGangList.Contains(gang) && !this.m_PlayerGangList.Contains(gang))
            {
                gang.m_Combat = gang.m_AutoRecruit = false;
                gang.m_LastMissID = GangMissions.NONE;
                if (gang.MemberNum <= 5)
                {
                    gang.m_MissionID = GangMissions.RECRUIT;
                }
                else
                {
                    gang.m_MissionID = GangMissions.GUARDING;
                }
                this.AddGang(gang);
                this.RemoveHireableGang(gang);
            }
        }
 
        #endregion

        #region Hireable gang
        /// <summary>
        /// Add a gang to Hireable gang list.
        /// </summary>
        /// <param name="newGang"><see cref="Gang"/> to add.</param>
        public void AddHireableGang(Gang newGang)
        {
            // TODO : Control newGang dosen't hire by player / rivals
            if ((newGang != null) && !this.m_HireableGangList.Contains(newGang))
            { this.m_HireableGangList.Add(newGang); }
        }

        /// <summary>
        /// Gets a recruitable gang.
        /// </summary>
        /// <param name="gangID">Position of the gang into the list.</param>
        /// <returns>HJirable <see cref="Gang"/> in gangID position into the list.</returns>
        public Gang GetHireableGang(int gangID)
        {
            // No gang to hire or less gang to hire than requested number?
            if (this.m_HireableGangList.Count.Equals(0) || (this.m_HireableGangList.Count <= gangID))
            { return null; }

            return this.m_HireableGangList[gangID];
        }

        /// <summary>
        /// Removed a recruitable gang from the list
        /// </summary>
        /// <param name="gangID">Position of gang to remove in list.</param>
        public void RemoveHireableGang(int gangID)
        {
            this.RemoveHireableGang(this.GetHireableGang(gangID));
        }
        /// <summary>
        /// Removed a recruitable gang from the list
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to remove.</param>
        public void RemoveHireableGang(Gang gang)
        {
            if (gang != null)
            {
                this.m_HireableGangList.Remove(gang);
            }
        }

        #endregion

        /// <summary>
        /// Fired a gang, so send it back to recruitables (or just delete if full up)
        /// </summary>
        /// <param name="gangID">Hire gang position into hire gang list</param>
        public void FireGang(int gangID)
        {
            this.FireGang(this.GetGang(gangID));
        }
        /// <summary>
        /// Fired a gang, so send it back to recruitables (or just delete if full up)
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to hire.</param>
        public void FireGang(Gang gang)
        {
            if (gang != null)
            {
                if (this.m_HireableGangList.Count < cConfig.Instance.Gangs.MaxRecruitList)
                {
                    gang.m_Combat = gang.m_AutoRecruit = false;
                    gang.m_LastMissID = GangMissions.NONE;
                    this.AddHireableGang(gang);
                }
                this.RemoveGang(gang);
            }
        }// 

        /// <summary>
        /// Get the number of player gang.
        /// </summary>
        /// <returns>Number of player gang.</returns>
        public int GetNumGangs()
        {
            return m_PlayerGangList.Count;
        }
        public int GetMaxNumGangs()
        {
            m_MaxNumGangs = 7 + GameEngine.Game.g_Brothels.GetNumBrothels();
            return m_MaxNumGangs;
        }
        /// <summary>
        /// Get the number of gang to hire.
        /// </summary>
        /// <returns>Number of gang to hire.</returns>
        public int GetNumHireableGangs()
        {
            return m_HireableGangList.Count;
        }
        public Gang GetTempGang()
        {
            Gang newGang = new Gang();
            newGang.MemberNum = WMRandom.Next() % 6 + 10;
            foreach (EntitySkill item in newGang.Skills)
            {
                item.Value = (WMRandom.Next() % 30) + 21;
            }
            foreach ( EntityStat item in newGang.Stats)
            {
                item.Value = (WMRandom.Next() % 30) + 21;
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;
            return newGang;
        } // creates a new gang

        /// <summary>
        /// Creates a new gang with stat/skill mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public Gang GetTempGang(int mod)
        {
            Gang newGang = new Gang();
            newGang.MemberNum = Math.Min(15, WMRandom.Bell(6, 18));
            foreach (EntitySkill item in newGang.Skills)
            {
                item.Value = (WMRandom.Next() % 40) + 21 + (WMRandom.Next() % mod);
                item.Value = Math.Max(Math.Min(item.Value, 100), 1);
            }
            foreach (EntityStat item in newGang.Stats)
            {
                item.Value = (WMRandom.Next() % 40) + 21 + (WMRandom.Next() % mod);
                item.Value = Math.Max(Math.Min(item.Value, 100), 1);
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;

            return newGang;
        }

        //Used by the new brothel security code
        public Gang GetTempWeakGang()
        {
            // MYR: Weak gangs attack girls when they work
            Gang newGang = new Gang();
            newGang.MemberNum = 15;
            foreach (EntitySkill item in newGang.Skills)
            {
                item.Value = WMRandom.Next() % 30 + 51;
            }
            foreach (EntityStat item in newGang.Stats)
            {
                item.Value = WMRandom.Next() % 30 + 51;
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            return newGang;
        }
        /// <summary>
        /// Get randomly a gang from a gang list.
        /// </summary>
        /// <param name="gangList">List of gang.</param>
        /// <returns><see cref="Gang"/> randomly selected from <paramref name="gangList"/>.</returns>
        public Gang RandomGang(List<Gang> gangList)
        {
            if (gangList == null || gangList.Count.Equals(0))
            { return null; }

            return gangList[WMRandom.Next(gangList.Count)];
        }

        #region TODO : code to convert
        /// <summary>
        /// Simple function to increase a gang's combat skills a bit.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to boost skill.</param>
        /// <param name="count">Number of skill to boost.</param>
        public void BoostGangCombatSkills(Gang gang, int count)
        {
            //List<WMaster.GameConcept.AbstractConcept.IValuableAttribut> possibleSkills = new List<WMaster.GameConcept.AbstractConcept.IValuableAttribut>;

            //possibleSkills.Add(gang.Skills[EnumSkills.COMBAT]);
            //possibleSkills.Add(gang.Skills[EnumSkills.MAGIC]);
            //possibleSkills.Add(gang.Stats[EnumStats.AGILITY]);
            //possibleSkills.Add(gang.Stats[EnumStats.CONSTITUTION]);

            //BoostGangRandomSkill(possibleSkills, count, 1);
            //possibleSkills.Clear();
        }
        /// <summary>
        /// Chooses from the passed skills/stats and raises one or more of them
        /// </summary>
        /// <param name="possibleSkills"></param>
        /// <param name="count"></param>
        /// <param name="boost_count"></param>
        public void BoostGangRandomSkill(List<WMaster.GameConcept.AbstractConcept.IValuableAttribut> possibleSkills, int count, int boost_count)
        {
            ///*
            //*	Which of the passed skills/stats will be raised this time?
            //*	Hopefully they'll tend to focus a bit more on what they're already good at...
            //*	that way, they will have strengths instead of becoming entirely homogenized
            //*
            //*	ex. 60 combat, 50 magic, and 40 intelligence: squared, that comes to 3600, 2500 and 1600...
            //*		so: ~46.75% chance combat, ~32.46% chance magic, ~20.78% chance intelligence
            //*/
            //for (int j = 0; j < count; j++) // we'll pick and boost a skill/stat "count" number of times
            //{
            //    //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
            //    //ORIGINAL LINE: int *affect_skill = 0;
            //    int affect_skill = 0;
            //    int total_chance = 0;
            //    List<WMaster.GameConcept.AbstractConcept.IValuableAttribut> chance = new List<WMaster.GameConcept.AbstractConcept.IValuableAttribut>();

            //    foreach (WMaster.GameConcept.AbstractConcept.IValuableAttribut item in possibleSkills)
            //    { // figure chances for each skill/stat; more likely to choose those they're better at
            //        chance.Add((int)Math.Pow((float)possibleSkills[i].Value, 2));
            //        total_chance += chance[i];
            //    }
            //    int choice = WMRandom.Next(total_chance);

            //    total_chance = 0;
            //    for (int i = 0; i < (int)chance.Count; i++)
            //    {
            //        if (choice < (chance[i] + total_chance))
            //        {
            //            affect_skill = possible_skills[i];
            //            break;
            //        }
            //        total_chance += chance[i];
            //    }
            //    /*
            //    *	OK, we've picked a skill/stat. Now to boost it however many times were specified
            //    */
            //    BoostGangSkill(affect_skill, boost_count);
            //}
        }
        /// <summary>
        /// Increases a specific skill/stat the specified number of times
        /// </summary>
        /// <param name="affect_skill"></param>
        /// <param name="count"></param>
        public void BoostGangSkill(int affect_skill, int count)
        {
            ///*
            //*	OK, we've been passed a skill/stat. Now to raise it an amount depending on how high the
            //*	skill/stat already is. The formula is fairly simple.
            //*	Where x = current skill level, and y = median boost amount:
            //*	y = (70/x)^2
            //*	If y > 5, y = 5.
            //*	Then, we get a random number ranging from (y/2) to (y*1.5) for the actual boost
            //*	amount.
            //*	Of course, we can't stick a floating point number into a char/int, so instead we
            //*	use the remaining decimal value as a percentage chance for 1 more point. For
            //*	example, 3.57 would be 3 points guaranteed, with 57% chance to instead get 4 points.
            //*
            //*	ex. 1: 50 points in skill. (70/50)^2 = 1.96. Possible point range: 0.98 to 2.94
            //*	ex. 2: 30 points in skill. (70/30)^2 = 5.44. Possible point range: 2.72 to 8.16
            //*	ex. 3: 75 points in skill. (70/75)^2 = 0.87. Possible point range: 0.44 to 1.31
            //*/
            //for (int j = 0; j < count; j++) // we'll boost the skill/stat "count" number of times
            //{
            //    if (affect_skill < 1)
            //    {
            //        affect_skill = 1;
            //    }

            //    double boost_amount = Math.Pow(70 / (double)affect_skill, 2);
            //    if (boost_amount > 5)
            //    {
            //        boost_amount = 5;
            //    }

            //    boost_amount = (double)WMRandom.InRange((int)((boost_amount / 2) * 100), (int)((boost_amount * 1.5) * 100)) / 100;
            //    int one_more = WMRandom.Percent((int)((boost_amount - (int)boost_amount) * 100)) ? 1 : 0;
            //    int final_boost = (int)(boost_amount + one_more);

            //    affect_skill += final_boost;

            //    if (affect_skill > 100)
            //    {
            //        affect_skill = 100;
            //    }
            //}
        }
        #endregion

        /// <summary>
        /// angBrawl - returns true if gang1 wins and false if gang2 wins
        /// If the Player's gang is in the fight, make sure it is the first gang
        /// If two Rivals are fighting set rivalVrival to true
        /// </summary>
        /// <param name="gang1">First or player gang to fight.</param>
        /// <param name="gang2">Second gang to fight.</param>
        /// <param name="rivalVrival"><b>True</b> if <paramref name="gang1"/> and <paramref name="gang2"/> are rivals gangs.</param>
        /// <returns></returns>
        public bool GangBrawl(Gang gang1, Gang gang2, bool rivalVrival)
        {
            if (gang1 == null || gang1.MemberNum < 1)
            {
                return false; // gang1 does not exist
            }
            if (gang2 == null || gang2.MemberNum < 1)
            {
                return true; // gang2 does not exist
            }

            cTariff tariff = new cTariff();
            // Player's gang or first gang if rivalVrival = true
            gang1.m_Combat = true;
            EnumSkills g1attack = EnumSkills.COMBAT;
            int initalNumber1 = gang1.MemberNum;
            int g1dodge = gang1.Stats[EnumStats.AGILITY].Value;
            if (rivalVrival)
            {
                gang1.m_Heal_Limit = 10;
            }
            int g1SwordLevel = (rivalVrival ? Math.Min(5, (WMRandom.Next() % (gang1.Skills[EnumSkills.COMBAT].Value / 20) + 1)) : m_SwordLevel);

            gang2.m_Combat = true;
            EnumSkills g2attack = EnumSkills.COMBAT;
            int initalNumber2 = gang2.MemberNum;
            int g2dodge = gang2.Stats[EnumStats.AGILITY].Value;
            gang2.m_Heal_Limit = 10;
            int g2SwordLevel = Math.Min(5, (WMRandom.Next() % (gang2.Skills[EnumSkills.COMBAT].Value / 20) + 1));

            int tmp = (gang1.MemberNum > gang2.MemberNum) ? gang1.MemberNum : gang2.MemberNum; // get the largest gang's number

            for (int i = 0; i < tmp; i++) // for each gang member in the largest gang
            {
                int g1Health = 100;
                int g1Mana = 100;
                int g2Health = 100;
                int g2Mana = 100;
                g1attack = EnumSkills.MAGIC;
                g2attack = EnumSkills.MAGIC;

                while (g1Health > 0 && g2Health > 0)
                {
                    // set what they attack with
                    g1attack = g1Mana <= 0 ? EnumSkills.COMBAT : EnumSkills.MAGIC;
                    g2attack = g2Mana <= 0 ? EnumSkills.COMBAT : EnumSkills.MAGIC;

                    // gang1 attacks
                    if (g1attack == EnumSkills.MAGIC)
                    {
                        g1Mana -= 7; // spend the mana before attacking
                    }
                    if (WMRandom.Percent(gang1.Skills[g1attack].Value))
                    {
                        int damage = (g1SwordLevel + 1) * Math.Max(1, gang1.strength() / 10);
                        if (g1attack == EnumSkills.MAGIC)
                        {
                            damage += gang1.Skills[EnumSkills.MAGIC].Value / 10 + 3;
                        }

                        // gang 2 attempts Dodge
                        if (!WMRandom.Percent(g2dodge))
                        {
                            damage = Math.Max(1, (damage - (gang2.Stats[EnumStats.CONSTITUTION].Value / 15)));
                            g2Health -= damage;
                        }
                    }

                    // gang2 use healing potions
                    if (gang2.HealLimit > 0 && g2Health <= 40)
                    {
                        gang2.AdjustHealLimit(-1);
                        g2Health += 30;
                    }

                    // gang2 Attacks
                    if (g2attack == EnumSkills.MAGIC)
                    {
                        g2Mana -= 7; // spend the mana before attacking
                    }
                    if (WMRandom.Percent(gang2.Skills[g2attack].Value))
                    {
                        int damage = (g2SwordLevel + 1) * Math.Max(1, gang2.Strength / 10);
                        if (g2attack == EnumSkills.MAGIC)
                        {
                            damage += gang2.Skills[EnumSkills.MAGIC].Value / 10 + 3;
                        }

                        if (!WMRandom.Percent(g1dodge))
                        {
                            damage = Math.Max(1, (damage - (gang1.Stats[EnumStats.CONSTITUTION].Value / 15)));
                            g1Health -= damage;
                        }
                    }

                    // gang1 use healing potions
                    if (gang1.HealLimit > 0 && g1Health <= 40)
                    {
                        gang1.AdjustHealLimit(-1);
                        if (!rivalVrival)
                        {
                            m_NumHealingPotions--;
                        }
                        g1Health += 30;
                    }

                    g1dodge = Math.Max(0, g1dodge - 1); // degrade gang1 dodge ability
                    g2dodge = Math.Max(0, g2dodge - 1); // degrade gang2 dodge ability
                }

                if (g2Health <= 0)
                {
                    gang2.MemberNum--;
                }
                if (gang2.MemberNum == 0)
                {
                    BoostGangCombatSkills(gang1, 3); // win by KO, boost 3 skills
                    return true;
                }

                if (g1Health <= 0)
                {
                    gang1.MemberNum--;
                }
                if (gang1.MemberNum == 0)
                {
                    BoostGangCombatSkills(gang2, 3); // win by KO, boost 3 skills
                    return false;
                }

                if ((initalNumber2 / 2) > gang2.MemberNum) // if the gang2 has lost half its number there is a 40% chance they will run away
                {
                    if (WMRandom.Percent(40))
                    {
                        BoostGangCombatSkills(gang1, 2); // win by runaway, boost 2 skills
                        return true; // the men run away
                    }
                }

                if ((initalNumber1 / 2) > gang1.MemberNum) // if the gang has lost half its number there is a 40% chance they will run away
                {
                    if (WMRandom.Percent(40))
                    {
                        BoostGangCombatSkills(gang2, 2); // win by runaway, boost 2 skills
                        return false; // the men run away
                    }
                }
            }

            return false;
        }

        void SendGang(int gangID, int missID)
        { throw new NotImplementedException(); } // sends a gang on a mission
         Gang GetGangOnMission(int missID)
        { throw new NotImplementedException(); } // gets a gang on the current mission
        Gang GetGangNotFull(int roomfor = 0, bool recruiting = true)
        { throw new NotImplementedException(); } // gets a gang with room to spare
        Gang GetGangRecruitingNotFull(int roomfor = 0)
        { throw new NotImplementedException(); } // gets a gang recruiting with room to spare
        void UpdateGangs()
        { throw new NotImplementedException(); }

        // returns true if the girl wins
        public bool GangCombat(sGirl girl, Gang gang)
        {
            WMLog.Trace("GangManager.GangCombat started!", WMLog.TraceLog.INFORMATION);

            try
            {

                // MYR: Sanity check: Incorporeal is an auto-win.
                if (girl.has_trait("Incorporeal"))
                {
                    girl.m_Stats[(int)EnumStats.HEALTH] = 100;
                    WMLog.Trace(string.Format("Girl vs. Goons: '{0}' is incorporeal, so she wins.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    gang.MemberNum = (int)gang.MemberNum / 2;
                    while (gang.MemberNum > 0) // Do the casualty calculation
                    {
                        if (WMRandom.Percent(40))
                        {
                            gang.MemberNum--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    WMLog.Trace(string.Format("{0}  goons escaped with their lives.", gang.MemberNum), WMLog.TraceLog.INFORMATION);
                    return true;
                }

                if (gang == null || gang.MemberNum == 0)
                {
                    WMLog.Trace("No gang to fight.", WMLog.TraceLog.INFORMATION);
                    return true;
                }

                int dodge = 0;
                EnumSkills attack = EnumSkills.COMBAT; // determined later, defaults to combat
                EnumSkills gattack = EnumSkills.COMBAT;

                int initalNumber = gang.MemberNum;

                attack = (girl.combat() >= girl.magic()) ? SKILL_COMBAT : SKILL_MAGIC; // first determine what she will fight with
                gattack = (gang.Combat >= gang.Magic) ? EnumSkills.COMBAT : EnumSkills.AGIC; // determine how gang will fight

                dodge = Math.Max(0, (girl.agility()) - girl.tiredness());

                int numGoons = gang.MemberNum;
                gang.m_Combat = true;

                /*
                *	don't let a gang use up more than their
                *	fair share in any one combat
                *
                *	limit is recalcualted each time on the number
                *	of potions remaining, restock is at end-of-turn
                *
                *	this means that gangs in combats later in the turn
                *	have fewer potions available.
                */

                WMLog.Trace(string.Format("Girl vs. Goons: {0} fights {1} opponents!", girl.m_Realname, numGoons), WMLog.TraceLog.INFORMATION);
                WMLog.Trace(string.Format("{0}  : Health {1}, Dodge {2}, Mana {3}", girl.m_Realname, girl.health(), dodge, girl.mana()), WMLog.TraceLog.INFORMATION);

                for (int i = 0; i < numGoons; i++)
                {
                    int gHealth = 100;
                    int gDodge = gang.Stats[EnumStats.AGILITY].Value;
                    int gMana = 100;

                    WMLog.Trace(string.Format("Goon #{0}: Health 100, Dodge {1}, Mana", i, gDodge), WMLog.TraceLog.INFORMATION);

                    while (girl.health() >= 20 && gHealth > 0)
                    {
                        // Girl attacks
                        WMLog.Trace(string.Format("    {0} attacks the goon.", girl.m_Realname), WMLog.TraceLog.INFORMATION);

                        if (attack == EnumSkills.MAGIC)
                        {
                            if (girl.mana() < 7)
                            {
                                WMLog.Trace(string.Format("    {0} insufficient mana: using combat.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                            }
                            else
                            {
                                girl.mana(-7);
                                WMLog.Trace(string.Format("    {0} casts a spell; mana now {0}.", girl.m_Realname, girl.mana()), WMLog.TraceLog.INFORMATION);
                            }
                        }
                        else
                        {
                            WMLog.Trace(string.Format("    {0} using physical attack.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                        }

                        int girlAttackChance = GameEngine.Game.g_Girls.GetSkill(girl, (int)attack);
                        int dieRoll = WMRandom.Next();

                        WMLog.Trace(string.Format("    attack chance = {0}.", girlAttackChance), WMLog.TraceLog.INFORMATION);
                        WMLog.Trace(string.Format("    die roll = {0}.", dieRoll), WMLog.TraceLog.INFORMATION);

                        if (dieRoll > girlAttackChance)
                        {
                            WMLog.Trace("      attack fails.", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            int damage = GameEngine.Game.g_Girls.GetCombatDamage(girl, (int)attack);
                            WMLog.Trace(string.Format("      attack hits! base damage is {0}.", damage), WMLog.TraceLog.INFORMATION);

                            /*
                            *				she may improve a little
                            *				(checked every round of combat? seems excessive)
                            */
                            int gain = WMRandom.Next % 2;
                            if (gain != 0)
                            {
                                WMLog.Trace(string.Format("    {0} gains {1} to attack skill.", girl.m_Realname, gain), WMLog.TraceLog.INFORMATION);
                                GameEngine.Game.g_Girls.UpdateSkill(girl, (int)attack, gain);
                            }

                            dieRoll = WMRandom.Next();

                            // Goon attempts Dodge
                            WMLog.Trace(string.Format("    Goon tries to dodge: needs {0} , gets {1} :", gDodge, dieRoll), WMLog.TraceLog.INFORMATION);

                            if (dieRoll <= gDodge)
                            {
                                WMLog.Trace("      Success!", WMLog.TraceLog.INFORMATION);
                            }
                            else
                            {
                                int conMod = gang.Stats[EnumStats.CONSTITUTION].Value / 10;
                                gHealth -= conMod;
                                WMLog.Trace(string.Format("      Failure!" + Environment.NewLine
                                    + "      Goon takes {0} damage, less {1} for CON" + Environment.NewLine
                                    + "      New health value = {1}", damage, conMod), WMLog.TraceLog.INFORMATION);
                            }
                        }

                        // goons use healing potions
                        if (gang.HealLimit > 0 && gHealth <= 40)
                        {
                            gang.AdjustHealLimit(-1);
                            m_NumHealingPotions--;
                            gHealth += 30;
                            WMLog.Trace(string.Format("Goon drinks healing potion: new health value = {0}. Gang has {1} remaining.", gHealth, gang.HealLimit), WMLog.TraceLog.INFORMATION);
                        }

                        // Goon Attacks

                        dieRoll = WMRandom.Next();
                        int goonAttackChance = gang.Skills[gattack].Value;
                        WMLog.Trace("  Goon Attack: ", WMLog.TraceLog.INFORMATION);

                        WMLog.Trace(string.Format("    chance = {0}, die roll = {1}:", goonAttackChance, dieRoll), WMLog.TraceLog.INFORMATION);
                        if (dieRoll > goonAttackChance)
                        {
                            WMLog.Trace("      attack fails!", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            WMLog.Trace("      attack succeeds!", WMLog.TraceLog.INFORMATION);

                            int damage = (m_SwordLevel + 1) * Math.Max(1, gang.Strength / 10);
                            if (gattack == EnumSkills.MAGIC)
                            {
                                if (gMana <= 0)
                                {
                                    gattack = EnumSkills.COMBAT;
                                }
                                else
                                {
                                    damage += 10;
                                    gMana -= 7;
                                }
                            }

                            // girl attempts Dodge
                            if (!WMRandom.Percent(dodge))
                            {
                                damage = Math.Max(1, (damage - (GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.CONSTITUTION) / 15)));
                                GameEngine.Game.g_Girls.UpdateStat(girl, (int)EnumStats.HEALTH, -damage);
                            }
                        }

                        dodge = Math.Max(0, (dodge - 1)); // degrade girls dodge ability
                        gDodge = Math.Max(0, (gDodge - 1)); // degrade goons dodge ability

                        if (girl.health() < 30 && girl.health() > 20)
                        {
                            if (WMRandom.Percent(girl.agility()))
                            {
                                BoostGangCombatSkills(gang, 2);
                                GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -1);
                                return false;
                            }
                        }
                    }

                    if (GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.HEALTH) <= 20)
                    {
                        BoostGangCombatSkills(gang, 2);
                        GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -1);
                        return false;
                    }
                    else
                    {
                        gang.MemberNum--;
                    }

                    if ((initalNumber / 2) > gang.MemberNum) // if the gang has lost half its number there is a 40% chance they will run away
                    {
                        if (WMRandom.Percent(40))
                        {
                            GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);
                            return true; // the men run away
                        }
                    }
                    if (gang.MemberNum == 0)
                    {
                        GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);
                        return true;
                    }
                }

                WMLog.Trace(string.Format("No more opponents: {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);

                GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);

                return true;
            }
            finally
            {
                WMLog.Trace("GangManager.GangCombat ended!", WMLog.TraceLog.INFORMATION);
            }
        }

        public IXmlElement SaveGangsXML(IXmlElement pRoot)
        {
            throw new NotImplementedException();
            //TiXmlElement pGangManager = new TiXmlElement("Gang_Manager");
            //pRoot.LinkEndChild(pGangManager);

            //TiXmlElement pGangs = new TiXmlElement("Gangs");
            //pGangManager.LinkEndChild(pGangs);
            //sGang gang = m_GangStart;
            //while (gang != null)
            //{
            //    TiXmlElement pGang = gang.SaveGangXML(pGangs);
            //    pGang.SetAttribute("MissionID", gang.m_MissionID);
            //    pGang.SetAttribute("LastMissID", gang.m_LastMissID);
            //    pGang.SetAttribute("Combat", gang.m_Combat);
            //    pGang.SetAttribute("AutoRecruit", gang.m_AutoRecruit);
            //    gang = gang.m_Next;
            //}
            //TiXmlElement pHireables = new TiXmlElement("Hireables");
            //pGangManager.LinkEndChild(pHireables);
            //sGang hgang = m_HireableGangStart;
            //while (hgang != null)
            //{
            //    hgang.SaveGangXML(pHireables);
            //    hgang = hgang.m_Next;
            //}

            //pGangManager.SetAttribute("BusinessesExtort", m_BusinessesExtort);
            //pGangManager.SetAttribute("SwordLevel", m_SwordLevel);
            //pGangManager.SetAttribute("NumHealingPotions", m_NumHealingPotions);
            //pGangManager.SetAttribute("NumNets", m_NumNets);
            //pGangManager.SetAttribute("KeepHealStocked", m_KeepHealStocked);
            //pGangManager.SetAttribute("KeepNetsStocked", m_KeepNetsStocked);

            //// `J` added for .06.01.10
            //if (m_Gang_Gets_Girls == 0 && m_Gang_Gets_Items == 0 && m_Gang_Gets_Beast == 0)
            //{
            //    m_Control_Gangs = cfg.catacombs.control_gangs();
            //    m_Gang_Gets_Items = (int)cfg.catacombs.gang_gets_items();
            //    m_Gang_Gets_Beast = (int)cfg.catacombs.gang_gets_beast();
            //    m_Gang_Gets_Girls = 100 - m_Gang_Gets_Items - m_Gang_Gets_Beast;

            //}
            //pGangManager.SetAttribute("ControlCatacombs", m_Control_Gangs);
            //pGangManager.SetAttribute("Gang_Gets_Girls", m_Gang_Gets_Girls);
            //pGangManager.SetAttribute("Gang_Gets_Items", m_Gang_Gets_Items);
            //pGangManager.SetAttribute("Gang_Gets_Beast", m_Gang_Gets_Beast);

            //return pGangManager;
        }
        public bool LoadGangsXML(IXmlHandle hGangManager)
        {
            throw new NotImplementedException();
            //Free(); //everything should be init even if we failed to load an XML element
            //TiXmlElement pGangManager = hGangManager.ToElement();
            //if (pGangManager == 0)
            //{
            //    return false;
            //}

            //m_NumGangs = 0; // load goons and goon missions
            //TiXmlElement pGangs = pGangManager.FirstChildElement("Gangs");
            //if (pGangs != null)
            //{
            //    for (TiXmlElement* pGang = pGangs.FirstChildElement("Gang"); pGang != 0; pGang = pGang.NextSiblingElement("Gang"))
            //    {
            //        sGang gang = new sGang();
            //        bool success = gang.LoadGangXML(TiXmlHandle(pGang));
            //        if (success)
            //        {
            //            AddGang(gang);
            //        }
            //        else
            //        {
            //            gang = null;
            //            continue;
            //        }
            //    }
            //}
            //m_NumHireableGangs = 0; // load hireable goons
            //TiXmlElement pHireables = pGangManager.FirstChildElement("Hireables");
            //if (pHireables != null)
            //{
            //    for (TiXmlElement* pGang = pHireables.FirstChildElement("Gang"); pGang != 0; pGang = pGang.NextSiblingElement("Gang"))
            //    {
            //        sGang hgang = new sGang();
            //        bool success = hgang.LoadGangXML(TiXmlHandle(pGang));
            //        if (success)
            //        {
            //            AddHireableGang(hgang);
            //        }
            //        else
            //        {
            //            hgang = null;
            //            continue;
            //        }
            //    }
            //}

            //pGangManager.QueryIntAttribute("BusinessesExtort", m_BusinessesExtort);
            //pGangManager.QueryIntAttribute("SwordLevel", m_SwordLevel);
            //pGangManager.QueryIntAttribute("NumHealingPotions", m_NumHealingPotions);
            //pGangManager.QueryIntAttribute("NumNets", m_NumNets);
            //pGangManager.QueryIntAttribute("KeepHealStocked", m_KeepHealStocked);
            //pGangManager.QueryIntAttribute("KeepNetsStocked", m_KeepNetsStocked);

            //// `J` added for .06.01.10
            //pGangManager.QueryValueAttribute<bool>("ControlCatacombs", m_Control_Gangs);
            //pGangManager.QueryIntAttribute("Gang_Gets_Girls", m_Gang_Gets_Girls);
            //pGangManager.QueryIntAttribute("Gang_Gets_Items", m_Gang_Gets_Items);
            //pGangManager.QueryIntAttribute("Gang_Gets_Beast", m_Gang_Gets_Beast);
            //if ((m_Gang_Gets_Girls == 0 && m_Gang_Gets_Items == 0 && m_Gang_Gets_Beast == 0) || m_Gang_Gets_Girls + m_Gang_Gets_Items + m_Gang_Gets_Beast != 100)
            //{
            //    m_Control_Gangs = cfg.catacombs.control_gangs();
            //    m_Gang_Gets_Items = (int)cfg.catacombs.gang_gets_items();
            //    m_Gang_Gets_Beast = (int)cfg.catacombs.gang_gets_beast();
            //    m_Gang_Gets_Girls = 100 - m_Gang_Gets_Items - m_Gang_Gets_Beast;
            //}

            //return true;
        }


        public int GetNumBusinessExtorted()
        {
            return m_BusinessesExtort;
        }
        public int NumBusinessExtorted(int n)
        {
            m_BusinessesExtort += n;
            return m_BusinessesExtort;
        }

        //C++ TO C# CONVERTER WARNING: C# has no equivalent to methods returning pointers to value types:
        //ORIGINAL LINE: int* GetWeaponLevel()
        public int GetWeaponLevel()
        {
            return m_SwordLevel;
        }

        //C++ TO C# CONVERTER WARNING: C# has no equivalent to methods returning pointers to value types:
        //ORIGINAL LINE: int* GetNets()
        public int GetNets()
        {
            return m_NumNets;
        }
        public int GetNetRestock()
        {
            return m_KeepNetsStocked;
        }
        public void KeepNetStocked(int stocked)
        {
            m_KeepNetsStocked = stocked;
        }
        int net_limit()
        { throw new NotImplementedException(); }

        //C++ TO C# CONVERTER WARNING: C# has no equivalent to methods returning pointers to value types:
        //ORIGINAL LINE: int* GetHealingPotions()
        public int GetHealingPotions()
        {
            return m_NumHealingPotions;
        }
        public void KeepHealStocked(int stocked)
        {
            m_KeepHealStocked = stocked;
        }
        public int GetHealingRestock()
        {
            return m_KeepHealStocked;
        }
        int healing_limit()
        { throw new NotImplementedException(); }

        bool sabotage_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool recapture_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool extortion_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool petytheft_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool grandtheft_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool kidnapp_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool catacombs_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool gangtraining(Gang gang)
        { throw new NotImplementedException(); }
        bool gangrecruiting(Gang gang)
        { throw new NotImplementedException(); }
        bool service_mission(Gang gang)
        { throw new NotImplementedException(); }
        bool losegang(Gang gang)
        { throw new NotImplementedException(); }
        void check_gang_recruit(Gang gang)
        { throw new NotImplementedException(); }
        void GangStartOfShift()
        { throw new NotImplementedException(); }
        void RestockNetsAndPots()
        { throw new NotImplementedException(); }

        int chance_to_catch(sGirl girl)
        { throw new NotImplementedException(); }

        bool GirlVsEnemyGang(sGirl girl, Gang enemy_gang)
        { throw new NotImplementedException(); }

        List<Gang> gangs_on_mission(int mission_id)
        { throw new NotImplementedException(); }
        List<Gang> gangs_watching_girls()
        { throw new NotImplementedException(); }

        public bool Control_Gangs()
        {
            return m_Control_Gangs;
        }
        public int Gang_Gets_Girls()
        {
            return m_Gang_Gets_Girls;
        }
        public int Gang_Gets_Items()
        {
            return m_Gang_Gets_Items;
        }
        public int Gang_Gets_Beast()
        {
            return m_Gang_Gets_Beast;
        }
        public bool Control_Gangs(bool cg)
        {
            return m_Control_Gangs = cg;
        }
        public int Gang_Gets_Girls(int g)
        {
            return m_Gang_Gets_Girls = g;
        }
        public int Gang_Gets_Items(int g)
        {
            return m_Gang_Gets_Items = g;
        }
        public int Gang_Gets_Beast(int g)
        {
            return m_Gang_Gets_Beast = g;
        }

    }
}
