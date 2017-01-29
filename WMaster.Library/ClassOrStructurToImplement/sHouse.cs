using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Concept;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single House
    public class sHouse : sBrothel, System.IDisposable
    {
        public sHouse()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public Gold m_Finance = new Gold();

        IXmlElement SaveHouseXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadHouseXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }

    }

}
