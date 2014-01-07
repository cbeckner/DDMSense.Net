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
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:language elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class LanguageTest : AbstractBaseTestCase {

		private const string TEST_QUALIFIER = "http://purl.org/dc/elements/1.1/language";
		private const string TEST_VALUE = "en";

		/// <summary>
		/// Constructor
		/// </summary>
		public LanguageTest() : base("language.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Language Fixture {
			get {
				try {
					return (new Language("http://purl.org/dc/elements/1.1/language", "en"));
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
		private Language GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Language component = null;
			try {
				component = new Language(element);
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
		private Language GetInstance(string message, string qualifier, string value) {
			bool expectFailure = !Util.isEmpty(message);
			Language component = null;
			try {
				component = new Language(qualifier, value);
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
			text.Append(BuildOutput(isHTML, "language.qualifier", TEST_QUALIFIER));
			text.Append(BuildOutput(isHTML, "language.value", TEST_VALUE));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:language ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Language.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildDDMSElement(Language.getName(version), null);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);

				// No optional fields
				GetInstance(SUCCESS, "", "");
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// Missing qualifier
				Element element = Util.buildDDMSElement(Language.getName(version), null);
				Util.addDDMSAttribute(element, "value", TEST_VALUE);
				GetInstance("qualifier attribute is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Missing qualifier
				GetInstance("qualifier attribute is required.", null, TEST_VALUE);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// No warnings
				Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());

				// Qualifier without value
				Element element = Util.buildDDMSElement(Language.getName(version), null);
				Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				string text = "A qualifier has been set without an accompanying value attribute.";
				string locator = "ddms:language";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

				// Neither attribute
				element = Util.buildDDMSElement(Language.getName(version), null);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				text = "Neither a qualifier nor a value was set on this language.";
				locator = "ddms:language";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Language elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Language dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Language elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Language dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
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
				Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Language component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Language.Builder builder = new Language.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Language.Builder builder = new Language.Builder();
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

				Language.Builder builder = new Language.Builder();
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