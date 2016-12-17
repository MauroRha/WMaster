using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cBuilding
    {
        private int m_capacity;
        private int m_free;

        private List<sFacility> m_facilities = new List<sFacility>();
        private List<sFacility> m_reversion;

        public int capacity()
        {
            return m_capacity;
        }
        public int free_space()
        {
            return m_free;
        }
        public int used_space()
        {
            return m_capacity - m_free;
        }

        public cBuilding()
        {
            m_capacity = 20;
            m_free = 20;
            m_reversion = new List<sFacility>();
        }

        public bool add(sFacility fac)
        {
            int needed = fac.space_taken();
            if (needed > m_free)
            {
                return false;
            }
            m_free -= needed;
            m_facilities.Add(fac);
            return true;
        }
        public sFacility remove(int i)
        {
            sFacility fac = m_facilities[i];
            m_facilities.RemoveAt(i);
            return fac;
        }
        public sFacility item_at(int i)
        {
            return m_facilities[i];
        }
        public sFacility this[int i]
        {
            get
            {
                return item_at(i);
            }
        }
        public int size()
        {
            return m_facilities.Count;
        }
        public void commit()
        {
            for (int i = 0; i < m_facilities.Count; i++)
            {
                m_facilities[i].commit();
            }
        }
        public void revert()
        {
            for (int i = 0; i < m_facilities.Count; i++)
            {
                m_facilities[i] = null;
            }
            m_facilities.Clear();
            if (m_reversion == null)
            {
                return;
            }
            m_free = m_capacity;
            for (int i = 0; i < m_reversion.Count; i++)
            {
                sFacility fpt = m_reversion[i];
                m_facilities.Add(fpt);
                m_free -= fpt.space_taken();
            }
            m_reversion = null;
        }

        // ofstream save(ofstream ofs, string building_name);
        //ifstream load(ifstream ifs);

        /*
         *	is the list free of changes that may need to be reverted?
         */
        public bool list_is_clean()
        { throw new NotImplementedException(); }
        /*
         *	copies the current list so we can revert all changes maed
         */
        public void make_reversion_list()
        { throw new NotImplementedException(); }
        public void clear_reversion_list()
        { throw new NotImplementedException(); }
    }
}
