using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enums;
using WMaster.Concept;
using WMaster.Entity.Item;

namespace WMaster.ClassOrStructurToImplement
{
    // structure to hold randomly generated girl information
    public class sRandomGirl : System.IDisposable
    {
        public sRandomGirl()
        { throw new NotImplementedException(); }
        public void Dispose()
        { throw new NotImplementedException(); }

        public string m_Name;
        public string m_Desc;

        public bool m_newRandom;
        //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to value types:
        //ORIGINAL LINE: bool* m_newRandomTable;
        public bool m_newRandomTable;

        public bool m_Human; // 1 means they are human otherwise they are not
        public bool m_Catacomb; // 1 means they are a monster found in catacombs, 0 means wanderer
        public bool m_Arena; // 1 means they are fighter found in arena
        public bool m_YourDaughter; // `J` 1 means they are your daughter
        public bool m_IsDaughter; // 1 means they are a set daughter

        public int[] m_MinStats = new int[(int)EnumStats.NUM_STATS]; // min and max stats they may start with
        public int[] m_MaxStats = new int[(int)EnumStats.NUM_STATS];

        public int[] m_MinSkills = new int[(int)EnumSkills.NUM_SKILLS]; // min and max skills they may start with
        public int[] m_MaxSkills = new int[(int)EnumSkills.NUM_SKILLS];

        public int m_NumTraits; // number of traits they are assigned
        public int m_NumTraitNames; // number of traits they are assigned
        public sTrait[] m_Traits = new sTrait[Constants.MAXNUM_TRAITS]; // List of traits they may start with
        public int[] m_TraitChance = new int[Constants.MAXNUM_TRAITS]; // the percentage change for each trait
        public int[] m_TraitChanceB = new int[200];
        public string[] m_TraitNames = new string[200]; // `J` fix for more than MAXNUM_TRAITS in .rgirlsx files

        // `J` added starting items for random girls
        public int m_NumItems;
        public int m_NumItemNames;
        public sInventoryItem[] m_Inventory = new sInventoryItem[Constants.MAXNUM_INVENTORY];
        public int[] m_ItemChance = new int[Constants.MAXNUM_GIRL_INVENTORY];
        public int[] m_ItemChanceB = new int[200];
        public string[] m_ItemNames = new string[200];


        public int m_MinMoney; // min and max money they can start with
        public int m_MaxMoney;

        public sRandomGirl m_Next;
        /*
        *	MOD: DocClox Sun Nov 15 06:11:43 GMT 2009
        *	stream operator for debugging
        *	plus a shitload of XML loader funcs
        */
        //C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
        //ORIGINAL LINE: friend ostream& operator <<(ostream &os, sRandomGirl &g);
        //ostream operator <<(ostream os, sRandomGirl g);
        /*
        *	one func to load the girl node,
        *	and then one each for each embedded node
        *
        *	Not so much difficult as tedious.
        */
        void load_from_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); } // uses sRandomGirl::load_from_xml
        void process_trait_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); }
        void process_item_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); }
        void process_stat_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); }
        void process_skill_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); }
        void process_cash_xml(IXmlElement NamelessParameter)
        { throw new NotImplementedException(); }
        /*
        *	END MOD
        */
        public static sGirl lookup; // used to look up stat and skill IDs
    }
}
