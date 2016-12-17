using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cEvents : System.IDisposable
    {
        /// <summary>
        /// constructor
        /// </summary>
        public cEvents()
        {
            m_bSorted = false;
        }
        public void Dispose()
        {
            Free();
        } // destructor

        void Free()
        { throw new NotImplementedException(); }
        public void Clear()
        {
            Free();
        }
        //void			DisplayMessages();		// No definition
        public void AddMessage(string message, int nImgType, int nEvent)
        { throw new NotImplementedException(); }
        cEvent GetMessage(int id)
        { throw new NotImplementedException(); }
        public int GetNumEvents()
        {
            return events.Count;
        }
        public bool IsEmpty()
        {
            return events.Count == 0;
        }
        bool HasGoodNews()
        { throw new NotImplementedException(); }
        bool HasUrgent()
        { throw new NotImplementedException(); }
        bool HasDanger()
        { throw new NotImplementedException(); }
        bool HasWarning()
        { throw new NotImplementedException(); }
        void DoSort()
        { throw new NotImplementedException(); }


        private List<cEvent> events = new List<cEvent>();
        private bool m_bSorted; // flag to only allow sort once
        uint MakeOrdinal(int nEvent)
        { throw new NotImplementedException(); }
    }
}
