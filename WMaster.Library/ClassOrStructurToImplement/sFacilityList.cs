using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    using System.Collections.Generic;

    public class sFacilityList
    {
        private static List<sFacility> list;
        sFacilityList()
        { throw new NotImplementedException(); }
        public int size()
        {
            return list.Count;
        }
        public sFacility this[int i]
        {
            get
            {
                return list[i];
            }
        }

        bool load_xml(string path)
        { throw new NotImplementedException(); }
        bool parse_facility(IXmlElement NamelessParameter1, sFacility fac)
        { throw new NotImplementedException(); }
    }

}
