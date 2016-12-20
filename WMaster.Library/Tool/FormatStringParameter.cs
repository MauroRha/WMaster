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

// Root namespace to provid global access
namespace WMaster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Couple of Parameter name / parameter value for string remplacement.
    /// </summary>
    public class FormatStringParameter
    {
        /// <summary>
        /// Name of parameter.
        /// </summary>
        private string _name;
        /// <summary>
        /// Get the name of parameter.
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// Value of parameter.
        /// </summary>
        public string _value;
        /// <summary>
        /// Get the value of parameter.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        public FormatStringParameter(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                WMaster.WMLog.Trace("FormatStringParameter(string name, string value) : name is null or empty string.", WMLog.TraceLog.ERROR);
                name = string.Empty;
            }

            this._name = name;
            this._value = value ?? string.Empty;
        }
    }
}
