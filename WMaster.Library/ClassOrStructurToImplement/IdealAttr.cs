using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    /*
    * we also need an idealized girl - one who combines the best attributes
    * from all the girls in the training set
    *
    * we don't need an underlying sGirl for this one - she's just
    * an abstraction
    */
    public class IdealAttr : cTrainable
    {
        private int m_attr_idx;
        private int m_value;
        private int m_potential;
        private int m_rand;
        IdealAttr(List<TrainableGirl> set, string name, int attr_idx)
        { throw new NotImplementedException(); }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int value() const
        public int value()
        {
            return m_value;
        }
        public void value(int n)
        {
            m_value = n;
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int potential() const
        public int potential()
        {
            return m_potential;
        }
        public void potential(int n)
        {
            m_potential = n;
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int noise() const
        public int noise()
        {
            return m_rand;
        }
        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: int attr_index() const
        public int attr_index()
        {
            return m_attr_idx;
        }
    }

}
