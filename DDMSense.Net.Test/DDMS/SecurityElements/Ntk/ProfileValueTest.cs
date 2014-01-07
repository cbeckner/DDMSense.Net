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
namespace DDMSense.Test.DDMS.SecurityElements.Ntk {


	
	using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using PropertyReader = DDMSense.Util.PropertyReader;
	using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using System.Xml.Linq;

	/// <summary>
	/// <para> Tests related to ntk:AccessProfileValue elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class ProfileValueTest : AbstractBaseTestCase {

		private const string TEST_VALUE = "profile";
		private const string TEST_VOCABULARY = "vocabulary";
		private new const string TEST_ID = "ID";
		private const string TEST_ID_REFERENCE = "ID";
		private const string TEST_QUALIFIER = "qualifier";

		/// <summary>
		/// Constructor
		/// </summary>
		public ProfileValueTest() : base("accessProfileValue.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		/// <param name="value"> the value for this component </param>
		public static ProfileValue GetFixture(string value) {
			try {
				return (new ProfileValue(value, TEST_VOCABULARY, null, null, null, SecurityAttributesTest.Fixture));
			} catch (InvalidDDMSException e) {
				fail("Could not create fixture: " + e.Message);
			}
			return (null);
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<ProfileValue> FixtureList {
			get {
				IList<ProfileValue> list = new List<ProfileValue>();
				list.Add(ProfileValueTest.GetFixture("profile"));
				return (list);
			}
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private ProfileValue GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			ProfileValue component = null;
			try {
				component = new ProfileValue(element);
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
		/// <param name="value"> the value of the element's child text </param>
		/// <param name="vocabulary"> the vocabulary (required) </param>
		/// <param name="id"> the NTK ID (optional) </param>
		/// <param name="idReference"> a reference to an NTK ID (optional) </param>
		/// <param name="qualifier"> an NTK qualifier (optional) </param>
		/// <returns> a valid object </returns>
		private ProfileValue GetInstance(string message, string value, string vocabulary, string id, string idReference, string qualifier) {
			bool expectFailure = !Util.isEmpty(message);
			ProfileValue component = null;
			try {
				component = new ProfileValue(value, vocabulary, id, idReference, qualifier, SecurityAttributesTest.Fixture);
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
			text.Append(BuildOutput(isHTML, "profileValue", TEST_VALUE));
			text.Append(BuildOutput(isHTML, "profileValue.vocabulary", TEST_VOCABULARY));
			text.Append(BuildOutput(isHTML, "profileValue.id", TEST_ID));
			text.Append(BuildOutput(isHTML, "profileValue.idReference", TEST_ID_REFERENCE));
			text.Append(BuildOutput(isHTML, "profileValue.qualifier", TEST_QUALIFIER));
			text.Append(BuildOutput(isHTML, "profileValue.classification", "U"));
			text.Append(BuildOutput(isHTML, "profileValue.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ntk:AccessProfileValue ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM);
				xml.Append(" ntk:id=\"ID\" ntk:IDReference=\"ID\" ntk:qualifier=\"qualifier\"");
				xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">");
				xml.Append(TEST_VALUE).Append("</ntk:AccessProfileValue>");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, ProfileValue.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ntkPrefix = PropertyReader.getPrefix("ntk");

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				XElement element = Util.buildElement(ntkPrefix, ProfileValue.getName(version), version.NtkNamespace, TEST_VALUE);
				Util.addAttribute(element, ntkPrefix, "vocabulary", version.NtkNamespace, TEST_VOCABULARY);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);

				// No optional fields
				GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ntkPrefix = PropertyReader.getPrefix("ntk");

				// Missing vocabulary
				XElement element = Util.buildElement(ntkPrefix, ProfileValue.getName(version), version.NtkNamespace, TEST_VALUE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("\"\" is not a valid NMTOKEN.", element);

				// Missing security attributes
				element = Util.buildElement(ntkPrefix, ProfileValue.getName(version), version.NtkNamespace, TEST_VALUE);
				Util.addAttribute(element, ntkPrefix, "vocabulary", version.NtkNamespace, TEST_VOCABULARY);
				GetInstance("classification is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing vocabulary
				GetInstance("\"\" is not a valid NMTOKEN.", TEST_VALUE, null, null, null, null);
				// Missing security attributes
				try {
					new ProfileValue(TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER, null);
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());

				// No value
				component = GetInstance(SUCCESS, null, TEST_VOCABULARY, null, null, null);
				assertEquals(1, component.ValidationWarnings.size());
				string text = "A ntk:AccessProfileValue element was found with no value.";
				string locator = "ntk:AccessProfileValue";
				AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProfileValue dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProfileValue dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, DIFFERENT_VALUE, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, DIFFERENT_VALUE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_VOCABULARY, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			// Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue component = GetInstance(SUCCESS, GetValidElement(sVersion));
				ProfileValue.Builder builder = new ProfileValue.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				ProfileValue.Builder builder = new ProfileValue.Builder();
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

				ProfileValue.Builder builder = new ProfileValue.Builder();
				builder.Value = TEST_VALUE;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "\"\" is not a valid NMTOKEN.");
				}
				builder.Vocabulary = "test";
				builder.SecurityAttributes.Classification = "U";
				builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.commit();
			}
		}
	}

}