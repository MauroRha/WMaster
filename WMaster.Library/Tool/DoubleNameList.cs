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
//  <copyright file="DoubleNameList.cs" company="The Pink Petal Devloment Team">
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

    /// <summary>
    /// Provide facilitie to load and create name from two sources, first and last name.
    /// </summary>
    public class DoubleNameList
    {
        /// <summary>
        /// List of firstname.
        /// </summary>
        private NameList _firstName = new NameList();
        /// <summary>
        /// List of lastname.
        /// </summary>
        private NameList _lastName = new NameList();

        /// <summary>
        /// Initialise an instance of <see cref="DoubleNameList"/> with empty name list
        /// </summary>
        public DoubleNameList()
        {
            this._firstName = new NameList();
            this._lastName = new NameList();
        }

        /// <summary>
        /// Initialise an instance of <see cref="DoubleNameList"/>. Fill first name list with <paramref name="firstNameFile"/> resources content and last name list with <paramref name="lastNameFile"/> resources content.
        /// </summary>
        /// <param name="firstNameFile">Resource containing first name list.</param>
        /// <param name="lastNameFile">Resource containing last name list.</param>
        public DoubleNameList(string firstNameFile, string lastNameFile)
        {
            this._firstName = new NameList(firstNameFile);
            this._lastName = new NameList(lastNameFile);
        }

        /// <summary>
        /// Fill first name mist with <paramref name="firstNameFile"/> resources content and last name list with <paramref name="lastNameFile"/> resources content.
        /// </summary>
        /// <param name="firstNameFile">Resource containing the list of first name.</param>
        /// <param name="lastNameFile">Resource containing the list of last name.</param>
        public void Load(string firstNameFile, string lastNameFile)
        {
            this._lastName.Load(firstNameFile);
            this._firstName.Load(lastNameFile);
        }

        /// <summary>
        /// Generate a random couple firstname / lastname.
        /// </summary>
        /// <returns>Random couple firstname / lastname.</returns>
        public string Random()
        {
            return String.Format("{0} {1}", _firstName.Random(), _lastName.Random());
        }
    }
}
