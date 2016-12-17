using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single clinic
    public class sClinic : sBrothel, System.IDisposable
    {
        public sClinic()
        { throw new NotImplementedException(); }// constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public cGold m_Finance = new cGold();

        IXmlElement SaveClinicXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadClinicXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }
    }
}
