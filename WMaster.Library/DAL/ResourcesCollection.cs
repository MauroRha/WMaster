namespace WMaster.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    /// <summary>
    /// List of Ressources files stored in save game
    /// </summary>
    public class ResourcesCollection
    {
        [XmlAttribute("NumberofFiles")]
        public string numberOfresources
        {
            get { return GirlResources.Count.ToString(); }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        [XmlArray("Girls_Files")]
        [XmlArrayItem("File")]
        public List<Resource> GirlResources;

        public ResourcesCollection()
        { this.GirlResources = new List<Resource>();}
    }
}
