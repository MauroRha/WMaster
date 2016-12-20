using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;
using WMaster.Game.Concept;

namespace WMaster.ClassOrStructurToImplement
{
    public class sChild : System.IDisposable
    {
        public int m_MultiBirth;
        public string multibirth_str()
        {
            if (m_MultiBirth == 2)
            {
                return "Twins";
            }
            if (m_MultiBirth == 3)
            {
                return "Triplets";
            }
            if (m_MultiBirth == 4)
            {
                return "Quads";
            }
            if (m_MultiBirth == 5)
            {
                return "Quints";
            }
            // `J` anything else is single
            m_MultiBirth = 1;
            return "Single";
        }
        public enum Gender
        {
            None = -1,
            Girl = 0,
            Boy = 1
        }
        public Gender m_Sex;
        public int m_GirlsBorn; // if multiple births, how many are girls
        public string boy_girl_str()
        {
            if (m_MultiBirth == 2)
            {
                return "twins";
            }
            if (m_MultiBirth == 3)
            {
                return "triplets";
            }
            if (m_MultiBirth > 3)
            {
                return "a litter";
            }
            if (m_Sex == Gender.Boy)
            {
                return "a baby boy";
            }
            if (m_Sex == Gender.Girl)
            {
                return "a baby girl";
            }
            return "a baby";
        }
        public bool is_boy()
        {
            return m_Sex == Gender.Boy;
        }
        public bool is_girl()
        {
            return m_Sex == Gender.Girl;
        }

        public int m_Age; // grows up at 60 weeks
        public bool m_IsPlayers; // 1 when players
        public byte m_Unborn; // 1 when child is unborn (for when stats are inherited from customers)

        // skills and stats from the father
        public int[] m_Stats = new int[(int)EnumStats.NUM_STATS];
        public int[] m_Skills = new int[(int)EnumSkills.NUM_SKILLS];

        public sChild m_Next;
        public sChild m_Prev;

        sChild(bool is_players = false, Gender gender = Gender.None, int MultiBirth = 1)
        { throw new NotImplementedException(); }
        public void Dispose()
        {
            m_Prev = null;
            if (m_Next != null)
            {
                m_Next.Dispose();
            }
            m_Next = null;
        }

        IXmlElement SaveChildXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadChildXML(IXmlHandle hChild)
        { throw new NotImplementedException(); }
    }
}
