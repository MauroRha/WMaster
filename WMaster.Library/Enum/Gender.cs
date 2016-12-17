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

//<!-- -------------------------------------------------------------------------------------------------------------------- -->
//<file>
//  <copyright file="Gender.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2016-12-13</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster.Enum
{
    using System;

    // TODO : REFACTORING - Rename enum value to lower case UC first
    // TODO : REFACTOR V2 - Convert to Class/Instance for PTVO use ?

    /// <summary>
    /// Gender enumeration with or withour (P)enis, (T)estes, (V)agina and (O)varies
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// All female.
        /// <para>P:0, T:0, V:1, O:1</para>
        /// </summary>
        FEMALE = 0,
        /// <summary>
        /// Female with vagina but no ovaries.
        /// <para>P:0, T:0, V:1, O:0</para>
        /// </summary>
        FEMALENEUT,
        /// <summary>
        /// Female with penis but no testes.
        /// <para>P:1, T:0, V:1, O:1</para>
        /// </summary>
        FUTA,
        /// <summary>
        /// Female with vagina and penis but no ovaries or testes.
        /// <para>P:1, T:0, V:1, O:0</para>
        /// </summary>
        FUTANEUT,
        /// <summary>
        /// Female with penis and testes.
        /// <para>P:1, T:1, V:1, O:1</para>
        /// </summary>
        FUTAFULL,
        /// <summary>
        /// No gender but more female.
        /// <para>P:0, T:0, V:0, O:0</para>
        /// </summary>
        NONEFEMALE,
        /// <summary>
        /// No gender at all.
        /// <para>P:0, T:0, V:0, O:0</para>
        /// </summary>
        NONE,
        /// <summary>
        /// No gender but more male.
        /// <para>P:0, T:0, V:0, O:0</para>
        /// </summary>
        NONEMALE,
        /// <summary>
        /// Male with vagina and ovaries.
        /// <para>P:1, T:1, V:1, O:1</para>
        /// </summary>
        HERMFULL,
        /// <summary>
        /// Male with penis and vagina but no testes or ovaries.
        /// <para>P:1, T:0, V:1, O:0</para>
        /// </summary>
        HERMNEUT,
        /// <summary>
        /// Male with vagina but no ovaries.
        /// <para>P:1, T:1, V:1, O:0</para>
        /// </summary>
        HERM,
        /// <summary>
        /// Male with penis but no testes.
        /// <para>P:1, T:0, V:0, O:0</para>
        /// </summary>
        MALENEUT,
        /// <summary>
        /// All male.
        /// <para>P:1, T:1, V:0, O:0</para>
        /// </summary>
        MALE,

        /// <summary>
        /// Number of different genders.
        /// </summary>
        [Obsolete("The NUM_GENDERS enum value of eGender must be replace by enum extention function returning the number of value in enum. Enum value must be only enum value!", false)]
        NUM_GENDERS
    };
}
