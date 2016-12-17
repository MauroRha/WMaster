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
    using WMaster.Tool;

    public class NameList
    {
        /// <summary>
        /// List of Names
        /// </summary>
        private List<string> _names = new List<string>();

        public NameList()
        { }

        public NameList(string fileName)
        {
            this.Load(fileName);
        }

        public string Random()
        {
            int size = this._names.Count();

            if (size.Equals(0))
            {
                WMaster.WMLog.Trace("No names in cNameList : Returning no name", WMLog.TraceLog.ERROR);
                return "";
            }

            return this._names[WMRandom.Next(size)];
        }

        public void Load(string fileName, bool removeFirstLine = true)
        {
            // TODO : CROSPLATFORM - translate I/O to OS dependent project
            this._names.Clear();
            this._names.AddRange(File.ReadAllLines(fileName));

            if (removeFirstLine && !this._names.Count().Equals(0))
            { this._names.RemoveAt(0); }

            if (this._names.Count().Equals(0))
            {
                WMaster.WMLog.Write(String.Format("Error: zero names found in file '{0}'", fileName));
                return;
            }
        }
    }
}
