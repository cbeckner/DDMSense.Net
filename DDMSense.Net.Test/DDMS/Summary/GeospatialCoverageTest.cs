using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using PointTest = DDMSense.Test.DDMS.Summary.Gml.PointTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using DDMSense.DDMS;
    using System.Xml.Linq;

    /// <summary>
    /// <para> Tests related to ddms:geospatialCoverage elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
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
                return (new GeospatialCoverage(null, null, null, PostalAddressTest.Fixture, null, null, version.IsAtLeast("4.0.1") ? order : (int?)null, null));
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

        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, GeospatialCoverage.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        
        public virtual void TestElementConstructorValid()
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

        
        public virtual void TestDataConstructorValid()
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

        
        public virtual void TestElementConstructorInvalid()
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

        
        public virtual void TestDataConstructorInvalid()
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

        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string precedence = version.IsAtLeast("4.0.1") ? TEST_PRECEDENCE : null;
                int? order = version.IsAtLeast("4.0.1") ? TEST_ORDER : null;

                GeospatialCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GeospatialCoverage dataComponent = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // boundingBox
                XElement element = BuildComponentElement(BoundingBoxTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // boundingGeometry
                element = BuildComponentElement(BoundingGeometryTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // postalAddress
                element = BuildComponentElement(PostalAddressTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // verticalExtent
                element = BuildComponentElement(VerticalExtentTest.Fixture);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
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
        public virtual void TestHTMLTextOutput()
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
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                Assert.Equals(BoundingBoxTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.Equals(BoundingBoxTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                Assert.Equals(BoundingGeometryTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.Equals(BoundingGeometryTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                Assert.Equals(PostalAddressTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.Equals(PostalAddressTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());

                component = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                Assert.Equals(VerticalExtentTest.Fixture.GetOutput(true, prefix, "") + HtmlIcism, component.ToHTML());
                Assert.Equals(VerticalExtentTest.Fixture.GetOutput(false, prefix, "") + TextIcism, component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string precedence = version.IsAtLeast("4.0.1") ? TEST_PRECEDENCE : null;
                int? order = version.IsAtLeast("4.0.1") ? TEST_ORDER : null;

                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, precedence, order);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        
        public virtual void TestGeographicIdentifierReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GeographicIdentifier geoId = GeographicIdentifierTest.CountryCodeBasedFixture;
                GetInstance(SUCCESS, geoId, null, null, null, null, null, null);
                GetInstance(SUCCESS, geoId, null, null, null, null, null, null);
            }
        }

        
        public virtual void TestBoundingBoxReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingBox box = BoundingBoxTest.Fixture;
                GetInstance(SUCCESS, null, box, null, null, null, null, null);
                GetInstance(SUCCESS, null, box, null, null, null, null, null);
            }
        }

        
        public virtual void TestBoundingGeometryReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingGeometry geo = BoundingGeometryTest.Fixture;
                GetInstance(SUCCESS, null, null, geo, null, null, null, null);
                GetInstance(SUCCESS, null, null, geo, null, null, null, null);
            }
        }

        
        public virtual void TestPostalAddressReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                PostalAddress address = PostalAddressTest.Fixture;
                GetInstance(SUCCESS, null, null, null, address, null, null, null);
                GetInstance(SUCCESS, null, null, null, address, null, null, null);
            }
        }

        
        public virtual void TestVerticalExtentReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                VerticalExtent extent = VerticalExtentTest.Fixture;
                GetInstance(SUCCESS, null, null, null, null, extent, null, null);
                GetInstance(SUCCESS, null, null, null, null, extent, null, null);
            }
        }

        
        public virtual void TestSecurityAttributes()
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
                    Assert.Equals(attr, component.SecurityAttributes);
                }
            }
        }

        [TestMethod]
        public virtual void TestWrongVersionSecurityAttributes()
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
        public virtual void TestWrongVersionPrecedenceOrder()
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
        public virtual void TestPrecedenceRestrictions()
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
        public virtual void TestGetLocatorSuffix()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                GeospatialCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                string suffix = version.IsAtLeast("4.0.1") ? "" : "/ddms:GeospatialExtent";
                Assert.Equals(suffix, component.LocatorSuffix);
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeospatialCoverage component = GetInstance(SUCCESS, GeographicIdentifierTest.CountryCodeBasedFixture, null, null, null, null, null, null);
                GeospatialCoverage.Builder builder = new GeospatialCoverage.Builder(component);
                Assert.Equals(component, builder.Commit());

                component = GetInstance(SUCCESS, null, BoundingBoxTest.Fixture, null, null, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.Equals(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, BoundingGeometryTest.Fixture, null, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.Equals(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, null, PostalAddressTest.Fixture, null, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.Equals(component, builder.Commit());

                component = GetInstance(SUCCESS, null, null, null, null, VerticalExtentTest.Fixture, null, null);
                builder = new GeospatialCoverage.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
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
        public virtual void TestBuilderValidation()
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