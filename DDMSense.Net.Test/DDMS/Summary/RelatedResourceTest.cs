using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

/* Copyright 2010 - 2013 by Brian Uri!
   
   This file is part of DDMSence.
   
   This library is free software; you can redistribute it and/or modify
   it under the terms of version 3.0 of the GNU Lesser General Public 
   License as published by the Free Software Foundation.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
   GNU Lesser General Public License for more details.
   
   You should have received a copy of the GNU Lesser General Public 
   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

   You can contact the author at ddmsence@urizone.net. The DDMSence
   home page is located at http://ddmsence.urizone.net/
 */
namespace DDMSense.Test.DDMS.Summary
{



    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using XLinkAttributes = DDMSense.DDMS.Summary.Xlink.XLinkAttributes;
    using XLinkAttributesTest = DDMSense.Test.DDMS.Summary.Xlink.XLinkAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to DDMS 2.0, 3.0, 3.1 ddms:RelatedResources elements or DDMS 4.0.1 ddms:relatedResource elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class RelatedResourceTest : AbstractBaseTestCase
    {

        private const string TEST_RELATIONSHIP = "http://purl.org/dc/terms/references";
        private const string TEST_DIRECTION = "outbound";
        private const string TEST_QUALIFIER = "http://purl.org/dc/terms/URI";
        private const string TEST_VALUE = "http://en.wikipedia.org/wiki/Tank";

        /// <summary>
        /// Constructor
        /// </summary>
        public RelatedResourceTest()
            : base("relatedResources.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static RelatedResource Fixture
        {
            get
            {
                try
                {
                    List<Link> links = new List<Link>();
                    links.Add(new Link(new XLinkAttributes("http://en.wikipedia.org/wiki/Tank", "role", null, null)));
                    return (new RelatedResource(links, "http://purl.org/dc/terms/references", "outbound", "http://purl.org/dc/terms/URI", "http://en.wikipedia.org/wiki/Tank", null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private RelatedResource GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RelatedResource component = null;
            try
            {
                component = new RelatedResource(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="links"> a list of links </param>
        /// <param name="relationship"> the relationship attribute (required) </param>
        /// <param name="direction"> the relationship direction (optional) </param>
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private RelatedResource GetInstance(string message, List<Link> links, string relationship, string direction, string qualifier, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RelatedResource component = null;
            try
            {
                component = new RelatedResource(links, relationship, direction, qualifier, value, SecurityAttributesTest.Fixture);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            string prefix = DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? "relatedResource." : "relatedResources.RelatedResource.";
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, prefix + "relationship", TEST_RELATIONSHIP));
            text.Append(BuildOutput(isHTML, prefix + "direction", TEST_DIRECTION));
            text.Append(BuildOutput(isHTML, prefix + "qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, prefix + "value", TEST_VALUE));
            text.Append(BuildOutput(isHTML, prefix + "link.type", "locator"));
            text.Append(BuildOutput(isHTML, prefix + "link.href", TEST_VALUE));
            text.Append(BuildOutput(isHTML, prefix + "link.role", "tank"));
            text.Append(BuildOutput(isHTML, prefix + "link.title", "Tank Page"));
            text.Append(BuildOutput(isHTML, prefix + "link.label", "tank"));
            text.Append(BuildOutput(isHTML, prefix + "classification", "U"));
            text.Append(BuildOutput(isHTML, prefix + "ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("<ddms:relatedResource ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ddms:relationship=\"http://purl.org/dc/terms/references\" ddms:direction=\"outbound\" ");
                xml.Append("ddms:qualifier=\"http://purl.org/dc/terms/URI\" ddms:value=\"http://en.wikipedia.org/wiki/Tank\" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
                xml.Append("\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ");
                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />\n");
                xml.Append("</ddms:relatedResource>");
            }
            else
            {
                xml.Append("<ddms:relatedResources ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ddms:relationship=\"http://purl.org/dc/terms/references\" ddms:direction=\"outbound\" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
                xml.Append("\t<ddms:RelatedResource ddms:qualifier=\"http://purl.org/dc/terms/URI\" ");
                xml.Append("ddms:value=\"http://en.wikipedia.org/wiki/Tank\">\n");
                xml.Append("\t\t<ddms:link xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:type=\"locator\" ");
                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />\n");
                xml.Append("\t</ddms:RelatedResource>\n");
                xml.Append("</ddms:relatedResources>\n");
            }
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RelatedResource.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                XElement innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

                // No optional fields
                GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, null, TEST_QUALIFIER, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing relationship
                XElement element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                XElement innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("relationship attribute is required.", element);

                // Invalid direction
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                Util.AddDDMSAttribute(element, "direction", "veeringLeft");
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("The direction attribute must be one of", element);

                // Relationship not URI
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", INVALID_URI);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("Invalid URI", element);

                // Missing qualifier
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("qualifier attribute is required.", element);

                // qualifier not URI
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", INVALID_URI);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("Invalid URI", element);

                // Missing value
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                innerElement.Add(new XElement(LinkTest.FixtureElement));
                GetInstance("value attribute is required.", element);

                // Missing link
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                GetInstance("At least 1 link must exist.", element);

                // Security Attributes
                element = Util.BuildDDMSElement(RelatedResource.GetName(version), null);
                Util.AddDDMSAttribute(element, "relationship", TEST_RELATIONSHIP);
                innerElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("RelatedResource", null);
                if (!version.IsAtLeast("4.0.1"))
                {
                    element.Add(innerElement);
                }
                Util.AddDDMSAttribute(innerElement, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(innerElement, "value", TEST_VALUE);
                Link link = new Link(XLinkAttributesTest.LocatorFixture, SecurityAttributesTest.Fixture);
                innerElement.Add(link.ElementCopy);
                GetInstance("Security attributes cannot be applied", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing relationship
                GetInstance("relationship attribute is required.", LinkTest.GetLocatorFixtureList(false), null, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

                // Invalid direction
                GetInstance("The direction attribute must be one of", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, "veeringLeft", TEST_QUALIFIER, TEST_VALUE);

                // Relationship not URI
                GetInstance("Invalid URI", LinkTest.GetLocatorFixtureList(false), INVALID_URI, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

                // Missing qualifier
                GetInstance("qualifier attribute is required.", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, null, TEST_VALUE);

                // Qualifier not URI
                GetInstance("Invalid URI", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, INVALID_URI, TEST_VALUE);

                // Missing value
                GetInstance("value attribute is required.", LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, null);

                // Missing link
                GetInstance("At least 1 link must exist.", null, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);

                // Security Attributes
                try
                {
                    Link link = new Link(XLinkAttributesTest.LocatorFixture, SecurityAttributesTest.Fixture);
                    List<Link> links = new List<Link>();
                    links.Add(link);
                    new RelatedResource(links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "Security attributes cannot be applied");
                }
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());

                // Pre-DDMS 4.0.1, too many relatedResource children
                if (!version.IsAtLeast("4.0.1"))
                {
                    XElement element = new XElement(GetValidElement(sVersion));
                    XElement child = Util.BuildDDMSElement("RelatedResource", null);
                    child.Add(Util.BuildDDMSAttribute("qualifier", "ignoreMe"));
                    child.Add(Util.BuildDDMSAttribute("value", "ignoreMe"));
                    child.Add(new XElement(LinkTest.FixtureElement));
                    element.Add(child);
                    component = GetInstance(SUCCESS, element);
                    Assert.Equals(1, component.ValidationWarnings.Count());
                    string text = "A ddms:RelatedResources element contains more than 1";
                    string locator = "ddms:relatedResources";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                RelatedResource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RelatedResource dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                RelatedResource elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RelatedResource dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), DIFFERENT_VALUE, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, "inbound", TEST_QUALIFIER, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, DIFFERENT_VALUE, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<Link> differentLinks = new List<Link>();
                differentLinks.Add(new Link(XLinkAttributesTest.LocatorFixture));
                differentLinks.Add(new Link(XLinkAttributesTest.LocatorFixture));
                dataComponent = GetInstance(SUCCESS, differentLinks, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());

                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(false), TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());

            }
        }

        [TestMethod]
        public virtual void TestLinkReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<Link> links = LinkTest.GetLocatorFixtureList(false);
                GetInstance(SUCCESS, links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
                GetInstance(SUCCESS, links, TEST_RELATIONSHIP, TEST_DIRECTION, TEST_QUALIFIER, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RelatedResource component = GetInstance(SUCCESS, GetValidElement(sVersion));
                RelatedResource.Builder builder = new RelatedResource.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RelatedResource.Builder builder = new RelatedResource.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Qualifier = TEST_QUALIFIER;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RelatedResource.Builder builder = new RelatedResource.Builder();
                builder.Qualifier = TEST_QUALIFIER;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "relationship attribute is required.");
                }
                builder.Relationship = TEST_RELATIONSHIP;
                builder.Qualifier = TEST_QUALIFIER;
                builder.Value = TEST_VALUE;
                builder.Links[0].XLinkAttributes.Type = "locator";
                builder.Links[0].XLinkAttributes.Href = "http://ddmsence.urizone.net/";
                builder.Links[0].XLinkAttributes.Role = "role";
                builder.Commit();

                // Skip empty Links
                builder = new RelatedResource.Builder();
                builder.Direction = TEST_DIRECTION;
                builder.Relationship = TEST_RELATIONSHIP;
                builder.Qualifier = TEST_QUALIFIER;
                builder.Value = TEST_VALUE;
                Link.Builder emptyBuilder = new Link.Builder();
                Link.Builder fullBuilder = new Link.Builder();
                fullBuilder.XLinkAttributes.Type = "locator";
                fullBuilder.XLinkAttributes.Href = "http://ddmsence.urizone.net/";
                fullBuilder.XLinkAttributes.Role = "role";
                builder.Links.Add(emptyBuilder);
                builder.Links.Add(fullBuilder);
                Assert.Equals(1, ((RelatedResource)builder.Commit()).Links.Count());
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                RelatedResource.Builder builder = new RelatedResource.Builder();
                Assert.IsNotNull(builder.Links[1]);
            }
        }
    }

}