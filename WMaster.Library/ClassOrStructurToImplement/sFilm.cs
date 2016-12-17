using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    public class sFilm
    {
        //int total_girls;
        //int total_cost;
        public float quality_multiplyer;
        public List<int> scene_quality = new List<int>();
        public sbyte time;
        public int final_quality;
        public bool[] sex_acts_flags = new bool[5];
        public int total_customers;
        public sFilm()
        {
            quality_multiplyer = 0F;
        }
    }
}
