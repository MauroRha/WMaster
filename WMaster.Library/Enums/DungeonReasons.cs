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
//  <copyright file="DungeonReasons.cs" company="The Pink Petal Devloment Team">
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

    /// <summary>
    /// Reasons for keeping them in the dungeon
    /// </summary>
    public enum DungeonReasons
    {
        /// <summary>
        /// Released from the dungeon on next update.
        /// </summary>
        Release = 0,
        /// <summary>
        /// A customer that failed to pay or provide adiquate compensation.
        /// </summary>
        Custnoplay,
        /// <summary>
        /// A new girl that was captured.
        /// </summary>
        GIRLCAPTURED,
        /// <summary>
        /// A new girl taken against her will.
        /// </summary>
        GIRLKIDNAPPED,
        /// <summary>
        /// A customer that was found hurting a girl.
        /// </summary>
        CUSTBEATGIRL,
        /// <summary>
        /// A customer that was found to be a spy for a rival.
        /// </summary>
        CUSTSPY,
        /// <summary>
        /// A captured rival.
        /// </summary>
        RIVAL,
        /// <summary>
        /// A girl placed here on a whim.
        /// </summary>
        GIRLWHIM,
        /// <summary>
        /// A girl that was placed here after being found stealing extra.
        /// </summary>
        GIRLSTEAL,
        /// <summary>
        /// This person has died and will be removed next turn.
        /// </summary>
        DEAD,
        /// <summary>
        /// Girl ran away but was recaptured.
        /// </summary>
        GIRLRUNAWAY,
        /// <summary>
        /// A newly brought slave.
        /// </summary>
        NEWSLAVE,
        /// <summary>
        /// A new girl who just joined you.
        /// </summary>
        NEWGIRL,
        /// <summary>
        /// A girl child just aged up.
        /// </summary>
        KID,
        /// <summary>
        /// A new girl who just joined you from the arena.
        /// </summary>
        NEWARENA,
        /// <summary>
        /// A new girl who was just recruited.
        /// </summary>
        RECRUITED,
    };
}
