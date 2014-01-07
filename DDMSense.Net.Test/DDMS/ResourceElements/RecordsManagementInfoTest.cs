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
    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

	/// <summary>
	/// <para> Tests related to ddms:recordsManagementInfo elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class RecordsManagementInfoTest : AbstractBaseTestCase {

		private static readonly bool? TEST_VITAL = true;

		/// <summary>
		/// Constructor
		/// </summary>
		public RecordsManagementInfoTest() : base("recordsManagementInfo.xml") {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static RecordsManagementInfo Fixture {
			get {
				try {
					return (new RecordsManagementInfo(RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL));
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
		private RecordsManagementInfo GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			RecordsManagementInfo component = null;
			try {
				component = new RecordsManagementInfo(element);
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
		/// <param name="keeper"> the record keeper (optional) </param>
		/// <param name="software"> the software (optional) </param>
		/// <param name="vitalRecord"> whether this is a vital record (optional, defaults to false) </param>
		/// <param name="org"> the organization </param>
		private RecordsManagementInfo GetInstance(string message, RecordKeeper keeper, ApplicationSoftware software, bool? vitalRecord) {
			bool expectFailure = !Util.isEmpty(message);
			RecordsManagementInfo component = null;
			try {
				component = new RecordsManagementInfo(keeper, software, vitalRecord);
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
			text.Append(RecordKeeperTest.Fixture.getOutput(isHTML, "recordsManagementInfo.", ""));
			text.Append(ApplicationSoftwareTest.Fixture.getOutput(isHTML, "recordsManagementInfo.", ""));
			text.Append(BuildOutput(isHTML, "recordsManagementInfo.vitalRecordIndicator", "true"));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:recordsManagementInfo ").Append(XmlnsDDMS);
			xml.Append(" ddms:vitalRecordIndicator=\"true\">\n");
			xml.Append("\t<ddms:recordKeeper>\n");
			xml.Append("\t\t<ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>\n");
			xml.Append("\t\t<ddms:organization>\n");
			xml.Append("\t\t\t<ddms:name>DISA</ddms:name>\n");
			xml.Append("\t\t</ddms:organization>\n");
			xml.Append("\t</ddms:recordKeeper>\n");
			xml.Append("\t<ddms:applicationSoftware ").Append(XmlnsISM);
			xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">IRM Generator 2L-9</ddms:applicationSoftware>\n");
			xml.Append("</ddms:recordsManagementInfo>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RecordsManagementInfo.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				XElement element = Util.buildDDMSElement(RecordsManagementInfo.getName(version), null);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);

				// No optional fields
				GetInstance(SUCCESS, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No invalid cases.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No invalid cases.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				RecordsManagementInfo dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				RecordsManagementInfo dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, null);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, null, TEST_VITAL);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, null, ApplicationSoftwareTest.Fixture, TEST_VITAL);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				new RecordsManagementInfo(null, null, TEST_VITAL);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The recordsManagementInfo element cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
				RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.VitalRecordIndicator = TEST_VITAL;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder();
				builder.ApplicationSoftware.Value = "value";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}
				builder.ApplicationSoftware.SecurityAttributes.Classification = "U";
				builder.ApplicationSoftware.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
				builder.commit();
			}
		}
	}

}