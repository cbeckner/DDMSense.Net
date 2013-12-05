using System;
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
namespace DDMSSense.DDMS.SecurityElements.Ntk {


	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using DDMSSense.DDMS;

	/// <summary>
	/// An immutable implementation of ntk:AccessProfileList.
	/// 
	/// <para>Unlike ntk:AccessIndividualList and ntk:AccessGroupList, this element is implemented in DDMSence because it has 
	/// security attributes.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ntk:AccessProfile</u>: A system access record matching a profile (1-to-many required), implemented as a 
	/// <seealso cref="Profile"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	///  
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </para>
	/// </summary>
	public sealed class ProfileList : AbstractBaseComponent {

		private List<Profile> _profiles = null;
		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public ProfileList(Element element) {
			try {
				SetXOMElement(element, false);
				IEnumerable<Element> values = element.Elements(XName.Get(Profile.GetName(DDMSVersion), Namespace));
				_profiles = new List<Profile>();
				for (int i = 0; i < values.Count; i++) {
					_profiles.Add(new Profile(values.get(i)));
				}
				_securityAttributes = new SecurityAttributes(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="profiles"> the list of profiles (at least 1 required) </param>
		/// <param name="securityAttributes"> security attributes (required) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public ProfileList(List<Profile> profiles, SecurityAttributes securityAttributes) {
			try {
				DDMSVersion version = DDMSVersion.GetCurrentVersion();
				Element element = Util.BuildElement(PropertyReader.GetPrefix("ntk"), ProfileList.GetName(version), version.NtkNamespace, null);
				SetXOMElement(element, false);
				if (profiles == null) {
					profiles = new List<Profile>();
				}
				foreach (Profile profile in profiles) {
                    Element.Add(profile.XOMElementCopy);
				}
				_profiles = profiles;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
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
		/// <li>At least 1 profile is required.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// <li>This component cannot exist until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireQualifiedName(Element, Namespace, ProfileList.GetName(DDMSVersion));
			if (Profiles.Count == 0) {
				throw new InvalidDDMSException("At least one profile is required.");
			}
			Util.RequireDDMSValue("security attributes", SecurityAttributes);
			SecurityAttributes.RequireClassification();

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, "profileList", suffix) + ".";
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, Profiles));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.AddRange(Profiles);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is ProfileList)) {
				return (false);
			}
			return (true);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("AccessProfileList");
		}

		/// <summary>
		/// Accessor for the list of profile values (1-many)
		/// </summary>
		public List<Profile> Profiles {
			get {
				return _profiles;
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
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 7851044806424206976L;
			internal List<Profile.Builder> _profiles;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(ProfileList profileList) {
				foreach (Profile value in profileList.Profiles) {
					Profiles.Add(new Profile.Builder(value));
				}
				SecurityAttributes = new SecurityAttributes.Builder(profileList.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


			public virtual ProfileList Commit() {
				if (Empty) {
					return (null);
				}
				List<Profile> values = new List<Profile>();
				foreach (IBuilder builder in Profiles) {
					Profile component = (Profile) builder.Commit();
					if (component != null) {
						values.Add(component);
					}
				}
				return (new ProfileList(values, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in Profiles) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the values
			/// </summary>
			public virtual List<Profile.Builder> Profiles {
				get {
					if (_profiles == null) {
						_profiles = new List<Profile.Builder>();
					}
					return _profiles;
				}
			}

			/// <summary>
			/// Builder accessor for the securityAttributes
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

		}
	}
}