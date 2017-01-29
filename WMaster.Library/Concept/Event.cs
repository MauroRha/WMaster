using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;

namespace WMaster.ClassOrStructurToImplement
{
    public class Event
    {
        /// <summary>
        /// Type of event.
        /// </summary>
        public EventType EventType { get; set; }
        /// <summary>
        /// Image Type of message.
        /// </summary>
        public ImageType MessageType { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Used for sort order.
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// All <see cref="EventType"/> act as good news.
        /// </summary>
        private static EventType m_GoodNews = EventType.GoodNews | EventType.LevelUp;
        /// <summary>
        /// All <see cref="EventType"/> act as urgent.
        /// </summary>
        private static EventType m_Urgent = EventType.Danger | EventType.Warning | EventType.NoWork | EventType.GoodNews | EventType.LevelUp;
        /// <summary>
        /// All <see cref="EventType"/> act as danger.
        /// </summary>
        private static EventType m_Danger = EventType.Danger;
        /// <summary>
        /// All <see cref="EventType"/> act as warning.
        /// </summary>
        private static EventType m_Warning = EventType.Warning | EventType.NoWork;

        //string		name;					//	name of who this event applies to, usually girl name
        //int			imageType;
        //int			imageNum;

        /// <summary>
        /// Get default text for event (Title).
        /// </summary>
        /// <returns>default text for event (Title).</returns>
        string TitleText()
        { return LocalString.GetString(LocalString.ResourceStringCategory.Global, string.Format("TitleEventType{0}", EventType)); } //    Default listbox Text
  
        /// <summary>
        /// Get <see cref="MessageCategory"/> of this event.
        /// </summary>
        /// <returns><see cref="MessageCategory"/> of this event.</returns>
        [Obsolete("IHM specific functionality rename to EventCategory")]
        public MessageCategory ListboxColour()
        {
            switch (EventType)
            {
                case EventType.Warning:
                    return MessageCategory.Darkblue;
                case EventType.Danger:
                    return MessageCategory.Red;
                case EventType.GoodNews:
                    return MessageCategory.Green;
                case EventType.NoWork:
                    return MessageCategory.Darkblue;
                case EventType.LevelUp:
                    return MessageCategory.Yellow;
                case EventType.Debug:
                    return MessageCategory.Red;
                case EventType.DayShift:
                case EventType.NightShift:
                case EventType.Summary:
                case EventType.Dungeon:
                case EventType.Matron:
                case EventType.Gang:
                case EventType.Brothel:
                case EventType.BackToWork:
                default:
                    return MessageCategory.Blue;
            }
        } //    Default Listbox Colour

        /// <summary>
        /// <b>True</b> id event is a good news.
        /// </summary>
        public bool IsGoodNews
        {
            get { return Event.m_GoodNews.HasFlag(EventType); }
        }
        /// <summary>
        /// <b>True</b> id event is urgent.
        /// </summary>
        public bool IsUrgent
        {
            get { return Event.m_Urgent.HasFlag(EventType); }
        }
        /// <summary>
        /// <b>True</b> id event is danger.
        /// </summary>
        public bool IsDanger
        {
            get { return Event.m_Danger.HasFlag(EventType); }
        }
        /// <summary>
        /// <b>True</b> id event is warning.
        /// </summary>
        public bool IsWarning
        {
            get { return Event.m_Warning.HasFlag(EventType); }
        }

        public static bool CmpEventPredicate(Event eFirst, Event eSecond)
        {
            return eFirst.Ordinal < eSecond.Ordinal;
        }
    }
}
