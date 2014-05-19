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
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to ddms:link elements </para>
    /// 
    /// <para> Because a ddms:link is a local component, we cannot load a valid document from a unit test data file. We have to
    /// build the well-formed XElement ourselves. </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class LinkTest : AbstractBaseTestCase
    {

        private const string TEST_TYPE = "locator";
        private const string TEST_HREF = "http://en.wikipedia.org/wiki/Tank";
        private const string TEST_ROLE = "tank";
        private const string TEST_TITLE = "Tank Page";
        private const string TEST_LABEL = "tank";

        /// <summary>
        /// Constructor
        /// </summary>
        public LinkTest()
            : base(null)
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static XElement FixtureElement
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;
                    XElement linkElement = Util.BuildDDMSElement(Link.GetName(version), null);
                    linkElement.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + linkElement.Name.LocalName;
                    XLinkAttributesTest.LocatorFixture.AddTo(linkElement);
                    return (linkElement);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="hasSecurity"> true for security attributes </param>
        public static List<Link> GetLocatorFixtureList(bool hasSecurity)
        {
            List<Link> links = new List<Link>();
            links.Add(GetLocatorFixture(hasSecurity));
            return (links);
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="hasSecurity"> true for security attributes </param>
        public static Link GetLocatorFixture(bool hasSecurity)
        {
            try
            {
                return (new Link(XLinkAttributesTest.LocatorFixture, hasSecurity ? SecurityAttributesTest.Fixture : null));
            }
            catch (InvalidDDMSException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                Assert.Fail("Could not create fixture.");
            }
            return (null);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private Link GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Link component = null;
            try
            {
                component = new Link(element);
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
        /// <param name="attributes"> the XLink Attributes </param>
        /// <returns> a valid object </returns>
        private Link GetInstance(string message, XLinkAttributes attributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Link component = null;
            try
            {
                component = new Link(attributes);
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
            StringBuilder text = new StringBuilder();
            text.Append(XLinkAttributesTest.LocatorFixture.GetOutput(isHTML, "link."));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:link ").Append(XmlnsDDMS).Append(" xmlns:xlink=\"").Append(version.XlinkNamespace).Append("\" ");
                xml.Append("xlink:type=\"locator\" xlink:href=\"http://en.wikipedia.org/wiki/Tank\" ");
                xml.Append("xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" />");
                return (xml.ToString());
            }
        }

        /// <summary>
        /// Helper method to create a XOM element that can be used to test element constructors
        /// </summary>
        /// <param name="type"> the type </param>
        /// <param name="href"> the href </param>
        /// <returns> XElement </returns>
        private XElement BuildComponentElement(string type, string href)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            XElement element = Util.BuildDDMSElement(Link.GetName(version), null);
            string xlinkPrefix = PropertyReader.GetPrefix("xlink");
            string xlinkNamespace = version.XlinkNamespace;
            if (type != null)
            {
                element.Add(Util.BuildAttribute(xlinkPrefix, "type", xlinkNamespace, type));
            }
            if (href != null)
            {
                element.Add(Util.BuildAttribute(xlinkPrefix, "href", xlinkNamespace, href));
            }
            element.Add(Util.BuildAttribute(xlinkPrefix, "role", xlinkNamespace, TEST_ROLE));
            return (element);
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, Link.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, FixtureElement);

                // No optional fields
                XElement element = BuildComponentElement(TEST_TYPE, TEST_HREF);
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
                GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);

                // No optional fields
                GetInstance(SUCCESS, new XLinkAttributes(TEST_HREF, null, null, null));
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing href
                XElement element = BuildComponentElement(TEST_TYPE, null);
                GetInstance("href attribute is required.", element);

                // invalid type
                element = BuildComponentElement("simple", TEST_HREF);
                GetInstance("The type attribute must have a fixed value", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing attributes
                GetInstance("type attribute is required.", (XLinkAttributes)null);

                // Missing href
                GetInstance("href attribute is required.", new XLinkAttributes("", TEST_ROLE, TEST_TITLE, TEST_LABEL));
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Link component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
                Link dataComponent = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
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
                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
                Link dataComponent = GetInstance(SUCCESS, new XLinkAttributes(DIFFERENT_VALUE, TEST_ROLE, TEST_TITLE, TEST_LABEL));
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Link elementComponent = GetInstance(SUCCESS, FixtureElement);
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Link component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
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
                Link component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, XLinkAttributesTest.LocatorFixture);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongLinkType()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GetInstance("The type attribute must have a fixed value", XLinkAttributesTest.SimpleFixture);
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Link component = GetInstance(SUCCESS, FixtureElement);
                Link.Builder builder = new Link.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Link.Builder builder = new Link.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.XLinkAttributes.Role = TEST_ROLE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Link.Builder builder = new Link.Builder();
                builder.XLinkAttributes.Role = TEST_ROLE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "type attribute is required.");
                }
                builder.XLinkAttributes.Type = "locator";
                builder.XLinkAttributes.Href = TEST_HREF;
                builder.Commit();
            }
        }
    }

}