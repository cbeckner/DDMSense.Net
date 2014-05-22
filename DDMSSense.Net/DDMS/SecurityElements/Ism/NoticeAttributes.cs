#region usings

using DDMSense.Extensions;
using DDMSense.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

#endregion usings

namespace DDMSense.DDMS.SecurityElements.Ism
{
    /// <summary>
    ///     Attribute group for the ISM notice markings used on a ddms:resource and ISM:Notice, starting in DDMS 4.0.1.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ISM:noticeType</u>: (optional)<br />
    ///                 <u>ISM:noticeReason</u>: (optional)<br />
    ///                 <u>ISM:noticeDate</u>: (optional)<br />
    ///                 <u>ISM:unregisteredNoticeType</u>: (optional)<br />
    ///                 <u>ISM:externalNotice</u>: (optional, starting in DDMS 4.1)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    /// </summary>
    public sealed class NoticeAttributes : AbstractAttributeGroup
    {
        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string NOTICE_TYPE_NAME = "noticeType";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string NOTICE_REASON_NAME = "noticeReason";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string NOTICE_DATE_NAME = "noticeDate";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string UNREGISTERED_NOTICE_TYPE_NAME = "unregisteredNoticeType";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string EXTERNAL_NOTICE_NAME = "externalNotice";

        /// <summary>
        ///     Maximum length of reason and unregistered notice type attributes.
        /// </summary>
        public const int MAX_LENGTH = 2048;

        private static readonly HashSet<string> ALL_NAMES = new HashSet<string>();

        /// <summary>
        ///     A set of all SecurityAttribute names which should not be converted into ExtensibleAttributes
        /// </summary>
        public static readonly HashSet<string> NON_EXTENSIBLE_NAMES = ALL_NAMES;

        private readonly bool? _externalNotice;
        private readonly string _noticeType;
        private DateTime? _noticeDate;
        private string _noticeReason;
        private string _unregisteredNoticeType;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element which is decorated with these attributes. </param>
        public NoticeAttributes(XElement element)
            : base(element.Name.NamespaceName)
        {
            ALL_NAMES.Add(NOTICE_TYPE_NAME);
            ALL_NAMES.Add(NOTICE_REASON_NAME);
            ALL_NAMES.Add(NOTICE_DATE_NAME);
            ALL_NAMES.Add(UNREGISTERED_NOTICE_TYPE_NAME);
            ALL_NAMES.Add(EXTERNAL_NOTICE_NAME);

            string icNamespace = DDMSVersion.IsmNamespace;

            _noticeType = (string)element.Attribute(XName.Get(NOTICE_TYPE_NAME, icNamespace));
            _noticeReason = (string)element.Attribute(XName.Get(NOTICE_REASON_NAME, icNamespace));
            _unregisteredNoticeType = (string)element.Attribute(XName.Get(UNREGISTERED_NOTICE_TYPE_NAME, icNamespace));
            string noticeDate = (string)element.Attribute(XName.Get(NOTICE_DATE_NAME, icNamespace));
            if (!String.IsNullOrEmpty(noticeDate))
                _noticeDate = DateTime.Parse(noticeDate);

            string external = (string)element.Attribute(XName.Get(EXTERNAL_NOTICE_NAME, icNamespace));
            if (!String.IsNullOrEmpty(external))
                _externalNotice = Convert.ToBoolean(external);

            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// @deprecated A new constructor was added for DDMS 4.1 to support ism:externalNotice. This constructor is preserved for
        /// backwards compatibility, but may disappear in the next major release.
        /// <param name="noticeType"> the notice type (with a value from the CVE) </param>
        /// <param name="noticeReason"> the reason associated with a notice </param>
        /// <param name="noticeDate"> the date associated with a notice </param>
        /// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NoticeAttributes(string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType)
            : this(noticeType, noticeReason, noticeDate, unregisteredNoticeType, null)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="noticeType"> the notice type (with a value from the CVE) </param>
        /// <param name="noticeReason"> the reason associated with a notice </param>
        /// <param name="noticeDate"> the date associated with a notice </param>
        /// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
        /// <param name="externalNotice"> true if this notice is for an external resource </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NoticeAttributes(string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType, bool? externalNotice)
            : base(DDMSVersion.CurrentVersion.Namespace)
        {
            ALL_NAMES.Add(NOTICE_TYPE_NAME);
            ALL_NAMES.Add(NOTICE_REASON_NAME);
            ALL_NAMES.Add(NOTICE_DATE_NAME);
            ALL_NAMES.Add(UNREGISTERED_NOTICE_TYPE_NAME);
            ALL_NAMES.Add(EXTERNAL_NOTICE_NAME);

            _noticeType = noticeType;
            _noticeReason = noticeReason;
            _unregisteredNoticeType = unregisteredNoticeType;
            if (!String.IsNullOrEmpty(noticeDate))
            {
                try
                {
                    _noticeDate = DateTime.Parse(noticeDate);
                }
                catch (ArgumentException)
                {
                    throw new InvalidDDMSException("The ISM:noticeDate attribute is not in a valid date format.");
                }
            }
            _externalNotice = externalNotice;
            Validate();
        }

        /// <summary>
        ///     Checks if any attributes have been set.
        /// </summary>
        /// <returns> true if no attributes have values, false otherwise </returns>
        public bool Empty
        {
            get
            {
                return (String.IsNullOrEmpty(NoticeType) && String.IsNullOrEmpty(NoticeReason) && String.IsNullOrEmpty(UnregisteredNoticeType) && NoticeDate == null);
            }
        }

        /// <summary>
        ///     Accessor for the noticeType attribute.
        /// </summary>
        public string NoticeType
        {
            get { return _noticeType.ToNonNullString(); }
            set { _noticeDate = DateTime.Parse(value); }
        }

        /// <summary>
        ///     Accessor for the noticeReason attribute.
        /// </summary>
        public string NoticeReason
        {
            get { return _noticeReason.ToNonNullString(); }
            set { _noticeReason = value; }
        }

        /// <summary>
        ///     Accessor for the unregisteredNoticeType attribute.
        /// </summary>
        public string UnregisteredNoticeType
        {
            get { return _unregisteredNoticeType.ToNonNullString(); }
            set { _unregisteredNoticeType = value; }
        }

        /// <summary>
        ///     Accessor for the externalNotice attribute. This may be null before DDMS 4.1.
        /// </summary>
        public bool? ExternalReference
        {
            get { return (_externalNotice); }
        }

        /// <summary>
        ///     Accessor for the noticeDate attribute. May return null if not set.
        /// </summary>
        public DateTime? NoticeDate
        {
            get { return _noticeDate.HasValue ? DateTime.Parse(_noticeDate.Value.ToString("o")) : _noticeDate; }
            set { _noticeDate = value; }
        }

        /// <summary>
        ///     Returns a non-null instance of notice attributes. If the instance passed in is not null, it will be returned.
        /// </summary>
        /// <param name="noticeAttributes"> the attributes to return by default </param>
        /// <returns> a non-null attributes instance </returns>
        /// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
        public static NoticeAttributes GetNonNullInstance(NoticeAttributes noticeAttributes)
        {
            return (noticeAttributes == null ? new NoticeAttributes(null, null, null, null) : noticeAttributes);
        }

        /// <summary>
        ///     Convenience method to add these attributes onto an existing XOM Element
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        public void AddTo(XElement element)
        {
            DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
            ValidateSameVersion(elementVersion);
            string icNamespace = elementVersion.IsmNamespace;
            string icPrefix = PropertyReader.GetPrefix("ism");

            Util.Util.AddAttribute(element, icPrefix, NOTICE_TYPE_NAME, icNamespace, NoticeType);
            Util.Util.AddAttribute(element, icPrefix, NOTICE_REASON_NAME, icNamespace, NoticeReason);
            if (NoticeDate != null)
                Util.Util.AddAttribute(element, icPrefix, NOTICE_DATE_NAME, icNamespace, NoticeDate.Value.ToString("yyyy-MM-dd"));

            Util.Util.AddAttribute(element, icPrefix, UNREGISTERED_NOTICE_TYPE_NAME, icNamespace, UnregisteredNoticeType);
            if (ExternalReference != null)
                Util.Util.AddAttribute(element, icPrefix, EXTERNAL_NOTICE_NAME, icNamespace, XmlConvert.ToString(ExternalReference.Value));
        }

        /// <summary>
        ///     Validates the attribute group. Where appropriate the <see cref="ISMVocabulary" /> enumerations are validated.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>If set, the noticeType attribute must be a valid token.</li>
        ///                 <li>The noticeReason must be shorter than 2048 characters.</li>
        ///                 <li>The unregisteredNoticeType must be shorter than 2048 characters.</li>
        ///                 <li>If set, the noticeDate attribute is a valid xs:date value.</li>
        ///                 <li>These attributes cannot be used until DDMS 4.0.1 or later.</li>
        ///                 <li>The externalNotice attribute cannot be used until DDMS 4.1 or later.</li>
        ///                 <li>Does NOT do any validation on the constraints described in the DES ISM specification.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            // Should be reviewed as additional versions of DDMS are supported.
            DDMSVersion version = DDMSVersion;
            if (version.IsAtLeast("4.0.1") && !String.IsNullOrEmpty(NoticeType))
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NOTICE_TYPE, NoticeType);

            if (!String.IsNullOrEmpty(NoticeReason) && NoticeReason.Length > MAX_LENGTH)
                throw new InvalidDDMSException("The noticeReason attribute must be shorter than " + MAX_LENGTH + " characters.");

            if (!String.IsNullOrEmpty(UnregisteredNoticeType) && UnregisteredNoticeType.Length > MAX_LENGTH)
                throw new InvalidDDMSException("The unregisteredNoticeType attribute must be shorter than " + MAX_LENGTH + " characters.");

            if (NoticeDate != null && !NoticeDate.Value.TimeOfDay.TotalSeconds.Equals(0))
                throw new InvalidDDMSException("The noticeDate attribute must be in the xs:date format (YYYY-MM-DD).");

            if (!version.IsAtLeast("4.0.1") && !Empty)
                throw new InvalidDDMSException("Notice attributes cannot be used until DDMS 4.0.1 or later.");

            // Test for 4.1 externalNotice is implicit, since 4.0.1 and 4.1 have same XML namespace.
            base.Validate();
        }

        /// <see cref="AbstractAttributeGroup#getOutput(boolean, String)"></see>
        public override string GetOutput(bool isHtml, string prefix)
        {
            string localPrefix = prefix.ToNonNullString();
            var text = new StringBuilder();
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + NOTICE_TYPE_NAME, NoticeType));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + NOTICE_REASON_NAME, NoticeReason));
            if (NoticeDate != null)
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + NOTICE_DATE_NAME, NoticeDate.Value.ToString("yyyy-MM-dd")));

            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + UNREGISTERED_NOTICE_TYPE_NAME, UnregisteredNoticeType));
            if (ExternalReference != null)
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + EXTERNAL_NOTICE_NAME, XmlConvert.ToString(ExternalReference.Value)));

            return (text.ToString());
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!(obj is NoticeAttributes))
                return (false);

            var test = (NoticeAttributes)obj;
            return (NoticeType.Equals(test.NoticeType) &&
                    NoticeReason.Equals(test.NoticeReason) &&
                    UnregisteredNoticeType.Equals(test.UnregisteredNoticeType) &&
                    Util.Util.NullEquals(NoticeDate, test.NoticeDate) &&
                    Util.Util.NullEquals(ExternalReference, test.ExternalReference));
        }

        /// <see cref="Object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = 0;
            result = 7 * result + NoticeType.GetHashCode();
            result = 7 * result + NoticeReason.GetHashCode();
            result = 7 * result + UnregisteredNoticeType.GetHashCode();
            if (NoticeDate != null)
                result = 7 * result + NoticeDate.GetHashCode();

            if (ExternalReference != null)
                result = 7 * result + ExternalReference.GetHashCode();

            return (result);
        }

        /// <summary>
        ///     Builder for these attributes.
        ///     <para>
        ///         This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
        ///         standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
        ///         null.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(NoticeAttributes attributes)
            {
                StringAttributes = new Dictionary<string, string>();

                NoticeType = attributes.NoticeType;
                NoticeReason = attributes.NoticeReason;
                if (attributes.NoticeDate != null)
                    NoticeDate = attributes.NoticeDate.Value.ToString("yyyy-MM-dd");

                UnregisteredNoticeType = attributes.UnregisteredNoticeType;
                ExternalNotice = attributes.ExternalReference;
            }

            /// <summary>
            ///     Checks if any values have been provided for this Builder.
            /// </summary>
            /// <returns> true if every field is empty </returns>
            public virtual bool Empty
            {
                get
                {
                    bool isEmpty = true;
                    foreach (var value in StringAttributes.Values)
                        isEmpty = isEmpty && String.IsNullOrEmpty(value);

                    return (isEmpty && ExternalNotice == null);
                }
            }

            /// <summary>
            ///     Builder accessor for the noticeType attribute
            /// </summary>
            public virtual string NoticeType
            {
                get { return (StringAttributes.GetValueOrNull(NOTICE_TYPE_NAME)); }
                set { StringAttributes[NOTICE_TYPE_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the noticeReason attribute
            /// </summary>
            public virtual string NoticeReason
            {
                get { return (StringAttributes.GetValueOrNull(NOTICE_REASON_NAME)); }
                set { StringAttributes[NOTICE_REASON_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the noticeDate attribute
            /// </summary>
            public virtual string NoticeDate
            {
                get { return (StringAttributes.GetValueOrNull(NOTICE_DATE_NAME)); }
                set { StringAttributes[NOTICE_DATE_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the unregisteredNoticeType attribute
            /// </summary>
            public virtual string UnregisteredNoticeType
            {
                get { return (StringAttributes.GetValueOrNull(UNREGISTERED_NOTICE_TYPE_NAME)); }
                set { StringAttributes[UNREGISTERED_NOTICE_TYPE_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the externalNotice attribute
            /// </summary>
            public virtual bool? ExternalNotice { get; set; }

            /// <summary>
            ///     Accessor for the map of attribute names to string values
            /// </summary>
            internal virtual IDictionary<string, string> StringAttributes { get; set; }

            /// <summary>
            ///     Finalizes the data gathered for this builder instance. Will always return an empty instance instead of
            ///     a null one.
            /// </summary>
            /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
            public virtual NoticeAttributes Commit()
            {
                return (new NoticeAttributes(NoticeType, NoticeReason, NoticeDate, UnregisteredNoticeType, ExternalNotice));
            }
        }
    }
}