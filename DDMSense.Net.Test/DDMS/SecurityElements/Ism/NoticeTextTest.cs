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
namespace DDMSense.Test.DDMS.SecurityElements.Ism {



    using DDMSense.DDMS.SecurityElements.Ism;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

	/// <summary>
	/// <para> Tests related to ISM:NoticeText elements </para>
	/// 
	/// <para> The valid instance of ISM:NoticeText is generated, rather than relying on the ISM schemas to validate an XML
	/// file. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class NoticeTextTest : AbstractBaseTestCase {

		private const string TEST_VALUE = "noticeText";
		private static readonly IList<string> TEST_POC_TYPES = Util.getXsListAsList("DoD-Dist-B");

		/// <summary>
		/// Constructor
		/// </summary>
		public NoticeTextTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static XElement FixtureElement {
			get {
				try {
					DDMSVersion version = DDMSVersion.CurrentVersion;
					string ismPrefix = PropertyReader.getPrefix("ism");
					string ismNs = version.IsmNamespace;
    
					XElement element = Util.buildElement(ismPrefix, NoticeText.getName(version), ismNs, TEST_VALUE);
					element.addNamespaceDeclaration(ismPrefix, version.IsmNamespace);
					SecurityAttributesTest.Fixture.addTo(element);
					Util.addAttribute(element, ismPrefix, "pocType", ismNs, "DoD-Dist-B");
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
		public static IList<NoticeText> FixtureList {
			get {
				try {
					IList<NoticeText> list = new List<NoticeText>();
					list.Add(new NoticeText(FixtureElement));
					return (list);
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
		private NoticeText GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			NoticeText component = null;
			try {
				component = new NoticeText(element);
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
		/// <param name="value"> the value (optional) </param>
		/// <param name="pocTypes"> the poc types (optional) </param>
		/// <returns> a valid object </returns>
		private NoticeText GetInstance(string message, string value, IList<string> pocTypes) {
			bool expectFailure = !Util.isEmpty(message);
			NoticeText component = null;
			try {
				component = new NoticeText(value, pocTypes, SecurityAttributesTest.Fixture);
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
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "noticeText", TEST_VALUE));
			text.Append(BuildOutput(isHTML, "noticeText.pocType", Util.getXsList(TEST_POC_TYPES)));
			text.Append(BuildOutput(isHTML, "noticeText.classification", "U"));
			text.Append(BuildOutput(isHTML, "noticeText.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ISM:NoticeText ").Append(XmlnsISM).Append(" ");
				xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\"");
				xml.Append(">").Append(TEST_VALUE).Append("</ISM:NoticeText>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_ISM_PREFIX, NoticeText.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ismPrefix = PropertyReader.getPrefix("ism");

				// All fields
				GetInstance(SUCCESS, FixtureElement);

				// No optional fields
				XElement element = Util.buildElement(ismPrefix, NoticeText.getName(version), version.IsmNamespace, null);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);

				// No optional fields
				GetInstance(SUCCESS, null, null);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ismPrefix = PropertyReader.getPrefix("ism");

				// Invalid pocType
				XElement element = Util.buildElement(ismPrefix, NoticeText.getName(version), version.IsmNamespace, null);
				Util.addAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "Unknown");
				GetInstance("Unknown is not a valid enumeration token", element);

				// Partial Invalid pocType
				element = Util.buildElement(ismPrefix, NoticeText.getName(version), version.IsmNamespace, null);
				Util.addAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "DoD-Dist-B Unknown");
				GetInstance("Unknown is not a valid enumeration token", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Invalid pocType
				GetInstance("Unknown is not a valid enumeration token", TEST_VALUE, Util.getXsListAsList("Unknown"));

				// Partial Invalid pocType
				GetInstance("Unknown is not a valid enumeration token", TEST_VALUE, Util.getXsListAsList("DoD-Dist-B Unknown"));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ismPrefix = PropertyReader.getPrefix("ism");

				// No warnings
				NoticeText component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(0, component.ValidationWarnings.size());

				// Empty value
				XElement element = Util.buildElement(ismPrefix, NoticeText.getName(version), version.IsmNamespace, null);
				SecurityAttributesTest.Fixture.addTo(element);
				component = GetInstance(SUCCESS, element);
				assertEquals(1, component.ValidationWarnings.size());
				string text = "An ISM:NoticeText element was found with no value.";
				string locator = "ISM:NoticeText";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText elementComponent = GetInstance(SUCCESS, FixtureElement);
				NoticeText dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);

				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText elementComponent = GetInstance(SUCCESS, FixtureElement);
				NoticeText dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_POC_TYPES);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, null);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new NoticeText(TEST_VALUE, TEST_POC_TYPES, null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The NoticeText element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText component = GetInstance(SUCCESS, FixtureElement);
				NoticeText.Builder builder = new NoticeText.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText.Builder builder = new NoticeText.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Value = TEST_VALUE;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				NoticeText.Builder builder = new NoticeText.Builder();
				builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				try {
					builder.commit();
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
				builder.SecurityAttributes.Classification = "U";
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				NoticeText.Builder builder = new NoticeText.Builder();
				assertNotNull(builder.PocTypes.get(1));
			}
		}
	}

}