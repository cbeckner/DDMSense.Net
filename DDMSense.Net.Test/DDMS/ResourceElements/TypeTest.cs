//using System;
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
//    using Util = DDMSense.Util.Util;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:type elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class TypeTest : AbstractBaseTestCase {

//        private const string TEST_DESCRIPTION = "Description";
//        private const string TEST_QUALIFIER = "DCMITYPE";
//        private const string TEST_VALUE = "http://purl.org/dc/dcmitype/Text";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public TypeTest() : base("type.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Type Fixture {
//            get {
//                try {
//                    return (new Type(null, "DCMITYPE", "http://purl.org/dc/dcmitype/Text", null));
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
//        private Type GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Type component = null;
//            try {
//                component = new Type(element);
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
//        /// <param name="description"> the description child text </param>
//        /// <param name="qualifier"> the qualifier value </param>
//        /// <param name="value"> the value </param>
//        /// <returns> a valid object </returns>
//        private Type GetInstance(string message, string description, string qualifier, string value) {
//            bool expectFailure = !Util.isEmpty(message);
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            Type component = null;
//            try {
//                component = new Type(description, qualifier, value, version.isAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
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
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder text = new StringBuilder();
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "type.description", TEST_DESCRIPTION));
//            }
//            text.Append(BuildOutput(isHTML, "type.qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "type.value", TEST_VALUE));
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "type.classification", "U"));
//                text.Append(BuildOutput(isHTML, "type.ownerProducer", "USA"));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                DDMSVersion version = DDMSVersion.CurrentVersion;
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:type ").Append(XmlnsDDMS).Append(" ");
//                if (version.isAtLeast("4.0.1")) {
//                    xml.Append(XmlnsISM).Append(" ");
//                }
//                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\"");
//                if (version.isAtLeast("4.0.1")) {
//                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description</ddms:type>");
//                } else {
//                    xml.Append(" />");
//                }
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Type.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Type.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, version.isAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);

//                // No optional fields
//                GetInstance(SUCCESS, "", "", "");
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Missing qualifier
//                XElement element = Util.buildDDMSElement(Type.getName(version), null);
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("qualifier attribute is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Missing qualifier
//                GetInstance("qualifier attribute is required.", null, null, TEST_VALUE);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Qualifier without value
//                XElement element = Util.buildDDMSElement(Type.getName(version), null);
//                Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
//                component = GetInstance(SUCCESS, element);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A qualifier has been set without an accompanying value attribute.";
//                string locator = "ddms:type";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

//                // Neither attribute
//                element = Util.buildDDMSElement(Type.getName(version), null);
//                component = GetInstance(SUCCESS, element);
//                assertEquals(1, component.ValidationWarnings.size());
//                text = "Neither a qualifier nor a value was set on this type.";
//                locator = "ddms:type";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                Type elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Type dataComponent = GetInstance(SUCCESS, version.isAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                Type elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Type dataComponent = GetInstance(SUCCESS, version.isAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//                if (version.isAtLeast("4.0.1")) {
//                    dataComponent = GetInstance(SUCCESS, "differentDescription", TEST_QUALIFIER, TEST_VALUE);
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, version.isAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, version.isAtLeast("4.0.1") ? TEST_DESCRIPTION : "", TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionSecurityAttributes() {
//            DDMSVersion.CurrentVersion = "3.1";
//            try {
//                new Type(null, TEST_QUALIFIER, TEST_VALUE, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Security attributes cannot be applied");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionDescriptionAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionDescriptionAttributes() {
//            DDMSVersion.CurrentVersion = "3.1";
//            try {
//                new Type(TEST_DESCRIPTION, TEST_QUALIFIER, TEST_VALUE, null);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot contain description");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Type component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Type.Builder builder = new Type.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Type.Builder builder = new Type.Builder();
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

//                Type.Builder builder = new Type.Builder();
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