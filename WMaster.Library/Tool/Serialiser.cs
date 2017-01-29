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
//  <copyright file="Serialiser.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-30</datecreated>
//  <summary>
// Static wrapper to <see cref="WMaster.Manager.GameEngine"/> providing easy access to it's functionality.
// <remarks><para>All methodes and properties are linked to same member of <see cref="WMaster.Manager.GameEngine"/> unique instance.</para></remarks>
//  </summary>
//  <remarks>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster.Tool
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Static class providing serialisation function for store or load save game, config file or external items / girls files.
    /// </summary>
    public static class Serialiser
    {
        #region Del
        ///// <summary>
        ///// Get XML serialisation using Xml Serialisation by returning XElement for Linq to XML use.
        ///// </summary>
        ///// <typeparam name="TType">Type of object to serialise.</typeparam>
        ///// <param name="entity">Object to serialise.</param>
        ///// <returns>An <see cref="XElement"/> containing serialisation of <paramref name="entity"/>.</returns>
        //public static XElement Serialise<TType>(TType entity, string entityName = null)
        //    where TType : class
        //{
        //    if (entity == null)
        //    {
        //        WMLog.Trace(string.Format("Calling serialisation when {0} entity is null!", typeof(TType)), WMLog.TraceLog.ERROR);
        //        return null;
        //    }

        //    Type entityType = typeof(TType);
        //    if (!entityType.IsSerializable)
        //    {
        //        WMLog.Trace(string.Format("Trying to serialize {0} but it wasn't serialisable!", typeof(TType)), WMLog.TraceLog.ERROR);
        //        return null;
        //    }

        //    if (entityName == null)
        //    {
        //        XmlRootAttribute root = typeof(TType).GetCustomAttributes(typeof(XmlRootAttribute), false).FirstOrDefault() as XmlRootAttribute;
        //        if (root != null)
        //        { entityName = root.ElementName; }
        //        else
        //        { entityName = typeof(TType).Name; }
        //    }
        //    XElement xe = new XElement(entityName);

        //    foreach (PropertyInfo pi in entityType.GetProperties())
        //    {
        //        if (pi.GetCustomAttribute<XmlIgnoreAttribute>() != null)
        //        { continue; }

        //        if (
        //    }
        //    XDocument doc = new XDocument();
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TType));

        //    using (XmlWriter writer = doc.CreateWriter())
        //    {
        //        xmlSerializer.Serialize(writer, entity);
        //    }

        //    if (doc.Root == null)
        //    {
        //        WMLog.Trace(string.Format("Unable to serialise {0} entity!", typeof(TType)), WMLog.TraceLog.ERROR);
        //        return null;
        //    }

        //    DelDotNetAttrib(doc.Root);

        //    return doc.Root;
        //}

        ///// <summary>
        ///// Provide serialisation of object inside <see cref="XmlWriter"/> as XmlEntity uqing XmlRootAttribute or type as entity name.
        ///// </summary>
        ///// <param name="serialisationWriter"><see cref="XmlWriter"/> of serialisation.</param>
        ///// <param name="entityToSerialise">Object to serialise.</param>
        //internal static void SerialiseSubEntity(XmlWriter serialisationWriter, object entityToSerialise)
        //{
        //    if (entityToSerialise == null)
        //    { return; }

        //    Type entityType = entityToSerialise.GetType();
        //    if (!entityType.IsSerializable)
        //    { return; }

        //    XmlRootAttribute rootAttribut = entityType.GetCustomAttributes(typeof(XmlRootAttribute), false).FirstOrDefault() as XmlRootAttribute;
        //    if (rootAttribut == null)
        //    { rootAttribut = new XmlRootAttribute(entityType.Name); }

        //    XmlAttributeOverrides overrides = new XmlAttributeOverrides();
        //    XmlAttributes attr = new XmlAttributes();
        //    attr.XmlRoot = rootAttribut;
        //    overrides.Add(entityType, attr);
        //    XmlSerializer xs = new XmlSerializer(entityType, overrides);
        //    xs.Serialize(serialisationWriter, entityToSerialise);
        //}

        ///// <summary>
        ///// Remove all .net xml attribut add in XmlSerialisation.
        ///// </summary>
        ///// <param name="element">Element to pass throw to remove attribut.</param>
        //private static void DelDotNetAttrib(XElement element)
        //{
        //    foreach (XElement elem in element.Elements())
        //    {
        //        DelDotNetAttrib(elem);
        //    }

        //    // Suppressing .Net XML serialisation specification.
        //    element.Attributes()
        //        .Where(a => a.Name.NamespaceName.Equals("http://www.w3.org/2000/xmlns/"))
        //        .Remove();
        //}

        ///// <summary>
        ///// Get <typeparamref name="TType"/> object from <see cref="XElement"/> <paramref name="xmlElement"/> by Xml Deserialisation.
        ///// </summary>
        ///// <typeparam name="TType">Type of object to deserialise.</typeparam>
        ///// <param name="xmlElement"><see cref="XElement"/> containing serialisation of <typeparamref name="TType"/> entity.</param>
        ///// <returns>Deserialised <typeparamref name="TType"/> entity.</returns>
        //public static TType DeSerialise<TType>(XElement xmlElement)
        //    where TType : class
        //{
        //    if (xmlElement == null)
        //    {
        //        WMLog.Trace(string.Format("Calling deserialisation of {0} entity when xmlElement is null!", typeof(TType)), WMLog.TraceLog.ERROR);
        //        return null;
        //    }

        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TType));
        //    TType returnValue;
        //    using (XmlReader reader = xmlElement.CreateReader())
        //    {
        //        returnValue = xmlSerializer.Deserialize(reader) as TType;
        //    }

        //    if (returnValue == null)
        //    {
        //        WMLog.Trace(string.Format("Unable to deserialise {0} entity!", typeof(TType)), WMLog.TraceLog.ERROR);
        //        return null;
        //    }

        //    return returnValue;
        //}
        #endregion
        private static CultureInfo m_Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        /// <summary>
        /// CurrentCulture defined by player or OS.
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            get { return Serialiser.m_Culture; }
        }

        /// <summary>
        /// Get the <see cref="XmlRootAttribute"/> 
        /// </summary>
        /// <param name="source">Class to fing RootAttribut</param>
        /// <returns>Root element name for class of <see cref="Type"/> <paramref name="classType"/></returns>
        public static string GetRootNameAttribut(object source)
        {
            if (source == null)
            { return null; }

            return Serialiser.GetRootNameAttribut(source.GetType());
        }

        /// <summary>
        /// Get the <see cref="XmlRootAttribute"/> 
        /// </summary>
        /// <param name="classType">Type of class to fing RootAttribut</param>
        /// <returns>Root element name for class of <see cref="Type"/> <paramref name="classType"/></returns>
        public static string GetRootNameAttribut(Type classType)
        {
            if (classType == null)
            { return null; }

            XmlRootAttribute root = classType.GetCustomAttributes(typeof(XmlRootAttribute), false).FirstOrDefault() as XmlRootAttribute;
            if (root == null)
            {
                WMLog.Trace(string.Format("No XmlRootAttribute in class of type \"{0}\"!", classType), WMLog.TraceLog.ERROR);
                return null;
            }

            return root.ElementName;
        }

        /// <summary>
        /// Get first <see cref="XElement"/> with name <paramref name="elementName"/> from <see cref="XElement"/> <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Where to find <see cref="XElement"/>.</param>
        /// <param name="elementName">Name of <see cref="XElement"/> to find.</param>
        /// <returns><see cref="XElement"/> with name <paramref name="elementName"/> or null if it dosen't exists.</returns>
        public static XElement GetElement(XElement source, string elementName)
        {
            if (source == null)
            { return null; }
            if (string.IsNullOrWhiteSpace(elementName))
            { return null; }

            try
            {
                if (source.Name.LocalName.Equals(elementName))
                { return source; }

                return source.Element(elementName);
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return null;
            }
        }

        /// <summary>
        /// Set InvariantCulture as default culture. Neet to save data like date in standard mode.
        /// </summary>
        public static void SetInvarientCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Restore CurrentCulture to player set.
        /// </summary>
        public static void RestoreCurrentCulture()
        {
            Thread.CurrentThread.CurrentCulture = Serialiser.CurrentCulture;
        }

        /// <summary>
        /// Try to convert <paramref name="source"/> string data (from XML file for example) into <paramref name="destination"/> data.
        /// </summary>
        /// <param name="source">String representation data to convert.</param>
        /// <param name="destination">Reference to destination to set with converted data.</param>
        /// <returns><b>True</b> if convertion was done.</returns>
        public static bool SetValue(string source, ref int destination)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            int convert;
            try
            {
                if (int.TryParse(source, out convert)) { destination = convert; }
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Try to convert <paramref name="source"/> string data (from XML file for example) into <paramref name="destination"/> data.
        /// </summary>
        /// <param name="source">String representation data to convert.</param>
        /// <param name="destination">Reference to destination to set with converted data.</param>
        /// <returns><b>True</b> if convertion was done.</returns>
        public static bool SetValue(string source, ref string destination)
        {
            if (source == null) { return false; }

            try
            {
                destination = source;
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Try to convert <paramref name="source"/> string data (from XML file for example) into <paramref name="destination"/> data.
        /// </summary>
        /// <param name="source">String representation data to convert.</param>
        /// <param name="destination">Reference to destination to set with converted data.</param>
        /// <returns><b>True</b> if convertion was done.</returns>
        public static bool SetValue(string source, ref bool destination)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            bool convert;
            try
            {
                if (bool.TryParse(source, out convert)) { destination = convert; }
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Try to convert <paramref name="source"/> string data (from XML file for example) into <paramref name="destination"/> data.
        /// </summary>
        /// <param name="source">String representation data to convert.</param>
        /// <param name="destination">Reference to destination to set with converted data.</param>
        /// <returns><b>True</b> if convertion was done.</returns>
        public static bool SetValue(string source, ref double destination)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            double convert;
            try
            {
                if (double.TryParse(source, out convert)) { destination = convert; }
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        /// <summary>
        /// Try to convert <paramref name="source"/> string data (from XML file for example) into <paramref name="destination"/> data.
        /// </summary>
        /// <param name="source">String representation data to convert.</param>
        /// <param name="destination">Reference to destination to set with converted data.</param>
        /// <returns><b>True</b> if convertion was done.</returns>
        public static bool SetPercentage(string source, ref double destination)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            double convert;
            try
            {
                if (double.TryParse(source.Replace("%", string.Empty), out convert)) { destination = convert; }
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
