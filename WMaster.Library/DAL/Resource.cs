namespace WMaster.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public class Resource
    {
        [XmlAttribute("Filename")]
        public string Filename { get; set; }

        public Resource()
        { ; }
    }
}
