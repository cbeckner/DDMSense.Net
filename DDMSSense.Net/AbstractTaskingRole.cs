#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS;
using DDMSense.DDMS.ResourceElements;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

namespace DDMSense
{
    /// <summary>
    ///     Base class for DDMS tasking role elements, including ddms:requesterInfo and ddms:addressee.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying element MUST be set before the component is used.
    ///     </para>
    /// </summary>
    public abstract class AbstractTaskingRole : AbstractBaseComponent
    {
        private readonly IRoleEntity _entity;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> The element representing this component </param>
        protected internal AbstractTaskingRole(XElement element)
        {
            try
            {
                SetElement(element, false);
                if (element.Elements().Count() > 0)
                {
                    XElement entityElement = element.Elements().First();
                    string entityType = entityElement.Name.LocalName;
                    
                    if (Organization.GetName(DDMSVersion).Equals(entityType))
                        _entity = new Organization(entityElement);
                    
                    if (Person.GetName(DDMSVersion).Equals(entityType))
                        _entity = new Person(entityElement);
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
        /// <param name="roleType">The type of producer this producer entity is fulfilling (i.e. creator or contributor) </param>
        /// <param name="entity">The actual entity fulfilling this role </param>
        /// <param name="securityAttributes">Any security attributes (optional) </param>
        protected internal AbstractTaskingRole(string roleType, IRoleEntity entity, SecurityAttributes securityAttributes)
        {
            try
            {
                Util.Util.RequireDDMSValue("entity", entity);
                XElement element = Util.Util.BuildDDMSElement(roleType, null);
                element.Add(entity.ElementCopy);
                _entity = entity;
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetElement(element, true);
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
        /// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        /// Validates the component.
        /// <remarks>
        /// The entity exists and is either a Person or an Organization.
        /// A classification is required.
        /// At least 1 ownerProducer exists and is non-empty.
        /// This component cannot exist until DDMS 4.0.1 or later.
        /// </remarks>
        /// </summary>
        /// <exception cref="InvalidDDMSException">Thrown if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSValue("entity", Entity);
            if (!(Entity is Organization) && !(Entity is Person))
                throw new InvalidDDMSException("The entity must be a person or an organization.");

            Util.Util.RequireBoundedChildCount(Element, Organization.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(Element, Person.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

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
        ///         Builders which are based upon this abstract class should implement the commit() method, returning the appropriate concrete object type.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            
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
                    Organization = new Organization.Builder((Organization) role.Entity);
                
                if (DDMS.ResourceElements.Person.GetName(version).Equals(EntityType))
                    Person = new Person.Builder((Person) role.Entity);
                
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
                        _securityAttributes = new SecurityAttributes.Builder();
                    
                    return _securityAttributes;
                }
                set { _securityAttributes = value; }
            }


            /// <summary>
            ///     Builder accessor for the entityType, which determines which of the 4 entity builders are used.
            /// </summary>
            public virtual string EntityType { get; set; }


            /// <summary>
            ///     Builder accessor for the organization builder
            /// </summary>
            public virtual Organization.Builder Organization
            {
                get
                {
                    if (_organization == null)
                        _organization = new Organization.Builder();
                    
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
                        _person = new Person.Builder();
                    
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
                DDMSVersion version = DDMSVersion.CurrentVersion;
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                    return ((Organization) Organization.Commit());
                
                return ((Person) Person.Commit());
            }
        }
    }
}