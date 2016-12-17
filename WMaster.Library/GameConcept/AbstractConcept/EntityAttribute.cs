using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WMaster.GameConcept.AbstractConcept
{
    /// <summary>
    /// Represente an abstract entity attribut based on enumeration (like Skills, Stats...)
    /// who can store value <b>short</b> because between -100 to 100 and may store little history :
    /// last week and last yeat to know attribut progression.
    /// </summary>
    /// <typeparam name="TEnumType">The enum type representing attribut.</typeparam>
    public abstract class EntityAttribute<TEnumType> : ITurnable, IValuableAttribut
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
        protected EntityAttribute()
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
