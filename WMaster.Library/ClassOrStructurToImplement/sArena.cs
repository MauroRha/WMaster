using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    // defines a single arena
    public class sArena : sBrothel, System.IDisposable
    {
        public sArena()
        { throw new NotImplementedException(); }// constructor
        public void Dispose()
        { throw new NotImplementedException(); }// destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public cGold m_Finance = new cGold();

        IXmlElement SaveArenaXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadArenaXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }
    }
}
