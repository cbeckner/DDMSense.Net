using System.Collections.Generic;
using System.Text;

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
namespace DDMSSense.DDMS.ResourceElements {


	using Element = System.Xml.Linq.XElement;
	
	using ExtensibleAttributes = DDMSSense.DDMS.Extensible.ExtensibleAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;

	/// <summary>
	/// An immutable implementation of ddms:organization.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>At least 1 name value must be non-empty.</li>
	/// </ul>
	/// 
	/// <para>DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A phone number can be set with no value.</li>
	/// <li>An email can be set with no value.</li>
	/// <li>An acronym can be set with no value.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <para>The name of this component was changed from "Organization" to "organization" in DDMS 4.0.1.</para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:name</u>: names of the producer entity (1-many, at least 1 required)<br />
	/// <u>ddms:phone</u>: phone numbers of the producer entity (0-many optional)<br />
	/// <u>ddms:email</u>: email addresses of the producer entity (0-many optional)<br />
	/// <u>ddms:subOrganization</u>: suborganization (0-many optional, starting in DDMS 4.0.1), implemented as a 
	/// <seealso cref="SubOrganization"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:acronym</u>: an acronym for the organization (optional, starting in DDMS 4.0.1)<br />
	/// <u><seealso cref="ExtensibleAttributes"/></u>
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Organization : AbstractRoleEntity {

		private List<SubOrganization> _subOrganizations = null;

		private const string ACRONYM_NAME = "acronym";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Organization(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public Organization(Element element) : base(element, false) {
			try {
				string @namespace = element.Name.NamespaceName;
				IEnumerable<Element> components = element.Elements(XName.Get(SubOrganization.GetName(DDMSVersion), @namespace));
				_subOrganizations = new List<SubOrganization>();
				for (int i = 0; i < components.Count; i++) {
					_subOrganizations.Add(new SubOrganization(components.Item(i)));
				}
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data.
		/// </summary>
		/// <param name="names"> an ordered list of names </param>
		/// <param name="phones"> an ordered list of phone numbers </param>
		/// <param name="emails"> an ordered list of email addresses </param>
		/// <param name="subOrganizations"> an ordered list of suborganizations </param>
		/// <param name="acronym"> the organization's acronym </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Organization(java.util.List<String> names, java.util.List<String> phones, java.util.List<String> emails, java.util.List<SubOrganization> subOrganizations, String acronym) throws DDMSSense.DDMS.InvalidDDMSException
		public Organization(List<string> names, List<string> phones, List<string> emails, List<SubOrganization> subOrganizations, string acronym) : this(names, phones, emails, subOrganizations, acronym, null) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data.
		/// </summary>
		/// <param name="names"> an ordered list of names </param>
		/// <param name="phones"> an ordered list of phone numbers </param>
		/// <param name="emails"> an ordered list of email addresses </param>
		/// <param name="subOrganizations"> an ordered list of suborganizations </param>
		/// <param name="acronym"> the organization's acronym </param>
		/// <param name="extensions"> extensible attributes (optional) </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Organization(java.util.List<String> names, java.util.List<String> phones, java.util.List<String> emails, java.util.List<SubOrganization> subOrganizations, String acronym, DDMSSense.DDMS.Extensible.ExtensibleAttributes extensions) throws DDMSSense.DDMS.InvalidDDMSException
		public Organization(List<string> names, List<string> phones, List<string> emails, List<SubOrganization> subOrganizations, string acronym, ExtensibleAttributes extensions) : base(Organization.GetName(DDMSVersion.GetCurrentVersion()), names, phones, emails, extensions, false) {
			try {
				if (subOrganizations == null) {
					subOrganizations = new List<SubOrganization>();
				}
				Util.AddDDMSAttribute(Element, ACRONYM_NAME, acronym);
				foreach (SubOrganization subOrganization in subOrganizations) {
                    Element.Add(subOrganization.XOMElementCopy);
				}
				_subOrganizations = subOrganizations;
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>Acronyms cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractRoleEntity#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Organization.GetName(DDMSVersion));

			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("4.0.1")) {
				if (!String.IsNullOrEmpty(Acronym)) {
					throw new InvalidDDMSException("An organization cannot have an acronym until DDMS 4.0.1 or later.");
				}
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A ddms:acronym attribute was found with no value.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (DDMSVersion.IsAtLeast("4.0.1")) {
                if (String.IsNullOrEmpty(Acronym) && Element.Attribute(XName.Get(ACRONYM_NAME, Namespace)).Value != null)
                {
				AddWarning("A ddms:acronym attribute was found with no value.");
				}
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, "", suffix);
			StringBuilder text = new StringBuilder(base.GetOutput(isHTML, localPrefix, ""));
			text.Append(BuildOutput(isHTML, localPrefix, SubOrganizations));
			text.Append(BuildOutput(isHTML, localPrefix + ACRONYM_NAME, Acronym));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(SubOrganizations);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Organization)) {
				return (false);
			}
			Organization test = (Organization) obj;
			return (Acronym.Equals(test.Acronym));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Acronym.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return (version.IsAtLeast("4.0.1") ? "organization" : "Organization");
		}

		/// <summary>
		/// Accessor for the suborganizations (0-many)
		/// </summary>
		public List<SubOrganization> SubOrganizations {
			get {
				return _subOrganizations;
			}
		}

		/// <summary>
		/// Accessor for the acronym
		/// </summary>
		public string Acronym {
			get {
				return (GetAttributeValue(ACRONYM_NAME));
			}
			set {                
					_acronym = value;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		public class Builder : AbstractRoleEntity.Builder {
			internal const long SerialVersionUID = 4565840434345629470L;
			internal List<SubOrganization.Builder> _subOrganizations;
			internal string _acronym;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Organization organization) : base(organization) {
				foreach (SubOrganization subOrg in organization.SubOrganizations) {
					SubOrganizations.Add(new SubOrganization.Builder(subOrg));
				}
				Acronym = organization.Acronym;
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Organization commit() throws DDMSSense.DDMS.InvalidDDMSException
			public override Organization Commit() {
				if (Empty) {
					return (null);
				}
				List<SubOrganization> subOrgs = new List<SubOrganization>();
				foreach (IBuilder builder in SubOrganizations) {
					SubOrganization component = (SubOrganization) builder.Commit();
					if (component != null) {
						subOrgs.Add(component);
					}
				}
				return (new Organization(Names, Phones, Emails, subOrgs, Acronym, ExtensibleAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public override bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in SubOrganizations) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (base.Empty && !hasValueInList && String.IsNullOrEmpty(Acronym));
				}
			}

			/// <summary>
			/// Builder accessor for suborganizations
			/// </summary>
			public virtual List<SubOrganization.Builder> SubOrganizations {
				get {
					if (_subOrganizations == null) {
                        _subOrganizations = new List<SubOrganization.Builder>();
					}
					return _subOrganizations;
				}
			}

			/// <summary>
			/// Builder accessor for the acronym
			/// </summary>
			public virtual string Acronym {
				get {
					return _acronym;
				}
                set { _acronym = value; }
			}

		}
	}
}