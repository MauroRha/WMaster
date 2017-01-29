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
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Enums;

    /// <summary>
    /// Manage event list.
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// List of events.
        /// </summary>
        private List<Event> events = new List<Event>();
        /// <summary>
        /// Flag to only allow sort once.
        /// </summary>
        private bool m_bSorted;
        /// <summary>
        /// Constructor.
        /// </summary>
        public EventManager()
        {
            m_bSorted = false;
        }

        /// <summary>
        /// Clear event list and reset sort.
        /// </summary>
        void Free()
        {
            events.Clear();
            m_bSorted = false;
        }
        /// <summary>
        /// Clear event list and reset sort.
        /// </summary>
        public void Clear()
        {
            Free();
        }
        //void			DisplayMessages();		// No definition
        /// <summary>
        /// Add new event to evelt list.
        /// </summary>
        /// <param name="message">Message of event.</param>
        /// <param name="imageType"></param>
        /// <param name="eventType"></param>
        public void AddMessage(string message, ImageType imageType, EventType eventType)
        {
            Event newEvent = new Event();
            newEvent.MessageType = imageType;
            newEvent.EventType = eventType;
            newEvent.Message = message;
            //newEvent.m_Ordinal		= MakeOrdinal(eve);
            events.Add(newEvent);
        }
        /// <summary>
        /// Get <paramref name="index"/> <see cref="Event"/>.
        /// </summary>
        /// <param name="index">Index of event.</param>
        /// <returns><see cref="Event"/> at position <paramref name="index"/></returns>
        public Event GetMessage(int index)
        {
            if (index > 0 && events.Count > index)
            { return events[index]; }
            return null;
        }
        /// <summary>
        /// Get number of event in list.
        /// </summary>
        /// <returns>Number of event in list.</returns>
        public int GetNumEvents()
        {
            return events.Count;
        }
        /// <summary>
        /// Get if event list is empty.
        /// </summary>
        /// <returns><b>True</b> if event list is empty.</returns>
        public bool IsEmpty()
        {
            return events.Count == 0;
        }
        /// <summary>
        /// Get if event list has good news event.
        /// </summary>
        /// <returns><b>True</b> if event list has good news event.</returns>
        public bool HasGoodNews()
        {
            foreach (Event lEvent in events)
            {
                if (lEvent.IsGoodNews)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get if event list has urgent event.
        /// </summary>
        /// <returns><b>True</b> if event list has urgent event.</returns>
        public bool HasUrgent()
        {
            foreach (Event lEvent in events)
            {
                if (lEvent.IsUrgent)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get if event list has danger event.
        /// </summary>
        /// <returns><b>True</b> if event list has danger event.</returns>
        public bool HasDanger()
        {
            foreach (Event lEvent in events)
            {
                if (lEvent.IsDanger)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get if event list has warning event.
        /// </summary>
        /// <returns><b>True</b> if event list has warning event.</returns>
        public bool HasWarning()
        {
            foreach (Event lEvent in events)
            {
                if (lEvent.IsWarning)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Sort event list with Ordinal order.
        /// </summary>
        public void DoSort()
        {
            if (!m_bSorted)
            {
                events.Sort(delegate(Event first, Event second)
                {
                    if (first == null) return -1;
                    else if (second == null) return 1;
                    else return first.Ordinal.CompareTo(second.Ordinal);
                });
                m_bSorted = true;
            }
        }

        /// <summary>
        /// Returns a int that is used in sorting events.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public int MakeOrdinal(EventType eventType)
        {
            int offset;
            switch (eventType)
            {
                case EventType.Warning:
                    offset = 2000;
                    break;
                case EventType.Danger:
                    offset = 1000;
                    break;
                case EventType.Dungeon:
                    offset = 6000;
                    break;
                case EventType.Matron:
                    offset = 5000;
                    break;
                case EventType.NoWork:
                    offset = 3000;
                    break;
                case EventType.Debug:
                    offset = 1;
                    break;
                case EventType.DayShift:
                case EventType.NightShift:
                case EventType.Summary:
                case EventType.Gang:
                case EventType.Brothel:
                case EventType.GoodNews:
                case EventType.BackToWork:
                case EventType.LevelUp:
                default:
                    offset = 10000;
                    break;
            }

            return (offset + GetNumEvents());
        }
    }
}
