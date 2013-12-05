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
    using DDMSSense.DDMS.ResourceElements;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:recordsManagementInfo.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:recordKeeper</u>: The organization responsible for the custody and ongoing management of the records 
	/// (0-1 optional), implemented as a <seealso cref="RecordKeeper"/><br />
	/// <u>ddms:applicationSoftware</u>: The software used to create the object to which this metadata applies (0-1 
	/// optional), implemented as an <seealso cref="ApplicationSoftware"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:vitalRecordIndicator</u>: An indication that a publication is categorized a vital record by the originating 
	/// agency (defaults to false)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class RecordsManagementInfo : AbstractBaseComponent {

		private RecordKeeper _recordKeeper = null;
		private ApplicationSoftware _applicationSoftware = null;

		private const string VITAL_RECORD_INDICATOR_NAME = "vitalRecordIndicator";
		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public RecordsManagementInfo(Element element) {
			try {
				SetXOMElement(element, false);
				Element recordKeeper = element.Element(XName.Get(RecordKeeper.GetName(DDMSVersion), Namespace));
				if (recordKeeper != null) {
					_recordKeeper = new RecordKeeper(recordKeeper);
				}
				Element applicationSoftware = element.Element(XName.Get(ApplicationSoftware.GetName(DDMSVersion), Namespace));
				if (applicationSoftware != null) {
					_applicationSoftware = new ApplicationSoftware(applicationSoftware);
				}
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="recordKeeper"> the record keeper (optional) </param>
		/// <param name="applicationSoftware"> the software (optional) </param>
		/// <param name="vitalRecordIndicator"> whether this is a vital record (optional, defaults to false) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public RecordsManagementInfo(RecordKeeper recordKeeper, ApplicationSoftware applicationSoftware, bool? vitalRecordIndicator) {
			try {
				Element element = Util.BuildDDMSElement(RecordsManagementInfo.GetName(DDMSVersion.GetCurrentVersion()), null);
				SetXOMElement(element, false);
				if (recordKeeper != null) {
					element.Add(recordKeeper.XOMElementCopy);
				}
				if (applicationSoftware != null) {
					element.Add(applicationSoftware.XOMElementCopy);
				}
				Util.AddDDMSAttribute(element, VITAL_RECORD_INDICATOR_NAME, Convert.ToString(vitalRecordIndicator));
				_recordKeeper = recordKeeper;
				_applicationSoftware = applicationSoftware;
				Validate();
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
		/// <li>Only 0-1 record keepers or applicationSoftwares exist.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, RecordsManagementInfo.GetName(DDMSVersion));
			Util.RequireBoundedChildCount(Element, RecordKeeper.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, ApplicationSoftware.GetName(DDMSVersion), 0, 1);

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			if (RecordKeeper != null) {
				text.Append(RecordKeeper.GetOutput(isHTML, localPrefix, ""));
			}
			if (ApplicationSoftware != null) {
				text.Append(ApplicationSoftware.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(BuildOutput(isHTML, localPrefix + VITAL_RECORD_INDICATOR_NAME, Convert.ToString(VitalRecordIndicator)));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(RecordKeeper);
				list.Add(ApplicationSoftware);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is RecordsManagementInfo)) {
				return (false);
			}
			RecordsManagementInfo test = (RecordsManagementInfo) obj;
			return (VitalRecordIndicator.Equals(test.VitalRecordIndicator));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + VitalRecordIndicator.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("recordsManagementInfo");
		}

		/// <summary>
		/// Accessor for the recordKeeper
		/// </summary>
		public RecordKeeper RecordKeeper {
			get {
				return _recordKeeper;
			}
			set {
					_recordKeeper = value;
			}
		}

		/// <summary>
		/// Accessor for the applicationSoftware
		/// </summary>
		public ApplicationSoftware ApplicationSoftware {
			get {
				return _applicationSoftware;
			}
			set {
					_applicationSoftware = value;
			}
		}

		/// <summary>
		/// Accessor for the vitalRecordIndicator attribute. This defaults to false if not found.
		/// </summary>
		public bool? VitalRecordIndicator {
			get {
				string value = GetAttributeValue(VITAL_RECORD_INDICATOR_NAME, Namespace);
				if ("true".Equals(value)) {
					return (true);
				}
				return (false);
			}			
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 7851044806424206976L;
			internal RecordKeeper.Builder _recordKeeper;
			internal ApplicationSoftware.Builder _applicationSoftware;
			internal bool? _vitalRecordIndicator;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(RecordsManagementInfo info) {
				if (info.RecordKeeper != null) {
					RecordKeeper = new RecordKeeper.Builder(info.RecordKeeper);
				}
				if (info.ApplicationSoftware != null) {
					ApplicationSoftware = new ApplicationSoftware.Builder(info.ApplicationSoftware);
				}
				VitalRecordIndicator = info.VitalRecordIndicator;
			}

			/// <seealso cref= IBuilder#commit() </seealso>


			public virtual RecordsManagementInfo Commit() {
				return (Empty ? null : new RecordsManagementInfo(RecordKeeper.Commit(), ApplicationSoftware.Commit(), VitalRecordIndicator));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (RecordKeeper.Empty && ApplicationSoftware.Empty && VitalRecordIndicator == null);
				}
			}

			/// <summary>
			/// Builder accessor for the recordKeeper
			/// </summary>
			public virtual RecordKeeper.Builder RecordKeeper {
				get {
					if (_recordKeeper == null) {
						_recordKeeper = new RecordKeeper.Builder();
					}
					return _recordKeeper;
				}
                set { _recordKeeper = value; }
			}


			/// <summary>
			/// Builder accessor for the applicationSoftware
			/// </summary>
			public virtual ApplicationSoftware.Builder ApplicationSoftware {
				get {
					if (_applicationSoftware == null) {
						_applicationSoftware = new ApplicationSoftware.Builder();
					}
					return _applicationSoftware;
				}
                set { _applicationSoftware = value; }
			}


			/// <summary>
			/// Builder accessor for the vitalRecordIndicator flag
			/// </summary>
			public virtual bool? VitalRecordIndicator {
				get {
					return _vitalRecordIndicator;
				}
                set { _vitalRecordIndicator = value; }
			}

		}
	}
}