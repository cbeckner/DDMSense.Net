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
	using XLinkAttributes = buri.ddmsence.ddms.summary.xlink.XLinkAttributes;
	using XLinkAttributesTest = buri.ddmsence.ddms.summary.xlink.XLinkAttributesTest;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to ddms:taskID elements </para>
	/// 
	/// <para> Because a ddms:taskID is a local component, we cannot load a valid document from a unit test data file. We have
	/// to build the well-formed Element ourselves. </para>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class TaskIDTest : AbstractBaseTestCase {

		private const string TEST_TASKING_SYSTEM = "MDR";
		private const string TEST_VALUE = "Task #12345";
		private const string TEST_NETWORK = "NIPRNet";
		private const string TEST_OTHER_NETWORK = "PBS";

		/// <summary>
		/// Constructor
		/// </summary>
		public TaskIDTest() : base(null) {
			RemoveSupportedVersions("2.0 3.0 3.1");
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static Element FixtureElement {
			get {
				try {
					DDMSVersion version = DDMSVersion.CurrentVersion;
    
					Element element = Util.buildDDMSElement(TaskID.getName(version), TEST_VALUE);
					element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
					element.addNamespaceDeclaration(PropertyReader.getPrefix("xlink"), version.XlinkNamespace);
					Util.addDDMSAttribute(element, "taskingSystem", TEST_TASKING_SYSTEM);
					Util.addAttribute(element, "", "network", "", TEST_NETWORK);
					Util.addAttribute(element, "", "otherNetwork", "", TEST_OTHER_NETWORK);
					XLinkAttributesTest.SimpleFixture.addTo(element);
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
		public static TaskID Fixture {
			get {
				try {
					return (new TaskID(TaskIDTest.FixtureElement));
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
		private TaskID GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			TaskID component = null;
			try {
				component = new TaskID(element);
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
		/// <param name="value"> the child text (optional) link href (required) </param>
		/// <param name="taskingSystem"> the tasking system (optional) </param>
		/// <param name="network"> the network (optional) </param>
		/// <param name="otherNetwork"> another network (optional) </param>
		/// <param name="attributes"> the xlink attributes (optional) </param>
		private TaskID GetInstance(string message, string value, string taskingSystem, string network, string otherNetwork, XLinkAttributes attributes) {
			bool expectFailure = !Util.isEmpty(message);
			TaskID component = null;
			try {
				component = new TaskID(value, taskingSystem, network, otherNetwork, attributes);
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
			text.Append(BuildOutput(isHTML, "taskID", TEST_VALUE));
			text.Append(BuildOutput(isHTML, "taskID.taskingSystem", TEST_TASKING_SYSTEM));
			text.Append(BuildOutput(isHTML, "taskID.network", TEST_NETWORK));
			text.Append(BuildOutput(isHTML, "taskID.otherNetwork", TEST_OTHER_NETWORK));
			text.Append(XLinkAttributesTest.SimpleFixture.getOutput(isHTML, "taskID."));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		private string ExpectedXMLOutput {
			get {
				StringBuilder xml = new StringBuilder();
				xml.Append("<ddms:taskID ").Append(XmlnsDDMS).Append(" ");
				xml.Append("xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:taskingSystem=\"MDR\" ");
				xml.Append("network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
				xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" ");
				xml.Append("xlink:arcrole=\"arcrole\" xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
				return (xml.ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, TaskID.getName(version));
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
				Element element = Util.buildDDMSElement(TaskID.getName(version), TEST_VALUE);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);

				// No optional fields
				GetInstance(SUCCESS, TEST_VALUE, null, null, null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Wrong type of XLinkAttributes (locator)
				Element element = Util.buildDDMSElement(TaskID.getName(version), TEST_VALUE);
				XLinkAttributesTest.LocatorFixture.addTo(element);
				GetInstance("The type attribute must have a fixed value", element);

				// Missing value
				element = Util.buildDDMSElement(TaskID.getName(version), null);
				GetInstance("value is required.", element);

				// Bad network
				element = Util.buildDDMSElement(TaskID.getName(version), TEST_VALUE);
				Util.addAttribute(element, "", "network", "", "PBS");
				GetInstance("The network attribute must be one of", element);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// Wrong type of XLinkAttributes
				GetInstance("The type attribute must have a fixed value", TEST_VALUE, null, null, null, XLinkAttributesTest.LocatorFixture);

				// Missing value
				GetInstance("value is required.", null, null, null, null, null);

				// Bad network
				GetInstance("The network attribute must be one of", TEST_VALUE, null, "PBS", null, null);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// No warnings
				TaskID component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
				TaskID dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
				TaskID dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, DIFFERENT_VALUE, XLinkAttributesTest.SimpleFixture);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, null);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID component = GetInstance(SUCCESS, FixtureElement);
				assertEquals(ExpectedXMLOutput, component.toXML());

				component = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
				assertEquals(ExpectedXMLOutput, component.toXML());
			}
		}

		public virtual void TestWrongVersion() {
			try {
				DDMSVersion.CurrentVersion = "4.0.1";
				XLinkAttributes attr = XLinkAttributesTest.SimpleFixture;
				DDMSVersion.CurrentVersion = "2.0";
				new TaskID(TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, attr);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "These attributes cannot decorate");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID component = GetInstance(SUCCESS, FixtureElement);
				TaskID.Builder builder = new TaskID.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				TaskID.Builder builder = new TaskID.Builder();
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

				TaskID.Builder builder = new TaskID.Builder();
				builder.TaskingSystem = TEST_TASKING_SYSTEM;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "value is required.");
				}
				builder.Value = TEST_VALUE;
				builder.commit();
			}
		}
	}

}