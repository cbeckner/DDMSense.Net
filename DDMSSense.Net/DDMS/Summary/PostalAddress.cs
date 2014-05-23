#region usings

using DDMSense.Extensions;
using DDMSense.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

#endregion usings

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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion usings

    /// <summary>
    ///     An immutable implementation of ddms:postalAddress.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A postalAddress element can be used with no child elements.</li>
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
    ///                 <u>ddms:street</u>: the street address (0-6 optional)<br />
    ///                 <u>ddms:city</u>: the city (0-1 optional)<br />
    ///                 <u>ddms:state</u>: the state (0-1 optional)<br />
    ///                 <u>ddms:province</u>: the province (0-1 optional)<br />
    ///                 <u>ddms:postalCode</u>: the postal code (0-1 optional)<br />
    ///                 <u>ddms:countryCode</u>: the country code (0-1 optional), implemented as a <see cref="CountryCode" />
    ///                 <br />
    ///             </td>
    ///         </tr>
    ///     </table>

    /// </summary>
    public sealed class PostalAddress : AbstractBaseComponent
    {
        private const string StreetName = "street";
        private const string CityName = "city";
        private const string StateName = "state";
        private const string ProvinceName = "province";
        private const string PostalCodeName = "postalCode";
        private string _city;
        private CountryCode _countryCode;
        private string _postalCode;
        private string _province;
        private string _state;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public PostalAddress(Element element)
        {
            try
            {
                SetElement(element, false);
                Streets = Util.Util.GetDDMSChildValues(element, StreetName);
                Element cityElement = element.Element(XName.Get(CityName, Namespace));
                if (cityElement != null)
                {
                    _city = cityElement.Value;
                }
                Element stateElement = element.Element(XName.Get(StateName, Namespace));
                if (stateElement != null)
                {
                    _state = stateElement.Value;
                }
                Element provinceElement = element.Element(XName.Get(ProvinceName, Namespace));
                if (provinceElement != null)
                {
                    _province = provinceElement.Value;
                }
                Element postalCodeElement = element.Element(XName.Get(PostalCodeName, Namespace));
                if (postalCodeElement != null)
                {
                    _postalCode = postalCodeElement.Value;
                }
                Element countryCodeElement = element.Element(XName.Get(CountryCode.GetName(DDMSVersion), Namespace));
                if (countryCodeElement != null)
                {
                    _countryCode = new CountryCode(countryCodeElement);
                }
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw;
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="streets"> the street address lines (0-6) </param>
        /// <param name="city"> the city (optional) </param>
        /// <param name="stateOrProvince"> the state or province (optional) </param>
        /// <param name="postalCode"> the postal code (optional) </param>
        /// <param name="countryCode"> the country code (optional) </param>
        /// <param name="hasState">
        ///     true if the stateOrProvince is a state, false if it is a province (only 1 of state or province
        ///     can exist in a postalAddress)
        /// </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public PostalAddress(List<string> streets, string city, string stateOrProvince, string postalCode,
            CountryCode countryCode, bool hasState)
        {
            try
            {
                if (streets == null)
                {
                    streets = new List<string>();
                }
                Element element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), null);
                foreach (var street in streets)
                {
                    element.Add(Util.Util.BuildDDMSElement(StreetName, street));
                }
                Util.Util.AddDDMSChildElement(element, CityName, city);
                if (hasState)
                {
                    Util.Util.AddDDMSChildElement(element, StateName, stateOrProvince);
                }
                else
                {
                    Util.Util.AddDDMSChildElement(element, ProvinceName, stateOrProvince);
                }
                Util.Util.AddDDMSChildElement(element, PostalCodeName, postalCode);
                if (countryCode != null)
                {
                    element.Add(countryCode.ElementCopy);
                }
                Streets = streets;
                _city = city;
                _state = hasState ? stateOrProvince : "";
                _province = hasState ? "" : stateOrProvince;
                _postalCode = postalCode;
                _countryCode = countryCode;
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw;
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(CountryCode);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the street addresses (max 6)
        /// </summary>
        public List<string> Streets { get; set; }

        /// <summary>
        ///     Accessor for the city
        /// </summary>
        public string City
        {
            get { return _city.ToNonNullString(); }
            set { _city = value; }
        }

        /// <summary>
        ///     Accessor for the state
        /// </summary>
        public string State
        {
            get { return _state.ToNonNullString(); }
            set { _state = value; }
        }

        /// <summary>
        ///     Accessor for the province
        /// </summary>
        public string Province
        {
            get { return _province.ToNonNullString(); }
            set { _province = value; }
        }

        /// <summary>
        ///     Accessor for the postalCode
        /// </summary>
        public string PostalCode
        {
            get { return _postalCode.ToNonNullString(); }
            set { _postalCode = value; }
        }

        /// <summary>
        ///     Accessor for the country code
        /// </summary>
        public CountryCode CountryCode
        {
            get { return (_countryCode); }
            set { _countryCode = value; }
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
        ///                 <li>Either a state or a province can exist, but not both.</li>
        ///                 <li>0-6 streets, 0-1 cities, 0-1 states, 0-1 provinces, 0-1 postal codes, and 0-1 country codes exist.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            if (!String.IsNullOrEmpty(State) && !String.IsNullOrEmpty(Province))
            {
                throw new InvalidDDMSException("Only 1 of state or province can be used.");
            }
            Util.Util.RequireBoundedChildCount(Element, StreetName, 0, 6);
            Util.Util.RequireBoundedChildCount(Element, CityName, 0, 1);
            Util.Util.RequireBoundedChildCount(Element, StateName, 0, 1);
            Util.Util.RequireBoundedChildCount(Element, ProvinceName, 0, 1);
            Util.Util.RequireBoundedChildCount(Element, PostalCodeName, 0, 1);
            Util.Util.RequireBoundedChildCount(Element, CountryCode.GetName(DDMSVersion), 0, 1);

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
        ///                 <li>A completely empty ddms:postalAddress element was found.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (Streets.Count == 0 && String.IsNullOrEmpty(City) && String.IsNullOrEmpty(State) &&
                String.IsNullOrEmpty(Province) && String.IsNullOrEmpty(PostalCode) && CountryCode == null)
            {
                AddWarning("A completely empty ddms:postalAddress element was found.");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + StreetName, Streets));
            text.Append(BuildOutput(isHtml, localPrefix + CityName, City));
            text.Append(BuildOutput(isHtml, localPrefix + StateName, State));
            text.Append(BuildOutput(isHtml, localPrefix + ProvinceName, Province));
            text.Append(BuildOutput(isHtml, localPrefix + PostalCodeName, PostalCode));
            if (CountryCode != null)
            {
                text.Append(CountryCode.GetOutput(isHtml, localPrefix, ""));
            }
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is PostalAddress))
            {
                return (false);
            }
            var test = (PostalAddress)obj;
            return (Util.Util.ListEquals(Streets, test.Streets) && City.Equals(test.City) && State.Equals(test.State) &&
                    Province.Equals(test.Province) && PostalCode.Equals(test.PostalCode));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Streets.GetOrderIndependentHashCode();
            result = 7 * result + City.GetHashCode();
            result = 7 * result + State.GetHashCode();
            result = 7 * result + Province.GetHashCode();
            result = 7 * result + PostalCode.GetHashCode();
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
            return ("postalAddress");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public sealed class Builder : IBuilder
        {
            internal string _city;
            internal CountryCode.Builder _countryCode;
            internal string _postalCode;
            internal string _province;
            internal string _state;
            internal List<string> _streets;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(PostalAddress address)
            {
                Streets = address.Streets;
                City = address.City;
                State = address.State;
                Province = address.Province;
                PostalCode = address.PostalCode;
                if (address.CountryCode != null)
                {
                    CountryCode = new CountryCode.Builder(address.CountryCode);
                }
            }

            /// <summary>
            ///     Builder accessor for the streets
            /// </summary>
            public List<string> Streets
            {
                get
                {
                    if (_streets == null)
                    {
                        _streets = new List<string>();
                    }
                    return _streets;
                }
                set { _streets = value; }
            }

            /// <summary>
            ///     Builder accessor for the city
            /// </summary>
            public string City
            {
                get { return _city; }
                set { _city = value; }
            }

            /// <summary>
            ///     Builder accessor for the state
            /// </summary>
            public string State
            {
                get { return _state; }
                set { _state = value; }
            }

            /// <summary>
            ///     Builder accessor for the province
            /// </summary>
            public string Province
            {
                get { return _province; }
                set { _province = value; }
            }

            /// <summary>
            ///     Builder accessor for the postalCode
            /// </summary>
            public string PostalCode
            {
                get { return _postalCode; }
                set { _postalCode = value; }
            }

            /// <summary>
            ///     Builder accessor for the countryCode
            /// </summary>
            public CountryCode.Builder CountryCode
            {
                get
                {
                    if (_countryCode == null)
                    {
                        _countryCode = new CountryCode.Builder();
                    }
                    return _countryCode;
                }
                set { _countryCode = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public IDDMSComponent Commit()
            {
                if (Empty)
                {
                    return (null);
                }
                bool hasStateAndProvince = (!String.IsNullOrEmpty(State) && !String.IsNullOrEmpty(Province));
                if (hasStateAndProvince)
                {
                    throw new InvalidDDMSException("Only 1 of state or province can be used.");
                }
                bool hasState = !String.IsNullOrEmpty(State);
                string stateOrProvince = hasState ? State : Province;
                return
                    (new PostalAddress(Streets, City, stateOrProvince, PostalCode, (CountryCode)CountryCode.Commit(),
                        hasState));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public bool Empty
            {
                get
                {
                    return (Util.Util.ContainsOnlyEmptyValues(Streets) && String.IsNullOrEmpty(City) &&
                            String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Province) &&
                            String.IsNullOrEmpty(PostalCode) && CountryCode.Empty);
                }
            }
        }
    }
}