#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS;
using DDMSense.DDMS.Extensible;

#endregion

namespace DDMSense
{
    /// <summary>
    ///     Base class for entities which fulfill some role, such as ddms:person and ddms:organization.
    ///     <para>
    ///         The HTML output of this class depends on the role type which the entity is associated with. For
    ///         example, if this entity's role is a "pointOfContact", the HTML meta tags will prefix each
    ///         field with "pointOfContact."
    ///     </para>
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before the component is used.
    ///     </para>
    
    ///     @since 2.0.0
    /// </summary>
    public abstract class AbstractRoleEntity : AbstractBaseComponent, IRoleEntity
    {
        private const string NAME_NAME = "name";
        private const string PHONE_NAME = "phone";
        private const string EMAIL_NAME = "email";

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        /// <param name="validateNow">
        ///     true to validate the component immediately. Because Person and Organization entities have
        ///     additional fields they should not be validated in the superconstructor.
        /// </param>
        protected internal AbstractRoleEntity(XElement element, bool validateNow)
        {
            try
            {
                Names = Util.Util.GetDDMSChildValues(element, NAME_NAME);
                Phones = Util.Util.GetDDMSChildValues(element, PHONE_NAME);
                Emails = Util.Util.GetDDMSChildValues(element, EMAIL_NAME);
                ExtensibleAttributes = new ExtensibleAttributes(element);
                SetElement(element, validateNow);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw;
            }
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="entityName"> the element name of this entity (i.e. organization, person) </param>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="extensibleAttributes"> extensible attributes (optional) </param>
        /// <param name="validateNow">
        ///     true to validate the component immediately. Because Person and Organization entities have
        ///     additional fields they should not be validated in the superconstructor.
        /// </param>
        protected internal AbstractRoleEntity(string entityName, List<string> names, List<string> phones, List<string> emails, ExtensibleAttributes extensibleAttributes, bool validateNow)
        {
            try
            {
                Util.Util.RequireDDMSValue("entityName", entityName);
                if (names == null)
                    names = new List<string>();

                if (phones == null)
                    phones = new List<string>();

                if (emails == null)
                    emails = new List<string>();

                XElement element = Util.Util.BuildDDMSElement(entityName, null);
                foreach (var name in names)
                    element.Add(Util.Util.BuildDDMSElement(NAME_NAME, name));

                foreach (var phone in phones)
                    element.Add(Util.Util.BuildDDMSElement(PHONE_NAME, phone));

                foreach (var email in emails)
                    element.Add(Util.Util.BuildDDMSElement(EMAIL_NAME, email));

                Names = names;
                Phones = phones;
                Emails = emails;
                ExtensibleAttributes = ExtensibleAttributes.GetNonNullInstance(extensibleAttributes);
                ExtensibleAttributes.AddTo(element);
                SetElement(element, validateNow);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw;
            }
        }

        /// <summary>
        ///     Accessor for the names of the entity (1 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public virtual List<string> Names { get; set; }

        /// <summary>
        ///     Accessor for the phone numbers of the entity (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public virtual List<string> Phones { get; set; }

        /// <summary>
        ///     Accessor for the emails of the entity (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        public virtual List<string> Emails { get; set; }

        /// <summary>
        ///     Accessor for the extensible attributes. Will always be non-null, even if not set.
        /// </summary>
        public virtual ExtensibleAttributes ExtensibleAttributes { get; set; }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The entity has at least 1 non-empty name.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            if (Element.Elements(XName.Get(NAME_NAME, Namespace)).Count() == 0)
                throw new InvalidDDMSException("At least 1 name element must exist.");

            if (Util.Util.ContainsOnlyEmptyValues(Names))
                throw new InvalidDDMSException("At least 1 name element must have a non-empty value.");

            base.Validate();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>A ddms:phone element was found with no value.</li>
        ///                 <li>A ddms:email element was found with no value.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            var phoneElements = Element.Elements(XName.Get(PHONE_NAME, Namespace));
            if (phoneElements.ToList().Any(element => String.IsNullOrEmpty(element.Value)))
                AddWarning("A ddms:phone element was found with no value.");
            
            var emailElements = Element.Elements(XName.Get(EMAIL_NAME, Namespace));
            if (emailElements.ToList().Any(element => String.IsNullOrEmpty(element.Value))) 
                AddWarning("A ddms:email element was found with no value.");
            
            base.ValidateWarnings();
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractRoleEntity))
                return (false);

            var test = (AbstractRoleEntity)obj;
            return (Util.Util.ListEquals(Names, test.Names) &&
                    Util.Util.ListEquals(Phones, test.Phones) &&
                    Util.Util.ListEquals(Emails, test.Emails) &&
                    ExtensibleAttributes.Equals(test.ExtensibleAttributes));
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Names.GetHashCode();
            result = 7 * result + Phones.GetHashCode();
            result = 7 * result + Emails.GetHashCode();
            result = 7 * result + ExtensibleAttributes.GetHashCode();
            return (result);
        }

        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "", suffix);
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + "entityType", Name));
            text.Append(BuildOutput(isHtml, localPrefix + NAME_NAME, Names));
            text.Append(BuildOutput(isHtml, localPrefix + PHONE_NAME, Phones));
            text.Append(BuildOutput(isHtml, localPrefix + EMAIL_NAME, Emails));
            text.Append(ExtensibleAttributes.GetOutput(isHtml, prefix));
            return (text.ToString());
        }

        /// <summary>
        ///     Abstract Builder for this DDMS component.
        ///     <para>
        ///         Builders which are based upon this abstract class should implement the commit() method, returning the
        ///         appropriate concrete object type.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            protected internal Builder()
            {
                Emails = new List<string>();
                ExtensibleAttributes = new ExtensibleAttributes.Builder();
                Names = new List<string>();
                Phones = new List<string>();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractRoleEntity entity)
            {
                Names = entity.Names;
                Phones = entity.Phones;
                Emails = entity.Emails;
                ExtensibleAttributes = new ExtensibleAttributes.Builder(entity.ExtensibleAttributes);
            }

            /// <summary>
            ///     Builder accessor for the names
            /// </summary>
            public virtual List<string> Names { get; set; }

            /// <summary>
            ///     Builder accessor for the phones
            /// </summary>
            public virtual List<string> Phones { get; set; }

            /// <summary>
            ///     Builder accessor for the emails
            /// </summary>
            public virtual List<string> Emails { get; set; }

            /// <summary>
            ///     Builder accessor for the Extensible Attributes
            /// </summary>
            public virtual ExtensibleAttributes.Builder ExtensibleAttributes { get; set; }
            
            public abstract IDDMSComponent Commit();

            /// <summary>
            ///     Helper method to determine if any values have been entered for this producer.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public virtual bool Empty
            {
                get
                {
                    return (Util.Util.ContainsOnlyEmptyValues(Names) && 
                            Util.Util.ContainsOnlyEmptyValues(Phones) &&
                            Util.Util.ContainsOnlyEmptyValues(Emails) && 
                            ExtensibleAttributes.Empty);
                }
            }
        }
    }
}