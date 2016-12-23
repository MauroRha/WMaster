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
namespace WMaster
{
    using System;
    using WMaster.Enums;

    // TODO : REFACTORING - Transfert constants to specific emplacement for ex : MAXNUM_GIRL_INVENTORY to girl class

    /// <summary>
    /// Application's constant contener
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Obtain the major version number
        /// <remarks>Old name = g_MajorVersion</remarks>
        /// </summary>
        public static int MajorVersion
        {
            get
            { return typeof(WMaster.Constants).Assembly.GetName().Version.Major; }
        }

        /// <summary>
        /// Obtain the minor version number
        /// <remarks>Old name = g_MinorVersionA</remarks>
        /// </summary>
        public static int MinorVersionA
        {
            get
            { return typeof(WMaster.Constants).Assembly.GetName().Version.Minor; }
        }

        /// <summary>
        /// Obtain the minor revision version number
        /// <remarks>Old name = g_MinorVersionB</remarks>
        /// </summary>
        public static int MinorVersionB
        {
            get
            { return typeof(WMaster.Constants).Assembly.GetName().Version.MinorRevision; }
        }

        /// <summary>
        /// Obtain the major revision version number
        /// <remarks>Old name = g_StableVersion</remarks>
        /// </summary>
        public static int StableVersion
        {
            get
            { return typeof(WMaster.Constants).Assembly.GetName().Version.MajorRevision; }
        }

        // the game flags
        public const int FLAG_CUSTNOPAY = 0;
        public const int FLAG_DUNGEONGIRLDIE = 1;
        public const int FLAG_DUNGEONCUSTDIE = 2;
        public const int FLAG_CUSTGAMBCHEAT = 3;
        public const int FLAG_RIVALLOSE = 4;

        #region Town Constants
        /// <summary>
        /// The amount of businesses in the town
        /// </summary>
        public const int TOWN_NUMBUSINESSES = 250;

        /// <summary>
        /// How much the authorities in the town are paid by the govenment
        /// </summary>
        public const int TOWN_OFFICIALSWAGES = 10;
        #endregion
        // Incomes
        public const int INCOME_BUSINESS = 10;

        // Item types
        /// <summary>
        /// Number of items that the shop may hold at one time
        /// </summary>
        public const int NUM_SHOPITEMS = 40;

        public static readonly int PREG_OFFSET = (int)ImageTypes.PREGNANT + 1;


        public const int NUM_GIRLFLAGS = 30;
        /// <summary>
        /// Maximum number of traits a girl can have.
        /// </summary>
        public const int MAXNUM_TRAITS = 60;
        /// <summary>
        /// Maximum number of items in inventory.
        /// </summary>
        public const int MAXNUM_INVENTORY = 3000;
        /// <summary>
        /// Maximum number of items a Girl can have in inventory.
        /// </summary>
        public const int MAXNUM_GIRL_INVENTORY = 40;
        /// <summary>
        /// Maximum number of items a Rival can have in inventory.
        /// </summary>
        public const int MAXNUM_RIVAL_INVENTORY = 40;

        /// <summary>
        /// how tall (in pixels) each list item is.
        /// </summary>
        [Obsolete("IHM value must move to IHM project", true)]
        public const int LISTBOX_ITEMHEIGHT = 20;
        /// <summary>
        /// how many columns are allowed.
        /// </summary>
        [Obsolete("IHM value must move to IHM project", true)]
        public const uint LISTBOX_COLUMNS = 25;

        // Listbox Constants moved from cListBox.h
        [Obsolete("IHM value must move to IHM project", false)]
        public const int COLOR_BLUE = 0;
        [Obsolete("IHM value must move to IHM project", false)]
        public const int COLOR_RED = 1;
        [Obsolete("IHM value must move to IHM project", false)]
        public const int COLOR_DARKBLUE = 2;
        [Obsolete("IHM value must move to IHM project", false)]
        public const int COLOR_GREEN = 3;
        [Obsolete("IHM value must move to IHM project", false)]
        public const int COLOR_YELLOW = 4; // `J` added

        // Constants determining which screen is currently showing. This will help with hotkeys and help menu. --PP
        // The variable that uses this constant is int g_CurrentScreen;
        public const int SCREEN_BROTHEL = 0;
        public const int SCREEN_TURNSUMMARY = 1;
        public const int SCREEN_GIRLMANAGEMENT = 2;
        public const int SCREEN_GIRLDETAILS = 3;
        public const int SCREEN_INVENTORY = 4;
        public const int SCREEN_GALLERY = 5;
        public const int SCREEN_TRANSFERGIRLS = 6;
        public const int SCREEN_GANGMANAGEMENT = 7;
        public const int SCREEN_BROTHELMANAGEMENT = 8;
        public const int SCREEN_DUNGEON = 9;
        public const int SCREEN_TOWN = 10;
        public const int SCREEN_MAYOR = 11;
        public const int SCREEN_BANK = 12;
        public const int SCREEN_JAIL = 13;
        public const int SCREEN_HOUSE = 14;
        public const int SCREEN_CLINIC = 15;
        public const int SCREEN_ARENA = 16;
        public const int SCREEN_TRYOUTS = 17;
        public const int SCREEN_CENTRE = 18;
        public const int SCREEN_STUDIO = 19;
        public const int SCREEN_CREATEMOVIE = 20;
        public const int SCREEN_BUILDINGMANAGEMENT = 21;
        public const int SCREEN_MAINMENU = 22;
        public const int SCREEN_SLAVEMARKET = 23;
        public const int SCREEN_PLAYERHOUSE = 24;
        public const int SCREEN_GALLERY2 = 25;
        public const int SCREEN_GETINPUT = 26;
        public const int SCREEN_PROPERTYMANAGEMENT = 27; // `J` added for managing all properties on 1 page
        public const int SCREEN_FARM = 28;
        public const int SCREEN_NEWGAME = 29;
        public const int SCREEN_PREPARING = 29;
        public const int SCREEN_SETTINGS = 30; // `J` added

        // The following constants are used with g_CurrBrothel to determine if we are currently working with a brothel or a new building. --PP
        public const int BUILDING_BROTHEL = 0;
        public const int BUILDING_STUDIO = 10;
        public const int BUILDING_CLINIC = 20;
        public const int BUILDING_ARENA = 30;
        public const int BUILDING_CENTRE = 40;
        public const int BUILDING_HOUSE = 50;
        public const int BUILDING_FARM = 60;
        /// <summary>
        /// This shows there was an error somehow, looking for a building that does not exist.
        /// </summary>
        public const int BUILDING_ERROR = 70;

        // The following constants are used with counting child types for girls.
        public const int CHILD00_TOTAL_BIRTHS = 0;
        public const int CHILD01_ALL_BEASTS = 1;
        public const int CHILD02_ALL_GIRLS = 2;
        public const int CHILD03_ALL_BOYS = 3;
        public const int CHILD04_CUSTOMER_GIRLS = 4;
        public const int CHILD05_CUSTOMER_BOYS = 5;
        public const int CHILD06_YOUR_GIRLS = 6;
        public const int CHILD07_YOUR_BOYS = 7;
        public const int CHILD08_MISCARRIAGES = 8;
        public const int CHILD09_ABORTIONS = 9;
        public const int CHILD_COUNT_TYPES = 10; // last type+1

        // girl specific triggers
        public const int TRIGGER_RANDOM = 0;	// May trigger each week
        public const int TRIGGER_SHOPPING = 1;	// May trigger when shopping
        public const int TRIGGER_SKILL = 2;	// May trigger when a skill is greater or equal to a value
        public const int TRIGGER_STAT = 3;		// same as skill
        public const int TRIGGER_STATUS = 4;	// has a particular status, ie slave, pregnant etc
        public const int TRIGGER_MONEY = 5;	// same as skill or stat levels
        public const int TRIGGER_MEET = 6;		// Triggers when meeting girl
        public const int TRIGGER_TALK = 7;		// triggered when talking to girl in dungeon on details screen
        public const int TRIGGER_WEEKSPAST = 8;	// certain number of weeks pass while girl is in employment
        public const int TRIGGER_GLOBALFLAG = 9;	// triggered when a global flag is set
        public const int TRIGGER_SCRIPTRUN = 10;	// triggered when a specifed script has been run
        public const int TRIGGER_KIDNAPPED = 11;	// triggers when a girl is kidnaped
        public const int TRIGGER_PLAYERMONEY = 12;	// triggers when players money hits a value

    }
}
