using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // Manages and loads the traits file
    public class cTraits : System.IDisposable
    {
        public cTraits()
        {
            m_ParentTrait = null;
            m_LastTrait = null;
            m_NumTraits = 0;
        }
        public void Dispose()
        { throw new NotImplementedException(); }

        public void Free()
        { throw new NotImplementedException(); } // Delete all the loaded data

        void LoadTraits(string filename)
        { throw new NotImplementedException(); } // Loads the traits from a file (adding them to the existing traits)
        void LoadXMLTraits(string filename)
        { throw new NotImplementedException(); } // Loads the traits from an XML file (adding them to the existing traits)
        void SaveTraits(string filename)
        { throw new NotImplementedException(); } // Saves the traits to a file

        void AddTrait(sTrait trait)
        { throw new NotImplementedException(); }
        void RemoveTrait(string name)
        { throw new NotImplementedException(); }
        sTrait GetTrait(string name)
        { throw new NotImplementedException(); }
        sTrait GetTraitNum(int num)
        { throw new NotImplementedException(); }
        public int GetNumTraits()
        {
            return m_NumTraits;
        }

        public int GetInheritChance(sTrait trait)
        {
            return trait.m_InheritChance;
        }
        public int GetRandomChance(sTrait trait)
        {
            return trait.m_RandomChance;
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	string GetTranslateName(string name);

        private int m_NumTraits;
        private sTrait m_ParentTrait; // the first trait in the list
        private sTrait m_LastTrait; // the last trait in the list
    }

}
