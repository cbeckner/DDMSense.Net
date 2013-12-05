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
namespace DDMSSense {


	using Element = System.Xml.Linq.XElement;
	using IBuilder = DDMSSense.DDMS.IBuilder;
	using IDDMSComponent = DDMSSense.DDMS.IDDMSComponent;
	using IRoleEntity = DDMSSense.DDMS.IRoleEntity;
	using InvalidDDMSException = DDMSSense.DDMS.InvalidDDMSException;
    using Organization = DDMSSense.DDMS.ResourceElements.Organization;
    using Person = DDMSSense.DDMS.ResourceElements.Person;
    using Service = DDMSSense.DDMS.ResourceElements.Service;
    using Unknown = DDMSSense.DDMS.ResourceElements.Unknown;
	using ISMVocabulary = DDMSSense.DDMS.SecurityElements.Ism.ISMVocabulary;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// Base class for DDMS producer elements, such as ddms:creator and ddms:contributor.
	/// 
	/// <para>
	/// Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set before
	/// the component is used.
	/// </para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:organization</u>: The organization who is in this role (0-1, optional)<br />
	/// <u>ddms:person</u>: the person who is in this role (0-1, optional)<br />
	/// <u>ddms:service</u>: The web service who is in this role (0-1, optional)<br />
	/// <u>ddms:unknown</u>: The unknown entity who is in this role (0-1, optional)<br />
	/// Only one of the nested entities can appear in this element.
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ISM:pocType</u>: Indicates that the element specifies a point-of-contact (POC) and the methods with which
	/// to contact them (optional, starting in DDMS 4.0.1.1).<br />
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are optional.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public abstract class AbstractProducerRole : AbstractBaseComponent {

		private IRoleEntity _entity = null;
		private List<string> _pocTypes = null;
		private SecurityAttributes _securityAttributes = null;

		private const string POC_TYPE_NAME = "pocType";

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="element"> the XOM element representing this component </param>


		protected internal AbstractProducerRole(Element element) {
			try {
				SetXOMElement(element, false);
				if (element.Elements().Count() > 0) {
					Element entityElement = (XElement)element.FirstNode;
					string entityType = entityElement.Name.LocalName;
					if (Organization.GetName(DDMSVersion).Equals(entityType)) {
						_entity = new Organization(entityElement);
					}
					if (Person.GetName(DDMSVersion).Equals(entityType)) {
						_entity = new Person(entityElement);
					}
					if (Service.GetName(DDMSVersion).Equals(entityType)) {
						_entity = new Service(entityElement);
					}
					if (Unknown.GetName(DDMSVersion).Equals(entityType)) {
						_entity = new Unknown(entityElement);
					}
				}
				string pocTypes = element.Attribute(XName.Get(POC_TYPE_NAME, DDMSVersion.IsmNamespace)).Value;
                _pocTypes = DDMSSense.Util.Util.GetXsListAsList(pocTypes);
				_securityAttributes = new SecurityAttributes(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="producerType"> the type of producer this producer entity is fulfilling (i.e. creator or contributor) </param>
		/// <param name="entity"> the actual entity fulfilling this role </param>
		/// <param name="pocTypes"> the pocType attribute (starting in DDMS 4.0.1) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>


		protected internal AbstractProducerRole(string producerType, IRoleEntity entity, List<string> pocTypes, SecurityAttributes securityAttributes) {
			try {
				if (pocTypes == null) {
					pocTypes = new List<string>();
				}
                DDMSSense.Util.Util.RequireDDMSValue("producer type", producerType);
                DDMSSense.Util.Util.RequireDDMSValue("entity", entity);
                Element element = DDMSSense.Util.Util.BuildDDMSElement(producerType, null);
				element.Add(entity.XOMElementCopy);
				_entity = entity;
				if (pocTypes.Count > 0) {
                    DDMSSense.Util.Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), POC_TYPE_NAME, DDMSVersion.GetCurrentVersion().IsmNamespace, DDMSSense.Util.Util.GetXsList(pocTypes));
				}
				_pocTypes = pocTypes;
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
		/// <li>A producer entity exists.</li>
		/// <li>Only 0-1 persons, organizations, services, or unknowns exist.</li>
		/// <li>The pocType cannot be used before DDMS 4.0.1.</li>
		/// <li>If set, the pocTypes must each be a valid token.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
            DDMSSense.Util.Util.RequireDDMSValue("entity", Entity);
            DDMSSense.Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 0, 1);
            DDMSSense.Util.Util.RequireBoundedChildCount(Element, Person.GetName(DDMSVersion), 0, 1);
            DDMSSense.Util.Util.RequireBoundedChildCount(Element, Service.GetName(DDMSVersion), 0, 1);
            DDMSSense.Util.Util.RequireBoundedChildCount(Element, Unknown.GetName(DDMSVersion), 0, 1);

			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("4.0.1") && PocTypes.Count > 0) {
				throw new InvalidDDMSException("This component cannot have a pocType until DDMS 4.0.1 or later.");
			}
			if (DDMSVersion.IsAtLeast("4.0.1")) {
				foreach (string pocType in PocTypes) {
					ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_POC_TYPE, pocType);
				}
			}

			base.Validate();
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is AbstractProducerRole)) {
				return (false);
			}
			AbstractProducerRole test = (AbstractProducerRole) obj;
            return (DDMSSense.Util.Util.ListEquals(PocTypes, test.PocTypes));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + PocTypes.GetHashCode();
			return (result);
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(((AbstractBaseComponent) Entity).GetOutput(isHTML, localPrefix, ""));
            text.Append(BuildOutput(isHTML, localPrefix + POC_TYPE_NAME, DDMSSense.Util.Util.GetXsList(PocTypes)));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(Entity);
				return (list);
			}
		}

		/// <summary>
		/// Accessor for the entity fulfilling this producer role
		/// </summary>
		public virtual IRoleEntity Entity {
			get {
				return _entity;
			}
		}

		/// <summary>
		/// Accessor for the pocType attribute.
		/// </summary>
		public virtual List<string> PocTypes {
			get {
				return (_pocTypes);
			}
			set {
					_pocTypes = value;
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
		/// Abstract Builder for this DDMS component.
		/// 
		/// <para>Builders which are based upon this abstract class should implement the commit() method, returning the appropriate
		/// concrete object type.</para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public abstract class Builder : IBuilder {
			public abstract IDDMSComponent Commit();
			internal const long SerialVersionUID = -1694935853087559491L;

			internal string _entityType;
			internal Organization.Builder _organization;
			internal Person.Builder _person;
			internal Service.Builder _service;
			internal Unknown.Builder _unknown;
			internal List<string> _pocTypes;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Base constructor
			/// </summary>
			protected internal Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			protected internal Builder(AbstractProducerRole producer) {
				EntityType = producer.Entity.Name;                
				DDMSVersion version = producer.DDMSVersion;
                if (DDMSSense.DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                {
					Organization = new Organization.Builder((Organization) producer.Entity);
				}
                if (DDMSSense.DDMS.ResourceElements.Person.GetName(version).Equals(EntityType))
                {
					Person = new Person.Builder((Person) producer.Entity);
				}
                if (DDMSSense.DDMS.ResourceElements.Service.GetName(version).Equals(EntityType))
                {
					Service = new Service.Builder((Service) producer.Entity);
				}
                if (DDMSSense.DDMS.ResourceElements.Unknown.GetName(version).Equals(EntityType))
                {
					Unknown = new Unknown.Builder((Unknown) producer.Entity);
				}
				PocTypes = producer.PocTypes;
				SecurityAttributes = new SecurityAttributes.Builder(producer.SecurityAttributes);
			}

			/// <summary>
			/// Commits the entity which is active in this builder, based on the entityType. </summary>
			/// <returns> the entity </returns>


			protected internal virtual IRoleEntity CommitSelectedEntity() {
				DDMSVersion version = DDMSVersion.GetCurrentVersion();
                if (DDMSSense.DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
					return (Organization.Commit());
				}
                if (DDMSSense.DDMS.ResourceElements.Person.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
					return (Person.Commit());
				}
                if (DDMSSense.DDMS.ResourceElements.Service.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
					return (Service.Commit());
				}
				return (Unknown.Commit());
			}

			/// <summary>
			/// Helper method to determine if any values have been entered for this producer.
			/// </summary>
			/// <returns> true if all values are empty </returns>
			public virtual bool Empty {
				get {
					return (Organization.Empty && Person.Empty && Service.Empty && Unknown.Empty && PocTypes.Count == 0 && SecurityAttributes.Empty);
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
                set { _securityAttributes = value; }
			}


			/// <summary>
			/// Builder accessor for the entityType, which determines which of the 4 entity builders are used.
			/// </summary>
			public virtual string EntityType {
				get {
					return _entityType;
				}
				set {
					_entityType = value;
				}
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
				set {
					_organization = value;
				}
			}


			/// <summary>
			/// Builder accessor for the person builder
			/// </summary>
			public virtual Person.Builder Person {
				get {
					if (_person == null) {
						_person = new Person.Builder();
					}
					return _person;
				}
				set {
					_person = value;
				}
			}


			/// <summary>
			/// Builder accessor for the service builder
			/// </summary>
			public virtual Service.Builder Service {
				get {
					if (_service == null) {
						_service = new Service.Builder();
					}
					return _service;
				}
				set {
					_service = value;
				}
			}


			/// <summary>
			/// Builder accessor for the unknown builder
			/// </summary>
			public virtual Unknown.Builder Unknown {
				get {
					if (_unknown == null) {
						_unknown = new Unknown.Builder();
					}
					return _unknown;
				}
				set {
					_unknown = value;
				}
			}


			/// <summary>
			/// Builder accessor for the pocTypes
			/// </summary>
			public virtual List<string> PocTypes {
				get {
					if (_pocTypes == null) {
                        _pocTypes = new List<string>();
					}
					return _pocTypes;
				}
                set { _pocTypes = value; }
			}

		}
	}
}