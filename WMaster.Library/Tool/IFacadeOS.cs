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
//  <copyright file="IFacadeOS.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-20</datecreated>
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Provide a Facade to access to all OS specific functionality.
    /// </summary>
    public interface IFacadeOS
    {
        /// <summary>
        /// Get XML stream of game configuration from OS source.
        /// </summary>
        /// <param name="filename">Source where was store XML configuration data.</param>
        /// <returns><see cref="XElement"/> of configuration data.</returns>
        XElement GetConfiguration(string filename = "config.xml");

        /// <summary>
        /// Save configuration data from <see cref="XElement"/> to mass storage.
        /// </summary>
        /// <param name="configurationData"><see cref="XElement"/> of configuration data.</param>
        /// <param name="filename">Destination to store XML configuration data.</param>
        /// <returns><b>True</b> whene saving is ok.</returns>
        bool SetConfiguration(XElement configurationData, string filename = "config.xml");

        /// <summary>
        /// Get the OS specific resource manager.
        /// </summary>
        /// <returns></returns>
        IResourceManager GetResourceManager();
    }
}
