using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cTrigger : System.IDisposable
    {
        public string m_Script; // the scripts filename
        public byte m_Type; // the type of trigger
        public byte m_Triggered; // 1 means this trigger has triggered already
        public byte m_Chance; // Percent chance of occuring
        public byte m_Once; // if 1 then this trigger will only work once, from then on it doesn't work
        public int[] m_Values = new int[3]; // values used for the triggers

        public cTrigger m_Next;

        public cTrigger()
        {
            m_Type = m_Triggered = m_Chance = m_Once = 0;
            m_Next = null;
        }
        public void Dispose()
        {
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Next = null;
        }

        IXmlElement SaveTriggerXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadTriggerXML(IXmlHandle hTrigger)
        { throw new NotImplementedException(); }

        bool get_once_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int get_type_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int get_chance_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int load_skill_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int load_stat_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int load_status_from_xml(IXmlHandle NamelessParameter)
        { throw new NotImplementedException(); }
        int load_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }
        int load_money_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }
        void load_meet_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }
        void load_talk_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }
        int load_weeks_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }
        int load_flag_from_xml(IXmlHandle el)
        { throw new NotImplementedException(); }

        /*
         *	some accessor funcs to make the meaning of the values 
         *	array elements a little less opaque
         */
        public int global_flag()
        {
            return m_Values[0];
        }
        public int global_flag(int n)
        {
            return m_Values[0] = n;
        }
        public int global_flag(string s)
        {
            if (s == "NoPay")
            {
                return m_Values[0] = Constants.FLAG_CUSTNOPAY;
            }
            if (s == "GirlDies")
            {
                return m_Values[0] = Constants.FLAG_DUNGEONGIRLDIE;
            }
            if (s == "CustomerDies")
            {
                return m_Values[0] = Constants.FLAG_DUNGEONCUSTDIE;
            }
            if (s == "GamblingCheat")
            {
                return m_Values[0] = Constants.FLAG_CUSTGAMBCHEAT;
            }
            if (s == "RivalLose")
            {
                return m_Values[0] = Constants.FLAG_RIVALLOSE;
            }
            return -1;
        }
        public int where()
        {
            return m_Values[0];
        }
        public int where(int n)
        {
            return m_Values[0] = n;
        }
        public int where(string s)
        {
            if (s == "Town" || s == "Dungeon")
            {
                return where(0);
            }
            if (s == "Catacombs" || s == "Brothel")
            {
                return where(1);
            }
            if (s == "SlaveMarket")
            {
                return where(2);
            }
            if (s == "Arena")
            {
                return where(3);
            }
            return -1;
        }
        public int status()
        {
            return m_Values[0];
        }
        public int status(int n)
        {
            return m_Values[0] = n;
        }
        public int stat()
        {
            return m_Values[0];
        }
        public int stat(int n)
        {
            return m_Values[0] = n;
        }
        public int skill()
        {
            return m_Values[0];
        }
        public int skill(int n)
        {
            return m_Values[0] = n;
        }
        public int has()
        {
            return m_Values[1];
        }
        public int has(int n)
        {
            return m_Values[1] = n;
        }
        public int threshold()
        {
            return m_Values[1];
        }
        public int threshold(int n)
        {
            return m_Values[1] = n;
        }
    }

}
