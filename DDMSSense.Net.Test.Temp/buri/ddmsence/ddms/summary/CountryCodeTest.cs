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
	/// <para> Tests related to ddms:countryCode elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class CountryCodeTest : AbstractBaseTestCase {

		private const string TEST_QUALIFIER = "ISO-3166";
		private const string TEST_VALUE = "USA";

		/// <summary>
		/// Constructor
		/// </summary>
		public CountryCodeTest() : base("countryCode.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static CountryCode Fixture {
			get {
				try {
					return (new CountryCode("ISO-3166", "USA"));
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
		private CountryCode GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			CountryCode component = null;
			try {
				component = new CountryCode(element);
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
		/// <param name="qualifier"> the qualifier value </param>
		/// <param name="value"> the value </param>
		/// <returns> a valid object </returns>
		private CountryCode GetInstance(string message, string qualifier, string value) {
			bool expectFailure = !Util.isEmpty(message);
			CountryCode component = null;
			try {
				component = new CountryCode(qualifier, value);
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
			text.Append(BuildOutput(isHTML, "countryCode.qualifier", TEST_QUALIFIER));
			text.Append(BuildOutput(isHTML, "countryCode.value", TEST_VALUE));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:countryCode ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, CountryCode.getName(version));
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
				GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string countryCodeName = CountryCode.getName(version);
				// Missing qualifier
				Element element = Util.buildDDMSElement(countryCodeName, null);
				Util.addDDMSAttribute(element, "value", TEST_VALUE);
				GetInstance("qualifier attribute is required.", element);

				// Empty qualifier
				element = Util.buildDDMSElement(countryCodeName, null);
				Util.addDDMSAttribute(element, "qualifier", "");
				Util.addDDMSAttribute(element, "value", TEST_VALUE);
				GetInstance("qualifier attribute is required.", element);

				// Missing value
				element = Util.buildDDMSElement(countryCodeName, null);
				Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
				GetInstance("value attribute is required.", element);

				// Empty value
				element = Util.buildDDMSElement(countryCodeName, null);
				Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
				Util.addDDMSAttribute(element, "value", "");
				GetInstance("value attribute is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Missing qualifier
				GetInstance("qualifier attribute is required.", null, TEST_VALUE);

				// Empty qualifier
				GetInstance("qualifier attribute is required.", "", TEST_VALUE);

				// Missing value
				GetInstance("value attribute is required.", TEST_QUALIFIER, null);

				// Empty value
				GetInstance("value attribute is required.", TEST_QUALIFIER, "");
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				CountryCode dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				CountryCode dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
				CountryCode.Builder builder = new CountryCode.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				CountryCode.Builder builder = new CountryCode.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Value = TEST_VALUE;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				CountryCode.Builder builder = new CountryCode.Builder();
				builder.Value = TEST_VALUE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "qualifier attribute is required.");
				}
				builder.Qualifier = TEST_QUALIFIER;
				builder.commit();
			}
		}
	}

}