/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

//<!-- -------------------------------------------------------------------------------------------------------------------- -->
//<file>
//  <copyright file="Config.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-13</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster
{
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WMaster.Tool;

    /// <summary>
    /// Provide, load and store game configuration data.
    /// </summary>
    [Serializable()]
    [XmlRoot("config")]
    public sealed class Configuration : ISerialisableEntity
    {
        #region Singleton
        /// <summary>
        /// Default configuration file name.
        /// </summary>
        private const string CONFIGURATION_RESOURCE = "config.xml";

        /// <summary>
        /// Singleton of <see cref="Configuration"/>.
        /// </summary>
        private static Configuration m_Instance = new Configuration();
        /// <summary>
        /// Get <see cref="Configuration"/> unique instance.
        /// </summary>
        public static Configuration Instance
        {
            get
            {
                if (!Game.IsInitialized)
                { throw new InvalidOperationException("Can't get GameEngine.Instance if Game wasn't initialized!"); }

                if (Configuration.m_Instance == null)
                { Configuration.m_Instance = new Configuration(); }

                return Configuration.m_Instance;
            }
        }

        /// <summary>
        /// Deserialise configuration data from an <see cref="XElement"/> containing serialised data of configuration.
        /// </summary>
        /// <param name="config"><see cref="XElement"/> containing serialised data of configuration.</param>
        internal static void Initialise(XElement config)
        {
            if (config == null)
            {
                Configuration.m_Instance = new Configuration();
                Configuration.SaveConfiguration();
                return;
            }

            try
            {
                if (!Configuration.Instance.Deserialise(config))
                { WMLog.Trace("Unable to deserialise Configuration file!", WMLog.TraceLog.ERROR); }
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                Configuration.m_Instance = new Configuration();
                Configuration.SaveConfiguration();
            }
        }

        /// <summary>
        /// Load data configuration from resources configuration.
        /// <remarks><para>XML data structure was get from <see cref="ResourcesManager"/></para></remarks>
        /// </summary>
        /// <param name="resourceName">Resource name storing configuration data. Will call resource manager to get XML structure.</param>
        public static void LoadConfiguration(string resourceName = null)
        {
            if (Game.IsInitialized)
            {
                WMLog.Trace(string.Format("Whore Master v{0}.{1}{2}.{3} BETA  Svn: {4}",
                    Constants.MajorVersion, Constants.MinorVersionA, Constants.MinorVersionB, Constants.StableVersion, Constants.SVN_REVISION),
                    WMLog.TraceLog.INFORMATION);
                WMLog.Trace("------------------------------------------------------------------------------------------------------------------------", WMLog.TraceLog.INFORMATION);
                WMLog.Trace("Loading Default configuration variables", WMLog.TraceLog.INFORMATION);

                XElement xe = Game.OS.GetConfiguration(resourceName ?? Configuration.CONFIGURATION_RESOURCE);
                if (xe == null)
                {
                    WMLog.Trace(string.Format("Unable to load configuration from configuration file {0}.", Configuration.CONFIGURATION_RESOURCE), WMLog.TraceLog.ERROR);
                }
                Configuration.Initialise(xe);
            }
        }

        /// <summary>
        /// Save configration data to mass storage.
        /// </summary>
        /// <param name="resourceName">Name of resource to save configuration in.</param>
        public static void SaveConfiguration(string resourceName = null)
        {
            if (Game.IsInitialized)
            {
                XElement xe = new XElement(Serialiser.GetRootNameAttribut(typeof(Configuration)));

                if (!Configuration.Instance.Serialise(xe))
                {
                    WMLog.Trace(string.Format("Unable to save configuration to configuration file {0}.", Configuration.CONFIGURATION_RESOURCE), WMLog.TraceLog.ERROR);
                }

                Game.OS.SetConfiguration(xe, resourceName ?? Configuration.CONFIGURATION_RESOURCE);
            }
        }
        #endregion

        #region Embeded class/structure
        /// <summary>
        /// Initial data configuration class.
        /// </summary>
        [Serializable()]
        [XmlRoot("Initial")]
        public class InitialData : ISerialisableEntity
        {
            // TODO : GirlMeet, GirlsHousePerc and SlaveHousePerc is percentage but stay between 0 to 100. all others value ingame is 0.0 to 1.0
            #region Private fields
            /// <summary>
            /// How much gold player start the game with.
            /// </summary>
            private int m_Gold = 4000;
            /// <summary>
            /// %chance player will meet a girl when walking around town.
            /// </summary>
            private double m_GirlMeet = 30;
            /// <summary>
            /// Dafault house percentage taken from free girls money gain.
            /// </summary>
            private double m_GirlsHousePerc = 60; // `J` added
            /// <summary>
            /// Whether girl keep tips separate from house percent.
            /// </summary>
            private bool m_GirlsKeepTips = true; // `J` added
            /// <summary>
            /// Dafault house percentage taken from slave girls money gain.
            /// </summary>
            private double m_SlaveHousePerc = 100;
            /// <summary>
            /// Wether or not slave girls get paid by the player directly for certain jobs
            /// ie. Cleaning, Advertising, Farming jobs, Film jobs, etc.
            /// </summary>
            private bool m_SlavePayOutOfPocket = true; // `J` added
            /// <summary>
            /// Whether slave keep tips separate from house percent.
            /// </summary>
            private bool m_SlaveKeepTips = false; // `J` added
            /// <summary>
            /// Initial free girls accomodation.
            /// </summary>
            private int m_GirlsAccom = 5;
            /// <summary>
            /// Initial save girl acomodation.
            /// </summary>
            private int m_SlaveAccom = 1;
            /// <summary>
            /// Whether or not the game will try to automatically use the player's items intelligently on girls each week.
		    /// This feature needs more testing.
            /// </summary>
            private bool m_AutoUseItems = false;
            /// <summary>
            /// Whether girls will automatically equip their best weapon and armor for combat jobs
            /// and also automatically unequip weapon and armor for regular jobs where such gear would be considered inappropriate
            /// (i.e. whores-with-swords). Set to "false" to disable this feature.
            /// </summary>
            private bool m_AutoCombatEquip = true;
            /// <summary>
            /// Affects multiplying the duration that they will keep a temporary trait that they get from being tortured.
            /// It is multiplied by the number of weeks in the dungeon.
            /// <remarks>
            ///     <para>
            ///         `J` added : If TortureTraitWeekMod is set to -1 then torture is harsher.
            ///         This doubles the chance of injuring the girls and doubles evil gain.
		    ///         Damage is increased by half. It also makes breaking the girls wills permanent.
            ///     </para>
            /// </remarks>
            /// </summary>
            private int m_TortureMod = 1; // `J` added
            private int m_HoroscopeType = 1; // `J` added
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set how much gold player start the game with.
            /// </summary>
            [XmlAttribute("Gold")]
            public int Gold
            {
                get { return this.m_Gold; }
                set { this.m_Gold = value; }
            }

            /// <summary>
            /// Get or set the %chance player will meet a girl when walking around town.
            /// </summary>
            [XmlAttribute("GirlMeet")]
            public double GirlMeet
            {
                get { return this.m_GirlMeet; }
                set { this.m_GirlMeet = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set the dafault house percentage taken from free girls money gain.
            /// </summary>
            [XmlAttribute("GirlsHousePerc")]
            public double GirlsHousePerc
            {
                get { return this.m_GirlsHousePerc; }
                set { this.m_GirlsHousePerc = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set whether girl keep tips separate from house percent.
            /// </summary>
            [XmlAttribute("GirlsKeepTips")]
            public bool GirlsKeepTips
            {
                get { return this.m_GirlsKeepTips; }
                set { this.m_GirlsKeepTips = value; }
            }

            /// <summary>
            /// Get or set the dafault house percentage taken from slave girls money gain.
            /// </summary>
            [XmlAttribute("SlaveHousePerc")]
            public double SlaveHousePerc
            {
                get { return this.m_SlaveHousePerc; }
                set { this.m_SlaveHousePerc = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set wether or not slave girls get paid by the player directly for certain jobs
            /// ie. Cleaning, Advertising, Farming jobs, Film jobs, etc.
            /// </summary>
            [XmlAttribute("SlavePayOutOfPocket")]
            public bool SlavePayOutOfPocket
            {
                get { return this.m_SlavePayOutOfPocket; }
                set { this.m_SlavePayOutOfPocket = value; }
            }

            /// <summary>
            /// Get or set whether slave keep tips separate from house percent.
            /// </summary>
            [XmlAttribute("SlaveKeepTips")]
            public bool SlaveKeepTips
            {
                get { return this.m_SlaveKeepTips; }
                set { this.m_SlaveKeepTips = value; }
            }

            /// <summary>
            /// Get or set initial free girls accomodation.
            /// </summary>
            [XmlAttribute("GirlsAccom")]
            public int GirlsAccom
            {
                get { return this.m_GirlsAccom; }
                set { this.m_GirlsAccom = Math.Max(Math.Min(value, 0), 9); }
            }

            /// <summary>
            /// Get or set initial save girl acomodation.
            /// </summary>
            [XmlAttribute("SlaveAccom")]
            public int SlaveAccom
            {
                get { return this.m_SlaveAccom; }
                set { this.m_SlaveAccom = Math.Max(Math.Min(value, 0), 9); ; }
            }
  
            /// <summary>
            /// Get or set whether or not the game will try to automatically use the player's items intelligently on girls each week.
            /// This feature needs more testing.
            /// </summary>
            [XmlAttribute("AutoUseItems")]
            public bool AutoUseItems
            {
                get { return this.m_AutoUseItems; }
                set { this.m_AutoUseItems = value; }
            }

            /// <summary>
            /// Get or set whether girls will automatically equip their best weapon and armor for combat jobs
            /// and also automatically unequip weapon and armor for regular jobs where such gear would be considered inappropriate
            /// (i.e. whores-with-swords). Set to "false" to disable this feature.
            /// </summary>
            [XmlAttribute("AutoCombatEquip")]
            public bool AutoCombatEquip
            {
                get { return this.m_AutoCombatEquip; }
                set { this.m_AutoCombatEquip = value; }
            }

            /// <summary>
            /// Affects multiplying the duration that they will keep a temporary trait that they get from being tortured.
            /// It is multiplied by the number of weeks in the dungeon.
            /// <remarks>
            ///     <para>
            ///         `J` added : If TortureTraitWeekMod is set to -1 then torture is harsher.
            ///         This doubles the chance of injuring the girls and doubles evil gain.
            ///         Damage is increased by half. It also makes breaking the girls wills permanent.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("TortureTraitWeekMod")]
            public int TortureMod
            {
                get { return this.m_TortureMod; }
                set { this.m_TortureMod = value; }
            }

            [XmlAttribute("HoroscopeType")]
            public int HoroscopeType
            {
                get { return this.m_HoroscopeType; }
                set { this.m_HoroscopeType = value; }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("Gold", this.Gold));
                    data.Add(new XAttribute("GirlMeet", this.GirlMeet.ToString("0.00%")));
                    data.Add(new XAttribute("GirlsHousePerc", this.GirlsHousePerc.ToString("0.00%")));
                    data.Add(new XAttribute("GirlsKeepTips", this.GirlsKeepTips));
                    data.Add(new XAttribute("SlaveHousePerc", this.SlaveHousePerc.ToString("0.00%")));
                    data.Add(new XAttribute("SlavePayOutOfPocket", this.SlavePayOutOfPocket));
                    data.Add(new XAttribute("SlaveKeepTips", this.SlaveKeepTips));
                    data.Add(new XAttribute("GirlsAccom", this.GirlsAccom));
                    data.Add(new XAttribute("SlaveAccom", this.SlaveAccom));
                    data.Add(new XAttribute("AutoUseItems", this.AutoUseItems));
                    data.Add(new XAttribute("AutoCombatEquip", this.AutoCombatEquip));
                    data.Add(new XAttribute("TortureTraitWeekMod", this.TortureMod));
                    data.Add(new XAttribute("HoroscopeType", this.HoroscopeType));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                int convertInt;
                double convertDouble;
                bool convertBool;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (int.TryParse(data.Attribute("Gold").Value, out convertInt)) /*                 */ { this.Gold = convertInt; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GirlMeet").Value) && double.TryParse(data.Attribute("GirlMeet").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GirlMeet = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GirlsHousePerc").Value) && double.TryParse(data.Attribute("GirlsHousePerc").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GirlsHousePerc = convertDouble; }
                    if (bool.TryParse(data.Attribute("GirlsKeepTips").Value, out convertBool)) /*      */ { this.GirlsKeepTips = convertBool; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("SlaveHousePerc").Value) && double.TryParse(data.Attribute("SlaveHousePerc").Value.Replace("%", string.Empty), out convertDouble))
                    { this.SlaveHousePerc = convertDouble; }
                    if (bool.TryParse(data.Attribute("SlavePayOutOfPocket").Value, out convertBool)) /**/ { this.SlavePayOutOfPocket = convertBool; }
                    if (bool.TryParse(data.Attribute("SlaveKeepTips").Value, out convertBool)) /*      */ { this.SlaveKeepTips = convertBool; }
                    if (int.TryParse(data.Attribute("GirlsAccom").Value, out convertInt)) /*           */ { this.GirlsAccom = convertInt; }
                    if (int.TryParse(data.Attribute("SlaveAccom").Value, out convertInt)) /*           */ { this.SlaveAccom = convertInt; }
                    if (bool.TryParse(data.Attribute("AutoUseItems").Value, out convertBool)) /*       */ { this.AutoUseItems = convertBool; }
                    if (bool.TryParse(data.Attribute("AutoCombatEquip").Value, out convertBool)) /*    */ { this.AutoCombatEquip = convertBool; }
                    if (int.TryParse(data.Attribute("TortureTraitWeekMod").Value, out convertInt)) /*  */ { this.TortureMod = convertInt; }
                    if (int.TryParse(data.Attribute("HoroscopeType").Value, out convertInt)) /*        */ { this.HoroscopeType = convertInt; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Income data configuration factors structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Income")]
        public class IncomeFactorsData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Multiplicator of money generate from extortion.
            /// </summary>
            private double m_Extortion = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate from girl working in brothel.
            /// </summary>
            private double m_BrothelWork = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate from girl working in street.
            /// </summary>
            private double m_StreetWork = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate from movie.
            /// </summary>
            private double m_MovieIncome = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate from girl stripper.
            /// </summary>
            private double m_StripperWork = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate from girl working as barmaid.
            /// </summary>
            private double m_BarmaidWork = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate when selling a slave.
            /// </summary>
            private double m_SlaveSales = 1.0;
            /// <summary>
            /// Multiplicator of money generate when selling an item.
            /// </summary>
            private double m_ItemSales = 0.5;
            /// <summary>
            /// Multiplicator of money generate at the clinic.
            /// </summary>
            private double m_ClinicIncome = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate at the arena.
            /// </summary>
            private double m_ArenaIncome = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money generate at the farm.
            /// </summary>
            private double m_FarmIncome = 1.0; // `J` ?not used?
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the multiplicator of money generate from extortion.
            /// </summary>
            [XmlAttribute("ExtortionIncome")]
            public double Extortion
            {
                get { return m_Extortion; }
                set { m_Extortion = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate from girl working in brothel.
            /// </summary>
            [XmlAttribute("GirlsWorkBrothel")]
            public double BrothelWork
            {
                get { return m_BrothelWork; }
                set { m_BrothelWork = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate from girl working in street.
            /// </summary>
            [XmlAttribute("GirlsWorkStreet")]
            public double StreetWork
            {
                get { return m_StreetWork; }
                set { m_StreetWork = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate from movie.
            /// </summary>
            [XmlAttribute("MovieIncome")]
            public double MovieIncome
            {
                get { return m_MovieIncome; }
                set { m_MovieIncome = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate from girl stripper.
            /// </summary>
            [XmlAttribute("StripperIncome")]
            public double StripperWork
            {
                get { return m_StripperWork; }
                set { m_StripperWork = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate from girl working as barmaid.
            /// </summary>
            [XmlAttribute("BarmaidIncome")]
            public double BarmaidWork
            {
                get { return m_BarmaidWork; }
                set { m_BarmaidWork = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate when selling a slave.
            /// </summary>
            [XmlAttribute("SlaveSales")]
            public double SlaveSales
            {
                get { return m_SlaveSales; }
                set { m_SlaveSales = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate when selling an item.
            /// </summary>
            [XmlAttribute("ItemSales")]
            public double ItemSales
            {
                get { return m_ItemSales; }
                set { m_ItemSales = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate at the clinic.
            /// </summary>
            [XmlAttribute("ClinicIncome")]
            public double ClinicIncome
            {
                get { return m_ClinicIncome; }
                set { m_ClinicIncome = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate at the arena.
            /// </summary>
            [XmlAttribute("ArenaIncome")]
            public double ArenaIncome
            {
                get { return m_ArenaIncome; }
                set { m_ArenaIncome = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money generate at the farm.
            /// </summary>
            [XmlAttribute("FarmIncome")]
            public double FarmIncome
            {
                get { return m_FarmIncome; }
                set { m_FarmIncome = Math.Min(value, 0.0); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("ExtortionIncome", this.Extortion));
                    data.Add(new XAttribute("GirlsWorkBrothel", this.BrothelWork));
                    data.Add(new XAttribute("GirlsWorkStreet", this.StreetWork));
                    data.Add(new XAttribute("MovieIncome", this.MovieIncome));
                    data.Add(new XAttribute("StripperIncome", this.StripperWork));
                    data.Add(new XAttribute("BarmaidIncome", this.BarmaidWork));
                    data.Add(new XAttribute("SlaveSales", this.SlaveSales));
                    data.Add(new XAttribute("ItemSales", this.ItemSales));
                    data.Add(new XAttribute("ClinicIncome", this.ClinicIncome));
                    data.Add(new XAttribute("ArenaIncome", this.ArenaIncome));
                    data.Add(new XAttribute("FarmIncome", this.FarmIncome));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (double.TryParse(data.Attribute("ExtortionIncome").Value, out convertDouble)) /* */ { this.Extortion = convertDouble; }
                    if (double.TryParse(data.Attribute("GirlsWorkBrothel").Value, out convertDouble)) /**/ { this.BrothelWork = convertDouble; }
                    if (double.TryParse(data.Attribute("GirlsWorkStreet").Value, out convertDouble)) /* */ { this.StreetWork = convertDouble; }
                    if (double.TryParse(data.Attribute("MovieIncome").Value, out convertDouble)) /*     */ { this.MovieIncome = convertDouble; }
                    if (double.TryParse(data.Attribute("StripperIncome").Value, out convertDouble)) /*  */ { this.StripperWork = convertDouble; }
                    if (double.TryParse(data.Attribute("BarmaidIncome").Value, out convertDouble)) /*   */ { this.BarmaidWork = convertDouble; }
                    if (double.TryParse(data.Attribute("SlaveSales").Value, out convertDouble)) /*      */ { this.SlaveSales = convertDouble; }
                    if (double.TryParse(data.Attribute("ItemSales").Value, out convertDouble)) /*       */ { this.ItemSales = convertDouble; }
                    if (double.TryParse(data.Attribute("ClinicIncome").Value, out convertDouble)) /*    */ { this.ClinicIncome = convertDouble; }
                    if (double.TryParse(data.Attribute("ArenaIncome").Value, out convertDouble)) /*     */ { this.ArenaIncome = convertDouble; }
                    if (double.TryParse(data.Attribute("FarmIncome").Value, out convertDouble)) /*      */ { this.FarmIncome = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Outgoings data configuration factors.
        /// </summary>
        [Serializable()]
        [XmlRoot("Expenses")]
        public class OutgoingFactorsData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Multiplicator of money cost from training a girl.
            /// </summary>
            private double m_Training = 0.0;
            /// <summary>
            /// Multiplicator of money for actress wages.
            /// </summary>
            private double m_ActressWages = 0.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money spent in movies.
            /// </summary>
            private double m_MovieCost = 1.0;
            /// <summary>
            /// Multiplicator of money for gang wages.
            /// </summary>
            private double m_GoonWages = 1.0;
            /// <summary>
            /// Multiplicator of money for matron wages.
            /// </summary>
            private double m_MatronWages = 1.0;
            /// <summary>
            /// Multiplicator of money for staff wages.
            /// </summary>
            private double m_StaffWages = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money for girl support.
            /// </summary>
            private double m_GirlSupport = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money spent for bying consumable.
            /// </summary>
            private double m_Consumables = 1.0;
            /// <summary>
            /// Multiplicator of money spent for bying item.
            /// </summary>
            private double m_ItemCost = 1.0;
            /// <summary>
            /// Multiplicator of money cost for a slave.
            /// </summary>
            private double m_SlaveCost = 1.0;
            /// <summary>
            /// Multiplicator of money cost for brothel.
            /// </summary>
            private double m_BrothelCost = 1.0;
            /// <summary>
            /// Multiplicator of money cost for brothel support.
            /// </summary>
            private double m_BrothelSupport = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money cost for bar.
            /// </summary>
            private double m_BarCost = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money cost for casino.
            /// </summary>
            private double m_CasinoCost = 1.0; // `J` ?not used?*
            /// <summary>
            /// Multiplicator of money for bribes.
            /// </summary>
            private double m_Bribes = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money cost for fines.
            /// </summary>
            private double m_Fines = 1.0; // `J` ?not used?
            /// <summary>
            /// Multiplicator of money cost for advertising.
            /// </summary>
            private double m_Advertising = 1.0;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the m
            /// </summary>
            [XmlAttribute("Training")]
            public double Training
            {
                get { return m_Training; }
                set { m_Training = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for actress wages
            /// </summary>
            [XmlAttribute("ActressWages")]
            public double ActressWages
            {
                get { return m_ActressWages; }
                set { m_ActressWages = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money spent in movies
            /// </summary>
            [XmlAttribute("MovieCost")]
            public double MovieCost
            {
                get { return m_MovieCost; }
                set { m_MovieCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for gang wages.
            /// </summary>
            [XmlAttribute("GoonWages")]
            public double GoonWages
            {
                get { return m_GoonWages; }
                set { m_GoonWages = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for matron wages.
            /// </summary>
            [XmlAttribute("MatronWages")]
            public double MatronWages
            {
                get { return m_MatronWages; }
                set { m_MatronWages = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for staff wages.
            /// </summary>
            [XmlAttribute("StaffWages")]
            public double StaffWages
            {
                get { return m_StaffWages; }
                set { m_StaffWages = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for girl support.
            /// </summary>
            [XmlAttribute("GirlSupport")]
            public double GirlSupport
            {
                get { return m_GirlSupport; }
                set { m_GirlSupport = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money spent for bying consumable.
            /// </summary>
            [XmlAttribute("Consumables")]
            public double Consumables
            {
                get { return m_Consumables; }
                set { m_Consumables = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money spent for bying item.
            /// </summary>
            [XmlAttribute("Items")]
            public double ItemCost
            {
                get { return m_ItemCost; }
                set { m_ItemCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for a slave.
            /// </summary>
            [XmlAttribute("SlavesBought")]
            public double SlaveCost
            {
                get { return m_SlaveCost; }
                set { m_SlaveCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for brothel.
            /// </summary>
            [XmlAttribute("BuyBrothel")]
            public double BrothelCost
            {
                get { return m_BrothelCost; }
                set { m_BrothelCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for brothel support.
            /// </summary>
            [XmlAttribute("BrothelSupport")]
            public double BrothelSupport
            {
                get { return m_BrothelSupport; }
                set { m_BrothelSupport = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for bar.
            /// </summary>
            [XmlAttribute("BarSupport")]
            public double BarCost
            {
                get { return m_BarCost; }
                set { m_BarCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for casino.
            /// </summary>
            [XmlAttribute("CasinoSupport")]
            public double CasinoCost
            {
                get { return m_CasinoCost; }
                set { m_CasinoCost = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money for bribes.
            /// </summary>
            [XmlAttribute("Bribes")]
            public double Bribes
            {
                get { return m_Bribes; }
                set { m_Bribes = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for fines.
            /// </summary>
            [XmlAttribute("Fines")]
            public double Fines
            {
                get { return m_Fines; }
                set { m_Fines = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Get or set the multiplicator of money cost for advertising.
            /// </summary>
            [XmlAttribute("Advertising")]
            public double Advertising
            {
                get { return m_Advertising; }
                set { m_Advertising = Math.Min(value, 0.0); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("Training", this.Training));
                    data.Add(new XAttribute("ActressWages", this.ActressWages));
                    data.Add(new XAttribute("MovieCost", this.MovieCost));
                    data.Add(new XAttribute("GoonWages", this.GoonWages));
                    data.Add(new XAttribute("MatronWages", this.MatronWages));
                    data.Add(new XAttribute("StaffWages", this.StaffWages));
                    data.Add(new XAttribute("GirlSupport", this.GirlSupport));
                    data.Add(new XAttribute("Consumables", this.Consumables));
                    data.Add(new XAttribute("Items", this.ItemCost));
                    data.Add(new XAttribute("SlavesBought", this.SlaveCost));
                    data.Add(new XAttribute("BuyBrothel", this.BrothelCost));
                    data.Add(new XAttribute("BrothelSupport", this.BrothelSupport));
                    data.Add(new XAttribute("BarSupport", this.BarCost));
                    data.Add(new XAttribute("CasinoSupport", this.CasinoCost));
                    data.Add(new XAttribute("Bribes", this.Bribes));
                    data.Add(new XAttribute("Fines", this.Fines));
                    data.Add(new XAttribute("Advertising", this.Advertising));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (double.TryParse(data.Attribute("Training").Value, out convertDouble)) /*      */ { this.Training = convertDouble; }
                    if (double.TryParse(data.Attribute("ActressWages").Value, out convertDouble)) /*  */ { this.ActressWages = convertDouble; }
                    if (double.TryParse(data.Attribute("MovieCost").Value, out convertDouble)) /*     */ { this.MovieCost = convertDouble; }
                    if (double.TryParse(data.Attribute("GoonWages").Value, out convertDouble)) /*     */ { this.GoonWages = convertDouble; }
                    if (double.TryParse(data.Attribute("MatronWages").Value, out convertDouble)) /*   */ { this.MatronWages = convertDouble; }
                    if (double.TryParse(data.Attribute("StaffWages").Value, out convertDouble)) /*    */ { this.StaffWages = convertDouble; }
                    if (double.TryParse(data.Attribute("GirlSupport").Value, out convertDouble)) /*   */ { this.GirlSupport = convertDouble; }
                    if (double.TryParse(data.Attribute("Consumables").Value, out convertDouble)) /*   */ { this.Consumables = convertDouble; }
                    if (double.TryParse(data.Attribute("Items").Value, out convertDouble)) /*         */ { this.ItemCost = convertDouble; }
                    if (double.TryParse(data.Attribute("SlavesBought").Value, out convertDouble)) /*  */ { this.SlaveCost = convertDouble; }
                    if (double.TryParse(data.Attribute("BuyBrothel").Value, out convertDouble)) /*    */ { this.BrothelCost = convertDouble; }
                    if (double.TryParse(data.Attribute("BrothelSupport").Value, out convertDouble)) /**/ { this.BrothelSupport = convertDouble; }
                    if (double.TryParse(data.Attribute("BarSupport").Value, out convertDouble)) /*    */ { this.BarCost = convertDouble; }
                    if (double.TryParse(data.Attribute("CasinoSupport").Value, out convertDouble)) /* */ { this.CasinoCost = convertDouble; }
                    if (double.TryParse(data.Attribute("Bribes").Value, out convertDouble)) /*        */ { this.Bribes = convertDouble; }
                    if (double.TryParse(data.Attribute("Fines").Value, out convertDouble)) /*         */ { this.Fines = convertDouble; }
                    if (double.TryParse(data.Attribute("Advertising").Value, out convertDouble)) /*   */ { this.Advertising = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Gamble data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Gambling")]
        public class GambleData : ISerialisableEntity
        {
            // TODO : ODDS is percentage but stay between 0 to 100. all others value ingame is 0.0 to 1.0
            #region Private fields
            /// <summary>
            /// Starting %chance for the tables.
            /// </summary>
            private double m_Odds = 49;
            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            private int m_Base = 79;
            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            private int m_Spread = 400;
            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            private double m_HouseFactor = 1.0;
            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            private double m_CustomerFactor = 0.5;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the starting %chance for the tables.
            /// </summary>
            [XmlAttribute("Odds")]
            public double Odds
            {
                get { return m_Odds; }
                set { m_Odds = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            [XmlAttribute("Base")]
            public int Base
            {
                get { return m_Base; }
                set { m_Base = value; }
            }

            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            [XmlAttribute("Spread")]
            public int Spread
            {
                get { return m_Spread; }
                set { m_Spread = Math.Min(value, 1); }
            }

            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            [XmlAttribute("HouseFactor")]
            public double HouseFactor
            {
                get { return m_HouseFactor; }
                set { m_HouseFactor = Math.Min(value, 0.0); }
            }

            /// <summary>
            /// Wins and losses on the tables are calculated as the <b>Base</b> value plus a random number between 1 and the value of <b>Spread</b>.
            /// If the house wins, the amount is multiplied by the <b>HouseFactor</b>.
            /// If the customer wins, by the <b>CustomerFactor</b>.
            /// </summary>
            [XmlAttribute("CustomerFactor")]
            public double CustomerFactor
            {
                get { return m_CustomerFactor; }
                set { m_CustomerFactor = Math.Min(value, 0.0); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("Odds", this.Odds.ToString("0.00%")));
                    data.Add(new XAttribute("Base", this.Base));
                    data.Add(new XAttribute("Spread", this.Spread));
                    data.Add(new XAttribute("HouseFactor", this.HouseFactor));
                    data.Add(new XAttribute("CustomerFactor", this.CustomerFactor));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                int convertInt;
                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("Odds").Value) && double.TryParse(data.Attribute("Odds").Value.Replace("%", string.Empty), out convertDouble))
                    { this.Odds = convertDouble; }

                    if (int.TryParse(data.Attribute("Base").Value, out convertInt)) /*                */ { this.Base = convertInt; }
                    if (int.TryParse(data.Attribute("Spread").Value, out convertInt)) /*              */ { this.Spread = convertInt; }
                    if (double.TryParse(data.Attribute("HouseFactor").Value, out convertDouble)) /*   */ { this.HouseFactor = convertDouble; }
                    if (double.TryParse(data.Attribute("CustomerFactor").Value, out convertDouble)) /**/ { this.CustomerFactor = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Tax data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Tax")]
        public class TaxData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Rate at which player's income is taxed.
            /// </summary>
            private double m_Rate = 0.06;
            /// <summary>
            /// Minimum adjusted rate after influence is used to lower the tax rate.
            /// </summary>
            private double m_Minimum = 0.01;
            /// <summary>
            /// Maximum % of your income that can be Laundered and so escape taxation.
            /// <remarks>
            ///     <para>
            ///         So if you have 100g income, and a 25% laundry rating, then between 1 and 25 gold will go directly into your pocket.
            ///         The remaining 75 Gold will be taxed at 6% (assuming no reduction due to political influence)
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_Laundry = 0.25;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the rate at which player's income is taxed.
            /// </summary>
            [XmlAttribute("Rate")]
            public double Rate
            {
                get { return m_Rate; }
                set { m_Rate = Math.Max(value, 0.0); }
            }

            /// <summary>
            /// Get or set the minimum adjusted rate after influence is used to lower the tax rate.
            /// </summary>
            [XmlAttribute("Minimum")]
            public double Minimum
            {
                get { return m_Minimum; }
                set { m_Minimum = Math.Max(value, 0.0); }
            }

            /// <summary>
            /// Get or set the maximum % of your income that can be Laundered and so escape taxation.
            /// <remarks>
            ///     <para>
            ///         So if you have 100g income, and a 25% laundry rating, then between 1 and 25 gold will go directly into your pocket.
            ///         The remaining 75 Gold will be taxed at 6% (assuming no reduction due to political influence)
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("Laundry")]
            public double Laundry
            {
                get { return m_Laundry; }
                set { m_Laundry = Math.Max(value, 0.0); }
            }

            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("Rate", Math.Round(this.Rate * 100).ToString("0.00%")));
                    data.Add(new XAttribute("Minimum", Math.Round(this.Minimum * 100).ToString("0.00%")));
                    data.Add(new XAttribute("Laundry", Math.Round(this.Laundry * 100).ToString("0.00%")));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("Rate").Value) && double.TryParse(data.Attribute("Rate").Value.Replace("%", string.Empty), out convertDouble))
                    { this.Rate = convertDouble / 100; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("Rate").Value) && double.TryParse(data.Attribute("Minimum").Value.Replace("%", string.Empty), out convertDouble))
                    { this.Minimum = convertDouble / 100; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("Rate").Value) && double.TryParse(data.Attribute("Laundry").Value.Replace("%", string.Empty), out convertDouble))
                    { this.Laundry = convertDouble / 100; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Pregnancy data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Pregnancy")]
        public class PregnancyData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Odds being pregnant by player relation.
            /// </summary>
            private double m_PlayerChance = 8;
            /// <summary>
            /// Odds being pregnant by customer relation.
            /// </summary>
            private double m_CustomerChance = 8;
            /// <summary>
            /// Odds being pregnant by monster relation.
            /// </summary>
            private double m_MonsterChance = 8;
            /// <summary>
            /// Multiplier for the pregnancy chance if both parties were happy post coitus.
            /// </summary>
            private double m_GoodSexFactor = 2.0; // `J` changed from 8 to 2
            /// <summary>
            /// %chance of any baby being female.
            /// </summary>
            private double m_ChanceOfGirl = 50;
            /// <summary>
            /// How long she is pregnant for.
            /// </summary>
            private int m_WeeksPregnant = 38;
            /// <summary>
            /// How long she is pregnant for.
            /// </summary>
            private int m_WeeksMonsterPregnant = 20; // `J` added
            /// <summary>
            /// Weekly percent chance that the pregnancy may fail.
            /// </summary>
            private double m_MiscarriageChance = 0.1; // `J` added
            /// <summary>
            /// Weekly percent chance that the monster pregnancy may fail.
            /// </summary>
            private double m_MiscarriageMonsterChance = 1.0; // `J` added
            /// <summary>
            /// How long is takes for the baby to grow up to age 18.
            /// <remarks><para>The magic of the world the game is set in causes children to age much faster. Real world is 936 weeks.</para></remarks>
            /// </summary>
            private int m_WeeksTillGrown = 60;
            /// <summary>
            /// How long before the girl can get pregnant again after giving birth.
            /// </summary>
            private int m_CoolDown = 4; // `J` changed from 60 weeks to 4 weeks
            /// <summary>
            /// Chance that an Anti-Preg Potion fails to work.
            /// </summary>
            private double m_AntiPregnancyFailure = 0.0; // `J` added
            /// <summary>
            /// Chance of multiple births.
            /// </summary>
            private double m_MultiBirthChance = 1.0; // `J` added
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the odds being pregnant by player relation.
            /// </summary>
            [XmlAttribute("PlayerChance")]
            public double PlayerChance
            {
                get { return m_PlayerChance; }
                set { m_PlayerChance = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set the odds being pregnant by customer relation.
            /// </summary>
            [XmlAttribute("CustomerChance")]
            public double CustomerChance
            {
                get { return m_CustomerChance; }
                set { m_CustomerChance = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set the odds being pregnant by monster relation.
            /// </summary>
            [XmlAttribute("MonsterChance")]
            public double MonsterChance
            {
                get { return m_MonsterChance; }
                set { m_MonsterChance = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set the multiplier for the pregnancy chance if both parties were happy post coitus.
            /// </summary>
            [XmlAttribute("GoodSexFactor")]
            public double GoodSexFactor
            {
                get { return m_GoodSexFactor; }
                set { m_GoodSexFactor = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set the %chance of any baby being female.
            /// </summary>
            [XmlAttribute("ChanceOfGirl")]
            public double ChanceOfGirl
            {
                get { return m_ChanceOfGirl; }
                set { m_ChanceOfGirl = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or ser how long she is pregnant for.
            /// </summary>
            [XmlAttribute("WeeksPregnant")]
            public int WeeksPregnant
            {
                get { return m_WeeksPregnant; }
                set { m_WeeksPregnant = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or ser how long she is pregnant for.
            /// </summary>
            [XmlAttribute("WeeksMonsterP")]
            public int WeeksMonsterPregnant
            {
                get { return m_WeeksMonsterPregnant; }
                set { m_WeeksMonsterPregnant = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set weekly percent chance that the pregnancy may fail.
            /// </summary>
            [XmlAttribute("MiscarriageChance")]
            public double MiscarriageChance
            {
                get { return m_MiscarriageChance; }
                set { m_MiscarriageChance = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Get or set weekly percent chance that the monster pregnancy may fail.
            /// </summary>
            [XmlAttribute("MiscarriageMonster")]
            public double MiscarriageMonsterChance
            {
                get { return m_MiscarriageMonsterChance; }
                set { m_MiscarriageMonsterChance = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Get or set how long is takes for the baby to grow up to age 18.
            /// <remarks><para>The magic of the world the game is set in causes children to age much faster. Real world is 936 weeks.</para></remarks>
            /// </summary>
            [XmlAttribute("WeeksTillGrown")]
            public int WeeksTillGrown
            {
                get { return m_WeeksTillGrown; }
                set { m_WeeksTillGrown = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set how long before the girl can get pregnant again after giving birth.
            /// </summary>
            [XmlAttribute("CoolDown")]
            public int CoolDown
            {
                get { return m_CoolDown; }
                set { m_CoolDown = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set chance that an Anti-Preg Potion fails to work.
            /// </summary>
            [XmlAttribute("AntiPregFailure")]
            public double AntiPregnancyFailure
            {
                get { return m_AntiPregnancyFailure; }
                set { m_AntiPregnancyFailure = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Get or set chance of multiple births.
            /// </summary>
            [XmlAttribute("MultiBirthChance")]
            public double MultiBirthChance
            {
                get { return m_MultiBirthChance; }
                set { m_MultiBirthChance = Math.Max(Math.Min(value, 0), 50); } // `J` limited
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("PlayerChance", this.PlayerChance.ToString("0.00%")));
                    data.Add(new XAttribute("CustomerChance", this.CustomerChance.ToString("0.00%")));
                    data.Add(new XAttribute("MonsterChance", this.MonsterChance.ToString("0.00%")));
                    data.Add(new XAttribute("GoodSexFactor", this.GoodSexFactor));
                    data.Add(new XAttribute("ChanceOfGirl", this.ChanceOfGirl.ToString("0.00%")));
                    data.Add(new XAttribute("WeeksPregnant", this.WeeksPregnant));
                    data.Add(new XAttribute("WeeksMonsterP", this.WeeksMonsterPregnant));
                    data.Add(new XAttribute("MiscarriageChance", this.MiscarriageChance.ToString("0.00%")));
                    data.Add(new XAttribute("MiscarriageMonster", this.MiscarriageMonsterChance.ToString("0.00%")));
                    data.Add(new XAttribute("WeeksTillGrown", this.WeeksTillGrown));
                    data.Add(new XAttribute("CoolDown", this.CoolDown));
                    data.Add(new XAttribute("AntiPregFailure", this.AntiPregnancyFailure.ToString("0.00%")));
                    data.Add(new XAttribute("MultiBirthChance", this.MultiBirthChance.ToString("0.00%")));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                int convertInt;
                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("PlayerChance").Value) && double.TryParse(data.Attribute("PlayerChance").Value.Replace("%", string.Empty), out convertDouble))
                    { this.PlayerChance = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("CustomerChance").Value) && double.TryParse(data.Attribute("CustomerChance").Value.Replace("%", string.Empty), out convertDouble))
                    { this.CustomerChance = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("MonsterChance").Value) && double.TryParse(data.Attribute("MonsterChance").Value.Replace("%", string.Empty), out convertDouble))
                    { this.MonsterChance = convertDouble; }
                    if (double.TryParse(data.Attribute("GoodSexFactor").Value, out convertDouble)) /*     */ { this.GoodSexFactor = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("ChanceOfGirl").Value) && double.TryParse(data.Attribute("ChanceOfGirl").Value.Replace("%", string.Empty), out convertDouble))
                    { this.ChanceOfGirl = convertDouble; }
                    if (int.TryParse(data.Attribute("WeeksPregnant").Value, out convertInt)) /*           */ { this.WeeksPregnant = convertInt; }
                    if (int.TryParse(data.Attribute("WeeksMonsterP").Value, out convertInt)) /*           */ { this.WeeksMonsterPregnant = convertInt; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("MiscarriageChance").Value) && double.TryParse(data.Attribute("MiscarriageChance").Value.Replace("%", string.Empty), out convertDouble))
                    { this.MiscarriageChance = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("MiscarriageMonster").Value) && double.TryParse(data.Attribute("MiscarriageMonster").Value.Replace("%", string.Empty), out convertDouble))
                    { this.MiscarriageMonsterChance = convertDouble; }
                    if (int.TryParse(data.Attribute("WeeksTillGrown").Value, out convertInt)) /*          */ { this.WeeksTillGrown = convertInt; }
                    if (int.TryParse(data.Attribute("CoolDown").Value, out convertInt)) /*                */ { this.CoolDown = convertInt; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("AntiPregFailure").Value) && double.TryParse(data.Attribute("AntiPregFailure").Value.Replace("%", string.Empty), out convertDouble))
                    { this.AntiPregnancyFailure = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("MultiBirthChance").Value) && double.TryParse(data.Attribute("MultiBirthChance").Value.Replace("%", string.Empty), out convertDouble))
                    { this.MultiBirthChance = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Prostitution data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Prostitution")]
        public class ProstitutionData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Base chances of rape occurring in streetwalking.
            /// </summary>
            private double m_RapeStreets = 5;
            /// <summary>
            /// Base chances of rape occurring in a brothel.
            /// </summary>
            private double m_RapeBrothel = 1;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or ser the base chances of rape occurring in streetwalking.
            /// </summary>
            [XmlAttribute("RapeStreet")]
            public double RapeStreets
            {
                get { return m_RapeStreets; }
                set { m_RapeStreets = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Get or ser the base chances of rape occurring in a brothel.
            /// </summary>
            [XmlAttribute("RapeBrothel")]
            public double RapeBrothel
            {
                get { return m_RapeBrothel; }
                set { m_RapeBrothel = Math.Max(Math.Min(value, 0.0), 100.0); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("RapeStreet", this.RapeStreets.ToString("0.00%")));
                    data.Add(new XAttribute("RapeBrothel", this.RapeBrothel.ToString("0.00%")));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("RapeStreet").Value) && double.TryParse(data.Attribute("RapeStreet").Value.Replace("%", string.Empty), out convertDouble))
                    { this.RapeStreets = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("RapeBrothel").Value) && double.TryParse(data.Attribute("RapeBrothel").Value.Replace("%", string.Empty), out convertDouble))
                    { this.RapeBrothel = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Catacombs data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Catacombs")]
        public class CatacombsData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Chance to get a Unique Girl when exploring the Catacombs.
            /// <remarks><para>After all Unique Girls have been found, the rest will be random girls.</para></remarks>
            /// </summary>
            private double m_UniqueCatacombs = 50.0;
            /// <summary>
            /// Determine if girl ratio of what to find in catacomb are used.
            /// If <B>ControlGirls</B> is true, these will determine what girl try to get when you send it into the catacombs.
            /// See <b>GirlGetsGirls</b>, <b>GirlGetsItems</b> and <b>GirlGetsBeast</b>
            /// </summary>
            private bool m_ControlGirls = false;
            /// <summary>
            /// Determine the ratio of Girls that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GirlGetsGirls = 33.33;
            /// <summary>
            /// Determine the ratio of Items that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GirlGetsItems = 33.33;
            /// <summary>
            /// Determine the ratio of Beasts that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GirlGetsBeast = 33.33;
            /// <summary>
            /// Determine if gang ratio of what to find in catacomb are used.
            /// If <B>ControlGangs</B> is true, these will determine what gang try to get when you send it into the catacombs.
            /// See <b>GangGetsGirls</b>, <b>GangGetsItems</b> and <b>GangGetsBeast</b>
            /// </summary>
            private bool m_ControlGangs = false;
            /// <summary>
            /// Determine the ratio of Girls that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GangGetsGirls = 33.33;
            /// <summary>
            /// Determine the ratio of Items that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GangGetsItems = 33.33;
            /// <summary>
            /// Determine the ratio of Beasts that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            private double m_GangGetsBeast = 33.33;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the chance to get a Unique Girl when exploring the Catacombs.
            /// <remarks><para>After all Unique Girls have been found, the rest will be random girls.</para></remarks>
            /// </summary>
            [XmlAttribute("UniqueCatacombs")]
            public double UniqueCatacombs
            {
                get { return m_UniqueCatacombs; }
                set { m_UniqueCatacombs = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine if girl ratio of what to find in catacomb are used.
            /// If <B>ControlGirls</B> is true, these will determine what girl try to get when you send it into the catacombs.
            /// See <b>GirlGetsGirls</b>, <b>GirlGetsItems</b> and <b>GirlGetsBeast</b>
            /// </summary>
            [XmlAttribute("ControlGirls")]
            public bool ControlGirls
            {
                get { return m_ControlGirls; }
                set { m_ControlGirls = value; }
            }

            /// <summary>
            /// Determine the ratio of Girls that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GirlGetsGirls")]
            public double GirlGetsGirls
            {
                get { return m_GirlGetsGirls; }
                set { m_GirlGetsGirls = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine the ratio of Items that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GirlGetsItems")]
            public double GirlGetsItems
            {
                get { return m_GirlGetsItems; }
                set { m_GirlGetsItems = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine the ratio of Beasts that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GirlGetsBeast")]
            public double GirlGetsBeast
            {
                get { return m_GirlGetsBeast; }
                set { m_GirlGetsBeast = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine if gang ratio of what to find in catacomb are used.
            /// If <B>ControlGangs</B> is true, these will determine what gang try to get when you send it into the catacombs.
            /// See <b>GangGetsGirls</b>, <b>GangGetsItems</b> and <b>GangGetsBeast</b>
            /// </summary>
            [XmlAttribute("ControlGangs")]
            public bool ControlGangs
            {
                get { return m_ControlGangs; }
                set { m_ControlGangs = value; }
            }

            /// <summary>
            /// Determine the ratio of Girls that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GangGetsGirls")]
            public double GangGetsGirls
            {
                get { return m_GangGetsGirls; }
                set { m_GangGetsGirls = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine the ratio of Items that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GangGetsItems")]
            public double GangGetsItems
            {
                get { return m_GangGetsItems; }
                set { m_GangGetsItems = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// Determine the ratio of Beasts that they try to come back with.
            /// <remarks>
            ///     <para>
            ///         The numbers entered here are normalized into fractions of 100% by the game.
            ///         Negative numbers are not allowed and all 0s will set to (100/3)% each.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("GangGetsBeast")]
            public double GangGetsBeast
            {
                get { return m_GangGetsBeast; }
                set { m_GangGetsBeast = Math.Max(Math.Min(value, 0.0), 100.0); }
            }
            #endregion

            /// <summary>
            /// Check if sum of girls or members ratio of finding girls, items and beast are equal to 100.
            /// </summary>
            public void CheckCatacombsData()
            {
                // make them percents
                double checkggirl = Catacombs.GirlGetsGirls + Catacombs.GirlGetsItems + Catacombs.GirlGetsBeast;
                if (checkggirl == 0)
                {
                    Catacombs.GirlGetsGirls = Catacombs.GirlGetsItems = Catacombs.GirlGetsBeast = (100 / 3);
                }
                else if (checkggirl != 100)
                {
                    Catacombs.GirlGetsGirls = (100.0 / checkggirl) * Catacombs.GirlGetsGirls;
                    Catacombs.GirlGetsItems = (100.0 / checkggirl) * Catacombs.GirlGetsItems;
                    Catacombs.GirlGetsBeast = 100.0 - Catacombs.GirlGetsGirls - Catacombs.GirlGetsItems;
                }

                double checkggang = Catacombs.GangGetsGirls + Catacombs.GangGetsItems + Catacombs.GangGetsBeast;
                if (checkggang == 0)
                {
                    Catacombs.GangGetsGirls = Catacombs.GangGetsItems = Catacombs.GangGetsBeast = (100 / 3);
                }
                else if (checkggang != 100)
                {
                    Catacombs.GangGetsGirls = (100.0 / checkggang) * Catacombs.GangGetsGirls;
                    Catacombs.GangGetsItems = (100.0 / checkggang) * Catacombs.GangGetsItems;
                    Catacombs.GangGetsBeast = 100.0 - Catacombs.GangGetsGirls - Catacombs.GangGetsItems;
                }
            }

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("UniqueCatacombs", this.UniqueCatacombs.ToString("0.00%")));
                    data.Add(new XAttribute("ControlGirls", this.ControlGirls));
                    data.Add(new XAttribute("GirlGetsGirls", this.GirlGetsGirls.ToString("0.00%")));
                    data.Add(new XAttribute("GirlGetsItems", this.GirlGetsItems.ToString("0.00%")));
                    data.Add(new XAttribute("GirlGetsBeast", this.GirlGetsBeast.ToString("0.00%")));
                    data.Add(new XAttribute("ControlGangs", this.ControlGangs));
                    data.Add(new XAttribute("GangGetsGirls", this.GangGetsGirls.ToString("0.00%")));
                    data.Add(new XAttribute("GangGetsItems", this.GangGetsItems.ToString("0.00%")));
                    data.Add(new XAttribute("GangGetsBeast", this.GangGetsBeast.ToString("0.00%")));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                double convertDouble;
                bool convertBool;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("UniqueCatacombs").Value) && double.TryParse(data.Attribute("UniqueCatacombs").Value.Replace("%", string.Empty), out convertDouble))
                    { this.UniqueCatacombs = convertDouble; }
                    if (bool.TryParse(data.Attribute("ControlGirls").Value, out convertBool)) /*       */ { this.ControlGirls = convertBool; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GirlGetsGirls").Value) && double.TryParse(data.Attribute("GirlGetsGirls").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GirlGetsGirls = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GirlGetsItems").Value) && double.TryParse(data.Attribute("GirlGetsItems").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GirlGetsItems = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GirlGetsBeast").Value) && double.TryParse(data.Attribute("GirlGetsBeast").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GirlGetsBeast = convertDouble; }
                    if (bool.TryParse(data.Attribute("ControlGangs").Value, out convertBool)) /*       */ { this.ControlGangs = convertBool; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GangGetsGirls").Value) && double.TryParse(data.Attribute("GangGetsGirls").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GangGetsGirls = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GangGetsItems").Value) && double.TryParse(data.Attribute("GangGetsItems").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GangGetsItems = convertDouble; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("GangGetsBeast").Value) && double.TryParse(data.Attribute("GangGetsBeast").Value.Replace("%", string.Empty), out convertDouble))
                    { this.GangGetsBeast = convertDouble; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Slave market data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("SlaveMarket")]
        public class SlaveMarketData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Chance to get a Unique Girl from the Slave Market.
            /// <remarks><para>After all Unique Girls have been found, the rest will be random girls.</para></remarks>
            /// </summary>
            private double m_UniqueMarket = 35;
            /// <summary>
            /// The minimum number of girls in the slave market each turn.
            /// <remarks><para>Absolude minimum of 0. if If <b>SlavesNewWeeklyMin</b> is higher than <b>SlavesNewWeeklyMax</b>, they get switched.</para></remarks>
            /// </summary>
            private int m_SlavesNewWeeklyMin = 5;
            /// <summary>
            /// The maximum number of girls in the slave market each turn.
            /// <remarks><para>Absolude minimum of 0. if If <b>SlavesNewWeeklyMin</b> is higher than <b>SlavesNewWeeklyMax</b>, they get switched.</para></remarks>
            /// </summary>
            private int m_SlavesNewWeeklyMax = 12;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the chance to get a Unique Girl from the Slave Market.
            /// <remarks><para>After all Unique Girls have been found, the rest will be random girls.</para></remarks>
            /// </summary>
            [XmlAttribute("UniqueMarket")]
            public double UniqueMarket
            {
                get { return m_UniqueMarket; }
                set { m_UniqueMarket = Math.Max(Math.Min(value, 0.0), 100.0); }
            }

            /// <summary>
            /// TGet or set te minimum number of girls in the slave market each turn.
            /// <remarks><para>Absolude minimum of 0. if If <b>SlavesNewWeeklyMin</b> is higher than <b>SlavesNewWeeklyMax</b>, they get switched.</para></remarks>
            /// </summary>
            [XmlAttribute("SlavesNewWeeklyMin")]
            public int SlavesNewWeeklyMin
            {
                get { return Math.Min(m_SlavesNewWeeklyMin, m_SlavesNewWeeklyMax); }
                set { m_SlavesNewWeeklyMin =  Math.Max(value, 0); }
            }

            /// <summary>
            /// Get or set the maximum number of girls in the slave market each turn.
            /// <remarks><para>Absolude minimum of 0. if If <b>SlavesNewWeeklyMin</b> is higher than <b>SlavesNewWeeklyMax</b>, they get switched.</para></remarks>
            /// </summary>
            [XmlAttribute("SlavesNewWeeklyMax")]
            public int SlavesNewWeeklyMax
            {
                get { return Math.Max(m_SlavesNewWeeklyMin, m_SlavesNewWeeklyMax); }
                set { m_SlavesNewWeeklyMax = Math.Min(value, 20); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("UniqueMarket", this.UniqueMarket.ToString("0.00%")));
                    data.Add(new XAttribute("SlavesNewWeeklyMin", this.SlavesNewWeeklyMin));
                    data.Add(new XAttribute("SlavesNewWeeklyMax", this.SlavesNewWeeklyMax));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                int convertInt;
                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (!string.IsNullOrWhiteSpace(data.Attribute("UniqueMarket").Value) && double.TryParse(data.Attribute("UniqueMarket").Value.Replace("%", string.Empty), out convertDouble))
                    { this.UniqueMarket = convertDouble; }
                    if (int.TryParse(data.Attribute("SlavesNewWeeklyMin").Value, out convertInt)) /**/ { this.SlavesNewWeeklyMin = convertInt; }
                    if (int.TryParse(data.Attribute("SlavesNewWeeklyMax").Value, out convertInt)) /**/ { this.SlavesNewWeeklyMax = convertInt; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Gang data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Gangs")]
        public class GangData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// Maximum number of recruitable gangs listed for player to hire.
            /// <remarks>
            ///     <para>
            ///         WARNING: BE CAREFUL here; the number of recruitable gangs plus the number of potential hired gangs
            ///         must not exceed the number of names stored in HiredGangNames.txt.
            ///         For example, with 20 names, player could have a max of 12 recruitables since he have to account for the possible 8 hired gangs.
            ///     </para>
            /// </remarks>
            /// </summary>
            private int m_MaxRecruitList = 6;
            /// <summary>
            /// How many random recruitable gangs are created for you at the start of a new game.
            /// </summary>
            private int m_StartRandom = 2;
            /// <summary>
            /// How many stat-boosted starting gangs are also added.
            /// </summary>
            private int m_StartBoosted = 2;
            /// <summary>
            /// Minimal number of initial gang members which are in each recruitable gang.
            /// <remarks><para>A random number between <b>InitMemberMin</b> and <b>InitMemberMax</b> is picked.</para></remarks>
            /// </summary>
            private int m_InitMemberMin = 1;
            /// <summary>
            /// Maximmal number of initial gang members which are in each recruitable gang.
            /// <remarks><para>A random number between <b>InitMemberMin</b> and <b>InitMemberMax</b> is picked.</para></remarks>
            /// </summary>
            private int m_InitMemberMax = 10;
            /// <summary>
            /// %chance each week that each unhired gang in the recruitable list is removed.
            /// </summary>
            private double m_ChanceRemoveUnwanted = 25;
            /// <summary>
            /// Minimum of new random gangs added to the recruitable gangs list each week.
            /// <remarks><para>A random number between <b>AddNewWeeklyMin</b> and <b>AddNewWeeklyMax</b> is picked.</para></remarks>
            /// </summary>
            private int m_AddNewWeeklyMin = 0;
            /// <summary>
            /// Maximum of new random gangs added to the recruitable gangs list each week.
            /// <remarks><para>A random number between <b>AddNewWeeklyMin</b> and <b>AddNewWeeklyMax</b> is picked.</para></remarks>
            /// </summary>
            private int m_AddNewWeeklyMax = 2;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set the maximum number of recruitable gangs listed for player to hire.
            /// <remarks>
            ///     <para>
            ///         WARNING: BE CAREFUL here; the number of recruitable gangs plus the number of potential hired gangs
            ///         must not exceed the number of names stored in HiredGangNames.txt.
            ///         For example, with 20 names, player could have a max of 12 recruitables since he have to account for the possible 8 hired gangs.
            ///     </para>
            /// </remarks>
            /// </summary>
            [XmlAttribute("MaxRecruitList")]
            public int MaxRecruitList
            {
                get { return m_MaxRecruitList; }
                set { m_MaxRecruitList = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set how many random recruitable gangs are created for you at the start of a new game.
            /// </summary>
            [XmlAttribute("StartRandom")]
            public int StartRandom
            {
                get { return m_StartRandom; }
                set { m_StartRandom = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set how many stat-boosted starting gangs are also added.
            /// </summary>
            [XmlAttribute("StartBoosted")]
            public int StartBoosted
            {
                get { return m_StartBoosted; }
                set { m_StartBoosted = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set the minimal number of initial gang members which are in each recruitable gang.
            /// <remarks><para>A random number between <b>InitMemberMin</b> and <b>InitMemberMax</b> is picked.</para></remarks>
            /// </summary>
            [XmlAttribute("InitMemberMin")]
            public int InitMemberMin
            {
                get { return m_InitMemberMin; }
                set { m_InitMemberMin = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set the maximmal number of initial gang members which are in each recruitable gang.
            /// <remarks><para>A random number between <b>InitMemberMin</b> and <b>InitMemberMax</b> is picked.</para></remarks>
            /// </summary>
            [XmlAttribute("InitMemberMax")]
            public int InitMemberMax
            {
                get { return m_InitMemberMax; }
                set { m_InitMemberMax = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set the %chance each week that each unhired gang in the recruitable list is removed.
            /// </summary>
            [XmlAttribute("ChanceRemoveUnwanted")]
            public double ChanceRemoveUnwanted
            {
                get { return m_ChanceRemoveUnwanted; }
                set { m_ChanceRemoveUnwanted = Math.Max(Math.Min(value, 0), 100); }
            }

            /// <summary>
            /// Get or set the minimum of new random gangs added to the recruitable gangs list each week.
            /// <remarks><para>A random number between <b>AddNewWeeklyMin</b> and <b>AddNewWeeklyMax</b> is picked.</para></remarks>
            /// </summary>
            [XmlAttribute("AddNewWeeklyMin")]
            public int AddNewWeeklyMin
            {
                get { return m_AddNewWeeklyMin; }
                set { m_AddNewWeeklyMin = Math.Min(value, 0); }
            }

            /// <summary>
            /// Get or set the maximum of new random gangs added to the recruitable gangs list each week.
            /// <remarks><para>A random number between <b>AddNewWeeklyMin</b> and <b>AddNewWeeklyMax</b> is picked.</para></remarks>
            /// </summary>
            [XmlAttribute("AddNewWeeklyMax")]
            public int AddNewWeeklyMax
            {
                get { return m_AddNewWeeklyMax; }
                set { m_AddNewWeeklyMax = Math.Min(value, 0); }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("MaxRecruitList", this.MaxRecruitList));
                    data.Add(new XAttribute("StartRandom", this.StartRandom));
                    data.Add(new XAttribute("StartBoosted", this.StartBoosted));
                    data.Add(new XAttribute("InitMemberMin", this.InitMemberMin));
                    data.Add(new XAttribute("InitMemberMax", this.InitMemberMax));
                    data.Add(new XAttribute("ChanceRemoveUnwanted", this.ChanceRemoveUnwanted.ToString("0.00%")));
                    data.Add(new XAttribute("AddNewWeeklyMin", this.AddNewWeeklyMin));
                    data.Add(new XAttribute("AddNewWeeklyMax", this.AddNewWeeklyMax));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                int convertInt;
                double convertDouble;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (int.TryParse(data.Attribute("MaxRecruitList").Value, out convertInt)) /*      */ { this.MaxRecruitList = convertInt; }
                    if (int.TryParse(data.Attribute("StartRandom").Value, out convertInt)) /*         */ { this.StartRandom = convertInt; }
                    if (int.TryParse(data.Attribute("StartBoosted").Value, out convertInt)) /*        */ { this.StartBoosted = convertInt; }
                    if (int.TryParse(data.Attribute("InitMemberMin").Value, out convertInt)) /*       */ { this.InitMemberMin = convertInt; }
                    if (int.TryParse(data.Attribute("InitMemberMax").Value, out convertInt)) /*       */ { this.InitMemberMax = convertInt; }
                    if (!string.IsNullOrWhiteSpace(data.Attribute("ChanceRemoveUnwanted").Value) && double.TryParse(data.Attribute("ChanceRemoveUnwanted").Value.Replace("%", string.Empty), out convertDouble))
                    { this.ChanceRemoveUnwanted = convertDouble; }
                    if (int.TryParse(data.Attribute("AddNewWeeklyMin").Value, out convertInt)) /*     */ { this.AddNewWeeklyMin = convertInt; }
                    if (int.TryParse(data.Attribute("AddNewWeeklyMax").Value, out convertInt)) /*     */ { this.AddNewWeeklyMax = convertInt; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Farm data configuration structure.
        /// </summary>
        [Serializable()]
        [XmlRoot("Farm")]
        public class FarmData : ISerialisableEntity
        {
            #region Private fields
            private bool m_Active = false;
            #endregion

            #region Public properties
            [XmlAttribute("Active")]
            public bool Active
            {
                get { return m_Active; }
                set { m_Active = value; }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("Active", this.Active));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                bool convertBool;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (bool.TryParse(data.Attribute("Active").Value, out convertBool)) /**/ { this.Active = convertBool; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Debug data configuration structure.
        /// </summary>
        [Obsolete("Try to syncronize DebugData to WMLog", false)]
        [Serializable()]
        [XmlRoot("Debug")]
        public class DebugData : ISerialisableEntity
        {
            #region Private fields
            private bool m_LogAll = false;
            private bool m_LogGirls = false;
            private bool m_LogRGirls = false;
            private bool m_LogGirlFights = false;
            private bool m_LogItems = false;
            private bool m_LogFonts = false;
            private bool m_LogTorture = false;
            private bool m_LogDebug = false;
            private bool m_LogExtraDetails = false;
            private bool m_LogShowNumbers = false;
            #endregion

            #region Public properties
            [XmlAttribute("LogAll")]
            public bool LogAll
            {
                get { return m_LogAll; }
                set { m_LogAll = value; }
            }

            [XmlAttribute("LogGirls")]
            public bool LogGirls
            {
                get { return m_LogGirls; }
                set { m_LogGirls = value; }
            }

            [XmlAttribute("LogRGirls")]
            public bool LogRGirls
            {
                get { return m_LogRGirls; }
                set { m_LogRGirls = value; }
            }

            [XmlAttribute("LogGirlFights")]
            public bool LogGirlFights
            {
                get { return m_LogGirlFights; }
                set { m_LogGirlFights = value; }
            }

            [XmlAttribute("LogItems")]
            public bool LogItems
            {
                get { return m_LogItems; }
                set { m_LogItems = value; }
            }

            [XmlAttribute("LogFonts")]
            public bool LogFonts
            {
                get { return m_LogFonts; }
                set { m_LogFonts = value; }
            }

            [XmlAttribute("LogTorture")]
            public bool LogTorture
            {
                get { return m_LogTorture; }
                set { m_LogTorture = value; }
            }

            [XmlAttribute("LogDebug")]
            public bool LogDebug
            {
                get { return m_LogDebug; }
                set { m_LogDebug = value; }
            }

            [XmlAttribute("LogExtraDetails")]
            public bool LogExtraDetails
            {
                get { return m_LogExtraDetails; }
                set { m_LogExtraDetails = value; }
            }

            [XmlAttribute("LogShowNumbers")]
            public bool LogShowNumbers
            {
                get { return m_LogShowNumbers; }
                set { m_LogShowNumbers = value; }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("LogAll", this.LogAll));
                    data.Add(new XAttribute("LogGirls", this.LogGirls));
                    data.Add(new XAttribute("LogRGirls", this.LogRGirls));
                    data.Add(new XAttribute("LogGirlFights", this.LogGirlFights));
                    data.Add(new XAttribute("LogItems", this.LogItems));
                    data.Add(new XAttribute("LogFonts", this.LogFonts));
                    data.Add(new XAttribute("LogTorture", this.LogTorture));
                    data.Add(new XAttribute("LogDebug", this.LogDebug));
                    data.Add(new XAttribute("LogExtraDetails", this.LogExtraDetails));
                    data.Add(new XAttribute("LogShowNumbers", this.LogShowNumbers));

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                bool convertBool;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (bool.TryParse(data.Attribute("LogAll").Value, out convertBool)) /*         */ { this.LogAll = convertBool; }
                    if (bool.TryParse(data.Attribute("LogGirls").Value, out convertBool)) /*       */ { this.LogGirls = convertBool; }
                    if (bool.TryParse(data.Attribute("LogRGirls").Value, out convertBool)) /*      */ { this.LogRGirls = convertBool; }
                    if (bool.TryParse(data.Attribute("LogGirlFights").Value, out convertBool)) /*  */ { this.LogGirlFights = convertBool; }
                    if (bool.TryParse(data.Attribute("LogItems").Value, out convertBool)) /*       */ { this.LogItems = convertBool; }
                    if (bool.TryParse(data.Attribute("LogFonts").Value, out convertBool)) /*       */ { this.LogFonts = convertBool; }
                    if (bool.TryParse(data.Attribute("LogTorture").Value, out convertBool)) /*     */ { this.LogTorture = convertBool; }
                    if (bool.TryParse(data.Attribute("LogDebug").Value, out convertBool)) /*       */ { this.LogDebug = convertBool; }
                    if (bool.TryParse(data.Attribute("LogExtraDetails").Value, out convertBool)) /**/ { this.LogExtraDetails = convertBool; }
                    if (bool.TryParse(data.Attribute("LogShowNumbers").Value, out convertBool)) /* */ { this.LogShowNumbers = convertBool; }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        /// <summary>
        /// Folders data configuration structure.
        /// </summary>
        [Obsolete("OS related structure", false)]
        [Serializable()]
        [XmlRoot("Folders")]
        public class FoldersData : ISerialisableEntity
        {
            #region Private fields
            /// <summary>
            /// If set to true, the game will save in both the game's default save folder as well as the folder set here.
            /// </summary>
            private bool m_Backupsaves = false;
            /// <summary>
            /// If set to true, the game will try to use default images before trying to find alternate images from the image tree.
            /// </summary>
            private bool m_PreferDefault = false;
            /// <summary>
            /// The location of the Characters folder
            /// </summary>
            private string m_Characters = string.Empty;
            /// <summary>
            /// `J` if character's location is set in config.xml.
            /// </summary>
            private bool m_ConfigXMLCharacters = false;
            /// <summary>
            /// The location of the Saves folder.
            /// </summary>
            private string m_Saves = string.Empty;
            /// <summary>
            /// `J` if saves's location is set in config.xml.
            /// </summary>
            private bool m_ConfigXMLSave = false;
            /// <summary>
            /// The location of the Items folder.
            /// </summary>
            private string m_Items = string.Empty;
            /// <summary>
            /// `J` if items's location is set in config.xml.
            /// </summary>
            private bool m_ConfigXMLItems = false;
            /// <summary>
            /// The location of the DefaultImages folder.
            /// </summary>
            private string m_DefaultImageLoc = string.Empty;
            /// <summary>
            /// J` if default images location is set in config.xml.
            /// </summary>
            private bool m_ConfigXMLDefaultImageLoc = false;
            #endregion

            #region Public properties
            /// <summary>
            /// Get or set if the game will save in both the game's default save folder as well as the folder set here.
            /// </summary>
            [XmlAttribute("BackupSaves")]
            public bool BackupSaves
            {
                get { return this.m_Backupsaves; }
                set { this.m_Backupsaves = value; }
            }

            /// <summary>
            /// Get or set if the game will try to use default images before trying to find alternate images from the image tree.
            /// </summary>
            [XmlAttribute("PreferDefault")]
            public bool PreferDefault
            {
                get { return this.m_PreferDefault; }
                set { this.m_PreferDefault = value; }
            }

            /// <summary>
            /// Get or set the location of the Characters folder.
            /// </summary>
            [XmlAttribute("Characters")]
            public string Characters
            {
                get { return this.m_Characters; }
                set { this.m_Characters = value ?? string.Empty; }
            }

            /// <summary>
            /// Get or set if character's location is set in config.xml.
            /// </summary>
            [XmlIgnore()]
            public bool ConfigXMLCharacters
            {
                get { return this.m_ConfigXMLCharacters; }
                set { this.m_ConfigXMLCharacters = value; }
            }

            /// <summary>
            /// Get or set the location of the Saves folder.
            /// </summary>
            [XmlAttribute("Saves")]
            public string Saves
            {
                get { return this.m_Saves; }
                set { this.m_Saves = value ?? string.Empty; }
            }

            /// <summary>
            /// Get or set if saves's location is set in config.xml.
            /// </summary>
            [XmlIgnore()]
            public bool ConfigXMLSave
            {
                get { return this.m_ConfigXMLSave; }
                set { this.m_ConfigXMLSave = value; }
            }

            /// <summary>
            /// Get or set the location of the Items folder.
            /// </summary>
            [XmlAttribute("Items")]
            public string Items
            {
                get { return this.m_Items; }
                set { this.m_Items = value ?? string.Empty; }
            }

            /// <summary>
            /// Get or set if items's location is set in config.xml.
            /// </summary>
            [XmlIgnore()]
            public bool ConfigXMLItems
            {
                get { return this.m_ConfigXMLItems; }
                set { this.m_ConfigXMLItems = value; }
            }

            /// <summary>
            /// Get or set the location of the DefaultImages folder.
            /// </summary>
            [XmlAttribute("DefaultImages")]
            public string DefaultImageLoc
            {
                get { return this.m_DefaultImageLoc; }
                set { this.m_DefaultImageLoc = value ?? string.Empty; }
            }

            /// <summary>
            /// Get or set if default images location is set in config.xml.
            /// </summary>
            [XmlIgnore()]
            public bool ConfigXMLDefaultImageLoc
            {
                get { return this.m_ConfigXMLDefaultImageLoc; }
                set { this.m_ConfigXMLDefaultImageLoc = value; }
            }
            #endregion

            #region Serialisation
            /// <summary>
            /// Serialise instance into <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data"><see cref="XElement"/> to store serialised data.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Serialise(XElement data)
            {
                if (data == null)
                { return false; }

                try
                {
                    Serialiser.SetInvarientCulture();
                    data.Add(new XAttribute("BackupSaves", this.BackupSaves));
                    data.Add(new XAttribute("PreferDefault", this.PreferDefault));
                    if (this.ConfigXMLCharacters)
                    { data.Add(new XAttribute("Characters", this.Characters)); }
                    if (this.ConfigXMLSave)
                    { data.Add(new XAttribute("Saves", this.Saves)); }
                    if (this.ConfigXMLItems)
                    { data.Add(new XAttribute("Items", this.Items)); }
                    if (this.ConfigXMLDefaultImageLoc)
                    { data.Add(new XAttribute("DefaultImages", this.DefaultImageLoc)); }

                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }

            /// <summary>
            /// Deserialise instance data from <see cref="XElement"/> <paramref name="data"/>.
            /// </summary>
            /// <param name="data">Where to find data to deserialise.</param>
            /// <returns><b>True</b> if no error occure.</returns>
            public bool Deserialise(XElement data)
            {
                if (data == null)
                { return false; }

                bool convertBool;
                try
                {
                    Serialiser.SetInvarientCulture();
                    if (bool.TryParse(data.Attribute("BackupSaves").Value, out convertBool)) /* */ { this.BackupSaves = convertBool; }
                    if (bool.TryParse(data.Attribute("PreferDefault").Value, out convertBool)) /*       */ { this.PreferDefault = convertBool; }

                    if (data.Attribute("Characters").Value != null)
                    {
                        this.Characters = data.Attribute("Characters").Value;
                        this.m_ConfigXMLCharacters = true;
                    }
                    if (data.Attribute("Saves").Value != null)
                    {
                        this.Saves = data.Attribute("Saves").Value;
                        this.m_ConfigXMLSave = true;
                    }
                    if (data.Attribute("Items").Value != null)
                    {
                        this.Items = data.Attribute("Items").Value;
                        this.m_ConfigXMLItems = true;
                    }
                    if (data.Attribute("DefaultImages").Value != null)
                    {
                        this.DefaultImageLoc = data.Attribute("DefaultImages").Value;
                        this.m_ConfigXMLDefaultImageLoc = true;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                    return false;
                }
                finally
                { Serialiser.RestoreCurrentCulture(); }
            }
            #endregion
        }

        //TODO : Rarity color must be move to interface.
        ///// <summary>
        ///// Item data configuration structure.
        ///// </summary>
        //[XmlRoot("Items")]
        //public class ItemData
        //{
        //    // IHM component
        //    //public SDL_Color[] rarity_color = Arrays.InitializeWithDefaultInstances<SDL_Color>(9);
        //    public ItemRarity Rarity;
        //}

        ///// <summary>
        ///// Resolution data configuration structure.
        ///// </summary>
        //[Obsolete("IHM related structure", true)]
        //[Serializable()]
        //[XmlRoot("Resolution")]
        //public class ResolutionData
        //{
        //    public string resolution;
        //    public int width;
        //    public int height;
        //    public int scalewidth;
        //    public int scaleheight;
        //    public bool fullscreen;
        //    public bool fixedscale;
        //    public bool configXML;
        //    public int list_scroll;
        //    public int text_scroll;
        //    public bool next_turn_enter;
        //}

        ///// <summary>
        ///// Item data configuration structure.
        ///// </summary>
        //[Obsolete("IHM related structure", true)]
        //[Serializable()]
        //[XmlRoot("Fonts")]
        //public class FontData
        //{
        //    public string normal;
        //    public string @fixed;
        //    public bool antialias;
        //    public bool showpercent;
        //    public int detailfontsize;
        //    public FontData()
        //    {
        //        this.normal = "";
        //        this.@fixed = "";
        //        this.antialias = false;
        //    }
        //}
        #endregion

        #region CTor / Initialisation
        /// <summary>
        /// Initialise a new <see cref="Configuration"/> instance. Provate constructor for singleton template.
        /// </summary>
        private Configuration()
        { }
        #endregion

        #region Fields / Properties
        /// <summary>
        /// Initial data configuration class.
        /// </summary>
        private InitialData m_Initial = new InitialData();
        /// <summary>
        /// Get or ser initial data configuration class.
        /// </summary>
        public static InitialData Initial
        {
            get { return Configuration.Instance.m_Initial; }
        }

        private IncomeFactorsData m_IncomeFactors = new IncomeFactorsData();
        public static IncomeFactorsData IncomeFactors
        {
            get { return Configuration.Instance.m_IncomeFactors; }
        }
        private OutgoingFactorsData m_OutgoingFactors = new OutgoingFactorsData();
        public static OutgoingFactorsData OutgoingFactors
        {
            get { return Configuration.Instance.m_OutgoingFactors; }
        }
        private GambleData m_Gamble = new GambleData();
        public static GambleData Gamble
        {
            get { return Configuration.Instance.m_Gamble; }
        }
        private TaxData m_Tax = new TaxData();
        public static TaxData Tax
        {
            get { return Configuration.Instance.m_Tax; }
        }
        private PregnancyData m_Pregnancy = new PregnancyData();
        public static PregnancyData Pregnancy
        {
            get { return Configuration.Instance.m_Pregnancy; }
        }
        private GangData m_Gangs = new GangData();
        public static GangData Gangs
        {
            get { return Configuration.Instance.m_Gangs; }
        }
        private ProstitutionData m_Prostitution = new ProstitutionData();
        public static ProstitutionData Prostitution
        {
            get { return Configuration.Instance.m_Prostitution; }
        }
        private CatacombsData m_Catacombs = new CatacombsData();
        public static CatacombsData Catacombs
        {
            get { return Configuration.Instance.m_Catacombs; }
        }
        private SlaveMarketData m_Slavemarket = new SlaveMarketData();
        public static SlaveMarketData Slavemarket
        {
            get { return Configuration.Instance.m_Slavemarket; }
        }
        private DebugData m_Debug = new DebugData();
        public static DebugData Debug
        {
            get { return Configuration.Instance.m_Debug; }
        }
        private FarmData m_Farm = new FarmData();
        public static FarmData Farm
        {
            get { return Configuration.Instance.m_Farm; }
        }
        [Obsolete("OS related structure", false)]
        private FoldersData m_Folders = new FoldersData();
        [Obsolete("OS related structure", false)]
        public static FoldersData Folders
        {
            get { return Configuration.Instance.m_Folders; }
        }
        #endregion

        public void get_debug_flags(XElement el)
        {
            if (Debug.LogAll)
            {
                Debug.LogGirls = Debug.LogRGirls = Debug.LogGirlFights = Debug.LogItems = Debug.LogFonts = Debug.LogTorture = Debug.LogDebug = Debug.LogExtraDetails = Debug.LogShowNumbers = Debug.LogAll;
            }
        }

        /// <summary>
        /// Serialise this instance data into <see cref="XElement"/>.
        /// </summary>
        /// <param name="data"><see cref="XElement"/> receving serialisation of this instance data.</param>
        public bool Serialise(XElement data)
        {
            if (data == null)
            { return false; }

            try
            {
                XComment comment;
                XElement element;

                comment = new XComment(@"
	Gold is how much gold you start the game with.
	GirlMeet is the %chance you'll meet a girl when walking around town.
	GirlsHousePerc and SlaveHousePerc is the default House Percentage for free girls and slave girls.
	GirlsKeepTips and GirlsKeepTips is whether they keep tips separate from house percent.
	SlavePayOutOfPocket is wether or not slave girls get paid by the player directly for certain jobs
		ie. Cleaning, Advertising, Farming jobs, Film jobs, etc.
	AutoUseItems is whether or not the game will try to automatically use
		the player's items intelligently on girls each week.
		This feature needs more testing.
	AutoCombatEquip determines whether girls will automatically equip their best weapon and
		armor for combat jobs and also automatically unequip weapon and armor for regular
		jobs where such gear would be considered inappropriate (i.e. whores-with-swords).
		Set to ""false"" to disable this feature.

	TortureTraitWeekMod affects multiplying the duration that they will
		keep a temporary trait that they get from being tortured.
		It is multiplied by the number of weeks in the dungeon.
`J` added		If TortureTraitWeekMod is set to -1 then torture is harsher.
		This doubles the chance of injuring the girls and doubles evil gain.
		Damage is increased by half. It also makes breaking the girls wills permanent.");
                data.Add(comment);
                element = new XElement("Initial");
                if (this.m_Initial.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	These are the numbers that will multiply the money from various sources of income.
		So setting ""GirlsWorkBrothel"" to ""0.5"" will reduce the cash your girls generate in the brothel by half.
		You can also use numbers >1 to increase income if you are so inclined.");
                data.Add(comment);
                element = new XElement("Income");
                if (this.m_IncomeFactors.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	These are the multipliers for your expenses.

	Training doesn't currently have a cost, so I'm setting it to 1 gold per girl per week
		and defaulting the multiplier to 0 (so no change by default).
	Set it higher and training begins to cost beyond the simple loss of income.

	ActressWages are like training costs:
	A per-girl expense nominally 1 gold per girl, but with a default factor of 0,
		so no change to the current scheme unless you alter that.

	MakingMovies is the setup cost for a movie:
	I'm going to make this 1000 gold per movie, but again, with a zero factor by default.

	Otherwise, same as above, except you probably want numbers > 1 to make things more expensive here.

	* not all are used but are retained just in case.");
                data.Add(comment);
                element = new XElement("Expenses");
                if (this.m_OutgoingFactors.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Gambling:

	The starting %chance for the tables is given by ""Odds""

	Wins and losses on the tables are calculated as the ""Base"" value
		plus a random number between 1 and the value of ""Spread"".
	If the house wins, the amount is multiplied by the HouseFactor
	If the customer wins, by the customer factor.

	So: if Base = 50 and spread = 100 then the basic amount
		won or lost per customer would be 50+d100.

	As it stands, the default odds are near 50%
	while the payout is 2:1 in favour of the house.
	So by default, the tables are rigged!");
                data.Add(comment);
                element = new XElement("Gambling");
                if (this.m_Gamble.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Taxes:
	Rate is the rate at which your income is taxed.
	Min is the minimum adjusted rate after influence is used to lower the tax rate.
	Laundry is the Maximum % of your income that can be Laundered and so escape taxation.
		So if you have 100g income, and a 25% laundry rating, then between 1 and 25 gold will go directly into your pocket.
		The remaining 75 Gold will be taxed at 6% (assuming no reduction due to political influence)");
                data.Add(comment);
                element = new XElement("Tax");
                if (this.m_Tax.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Pregnancy:
	PlayerChance, CustomerChance and MonsterChance give the odds of her
		getting knocked up by the PC, a customer and a monster, respectively
	GoodSexFactor is the multiplier for the pregnancy chance if both parties were happy post coitus.
	ChanceOfGirl is the %chance of any baby being female.
	WeeksPregnant and WeeksMonsterP is how long she is pregnant for.
	MiscarriageChance and MiscarriageMonster is the weekly percent chance that the pregnancy may fail.
	WeeksTillGrown is how long is takes for the baby to grow up to age 18
		The magic of the world the game is set in causes children to age much faster.
		Real world is 936 weeks.
	CoolDown is how long before the girl can get pregnant again after giving birth.
	AntiPregFailure is the chance that an Anti-Preg Potion fails to work.
	MultiBirthChance is the chance of multiple births.");
                data.Add(comment);
                element = new XElement("Pregnancy");
                if (this.m_Pregnancy.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	These are the base chances of rape occurring in a brothel and streetwalking.");
                data.Add(comment);
                element = new XElement("Prostitution");
                if (this.m_Prostitution.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Catacombs Settings:
	UniqueCatacombs:   Chance to get a Unique Girl when exploring the Catacombs.
		After all Unique Girls have been found, the rest will be random girls.
	
	Who gets What:
		These settings will determine the ratio of Girls to Items to Beasts that they try to come back with.
		If the Controls are true, these will determine what they try to get when you send a Girl or Gang into the catacombs.
		The numbers entered here are normalized into fractions of 100% by the game.
		Negative numbers are not allowed and all 0s will set to (100/3)% each.");
                data.Add(comment);
                element = new XElement("Catacombs");
                if (this.m_Catacombs.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Slave Market Settings:
	UniqueMarket:   Chance to get a Unique Girl from the Slave Market.
		After all Unique Girls have been found, the rest will be random girls.
	
	SlavesNewWeekly...:	The minimum and maximum number of girls in the slave market each turn.
		Absolude minimum of 0 and maximum of 20.
		If min is higher than max, they get switched.");
                data.Add(comment);
                element = new XElement("SlaveMarket");
                if (this.m_Slavemarket.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Gangs:
	MaxRecruitList limits the maximum number of recruitable gangs listed for you to hire.
		WARNING: BE CAREFUL here; the number of recruitable gangs plus the number of potential hired
			gangs must not exceed the number of names stored in HiredGangNames.txt.
		For example, with 20 names, you could have a max of 12 recruitables since you have to
			account for the possible 8 hired gangs.
	StartRandom is how many random recruitable gangs are created for you at the start of a new game.
	StartBoosted is how many stat-boosted starting gangs are also added.
	InitMemberMin and InitMemberMax indicate the number of initial gang members which are in each recruitable gang;
		a random number between Min and Max is picked.
	AddNewWeeklyMin and AddNewWeeklyMax indicate how many new random gangs are added to the recruitable
		gangs list each week; a random number between Min and Max is picked.
	ChanceRemoveUnwanted is the %chance each week that each unhired gang in the recruitable list is removed.");
                data.Add(comment);
                element = new XElement("Gangs");
                if (this.m_Gangs.Serialise(element))
                { data.Add(element); }

                //TODO : XML comment for Farm configuration data.
                data.Add(comment);
                element = new XElement("Farm");
                if (this.m_Farm.Serialise(element))
                { data.Add(element); }

                //TODO : XML comment for Debug configuration data.
                data.Add(comment);
                element = new XElement("Debug");
                if (this.m_Debug.Serialise(element))
                { data.Add(element); }

                comment = new XComment(@"
	Characters     = The location of the Characters folder
	Saves          = The location of the Saves folder
	DefaultImages  = The location of the DefaultImages folder
	Items          = The location of the Items folder
			The folders can be relative to the EXE file or an exact folder path.
				Relative folders refers to its location to the parent folder of the game
					Use  '..\Characters'  and the like.
				Absolute folders is the full path name as defined by the operating system.
					Use  'c:\WM\Characters'  and the like.
	BackupSaves    = 'true' or 'false'
			If set to true, the game will save in both the game's default
				save folder as well as the folder set here.
	PreferDefault    = 'true' or 'false'
			If set to true, the game will try to use default images
				before trying to find alternate images from the image tree.");
                data.Add(comment);
                element = new XElement("Folders");
                if (this.m_Folders.Serialise(element))
                { data.Add(element); }

                // Skip Resolution. Will be move to IHM configuration file.
                
                // Skip Items.

                // Skip Fonts. Will be move to IHM configuration file.
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Deserialise <see cref="Configuration"/> instance from <see cref="XElement"/>.
        /// </summary>
        /// <param name="data"><see cref="XElement"/> storing serialised data of this <see cref="Configuration"/> instance.</param>
        public bool Deserialise(XElement data)
        {
            XElement workingElement = Serialiser.GetElement(data, Serialiser.GetRootNameAttribut(this));
            if (workingElement == null)
            { return false; }

            try
            {
                this.m_Initial.Deserialise(data.Element("Initial"));
                this.m_IncomeFactors.Deserialise(data.Element("Income"));
                this.m_OutgoingFactors.Deserialise(data.Element("Expenses"));
                this.m_Gamble.Deserialise(data.Element("Gambling"));
                this.m_Tax.Deserialise(data.Element("Tax"));
                this.m_Pregnancy.Deserialise(data.Element("Pregnancy"));
                this.m_Prostitution.Deserialise(data.Element("Prostitution"));
                this.m_Catacombs.Deserialise(data.Element("Catacombs"));
                this.m_Slavemarket.Deserialise(data.Element("SlaveMarket"));
                this.m_Gangs.Deserialise(data.Element("Gangs"));
                this.m_Farm.Deserialise(data.Element("Farm"));
                this.m_Debug.Deserialise(data.Element("Debug"));
                this.m_Folders.Deserialise(data.Element("Folders"));

                Configuration.Catacombs.CheckCatacombsData();
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }
    }
}
