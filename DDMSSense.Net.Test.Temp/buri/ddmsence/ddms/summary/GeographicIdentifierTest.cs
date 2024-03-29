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
namespace buri.ddmsence.ddms.summary {


	using Element = nu.xom.Element;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:geographicIdentifier elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class GeographicIdentifierTest : AbstractBaseTestCase {

		private static readonly IList<string> TEST_NAMES = new List<string>();
		static GeographicIdentifierTest() {
			TEST_NAMES.Add("The White House");
			TEST_REGIONS.Add("Mid-Atlantic States");
		}

		private static readonly IList<string> TEST_REGIONS = new List<string>();

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public GeographicIdentifierTest() throws buri.ddmsence.ddms.InvalidDDMSException
		public GeographicIdentifierTest() : base("geographicIdentifier.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing. This object will be based on a country code.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static GeographicIdentifier getCountryCodeBasedFixture() throws buri.ddmsence.ddms.InvalidDDMSException
		public static GeographicIdentifier CountryCodeBasedFixture {
			get {
				try {
					return new GeographicIdentifier(null, null, new CountryCode("urn:us:gov:ic:cvenum:irm:coverage:iso3166:trigraph:v1", "LAO"), null);
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing. This object will be based on a facility ID
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static GeographicIdentifier getFacIdBasedFixture() throws buri.ddmsence.ddms.InvalidDDMSException
		public static GeographicIdentifier FacIdBasedFixture {
			get {
				try {
					return (new GeographicIdentifier(FacilityIdentifierTest.Fixture));
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
		private GeographicIdentifier GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			GeographicIdentifier component = null;
			try {
				component = new GeographicIdentifier(element);
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
		/// <param name="names"> the names (optional) </param>
		/// <param name="regions"> the region names (optional) </param>
		/// <param name="countryCode"> the country code (optional) </param>
		/// <param name="subDivisionCode"> the subdivision code (optional, starting in DDMS 4.0.1) </param>
		/// <param name="facilityIdentifier"> the facility identifier (optional) </param>
		/// <returns> a valid object </returns>
		private GeographicIdentifier GetInstance(string message, IList<string> names, IList<string> regions, CountryCode countryCode, SubDivisionCode subDivisionCode, FacilityIdentifier facilityIdentifier) {
			bool expectFailure = !Util.isEmpty(message);
			GeographicIdentifier component = null;
			try {
				if (facilityIdentifier != null) {
					component = new GeographicIdentifier(facilityIdentifier);
				} else {
					component = new GeographicIdentifier(names, regions, countryCode, subDivisionCode);
				}
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
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "geographicIdentifier.name", TEST_NAMES[0]));
			text.Append(BuildOutput(isHTML, "geographicIdentifier.region", TEST_REGIONS[0]));
			text.Append(CountryCodeTest.Fixture.getOutput(isHTML, "geographicIdentifier.", ""));
			if (version.isAtLeast("4.0.1")) {
				text.Append(SubDivisionCodeTest.Fixture.getOutput(isHTML, "geographicIdentifier.", ""));
			}
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			DDMSVersion version = DDMSVersion.CurrentVersion;
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:geographicIdentifier ").Append(XmlnsDDMS).Append(">\n\t");
			xml.Append("<ddms:name>The White House</ddms:name>\n\t");
			xml.Append("<ddms:region>Mid-Atlantic States</ddms:region>\n\t");
			xml.Append("<ddms:countryCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
			if (version.isAtLeast("4.0.1")) {
				xml.Append("\t<ddms:subDivisionCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
			}
			xml.Append("</ddms:geographicIdentifier>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, GeographicIdentifier.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string geoIdName = GeographicIdentifier.getName(version);

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(Util.buildDDMSElement("name", TEST_NAMES[0]));
				GetInstance(SUCCESS, element);

				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(Util.buildDDMSElement("region", TEST_REGIONS[0]));
				GetInstance(SUCCESS, element);

				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(CountryCodeTest.Fixture.XOMElementCopy);
				GetInstance(SUCCESS, element);

				if (version.isAtLeast("4.0.1")) {
					element = Util.buildDDMSElement(geoIdName, null);
					element.appendChild(SubDivisionCodeTest.Fixture.XOMElementCopy);
					GetInstance(SUCCESS, element);
				}

				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(FacilityIdentifierTest.Fixture.XOMElementCopy);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

				// All fields
				GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);

				// No optional fields
				GetInstance(SUCCESS, TEST_NAMES, null, null, null, null);
				GetInstance(SUCCESS, null, TEST_REGIONS, null, null, null);
				GetInstance(SUCCESS, null, null, CountryCodeTest.Fixture, null, null);
				if (version.isAtLeast("4.0.1")) {
					GetInstance(SUCCESS, null, null, null, subCode, null);
				}
				GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string geoIdName = GeographicIdentifier.getName(version);

				// At least 1 name, region, countryCode, or facilityIdentifier must exist.
				Element element = Util.buildDDMSElement(geoIdName, null);
				GetInstance("At least 1 of ", element);

				// No more than 1 countryCode
				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(CountryCodeTest.Fixture.XOMElementCopy);
				element.appendChild(CountryCodeTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 countryCode", element);

				// No more than 1 subDivisionCode
				if (version.isAtLeast("4.0.1")) {
					element = Util.buildDDMSElement(geoIdName, null);
					element.appendChild(SubDivisionCodeTest.Fixture.XOMElementCopy);
					element.appendChild(SubDivisionCodeTest.Fixture.XOMElementCopy);
					GetInstance("No more than 1 subDivisionCode", element);
				}

				// No more than 1 facilityIdentifier
				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(FacilityIdentifierTest.Fixture.XOMElementCopy);
				element.appendChild(FacilityIdentifierTest.Fixture.XOMElementCopy);
				GetInstance("No more than 1 facilityIdentifier", element);

				// facilityIdentifier must be alone
				element = Util.buildDDMSElement(geoIdName, null);
				element.appendChild(CountryCodeTest.Fixture.XOMElementCopy);
				element.appendChild(FacilityIdentifierTest.Fixture.XOMElementCopy);
				GetInstance("facilityIdentifier cannot be used", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// At least 1 name, region, countryCode, subDivisionCode or facilityIdentifier must exist.
				GetInstance("At least 1 of ", null, null, null, null, null);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

				GeographicIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				GeographicIdentifier dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

				Element element = Util.buildDDMSElement(GeographicIdentifier.getName(version), null);
				element.appendChild(FacilityIdentifierTest.Fixture.XOMElementCopy);
				elementComponent = GetInstance(SUCCESS, element);
				dataComponent = GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

				GeographicIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				GeographicIdentifier dataComponent = GetInstance(SUCCESS, null, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, CountryCodeTest.Fixture, subCode, null);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, subCode, null);
				assertFalse(elementComponent.Equals(dataComponent));

				if (version.isAtLeast("4.0.1")) {
					dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, null, null);
					assertFalse(elementComponent.Equals(dataComponent));
				}

				dataComponent = GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

				GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLFacIdOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLFacIdOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GeographicIdentifier component = FacIdBasedFixture;
				StringBuilder facIdOutput = new StringBuilder();
				facIdOutput.Append("<meta name=\"geographicIdentifier.facilityIdentifier.beNumber\" content=\"1234DD56789\" />\n");
				facIdOutput.Append("<meta name=\"geographicIdentifier.facilityIdentifier.osuffix\" content=\"DD123\" />\n");
				assertEquals(facIdOutput.ToString(), component.toHTML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testTextFacIdOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestTextFacIdOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GeographicIdentifier component = FacIdBasedFixture;
				StringBuilder facIdOutput = new StringBuilder();
				facIdOutput.Append("geographicIdentifier.facilityIdentifier.beNumber: 1234DD56789\n");
				facIdOutput.Append("geographicIdentifier.facilityIdentifier.osuffix: DD123\n");
				assertEquals(facIdOutput.ToString(), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

				GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCountryCodeReuse() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestCountryCodeReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode code = CountryCodeTest.Fixture;
				GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, code, null, null);
				GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, code, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSubDivisionCodeReuse() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestSubDivisionCodeReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				if (!version.isAtLeast("4.0.1")) {
					continue;
				}

				SubDivisionCode code = SubDivisionCodeTest.Fixture;
				GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, code, null);
				GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, code, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testFacilityIdentifierReuse() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestFacilityIdentifierReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier facId = FacilityIdentifierTest.Fixture;
				GetInstance(SUCCESS, null, null, null, null, facId);
				GetInstance(SUCCESS, null, null, null, null, facId);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));

				// Equality after Building (CountryCode-based)
				GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder(component);
				assertEquals(component, builder.commit());

				// Equality after Building (FacID-based)
				FacilityIdentifier facId = FacilityIdentifierTest.Fixture;
				component = new GeographicIdentifier(facId);
				builder = new GeographicIdentifier.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Names = TEST_NAMES;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
				builder.FacilityIdentifier.BeNumber = "1234DD56789";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "osuffix is required.");
				}
				builder.FacilityIdentifier.Osuffix = "osuffix";
				builder.commit();

				// Non-FacID-based
				builder = new GeographicIdentifier.Builder();
				builder.Names.add("Name");
				builder.Regions.add("Region");
				CountryCode countryCode = CountryCodeTest.Fixture;
				builder.CountryCode.Qualifier = countryCode.Qualifier;
				builder.CountryCode.Value = countryCode.Value;
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
				assertNotNull(builder.Names.get(1));
				assertNotNull(builder.Regions.get(1));
			}
		}
	}

}