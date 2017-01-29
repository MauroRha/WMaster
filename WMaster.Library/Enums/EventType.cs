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
//  <copyright file="Const.cs" company="The Pink Petal Devloment Team">
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

    /// <summary>
    /// Event type
    /// <remarks><para>`J` When modifying Action types, search for "J-Change-Action-Types"  :  found in >> Constants.h</para></remarks>
    /// </summary>
    [Flags]
    public enum EventType
    {
        None /*       */ = 0x0000,
        DayShift /*   */ = 0x0001,
        NightShift /* */ = 0x0010,
        Warning /*    */ = 0x0011,
        Danger /*     */ = 0x0100,
        GoodNews /*   */ = 0x0101,
        Summary /*    */ = 0x0110,
        Dungeon /*    */ = 0x0111, // For torturer reports
        Matron /*     */ = 0x1000, // For Matron reports
        Gang /*       */ = 0x1001,
        Brothel /*    */ = 0x1010,
        NoWork /*     */ = 0x1011,
        BackToWork /* */ = 0x1100,
        LevelUp /*    */ = 0x1101, // `J` added
        Debug /*      */ = 0x1110
    };
}
