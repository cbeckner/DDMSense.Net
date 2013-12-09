#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.DDMS.SecurityElements.Ntk;
using DDMSSense.Util;

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

namespace DDMSSense.DDMS.SecurityElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:security.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:noticeList</u>: A collection of IC notices (optional, starting in DDMS 4.0.1), implemented as a
    ///                 <see cref="NoticeList" /><br />
    ///                 <u>ntk:Access</u>: Need-To-Know access information (optional, starting in DDMS 4.0.1), implemented as
    ///                 an
    ///                 <see cref="Access" /><br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ISM:excludeFromRollup</u>: (required, fixed as "true", starting in DDMS 3.0)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 0.9.b
    /// </summary>
    public sealed class Security : AbstractBaseComponent
    {
        private const string FIXED_ROLLUP = "true";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string EXCLUDE_FROM_ROLLUP_NAME = "excludeFromRollup";

        private Access _access;
        private NoticeList _noticeList;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Security(Element element)
        {
            try
            {
                SetXOMElement(element, false);
                Element noticeListElement = element.Element(XName.Get(NoticeList.GetName(DDMSVersion), Namespace));
                if (noticeListElement != null)
                {
                    _noticeList = new NoticeList(noticeListElement);
                }

                Element accessElement = element.Element(XName.Get(Access.GetName(DDMSVersion), DDMSVersion.NtkNamespace));
                if (accessElement != null)
                {
                    _access = new Access(accessElement);
                }
                _securityAttributes = new SecurityAttributes(element);
                Validate();
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
        /// <param name="noticeList"> notice list (optional) </param>
        /// <param name="access"> NTK access information (optional) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Security(NoticeList noticeList, Access access, SecurityAttributes securityAttributes)
        {
            try
            {
                DDMSVersion version = DDMSVersion.GetCurrentVersion();

                Element element = Util.Util.BuildDDMSElement(GetName(version), null);
                if (noticeList != null)
                {
                    element.Add(noticeList.XOMElementCopy);
                }
                if (access != null)
                {
                    element.Add(access.XOMElementCopy);
                }
                if (DDMSVersion.GetCurrentVersion().IsAtLeast("3.0"))
                {
                    Util.Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), EXCLUDE_FROM_ROLLUP_NAME,
                        DDMSVersion.GetCurrentVersion().IsmNamespace, FIXED_ROLLUP);
                }
                _noticeList = noticeList;
                _access = access;
                _securityAttributes = securityAttributes;
                if (securityAttributes != null)
                {
                    securityAttributes.AddTo(element);
                }
                SetXOMElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(NoticeList);
                list.Add(Access);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the excludeFromRollup attribute. This may be null for DDMS 2.0 components.
        /// </summary>
        public bool? ExcludeFromRollup
        {
            get
            {
                string value = GetAttributeValue(EXCLUDE_FROM_ROLLUP_NAME, DDMSVersion.GetCurrentVersion().IsmNamespace);
                if ("true".Equals(value))
                {
                    return (true);
                }
                if ("false".Equals(value))
                {
                    return (false);
                }
                return (null);
            }
        }

        /// <summary>
        ///     Accessor for the NoticeList. May be null.
        /// </summary>
        public NoticeList NoticeList
        {
            get { return (_noticeList); }
            set { _noticeList = value; }
        }

        /// <summary>
        ///     Accessor for the Access. May be null.
        /// </summary>
        public Access Access
        {
            get { return (_access); }
            set { _access = value; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
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
        ///                 <li>A classification is required.</li>
        ///                 <li>Only 0-1 noticeLists or Access elements exist.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>The excludeFromRollup is set and has a value of "true", starting in DDMS 3.0.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQName(Element, GetName(DDMSVersion));
            Util.Util.RequireBoundedChildCount(Element, NoticeList.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Access.GetName(DDMSVersion), 0, 1);

            // Should be reviewed as additional versions of DDMS are supported.
            if (DDMSVersion.IsAtLeast("3.0"))
            {
                if (ExcludeFromRollup == null)
                {
                    throw new InvalidDDMSException("The excludeFromRollup attribute is required.");
                }
                if (!FIXED_ROLLUP.Equals(Convert.ToString(ExcludeFromRollup)))
                {
                    throw new InvalidDDMSException("The excludeFromRollup attribute must have a fixed value of \"" +
                                                   FIXED_ROLLUP + "\".");
                }
            }
            else if (ExcludeFromRollup != null)
            {
                throw new InvalidDDMSException("The excludeFromRollup attribute cannot be used until DDMS 3.0 or later.");
            }

            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            if (ExcludeFromRollup != null)
            {
                text.Append(BuildOutput(isHtml, localPrefix + EXCLUDE_FROM_ROLLUP_NAME,
                    Convert.ToString(ExcludeFromRollup)));
            }
            if (NoticeList != null)
            {
                text.Append(NoticeList.GetOutput(isHtml, localPrefix, ""));
            }
            if (Access != null)
            {
                text.Append(Access.GetOutput(isHtml, localPrefix, ""));
            }
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Security))
            {
                return (false);
            }
            return (true);
            // ExcludeFromRollup is not included in equality or hashCode, because it is fixed at TRUE.
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("security");
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
            internal const long SerialVersionUID = -7744353774641616270L;
            internal Access.Builder _access;
            internal NoticeList.Builder _noticeList;
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
            public Builder(Security security)
            {
                if (security.NoticeList != null)
                {
                    NoticeList = new NoticeList.Builder(security.NoticeList);
                }
                if (security.Access != null)
                {
                    Access = new Access.Builder(security.Access);
                }
                SecurityAttributes = new SecurityAttributes.Builder(security.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the noticeList
            /// </summary>
            public virtual NoticeList.Builder NoticeList
            {
                get
                {
                    if (_noticeList == null)
                    {
                        _noticeList = new NoticeList.Builder();
                    }
                    return _noticeList;
                }
                set { _noticeList = value; }
            }


            /// <summary>
            ///     Builder accessor for the access
            /// </summary>
            public virtual Access.Builder Access
            {
                get
                {
                    if (_access == null)
                    {
                        _access = new Access.Builder();
                    }
                    return _access;
                }
                set { _access = value; }
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
                return (Empty
                    ? null
                    : new Security((NoticeList) NoticeList.Commit(), (Access) Access.Commit(),
                        SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (NoticeList.Empty && Access.Empty && SecurityAttributes.Empty); }
            }
        }
    }
}