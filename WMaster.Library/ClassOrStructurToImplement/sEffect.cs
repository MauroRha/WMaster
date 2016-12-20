using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public struct sEffect
    {
        // MOD docclox
        /*
         *	let's have an enum for possible values of m_Affects
         */
        public enum What
        {
            Skill = 0,
            Stat = 1,
            Nothing = 2,
            GirlStatus = 3,
            Trait = 4,
            Enjoy = 5
        };
        public What m_Affects;
        /*
         *	define an ostream operator so it will pretty print
         *	(more useful for debugging than game play
         *	but why take it out?)
         */
        //	friend ostream& operator << (ostream& os, sEffect::What &w);
        /*
         *	and a function to go the other way
         *	we need this to turn the strings in the xml file
         *	into numbers
         */
        public void set_what(string s)
        {
            if (s == "Skill")
                m_Affects = What.Skill;
            else if (s == "Stat")
                m_Affects = What.Stat;
            else if (s == "Nothing")
                m_Affects = What.Nothing;
            else if (s == "GirlStatus")
                m_Affects = What.GirlStatus;
            else if (s == "Trait")
                m_Affects = What.Trait;
            else if (s == "Enjoy")
                m_Affects = What.Enjoy;
            else
            {
                m_Affects = What.Nothing;
                WMaster.WMLog.Trace(string.Format("Bad 'what' string for item effect: unexpected value '{0}'", s), WMLog.TraceLog.ERROR);
            }
        }
        /*
         *	can't make an enum for this since it can represent
         *	different quantites.
         *
         *	The OO approach would be to write some variant classes, I expect
         *	but really? Life's too short...
         */
        public byte m_EffectID;	// what stat, skill or status effect it affects
        /*
         *	but I still need strings for the skills, states, traits and so forth
         *
         *	these should be (were until the merge) in sGirl. Will be again
         *	as soon as I sort the main mess out...
         */
        public string girl_status_name(uint id)
        { throw new NotImplementedException(); }
        public string skill_name(uint id)
        { throw new NotImplementedException(); }		// WD:	Use definition in sGirl::
        public string stat_name(uint id)
        { throw new NotImplementedException(); }			// WD:	Use definition in sGirl::
        public string enjoy_name(uint id)
        { throw new NotImplementedException(); }		// `J`	Use definition in sGirl::

        /*
         *	and we need to go the other way,
         *	setting m_EffectID from the strings in the XML file
         *
         *	WD:	Change to use definition and code in sGirl::
         *		remove duplicated code
         */
        public bool set_skill(string s)
        { throw new NotImplementedException(); }
        public bool set_girl_status(string s)
        { throw new NotImplementedException(); }
        public bool set_stat(string s)
        { throw new NotImplementedException(); }
        public bool set_Enjoyment(string s)
        { throw new NotImplementedException(); }


        /*
         *	magnitude of the effect. 
         *	-10 will subtract 10 from the target stat while equiped
         *	and add 10 when unequiped.
         *
         *	With status effects and traits 1 means add,
         *	0 means take away and 2 means disable
         */
        public int m_Amount;

        public int m_Duration;	// `J` added for temporary trait duration
        /*
         *	name of the trait it adds
         */
        public string m_Trait;
        /*
         *	and a pretty printer for the class as a whole
         *	just a debug thing, really
         */
        public static string ToString(sEffect eff)
        {
            return eff.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Effect: {0}", this.m_Affects));
            if (this.m_Affects == What.Stat)
            { sb.AppendLine(this.stat_name(this.m_EffectID)); }
            if (this.m_Affects == What.Skill)
            { sb.AppendLine(this.skill_name(this.m_EffectID)); }
            if (this.m_Affects == What.Trait)
            { sb.AppendLine(this.m_Trait); }
            if (this.m_Affects == What.GirlStatus)
            { sb.AppendLine(this.girl_status_name(this.m_EffectID)); }
            if (this.m_Affects == What.Enjoy)
            { sb.AppendLine(this.enjoy_name(this.m_EffectID)); }
		    sb.AppendLine(this.m_Amount > 0 ? " +" : " ");
		    sb.AppendLine(this.m_Amount.ToString());
		    return sb.ToString();
        }
    }
}
