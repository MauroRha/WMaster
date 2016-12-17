﻿/*
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
//  <copyright file="Objectives.cs" company="The Pink Petal Devloment Team">
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
    /// Random objectives (well kinda random, they appear to guide the player for the win)
    /// </summary>
    public enum Objectives
    {
        REACHGOLDTARGET = 0,
        GETNEXTBROTHEL,
        LAUNCHSUCCESSFULATTACK,
        HAVEXGOONS,
        STEALXAMOUNTOFGOLD,
        CAPTUREXCATACOMBGIRLS,
        HAVEXMONSTERGIRLS,
        KIDNAPXGIRLS,
        EXTORTXNEWBUSINESS,
        HAVEXAMOUNTOFGIRLS,

        [Obsolete("The NUMJOBTYPES enum value of eJobFilter must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_OBJECTIVES
    };
}
