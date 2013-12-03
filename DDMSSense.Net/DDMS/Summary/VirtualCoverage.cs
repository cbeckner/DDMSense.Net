using System;
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
namespace DDMSSense.DDMS.Summary {

	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:virtualCoverage.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para><ul>
	/// <li>If address is specified, protocol must not be empty.</li>
	/// </ul>
	/// 
	/// <para>
	/// DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A virtualCoverage element can be used with no attributes.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:address</u>: a computer or telecommunications network address, or a network name or locale. (optional).<br />
	/// <u>ddms:protocol</u>: the type of rules for data transfer that apply to the Virtual Address (can stand alone, but
	/// should be used if address is provided)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional. (starting in DDMS 3.0)
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class VirtualCoverage : AbstractBaseComponent {

		private SecurityAttributes _securityAttributes = null;

		private const string ADDRESS_NAME = "address";
		private const string PROTOCOL_NAME = "protocol";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public VirtualCoverage(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public VirtualCoverage(Element element) {
			try {
				_securityAttributes = new SecurityAttributes(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="address"> the virtual address (optional) </param>
		/// <param name="protocol"> the network protocol (optional, should be used if address is provided) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public VirtualCoverage(String address, String protocol, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public VirtualCoverage(string address, string protocol, SecurityAttributes securityAttributes) {
			try {
				Element element = Util.BuildDDMSElement(VirtualCoverage.GetName(DDMSVersion.GetCurrentVersion()), null);
				Util.AddDDMSAttribute(element, ADDRESS_NAME, address);
				Util.AddDDMSAttribute(element, PROTOCOL_NAME, protocol);
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
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
		/// <li>If an address is provided, the protocol is required and must not be empty.</li>
		/// <li>The SecurityAttributes do not exist until DDMS 3.0 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, VirtualCoverage.GetName(DDMSVersion));
			if (!String.IsNullOrEmpty(Address)) {
				Util.RequireDDMSValue(PROTOCOL_NAME, Protocol);
			}
			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("3.0") && !SecurityAttributes.Empty) {
				throw new InvalidDDMSException("Security attributes cannot be applied to this component until DDMS 3.0 or later.");
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A completely empty ddms:virtualCoverage element was found.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (String.IsNullOrEmpty(Address) && String.IsNullOrEmpty(Protocol)) {
				AddWarning("A completely empty ddms:virtualCoverage element was found.");
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + ADDRESS_NAME, Address));
			text.Append(BuildOutput(isHTML, localPrefix + PROTOCOL_NAME, Protocol));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is VirtualCoverage)) {
				return (false);
			}
			VirtualCoverage test = (VirtualCoverage) obj;
			return (Address.Equals(test.Address) && Protocol.Equals(test.Protocol));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Address.GetHashCode();
			result = 7 * result + Protocol.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("virtualCoverage");
		}

		/// <summary>
		/// Accessor for the address attribute (optional)
		/// </summary>
		public string Address {
			get {
				return (GetAttributeValue(ADDRESS_NAME));
			}
			set {
					_address = value;
			}
		}

		/// <summary>
		/// Accessor for the protocol attribute (optional, should be used if address is supplied)
		/// </summary>
		public string Protocol {
			get {
				return (GetAttributeValue(PROTOCOL_NAME));
			}
			set {
					_protocol = value;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes.  Will always be non-null, even if it has no values set.
		/// </summary>
		public override SecurityAttributes SecurityAttributes {
			get {
				return (_securityAttributes);
			}
			set {
					_securityAttributes = value;
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
			internal const long SerialVersionUID = 2986952678400201045L;
			internal string _address;
			internal string _protocol;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(VirtualCoverage coverage) {
				Address = coverage.Address;
				Protocol = coverage.Protocol;
				SecurityAttributes = new SecurityAttributes.Builder(coverage.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public VirtualCoverage commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual VirtualCoverage Commit() {
				return (Empty ? null : new VirtualCoverage(Address, Protocol, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Address) && String.IsNullOrEmpty(Protocol) && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the address attribute
			/// </summary>
			public virtual string Address {
				get {
					return _address;
				}
			}


			/// <summary>
			/// Builder accessor for the protocol attribute
			/// </summary>
			public virtual string Protocol {
				get {
					return _protocol;
				}
			}


			/// <summary>
			/// Builder accessor for the Security Attributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
			}

		}
	}
}