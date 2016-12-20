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
namespace WMaster.Game
{
    using System;
    using System.Collections.Generic;
    using WMaster.ClassOrStructurToImplement;

    public class Events
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Events()
        {
            m_bSorted = false;
        }

        void Free()
        { throw new NotImplementedException(); }
        public void Clear()
        {
            Free();
        }
        //void			DisplayMessages();		// No definition
        public void AddMessage(string message, int nImgType, int nEvent)
        { throw new NotImplementedException(); }
        cEvent GetMessage(int id)
        { throw new NotImplementedException(); }
        public int GetNumEvents()
        {
            return events.Count;
        }
        public bool IsEmpty()
        {
            return events.Count == 0;
        }
        bool HasGoodNews()
        { throw new NotImplementedException(); }
        bool HasUrgent()
        { throw new NotImplementedException(); }
        bool HasDanger()
        { throw new NotImplementedException(); }
        bool HasWarning()
        { throw new NotImplementedException(); }
        void DoSort()
        { throw new NotImplementedException(); }


        private List<cEvent> events = new List<cEvent>();
        private bool m_bSorted; // flag to only allow sort once
        uint MakeOrdinal(int nEvent)
        { throw new NotImplementedException(); }
    }
}
