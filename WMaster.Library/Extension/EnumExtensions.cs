using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.Extension
{
    using System;
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
        public static bool Contains(this Enum value, Enum search)
        {
            int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            int searchAsInt = Convert.ToInt32(search, CultureInfo.InvariantCulture);

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
        public static IEnumerable<T> GetAllItems<T>(this Enum value) where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        /// <summary>
        /// Retourne tous les éléments de l'énumération "Flag" combiné dans la valeur passé en paramètres.
        /// Attention!! L'énumération doit posédé l'attribut <see cref="FlagsAttribute"/> et chaque valeur de l'énumération doit être une puissance de 2 distinct.
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
        public static IEnumerable<T> GetAllSelectedItems<T>(this Enum value)
        {
            int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);

            foreach (object item in Enum.GetValues(typeof(T)))
            {
                int itemAsInt = Convert.ToInt32(item, CultureInfo.InvariantCulture);

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
        public static int Count<T>(this Enum value) where T : struct
        {
            return Enum.GetNames(typeof(T)).Length; ;
        }
    }
}