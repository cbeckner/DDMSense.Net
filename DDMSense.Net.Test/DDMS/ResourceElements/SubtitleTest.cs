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
//    /// <para> Tests related to ddms:subtitle elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class SubtitleTest : AbstractBaseTestCase {

//        private const string TEST_VALUE = "Version 0.1";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public SubtitleTest() : base("subtitle.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Subtitle Fixture {
//            get {
//                try {
//                    return (new Subtitle("Version 0.1", SecurityAttributesTest.Fixture));
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
//        private Subtitle GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Subtitle component = null;
//            try {
//                component = new Subtitle(element);
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
//        /// <param name="value"> the child text </param>
//        /// <returns> a valid object </returns>
//        private Subtitle GetInstance(string message, string value) {
//            bool expectFailure = !Util.isEmpty(message);
//            Subtitle component = null;
//            try {
//                component = new Subtitle(value, SecurityAttributesTest.Fixture);
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
//            text.Append(BuildOutput(isHTML, "subtitle", TEST_VALUE));
//            text.Append(BuildOutput(isHTML, "subtitle.classification", "U"));
//            text.Append(BuildOutput(isHTML, "subtitle.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:subtitle ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append(TEST_VALUE).Append("</ddms:subtitle>");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Subtitle.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Subtitle.getName(version), null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, TEST_VALUE);

//                // No optional fields
//                GetInstance(SUCCESS, "");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Bad security attributes
//                XElement element = Util.buildDDMSElement(Subtitle.getName(version), null);
//                GetInstance("classification is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Bad security attributes
//                try {
//                    new Subtitle(TEST_VALUE, (SecurityAttributes) null);
//                    fail("Allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "classification is required.");
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                Subtitle component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // No value
//                XElement element = Util.buildDDMSElement(Subtitle.getName(version), null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                component = GetInstance(SUCCESS, element);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A ddms:subtitle element was found with no subtitle value.";
//                string locator = "ddms:subtitle";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Subtitle elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Subtitle dataComponent = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Subtitle elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Subtitle dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Subtitle component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Subtitle component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_VALUE);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Subtitle component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Subtitle.Builder builder = new Subtitle.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Subtitle.Builder builder = new Subtitle.Builder();
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

//                Subtitle.Builder builder = new Subtitle.Builder();
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