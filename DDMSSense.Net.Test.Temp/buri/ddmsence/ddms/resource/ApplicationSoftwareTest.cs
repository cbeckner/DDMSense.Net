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
namespace buri.ddmsence.ddms.resource {

	using Element = nu.xom.Element;
	using SecurityAttributes = buri.ddmsence.ddms.security.ism.SecurityAttributes;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:applicationSoftware elements </para>
	/// 
	/// <para> Because a ddms:applicationSoftware is a local component, we cannot load a valid document from a unit test data
	/// file. We have to build the well-formed Element ourselves. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class ApplicationSoftwareTest : AbstractBaseTestCase {

		private const string TEST_VALUE = "IRM Generator 2L-9";

		/// <summary>
		/// Constructor
		/// </summary>
		public ApplicationSoftwareTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Element FixtureElement {
			get {
				try {
					DDMSVersion version = DDMSVersion.CurrentVersion;
					Element element = Util.buildDDMSElement(ApplicationSoftware.getName(version), TEST_VALUE);
					element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
					element.addNamespaceDeclaration(PropertyReader.getPrefix("ism"), version.IsmNamespace);
					SecurityAttributesTest.Fixture.addTo(element);
					return (element);
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static ApplicationSoftware Fixture {
			get {
				try {
					return (new ApplicationSoftware(FixtureElement));
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
		private ApplicationSoftware GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			ApplicationSoftware component = null;
			try {
				component = new ApplicationSoftware(element);
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
		/// <param name="value"> the child text </param>
		/// <returns> a valid object </returns>
		private ApplicationSoftware GetInstance(string message, string value) {
			bool expectFailure = !Util.isEmpty(message);
			ApplicationSoftware component = null;
			try {
				component = new ApplicationSoftware(value, SecurityAttributesTest.Fixture);
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
			text.Append(BuildOutput(isHTML, "applicationSoftware", TEST_VALUE));
			text.Append(BuildOutput(isHTML, "applicationSoftware.classification", "U"));
			text.Append(BuildOutput(isHTML, "applicationSoftware.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:applicationSoftware ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append(TEST_VALUE).Append("</ddms:applicationSoftware>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, ApplicationSoftware.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// All fields
				GetInstance(SUCCESS, FixtureElement);

				// No optional fields
				Element element = Util.buildDDMSElement(ApplicationSoftware.getName(version), null);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_VALUE);

				// No optional fields
				GetInstance(SUCCESS, "");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Bad security attributes
				Element element = Util.buildDDMSElement(ApplicationSoftware.getName(version), null);
				GetInstance("classification is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Bad security attributes
				try {
					new ApplicationSoftware(TEST_VALUE, (SecurityAttributes) null);
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// No warnings
				ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(0, component.ValidationWarnings.size());

				// No value
				Element element = Util.buildDDMSElement(ApplicationSoftware.getName(version), null);
				SecurityAttributesTest.Fixture.addTo(element);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				string text = "A ddms:applicationSoftware element was found with no value.";
				string locator = "ddms:applicationSoftware";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware elementComponent = GetInstance(SUCCESS, FixtureElement);
				ApplicationSoftware dataComponent = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware elementComponent = GetInstance(SUCCESS, FixtureElement);
				ApplicationSoftware dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new ApplicationSoftware(TEST_VALUE, SecurityAttributesTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The applicationSoftware element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
				ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder();
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

				ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder();
				builder.Value = TEST_VALUE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
				builder.SecurityAttributes.Classification = "U";
				builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.commit();
			}
		}
	}

}