using System;
using System.Collections.Generic;
using System.Text;

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
namespace DDMSSense.DDMS.ResourceElements {



	using Element = System.Xml.Linq.XElement;
	
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:dates.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A dates element can be used with none of the seven date values set.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:acquiredOn</u>: the acquisition date (0-many, starting in DDMS 4.1), implemented as an <seealso cref="ApproximableDate"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:created</u>: creation date (optional)<br />
	/// <u>ddms:posted</u>: posting date (optional)<br />
	/// <u>ddms:validTil</u>: expiration date (optional)<br />
	/// <u>ddms:infoCutOff</u>: info cutoff date (optional)<br />
	/// <u>ddms:approvedOn</u>: approved for posting date (optional, starting in DDMS 3.1)<br />
	/// <u>ddms:receivedOn</u>: received date (optional, starting in DDMS 4.0.1)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Dates : AbstractBaseComponent {

		private List<ApproximableDate> _acquiredOns = null;

		private const string CREATED_NAME = "created";
		private const string POSTED_NAME = "posted";
		private const string VALID_TIL_NAME = "validTil";
		private const string INFO_CUT_OFF_NAME = "infoCutOff";
		private const string APPROVED_ON_NAME = "approvedOn";
		private const string RECEIVED_ON_NAME = "receivedOn";
		private const string ACQUIRED_ON_NAME = "acquiredOn";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Dates(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public Dates(Element element) {
			try {
				SetXOMElement(element, false);
				_acquiredOns = new List<ApproximableDate>();
				IEnumerable<Element> acquiredOns = element.Elements(XName.Get(ACQUIRED_ON_NAME, Namespace));
				foreach(var el in acquiredOns)
					_acquiredOns.Add(new ApproximableDate(el));
				
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data. 
		/// </summary>
		/// @deprecated A new constructor was added for DDMS 4.1 to support ddms:acquiredOn. This constructor is preserved for 
		/// backwards compatibility, but may disappear in the next major release.
		/// 
		/// <param name="created"> the creation date (optional) </param>
		/// <param name="posted"> the posting date (optional) </param>
		/// <param name="validTil"> the expiration date (optional) </param>
		/// <param name="infoCutOff"> the info cutoff date (optional) </param>
		/// <param name="approvedOn"> the approved on date (optional, starting in DDMS 3.1) </param>
		/// <param name="receivedOn"> the received on date (optional, starting in DDMS 4.0.1) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Dates(String created, String posted, String validTil, String infoCutOff, String approvedOn, String receivedOn) throws DDMSSense.DDMS.InvalidDDMSException
		public Dates(string created, string posted, string validTil, string infoCutOff, string approvedOn, string receivedOn) : this(null, created, posted, validTil, infoCutOff, approvedOn, receivedOn) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data. 
		/// </summary>
		/// <param name="acquiredOns"> the acquisition dates (optional, starting in DDMS 4.1) </param>
		/// <param name="created"> the creation date (optional) </param>
		/// <param name="posted"> the posting date (optional) </param>
		/// <param name="validTil"> the expiration date (optional) </param>
		/// <param name="infoCutOff"> the info cutoff date (optional) </param>
		/// <param name="approvedOn"> the approved on date (optional, starting in DDMS 3.1) </param>
		/// <param name="receivedOn"> the received on date (optional, starting in DDMS 4.0.1)
		/// </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Dates(java.util.List<DDMSSense.DDMS.ApproximableDate> acquiredOns, String created, String posted, String validTil, String infoCutOff, String approvedOn, String receivedOn) throws DDMSSense.DDMS.InvalidDDMSException
		public Dates(List<ApproximableDate> acquiredOns, string created, string posted, string validTil, string infoCutOff, string approvedOn, string receivedOn) {
			try {
				Element element = Util.BuildDDMSElement(Dates.GetName(DDMSVersion.GetCurrentVersion()), null);
				if (acquiredOns == null) {
					acquiredOns = new List<ApproximableDate>();
				}
				_acquiredOns = acquiredOns;
				foreach (ApproximableDate acquiredOn in acquiredOns) {
					element.Add(acquiredOn.XOMElementCopy);
				}

				Util.AddDDMSAttribute(element, CREATED_NAME, created);
				Util.AddDDMSAttribute(element, POSTED_NAME, posted);
				Util.AddDDMSAttribute(element, VALID_TIL_NAME, validTil);
				Util.AddDDMSAttribute(element, INFO_CUT_OFF_NAME, infoCutOff);
				Util.AddDDMSAttribute(element, APPROVED_ON_NAME, approvedOn);
				Util.AddDDMSAttribute(element, RECEIVED_ON_NAME, receivedOn);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>If set, each date attribute adheres to an acceptable date format.</li>
		/// <li>The approvedOn date cannot be used until DDMS 3.1 or later.</li>
		/// <li>The receivedOn date cannot be used until DDMS 4.0.1 or later.</li>
		/// <li>An acquiredOn date cannot be used until DDMS 4.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Dates.GetName(DDMSVersion));
			if (!String.IsNullOrEmpty(CreatedString)) {
				Util.RequireDDMSDateFormat(CreatedString, Namespace);
			}
			if (!String.IsNullOrEmpty(PostedString)) {
				Util.RequireDDMSDateFormat(PostedString, Namespace);
			}
			if (!String.IsNullOrEmpty(ValidTilString)) {
				Util.RequireDDMSDateFormat(ValidTilString, Namespace);
			}
			if (!String.IsNullOrEmpty(InfoCutOffString)) {
				Util.RequireDDMSDateFormat(InfoCutOffString, Namespace);
			}
			if (!String.IsNullOrEmpty(ApprovedOnString)) {
				Util.RequireDDMSDateFormat(ApprovedOnString, Namespace);
			}
			if (!String.IsNullOrEmpty(ReceivedOnString)) {
				Util.RequireDDMSDateFormat(ReceivedOnString, Namespace);
			}

			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("3.1") && !String.IsNullOrEmpty(ApprovedOnString)) {
				throw new InvalidDDMSException("This component cannot have an approvedOn date until DDMS 3.1 or later.");
			}
			if (!DDMSVersion.IsAtLeast("4.0.1") && !String.IsNullOrEmpty(ReceivedOnString)) {
				throw new InvalidDDMSException("This component cannot have a receivedOn date until DDMS 4.0.1 or later.");
			}
			if (!DDMSVersion.IsAtLeast("4.1") && AcquiredOns.Count > 0) {
				throw new InvalidDDMSException("This component cannot have an acquiredOn date until DDMS 4.1 or later.");
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A completely empty ddms:dates element was found.</li>
		/// <li>A ddms:acquiredOn element may cause issues for DDMS 4.0 records.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (String.IsNullOrEmpty(CreatedString) && String.IsNullOrEmpty(PostedString) && String.IsNullOrEmpty(ValidTilString) && String.IsNullOrEmpty(InfoCutOffString) && String.IsNullOrEmpty(ApprovedOnString) && String.IsNullOrEmpty(ReceivedOnString) && AcquiredOns.Count == 0) {
				AddWarning("A completely empty ddms:dates element was found.");
			}
			if (AcquiredOns.Count > 0) {
				AddDdms40Warning("ddms:acquiredOn element");
			}

			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, AcquiredOns));
			text.Append(BuildOutput(isHTML, localPrefix + CREATED_NAME, CreatedString));
			text.Append(BuildOutput(isHTML, localPrefix + POSTED_NAME, PostedString));
			text.Append(BuildOutput(isHTML, localPrefix + VALID_TIL_NAME, ValidTilString));
			text.Append(BuildOutput(isHTML, localPrefix + INFO_CUT_OFF_NAME, InfoCutOffString));
			text.Append(BuildOutput(isHTML, localPrefix + APPROVED_ON_NAME, ApprovedOnString));
			text.Append(BuildOutput(isHTML, localPrefix + RECEIVED_ON_NAME, ReceivedOnString));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(AcquiredOns);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Dates)) {
				return (false);
			}
			Dates test = (Dates) obj;
			return (CreatedString.Equals(test.CreatedString) && PostedString.Equals(test.PostedString) && ValidTilString.Equals(test.ValidTilString) && InfoCutOffString.Equals(test.InfoCutOffString) && ApprovedOnString.Equals(test.ApprovedOnString) && ReceivedOnString.Equals(test.ReceivedOnString));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + CreatedString.GetHashCode();
			result = 7 * result + PostedString.GetHashCode();
			result = 7 * result + ValidTilString.GetHashCode();
			result = 7 * result + InfoCutOffString.GetHashCode();
			result = 7 * result + ApprovedOnString.GetHashCode();
			result = 7 * result + ReceivedOnString.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("dates");
		}

		/// <summary>
		/// Accessor for the created date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getCreatedString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? Created {
			get {
				try {
					return (DateTime.Parse(CreatedString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the created date (optional).
		/// </summary>
		public string CreatedString {
			get {
				return (GetAttributeValue(CREATED_NAME));
			}
		}

		/// <summary>
		/// Accessor for the posted date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getPostedString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? Posted {
			get {
				try {
					return (DateTime.Parse(PostedString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the posted date (optional).
		/// </summary>
		public string PostedString {
			get {
				return (GetAttributeValue(POSTED_NAME));
			}
		}

		/// <summary>
		/// Accessor for the expiration date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getValidTilString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? ValidTil {
			get {
				try {
					return (DateTime.Parse(ValidTilString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the expiration date (optional).
		/// </summary>
		public string ValidTilString {
			get {
				return (GetAttributeValue(VALID_TIL_NAME));
			}
		}

		/// <summary>
		/// Accessor for the cutoff date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getInfoCutOffString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? InfoCutOff {
			get {
				try {
					return (DateTime.Parse(InfoCutOffString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the cutoff date (optional).
		/// </summary>
		public string InfoCutOffString {
			get {
				return (GetAttributeValue(INFO_CUT_OFF_NAME));
			}
		}

		/// <summary>
		/// Accessor for the approved on date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getApprovedOnString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? ApprovedOn {
			get {
				try {
					return (DateTime.Parse(ApprovedOnString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the approved on date (optional).
		/// </summary>
		public string ApprovedOnString {
			get {
				return (GetAttributeValue(APPROVED_ON_NAME));
			}
		}

		/// <summary>
		/// Accessor for the received on date (optional). Returns a copy.
		/// </summary>
		/// @deprecated Because DDMS 4.1 added a new allowable date format (ddms:DateHourMinType),
		/// DateTime is no longer a sufficient representation. This accessor will return
		/// null for dates in the new format. Use <code>getReceivedOnString()</code> to
		/// access the raw XML format of the date instead. 
		public DateTime? ReceivedOn {
			get {
				try {
					return (DateTime.Parse(ReceivedOnString));
				} catch (System.ArgumentException) {
					return (null);
				}
			}
		}

		/// <summary>
		/// Accessor for the received on date (optional).
		/// </summary>
		public string ReceivedOnString {
			get {
				return (GetAttributeValue(RECEIVED_ON_NAME));
			}
		}

		/// <summary>
		/// Accessor for the acquiredOn dates (0-many optional). 
		/// </summary>
		public List<ApproximableDate> AcquiredOns {
			get {
				return _acquiredOns;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = -2857638896738260719L;
			internal List<ApproximableDate.Builder> _acquiredOns;
			internal string _created;
			internal string _posted;
			internal string _validTil;
			internal string _infoCutOff;
			internal string _approvedOn;
			internal string _receivedOn;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Dates dates) {
				foreach (ApproximableDate acquiredOn in dates.AcquiredOns) {
					AcquiredOns.Add(new ApproximableDate.Builder(acquiredOn));
				}
				Created = dates.CreatedString;
				Posted = dates.PostedString;
				ValidTil = dates.ValidTilString;
				InfoCutOff = dates.InfoCutOffString;
				ApprovedOn = dates.ApprovedOnString;
				ReceivedOn = dates.ReceivedOnString;
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Dates commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual Dates Commit() {
				if (Empty) {
					return (null);
				}
				List<ApproximableDate> acquiredOns = new List<ApproximableDate>();
				foreach (IBuilder builder in AcquiredOns) {
					ApproximableDate component = (ApproximableDate) builder.Commit();
					if (component != null) {
						acquiredOns.Add(component);
					}
				}
				return (new Dates(acquiredOns, Created, Posted, ValidTil, InfoCutOff, ApprovedOn, ReceivedOn));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in AcquiredOns) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && String.IsNullOrEmpty(Created) && String.IsNullOrEmpty(Posted) && String.IsNullOrEmpty(ValidTil) && String.IsNullOrEmpty(InfoCutOff) && String.IsNullOrEmpty(ApprovedOn) && String.IsNullOrEmpty(ReceivedOn));
				}
			}

			/// <summary>
			/// Builder accessor for the acquiredOn dates
			/// </summary>
			public virtual List<ApproximableDate.Builder> AcquiredOns {
				get {
					if (_acquiredOns == null) {
                        _acquiredOns = new List<ApproximableDate.Builder>();
					}
					return _acquiredOns;
				}
                set { _acquiredOns = value; }
			}

			/// <summary>
			/// Builder accessor for the created date.
			/// </summary>
			public virtual string Created {
				get {
					return _created;
				}
                set { _created = value; }
			}


			/// <summary>
			/// Builder accessor for the posted date.
			/// </summary>
			public virtual string Posted {
				get {
					return _posted;
				}
                set { _posted = value; }
			}


			/// <summary>
			/// Builder accessor for the validTil date.
			/// </summary>
			public virtual string ValidTil {
				get {
					return _validTil;
				}
                set { _validTil = value; }
			}


			/// <summary>
			/// Builder accessor for the infoCutOff date.
			/// </summary>
			public virtual string InfoCutOff {
				get {
					return _infoCutOff;
				}
                set { _infoCutOff = value; }
			}


			/// <summary>
			/// Builder accessor for the approvedOn date.
			/// </summary>
			public virtual string ApprovedOn {
				get {
					return _approvedOn;
				}
                set { _approvedOn = value; }
			}


			/// <summary>
			/// Builder accessor for the receivedOn
			/// </summary>
			public virtual string ReceivedOn {
				get {
					return _receivedOn;
				}
                set { _receivedOn = value; }
			}

		}
	}
}