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
//  <copyright file="ItemType.cs" company="The Pink Petal Devloment Team">
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

    public enum ItemType
    {
        /// <summary>
        /// Worn on fingers (max 8).
        /// </summary>
        RING = 1,
        /// <summary>
        /// Worn on body, (max 1).
        /// </summary>
        DRESS,
        /// <summary>
        /// Worn on feet, (max 1) often unequipped when going into combat
        /// </summary>
        SHOES,
        /// <summary>
        /// Eaten, single use.
        /// </summary>
        FOOD,
        /// <summary>
        /// Worn on neck, (max 1).
        /// </summary>
        NECKLACE,
        /// <summary>
        /// Equipped on body, often unequipped outside of combat, (max 2).
        /// </summary>
        WEAPON,
        /// <summary>
        /// Worn on face, single use.
        /// </summary>
        MAKEUP,
        /// <summary>
        /// Worn on body over dresses, often unequipped outside of combat, (max 1).
        /// </summary>
        ARMOR,
        /// <summary>
        /// These items don't usually do anythinig just random stuff girls might buy. The ones that do, cause a constant effect without having to be equiped.
        /// </summary>
        MISC,
        /// <summary>
        /// Worn around arms, (max 2)
        /// </summary>
        ARMBAND,
        /// <summary>
        /// Small weapon which can be hidden on body, (max 2).
        /// </summary>
        SMWEAPON,
        /// <summary>
        /// CRAZY added this - underwear (max 1).
        /// </summary>
        UNDERWEAR,
        /// <summary>
        /// CRAZY added this - Noncombat worn on the head (max 1).
        /// </summary>
        HAT,
        /// <summary>
        /// CRAZY added this	- Combat worn on the head (max 1).
        /// </summary>
        HELMET,
        /// <summary>
        /// CRAZY added this	- Glasses (max 1).
        /// </summary>
        GLASSES,
        /// <summary>
        /// CRAZY added this - Swimsuit (max 1 in use but can have as many as they want).
        /// </summary>
        SWIMSUIT,
        /// <summary>
        /// `J`   added this - Combat Shoes (max 1) often unequipped outside of combat.
        /// </summary>
        COMBATSHOES,
        /// <summary>
        /// `J`   added this - Shields (max 1) often unequipped outside of combat.
        /// </summary>
        SHIELD
    };
    //const unsigned int INVLEGS = ;		//CRAZY added this
}
