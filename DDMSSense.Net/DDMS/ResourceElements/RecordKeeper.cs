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
namespace DDMSSense.DDMS.ResourceElements {


	using Element = System.Xml.Linq.XElement;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:recordKeeper.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>The recordKeeperID must not be empty.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:recordKeeperID</u>: A unique identifier for the Record Keeper (exactly 1 required)<br />
	/// <u>ddms:organization</u>: The organization which acts as the record keeper (exactly 1 required), implemented as an
	/// <seealso cref="Organization"/><br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class RecordKeeper : AbstractBaseComponent {

		private Organization _organization = null;

		private const string RECORD_KEEPER_ID_NAME = "recordKeeperID";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public RecordKeeper(Element element) {
			try {
				Util.RequireDDMSValue("element", element);
				if (element.Nodes().Count() > 1) {
					Element organizationElement = (XElement)element.FirstNode;
					if (organizationElement != null) {
						_organization = new Organization(organizationElement);
					}
				}
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="recordKeeperID"> a unique ID for the organization (required) </param>
		/// <param name="organization"> the organization acting as record keeper (required) </param>


		public RecordKeeper(string recordKeeperID, Organization organization) {
			try {
				Element element = Util.BuildDDMSElement(RecordKeeper.GetName(DDMSVersion.GetCurrentVersion()), null);
				if (!String.IsNullOrEmpty(recordKeeperID)) {
					element.Add(Util.BuildDDMSElement(RECORD_KEEPER_ID_NAME, recordKeeperID));
				}
				if (organization != null) {
					element.Add(organization.XOMElementCopy);
				}
				_organization = organization;
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
		/// <li>The recordKeeperID exists.</li>
		/// <li>The organization exists.</li>
		/// <li>Exactly 1 organization exists.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractProducerRole#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, RecordKeeper.GetName(DDMSVersion));
			Util.RequireDDMSValue("record keeper ID", RecordKeeperID);
			Util.RequireDDMSValue("organization", Organization);
			Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 1, 1);

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is RecordKeeper)) {
				return (false);
			}
			RecordKeeper test = (RecordKeeper) obj;
			return (RecordKeeperID.Equals(test.RecordKeeperID));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + RecordKeeperID.GetHashCode();
			return (result);
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + RECORD_KEEPER_ID_NAME, RecordKeeperID));
			text.Append(Organization.GetOutput(isHTML, localPrefix, ""));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(Organization);
				return (list);
			}
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("recordKeeper");
		}

		/// <summary>
		/// Accessor for the recordKeeperID
		/// </summary>
		public virtual string RecordKeeperID {
			get {
				return (Util.GetFirstDDMSChildValue(Element, RECORD_KEEPER_ID_NAME));
			}
		}

		/// <summary>
		/// Accessor for the organization
		/// </summary>
		public virtual Organization Organization {
			get {
				return (_organization);
			}
			set {
					_organization = value;
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
			internal const long SerialVersionUID = 4565840434345629470L;
			internal string _recordKeeperID;
			internal Organization.Builder _organization;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(RecordKeeper keeper) {
				RecordKeeperID = keeper.RecordKeeperID;
				Organization = new Organization.Builder(keeper.Organization);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				return (Empty ? null : new RecordKeeper(RecordKeeperID, Organization.Commit()));
			}

			/// <summary>
			/// Helper method to determine if any values have been entered.
			/// </summary>
			/// <returns> true if all values are empty </returns>
			public virtual bool Empty {
				get {
					return (Organization.Empty && String.IsNullOrEmpty(RecordKeeperID));
				}
			}

			/// <summary>
			/// Builder accessor for the recordKeeperID
			/// </summary>
			public virtual string RecordKeeperID {
				get {
					return _recordKeeperID;
				}
                set { _recordKeeperID = value; }
			}


			/// <summary>
			/// Builder accessor for the organization builder
			/// </summary>
			public virtual Organization.Builder Organization {
				get {
					if (_organization == null) {
						_organization = new Organization.Builder();
					}
					return _organization;
				}
                set { _organization = value; }
			}

		}
	}

}