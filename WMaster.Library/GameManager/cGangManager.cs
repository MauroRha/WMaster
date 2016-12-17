using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;
using WMaster.GameConcept;
using WMaster.GameConcept.AbstractConcept;
using WMaster.GameConcept.Item;
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

        /// <summary>
        /// Number of Nets the gang have.
        /// </summary>
        private int m_NumNets;
        /// <summary>
        /// Get number of Nets the gang have.
        /// </summary>
        /// <returns>Number of Nets the gang have.</returns>
        public int GetNets()
        {
            return m_NumNets;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="cGangManager"/>
        /// TODO : REFACTORING - Make singleton or static class?
        /// </summary>
        public cGangManager()
        {
            m_NumGangNames = 0;

            // TODO : Get number of gang name
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
            //    int name = WMRandom % m_NumGangNames;
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
            // TODO : GAME - Control newGang dosen't hire by player / rivals
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
                gang.HasSeenCombat = gang.AutoRecruit = false;
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
            // TODO : GAME - Control newGang dosen't hire by player / rivals
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
                    gang.HasSeenCombat = gang.AutoRecruit = false;
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
        /// <summary>
        /// Get the maximum number of gang player can have.
        /// </summary>
        /// <returns>Maximum number of gang player can have.</returns>
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

        /// <summary>
        /// Creates a new gang for single use
        /// </summary>
        /// <returns>Temporary new <see cref="Gang"/>.</returns>
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
        }

        /// <summary>
        /// Creates a new weak gang for single use.
        /// <remarks><para>Used by the new brothel security code.</para></remarks>
        /// </summary>
        /// <returns>Temporary new weak <see cref="Gang"/>.</returns>
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

        /// <summary>
        /// Simple function to increase a gang's combat skills a bit.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to boost skill.</param>
        /// <param name="count">Number of skill to boost.</param>
        public void BoostGangCombatSkills(Gang gang, int count)
        {
            List<IValuableAttribut> possibleSkills = new List<IValuableAttribut>();

            possibleSkills.Add(gang.Skills[EnumSkills.COMBAT]);
            possibleSkills.Add(gang.Skills[EnumSkills.MAGIC]);
            possibleSkills.Add(gang.Stats[EnumStats.AGILITY]);
            possibleSkills.Add(gang.Stats[EnumStats.CONSTITUTION]);

            BoostGangRandomSkill(possibleSkills, count, 1);
        }

        /// <summary>
        /// Chooses from the passed skills/stats and raises one or more of them
        /// </summary>
        /// <param name="possibleSkills"></param>
        /// <param name="count"></param>
        /// <param name="boostCount"></param>
        private void BoostGangRandomSkill(List<IValuableAttribut> possibleSkills, int count, int boostCount)
        {
            /*
            *	Which of the passed skills/stats will be raised this time?
            *	Hopefully they'll tend to focus a bit more on what they're already good at...
            *	that way, they will have strengths instead of becoming entirely homogenized
            *
            *	ex. 60 combat, 50 magic, and 40 intelligence: squared, that comes to 3600, 2500 and 1600...
            *		so: ~46.75% chance combat, ~32.46% chance magic, ~20.78% chance intelligence
            */
            if (possibleSkills == null || possibleSkills.Count.Equals(0))
            { return; }

            Dictionary<IValuableAttribut, int> chance = new Dictionary<IValuableAttribut, int>();
            for (int j = 0; j < count; j++) // we'll pick and boost a skill/stat "count" number of times
            {
                IValuableAttribut affectSkill = null;
                chance.Clear();
                int totalChance = 0;

                foreach (IValuableAttribut item in possibleSkills)
                { // figure chances for each skill/stat; more likely to choose those they're better at
                    int calcChance = (int)Math.Pow((float)item.Value, 2);
                    chance.Add(item, calcChance);
                    totalChance += calcChance;
                }
                int choice = WMRandom.Next(totalChance);

                totalChance = 0;
                foreach (IValuableAttribut item in possibleSkills)
                {
                    if (choice < (chance[item] + totalChance))
                    {
                        affectSkill = item;
                        break;
                    }
                    totalChance += chance[item];
                }
                /*
                *	OK, we've picked a skill/stat. Now to boost it however many times were specified
                */
                BoostGangSkill(affectSkill, boostCount);
            }
        }

        /// <summary>
        /// Increases a specific skill/stat the specified number of times.
        /// </summary>
        /// <param name="affectSkill">Skill to increase.</param>
        /// <param name="count">Number of time to increase.</param>
        private void BoostGangSkill(IValuableAttribut affectSkill, int count)
        {
            /*
            *	OK, we've been passed a skill/stat. Now to raise it an amount depending on how high the
            *	skill/stat already is. The formula is fairly simple.
            *	Where x = current skill level, and y = median boost amount:
            *	y = (70/x)^2
            *	If y > 5, y = 5.
            *	Then, we get a random number ranging from (y/2) to (y*1.5) for the actual boost
            *	amount.
            *	Of course, we can't stick a floating point number into a char/int, so instead we
            *	use the remaining decimal value as a percentage chance for 1 more point. For
            *	example, 3.57 would be 3 points guaranteed, with 57% chance to instead get 4 points.
            *
            *	ex. 1: 50 points in skill. (70/50)^2 = 1.96. Possible point range: 0.98 to 2.94
            *	ex. 2: 30 points in skill. (70/30)^2 = 5.44. Possible point range: 2.72 to 8.16
            *	ex. 3: 75 points in skill. (70/75)^2 = 0.87. Possible point range: 0.44 to 1.31
            */
            if (affectSkill == null)
            { return; }

            for (int j = 0; j < count; j++) // we'll boost the skill/stat "count" number of times
            {
                if (affectSkill.Value < 1)
                {
                    affectSkill.Value = 1;
                }

                double boostAmount = Math.Pow(70 / (double)affectSkill.Value, 2);
                if (boostAmount > 5)
                {
                    boostAmount = 5;
                }

                boostAmount = (double)WMRandom.InRange((int)((boostAmount / 2) * 100), (int)((boostAmount * 1.5) * 100)) / 100;
                int oneMore = WMRandom.Percent((int)((boostAmount - (int)boostAmount) * 100)) ? 1 : 0;
                int finalBoost = (int)(boostAmount + oneMore);

                affectSkill.Value += finalBoost;
            }
        }

        /// <summary>
        /// GangBrawl - returns true if gang1 wins and false if gang2 wins
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
            gang1.HasSeenCombat = true;
            EnumSkills g1attack = EnumSkills.COMBAT;
            int initalNumber1 = gang1.MemberNum;
            int g1dodge = gang1.Stats[EnumStats.AGILITY].Value;
            if (rivalVrival)
            {
                gang1.m_Heal_Limit = 10;
            }
            int g1SwordLevel = (rivalVrival ? Math.Min(5, (WMRandom.Next() % (gang1.Skills[EnumSkills.COMBAT].Value / 20) + 1)) : m_SwordLevel);

            gang2.HasSeenCombat = true;
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
                        int damage = (g1SwordLevel + 1) * Math.Max(1, gang1.Strength / 10);
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

                attack = (girl.combat() >= girl.magic()) ? EnumSkills.COMBAT : EnumSkills.MAGIC; // first determine what she will fight with
                gattack = (gang.Combat >= gang.Magic) ? EnumSkills.COMBAT : EnumSkills.MAGIC; // determine how gang will fight

                dodge = Math.Max(0, (girl.agility()) - girl.tiredness());

                int numGoons = gang.MemberNum;
                gang.HasSeenCombat = true;

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
                            int gain = WMRandom.Next() % 2;
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

        // MYR: This is similar to GangCombat, but instead of one of the players gangs
        //      fighting the girl, some random gang attacks her.  This random gang
        //      doesn't have healing potions and the weapon levels of a player gang.
        //      ATM only the new security code uses it.
        //      This will also be needed to be updated to the new way of doing combat.
        // true means the girl won

        public bool GirlVsEnemyGang(sGirl girl, Gang enemyGang)
        {
            // MYR: Sanity check: Incorporeal is an auto-win.
            if (girl.has_trait("Incorporeal"))
            {
                girl.m_Stats[(int)EnumStats.HEALTH] = 100;

                WMLog.Trace(string.Format("Girl vs. Goons: {0} is incorporeal, so she wins.", girl.m_Realname), WMLog.TraceLog.INFORMATION);

                enemyGang.MemberNum = (int)enemyGang.MemberNum / 2;
                while (enemyGang.MemberNum > 0) // Do the casualty calculation
                {
                    if (WMRandom.Percent(40))
                    {
                        enemyGang.MemberNum--;
                    }
                    else
                    {
                        break;
                    }
                }
                WMLog.Trace(string.Format("  {0} goons escaped with their lives.", enemyGang.MemberNum), WMLog.TraceLog.INFORMATION);
                return true;
            }

            int dodge = GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.AGILITY); // MYR: Was 0
            int mana = GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.MANA); // MYR: Like agility, mana is now per battle

            EnumSkills attack = EnumSkills.COMBAT; // determined later, defaults to combat
            EnumSkills goonAttack = EnumSkills.COMBAT;

            if (enemyGang == null || enemyGang.MemberNum == 0)
            {
                return true;
            }

            // first determine what she will fight with
            if (GameEngine.Game.g_Girls.GetSkill(girl, (int)EnumSkills.COMBAT) > GameEngine.Game.g_Girls.GetSkill(girl, (int)EnumSkills.MAGIC))
            {
                attack = EnumSkills.COMBAT;
            }
            else
            {
                attack = EnumSkills.MAGIC;
            }

            // determine how gang will fight
            if (enemyGang.Skills[EnumSkills.COMBAT].Value > enemyGang.Skills[EnumSkills.MAGIC].Value)
            {
                goonAttack = EnumSkills.COMBAT;
            }
            else
            {
                goonAttack = EnumSkills.MAGIC;
            }

            int initialNum = enemyGang.MemberNum;

            enemyGang.HasSeenCombat = true;


            WMLog.Trace(string.Format("Girl vs. Goons: {0} fights {1} opponents!", girl.m_Realname, initialNum), WMLog.TraceLog.INFORMATION);
            WMLog.Trace(string.Format("{0}: Health {1}, Dodge {2}, Mana {3}",
                girl.m_Realname,
                girl.health(),
                GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.AGILITY),
                girl.mana()), WMLog.TraceLog.INFORMATION);

            for (int i = 0; i < initialNum; i++)
            {
                WMLog.Trace(string.Format("Goon #{0}: Health: {1}, Mana: {2}, Dodge: {3}, Attack:{4}, Constitution:{5}",
                    i,
                    enemyGang.Stats[EnumStats.HEALTH].Value,
                    enemyGang.Stats[EnumStats.MANA].Value,
                    enemyGang.Stats[EnumStats.AGILITY].Value,
                    enemyGang.Skills[goonAttack].Value,
                    enemyGang.Stats[EnumStats.CONSTITUTION].Value), WMLog.TraceLog.INFORMATION);

                int gHealth = enemyGang.Stats[EnumStats.HEALTH].Value;
                int gDodge = enemyGang.Stats[EnumStats.AGILITY].Value;
                int gMana = enemyGang.Stats[EnumStats.MANA].Value;

                while (GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.HEALTH) >= 20 && gHealth > 0)
                {
                    // Girl attacks
                    WMLog.Trace(string.Format("  {0} attacks the goon.", girl.m_Realname), WMLog.TraceLog.INFORMATION);

                    if (attack == EnumSkills.MAGIC)
                    {

                        if (mana < 5)
                        {
                            attack = EnumSkills.COMBAT;
                            WMLog.Trace(string.Format("    {0} insufficient mana: using combat.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {

                            mana = mana - 5;
                            WMLog.Trace(string.Format("    {0} casts a spell; mana now {0}.", girl.m_Realname, mana), WMLog.TraceLog.INFORMATION);
                        }
                    }
                    else
                    {
                        WMLog.Trace(string.Format("    {0} using physical attack.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    }

                    int girlAttackChance = GameEngine.Game.g_Girls.GetSkill(girl, (int)attack);

                    int dieRoll = WMRandom.Next();

                    WMLog.Trace(string.Format("    attack chance {0} die roll:{1}.", girlAttackChance, dieRoll), WMLog.TraceLog.INFORMATION);

                    if (dieRoll > girlAttackChance)
                    {
                        WMLog.Trace("      attack misses.", WMLog.TraceLog.INFORMATION);
                    }
                    else
                    {
                        int damage = GameEngine.Game.g_Girls.GetCombatDamage(girl, (int)attack);

                        dieRoll = WMRandom.Next();

                        // Goon attempts Dodge
                        WMLog.Trace(string.Format("    Goon tries to dodge: needs {0}, gets {1}.", gDodge, dieRoll), WMLog.TraceLog.INFORMATION);

                        // Dodge maxes out at 95%
                        if ((dieRoll <= gDodge) && (dieRoll <= 95))
                        {
                            WMLog.Trace("    success!", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            int conMod = enemyGang.Stats[EnumStats.CONSTITUTION].Value / 20;
                            damage -= conMod;
                            if (damage <= 0) // MYR: Minimum 1 damage on hit
                            {
                                damage = 1;
                            }
                            gHealth -= damage;
                            WMLog.Trace(string.Format("    Goon takes {0}. New health value: {1}.", damage, gHealth), WMLog.TraceLog.INFORMATION);
                        }
                    }

                    if (gHealth <= 0) // Goon may have been killed by damage above
                    {
                        continue;
                    }

                    // Goon Attacks

                    dieRoll = WMRandom.Next();
                    WMLog.Trace("  Goon Attack:", WMLog.TraceLog.INFORMATION);
                    WMLog.Trace(string.Format("    chance:{0}, die roll:{1}.", (int)enemyGang.Skills[goonAttack].Value, dieRoll), WMLog.TraceLog.INFORMATION);

                    if (dieRoll > enemyGang.Skills[goonAttack].Value)
                    {
                        WMLog.Trace("    attack fails!", WMLog.TraceLog.INFORMATION);
                    }
                    else
                    {
                        WMLog.Trace("    attack succeeds!", WMLog.TraceLog.INFORMATION);

                        // MYR: Goon damage calculation is different from girl's.  Do we care?
                        int damage = 5 + enemyGang.Skills[goonAttack].Value / 10;

                        if (goonAttack == EnumSkills.MAGIC)
                        {
                            if (gMana < 10)
                            {
                                goonAttack = EnumSkills.COMBAT;
                            }
                            else
                            {
                                damage += 8;
                                gMana -= 10;
                            }
                        }

                        // girl attempts Dodge
                        dieRoll = WMRandom.Next();

                        WMLog.Trace(string.Format("    {0} tries to dodge: needs {1}, gets {2}.", girl.m_Realname, dodge, dieRoll), WMLog.TraceLog.INFORMATION);

                        // MYR: Girl dodge maxes out at 90 (Gang dodge at 95).  It's a bit of a hack
                        if (dieRoll <= dodge && dieRoll <= 90)
                        {
                            WMLog.Trace("    succeeds!", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            GameEngine.Game.g_Girls.TakeCombatDamage(girl, -damage); // MYR: Note change

                            WMLog.Trace(string.Format("  {0} takes {1}. New health value: {2}.", girl.m_Realname, damage, dieRoll, girl.health()), WMLog.TraceLog.INFORMATION);
                            if (girl.has_trait("Incorporeal"))
                            {
                                WMLog.Trace("    (Girl is Incorporeal)", WMLog.TraceLog.INFORMATION);
                            }
                        }
                    }

                    // update girls dodge ability
                    if ((dodge - 1) < 0)
                    {
                        dodge = 0;
                    }
                    else
                    {
                        dodge--;
                    }

                    // update goons dodge ability
                    if ((gDodge - 1) < 0)
                    {
                        gDodge = 0;
                    }
                    else
                    {
                        gDodge--;
                    }
                } // While loop

                if (GameEngine.Game.g_Girls.GetStat(girl, (int)EnumStats.HEALTH) <= 20)
                {
                    WMLog.Trace(string.Format("The gang overwhelmed and defeated {0}. She lost the battle.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -5);
                    return false;
                }
                else
                {
                    enemyGang.MemberNum--; // Gang casualty
                }

                // if the gang has lost half its number there is a chance they will run away
                // This is checked for every member killed over 50%
                if ((initialNum / 2) > enemyGang.MemberNum)
                {
                    if (WMRandom.Percent(50)) // MYR: Adjusting this has a big effect
                    {
                        WMLog.Trace(string.Format("The gang ran away after losing too many members. {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                        GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);
                        return true; // the men run away
                    }
                }
                // Gang fought to the death
                if (enemyGang.MemberNum == 0)
                {
                    WMLog.Trace(string.Format("The gang fought to bitter end. They are all dead. {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);
                    return true;
                }
            }

            WMLog.Trace(string.Format("No more opponents: {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);

            GameEngine.Game.g_Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);

            return true;
        }
        /// <summary>
        /// Get the number of business extorted.
        /// </summary>
        /// <returns>Number of business extorted.</returns>
        public int GetNumBusinessExtorted()
        {
            return m_BusinessesExtort;
        }
        /// <summary>
        /// Adjuste number of business extorted.
        /// </summary>
        /// <param name="n">Adjustement value of number of business extorted.</param>
        /// <returns>Number of business extorted.</returns>
        public int NumBusinessExtorted(int n)
        {
            // TODO : GAME - Check m_BusinessesExtort < Max value
            m_BusinessesExtort += n;
            return m_BusinessesExtort;
        }

        public int GetWeaponLevel()
        {
            return m_SwordLevel;
        }
        public int GetNetRestock()
        {
            return m_KeepNetsStocked;
        }
        public void KeepNetStocked(int stocked)
        {
            m_KeepNetsStocked = stocked;
        }

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

        /// <summary>
        /// Get the max number of healpotion a gang can use. (?)
        /// </summary>
        /// <remarks><para>`J` replaced with passing out of pots/nets in GangStartOfShift() for .06.01.09</para></remarks>
        /// <returns>Max number of healpotion a gang can use.</returns>
        public int HealingLimit()
        {
            if (m_PlayerGangList.Count.Equals(0) || m_NumHealingPotions  < 1)
            {
                return 0;
            }
            int limit;
            // take the number of potions and divide by the the number of gangs
            limit = m_NumHealingPotions / m_PlayerGangList.Count;
            /*
            *	if that rounds to less than zero, and there are still
            *	potions available, make sure they get at least one to use
            */
            if ((limit < 1) && (m_NumHealingPotions > 0))
            {
                limit = 1;
            }
            return limit;
        }

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

        /// <summary>
        /// Get the number of net a gang can use (?)
        /// <remarks><para>`J` - Added for .06.01.09</para></remarks>
        /// </summary>
        /// <returns>Number of net a gang can use</returns>
        public int NetLimit()
        {
            if (m_PlayerGangList.Count < 1 || m_NumNets < 1)
            {
                return 0;
            }
            int limit;
            // take the number of nets and divide by the the number of gangs
            limit = m_NumNets / m_PlayerGangList.Count;
            /*
            *	if that rounds to less than zero, and there are still
            *	nets available, make sure they get at least one to use
            */
            if ((limit < 1) && (m_NumNets > 0))
            {
                limit = 1;
            }
            return limit;
        }

        // ----- Mission related
        /// <summary>
        /// Update hureable ganglist and launch mission for player gang.
        /// <remarks><para>Missions done here - Updated for .06.01.09</para></remarks>
        /// </summary>
        public void UpdateGangs()
        {
            cTariff tariff = new cTariff();

            // maintain recruitable gangs list, potentially pruning some old ones
            int removeChance = GameEngine.Game.cfg.Gangs.ChanceRemoveUnwanted;
            foreach(Gang item in m_HireableGangList)
            {
                if (WMRandom.Percent(removeChance))
                {
                    WMLog.Trace(string.Format("Culling recruitable gang: {0}", item.Name), WMLog.TraceLog.INFORMATION);
                    RemoveHireableGang(item);
                }
            }

            // maybe add some new gangs to the recruitable list
            int addMin = GameEngine.Game.cfg.Gangs.AddNewWeeklyMin;
            int addMax = GameEngine.Game.cfg.Gangs.AddNewWeeklyMax;
            int addRecruits = WMRandom.Bell(addMin, addMax);
            for (int i = 0; i < addRecruits; i++)
            {
                if (m_HireableGangList.Count >= GameEngine.Game.cfg.Gangs.MaxRecruitList)
                {
                    break;
                }
                WMLog.Trace("Adding new recruitable gang.", WMLog.TraceLog.INFORMATION);
                AddNewGang(false);
            }

            // now, deal with player controlled gangs on missions
            foreach (Gang item in m_PlayerGangList)
            {
                switch (item.m_MissionID)
                {
                    case GangMissions.GUARDING: // these are handled in GangStartOfShift()
                    case GangMissions.SPYGIRLS:
                        break;
                    case GangMissions.CAPTUREGIRL:
                        if (!GameEngine.Game.g_Brothels.RunawaysGirlList.Count.Equals(0))
                        {
                            recapture_mission(item);
                            break;
                        }
                        else
                        {
                            // TODO : TRADUCTION - Localisation of message
                            item.m_Events.AddMessage("This gang was sent to look for runaways but there are none so they went looking for any girl to kidnap instead.", (int)ImageTypes.PROFILE, Constants.EVENT_GANG);
                            kidnapp_mission(item);
                            break;
                        }
                    case GangMissions.KIDNAPP:
                        kidnapp_mission(item);
                        break;
                    case GangMissions.SABOTAGE:
                        sabotage_mission(item);
                        break;
                    case GangMissions.EXTORTION:
                        extortion_mission(item);
                        break;
                    case GangMissions.PETYTHEFT:
                        petytheft_mission(item);
                        break;
                    case GangMissions.GRANDTHEFT:
                        grandtheft_mission(item);
                        break;
                    case GangMissions.CATACOMBS:
                        catacombs_mission(item);
                        break;
                    case GangMissions.TRAINING:
                        gangtraining(item);
                        break;
                    case GangMissions.RECRUIT:
                        gangrecruiting(item);
                        break;
                    case GangMissions.SERVICE:
                        service_mission(item);
                        break;
                    default:
                        // TODO : TRADUCTION - Localisation of message
                        item.m_Events.AddMessage(string.Format("Error: no mission set or mission not found : {0}", item.m_MissionID),
                            (int)ImageTypes.PROFILE, Constants.EVENT_GANG);
                        continue;
                }

                if (losegang(item))
                {
                    continue; // if they all died, move on.
                }
                if (item.HasSeenCombat == false && item.MemberNum < 15)
                {
                    item.MemberNum++;
                }
                check_gang_recruit(item);
            }

            GameEngine.Game.g_Brothels.m_Rivals.Update(m_BusinessesExtort); // Update the rivals

            RestockNetsAndPots();
        }

        /// <summary>
        /// Restock gang's net and healing.
        /// <remarks><para>`J` restock at the start and end of the gang shift - Added for .06.01.09.</para></remarks>
        /// </summary>
        public void RestockNetsAndPots()
        {
            cTariff tariff = new cTariff();

            WMLog.Trace(string.Format("Time to restock heal potions and nets{0}Heal Flag    = {1}{0}Heal Target  = {2}{0}Heal Current = {3}{0}Nets Flag    = {4}{0}Nets Target  = {5}{0}Nets Current = {6}"
                , Environment.NewLine, (bool)(m_KeepHealStocked > 0), m_KeepHealStocked, m_KeepHealStocked, (bool)(m_KeepNetsStocked > 0), m_KeepNetsStocked, m_KeepNetsStocked),
                WMLog.TraceLog.INFORMATION);

            if (m_KeepHealStocked > 0 && m_KeepHealStocked > m_NumHealingPotions)
            {
                int diff = m_KeepHealStocked - m_NumHealingPotions;
                m_NumHealingPotions = m_KeepHealStocked;
                GameEngine.Game.Gold.consumable_cost(tariff.healing_price(diff));
            }
            if (m_KeepNetsStocked > 0 && m_KeepNetsStocked > m_NumNets)
            {
                int diff = m_KeepNetsStocked - m_NumNets;
                m_NumNets = m_KeepNetsStocked;
                GameEngine.Game.Gold.consumable_cost(tariff.nets_price(diff));
            }
        }

        /// <summary>
        /// Sends a gang on a mission.
        /// </summary>
        /// <param name="gangID"></param>
        /// <param name="missID"></param>
        [Obsolete("Use void SendGang(Gang gang, int missID)", false)]
        public void SendGang(int gangID, GangMissions mission)
        {
            SendGang(m_PlayerGangList[gangID], mission);
        }

        /// <summary>
        /// Sends a gang on a mission.
        /// </summary>
        /// <param name="gangID">Gang to send on mission.</param>
        /// <param name="missID">Mission to affect to gang.</param>
        public void SendGang(Gang gang, GangMissions mission)
        {
            if (gang == null)
            { return; }

            gang.m_MissionID = mission;
        }

        /// <summary>
        /// Get the first gang on mission.
        /// </summary>
        /// <param name="missID">Mission of gang to find.</param>
        /// <returns>First <see cref="Gang"/> assigned to <paramref name="mission"/></returns>
        public Gang GetGangOnMission(GangMissions mission)
        {
            return m_PlayerGangList
                .Where(g => g.CurrentMission.Equals(mission))
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets a gang with room to spare.
        /// <remarks><para>`J` - Added for .06.02.18</para></remarks>
        /// </summary>
        /// <param name="roomFor">Number of place formember to recruit.</param>
        /// <param name="recruiting"><b>True</b> to take care of <paramref name="roomFor"/></param>
        /// <returns></returns>
        public Gang GetGangNotFull(int roomFor, bool recruiting)
        {
            List<GangMissions> mission = new List<GangMissions>() { GangMissions.RECRUIT, GangMissions.TRAINING, GangMissions.SPYGIRLS, GangMissions.GUARDING, GangMissions.SERVICE };
            foreach (Gang item in m_PlayerGangList)
            {
                if (mission.Contains(item.m_MissionID))
                {
                    // TODO : Replace literal to Max gang member value
                    if (
                        (recruiting && item.MemberNum + roomFor <= 15)
                        ||
                        (!recruiting && item.MemberNum < 15)
                        )
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a gang recruiting with room to spare.
        /// </summary>
        /// <param name="roomFor">Number of free room in gang to find.</param>
        /// <returns><see cref="Gang"/> with room to spare</returns>
        public Gang GetGangRecruitingNotFull(int roomFor)
        {
            List<GangMissions> mission = new List<GangMissions>() { GangMissions.RECRUIT, GangMissions.TRAINING, GangMissions.SPYGIRLS, GangMissions.GUARDING, GangMissions.SERVICE };
            foreach (Gang item in m_PlayerGangList)
            {
                if (item.MemberNum + roomFor <= 15)
                {
                    if (mission.Contains(item.m_MissionID))
                    {
                        return item;
                    }
                }
            }

            // if none are found then get a gang that has room for at least 1
            foreach (Gang item in m_PlayerGangList)
            {
                if (item.MemberNum < 15)
                {
                    if (mission.Contains(item.m_MissionID))
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get a list of all the gangs doing MISS_FOO.
        /// </summary>
        /// <param name="mission">The mission gang must be affected to.</param>
        /// <returns>List of all gang affected to <paramref name="mission"/>.</returns>
        public List<Gang> GangsOnMission(GangMissions mission)
        {
            List<Gang> list = new List<Gang>(); // loop through the gangs
            foreach (Gang item in m_PlayerGangList)
            {
                // if they're doing the job we are looking for, take them
                if (item.m_MissionID.Equals(mission))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        // 
        /// <summary>
        /// Get a list of all the gangs doing watching girls missions.
        /// <remarks><para>`J` - Added for .06.01.09</para></remarks>
        /// </summary>
        /// <returns>List of all gang affected watch girls.</returns>
        public List<Gang> GangsWatchingGirls()
        {
            List<Gang> list = new List<Gang>(); // loop through the gangs
            foreach (Gang item in m_PlayerGangList)
            {
                // if they're doing the job we are looking for, take them
                if (item.m_MissionID.Equals(GangMissions.GUARDING) || item.m_MissionID.Equals(GangMissions.SPYGIRLS))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// two objectives here:
        /// A: multiple squads spying on the girls improves the chance of catching thieves
        /// B: The intelligence of the girl and the goons affects the result
        /// </summary>
        /// <param name="girl"><see cref="sGirl"/> to catch.</param>
        /// <returns>Chance (0 to 100) to catch em.</returns>
        public int ChanceToCatch(sGirl girl)
        {
            int pc = 0;
            List<Gang> gvec = GangsOnMission(GangMissions.SPYGIRLS); // get a vector containing all the spying gangs

            WMLog.Trace(string.Format("GangManager.ChanceToCatch: {0} gangs spying.",gvec.Count), WMLog.TraceLog.INFORMATION);

            foreach (Gang item in gvec)
            {
                /*
                *		now then: the basic chance is 5 * number of goons
                *		but I want to modify that for the intelligence
                *		of the girl, and that of the squad
                */
                float mod = 100.0f + item.Intelligence;
                mod -= girl.intelligence();
                mod /= 100.0f;
                /*
                *		that should give us a multiplier that can
                *		at one extreme, double the chances of the sqaud
                *		catching her, and at the other, reduce it to zero
                */
                pc += (int)(5 * item.MemberNum * mod);

                // GBN : Moving upgrading inteligence stat inside loop to affect all gangs instead of only last of them.
                BoostGangSkill(item.Stats[EnumStats.INTELLIGENCE], 1);
            }
            if (pc > 100)
            {
                pc = 100;
            }
            return pc;
        }

        /// <summary>
        /// Performe a gang sabotage mission against player's rival.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns></returns>
        public bool sabotage_mission(Gang gang)
        {
            // TODO : TRADUCTION - Try to localize event text of mission.
            StringBuilder sabotageEvent = new StringBuilder();
            sabotageEvent.AppendLine(string.Format("Gang   {0}   is attacking rivals.", gang.Name));
            /*
            *	See if they can find any enemy assets to attack
            *
            *	I'd like to add a little more intelligence to this.
            *	Modifiers based on gang intelligence, for instance
            *	Allow a "scout" activity for gangs that improves the
            *	chances of a raid. That sort of thing.
            */
            if (!WMRandom.Percent(Math.Min(90, gang.Intelligence)))
            {
                gang.m_Events.AddMessage("They failed to find any enemy assets to hit.", (int)ImageTypes.PROFILE, Constants.EVENT_GANG);
                return false;
            }
            /*
            *	if yes then do damage to a random rival
            *
            *	Something else to consider: rival choice should be
            *	weighted by number of territories controlled
            *	(or - if we go with the ward idea - by territories
            *	controlled in the ward in question
            *
            *	of course, if there is no rival, it's academic
            */
            cRival rival = GameEngine.Game.g_Brothels.GetRivalManager().GetRandomRivalToSabotage();
            Gang rivalGang;
            if (rival == null)
            {
                gang.m_Events.AddMessage("Scouted the city in vain, seeking would-be challengers to your dominance.", (int)ImageTypes.PROFILE, Constants.EVENT_GANG);
                return false;
            }

            if (rival.m_NumGangs > 0)
            {
                rivalGang = GetTempGang(rival.m_Power);
                sabotageEvent.AppendLine(string.Format("Your men run into a gang from {0} and a brawl breaks out.", rival.m_Name));
                if (GangBrawl(gang, rivalGang, false) == false)
                {
                    rivalGang = null;
                    if (gang.MemberNum > 0)
                    {
                        sabotageEvent.Append("Your men lost. The ");
                        if (gang.MemberNum == 1)
                        {
                            sabotageEvent.Append("lone survivor fights his");
                        }
                        else
                        {
                            sabotageEvent.Append(string.Format("{0} survivors fight their", gang.MemberNum));
                        }
                        sabotageEvent.Append(" way back to friendly territory.");
                        gang.m_Events.AddMessage(sabotageEvent.ToString(), (int)ImageTypes.PROFILE, Constants.EVENT_DANGER);
                    }
                    else
                    {
                        sabotageEvent.Append(string.Format("Your gang {0} fails to report back from their sabotage mission." + Environment.NewLine + "Later you learn that they were wiped out to the last man.", gang.MemberNum));
                        // TODO : Interface Windows
                        //g_MessageQue.AddToQue(sabotageEvent.ToString(), Constants.COLOR_RED);
                    }
                    return false;
                }
                else
                {
                    sabotageEvent.AppendLine("Your men win.");
                }
                if (rivalGang.MemberNum <= 0) // clean up the rival gang
                {
                    rival.m_NumGangs--;
                    sabotageEvent.AppendFormat("The enemy gang is destroyed. {0} has ", rival.m_Name);
                    if (rival.m_NumGangs == 0)
                    {
                        sabotageEvent.AppendLine("no more gangs left!");
                    }
                    else if (rival.m_NumGangs <= 3)
                    {
                        sabotageEvent.AppendLine("a few gangs left.");
                    }
                    else
                    {
                        sabotageEvent.AppendLine("a lot of gangs left.");
                    }
                }
                rivalGang = null;
            }
            else
            {
                sabotageEvent.AppendFormat("Your men encounter no resistance when you go after {0}.", rival.m_Name);
            }


            // if we had an objective to attack a rival we just achieved it
            if (GameEngine.Game.g_Brothels.GetObjective() != null && GameEngine.Game.g_Brothels.GetObjective().m_Objective == (int)Objectives.LAUNCHSUCCESSFULATTACK)
            {
                GameEngine.Game.g_Brothels.PassObjective();
            }

            // If the rival has some businesses under his control he's going to lose some of them
            if (rival.m_BusinessesExtort > 0)
            {
                // mod: brighter goons do better damage they need 100% to be better than before however
                int spread = gang.Intelligence / 4;
                int num = 1 + WMRandom.Next(spread); // get the number of businesses lost
                if (rival.m_BusinessesExtort < num) // Can't destroy more businesses than they have
                {
                    num = rival.m_BusinessesExtort;
                }
                rival.m_BusinessesExtort -= num;

                sabotageEvent.AppendFormat("Your men destroy {0} of their businesses. {1} have ", num, rival.m_Name);
                if (rival.m_BusinessesExtort == 0)
                {
                    sabotageEvent.AppendLine("no more businesses left!");
                }
                else if (rival.m_BusinessesExtort <= 10)
                {
                    sabotageEvent.AppendLine("a few businesses left.");
                }
                else
                {
                    sabotageEvent.AppendLine("a lot of businesses left.");
                }
            }
            else
            {
                sabotageEvent.AppendLine(string.Format("{0} have no businesses to attack.", rival.m_Name));
            }

            if (rival.m_Gold > 0)
            {
                // mod: brighter goons are better thieves
                // `J` changed it // they need 100% to be better than before however	
                // `J` now based on rival's gold
                // `J` bookmark - your gang sabotage mission gold taken
                int gold = WMRandom.Next((int)(((double)gang.Intelligence / 2000.0) * (double)rival.m_Gold)) + WMRandom.Next((gang.Intelligence / 5) * gang.MemberNum); // plus (int/5)*num -  0-5% of rival's gold
                if (gold > rival.m_Gold)
                {
                    gold = (int)rival.m_Gold;
                }
                rival.m_Gold -= gold;

                // some of the money taken 'dissappears' before the gang reports it.
                if (WMRandom.Percent(20) && gold > 1000)
                {
                    gold -= WMRandom.Next() % 1000;
                }

                sabotageEvent.AppendFormat("\nYour men steal {0} gold from them. ", gold);
                if (rival.m_Gold == 0)
                {
                    sabotageEvent.AppendLine("Mu-hahahaha!  They're penniless now!");
                }
                else if (rival.m_Gold <= 10000)
                {
                    sabotageEvent.AppendLine(string.Format("{0} is looking pretty poor.", rival.m_Name));
                }
                else
                {
                    sabotageEvent.AppendLine(string.Format("It looks like {0} still has a lot of gold.", rival.m_Name));
                }

                /*
                `J` zzzzzz - need to add more and better limiters
                Suggestions from Whitetooth:
                I'm guessing those factors are based on there skills which make sense. For Example:
                Men - Overall number of people able to carry gold after sabotage.
                Combat - total amount of gold each man can hold.
                Magic - Amount of extra gold the gang can carry with magic not relying on combat or men. Magic could be bonus gold that can't be dropped, bribed, or stolen on the way back.
                Intel - Could be a overall factor to check if the gang knows what is valuable and what isn't.
                Agility - Could be a check for clumsiness of the gang; they could drop valuables on the way back.
                Tough - Checks if there tough enough to intimidate any guards or protect the money they have.
                Charisma - Factors how much gold they have to bribe to guards if they get caught and can't intimidate them.
                The order of checks could be -> Intel -> Magic -> Men - > Combat -> Agility -> Tough -> Charisma
                */

                // `J` bookmark - limit gold taken by gang sabotage
                bool limit = false;
                if (gold > 15000)
                {
                    limit = true;
                    int burnedbonds = (gold / 10000);
                    int bbcost = burnedbonds * 10000;
                    gold -= bbcost;
                    sabotageEvent.Append("As your men are fleeing, one of them has to jump through a wall of fire. When he does, he drops ");
                    if (burnedbonds == 1)
                    {
                        sabotageEvent.Append("a");
                    }
                    else if (burnedbonds > 4)
                    {
                        sabotageEvent.Append("a stack of ");
                    }
                    else
                    {
                        sabotageEvent.Append(burnedbonds);
                    }

                    sabotageEvent.Append(string.Format(" Gold Bearer Bond{0} worth 10k gold each. {1} gold just went up in smoke.",
                        burnedbonds > 1 ? "s" : "", bbcost));
                }
                if (gold > 5000 && WMRandom.Percent(50))
                {
                    limit = true;
                    int spill = (WMRandom.Next() % 4500) + 500;
                    gold -= spill;
                    sabotageEvent.Append(string.Format("As they are being chased through the streets by {0}'s people, one of your gang members cuts open a sack of gold spilling its contents in the street. As the throngs of civilians stream in to collect the coins, they block the pursuers and allow you men to get away safely.",
                        rival.m_Name));
                }

                if (gold > 5000)
                {
                    limit = true;
                    int bribeperc = ((WMRandom.Next() % 15) * 5) + 10;
                    int bribe = (int)(gold * ((double)bribeperc / 100.0));
                    gold -= bribe;
                    sabotageEvent.AppendLine(string.Format("As your gang leave your rival's territory on the way back to your brothel, they come upon a band of local police that are hunting them. Their boss demands {0}"
                        + "% of what your gang is carrying in order to let them go.  They pay them {1} gold and continue on home.",
                        bribeperc, bribe));
                }

                if (limit)
                {
                    sabotageEvent.AppendLine(string.Format("{0} returns with {1} gold.",
                        gang.Name, gold));
                }
                GameEngine.Game.Gold.plunder(gold);
            }
            else
            {
                sabotageEvent.AppendLine("The losers have no gold to take.");
            }

            if (rival.m_NumInventory > 0 && WMRandom.Percent(Math.Min(75, gang.Intelligence)))
            {
                cRivalManager r = new cRivalManager();
                int num = r.GetRandomRivalItemNum(rival);
                sInventoryItem item = r.GetRivalItem(rival, num);
                if (item != null)
                {
                    sabotageEvent.AppendFormat("Your men steal an item from them, one {0}.", item.Name);
                    r.RemoveRivalInvByNumber(rival, num);
                    GameEngine.Game.g_Brothels.AddItemToInventory(item);
                }
            }

            if (rival.m_NumBrothels > 0 && WMRandom.Percent(gang.Intelligence / Math.Min(3, 11 - rival.m_NumBrothels)))
            {
                rival.m_NumBrothels--;
                rival.m_Power--;
                sabotageEvent.AppendFormat("Your men burn down one of {0}'s Brothels. {1}", rival.m_Name, rival.m_Name);
                if (rival.m_NumBrothels == 0)
                {
                    sabotageEvent.AppendLine(" has no Brothels left.");
                }
                else if (rival.m_NumBrothels <= 3)
                {
                    sabotageEvent.AppendLine(" is in control of very few Brothels.");
                }
                else
                {
                    sabotageEvent.AppendLine(" has many Brothels left.");
                }
            }
            if (rival.m_NumGamblingHalls > 0 && WMRandom.Percent(gang.Intelligence / Math.Min(1, 9 - rival.m_NumGamblingHalls)))
            {
                rival.m_NumGamblingHalls--;
                sabotageEvent.AppendFormat("Your men burn down one of {0}'s Gambling Halls. {1}", rival.m_Name, rival.m_Name);
                if (rival.m_NumGamblingHalls == 0)
                {
                    sabotageEvent.AppendLine(" has no Gambling Halls left.");
                }
                else if (rival.m_NumGamblingHalls <= 3)
                {
                    sabotageEvent.AppendLine(" is in control of very few Gambling Halls .");
                }
                else
                {
                    sabotageEvent.AppendLine(" has many Gambling Halls left.");
                }
            }
            if (rival.m_NumBars > 0 && WMRandom.Percent(gang.Intelligence / Math.Min(1, 7 - rival.m_NumBars)))
            {
                rival.m_NumBars--;
                sabotageEvent.AppendFormat("Your men burn down one of {0}'s Bars. {1}", rival.m_Name, rival.m_Name);
                if (rival.m_NumBars == 0)
                {
                    sabotageEvent.AppendLine(" has no Bars left.");
                }
                else if (rival.m_NumBars <= 3)
                {
                    sabotageEvent.AppendLine(" is in control of very few Bars.");
                }
                else
                {
                    sabotageEvent.AppendLine(" has many Bars left.");
                }
            }

            BoostGangSkill(gang.Stats[EnumStats.INTELLIGENCE], 2);
            gang.m_Events.AddMessage(sabotageEvent.ToString(), (int)ImageTypes.PROFILE, Constants.EVENT_GANG);

            // See if the rival is eliminated:  If 4 or more are zero or less, the rival is eliminated
            int VictoryPoints = 0;
            if (rival.m_Gold <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumGangs <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_BusinessesExtort <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumBrothels <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumGamblingHalls <= 0)
            {
                VictoryPoints++;
            }
            if (rival.m_NumBars <= 0)
            {
                VictoryPoints++;
            }

            if (VictoryPoints >= 4)
            {
                StringBuilder ssVic = new StringBuilder();
                ssVic.AppendFormat("You have dealt {0} a fatal blow.  Their criminal organization crumbles to nothing before you.", rival.m_Name);
                GameEngine.Game.g_Brothels.m_Rivals.RemoveRival(rival);
                gang.m_Events.AddMessage(ssVic.ToString(), (int)ImageTypes.PROFILE, Constants.EVENT_GOODNEWS);
            }
            return true;
        }
    

        #region XML Save/Load gang
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
        #endregion

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

    }
}
