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
//  <copyright file="GangMissions.cs" company="The Pink Petal Devloment Team">
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
    using System.Xml.Serialization;

    // TODO : REFACTORING - Rename enum value to lower case UC first

    /// <summary>
    /// Goon missions.
    /// </summary>
    [Flags] // Need Flags attribut for XmlSerialisation of Enum
    public enum GangMissions
    {
        /// <summary>
        /// Guarding player businesses.
        /// </summary>
        [XmlEnum("MISS_GUARDING")]
        GUARDING = 0,
        /// <summary>
        /// Sabotaging rival business.
        /// </summary>
        SABOTAGE,
        /// <summary>
        /// Checking up on the girls while they work.
        /// </summary>
        SPYGIRLS,
        /// <summary>
        /// Looking for runaway girls.
        /// </summary>
        CAPTUREGIRL,
        /// <summary>
        /// Exthortion of local business for money in return for protection.
        /// </summary>
        EXTORTION,
        /// <summary>
        /// Go out on the streets and steal from people.
        /// </summary>
        PETYTHEFT,
        /// <summary>
        /// Go and rob local business while noone is there.
        /// </summary>
        GRANDTHEFT,
        /// <summary>
        /// Go out and kidnap homeless or lost girls.
        /// </summary>
        KIDNAPP,
        /// <summary>
        /// Men go down into the catacombs to find treasures.
        /// </summary>
        CATACOMBS,
        /// <summary>
        /// Men improve their skills.
        /// </summary>
        TRAINING,
        /// <summary>
        /// Men recuit new members.
        /// </summary>
        RECRUIT,
        /// <summary>
        /// men will do community service - `J` added for .06.02.41.
        /// </summary>
        SERVICE,
        /// <summary>
        /// Men will help break girls in the dungeon
        /// </summary>
        DUNGEON,
        /// <summary>
        /// No mission defined, need for last mission whene not storing somthing.
        /// </summary>
        NONE
    };
}
