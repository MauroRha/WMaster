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
//  <copyright file="Stats.cs" company="The Pink Petal Devloment Team">
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
namespace WMaster.Concept.Attributs
{
    using System;
    using System.Xml.Serialization;

    // TODO : REFACTORING - Rename enum value to lower case UC first

    // WARNING !! : When adding EnumStats you must add stat name to resource manager. Code is "PREFIX_ATTRIBUTE_NAME + enum.tostring()" in Global category
    /// <summary>
    /// Editable Character Stats (used for traits).
    /// <remarks><para>`J` When modifying Stats or Skills, search for "J-Change-Stats-Skills"  :  found in >> Constants.h</para></remarks>
    /// </summary>
    [Flags] // Need Flags attribut for XmlSerialisation of Enum
    public enum EnumStats
    {
        /// <summary>
        /// Anal performing skill.
        /// </summary>
        [XmlEnum("STAT_CHARISMA")]
        Charisma = /*     */ 0x00001,
        Happiness = /*    */ 0x00010,
        Libido = /*       */ 0x00011,
        Constitution = /* */ 0x00100,
        Intelligence = /* */ 0x00101,
        Confidence = /*   */ 0x00110,
        Mana = /*         */ 0x00111,
        Agility = /*      */ 0x01000,
        Fame = /*         */ 0x01001,
        Level = /*        */ 0x01010,
        AskPrice = /*     */ 0x01011,
        House = /*        */ 0x01100,
        Exp = /*          */ 0x01101,
        Age = /*          */ 0x01110,
        Obedience = /*    */ 0x01111,
        Spirit = /*       */ 0x10000,
        Beauty = /*       */ 0x10001,
        Tiredness = /*    */ 0x10010,
        Health = /*       */ 0x10011,
        PCFear = /*       */ 0x10100,
        PCLove = /*       */ 0x10101,
        PCHate = /*       */ 0x10110,
        Morality = /*     */ 0x10111,
        Refinement = /*   */ 0x11000,
        Dignity = /*      */ 0x11001,
        Lactation = /*    */ 0x11010,
        Strength = /*     */ 0x11011,
        /// <summary>
        /// Will be used for when a girl has a bf/gf to do different events
        /// </summary>
        NPCLove = /*      */ 0x11100,
        /// <summary>
        /// SIN: Life is hard here...
        /// </summary>
        Sanity = /*       */ 0x11101,

        /// <summary>
        /// 1 more than the last stat.
        /// </summary>
        [Obsolete("The NUM_STATS enum value of eStats must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_STATS
    }
}
