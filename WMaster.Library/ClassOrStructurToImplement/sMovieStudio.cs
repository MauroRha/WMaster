using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single studio
    public class sMovieStudio : sBrothel, System.IDisposable
    {
        public sMovieStudio()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public cGold m_Finance = new cGold();

        IXmlElement SaveMovieStudioXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadMovieStudioXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }

    }

}
