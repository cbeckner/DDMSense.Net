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
namespace buri.ddmsence.ddms.security.ntk {

	using Element = nu.xom.Element;
	using SecurityAttributesTest = buri.ddmsence.ddms.security.ism.SecurityAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ntk:AccessSystemName elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class SystemNameTest : AbstractBaseTestCase {

		private const string TEST_VALUE = "DIAS";
		private new const string TEST_ID = "ID";
		private const string TEST_ID_REFERENCE = "ID";
		private const string TEST_QUALIFIER = "qualifier";

		/// <summary>
		/// Constructor
		/// </summary>
		public SystemNameTest() : base("accessSystemName.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static SystemName Fixture {
			get {
				try {
					return (new SystemName(TEST_VALUE, null, null, null, SecurityAttributesTest.Fixture));
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
		private SystemName GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			SystemName component = null;
			try {
				component = new SystemName(element);
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
		/// <param name="id"> the NTK ID (optional) </param>
		/// <param name="idReference"> a reference to an NTK ID (optional) </param>
		/// <param name="qualifier"> an NTK qualifier (optional) </param>
		/// <returns> a valid object </returns>
		private SystemName GetInstance(string message, string value, string id, string idReference, string qualifier) {
			bool expectFailure = !Util.isEmpty(message);
			SystemName component = null;
			try {
				component = new SystemName(value, id, idReference, qualifier, SecurityAttributesTest.Fixture);
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
			text.Append(BuildOutput(isHTML, "systemName", TEST_VALUE));
			text.Append(BuildOutput(isHTML, "systemName.id", TEST_ID));
			text.Append(BuildOutput(isHTML, "systemName.idReference", TEST_ID_REFERENCE));
			text.Append(BuildOutput(isHTML, "systemName.qualifier", TEST_QUALIFIER));
			text.Append(BuildOutput(isHTML, "systemName.classification", "U"));
			text.Append(BuildOutput(isHTML, "systemName.ownerProducer", "USA"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ntk:AccessSystemName ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM);
				xml.Append(" ntk:id=\"ID\" ntk:IDReference=\"ID\" ntk:qualifier=\"qualifier\"");
				xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
				xml.Append(TEST_VALUE).Append("</ntk:AccessSystemName>");
				return (xml.ToString());
			}
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, SystemName.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ntkPrefix = PropertyReader.getPrefix("ntk");

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildElement(ntkPrefix, SystemName.getName(version), version.NtkNamespace, TEST_VALUE);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);

				// No optional fields
				GetInstance(SUCCESS, TEST_VALUE, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ntkPrefix = PropertyReader.getPrefix("ntk");

				// Missing value
				Element element = Util.buildElement(ntkPrefix, SystemName.getName(version), version.NtkNamespace, null);
				SecurityAttributesTest.Fixture.addTo(element);
				GetInstance("\"\" is not a valid NMTOKEN.", element);

				// Missing security attributes
				element = Util.buildElement(ntkPrefix, SystemName.getName(version), version.NtkNamespace, TEST_VALUE);
				GetInstance("classification is required.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing value
				GetInstance("\"\" is not a valid NMTOKEN.", null, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);

				// Missing security attributes
				try {
					new SystemName(TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER, null);
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
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				SystemName component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				SystemName dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				SystemName dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, DIFFERENT_VALUE, TEST_QUALIFIER);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_ID, TEST_ID_REFERENCE, TEST_QUALIFIER);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			// Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName component = GetInstance(SUCCESS, GetValidElement(sVersion));
				SystemName.Builder builder = new SystemName.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SystemName.Builder builder = new SystemName.Builder();
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

				SystemName.Builder builder = new SystemName.Builder();
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