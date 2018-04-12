using System;
using System.Globalization;
using System.Text;

namespace QualisysExtensions.String
{
    /// <summary>
    ///     Extensión para facilitar el uso de textos
    /// </summary>
    /// <remarks>
    ///     Raul Anaya, 11/01/2018
    /// </remarks>
    public static class StringExtension
    {
        /// <summary>
        ///     Método para quitar diacríticos
        /// </summary>
        /// <param name="pStrString">
        ///     Texto
        /// </param>
        /// <returns>
        ///     Texto normalizado
        /// </returns>
        public static string RemoveDiacritics(this string pStrString)
        {
            string lObjStrNormalizedString = pStrString.Normalize(NormalizationForm.FormD);
            StringBuilder lObjStringBuilder = new StringBuilder();

            for (int i = 0; i < lObjStrNormalizedString.Length; i++)
            {
                Char lObjChar = lObjStrNormalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(lObjChar) != UnicodeCategory.NonSpacingMark)
                {
                    lObjStringBuilder.Append(lObjChar);
                } 
            }

            return lObjStringBuilder.ToString();
        }
    }
}
