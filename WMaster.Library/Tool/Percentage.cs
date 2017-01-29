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
//  <copyright file="Percentage.cs" company="The Pink Petal Devloment Team">
//      Copyright © 2009, 2010 - The Pink Petal Devloment Team.
//  </copyright>
//  <author>Graben</author>
//  <datecreated>2017-01-02</datecreated>
//  <summary>
//  </summary>
//  <remarks>
//      <para name="Rem">Extract from GitHub : relased find in December 2016</para>
//      <para name="Review_2016-12">Gbn - 12/2016 : Create / Convert to C# / Refactoring</para>
//  </remarks>
//</file>
//<!-- -------------------------------------------------------------------------------------------------------------------- -->
namespace WMaster.Tool
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Structure representing a percentage. Can get value in range 0.0 to 1.0 or 0.0 to 100.0.
    /// </summary>
    public struct Percentage
    {
        /// <summary>
        /// Internal value of percentage. Store in range between 0.0 to 1.0.
        /// </summary>
        private double m_Value;

        /// <summary>
        /// Get or set percentage with range value between 0.0 to 100.0.
        /// </summary>
        public double Percent
        {
            get { return this.m_Value * 100; }
            set
            { this.m_Value = Math.Max(Math.Min(value, 100.0), 0.0) / 100; }
        }

        /// <summary>
        /// Get or set percentage with range value between 0.0 to 1.0.
        /// </summary>
        public double Coefficient
        {
            get { return this.m_Value; }
            set { this.m_Value = Math.Max(Math.Min(value, 1.0), 0.0); }
        }

        /// <summary>
        /// Convert the numeric value of this instance to string representation on format 0.00 to 100.00 ended with %
        /// </summary>
        /// <returns>String representation of this percentage.</returns>
        public override string ToString()
        {
            return (this.m_Value * 100).ToString("0.00%");
        }

        /// <summary>
        /// Initialise a Percentage instance.
        /// </summary>
        /// <param name="value">Range 0.0 / 100.0 value of percentage.</param>
        public Percentage(double value)
        {
            this.m_Value = Math.Max(Math.Min(value, 100.0), 0.0) / 100;
        }
    }
}
