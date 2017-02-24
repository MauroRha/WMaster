/*
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
#pragma once
/*
 * #pragma once does the same thing as #ifndef __FOO_H etc etc
 */

//#include <iostream>
//#include <fstream>
//#include <sstream>
//#include <string>
using namespace System;
using namespace System::IO;
using namespace System::Text;

namespace BrothelMaster
{
	public ref struct CLogInner
	{
	public:
		CLogInner();
		~CLogInner();
		void init();

		void write(String^ text);
		StreamWriter^	os()	{ return m_ofile; }
		StringBuilder%	ss()	{ return m_ss; }
		void ssend() {
			write(m_ss.ToString());
			m_ss.Clear();
		}
		static	bool setup = false;
		StreamWriter^ m_ofile;
		StringBuilder m_ss;
	};


	public ref class CLog
	{
	public:
		CLog() {
			CLog(false);
		}
		CLog(bool a_glob) {
			m_glob = a_glob;
		}
		~CLog() {
			if(!m_glob) {
				return;
			}
			if(inner) {
				delete inner;
			}
			inner = nullptr;
		}
		void write(String^ text)	{
			if(inner == nullptr) inner = gcnew CLogInner();
			inner->write(text);
		}
		StreamWriter^	os()	{
			if(inner == nullptr) inner = gcnew CLogInner();
			return inner->m_ofile;
		}
		StringBuilder% ss()	{
			if(inner == nullptr) inner = gcnew CLogInner();
			return inner->m_ss;
		}
		void		ssend()	{
			if(inner == nullptr) inner = gcnew CLogInner();
			inner->ssend();
		}
	private:
		bool m_glob;
		static	CLogInner ^inner;
	};

}
