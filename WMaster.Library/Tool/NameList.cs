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
//  <copyright file="NameList.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Tool
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represent a list of name who can randomize a name.
    /// </summary>
    public class NameList
    {
        /// <summary>
        /// List of Names.
        /// </summary>
        private List<string> _names = new List<string>();

        /// <summary>
        /// Initialise an instance of <see cref="NameList"/> with empty list of name.
        /// </summary>
        public NameList()
        { }

        /// <summary>
        /// Initialise an instance of <see cref="DoubleNameList"/>. Fill name list with <paramref name="firstNameFile"/> resources content
        /// </summary>
        /// <param name="fileName">Resource containing name list.</param>
        public NameList(string fileName)
        {
            this.Load(fileName);
        }

        /// <summary>
        /// Fill name list with <paramref name="resourceCode"/> resources content.
        /// </summary>
        /// <param name="resourceCode">Resource containing the list of name.</param>
        public void Load(string resourceCode)
        {
            this._names = GameEngine.Game.Resources.GetResourcesLines(resourceCode);

            //bool removeFirstLine = true;
            //// TODO : CROSPLATFORM - translate I/O to OS dependent project
            //this._names.Clear();
            //this._names.AddRange(File.ReadAllLines(fileName));

            //if (removeFirstLine && !this._names.Count().Equals(0))
            //{ this._names.RemoveAt(0); }

            if (this._names.Count().Equals(0))
            {
                WMLog.Trace(String.Format("Error: zero names found in resource '{0}'", resourceCode), WMLog.TraceLog.ERROR);
                return;
            }
        }

        /// <summary>
        /// Generate a random name.
        /// </summary>
        /// <returns>Random name.</returns>
        public string Random()
        {
            int size = this._names.Count();

            if (size.Equals(0))
            {
                WMaster.WMLog.Trace("No names in cNameList : Returning no name", WMLog.TraceLog.ERROR);
                return "";
            }

            return this._names[WMRand.Random(size)];
        }

        /// <summary>
        /// Exclude all name from <paramref name="excludeList"/> to list.
        /// </summary>
        /// <param name="excludeList">List of name to exclude.</param>
        public void Exclude(IEnumerable<string> excludeList)
        {
            if (excludeList == null)
            { return; }

            foreach (string item in excludeList)
            {
                this._names.Remove(item);
            }
        }
    }
}
