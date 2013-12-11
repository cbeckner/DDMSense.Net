#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
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

namespace DDMSSense.DDMS.SecurityElements.Ntk
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ntk:Access.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>An Access element with no individual, group, or profile information can be used.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:AccessIndividualList/ntk:AccessIndividual</u>: A list system access info for individuals,
    ///                 implemented
    ///                 as a list of <see cref="Individual" /><br />
    ///                 <u>ntk:AccessGroupList/ntk:AccessGroup</u>: A list system access info for groups, implemented as a list
    ///                 of
    ///                 <see cref="Group" /><br />
    ///                 <u>ntk:AccessProfileList</u>: A list system access info for profiles, implemented as a
    ///                 <see cref="ProfileList" /><br />
    ///                 The list of profiles is a full-fledged object because the list might have security attributes. The
    ///                 other two lists
    ///                 are merely Java lists containing the real data.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:externalReference</u>: A boolean attribute, true if this Access element describes an external
    ///                 resource (optional,
    ///                 starting in DDMS 4.1)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    ///     @since 2.0.0
    /// </summary>
    public sealed class Access : AbstractBaseComponent
    {
        private const string INDIVIDUAL_LIST_NAME = "AccessIndividualList";
        private const string GROUP_LIST_NAME = "AccessGroupList";
        private const string EXTERNAL_REFERENCE_NAME = "externalReference";
        private readonly List<Group> _groups;
        private readonly List<Individual> _individuals;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Access(Element element)
        {
            ProfileList = null;
            try
            {
                SetElement(element, false);
                _individuals = new List<Individual>();
                Element individualList = element.Element(XName.Get(INDIVIDUAL_LIST_NAME, Namespace));
                if (individualList != null)
                {
                    IEnumerable<Element> individuals = individualList.Elements();
                    foreach (var individual in individuals)
                    {
                        _individuals.Add(new Individual(individual));
                    }
                }
                _groups = new List<Group>();
                Element groupList = element.Element(XName.Get(GROUP_LIST_NAME, Namespace));
                if (groupList != null)
                {
                    IEnumerable<Element> groups = groupList.Elements();
                    foreach (var group in groups)
                        _groups.Add(new Group(group));
                }
                Element profileList = element.Element(XName.Get(ProfileList.GetName(DDMSVersion), Namespace));
                if (profileList != null)
                {
                    ProfileList = new ProfileList(profileList);
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
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// @deprecated A new constructor was added for DDMS 4.1 to support ntk:externalResource. This constructor is preserved for 
        /// backwards compatibility, but may disappear in the next major release.
        /// <param name="individuals"> a list of individuals </param>
        /// <param name="groups"> a list of groups </param>
        /// <param name="profileList"> the profile list </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Access(List<Individual> individuals, List<Group> groups, ProfileList profileList,
            SecurityAttributes securityAttributes) : this(individuals, groups, profileList, null, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="individuals"> a list of individuals </param>
        /// <param name="groups"> a list of groups </param>
        /// <param name="profileList"> the profile list </param>
        /// <param name="externalReference"> a boolean attribute (optional) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Access(List<Individual> individuals, List<Group> groups, ProfileList profileList, bool? externalReference,
            SecurityAttributes securityAttributes)
        {
            ProfileList = null;
            try
            {
                DDMSVersion version = DDMSVersion.GetCurrentVersion();
                string ntkPrefix = PropertyReader.GetPrefix("ntk");
                string ntkNamespace = version.NtkNamespace;

                Element element = Util.Util.BuildElement(ntkPrefix, GetName(version), ntkNamespace, null);
                SetElement(element, false);

                if (individuals == null)
                {
                    individuals = new List<Individual>();
                }
                if (individuals.Count > 0)
                {
                    Element individualList = Util.Util.BuildElement(ntkPrefix, INDIVIDUAL_LIST_NAME, ntkNamespace, null);
                    element.Add(individualList);
                    foreach (var individual in individuals)
                    {
                        individualList.Add(individual.ElementCopy);
                    }
                }
                if (groups == null)
                {
                    groups = new List<Group>();
                }
                if (groups.Count > 0)
                {
                    Element groupList = Util.Util.BuildElement(ntkPrefix, GROUP_LIST_NAME, ntkNamespace, null);
                    element.Add(groupList);
                    foreach (var group in groups)
                    {
                        groupList.Add(group.ElementCopy);
                    }
                }
                if (profileList != null)
                {
                    element.Add(profileList.ElementCopy);
                }
                if (externalReference != null)
                {
                    Util.Util.AddAttribute(element, ntkPrefix, EXTERNAL_REFERENCE_NAME, ntkNamespace,
                        Convert.ToString(externalReference));
                }

                _individuals = individuals;
                _groups = groups;
                ProfileList = profileList;
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
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
                list.AddRange(Individuals);
                list.AddRange(Groups);
                list.Add(ProfileList);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the individuals
        /// </summary>
        public List<Individual> Individuals
        {
            get { return _individuals; }
        }

        /// <summary>
        ///     Accessor for the groups
        /// </summary>
        public List<Group> Groups
        {
            get { return _groups; }
        }

        /// <summary>
        ///     Accessor for the profileList
        /// </summary>
        public ProfileList ProfileList { get; set; }

        /// <summary>
        ///     Accessor for the externalReference attribute. This may be null for Access elements before DDMS 4.1.
        /// </summary>
        public bool? ExternalReference
        {
            get
            {
                string value = GetAttributeValue(EXTERNAL_REFERENCE_NAME, DDMSVersion.NtkNamespace);
                if ("true".Equals(value))
                {
                    return (true);
                }
                if ("false".Equals(value))
                {
                    return (false);
                }
                return (null);
            }
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
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>A classification is required.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>This component cannot exist until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireQualifiedName(Element, Namespace, GetName(DDMSVersion));
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

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
        ///                 <li>No individuals, groups, or profiles are described in this Access element.</li>
        ///                 <li>An externalReference attribute may cause issues for DDMS 4.0 records.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (Individuals.Count == 0 && Groups.Count == 0 && ProfileList == null)
            {
                AddWarning("An ntk:Access element was found with no individual, group, or profile information.");
            }
            if (ExternalReference != null)
            {
                AddDdms40Warning("ntk:externalReference attribute");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, "access", suffix) + ".";
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + "individualList.", Individuals));
            text.Append(BuildOutput(isHtml, localPrefix + "groupList.", Groups));
            if (ProfileList != null)
            {
                text.Append(ProfileList.GetOutput(isHtml, localPrefix, ""));
            }
            if (ExternalReference != null)
            {
                text.Append(BuildOutput(isHtml, localPrefix + EXTERNAL_REFERENCE_NAME,
                    Convert.ToString(ExternalReference)));
            }
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Access))
            {
                return (false);
            }
            var test = (Access) obj;
            return (Util.Util.NullEquals(ExternalReference, test.ExternalReference));
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("Access");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            internal const long SerialVersionUID = 7851044806424206976L;
            internal bool? _externalReference;
            internal List<Group.Builder> _groups;
            internal List<Individual.Builder> _individuals;
            internal ProfileList.Builder _profileList;
            internal SecurityAttributes.Builder _securityAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Access access)
            {
                foreach (var individual in access.Individuals)
                {
                    Individuals.Add(new Individual.Builder(individual));
                }
                foreach (var group in access.Groups)
                {
                    Groups.Add(new Group.Builder(group));
                }
                if (access.ProfileList != null)
                {
                    ProfileList = new ProfileList.Builder(access.ProfileList);
                }
                ExternalReference = access.ExternalReference;
                SecurityAttributes = new SecurityAttributes.Builder(access.SecurityAttributes);
            }


            /// <summary>
            ///     Builder accessor for the individuals
            /// </summary>
            public virtual List<Individual.Builder> Individuals
            {
                get
                {
                    if (_individuals == null)
                    {
                        _individuals = new List<Individual.Builder>();
                    }
                    return _individuals;
                }
            }

            /// <summary>
            ///     Builder accessor for the groups
            /// </summary>
            public virtual List<Group.Builder> Groups
            {
                get
                {
                    if (_groups == null)
                    {
                        _groups = new List<Group.Builder>();
                    }
                    return _groups;
                }
            }

            /// <summary>
            ///     Builder accessor for the profileList
            /// </summary>
            public virtual ProfileList.Builder ProfileList
            {
                get
                {
                    if (_profileList == null)
                    {
                        _profileList = new ProfileList.Builder();
                    }
                    return _profileList;
                }
                set { _profileList = value; }
            }


            /// <summary>
            ///     Accessor for the externalReference attribute
            /// </summary>
            public virtual bool? ExternalReference
            {
                get { return _externalReference; }
                set { _externalReference = value; }
            }


            /// <summary>
            ///     Builder accessor for the securityAttributes
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

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                var individuals = new List<Individual>();
                foreach (IBuilder builder in Individuals)
                {
                    var component = (Individual) builder.Commit();
                    if (component != null)
                    {
                        individuals.Add(component);
                    }
                }
                var groups = new List<Group>();
                foreach (IBuilder builder in Groups)
                {
                    var component = (Group) builder.Commit();
                    if (component != null)
                    {
                        groups.Add(component);
                    }
                }

                return
                    (new Access(individuals, groups, (ProfileList) ProfileList.Commit(), ExternalReference,
                        SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in Individuals)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    foreach (IBuilder builder in Groups)
                    {
                        hasValueInList = hasValueInList || !builder.Empty;
                    }
                    return (!hasValueInList && ProfileList.Empty && ExternalReference == null &&
                            SecurityAttributes.Empty);
                }
            }
        }
    }
}