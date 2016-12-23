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
    using System.Xml.Serialization;
    using WMaster.Manager;

    /// <summary>
    /// Represente an abstract entity attribut based on enumeration (like Skills, Stats...)
    /// who can store value <b>short</b> because between -100 to 100 and may store little history :
    /// last week and last yeat to know attribut progression.
    /// </summary>
    /// <typeparam name="TEnumType">The enum type representing attribut.</typeparam>
    public abstract class Attribute<TEnumType> : ITurnable, IValuableAttribut
        where TEnumType : struct, IConvertible
    {
        #region Fields & Properties
        /// <summary>
        /// Current value of Attribute. Arribut value with all modifiers applies.
        /// </summary>
        private int m_CurrentValue;
        /// <summary>
        /// Base value of Attribute without any modifier apply.
        /// </summary>
        private int m_BaseValue;

        /// <summary>
        /// Get or set the Caracteristique skill.
        /// </summary>
        [XmlAttribute("Name")]
        public TEnumType Caracteristique { get; set; }

        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        public abstract int UpperBoundLimit { get; }
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        public abstract int LowerBoundLimit { get; }

        /// <summary>
        /// Get or ser the base value of Attribute. It's the original value without modifier like items, drugs, charms....
        /// </summary>
        [XmlAttribute("Value")]
        public int Value
        {
            get { return this.CurrentValue; }
            set { this.m_CurrentValue = Math.Max(Math.Min(value, this.UpperBoundLimit), this.LowerBoundLimit); }
        }

        /// <summary>
        /// Get or set the current value of Attribute. Arribut value with all modifiers affected.
        /// </summary>
        [XmlIgnore()]
        public int CurrentValue
        {
            get { return this.m_CurrentValue; }
        }

        /// <summary>
        /// Caracteristic value of last week. Need to know weekly progression value.
        /// </summary>
        private int? m_LastWeekValue = null;
        /// <summary>
        /// Get the set the caracteristic value of last week.
        /// Needed to know weekly progression value.
        /// <remarks><para>WARNING !!! Setting this value must only be donne by XmlSerializer!</para></remarks>
        /// </summary>
        [XmlAttribute("LastWeekValue")]
        public int? LastWeekValue
        {
            get { return this.m_LastWeekValue; }
            set { this.m_LastWeekValue = value; }
        }

        /// <summary>
        /// Caracteristic value of last year. Need to know annual progression value.
        /// </summary>
        private int? m_LastYearValue = null;
        /// <summary>
        /// Get or set the caracteristic value of last year.
        /// Needed to know annual progression value.
        /// <remarks><para>WARNING !!! Setting this value must only be donne by XmlSerializer!</para></remarks>
        /// </summary>
        [XmlAttribute("LastYearValue")]
        public int? LastYearValue
        {
            get { return this.m_LastYearValue; }
            set { this.m_LastYearValue = value; }
        }
        #endregion

        /// <summary>
        /// Protected constructor, to ensure no instance of this object can be initialize.
        /// <remarks><para>Check if <typeparamref name="TEnumType"/> is en enum.</para></remarks>
        /// </summary>
        protected Attribute()
        {
            if (!typeof(TEnumType).IsEnum)
            { throw new ArgumentException("TEnumType must be an enumerated type"); }
        }

        /// <summary>
        /// Close current week and do traitement for initialise new week.
        /// </summary>
        public void NextWeek()
        {
            this.LastWeekValue = this.Value;
        }

        /// <summary>
        /// Close current year and do traitement for initialise new year.
        /// </summary>
        public void NextYear()
        {
            this.LastYearValue = this.Value;
        }

        /// <summary>
        /// Return the weekly progression of <typeparamref name="TEnumType"/>.
        /// </summary>
        /// <returns>Weekly progression of <typeparamref name="TEnumType"/>.</returns>
        public int WeekProgression()
        {
            return (this.LastWeekValue.HasValue) ? (this.Value - this.LastWeekValue.Value) : 0;
        }

        /// <summary>
        /// Return the annual progression of <typeparamref name="TEnumType"/>.
        /// </summary>
        /// <returns>Annual progression of <typeparamref name="TEnumType"/></returns>
        public int YearProgression()
        {
            return (this.LastYearValue.HasValue) ? (this.Value - this.LastYearValue.Value) : 0;
        }
    }
}
