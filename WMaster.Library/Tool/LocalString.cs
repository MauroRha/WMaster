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
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provide static <see cref="StringBuilder"/> like localized string manager.
    /// </summary>
    public class LocalString
    {
        /// <summary>
        /// Resource cotegory to find string.
        /// </summary>
        [Flags]
        public enum ResourceStringCategory
        {
            /// <summary>
            /// Global related string.
            /// </summary>
            Global = /*        */ 0x0001,
            /// <summary>
            /// Player related string.
            /// </summary>
            Player = /*        */ 0x0002,
            /// <summary>
            /// Brothel report.
            /// </summary>
            Brothel = /*       */ 0x0003,
            /// <summary>
            /// Gang related string.
            /// </summary>
            Gang = /*          */ 0x0010,
            /// <summary>
            /// Gang mission report.
            /// </summary>
            GangMission = /*   */ 0x0011,
            /// <summary>
            /// Girl related string.
            /// </summary>
            Girl = /*          */ 0x0020,
            /// <summary>
            /// Girl job report.
            /// </summary>
            GirlJob = /*       */ 0x0021,
            /// <summary>
            /// item text resources..
            /// </summary>
            Items = /*         */ 0x0030,
            /// <summary>
            /// Girls text resources. To find in girl subdirectory.
            /// </summary>
            ExternalGirls = /* */ 0xFFE0,
            /// <summary>
            /// Items text resources. To find in item subdirectory.
            /// </summary>
            ExternalItems = /* */ 0xFFF0
        }

        /// <summary>
        /// Localized strings store inside Dictionary resource category->resource name->string translated.
        /// </summary>
        private static Dictionary<LocalString.ResourceStringCategory, Dictionary<string, string>> _localizedString;
        /// <summary>
        /// Get localized strings store inside Dictionary resource category->resource name->string translated with Lazy initialisation.
        /// </summary>
        private static Dictionary<LocalString.ResourceStringCategory, Dictionary<string, string>> LocalizedString
        {
            get
            {
                if (LocalString._localizedString == null)
                {
                    LocalString._localizedString = new Dictionary<LocalString.ResourceStringCategory, Dictionary<string, string>>();
                }
                return LocalString._localizedString;
            }
        }

        private static void InitializeStringManager()
        {
            throw new NotImplementedException();
            // TODO : Loading string resources. Switch loading by category.
        }
        
        /// <summary>
        /// Internal <see cref="StringBuilder"/> storing localised resource text.
        /// </summary>
        StringBuilder m_String = new StringBuilder();

        /// <summary>
        /// Get localised (or default if not translate) string with resource name <paramref name="resourceName"/> store in <paramref name="category"/> resources.
        /// </summary>
        /// <param name="category"><see cref="LocalString.ResourceStringCategory"/> where to find resource.</param>
        /// <param name="resourceName">Unique name (inside category) of resource to find.</param>
        /// <returns>Localised string or <paramref name="resourceName"/> if not found.</returns>
        public static string GetString(LocalString.ResourceStringCategory category, string resourceName)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.GetString : resourceName is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }

            if (LocalizedString.ContainsKey(category))
            {
                if (LocalizedString[category].ContainsKey(resourceName))
                {
                    string returnValue = LocalizedString[category][resourceName].Replace("[[:NewLine:]]", Environment.NewLine);
                    return returnValue;
                }
                else
                {
                    WMLog.Trace(string.Format("LocalString.GetString : No resources {0} string foud for Category {1}.", resourceName, category), WMLog.TraceLog.ERROR);
                }
            }
            else
            {
                WMLog.Trace(string.Format("LocalString.GetString : No resources string foud for Category {0}.", category), WMLog.TraceLog.ERROR);
            }
            return resourceName;
        }

        /// <summary>
        /// Get localised (or default if not translate) string with resource name <paramref name="resourceName"/> store in <paramref name="category"/> resources ended with new line.
        /// </summary>
        /// <param name="category"><see cref="LocalString.ResourceStringCategory"/> where to find resource.</param>
        /// <param name="resourceName">Unique name (inside category) of resource to find.</param>
        /// <returns>Localised string or <paramref name="resourceName"/> if not found.</returns>
        public static string GetStringLine(LocalString.ResourceStringCategory category, string resourceName)
        {
            if (resourceName == null)
            {
                WMLog.Trace("GetStringLine.GetString : resourceName is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }

            return LocalString.GetString(category, resourceName) + Environment.NewLine;
        }

        /// <summary>
        /// Get localised (or default if not translate) string with resource name <paramref name="resourceName"/> store in <paramref name="category"/> resources.
        /// </summary>
        /// <param name="category"><see cref="LocalString.ResourceStringCategory"/> where to find resource.</param>
        /// <param name="resourceName">Unique name (inside category) of resource to find.</param>
        /// <returns>Localised string or <paramref name="resourceName"/> if not found.</returns>
        public static string GetStringFormat(LocalString.ResourceStringCategory category, string resourceName, List<FormatStringParameter> replacementValues)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.GetStringFormat : resourceName is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }
            if (replacementValues == null)
            {
                WMLog.Trace("LocalString.GetStringFormat : replacementValues is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }

            try
            {
                string template = LocalString.GetString(category, resourceName);
                if (template == null)
                {
                    WMLog.Trace("LocalString.AppendFormat : category: {0}, resource name: {1} return null or empty string.", WMLog.TraceLog.WARNING);
                    return string.Empty;
                }

                foreach (FormatStringParameter replacement in replacementValues)
                {
                    template = template.Replace(string.Format("[[:{0}:]]", replacement.Name), replacement.Value);
                }
                return template;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get localised (or default if not translate) string with resource name <paramref name="resourceName"/> store in <paramref name="category"/> resources ended with new line.
        /// </summary>
        /// <param name="category"><see cref="LocalString.ResourceStringCategory"/> where to find resource.</param>
        /// <param name="resourceName">Unique name (inside category) of resource to find.</param>
        /// <returns>Localised string or <paramref name="resourceName"/> if not found.</returns>
        public static string GetStringFormatLine(LocalString.ResourceStringCategory category, string resourceName, List<FormatStringParameter> replacementValues)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.GetStringFormatLine : resourceName is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }
            if (replacementValues == null)
            {
                WMLog.Trace("LocalString.GetStringFormatLine : replacementValues is null.", WMLog.TraceLog.ERROR);
                return string.Empty;
            }

            try
            {
                string template = LocalString.GetStringFormat(category, resourceName, replacementValues) + Environment.NewLine;
                return template;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return string.Empty;
            }
        }

        /// <summary>
        /// Initialise an instance of <see cref="LocalString"/>
        /// </summary>
        public LocalString()
        {
            // TODO : Load string
            // TODO : replace [[:NewLine:]] by Environement.NewLine
        }

        /// <summary>
        /// Append resource string with replacement value.
        /// </summary>
        /// <param name="category">Categorie of resource.</param>
        /// <param name="resourceName">Code name of resource.</param>
        /// <param name="replacementValues">List of parameters to replace in string.</param>
        public void AppendFormat(ResourceStringCategory category, string resourceName, List<FormatStringParameter> replacementValues)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.AppendFormat : resourceName is null.", WMLog.TraceLog.ERROR);
                return;
            }
            if (replacementValues == null)
            {
                WMLog.Trace("LocalString.AppendFormat : replacementValues is null.", WMLog.TraceLog.ERROR);
                return;
            }

            string template = LocalString.GetStringFormat(category, resourceName, replacementValues);
            this.m_String.Append(template);
        }

        /// <summary>
        /// Append resource string with replacement value end with new line.
        /// </summary>
        /// <param name="category">Categorie of resource.</param>
        /// <param name="resourceName">Code name of resource.</param>
        /// <param name="replacementValues">List of parameters to replace in string.</param>
        public void AppendLineFormat(ResourceStringCategory category, string resourceName, List<FormatStringParameter> replacementValues)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.AppendLineFormat : resourceName is null.", WMLog.TraceLog.ERROR);
                return;
            }
            if (replacementValues == null)
            {
                WMLog.Trace("LocalString.AppendLineFormat : replacementValues is null.", WMLog.TraceLog.ERROR);
                return;
            }

            string template = LocalString.GetStringFormatLine(category, resourceName, replacementValues);
            this.m_String.Append(template);
        }

        /// <summary>
        /// Append resource string.
        /// </summary>
        /// <param name="category">Categorie of resource.</param>
        /// <param name="resourceName">Code name of resource.</param>
        public void Append(ResourceStringCategory category, string resourceName)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.Append : resourceName is null.", WMLog.TraceLog.ERROR);
                return;
            }

            string template = LocalString.GetString(category, resourceName);
            this.m_String.Append(template);
        }

        /// <summary>
        /// Append string <paramref name="litteral"/> without modification.
        /// </summary>
        /// <param name="litteral">Litteral to add as is.</param>
        public void AppendLineLitteral(string litteral)
        {
            if (litteral == null)
            {
                WMLog.Trace("LocalString.AppendLitteral : litteral is null.", WMLog.TraceLog.ERROR);
                return;
            }

            this.AppendLitteral(litteral);
            this.NewLine();
        }

        /// <summary>
        /// Append string <paramref name="litteral"/> without modification.
        /// </summary>
        /// <param name="litteral">Litteral to add as is.</param>
        public void AppendLitteral(string litteral)
        {
            if (litteral == null)
            {
                WMLog.Trace("LocalString.AppendLitteral : litteral is null.", WMLog.TraceLog.ERROR);
                return;
            }

            this.m_String.Append(litteral);
        }

        /// <summary>
        /// Append string <paramref name="litteral"/> without modification.
        /// </summary>
        /// <param name="litteral">Litteral to add as is.</param>
        /// <param name="replacementValues">List of parameters to replace in string.</param>
        public void AppendLitteralFormat(string litteral, List<FormatStringParameter> replacementValues)
        {
            if (litteral == null)
            {
                WMLog.Trace("LocalString.AppendLitteralFormat : litteral is null.", WMLog.TraceLog.ERROR);
                return;
            }
            if (replacementValues == null)
            {
                WMLog.Trace("LocalString.AppendLitteralFormat : replacementValues is null.", WMLog.TraceLog.ERROR);
                return;
            }

            foreach (FormatStringParameter replacement in replacementValues)
            {
                litteral = litteral.Replace(string.Format("[[:{0}:]]", replacement.Name), replacement.Value);
            }

            this.m_String.Append(litteral);
        }

        /// <summary>
        /// Append resource string end with new line.
        /// </summary>
        /// <param name="category">Categorie of resource.</param>
        /// <param name="resourceName">Code name of resource.</param>
        public void AppendLine(ResourceStringCategory category, string resourceName)
        {
            if (resourceName == null)
            {
                WMLog.Trace("LocalString.Append : resourceName is null.", WMLog.TraceLog.ERROR);
                return;
            }

            string template = LocalString.GetStringLine(category, resourceName);
            this.m_String.Append(template);
        }

        /// <summary>
        /// Append new line.
        /// </summary>
        public void NewLine()
        {
            this.m_String.Append(Environment.NewLine);
        }

        /// <summary>
        /// Append a comma (',').
        /// </summary>
        public void Comma()
        {
            this.m_String.Append(',');
        }

        /// <summary>
        /// Append a dot ('.').
        /// </summary>
        public void Dot()
        {
            this.m_String.Append(',');
        }

        /// <summary>
        /// Clear the <see cref="LocalString"/> buffer.
        /// </summary>
        public void Clear()
        {
            this.m_String.Clear();
        }

        /// <summary>
        /// Get string composition.
        /// </summary>
        /// <returns>Result of string composition.</returns>
        public override string ToString()
        {
            return this.m_String.ToString();
        }

        /// <summary>
        /// If <see cref="LocalString"/> have message set.
        /// </summary>
        /// <returns><b>True</b> if <see cref="LocalString"/> has message set.</returns>
        public bool HasMessage()
        {
            return !this.m_String.Length.Equals(0);
        }
    }
}
