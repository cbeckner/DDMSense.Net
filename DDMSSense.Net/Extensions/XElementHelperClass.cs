#region usings

#endregion usings

namespace DDMSense.Extensions
{
    #region usings

    using System;
    using System.Xml.Linq;

    #endregion usings

    public static class XElementHelperClass
    {
        public static string GetPrefix(this XElement element)
        {
            return element.GetPrefixOfNamespace(element.Name.Namespace);
        }

        /// <summary>
        ///     Returns an empty string in place of a null one.
        /// </summary>
        /// <param name="string"> the string to convert, if null </param>
        /// <returns> an empty string if the string is null, or the string untouched </returns>
        public static string ToNonNullString(this XElement el)
        {
            if (el == null) return string.Empty;
            if (String.IsNullOrEmpty(el.Value.ToString())) return string.Empty;
            return el.Value;
        }
    }

    public static class XAttributeHelperClass
    {
        public static string GetPrefix(this XAttribute attribute)
        {
            if (attribute.Parent != null)
                return attribute.Parent.GetPrefixOfNamespace(attribute.Name.Namespace);
            return string.Empty;
        }

        /// <summary>
        ///     Returns an empty string in place of a null one.
        /// </summary>
        /// <param name="string"> the string to convert, if null </param>
        /// <returns> an empty string if the string is null, or the string untouched </returns>
        public static string ToNonNullString(this XAttribute attr)
        {
            if (attr == null) return string.Empty;
            if (String.IsNullOrEmpty(attr.Value.ToString())) return string.Empty;
            return attr.Value;
        }
    }
}