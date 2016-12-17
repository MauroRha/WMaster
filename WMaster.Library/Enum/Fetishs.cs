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
//  <copyright file="Fetishs.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Enum
{
    using System;

    // TODO : REFACTORING - Rename enum value to lower case UC first

    /// <summary>
    /// Customer fetishes (important that they are ordered from normal to weird).
    /// </summary>
    public enum Fetishs
    {
        /// <summary>
        /// Will like and try any form of sex (a nympho basically)
        /// </summary>
        TRYANYTHING = 0,
        /// <summary>
        /// Wants a particular girl.
        /// </summary>
        SPECIFICGIRL,
        /// <summary>
        /// Likes girls with big boobs.
        /// </summary>
        BIGBOOBS,
        /// <summary>
        /// Likes girls with lots of sex appeal.
        /// </summary>
        SEXY,
        /// <summary>
        /// Likes girls that are cute.
        /// </summary>
        CUTEGIRLS,
        /// <summary>
        /// Likes girls with good figures.
        /// </summary>
        FIGURE,
        /// <summary>
        /// Likes lolitas.
        /// </summary>
        LOLITA,
        /// <summary>
        /// Likes girls with good arses.
        /// </summary>
        ARSE,
        /// <summary>
        /// Likes cool girls, may chat with them a little.
        /// </summary>
        COOLGIRLS,
        /// <summary>
        /// Likes girls with class.
        /// </summary>
        ELEGANT,
        /// <summary>
        /// Likes nerds or clumsy girls.
        /// </summary>
        NERDYGIRLS,
        /// <summary>
        /// Likes girls with small boobs.
        /// </summary>
        SMALLBOOBS,
        /// <summary>
        /// Likes girls with a bit of danger.
        /// </summary>
        DANGEROUSGIRLS,
        /// <summary>
        /// Likes non human girls.
        /// </summary>
        NONHUMAN,
        /// <summary>
        /// Likes freaky girls.
        /// </summary>
        FREAKYGIRLS,
        /// <summary>
        /// Likes girls with dicks.
        /// </summary>
        FUTAGIRLS,
        /// <summary>
        /// Likes tall girls.
        /// </summary>
        TALLGIRLS,
        /// <summary>
        /// Likes short girls.
        /// </summary>
        SHORTGIRLS,
        /// <summary>
        /// Likes fat girls.
        /// </summary>
        FATGIRLS,

        [Obsolete("The NUM_FETISH enum value of eFetishs must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_FETISH
    };
}
