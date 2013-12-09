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
    ///     Base class for DDMS tasking role elements, including ddms:requesterInfo and ddms:addressee.
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
    ///                 <u>ddms:organization</u>: The organization who is the addressee (0-1, optional), implemented as an
    ///                 <see cref="Organization" /><br />
    ///                 <u>ddms:person</u>: the person who is the addressee (0-1, optional), implemented as a
    ///                 <see cref="Person" /><br />
    ///                 Only one of the nested entities can appear.
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
    ///                 :  The classification and
    ///                 ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 2.0.0
    /// </summary>
    public abstract class AbstractTaskingRole : AbstractBaseComponent
    {
        private readonly IRoleEntity _entity;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        protected internal AbstractTaskingRole(Element element)
        {
            try
            {
                SetXOMElement(element, false);
                if (element.Nodes().Count() > 0)
                {
                    Element entityElement = element.Elements().First();
                    string entityType = entityElement.Name.LocalName;
                    if (Organization.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Organization(entityElement);
                    }
                    if (Person.GetName(DDMSVersion).Equals(entityType))
                    {
                        _entity = new Person(entityElement);
                    }
                }
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
        /// <param name="roleType"> the type of producer this producer entity is fulfilling (i.e. creator or contributor) </param>
        /// <param name="entity"> the actual entity fulfilling this role </param>
        /// <param name="securityAttributes"> any security attributes (optional) </param>
        protected internal AbstractTaskingRole(string roleType, IRoleEntity entity,
            SecurityAttributes securityAttributes)
        {
            try
            {
                Util.Util.RequireDDMSValue("entity", entity);
                Element element = Util.Util.BuildDDMSElement(roleType, null);
                element.Add(entity.XOMElementCopy);
                _entity = entity;
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
        ///     Accessor for the producer entity
        /// </summary>
        public virtual IRoleEntity Entity
        {
            get { return _entity; }
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
        ///                 <li>The entity exists and is either a Person or an Organization.</li>
        ///                 <li>A classification is required.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>This component cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSValue("entity", Entity);
            if (!(Entity is Organization) && !(Entity is Person))
            {
                throw new InvalidDDMSException("The entity must be a person or an organization.");
            }
            Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Person.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractTaskingRole))
            {
                return (false);
            }
            return (true);
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(((AbstractBaseComponent) Entity).GetOutput(isHtml, localPrefix, ""));
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
            internal SecurityAttributes.Builder _securityAttributes;

            /// <summary>
            ///     Base constructor
            /// </summary>
            protected internal Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractTaskingRole role)
            {
                DDMSVersion version = role.DDMSVersion;
                EntityType = role.Entity.Name;
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                {
                    Organization = new Organization.Builder((Organization) role.Entity);
                }
                if (DDMS.ResourceElements.Person.GetName(version).Equals(EntityType))
                {
                    Person = new Person.Builder((Person) role.Entity);
                }
                SecurityAttributes = new SecurityAttributes.Builder(role.SecurityAttributes);
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

            public abstract IDDMSComponent Commit();

            /// <summary>
            ///     Helper method to determine if any values have been entered for this producer.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public virtual bool Empty
            {
                get { return (Organization.Empty && Person.Empty && SecurityAttributes.Empty); }
            }

            /// <summary>
            ///     Commits the entity which is active in this builder, based on the entityType.
            /// </summary>
            /// <returns> the entity </returns>
            protected internal virtual IRoleEntity CommitSelectedEntity()
            {
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                {
                    return ((Organization) Organization.Commit());
                }
                return ((Person) Person.Commit());
            }
        }
    }
}