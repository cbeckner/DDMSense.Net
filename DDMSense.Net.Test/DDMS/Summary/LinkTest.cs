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
//namespace DDMSense.Test.DDMS.Summary {


	
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using XLinkAttributes = DDMSense.DDMS.Summary.Xlink.XLinkAttributes;
//    using XLinkAttributesTest = DDMSense.Test.DDMS.Summary.Xlink.XLinkAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:link elements </para>
//    /// 
//    /// <para> Because a ddms:link is a local component, we cannot load a valid document from a unit test data file. We have to
//    /// build the well-formed XElement ourselves. </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class LinkTest : AbstractBaseTestCase {

//        private const string TEST_TYPE = "locator";
//        private const string TEST_HREF = "http://en.wikipedia.org/wiki/Tank";
//        private const string TEST_ROLE = "tank";
//        private const string TEST_TITLE = "Tank Page";
//        private const string TEST_LABEL = "tank";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public LinkTest() : base(null) {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static XElement FixtureElement {
//            get {
//                try {
//                    DDMSVersion version = DDMSVersion.CurrentVersion;
//                    XElement linkElement = Util.buildDDMSElement(Link.getName(version), null);
//                    linkElement.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
//                    XLinkAttributesTest.LocatorFixture.addTo(linkElement);
//                    return (linkElement);
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        /// <param name="hasSecurity"> true for security attributes </param>
//        public static IList<Link> GetLocatorFixtureList(bool hasSecurity) {
//            IList<Link> links = new List<Link>();
//            links.Add(GetLocatorFixture(hasSecurity));
//            return (links);
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        /// <param name="hasSecurity"> true for security attributes </param>
//        public static Link GetLocatorFixture(bool hasSecurity) {
//            try {
//                return (new Link(XLinkAttributesTest.LocatorFixture, hasSecurity ? SecurityAttributesTest.Fixture : null));
//            } catch (InvalidDDMSException e) {
//                Console.WriteLine(e.ToString());
//                Console.Write(e.StackTrace);
//                fail("Could not create fixture.");
//            }
//            return (null);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="element"> the element to build from </param>
//        /// <returns> a valid object </returns>
//        private Link GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Link component = null;
//            try {
//                component = new Link(element);
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
//        /// <param name="attributes"> the XLink Attributes </param>
//        /// <returns> a valid object </returns>
//        private Link GetInstance(string message, XLinkAttributes attributes) {
//            bool expectFailure = !Util.isEmpty(message);
//            Link component = null;
//            try {
//                component = new Link(attributes);
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
//            text.Append(XLinkAttributesTest.LocatorFixture.getOutput(isHTML, "link."));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                DDMSVersion version = DDMSVersion.CurrentVersion;
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:link ").Append(XmlnsDDMS).Append(" xmlns:xlink=\"").Append(version.XlinkNamespace).Append("\" ");
//                xml.Append("xlink:type=\"locator\" xlink:href=\"http://en.wikipedia.org/wiki/Tank\" ");
//                xml.Append("xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />");
//                return (xml.ToString());
//            }
//        }

//        /// <summary>
//        /// Helper method to create a XOM element that can be used to test element constructors
//        /// </summary>
//        /// <param name="type"> the type </param>
//        /// <param name="href"> the href </param>
//        /// <returns> XElement </returns>
//        private XElement BuildComponentElement(string type, string href) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            XElement element = Util.buildDDMSElement(Link.getName(version), null);
//            string xlinkPrefix = PropertyReader.getPrefix("xlink");
//            string xlinkNamespace = version.XlinkNamespace;
//            if (type != null) {
//                element.addAttribute(Util.buildAttribute(xlinkPrefix, "type", xlinkNamespace, type));
//            }
//            if (href != null) {
//                element.addAttribute(Util.buildAttribute(xlinkPrefix, "href", xlinkNamespace, href));
//            }
//            element.addAttribute(Util.buildAttribute(xlinkPrefix, "role", xlinkNamespace, TEST_ROLE));
//            return (element);
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, Link.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, FixtureElement);

//                // No optional fields
//                XElement element = BuildComponentElement(TEST_TYPE, TEST_HREF);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);

//                // No optional fields
//                GetInstance(SUCCESS, new XLinkAttributes(TEST_HREF, null, null, null));
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Missing href
//                XElement element = BuildComponentElement(TEST_TYPE, null);
//                GetInstance("href attribute is required.", element);

//                // invalid type
//                element = BuildComponentElement("simple", TEST_HREF);
//                GetInstance("The type attribute must have a fixed value", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Missing attributes
//                GetInstance("type attribute is required.", (XLinkAttributes) null);

//                // Missing href
//                GetInstance("href attribute is required.", new XLinkAttributes("", TEST_ROLE, TEST_TITLE, TEST_LABEL));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No warnings
//                Link component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
//                Link dataComponent = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
//                Link dataComponent = GetInstance(SUCCESS, new XLinkAttributes(DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_LABEL));
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Link component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Link component = GetInstance(SUCCESS, FixtureElement);
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongLinkType() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongLinkType() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                GetInstance("The type attribute must have a fixed value", XLinkAttributesTest.SimpleFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Link component = GetInstance(SUCCESS, FixtureElement);
//                Link.Builder builder = new Link.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Link.Builder builder = new Link.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.XLinkAttributes.Role = TEST_ROLE;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Link.Builder builder = new Link.Builder();
//                builder.XLinkAttributes.Role = TEST_ROLE;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "type attribute is required.");
//                }
//                builder.XLinkAttributes.Type = "locator";
//                builder.XLinkAttributes.Href = TEST_HREF;
//                builder.commit();
//            }
//        }
//    }

//}