namespace WMaster.DAL
{
    using System.Xml.Serialization;

    /// <summary>
    /// Root class for save game
    /// </summary>
    [XmlRoot("Root")]
    public class SaveGame
    {
        /// <summary>
        /// Save game major version.
        /// </summary>
        [XmlAttribute("MajorVersion")]
        public string MajorVersion
        {
            get { return Constants.MajorVersion.ToString(); }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        /// <summary>
        /// Save game minor version A.
        /// </summary>
        [XmlAttribute("MinorVersionA")]
        public string MinorVersionA
        {
            get { return Constants.MinorVersionA.ToString(); }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        /// <summary>
        /// Save game minor version B.
        /// </summary>
        [XmlAttribute("MinorVersionB")]
        public string MinorVersionB
        {
            get { return Constants.MinorVersionB.ToString(); }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        /// <summary>
        /// Save game stable version.
        /// </summary>
        [XmlAttribute("StableVersion")]
        public string StableVersion
        {
            get { return Constants.StableVersion.ToString(); }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save ApproxRevision.
        /// </summary>
        [XmlAttribute("ApproxRevision")]
        public string ApproxRevision
        {
            get { return "Crazy and PP's mod version .06.02.38"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save ExeVersion.
        /// </summary>
        [XmlAttribute("ExeVersion")]
        public string ExeVersion
        {
            get { return "official"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save WalkAround.
        /// </summary>
        [XmlAttribute("WalkAround")]
        public string WalkAround
        {
            get { return "1"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save TalkCount.
        /// </summary>
        [XmlAttribute("TalkCount")]
        public string TalkCount
        {
            get { return "10"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save TryCentre.
        /// </summary>
        [XmlAttribute("TryCentre")]
        public string TryCentre
        {
            get { return "0"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save TryOuts.
        /// </summary>
        [XmlAttribute("TryOuts")]
        public string TryOuts
        {
            get { return "0"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save TryEr.
        /// </summary>
        [XmlAttribute("TryEr")]
        public string TryEr
        {
            get { return "0"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }

        // TODO : Find where to save/load this
        /// <summary>
        /// Save TryCast.
        /// </summary>
        [XmlAttribute("TryCast")]
        public string TryCast
        {
            get { return "0"; }
            // Setter do nothing. need to deserialization
            set { ; }
        }


        /// <summary>
        /// Current year in game.
        /// </summary>
        [XmlAttribute("Year")]
        public string Year
        {
            get { return GameEngine.Year.ToString(); }
            set
            {
                int year = GameEngine.Year;
                int.TryParse(value, out year);
                GameEngine.Year = year;
            }
        }

        /// <summary>
        /// Current month in game.
        /// </summary>
        [XmlAttribute("Month")]
        public string Month
        {
            get { return GameEngine.Month.ToString(); }
            set
            {
                int month = GameEngine.Month;
                int.TryParse(value, out month);
                GameEngine.Month = month;
            }
        }

        /// <summary>
        /// Current day in game.
        /// </summary>
        [XmlAttribute("Day")]
        public string Day
        {
            get { return GameEngine.Day.ToString(); }
            set
            {
                int day = GameEngine.Day;
                int.TryParse(value, out day);
                GameEngine.Day = day;
            }
        }

        // TODO : read and check resources game collection
        /// <summary>
        /// List of resources file stored in game.
        /// </summary>
        [XmlElement("Loaded_Files")]
        public ResourcesCollection Resources;

        /// <summary>
        /// XmlSerialiser need public constructor without param to serialize class.
        /// </summary>
        public SaveGame()
        {
            this.Resources = new ResourcesCollection();
        }
    }
}
