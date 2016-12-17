using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    using System.Runtime.InteropServices;
    
    public class sEntry : System.IDisposable // represents a single entry in an action
    {
        public uint m_Type; // Type of entry
        //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes:
        //ORIGINAL LINE: union
        //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct:
        [StructLayout(LayoutKind.Explicit)]
        public struct AnonymousStruct
        {
            [FieldOffset(0)]
            public int m_NumChoices; // how many choices in _CHOICE
            [FieldOffset(0)]
            public int m_TypeID; // What id is _TYPE
            [FieldOffset(0)]
            public int m_lMin; // min long number
            [FieldOffset(0)]
            public float m_fMin; // min float number
        }

        //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes:
        //ORIGINAL LINE: union
        //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct2:
        [StructLayout(LayoutKind.Explicit)]
        public struct AnonymousStruct2
        {
            [FieldOffset(0)]
            public int m_lMax; // max long number
            [FieldOffset(0)]
            public float m_fMax; // max float number
            [FieldOffset(0)]
            public string[] m_Choices; // text array of choices for _CHOICE
        }

        // Structure constructor to clear to default values
        public sEntry()
        {
            //m_Type = _NONE;
            //m_NumChoices = 0;
            //m_Choices = 0;
            //m_TypeID = 0;
        }

        // Structure destructor to clean up used resources
        public void Dispose()
        {
            // Special case for choice types
            //if ((m_Type == _CHOICE) && m_Choices != 0)
            //{
            //    if (m_NumChoices)
            //    {
            //        for (int i = 0; i < m_NumChoices; i++)
            //        {
            //            Arrays.DeleteArray(m_Choices[i]); // Delete choice text
            //            m_Choices[i] = 0;
            //        }
            //    }
            //    Arrays.DeleteArray(m_Choices); // Delete choice array
            //    m_Choices = 0;
            //}
        }
    }

}
