using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class sFacility
    {
        public string m_type_name;
        public string m_instance_name;
        public string m_desc;
        public int m_space_taken;
        public int m_slots;
        public int m_base_price;
        public int m_paid;
        public sBoundedVar_Provides m_provides = new sBoundedVar_Provides();
        public sBoundedVar m_glitz = new sBoundedVar();
        public sBoundedVar m_secure = new sBoundedVar();
        public sBoundedVar m_stealth = new sBoundedVar();
        public bool new_flag;
        public cTariff tariff = new cTariff();

        public sFacility()
        {
            m_type_name = "";
            m_instance_name = "";
            m_desc = "";
            m_space_taken = 0;
            m_slots = 0;
            m_base_price = 0;
            new_flag = false;
        }
        public sFacility(sFacility f)
        {
            m_base_price = f.m_base_price;
            m_desc = f.m_desc;
            m_instance_name = f.m_instance_name;
            m_slots = f.m_slots;
            m_space_taken = f.m_space_taken;
            m_type_name = f.m_type_name;
            new_flag = f.new_flag;
        }

        public void commit()
        {
            new_flag = false;
            m_base_price = 0;
        }

        public string name()
        {
            if (m_instance_name != "")
            {
                return m_instance_name;
            }
            return m_type_name;
        }

        public string desc()
        {
            return m_desc;
        }
        public string type()
        {
            return m_type_name;
        }
        public int space_taken()
        {
            return m_space_taken;
        }
        public int slots()
        {
            return m_slots;
        }
        public int price()
        {
            return tariff.buy_facility(m_base_price);
        }
        public int glitz()
        {
            return m_glitz.m_curr;
        }
        public void glitz_up()
        {
            m_glitz.up();
        }
        public void glitz_down()
        {
            m_glitz.down();
        }

        public int secure()
        {
            return m_secure.m_curr;
        }
        public void secure_up()
        {
            m_secure.up();
        }
        public void secure_down()
        {
            m_secure.down();
        }

        public int stealth()
        {
            return m_stealth.m_curr;
        }
        public void stealth_up()
        {
            m_stealth.up();
        }
        public void stealth_down()
        {
            m_stealth.down();
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	void load_from_xml(TiXmlElement el);

        public sFacility clone()
        {
            return new sFacility(this);
        }
    }

}
