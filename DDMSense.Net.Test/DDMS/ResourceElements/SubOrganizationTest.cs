//using System.Collections.Generic;
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


	
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:subOrganization elements </para>
//    /// 
//    /// <para> Because a ddms:subOrganization is a local component, we cannot load a valid document from a unit test data file.
//    /// We have to build the well-formed XElement ourselves. </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class SubOrganizationTest : AbstractBaseTestCase {

//        private const string TEST_VALUE = "PEO-GES";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public SubOrganizationTest() : base(null) {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static IList<SubOrganization> FixtureList {
//            get {
//                try {
//                    if (!DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
//                        return (null);
//                    }
//                    IList<SubOrganization> subOrgs = new List<SubOrganization>();
//                    subOrgs.Add(new SubOrganization("sub1", SecurityAttributesTest.Fixture));
//                    subOrgs.Add(new SubOrganization("sub2", SecurityAttributesTest.Fixture));
//                    return (subOrgs);
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static XElement FixtureElement {
//            get {
//                try {
//                    DDMSVersion version = DDMSVersion.CurrentVersion;
//                    XElement element = Util.buildDDMSElement(SubOrganization.getName(version), TEST_VALUE);
//                    element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
//                    element.addNamespaceDeclaration(PropertyReader.getPrefix("ism"), version.IsmNamespace);
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    return (element);
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
//        private SubOrganization GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            SubOrganization component = null;
//            try {
//                component = new SubOrganization(element);
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
//        /// <param name="value"> the value </param>
//        /// <returns> a valid object </returns>
//        private SubOrganization GetInstance(string message, string value) {
//            bool expectFailure = !Util.isEmpty(message);
//            SubOrganization component = null;
//            try {
//                component = new SubOrganization(value, SecurityAttributesTest.Fixture);
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
//            text.Append(BuildOutput(isHTML, "subOrganization", TEST_VALUE));
//            text.Append(BuildOutput(isHTML, "subOrganization.classification", "U"));
//            text.Append(BuildOutput(isHTML, "subOrganization.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:subOrganization ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append(TEST_VALUE);
//                xml.Append("</ddms:subOrganization>");
//                return (xml.ToString());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, SubOrganization.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                GetInstance(SUCCESS, FixtureElement);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                GetInstance(SUCCESS, TEST_VALUE);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Missing child text
//                XElement element = Util.buildDDMSElement(SubOrganization.getName(version), null);
//                GetInstance("subOrganization value is required.", element);

//                // Empty child text
//                element = Util.buildDDMSElement(SubOrganization.getName(version), "");
//                GetInstance("subOrganization value is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Missing child text
//                GetInstance("subOrganization value is required.", (string) null);

//                // Empty child text
//                GetInstance("subOrganization value is required.", "");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // No warnings
//                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization elementComponent = GetInstance(SUCCESS, FixtureElement);
//                SubOrganization dataComponent = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization elementComponent = GetInstance(SUCCESS, FixtureElement);
//                SubOrganization dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            try {
//                DDMSVersion.CurrentVersion = "2.0";
//                new SubOrganization(TEST_VALUE, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The subOrganization element cannot be used");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
//                SubOrganization.Builder builder = new SubOrganization.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                SubOrganization.Builder builder = new SubOrganization.Builder();
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

//                SubOrganization.Builder builder = new SubOrganization.Builder();
//                builder.Value = TEST_VALUE;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "classification is required.");
//                }
//                builder.SecurityAttributes.Classification = "U";
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }
//    }

//}