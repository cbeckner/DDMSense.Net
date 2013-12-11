#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS;
using DDMSSense.DDMS.Extensible;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Extensions;
using DDMSSense.Util;

#endregion

namespace DDMSSense
{
    /// <summary>
    ///     Top-level base class for all DDMS elements and attributes modeled as Java objects.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before
    ///         the component is used. It is assumed that after the constructor on a component has been called, the component
    ///         will be
    ///         well-formed and valid.
    ///     </para>
    
    
    /// </summary>
    public abstract class AbstractBaseComponent : IDDMSComponent
    {
        private XElement _element;
        private List<ValidationMessage> _warnings;

        /// <summary>
        ///     Empty constructor
        /// </summary>
        protected internal AbstractBaseComponent()
        {
        }

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        protected internal AbstractBaseComponent(XElement element)
        {
            try
            {
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for a collection of nested components. A list such as this is useful for bulk actions, such as checking
        ///     emptiness, equality, generating hash codes, or applying mass validation.
        /// </summary>
        protected internal virtual List<IDDMSComponent> NestedComponents
        {
            get { return new List<IDDMSComponent>(); }
        }

        /// <summary>
        ///     Returns the most recent compatible DDMSVersion for this component, based on the XML Namespace. Depends on the XOM
        ///     Element being set. For DDMS versions that share the same namespace (4.0.1 and 4.1), the newer version is always
        ///     returned.
        /// </summary>
        /// <returns> a version </returns>
        /// <exception cref="UnsupportedVersionException"> if the XML namespace is not one of the supported DDMS namespaces. </exception>
        protected internal virtual DDMSVersion DDMSVersion
        {
            get { return (DDMSVersion.GetVersionForNamespace(Namespace)); }
        }

        /// <summary>
        ///     Can be overridden to change the locator string used in warnings and errors.
        ///     <para>
        ///         For components such as Format, there are wrapper elements that are not implemented as Java objects. These
        ///         elements should be included in the XPath string used to identify the source of the error.
        ///     </para>
        ///     <para>
        ///         For example, if a ddms:extent element has a warning and the ddms:format element reports it, the locator
        ///         information should be "/ddms:format/ddms:Media/ddms:extent" and not the default of "/ddms:format/ddms:extent"
        ///     </para>
        /// </summary>
        /// <returns> an empty string, unless overridden. </returns>
        protected internal virtual string LocatorSuffix
        {
            get { return (""); }
        }

        /// <summary>
        ///     Accessor for the list of validation warnings.
        ///     <para>This is the private copy that should be manipulated during validation. Lazy initialization.</para>
        /// </summary>
        /// <returns> an editable list of warnings </returns>
        private List<ValidationMessage> Warnings
        {
            get
            {
                if (_warnings == null)
                    _warnings = new List<ValidationMessage>();

                return (_warnings);
            }
        }

        /// <summary>
        ///     Accessor for the XOM element representing this component
        /// </summary>
        protected internal virtual XElement Element
        {
            get { return _element; }
        }

        /// <summary>
        ///     Will return an empty string if the element is not set, but this cannot occur after instantiation.
        /// </summary>
        /// <see cref="IDDMSComponent#getPrefix()"></see>
        public virtual string Prefix
        {
            get { return (Element == null ? "" : Element.GetPrefix()); }
        }

        /// <summary>
        ///     Will return an empty string if the element is not set, but this cannot occur after instantiation.
        /// </summary>
        /// <see cref="IDDMSComponent#getName()"></see>
        public virtual string Name
        {
            get { return (Element == null ? "" : Element.Name.LocalName); }
        }

        /// <summary>
        ///     Will return an empty string if the element is not set, but this cannot occur after instantiation.
        /// </summary>
        /// <see cref="IDDMSComponent#getNamespace()"></see>
        public virtual string Namespace
        {
            get { return (Element == null ? "" : Element.Name.NamespaceName); }
        }

        /// <summary>
        ///     Will return an empty string if the element is not set, but this cannot occur after instantiation.
        /// </summary>
        /// <see cref="IDDMSComponent#getQualifiedName()"></see>
        public virtual string QualifiedName
        {
            get { return (Element == null ? "" : Element.Name.LocalName); }
        }

        /// <summary>
        ///     The base implementation of a DDMS component assumes that there are no security attributes. Components with
        ///     attributes should override this.
        /// </summary>
        public virtual SecurityAttributes SecurityAttributes
        {
            get { return (null); }
            set { value = null; }
        }

        /// <see cref="IDDMSComponent#getValidationWarnings()"></see>
        public virtual List<ValidationMessage> ValidationWarnings
        {
            get { return Warnings; }
        }

        /// <see cref="IDDMSComponent#toHTML()"></see>
        public virtual string ToHTML()
        {
            return (GetOutput(true, "", ""));
        }

        /// <see cref="IDDMSComponent#toText()"></see>
        public virtual string ToText()
        {
            return (GetOutput(false, "", ""));
        }

        /// <summary>
        ///     Will return an empty string if the name is not set, but this cannot occur after instantiation.
        /// </summary>
        /// <see cref="IDDMSComponent#toXML()"></see>
        public virtual string ToXML()
        {
            return (Element == null ? "" : Element.ToString());
        }

        /// <summary>
        ///     Accessor for a copy of the underlying XOM element
        /// </summary>
        public virtual XElement ElementCopy
        {
            get { return (new XElement(_element)); }
        }

        /// <summary>
        ///     Base case for validation. This method can be overridden for more in-depth validation. It is always assumed that
        ///     the subcomponents of a component are already valid.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>
        ///                     A name exists and is
        ///                     not empty.
        ///                 </li>
        ///                 <li>All child components use the same version of DDMS as this component.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal virtual void Validate()
        {
            Util.Util.RequireDDMSValue("name", Name);
            foreach (var nested in NestedComponents)
            {
                if (nested is ExtensibleElement || nested == null)
                    continue;

                Util.Util.RequireCompatibleVersion(this, nested);
            }
            ValidateWarnings();
        }

        /// <summary>
        ///     Base case for warnings. This method can be overridden for more in-depth validation. It is always assumed that the
        ///     subcomponents of a component are already valid.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>
        ///                     Adds any warnings
        ///                     from any nested components.
        ///                 </li>
        ///                 <li>Adds any warnings from any security attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal virtual void ValidateWarnings()
        {
            foreach (var nested in NestedComponents)
            {
                if (nested == null)
                    continue;
                AddWarnings(nested.ValidationWarnings, false);
            }
            if (SecurityAttributes != null)
                AddWarnings(SecurityAttributes.ValidationWarnings, true);
        }

        /// <summary>
        ///     Adds a warning about a component which is used in a valid manner, but may cause issuse with systems that only
        ///     process an earlier DDMS version with the same namespace.
        ///     <para>
        ///         DDMS 4.0 and 4.1 share the same XML namespace, so it is impossible to tell which DDMS version is employed from
        ///         the XML namespace alone. If an XML instance of a metacard employs a new DDMS 4.1 construct (like the new
        ///         ntk:Access element in a ddms:metacardInfo element), that XML instance will fail to work in a DDMS 4.0 system.
        ///     </para>
        /// </summary>
        /// <param name="component"> a text description of the component that is being warned about </param>
        protected internal virtual void AddDdms40Warning(string component)
        {
            AddWarning("The " + component +
                       " in this DDMS component was introduced in DDMS 4.1, and will prevent this XML instance" +
                       "from being understood by DDMS 4.0.1 systems.");
        }

        /// <summary>
        ///     Renders this component as HTML or Text, with an optional prefix to nest it.
        /// </summary>
        /// <param name="isHtml"> true for HTML, false for Text. </param>
        /// <param name="prefix"> an optional prefix to put on each name. </param>
        /// <param name="suffix">
        ///     an optional suffix to append to each name, such as an index.
        /// </param>
        /// <returns> the HTML or Text representation of this component </returns>
        public abstract string GetOutput(bool isHtml, string prefix, string suffix);

        /// <summary>
        ///     Convenience method to build a meta tag for HTML output or a text line for Text output.
        /// </summary>
        /// <param name="isHTML"> true for HTML, false for Text </param>
        /// <param name="name"> the name of the name-value pairing (will be escaped in HTML) </param>
        /// <param name="content"> the value of the name-value pairing (will be escaped in HTML) </param>
        /// <returns> a string containing the output </returns>
        public static string BuildOutput(bool isHTML, string name, string content)
        {
            if (String.IsNullOrEmpty(content))
                return ("");

            var tag = new StringBuilder();
            tag.Append(isHTML ? "<meta name=\"" : "");
            tag.Append(isHTML ? Util.Util.XmlEscape(name) : name);
            tag.Append(isHTML ? "\" content=\"" : ": ");
            tag.Append(isHTML ? Util.Util.XmlEscape(content) : content);
            tag.Append(isHTML ? "\" />\n" : "\n");
            return (tag.ToString());
        }

        /// <summary>
        ///     Convenience method to build a meta tag for HTML output or a text line for Text output for a list of multiple DDMS
        ///     components.
        /// </summary>
        /// <param name="isHTML"> true for HTML, false for Text </param>
        /// <param name="prefix"> the first part of the name in the name-value pairing (will be escaped in HTML) </param>
        /// <param name="contents"> a list of the values (will be escaped in HTML) </param>
        /// <returns> a string containing the output </returns>
        protected internal virtual string BuildOutput<T1>(bool isHTML, string prefix, List<T1> contents)
        {
            var values = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                object @object = contents[i];
                if (@object is AbstractBaseComponent)
                    values.Append(((AbstractBaseComponent)@object).GetOutput(isHTML, prefix, BuildIndex(i, contents.Count)));
                else if (@object is string)
                    values.Append(BuildOutput(isHTML, prefix + BuildIndex(i, contents.Count), (string)@object));
                else
                    values.Append(BuildOutput(isHTML, prefix + BuildIndex(i, contents.Count), Convert.ToString(@object)));
            }
            return (values.ToString());
        }

        /// <summary>
        ///     Convenience method to construct a naming prefix for use in HTML/Text output
        /// </summary>
        /// <param name="prefix"> an optional first part to the prefix </param>
        /// <param name="token"> an optional second part to the prefix </param>
        /// <param name="suffix"> an optional third part to the prefix </param>
        /// <returns> a String containing the concatenated values </returns>
        protected internal virtual string BuildPrefix(string prefix, string token, string suffix)
        {
            return (Util.Util.GetNonNullString(prefix) + Util.Util.GetNonNullString(token) + Util.Util.GetNonNullString(suffix));
        }

        /// <summary>
        ///     Constructs a braced 1-based index to differentiate multiples in HTML/Text output, based on the 0-based list index
        ///     of the item, and the <code>output.indexLevel</code> configurable property. When this property is 0, indices are
        ///     never shown. At 1, indices are shown when needed, but hidden when there is only 1 item to display. At 2, indices
        ///     are always shown. If the property is set to something else, it defaults to 0.
        /// </summary>
        /// <param name="index"> the 0-based index of an item in a list </param>
        /// <param name="total"> the total number of items in that list </param>
        /// <returns> a String containing the index text, if applicable </returns>
        protected internal virtual string BuildIndex(int index, int total)
        {
            if (total < 1)
                throw new ArgumentException("The total must be at least 1.");

            if (index < 0 || index >= total)
                throw new ArgumentException("The index is not properly bounded between 0 and " + (total - 1));

            string indexLevel = PropertyReader.GetProperty("output.indexLevel");
            if ("2".Equals(indexLevel))
                return ("[" + (index + 1) + "]");

            if ("1".Equals(indexLevel) && (total > 1))
                return ("[" + (index + 1) + "]");

            return ("");
        }

        /// <summary>
        ///     Convenience method to look up an attribute which is in the same namespace as the enclosing element
        /// </summary>
        /// <param name="name"> the local name of the attribute </param>
        /// <returns> attribute value, or an empty string if it does not exist </returns>
        protected internal virtual string GetAttributeValue(string name)
        {
            return (GetAttributeValue(name, Namespace));
        }

        /// <summary>
        ///     Convenience method to look up an attribute
        /// </summary>
        /// <param name="name"> the local name of the attribute </param>
        /// <param name="namespaceURI"> the namespace of the attribute </param>
        /// <returns> attribute value, or an empty string if it does not exist </returns>
        protected internal virtual string GetAttributeValue(string name, string namespaceURI)
        {
            Util.Util.RequireValue("name", name);
            string attrValue = Element.Attribute(XName.Get(name, Util.Util.GetNonNullString(namespaceURI))).Value;
            return (Util.Util.GetNonNullString(attrValue));
        }

        /// <summary>
        ///     Convenience method to get the first child element with a given name in the same namespace as the parent element
        /// </summary>
        /// <param name="name"> the local name to search for </param>
        /// <returns> the element, or null if it does not exist </returns>
        protected internal virtual XElement GetChild(string name)
        {
            Util.Util.RequireValue("name", name);
            return (Element.Element(XName.Get(name, Namespace)));
        }

        /// <summary>
        ///     Convenience method to convert one of the lat/lon fields into a Double. Returns null if the field does not exist
        ///     or cannot be converted into a Double.
        /// </summary>
        /// <param name="element"> the parent element </param>
        /// <param name="name"> the local name of the child </param>
        /// <returns> a Double, or null if it cannot be created </returns>
        protected internal static double? GetChildTextAsDouble(XElement element, string name)
        {
            Util.Util.RequireValue("element", element);
            Util.Util.RequireValue("name", name);
            XElement childElement = element.Element(XName.Get(name, element.Name.NamespaceName));
            if (childElement == null)
                return (null);

            return (GetStringAsDouble(childElement.Value));
        }

        /// <summary>
        ///     Helper method to assist with string to double conversion
        /// </summary>
        /// <param name="string"> the double as a string </param>
        /// <returns> a Double if possible, or null if the string cannot be converted </returns>
        protected internal static double? GetStringAsDouble(string @string)
        {
            try
            {
                return (Convert.ToDouble(@string));
            }
            catch (FormatException)
            {
                return (null);
            }
        }

        /// <summary>
        ///     Helper method to validate that a specific version of DDMS (or higher) is being used.
        /// </summary>
        /// <param name="version"> the threshold version </param>
        /// <exception cref="InvalidDDMSException"> if the version is not high enough </exception>
        protected internal virtual void RequireVersion(string version)
        {
            if (!DDMSVersion.IsAtLeast(version))
                throw new InvalidDDMSException("The " + Name + " element cannot be used until DDMS " + version + " or later.");
        }

        /// <summary>
        ///     Test for logical equality.
        ///     <para>
        ///         The base case tests against the name value and namespaceURI, as well as any child components classified as
        ///         "nested components" and any security attributes. Extending classes may require additional rules for equality.
        ///         This case automatically includes any nested components or security attributes.
        ///     </para>
        /// </summary>
        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return (true);
            
            if (!(obj is AbstractBaseComponent) || !(GetType().Equals(obj.GetType())))
                return (false);
            
            var test = (AbstractBaseComponent)obj;
            return (Name.Equals(test.Name) && 
                    Namespace.Equals(test.Namespace) &&
                    Util.Util.ListEquals(NestedComponents, test.NestedComponents) &&
                    Util.Util.NullEquals(SecurityAttributes, test.SecurityAttributes));
        }

        /// <summary>
        ///     Returns a hashcode for the component.
        ///     <para>This automatically includes any nested components or security attributes.</para>
        /// </summary>
        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = Name.GetHashCode();
            result = 7 * result + Namespace.GetHashCode();
            foreach (var nested in NestedComponents)
            {
                if (nested == null)
                    continue;
                
                result = 7 * result + nested.GetHashCode();
            }
            
            if (SecurityAttributes != null)
                result = 7 * result + SecurityAttributes.GetHashCode();
            
            return (result);
        }

        /// <summary>
        ///     Returns the XML representation of the component
        /// </summary>
        /// <see cref="object#toString()"></see>
        public override string ToString()
        {
            return (ToXML());
        }

        /// <summary>
        ///     Convenience method to create a warning and add it to the list of validation warnings.
        /// </summary>
        /// <param name="text"> the description text </param>
        protected internal virtual void AddWarning(string text)
        {
            Warnings.Add(ValidationMessage.NewWarning(text, QualifiedName + LocatorSuffix));
        }

        /// <summary>
        ///     Convenience method to add multiple warnings to the list of validation warnings.
        ///     <para>
        ///         Child locator information will be prefixed with the parent (this) locator information. This does not overwrite
        ///         the original warning -- it creates a new copy.
        ///     </para>
        /// </summary>
        /// <param name="warnings"> the list of validation messages to add </param>
        /// <param name="forAttributes">
        ///     if true, the locator suffix is not used, because the attributes will be for the topmost
        ///     element (for example, warnings for gml:Polygon's security attributes should not end up with a locator of
        ///     /gml:Polygon/gml:exterior/gml:LinearRing).
        /// </param>
        protected internal virtual void AddWarnings(List<ValidationMessage> warnings, bool forAttributes)
        {
            foreach (var warning in warnings)
            {
                string newLocator = QualifiedName + (forAttributes ? "" : LocatorSuffix) + warning.Locator;
                Warnings.Add(ValidationMessage.NewWarning(warning.Text, newLocator));
            }
        }

        /// <summary>
        ///     Accessor for the XOM element representing this component. When the element is set, the component is validated
        ///     again with <code>validate</code>.
        /// </summary>
        /// <param name="element"> the XOM element to use </param>
        /// <param name="validateNow"> whether to validate the component immediately after setting </param>
        protected internal virtual void SetElement(XElement element, bool validateNow)
        {
            Util.Util.RequireDDMSValue("XOM Element", element);
            _element = element;
            if (validateNow)
                Validate();
        }
    }
}