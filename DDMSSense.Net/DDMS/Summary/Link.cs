#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.ResourceElements;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.DDMS.Summary.Xlink;
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
    ///     An immutable implementation of ddms:link.
    ///     <para>This element is not a global component, but is being implemented because it has attributes.</para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>The href value must not be empty.</li>
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
    ///                 <u>
    ///                     <see cref="XLinkAttributes" />
    ///                 </u>
    ///                 : The xlink:type attribute is required and must have a fixed
    ///                 value of "locator". The xlink:href attribute is also required.<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : Only allowed when used in the context of a <see cref="RevisionRecall" />
    ///                 (starting in DDMS 4.0.1). The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Link : AbstractBaseComponent
    {
        private const string FIXED_TYPE = "locator";
        private SecurityAttributes _securityAttributes;
        private XLinkAttributes _xlinkAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Link(Element element)
        {
            try
            {
                _xlinkAttributes = new XLinkAttributes(element);
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
        /// <param name="xlinkAttributes"> the xlink attributes </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Link(XLinkAttributes xlinkAttributes) : this(xlinkAttributes, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="xlinkAttributes"> the xlink attributes </param>
        /// <param name="securityAttributes"> attributes, which are only allowed on links within a ddms:revisionRecall </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Link(XLinkAttributes xlinkAttributes, SecurityAttributes securityAttributes)
        {
            try
            {
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                _xlinkAttributes = XLinkAttributes.GetNonNullInstance(xlinkAttributes);
                _xlinkAttributes.AddTo(element);
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
        ///     Accessor for the XLink Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public XLinkAttributes XLinkAttributes
        {
            get { return (_xlinkAttributes); }
            set { _xlinkAttributes = value; }
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
        ///                 <li>The xlink:type is set and has a value of "locator".</li>
        ///                 <li>The xlink:href is set and non-empty.</li>
        ///                 <li>
        ///                     Does not validate the security attributes. It is the parent class' responsibility
        ///                     to do that.
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("type attribute", XLinkAttributes.Type);
            Util.Util.RequireDDMSValue("href attribute", XLinkAttributes.Href);

            if (!XLinkAttributes.Type.Equals(FIXED_TYPE))
            {
                throw new InvalidDDMSException("The type attribute must have a fixed value of \"" + FIXED_TYPE + "\".");
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
        ///                 <li>Include any warnings from the XLink attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (XLinkAttributes != null)
            {
                AddWarnings(XLinkAttributes.ValidationWarnings, true);
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(XLinkAttributes.GetOutput(isHtml, localPrefix));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Link))
            {
                return (false);
            }
            var test = (Link) obj;
            return (XLinkAttributes.Equals(test.XLinkAttributes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + XLinkAttributes.GetHashCode();
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
            return ("link");
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
            
            internal SecurityAttributes.Builder _securityAttributes;
            internal XLinkAttributes.Builder _xlinkAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Link link)
            {
                XLinkAttributes = new XLinkAttributes.Builder(link.XLinkAttributes);
                SecurityAttributes = new SecurityAttributes.Builder(link.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the XLink Attributes
            /// </summary>
            public virtual XLinkAttributes.Builder XLinkAttributes
            {
                get
                {
                    if (_xlinkAttributes == null)
                    {
                        _xlinkAttributes = new XLinkAttributes.Builder();
                    }
                    return _xlinkAttributes;
                }
                set { _xlinkAttributes = value; }
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
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new Link(XLinkAttributes.Commit(), SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (XLinkAttributes.Empty && SecurityAttributes.Empty); }
            }
        }
    }
}