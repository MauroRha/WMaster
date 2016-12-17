using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement
{
/*
 * I don't generally see the point of using chars over ints
 * for stat values - most PCs have more than enough memory to make the
 * space considerations trivial.
 *
 * that said, this would be an easy change to a template class
 * if we did want a char based version. Then a sBoundedVar would
 * use three times the memory of a char rather than 12 times, which
 * could be important if we're looking at porting to other platforms
 *
 * We could use static constants for the min and max values as well,
 * although we'd need a new subclass for each set of bounds and then to 
 * override the constants. Then of course we'd probably need a virtual accessor
 * to make sure we got the right constants ... and then the vtable would 
 * wipe out any savings in memory from the two ints (or chars).
 *
 * probably easier to stick with three values, all told.
 */
public class sBoundedVar
{
	public int m_min;
	public int m_max;
	public int m_curr;
/*
 *	defaults are set up for facility adjusters
 *	so range is 0-9, default to zero
 */
	public sBoundedVar()
	{
		m_min = 0;
		m_max = 9;
		m_curr = 0;
	}
/*
 *	but we could create one with any range
 */
	public sBoundedVar(int min, int max, int def = 0)
	{
		m_min = min;
		m_max = max;
		m_curr = def;
	}
/*
 *	methods for adjuster buttons - simple increment with
 *	bounds checking
 */
	public void up()
	{
		if (m_curr < m_max)
		{
			m_curr++;
		}
	}
	public void down()
	{
		if (m_curr > m_min)
		{
			m_curr--;
		}
	}
/*
 *	operators = += -= 
 */
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: int operator =(int val)
	public int CopyFrom(int val)
	{
		m_curr = val;
		return bound();
	}

    //public static int operator += (int val)
    //{
    //    m_curr += val;
    //    return bound();
    //}

    //public static int operator -= (int val)
    //{
    //    m_curr -= val;
    //    return bound();
    //}
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
		return m_curr;
	}
/*
 *	rather than a save method, just return an XML element
 *	that can be linked into a larger tree
 *
 *	The XML is going to look something like this:
 *
 *	<BoundedVar
 *		Name	= "Glitz"
 *		Min	= "0"
 *		Max	= "9"
 *		Curr	= "3"
 *	/>
 */
IXmlElement to_xml(string name)
        { throw new NotImplementedException(); }
bool from_xml(IXmlElement el)
        { throw new NotImplementedException(); }
}

}
