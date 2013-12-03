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
namespace DDMSSense.DDMS.Summary {


	using Element = System.Xml.Linq.XElement;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ddms:geographicIdentifier.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>No more than 1 countryCode, subDivisionCode, or facilityIdentifier can be used. The schema seems to support this 
	/// assertion with explicit restrictions on those elements, but the enclosing xs:choice element allows multiples.</li>
	/// <li>At least 1 of name, region, countryCode, subDivisionCode, or facilityIdentifier must be present. Once again, the 
	/// xs:choice restrictions create a loophole which could allow a completely empty geographicIdentifier to be valid.</li>
	/// </ul>
	/// </td></tr></table>
	/// 				
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:name</u>: geographic name (0-many optional)<br />
	/// <u>ddms:region</u>: geographic region (0-many optional)<br />
	/// <u>ddms:countryCode</u>: the country code (0-1 optional), implemented as a <seealso cref="CountryCode"/><br />
	/// <u>ddms:subDivisionCode</u>: the subdivision code (0-1 optional, starting in DDMS 4.0.1), implemented as a 
	/// <seealso cref="SubDivisionCode"/><br />
	/// <u>ddms:facilityIdentifier</u>: the facility identifier (0-1 optional), implemented as a 
	/// <seealso cref="FacilityIdentifier"/><br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class GeographicIdentifier : AbstractBaseComponent {

		private List<string> _names = null;
		private List<string> _regions = null;
		private CountryCode _countryCode = null;
		private SubDivisionCode _subDivisionCode = null;
		private FacilityIdentifier _facilityIdentifier = null;

		private const string NAME_NAME = "name";
		private const string REGION_NAME = "region";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public GeographicIdentifier(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public GeographicIdentifier(Element element) {
			try {
				SetXOMElement(element, false);
				DDMSVersion version = DDMSVersion;
				_names = Util.GetDDMSChildValues(element, NAME_NAME);
				_regions = Util.GetDDMSChildValues(element, REGION_NAME);
				Element countryCodeElement = element.Element(XName.Get(CountryCode.GetName(version), Namespace));
				if (countryCodeElement != null) {
					_countryCode = new CountryCode(countryCodeElement);
				}
				Element subDivisionCodeElement = element.Element(XName.Get(SubDivisionCode.GetName(version), Namespace));
				if (subDivisionCodeElement != null) {
					_subDivisionCode = new SubDivisionCode(subDivisionCodeElement);
				}
				Element facilityIdentifierElement = element.Element(XName.Get(FacilityIdentifier.GetName(version), Namespace));
				if (facilityIdentifierElement != null) {
					_facilityIdentifier = new FacilityIdentifier(facilityIdentifierElement);
				}
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data. Note that the facilityIdentifier component cannot be used
		/// with the components in this constructor. 
		/// </summary>
		/// <param name="names"> the names (optional) </param>
		/// <param name="regions"> the region names (optional) </param>
		/// <param name="countryCode"> the country code (optional) </param>
		/// <param name="subDivisionCode"> the subdivision code (optional, starting in DDMS 4.0.1) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public GeographicIdentifier(java.util.List<String> names, java.util.List<String> regions, CountryCode countryCode, SubDivisionCode subDivisionCode) throws DDMSSense.DDMS.InvalidDDMSException
		public GeographicIdentifier(List<string> names, List<string> regions, CountryCode countryCode, SubDivisionCode subDivisionCode) {
			try {
				if (names == null) {
					names = new List<string>();
				}
				if (regions == null) {
					regions = new List<string>();
				}
				Element element = Util.BuildDDMSElement(GeographicIdentifier.GetName(DDMSVersion.GetCurrentVersion()), null);
				foreach (string name in names) {
					element.Add(Util.BuildDDMSElement(NAME_NAME, name));
				}
				foreach (string region in regions) {
					element.Add(Util.BuildDDMSElement(REGION_NAME, region));
				}
				if (countryCode != null) {
					element.Add(countryCode.XOMElementCopy);
				}
				if (subDivisionCode != null) {
					element.Add(subDivisionCode.XOMElementCopy);
				}
				_names = names;
				_regions = regions;
				_countryCode = countryCode;
				_subDivisionCode = subDivisionCode;
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="facilityIdentifier"> the facility identifier (required in this constructor) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public GeographicIdentifier(FacilityIdentifier facilityIdentifier) throws DDMSSense.DDMS.InvalidDDMSException
		public GeographicIdentifier(FacilityIdentifier facilityIdentifier) {
			Element element = Util.BuildDDMSElement(GeographicIdentifier.GetName(DDMSVersion.GetCurrentVersion()), null);
			if (facilityIdentifier != null) {
				element.Add(facilityIdentifier.XOMElementCopy);
			}
			_names = new List<string>();
			_regions = new List<string>();
			_facilityIdentifier = facilityIdentifier;
			SetXOMElement(element, true);
		}
		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>At least 1 of name, region, countryCode, subDivisionCode or facilityIdentifier must exist.</li>
		/// <li>No more than 1 countryCode, subDivisionCode or facilityIdentifier can exist.</li>
		/// <li>If facilityIdentifier is used, no other components can exist.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, GeographicIdentifier.GetName(DDMSVersion));
			if (Names.Count == 0 && Regions.Count == 0 && CountryCode == null && SubDivisionCode == null && FacilityIdentifier == null) {
				throw new InvalidDDMSException("At least 1 of name, region, countryCode, subDivisionCode, or facilityIdentifier must exist.");
			}
			Util.RequireBoundedChildCount(Element, CountryCode.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, SubDivisionCode.GetName(DDMSVersion), 0, 1);
			Util.RequireBoundedChildCount(Element, FacilityIdentifier.GetName(DDMSVersion), 0, 1);
			if (HasFacilityIdentifier()) {
				if (Names.Count > 0 || Regions.Count > 0 || CountryCode != null || SubDivisionCode != null) {
					throw new InvalidDDMSException("facilityIdentifier cannot be used in tandem with other components.");
				}
			}
			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + NAME_NAME, Names));
			text.Append(BuildOutput(isHTML, localPrefix + REGION_NAME, Regions));
			if (CountryCode != null) {
				text.Append(CountryCode.GetOutput(isHTML, localPrefix, ""));
			}
			if (SubDivisionCode != null) {
				text.Append(SubDivisionCode.GetOutput(isHTML, localPrefix, ""));
			}
			if (HasFacilityIdentifier()) {
				text.Append(FacilityIdentifier.GetOutput(isHTML, localPrefix, ""));
			}
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(CountryCode);
				list.Add(SubDivisionCode);
				list.Add(FacilityIdentifier);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is GeographicIdentifier)) {
				return (false);
			}
			GeographicIdentifier test = (GeographicIdentifier) obj;
			return (Util.ListEquals(Names, test.Names) && Util.ListEquals(Regions, test.Regions));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Names.GetHashCode();
			result = 7 * result + Regions.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("geographicIdentifier");
		}

		/// <summary>
		/// Accessor for the names
		/// </summary>
		public List<string> Names {
			get {
				return _names;
			}
			set {
                _names = value;
			}
		}

		/// <summary>
		/// Accessor for the regions
		/// </summary>
		public List<string> Regions {
			get {
				return _regions;
			}
			set {
                _regions = value;
			}
		}

		/// <summary>
		/// Accessor for the country code. May return null if no code was used.
		/// </summary>
		public CountryCode CountryCode {
			get {
				return (_countryCode);
			}
			set {
					_countryCode = value;
			}
		}

		/// <summary>
		/// Accessor for the subdivision code. May return null if no code was used.
		/// </summary>
		public SubDivisionCode SubDivisionCode {
			get {
				return (_subDivisionCode);
			}
			set {
					_subDivisionCode = value;
			}
		}

		/// <summary>
		/// Accessor for the facility identifier. May return null if no identifier was used.
		/// </summary>
		public FacilityIdentifier FacilityIdentifier {
			get {
				return (_facilityIdentifier);
			}
			set {
					_facilityIdentifier = value;
			}
		}

		/// <summary>
		/// Accessor for whether this geographic identifier is using a facility identifier.
		/// </summary>
		public bool HasFacilityIdentifier() {
			return (FacilityIdentifier != null);
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = -6626896938484051916L;
			internal List<string> _names = null;
			internal List<string> _regions = null;
			internal CountryCode.Builder _countryCode = null;
			internal SubDivisionCode.Builder _subDivisionCode = null;
			internal FacilityIdentifier.Builder _facilityIdentifier = null;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(GeographicIdentifier identifier) {
				if (identifier.HasFacilityIdentifier()) {
					FacilityIdentifier = new FacilityIdentifier.Builder(identifier.FacilityIdentifier);
				} else {
					Names = identifier.Names;
					Regions = identifier.Regions;
					if (identifier.CountryCode != null) {
						CountryCode = new CountryCode.Builder(identifier.CountryCode);
					}
					if (identifier.SubDivisionCode != null) {
						SubDivisionCode = new SubDivisionCode.Builder(identifier.SubDivisionCode);
					}
				}
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public GeographicIdentifier commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual GeographicIdentifier Commit() {
				if (Empty) {
					return (null);
				}
				FacilityIdentifier identifier = FacilityIdentifier.Commit();
				if (identifier != null) {
					return (new GeographicIdentifier(identifier));
				}
				return (new GeographicIdentifier(Names, Regions, CountryCode.Commit(), SubDivisionCode.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (Util.ContainsOnlyEmptyValues(Names) && Util.ContainsOnlyEmptyValues(Regions) && CountryCode.Empty && SubDivisionCode.Empty && FacilityIdentifier.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the names
			/// </summary>
			public virtual List<string> Names {
				get {
					if (_names == null) {
                        _names = new List<string>();
					}
					return _names;
				}
                set { _names = value; }
			}


			/// <summary>
			/// Builder accessor for the regions
			/// </summary>
			public virtual List<string> Regions {
				get {
					if (_regions == null) {
                        _regions = new List<string>();
					}
					return _regions;
				}
                set { _regions = value; }
			}


			/// <summary>
			/// Builder accessor for the country code
			/// </summary>
			public virtual CountryCode.Builder CountryCode {
				get {
					if (_countryCode == null) {
						_countryCode = new CountryCode.Builder();
					}
					return _countryCode;
				}
                set { _countryCode = value; }
			}


			/// <summary>
			/// Builder accessor for the subdivision code
			/// </summary>
			public virtual SubDivisionCode.Builder SubDivisionCode {
				get {
					if (_subDivisionCode == null) {
						_subDivisionCode = new SubDivisionCode.Builder();
					}
					return _subDivisionCode;
				}
                set { _subDivisionCode = value; }
			}


			/// <summary>
			/// Builder accessor for the facility identifier
			/// </summary>
			public virtual FacilityIdentifier.Builder FacilityIdentifier {
				get {
					if (_facilityIdentifier == null) {
						_facilityIdentifier = new FacilityIdentifier.Builder();
					}
					return _facilityIdentifier;
				}
                set { _facilityIdentifier = value; }
			}

		}
	}
}