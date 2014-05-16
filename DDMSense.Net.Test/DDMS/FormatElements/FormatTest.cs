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
//namespace DDMSense.Test.DDMS.FormatElements {


//    using DDMSense.DDMS.FormatElements;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to ddms:format elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class FormatTest : AbstractBaseTestCase {

//        private const string TEST_MIME_TYPE = "text/xml";
//        private const string TEST_MEDIUM = "digital";

//        /// <summary>
//        /// Constructor
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public FormatTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public FormatTest() : base("format.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Format Fixture {
//            get {
//                try {
//                    return (new Format("text/xml", null, null));
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
//        private Format GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Format component = null;
//            try {
//                component = new Format(element);
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
//        /// <param name="mimeType"> the mimeType element (required) </param>
//        /// <param name="extent"> the extent element (may be null) </param>
//        /// <param name="medium"> the medium element (may be null) </param>
//        /// <returns> a valid object </returns>
//        private Format GetInstance(string message, string mimeType, Extent extent, string medium) {
//            bool expectFailure = !Util.isEmpty(message);
//            Format component = null;
//            try {
//                component = new Format(mimeType, extent, medium);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Helper method to manage the deprecated Media wrapper element
//        /// </summary>
//        /// <param name="innerElement"> the element containing the guts of this component </param>
//        /// <returns> the element itself in DDMS 4.0.1 or later, or the element wrapped in another element </returns>
//        private XElement WrapInnerElement(XElement innerElement) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            string name = Format.getName(version);
//            if (version.isAtLeast("4.0.1")) {
//                innerElement.LocalName = name;
//                return (innerElement);
//            }
//            XElement element = Util.buildDDMSElement(name, null);
//            element.appendChild(innerElement);
//            return (element);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            string prefix = version.isAtLeast("4.0.1") ? "format." : "format.Media.";
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, prefix + "mimeType", TEST_MIME_TYPE));
//            text.Append(ExtentTest.Fixture.getOutput(isHTML, prefix, ""));
//            text.Append(BuildOutput(isHTML, prefix + "medium", TEST_MEDIUM));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:format ").Append(XmlnsDDMS).Append(">\n\t");
//            if (DDMSVersion.CurrentVersion.isAtLeast("4.0.1")) {
//                xml.Append("<ddms:mimeType>text/xml</ddms:mimeType>\n\t");
//                xml.Append("<ddms:extent ddms:qualifier=\"sizeBytes\" ddms:value=\"75000\" />\n\t");
//                xml.Append("<ddms:medium>digital</ddms:medium>\n");
//            } else {
//                xml.Append("<ddms:Media>\n\t\t");
//                xml.Append("<ddms:mimeType>text/xml</ddms:mimeType>\n\t\t");
//                xml.Append("<ddms:extent ddms:qualifier=\"sizeBytes\" ddms:value=\"75000\" />\n\t\t");
//                xml.Append("<ddms:medium>digital</ddms:medium>\n\t");
//                xml.Append("</ddms:Media>\n");
//            }
//            xml.Append("</ddms:format>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Format.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement mediaElement = Util.buildDDMSElement("Media", null);
//                Util.addDDMSChildElement(mediaElement, "mimeType", "text/html");
//                GetInstance(SUCCESS, WrapInnerElement(mediaElement));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);

//                // No optional fields
//                GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string extentName = Extent.getName(version);

//                // Missing mimeType
//                XElement mediaElement = Util.buildDDMSElement("Media", null);
//                GetInstance("mimeType is required.", WrapInnerElement(mediaElement));

//                // Empty mimeType
//                mediaElement = Util.buildDDMSElement("Media", null);
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", ""));
//                GetInstance("mimeType is required.", WrapInnerElement(mediaElement));

//                // Too many mimeTypes
//                mediaElement = Util.buildDDMSElement("Media", null);
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", TEST_MIME_TYPE));
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", TEST_MIME_TYPE));
//                GetInstance("Exactly 1 mimeType element must exist.", WrapInnerElement(mediaElement));

//                // Too many extents
//                mediaElement = Util.buildDDMSElement("Media", null);
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", TEST_MIME_TYPE));
//                mediaElement.appendChild(Util.buildDDMSElement(extentName, null));
//                mediaElement.appendChild(Util.buildDDMSElement(extentName, null));
//                GetInstance("No more than 1 extent element can exist.", WrapInnerElement(mediaElement));

//                // Too many mediums
//                mediaElement = Util.buildDDMSElement("Media", null);
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", TEST_MIME_TYPE));
//                mediaElement.appendChild(Util.buildDDMSElement("medium", TEST_MEDIUM));
//                mediaElement.appendChild(Util.buildDDMSElement("medium", TEST_MEDIUM));
//                GetInstance("No more than 1 medium element can exist.", WrapInnerElement(mediaElement));

//                // Invalid Extent
//                XElement extentElement = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(extentElement, "value", "test");
//                mediaElement = Util.buildDDMSElement("Media", null);
//                Util.addDDMSChildElement(mediaElement, "mimeType", "text/html");
//                mediaElement.appendChild(extentElement);
//                GetInstance("qualifier attribute is required.", WrapInnerElement(mediaElement));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Missing mimeType
//                GetInstance("mimeType is required.", null, ExtentTest.Fixture, TEST_MEDIUM);

//                // Empty mimeType
//                GetInstance("mimeType is required.", "", ExtentTest.Fixture, TEST_MEDIUM);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // No warnings
//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Medium element with no value or empty value
//                XElement mediaElement = Util.buildDDMSElement("Media", null);
//                mediaElement.appendChild(Util.buildDDMSElement("mimeType", TEST_MIME_TYPE));
//                mediaElement.appendChild(Util.buildDDMSElement("medium", null));
//                component = GetInstance(SUCCESS, WrapInnerElement(mediaElement));
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A ddms:medium element was found with no value.";
//                string locator = version.isAtLeast("4.0.1") ? "ddms:format" : "ddms:format/ddms:Media";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

//                // Nested warnings
//                component = GetInstance(SUCCESS, TEST_MIME_TYPE, new Extent("sizeBytes", ""), TEST_MEDIUM);
//                assertEquals(1, component.ValidationWarnings.size());
//                text = "A qualifier has been set without an accompanying value attribute.";
//                locator = (version.isAtLeast("4.0.1")) ? "ddms:format/ddms:extent" : "ddms:format/ddms:Media/ddms:extent";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Format dataComponent = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Format elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Format dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, ExtentTest.Fixture, TEST_MEDIUM);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_MIME_TYPE, null, TEST_MEDIUM);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, "TEST_MIME_TYPE", ExtentTest.Fixture, null);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, TEST_MIME_TYPE, ExtentTest.Fixture, TEST_MEDIUM);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testExtentReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestExtentReuse() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Extent extent = ExtentTest.Fixture;
//                GetInstance(SUCCESS, TEST_MIME_TYPE, extent, TEST_MEDIUM);
//                GetInstance(SUCCESS, TEST_MIME_TYPE, extent, TEST_MEDIUM);
//            }
//        }

//        public virtual void TestGetExtentQualifier() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(component.ExtentQualifier, component.Extent.Qualifier);
//            }
//        }

//        public virtual void TestGetExtentQualifierNoExtent() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
//                assertEquals("", component.ExtentQualifier);
//            }
//        }

//        public virtual void TestGetExtentValue() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(component.ExtentValue, component.Extent.Value);
//            }
//        }

//        public virtual void TestGetExtentValueNoExtent() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Format component = GetInstance(SUCCESS, TEST_MIME_TYPE, null, null);
//                assertEquals("", component.ExtentValue);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Format component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Format.Builder builder = new Format.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Format.Builder builder = new Format.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.MimeType = TEST_MIME_TYPE;
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Format.Builder builder = new Format.Builder();
//                builder.Medium = TEST_MEDIUM;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "mimeType is required.");
//                }
//                builder.MimeType = TEST_MIME_TYPE;
//                builder.commit();

//                // No extent vs. empty extent
//                builder = new Format.Builder();
//                builder.MimeType = TEST_MIME_TYPE;
//                builder.Medium = TEST_MEDIUM;
//                assertNotNull(builder.Extent);
//                assertNull(builder.commit().Extent);
//                builder.Extent.Qualifier = "sizeBytes";
//                builder.Extent.Value = "75000";
//                assertNotNull(builder.commit().Extent);
//            }
//        }
//    }

//}