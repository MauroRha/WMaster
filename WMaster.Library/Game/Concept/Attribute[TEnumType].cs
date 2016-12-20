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
namespace WMaster.Game.Concept
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Represente an abstract entity attribut based on enumeration (like Skills, Stats...)
    /// who can store value <b>short</b> because between -100 to 100 and may store little history :
    /// last week and last yeat to know attribut progression.
    /// </summary>
    /// <typeparam name="TEnumType">The enum type representing attribut.</typeparam>
    public abstract class Attribute<TEnumType> : ITurnable, IValuableAttribut
        where TEnumType : struct, IConvertible
    {
        /// <summary>
        /// Maximum value attribut can take.
        /// </summary>
        protected abstract int UpperBoundLimit { get; }
        /// <summary>
        /// Minimum value attribut can take.
        /// </summary>
        protected abstract int LowerBoundLimit { get; }

        /// <summary>
        /// Get or set the Caracteristique skill.
        /// </summary>
        [XmlAttribute("Name")]
        public TEnumType Caracteristique { get; set; }

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
        /// Get or ser the current value of Attribute. Just a Wrapper to CurrentValue property.
        /// </summary>
        [XmlIgnore()]
        public int Value
        {
            get { return this.CurrentValue; }
            set { this.CurrentValue = value; }
        }

        private int m_CurrentValue;
        /// <summary>
        /// Get or set the current value of Attribute
        /// </summary>
        [XmlAttribute("CurrentValue")]
        public int CurrentValue
        {
            get { return this.m_CurrentValue; }
            set { this.m_CurrentValue = Math.Max(Math.Min(value, this.UpperBoundLimit), this.LowerBoundLimit); }
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
        private int? m_LastYearValue = -1;
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

        /// <summary>
        /// Close current week and do traitement for initialise new week.
        /// </summary>
        public void NextWeek()
        {
            this.LastWeekValue = this.CurrentValue;
        }

        /// <summary>
        /// Close current year and do traitement for initialise new year.
        /// </summary>
        public void NextYear()
        {
            this.LastYearValue = this.CurrentValue;
        }

        /// <summary>
        /// Return the weekly progression of <typeparamref name="TEnumType"/> .
        /// </summary>
        /// <returns>Weekly progression of <typeparamref name="TEnumType"/> </returns>
        public int WeekProgression()
        {
            return (this.LastWeekValue.HasValue) ? this.CurrentValue - this.LastWeekValue.Value : 0;
        }

        /// <summary>
        /// Return the annual progression of <typeparamref name="TEnumType"/> .
        /// </summary>
        /// <returns>Annual progression of <typeparamref name="TEnumType"/> </returns>
        public int YearProgression()
        {
            return (this.LastYearValue.HasValue) ? (this.CurrentValue - this.LastYearValue.Value) : 0;
        }
    }
}
