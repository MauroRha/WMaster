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
//  <copyright file="Game.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-13</datecreated>
//  <summary>
// Static wrapper to <see cref="WMaster.Manager.GameEngine"/> providing easy access to it's functionality.
// <remarks><para>All methodes and properties are linked to same member of <see cref="WMaster.Manager.GameEngine"/> unique instance.</para></remarks>
//  </summary>
//  <remarks>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Entity.Item;
    using WMaster.Manager;
    using WMaster.Tool;

    /// <summary>
    /// Static wrapper to <see cref="WMaster.Manager.GameEngine"/> providing easy access to it's functionality.
    /// <remarks><para>All methodes and properties are linked to same member of <see cref="WMaster.Manager.GameEngine"/> unique instance.</para></remarks>
    /// </summary>
    public static class Game
    {
        public static Config Configuration
        {
            get { return GameEngine.Instance.cfg; }
        }

        private static MessageQue m_MessageQue = new MessageQue();
        public static MessageQue MessageQue
        {
            get { return Game.m_MessageQue; }
        }


        /// <summary>
        /// Get or set the current year in game.
        /// </summary>
        public static int Year
        {
            get { return GameEngine.Instance.Year; }
            set { GameEngine.Instance.Year = value; }
        }
        /// <summary>
        /// Get or set the current month in game.
        /// </summary>
        public static int Month
        {
            get { return GameEngine.Instance.Month; }
            set { GameEngine.Instance.Month = value; }
        }
        /// <summary>
        /// Get or set the current day in game.
        /// </summary>
        public static int Day
        {
            get { return GameEngine.Instance.Day; }
            set { GameEngine.Instance.Day = value; }
        }

        /// <summary>
        /// Player gold
        /// </summary>
        [Obsolete("Move gold to player instance?", false)]
        public static WMaster.ClassOrStructurToImplement.cGold Gold
        { get { return GameEngine.Instance.Gold; } }

        /// <summary>
        /// Get Player manager.
        /// </summary>
        public static cPlayer Player
        {
            get { return GameEngine.Instance.g_Brothels.GetPlayer(); }
        }

        /// <summary>
        /// Get the rivals manager.
        /// </summary>
        public static cRivalManager Rivals
        {
            get { return GameEngine.Instance.g_Brothels.GetRivalManager(); }
        }

        /// <summary>
        /// Get the dungeon manager
        /// </summary>
        public static cDungeon Dungeon
        {
            get { return GameEngine.Instance.g_Brothels.GetDungeon(); }
        }

        #region Game manager
        /// <summary>
        /// Get the resource manager.
        /// </summary>
        public static IResourceManager Resources
        {
            get { return GameEngine.Instance.Resources; }
        }
        
        ///// <summary>
        ///// Resource manager (localized string, images, sound, ect.).
        ///// </summary>
        //public CResourceManager Resources
        //{
        //    get { return GameEngine.Instance.rmanager; }
        //}

        /// <summary>
        /// Get girl manager.
        /// </summary>
        public static cGirls Girls
        {
            get { return GameEngine.Instance.g_Girls; }
        }

        /// <summary>
        /// Get brothel Manager.
        /// </summary>
        public static cBrothelManager Brothels
        {
            get { return GameEngine.Instance.g_Brothels; }
        }

        /// <summary>
        /// Get gang Manager.
        /// </summary>
        public static GangManager Gangs
        {
            get { return GameEngine.Instance.g_Gangs; }
        }

        /// <summary>
        /// Get customer Manager.
        /// </summary>
        public static cCustomers Customers
        {
            get { return GameEngine.Instance.g_Customers; }
        }

        /// <summary>
        /// Get clinic Manager.
        /// </summary>
        public static cClinicManager Clinic
        {
            get { return GameEngine.Instance.g_Clinic; }
        }

        /// <summary>
        /// Get movie Studio Manager.
        /// </summary>
        public static cMovieStudioManager Studios
        {
            get { return GameEngine.Instance.g_Studios; }
        }

        /// <summary>
        /// Get arena Manager.
        /// </summary>
        private static cArenaManager Arena
        {
            get { return GameEngine.Instance.g_Arena; }
        }

        /// <summary>
        /// Get centre Manager.
        /// </summary>
        private static cCentreManager Centre
        {
            get { return GameEngine.Instance.g_Centre; }
        }

        /// <summary>
        /// Get house Manager.
        /// </summary>
        public static cHouseManager House
        {
            get { return GameEngine.Instance.g_House; }
        }

        /// <summary>
        /// Get farm Manager.
        /// </summary>
        public static cFarmManager Farm
        {
            get { return GameEngine.Instance.g_Farm; }
        }

        /// <summary>
        /// Get inventory manager.
        /// </summary>
        public static Inventory Inventory
        {
            get { return GameEngine.Instance.g_InvManager; }
        }
        #endregion
    }
}
