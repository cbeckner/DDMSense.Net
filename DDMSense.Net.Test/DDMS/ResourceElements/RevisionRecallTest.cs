using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace DDMSense.Test.DDMS.ResourceElements
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Link = DDMSense.DDMS.Summary.Link;
    using LinkTest = DDMSense.Test.DDMS.Summary.LinkTest;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;
    using XLinkAttributes = DDMSense.DDMS.Summary.Xlink.XLinkAttributes;
    using XLinkAttributesTest = DDMSense.Test.DDMS.Summary.Xlink.XLinkAttributesTest;

    /// <summary>
    /// <para> Tests related to ddms:revisionRecall elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class RevisionRecallTest : AbstractBaseTestCase
    {
        private static readonly int? TEST_REVISION_ID = Convert.ToInt32(1);
        private const string TEST_REVISION_TYPE = "ADMINISTRATIVE RECALL";
        private const string TEST_VALUE = "Description of Recall";
        private const string TEST_NETWORK = "NIPRNet";
        private const string TEST_OTHER_NETWORK = "PBS";

        /// <summary>
        /// Constructor
        /// </summary>
        public RevisionRecallTest()
            : base("revisionRecall.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static XElement TextFixtureElement
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                XElement element = new XElement((new RevisionRecallTest()).GetValidElement(version.Version));
                element.Elements().Remove();
                element.Add(TEST_VALUE);
                return (element);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static RevisionRecall TextFixture
        {
            get
            {
                try
                {
                    return (new RevisionRecall(TextFixtureElement));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private RevisionRecall GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RevisionRecall component = null;
            try
            {
                component = new RevisionRecall(element);
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
        /// <param name="links"> associated links (optional) </param>
        /// <param name="details"> associated details (optional) </param>
        /// <param name="revisionID"> integer ID for this recall (required) </param>
        /// <param name="revisionType"> type of revision (required) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
        private RevisionRecall GetInstance(string message, List<Link> links, List<Details> details, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RevisionRecall component = null;
            try
            {
                component = new RevisionRecall(links, details, revisionID, revisionType, network, otherNetwork, xlinkAttributes, SecurityAttributesTest.Fixture);
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
        /// <param name="value"> child text (optional) </param>
        /// <param name="revisionID"> integer ID for this recall (required) </param>
        /// <param name="revisionType"> type of revision (required) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
        private RevisionRecall GetInstance(string message, string value, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RevisionRecall component = null;
            try
            {
                component = new RevisionRecall(value, revisionID, revisionType, network, otherNetwork, xlinkAttributes, SecurityAttributesTest.Fixture);
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
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private String getExpectedOutput(boolean hasLinks, boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private string GetExpectedOutput(bool hasLinks, bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            if (!hasLinks)
            {
                text.Append(BuildOutput(isHTML, "revisionRecall", TEST_VALUE));
            }
            text.Append(BuildOutput(isHTML, "revisionRecall.revisionID", "1"));
            text.Append(BuildOutput(isHTML, "revisionRecall.revisionType", "ADMINISTRATIVE RECALL"));
            text.Append(BuildOutput(isHTML, "revisionRecall.network", "NIPRNet"));
            text.Append(BuildOutput(isHTML, "revisionRecall.otherNetwork", "PBS"));
            if (hasLinks)
            {
                text.Append(BuildOutput(isHTML, "revisionRecall.link.type", "locator"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.href", "http://en.wikipedia.org/wiki/Tank"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.role", "tank"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.title", "Tank Page"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.label", "tank"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.classification", "U"));
                text.Append(BuildOutput(isHTML, "revisionRecall.link.ownerProducer", "USA"));
                text.Append(BuildOutput(isHTML, "revisionRecall.details", "Details"));
                text.Append(BuildOutput(isHTML, "revisionRecall.details.classification", "U"));
                text.Append(BuildOutput(isHTML, "revisionRecall.details.ownerProducer", "USA"));
            }
            text.Append(XLinkAttributesTest.ResourceFixture.GetOutput(isHTML, "revisionRecall."));
            text.Append(SecurityAttributesTest.Fixture.GetOutput(isHTML, "revisionRecall."));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string GetExpectedXMLOutput(bool hasLinks)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:revisionRecall ").Append(XmlnsDDMS).Append(" xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
            xml.Append(XmlnsISM).Append(" ");
            xml.Append("ddms:revisionID=\"1\" ddms:revisionType=\"ADMINISTRATIVE RECALL\" ");
            xml.Append("network=\"NIPRNet\" otherNetwork=\"PBS\" ");
            xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");

            if (hasLinks)
            {
                xml.Append("<ddms:link xlink:type=\"locator\" xlink:href=\"http://en.wikipedia.org/wiki/Tank\" ");
                xml.Append("xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" />");
                xml.Append("<ddms:details ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Details</ddms:details>");
            }
            else
            {
                xml.Append(TEST_VALUE);
            }
            xml.Append("</ddms:revisionRecall>");

            return (xml.ToString());
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RevisionRecall.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields (links)
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // All fields (text)
                GetInstance(SUCCESS, TextFixtureElement);

                // No optional fields (links)
                XElement element = Util.BuildDDMSElement(RevisionRecall.GetName(version), null);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                element.Add(LinkTest.GetLocatorFixture(true).ElementCopy);
                GetInstance(SUCCESS, element);

                // No optional fields (text)
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), null);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields (links)
                GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

                // All fields (text)
                GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

                // No optional fields (links)
                GetInstance(SUCCESS, null, null, TEST_REVISION_ID, TEST_REVISION_TYPE, null, null, null);

                // No optional fields (text)
                GetInstance(SUCCESS, null, TEST_REVISION_ID, TEST_REVISION_TYPE, null, null, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong type of XLinkAttributes (locator)
                XElement element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                XLinkAttributesTest.LocatorFixture.AddTo(element);
                GetInstance("revision ID is required.", element);

                // Both text AND links/details, text first
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                element.Add(LinkTest.GetLocatorFixture(true).ElementCopy);
                GetInstance("A ddms:revisionRecall element cannot have both child text and nested elements.", element);

                // Both text AND links/details, text last
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), null);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                element.Add(LinkTest.GetLocatorFixture(true).ElementCopy);
                element.Add(TEST_VALUE);
                GetInstance("A ddms:revisionRecall element cannot have both child text and nested elements.", element);

                // Links without security attributes
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), null);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                Link link = new Link(XLinkAttributesTest.LocatorFixture, null);
                element.Add(link.ElementCopy);
                GetInstance("classification is required.", element);

                // Bad revisionID
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddDDMSAttribute(element, "revisionID", "one");
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("revision ID is required.", element);

                // Missing revisionID
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("revision ID is required.", element);

                // Missing revisionType
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("The revisionType attribute must be one of", element);

                // Bad revisionType
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", "MISTAKE");
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("The revisionType attribute must be one of", element);

                // Bad network
                element = Util.BuildDDMSElement(RevisionRecall.GetName(version), TEST_VALUE);
                Util.AddAttribute(element, "", "network", "", "PBS");
                Util.AddDDMSAttribute(element, "revisionID", TEST_REVISION_ID.ToString());
                Util.AddDDMSAttribute(element, "revisionType", TEST_REVISION_TYPE);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("The network attribute must be one of", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong type of XLinkAttributes (locator)
                GetInstance("The type attribute must have a fixed value", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.LocatorFixture);

                // Links without security attributes
                Link link = new Link(XLinkAttributesTest.LocatorFixture, null);
                List<Link> linkList = new List<Link>();
                linkList.Add(link);
                GetInstance("classification is required.", linkList, DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.LocatorFixture);

                // Missing revisionID
                GetInstance("revision ID is required.", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, null, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

                // Missing revisionType
                GetInstance("The revisionType attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, null, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

                // Bad revisionType
                GetInstance("The revisionType attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, "MISTAKE", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);

                // Bad network
                GetInstance("The network attribute must be one of", LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, "PBS", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void ResourceElements_RevisionRecall_Warnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void ResourceElements_RevisionRecall_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_ConstructorEquality()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // links
                RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RevisionRecall dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                expected.LoadXml(elementComponent.ToXML());
                actual.LoadXml(dataComponent.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // text
                elementComponent = GetInstance(SUCCESS, TextFixtureElement);
                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                expected.LoadXml(elementComponent.ToXML());
                actual.LoadXml(dataComponent.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // links
                RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RevisionRecall dataComponent = GetInstance(SUCCESS, null, DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), null, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, Convert.ToInt32(2), TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, "ADMINISTRATIVE REVISION", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, "DoD-Dist-B", XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                // text
                elementComponent = GetInstance(SUCCESS, TextFixtureElement);
                dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, Convert.ToInt32(2), TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, "ADMINISTRATIVE REVISION", TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, "DoD-Dist-B", XLinkAttributesTest.ResourceFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RevisionRecall elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // links
                RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(true, false), component.ToText());

                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(true, false), component.ToText());

                // text
                component = GetInstance(SUCCESS, TextFixtureElement);
                Assert.AreEqual(GetExpectedOutput(false, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                Assert.AreEqual(GetExpectedOutput(false, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // links
                expected.LoadXml(GetExpectedXMLOutput(true));
                RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, LinkTest.GetLocatorFixtureList(true), DetailsTest.FixtureList, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                // text
                expected.LoadXml(GetExpectedXMLOutput(false));
                component = GetInstance(SUCCESS, TextFixtureElement);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.ResourceFixture);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("4.0.1");
                XLinkAttributes attr = XLinkAttributesTest.SimpleFixture;
                DDMSVersion.SetCurrentVersion("2.0");
                new RevisionRecall(TEST_VALUE, TEST_REVISION_ID, TEST_REVISION_TYPE, TEST_NETWORK, TEST_OTHER_NETWORK, attr, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_BuilderEquality()
        {

            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);


                // Equality after Building (links)
                RevisionRecall component = GetInstance(SUCCESS, GetValidElement(sVersion));
                RevisionRecall.Builder builder = new RevisionRecall.Builder(component);
                expected.LoadXml(component.ToXML());
                actual.LoadXml(builder.Commit().ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                // Equality after Building (text)
                component = GetInstance(SUCCESS, TextFixtureElement);
                builder = new RevisionRecall.Builder(component);
                expected.LoadXml(component.ToXML());
                actual.LoadXml(builder.Commit().ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RevisionRecall.Builder builder = new RevisionRecall.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Links.Add(new Link.Builder());
                builder.Links[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);

                builder = new RevisionRecall.Builder();
                Assert.IsTrue(builder.Empty);
                builder.Details.Add(new Details.Builder());
                builder.Details[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RevisionRecall_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RevisionRecall.Builder builder = new RevisionRecall.Builder();
                builder.RevisionID = TEST_REVISION_ID;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The revisionType attribute must be one of");
                }
                builder.RevisionType = TEST_REVISION_TYPE;
                builder.Commit();
            }
        }
    }
}