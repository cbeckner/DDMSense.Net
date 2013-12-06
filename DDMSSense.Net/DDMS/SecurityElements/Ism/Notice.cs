using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
namespace DDMSSense.DDMS.SecurityElements.Ism
{


    using Element = System.Xml.Linq.XElement;
    using DDMSVersion = DDMSSense.Util.DDMSVersion;

    using PropertyReader = DDMSSense.Util.PropertyReader;
    using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS;
    using System.Xml.Linq;

    /// <summary>
    /// An immutable implementation of ISM:Notice.
    /// 
    /// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
    /// <u>ISM:NoticeText</u>: The text associated with this Notice (1-to-many required), implemented as a <seealso cref="NoticeText"/><br />
    /// </td></tr></table>
    /// 
    /// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
    /// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional.<br />
    /// <u><seealso cref="NoticeAttributes"/></u>
    /// </td></tr></table>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    public sealed class Notice : AbstractBaseComponent
    {

        private List<NoticeText> _noticeTexts = null;
        private SecurityAttributes _securityAttributes = null;
        private NoticeAttributes _noticeAttributes = null;

        /// <summary>
        /// Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


        public Notice(Element element)
        {
            try
            {
                SetXOMElement(element, false);
                _noticeTexts = new List<NoticeText>();
                IEnumerable<Element> noticeTexts = element.Elements(XName.Get(NoticeText.GetName(DDMSVersion), DDMSVersion.IsmNamespace));
                noticeTexts.ToList().ForEach(n => _noticeTexts.Add(new NoticeText(n)));
                _noticeAttributes = new NoticeAttributes(element);
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
        /// Constructor for creating a component from raw data
        /// </summary>
        /// <param name="noticeTexts"> the notice texts (at least 1 required) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are optional) </param>
        /// <param name="noticeAttributes"> any notice attributes </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


        public Notice(List<NoticeText> noticeTexts, SecurityAttributes securityAttributes, NoticeAttributes noticeAttributes)
        {
            try
            {
                if (noticeTexts == null)
                {
                    noticeTexts = new List<NoticeText>();
                }
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                Element element = Util.BuildElement(PropertyReader.GetPrefix("ism"), Notice.GetName(version), version.IsmNamespace, null);
                foreach (NoticeText noticeText in noticeTexts)
                {
                    element.Add(noticeText.XOMElementCopy);
                }
                _noticeTexts = noticeTexts;
                _noticeAttributes = NoticeAttributes.GetNonNullInstance(noticeAttributes);
                _noticeAttributes.AddTo(element);
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetXOMElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        /// Validates the component.
        /// 
        /// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
        /// <li>The qualified name of the element is correct.</li>
        /// <li>At least 1 NoticeText exists.</li>
        /// <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        /// </td></tr></table>
        /// </summary>
        /// <seealso cref= AbstractBaseComponent#validate() </seealso>


        protected internal override void Validate()
        {
            Util.RequireQualifiedName(Element, DDMSVersion.IsmNamespace, Notice.GetName(DDMSVersion));
            if (NoticeTexts.Count == 0)
            {
                throw new InvalidDDMSException("At least one ISM:NoticeText must exist within an ISM:Notice element.");
            }

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <summary>
        /// Validates any conditions that might result in a warning.
        /// 
        /// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
        /// <li>An externalNotice attribute may cause issues for DDMS 4.0 records.</li>
        /// <li>Include any validation warnings from the notice attributes.</li>
        /// </td></tr></table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (!NoticeAttributes.Empty)
            {
                AddWarnings(NoticeAttributes.ValidationWarnings, true);
                if (NoticeAttributes.ExternalReference != null)
                {
                    AddDdms40Warning("ISM:externalNotice attribute");
                }
            }
            base.ValidateWarnings();
        }

        /// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
        public override string GetOutput(bool isHTML, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "notice", suffix + ".");
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, localPrefix, NoticeTexts));
            text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
            text.Append(NoticeAttributes.GetOutput(isHTML, localPrefix));
            return (text.ToString());
        }

        /// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                List<IDDMSComponent> list = new List<IDDMSComponent>();
                list.AddRange(NoticeTexts);
                return (list);
            }
        }

        /// <seealso cref= Object#equals(Object) </seealso>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Notice))
            {
                return (false);
            }
            Notice test = (Notice)obj;
            return (NoticeAttributes.Equals(test.NoticeAttributes));
        }

        /// <seealso cref= Object#hashCode() </seealso>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + NoticeAttributes.GetHashCode();
            return (result);
        }

        /// <summary>
        /// Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.RequireValue("version", version);
            return ("Notice");
        }

        /// <summary>
        /// Accessor for the list of NoticeTexts.
        /// </summary>
        public List<NoticeText> NoticeTexts
        {
            get
            {
                return _noticeTexts;
            }
        }

        /// <summary>
        /// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get
            {
                return (_securityAttributes);
            }
            set
            {
                _securityAttributes = value;
            }
        }

        /// <summary>
        /// Accessor for the Notice Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public NoticeAttributes NoticeAttributes
        {
            get
            {
                return (_noticeAttributes);
            }
            set
            {
                _noticeAttributes = value;
            }
        }

        /// <summary>
        /// Builder for this DDMS component.
        /// </summary>
        /// <seealso cref= IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0 </seealso>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = 7750664735441105296L;
            internal List<NoticeText.Builder> _noticeTexts;
            internal SecurityAttributes.Builder _securityAttributes = null;
            internal NoticeAttributes.Builder _noticeAttributes = null;

            /// <summary>
            /// Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            /// Constructor which starts from an existing component.
            /// </summary>
            public Builder(Notice notice)
            {
                foreach (NoticeText noticeText in notice.NoticeTexts)
                {
                    NoticeTexts.Add(new NoticeText.Builder(noticeText));
                }
                SecurityAttributes = new SecurityAttributes.Builder(notice.SecurityAttributes);
                NoticeAttributes = new NoticeAttributes.Builder(notice.NoticeAttributes);
            }

            /// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                List<NoticeText> noticeTexts = new List<NoticeText>();
                foreach (IBuilder builder in NoticeTexts)
                {
                    NoticeText component = (NoticeText)builder.Commit();
                    if (component != null)
                    {
                        noticeTexts.Add(component);
                    }
                }
                return (new Notice(noticeTexts, SecurityAttributes.Commit(), NoticeAttributes.Commit()));
            }

            /// <seealso cref= IBuilder#isEmpty() </seealso>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in NoticeTexts)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && SecurityAttributes.Empty && NoticeAttributes.Empty);
                }
            }

            /// <summary>
            /// Builder accessor for the noticeTexts
            /// </summary>
            public virtual List<NoticeText.Builder> NoticeTexts
            {
                get
                {
                    if (_noticeTexts == null)
                    {
                        _noticeTexts = new List<NoticeText.Builder>();
                    }
                    return _noticeTexts;
                }
            }

            /// <summary>
            /// Builder accessor for the securityAttributes
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


            /// <summary>
            /// Builder accessor for the noticeAttributes
            /// </summary>
            public virtual NoticeAttributes.Builder NoticeAttributes
            {
                get
                {
                    if (_noticeAttributes == null)
                    {
                        _noticeAttributes = new NoticeAttributes.Builder();
                    }
                    return _noticeAttributes;
                }
                set { _noticeAttributes = value; }
            }

        }
    }
}