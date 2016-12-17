using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
    /*
    a class to handle all the child related code to prevent errors.
    */
    public class cChildList : System.IDisposable
    {

        public sChild m_FirstChild;
        public sChild m_LastChild;
        public int m_NumChildren;
        public cChildList()
        {
            m_FirstChild = null;
            m_LastChild = null;
            m_NumChildren = 0;
        }
        public void Dispose()
        {
            if (m_FirstChild != null)
            {
                if (m_FirstChild != null)
                {
                    m_FirstChild.Dispose();
                }
            }
        }
        void add_child(sChild NamelessParameter)
        { throw new NotImplementedException(); }
        sChild remove_child(sChild NamelessParameter1, sGirl NamelessParameter2)
        { throw new NotImplementedException(); }
        //void handle_childs();
        //void save_data(ofstream);
        //void write_data(ofstream);
        //sChild* GenerateBornChild();//need to figure out what the player/customer base class is and if needed create one
        //sChild* GenerateUnbornChild();

    }

}
