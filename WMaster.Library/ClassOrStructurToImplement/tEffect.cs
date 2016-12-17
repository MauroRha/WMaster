using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class tEffect // `J` copied from cInventory.cpp - .06.01.17
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
            Enjoy = 4
        }
        public What m_Affects;
        /*
        *	define an ostream operator so it will pretty print
        *	(more useful for debugging than game play
        *	but why take it out?)
        */
        //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
        //ORIGINAL LINE: friend ostream& operator << (ostream& os, tEffect::What &w);
        //ostream operator << (ostream os, tEffect::What w);
        /*
        *	and a function to go the other way
        *	we need this to turn the strings in the xml file
        *	into numbers
        */
        public void set_what(string s)
        {
            if (s == "Skill")
            {
                m_Affects = What.Skill;
            }
            else if (s == "Stat")
            {
                m_Affects = What.Stat;
            }
            else if (s == "Nothing")
            {
                m_Affects = What.Nothing;
            }
            else if (s == "GirlStatus")
            {
                m_Affects = What.GirlStatus;
            }
            else if (s == "Enjoy")
            {
                m_Affects = What.Enjoy;
            }
            else
            {
                m_Affects = What.Nothing;
                WMLog.Trace(String.Format("Bad 'what' string for item effect: '{0}'", s), WMLog.TraceLog.ERROR);
            }
        }
        /*
        *	can't make an enum for this since it can represent
        *	different quantites.
        *
        *	The OO approach would be to write some variant classes, I expect
        *	but really? Life's too short...
        */
        public byte m_EffectID; // what stat, skill or status effect it affects
        /*
        *	but I still need strings for the skills, states, traits and so forth
        *
        *	these should be (were until the merge) in sGirl. Will be again
        *	as soon as I sort the main mess out...
        */
        string girl_status_name(uint id)
        { throw new NotImplementedException(); }
        string skill_name(uint id)
        { throw new NotImplementedException(); } // WD:    Use definition in sGirl::
        string stat_name(uint id)
        { throw new NotImplementedException(); } // WD:    Use definition in sGirl::
        string enjoy_name(uint id)
        { throw new NotImplementedException(); } // `J`    Use definition in sGirl::

        /*
        *	and we need to go the other way,
        *	setting m_EffectID from the strings in the XML file
        *
        *	WD:	Change to use definition and code in sGirl::
        *		remove duplicated code
        */
        bool set_skill(string s)
        { throw new NotImplementedException(); }
        bool set_girl_status(string s)
        { throw new NotImplementedException(); }
        bool set_stat(string s)
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

        public int m_Duration; // `J` added for temporary trait duration
        /*
        *	name of the trait it adds
        */
        public string m_Trait;
        /*
        *	and a pretty printer for the class as a whole
        *	just a debug thing, really
        */
        //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
        //ORIGINAL LINE: friend ostream& operator << (ostream& os, tEffect &eff)
        //public static ostream operator <<(ostream os, tEffect eff)
        //{
        //    os << "Effect: " << eff.m_Affects << " ";
        //    if (eff.m_Affects == What.Stat)
        //    {
        //        os << eff.stat_name(eff.m_EffectID);
        //    }
        //    if (eff.m_Affects == What.Skill)
        //    {
        //        os << eff.skill_name(eff.m_EffectID);
        //    }
        //    if (eff.m_Affects == What.Enjoy)
        //    {
        //        os << eff.enjoy_name(eff.m_EffectID);
        //    }
        //    if (eff.m_Affects == What.GirlStatus)
        //    {
        //        os << eff.girl_status_name(eff.m_EffectID);
        //    }
        //    os << (eff.m_Amount > 0 ? " +" : " ") << eff.m_Amount;
        //    return os << "\n";
        //}
        // end mod
    }

}
