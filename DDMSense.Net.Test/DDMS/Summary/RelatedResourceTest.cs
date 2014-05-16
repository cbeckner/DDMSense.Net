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
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to DDMS 2.0, 3.0, 3.1 ddms:RelatedResources elements or DDMS 4.0.1 ddms:relatedResource elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class RelatedResourceTest : AbstractBaseTestCase {

//        private const string TEST_RELATIONSHIP = "http://purl.org/dc/terms/references";
//        private const string TEST_DIRECTION = "outbound";
//        private const string TEST_QUALIFIER = "http://purl.org/dc/terms/URI";
//        private const string TEST_VALUE = "http://en.wikipedia.org/wiki/Tank";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public RelatedResourceTest() : base("relatedResources.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static RelatedResource Fixture {
//            get {
//                try {
//                    IList<Link> links = new List<Link>();
//                    links.Add(new Link(new XLinkAttributes("http://en.wikipedia.org/wiki/Tank", "role", null, null)));
//                    return (new RelatedResource(links, "http://purl.org/dc/terms/references", "outbound", "http://purl.org/dc/terms/URI", "http://en.wikipedia.org/wiki/Tank", null));
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
//        private RelatedResource GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            RelatedResource component = null;
//            try {
//                component = new RelatedResource(element);
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
//        /// <param name="links"> a list of links </param>
//        /// <param name="relationship"> the relationship attribute (required) </param>
//        /// <param name="direction"> the relationship direction (optional) </param>
//        /// <param name="qualifier"> the qualifier value </param>
//        /// <param name="value"> the value </param>
//        /// <returns> a valid object </returns>
//        private RelatedResource GetInstance(string message, IList<Link> links, string relationship, string direction, string qualifier, string value) {
//            bool expectFailure = !Util.isEmpty(message);
//            RelatedResource component = null;
//            try {
//                component = new RelatedResource(links, relationship, direction, qualifier, value, SecurityAttributesTest.Fixture);
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
//            string prefix = DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "relatedResource." : "relatedResources.RelatedResource.";
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, prefix + "relationship", TEST_RELATIONSHIP));
//            text.Append(BuildOutput(isHTML, prefix + "direction", TEST_DIRECTION));
//            text.Append(BuildOutput(isHTML, prefix + "qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, prefix + "value", TEST_VALUE));
//            text.Append(BuildOutput(isHTML, prefix + "link.type", "locator"));
//            text.Append(BuildOutput(isHTML, prefix + "link.href", TEST_VALUE));
//            text.Append(BuildOutput(isHTML, prefix + "link.role", "tank"));
//            text.Append(BuildOutput(isHTML, prefix + "link.title", "Tank Page"));
//            text.Append(BuildOutput(isHTML, prefix + "link.label", "tank"));
//            text.Append(BuildOutput(isHTML, prefix + "classification", "U"));
//            text.Append(BuildOutput(isHTML, prefix + "ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            if (version.isAtLeast("4.0.1")) {
//                xml.Append("<ddms:relatedResource ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ddms:relationship=\"http://purl.org/dc/terms/references\" ddms:direction=\"outbound\" ");
//                xml.Append("ddms:qualifier=\"http://purl.org/dc/terms/URI\" ddms:value=\"http://en.wikipedia.org/wiki/Tank\" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ");
//                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />\n");
//                xml.Append("</ddms:relatedResource>");
//            } else {
//                xml.Append("<ddms:relatedResources ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ddms:relationship=\"http://purl.org/dc/terms/references\" ddms:direction=\"outbound\" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t<ddms:RelatedResource ddms:qualifier=\"http://purl.org/dc/terms/URI\" ");
//                xml.Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
//                xml.Append("\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ");
//                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />\n");
//                xml.Append("\t</ddms:RelatedResource>\n");
//                xml.Append("</ddms:relatedResources>\n");
//            }
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RelatedResource.getName(version));
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
//                XElement element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                XElement innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields
//                GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

//                // No optional fields
//                GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, null, TEST_QUALIFIER, TEST_VALUE);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Missing relationship
//                XElement element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                XElement innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("relationship attribute is required.", element);

//                // Invalid direction
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                Util.addDDMSAttribute(element, "direction", "veeringLeft");
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("The direction attribute must be one of", element);

//                // Relationship not URI
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", INVALID_URI);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("Invalid URI", element);

//                // Missing qualifier
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("qualifier attribute is required.", element);

//                // qualifier not URI
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", INVALID_URI);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("Invalid URI", element);

//                // Missing value
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                innerElement.appendChild(new XElement(LinkTest.FixtureElement));
//                GetInstance("value attribute is required.", element);

//                // Missing link
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                GetInstance("At least 1 link must exist.", element);

//                // Security Attributes
//                element = Util.buildDDMSElement(RelatedResource.getName(version), null);
//                Util.addDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
//                innerElement = version.isAtLeast("4.0.1") ? element : Util.buildDDMSElement("RelatedResource", null);
//                if (!version.isAtLeast("4.0.1")) {
//                    element.appendChild(innerElement);
//                }
//                Util.addDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(innerElement, "value", TEST_VALUE);
//                Link link = new Link(XLinkAttributesTest.LocatorFixture, SecurityAttributesTest.Fixture);
//                innerElement.appendChild(link.XOMElementCopy);
//                GetInstance("Security attributes cannot be applied", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Missing relationship
//                GetInstance("relationship attribute is required.", LinkTest.GetLocatorFixtureList(false), null, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

//                // Invalid direction
//                GetInstance("The direction attribute must be one of", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, "veeringLeft", TEST_QUALIFIER, TEST_VALUE);

//                // Relationship not URI
//                GetInstance("Invalid URI", LinkTest.GetLocatorFixtureList(false), INVALID_URI, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

//                // Missing qualifier
//                GetInstance("qualifier attribute is required.", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, null, TEST_VALUE);

//                // Qualifier not URI
//                GetInstance("Invalid URI", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, INVALID_URI, TEST_VALUE);

//                // Missing value
//                GetInstance("value attribute is required.", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, null);

//                // Missing link
//                GetInstance("At least 1 link must exist.", null, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

//                // Security Attributes
//                try {
//                    Link link = new Link(XLinkAttributesTest.LocatorFixture, SecurityAttributesTest.Fixture);
//                    IList<Link> links = new List<Link>();
//                    links.Add(link);
//                    new RelatedResource(links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE, null);
//                    fail("Allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "Security attributes cannot be applied");
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No warnings
//                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Pre-DDMS 4.0.1, too many relatedResource children
//                if (!version.isAtLeast("4.0.1")) {
//                    XElement element = new XElement(GetValidElement(sVersion));
//                    XElement child = Util.buildDDMSElement("RelatedResource", null);
//                    child.addAttribute(Util.buildDDMSAttribute("qualifier", "ignoreMe"));
//                    child.addAttribute(Util.buildDDMSAttribute("value", "ignoreMe"));
//                    child.appendChild(new XElement(LinkTest.FixtureElement));
//                    element.appendChild(child);
//                    component = GetInstance(SUCCESS, element);
//                    assertEquals(1, component.ValidationWarnings.size());
//                    string text = "A ddms:RelatedResources element contains more than 1";
//                    string locator = "ddms:relatedResources";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                RelatedResource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                RelatedResource dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                RelatedResource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                RelatedResource dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), DIFFERENT_VALUE, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, "inbound", TEST_QUALIFIER, TEST_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, DIFFERENT_VALUE, TEST_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, DIFFERENT_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));

//                IList<Link> differentLinks = new List<Link>();
//                differentLinks.Add(new Link(XLinkAttributesTest.LocatorFixture));
//                differentLinks.Add(new Link(XLinkAttributesTest.LocatorFixture));
//                dataComponent = GetInstance(SUCCESS, differentLinks, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());

//                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testLinkReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestLinkReuse() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                IList<Link> links = LinkTest.GetLocatorFixtureList(false);
//                GetInstance(SUCCESS, links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//                GetInstance(SUCCESS, links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                RelatedResource.Builder builder = new RelatedResource.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                RelatedResource.Builder builder = new RelatedResource.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Qualifier = TEST_QUALIFIER;
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                RelatedResource.Builder builder = new RelatedResource.Builder();
//                builder.Qualifier = TEST_QUALIFIER;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "relationship attribute is required.");
//                }
//                builder.Relationship = TEST_RELATIONSHIP;
//                builder.Qualifier = TEST_QUALIFIER;
//                builder.Value = TEST_VALUE;
//                builder.Links.get(0).XLinkAttributes.Type = "locator";
//                builder.Links.get(0).XLinkAttributes.Href = "http://ddmsence.urizone.net/";
//                builder.Links.get(0).XLinkAttributes.Role = "role";
//                builder.commit();

//                // Skip empty Links
//                builder = new RelatedResource.Builder();
//                builder.Direction = TEST_DIRECTION;
//                builder.Relationship = TEST_RELATIONSHIP;
//                builder.Qualifier = TEST_QUALIFIER;
//                builder.Value = TEST_VALUE;
//                Link.Builder emptyBuilder = new Link.Builder();
//                Link.Builder fullBuilder = new Link.Builder();
//                fullBuilder.XLinkAttributes.Type = "locator";
//                fullBuilder.XLinkAttributes.Href = "http://ddmsence.urizone.net/";
//                fullBuilder.XLinkAttributes.Role = "role";
//                builder.Links.add(emptyBuilder);
//                builder.Links.add(fullBuilder);
//                assertEquals(1, builder.commit().Links.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                RelatedResource.Builder builder = new RelatedResource.Builder();
//                assertNotNull(builder.Links.get(1));
//            }
//        }
//    }

//}