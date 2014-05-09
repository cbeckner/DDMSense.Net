#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.Extensible;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    /// <summary>
    ///     An immutable implementation of ddms:person.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>At least 1 name value must be non-empty.</li>
    ///                     <li>The surname must be non-empty.</li>
    ///                 </ul>
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A phone number can be set with no value.</li>
    ///                     <li>An email can be set with no value.</li>
    ///                     <li>A userID can be set with no value.</li>
    ///                     <li>An affiliation can be set with no value.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <para>The name of this component was changed from "Person" to "person" in DDMS 4.0.1.</para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:name</u>: names of the producer (1-many, at least 1 required)<br />
    ///                 <u>ddms:surname</u>: surname of the producer (exactly 1 required)<br />
    ///                 <u>ddms:userID</u>: userId of the producer (0-1 optional)<br />
    ///                 <u>ddms:affiliation</u>: organizational affiliation (0-1 optional)<br />
    ///                 <u>ddms:phone</u>: phone numbers of the producer (0-many optional)<br />
    ///                 <u>ddms:email</u>: email addresses of the producer (0-many optional)<br />
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
    ///                     <see cref="ExtensibleAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>


    /// </summary>
    public sealed class Person : AbstractRoleEntity
    {
        private const string AFFILIATION_NAME = "affiliation";
        private const string USERID_NAME = "userID";
        private const string SURNAME_NAME = "surname";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Person(XElement element)
            : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="surname"> the surname of the person </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="userID"> optional unique identifier within an organization </param>
        /// <param name="affiliation"> organizational affiliation of the person </param>
        public Person(List<string> names, string surname, List<string> phones, List<string> emails, string userID, string affiliation)
            : this(names, surname, phones, emails, userID, affiliation, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="surname"> the surname of the person </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="userID"> optional unique identifier within an organization </param>
        /// <param name="affiliation"> organizational affiliation of the person </param>
        /// <param name="extensions"> extensible attributes (optional) </param>
        public Person(List<string> names, string surname, List<string> phones, List<string> emails, string userID, string affiliation, ExtensibleAttributes extensions)
            : base(GetName(DDMSVersion.GetCurrentVersion()), names, phones, emails, extensions, false)
        {
            try
            {
                int insertIndex = (names == null ? 0 : names.Count);
                AddExtraElements(insertIndex, surname, userID, affiliation);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the surname of the person
        /// </summary>
        public string Surname
        {
            get { return (Util.Util.GetFirstDDMSChildValue(Element, SURNAME_NAME)); }
        }

        /// <summary>
        ///     Accessor for the userID of the person
        /// </summary>
        public string UserID
        {
            get { return (Util.Util.GetFirstDDMSChildValue(Element, USERID_NAME)); }
        }

        /// <summary>
        ///     Accessor for the affiliation of the person
        /// </summary>
        public string Affiliation
        {
            get { return (Util.Util.GetFirstDDMSChildValue(Element, AFFILIATION_NAME)); }
        }

        /// <summary>
        ///     Inserts additional elements into the existing entity. Because the personType contains a sequence,
        ///     additional fields must be inserted among the name, phone, and email elements.
        /// </summary>
        /// <param name="insertIndex"> the index of the position after the last names element </param>
        /// <param name="surname"> the surname of the person </param>
        /// <param name="userID"> optional unique identifier within an organization </param>
        /// <param name="affiliation"> organizational affiliation of the person </param>
        /// <exception cref="InvalidDDMSException"> if the result is an invalid component </exception>
        private void AddExtraElements(int insertIndex, string surname, string userID, string affiliation)
        {
            XElement element = Element;
            if (DDMSVersion.IsAtLeast("4.0.1"))
            {
                element.AddFirst(Util.Util.BuildDDMSElement(SURNAME_NAME, surname), insertIndex);
                if (!String.IsNullOrEmpty(userID))
                    element.Add(Util.Util.BuildDDMSElement(USERID_NAME, userID));

                if (!String.IsNullOrEmpty(affiliation))
                    element.Add(Util.Util.BuildDDMSElement(AFFILIATION_NAME, affiliation));
            }
            else
            {
                // 	Inserting in reverse order allow the same index to be reused. Later inserts will "push" the early ones
                // 	forward.
                if (!String.IsNullOrEmpty(affiliation))
                    element.AddAfterSelf(Util.Util.BuildDDMSElement(AFFILIATION_NAME, affiliation), element.Nodes().ToList()[insertIndex]);

                if (!String.IsNullOrEmpty(userID))
                    element.AddAfterSelf(Util.Util.BuildDDMSElement(USERID_NAME, userID), element.Nodes().ToList()[insertIndex]);

                element.AddAfterSelf(Util.Util.BuildDDMSElement(SURNAME_NAME, surname), element.Nodes().ToList()[insertIndex]);
            }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>Surname exists and is not empty.</li>
        ///                 <li>Exactly 1 surname, 0-1 userIDs, 0-1 affiliations exist.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractRoleEntity#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue(SURNAME_NAME, Surname);
            Util.Util.RequireBoundedChildCount(Element, SURNAME_NAME, 1, 1);
            Util.Util.RequireBoundedChildCount(Element, USERID_NAME, 0, 1);
            Util.Util.RequireBoundedChildCount(Element, AFFILIATION_NAME, 0, 1);

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
        ///                 <li>A ddms:userID element was found with no value.</li>
        ///                 <li>A ddms:affiliation element was found with no value.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(UserID) && Element.Elements(XName.Get(USERID_NAME, Namespace)).Count() == 1)
                AddWarning("A ddms:userID element was found with no value.");
            
            if (String.IsNullOrEmpty(Affiliation) &&
                Element.Elements(XName.Get(AFFILIATION_NAME, Namespace)).Count() == 1)
                AddWarning("A ddms:affiliation element was found with no value.");
            
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "", suffix);
            var text = new StringBuilder(base.GetOutput(isHtml, localPrefix, ""));
            text.Append(BuildOutput(isHtml, localPrefix + SURNAME_NAME, Surname));
            text.Append(BuildOutput(isHtml, localPrefix + USERID_NAME, UserID));
            text.Append(BuildOutput(isHtml, localPrefix + AFFILIATION_NAME, Affiliation));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Person))
                return (false);
            
            var test = (Person)obj;
            return (Surname.Equals(test.Surname) && UserID.Equals(test.UserID) && Affiliation.Equals(test.Affiliation));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Surname.GetHashCode();
            result = 7 * result + UserID.GetHashCode();
            result = 7 * result + Affiliation.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return (version.IsAtLeast("4.0.1") ? "person" : "Person");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        public class Builder : AbstractRoleEntity.Builder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Person person)
                : base(person)
            {
                Surname = person.Surname;
                UserID = person.UserID;
                Affliation = person.Affiliation;
            }

            /// <summary>
            ///     Helper method to determine if any values have been entered for this Person.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public override bool Empty
            {
                get
                {
                    return (base.Empty && String.IsNullOrEmpty(Surname) && String.IsNullOrEmpty(UserID) &&
                            String.IsNullOrEmpty(Affliation));
                }
            }

            /// <summary>
            ///     Builder accessor for the surname
            /// </summary>
            public virtual string Surname { get; set; }


            /// <summary>
            ///     Builder accessor for the userID
            /// </summary>
            public virtual string UserID { get; set; }


            /// <summary>
            ///     Builder accessor for the affliation
            /// </summary>
            public virtual string Affliation { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty
                    ? null
                    : new Person(Names, Surname, Phones, Emails, UserID, Affliation, ExtensibleAttributes.Commit()));
            }
        }
    }
}