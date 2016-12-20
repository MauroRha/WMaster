/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace WMaster.Game.Manager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using WMaster.ClassOrStructurToImplement;

    public class cTriggerList : System.IDisposable
    {
        public cTriggerList()
        {
            m_Triggers = null;
            m_CurrTrigger = null;
            m_Last = null;
            m_NumTriggers = 0;
            m_GirlTarget = null;
        }
        public void Dispose()
        {
            Free();
        }

        void Free()
        { throw new NotImplementedException(); }
        void LoadList(string filename)
        { throw new NotImplementedException(); }
        IXmlElement SaveTriggersXML(IXmlElement pRoot)
        { throw new NotImplementedException(); }
        bool LoadTriggersXML(IXmlHandle hTriggers)
        { throw new NotImplementedException(); }
        void LoadTriggersLegacy(Stream ifs)
        { throw new NotImplementedException(); }

        void AddTrigger(cTrigger trigger)
        { throw new NotImplementedException(); }

        void AddToQue(cTrigger trigger)
        { throw new NotImplementedException(); }
        void RemoveFromQue(cTrigger trigger)
        { throw new NotImplementedException(); }
        cTriggerQue GetNextQueItem()
        { throw new NotImplementedException(); }

        cTrigger CheckForScript(int Type, bool trigger, int[] values)
        { throw new NotImplementedException(); }

        void ProcessTriggers()
        { throw new NotImplementedException(); } // function that process the triggers in the list and adds them to the que if the conditions are met
        void ProcessNextQueItem(string fileloc)
        { throw new NotImplementedException(); }

        // set script targets
        public void SetGirlTarget(sGirl girl)
        {
            //C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent to pointers to variables (in C#, the variable no longer points to the original when the original variable is re-assigned):
            //ORIGINAL LINE: m_GirlTarget = girl;
            m_GirlTarget = girl;
        }

        bool HasRun(int num)
        { throw new NotImplementedException(); }

        private cTrigger m_CurrTrigger;
        private cTrigger m_Triggers;
        private cTrigger m_Last;
        private int m_NumTriggers;

        //int m_NumQued;
        //cTriggerQue* m_StartQue;
        //cTriggerQue* m_EndQue;
        private Queue<cTriggerQue> m_TriggerQueue = new Queue<cTriggerQue>(); //mod


        // script targets (things that the script will affect with certain commands)
        private sGirl m_GirlTarget; // if not 0 then the script is affecting a girl
    }

}
