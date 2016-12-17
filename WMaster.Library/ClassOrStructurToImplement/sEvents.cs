using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class cEvent
    {
        public byte m_Event; // type of event
        public byte m_MessageType; // Image Type of message
        public string m_Message;

        //string		name;					//	name of who this event applies to, usually girl name
        //int			imageType;
        //int			imageNum;

        string TitleText()
        { throw new NotImplementedException(); } //    Default listbox Text
        uint ListboxColour()
        { throw new NotImplementedException(); } //    Default Listbox Colour
        public uint m_Ordinal; //  Used for sort order
        bool IsGoodNews()
        { throw new NotImplementedException(); }
        bool IsUrgent()
        { throw new NotImplementedException(); }
        bool IsDanger()
        { throw new NotImplementedException(); }
        bool IsWarning()
        { throw new NotImplementedException(); }
        public static bool CmpEventPredicate(cEvent eFirst, cEvent eSecond)
        {
            return eFirst.m_Ordinal < eFirst.m_Ordinal;
        }
    }
}
