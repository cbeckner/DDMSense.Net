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
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:facilityIdentifier elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class FacilityIdentifierTest : AbstractBaseTestCase {
		private const string TEST_BENUMBER = "1234DD56789";
		private const string TEST_OSUFFIX = "DD123";

		/// <summary>
		/// Constructor
		/// </summary>
		public FacilityIdentifierTest() : base("facilityIdentifier.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static FacilityIdentifier Fixture {
			get {
				try {
					return (new FacilityIdentifier("1234DD56789", "DD123"));
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
		private FacilityIdentifier GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			FacilityIdentifier component = null;
			try {
				component = new FacilityIdentifier(element);
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
		/// <param name="beNumber"> the beNumber (required) </param>
		/// <param name="osuffix"> the Osuffix (required, because beNumber is required) </param>
		/// <returns> a valid object </returns>
		private FacilityIdentifier GetInstance(string message, string beNumber, string osuffix) {
			bool expectFailure = !Util.isEmpty(message);
			FacilityIdentifier component = null;
			try {
				component = new FacilityIdentifier(beNumber, osuffix);
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
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "facilityIdentifier.beNumber", TEST_BENUMBER));
			text.Append(BuildOutput(isHTML, "facilityIdentifier.osuffix", TEST_OSUFFIX));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:facilityIdentifier ").Append(XmlnsDDMS).Append(" ");
			xml.Append("ddms:beNumber=\"").Append(TEST_BENUMBER).Append("\" ");
			xml.Append("ddms:osuffix=\"").Append(TEST_OSUFFIX).Append("\" />");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, FacilityIdentifier.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GetInstance(SUCCESS, GetValidElement(sVersion));
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// Missing beNumber
				Element element = Util.buildDDMSElement(FacilityIdentifier.getName(version), null);
				Util.addDDMSAttribute(element, "osuffix", TEST_OSUFFIX);
				GetInstance("beNumber is required.", element);

				// Empty beNumber
				element = Util.buildDDMSElement(FacilityIdentifier.getName(version), null);
				Util.addDDMSAttribute(element, "beNumber", "");
				Util.addDDMSAttribute(element, "osuffix", TEST_OSUFFIX);
				GetInstance("beNumber is required.", element);

				// Missing osuffix
				element = Util.buildDDMSElement(FacilityIdentifier.getName(version), null);
				Util.addDDMSAttribute(element, "beNumber", TEST_BENUMBER);
				GetInstance("osuffix is required.", element);

				// Empty osuffix
				element = Util.buildDDMSElement(FacilityIdentifier.getName(version), null);
				Util.addDDMSAttribute(element, "beNumber", TEST_BENUMBER);
				Util.addDDMSAttribute(element, "osuffix", "");
				GetInstance("osuffix is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Missing beNumber
				GetInstance("beNumber is required.", null, TEST_OSUFFIX);

				// Empty beNumber
				GetInstance("beNumber is required.", "", TEST_OSUFFIX);

				// Missing osuffix
				GetInstance("osuffix is required.", TEST_BENUMBER, null);

				// Empty osuffix
				GetInstance("osuffix is required.", TEST_BENUMBER, "");
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				FacilityIdentifier dataComponent = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				FacilityIdentifier dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_OSUFFIX);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_BENUMBER, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
				FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.BeNumber = TEST_BENUMBER;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder();
				builder.BeNumber = TEST_BENUMBER;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "osuffix is required.");
				}
				builder.Osuffix = TEST_OSUFFIX;
				builder.commit();
			}
		}
	}

}