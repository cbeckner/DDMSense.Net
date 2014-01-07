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


	using Attribute = nu.xom.Attribute;
	using Element = nu.xom.Element;
	using ExtensibleAttributes = buri.ddmsence.ddms.extensible.ExtensibleAttributes;
	using ExtensibleAttributesTest = buri.ddmsence.ddms.extensible.ExtensibleAttributesTest;
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:keyword elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class KeywordTest : AbstractBaseTestCase {

		private const string TEST_VALUE = "Tornado";

		/// <summary>
		/// Constructor
		/// </summary>
		public KeywordTest() : base("keyword.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<Keyword> FixtureList {
			get {
				try {
					IList<Keyword> keywords = new List<Keyword>();
					keywords.Add(new Keyword("DDMSence", null));
					keywords.Add(new Keyword("Uri", null));
					return (keywords);
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
		private Keyword GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Keyword component = null;
			try {
				component = new Keyword(element);
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
		/// <param name="value"> the value child text </param>
		/// <returns> a valid object </returns>
		private Keyword GetInstance(string message, string value) {
			bool expectFailure = !Util.isEmpty(message);
			DDMSVersion version = DDMSVersion.CurrentVersion;
			Keyword component = null;
			try {
				component = new Keyword(value, version.isAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
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
			text.Append(BuildOutput(isHTML, "keyword", TEST_VALUE));
			if (version.isAtLeast("4.0.1")) {
				text.Append(BuildOutput(isHTML, "keyword.classification", "U"));
				text.Append(BuildOutput(isHTML, "keyword.ownerProducer", "USA"));
			}
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:keyword ").Append(XmlnsDDMS).Append(" ");
				if (version.isAtLeast("4.0.1")) {
					xml.Append(XmlnsISM).Append(" ");
				}
				xml.Append("ddms:value=\"").Append(TEST_VALUE).Append("\"");
				if (version.isAtLeast("4.0.1")) {
					xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
				}
				xml.Append(" />");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Keyword.getName(version));
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
				GetInstance(SUCCESS, TEST_VALUE);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// Missing value
				Element element = Util.buildDDMSElement(Keyword.getName(version), null);
				GetInstance("value attribute is required.", element);

				// Empty value
				element = Util.buildDDMSElement(Keyword.getName(version), "");
				GetInstance("value attribute is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Missing value
				GetInstance("value attribute is required.", (string) null);

				// Empty value
				GetInstance("value attribute is required.", "");
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Keyword dataComponent = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Keyword dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Keyword elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleSuccess() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestExtensibleSuccess() {
			// Extensible attribute added
			DDMSVersion.CurrentVersion = "3.0";
			ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
			new Keyword("xml", null, attr);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testExtensibleFailure() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestExtensibleFailure() {
			// Wrong DDMS Version
			DDMSVersion.CurrentVersion = "2.0";
			ExtensibleAttributes attributes = ExtensibleAttributesTest.Fixture;
			try {
				new Keyword(TEST_VALUE, null, attributes);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "xs:anyAttribute cannot be applied");
			}

			DDMSVersion version = DDMSVersion.setCurrentVersion("3.0");
			Attribute attr = new Attribute("ddms:value", version.Namespace, "dog");

			// Using ddms:value as the extension (data)
			IList<Attribute> extAttributes = new List<Attribute>();
			extAttributes.Add(attr);
			attributes = new ExtensibleAttributes(extAttributes);
			try {
				new Keyword(TEST_VALUE, null, attributes);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The extensible attribute with the name, ddms:value");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWrongVersionSecurityAttributes() {
			DDMSVersion.CurrentVersion = "3.1";
			try {
				new Keyword(TEST_VALUE, SecurityAttributesTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Security attributes cannot be applied");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Keyword component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Keyword.Builder builder = new Keyword.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Keyword.Builder builder = new Keyword.Builder();
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

				// No invalid cases that span all versions of DDMS. The only field is "value" and if that is missing, the builder is empty.
			}
		}
	}

}