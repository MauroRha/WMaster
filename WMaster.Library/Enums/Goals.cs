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
//  <copyright file="Goals.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Enums
{
    using System;

    // TODO : REFACTORING - Rename enum value to lower case UC first
    // TODO : REFACTORING - Rename enum Goals to CustomersGoals

    /// <summary>
    /// Customers goals
    /// </summary>
    public enum Goals
    {
        /// <summary>
        /// The customer is not sure what they want to do.
        /// </summary>
        UNDECIDED = 0,
        /// <summary>
        /// The customer wants to start a fight.
        /// </summary>
        FIGHT,
        /// <summary>
        /// They want to rape someone?
        /// </summary>
        RAPE,
        /// <summary>
        /// The customer wants to get laid.
        /// </summary>
        SEX,
        /// <summary>
        /// The customer wants to get drunk.
        /// </summary>
        GETDRUNK,
        /// <summary>
        /// The customer wants to gamble.
        /// </summary>
        GAMBLE,
        /// <summary>
        /// The customer wants to be entertained.
        /// </summary>
        ENTERTAINMENT,
        /// <summary>
        /// They want sexual entertainment.
        /// </summary>
        XXXENTERTAINMENT,
        /// <summary>
        /// They just want company and a friendly ear.
        /// </summary>
        LONELY,
        /// <summary>
        /// Their muscles hurt and want someone to work on it, (should Happy Ending be a separate job?).
        /// </summary>
        MASSAGE,
        /// <summary>
        /// They want to see someone naked.
        /// </summary>
        STRIPSHOW,
        /// <summary>
        /// They want to see something strange, nonhuman or just different.
        /// </summary>
        FREAKSHOW,
        /// <summary>
        /// They want to have sex with something strange, nonhuman or just different.
        /// </summary>
        CULTURALEXPLORER,
        /// <summary>
        /// The customer wants to do something different.
        /// </summary>
        OTHER,

        [Obsolete("The NUM_GOALS enum value of eGoals must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_GOALS
    };
}
