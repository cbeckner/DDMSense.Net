using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DDMSSense.Extensions;
/* Copyright 2010 - 2013 by Brian Uri!
   
   This file is part of DDMSence.
   
   This library is free software; you can redistribute it and/or modify
   it under the terms of version 3.0 of the GNU Lesser General Public 
   License as published by the Free Software Foundation.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
   GNU Lesser General Public License for more details.
   
   You should have received a copy of the GNU Lesser General Public 
   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

   You can contact the author at ddmsence@urizone.net. The DDMSence
   home page is located at http://ddmsence.urizone.net/
*/
namespace DDMSSense.Util
{
    using Attribute = System.Xml.Linq.XAttribute;
    using Document = System.Xml.Linq.XDocument;
    using Element = System.Xml.Linq.XElement;
    using Node = System.Xml.Linq.XNode;
    using ParsingException = System.Xml.XmlException;
    using XSLException = System.Xml.Xsl.XsltException;
    using XSLTransform = System.Xml.Xsl.XslTransform;
    using IDDMSComponent = DDMSSense.DDMS.IDDMSComponent;
    using InvalidDDMSException = DDMSSense.DDMS.InvalidDDMSException;
    using Notice = DDMSSense.DDMS.SecurityElements.Ism.Notice;
    using Access = DDMSSense.DDMS.SecurityElements.Ntk.Access;
    using Point = DDMSSense.DDMS.Summary.Gml.Point;
    using Polygon = DDMSSense.DDMS.Summary.Gml.Polygon;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// A collection of static utility methods.
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class Util
    {

        private static XSLTransform _schematronIncludeTransform;
        private static XSLTransform _schematronAbstractTransform;
        private static IDictionary<string, XSLTransform> _schematronSvrlTransforms = new Dictionary<string, XSLTransform>();

        private static readonly Dictionary<string, string> XML_SPECIAL_CHARS = new Dictionary<string, string>();
        static Util()
        {
            XML_SPECIAL_CHARS.Add("&", "&amp;");
            XML_SPECIAL_CHARS.Add("\"", "&quot;");
            XML_SPECIAL_CHARS.Add("'", "&apos;");
            XML_SPECIAL_CHARS.Add("<", "&lt;");
            XML_SPECIAL_CHARS.Add(">", "&gt;");
        }

        private const string DDMS_DATE_HOUR_MIN_PATTERN = "[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}(Z|[\\-\\+][0-9]{2}:[0-9]{2})?";

        private static List<XmlQualifiedName> DATE_DATATYPES = new List<XmlQualifiedName>();

        /// <summary>
        /// Private to prevent instantiation.
        /// </summary>
        private Util()
        {
        }

        /// <summary>
        /// Returns an empty string in place of a null one.
        /// </summary>
        /// <param name="string"> the string to convert, if null </param>
        /// <returns> an empty string if the string is null, or the string untouched </returns>
        public static string GetNonNullString(string @string)
        {
            return (@string == null ? "" : @string);
        }

        /// <summary>
        /// Helper method to convert an xs:NMTOKENS data type into a List of Strings.
        /// 
        /// <para>
        /// The number of items returned is based on the normalization of the whitespace first. So, an xs:list defined as
        /// "a   b" will return a List of 2 Strings ("a", "b"), and not a List of 4 String ("a", "", "", "b")
        /// </para>
        /// </summary>
        /// <param name="value"> the xs:list style String to parse </param>
        /// <returns> a List (never null) </returns>
        public static List<string> GetXsListAsList(string value)
        {
            if (String.IsNullOrEmpty(value)) return new List<string>();
            return value.Split(' ').Where(s => !String.IsNullOrEmpty(s)).ToList();
        }

        /// <summary>
        /// Converts a list of objects into a space-delimited xs:list, using the object's toString() implementation
        /// </summary>
        /// <param name="list"> the list to convert </param>
        /// <returns> a space-delimited string, or empty string if the list was empty. </returns>
        public static string GetXsList<T1>(List<T1> list)
        {
            if (list == null)
            {
                return ("");
            }
            StringBuilder buffer = new StringBuilder();
            foreach (object @string in list)
            {
                buffer.Append(@string).Append(" ");
            }
            return (buffer.ToString().Trim());
        }

        /// <summary>
        /// Returns an int value for a boolean, for use in a hashCode function.
        /// </summary>
        /// <param name="b">	the boolean </param>
        /// <returns> 1 for true and 0 for false </returns>
        public static int BooleanHashCode(bool b)
        {
            return (b ? 1 : 0);
        }

        /// <summary>
        /// Checks if all of the entries in a list of Strings is empty or null.
        /// </summary>
        /// <param name="list"> the list containing strings </param>
        /// <returns> true if the list only has null or empty values </returns>
        public static bool ContainsOnlyEmptyValues(List<string> list)
        {
            return list.Any(s => !String.IsNullOrEmpty(s));
        }

        /// <summary>
        /// Gets the child text of the first child element matching the name in the DDMS namespace.
        /// </summary>
        /// <param name="parent"> the parent element </param>
        /// <param name="name"> the name of the child element </param>
        /// <returns> the child text of the first discovered child element </returns>
        public static string GetFirstDDMSChildValue(Element parent, string name)
        {
            Util.RequireValue("parent element", parent);
            Util.RequireValue("child name", name);
            if (!DDMSVersion.IsSupportedDDMSNamespace(parent.Name.NamespaceName))
            {
                throw new System.ArgumentException("This method should only be called on an element in the DDMS namespace.");
            }
            Element child = parent.Element(XName.Get(name, parent.Name.NamespaceName));
            return (child == null ? "" : child.Value);
        }

        /// <summary>
        /// Gets the child text of any child elements in the DDMS namespace and returns them as a list.
        /// </summary>
        /// <param name="parent"> the parent element </param>
        /// <param name="name"> the name of the child element </param>
        /// <returns> a List of strings, where each string is child text of matching elements </returns>
        public static List<string> GetDDMSChildValues(Element parent, string name)
        {
            Util.RequireValue("parent element", parent);
            Util.RequireValue("child name", name);
            if (!DDMSVersion.IsSupportedDDMSNamespace(parent.Name.NamespaceName))
            {
                throw new System.ArgumentException("This method should only be called on an element in the DDMS namespace.");
            }
            List<string> childTexts = new List<string>();
            IEnumerable<Element> childElements = parent.Elements(XName.Get(name, parent.Name.NamespaceName));
            foreach (var el in childElements)
            {
                childTexts.Add(el.Value);
            }
            return (childTexts);
        }

        /// <summary>
        /// Asserts that a value required for DDMS is not null or empty.
        /// </summary>
        /// <param name="description">	a descriptive name of the value </param>
        /// <param name="value">			the value to check </param>
        /// <exception cref="InvalidDDMSException"> if the value is null or empty </exception>
        public static void RequireDDMSValue(string description, object value)
        {
            if (value == null || (value is string && String.IsNullOrEmpty((string)value)))
            {
                throw new InvalidDDMSException(description + " is required.");
            }
        }

        /// <summary>
        /// Asserts that a date format is one of the 5 types accepted by DDMS.
        /// </summary>
        /// <param name="date"> the date in its raw XML format </param>
        /// <param name="ddmsNamespace"> the DDMS namespace of this date (DDM 4.0.1 and earlier only support 4 types). </param>
        /// <exception cref="InvalidDDMSException"> if the value is invalid. Does nothing if value is null. </exception>
        public static void RequireDDMSDateFormat(string date, string ddmsNamespace)
        {
            DDMSVersion version = DDMSVersion.GetVersionForNamespace(ddmsNamespace);

            if (version.IsAtLeast("4.1") && System.Text.RegularExpressions.Regex.Matches(date, DDMS_DATE_HOUR_MIN_PATTERN).Count > 0)
            {
                return;
            }

            bool isXsdType = false;
            try
            {
                DateTime calendar = DateTime.Parse(date);
                isXsdType = DATE_DATATYPES.Contains(calendar.XMLSchemaType);
            }
            catch (System.ArgumentException)
            {
                // Fall-through 
            }
            if (!isXsdType)
            {
                string message = "The date datatype must be one of " + DATE_DATATYPES;
                if (version.IsAtLeast("4.1"))
                {
                    message += " or ddms:DateHourMinType";
                }
                throw new InvalidDDMSException(message);
            }
        }

        /// <summary>
        /// Asserts that the qualified name of an element matches the expected name and a supported version of the 
        /// DDMS XML namespace
        /// </summary>
        /// <param name="element"> the element to check </param>
        /// <param name="localName"> the local name to compare to </param>
        /// <exception cref="InvalidDDMSException"> if the name is incorrect </exception>
        public static void RequireDDMSQName(Element element, string localName)
        {
            Util.RequireValue("element", element);
            Util.RequireValue("local name", localName);
            if (!localName.Equals(element.Name.LocalName) || !DDMSVersion.IsSupportedDDMSNamespace(element.Name.NamespaceName))
            {
                throw new InvalidDDMSException("Unexpected namespace URI and local name encountered: " + element.Name);
            }
        }

        /// <summary>
        /// Asserts that the qualified name of an element matches the expected name and namespace URI
        /// </summary>
        /// <param name="element"> the element to check </param>
        /// <param name="namespaceURI"> the namespace to check </param>
        /// <param name="localName"> the local name to compare to </param>
        /// <exception cref="IllegalArgumentException"> if the name is incorrect </exception>
        public static void RequireQualifiedName(Element element, string namespaceURI, string localName)
        {
            Util.RequireValue("element", element);
            Util.RequireValue("local name", localName);
            if (namespaceURI == null)
            {
                namespaceURI = "";
            }
            if (!localName.Equals(element.Name.LocalName) || !namespaceURI.Equals(element.Name.NamespaceName))
            {
                throw new InvalidDDMSException("Unexpected namespace URI and local name encountered: " + element.Name);
            }
        }

        /// <summary>
        /// Asserts that a value required, for general cases.
        /// </summary>
        /// <param name="description">	a descriptive name of the value </param>
        /// <param name="value">			the value to check </param>
        /// <exception cref="IllegalArgumentException"> if the value is null or empty </exception>
        public static void RequireValue(string description, object value)
        {
            if (value == null || (value is string && String.IsNullOrEmpty((string)value)))
            {
                throw new System.ArgumentException(description + " is required.");
            }
        }

        /// <summary>
        /// Checks that the number of child elements with the given name in the same namespace as the parent are bounded.
        /// </summary>
        /// <param name="parent">		the parent element </param>
        /// <param name="childName">		the local name of the child </param>
        /// <param name="lowBound">		the lowest value the number can be </param>
        /// <param name="highBound">		the highest value the number can be </param>
        /// <exception cref="InvalidDDMSException"> if the number is out of bounds </exception>
        public static void RequireBoundedChildCount(Element parent, string childName, int lowBound, int highBound)
        {
            Util.RequireValue("parent element", parent);
            Util.RequireValue("child name", childName);
            int childCount = parent.Elements(XName.Get(childName, parent.Name.NamespaceName)).Count();
            if (!IsBounded(childCount, lowBound, highBound))
            {
                StringBuilder error = new StringBuilder();
                if (lowBound == highBound)
                {
                    error.Append("Exactly ").Append(highBound).Append(" ").Append(childName).Append(" element");
                    if (highBound != 1)
                    {
                        error.Append("s");
                    }
                    error.Append(" must exist.");
                }
                else if (lowBound == 0)
                {
                    error.Append("No more than ").Append(highBound).Append(" ").Append(childName).Append(" element");
                    if (highBound != 1)
                    {
                        error.Append("s");
                    }
                    error.Append(" can exist.");
                }
                else
                {
                    error.Append("The number of ").Append(childName).Append(" elements must be between ").Append(lowBound).Append(" and ").Append(highBound).Append(".");
                }
                throw new InvalidDDMSException(error.ToString());
            }
        }

        /// <summary>
        /// Validates that a list of strings contains NCNames. This method uses the built-in Verifier in XOM by attempting to
        /// create a new Element with the test string as a local name (Local names must be NCNames).
        /// </summary>
        /// <param name="names"> a list of names to check </param>
        /// <exception cref="InvalidDDMSException"> if any name is not an NCName. </exception>
        public static void RequireValidNCNames(List<string> names)
        {
            if (names == null)
            {
                names = new List<string>();
            }
            foreach (string name in names)
            {
                RequireValidNCName(name);
            }
        }

        /// <summary>
        /// Validates that a child component has a compatible DDMS version as the parent.
        /// </summary>
        /// <param name="parent"> the parent component </param>
        /// <param name="child"> the child component </param>
        /// <exception cref="InvalidDDMSException"> if  </exception>
        public static void RequireCompatibleVersion(IDDMSComponent parent, IDDMSComponent child)
        {
            Util.RequireValue("parent", parent);
            Util.RequireValue("child", child);
            // Cover acceptable case where parent (e.g. BoundingGeometry) has different XML namespace than child.
            string parentNamespace = parent.Namespace;
            if (child is Polygon || child is Point)
            {
                parentNamespace = DDMSVersion.GetVersionForNamespace(parentNamespace).GmlNamespace;
            }
            if (child is Access)
            {
                parentNamespace = DDMSVersion.GetVersionForNamespace(parentNamespace).NtkNamespace;
            }
            if (child is Notice)
            {
                parentNamespace = DDMSVersion.GetVersionForNamespace(parentNamespace).IsmNamespace;
            }
            string childNamespace = child.Namespace;
            if (!parentNamespace.Equals(childNamespace))
            {
                throw new InvalidDDMSException("A child component, " + child.Name + ", is using a different version of DDMS from its parent.");
            }
        }

        /// <summary>
        /// Validates that a string is an NCName. This method relies on Saxon's library
        /// methods.
        /// </summary>
        /// <param name="name"> the name to check </param>
        /// <exception cref="InvalidDDMSException"> if the name is not an NCName. </exception>
        public static void RequireValidNCName(string name)
        {
            if (!((new Name10Checker()).isValidNCName(GetNonNullString(name))))
            {
                throw new InvalidDDMSException("\"" + name + "\" is not a valid NCName.");
            }
        }

        /// <summary>
        /// Validates that a string is an NMTOKEN. This method relies on Saxon's library
        /// methods.
        /// </summary>
        /// <param name="name"> the name to check </param>
        /// <exception cref="InvalidDDMSException"> if the name is not an NMTOKEN. </exception>
        public static void RequireValidNMToken(string name)
        {
            if (!((new Name10Checker()).isValidNmtoken(GetNonNullString(name))))
            {
                throw new InvalidDDMSException("\"" + name + "\" is not a valid NMTOKEN.");
            }
        }

        /// <summary>
        /// Checks that a string is a valid URI.
        /// </summary>
        /// <param name="uri">	the string to test </param>
        /// <exception cref="InvalidDDMSException"> if the string cannot be built into a URI </exception>
        public static void RequireDDMSValidURI(string uri)
        {
            Util.RequireValue("uri", uri);
            try
            {
                new Uri(uri);
            }
            catch (UriFormatException e)
            {
                throw new InvalidDDMSException(e);
            }
        }

        /// <summary>
        /// Validates a longitude value
        /// </summary>
        /// <param name="value"> the value to test </param>
        /// <exception cref="InvalidDDMSException"> </exception>
        public static void RequireValidLongitude(double? value)
        {
            if (value == null || (-180L).CompareTo(value) > 0 || (180L).CompareTo(value) < 0)
            {
                throw new InvalidDDMSException("A longitude value must be between -180 and 180 degrees: " + value);
            }
        }

        /// <summary>
        /// Validates a latitude value
        /// </summary>
        /// <param name="value"> the value to test </param>
        /// <exception cref="InvalidDDMSException"> </exception>
        public static void RequireValidLatitude(double? value)
        {
            if (value == null || (-90L).CompareTo(value) > 0 || (90L).CompareTo(value) < 0)
            {
                throw new InvalidDDMSException("A latitude value must be between -90 and 90 degrees: " + value);
            }
        }

        /// <summary>
        /// Checks that a number is between two values, inclusive
        /// </summary>
        /// <param name="testCount">	the number to evaluate </param>
        /// <param name="lowBound"> the lowest value the number can be </param>
        /// <param name="highBound"> the highest value the number can be </param>
        /// <returns> true if the number is bounded, false otherwise </returns>
        /// <exception cref="IllegalArgumentException"> if the range is invalid. </exception>
        public static bool IsBounded(int testCount, int lowBound, int highBound)
        {
            if (lowBound > highBound)
            {
                throw new System.ArgumentException("Invalid number range: " + lowBound + " to " + highBound);
            }
            return (testCount >= lowBound && testCount <= highBound);
        }

        /// <summary>
        /// Checks if two lists of Objects are identical. Returns true if the lists are the same length and each indexed
        /// string also exists at the same index in the other list.
        /// </summary>
        /// <param name="list1"> the first list </param>
        /// <param name="list2"> the second list </param>
        /// <returns> true if the lists are of equal.Count and contain the same objects, false otherwise. </returns>
        /// <exception cref="IllegalArgumentException"> if one of the lists is null </exception>
        public static bool ListEquals<T1, T2>(List<T1> list1, List<T2> list2)
        {
            if (list1 == null || list2 == null)
            {
                throw new System.ArgumentException("Null lists cannot be compared.");
            }
            if (list1.Equals(list2))
            {
                return (true);
            }
            if (list1.Count != list2.Count)
            {
                return (false);
            }
            for (int i = 0; i < list1.Count; i++)
            {
                object value1 = list1[i];
                object value2 = list2[i];
                if (!NullEquals(value1, value2))
                {
                    return (false);
                }
            }
            return (true);
        }

        /// <summary>
        /// Checks object equality when the objects could possible be null.
        /// </summary>
        /// <param name="obj1"> the first object </param>
        /// <param name="obj2"> the second object </param>
        /// <returns> true if both objects are null or obj1 equals obj2, false otherwise </returns>
        public static bool NullEquals(object obj1, object obj2)
        {
            return (obj1 == null ? obj2 == null : obj1.Equals(obj2));
        }

        /// <summary>
        /// Replaces XML special characters - '&', '<', '>', '\'', '"' 
        /// </summary>
        /// <param name="input"> the string to escape. </param>
        /// <returns> escaped String </returns>
        public static string XmlEscape(string input)
        {
            if (input != null)
            {
                for (IEnumerator<string> iterator = XML_SPECIAL_CHARS.Keys.GetEnumerator(); iterator.MoveNext(); )
                {
                    string pattern = iterator.Current;
                    input = System.Text.RegularExpressions.Regex.Replace(input,pattern,(string)XML_SPECIAL_CHARS[pattern]);
                }
            }
            return input;
        }

        /// <summary>
        /// Capitalizes the first letter of a String. Silently does nothing if the string is null, empty, or not a letter.
        /// </summary>
        /// <param name="string">	the string to capitalize </param>
        /// <returns> the capitalized string </returns>
        public static string Capitalize(string @string)
        {
            if (String.IsNullOrEmpty(@string))
            {
                return (@string);
            }
            if (@string.Length == 1)
            {
                return (@string.ToUpper());
            }
            return (@string.Substring(0, 1).ToUpper() + @string.Substring(1, @string.Length - 1));
        }

        /// <summary>
        /// Helper method to add a ddms attribute to an element. Will not add the attribute if the value
        /// is empty or null. This method uses the DDMS namespace defined with DDMSVersion.getCurrentVersion().
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <param name="attributeName"> the name of the attribute (will be within the DDMS namespace) </param>
        /// <param name="attributeValue"> the value of the attribute </param>
        public static void AddDDMSAttribute(Element element, string attributeName, string attributeValue)
        {
            AddAttribute(element, PropertyReader.GetPrefix("ddms"), attributeName, DDMSVersion.GetCurrentVersion().Namespace, attributeValue);
        }

        /// <summary>
        /// Helper method to add an attribute to an element. Will not add the attribute if the value
        /// is empty or null.
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <param name="prefix"> the prefix to use (without a trailing colon) </param>
        /// <param name="attributeName"> the name of the attribute </param>
        /// <param name="namespaceURI"> the namespace this attribute is in </param>
        /// <param name="attributeValue"> the value of the attribute </param>
        public static void AddAttribute(Element element, string prefix, string attributeName, string namespaceURI, string attributeValue)
        {
            if (!String.IsNullOrEmpty(attributeValue))
            {
                element.Add(Util.BuildAttribute(prefix, attributeName, namespaceURI, attributeValue));
            }
        }

        /// <summary>
        /// Helper method to add a ddms child element to an element. Will not add if the value
        /// is empty or null.
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <param name="childName"> the name of the child (will be within the DDMS namespace) </param>
        /// <param name="childValue"> the value of the attribute </param>
        public static void AddDDMSChildElement(Element element, string childName, string childValue)
        {
            if (!String.IsNullOrEmpty(childValue))
            {
                element.Add(Util.BuildDDMSElement(childName, childValue));
            }
        }

        /// <summary>
        /// Convenience method to create an element in the default DDMS namespace with some child text. 
        /// The resultant element will use the DDMS prefix and have no attributes or children (yet).
        /// </summary>
        /// <param name="name"> the local name of the element </param>
        /// <param name="childText"> the text of the element (optional) </param>
        public static Element BuildDDMSElement(string name, string childText)
        {
            return (BuildElement(PropertyReader.GetPrefix("ddms"), name, DDMSVersion.GetCurrentVersion().Namespace, childText));
        }

        /// <summary>
        /// Convenience method to create an element in a namespace with some child text. 
        /// The resultant element will use a custom prefix and have no attributes or children (yet).
        /// </summary>
        /// <param name="prefix"> the prefix to use (without a trailing colon) </param>
        /// <param name="name"> the local name of the element </param>
        /// <param name="namespaceURI"> the namespace this element is in </param>
        /// <param name="childText"> the text of the element (optional) </param>
        public static Element BuildElement(string prefix, string name, string namespaceURI, string childText)
        {
            Util.RequireValue("name", name);
            prefix = (String.IsNullOrEmpty(prefix) ? "" : prefix + ":");
            Element element = new Element(prefix + name, namespaceURI);
            if (!String.IsNullOrEmpty(childText))
            {
                element.Add(childText);
            }
            return (element);
        }

        /// <summary>
        /// Convenience method to create an attribute in the default DDMS namespace. The resultant attribute will use the
        /// DDMS prefix and have the provided value.
        /// </summary>
        /// <param name="name"> the local name of the attribute </param>
        /// <param name="value"> the value of the attribute </param>
        public static Attribute BuildDDMSAttribute(string name, string value)
        {
            return (BuildAttribute(PropertyReader.GetPrefix("ddms"), name, DDMSVersion.GetCurrentVersion().Namespace, value));
        }

        /// <summary>
        /// Convenience method to create an attribute in a namespace. 
        /// </summary>
        /// <param name="prefix"> the prefix to use (without a trailing colon) </param>
        /// <param name="name"> the local name of the attribute </param>
        /// <param name="namespaceURI"> the namespace this attribute is in </param>
        /// <param name="value"> the value of the attribute </param>
        public static Attribute BuildAttribute(string prefix, string name, string namespaceURI, string value)
        {
            RequireValue("name", name);
            RequireValue("value", value);
            prefix = (String.IsNullOrEmpty(prefix) ? "" : prefix + ":");
            if (namespaceURI == null)
            {
                namespaceURI = "";
            }
            return (new Attribute(XName.Get(prefix + name, namespaceURI), value));
        }

        /// <summary>
        /// Loads a XOM object tree from an input stream. This method does no schema validation.
        /// </summary>
        /// <param name="inputStream"> the input stream containing the XML document </param>
        /// <returns> a XOM Document </returns>
        /// <exception cref="IOException"> if there are problems loading or parsing the input stream </exception>
        public static Document BuildXmlDocument(Stream inputStream)
        {
            Util.RequireValue("input stream", inputStream);
            try
            {
                return XDocument.Load(inputStream);
            }
            catch (ParsingException e)
            {
                throw new IOException(e.Message);
            }
        }

        /// <summary>
        /// Locates the queryBinding attribute in an ISO Schematron file and returns it.
        /// </summary>
        /// <param name="schDocument"> the Schematron file as an XML Document </param>
        /// <returns> the value of the queryBinding attribute, or "xslt" if undefined. </returns>
        /// <exception cref="IOException"> if there are file-related problems with looking up the attribute </exception>
        public static string GetSchematronQueryBinding(Document schDocument)
        {
            Attribute attr = schDocument.Root.Attribute("queryBinding");
            return (attr == null ? "xslt" : attr.Value);
        }

        /// <summary>
        /// Takes a Schematron file and transforms it with the ISO Schematron skeleton files.
        /// 
        /// <ol>
        /// <li>The schema is preprocessed with iso_dsdl_include.xsl.</li>
        /// <li>The schema is preprocessed with iso_abstract_expand.xsl.</li>
        /// <li>The schema is compiled with iso_svrl_for_xslt1.xsl.</li>
        /// </ol>
        /// 
        /// <para>The XSLTransform instance using the result of the processing is returned. This XSLTransform can then be used
        /// to validate DDMS components.</para>
        /// </summary>
        /// <param name="schematronFile"> the Schematron file </param>
        /// <returns> the XSLTransform instance </returns>
        /// <exception cref="IOException"> if there are file-related problems with preparing the stylesheets </exception>
        /// <exception cref="XSLException"> if stylesheet transformation fails </exception>
        public static XSLTransform BuildSchematronTransform(string schematronFile)
        {
            Document schDocument = Util.BuildXmlDocument(File.OpenRead(schematronFile));
            string queryBinding = GetSchematronQueryBinding(schDocument);

            //		long time = new Date().getTime();
            XSLTransform phase1 = SchematronIncludeTransform;
            //		System.out.println((new Date().getTime() - time) + "ms (Include)");

            //		time = new Date().getTime();
            XSLTransform phase2 = SchematronAbstractTransform;
            //		System.out.println((new Date().getTime() - time) + "ms (Abstract)");

            //		time = new Date().getTime();
            XSLTransform phase3 = GetSchematronSvrlTransform(queryBinding);
            //		System.out.println((new Date().getTime() - time) + "ms (SVRL)");

            //		time = new Date().getTime();
            Node nodes = phase3.Transform(phase2.Transform(phase1.Transform(schDocument)));
            //		System.out.println((new Date().getTime() - time) + "ms (Base transformation 1, 2, 3)");

            //		time = new Date().getTime();
            XSLTransform finalTransform = new XSLTransform(XSLTransform.toDocument(nodes));
            //		System.out.println((new Date().getTime() - time) + "ms (Schematron Validation)");

            return (finalTransform);
        }

        /// <summary>
        /// Clears any previous instantiated transforms.
        /// </summary>
        private static void ClearTransformCaches()
        {
            lock (typeof(Util))
            {
                _schematronIncludeTransform = null;
                _schematronAbstractTransform = null;
                _schematronSvrlTransforms.Clear();
            }
        }

        /// <summary>
        /// Lazy instantiation / cached accessor for the first step of Schematron validation.
        /// </summary>
        /// <returns> the phase one transform </returns>
        private static XSLTransform SchematronIncludeTransform
        {
            get
            {
                lock (typeof(Util))
                {
                    if (_schematronIncludeTransform == null)
                    {
                        Stream includeStylesheet = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream("schematron/iso_dsdl_include.xsl");
                        _schematronIncludeTransform = new XSLTransform(Util.BuildXmlDocument(includeStylesheet));
                    }
                    return (_schematronIncludeTransform);
                }
            }
        }

        /// <summary>
        /// Lazy instantiation / cached accessor for the second step of Schematron validation.
        /// </summary>
        /// <returns> the phase two transform </returns>
        private static XSLTransform SchematronAbstractTransform
        {
            get
            {
                lock (typeof(Util))
                {
                    if (_schematronAbstractTransform == null)
                    {
                        Stream abstractStylesheet = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream("schematron/iso_abstract_expand.xsl");
                        _schematronAbstractTransform = new XSLTransform(Util.BuildXmlDocument(abstractStylesheet));
                    }
                    return (_schematronAbstractTransform);
                }
            }
        }

        /// <summary>
        /// Lazy instantiation / cached accessor for the third step of Schematron validation, using XSLT1 or XSLT2
        /// </summary>
        /// <param name="queryBinding"> the queryBinding value of the Schematron file. Currently "xslt" or "xslt2" are supported. </param>
        /// <returns> the phase three transform </returns>
        /// <exception cref="IllegalArgumentException"> if the queryBinding is unsupported </exception>
        private static XSLTransform GetSchematronSvrlTransform(string queryBinding)
        {
            lock (typeof(Util))
            {
                string resourceName;
                if ("xslt2".Equals(queryBinding))
                {
                    resourceName = "schematron/iso_svrl_for_xslt2.xsl";
                }
                else if ("xslt".Equals(queryBinding))
                {
                    resourceName = "schematron/iso_svrl_for_xslt1.xsl";
                }
                else
                {
                    throw new System.ArgumentException("DDMSence currently only supports Schematron files with a queryBinding attribute of \"xslt\" or \"xslt2\".");
                }
                if (_schematronSvrlTransforms.GetValueOrNull(resourceName) == null)
                {
                    try
                    {
                        Stream schematronStylesheet = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
                        Document svrlStylesheet = Util.BuildXmlDocument(schematronStylesheet);

                        // XOM passes the Base URI to Xalan as the SystemId, which cannot be empty.
                        Uri svrlUri = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceInfo(resourceName).ResourceLocation;
                        svrlStylesheet.BaseUri = svrlUri.ToString();

                        _schematronSvrlTransforms[resourceName] = new XSLTransform(svrlStylesheet);
                    }
                    catch (UriFormatException e)
                    {
                        throw new IOException(e.Message);
                    }
                }
                return (_schematronSvrlTransforms.GetValueOrNull(resourceName));
            }
        }
    }
}