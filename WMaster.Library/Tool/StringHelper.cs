// TODO : Translate comment
namespace WMaster.Tool
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Classse static d'extention de fonctionnalitées de chaines de caractères.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Supprime les caractères accentuées d'une chaine de caractères
        /// </summary>
        /// <param name="str">Une chaine avec ou sans caractères accentuées.</param>
        /// <returns>Une chaines ou chaque caractères accentuées a été remplacé par son caractère de base.</returns>
        public static string RemoveDiacritics(this string str)
        {
            string formD = str.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sbNoDiacritics = new System.Text.StringBuilder();
            foreach (char c in formD)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sbNoDiacritics.Append(c);
            }
            string noDiacritics = sbNoDiacritics.ToString().Normalize(System.Text.NormalizationForm.FormC);
            return noDiacritics;
        }

        /// <summary>
        /// Retourne une chaine de caractère prète pour comparaison en base : sans accents, sans espages et sans ponctuations.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetForCompare(this string str)
        {
            string returnValue = str.RemoveDiacritics().ToUpper();
            returnValue = Regex.Replace(returnValue, @"[^0-9A-Z]", string.Empty);
            return returnValue;
        }

        /// <summary>
        /// Tronc la chaine a length caractères au plus.
        /// </summary>
        /// <param name="str">La chaine d'origine.</param>
        /// <param name="length">Le nombre maximum de caractères.</param>
        /// <returns>Une chaine de <paramref name="length"/> caractères au maximum.</returns>
        public static string MaxLength(string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            { return str; }

            if (length <= 0)
            { return string.Empty; }

            return str.Substring(0, Math.Min(str.Length, length));
        }
    }
}
