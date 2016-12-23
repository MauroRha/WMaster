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
    /// Actions girls can disobey against
    /// <remarks><para>`J` When modifying Action types, search for "J-Change-Action-Types"  :  found in >> Constants.h</para></remarks>
    /// </summary>
    public enum ActionTypes
    {
        Combat = 0,
        Sex,
        Workescort,
        Workcleaning,
        Workmatron,
        Workbar,
        Workhall,
        Workshow,
        Worksecurity,
        Workadvertising,
        Worktorturer,
        Workcaring,
        Workdoctor,
        Workmovie,
        Workcustserv,
        Workcentre,
        Workclub,
        Workharem,
        Workrecruit,
        Worknurse,
        Workmechanic,
        Workcounselor,
        Workmusic,
        Workstrip,
        Workmilk,
        Workmasseuse,
        Workfarm,
        Worktraining,
        Workrehab,
        Workmakepotions,
        Workmakeitems,
        Workcooking,
        Worktherapy,
        Workhousepet,

        // General must be the last sction type
        General,

        [Obsolete("The NUM_ACTIONTYPES enum value of eActionTypes must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_ACTIONTYPES
    };
}