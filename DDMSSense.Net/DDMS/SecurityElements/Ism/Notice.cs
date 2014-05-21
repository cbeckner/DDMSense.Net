#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.SecurityElements.Ism
{
    /// <summary>
    ///     An immutable implementation of ISM:Notice.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ISM:NoticeText</u>: The text associated with this Notice (1-to-many required), implemented as a
    ///                 <see cref="NoticeText" /><br />
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
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and ownerProducer attributes are optional.<br />
    ///                 <u>
    ///                     <see cref="NoticeAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>
    /// </summary>
    public sealed class Notice : AbstractBaseComponent
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Notice(XElement element)
        {
            try
            {
                SetElement(element, false);
                NoticeTexts = new List<NoticeText>();
                IEnumerable<XElement> noticeTexts =                    element.Elements(XName.Get(NoticeText.GetName(DDMSVersion), DDMSVersion.IsmNamespace));
                noticeTexts.ToList().ForEach(n => NoticeTexts.Add(new NoticeText(n)));
                NoticeAttributes = new NoticeAttributes(element);
                SecurityAttributes = new SecurityAttributes(element);
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
        /// <param name="noticeTexts"> the notice texts (at least 1 required) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are optional) </param>
        /// <param name="noticeAttributes"> any notice attributes </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Notice(List<NoticeText> noticeTexts, SecurityAttributes securityAttributes,
            NoticeAttributes noticeAttributes)
        {
            try
            {
                if (noticeTexts == null)
                    noticeTexts = new List<NoticeText>();
                
                DDMSVersion version = DDMSVersion.CurrentVersion;
                XElement element = Util.Util.BuildElement(PropertyReader.GetPrefix("ism"), GetName(version), version.IsmNamespace, null);
                foreach (var noticeText in noticeTexts)
                    element.Add(noticeText.ElementCopy);
                
                NoticeTexts = noticeTexts;
                NoticeAttributes = NoticeAttributes.GetNonNullInstance(noticeAttributes);
                NoticeAttributes.AddTo(element);
                SecurityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                SecurityAttributes.AddTo(element);
                SetElement(element, true);
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
                list.AddRange(NoticeTexts);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the list of NoticeTexts.
        /// </summary>
        public List<NoticeText> NoticeTexts { get; private set; }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes { get; set; }

        /// <summary>
        ///     Accessor for the Notice Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public NoticeAttributes NoticeAttributes { get; set; }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>At least 1 NoticeText exists.</li>
        ///                 <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, DDMSVersion.IsmNamespace, GetName(DDMSVersion));
            if (NoticeTexts.Count == 0)
                throw new InvalidDDMSException("At least one ISM:NoticeText must exist within an ISM:Notice element.");
            
            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

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
        ///                 <li>An externalNotice attribute may cause issues for DDMS 4.0 records.</li>
        ///                 <li>Include any validation warnings from the notice attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (!NoticeAttributes.Empty)
            {
                AddWarnings(NoticeAttributes.ValidationWarnings, true);
                if (NoticeAttributes.ExternalReference != null)
                    AddDdms40Warning("ISM:externalNotice attribute");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "notice", suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix, NoticeTexts));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            text.Append(NoticeAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Notice))
                return (false);
            
            var test = (Notice) obj;
            return (NoticeAttributes.Equals(test.NoticeAttributes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + NoticeAttributes.GetHashCode();
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
            return ("Notice");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                NoticeAttributes = new NoticeAttributes.Builder();
                NoticeTexts = new List<NoticeText.Builder>();
                SecurityAttributes = new SecurityAttributes.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Notice notice) :this()
            {
                foreach (var noticeText in notice.NoticeTexts)
                    NoticeTexts.Add(new NoticeText.Builder(noticeText));
                
                SecurityAttributes = new SecurityAttributes.Builder(notice.SecurityAttributes);
                NoticeAttributes = new NoticeAttributes.Builder(notice.NoticeAttributes);
            }

            /// <summary>
            ///     Builder accessor for the noticeTexts
            /// </summary>
            public virtual List<NoticeText.Builder> NoticeTexts { get; private set; }

            /// <summary>
            ///     Builder accessor for the securityAttributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes { get; set; }


            /// <summary>
            ///     Builder accessor for the noticeAttributes
            /// </summary>
            public virtual NoticeAttributes.Builder NoticeAttributes { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                    return (null);
                
                var noticeTexts = new List<NoticeText>();
                foreach (IBuilder builder in NoticeTexts)
                {
                    var component = (NoticeText) builder.Commit();
                    if (component != null)
                        noticeTexts.Add(component);
                }
                return (new Notice(noticeTexts, SecurityAttributes.Commit(), NoticeAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in NoticeTexts)
                        hasValueInList = hasValueInList || !builder.Empty;
                    
                    return (!hasValueInList && SecurityAttributes.Empty && NoticeAttributes.Empty);
                }
            }
        }
    }
}