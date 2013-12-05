using System;
using System.Collections.Generic;
using System.Text;
using DDMSSense.Extensions;
using System.Linq;
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
	/// An immutable implementation of ddms:postalAddress.
	///  
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A postalAddress element can be used with no child elements.</li>
	/// </ul>
	/// </td></tr></table>
	/// 	
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:street</u>: the street address (0-6 optional)<br />
	/// <u>ddms:city</u>: the city (0-1 optional)<br />
	/// <u>ddms:state</u>: the state (0-1 optional)<br />
	/// <u>ddms:province</u>: the province (0-1 optional)<br />
	/// <u>ddms:postalCode</u>: the postal code (0-1 optional)<br />
	/// <u>ddms:countryCode</u>: the country code (0-1 optional), implemented as a <seealso cref="CountryCode"/><br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class PostalAddress : AbstractBaseComponent {

		private List<string> _streets = null;
		private string _city = null;
		private string _state = null;
		private string _province = null;
		private string _postalCode = null;
		private CountryCode _countryCode = null;

		private const string STREET_NAME = "street";
		private const string CITY_NAME = "city";
		private const string STATE_NAME = "state";
		private const string PROVINCE_NAME = "province";
		private const string POSTAL_CODE_NAME = "postalCode";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public PostalAddress(Element element) {
			try {
				SetXOMElement(element, false);
				_streets = Util.GetDDMSChildValues(element, STREET_NAME);
				Element cityElement = element.Element(XName.Get(CITY_NAME, Namespace));
				if (cityElement != null) {
					_city = cityElement.Value;
				}
				Element stateElement = element.Element(XName.Get(STATE_NAME, Namespace));
				if (stateElement != null) {
					_state = stateElement.Value;
				}
				Element provinceElement = element.Element(XName.Get(PROVINCE_NAME, Namespace));
				if (provinceElement != null) {
					_province = provinceElement.Value;
				}
				Element postalCodeElement = element.Element(XName.Get(POSTAL_CODE_NAME, Namespace));
				if (postalCodeElement != null) {
					_postalCode = postalCodeElement.Value;
				}
				Element countryCodeElement = element.Element(XName.Get(CountryCode.GetName(DDMSVersion), Namespace));
				if (countryCodeElement != null) {
					_countryCode = new CountryCode(countryCodeElement);
				}
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="streets"> the street address lines (0-6) </param>
		/// <param name="city"> the city (optional) </param>
		/// <param name="stateOrProvince"> the state or province (optional) </param>
		/// <param name="postalCode"> the postal code (optional) </param>
		/// <param name="countryCode"> the country code (optional) </param>
		/// <param name="hasState"> true if the stateOrProvince is a state, false if it is a province (only 1 of state or province 
		/// can exist in a postalAddress) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public PostalAddress(List<string> streets, string city, string stateOrProvince, string postalCode, CountryCode countryCode, bool hasState) {
			try {
				if (streets == null) {
					streets = new List<string>();
				}
				Element element = Util.BuildDDMSElement(PostalAddress.GetName(DDMSVersion.GetCurrentVersion()), null);
				foreach (string street in streets) {
					element.Add(Util.BuildDDMSElement(STREET_NAME, street));
				}
				Util.AddDDMSChildElement(element, CITY_NAME, city);
				if (hasState) {
					Util.AddDDMSChildElement(element, STATE_NAME, stateOrProvince);
				} else {
					Util.AddDDMSChildElement(element, PROVINCE_NAME, stateOrProvince);
				}
				Util.AddDDMSChildElement(element, POSTAL_CODE_NAME, postalCode);
				if (countryCode != null) {
					element.Add(countryCode.XOMElementCopy);
				}
				_streets = streets;
				_city = city;
				_state = hasState ? stateOrProvince : "";
				_province = hasState ? "" : stateOrProvince;
				_postalCode = postalCode;
				_countryCode = countryCode;
				SetXOMElement(element, true);
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
		/// <li>Either a state or a province can exist, but not both.</li>
		/// <li>0-6 streets, 0-1 cities, 0-1 states, 0-1 provinces, 0-1 postal codes, and 0-1 country codes exist.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, PostalAddress.GetName(DDMSVersion));
			if (!String.IsNullOrEmpty(State) && !String.IsNullOrEmpty(Province)) {
				throw new InvalidDDMSException("Only 1 of state or province can be used.");
			}
			Util.RequireBoundedChildCount(Element, STREET_NAME, 0, 6);
			Util.RequireBoundedChildCount(Element, CITY_NAME, 0, 1);
			Util.RequireBoundedChildCount(Element, STATE_NAME, 0, 1);
			Util.RequireBoundedChildCount(Element, PROVINCE_NAME, 0, 1);
			Util.RequireBoundedChildCount(Element, POSTAL_CODE_NAME, 0, 1);
			Util.RequireBoundedChildCount(Element, CountryCode.GetName(DDMSVersion), 0, 1);

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A completely empty ddms:postalAddress element was found.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (Streets.Count == 0 && String.IsNullOrEmpty(City) && String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Province) && String.IsNullOrEmpty(PostalCode) && CountryCode == null) {
				AddWarning("A completely empty ddms:postalAddress element was found.");
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + STREET_NAME, Streets));
			text.Append(BuildOutput(isHTML, localPrefix + CITY_NAME, City));
			text.Append(BuildOutput(isHTML, localPrefix + STATE_NAME, State));
			text.Append(BuildOutput(isHTML, localPrefix + PROVINCE_NAME, Province));
			text.Append(BuildOutput(isHTML, localPrefix + POSTAL_CODE_NAME, PostalCode));
			if (CountryCode != null) {
				text.Append(CountryCode.GetOutput(isHTML, localPrefix, ""));
			}
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = new List<IDDMSComponent>();
				list.Add(CountryCode);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is PostalAddress)) {
				return (false);
			}
			PostalAddress test = (PostalAddress) obj;
			return (Util.ListEquals(Streets, test.Streets) && City.Equals(test.City) && State.Equals(test.State) && Province.Equals(test.Province) && PostalCode.Equals(test.PostalCode));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + Streets.GetHashCode();
			result = 7 * result + City.GetHashCode();
			result = 7 * result + State.GetHashCode();
			result = 7 * result + Province.GetHashCode();
			result = 7 * result + PostalCode.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("postalAddress");
		}

		/// <summary>
		/// Accessor for the street addresses (max 6)
		/// </summary>
		public List<string> Streets {
			get {
				return _streets;
			}
			set {
                _streets = value;
			}
		}

		/// <summary>
		/// Accessor for the city
		/// </summary>
		public string City {
			get {
				return (Util.GetNonNullString(_city));
			}
			set {
					_city = value;
			}
		}

		/// <summary>
		/// Accessor for the state
		/// </summary>
		public string State {
			get {
				return (Util.GetNonNullString(_state));
			}
			set {
					_state = value;
			}
		}

		/// <summary>
		/// Accessor for the province
		/// </summary>
		public string Province {
			get {
				return (Util.GetNonNullString(_province));
			}
			set {
					_province = value;
			}
		}

		/// <summary>
		/// Accessor for the postalCode
		/// </summary>
		public string PostalCode {
			get {
				return (Util.GetNonNullString(_postalCode));
			}
			set {
					_postalCode = value;
			}
		}

		/// <summary>
		/// Accessor for the country code
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
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 6887962646280796652L;
			internal List<string> _streets;
			internal string _city;
			internal string _state;
			internal string _province;
			internal string _postalCode;
			internal CountryCode.Builder _countryCode;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(PostalAddress address) {
				Streets = address.Streets;
				City = address.City;
				State = address.State;
				Province = address.Province;
				PostalCode = address.PostalCode;
				if (address.CountryCode != null) {
					CountryCode = new CountryCode.Builder(address.CountryCode);
				}
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				bool hasStateAndProvince = (!String.IsNullOrEmpty(State) && !String.IsNullOrEmpty(Province));
				if (hasStateAndProvince) {
					throw new InvalidDDMSException("Only 1 of state or province can be used.");
				}
				bool hasState = !String.IsNullOrEmpty(State);
				string stateOrProvince = hasState ? State : Province;
				return (new PostalAddress(Streets, City, stateOrProvince, PostalCode, CountryCode.Commit(), hasState));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (Util.ContainsOnlyEmptyValues(Streets) && String.IsNullOrEmpty(City) && String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Province) && String.IsNullOrEmpty(PostalCode) && CountryCode.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the streets
			/// </summary>
			public virtual List<string> Streets {
				get {
					if (_streets == null) {
						_streets = new List<string>();
					}
					return _streets;
				}
                set { _streets = value; }
			}


			/// <summary>
			/// Builder accessor for the city
			/// </summary>
			public virtual string City {
				get {
					return _city;
				}
                set { _city = value; }
			}


			/// <summary>
			/// Builder accessor for the state
			/// </summary>
			public virtual string State {
				get {
					return _state;
				}
                set { _state = value; }
			}


			/// <summary>
			/// Builder accessor for the province
			/// </summary>
			public virtual string Province {
				get {
					return _province;
				}
                set { _province = value; }
			}


			/// <summary>
			/// Builder accessor for the postalCode
			/// </summary>
			public virtual string PostalCode {
				get {
					return _postalCode;
				}
                set { _postalCode = value; }
			}


			/// <summary>
			/// Builder accessor for the countryCode
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

		}
	}
}