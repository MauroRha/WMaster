﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // defines a single farm
    public class sFarm : sBrothel, System.IDisposable
    {
        public sFarm()
        { throw new NotImplementedException(); } // constructor
        public void Dispose()
        { throw new NotImplementedException(); } // destructor
        public ushort m_var; // customers used for temp purposes but must still be taken into account
        public cGold m_Finance = new cGold();

        IXmlElement SaveFarmXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadFarmXML(IXmlHandle hBrothel)
        { throw new NotImplementedException(); }

    }

}