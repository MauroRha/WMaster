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
//  <copyright file="FreeFireSellDump.cs" company="The Pink Petal Devloment Team">
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

    // TODO : REFACTORING - Rename FFSD enum to FreeFireSellDump
    // TODO : REFACTORING - Rename enum value to lower case UC first

    /// <summary>
    /// `J` Free Fire Sell Dump
    /// </summary>
    public enum FFSD
    {
        keep = 0,
        fire,
        free,
        sell,
        dump,
        /// <summary>
        /// Fire & Dump.
        /// </summary>
        fidu,
        /// <summary>
        /// Fire & Sell.
        /// </summary>
        fise,
        /// <summary>
        /// Fire, Sell & Dump.
        /// </summary>
        fisd,
        /// <summary>
        /// Free & Dump.
        /// </summary>
        frdu,
        /// <summary>
        /// Sell & Dump.
        /// </summary>
        sedu,
        /// <summary>
        /// Proper funeral.
        /// </summary>
        dump1,
        /// <summary>
        /// Dump in shollow unmarked grave.
        /// </summary>
        dump2,
        /// <summary>
        /// Dump on side of the road.
        /// </summary>
        dump3,
        /// <summary>
        /// Sell dead to highest bidder.
        /// </summary>
        dump4,
        /// <summary>
        /// Have your slave girls dispose of the dead then free them.
        /// </summary>
        frdu1,
        /// <summary>
        /// Have your slave girls dispose of the dead then stay slaves.
        /// </summary>
        frdu2,
        /// <summary>
        /// Sell all the girls, living and dead.
        /// </summary>
        sedu1,
        /// <summary>
        /// Throw a freedom party.
        /// </summary>
        //free
        //FFSD_,
    }
}
