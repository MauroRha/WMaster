using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    // holds data for movies
    public class sMovieScene : System.IDisposable
    {
        public string m_Name;
        public string m_Actress;
        public string m_Director;
        public int m_Job;
        public int m_Init_Quality;
        public int m_Quality;
        public int m_Promo_Quality;
        public int m_Money_Made;
        public int m_RunWeeks;
        public sMovieScene()
        {
        }
        public void Dispose()
        {
        }
        void OutputSceneRow(string Data, List<string> columnNames)
        { throw new NotImplementedException(); }
        void OutputSceneDetailString(string Data, string detailName)
        { throw new NotImplementedException(); }
    }

}
