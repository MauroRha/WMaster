using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{

    /*
     * now we can write a wrapper for a girl where all her trainable bits
     * appear to be in one array, with a single access method
     */
    public class TrainableGirl
    {
        private sGirl m_girl;
        private List<cTrainable> stats = new List<cTrainable>();
        TrainableGirl(sGirl girl)
        { throw new NotImplementedException(); }
        public cTrainable this[int index]
        {
            get
            {
                return stats[index];
            }
            set
            {
                stats[index] = value;
            }
        }
        public int size()
        {
            return stats.Count;
        }
        /*
         *	this is useful for solo training
         */
        string update_random(int size = 1)
        { throw new NotImplementedException(); }
        public sGirl girl()
        {
            return m_girl;
        }
    }

}
