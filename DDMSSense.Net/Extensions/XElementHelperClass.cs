#region usings



#endregion

namespace DDMSSense.Extensions
{
    #region usings

    using System.Xml.Linq;

    #endregion

    public static class XElementHelperClass
    {
        public static string GetPrefix(this XElement element)
        {
            return element.GetPrefixOfNamespace(element.Name.Namespace);
        }
    }
}