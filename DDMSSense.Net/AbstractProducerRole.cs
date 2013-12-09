#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS;
using DDMSSense.DDMS.ResourceElements;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Util;

#endregion

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

namespace DDMSSense
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     Base class for DDMS producer elements, such as ddms:creator and ddms:contributor.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before
    ///         the component is used.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:organization</u>: The organization who is in this role (0-1, optional)<br />
    ///                 <u>ddms:person</u>: the person who is in this role (0-1, optional)<br />
    ///                 <u>ddms:service</u>: The web service who is in this role (0-1, optional)<br />
    ///                 <u>ddms:unknown</u>: The unknown entity who is in this role (0-1, optional)<br />
    ///                 Only one of the nested entities can appear in this element.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ISM:pocType</u>: Indicates that the element specifies a point-of-contact (POC) and the methods with
    ///                 which
    ///                 to contact them (optional, starting in DDMS 4.0.1.1).<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are optional.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 2.0.0
    /// </summary>
    public abstract class AbstractProducerRole : AbstractBaseComponent
    {
        private const string POC_TYPE_NAME = "pocType";
        private readonly IRoleEntity _entity;
        private List<string> _pocTypes;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        protected internal AbstractProducerRole(Element element)
        {
            try
            {
                SetXOMElement(element, false);
                if (element.Elements().Count() > 0)
                {
                    var entityElement = (XElement) element.FirstNode;
                    string entityType = entityElement.Name.LocalName;
                    if (Organization.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Organization(entityElement);
                    }
                    if (Person.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Person(entityElement);
                    }
                    if (Service.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Service(entityElement);
                    }
                    if (Unknown.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Unknown(entityElement);
                    }
                }
                string pocTypes = element.Attribute(XName.Get(POC_TYPE_NAME, DDMSVersion.IsmNamespace)).Value;
                _pocTypes = Util.Util.GetXsListAsList(pocTypes);
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
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="producerType"> the type of producer this producer entity is fulfilling (i.e. creator or contributor) </param>
        /// <param name="entity"> the actual entity fulfilling this role </param>
        /// <param name="pocTypes"> the pocType attribute (starting in DDMS 4.0.1) </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        protected internal AbstractProducerRole(string producerType, IRoleEntity entity, List<string> pocTypes,
            SecurityAttributes securityAttributes)
        {
            try
            {
                if (pocTypes == null)
                {
                    pocTypes = new List<string>();
                }
                Util.Util.RequireDDMSValue("producer type", producerType);
                Util.Util.RequireDDMSValue("entity", entity);
                Element element = Util.Util.BuildDDMSElement(producerType, null);
                element.Add(entity.XOMElementCopy);
                _entity = entity;
                if (pocTypes.Count > 0)
                {
                    Util.Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), POC_TYPE_NAME,
                        DDMSVersion.GetCurrentVersion().IsmNamespace, Util.Util.GetXsList(pocTypes));
                }
                _pocTypes = pocTypes;
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

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(Entity);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the entity fulfilling this producer role
        /// </summary>
        public virtual IRoleEntity Entity
        {
            get { return _entity; }
        }

        /// <summary>
        ///     Accessor for the pocType attribute.
        /// </summary>
        public virtual List<string> PocTypes
        {
            get { return (_pocTypes); }
            set { _pocTypes = value; }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>A producer entity exists.</li>
        ///                 <li>Only 0-1 persons, organizations, services, or unknowns exist.</li>
        ///                 <li>The pocType cannot be used before DDMS 4.0.1.</li>
        ///                 <li>If set, the pocTypes must each be a valid token.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSValue("entity", Entity);
            Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Person.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Service.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Unknown.GetName(DDMSVersion), 0, 1);

            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("4.0.1") && PocTypes.Count > 0)
            {
                throw new InvalidDDMSException("This component cannot have a pocType until DDMS 4.0.1 or later.");
            }
            if (DDMSVersion.IsAtLeast("4.0.1"))
            {
                foreach (var pocType in PocTypes)
                {
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_POC_TYPE, pocType);
                }
            }

            base.Validate();
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractProducerRole))
            {
                return (false);
            }
            var test = (AbstractProducerRole) obj;
            return (Util.Util.ListEquals(PocTypes, test.PocTypes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + PocTypes.GetHashCode();
            return (result);
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(((AbstractBaseComponent) Entity).GetOutput(isHtml, localPrefix, ""));
            text.Append(BuildOutput(isHtml, localPrefix + POC_TYPE_NAME, Util.Util.GetXsList(PocTypes)));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <summary>
        ///     Abstract Builder for this DDMS component.
        ///     <para>
        ///         Builders which are based upon this abstract class should implement the commit() method, returning the
        ///         appropriate
        ///         concrete object type.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            internal const long SerialVersionUID = -1694935853087559491L;

            internal string _entityType;
            internal Organization.Builder _organization;
            internal Person.Builder _person;
            internal List<string> _pocTypes;
            internal SecurityAttributes.Builder _securityAttributes;
            internal Service.Builder _service;
            internal Unknown.Builder _unknown;

            /// <summary>
            ///     Base constructor
            /// </summary>
            protected internal Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractProducerRole producer)
            {
                EntityType = producer.Entity.Name;
                DDMSVersion version = producer.DDMSVersion;
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                {
                    Organization = new Organization.Builder((Organization) producer.Entity);
                }
                if (DDMS.ResourceElements.Person.GetName(version).Equals(EntityType))
                {
                    Person = new Person.Builder((Person) producer.Entity);
                }
                if (DDMS.ResourceElements.Service.GetName(version).Equals(EntityType))
                {
                    Service = new Service.Builder((Service) producer.Entity);
                }
                if (DDMS.ResourceElements.Unknown.GetName(version).Equals(EntityType))
                {
                    Unknown = new Unknown.Builder((Unknown) producer.Entity);
                }
                PocTypes = producer.PocTypes;
                SecurityAttributes = new SecurityAttributes.Builder(producer.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the Security Attributes
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
            ///     Builder accessor for the entityType, which determines which of the 4 entity builders are used.
            /// </summary>
            public virtual string EntityType
            {
                get { return _entityType; }
                set { _entityType = value; }
            }


            /// <summary>
            ///     Builder accessor for the organization builder
            /// </summary>
            public virtual Organization.Builder Organization
            {
                get
                {
                    if (_organization == null)
                    {
                        _organization = new Organization.Builder();
                    }
                    return _organization;
                }
                set { _organization = value; }
            }


            /// <summary>
            ///     Builder accessor for the person builder
            /// </summary>
            public virtual Person.Builder Person
            {
                get
                {
                    if (_person == null)
                    {
                        _person = new Person.Builder();
                    }
                    return _person;
                }
                set { _person = value; }
            }


            /// <summary>
            ///     Builder accessor for the service builder
            /// </summary>
            public virtual Service.Builder Service
            {
                get
                {
                    if (_service == null)
                    {
                        _service = new Service.Builder();
                    }
                    return _service;
                }
                set { _service = value; }
            }


            /// <summary>
            ///     Builder accessor for the unknown builder
            /// </summary>
            public virtual Unknown.Builder Unknown
            {
                get
                {
                    if (_unknown == null)
                    {
                        _unknown = new Unknown.Builder();
                    }
                    return _unknown;
                }
                set { _unknown = value; }
            }


            /// <summary>
            ///     Builder accessor for the pocTypes
            /// </summary>
            public virtual List<string> PocTypes
            {
                get
                {
                    if (_pocTypes == null)
                    {
                        _pocTypes = new List<string>();
                    }
                    return _pocTypes;
                }
                set { _pocTypes = value; }
            }

            public abstract IDDMSComponent Commit();

            /// <summary>
            ///     Helper method to determine if any values have been entered for this producer.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public virtual bool Empty
            {
                get
                {
                    return (Organization.Empty && Person.Empty && Service.Empty && Unknown.Empty && PocTypes.Count == 0 &&
                            SecurityAttributes.Empty);
                }
            }

            /// <summary>
            ///     Commits the entity which is active in this builder, based on the entityType.
            /// </summary>
            /// <returns> the entity </returns>
            protected internal virtual IRoleEntity CommitSelectedEntity()
            {
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                if (DDMS.ResourceElements.Organization.GetName(version)
                    .Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ((Organization) Organization.Commit());
                }
                if (DDMS.ResourceElements.Person.GetName(version)
                    .Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ((Person) Person.Commit());
                }
                if (DDMS.ResourceElements.Service.GetName(version)
                    .Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ((Service) Service.Commit());
                }
                return ((Unknown) Unknown.Commit());
            }
        }
    }
}