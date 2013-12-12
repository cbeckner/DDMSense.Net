#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSSense.DDMS.Extensible;
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

namespace DDMSSense.DDMS.ResourceElements
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:organization.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>At least 1 name value must be non-empty.</li>
    ///                 </ul>
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A phone number can be set with no value.</li>
    ///                     <li>An email can be set with no value.</li>
    ///                     <li>An acronym can be set with no value.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <para>The name of this component was changed from "Organization" to "organization" in DDMS 4.0.1.</para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:name</u>: names of the producer entity (1-many, at least 1 required)<br />
    ///                 <u>ddms:phone</u>: phone numbers of the producer entity (0-many optional)<br />
    ///                 <u>ddms:email</u>: email addresses of the producer entity (0-many optional)<br />
    ///                 <u>ddms:subOrganization</u>: suborganization (0-many optional, starting in DDMS 4.0.1), implemented as
    ///                 a
    ///                 <see cref="SubOrganization" /><br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:acronym</u>: an acronym for the organization (optional, starting in DDMS 4.0.1)<br />
    ///                 <u>
    ///                     <see cref="ExtensibleAttributes" />
    ///                 </u>
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Organization : AbstractRoleEntity
    {
        private const string ACRONYM_NAME = "acronym";
        private readonly List<SubOrganization> _subOrganizations;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Organization(Element element) : base(element, false)
        {
            try
            {
                string @namespace = element.Name.NamespaceName;
                IEnumerable<Element> components =
                    element.Elements(XName.Get(SubOrganization.GetName(DDMSVersion), @namespace));
                _subOrganizations = new List<SubOrganization>();
                components.ToList().ForEach(c => _subOrganizations.Add(new SubOrganization(c)));

                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="subOrganizations"> an ordered list of suborganizations </param>
        /// <param name="acronym"> the organization's acronym </param>
        public Organization(List<string> names, List<string> phones, List<string> emails,
            List<SubOrganization> subOrganizations, string acronym)
            : this(names, phones, emails, subOrganizations, acronym, null)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// <param name="names"> an ordered list of names </param>
        /// <param name="phones"> an ordered list of phone numbers </param>
        /// <param name="emails"> an ordered list of email addresses </param>
        /// <param name="subOrganizations"> an ordered list of suborganizations </param>
        /// <param name="acronym"> the organization's acronym </param>
        /// <param name="extensions"> extensible attributes (optional) </param>
        public Organization(List<string> names, List<string> phones, List<string> emails,
            List<SubOrganization> subOrganizations, string acronym, ExtensibleAttributes extensions)
            : base(GetName(DDMSVersion.GetCurrentVersion()), names, phones, emails, extensions, false)
        {
            try
            {
                if (subOrganizations == null)
                {
                    subOrganizations = new List<SubOrganization>();
                }
                Util.Util.AddDDMSAttribute(Element, ACRONYM_NAME, acronym);
                foreach (var subOrganization in subOrganizations)
                {
                    Element.Add(subOrganization.ElementCopy);
                }
                _subOrganizations = subOrganizations;
                Validate();
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
                list.AddRange(SubOrganizations);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the suborganizations (0-many)
        /// </summary>
        public List<SubOrganization> SubOrganizations
        {
            get { return _subOrganizations; }
        }

        /// <summary>
        ///     Accessor for the acronym
        /// </summary>
        public string Acronym
        {
            get { return (GetAttributeValue(ACRONYM_NAME)); }
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
        ///                 <li>Acronyms cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractRoleEntity#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));

            // Should be reviewed as additional versions of DDMS are supported.
            if (!DDMSVersion.IsAtLeast("4.0.1"))
            {
                if (!String.IsNullOrEmpty(Acronym))
                {
                    throw new InvalidDDMSException("An organization cannot have an acronym until DDMS 4.0.1 or later.");
                }
            }

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
        ///                 <li>A ddms:acronym attribute was found with no value.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (DDMSVersion.IsAtLeast("4.0.1"))
            {
                if (String.IsNullOrEmpty(Acronym) && Element.Attribute(XName.Get(ACRONYM_NAME, Namespace)).Value != null)
                {
                    AddWarning("A ddms:acronym attribute was found with no value.");
                }
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "", suffix);
            var text = new StringBuilder(base.GetOutput(isHtml, localPrefix, ""));
            text.Append(BuildOutput(isHtml, localPrefix, SubOrganizations));
            text.Append(BuildOutput(isHtml, localPrefix + ACRONYM_NAME, Acronym));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Organization))
            {
                return (false);
            }
            var test = (Organization) obj;
            return (Acronym.Equals(test.Acronym));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Acronym.GetHashCode();
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
            return (version.IsAtLeast("4.0.1") ? "organization" : "Organization");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        public class Builder : AbstractRoleEntity.Builder
        {
            internal const long SerialVersionUID = 4565840434345629470L;
            internal string _acronym;
            internal List<SubOrganization.Builder> _subOrganizations;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Organization organization) : base(organization)
            {
                foreach (var subOrg in organization.SubOrganizations)
                {
                    SubOrganizations.Add(new SubOrganization.Builder(subOrg));
                }
                Acronym = organization.Acronym;
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in SubOrganizations)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (base.Empty && !hasValueInList && String.IsNullOrEmpty(Acronym));
                }
            }

            /// <summary>
            ///     Builder accessor for suborganizations
            /// </summary>
            public virtual List<SubOrganization.Builder> SubOrganizations
            {
                get
                {
                    if (_subOrganizations == null)
                    {
                        _subOrganizations = new List<SubOrganization.Builder>();
                    }
                    return _subOrganizations;
                }
            }

            /// <summary>
            ///     Builder accessor for the acronym
            /// </summary>
            public virtual string Acronym
            {
                get { return _acronym; }
                set { _acronym = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var subOrgs = new List<SubOrganization>();
                foreach (IBuilder builder in SubOrganizations)
                {
                    var component = (SubOrganization) builder.Commit();
                    if (component != null)
                    {
                        subOrgs.Add(component);
                    }
                }
                return (new Organization(Names, Phones, Emails, subOrgs, Acronym, ExtensibleAttributes.Commit()));
            }
        }
    }
}