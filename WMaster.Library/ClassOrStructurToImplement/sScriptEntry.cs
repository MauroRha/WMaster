using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    using System.Runtime.InteropServices;

    public class sScriptEntry : System.IDisposable
    {
        public int m_Type; // Type of entry (_TEXT, _BOOL, etc.)
        //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes:
        //ORIGINAL LINE: union
        //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct:
        [StructLayout(LayoutKind.Explicit)]
        public struct AnonymousStruct
        {
            [FieldOffset(0)]
            public int m_IOValue; // Used for saving/loading
            [FieldOffset(0)]
            public int m_Length; // Length of text (w/ 0 terminator)
            [FieldOffset(0)]
            public int m_Selection; // Selection in choice
            [FieldOffset(0)]
            public bool m_bValue; // bool value
            [FieldOffset(0)]
            public int m_lValue; // long value
            [FieldOffset(0)]
            public float m_fValue; // float value
        }

        public string m_Text; // Text buffer
        public int m_Var;

        public sScriptEntry()
        {
            //m_Type = _NONE; // Clear to default values
            //m_IOValue = 0;
            //m_Text = 0;
            //m_Var = 0;
        }

        public void Dispose()
        {
            //if (m_Text != 0)
            //{
            //    Arrays.DeleteArray(m_Text);
            //}
            //m_Text = 0;
        } // Delete text buffer
    }
}
