using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using Reserva.Infra.Attributes;

namespace Reserva.Infra.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        ///     Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var cultureInfo = new CultureInfo(cultureInfoName);
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        ///     Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace(str) ? str : cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        //public static string ToHtml(this string value)
        //{
        //    if (string.IsNullOrEmpty(value)) return value;
        //    return System.Web.HttpUtility.HtmlEncode(value);
        //}

        public static string Left(this string value, int left)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substring(0, left);
        }

        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substring(value.Length - length);
        }

        public static string SemCaracteresEspeciais(this string s)
        {
            /* Troca os caracteres acentuados por não acentuados */
            string[] acentos =
            {
                "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È",
                "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â",
                "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û"
            };
            string[] semAcento =
            {
                "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E",
                "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a",
                "e", "i", "o", "u", "A", "E", "I", "O", "U"
            };
            for (var i = 0; i < acentos.Length; i++) s = s.Replace(acentos[i], semAcento[i]);
            /* Troca os caracteres especiais da string por "" */
            string[] caracteresEspeciais = {".", "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°"};
            for (var i = 0; i < caracteresEspeciais.Length; i++) s = s.Replace(caracteresEspeciais[i], "");
            /* Troca os espaços no início por "" */
            s = s.Replace("^\\s+", "");
            /* Troca os espaços no início por "" */
            s = s.Replace("\\s+$", "");
            /* Troca os espaços duplicados, tabulações e etc por  " " */
            s = s.Replace("\\s+", " ");
            return s;
        }

        public static bool IsNumeric(this string s)
        {
            float _;
            return float.TryParse(s, out _);
        }


        public static string ApenasNumeros(this string s)
        {
            var stringBuilder = new StringBuilder();
            foreach (var c in s)
                if (char.IsNumber(c))
                    stringBuilder.Append(c);

            return stringBuilder.ToString();
        }

        public static string LINQString(this string s)
        {
            var normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < normalizedString.Length; i++)
            {
                var c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().ToLower();
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string ReplaceAllRepleceable<T>(this string s, T dados)
        {
            var properties = typeof(T).GetProperties();
            var ret = s;

            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<ReplaceableAttribute>();

                if (attr == null) continue;

                var value = property.GetValue(dados, null);
                ret = ret.Replace($"[{property.Name}]", value?.ToString());
            }

            return ret;
        }
    }
}