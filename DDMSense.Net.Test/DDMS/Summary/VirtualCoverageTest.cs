//using System.Text;

///* Copyright 2010 - 2013 by Brian Uri!
   
//   This file is part of DDMSence.
   
//   This library is free software; you can redistribute it and/or modify
//   it under the terms of version 3.0 of the GNU Lesser General Public 
//   License as published by the Free Software Foundation.
   
//   This library is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
//   GNU Lesser General Public License for more details.
   
//   You should have received a copy of the GNU Lesser General Public 
//   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

//   You can contact the author at ddmsence@urizone.net. The DDMSence
//   home page is located at http://ddmsence.urizone.net/
// */
//namespace DDMSense.Test.DDMS.Summary {

	
//    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:virtualCoverage elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class VirtualCoverageTest : AbstractBaseTestCase {

//        private const string TEST_ADDRESS = "123.456.789.0";
//        private const string TEST_PROTOCOL = "IP";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public VirtualCoverageTest() : base("virtualCoverage.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static VirtualCoverage Fixture {
//            get {
//                try {
//                    return (new VirtualCoverage("123.456.789.0", "IP", null));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="element"> the element to build from </param>
//        /// <returns> a valid object </returns>
//        private VirtualCoverage GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            VirtualCoverage component = null;
//            try {
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    SecurityAttributesTest.Fixture.addTo(element);
//                }
//                component = new VirtualCoverage(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="address"> the virtual address (optional) </param>
//        /// <param name="protocol"> the network protocol (optional, should be used if address is provided) </param>
//        /// <returns> a valid object </returns>
//        private VirtualCoverage GetInstance(string message, string address, string protocol) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            VirtualCoverage component = null;
//            try {
//                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.isAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
//                component = new VirtualCoverage(address, protocol, attr);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, "virtualCoverage.address", TEST_ADDRESS));
//            text.Append(BuildOutput(isHTML, "virtualCoverage.protocol", TEST_PROTOCOL));
//            if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                text.Append(BuildOutput(isHTML, "virtualCoverage.classification", "U"));
//                text.Append(BuildOutput(isHTML, "virtualCoverage.ownerProducer", "USA"));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:virtualCoverage ").Append(XmlnsDDMS);
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    xml.Append(" ").Append(XmlnsISM);
//                }
//                xml.Append(" ddms:address=\"").Append(TEST_ADDRESS).Append("\" ddms:protocol=\"").Append(TEST_PROTOCOL).Append("\"");
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
//                }
//                xml.Append(" />");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, VirtualCoverage.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(VirtualCoverage.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);

//                // No optional fields
//                GetInstance(SUCCESS, null, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // address without protocol
//                XElement element = Util.buildDDMSElement(VirtualCoverage.getName(version), null);
//                Util.addDDMSAttribute(element, "address", TEST_ADDRESS);
//                GetInstance("protocol is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // address without protocol
//                GetInstance("protocol is required.", TEST_ADDRESS, null);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Empty element
//                XElement element = Util.buildDDMSElement(VirtualCoverage.getName(version), null);
//                component = GetInstance(SUCCESS, element);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A completely empty ddms:virtualCoverage element was found.";
//                string locator = "ddms:virtualCoverage";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VirtualCoverage dataComponent = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VirtualCoverage dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_PROTOCOL);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_ADDRESS, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                VirtualCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_ADDRESS, TEST_PROTOCOL);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestSecurityAttributes() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                SecurityAttributes attr = (!version.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
//                VirtualCoverage component = new VirtualCoverage(TEST_ADDRESS, TEST_PROTOCOL, attr);
//                if (!version.isAtLeast("3.0")) {
//                    assertTrue(component.SecurityAttributes.Empty);
//                } else {
//                    assertEquals(attr, component.SecurityAttributes);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionSecurityAttributes() {
//            DDMSVersion.CurrentVersion = "2.0";
//            try {
//                new VirtualCoverage(TEST_ADDRESS, TEST_PROTOCOL, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Security attributes cannot be applied");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                VirtualCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VirtualCoverage.Builder builder = new VirtualCoverage.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                VirtualCoverage.Builder builder = new VirtualCoverage.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Address = TEST_ADDRESS;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                VirtualCoverage.Builder builder = new VirtualCoverage.Builder();
//                builder.Address = TEST_ADDRESS;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "protocol is required.");
//                }
//                builder.Protocol = TEST_PROTOCOL;
//                builder.commit();
//            }
//        }
//    }

//}