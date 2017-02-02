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
//  <copyright file="Skills.cs" company="The Pink Petal Devloment Team">
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

    // WARNING !! : When adding EnumSkills you must add skill name to resource manager. Code is "PREFIX_ATTRIBUTE_NAME + enum.tostring()" in Global category
    /// <summary>
    /// Editable Character skills (used for traits).
    /// <remarks><para>`J` When modifying Stats or Skills, search for "J-Change-Stats-Skills"  :  found in >> Constants.h</para></remarks>
    /// </summary>
    [Flags] // Need Flags attribut for XmlSerialisation of Enum
    public enum EnumSkills
    {
        /// <summary>
        /// Anal performing skill.
        /// </summary>
        [XmlEnum("SKILL_ANAL")]
        Anal = /*           */ 0x00001,
        Magic = /*          */ 0x00010,
        BDSM = /*           */ 0x00011,
        NormalSex = /*      */ 0x00100,
        Beastiality = /*    */ 0x00101,
        GroupSex = /*       */ 0x00110,
        Lesbian = /*        */ 0x00111,
        Service = /*        */ 0x01000,
        Striptease = /*     */ 0x01001,
        Combat = /*         */ 0x01010,
        OralSex = /*        */ 0x01011,
        TittySex = /*       */ 0x01100,
        Medicine = /*       */ 0x01101,
        Performance = /*    */ 0x01110,
        HandJob = /*        */ 0x01111,
        Crafting = /*       */ 0x10000,
        Herbalism = /*      */ 0x10001,
        Farming = /*        */ 0x10010,
        Brewing = /*        */ 0x10011,
        AnimalHandling = /* */ 0x10100,
        FootJob = /*        */ 0x10101,
        Cooking = /*        */ 0x10110,

        /// <summary>
        /// 1 more than the last skill.
        /// </summary>
        [Obsolete("The NUM_SKILLS enum value of eSkills must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_SKILLS, // 
        //const unsigned int SKILL_MAST		= ;
    }
}
