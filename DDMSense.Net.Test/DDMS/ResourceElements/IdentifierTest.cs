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
//namespace DDMSense.Test.DDMS.ResourceElements {


//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to ddms:Identifier elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class IdentifierTest : AbstractBaseTestCase {

//        private const string TEST_QUALIFIER = "URI";
//        private const string TEST_VALUE = "urn:buri:ddmsence:testIdentifier";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public IdentifierTest() : base("identifier.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Identifier Fixture {
//            get {
//                try {
//                    return (new Identifier("URI", "urn:buri:ddmsence:testIdentifier"));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private Identifier GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Identifier component = null;
//            try {
//                component = new Identifier(element);
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
//        /// <param name="qualifier"> the qualifier value </param>
//        /// <param name="value"> the value </param>
//        /// <returns> a valid object </returns>
//        private Identifier GetInstance(string message, string qualifier, string value) {
//            bool expectFailure = !Util.isEmpty(message);
//            Identifier component = null;
//            try {
//                component = new Identifier(qualifier, value);
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
//            text.Append(BuildOutput(isHTML, "identifier.qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "identifier.value", TEST_VALUE));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:identifier ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Identifier.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                GetInstance(SUCCESS, GetValidElement(sVersion));
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string identifierName = Identifier.getName(version);

//                // Missing qualifier
//                XElement element = Util.buildDDMSElement(identifierName, null);
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("qualifier attribute is required.", element);

//                // Empty qualifier
//                element = Util.buildDDMSElement(identifierName, null);
//                Util.addDDMSAttribute(element, "qualifier", "");
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("qualifier attribute is required.", element);

//                // Missing value
//                element = Util.buildDDMSElement(identifierName, null);
//                Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
//                GetInstance("value attribute is required.", element);

//                // Empty value
//                element = Util.buildDDMSElement(identifierName, null);
//                Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(element, "value", "");
//                GetInstance("value attribute is required.", element);

//                // Qualifier not URI
//                element = Util.buildDDMSElement(identifierName, null);
//                Util.addDDMSAttribute(element, "qualifier", INVALID_URI);
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("Invalid URI", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Missing qualifier
//                GetInstance("qualifier attribute is required.", null, TEST_VALUE);

//                // Empty qualifier
//                GetInstance("qualifier attribute is required.", "", TEST_VALUE);

//                // Missing value
//                GetInstance("value attribute is required.", TEST_QUALIFIER, null);

//                // Empty value
//                GetInstance("value attribute is required.", TEST_QUALIFIER, "");

//                // Qualifier not URI
//                GetInstance("Invalid URI", INVALID_URI, TEST_VALUE);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No warnings
//                Identifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Identifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Identifier dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Identifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Identifier dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Identifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Identifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Identifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Identifier.Builder builder = new Identifier.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Identifier.Builder builder = new Identifier.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Value = TEST_VALUE;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Identifier.Builder builder = new Identifier.Builder();
//                builder.Value = TEST_VALUE;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "qualifier attribute is required.");
//                }
//                builder.Qualifier = TEST_QUALIFIER;
//                builder.commit();
//            }
//        }
//    }

//}