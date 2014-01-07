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
namespace DDMSense.Test.DDMS.ResourceElements {


    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

	/// <summary>
	/// <para> Tests related to ddms:recordKeeper elements </para>
	/// 
	/// <para> Because a ddms:recordKeeper is a local component, we cannot load a valid document from a unit test data file. We
	/// have to build the well-formed XElement ourselves. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class RecordKeeperTest : AbstractBaseTestCase {

		private new const string TEST_ID = "#289-99202.9";
		private const string TEST_NAME = "DISA";

		/// <summary>
		/// Constructor
		/// </summary>
		public RecordKeeperTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static XElement FixtureElement {
			get {
				DDMSVersion version = DDMSVersion.CurrentVersion;
				XElement element = Util.buildDDMSElement(RecordKeeper.getName(version), null);
				element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
				XElement idElement = Util.buildDDMSElement("recordKeeperID", TEST_ID);
				element.appendChild(idElement);
				element.appendChild(OrganizationTest.Fixture.XOMElementCopy);
				return (element);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static RecordKeeper Fixture {
			get {
				try {
					return (new RecordKeeper(FixtureElement));
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
		private RecordKeeper GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			RecordKeeper component = null;
			try {
				component = new RecordKeeper(element);
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
		/// <param name="recordKeeperID"> ID value </param>
		/// <param name="org"> the organization </param>
		private RecordKeeper GetInstance(string message, string recordKeeperID, Organization org) {
			bool expectFailure = !Util.isEmpty(message);
			RecordKeeper component = null;
			try {
				component = new RecordKeeper(recordKeeperID, org);
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
			text.Append(BuildOutput(isHTML, "recordKeeper.recordKeeperID", TEST_ID));
			text.Append(BuildOutput(isHTML, "recordKeeper.entityType", "organization"));
			text.Append(BuildOutput(isHTML, "recordKeeper.name", TEST_NAME));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:recordKeeper ").Append(XmlnsDDMS).Append(">");
				xml.Append("<ddms:recordKeeperID>").Append(TEST_ID).Append("</ddms:recordKeeperID>");
				xml.Append("<ddms:organization>");
				xml.Append("<ddms:name>").Append(TEST_NAME).Append("</ddms:name>");
				xml.Append("</ddms:organization>");
				xml.Append("</ddms:recordKeeper>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, RecordKeeper.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, FixtureElement);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Missing recordKeeperID
				XElement element = Util.buildDDMSElement(RecordKeeper.getName(version), null);
				element.appendChild(OrganizationTest.Fixture.XOMElementCopy);
				GetInstance("record keeper ID is required.", element);

				// Empty recordKeeperID
				element = Util.buildDDMSElement(RecordKeeper.getName(version), null);
				element.appendChild(Util.buildDDMSElement("recordKeeperID", null));
				element.appendChild(OrganizationTest.Fixture.XOMElementCopy);
				GetInstance("record keeper ID is required.", element);

				// Missing organization
				element = Util.buildDDMSElement(RecordKeeper.getName(version), null);
				element.appendChild(Util.buildDDMSElement("recordKeeperID", TEST_ID));
				GetInstance("organization is required.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Missing recordKeeperID
				GetInstance("record keeper ID is required.", null, OrganizationTest.Fixture);

				// Missing organization
				GetInstance("organization is required.", TEST_ID, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper elementComponent = GetInstance(SUCCESS, FixtureElement);
				RecordKeeper dataComponent = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper elementComponent = GetInstance(SUCCESS, FixtureElement);
				RecordKeeper dataComponent = GetInstance(SUCCESS, "newID", OrganizationTest.Fixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_ID, new Organization(Util.getXsListAsList("DARPA"), null, null, null, null));
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new RecordKeeper(TEST_ID, OrganizationTest.Fixture);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The recordKeeper element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
				RecordKeeper.Builder builder = new RecordKeeper.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper.Builder builder = new RecordKeeper.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.RecordKeeperID = TEST_ID;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordKeeper.Builder builder = new RecordKeeper.Builder();
				builder.RecordKeeperID = TEST_ID;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "organization is required.");
				}
				builder.Organization.Names = Util.getXsListAsList(TEST_NAME);
				builder.commit();
			}
		}
	}

}