using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace DDMSense.Test.DDMS.Summary
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.Summary;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PointTest = DDMSense.Test.DDMS.Summary.Gml.PointTest;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:geospatialCoverage elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class GeospatialCoverageTest : AbstractBaseTestCase
    {
        private const string TEST_PRECEDENCE = "Primary";
        private static readonly int? TEST_ORDER = Convert.ToInt32(1);

        /// <summary>
        /// Constructor
        /// </summary>
        public GeospatialCoverageTest()
            : base("geospatialCoverage.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="a"> fixed order value </param>
        public static GeospatialCoverage GetFixture(int order)
        {
            try
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                return (new GeospatialCoverage(null, null, null, PostalAddressTest.Fixture, null, null, version.IsAtLeast("4.0.1") ? Convert.ToInt32(order) : (int?)null, null));
            }
            catch (InvalidDDMSException e)
            {
                Assert.Fail("Could not create fixture: " + e.Message);
            }
            return (null);
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static GeospatialCoverage Fixture
        {
            get
            {
                try
                {
                    return (new GeospatialCoverage(null, null, new BoundingGeometry(null, PointTest.FixtureList), null, null, null, null, null));
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
        private GeospatialCoverage GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            GeospatialCoverage component = null;
            try
            {
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    SecurityAttributesTest.Fixture.AddTo(element);
                }
                component = new GeospatialCoverage(element);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="geographicIdentifier"> an identifier (0-1 optional) </param>
        /// <param name="boundingBox"> a bounding box (0-1 optional) </param>
        /// <param name="boundingGeometry"> a set of bounding geometry (0-1 optional) </param>
        /// <param name="postalAddress"> an address (0-1 optional) </param>
        /// <param name="verticalExtent"> an extent (0-1 optional) </param>
        /// <param name="precedence"> the precedence value (optional, DDMS 4.0.1) </param>
        /// <param name="order"> the order value (optional, DDMS 4.0.1) </param>
        /// <returns> a valid object </returns>
        private GeospatialCoverage GetInstance(string message, GeographicIdentifier geographicIdentifier, BoundingBox boundingBox, BoundingGeometry boundingGeometry, PostalAddress postalAddress, VerticalExtent verticalExtent, string precedence, int? order)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            GeospatialCoverage component = null;
            try
            {
                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.IsAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
                component = new GeospatialCoverage(geographicIdentifier, boundingBox, boundingGeometry, postalAddress, verticalExtent, precedence, order, attr);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Returns the ISM attributes HTML output, if the DDMS Version supports it.
        /// </summary>
        private string HtmlIcism
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                string prefix = version.IsAtLeast("4.0.1") ? "geospatialCoverage." : "geospatialCoverage.GeospatialExtent.";
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    return (BuildOutput(true, prefix + "classification", "U") + BuildOutput(true, prefix + "ownerProducer", "USA"));
                }
                return ("");
            }
        }

        /// <summary>
        /// Returns the ISM attributes Text output, if the DDMS Version supports it.
        /// </summary>
        private string TextIcism
        {
            get
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                string prefix = version.IsAtLeast("4.0.1") ? "geospatialCoverage." : "geospatialCoverage.GeospatialExtent.";
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    return (BuildOutput(false, prefix + "classification", "U") + BuildOutput(false, prefix + "ownerProducer", "USA"));
                }
                return ("");
            }
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string prefix = version.IsAtLeast("4.0.1") ? "geospatialCoverage." : "geospatialCoverage.GeospatialExtent.";
            StringBuilder text = new StringBuilder();
            text.Append(GeographicIdentifierTest.CountryCodeBasedFixture.GetOutput(isHTML, prefix, ""));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, prefix + "precedence", "Primary"));
                text.Append(BuildOutput(isHTML, prefix + "order", "1"));
            }
            if (version.IsAtLeast("3.0"))
            {
                text.Append(SecurityAttributesTest.Fixture.GetOutput(isHTML, prefix));
            }
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
            xml.Append("<ddms:geospatialCoverage ").Append(XmlnsDDMS);
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ").Append(XmlnsISM).Append(" ");
                if (version.IsAtLeast("4.0.1"))
                {
                    xml.Append("ddms:precedence=\"Primary\" ddms:order=\"1\" ");
                }
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
            }
            xml.Append(">\n\t");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("<ddms:geographicIdentifier>\n\t\t");
                xml.Append("<ddms:countryCode ddms:qualifier=\"urn:us:gov:ic:cvenum:irm:coverage:iso3166:trigraph:v1\" ddms:value=\"LAO\" />\n\t");
                xml.Append("</ddms:geographicIdentifier>\n");
            }
            else
            {
                xml.Append("<ddms:GeospatialExtent>\n\t\t<ddms:geographicIdentifier>\n\t\t\t");
                xml.Append("<ddms:countryCode ddms:qualifier=\"urn:us:gov:ic:cvenum:irm:coverage:iso3166:trigraph:v1\" ddms:value=\"LAO\" />\n\t\t");
                xml.Append("</ddms:geographicIdentifier>\n\t</ddms:GeospatialExtent>\n");
            }
            xml.Append("</ddms:geospatialCoverage>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        /// <summary>
        /// Helper method to create a XOM element that can be used to test element constructors
        /// </summary>
        /// <param name="component"> the child of the GeospatialExtent </param>
        /// <returns> XElement </returns>
        private XElement BuildComponentElement(IDDMSComponent component)
        {
            List<IDDMSComponent> list = new List<IDDMSComponent>();
            if (component != null)
            {
                list.Add(component);
            }
            return (BuildComponentElement(list));
        }

        /// <summary>
        /// Helper method to create a XOM element that can be used to test element constructors
        /// </summary>
        /// <param name="components"> the children of the GeospatialExtent </param>
        /// <returns> XElement </returns>
        private XElement BuildComponentElement(List<IDDMSComponent> components)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            XElement element = Util.BuildDDMSElement(GeospatialCoverage.GetName(DDMSVersion.CurrentVersion), null);
            XElement extElement = version.IsAtLeast("4.0.1") ? element : Util.BuildDDMSElement("GeospatialExtent", null);
            foreach (IDDMSComponent component in components)
            {
                if (component != null)
                {
                    extElement.Add(component.ElementCopy);
                }
            }
            if (!version.IsAtLeast("4.0.1"))
            {
                element.Add(extElement);
            }
            return (element);
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, GeospatialCoverage.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // geographicIdentifier
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // boundingBox
                XElement element = BuildComponentElement(BoundingBoxTest.Fixture);
                GetInstance(SUCCESS, element);

                // boundingGeometry
                element = BuildComponentElement(BoundingGeometryTest.Fixture);
                GetInstance(SUCCESS, element);

                // postalAddress
                element = BuildComponentElement(PostalAddressTest.Fixture);
                GetInstance(SUCCESS, element);

                // verticalExtent
                element = BuildComponentElement(VerticalExtentTest.Fixture);
                GetInstance(SUCCESS, element);

                // everything
                List<IDDMSComponent> list = new List<IDDMSComponent>();
                list.Add(BoundingBoxTest.Fixture);
                list.Add(BoundingGeometryTest.Fixture);
                list.Add(PostalAddressTest.Fixture);
                list.Add(VerticalExtentTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // geographicIdentifier
                GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, null);

                // geographicIdentifier with DDMS 4.0.1 attributes
                if (version.IsAtLeast("4.0.1"))
                {
                    GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, TEST_PRECEDENCE, TEST_ORDER);
                }

                // boundingBox
                GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);

                // boundingGeometry
                GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);

                // postalAddress
                GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);

                // verticalExtent
                GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);

                // everything
                GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, BoundingGeometryTest.Fixture, PostalAddressTest.Fixture, VerticalExtentTest.Fixture, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // At least 1 of geographicIdentifier, boundingBox, boundingGeometry, postalAddress, or verticalExtent
                // must be used.
                XElement element = BuildComponentElement((IDDMSComponent)null);
                GetInstance("At least 1 of ", element);

                // Too many geographicIdentifier
                List<IDDMSComponent> list = new List<IDDMSComponent>();
                list.Add(GeographicIdentifierTest.CountryCodeBasedFixture);
                list.Add(GeographicIdentifierTest.CountryCodeBasedFixture);
                element = BuildComponentElement(list);
                GetInstance("No more than 1 geographicIdentifier", element);

                // Too many boundingBox
                list = new List<IDDMSComponent>();
                list.Add(BoundingBoxTest.Fixture);
                list.Add(BoundingBoxTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance("No more than 1 boundingBox", element);

                // Too many boundingGeometry
                list = new List<IDDMSComponent>();
                list.Add(BoundingGeometryTest.Fixture);
                list.Add(BoundingGeometryTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance("No more than 1 boundingGeometry", element);

                // Too many postalAddress
                list = new List<IDDMSComponent>();
                list.Add(PostalAddressTest.Fixture);
                list.Add(PostalAddressTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance("No more than 1 postalAddress", element);

                // Too many verticalExtent
                list = new List<IDDMSComponent>();
                list.Add(VerticalExtentTest.Fixture);
                list.Add(VerticalExtentTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance("No more than 1 verticalExtent", element);

                // If facilityIdentifier is used, nothing else can.
                list = new List<IDDMSComponent>();
                list.Add(GeographicIdentifierTest.FacIdBasedFixture);
                list.Add(VerticalExtentTest.Fixture);
                element = BuildComponentElement(list);
                GetInstance("A geographicIdentifier containing a facilityIdentifier", element);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // At least 1 of geographicIdentifier, boundingBox, boundingGeometry, postalAddress, or verticalExtent
                // must be used.
                GetInstance("At least 1 of ", null, null, null, null, null, null, null);

                // If facilityIdentifier is used, nothing else can.
                GetInstance("A geographicIdentifier containing a facilityIdentifier", GeographicIdentifierTest.FacIdBasedFixture, BoundingBoxTest.Fixture, null, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string precedence = version.IsAtLeast("4.0.1") ? TEST_PRECEDENCE : null;
                int? order = version.IsAtLeast("4.0.1") ? TEST_ORDER : null;

                GeospatialCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GeospatialCoverage dataComponent = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // boundingBox
                XElement element = BuildComponentElement(BoundingBoxTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // boundingGeometry
                element = BuildComponentElement(BoundingGeometryTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // postalAddress
                element = BuildComponentElement(PostalAddressTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // verticalExtent
                element = BuildComponentElement(VerticalExtentTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                GeospatialCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GeospatialCoverage dataComponent = null;
                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, TEST_PRECEDENCE, null);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));

                    dataComponent = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, TEST_ORDER);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
                dataComponent = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string prefix = "geospatialCoverage.";
                if (!version.IsAtLeast("4.0.1"))
                {
                    prefix += "GeospatialExtent.";
                }
                string precedence = version.IsAtLeast("4.0.1") ? TEST_PRECEDENCE : null;
                int? order = version.IsAtLeast("4.0.1") ? TEST_ORDER : null;

                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.AreEqual(BoundingBoxTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.AreEqual(BoundingBoxTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                Assert.AreEqual(BoundingGeometryTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.AreEqual(BoundingGeometryTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                Assert.AreEqual(PostalAddressTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.AreEqual(PostalAddressTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                Assert.AreEqual(VerticalExtentTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.AreEqual(VerticalExtentTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string precedence = version.IsAtLeast("4.0.1") ? TEST_PRECEDENCE : null;
                int? order = version.IsAtLeast("4.0.1") ? TEST_ORDER : null;

                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        public virtual void Summary_GeospatialCoverage_GeographicIdentifierReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GeographicIdentifier geoId = GeographicIdentifierTest.CountryCodeBasedFixture;
                GetInstance(SUCCESS, geoId, null, null, null, null, null, null);
                GetInstance(SUCCESS, geoId, null, null, null, null, null, null);
            }
        }

        public virtual void Summary_GeospatialCoverage_BoundingBoxReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox box = BoundingBoxTest.Fixture;
                GetInstance(SUCCESS, null, box, null, null, null, null, null);
                GetInstance(SUCCESS, null, box, null, null, null, null, null);
            }
        }

        public virtual void Summary_GeospatialCoverage_BoundingGeometryReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingGeometry geo = BoundingGeometryTest.Fixture;
                GetInstance(SUCCESS, null, null, geo, null, null, null, null);
                GetInstance(SUCCESS, null, null, geo, null, null, null, null);
            }
        }

        public virtual void Summary_GeospatialCoverage_PostalAddressReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                PostalAddress address = PostalAddressTest.Fixture;
                GetInstance(SUCCESS, null, null, null, address, null, null, null);
                GetInstance(SUCCESS, null, null, null, address, null, null, null);
            }
        }

        public virtual void Summary_GeospatialCoverage_VerticalExtentReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent extent = VerticalExtentTest.Fixture;
                GetInstance(SUCCESS, null, null, null, null, extent, null, null);
                GetInstance(SUCCESS, null, null, null, null, extent, null, null);
            }
        }

        public virtual void Summary_GeospatialCoverage_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                GeospatialCoverage component = new GeospatialCoverage(GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, null, attr);
                if (!version.IsAtLeast("3.0"))
                {
                    Assert.IsTrue(component.SecurityAttributes.Empty);
                }
                else
                {
                    Assert.AreEqual(attr, component.SecurityAttributes);
                }
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new GeospatialCoverage(GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, null, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_WrongVersionPrecedenceOrder()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new GeospatialCoverage(GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, TEST_PRECEDENCE, null, null);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The ddms:precedence attribute cannot be used");
            }
            try
            {
                new GeospatialCoverage(GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, TEST_ORDER, null);
                Assert.Fail("Allowed different versions.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The ddms:order attribute cannot be used");
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_PrecedenceRestrictions()
        {
            DDMSVersion.SetCurrentVersion("4.0.1");
            try
            {
                new GeospatialCoverage(GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, "Tertiary", null, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The ddms:precedence attribute must have a value from");
            }
            try
            {
                new GeospatialCoverage(null, BoundingBoxTest.Fixture, null, null, null, TEST_PRECEDENCE, null, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The ddms:precedence attribute should only be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_GetLocatorSuffix()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                string suffix = version.IsAtLeast("4.0.1") ? "" : "/ddms:GeospatialExtent";

                PrivateObject po = new PrivateObject(component, new PrivateType(typeof(GeospatialCoverage)));
                Assert.AreEqual(suffix, po.GetFieldOrProperty("LocatorSuffix").ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeospatialCoverage component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, null);
                GeospatialCoverage.Builder builder = new GeospatialCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                component = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeospatialCoverage.Builder builder = new GeospatialCoverage.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Order = TEST_ORDER;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_GeospatialCoverage_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeospatialCoverage.Builder builder = new GeospatialCoverage.Builder();
                builder.VerticalExtent.Datum = "AGL";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "A ddms:verticalExtent requires ");
                }
                builder.VerticalExtent.UnitOfMeasure = "Fathom";
                builder.VerticalExtent.MaxVerticalExtent = Convert.ToDouble(2);
                builder.VerticalExtent.MinVerticalExtent = Convert.ToDouble(1);
                builder.Commit();
            }
        }
    }
}