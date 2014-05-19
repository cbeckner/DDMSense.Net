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

	
//    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:source elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class SourceTest : AbstractBaseTestCase {

//        private const string TEST_QUALIFIER = "URL";
//        private const string TEST_VALUE = "http://www.xmethods.com";
//        private const string TEST_SCHEMA_QUALIFIER = "WSDL";
//        private const string TEST_SCHEMA_HREF = "http://www.xmethods.com/sd/2001/TemperatureService?wsdl";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public SourceTest() : base("source.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Source Fixture {
//            get {
//                try {
//                    return (new Source(null, "http://www.xmethods.com", null, null, null));
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
//        private Source GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Source component = null;
//            try {
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    SecurityAttributesTest.Fixture.addTo(element);
//                }
//                component = new Source(element);
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
//        /// <param name="schemaQualifier"> the value of the schemaQualifier attribute </param>
//        /// <param name="schemaHref"> the value of the schemaHref attribute </param>
//        /// <returns> a valid object </returns>
//        private Source GetInstance(string message, string qualifier, string value, string schemaQualifier, string schemaHref) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Source component = null;
//            try {
//                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
//                component = new Source(qualifier, value, schemaQualifier, schemaHref, attr);
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
//            text.Append(BuildOutput(isHTML, "source.qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "source.value", TEST_VALUE));
//            text.Append(BuildOutput(isHTML, "source.schemaQualifier", TEST_SCHEMA_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "source.schemaHref", TEST_SCHEMA_HREF));
//            if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                text.Append(BuildOutput(isHTML, "source.classification", "U"));
//                text.Append(BuildOutput(isHTML, "source.ownerProducer", "USA"));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:source ").Append(XmlnsDDMS).Append(" ");
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    xml.Append(XmlnsISM).Append(" ");
//                }
//                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" ");
//                xml.Append("ddms:schemaQualifier=\"").Append(TEST_SCHEMA_QUALIFIER).Append("\" ");
//                xml.Append("ddms:schemaHref=\"").Append(TEST_SCHEMA_HREF).Append("\" ");
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
//                }
//                xml.Append("/>");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Source.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Source.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);

//                // No optional fields
//                GetInstance(SUCCESS, "", "", "", "");
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Href not URI
//                XElement element = Util.buildDDMSElement(Source.getName(version), null);
//                Util.addDDMSAttribute(element, "schemaHref", INVALID_URI);
//                GetInstance("Invalid URI", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalidHrefNotURI() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Href not URI
//                GetInstance("Invalid URI", TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, INVALID_URI);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());

//                XElement element = Util.buildDDMSElement(Source.getName(version), null);
//                component = GetInstance(SUCCESS, element);
//                Assert.Equals(1, component.ValidationWarnings.size());
//                string text = "A completely empty ddms:source element was found.";
//                string locator = "ddms:source";
//                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Source elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Source dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Source elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Source dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, DIFFERENT_VALUE, TEST_SCHEMA_HREF);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, DIFFERENT_VALUE);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestSecurityAttributes() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                SecurityAttributes attr = (!version.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
//                Source component = new Source(TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF, attr);
//                if (!version.isAtLeast("3.0")) {
//                    Assert.IsTrue(component.SecurityAttributes.Empty);
//                } else {
//                    Assert.Equals(attr, component.SecurityAttributes);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionSecurityAttributes() {
//            DDMSVersion.SetCurrentVersion("2.0");
//            try {
//                new Source(TEST_QUALIFIER, TEST_VALUE, TEST_SCHEMA_QUALIFIER, TEST_SCHEMA_HREF, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Security attributes cannot be applied");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Source component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Source.Builder builder = new Source.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Source.Builder builder = new Source.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Value = TEST_VALUE;
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Source.Builder builder = new Source.Builder();
//                builder.SecurityAttributes.Classification = "SuperSecret";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "SuperSecret is not a valid enumeration token");
//                }
//                builder.SecurityAttributes.Classification = "";
//                builder.Qualifier = TEST_QUALIFIER;
//                builder.Qualifier = TEST_VALUE;
//                builder.commit();
//            }
//        }
//    }
//}