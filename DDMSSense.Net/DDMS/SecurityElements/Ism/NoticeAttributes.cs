using System;
using System.Collections.Generic;
using System.Text;
using DDMSSense.Extensions;
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
namespace DDMSSense.DDMS.SecurityElements.Ism {



	using Element = System.Xml.Linq.XElement;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.Xml.Linq;

	/// <summary>
	/// Attribute group for the ISM notice markings used on a ddms:resource and ISM:Notice, starting in DDMS 4.0.1.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ISM:noticeType</u>: (optional)<br />
	/// <u>ISM:noticeReason</u>: (optional)<br />
	/// <u>ISM:noticeDate</u>: (optional)<br />
	/// <u>ISM:unregisteredNoticeType</u>: (optional)<br />
	/// <u>ISM:externalNotice</u>: (optional, starting in DDMS 4.1)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class NoticeAttributes : AbstractAttributeGroup {
		private string _noticeType = null;
		private string _noticeReason = null;
		private DateTime? _noticeDate = null;
		private string _unregisteredNoticeType = null;
		private bool? _externalNotice = null;

		/// <summary>
		/// Attribute name </summary>
		public const string NOTICE_TYPE_NAME = "noticeType";

		/// <summary>
		/// Attribute name </summary>
		public const string NOTICE_REASON_NAME = "noticeReason";

		/// <summary>
		/// Attribute name </summary>
		public const string NOTICE_DATE_NAME = "noticeDate";

		/// <summary>
		/// Attribute name </summary>
		public const string UNREGISTERED_NOTICE_TYPE_NAME = "unregisteredNoticeType";

		/// <summary>
		/// Attribute name </summary>
		public const string EXTERNAL_NOTICE_NAME = "externalNotice";

		/// <summary>
		/// Maximum length of reason and unregistered notice type attributes. </summary>
		public const int MAX_LENGTH = 2048;

		private static readonly HashSet<string> ALL_NAMES = new HashSet<string>();
		static NoticeAttributes() {
			ALL_NAMES.Add(NOTICE_TYPE_NAME);
			ALL_NAMES.Add(NOTICE_REASON_NAME);
			ALL_NAMES.Add(NOTICE_DATE_NAME);
			ALL_NAMES.Add(UNREGISTERED_NOTICE_TYPE_NAME);
			ALL_NAMES.Add(EXTERNAL_NOTICE_NAME);
		}

		/// <summary>
		/// A set of all SecurityAttribute names which should not be converted into ExtensibleAttributes </summary>
		public static readonly HashSet<string> NON_EXTENSIBLE_NAMES = ALL_NAMES;

		/// <summary>
		/// Returns a non-null instance of notice attributes. If the instance passed in is not null, it will be returned.
		/// </summary>
		/// <param name="noticeAttributes"> the attributes to return by default </param>
		/// <returns> a non-null attributes instance </returns>
		/// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NoticeAttributes getNonNullInstance(NoticeAttributes noticeAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public static NoticeAttributes GetNonNullInstance(NoticeAttributes noticeAttributes) {
			return (noticeAttributes == null ? new NoticeAttributes(null, null, null, null) : noticeAttributes);
		}

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="element"> the XOM element which is decorated with these attributes. </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public NoticeAttributes(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public NoticeAttributes(Element element) : base(element.Name.NamespaceName) {
			string icNamespace = DDMSVersion.IsmNamespace;

			_noticeType = element.Attribute(XName.Get(NOTICE_TYPE_NAME, icNamespace)).Value;
			_noticeReason = element.Attribute(XName.Get(NOTICE_REASON_NAME, icNamespace)).Value;
			_unregisteredNoticeType = element.Attribute(XName.Get(UNREGISTERED_NOTICE_TYPE_NAME, icNamespace)).Value;
			string noticeDate = element.Attribute(XName.Get(NOTICE_DATE_NAME, icNamespace)).Value;
			if (!String.IsNullOrEmpty(noticeDate)) {
				_noticeDate = DateTime.Parse(noticeDate);
			}
			string external = element.Attribute(XName.Get(EXTERNAL_NOTICE_NAME, icNamespace)).Value;
			if (!String.IsNullOrEmpty(external)) {
				_externalNotice = Convert.ToBoolean(external);
			}
			Validate();
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		///  @deprecated A new constructor was added for DDMS 4.1 to support ism:externalNotice. This constructor is preserved for 
		/// backwards compatibility, but may disappear in the next major release.
		/// 
		/// <param name="noticeType"> the notice type (with a value from the CVE) </param>
		/// <param name="noticeReason"> the reason associated with a notice </param>
		/// <param name="noticeDate"> the date associated with a notice </param>
		/// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public NoticeAttributes(String noticeType, String noticeReason, String noticeDate, String unregisteredNoticeType) throws DDMSSense.DDMS.InvalidDDMSException
		public NoticeAttributes(string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType) : this(noticeType, noticeReason, noticeDate, unregisteredNoticeType, null) {
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="noticeType"> the notice type (with a value from the CVE) </param>
		/// <param name="noticeReason"> the reason associated with a notice </param>
		/// <param name="noticeDate"> the date associated with a notice </param>
		/// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
		/// <param name="externalNotice"> true if this notice is for an external resource </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public NoticeAttributes(String noticeType, String noticeReason, String noticeDate, String unregisteredNoticeType, Boolean externalNotice) throws DDMSSense.DDMS.InvalidDDMSException
		public NoticeAttributes(string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType, bool? externalNotice) : base(DDMSVersion.GetCurrentVersion().Namespace) {
			_noticeType = noticeType;
			_noticeReason = noticeReason;
			_unregisteredNoticeType = unregisteredNoticeType;
			if (!String.IsNullOrEmpty(noticeDate)) {
				try {
					_noticeDate = DateTime.Parse(noticeDate);
				} catch (System.ArgumentException) {
					throw new InvalidDDMSException("The ISM:noticeDate attribute is not in a valid date format.");
				}
			}
			_externalNotice = externalNotice;
			Validate();
		}

		/// <summary>
		/// Convenience method to add these attributes onto an existing XOM Element
		/// </summary>
		/// <param name="element"> the element to decorate </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void addTo(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public void AddTo(Element element) {
			DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
			ValidateSameVersion(elementVersion);
			string icNamespace = elementVersion.IsmNamespace;
			string icPrefix = PropertyReader.GetPrefix("ism");

			Util.AddAttribute(element, icPrefix, NOTICE_TYPE_NAME, icNamespace, NoticeType);
			Util.AddAttribute(element, icPrefix, NOTICE_REASON_NAME, icNamespace, NoticeReason);
			if (NoticeDate != null) {
				Util.AddAttribute(element, icPrefix, NOTICE_DATE_NAME, icNamespace, NoticeDate.ToString("o"));
			}
			Util.AddAttribute(element, icPrefix, UNREGISTERED_NOTICE_TYPE_NAME, icNamespace, UnregisteredNoticeType);
			if (ExternalReference != null) {
				Util.AddAttribute(element, icPrefix, EXTERNAL_NOTICE_NAME, icNamespace, Convert.ToString(ExternalReference));
			}
		}

		/// <summary>
		/// Checks if any attributes have been set.
		/// </summary>
		/// <returns> true if no attributes have values, false otherwise </returns>
		public bool Empty {
			get {
				return (String.IsNullOrEmpty(NoticeType) && String.IsNullOrEmpty(NoticeReason) && String.IsNullOrEmpty(UnregisteredNoticeType) && NoticeDate == null && ExternalReference == null);
			}
		}

		/// <summary>
		/// Validates the attribute group. Where appropriate the <seealso cref="ISMVocabulary"/> enumerations are validated.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>If set, the noticeType attribute must be a valid token.</li>
		/// <li>The noticeReason must be shorter than 2048 characters.</li>
		/// <li>The unregisteredNoticeType must be shorter than 2048 characters.</li>
		/// <li>If set, the noticeDate attribute is a valid xs:date value.</li>
		/// <li>These attributes cannot be used until DDMS 4.0.1 or later.</li>
		/// <li>The externalNotice attribute cannot be used until DDMS 4.1 or later.</li>
		/// <li>Does NOT do any validation on the constraints described in the DES ISM specification.</li>
		/// </td></tr></table>
		/// </summary>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			// Should be reviewed as additional versions of DDMS are supported.
			DDMSVersion version = DDMSVersion;
			if (version.IsAtLeast("4.0.1") && !String.IsNullOrEmpty(NoticeType)) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NOTICE_TYPE, NoticeType);
			}
			if (!String.IsNullOrEmpty(NoticeReason) && NoticeReason.Length > MAX_LENGTH) {
				throw new InvalidDDMSException("The noticeReason attribute must be shorter than " + MAX_LENGTH + " characters.");
			}
			if (!String.IsNullOrEmpty(UnregisteredNoticeType) && UnregisteredNoticeType.Length > MAX_LENGTH) {
				throw new InvalidDDMSException("The unregisteredNoticeType attribute must be shorter than " + MAX_LENGTH + " characters.");
			}
			if (NoticeDate != null) {
				throw new InvalidDDMSException("The noticeDate attribute must be in the xs:date format (YYYY-MM-DD).");
			}
			if (!version.IsAtLeast("4.0.1") && !Empty) {
				throw new InvalidDDMSException("Notice attributes cannot be used until DDMS 4.0.1 or later.");
			}
			// Test for 4.1 externalNotice is implicit, since 4.0.1 and 4.1 have same XML namespace.

			base.Validate();
		}

		/// <seealso cref= AbstractAttributeGroup#getOutput(boolean, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix) {
			string localPrefix = Util.GetNonNullString(prefix);
			StringBuilder text = new StringBuilder();
			text.Append(Resource.BuildOutput(isHTML, localPrefix + NOTICE_TYPE_NAME, NoticeType));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + NOTICE_REASON_NAME, NoticeReason));
			if (NoticeDate != null) {
				text.Append(Resource.BuildOutput(isHTML, localPrefix + NOTICE_DATE_NAME, NoticeDate.ToString("o")));
			}
			text.Append(Resource.BuildOutput(isHTML, localPrefix + UNREGISTERED_NOTICE_TYPE_NAME, UnregisteredNoticeType));
			if (ExternalReference != null) {
				text.Append(Resource.BuildOutput(isHTML, localPrefix + EXTERNAL_NOTICE_NAME, Convert.ToString(ExternalReference)));
			}

			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!(obj is NoticeAttributes)) {
				return (false);
			}
			NoticeAttributes test = (NoticeAttributes) obj;
			return (NoticeType.Equals(test.NoticeType) && NoticeReason.Equals(test.NoticeReason) && UnregisteredNoticeType.Equals(test.UnregisteredNoticeType) && Util.NullEquals(NoticeDate, test.NoticeDate) && Util.NullEquals(ExternalReference, test.ExternalReference));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = 0;
			result = 7 * result + NoticeType.GetHashCode();
			result = 7 * result + NoticeReason.GetHashCode();
			result = 7 * result + UnregisteredNoticeType.GetHashCode();
			if (NoticeDate != null) {
				result = 7 * result + NoticeDate.GetHashCode();
			}
			if (ExternalReference != null) {
				result = 7 * result + ExternalReference.GetHashCode();
			}
			return (result);
		}

		/// <summary>
		/// Accessor for the noticeType attribute.
		/// </summary>
		public string NoticeType {
			get {
				return (Util.GetNonNullString(_noticeType));
			}
			set {
                _noticeDate = DateTime.Parse(value);
			}
		}

		/// <summary>
		/// Accessor for the noticeReason attribute.
		/// </summary>
		public string NoticeReason {
			get {
				return (Util.GetNonNullString(_noticeReason));
			}
			set {
                _noticeReason = value;
			}
		}

		/// <summary>
		/// Accessor for the unregisteredNoticeType attribute.
		/// </summary>
		public string UnregisteredNoticeType {
			get {
				return (Util.GetNonNullString(_unregisteredNoticeType));
			}
			set {
                _unregisteredNoticeType = value;
			}
		}

		/// <summary>
		/// Accessor for the externalNotice attribute. This may be null before DDMS 4.1.
		/// </summary>
		public bool? ExternalReference {
			get {
				return (_externalNotice);
			}
		}

		/// <summary>
		/// Accessor for the noticeDate attribute. May return null if not set.
		/// </summary>
		public DateTime NoticeDate {
			get {
				return DateTime.Parse(_noticeDate.Value.ToString("o"));
			}
			set {
                _noticeDate = value;
			}
		}

		/// <summary>
		/// Builder for these attributes.
		/// 
		/// <para>This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
		/// standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
		/// null.
		/// 
		/// </para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public class Builder {
			internal const long SerialVersionUID = 279072341662308051L;
			internal IDictionary<string, string> _stringAttributes = new Dictionary<string, string>();
			internal bool? _externalNotice;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(NoticeAttributes attributes) {
				NoticeType = attributes.NoticeType;
				NoticeReason = attributes.NoticeReason;
				if (attributes.NoticeDate != null) {
					NoticeDate = attributes.NoticeDate.ToString("o");
				}
				UnregisteredNoticeType = attributes.UnregisteredNoticeType;
				ExternalNotice = attributes.ExternalReference;
			}

			/// <summary>
			/// Finalizes the data gathered for this builder instance. Will always return an empty instance instead of
			/// a null one.
			/// </summary>
			/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public NoticeAttributes commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual NoticeAttributes Commit() {
				return (new NoticeAttributes(NoticeType, NoticeReason, NoticeDate, UnregisteredNoticeType, ExternalNotice));
			}

			/// <summary>
			/// Checks if any values have been provided for this Builder.
			/// </summary>
			/// <returns> true if every field is empty </returns>
			public virtual bool Empty {
				get {
					bool isEmpty = true;
					foreach (string value in StringAttributes.Values) {
						isEmpty = isEmpty && String.IsNullOrEmpty(value);
					}
					return (isEmpty && ExternalNotice == null);
				}
			}

			/// <summary>
			/// Builder accessor for the noticeType attribute
			/// </summary>
			public virtual string NoticeType {
				get {
					return (StringAttributes.GetValueOrNull(NOTICE_TYPE_NAME));
				}
                set { StringAttributes[NOTICE_TYPE_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the noticeReason attribute
			/// </summary>
			public virtual string NoticeReason {
				get {
					return (StringAttributes.GetValueOrNull(NOTICE_REASON_NAME));
				}
                set { StringAttributes[NOTICE_REASON_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the noticeDate attribute
			/// </summary>
			public virtual string NoticeDate {
				get {
					return (StringAttributes.GetValueOrNull(NOTICE_DATE_NAME));
				}
                set { StringAttributes[NOTICE_DATE_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the unregisteredNoticeType attribute
			/// </summary>
			public virtual string UnregisteredNoticeType {
				get {
					return (StringAttributes.GetValueOrNull(UNREGISTERED_NOTICE_TYPE_NAME));
				}
                set { StringAttributes[UNREGISTERED_NOTICE_TYPE_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the externalNotice attribute
			/// </summary>
			public virtual bool? ExternalNotice {
				get {
					return (_externalNotice);
				}
				set {
					_externalNotice = value;
				}
			}


			/// <summary>
			/// Accessor for the map of attribute names to string values
			/// </summary>
			internal virtual IDictionary<string, string> StringAttributes {
				get {
					return (_stringAttributes);
				}
			}
		}
	}
}