#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.Extensible;
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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:keyword.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>The keyword value must not be empty.</li>
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
    ///                 <u>ddms:value</u>: The keyword itself (required)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are optional. (starting in DDMS
    ///                 4.0.1)<br />
    ///                 <u>
    ///                     <see cref="ExtensibleAttributes" />
    ///                 </u>
    ///                 : (optional, starting in DDMS 3.0).
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Keyword : AbstractBaseComponent
    {
        private const string VALUE_NAME = "value";
        private ExtensibleAttributes _extensibleAttributes;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Keyword(Element element)
        {
            try
            {
                _securityAttributes = new SecurityAttributes(element);
                _extensibleAttributes = new ExtensibleAttributes(element);
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
        /// <param name="value"> the value attribute (required) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Keyword(string value, SecurityAttributes securityAttributes) : this(value, securityAttributes, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        ///     <para>
        ///         This constructor will throw an InvalidDDMSException if the extensible attributes uses the reserved
        ///         attribute "ddms:value".
        ///     </para>
        /// </summary>
        /// <param name="value"> the value attribute (required) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        /// <param name="extensibleAttributes"> extensible attributes (optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Keyword(string value, SecurityAttributes securityAttributes, ExtensibleAttributes extensibleAttributes)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                Util.Util.AddDDMSAttribute(element, VALUE_NAME, value);
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                _extensibleAttributes = ExtensibleAttributes.GetNonNullInstance(extensibleAttributes);
                _extensibleAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the value attribute.
        /// </summary>
        public string Value
        {
            get { return (GetAttributeValue(VALUE_NAME)); }
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
        ///     Accessor for the extensible attributes. Will always be non-null, even if not set.
        /// </summary>
        public ExtensibleAttributes ExtensibleAttributes
        {
            get { return (_extensibleAttributes); }
            set { _extensibleAttributes = value; }
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
        ///                 <li>The keyword value exists and is not empty.</li>
        ///                 <li>The SecurityAttributes do not exist until DDMS 4.0.1 or later.</li>
        ///                 <li>No extensible attributes can exist until DDMS 3.0 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("value attribute", Value);
            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("4.0.1") && !SecurityAttributes.Empty)
            {
                throw new InvalidDDMSException(
                    "Security attributes cannot be applied to this component until DDMS 4.0.1 or later.");
            }
            if (!DDMSVersion.IsAtLeast("3.0") && !ExtensibleAttributes.Empty)
            {
                throw new InvalidDDMSException(
                    "xs:anyAttribute cannot be applied to ddms:keyword until DDMS 3.0 or later.");
            }

            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, Value));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            text.Append(ExtensibleAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Keyword))
            {
                return (false);
            }
            var test = (Keyword) obj;
            return (Value.Equals(test.Value) && ExtensibleAttributes.Equals(test.ExtensibleAttributes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Value.GetHashCode();
            result = 7*result + ExtensibleAttributes.GetHashCode();
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
            return ("keyword");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            
            internal ExtensibleAttributes.Builder _extensibleAttributes;
            internal SecurityAttributes.Builder _securityAttributes;
            internal string _value;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Keyword keyword)
            {
                _value = keyword.Value;
                _securityAttributes = new SecurityAttributes.Builder(keyword.SecurityAttributes);
                _extensibleAttributes = new ExtensibleAttributes.Builder(keyword.ExtensibleAttributes);
            }

            /// <summary>
            ///     Builder accessor for the value attribute
            /// </summary>
            public virtual string Value
            {
                get { return _value; }
                set { _value = value; }
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
            }


            /// <summary>
            ///     Builder accessor for the Extensible Attributes
            /// </summary>
            public virtual ExtensibleAttributes.Builder ExtensibleAttributes
            {
                get
                {
                    if (_extensibleAttributes == null)
                    {
                        _extensibleAttributes = new ExtensibleAttributes.Builder();
                    }
                    return _extensibleAttributes;
                }
            }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new Keyword(Value, SecurityAttributes.Commit(), ExtensibleAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(Value) && SecurityAttributes.Empty && ExtensibleAttributes.Empty); }
            }
        }
    }
}