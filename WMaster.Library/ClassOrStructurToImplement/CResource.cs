using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class CResource : System.IDisposable
    {
        public virtual void Register()
        {
        } // registers the resource with the resource manager
        public virtual void Free()
        {
        } // Free all data
        public virtual void FreeResources()
        {
        } // Frees only the loaded data, this is so the class isn't destroyed
        public CResource()
        {
            m_Next = null;
            m_Prev = null;
            //m_TimeUsed = g_Graphics.GetTicks();
        }
        public void Dispose()
        {
            Free();
            m_Next = null;
            m_Prev = null;
        }

        public CResource m_Next; // pointer to the next resource or null if end of list
        public CResource m_Prev; // Pointer to the previous resource or null if top of list
        public uint m_TimeUsed; // Stores the last time this resource was used
        public bool m_Registered;
    }

}
