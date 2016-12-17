using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMaster.Enum;

namespace WMaster.ClassOrStructurToImplement
{
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
