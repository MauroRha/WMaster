using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    /*
    * this represents a trainaible attribute of a girl
    *
    * currently that means skill or stat.
    */
    public class cTrainable
    {
        /*
         *	comments in the forum notwithstanding, we do too need a flag here
         *	this can't be a pure virtual, since vector tries to construct
         *	a class instance before assigning a new member to a slot
         *
         *	could try making it default to skill or stat I suppose
         *	but this is probably safer
         */
        public enum AType
        {
            Stat,
            Skill
        }
        /*
         *	constructor - nothing fancy here
         */
        public cTrainable()
        {
        }

        public cTrainable(sGirl girl, string stat_name, int index, AType typ)
        {
            //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to variables (in C#, the variable no longer points to the original when the original variable is re-assigned):
            //ORIGINAL LINE: m_girl = girl;
            m_girl = girl;
            m_index = index;
            m_name = stat_name;
            m_type = typ;
            m_gain = 0;
        }
        public cTrainable(cTrainable t)
        {
            m_girl = t.m_girl;
            m_index = t.m_index;
            m_name = t.m_name;
            m_type = t.m_type;
            m_gain = t.m_gain;
        }

        //C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
        //ORIGINAL LINE: void operator =(const cTrainable& t)
        public void CopyFrom(cTrainable t)
        {
            m_girl = t.m_girl;
            m_index = t.m_index;
            m_name = t.m_name;
            m_type = t.m_type;
            m_gain = t.m_gain;
        }

        public string name()
        {
            return m_name;
        }
        /*
         *	lost the virtual here - don't need it
         */
        int value()
        { throw new NotImplementedException(); }
        void upd(int increment)
        { throw new NotImplementedException(); }

        public int gain()
        {
            return m_gain;
        }
        protected sGirl m_girl;
        protected string m_name;
        protected int m_index;
        protected AType m_type;
        protected int m_gain;
    }

}
