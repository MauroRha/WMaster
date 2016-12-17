using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{
    using System.Collections.Generic;

    public class IdealGirl
    {
        private List<IdealAttr> stats = new List<IdealAttr>();
        IdealGirl(List<TrainableGirl> set)
        { throw new NotImplementedException(); }
        /*cTrainable*/
        public IdealAttr this[int index]
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
        List<int> training_indices()
        { throw new NotImplementedException(); }
    }

}
