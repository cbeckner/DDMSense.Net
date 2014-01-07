#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

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

namespace DDMSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:source.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A source element can be used with none of the attributes set.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:qualifier</u>: specifies the source of the type vocabulary (optional)<br />
    ///                 <u>ddms:value</u>: includes terms describing general categories, functions, genres, or aggregation
    ///                 levels
    ///                 (optional)<br />
    ///                 <u>ddms:schemaQualifier</u>: the schema type (optional)<br />
    ///                 <u>ddms:schemaHref</u>: a resolvable reference to the schema (optional)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are optional. (starting
    ///                 in DDMS 3.0)
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Source : AbstractQualifierValue
    {
        private const string SCHEMA_QUALIFIER_NAME = "schemaQualifier";
        private const string SCHEMA_HREF_NAME = "schemaHref";
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Source(Element element)
        {
            try
            {
                _securityAttributes = new SecurityAttributes(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="qualifier">	the value of the qualifier attribute </param>
        /// <param name="value">	the value of the value attribute </param>
        /// <param name="schemaQualifier"> the value of the schemaQualifier attribute </param>
        /// <param name="schemaHref"> the value of the schemaHref attribute </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Source(string qualifier, string value, string schemaQualifier, string schemaHref,
            SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.GetCurrentVersion()), qualifier, value, false)
        {
            try
            {
                Element element = Element;
                Util.Util.AddDDMSAttribute(element, SCHEMA_QUALIFIER_NAME, schemaQualifier);
                Util.Util.AddDDMSAttribute(element, SCHEMA_HREF_NAME, schemaHref);
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the value of the schema qualifier
        /// </summary>
        public string SchemaQualifier
        {
            get { return (GetAttributeValue(SCHEMA_QUALIFIER_NAME)); }
        }

        /// <summary>
        ///     Accessor for the value of the schema href
        /// </summary>
        public string SchemaHref
        {
            get { return (GetAttributeValue(SCHEMA_HREF_NAME)); }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>If a schemaHref is present, it is a valid URI.</li>
        ///                 <li>The SecurityAttributes do not exist until DDMS 3.0 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            if (!String.IsNullOrEmpty(SchemaHref))
            {
                Util.Util.RequireDDMSValidUri(SchemaHref);
            }
            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("3.0") && !SecurityAttributes.Empty)
            {
                throw new InvalidDDMSException(
                    "Security attributes cannot be applied to this component until DDMS 3.0 or later.");
            }

            base.Validate();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>A completely empty ddms:source element was found.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value) && String.IsNullOrEmpty(SchemaQualifier) &&
                String.IsNullOrEmpty(SchemaHref))
            {
                AddWarning("A completely empty ddms:source element was found.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + QUALIFIER_NAME, Qualifier));
            text.Append(BuildOutput(isHtml, localPrefix + VALUE_NAME, Value));
            text.Append(BuildOutput(isHtml, localPrefix + SCHEMA_QUALIFIER_NAME, SchemaQualifier));
            text.Append(BuildOutput(isHtml, localPrefix + SCHEMA_HREF_NAME, SchemaHref));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Source))
            {
                return (false);
            }
            var test = (Source) obj;
            return (SchemaQualifier.Equals(test.SchemaQualifier) && SchemaHref.Equals(test.SchemaHref));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + SchemaQualifier.GetHashCode();
            result = 7*result + SchemaHref.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("source");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        public class Builder : AbstractQualifierValue.Builder
        {
            internal const long SerialVersionUID = -514632949760329348L;
            internal string _schemaHref;
            internal string _schemaQualifier;
            internal SecurityAttributes.Builder _securityAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Source source) : base(source)
            {
                SchemaQualifier = source.SchemaQualifier;
                SchemaHref = source.SchemaHref;
                SecurityAttributes = new SecurityAttributes.Builder(source.SecurityAttributes);
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get
                {
                    return (base.Empty && String.IsNullOrEmpty(SchemaQualifier) && String.IsNullOrEmpty(SchemaHref) &&
                            SecurityAttributes.Empty);
                }
            }

            /// <summary>
            ///     Builder accessor for the schema qualifier
            /// </summary>
            public virtual string SchemaQualifier
            {
                get { return _schemaQualifier; }
                set { _schemaQualifier = value; }
            }


            /// <summary>
            ///     Builder accessor for the schema href
            /// </summary>
            public virtual string SchemaHref
            {
                get { return _schemaHref; }
                set { _schemaHref = value; }
            }


            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes
            {
                get
                {
                    if (_securityAttributes == null)
                    {
                        _securityAttributes = new SecurityAttributes.Builder();
                    }
                    return _securityAttributes;
                }
                set { _securityAttributes = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty
                    ? null
                    : new Source(Qualifier, Value, SchemaQualifier, SchemaHref, SecurityAttributes.Commit()));
            }
        }
    }
}