using System;
using System.Collections.Generic;

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
namespace DDMSSense {


	using Element = System.Xml.Linq.XElement;
	using IBuilder = DDMSSense.DDMS.IBuilder;
	using IDDMSComponent = DDMSSense.DDMS.IDDMSComponent;
	using InvalidDDMSException = DDMSSense.DDMS.InvalidDDMSException;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using SystemName = DDMSSense.DDMS.SecurityElements.Ntk.SystemName;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using PropertyReader = DDMSSense.Util.PropertyReader;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// Base class for NTK elements which describe system access rules for an individual, group, or profile.
	/// 
	/// <para> Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
	/// before the component is used. </para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ntk:AccessSystemName</u>: The system described by this access record (exactly 1 required), implemented as a 
	/// <seealso cref="SystemName"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public abstract class AbstractAccessEntity : AbstractBaseComponent {

		private SystemName _systemName = null;
		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element. Does not validate.
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public AbstractAccessEntity(Element element) {
			SetXOMElement(element, false);
			Element systemElement = element.Element(XName.Get(SystemName.GetName(DDMSVersion), Namespace));
			if (systemElement != null) {
				_systemName = new SystemName(systemElement);
			}
			_securityAttributes = new SecurityAttributes(element);
		}

		/// <summary>
		/// Constructor for creating a component from raw data. Does not validate.
		/// </summary>
		/// <param name="systemName"> the system name (required) </param>
		/// <param name="securityAttributes"> security attributes (required) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public AbstractAccessEntity(string name, SystemName systemName, SecurityAttributes securityAttributes) {
			DDMSVersion version = DDMSVersion.GetCurrentVersion();
			Element element = DDMSSense.Util.Util.BuildElement(PropertyReader.GetPrefix("ntk"), name, version.NtkNamespace, null);
			SetXOMElement(element, false);
			if (systemName != null) {
				element.Add(systemName.XOMElementCopy);
			}
			_systemName = systemName;
			_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
			_securityAttributes.AddTo(element);
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A systemName is required.</li>
		/// <li>Exactly 1 systemName exists.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			DDMSSense.Util.Util.RequireDDMSValue("systemName", SystemName);
            DDMSSense.Util.Util.RequireBoundedChildCount(Element, SystemName.GetName(DDMSVersion), 1, 1);
            DDMSSense.Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
			SecurityAttributes.RequireClassification();

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(SystemName);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is AbstractAccessEntity)) {
				return (false);
			}
			return (true);
		}

		/// <summary>
		/// Accessor for the system name
		/// </summary>
		public virtual SystemName SystemName {
			get {
				return _systemName;
			}
			set {
					_systemName = value;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
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
		/// @since 2.0.0 </seealso>
		[Serializable]
		public abstract class Builder : IBuilder {
			public abstract IDDMSComponent Commit();
			internal const long SerialVersionUID = 7851044806424206976L;
			internal SystemName.Builder _systemName;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(AbstractAccessEntity group) {
				if (group.SystemName != null) {
					SystemName = new SystemName.Builder(group.SystemName);
				}
				SecurityAttributes = new SecurityAttributes.Builder(group.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (SystemName.Empty && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the systemName
			/// </summary>
			public virtual SystemName.Builder SystemName {
				get {
					if (_systemName == null) {
						_systemName = new SystemName.Builder();
					}
					return _systemName;
				}
                set { _systemName = value; }
			}


			/// <summary>
			/// Builder accessor for the securityAttributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
                set { _securityAttributes = value; }
			}

		}
	}
}