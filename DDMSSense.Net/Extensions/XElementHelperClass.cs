#region usings

#endregion usings

namespace DDMSense.Extensions
{
    #region usings

    using System.Xml.Linq;

    #endregion usings

    public static class XElementHelperClass
    {
        public static string GetPrefix(this XElement element)
        {
            return element.GetPrefixOfNamespace(element.Name.Namespace);
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
    }
}