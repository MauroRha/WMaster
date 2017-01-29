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
namespace WMaster.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameManager
    {
        #region Singleton
        /// <summary>
        /// Singleton of <see cref="GameManager"/>.
        /// </summary>
        private static GameManager m_Instance;
        /// <summary>
        /// Get unique instance of <see cref="GameManager"/>.
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (GameManager.m_Instance == null)
                { GameManager.m_Instance = new GameManager(); }
                return GameManager.m_Instance;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public GameManager()
        { throw new NotImplementedException(); }
        #endregion
    }
}
