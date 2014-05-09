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

namespace DDMSSense
{
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
    /// </summary>
    public abstract class AbstractProducerRole : AbstractBaseComponent
    {
        private const string POC_TYPE_NAME = "pocType";

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        protected internal AbstractProducerRole(XElement element)
        {
            try
            {
                SetElement(element, false);
                if (element.Elements().Count() > 0)
                {
                    var entityElement = (XElement)element.FirstNode;
                    string entityType = entityElement.Name.LocalName;
                    if (Organization.GetName(DDMSVersion).Equals(entityType))
                        Entity = new Organization(entityElement);

                    if (Person.GetName(DDMSVersion).Equals(entityType))
                        Entity = new Person(entityElement);

                    if (Service.GetName(DDMSVersion).Equals(entityType))
                        Entity = new Service(entityElement);

                    if (Unknown.GetName(DDMSVersion).Equals(entityType))
                        Entity = new Unknown(entityElement);
                }
                string pocTypes = (string)element.Attribute(XName.Get(POC_TYPE_NAME, DDMSVersion.IsmNamespace)) ?? string.Empty;
                PocTypes = Util.Util.GetXsListAsList(pocTypes);
                SecurityAttributes = new SecurityAttributes(element);
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
                    pocTypes = new List<string>();

                Util.Util.RequireDDMSValue("producer type", producerType);
                Util.Util.RequireDDMSValue("entity", entity);
                XElement element = Util.Util.BuildDDMSElement(producerType, null);
                element.Add(entity.ElementCopy);
                Entity = entity;

                if (pocTypes.Count > 0)
                    Util.Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), POC_TYPE_NAME, DDMSVersion.GetCurrentVersion().IsmNamespace, Util.Util.GetXsList(pocTypes));

                PocTypes = pocTypes;
                SecurityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                SecurityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

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
        public virtual IRoleEntity Entity { get; private set; }

        /// <summary>
        ///     Accessor for the pocType attribute.
        /// </summary>
        public virtual List<string> PocTypes { get; set; }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes { get; set; }

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

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractProducerRole))
                return (false);

            var test = (AbstractProducerRole)obj;
            return (Util.Util.ListEquals(PocTypes, test.PocTypes));
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + PocTypes.GetHashCode();
            return (result);
        }

        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(((AbstractBaseComponent)Entity).GetOutput(isHtml, localPrefix, ""));
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
        /// <see cref="IBuilder"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            

            /// <summary>
            ///     Base constructor
            /// </summary>
            protected internal Builder()
            {
                Organization = new Organization.Builder();
                SecurityAttributes = new SecurityAttributes.Builder();
                Person = new Person.Builder();
                Service = new Service.Builder();
                Unknown = new Unknown.Builder();
                PocTypes = new List<string>();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractProducerRole producer)
            {
                EntityType = producer.Entity.Name;
                DDMSVersion version = producer.DDMSVersion;
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType))
                    Organization = new Organization.Builder((Organization)producer.Entity);

                if (DDMS.ResourceElements.Person.GetName(version).Equals(EntityType))
                    Person = new Person.Builder((Person)producer.Entity);

                if (DDMS.ResourceElements.Service.GetName(version).Equals(EntityType))
                    Service = new Service.Builder((Service)producer.Entity);

                if (DDMS.ResourceElements.Unknown.GetName(version).Equals(EntityType))
                    Unknown = new Unknown.Builder((Unknown)producer.Entity);

                PocTypes = producer.PocTypes;
                SecurityAttributes = new SecurityAttributes.Builder(producer.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes { get; set; }
            
            /// <summary>
            ///     Builder accessor for the entityType, which determines which of the 4 entity builders are used.
            /// </summary>
            public virtual string EntityType { get; set; }
            
            /// <summary>
            ///     Builder accessor for the organization builder
            /// </summary>
            public virtual Organization.Builder Organization{ get; set; }
           
            /// <summary>
            ///     Builder accessor for the person builder
            /// </summary>
            public virtual Person.Builder Person{ get; set; }
            
            /// <summary>
            ///     Builder accessor for the service builder
            /// </summary>
            public virtual Service.Builder Service{ get; set; }
            
            /// <summary>
            ///     Builder accessor for the unknown builder
            /// </summary>
            public virtual Unknown.Builder Unknown{ get; set; }
            
            /// <summary>
            ///     Builder accessor for the pocTypes
            /// </summary>
            public virtual List<string> PocTypes{ get; set; }
            
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
                if (DDMS.ResourceElements.Organization.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                    return ((Organization)Organization.Commit());

                if (DDMS.ResourceElements.Person.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                    return ((Person)Person.Commit());

                if (DDMS.ResourceElements.Service.GetName(version).Equals(EntityType, StringComparison.CurrentCultureIgnoreCase))
                    return ((Service)Service.Commit());

                return ((Unknown)Unknown.Commit());
            }
        }
    }
}