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

using namespace System;

namespace BrothelMaster
{
	//class CRenderObject;

	// An object that can be added to the render que, eg a character, or a game object.
	public ref class CRenderObject
	{
	public:
		CRenderObject() {m_Next = nullptr;}
		virtual void Draw() {};

		CRenderObject^ m_Next;
	};

	public ref class CRenderQue
	{
	public:
		CRenderQue() {m_Parent = nullptr; m_Last = nullptr;}
		~CRenderQue() {m_Parent = nullptr; m_Last = nullptr;}

		void DrawQue();
		void ClearQue();

		void AddObject(CRenderObject^ object);
	private:
		CRenderObject^ m_Parent;
		CRenderObject^ m_Last;
	};
}
