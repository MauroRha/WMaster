using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // holds data for movies
    public class sMovie : System.IDisposable
    {
        public int m_Init_Quality;
        public int m_Promo_Quality;
        public int m_Quality;
        public int m_Money_Made;
        public int m_RunWeeks;
        public sMovie m_Next;
        public sMovie()
        {
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
    }
}
