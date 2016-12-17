using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMaster.ClassOrStructurToImplement
{
/*
 * this is a bit of a mess from a model-view-controller point of view
 * but I'm not clear how to untangle it right now.
 *
 * So I'll make it work for now, and untangle it later
 */
    public class sBoundedVar_Provides : sBoundedVar
    {
        public int m_inc; // increment - this per bump
        public int m_space; // space taken
        public double m_slots_per_space; // how many slots for one space?

        /*
         *	we need to know how much extra space a bump would consume
         */
        public int space_needed()
        {
            /*
             *		we always use a whole space
             *		if we get 4 kennels per space, then we bump
             *		in bundles of 4 slots and one space
             */
            if (m_slots_per_space >= 1)
            {
                return 1;
            }
            /*
             *		otherwise we return the spaces needed for the
             *		next whole slot. Fractional slots are dropped
             */
            return (int)Math.Floor((m_curr + 1) / m_slots_per_space) - m_space;
        }
        /*
         *	same exercise from a slot perspective
         */
        public int slots_needed()
        {
            if (m_slots_per_space < 1)
            {
                return 1;
            }
            return (int)Math.Ceiling((m_space + 1) * m_slots_per_space) - m_curr;
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	void init(sFacility fac);

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	TiXmlElement to_xml(string name);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //	bool from_xml(TiXmlElement el);

        public void up()
        {
            int slot_inc = slots_needed();
            if (m_curr + slot_inc > m_max)
            {
                return;
            }
            m_space += space_needed();
            m_curr += slot_inc;
        }

        /*
         *	this is a little complicated
         *
         *	if a slot takes more than one space
         *	then we drop enough spaces to reduce the slot count
         *	so if the facility is a 4 space apartment suite,
         *	we drop by 4 spaces and one slot
         *
         *	on the other hand, if there are multiple slots per space
         *	we want to drop a space, and reduce the slot count to the maximum
         *	that fit in the new size. So if we get 4 kennels to the space,
         *	we reduce by 1 space and 4 kennel slots.
         */
        /*
         *	this is a little complicated
         *
         *	if a slot takes more than one space
         *	then we drop enough spaces to reduce the slot count
         *	so if the facility is a 4 space apartment suite,
         *	we drop by 4 spaces and one slot
         *
         *	on the other hand, if there are multiple slots per space
         *	we want to drop a space, and reduce the slot count to the maximum
         *	that fit in the new size. So if we get 4 kennels to the space,
         *	we reduce by 1 space and 4 kennel slots.
         */
        public void down()
        {
            if (m_curr <= m_min)
            {
                return;
            }
            /*
             *		The simplest case is if we get one slot per space
             *		this probably collapses elegantly into one of the
             *		other cases, but since I'm having trouble getting
             *		my head around the problem, I'm going to invoke the KISS principle.
             *		Keep It Simple, Stupid.
             */
            if (m_slots_per_space == 1.0)
            {
                m_curr--;
                m_space--;
                return;
            }
            /*
             *		if the slots-per-space count is more than one
             *		we can just drop a space and re-calculate
             */
            if (m_slots_per_space > 1.0)
            {
                m_space--;
                /*
                 *			just because we get more than one
                 *			that doesn't mean we get a whole number
                 *			it might be a 3-for-two deal, for instance
                 *
                 *			so we use floor to drop any fractional slots
                 *			from the calculation
                 */
                m_curr = (int)Math.Floor(m_slots_per_space * m_space);
                return;
            }
            /*
             *		if we get here, we get less than one slot for a space
             *		so we drop the slot count by one, and then re-calculate space
             *		instead.
             */
            m_curr--;
            /*
             *		again, we might not get a whole number - dorms use 6 slots 
             *		in 2 spaces, for instance. So make sure the space requirement
             *		rounds UP
             */
            m_space = (int)Math.Ceiling(m_curr / m_slots_per_space);
        }
        public int bound()
        {
            if (m_curr < m_min)
            {
                m_curr = m_min;
            }
            if (m_curr > m_max)
            {
                m_curr = m_max;
            }
            /*
             *		we're setting the slot count here, 
             *		so calculate the space based on that
             */
            m_space = (int)Math.Ceiling(m_curr / m_slots_per_space);
            return m_curr;
        }
    }
}
