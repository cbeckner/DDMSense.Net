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
namespace DDMSense.Test.DDMS.Summary {



    using DDMSense.DDMS.Summary;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

	/// <summary>
	/// <para> Tests related to ddms:postalAddress elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class PostalAddressTest : AbstractBaseTestCase {

		private static readonly IList<string> TEST_STREETS = new List<string>();
		static PostalAddressTest() {
			TEST_STREETS.Add("1600 Pennsylvania Avenue, NW");
		}
		private const string TEST_CITY = "Washington";
		private const string TEST_STATE = "DC";
		private const string TEST_PROVINCE = "Alberta";
		private const string TEST_POSTAL_CODE = "20500";

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public PostalAddressTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public PostalAddressTest() : base("postalAddress.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static PostalAddress Fixture {
			get {
				try {
					return (new PostalAddress(null, null, "VA", null, null, true));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private PostalAddress GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			PostalAddress component = null;
			try {
				component = new PostalAddress(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Helper method to create an object which is expected to be valid.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="streets"> the street address lines (0-6) </param>
		/// <param name="city"> the city (optional) </param>
		/// <param name="stateOrProvince"> the state or province (optional) </param>
		/// <param name="postalCode"> the postal code (optional) </param>
		/// <param name="countryCode"> the country code (optional) </param>
		/// <param name="hasState"> true if the stateOrProvince is a state, false if it is a province (only 1 of state or province
		/// can exist in a postalAddress) </param>
		/// <returns> a valid object </returns>
		private PostalAddress GetInstance(string message, IList<string> streets, string city, string stateOrProvince, string postalCode, CountryCode countryCode, bool hasState) {
			bool expectFailure = !Util.isEmpty(message);
			PostalAddress component = null;
			try {
				component = new PostalAddress(streets, city, stateOrProvince, postalCode, countryCode, hasState);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML, boolean hasState) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML, bool hasState) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "postalAddress.street", TEST_STREETS[0]));
			text.Append(BuildOutput(isHTML, "postalAddress.city", TEST_CITY));
			if (hasState) {
				text.Append(BuildOutput(isHTML, "postalAddress.state", TEST_STATE));
			} else {
				text.Append(BuildOutput(isHTML, "postalAddress.province", TEST_PROVINCE));
			}
			text.Append(BuildOutput(isHTML, "postalAddress.postalCode", TEST_POSTAL_CODE));
			text.Append(BuildOutput(isHTML, "postalAddress.countryCode.qualifier", "ISO-3166"));
			text.Append(BuildOutput(isHTML, "postalAddress.countryCode.value", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs.
		/// @boolean whether this address has a state or a province </param>
		private string GetExpectedXMLOutput(bool preserveFormatting, bool hasState) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:postalAddress ").Append(XmlnsDDMS).Append(">\n\t");
			xml.Append("<ddms:street>1600 Pennsylvania Avenue, NW</ddms:street>\n\t");
			xml.Append("<ddms:city>Washington</ddms:city>\n\t");
			if (hasState) {
				xml.Append("<ddms:state>DC</ddms:state>\n\t");
			} else {
				xml.Append("<ddms:province>Alberta</ddms:province>\n\t");
			}
			xml.Append("<ddms:postalCode>20500</ddms:postalCode>\n\t");
			xml.Append("<ddms:countryCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
			xml.Append("</ddms:postalAddress>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, PostalAddress.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				XElement element = Util.buildDDMSElement(PostalAddress.getName(version), null);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);

				// All fields with a province
				GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);

				// No optional fields
				GetInstance(SUCCESS, null, null, null, null, null, false);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string postalName = PostalAddress.getName(version);

				// Either a state or a province but not both.
				XElement element = Util.buildDDMSElement(postalName, null);
				element.appendChild(Util.buildDDMSElement("state", TEST_STATE));
				element.appendChild(Util.buildDDMSElement("province", TEST_PROVINCE));
				GetInstance("Only 1 of state or province can be used.", element);

				// Too many streets
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 7; i++) {
					element.appendChild(Util.buildDDMSElement("street", "street" + i));
				}
				GetInstance("No more than 6 street elements can exist.", element);

				// Too many cities
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 2; i++) {
					element.appendChild(Util.buildDDMSElement("city", "city" + i));
				}
				GetInstance("No more than 1 city element can exist.", element);

				// Too many states
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 2; i++) {
					element.appendChild(Util.buildDDMSElement("state", "state" + i));
				}
				GetInstance("No more than 1 state element can exist.", element);

				// Too many provinces
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 2; i++) {
					element.appendChild(Util.buildDDMSElement("province", "province" + i));
				}
				GetInstance("No more than 1 province element can exist.", element);

				// Too many postalCodes
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 2; i++) {
					element.appendChild(Util.buildDDMSElement("postalCode", "postalCode" + i));
				}
				GetInstance("No more than 1 postalCode element can exist.", element);

				// Too many country codes
				element = Util.buildDDMSElement(postalName, null);
				for (int i = 0; i < 2; i++) {
					element.appendChild((new CountryCode("ISO-123" + i, "US" + i)).XOMElementCopy);
				}
				GetInstance("No more than 1 countryCode element can exist.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Too many streets
				IList<string> streets = new List<string>();
				for (int i = 0; i < 7; i++) {
					streets.Add("Street" + i);
				}
				GetInstance("No more than 6 street elements can exist.", streets, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// No warnings
				PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());

				// Empty element
				XElement element = Util.buildDDMSElement(PostalAddress.getName(version), null);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				string text = "A completely empty ddms:postalAddress element was found.";
				string locator = "ddms:postalAddress";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				PostalAddress elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				PostalAddress dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				PostalAddress dataComponent = GetInstance(SUCCESS, null, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_STREETS, null, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, null, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, null, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, null, CountryCodeTest.Fixture, true);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, null, true);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true, true), component.toHTML());
				assertEquals(GetExpectedOutput(false, true), component.toText());

				component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertEquals(GetExpectedOutput(true, true), component.toHTML());
				assertEquals(GetExpectedOutput(false, true), component.toText());

				component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
				assertEquals(GetExpectedOutput(true, false), component.toHTML());
				assertEquals(GetExpectedOutput(false, false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true, true), component.toXML());

				component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
				assertEquals(GetExpectedXMLOutput(false, true), component.toXML());

				component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
				assertEquals(GetExpectedXMLOutput(false, false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCountryCodeReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestCountryCodeReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode code = CountryCodeTest.Fixture;
				GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, code, true);
				GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, code, true);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
				PostalAddress.Builder builder = new PostalAddress.Builder(component);
				assertEquals(component, builder.commit());

				// No country code
				builder = new PostalAddress.Builder(component);
				builder.CountryCode = new CountryCode.Builder();
				PostalAddress address = builder.commit();
				assertNull(address.CountryCode);

				// Country code
				CountryCode countryCode = CountryCodeTest.Fixture;
				builder = new PostalAddress.Builder();
				builder.CountryCode.Qualifier = countryCode.Qualifier;
				builder.CountryCode.Value = countryCode.Value;
				builder.Streets.add("1600 Pennsylvania Avenue, NW");
				address = builder.commit();
				assertEquals(countryCode, address.CountryCode);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress.Builder builder = new PostalAddress.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.City = TEST_CITY;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				PostalAddress.Builder builder = new PostalAddress.Builder();
				builder.State = TEST_STATE;
				builder.Province = TEST_PROVINCE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "Only 1 of state or province can be used.");
				}
				builder.Province = "";
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				PostalAddress.Builder builder = new PostalAddress.Builder();
				assertNotNull(builder.Streets.get(1));
			}
		}
	}

}