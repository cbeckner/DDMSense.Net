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
//namespace DDMSense.Test.DDMS.Summary.Xlink {


//    using DDMSense.DDMS.Summary.Xlink;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to the XLINK attributes on ddms:link and ddms:taskID elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class XLinkAttributesTest : AbstractBaseTestCase {

//        private const string TEST_HREF = "http://en.wikipedia.org/wiki/Tank";
//        private const string TEST_ROLE = "tank";
//        private const string TEST_TITLE = "Tank Page";
//        private const string TEST_LABEL = "tank";
//        private const string TEST_ARCROLE = "arcrole";
//        private const string TEST_SHOW = "new";
//        private const string TEST_ACTUATE = "onLoad";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public XLinkAttributesTest() : base(null) {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing. The type will be "locator".
//        /// </summary>
//        public static XLinkAttributes LocatorFixture {
//            get {
//                try {
//                    return (new XLinkAttributes(TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing. The type will be "simple".
//        /// </summary>
//        public static XLinkAttributes SimpleFixture {
//            get {
//                try {
//                    return (new XLinkAttributes(TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing. The type will be "resource".
//        /// </summary>
//        public static XLinkAttributes ResourceFixture {
//            get {
//                try {
//                    return (new XLinkAttributes(TEST_ROLE, TEST_TITLE, TEST_LABEL));
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
//        private XLinkAttributes GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            XLinkAttributes attributes = null;
//            try {
//                attributes = new XLinkAttributes(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="label"> the name of the link (optional) </param>
//        /// <returns> a valid object </returns>
//        private XLinkAttributes GetInstance(string message, string role, string title, string label) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            XLinkAttributes attributes = null;
//            try {
//                attributes = new XLinkAttributes(role, title, label);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="href"> the link href (optional) </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="label"> the name of the link (optional) </param>
//        /// <returns> a valid object </returns>
//        private XLinkAttributes GetInstance(string message, string href, string role, string title, string label) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            XLinkAttributes attributes = null;
//            try {
//                attributes = new XLinkAttributes(href, role, title, label);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="href"> the link href (optional) </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="arcrole"> the arcrole attribute (optional) </param>
//        /// <param name="show"> the show token (optional) </param>
//        /// <param name="actuate"> the actuate token (optional) </param>
//        /// <returns> a valid object </returns>
//        private XLinkAttributes GetInstance(string message, string href, string role, string title, string arcrole, string show, string actuate) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            XLinkAttributes attributes = null;
//            try {
//                attributes = new XLinkAttributes(href, role, title, arcrole, show, actuate);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
//        private string GetExpectedOutput(bool isHTML, string type) {
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, "type", type));
//            if (!"resource".Equals(type)) {
//                text.Append(BuildOutput(isHTML, "href", TEST_HREF));
//            }
//            text.Append(BuildOutput(isHTML, "role", TEST_ROLE));
//            text.Append(BuildOutput(isHTML, "title", TEST_TITLE));
//            if (!"simple".Equals(type)) {
//                text.Append(BuildOutput(isHTML, "label", TEST_LABEL));
//            }
//            if ("simple".Equals(type)) {
//                text.Append(BuildOutput(isHTML, "arcrole", TEST_ARCROLE));
//                text.Append(BuildOutput(isHTML, "show", TEST_SHOW));
//                text.Append(BuildOutput(isHTML, "actuate", TEST_ACTUATE));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Helper method to add attributes to a XOM element. The element is not validated.
//        /// </summary>
//        /// <param name="element"> element </param>
//        /// <param name="href"> the link href (optional) </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="arcrole"> the arcrole attribute (optional) </param>
//        /// <param name="show"> the show token (optional) </param>
//        /// <param name="actuate"> the actuate token (optional) </param>
//        private void AddAttributes(XElement element, string href, string role, string title, string arcrole, string show, string actuate) {
//            string xlinkPrefix = PropertyReader.getPrefix("xlink");
//            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
//            Util.addAttribute(element, xlinkPrefix, "type", xlinkNamespace, "simple");
//            Util.addAttribute(element, xlinkPrefix, "href", xlinkNamespace, href);
//            Util.addAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
//            Util.addAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
//            Util.addAttribute(element, xlinkPrefix, "arcrole", xlinkNamespace, arcrole);
//            Util.addAttribute(element, xlinkPrefix, "show", xlinkNamespace, show);
//            Util.addAttribute(element, xlinkPrefix, "actuate", xlinkNamespace, actuate);
//        }

//        /// <summary>
//        /// Helper method to add attributes to a XOM element. The element is not validated.
//        /// </summary>
//        /// <param name="element"> element </param>
//        /// <param name="href"> the link href (optional) </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="label"> the name of the link (optional) </param>
//        private void AddAttributes(XElement element, string href, string role, string title, string label) {
//            string xlinkPrefix = PropertyReader.getPrefix("xlink");
//            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
//            Util.addAttribute(element, xlinkPrefix, "type", xlinkNamespace, "locator");
//            Util.addAttribute(element, xlinkPrefix, "href", xlinkNamespace, href);
//            Util.addAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
//            Util.addAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
//            Util.addAttribute(element, xlinkPrefix, "label", xlinkNamespace, label);
//        }

//        /// <summary>
//        /// Helper method to add attributes to a XOM element. The element is not validated.
//        /// </summary>
//        /// <param name="element"> element </param>
//        /// <param name="href"> the link href (optional) </param>
//        /// <param name="role"> the role attribute (optional) </param>
//        /// <param name="title"> the link title (optional) </param>
//        /// <param name="label"> the name of the link (optional) </param>
//        private void AddAttributes(XElement element, string role, string title, string label) {
//            string xlinkPrefix = PropertyReader.getPrefix("xlink");
//            string xlinkNamespace = DDMSVersion.CurrentVersion.XlinkNamespace;
//            Util.addAttribute(element, xlinkPrefix, "type", xlinkNamespace, "resource");
//            Util.addAttribute(element, xlinkPrefix, "role", xlinkNamespace, role);
//            Util.addAttribute(element, xlinkPrefix, "title", xlinkNamespace, title);
//            Util.addAttribute(element, xlinkPrefix, "label", xlinkNamespace, label);
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields (locator)
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                GetInstance(SUCCESS, element);

//                // All fields (simple)
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                GetInstance(SUCCESS, element);

//                // All fields (resource)
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                GetInstance(SUCCESS, element);

//                // No optional fields (all)
//                element = Util.buildDDMSElement("link", null);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields (locator)
//                GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);

//                // All fields (simple)
//                GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);

//                // All fields (resource)
//                GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);

//                // No optional fields (all)
//                GetInstance(SUCCESS, null, null, null, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // href is not valid URI
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, INVALID_URI, null, null, null);
//                GetInstance("Invalid URI", element);

//                // role is not valid URI
//                if (version.isAtLeast("4.0.1")) {
//                    element = Util.buildDDMSElement("link", null);
//                    AddAttributes(element, null, INVALID_URI, null, null);
//                    GetInstance("Invalid URI", element);
//                }

//                // label is not valid NCName
//                if (version.isAtLeast("4.0.1")) {
//                    element = Util.buildDDMSElement("link", null);
//                    AddAttributes(element, null, null, null, "ddms:prefix& GML");
//                    GetInstance("\"ddms:prefix& GML\" is not a valid NCName.", element);
//                }

//                // invalid arcrole
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, null, null, null, INVALID_URI, null, null);

//                // invalid show
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, null, null, null, null, "notInTheTokenList", null);

//                // invalid actuate
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, null, null, null, null, null, "notInTheTokenList");
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // href is not valid URI
//                GetInstance("Invalid URI", INVALID_URI, null, null, null);

//                // role is not valid URI
//                if (version.isAtLeast("4.0.1")) {
//                    GetInstance("Invalid URI", null, INVALID_URI, null, null);
//                }

//                // label is not valid NCName
//                if (version.isAtLeast("4.0.1")) {
//                    GetInstance("\"ddms:prefix& GML\" is not a valid NCName.", null, null, null, "ddms:prefix& GML");
//                }

//                // invalid arcrole
//                GetInstance("Invalid URI", null, null, null, INVALID_URI, null, null);

//                // invalid show
//                GetInstance("The show attribute must be one of", null, null, null, null, "notInTheTokenList", null);

//                // invalid actuate
//                GetInstance("The actuate attribute must be one of", null, null, null, null, null, "notInTheTokenList");
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // No warnings
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                XLinkAttributes component = GetInstance(SUCCESS, element);
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // locator version
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                XLinkAttributes elementAttributes = GetInstance(SUCCESS, element);
//                XLinkAttributes dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                assertEquals(elementAttributes, dataAttributes);
//                assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());

//                // simple version
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                elementAttributes = GetInstance(SUCCESS, element);
//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                assertEquals(elementAttributes, dataAttributes);
//                assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());

//                // resource version
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                elementAttributes = GetInstance(SUCCESS, element);
//                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                assertEquals(elementAttributes, dataAttributes);
//                assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // locator version
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                XLinkAttributes elementAttributes = GetInstance(SUCCESS, element);
//                XLinkAttributes dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, DIFFERENT_VALUE, TEST_TITLE, TEST_LABEL);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, DIFFERENT_VALUE, TEST_LABEL);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                // simple version
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);

//                dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, DIFFERENT_VALUE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, DIFFERENT_VALUE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE, TEST_SHOW, TEST_ACTUATE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, "replace", TEST_ACTUATE);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, "onRequest");
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                // resource version
//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                elementAttributes = GetInstance(SUCCESS, element);
//                dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_TITLE, TEST_LABEL);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, DIFFERENT_VALUE, TEST_LABEL);
//                assertFalse(elementAttributes.Equals(dataAttributes));

//                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, DIFFERENT_VALUE);
//                assertFalse(elementAttributes.Equals(dataAttributes));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                XLinkAttributes attributes = new XLinkAttributes(element);
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(attributes.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XElement element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                XLinkAttributes attributes = new XLinkAttributes(element);
//                assertEquals(GetExpectedOutput(true, "locator"), attributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "locator"), attributes.getOutput(false, ""));

//                XLinkAttributes dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                assertEquals(GetExpectedOutput(true, "locator"), dataAttributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "locator"), dataAttributes.getOutput(false, ""));

//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                attributes = new XLinkAttributes(element);
//                assertEquals(GetExpectedOutput(true, "simple"), attributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "simple"), attributes.getOutput(false, ""));

//                dataAttributes = GetInstance(SUCCESS, TEST_HREF, TEST_ROLE, TEST_TITLE, TEST_ARCROLE, TEST_SHOW, TEST_ACTUATE);
//                assertEquals(GetExpectedOutput(true, "simple"), dataAttributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "simple"), dataAttributes.getOutput(false, ""));

//                element = Util.buildDDMSElement("link", null);
//                AddAttributes(element, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                attributes = new XLinkAttributes(element);
//                assertEquals(GetExpectedOutput(true, "resource"), attributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "resource"), attributes.getOutput(false, ""));

//                dataAttributes = GetInstance(SUCCESS, TEST_ROLE, TEST_TITLE, TEST_LABEL);
//                assertEquals(GetExpectedOutput(true, "resource"), dataAttributes.getOutput(true, ""));
//                assertEquals(GetExpectedOutput(false, "resource"), dataAttributes.getOutput(false, ""));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testAddTo() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestAddTo() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                XLinkAttributes component = LocatorFixture;

//                XElement element = Util.buildDDMSElement("sample", null);
//                component.addTo(element);
//                XLinkAttributes output = new XLinkAttributes(element);
//                assertEquals(component, output);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testGetNonNull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestGetNonNull() {
//            XLinkAttributes component = new XLinkAttributes();
//            XLinkAttributes output = XLinkAttributes.getNonNullInstance(null);
//            assertEquals(component, output);

//            output = XLinkAttributes.getNonNullInstance(LocatorFixture);
//            assertEquals(LocatorFixture, output);
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XLinkAttributes component = LocatorFixture;
//                XLinkAttributes.Builder builder = new XLinkAttributes.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XLinkAttributes.Builder builder = new XLinkAttributes.Builder();
//                assertNotNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Label = TEST_LABEL;
//                assertFalse(builder.Empty);

//                // An untyped instance
//                XLinkAttributes output = builder.commit();
//                assertTrue(String.IsNullOrEmpty(output.Type));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XLinkAttributes.Builder builder = new XLinkAttributes.Builder();
//                builder.Type = "locator";
//                builder.Href = INVALID_URI;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "Invalid URI");
//                }
//                builder.Type = "locator";
//                builder.Href = TEST_HREF;
//                builder.commit();
//                builder.Type = "simple";
//                builder.commit();
//                builder.Type = "resource";
//                builder.commit();
//            }
//        }
//    }

//}