﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Concept;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single centre
    public class sCentre : sBrothel, System.IDisposable
    {
        public sCentre()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public Gold m_Finance = new Gold();

        IXmlElement SaveCentreXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadCentreXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }

    }

}
