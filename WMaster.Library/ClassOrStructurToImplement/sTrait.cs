using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // Represents a single trait
    public class sTrait : System.IDisposable
    {
        public string m_Name = string.Empty; // the name and unique ID of the trait
        public string m_Desc = string.Empty; // a description of the trait
        public string m_Type = string.Empty; // a description of the trait
        public int m_InheritChance = -1; // chance of inheriting the trait
        public int m_RandomChance = -1; // chance of a random girl to get the trait
        public bool m_Use_XML_Mods = false; // `J` added to allow customized traits - .06.01.17
        public List<tEffect> m_Effects = new List<tEffect>();

        public sTrait m_Next; // the next trait in the list

        public sTrait()
        {
            m_Name = m_Desc = m_Type = string.Empty;
            m_InheritChance = -1;
            m_RandomChance = -1;
            m_Next = null;
        }

        public void Dispose()
        {
            //if (m_Name != string.Empty)
            //{
            //    Arrays.DeleteArray(m_Name);
            //}
            //m_Name = 0;
            //if (m_Desc != string.Empty)
            //{
            //    Arrays.DeleteArray(m_Desc);
            //}
            //m_Desc = 0;
            //if (m_Type != 0)
            //{
            //    Arrays.DeleteArray(m_Type);
            //}
            //m_Type = 0;
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Next = null;
            m_InheritChance = 0;
            m_RandomChance = 0;
        }
    }
}
