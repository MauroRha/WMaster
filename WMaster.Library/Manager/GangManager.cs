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
    using System.Text;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Enums;
    using WMaster.Concept;
    using WMaster.Entity.Item;
    using WMaster.Entity.Living;
    using WMaster.Tool;
    using WMaster.Concept.Attributs;
    using WMaster.Concept.GangMission;

    /// <summary>
    /// Manages all the player gangs
    /// </summary>
    public class GangManager
    {
        #region Static members
        /// <summary>
        /// Singleton of <see cref="GangManager"/>.
        /// </summary>
        private static GangManager m_Instance;
        /// <summary>
        /// Get unique instance of <see cref="GangManager"/>.
        /// </summary>
        public static GangManager Instance
        {
            get
            {
                if (GangManager.m_Instance == null)
                { GangManager.m_Instance = new GangManager(); }
                return GangManager.m_Instance;
            }
        }
        #endregion

        /// <summary>
        /// Number of businesses under player control.
        /// <remarks><para>TODO : May translate to player class</para></remarks>
        /// </summary>
        private int m_BusinessesExtort;
        /// <summary>
        /// Get or set the number of business extorted.
        /// </summary>
        public static int NumBusinessExtort
        {
            get { return GangManager.Instance.m_BusinessesExtort; }
            // TODO : GAME - Check m_BusinessesExtort < Max value?
            set { GangManager.Instance.m_BusinessesExtort = value; }
        }
        /// <summary>
        /// Adjuste number of business extorted.
        /// </summary>
        /// <param name="n">Adjustement value of number of business extorted.</param>
        /// <returns>Number of business extorted.</returns>
        public static int AdjustBusinessExtorted(int n)
        {
            // TODO : GAME - Check m_BusinessesExtort < Max value?
            NumBusinessExtort += n;
            return NumBusinessExtort;
        }
        /// <summary>
        /// Maximum number of gang player can hire
        /// </summary>
        private int m_MaxNumGangs;

        /// <summary>
        /// Use to switch between old/new code in catacombs mission.
        /// </summary>
        [Obsolete("Do choice between new or old cade. Use compilation pragma ?")]
        private bool m_ControlGangs;
        public static bool ControlGangs
        {
            get { return GangManager.Instance.m_ControlGangs; }
            set { GangManager.Instance.m_ControlGangs = value; }
        }

        #region Catacombs relativs settings
        /// <summary>
        /// Percentage of looking for girls in catacombs.
        /// </summary>
        private int m_GangGetsGirls;
        /// <summary>
        /// Get or set the percentage of looking for girls in catacombs.
        /// </summary>
        public static int GangGetsGirls
        {
            get { return GangManager.Instance.m_GangGetsGirls; }
            set { GangManager.Instance.m_GangGetsGirls = Math.Max(Math.Min(value, 0), 100); }
        }
        /// <summary>
        /// Percentage of looking for items in catacombs.
        /// </summary>
        private int m_GangGetsItems;
        /// <summary>
        /// Get or set the percentage of looking for items in catacombs.
        /// </summary>
        public static int GangGetsItems
        {
            get { return GangManager.Instance.m_GangGetsItems; }
            set { GangManager.Instance.m_GangGetsItems = Math.Max(Math.Min(value, 0), 100); }
        }
        /// <summary>
        /// Percentage of looking for beasts in catacombs.
        /// </summary>
        private int m_GangGetsBeast;
        /// <summary>
        /// Get or set the percentage of looking for beasts in catacombs.
        /// </summary>
        public static int GangGetsBeast
        {
            get { return GangManager.Instance.m_GangGetsBeast; }
            set { GangManager.Instance.m_GangGetsBeast = Math.Max(Math.Min(value, 0), 100); }
        }
        #endregion

        //private int m_NumGangNames;
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


        // TODO : Bound SwordLevet to upper limite (4?)
        /// <summary>
        /// Weapond level the gangs has been upgraded.
        /// </summary>
        private int m_SwordLevel;
        /// <summary>
        /// Get or set the weapond level the gangs has been upgraded.
        /// </summary>
        public static int SwordLevel
        {
            get { return GangManager.Instance.m_SwordLevel; }
            set { GangManager.Instance.m_SwordLevel = Math.Min(value, 0); }
        }
        /// <summary>
        /// Return the weapon level of gang.
        /// </summary>
        /// <returns>The weapon level the gang.</returns>
        [Obsolete("Use SwordLevel property", false)]
        public int GetWeaponLevel()
        {
            return m_SwordLevel;
        }

        #region Nets
        // gang armory
        // mod - changing the keep stocked flag to an int
        // so we can record the level at which to maintain
        // the stock - then we can restock at turn end
        // to prevent squads becoming immortal by
        // burning money
        private int m_KeepNetsStocked;
        /// <summary>
        /// Number of Nets player have.
        /// </summary>
        [Obsolete("Use property instead of private fields", false)]
        private int m_NumNets;
        /// <summary>
        /// Get or set the number of nets player have.
        /// </summary>
        public static int NumNets
        {
            get { return GangManager.Instance.m_NumNets; }
            set { GangManager.Instance.m_NumNets = Math.Min(value, 0); }
        }
        /// <summary>
        /// Get number of Nets the gang have.
        /// </summary>
        /// <returns>Number of Nets the gang have.</returns>
        [Obsolete("Use NumNets property", false)]
        public static int GetNets()
        {
            return NumNets;
        }
        public static void UseANet()
        {
            NumNets--;
        }
        #endregion

        #region Healing Pot
        // gang armory
        // mod - changing the keep stocked flag to an int
        // so we can record the level at which to maintain
        // the stock - then we can restock at turn end
        // to prevent squads becoming immortal by
        // burning money
        private int m_KeepHealStocked;
        /// <summary>
        /// Number of healing pot the player have.
        /// </summary>
        [Obsolete("Use property instead of private fields", false)]
        private int m_NumHealingPotions;
        /// <summary>
        /// Get or set the number of healing pot player have
        /// </summary>
        public static int NumHealingPotions
        {
            get { return GangManager.Instance.m_NumHealingPotions; }
            set { GangManager.Instance.m_NumHealingPotions = Math.Min(value, 0); }
        }
        /// <summary>
        /// Get the max number of healpotion a gang can use. (?)
        /// </summary>
        /// <remarks><para>`J` replaced with passing out of pots/nets in GangStartOfShift() for .06.01.09</para></remarks>
        /// <returns>Max number of healpotion a gang can use.</returns>
        public int HealingLimit()
        {
            if (m_PlayerGangList.Count.Equals(0) || m_NumHealingPotions < 1)
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
        #endregion

        /// <summary>
        /// Initialise a new instance of <see cref="GangManager"/>
        /// TODO : REFACTORING - Make singleton or static class?
        /// </summary>
        public GangManager()
        {
            m_BusinessesExtort = 0;
            m_NumHealingPotions = m_NumNets = m_SwordLevel = 0;
            m_KeepHealStocked = m_KeepNetsStocked = 0;
            m_ControlGangs = false;
            m_GangGetsGirls = m_GangGetsItems = m_GangGetsBeast = 0;
        }

        /// <summary>
        /// Free/(re)initialize GangManager datas
        /// </summary>
        public void Free()
        {
            m_PlayerGangList.Clear();
            m_HireableGangList.Clear();
            m_BusinessesExtort = 0;
            m_NumHealingPotions = m_SwordLevel = m_NumNets = 0;
            m_KeepHealStocked = m_KeepNetsStocked = 0;
            m_ControlGangs = false;
            m_GangGetsGirls = m_GangGetsItems = m_GangGetsBeast = 0;
        }

        /// <summary>
        /// Adds a new randomly generated gang to the recruitable list
        /// </summary>
        /// <param name="boosted"><b>True</b> to boos number of gang member, skills and statistic.</param>
        public void AddNewGang(bool boosted)
        {
            Gang newGang = new Gang();

            int maxMembers = Configuration.Gangs.InitMemberMax;
            int minMembers = Configuration.Gangs.InitMemberMin;
            newGang.MemberNum = minMembers + WMRand.Random() % (maxMembers + 1 - minMembers);
            if (boosted)
            {
                newGang.MemberNum = Math.Min(15, newGang.MemberNum + 5);
            }

            int new_val;
            foreach (Skill skill in newGang.Skills)
            {
                new_val = (WMRand.Random() % 30) + 21;
                if (WMRand.Random() % 5 == 1)
                {
                    new_val += 1 + WMRand.Random() % 10;
                }
                if (boosted)
                {
                    new_val += 10 + WMRand.Random() % 11;
                }
                skill.Value = new_val;
            }

            foreach (Stat item in newGang.Stats)
            {
                new_val = (WMRand.Random() % 30) + 21;
                if (WMRand.Random() % 5 == 1)
                {
                    new_val += WMRand.Random() % 10;
                }
                if (boosted)
                {
                    new_val += 10 + WMRand.Random() % 11;
                }
                item.Value = new_val;
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;

            newGang.Name = GetNewGangName();

            this.m_HireableGangList.Add(newGang);
        }

        /// <summary>
        /// Get a new random gang name from [[:HiredGangNames:]] gang name resource exclusing 
        /// </summary>
        /// <returns>New random gang name different of all eisting gang name.</returns>
        private string GetNewGangName()
        {
            NameList name = new NameList("HiredGangNames");

            name.Exclude(m_PlayerGangList.Select(g => g.Name));
            name.Exclude(m_HireableGangList.Select(g => g.Name));

            return name.Random();
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
                gang.LastMission = GangMissionBase.None;
                if (gang.MemberNum <= 5)
                {
                    GangMissionBase.SetGangMission(EnuGangMissions.Recruit, gang);
                }
                else
                {
                    GangMissionBase.SetGangMission(EnuGangMissions.Guarding, gang);
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
                if (this.m_HireableGangList.Count < Configuration.Gangs.MaxRecruitList)
                {
                    gang.HasSeenCombat = gang.AutoRecruit = false;
                    gang.LastMission = GangMissionBase.None;
                    this.AddHireableGang(gang);
                }
                this.RemoveGang(gang);
            }
        }

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
            // TODO : GAME - Use max gang num instead of literal value
            m_MaxNumGangs = 7 + Game.Brothels.GetNumBrothels();
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
        /// Creates a new nonamed gang for single use
        /// </summary>
        /// <returns>Temporary new <see cref="Gang"/>.</returns>
        public static Gang GetTempGang()
        {
            Gang newGang = new Gang();
            newGang.MemberNum = WMRand.Random() % 6 + 10;
            foreach (Skill item in newGang.Skills)
            {
                item.Value = (WMRand.Random() % 30) + 21;
            }
            foreach ( Stat item in newGang.Stats)
            {
                item.Value = (WMRand.Random() % 30) + 21;
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;
            return newGang;
        }
        /// <summary>
        /// Creates a new nonamed gang with stat/skill mod
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static Gang GetTempGang(int mod)
        {
            Gang newGang = new Gang();
            newGang.MemberNum = Math.Min(15, WMRand.Bell(6, 18));
            foreach (Skill item in newGang.Skills)
            {
                item.Value = (WMRand.Random() % 40) + 21 + (WMRand.Random() % mod);
                item.Value = Math.Max(Math.Min(item.Value, 100), 1);
            }
            foreach (Stat item in newGang.Stats)
            {
                item.Value = (WMRand.Random() % 40) + 21 + (WMRand.Random() % mod);
                item.Value = Math.Max(Math.Min(item.Value, 100), 1);
            }
            newGang.Stats[EnumStats.HEALTH].Value = 100;
            newGang.Stats[EnumStats.HAPPINESS].Value = 100;

            return newGang;
        }

        /// <summary>
        /// Creates a new nonamed weak gang for single use.
        /// <remarks><para>Used by the new brothel security code.</para></remarks>
        /// </summary>
        /// <returns>Temporary new weak <see cref="Gang"/>.</returns>
        public Gang GetTempWeakGang()
        {
            // MYR: Weak gangs attack girls when they work
            Gang newGang = new Gang();
            newGang.MemberNum = 15;
            foreach (Skill item in newGang.Skills)
            {
                item.Value = WMRand.Random() % 30 + 51;
            }
            foreach (Stat item in newGang.Stats)
            {
                item.Value = WMRand.Random() % 30 + 51;
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

            return gangList[WMRand.Random(gangList.Count)];
        }

        /// <summary>
        /// Simple function to increase a gang's combat skills a bit.
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to boost skill.</param>
        /// <param name="count">Number of skill to boost.</param>
        public static void BoostGangCombatSkills(Gang gang, int count)
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
        public static void BoostGangRandomSkill(List<IValuableAttribut> possibleSkills, int count, int boostCount)
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
                int choice = WMRand.Random(totalChance);

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
        public static void BoostGangSkill(IValuableAttribut affectSkill, int count)
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

                boostAmount = (double)WMRand.InRange((int)((boostAmount / 2) * 100), (int)((boostAmount * 1.5) * 100)) / 100;
                int oneMore = WMRand.Percent((int)((boostAmount - (int)boostAmount) * 100)) ? 1 : 0;
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
        public static bool GangBrawl(Gang gang1, Gang gang2, bool rivalVrival)
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
                gang1.HealLimit = 10;
            }
            int g1SwordLevel = (rivalVrival ? Math.Min(5, (WMRand.Random() % (gang1.Skills[EnumSkills.COMBAT].Value / 20) + 1)) : SwordLevel);

            gang2.HasSeenCombat = true;
            EnumSkills g2attack = EnumSkills.COMBAT;
            int initalNumber2 = gang2.MemberNum;
            int g2dodge = gang2.Stats[EnumStats.AGILITY].Value;
            gang2.HealLimit = 10;
            int g2SwordLevel = Math.Min(5, (WMRand.Random() % (gang2.Skills[EnumSkills.COMBAT].Value / 20) + 1));

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
                    if (WMRand.Percent(gang1.Skills[g1attack].Value))
                    {
                        int damage = (g1SwordLevel + 1) * Math.Max(1, gang1.Strength / 10);
                        if (g1attack == EnumSkills.MAGIC)
                        {
                            damage += gang1.Skills[EnumSkills.MAGIC].Value / 10 + 3;
                        }

                        // gang 2 attempts Dodge
                        if (!WMRand.Percent(g2dodge))
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
                    if (WMRand.Percent(gang2.Skills[g2attack].Value))
                    {
                        int damage = (g2SwordLevel + 1) * Math.Max(1, gang2.Strength / 10);
                        if (g2attack == EnumSkills.MAGIC)
                        {
                            damage += gang2.Skills[EnumSkills.MAGIC].Value / 10 + 3;
                        }

                        if (!WMRand.Percent(g1dodge))
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
                            NumHealingPotions--;
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
                    if (WMRand.Percent(40))
                    {
                        BoostGangCombatSkills(gang1, 2); // win by runaway, boost 2 skills
                        return true; // the men run away
                    }
                }

                if ((initalNumber1 / 2) > gang1.MemberNum) // if the gang has lost half its number there is a 40% chance they will run away
                {
                    if (WMRand.Percent(40))
                    {
                        BoostGangCombatSkills(gang2, 2); // win by runaway, boost 2 skills
                        return false; // the men run away
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Fight between a girl and a gang.
        /// <remarks><para>Returns <b>True</b> if the girl wins.</para></remarks>
        /// </summary>
        /// <param name="girl">A girl who ficht.</param>
        /// <param name="gang">A gang who fight.</param>
        /// <returns><b>True</b> if the girl wins</returns>
        public static bool GangCombat(sGirl girl, Gang gang)
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
                        if (WMRand.Percent(40))
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

                        int girlAttackChance = Game.Girls.GetSkill(girl, (int)attack);
                        int dieRoll = WMRand.Random();

                        WMLog.Trace(string.Format("    attack chance = {0}.", girlAttackChance), WMLog.TraceLog.INFORMATION);
                        WMLog.Trace(string.Format("    die roll = {0}.", dieRoll), WMLog.TraceLog.INFORMATION);

                        if (dieRoll > girlAttackChance)
                        {
                            WMLog.Trace("      attack fails.", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            int damage = Game.Girls.GetCombatDamage(girl, (int)attack);
                            WMLog.Trace(string.Format("      attack hits! base damage is {0}.", damage), WMLog.TraceLog.INFORMATION);

                            /*
                            *				she may improve a little
                            *				(checked every round of combat? seems excessive)
                            */
                            int gain = WMRand.Random() % 2;
                            if (gain != 0)
                            {
                                WMLog.Trace(string.Format("    {0} gains {1} to attack skill.", girl.m_Realname, gain), WMLog.TraceLog.INFORMATION);
                                Game.Girls.UpdateSkill(girl, (int)attack, gain);
                            }

                            dieRoll = WMRand.Random();

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
                            NumHealingPotions--;
                            gHealth += 30;
                            WMLog.Trace(string.Format("Goon drinks healing potion: new health value = {0}. Gang has {1} remaining.", gHealth, gang.HealLimit), WMLog.TraceLog.INFORMATION);
                        }

                        // Goon Attacks
                        dieRoll = WMRand.Random();
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

                            int damage = (SwordLevel + 1) * Math.Max(1, gang.Strength / 10);
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
                            if (!WMRand.Percent(dodge))
                            {
                                damage = Math.Max(1, (damage - (Game.Girls.GetStat(girl, (int)EnumStats.CONSTITUTION) / 15)));
                                Game.Girls.UpdateStat(girl, (int)EnumStats.HEALTH, -damage);
                            }
                        }

                        dodge = Math.Max(0, (dodge - 1)); // degrade girls dodge ability
                        gDodge = Math.Max(0, (gDodge - 1)); // degrade goons dodge ability

                        if (girl.health() < 30 && girl.health() > 20)
                        {
                            if (WMRand.Percent(girl.agility()))
                            {
                                BoostGangCombatSkills(gang, 2);
                                Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -1);
                                return false;
                            }
                        }
                    }

                    if (Game.Girls.GetStat(girl, (int)EnumStats.HEALTH) <= 20)
                    {
                        BoostGangCombatSkills(gang, 2);
                        Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -1);
                        return false;
                    }
                    else
                    {
                        gang.MemberNum--;
                    }

                    if ((initalNumber / 2) > gang.MemberNum) // if the gang has lost half its number there is a 40% chance they will run away
                    {
                        if (WMRand.Percent(40))
                        {
                            Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);
                            return true; // the men run away
                        }
                    }
                    if (gang.MemberNum == 0)
                    {
                        Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);
                        return true;
                    }
                }

                WMLog.Trace(string.Format("No more opponents: {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);

                Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +1);

                return true;
            }
            finally
            {
                WMLog.Trace("GangManager.GangCombat ended!", WMLog.TraceLog.INFORMATION);
            }
        }

        /// <summary>
        /// Fight between a girl and a gang.
        /// <remarks>
        ///     <para>Returns <b>True</b> if the girl wins.</para>
        ///     <para>
        ///         MYR: This is similar to GangCombat, but instead of one of the players gangs
        ///         fighting the girl, some random gang attacks her.  This random gang
        ///         doesn't have healing potions and the weapon levels of a player gang.
        ///         ATM only the new security code uses it.
        ///         This will also be needed to be updated to the new way of doing combat.
        /// </para>
        /// </remarks>
        /// </summary>
        /// <param name="girl">A girl who ficht.</param>
        /// <param name="gang">A gang who fight.</param>
        /// <returns><b>True</b> if the girl wins</returns>
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
                    if (WMRand.Percent(40))
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

            int dodge = Game.Girls.GetStat(girl, (int)EnumStats.AGILITY); // MYR: Was 0
            int mana = Game.Girls.GetStat(girl, (int)EnumStats.MANA); // MYR: Like agility, mana is now per battle

            EnumSkills attack = EnumSkills.COMBAT; // determined later, defaults to combat
            EnumSkills goonAttack = EnumSkills.COMBAT;

            if (enemyGang == null || enemyGang.MemberNum == 0)
            {
                return true;
            }

            // first determine what she will fight with
            if (Game.Girls.GetSkill(girl, (int)EnumSkills.COMBAT) > Game.Girls.GetSkill(girl, (int)EnumSkills.MAGIC))
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
                Game.Girls.GetStat(girl, (int)EnumStats.AGILITY),
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

                while (Game.Girls.GetStat(girl, (int)EnumStats.HEALTH) >= 20 && gHealth > 0)
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

                    int girlAttackChance = Game.Girls.GetSkill(girl, (int)attack);

                    int dieRoll = WMRand.Random();

                    WMLog.Trace(string.Format("    attack chance {0} die roll:{1}.", girlAttackChance, dieRoll), WMLog.TraceLog.INFORMATION);

                    if (dieRoll > girlAttackChance)
                    {
                        WMLog.Trace("      attack misses.", WMLog.TraceLog.INFORMATION);
                    }
                    else
                    {
                        int damage = Game.Girls.GetCombatDamage(girl, (int)attack);

                        dieRoll = WMRand.Random();

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
                    dieRoll = WMRand.Random();
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
                        dieRoll = WMRand.Random();

                        WMLog.Trace(string.Format("    {0} tries to dodge: needs {1}, gets {2}.", girl.m_Realname, dodge, dieRoll), WMLog.TraceLog.INFORMATION);

                        // MYR: Girl dodge maxes out at 90 (Gang dodge at 95).  It's a bit of a hack
                        if (dieRoll <= dodge && dieRoll <= 90)
                        {
                            WMLog.Trace("    succeeds!", WMLog.TraceLog.INFORMATION);
                        }
                        else
                        {
                            Game.Girls.TakeCombatDamage(girl, -damage); // MYR: Note change

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

                if (Game.Girls.GetStat(girl, (int)EnumStats.HEALTH) <= 20)
                {
                    WMLog.Trace(string.Format("The gang overwhelmed and defeated {0}. She lost the battle.", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, -5);
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
                    if (WMRand.Percent(50)) // MYR: Adjusting this has a big effect
                    {
                        WMLog.Trace(string.Format("The gang ran away after losing too many members. {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                        Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);
                        return true; // the men run away
                    }
                }
                // Gang fought to the death
                if (enemyGang.MemberNum == 0)
                {
                    WMLog.Trace(string.Format("The gang fought to bitter end. They are all dead. {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);
                    Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);
                    return true;
                }
            }

            WMLog.Trace(string.Format("No more opponents: {0} WINS!", girl.m_Realname), WMLog.TraceLog.INFORMATION);

            Game.Girls.UpdateEnjoyment(girl, (int)ActionTypes.Combat, +5);

            return true;
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
            double removeChance = Configuration.Gangs.ChanceRemoveUnwanted;
            foreach(Gang item in m_HireableGangList)
            {
                if (WMRand.Percent(removeChance))
                {
                    WMLog.Trace(string.Format("Culling recruitable gang: {0}", item.Name), WMLog.TraceLog.INFORMATION);
                    RemoveHireableGang(item);
                }
            }

            // maybe add some new gangs to the recruitable list
            int addMin = Configuration.Gangs.AddNewWeeklyMin;
            int addMax = Configuration.Gangs.AddNewWeeklyMax;
            int addRecruits = WMRand.Bell(addMin, addMax);
            for (int i = 0; i < addRecruits; i++)
            {
                if (m_HireableGangList.Count >= Configuration.Gangs.MaxRecruitList)
                {
                    break;
                }
                WMLog.Trace("Adding new recruitable gang.", WMLog.TraceLog.INFORMATION);
                AddNewGang(false);
            }

            // now, deal with player controlled gangs on missions
            foreach (Gang item in m_PlayerGangList)
            {
                if (item.CurrentMission != null)
                { item.CurrentMission.DoTheJob(); }
                //switch (item.MissionType)
                //{
                //    case EnuGangMissions.Guarding: // these are handled in GangStartOfShift()
                //    case EnuGangMissions.SpyGirls:
                //        break;
                //    case EnuGangMissions.RecaptureGirls:
                //        if (!Game.Brothels.RunawaysGirlList.Count.Equals(0))
                //        {
                //            RecaptureMission(item);
                //            break;
                //        }
                //        else
                //        {
                //            item.m_Events.AddMessage(
                //                LocalString.GetStringLine(LocalString.ResourceStringCategory.Global, "ThisGangWasSentToLookForRunawaysButThereAreNoneSoTheyWentLookingForAnyGirlToKidnapInstead"),
                //                ImageTypes.PROFILE, EventType.GANG);
                //            KidnappMission(item);
                //            break;
                //        }
                //    case EnuGangMissions.KidnappGirls:
                //        KidnappMission(item);
                //        break;
                //    case EnuGangMissions.Sabotage:
                //        SabotageMission(item);
                //        break;
                //    case EnuGangMissions.Extortion:
                //        ExtortionMission(item);
                //        break;
                //    case EnuGangMissions.PettyTheft:
                //        PettyTheftMission(item);
                //        break;
                //    case EnuGangMissions.GrandTheft:
                //        GrandTheftMission(item);
                //        break;
                //    case EnuGangMissions.Catacombs:
                //        CatacombsMission(item);
                //        break;
                //    case EnuGangMissions.Training:
                //        GangTraining(item);
                //        break;
                //    case EnuGangMissions.Recruit:
                //        GangRecruiting(item);
                //        break;
                //    case EnuGangMissions.Service:
                //        ServiceMission(item);
                //        break;
                //    default:
                //        // TODO : Replace m_MissionID to Mission name
                //        item.m_Events.AddMessage(
                //            LocalString.GetStringFormatLine(
                //                LocalString.ResourceStringCategory.Global,
                //                "ErrorNoMissionSetOrMissionNotFound[MissionName]",
                //                new List<FormatStringParameter>() { new FormatStringParameter("MissionName", item.MissionType.ToString()) }),
                //            ImageTypes.PROFILE, EventType.GANG);
                //        continue;
                //}

                if (LoseGang(item))
                {
                    continue; // if they all died, move on.
                }
                if (item.HasSeenCombat == false && item.MemberNum < 15)
                {
                    item.MemberNum++;
                }
                CheckGangRecruit(item);
            }

            Game.Brothels.m_Rivals.Update(m_BusinessesExtort); // Update the rivals

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
                Game.Gold.consumable_cost(tariff.healing_price(diff));
            }
            if (m_KeepNetsStocked > 0 && m_KeepNetsStocked > m_NumNets)
            {
                int diff = m_KeepNetsStocked - m_NumNets;
                m_NumNets = m_KeepNetsStocked;
                Game.Gold.consumable_cost(tariff.nets_price(diff));
            }
        }

        /// <summary>
        /// Sends a gang on a mission.
        /// </summary>
        /// <param name="gangID">Index of gang in player list.</param>
        /// <param name="mission">Mission to affect to gang.</param>
        [Obsolete("Use GangMissionBase.SetGangMission(...) instead of SendGang(...)", false)]
        public void SendGang(int gangID, EnuGangMissions mission)
        {
            SendGang(m_PlayerGangList[gangID], mission);
        }

        /// <summary>
        /// Sends a gang on a mission.
        /// </summary>
        /// <param name="gang">Gang to send on mission.</param>
        /// <param name="mission">Mission to affect to gang.</param>
        [Obsolete("Use GangMissionBase.SetGangMission(...) instead of SendGang(...)", false)]
        public void SendGang(Gang gang, EnuGangMissions mission)
        {
            if (gang == null)
            { return; }

            GangMissionBase.SetGangMission(mission, gang);
        }

        /// <summary>
        /// Get the first gang on mission.
        /// </summary>
        /// <param name="mission">Mission of gang to find.</param>
        /// <returns>First <see cref="Gang"/> assigned to <paramref name="mission"/>.</returns>
        public Gang GetGangOnMission(EnuGangMissions mission)
        {
            return m_PlayerGangList
                .Where(g => g.MissionType.Equals(mission))
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
            List<EnuGangMissions> mission = new List<EnuGangMissions>() { EnuGangMissions.Recruit, EnuGangMissions.Training, EnuGangMissions.SpyGirls, EnuGangMissions.Guarding, EnuGangMissions.Service };
            foreach (Gang item in m_PlayerGangList)
            {
                if (mission.Contains(item.MissionType))
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
        public static Gang GetGangRecruitingNotFull(int roomFor)
        {
            List<EnuGangMissions> mission = new List<EnuGangMissions>() { EnuGangMissions.Recruit, EnuGangMissions.Training, EnuGangMissions.SpyGirls, EnuGangMissions.Guarding, EnuGangMissions.Service };
            foreach (Gang lGang in GangManager.Instance.m_PlayerGangList)
            {
                if (lGang.MemberNum + roomFor <= 15)
                {
                    if (mission.Contains(lGang.MissionType))
                    {
                        return lGang;
                    }
                }
            }

            // if none are found then get a gang that has room for at least 1
            foreach (Gang lGang in GangManager.Instance.m_PlayerGangList)
            {
                if (lGang.MemberNum < 15)
                {
                    if (mission.Contains(lGang.MissionType))
                    {
                        return lGang;
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
        public List<Gang> GangsOnMission(EnuGangMissions mission)
        {
            return m_PlayerGangList
                .Where(g => g.MissionType.Equals(mission))
                .ToList();
            //List<Gang> list = new List<Gang>(); // loop through the gangs
            //foreach (Gang item in m_PlayerGangList)
            //{
            //    // if they're doing the job we are looking for, take them
            //    if (item.MissionType.Equals(mission))
            //    {
            //        list.Add(item);
            //    }
            //}
            //return list;
        }

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
                if (item.MissionType.Equals(EnuGangMissions.Guarding) || item.MissionType.Equals(EnuGangMissions.SpyGirls))
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
            List<Gang> gvec = GangsOnMission(EnuGangMissions.SpyGirls); // get a vector containing all the spying gangs

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

        #region Mission
        /// <summary>
        /// Performe a gang sabotage mission against player's rival.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionSabotage is affected to gang", true)]
        public bool SabotageMission(Gang gang)
        {
            LocalString sabotageEvent = new LocalString();
            sabotageEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global, "Gang[GangName]IsAttackingRivals", new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
            /*
            *	See if they can find any enemy assets to attack
            *
            *	I'd like to add a little more intelligence to this.
            *	Modifiers based on gang intelligence, for instance
            *	Allow a "scout" activity for gangs that improves the
            *	chances of a raid. That sort of thing.
            */
            if (!WMRand.Percent(Math.Min(90, gang.Intelligence)))
            {

                gang.m_Events.AddMessage(
                    LocalString.GetString(LocalString.ResourceStringCategory.Global, "TheyFailedToFindAnyEnemyAssetsToHit"),
                    ImageTypes.PROFILE, EventType.GANG);
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
            cRival rival = Game.Rivals.GetRandomRivalToSabotage();
            Gang rivalGang;
            if (rival == null)
            {
                gang.m_Events.AddMessage(
                    LocalString.GetString(LocalString.ResourceStringCategory.Global, "ScoutedTheCityInVainSeekingWouldBeChallengersToYourDominance"),
                    ImageTypes.PROFILE, EventType.GANG);
                return false;
            }

            if (rival.m_NumGangs > 0)
            {
                rivalGang = GetTempGang(rival.m_Power);
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenRunIntoAGangFrom[GangName]AndABrawlBreaksOut",
                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                if (GangBrawl(gang, rivalGang, false) == false)
                {
                    rivalGang = null;
                    if (gang.MemberNum == 0)
                    {
                        // TODO : Check if event is shown when gang was disband
                        sabotageEvent.AppendFormat(
                            LocalString.ResourceStringCategory.Global,
                            "YourGang[GangName]FailsToReportBackFromTheirSabotageMissionLaterYouLearnThatTheyWereWipedOutToTheLastMan",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                    }
                    else if (gang.MemberNum == 1)
                    {
                        sabotageEvent.Append(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenLostTheLoneSurvivorFightsHisWayBackToFriendlyTerritory");
                    }
                    else
                    {
                        sabotageEvent.AppendFormat(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenLostThe[GangMemberNum]SurvivorsFightTheirWayBackToFriendlyTerritory",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangMemberNum", gang.MemberNum) });
                    }
                    gang.m_Events.AddMessage(sabotageEvent.ToString(), ImageTypes.PROFILE, EventType.DANGER);
                    return false;
                }
                else
                {
                    sabotageEvent.Append(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenWin");
                }
                if (rivalGang.MemberNum <= 0) // clean up the rival gang
                {
                    rival.m_NumGangs--;

                    if (rival.m_NumGangs == 0)
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasNoMoreGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                    else if (rival.m_NumGangs <= 3)
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasAFewGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                    else
                    {
                        sabotageEvent.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheEnemyGangIsDestroyed[RivalName]HasALotOfGangsLeft",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    }
                }
                rivalGang = null;
            }
            else
            {
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenEncounterNoResistanceWhenyougoAfter[RivalGangName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalGangName", rival.m_Name) });
            }

            // if we had an objective to attack a rival we just achieved it
            if (Game.Brothels.GetObjective() != null && Game.Brothels.GetObjective().m_Objective == (int)Objectives.LAUNCHSUCCESSFULATTACK)
            {
                Game.Brothels.PassObjective();
            }

            // If the rival has some businesses under his control he's going to lose some of them
            if (rival.m_BusinessesExtort > 0)
            {
                // mod: brighter goons do better damage they need 100% to be better than before however
                int spread = gang.Intelligence / 4;
                int num = 1 + WMRand.Random(spread); // get the number of businesses lost
                if (rival.m_BusinessesExtort < num) // Can't destroy more businesses than they have
                {
                    num = rival.m_BusinessesExtort;
                }
                rival.m_BusinessesExtort -= num;

                if (rival.m_BusinessesExtort == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveNoMoreBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_BusinessesExtort <= 10)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveAFewBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenDestroy[Number]OfTheirBusinesses[RivalName]HaveALotOfBusinessesLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", num), new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            else
            {
                sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "[RivalName]HaveNoBusinessesToAttack",
                    new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
            }

            if (rival.m_Gold > 0)
            {
                // mod: brighter goons are better thieves
                // `J` changed it // they need 100% to be better than before however	
                // `J` now based on rival's gold
                // `J` bookmark - your gang sabotage mission gold taken
                int gold = WMRand.Random((int)(((double)gang.Intelligence / 2000.0) * (double)rival.m_Gold)) + WMRand.Random((gang.Intelligence / 5) * gang.MemberNum); // plus (int/5)*num -  0-5% of rival's gold
                if (gold > rival.m_Gold)
                {
                    gold = (int)rival.m_Gold;
                }
                rival.m_Gold -= gold;

                // some of the money taken 'dissappears' before the gang reports it.
                if (WMRand.Percent(20) && gold > 1000)
                {
                    gold -= WMRand.Random() % 1000;
                }

                if (rival.m_Gold == 0)
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThemMuhahahaha[RivalName]IsPennilessNow",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_Gold <= 10000)
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThem[RivalName]IsLookingPrettyPoor",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourMenSteal[GoldAmount]GoldFromThemItLooksLike[RivalName]StillHasALotOfGold",
                    new List<FormatStringParameter>() { new FormatStringParameter("GoldAmount", gold), new FormatStringParameter("RivalName", rival.m_Name) });
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

                    if (burnedbonds == 1)
                    {
                        sabotageEvent.AppendLineFormat(
                      LocalString.ResourceStringCategory.Global,
                      "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAGoldBearerBondWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                      new List<FormatStringParameter>() { new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }
                    else if (burnedbonds > 4)
                    {
                        sabotageEvent.AppendLineFormat(
                       LocalString.ResourceStringCategory.Global,
                       "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDropsAStackOfGoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                       new List<FormatStringParameter>() { new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }
                    else
                    {
                        sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsYourMenAreFleeingOneOfThemHasToJumpThroughAWallOfFireWhenHeDoesHeDrops[BurnedBonds]GoldBearerBondsWorth10kGoldEach[BurnedBondsCost]GoldJustWentUpInSmoke",
                        new List<FormatStringParameter>() { new FormatStringParameter("BurnedBonds", burnedbonds), new FormatStringParameter("BurnedBondsCost", bbcost) });
                    }

                }
                if (gold > 5000 && WMRand.Percent(50))
                {
                    limit = true;
                    int spill = (WMRand.Random() % 4500) + 500;
                    gold -= spill;
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsTheyAreBeingChasedThroughTheStreetsBy[RivalName]sPeopleOneOfYourGangMembersCutsOpenASackOfGoldSpillingItsContentsInTheStreetAsThThrongsOfCiviliansStreamInToCollectTheCoinsTheBlockThePursuersAndAllowYouMenToGetAwaySafely",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }

                if (gold > 5000)
                {
                    limit = true;
                    int bribeperc = ((WMRand.Random() % 15) * 5) + 10;
                    int bribe = (int)(gold * ((double)bribeperc / 100.0));
                    gold -= bribe;
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AsYourGangLeaveYourRivalsTerritoryOnTheWayBackToYourBrothelTheyComeUponABandOfLocalPoliceThatAreHuntingThemTheirBossDemands[BribePercent]OfWhatYourGangIsCarryingInOrderToLetThemGoTheyPayThem[Bribe]GoldAndContinueOnHome",
                        new List<FormatStringParameter>() { new FormatStringParameter("BribePercent", bribeperc), new FormatStringParameter("Bribe", bribe) });
                }

                if (limit)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]ReturnsWith[GoldAmount]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name), new FormatStringParameter("GoldAmount", gold) });
                }
                Game.Gold.plunder(gold);
            }
            else
            {
                sabotageEvent.AppendLine(
                    LocalString.ResourceStringCategory.Global,
                    "TheLosersHaveNoGoldToTake");
            }

            if (rival.m_NumInventory > 0 && WMRand.Percent(Math.Min(75, gang.Intelligence)))
            {
                cRivalManager r = new cRivalManager();
                int num = r.GetRandomRivalItemNum(rival);
                sInventoryItem item = r.GetRivalItem(rival, num);
                if (item != null)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenStealAnItemFromThemOne[ItemName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("ItemName", item.Name) });

                    r.RemoveRivalInvByNumber(rival, num);
                    Game.Brothels.AddItemToInventory(item);
                }
            }

            if (rival.m_NumBrothels > 0 && WMRand.Percent(gang.Intelligence / Math.Min(3, 11 - rival.m_NumBrothels)))
            {
                rival.m_NumBrothels--;
                rival.m_Power--;
                if (rival.m_NumBrothels == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasNoBrothelsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumBrothels <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]IsInControlOfVeryFewBrothels",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBrothels[RivalName]HasManyBrothelsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if (rival.m_NumGamblingHalls > 0 && WMRand.Percent(gang.Intelligence / Math.Min(1, 9 - rival.m_NumGamblingHalls)))
            {
                rival.m_NumGamblingHalls--;

                if (rival.m_NumGamblingHalls == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasNoGamblingHallsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumGamblingHalls <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]IsInControlOfVeryFewGamblingHalls",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sGamblingHalls[RivalName]HasManyGamblingHallsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if (rival.m_NumBars > 0 && WMRand.Percent(gang.Intelligence / Math.Min(1, 7 - rival.m_NumBars)))
            {
                rival.m_NumBars--;

                if (rival.m_NumBars == 0)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasNoBarsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else if (rival.m_NumBars <= 3)
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]IsInControlOfVeryFewBars",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    sabotageEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenBurnDownOneOf[RivalName]sBars[RivalName]HasManyBarsLeft",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }

            BoostGangSkill(gang.Stats[EnumStats.INTELLIGENCE], 2);
            gang.m_Events.AddMessage(sabotageEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);

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
                LocalString ssVic = new LocalString();
                ssVic.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YouHaveDealt[RivalName]AFatalBlowTheirCriminalOrganizationCrumblesToNothingBeforeYou",
                    new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                Game.Brothels.m_Rivals.RemoveRival(rival);
                gang.m_Events.AddMessage(ssVic.ToString(), ImageTypes.PROFILE, EventType.GOODNEWS);
            }
            return true;
        }

        /// <summary>
        /// Performe a gang recapturing mission against escaped player's girl.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionRecaptureGirls is affected to gang", true)]
        public bool RecaptureMission(Gang gang)
        {
            LocalString recaptureEven = new LocalString();
            recaptureEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsLookingForEscapedGirls",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });

            // check if any girls have run away, if no runnaway then the gang continues on as normal
            sGirl runnaway = Game.Brothels.GetFirstRunaway();
            if (runnaway == null) // `J` this should have been replaced by a check in the gang mission list
            {
                recaptureEven.AppendLine(
                    LocalString.ResourceStringCategory.Global,
                    "ThereAreNoneOfYourGirlsWhoHaveRunAwaySoTheyHaveNooneToLookFor");
                gang.m_Events.AddMessage(recaptureEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                return false;
            }

            LocalString RGmsg = new LocalString();
            string girlName = runnaway.m_Realname;
            bool captured = false;
            int damagedNets = 0;
            ImageTypes girlImageType = ImageTypes.PROFILE;
            EventType gangEventType = EventType.GANG;

            if (!Game.Brothels.FightsBack(runnaway))
            {
                recaptureEven.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourGoonsFind[GirlName]AndSheComesQuietlyWithoutPuttingUpAFight",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                RGmsg.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "[GirlName]WasRecapturedBy[GangName]SheGaveUpWithoutAFight",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                captured = true;
            }
            if (!captured && gang.NetLimit > 0) // try to capture using net
            {
                recaptureEven.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "YourGoonsFind[GirlName]AndTheyTryToCatchHerInTheirNets",
                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                int tries = 0;
                while (gang.NetLimit > 0 && !captured)
                {
                    int damagechance = 40;
                    if (WMRand.Percent(gang.Combat)) // hit her with the net
                    {
                        if (!WMRand.Percent((double)(runnaway.agility() - tries) / 2.0)) // she can't avoid or get out of the net
                        {
                            captured = true;
                        }
                        else
                        {
                            damagechance = 60;
                        }
                    }

                    if (WMRand.Percent(damagechance))
                    {
                        damagedNets++;
                        gang.AdjustNetLimit(-1);
                        m_NumNets--;
                    }
                    tries++;
                }
                if (captured)
                {
                    if (damagedNets > 0)
                    {
                        if (gang.NetLimit == 0)
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                        else if (gang.NetLimit == 1)
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                        else
                        {
                            recaptureEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                        }
                    }
                    recaptureEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow");
                    girlImageType = ImageTypes.DEATH;
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                    BoostGangSkill(gang.Stats[EnumStats.INTELLIGENCE], 2);
                }
                else
                {
                    recaptureEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                }
            }
            if (!captured)
            {
                if (Game.Brothels.FightsBack(runnaway)) // kidnap her
                {
                    if (damagedNets == 0)
                    {
                        recaptureEven.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GangName]AttemptToRecaptureHer",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                    }
                    if (!GangCombat(runnaway, gang))
                    {
                        girlImageType = ImageTypes.DEATH;
                        recaptureEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "SheFightsBackButYourMenSucceedInCapturingHer");
                        RGmsg.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                        BoostGangSkill(gang.Skills[EnumSkills.COMBAT], 1);
                        captured = true;
                    }
                    else
                    {
                        recaptureEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets");
                        gangEventType = EventType.DANGER;
                    }
                }
                else if (damagedNets == 0)
                {
                    recaptureEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]RecaptureHerSuccessfullyWithoutAFussSheIsInYourDungeonNow",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                    captured = true;
                }
                else
                {
                    recaptureEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer");
                    RGmsg.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight",
                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                    captured = true;
                }
            }

            gang.m_Events.AddMessage(recaptureEven.ToString(), ImageTypes.PROFILE, gangEventType);
            if (captured)
            {
                runnaway.m_Events.AddMessage(RGmsg.ToString(), girlImageType, EventType.GANG);
                runnaway.m_RunAway = 0;
                Game.Brothels.RemoveGirlFromRunaways(runnaway);
                Game.Dungeon.AddGirl(runnaway, DungeonReasons.GIRLRUNAWAY);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performe a gang extortion mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionExtortion is affected to gang", true)]
        public bool ExtortionMission(Gang gang)
        {
            LocalString extortionEven = new LocalString();
            Game.Player.disposition(-1);
            Game.Player.customerfear(1);
            Game.Player.suspicion(1);
            extortionEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsCapturingTerritory",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });

            // Case 1:  Neutral businesses still around
            int numB = Game.Rivals.GetNumBusinesses();
            int uncontrolled = Constants.TOWN_NUMBUSINESSES - m_BusinessesExtort - numB;
            int n = 0;
            int trycount = 1;
            if (uncontrolled > 0)
            {
                trycount += WMRand.Random() % 5; // 1-5
                while (uncontrolled > 0 && trycount > 0)
                {
                    trycount--;
                    if (WMRand.Percent(gang.Charisma / 2)) // convince
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.customerfear(-1);
                    }
                    else if (WMRand.Percent(gang.Intelligence / 2)) // outwit
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.disposition(-1);
                    }
                    else if (WMRand.Percent(gang.Combat / 2)) // threaten
                    {
                        uncontrolled--;
                        n++;
                        Game.Player.disposition(-1);
                        Game.Player.customerfear(2);
                    }
                }

                if (n == 0)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "TheyFailToGainAnyMoreNeutralTerritories");
                }
                else if (n == 1)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YouGainControlOfOneMoreNeutralTerritories");
                }
                else
                {
                    extortionEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YouGainControlOf[Number]MoreNeutralTerritory",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", n) });
                }
                m_BusinessesExtort += n;
                Game.Gold.extortion(n * 20);

                if (uncontrolled <= 0)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereAreNoMoreUncontrolledBusinessesLeft");
                }
                if (uncontrolled == 1)
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereIsOneUncontrolledBusinessesLeft");
                }
                else
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "ThereAreUncontrolledBusinessesLeft");
                }
            }
            else // Case 2: Steal bussinesses away from rival if no neutral businesses left
            {
                cRival rival = Game.Rivals.GetRandomRivalWithBusinesses();
                if (rival != null && rival.m_BusinessesExtort > 0)
                {
                    extortionEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "TheyStormIntoYourRival[[:RivalName:]]sTerritory",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                    bool defended = false;
                    if (rival.m_NumGangs > 0)
                    {
                        Gang rival_gang = GetTempGang(rival.m_Power);
                        defended = true;
                        extortionEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "YourMenRunIntoOneOfTheirGangsAndABrawlBreaksOut");

                        if (GangBrawl(gang, rival_gang, false))
                        {
                            trycount += WMRand.Random() % 3;

                            if (rival_gang.MemberNum <= 0)
                            {
                                extortionEven.Append(
                                    LocalString.ResourceStringCategory.Global,
                                    "TheyDestroyTheDefendersAnd");
                                rival.m_NumGangs--;
                            }
                            else
                            {
                                extortionEven.Append(
                                    LocalString.ResourceStringCategory.Global,
                                    "TheyDefeatTheDefendersAnd");
                            }
                        }
                        else
                        {
                            extortionEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "YourGangHasBeenDefeatedAndFailToTakeControlOfAnyNewTerritory");
                            gang.m_Events.AddMessage(extortionEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                            return false;
                        }
                        rival_gang = null;
                    }
                    else // Rival has no gangs
                    {
                        extortionEven.Append(
                            LocalString.ResourceStringCategory.Global,
                            "TheyFacedNoOppositionAsthey");
                        trycount += WMRand.Random() % 5;
                    }

                    while (trycount > 0 && rival.m_BusinessesExtort > 0)
                    {
                        trycount--;
                        rival.m_BusinessesExtort--;
                        m_BusinessesExtort++;
                        n++;
                    }

                    if (n > 0)
                    {
                        if (n == 1)
                        {
                            extortionEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "TookOverOneOf[RivalName]sTerritory",
                                new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                        }
                        else
                        {
                            extortionEven.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "TookOver[Number]Of[RivalName]sTerritories",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", n), new FormatStringParameter("RivalName", rival.m_Name) });
                        }
                    }
                    else
                    {
                        extortionEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "LeftErrorNoTerritoriesGainedButShouldHaveBeen");
                    }
                }
                else
                {
                    extortionEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YouFailToTakeControlOfAnyOfNewTerritories");
                }
            }

            gang.m_Events.AddMessage(extortionEven.ToString(), ImageTypes.PROFILE, EventType.GANG);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.EXTORTXNEWBUSINESS))
            {
                Game.Brothels.GetObjective().m_SoFar += n;
            }

            return true;
        }

        /// <summary>
        /// Performe a gang petty theft mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionPettyTheft is affected to gang", true)]
        public bool PettyTheftMission(Gang gang)
        {
            LocalString pettyTheftEven = new LocalString();
            pettyTheftEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsPerformingPettyTheft",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });

            Game.Player.disposition(-1);
            Game.Player.customerfear(1);
            Game.Player.suspicion(1);

            int gangMemberNumStart = gang.MemberNum;
            int gangMemberNumLost = 0;

            // `J` chance of running into a rival gang updated for .06.02.41
            int gangs = Game.Rivals.GetNumRivalGangs();
            int chance = 5 + Math.Max(20, gangs * 2); // 5% base +2% per gang, 25% max

            if (WMRand.Percent(chance))
            {
                cRival rival = Game.Rivals.GetRandomRivalWithGangs();
                if (rival != null && rival.m_NumGangs > 0)
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenRunIntoAGangFrom[RivalName]AndABrawlBreaksOut",
                        new List<FormatStringParameter>() { new FormatStringParameter("RivalName", rival.m_Name) });
                }
                else
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenRunIntoGroupOfThugsFromTheStreetsAndABrawlBreaksOut");
                }

                Gang rivalGang = GetTempGang();
                if (GangBrawl(gang, rivalGang, false))
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenWin");
                }
                else
                {
                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenLoseTheFight");
                    gang.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                    return false;
                }
                if (rival != null && rival.m_NumGangs > 0 && rivalGang.MemberNum <= 0)
                {
                    rival.m_NumGangs--;
                }
                rivalGang = null;

                gangMemberNumLost += gangMemberNumStart - gang.MemberNum;
            }
            else if (WMRand.Percent(1)) // `J` added for .06.02.41
            {
                sGirl girl = Game.Girls.GetRandomGirl();
                if (girl.has_trait("Incorporeal"))
                {
                    girl = Game.Girls.GetRandomGirl(); // try not to get an incorporeal girl but only 1 check
                }
                if (girl != null)
                {
                    string girlName = girl.m_Realname;
                    LocalString NGmsg = new LocalString();
                    ImageTypes girlImageType = ImageTypes.PROFILE;
                    EventType eventType = EventType.GANG;
                    EventType gangEventType = EventType.GANG;
                    DungeonReasons dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                    int damagedNets = 0;

                    // `J` make sure she is ready for a fight
                    if (girl.combat() < 50)
                    {
                        girl.combat(10 + WMRand.Random() % 30);
                    }
                    if (girl.magic() < 50)
                    {
                        girl.magic(10 + WMRand.Random() % 20);
                    }
                    if (girl.constitution() < 50)
                    {
                        girl.constitution(10 + WMRand.Random() % 20);
                    }
                    if (girl.agility() < 50)
                    {
                        girl.agility(10 + WMRand.Random() % 20);
                    }
                    if (girl.confidence() < 50)
                    {
                        girl.agility(10 + WMRand.Random() % 40);
                    }
                    girl.health(100);
                    girl.tiredness(-100);

                    pettyTheftEven.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenAreConfrontedByAMaskedVigilante");
                    if (!GangCombat(girl, gang))
                    {
                        gangMemberNumLost += gangMemberNumStart - gang.MemberNum;
                        int goldWin = girl.m_Money > 0 ? girl.m_Money : WMRand.Random() % 100 + 1; // take all her money or 1-100 if she has none
                        girl.m_Money = 0;
                        Game.Gold.petty_theft(goldWin);

                        if (gangMemberNumLost > gangMemberNumStart / 2)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsWellButYourMenStillManageToCaptureHer");
                        }
                        else if (gangMemberNumLost == 0)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButLosesQuickly");
                        }
                        else if (gangMemberNumLost == 1)
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButTheyTakeHerDownWithOnlyOneCasualty");
                        }
                        else
                        {
                            pettyTheftEven.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheFightsYourMenButTheyTakeHerDownWithOnlyAFewCasualties");
                        }
                        pettyTheftEven.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "TheyUnmask[GirlName]TakeAllHerGold[Number]FromHerAndDragHerToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", goldWin) });
                        girlImageType = ImageTypes.DEATH;
                        dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                        girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                        girl.add_trait("Kidnapped", 5 + WMRand.Random() % 11);

                        // TODO : What to do with NGmsg ?!?
                        NGmsg.AppendLineFormat(
                            LocalString.ResourceStringCategory.Global,
                            "[GirlName]TriedToStop[GangName]FromComittingPettyTheftButLostSheWasDraggedBackToTheDungeon",
                            new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                        BoostGangSkill(gang.Skills[EnumSkills.COMBAT], 1);

                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
                        {
                            Game.Brothels.GetObjective().m_SoFar += goldWin;
                        }
                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.KIDNAPXGIRLS))
                        {
                            Game.Brothels.GetObjective().m_SoFar++; // `J` You are technically kidnapping her
                        }
                        return true;
                    }
                    else
                    {
                        pettyTheftEven.AppendLine(
                            LocalString.ResourceStringCategory.Global,
                            "SheDefeatsYourMenAndDisappearsBackIntoTheShadows");
                        gang.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);
                        return false;
                    }
                }
            }

            int difficulty = Math.Max(0, WMRand.Bell(1, 6) - 2); // 0-4
            string who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoPeople");
            int fightBackChance = 0;
            int numberOfTargets = 2 + WMRand.Random() % 9;
            int targetFight = numberOfTargets;
            int gold = 0;
            int goldBase = 1;

            if (difficulty <= 0)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoKids");
                fightBackChance = 50;
                goldBase += 20;
                difficulty = 0;
            }
            if (difficulty == 1)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoLittleOldLadies");
                fightBackChance = 40;
                goldBase += 40;
            }
            if (difficulty == 2)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoNobleMenAndWomen");
                fightBackChance = 30;
                goldBase += 60;
            }
            if (difficulty == 3)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoSmallStalls");
                fightBackChance = 50;
                goldBase += 80;
            }
            if (difficulty >= 4)
            {
                who = LocalString.GetString(LocalString.ResourceStringCategory.Global, "WhoTraders");
                fightBackChance = 70;
                goldBase += 100;
                difficulty = 4;
            }

            for (int i = 0; i < numberOfTargets; i++)
            {
                gold += WMRand.Random() % goldBase;
            }

            if (WMRand.Percent(fightBackChance)) // determine losses if they fight back
            {
                while (gang.MemberNum > 0 && targetFight > 0) // fight until someone wins
                {
                    if (WMRand.Percent(gang.Combat))
                    {
                        targetFight--; // you win so lower their numbers
                    }
                    else if (WMRand.Percent(WMRand.Random() % 11 + (difficulty * 10))) // or they win
                    {
                        if (gang.HealLimit > 0)
                        {
                            gang.AdjustHealLimit(-1);
                            m_NumHealingPotions--;
                        } // but you heal
                        else
                        {
                            gang.MemberNum--;
                            gangMemberNumLost++;
                        } // otherwise lower your numbers
                    }
                }
            }

            if (gang.MemberNum <= 0)
            {
                return false; // they all died so return and the message will be taken care of in the losegang function
            }

            pettyTheftEven.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "YourGangRobs[NumberWho][Who]AndGet[NumberGold]GoldFromThem",
                new List<FormatStringParameter>() {
                    new FormatStringParameter("NumberWho", numberOfTargets),
                    new FormatStringParameter("Who", who),
                    new FormatStringParameter("NumberGold", gold)
                });
            if (gangMemberNumLost > 0)
            {
                if (gangMemberNumLost == 1)
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]LostOneMan",
                        new List<FormatStringParameter>() {
                            new FormatStringParameter("GangName", gang.Name)
                        });
                }
                else
                {
                    pettyTheftEven.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "[GangName]Lost[Number]Men",
                        new List<FormatStringParameter>() {
                            new FormatStringParameter("GangName", gang.Name),
                            new FormatStringParameter("Number", gangMemberNumLost),
                        });
                }
            }

            gang.m_Events.AddMessage(pettyTheftEven.ToString(), ImageTypes.PROFILE, EventType.GANG);

            Game.Gold.petty_theft(gold);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
            {
                Game.Brothels.GetObjective().m_SoFar += gold;
            }
            return true;
        }

        /// <summary>
        /// Performe a gang grand theft mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionGrandTheft is affected to gang", true)]
        public bool GrandTheftMission(Gang gang)
        {
            LocalString grandTheftEvent = new LocalString();
            Game.Player.disposition(-3);
            Game.Player.customerfear(3);
            Game.Player.suspicion(3);
            bool fightRival = false;
            cRival rival = null;
            Gang defenders = null;
            string place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThievePlace");
            int defenceChance = 0;
            int gold = 1;
            int difficulty = Math.Max(0, WMRand.Bell(0, 6) - 2); // 0-4

            if (difficulty <= 0)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThieveSmallShop");
                defenceChance = 10;
                gold += 10 + WMRand.Random() % 290;
                difficulty = 0;
            }
            if (difficulty == 1)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThieveSmithy");
                defenceChance = 30;
                gold += 50 + WMRand.Random() % 550;
            }
            if (difficulty == 2)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThieveJeweler");
                defenceChance = 50;
                gold += 200 + WMRand.Random() % 800;
            }
            if (difficulty == 3)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThieveTradeCaravan");
                defenceChance = 70;
                gold += 500 + WMRand.Random() % 1500;
            }
            if (difficulty >= 4)
            {
                place = LocalString.GetString(LocalString.ResourceStringCategory.Global, "ThieveBank");
                defenceChance = 90;
                gold += 1000 + WMRand.Random() % 4000;
                difficulty = 4;
            }

            grandTheftEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]GoesOutToRobA[ThievePlace]",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name), new FormatStringParameter("ThievePlace", place) });

            // `J` chance of running into a rival gang updated for .06.02.41
            int gangs = Game.Rivals.GetNumRivalGangs();
            int chance = 10 + Math.Max(30, gangs * 2); // 10% base +2% per gang, 40% max

            if (WMRand.Percent(chance))
            {
                rival = Game.Rivals.GetRandomRivalWithGangs();
                if (rival != null && rival.m_NumGangs > 0)
                {
                    fightRival = true;
                    defenders = GetTempGang(rival.m_Power);

                    grandTheftEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "The[ThievePlace]IsGuardedBAGangFrom[RivalName]",
                        new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place), new FormatStringParameter("RivalName", rival.m_Name) });
                }
            }
            if ((defenders == null) && WMRand.Percent(defenceChance))
            {
                defenders = GetTempGang(difficulty * 3);
                grandTheftEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "The[ThievePlace]HasItsOwnGuards",
                    new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place) });
            }
            if (defenders == null)
            {
                grandTheftEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "The[ThievePlace]IsUnguarded",
                    new List<FormatStringParameter>() { new FormatStringParameter("ThievePlace", place) });
            }

            if (defenders != null)
            {
                if (!GangBrawl(gang, defenders, false))
                {
                    grandTheftEvent.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenLoseTheFight");
                    gang.m_Events.AddMessage(grandTheftEvent.ToString(), ImageTypes.PROFILE, EventType.DANGER);
                    return false;
                }
                    grandTheftEvent.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "YourMenWin");
            }

            if (fightRival && defenders.MemberNum <= 0)
            {
                rival.m_NumGangs--;
            }
            defenders = null;

            // rewards
            grandTheftEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "TheyGetAwayWith[Number]GoldFromThe[ThievePlace]",
                new List<FormatStringParameter>() { new FormatStringParameter("Number", gold), new FormatStringParameter("ThievePlace", place) });

            // `J` zzzzzz - need to add items


            Game.Player.suspicion(gold / 1000);

            Game.Gold.grand_theft(gold);
            gang.m_Events.AddMessage(grandTheftEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);

            if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.STEALXAMOUNTOFGOLD))
            {
                Game.Brothels.GetObjective().m_SoFar += gold;
            }
            return true;
        }

        /// <summary>
        /// Performe a gang kidnapping mission against commerce.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionKidnappGirls is affected to gang", true)]
        public bool KidnappMission(Gang gang)
        {
            LocalString kidnappMissionEvent = new LocalString();
            kidnappMissionEvent.AppendLineFormat(
                LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsKidnappingGirls",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
            bool captured = false;

            if (WMRand.Percent(Math.Min(75, gang.Intelligence))) // chance to find a girl to kidnap
            {
                sGirl girl = Game.Girls.GetRandomGirl();
                if (girl != null)
                {
                    int[] v = { -1, -1 };
                    if (girl.m_Triggers.CheckForScript((int)Constants.TRIGGER_KIDNAPPED, true, v) != null)
                    {
                        return true; // not sure if they got the girl from the script but assume they do.
                    }

                    string girlName = girl.m_Realname;
                    LocalString NGmsg = new LocalString();
                    ImageTypes girlImageType = ImageTypes.PROFILE;
                    EventType eventType = EventType.GANG;
                    EventType gangEventType = EventType.GANG;
                    DungeonReasons dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                    int damagedNets = 0;


                    /* MYR: For some reason I can't figure out, a number of girl's house percentages
                    are at zero or set to zero when they are sent to the dungeon. I'm not sure
                    how to fix it, so I'm explicitly setting the percentage to 60 here */
                    // TODO : Remove house stat fixing when bug set to 0 when enter dungeon as fixed
                    girl.m_Stats[(int)EnumStats.HOUSE] = 60;

                    if (WMRand.Percent(Math.Min(75, gang.Charisma))) // convince her
                    {
                        kidnappMissionEvent.AppendLineFormat(
                           LocalString.ResourceStringCategory.Global,
                           "YourMenFindAGirl[GirlName]AndConvinceHerThatSheShouldWorkForYou",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        NGmsg.AppendLineFormat(
                           LocalString.ResourceStringCategory.Global,
                           "[GirlName]WasTalkedIntoWorkingForYouBy[GangName]",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                        dungeonReason = DungeonReasons.NEWGIRL;
                        BoostGangSkill(gang.Stats[EnumStats.CHARISMA], 3);
                        captured = true;
                        if ((Game.Brothels.GetObjective() != null) && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.KIDNAPXGIRLS))
                        {
                            Game.Brothels.GetObjective().m_SoFar++; // `J` Added to make Charisma Kidnapping count
                            if (WMRand.Percent(Game.Brothels.GetObjective().m_Target * 10)) // but possibly reduce the reward to gold only
                            {
                                Game.Brothels.GetObjective().m_Reward = (int)Rewards.GOLD;
                            }
                        }
                    }
                    if (!captured && gang.NetLimit > 0) // try to capture using net
                    {
                        kidnappMissionEvent.AppendLineFormat(
                           LocalString.ResourceStringCategory.Global,
                           "YourMenFindAGirl[GirlName]AndTryToCatchHerInTheirNets",
                           new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        int tries = 0;
                        while (gang.NetLimit > 0 && !captured)
                        {
                            int damageChance = 40;
                            if (WMRand.Percent(gang.Combat)) // hit her with the net
                            {
                                if (!WMRand.Percent((double)(girl.agility() - tries) / 2.0)) // she can't avoid or get out of the net
                                {
                                    captured = true;
                                }
                                else
                                {
                                    damageChance = 60;
                                }
                            }

                            if (WMRand.Percent(damageChance))
                            {
                                damagedNets++;
                                gang.AdjustNetLimit(-1);
                                m_NumNets--;
                            }
                            tries++;
                        }
                        if (captured)
                        {
                            if (damagedNets > 0)
                            {

                                if (gang.NetLimit == 0)
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.Global,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheTatteredRemainsOfTheirLastNet",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                                else if (gang.NetLimit == 1)
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.Global,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                                else
                                {
                                    kidnappMissionEvent.AppendLineFormat(
                                       LocalString.ResourceStringCategory.Global,
                                       "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirNets",
                                       new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                                }
                            }
                            kidnappMissionEvent.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "SheStrugglesAgainstTheNetYourMenUseButItIsPointlessSheIsInYourDungeonNow");
                            girlImageType = ImageTypes.DEATH;
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                            girl.add_trait("Kidnapped", 5 + WMRand.Random() % 11);
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamage[Number]OfTheirNetsBeforeTheyFinallyCaughtHerInTheirLastNet",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("Number", damagedNets) });
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]WasCapturedInANetAndDraggedBackToTheDungeonBy[GangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName), new FormatStringParameter("GangName", gang.Name) });
                            BoostGangSkill(gang.Stats[EnumStats.INTELLIGENCE], 2);
                        }
                        else
                        {
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]ManagedToDamageAllOfTheirNetsSoTheyHaveToDoThingsTheHardWay",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                        }
                    }
                    if (!captured)
                    {
                        if (Game.Brothels.FightsBack(girl)) // kidnap her
                        {
                            if (damagedNets == 0)
                            {
                                kidnappMissionEvent.AppendLineFormat(
                                    LocalString.ResourceStringCategory.Global,
                                    "YourMenFindAGirl[GirlName]AndAttemptToKidnapHer",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girlName) });
                            }
                            if (!GangCombat(girl, gang))
                            {
                                girlImageType = ImageTypes.DEATH;
                                dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                                girl.m_Stats[(int)EnumStats.OBEDIENCE] = 0;
                                girl.add_trait("Kidnapped", 10 + WMRand.Random() % 11);
                                kidnappMissionEvent.AppendLine(
                                    LocalString.ResourceStringCategory.Global,
                                    "SheFightsBackButYourMenSucceedInKidnappingHer");
                                NGmsg.AppendLineFormat(
                                    LocalString.ResourceStringCategory.Global,
                                    "[GirlName]FoughtWith[GangName]ButLostSheWasDraggedBackToTheDungeon",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                                BoostGangSkill(gang.Skills[EnumSkills.COMBAT], 1);
                                captured = true;
                            }
                            else
                            {
                                kidnappMissionEvent.AppendLine(
                                    LocalString.ResourceStringCategory.Global,
                                    "TheGirlFightsBackAndDefeatsYourMenBeforeEscapingIntoTheStreets");
                                gangEventType = EventType.DANGER;
                            }
                        }
                        else if (damagedNets == 0)
                        {
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.add_trait("Kidnapped", 3 + WMRand.Random() % 8);
                            kidnappMissionEvent.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GangName]KidnapHerSuccessfullyWithoutAFussSheIsInYourDungeonNow",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAFight",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                            captured = true;
                        }
                        else
                        {
                            dungeonReason = DungeonReasons.GIRLKIDNAPPED;
                            girl.add_trait("Kidnapped", 5 + WMRand.Random() % 8);
                            kidnappMissionEvent.AppendLine(
                                LocalString.ResourceStringCategory.Global,
                                "AfterDodgingAllOfTheirNetsSheGivesUpWhenTheyPullOutTheirWeaponsAndPrepareToKillHer");
                            NGmsg.AppendLineFormat(
                                LocalString.ResourceStringCategory.Global,
                                "[GirlName]WasSurroundedBy[GangName]AndGaveUpWithoutAnymoreOfAFight",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", girl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                            captured = true;
                        }
                    }

                    if (captured)
                    {
                        girl.m_Events.AddMessage(NGmsg.ToString(), girlImageType, eventType);
                        Game.Dungeon.AddGirl(girl, dungeonReason);
                        BoostGangSkill(gang.Stats[EnumStats.INTELLIGENCE], 1);
                    }
                    gang.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageTypes.PROFILE, gangEventType);
                }
                else
                {
                    kidnappMissionEvent.AppendLine(
                        LocalString.ResourceStringCategory.Global,
                        "TheyFailedToFindAnyGirlsToKidnap");
                    gang.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
                }
            }
            else
            {
                kidnappMissionEvent.AppendLine(
                    LocalString.ResourceStringCategory.Global,
                    "TheyFailedToFindAnyGirlsToKidnap");
                gang.m_Events.AddMessage(kidnappMissionEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            }
            return captured;
        }

        /// <summary>
        /// Performe a gang mission into catacombes.
        /// <remarks><para>`J` returns true if they succeded, false if they failed - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionCatacombs is affected to gang", true)]
        public bool CatacombsMission(Gang gang)
        {
            LocalString catacombsMissionEvent = new LocalString();
            gang.HasSeenCombat = true;
            int num = gang.MemberNum;
            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsExploringTheCatacombs",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });

            if (!m_ControlGangs) // use old code
            {
                catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global, "YouTellThemToGetWhateverTheyCanFind");

                // determine losses
                gang.HasSeenCombat = true;
                for (int i = 0; i < num; i++)
                {
                    if (WMRand.Percent(gang.Combat))
                    {
                        continue;
                    }
                    if (gang.HealLimit == 0)
                    {
                        gang.MemberNum--;
                        continue;
                    }
                    if (WMRand.Percent(5)) // `J` 5% chance they will not get the healing potion in time.
                    {
                        gang.MemberNum--; // needed to have atleast some chance or else they are totally invincable.
                    }
                    gang.AdjustHealLimit(-1);
                    m_NumHealingPotions--;
                }

                if (gang.MemberNum <= 0)
                {
                    return false;
                }
                else
                {
                    if (num == gang.MemberNum)
                    {
                        catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "All[Number]OfThemReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.MemberNum) });
                    }
                    else
                    {
                        catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "[Number]OfThe[GangNumber]WhoWentOutReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.MemberNum), new FormatStringParameter("GangNumber", num) });
                    }

                    // determine loot
                    int gold = gang.MemberNum;
                    gold += WMRand.Random() % (gang.MemberNum * 100);
                    catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                        "TheyBringBackWithThem[Number]Gold",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
                    Game.Gold.catacomb_loot(gold);

                    int items = 0;
                    while (WMRand.Percent((gang.Intelligence / 2) + 30) && items <= (gang.MemberNum / 3)) // item chance
                    {
                        bool quit = false;
                        bool add = false;
                        sInventoryItem temp = Game.Inventory.GetRandomCatacombItem();
                        if (temp != null)
                        {
                            catacombsMissionEvent.Comma();
                            catacombsMissionEvent.NewLine();
                            int curI = Game.Brothels.HasItem(temp.Name, -1);
                            bool loop = true;
                            while (loop)
                            {
                                if (curI != -1)
                                {
                                    if (Game.Brothels.m_NumItem[curI] >= 999)
                                    {
                                        curI = Game.Brothels.HasItem(temp.Name, curI + 1);
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
                                    catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                        "One[ItemName]",
                                        new List<FormatStringParameter>() { new FormatStringParameter("ItemName", temp.Name) });
                                    Game.Brothels.m_NumItem[curI]++;
                                    items++;
                                }
                                else
                                {
                                    for (int j = 0; j < Constants.MAXNUM_INVENTORY; j++)
                                    {
                                        if (Game.Brothels.m_Inventory[j] == null)
                                        {
                                            catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                                "One[ItemName]",
                                                new List<FormatStringParameter>() { new FormatStringParameter("ItemName", temp.Name) });
                                            Game.Brothels.m_Inventory[j] = temp;
                                            items++;
                                            Game.Brothels.m_EquipedItems[j] = 0;
                                            Game.Brothels.m_NumInventory++;
                                            Game.Brothels.m_NumItem[j]++;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                quit = true;
                                catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                                    "YourInventoryIsFull");
                            }
                        }

                        if (quit)
                        {
                            break;
                        }
                    }
                    catacombsMissionEvent.Dot();


                    int girl = 0;
                    // determine if get a catacomb girl (is "monster" if trait not human)
                    if (WMRand.Percent((gang.Intelligence / 4) + 25))
                    {
                        sGirl ugirl = null;
                        bool unique = false;
                        if (WMRand.Percent(50))
                        {
                            unique = true; // chance of getting unique girl
                        }
                        if (unique)
                        {
                            ugirl = Game.Girls.GetRandomGirl(false, true);
                            if (ugirl == null)
                            {
                                unique = false;
                            }
                        }

                        if ((Game.Brothels.GetObjective() != null) && Game.Brothels.GetObjective().m_Objective == (int)Objectives.CAPTUREXCATACOMBGIRLS)
                        {
                            Game.Brothels.GetObjective().m_SoFar++;
                        }

                        catacombsMissionEvent.NewLine();

                        LocalString NGmsg = new LocalString();
                        if (unique)
                        {
                            girl++;
                            catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                "YourMenAlsoCapturedAGirlNamed[GirlName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname) });

                            // TODO : Need comprehention
                            ugirl.m_States &= ~(1 << (int)Status.CATACOMBS);
                            ugirl.add_trait("Kidnapped", 2 + WMRand.Random() % 10);
                            NGmsg.AppendFormat(LocalString.ResourceStringCategory.Global,
                                "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                            ugirl.m_Events.AddMessage(NGmsg.ToString(), ImageTypes.PROFILE, EventType.GANG);
                            Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                        }
                        else
                        {
                            ugirl = Game.Girls.CreateRandomGirl(0, false, false, false, true);
                            if (ugirl != null) // make sure a girl was returned
                            {
                                girl++;
                                catacombsMissionEvent.Append(LocalString.ResourceStringCategory.Global,
                                    "YourMenAlsoCapturedAGirl");
                                ugirl.add_trait("Kidnapped", 2 + WMRand.Random() % 10);
                                NGmsg.AppendFormat(LocalString.ResourceStringCategory.Global,
                                    "[GirlName]WasCapturedInTheCatacombsBy[GangName]",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                                ugirl.m_Events.AddMessage(NGmsg.ToString(), ImageTypes.PROFILE, EventType.GANG);
                                Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                            }
                        }
                    }
                    // `J` determine if they bring back any beasts
                    int beasts = Math.Max(0, (WMRand.Random() % 5) - 2);
                    if (girl == 0 && gang.MemberNum > 13)
                    {
                        beasts++;
                    }
                    if (beasts > 0 && WMRand.Percent(gang.MemberNum * 5))
                    {
                        catacombsMissionEvent.NewLine();
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "YourMenAlsoBringBack[Number]Beasts",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", beasts) });
                        Game.Brothels.add_to_beasts(beasts);
                    }
                }
            }
            else	// use new code
            {
                int totalGirls = 0;
                int totalItems = 0;
                int totalBeast = 0;
                int bringBackNum = 0;
                int gold = 0;

                // do the intro text
                string girlCatacombsLookFor = Game.Girls.catacombs_look_for(m_GangGetsGirls, m_GangGetsItems, m_GangGetsBeast);
                // TODO : Add string to event (verify g_Girls.catacombs_look_for use resources language string)
                //catacombsMissionEvent.Append(girlCatacombsLookFor);

                // do the bring back loop
                while (gang.MemberNum >= 1 && bringBackNum < gang.MemberNum * Math.Max(1, gang.Strength / 20))
                {
                    double choice = (WMRand.Random() % 10001) / 100.0;
                    gold += WMRand.Random() % (gang.MemberNum * 20);

                    if (choice < m_GangGetsGirls) // get girl = 10 point
                    {
                        bool gotGirl = false;
                        sGirl tempGirl = Game.Girls.CreateRandomGirl(18, false, false, false, true); // `J` Legal Note: 18 is the Legal Age of Majority for the USA where I live
                        if (gang.NetLimit > 0) // try to capture using net
                        {
                            int tries = 0;
                            while (gang.NetLimit > 0 && !gotGirl) // much harder to net a girl in the catacombs
                            {
                                int damagechance = 40; // higher damage net chance in the catacombs
                                if (WMRand.Percent(gang.Combat)) // hit her with the net
                                {
                                    if (!WMRand.Percent((double)(tempGirl.agility() - tries))) // she can't avoid or get out of the net
                                    {
                                        gotGirl = true;
                                    }
                                    else
                                    {
                                        damagechance = 80;
                                    }
                                }

                                if (WMRand.Percent(damagechance))
                                {
                                    gang.AdjustNetLimit(-1);
                                    m_NumNets--;
                                }
                                tries++;
                            }
                        }
                        if (!gotGirl) // fight the girl if not netted
                        {
                            if (!GangCombat(tempGirl, gang))
                            {
                                gotGirl = true;
                            }
                        }
                        tempGirl = null;
                        if (gotGirl)
                        {
                            bringBackNum += 10;
                            totalGirls++;
                        }
                        else
                        {
                            bringBackNum += 5;
                        }
                    }
                    else if (choice < m_GangGetsGirls + m_GangGetsItems) // get item = 4 points
                    {
                        bool gotitem = false;
                        if (WMRand.Percent(33)) // item is guarded
                        {
                            sGirl tempgirl = Game.Girls.CreateRandomGirl(18, false, false, false, true); // `J` Legal Note: 18 is the Legal Age of Majority for the USA where I live
                            if (!GangCombat(tempgirl, gang))
                            {
                                gotitem = true;
                            }
                            if (WMRand.Percent(20))
                            {
                                totalItems++;
                                bringBackNum += 2;
                            }
                            else if (WMRand.Percent(50))
                            {
                                gold += 1 + WMRand.Random() % 200;
                            }
                        }
                        else
                        {
                            gotitem = true;
                        }
                        if (gotitem)
                        {
                            bringBackNum += 4;
                            totalItems++;
                        }
                        else
                        {
                            bringBackNum += 2;
                        }
                    }
                    else // get beast = 2 point
                    {
                        bool gotBeast = false;
                        if (gang.NetLimit > 0) // try to capture using net
                        {
                            while (gang.NetLimit > 0 && !gotBeast)
                            {
                                int damageChance = 50; // higher damage net chance in the catacombs
                                if (WMRand.Percent(gang.Combat)) // hit it with the net
                                {
                                    if (!WMRand.Percent(60))
                                    {
                                        gotBeast = true;
                                    }
                                    else
                                    {
                                        damageChance = 80;
                                    }
                                }
                                if (WMRand.Percent(damageChance))
                                {
                                    gang.AdjustNetLimit(-1);
                                    m_NumNets--;
                                }
                            }
                        }
                        if (!gotBeast) // fight it
                        {
                            // the last few members will runaway or allow the beast to run away so that the can still bring back what they have
                            while (gang.MemberNum > 1 + WMRand.Random() % 3 && !gotBeast)
                            {
                                if (WMRand.Percent(Math.Min(90, gang.Combat)))
                                {
                                    gotBeast = true;
                                    continue;
                                }
                                if (gang.HealLimit == 0)
                                {
                                    gang.MemberNum--;
                                    continue;
                                }
                                // `J` 5% chance they will not get the healing potion in time.
                                // needed to have atleast some chance or else they are totally invincable.
                                if (WMRand.Percent(5))
                                {
                                    gang.MemberNum--;
                                }
                                gang.AdjustHealLimit(-1);
                                m_NumHealingPotions--;
                            }
                        }
                        if (gotBeast)
                        {
                            int numbeasts = 1 + WMRand.Random() % 3;
                            bringBackNum += numbeasts * 2;
                            totalBeast += numbeasts;
                        }
                        else
                        {
                            bringBackNum++;
                        }
                    }
                }

                // determine loot
                if (gang.MemberNum < 1)
                {
                    return false; // they all died
                }
                else
                {
                    if (num == gang.MemberNum)
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "All[Number]OfThemReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.MemberNum) });
                    }
                    else
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "[Number]OfThe[GangNumber]WhoWentOutReturn",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.MemberNum), new FormatStringParameter("GangNumber", num) });
                    }

                    if (gold > 0)
                    {
                        catacombsMissionEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "TheyBringBackWithThem[Number]Gold",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
                        Game.Gold.catacomb_loot(gold);
                    }

                    // get catacomb girls (is "monster" if trait not human)
                    if (totalGirls > 0)
                    {
                        if (totalGirls == 1)
                        {
                            catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                                "YourMenCapturedOneGirl");
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "YourMenCaptured[Number]Girls",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalGirls) });
                        }

                        for (int i = 0; i < totalGirls; i++)
                        {
                            sGirl ugirl = null;
                            bool unique = WMRand.Percent(Configuration.Catacombs.UniqueCatacombs); // chance of getting unique girl
                            if (unique)
                            {
                                ugirl = Game.Girls.GetRandomGirl(false, true);
                                if (ugirl == null)
                                {
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                    "[UniqueGirlName]Unique",
                                    new List<FormatStringParameter>() { new FormatStringParameter("UniqueGirlName", ugirl.m_Realname) });
                       
                                // TODO : Need comprehention
                                ugirl.m_States &= ~(1 << (int)Status.CATACOMBS);

                                LocalString NGmsg = new LocalString();
                                ugirl.add_trait("Kidnapped", 2 + WMRand.Random() % 10);
                                NGmsg.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                    "[GirlName] was captured in the catacombs by [GangName]",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                                ugirl.m_Events.AddMessage(NGmsg.ToString(), ImageTypes.PROFILE, EventType.GANG);
                                Game.Dungeon.AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                            }
                            else
                            {
                                ugirl = Game.Girls.CreateRandomGirl(0, false, false, false, true);
                                if (ugirl != null) // make sure a girl was returned
                                {
                                    catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                        "[GirlName]",
                                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname) });
                                    LocalString NGmsg = new LocalString();
                                    ugirl.add_trait("Kidnapped", 2 + WMRand.Random() % 10);
                                    NGmsg.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                        "[GirlName] was captured in the catacombs by [GangName]",
                                        new List<FormatStringParameter>() { new FormatStringParameter("GirlName", ugirl.m_Realname), new FormatStringParameter("GangName", gang.Name) });
                                    ugirl.m_Events.AddMessage(NGmsg.ToString(), ImageTypes.PROFILE, EventType.GANG);
                                    Game.Brothels.GetDungeon().AddGirl(ugirl, DungeonReasons.GIRLCAPTURED);
                                }
                            }
                            if ((ugirl != null) && (Game.Brothels.GetObjective() != null)
                                && (Game.Brothels.GetObjective().m_Objective == (int)Objectives.CAPTUREXCATACOMBGIRLS))
                            {
                                Game.Brothels.GetObjective().m_SoFar++;
                            }
                        }
                    }
                    catacombsMissionEvent.NewLine();

                    // get items
                    if (totalItems > 0)
                    {
                        if (totalItems == 1)
                        {
                            catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                                "YourMenBringBackOneItem");
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "YourMenBringBack[Number]Items",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalItems) });
                        }

                        for (int i = 0; i < totalItems; i++)
                        {
                            bool quit = false;
                            bool add = false;
                            sInventoryItem temp = Game.Inventory.GetRandomCatacombItem();
                            if (temp != null)
                            {
                                catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                    "[ItemName]",
                                    new List<FormatStringParameter>() { new FormatStringParameter("ItemName", temp.Name) });
                                int curI = Game.Brothels.HasItem(temp.Name, -1);
                                bool loop = true;
                                while (loop)
                                {
                                    if (curI != -1)
                                    {
                                        if (Game.Brothels.m_NumItem[curI] >= 999)
                                        {
                                            curI = Game.Brothels.HasItem(temp.Name, curI + 1);
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
                                        Game.Brothels.m_NumItem[curI]++;
                                    }
                                    else
                                    {
                                        for (int j = 0; j < Constants.MAXNUM_INVENTORY; j++)
                                        {
                                            if (Game.Brothels.m_Inventory[j] == null)
                                            {
                                                Game.Brothels.m_Inventory[j] = temp;
                                                Game.Brothels.m_EquipedItems[j] = 0;
                                                Game.Brothels.m_NumInventory++;
                                                Game.Brothels.m_NumItem[j]++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    quit = true;
                                    catacombsMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                                        "YourInventoryIsFull");
                                }
                            }
                            if (quit)
                            {
                                break;
                            }
                        }
                    }

                    // bring back any beasts
                    if (totalBeast > 0)
                    {
                        if (totalGirls + totalItems > 0)
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "YourMenAlsoBringBack[Number]Beasts",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalBeast) });
                        }
                        else
                        {
                            catacombsMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "YourMenBringBack[Number]Beasts",
                                new List<FormatStringParameter>() { new FormatStringParameter("Number", totalBeast) });
                        }
                        Game.Brothels.add_to_beasts(totalBeast);
                    }
                }
            }
            gang.m_Events.AddMessage(catacombsMissionEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            return true;
 
        }

        /// <summary>
        /// Performe a gang service mission.
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionService is affected to gang", true)]
        public bool ServiceMission(Gang gang)
        {
            LocalString serviceMissionEvent = new LocalString();
            serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                "Gang[GangName]SpendTheWeekHelpingOutTheCommunity",
                new List<FormatStringParameter>() {new FormatStringParameter("GangName", gang.Name)});

            int suspicion = WMRand.Bell(0, 2);
            int customerFear = WMRand.Bell(0, 2);
            int disposition = WMRand.Bell(0, 3);
            int service = WMRand.Bell(0, 3);
            int charisma = 0;
            int intelligence = 0;
            int agility = 0;
            int magic = 0;
            int gold = 0;
            int security = 0;
            int beasts = 0;
            int percent = Math.Max(10, Math.Min(gang.MemberNum * 6, gang.Service));

            for (int i = 0; i < gang.MemberNum / 2; i++)
            {
                if (WMRand.Percent(percent))
                {
                    switch (WMRand.Random() % 9)
                    {
                        case 0:
                            suspicion++;
                            break;
                        case 1:
                            customerFear++;
                            break;
                        case 2:
                            disposition++;
                            break;
                        case 3:
                            charisma++;
                            break;
                        case 4:
                            intelligence++;
                            break;
                        case 5:
                            agility++;
                            break;
                        case 6:
                            magic++;
                            break;
                        case 7:
                            gold += WMRand.Random() % 10 + 1;
                            break;
                        default:
                            service++;
                            break;
                    }
                }
            }

            if (gang.MemberNum < 15 && WMRand.Percent(Math.Min(50, gang.Charisma)))
            {
                int addNum = Math.Max(1, WMRand.Bell(-2, 4));
                if (addNum + gang.MemberNum > 15)
                {
                    addNum = 15 - gang.MemberNum;
                }
                serviceMissionEvent.NewLine();;

                if (addNum <= 1)
                {
                    addNum = 1;
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "ALocalBoyDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                else if (addNum == 2)
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "TwoLocalsDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                else
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "SomeLocalsDecidedToJoinYourGangToHelpOutTheirCommunity");
                }
                gang.MemberNum += addNum;
            }

            if (WMRand.Percent(Math.Max(10, Math.Min(gang.MemberNum * 6, gang.Intelligence))))
            {
                sBrothel brothel = Game.Brothels.GetRandomBrothel();
                security = Math.Max(5 + WMRand.Random() % 26, gang.Intelligence / 4);
                brothel.m_SecurityLevel += security;
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "TheyCleanedUpAround[BrothelName]FixingLightsRemovingDebrisAndMakingSureTheAreaIsSecure",
                    new List<FormatStringParameter>() { new FormatStringParameter("BrothelName", brothel.m_Name) });
            }
            if (WMRand.Percent(Math.Max(10, Math.Min(gang.MemberNum * 6, gang.Intelligence))))
            {
                beasts += (Math.Max(1, WMRand.Bell(-4, 4)));
                if (beasts <= 1)
                {
                    beasts = 1;
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "TheyRoundedUpAStrayBeastAndBroughtItToTheBrothel");
                }
                else if (beasts == 2)
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "TheyRoundedUpTwoStrayBeastsAndBroughtThemToTheBrothel");
                }
                else
                {
                    serviceMissionEvent.AppendLine(LocalString.ResourceStringCategory.Global,
                        "TheyRoundedUpSomeStrayBeastsAndBroughtThemToTheBrothel");
                }
            }

            if (security > 0)
            {
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Security[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", security) });
            }
            if (beasts > 0)
            {
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Beasts[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", beasts) });
            }
            if (suspicion > 0)
            {
                Game.Player.suspicion(-suspicion);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Suspicion[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", suspicion) });
            }
            if (customerFear > 0)
            {
                Game.Player.customerfear(-customerFear);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "CustomerFear[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", customerFear) });
            }
            if (disposition > 0)
            {
                Game.Player.disposition(disposition);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Disposition[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", disposition) });
            }
            if (service > 0)
            {
                gang.AdjustSkill(EnumSkills.SERVICE, service);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Service[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", service) });
            }
            if (charisma > 0)
            {
                gang.AdjustStat(EnumStats.CHARISMA, charisma);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Charisma[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", charisma) });
            }
            if (intelligence > 0)
            {
                gang.AdjustStat(EnumStats.INTELLIGENCE, intelligence);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Intelligence[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", intelligence) });
            }
            if (agility > 0)
            {
                gang.AdjustStat(EnumStats.AGILITY, agility);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Agility[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", agility) });
            }
            if (magic > 0)
            {
                gang.AdjustSkill(EnumSkills.MAGIC, magic);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "Magic[Number]",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", magic) });
            }
            if (gold > 0)
            {
                Game.Gold.misc_credit(gold);
                serviceMissionEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "TheyRecieved[Number]GoldInTipsFromGratefulPeople",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gold) });
            }

            gang.m_Events.AddMessage(serviceMissionEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            return true;
        }

        /// <summary>
        /// Performe a gang training mission.
        /// <remarks><para>`J` - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionTraining is affected to gang", true)]
        public bool GangTraining(Gang gang)
        {
            LocalString gangTrainingEvent = new LocalString();
            gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                "Gang[GangName]SpendTheWeekTrainingAndImprovingTheirSkills",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });

            int oldCombat = gang.Combat;
            int oldMagic = gang.Magic;
            int oldIntelligence = gang.Intelligence;
            int oldAgility = gang.Agility;
            int oldConstitution = gang.Constitution;
            int oldCharisma = gang.Charisma;
            int oldStrength = gang.Strength;
            int oldService = gang.Service;

            List<IValuableAttribut> possibleSkills = new List<IValuableAttribut>();
            possibleSkills.Add(gang.Skills[EnumSkills.COMBAT]);
            possibleSkills.Add(gang.Skills[EnumSkills.MAGIC]);
            possibleSkills.Add(gang.Stats[EnumStats.INTELLIGENCE]);
            possibleSkills.Add(gang.Stats[EnumStats.AGILITY]);
            possibleSkills.Add(gang.Stats[EnumStats.CONSTITUTION]);
            possibleSkills.Add(gang.Stats[EnumStats.CHARISMA]);
            possibleSkills.Add(gang.Stats[EnumStats.STRENGTH]);
            possibleSkills.Add(gang.Skills[EnumSkills.SERVICE]);

            int count = (WMRand.Random() % 3) + 2; // get 2-4 potential skill/stats to boost
            for (int i = 0; i < count; i++)
            {
                int boostCount = (WMRand.Random() % 3) + 1; // boost each 1-3 times
                BoostGangRandomSkill(possibleSkills, 1, boostCount);
            }
            possibleSkills.Clear();

            if (gang.Skills[EnumSkills.COMBAT].Value > oldCombat)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Combat",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Skills[EnumSkills.COMBAT].Value - oldCombat) });
            }
            if (gang.Skills[EnumSkills.MAGIC].Value > oldMagic)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Magic",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Skills[EnumSkills.MAGIC].Value - oldMagic) });
            }
            if (gang.Stats[EnumStats.INTELLIGENCE].Value > oldIntelligence)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Intelligence",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Stats[EnumStats.INTELLIGENCE].Value - oldIntelligence) });
            }
            if (gang.Stats[EnumStats.AGILITY].Value > oldAgility)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Agility",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Stats[EnumStats.AGILITY].Value - oldAgility) });
            }
            if (gang.Stats[EnumStats.CONSTITUTION].Value > oldConstitution)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Toughness",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Stats[EnumStats.CONSTITUTION].Value - oldConstitution) });
            }
            if (gang.Stats[EnumStats.CHARISMA].Value > oldCharisma)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Charisma",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Stats[EnumStats.CHARISMA].Value - oldCharisma) });
            }
            if (gang.Stats[EnumStats.STRENGTH].Value > oldStrength)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Strength",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Stats[EnumStats.STRENGTH].Value - oldStrength) });
            }
            if (gang.Skills[EnumSkills.SERVICE].Value > oldService)
            {
                gangTrainingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                    "[Number]Service",
                    new List<FormatStringParameter>() { new FormatStringParameter("Number", gang.Skills[EnumSkills.SERVICE].Value - oldService) });
            }

            gang.m_Events.AddMessage(gangTrainingEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            gang.HasSeenCombat = false;
            return false;
        }

        /// <summary>
        /// Performe a gang training mission.
        /// <remarks><para>`J` - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> performing mission.</param>
        /// <returns><b>True</b> if mission is a sucess</returns>
        [Obsolete("Use gang.CurrentMission.DoTheJob() when GangMissionRecruit is affected to gang", true)]
        public bool GangRecruiting(Gang gang)
        {
            LocalString gangRecruitingEvent = new LocalString();
            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                "Gang[GangName]IsRecruiting",
                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
            int recruit = 0;
            int start = WMRand.Bell(1, 6); // 1-6 people are available for recruitment
            int available = start;
            int add = Math.Max(0, WMRand.Bell(0, 4) - 1); // possibly get 1-3 without having to ask
            start += add;
            int playerDisposition = Game.Player.disposition();
            while (available > 0)
            {
                int chance = gang.Charisma;
                if (WMRand.Percent(gang.Magic / 4))
                {
                    chance += gang.Magic / 10;
                }
                if (WMRand.Percent(gang.Combat / 4))
                {
                    chance += gang.Combat / 10;
                }
                if (WMRand.Percent(gang.Intelligence / 4))
                {
                    chance += gang.Intelligence / 10;
                }
                if (WMRand.Percent(gang.Agility / 4))
                {
                    chance += gang.Agility / 10;
                }
                if (WMRand.Percent(gang.Constitution / 4))
                {
                    chance += gang.Constitution / 10;
                }
                if (WMRand.Percent(gang.Strength / 4))
                {
                    chance += gang.Strength / 10;
                }

                // less chance of them wanting to work for really evil or really good player
                if (playerDisposition < -50)
                {
                    chance += (playerDisposition + 50) / 2; // -25 for -100 disp
                }
                if (playerDisposition > -20 && playerDisposition < 0)
                {
                    chance += (22 + playerDisposition) / 2; // +1 for -19  to +10 for -2
                }
                if (playerDisposition == 0)
                {
                    chance += 10; // +10 for -2,-1,0,1,2
                }
                if (playerDisposition < 20 && playerDisposition > 0)
                {
                    chance += (22 - playerDisposition) / 2; // +1 for 19   to +10 for 2
                }
                if (playerDisposition > 50)
                {
                    chance -= (playerDisposition - 50) / 3; // -16 for > 98 disp
                }

                if (chance > 90)
                {
                    chance = 90;
                }
                if (chance < 20)
                {
                    chance = 20; // 20-90% chance
                }
                if (WMRand.Percent(chance))
                {
                    add++;
                }
                available--;
            }

            while (add > recruit && gang.MemberNum < 15)
            {
                recruit++;
                gang.MemberNum++;
            }
            if (start < 1)
            {
                gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "TheyWereUnableToFindAnyoneToRecruit");
            }
            else
            {
                if (start == 1)
                {
                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyFoundOnePersonToTryToRecruit");
                }
                else
                {
                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyFoundPeopleToTryToRecruit");
                }

                if (start == 1)
                {
                    if (add == start)
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndTheyGotHimToJoin");
                    }
                    else
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButHeDidntWantToJoin");
                    }
                }
                else if (add <= 0)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButWereUnableToGetAnyToJoin");
                }
                else if (add == start)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndManagedToGetAllOfThemToJoin");
                }
                else if (add == 1)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButWereOnlyAbleToConvinceOneOfThemToJoin");
                }
                else
                {
                    gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                        "AndWereAbleToConvince[Number]OfThemToJoin",
                        new List<FormatStringParameter>() { new FormatStringParameter("Number", add) });
                }

                if (gang.MemberNum >= 15 && add == recruit)
                {
                    gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "TheyGotAsManyAsTheyNeededToFillTheirRanks");
                }
                else if (gang.MemberNum >= 15 && add > recruit)
                {
                    gang.MemberNum = 15;
                    if (recruit == 1)
                    {
                        gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyOnlyHadRoomForOneMoreInTheirGangSoThey");
                    }
                    else
                    {
                        gangRecruitingEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                            "TheyOnlyHadRoomFor[Number]MoreInTheirGangSoThey",
                            new List<FormatStringParameter>() { new FormatStringParameter("Number", recruit) });
                    }
                    int passNum = add - recruit;
                    Gang passTo = GetGangRecruitingNotFull(passNum);
                    if (passTo != null)
                    {
                        gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                            "SentTheRestToJoin[GangName]",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", passTo.Name) });
                        LocalString pss = new LocalString();
                        if (passNum > 1)
                        {
                            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "[GangName]Sent[Number]RecruitsThatTheyHadNoRoomForTo[ToGangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name), new FormatStringParameter("Number", passNum), new FormatStringParameter("ToGangName", passTo.Name) });
                        }
                        else
                        {
                            gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                "[GangName]SentOneRecruitThatTheyHadNoRoomForTo[ToGangName]",
                                new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name), new FormatStringParameter("ToGangName", passTo.Name) });
                        }
                        int passNumGotThere = 0;
                        for (int i = 0; i < passNum; i++)
                        {
                            if (passTo.MissionType == EnuGangMissions.Recruit)
                            {
                                if (WMRand.Percent(75))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Training)
                            {
                                if (WMRand.Percent(50))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.SpyGirls)
                            {
                                if (WMRand.Percent(95))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Guarding)
                            {
                                if (WMRand.Percent(30))
                                {
                                    passNumGotThere++;
                                }
                            }
                            if (passTo.MissionType == EnuGangMissions.Service)
                            {
                                if (WMRand.Percent(90))
                                {
                                    passNumGotThere++;
                                }
                            }
                        }
                        if (passNumGotThere > 0)
                        {
                            if (passNumGotThere == passNum)
                            {
                                if (passNum > 1)
                                {
                                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyAllArrived");
                                }
                                else
                                {
                                    gangRecruitingEvent.Append(LocalString.ResourceStringCategory.Global, "TheyArrived");
                                }
                            }
                            else
                            {
                                gangRecruitingEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                                    "Only[Number]Arrived",
                                    new List<FormatStringParameter>() { new FormatStringParameter("Number", passNumGotThere) });
                            }
                            if (passTo.MemberNum + passNumGotThere <= 15)
                            {
                                gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "AndGotAcceptedIntoTheGang");
                            }
                            else
                            {
                                passNumGotThere = 15 - passTo.MemberNum;
                                gangRecruitingEvent.AppendLineFormat(LocalString.ResourceStringCategory.Global,
                                    "But[GangName]CouldOnlyTake[Number]OfThem",
                                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", passTo.Name), new FormatStringParameter("Number", passNumGotThere) });
                            }
                            passTo.MemberNum += passNumGotThere;
                        }
                        else
                        {
                            gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "ButNoneShowedUp");
                        }
                        passTo.m_Events.AddMessage(pss.ToString(), ImageTypes.PROFILE, EventType.GANG);
                    }
                    else
                    {
                        gangRecruitingEvent.AppendLine(LocalString.ResourceStringCategory.Global, "HadToTurnAwayTheRest");
                    }
                }
            }
            gang.m_Events.AddMessage(gangRecruitingEvent.ToString(), ImageTypes.PROFILE, EventType.GANG);
            gang.HasSeenCombat = true; // though not actually combat, this prevents the automatic +1 member at the end of the week
            return false;
        }
        #endregion

        // TODO : Is there a good place for this methode? Call GangMission.GetName function.
        /// <summary>
        /// Declaring losing gang general event.
        /// <remarks><para>`J` - updated for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to remove.</param>
        /// <returns><b>True</b> gang as no member left and wase remove from player's gang list. <b>False</b> if gang ase alway one or more member</returns>
        public bool LoseGang(Gang gang)
        {
            if (gang.MemberNum <= 0)
            {
                LocalString loseGangEvent = new LocalString();
                GangMissionBase mission = gang.CurrentMission ?? GangMissionBase.None;
                loseGangEvent.AppendFormat(LocalString.ResourceStringCategory.Global,
                    "[GangName]WasLostWhile[GangMissionName]",
                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name), new FormatStringParameter("GangMissionName", mission.GetMissionName()) });

                //switch (mission)
                //{
                //    case GangMissions.Guarding:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "Guarding");
                //        break;
                //    case GangMissions.Sabotage:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "AttackingYourRivals");
                //        break;
                //    case GangMissions.SpyGirls:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "SpyingOnYourGirls");
                //        // TODO : Log, a gang cant be lose while spying girl
                //        break;
                //    case GangMissions.RecaptureGirls:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "TryingToRecaptureARunaway");
                //        break;
                //    case GangMissions.Extortion:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "TryingToExtortNewBusinesses");
                //        break;
                //    case GangMissions.PettyTheft:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "PerformingPettyCrimes");
                //        break;
                //    case GangMissions.GrandTheft:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "PerformingMajorCrimes");
                //        break;
                //    case GangMissions.KidnappGirls:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "TryingToKidnapGirls");
                //        break;
                //    case GangMissions.Catacombs:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "ExploringTheCatacombs");
                //        break;
                //    case GangMissions.Training:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "Training");
                //        // TODO : Log, a gang cant be lose while training
                //        break;
                //    case GangMissions.Recruit:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "Recruiting");
                //        // TODO : Log, a gang cant be lose while recruiting
                //        break;
                //    case GangMissions.Service:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "HelpingTheCommunity");
                //        break;
                //    default:
                //        loseGangEvent.Append(LocalString.ResourceStringCategory.Global, "OnAMission");
                //        // TODO : Log, GangMissions unknown
                //        break;
                //}
                Game.MessageQue.Enqueue(loseGangEvent.ToString(), MessageCategory.COLOR_RED);
                RemoveGang(gang);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check for gang recruiting and set gang mission according to member number.
        /// <remarks><para>`J` - Added for .06.01.09</para></remarks>
        /// </summary>
        /// <param name="gang"><see cref="Gang"/> to check.</param>
        public void CheckGangRecruit(Gang gang)
        {
            LocalString checkGangRecruitEvent = new LocalString();
            if (gang.MissionType == EnuGangMissions.Service || gang.MissionType == EnuGangMissions.Training)
            {
                // `J` service and training can have as few as 1 member doing it.
            }
            else if (gang.MemberNum <= 5 && gang.MissionType != EnuGangMissions.Recruit)
            {
                checkGangRecruitEvent.AppendLineFormat(
                    LocalString.ResourceStringCategory.Global,
                    "Gang[GangName]WereSetToRecruitDueToLowNumbers",
                    new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                gang.m_Events.AddMessage(checkGangRecruitEvent.ToString(), ImageTypes.PROFILE, EventType.WARNING);
                gang.AutoRecruit = true;
                gang.LastMission = gang.CurrentMission;
                GangMissionBase.SetGangMission(EnuGangMissions.Recruit, gang);
            }
            else if (gang.MissionType == EnuGangMissions.Recruit && gang.MemberNum >= 15)
            {
                if (gang.AutoRecruit)
                {
                    checkGangRecruitEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "Gang[GangName]WerePlacedBackOnTheirPreviousMissionNowThatTheirNumbersAreBackToNormal",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                    gang.CurrentMission = gang.LastMission;
                    gang.AutoRecruit = false;
                }
                else
                {
                    checkGangRecruitEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "Gang[GangName]WerePlacedOnGuardDutyFromRecruitmentAsTheirNumbersAreFull",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", gang.Name) });
                    GangMissionBase.SetGangMission(EnuGangMissions.Guarding, gang);
                }
                gang.m_Events.AddMessage(checkGangRecruitEvent.ToString(), ImageTypes.PROFILE, EventType.WARNING);
            }
        }

        // TODO : Extract all mission relative to corresponding GangMission object
        /// <summary>
        /// Reffiling gang net and healing pot.
        /// <remarks><para>`J` - Added for .06.01.09</para></remarks>
        /// </summary>
        public void GangStartOfShift()
        {
            cTariff tariff = new cTariff();
            LocalString gangStartOfShiftEvent = new LocalString();

            RestockNetsAndPots();

            if (m_PlayerGangList.Count.Equals(0))
            {
                return; // no gangs
            }

            int totalPots = m_NumHealingPotions;
            int totalNets = m_NumNets;
            int gangsNeedingNets = 0;
            int gangsNeedingPots = 0;
            int givePots = 0;
            int giveNets = 0;
            int potsPassedOut = 0;
            int netsPassedOut = 0;


            // update goons for the start of the turn
            int cost = 0;
            for (int i = m_PlayerGangList.Count - 1; i >= 0; i--)
            {
                Gang currentGang = m_PlayerGangList[i];

                if (currentGang.MemberNum <= 0) // clear dead
                {
                    gangStartOfShiftEvent.AppendLineFormat(
                        LocalString.ResourceStringCategory.Global,
                        "AllOfTheMenInGang[GangName]HaveDied",
                        new List<FormatStringParameter>() { new FormatStringParameter("GangName", currentGang.Name) });
                    Game.MessageQue.Enqueue(gangStartOfShiftEvent.ToString(), MessageCategory.COLOR_RED);

                    RemoveGang(currentGang);
                    continue;
                }

                currentGang.HasSeenCombat = false;
                currentGang.m_Events.Clear();
                cost += tariff.goon_mission_cost(currentGang.MissionType); // sum up the cost of all the goon missions
                currentGang.NetLimit = 0;
                currentGang.HealLimit = 0;

                CheckGangRecruit(currentGang);

                if (currentGang.MissionType == EnuGangMissions.SpyGirls)
                {
                    string localString = LocalString.GetStringFormatLine(LocalString.ResourceStringCategory.Global,
                            "Gang[GangName]IsSpyingOnYourGirls",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", currentGang.Name) } );
                    currentGang.m_Events.AddMessage(localString, ImageTypes.PROFILE, EventType.GANG);
                }
                if (currentGang.MissionType == EnuGangMissions.Guarding)
                {
                    string localString = LocalString.GetStringFormatLine(LocalString.ResourceStringCategory.Global,
                            "Gang[GangName]IsGuarding",
                            new List<FormatStringParameter>() { new FormatStringParameter("GangName", currentGang.Name) } );
                    currentGang.m_Events.AddMessage(localString, ImageTypes.PROFILE, EventType.GANG);
                }
            }
            Game.Gold.goon_wages(cost);

            if (m_PlayerGangList.Count < 1)
            {
                return; // all gangs died?
            }
            if (totalPots == 0 && totalNets == 0)
            {
                return; // no potions or nets
            }

            // check numbers needed
            foreach (Gang currentGang in m_PlayerGangList)
            {
                switch (currentGang.MissionType)
                {
                    case EnuGangMissions.SpyGirls:
                    case EnuGangMissions.Guarding:
                    case EnuGangMissions.Sabotage:
                    case EnuGangMissions.Extortion:
                    case EnuGangMissions.PettyTheft:
                    case EnuGangMissions.GrandTheft:
                        gangsNeedingPots++;
                        break;
                    case EnuGangMissions.RecaptureGirls:
                    case EnuGangMissions.KidnappGirls:
                    case EnuGangMissions.Catacombs:
                        gangsNeedingPots++;
                        gangsNeedingNets++;
                        break;
                    default:
                        break;
                }
            }

            if (gangsNeedingNets == 0 && gangsNeedingPots == 0)
            {
                return; // no gang needs any so don't pass them out
            }

            if (totalPots > 0 && gangsNeedingPots > 0)
            {
                givePots = 1 + (totalPots / gangsNeedingPots);
            }
            if (totalNets > 0 && gangsNeedingNets > 0)
            {
                giveNets = 1 + (totalNets / gangsNeedingNets);
            }

            foreach (Gang currentGang in m_PlayerGangList)
            {
                if (potsPassedOut + givePots > totalPots)
                {
                    givePots = totalPots - potsPassedOut;
                }
                if (netsPassedOut + giveNets > totalNets)
                {
                    giveNets = totalNets - netsPassedOut;
                }

                switch (currentGang.MissionType)
                {
                    case EnuGangMissions.Guarding:
                    case EnuGangMissions.SpyGirls:
                    case EnuGangMissions.Sabotage:
                    case EnuGangMissions.Extortion:
                    case EnuGangMissions.PettyTheft:
                    case EnuGangMissions.GrandTheft:
                        currentGang.HealLimit = givePots;
                        potsPassedOut += givePots;
                        break;
                    case EnuGangMissions.RecaptureGirls:
                    case EnuGangMissions.KidnappGirls:
                    case EnuGangMissions.Catacombs:
                        currentGang.HealLimit = givePots;
                        potsPassedOut += givePots;
                        currentGang.NetLimit = giveNets;
                        netsPassedOut += giveNets;
                        break;
                    default:
                        break;
                }
            }
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

    }
}
