using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public abstract class CResource : System.IDisposable
    {
        /// <summary>
        /// Stores the last time this resource was used.
        /// </summary>
        public long m_TimeUsed;

        /// <summary>
        /// registers the resource with the resource manager.
        /// </summary>
        public abstract void Register();

        /// <summary>
        /// Free all data.
        /// </summary>
        public abstract void Free();

        /// <summary>
        /// Frees only the loaded data, this is so the class isn't destroyed.
        /// </summary>
        public abstract void FreeResources();

        public CResource()
        {
            m_TimeUsed = DateTime.Now.Ticks;
        }

        public void Dispose()
        {
            Free();
        }

        public bool m_Registered;
    }

}
