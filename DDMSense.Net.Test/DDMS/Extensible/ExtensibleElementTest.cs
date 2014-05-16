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
//namespace DDMSense.Test.DDMS.Extensible {


//    using DDMSense.DDMS.Extensible;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to extensible layer elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 1.1.0
//    /// </summary>
//    public class ExtensibleElementTest : AbstractBaseTestCase {

//        private const string TEST_NAME = "extension";
//        private const string TEST_PREFIX = "ddmsence";
//        private const string TEST_NAMESPACE = "http://ddmsence.urizone.net/";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public ExtensibleElementTest() : base(null) {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static XElement FixtureElement {
//            get {
//                return (Util.buildElement(TEST_PREFIX, TEST_NAME, TEST_NAMESPACE, "This is an extensible element."));
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private ExtensibleElement GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            ExtensibleElement component = null;
//            try {
//                component = new ExtensibleElement(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddmsence:extension xmlns:ddmsence=\"http://ddmsence.urizone.net/\">").Append("This is an extensible element.</ddmsence:extension>");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), TEST_PREFIX, TEST_NAME);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, FixtureElement);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Using the DDMS namespace
//                XElement element = Util.buildDDMSElement("name", null);
//                GetInstance("Extensible elements cannot be defined in the DDMS namespace.", element);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No warnings
//                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                ExtensibleElement elementComponent = GetInstance(SUCCESS, FixtureElement);

//                XElement element = Util.buildElement(TEST_PREFIX, TEST_NAME, TEST_NAMESPACE, "This is an extensible element.");
//                ExtensibleElement dataComponent = GetInstance(SUCCESS, element);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                ExtensibleElement elementComponent = GetInstance(SUCCESS, FixtureElement);
//                XElement element = Util.buildElement(TEST_PREFIX, "newName", TEST_NAMESPACE, "This is an extensible element.");
//                ExtensibleElement dataComponent = GetInstance(SUCCESS, element);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals("", component.toHTML());
//                assertEquals("", component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ExtensibleElement component = GetInstance(SUCCESS, FixtureElement);
//                ExtensibleElement.Builder builder = new ExtensibleElement.Builder(component);
//                assertEquals(component, builder.commit());

//                builder = new ExtensibleElement.Builder();
//                builder.Xml = FixtureElement.toXML();
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ExtensibleElement.Builder builder = new ExtensibleElement.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Xml = "<test/>";
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ExtensibleElement.Builder builder = new ExtensibleElement.Builder();
//                builder.Xml = "InvalidXml";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "Could not create a valid element");
//                }
//                builder.Xml = ExpectedXMLOutput;
//                builder.commit();
//            }
//        }
//    }

//}