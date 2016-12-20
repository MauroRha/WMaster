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
    using WMaster.ClassOrStructurToImplement;
    using WMaster.Enum;

    // Customer base
    public class cCustomers : System.IDisposable
    {
        public cCustomers()
        { throw new NotImplementedException(); }
        public void Dispose()
        { throw new NotImplementedException(); }

        void Free()
        { throw new NotImplementedException(); }

        void GenerateCustomers(sBrothel brothel, DayShift Day0Night1 = DayShift.Day)
        { throw new NotImplementedException(); } // generates a random amount of possible customers based on the number of poor, rich, and middle class
        sCustomer CreateCustomer(sBrothel brothel)
        { throw new NotImplementedException(); }

        //	sCustomer* GetParentCustomer();		// Gets a random customer from the customer base
        void GetCustomer(sCustomer customer, sBrothel brothel)
        { throw new NotImplementedException(); }
        void ChangeCustomerBase()
        { throw new NotImplementedException(); } // Changes customer base, it is based on how much money the player is bring into the town
        public int GetNumCustomers()
        {
            return m_NumCustomers;
        }
        public void AdjustNumCustomers(int amount)
        {
            m_NumCustomers += amount;
        }
        void Add(sCustomer cust)
        { throw new NotImplementedException(); }
        void Remove(sCustomer cust)
        { throw new NotImplementedException(); }
        void SetGoals(sCustomer cust)
        { throw new NotImplementedException(); }
        //	int GetHappiness();	//mod

        private int m_Poor; // percentage of poor people in the town
        private int m_Middle; // percentage of middle class people in the town
        private int m_Rich; // percentage of rich people in the town

        private int m_NumCustomers;
        private sCustomer m_Parent;
        private sCustomer m_Last;
    }

}
