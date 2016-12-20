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
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Classe d'extenion de fonctionalité pour les énumérations
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Détermine si la combinaison de valeur de l'énumération contient une valeur spécifique (spécifié) .
        /// </summary>
        /// <param name="value">
        /// La combinaison de valeur de l'énumération a tester.
        /// </param>
        /// <param name="search">
        /// La valeur a rechercher.
        /// </param>
        /// <returns>
        /// <b>true</b> si la combinaison de valeur de l'énumération contient la valeur spécifié; sinon, <c>false</c>.
        /// </returns>
        /// <example>
        /// Affiche si ro contient RegexOptions.CultureInvariant.
        /// <code>
        /// RegexOptions ro = RegexOptions.CultureInvariant | RegexOptions.Multiline;
        /// if (dummy.Contains&lt;RegexOptions&gt;(RegexOptions.CultureInvariant))
        /// {
        ///     Console.WriteLine("ro contient RegexOptions.CultureInvariant");
        /// }
        /// </code>
        /// </example>
        public static bool Contains(this System.Enum value, System.Enum search)
        {
            int valueAsInt = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
            int searchAsInt = System.Convert.ToInt32(search, CultureInfo.InvariantCulture);

            if (searchAsInt == (valueAsInt & searchAsInt))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Obtient tous les éléments d'une énumération.
        /// </summary>
        /// <typeparam name="T">
        /// Le type de l'énumération.
        /// </typeparam>
        /// <param name="value">
        /// Une valeur de l'énumération.
        /// </param>
        /// <returns>
        /// Tous les éléments de l'énumération sous forme de collection
        /// </returns>
        public static IEnumerable<T> GetAllItems<T>(this System.Enum value) where T : struct
        {
            foreach (object item in System.Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        /// <summary>
        /// Retourne tous les éléments de l'énumération "Flag" combiné dans la valeur passé en paramètres.
        /// Attention!! L'énumération doit posédé l'attribut <see cref="System.FlagsAttribute"/> et chaque valeur de l'énumération doit être une puissance de 2 distinct.
        /// </summary>
        /// <typeparam name="T">
        /// Le type de l'énumération.
        /// </typeparam>
        /// <param name="value">
        /// La combinaison des énumérations a retourné.
        /// </param>
        /// <returns>
        /// Eléments de l'énumération "Flag" combiné dans la valeur passé en paramètres
        /// </returns>
        /// <example>
        /// Affiche les valeurs CultureInvariant et Multiline.
        /// <code>
        /// RegexOptions ro = RegexOptions.CultureInvariant | RegexOptions.Multiline;
        /// foreach (RegexOptions item in ro.GetAllSelectedItems&lt;RegexOptions&gt;())
        /// {
        ///    Console.WriteLine(item);
        /// }
        /// </code>
        /// </example>
        public static IEnumerable<T> GetAllSelectedItems<T>(this System.Enum value)
        {
            int valueAsInt = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);

            foreach (object item in System.Enum.GetValues(typeof(T)))
            {
                int itemAsInt = System.Convert.ToInt32(item, CultureInfo.InvariantCulture);

                if (itemAsInt == (valueAsInt & itemAsInt))
                {
                    yield return (T)item;
                }
            }
        }

        /// <summary>
        /// Obtient le nombre d'éléments d'une énumération.
        /// </summary>
        /// <typeparam name="T">
        /// Type de l'énumération.
        /// </typeparam>
        /// <param name="value">
        /// Une valeur de l'énumération.
        /// </param>
        /// <returns>
        /// Le nombre d'éléments de l'énumération sous forme de collection
        /// </returns>
        public static int Count<T>(this System.Enum value) where T : struct
        {
            return System.Enum.GetNames(typeof(T)).Length;
        }
    }
}