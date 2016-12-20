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
namespace WMaster.Game.Entity.Living
{
    using System;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Enum;
    using WMaster.Game.Concept;

    /// <summary>
    /// Class of gang definition.
    /// </summary>
    public class Gang :
        ITurnable,
        ICaracteristicOwnEntity
    {
        /// <summary>
        /// Current number of member in the gang.
        /// </summary>
        private int m_Num;
        /// <summary>
        /// Get or set the current number of member in the gang.
        /// </summary>
        public int MemberNum
        {
            get { return this.m_Num; }
            set { this.m_Num = Math.Max(value, 0); }
        }

        /// <summary>
        /// List of all skills of the gang.
        /// </summary>
        private SkillsCollection m_Skills = new SkillsCollection();
        /// <summary>
        /// Get the list of all skills of the gang.
        /// </summary>
        public SkillsCollection Skills
        {
            get { return this.m_Skills; }
        }

        /// <summary>
        /// List of all stats of the gang.
        /// </summary>
        private StatsCollection m_Stats = new StatsCollection();
        /// <summary>
        /// Get the list of all stats of the gang.
        /// </summary>
        public StatsCollection Stats
        {
            get { return this.m_Stats; }
        }

        /// <summary>
        /// The type of mission currently performing.
        /// </summary>
        public GangMissions m_MissionID;
        /// <summary>
        /// Get or set the type of mission currently performing.
        /// </summary>
        public GangMissions CurrentMission
        {
            get { return this.m_MissionID; }
            set { this.m_MissionID = value; }
        }

        /// <summary>
        /// The last mission if auto changed to recruit mission.
        /// </summary>
        public GangMissions m_LastMissID;
        /// <summary>
        /// Get or set the last mission if auto changed to recruit mission.
        /// </summary>
        public GangMissions LastMission
        {
            get { return this.m_LastMissID; }
            set { this.m_LastMissID = value; }
        }

        /// <summary>
        /// Number of potions the gang has.
        /// </summary>
        public int m_Heal_Limit;
        /// <summary>
        /// Get or set the number of potions the gang has.
        /// </summary>
        public int HealLimit
        {
            get { return this.m_Heal_Limit; }
            set { this.m_Heal_Limit = Math.Max(value, 0); }
        }
        /// <summary>
        /// Adjust heal potion number adding <paramref name="n"/> item.
        /// </summary>
        /// <param name="n">Number of health potion to add</param>
        public void AdjustHealLimit(int n)
        {
            this.HealLimit += n;
        }

        /// <summary>
        /// Number of nets the gang has.
        /// </summary>
        public int m_Net_Limit;
        /// <summary>
        /// Get or set the number of nets the gang has.
        /// </summary>
        public int NetLimit
        {
            get { return this.m_Net_Limit; }
            set { this.m_Net_Limit = Math.Max(value, 0); }
        }
        /// <summary>
        /// Adjust net number adding <paramref name="n"/> item.
        /// </summary>
        /// <param name="n">Number of net to add</param>
        public void AdjustNetLimit(int n)
        {
            this.NetLimit += n;
        }

        /// <summary>
        /// <b>True</b> if auto recruiting
        /// </summary>
        public bool AutoRecruit { get; set; }

        /// <summary>
        /// Name of gang.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <b>True </b> when gang has seen combat in the last week.
        /// </summary>
        private bool m_Combat;
        /// <summary>
        /// <b>True </b> when gang has seen combat in the last week.
        /// </summary>
        public bool HasSeenCombat
        {
            get { return m_Combat; }
            set { m_Combat = value; }
        }

        public Events m_Events = new Events();

        public IXmlElement SaveGangXML(IXmlElement pRoot)
        {
            throw new NotImplementedException();
            //TiXmlElement pGang = new TiXmlElement("Gang");
            //pRoot.LinkEndChild(pGang);
            //pGang.SetAttribute("Num", m_Num);
            //SaveSkillsXML(pGang, m_Skills);
            //SaveStatsXML(pGang, m_Stats);
            //pGang.SetAttribute("Name", m_Name);
            //return pGang;
        }
        public bool LoadGangXML(IXmlHandle hGang)
        {
            throw new NotImplementedException();
            //TiXmlElement pGang = hGang.ToElement();
            //if (pGang == 0)
            //{
            //    return false;
            //}
            //if (pGang.Attribute("Name"))
            //{
            //    m_Name = pGang.Attribute("Name");
            //}
            //pGang.QueryIntAttribute("Num", m_Num);
            //LoadSkillsXML(hGang.FirstChild("Skills"), m_Skills);
            //LoadStatsXML(hGang.FirstChild("Stats"), m_Stats);
            //if (m_Skills[SKILL_MAGIC] <= 0 || m_Skills[SKILL_COMBAT] <= 0 || m_Stats[STAT_INTELLIGENCE] <= 0 || m_Stats[STAT_AGILITY] <= 0 || m_Stats[STAT_CONSTITUTION] <= 0 || m_Stats[STAT_CHARISMA] <= 0 || m_Stats[STAT_STRENGTH] <= 0 || m_Skills[SKILL_SERVICE] <= 0)
            //{
            //    int total = Math.Max(0, m_Skills[SKILL_MAGIC]) + Math.Max(0, m_Skills[SKILL_COMBAT]) + Math.Max(0, m_Stats[STAT_INTELLIGENCE]) + Math.Max(0, m_Stats[STAT_AGILITY]) + Math.Max(0, m_Stats[STAT_CONSTITUTION]) + Math.Max(0, m_Stats[STAT_CHARISMA]) + Math.Max(0, m_Stats[STAT_STRENGTH]);
            //    int low = total / 8;
            //    int high = total / 6;
            //    if (m_Skills[SKILL_MAGIC] <= 0)
            //    {
            //        m_Skills[SKILL_MAGIC] = g_Dice.bell(low, high);
            //    }
            //    if (m_Skills[SKILL_COMBAT] <= 0)
            //    {
            //        m_Skills[SKILL_COMBAT] = g_Dice.bell(low, high);
            //    }
            //    if (m_Stats[STAT_INTELLIGENCE] <= 0)
            //    {
            //        m_Stats[STAT_INTELLIGENCE] = g_Dice.bell(low, high);
            //    }
            //    if (m_Stats[STAT_AGILITY] <= 0)
            //    {
            //        m_Stats[STAT_AGILITY] = g_Dice.bell(low, high);
            //    }
            //    if (m_Stats[STAT_CONSTITUTION] <= 0)
            //    {
            //        m_Stats[STAT_CONSTITUTION] = g_Dice.bell(low, high);
            //    }
            //    if (m_Stats[STAT_CHARISMA] <= 0)
            //    {
            //        m_Stats[STAT_CHARISMA] = g_Dice.bell(low, high);
            //    }
            //    if (m_Stats[STAT_STRENGTH] <= 0)
            //    {
            //        m_Stats[STAT_STRENGTH] = g_Dice.bell(low, high);
            //    }
            //    if (m_Skills[SKILL_SERVICE] <= 0)
            //    {
            //        m_Skills[SKILL_SERVICE] = g_Dice.bell(low / 2, high); // `J` added for .06.02.41
            //    }
            //}

            ////these may not have been saved
            ////if not, the query just does not set the value
            ////so the default is used, assuming the gang was properly init
            //pGang.QueryValueAttribute<u_int>("MissionID", m_MissionID);
            //pGang.QueryIntAttribute("LastMissID", m_LastMissID);
            //pGang.QueryValueAttribute<bool>("Combat", m_Combat);
            //pGang.QueryValueAttribute<bool>("AutoRecruit", m_AutoRecruit);

            //return true;
        }

        /// <summary>
        /// Get Magic skill value.
        /// </summary>
        public int Magic
        {
            get { return Skills[EnumSkills.MAGIC].Value; }
        }
        /// <summary>
        /// Get Combat skill value.
        /// </summary>
        public int Combat
        {
            get { return Skills[EnumSkills.COMBAT].Value; }
        }
        /// <summary>
        /// Get Service skill value.
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        public int Service
        {
            get { return Skills[EnumSkills.SERVICE].Value; }
        }
        /// <summary>
        /// Get Intelligence stats value.
        /// </summary>
        public int Intelligence
        {
            get { return Stats[EnumStats.INTELLIGENCE].Value; }
        }
        /// <summary>
        /// Get Agility stats value.
        /// </summary>
        public int Agility
        {
            get { return Stats[EnumStats.AGILITY].Value; }
        }
        /// <summary>
        /// Get Constitution stats value.
        /// </summary>
        public int Constitution
        {
            get { return Stats[EnumStats.CONSTITUTION].Value; }
        }
        /// <summary>
        /// Get Charisma stats value.
        /// </summary>
        public int Charisma
        {
            get { return Stats[EnumStats.CHARISMA].Value; }
        }
        /// <summary>
        /// Get Strength stats value.
        /// </summary>
        public int Strength
        {
            get { return Stats[EnumStats.STRENGTH].Value; }
        }
        /// <summary>
        /// Get Happiness stats value.
        /// </summary>
        public int Happy
        {
            get { return Stats[EnumStats.HAPPINESS].Value; }
        }

        /// <summary>
        /// Adjust gang skill to skill value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="gang">Instance of gang to adjust.</param>
        /// <param name="skill">Skill to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        public static void AdjustGangSkill(Gang gang, EnumSkills skill, int amount)
        {
            gang.AdjustSkill(skill, amount);
        }
        /// <summary>
        /// Adjust gang skill to skill value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="skill">Skill to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustSkill(EnumSkills skill, int amount)
        {
            this.Skills[skill].Value += amount;
            this.Skills[skill].Value = Math.Max(Math.Min(this.Skills[skill].Value, 100), 0);
        }

        /// <summary>
        /// Adjust gang statistic to statistic value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="gang">Instance of gang to adjust.</param>
        /// <param name="stat">Stat to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        public static void AdjustGangStat(Gang gang, EnumStats stat, int amount)
        {
            gang.AdjustStat(stat, amount);
        }
        /// <summary>
        /// Adjust gang statistic to statistic value + <paramref name="amount"/> borned between 0 to 100
        /// <remarks><para>`J` added for .06.02.41</para></remarks>
        /// </summary>
        /// <param name="stat">Stat to adjust.</param>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustStat(EnumStats stat, int amount)
        {
            this.Stats[stat].Value += amount;
            this.Stats[stat].Value = Math.Max(Math.Min(this.Stats[stat].Value, 100), 0);
        }

        /// <summary>
        /// Adjusting Magic gang skill to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustMagic(int amount)
        {
            this.AdjustSkill(EnumSkills.MAGIC, amount);
        }
        /// <summary>
        /// Adjusting Combat gang skill to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustCombat(int amount)
        {
            this.AdjustSkill(EnumSkills.COMBAT, amount);
        }
        /// <summary>
        /// Adjusting Service gang skill to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustService(int amount)
        {
            this.AdjustSkill(EnumSkills.SERVICE, amount);
        }
        /// <summary>
        /// Adjusting Intelligence gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustIntelligence(int amount)
        {
            this.AdjustStat(EnumStats.INTELLIGENCE, amount);
        }
        /// <summary>
        /// Adjusting Agility gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustAgility(int amount)
        {
            this.AdjustStat(EnumStats.AGILITY, amount);
        }
        /// <summary>
        /// Adjusting Constitution gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustConstitution(int amount)
        {
            this.AdjustStat(EnumStats.CONSTITUTION, amount);
        }
        /// <summary>
        /// Adjusting Charisma gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustCharisma(int amount)
        {
            this.AdjustStat(EnumStats.CHARISMA, amount);
        }
        /// <summary>
        /// Adjusting Strength gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustStrength(int amount)
        {
            this.AdjustStat(EnumStats.STRENGTH, amount);
        }
        /// <summary>
        /// Adjusting Happiness gang stat to <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Adjusting amount value.</param>
        public void AdjustHappy(int amount)
        {
            this.AdjustStat(EnumStats.HAPPINESS, amount);
        }

        /// <summary>
        /// Initialise a new instance of Gang with default value.
        /// </summary>
        public Gang()
        {
            Name = "Unnamed";
            m_Num = 0;

            m_MissionID = GangMissions.GUARDING;
            m_LastMissID = GangMissions.NONE;

            HasSeenCombat = false;
            AutoRecruit = false;

            foreach (Skill item in this.Skills)
            { item.Value = 0; }
            foreach (Stat item in this.Stats)
            { item.Value = 0; }
            this.Stats[EnumStats.HEALTH].Value = 100;
            this.Stats[EnumStats.HAPPINESS].Value = 100;
        }

        public void NextWeek()
        {
            foreach (Skill item in m_Skills)
            {
                item.NextWeek();
            }
            foreach (Stat item in m_Stats)
            {
                item.NextWeek();
            }
            this.HasSeenCombat = false;
        }

        public void NextYear()
        {
            foreach (Skill item in m_Skills)
            {
                item.NextYear();
            }
            foreach (Stat item in m_Stats)
            {
                item.NextYear();
            }
        }
    }

}
