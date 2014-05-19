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
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using System;

    /// <summary>
    /// <para> Tests related to ddms:geographicIdentifier elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class GeographicIdentifierTest : AbstractBaseTestCase
    {

        private static readonly List<string> TEST_NAMES = new List<string>();
        static GeographicIdentifierTest()
        {
            TEST_NAMES.Add("The White House");
            TEST_REGIONS.Add("Mid-Atlantic States");
        }

        private static readonly List<string> TEST_REGIONS = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public GeographicIdentifierTest()
            : base("geographicIdentifier.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing. This object will be based on a country code.
        /// </summary>
        public static GeographicIdentifier CountryCodeBasedFixture
        {
            get
            {
                try
                {
                    return new GeographicIdentifier(null, null, new CountryCode("urn:us:gov:ic:cvenum:irm:coverage:iso3166:trigraph:v1", "LAO"), null);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing. This object will be based on a facility ID
        /// </summary>
        public static GeographicIdentifier FacIdBasedFixture
        {
            get
            {
                try
                {
                    return (new GeographicIdentifier(FacilityIdentifierTest.Fixture));
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
        private GeographicIdentifier GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            GeographicIdentifier component = null;
            try
            {
                component = new GeographicIdentifier(element);
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
        /// <param name="names"> the names (optional) </param>
        /// <param name="regions"> the region names (optional) </param>
        /// <param name="countryCode"> the country code (optional) </param>
        /// <param name="subDivisionCode"> the subdivision code (optional, starting in DDMS 4.0.1) </param>
        /// <param name="facilityIdentifier"> the facility identifier (optional) </param>
        /// <returns> a valid object </returns>
        private GeographicIdentifier GetInstance(string message, List<string> names, List<string> regions, CountryCode countryCode, SubDivisionCode subDivisionCode, FacilityIdentifier facilityIdentifier)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            GeographicIdentifier component = null;
            try
            {
                if (facilityIdentifier != null)
                {
                    component = new GeographicIdentifier(facilityIdentifier);
                }
                else
                {
                    component = new GeographicIdentifier(names, regions, countryCode, subDivisionCode);
                }
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
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "geographicIdentifier.name", TEST_NAMES[0]));
            text.Append(BuildOutput(isHTML, "geographicIdentifier.region", TEST_REGIONS[0]));
            text.Append(CountryCodeTest.Fixture.GetOutput(isHTML, "geographicIdentifier.", ""));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(SubDivisionCodeTest.Fixture.GetOutput(isHTML, "geographicIdentifier.", ""));
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
            xml.Append("<ddms:geographicIdentifier ").Append(XmlnsDDMS).Append(">\n\t");
            xml.Append("<ddms:name>The White House</ddms:name>\n\t");
            xml.Append("<ddms:region>Mid-Atlantic States</ddms:region>\n\t");
            xml.Append("<ddms:countryCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append("\t<ddms:subDivisionCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
            }
            xml.Append("</ddms:geographicIdentifier>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, GeographicIdentifier.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string geoIdName = GeographicIdentifier.GetName(version);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(Util.BuildDDMSElement("name", TEST_NAMES[0]));
                GetInstance(SUCCESS, element);

                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(Util.BuildDDMSElement("region", TEST_REGIONS[0]));
                GetInstance(SUCCESS, element);

                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(CountryCodeTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);

                if (version.IsAtLeast("4.0.1"))
                {
                    element = Util.BuildDDMSElement(geoIdName, null);
                    element.Add(SubDivisionCodeTest.Fixture.ElementCopy);
                    GetInstance(SUCCESS, element);
                }

                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(FacilityIdentifierTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }


        [TestMethod]
        public virtual void Summary_GeographicIdentifier_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

                // All fields
                GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);

                // No optional fields
                GetInstance(SUCCESS, TEST_NAMES, null, null, null, null);
                GetInstance(SUCCESS, null, TEST_REGIONS, null, null, null);
                GetInstance(SUCCESS, null, null, CountryCodeTest.Fixture, null, null);
                if (version.IsAtLeast("4.0.1"))
                {
                    GetInstance(SUCCESS, null, null, null, subCode, null);
                }
                GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
            }
        }


        [TestMethod]
        public virtual void Summary_GeographicIdentifier_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string geoIdName = GeographicIdentifier.GetName(version);

                // At least 1 name, region, countryCode, or facilityIdentifier must exist.
                XElement element = Util.BuildDDMSElement(geoIdName, null);
                GetInstance("At least 1 of ", element);

                // No more than 1 countryCode
                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(CountryCodeTest.Fixture.ElementCopy);
                element.Add(CountryCodeTest.Fixture.ElementCopy);
                GetInstance("No more than 1 countryCode", element);

                // No more than 1 subDivisionCode
                if (version.IsAtLeast("4.0.1"))
                {
                    element = Util.BuildDDMSElement(geoIdName, null);
                    element.Add(SubDivisionCodeTest.Fixture.ElementCopy);
                    element.Add(SubDivisionCodeTest.Fixture.ElementCopy);
                    GetInstance("No more than 1 subDivisionCode", element);
                }

                // No more than 1 facilityIdentifier
                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(FacilityIdentifierTest.Fixture.ElementCopy);
                element.Add(FacilityIdentifierTest.Fixture.ElementCopy);
                GetInstance("No more than 1 facilityIdentifier", element);

                // facilityIdentifier must be alone
                element = Util.BuildDDMSElement(geoIdName, null);
                element.Add(CountryCodeTest.Fixture.ElementCopy);
                element.Add(FacilityIdentifierTest.Fixture.ElementCopy);
                GetInstance("facilityIdentifier cannot be used", element);
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // At least 1 name, region, countryCode, subDivisionCode or facilityIdentifier must exist.
                GetInstance("At least 1 of ", null, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

                GeographicIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GeographicIdentifier dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                XElement element = Util.BuildDDMSElement(GeographicIdentifier.GetName(version), null);
                element.Add(FacilityIdentifierTest.Fixture.ElementCopy);
                elementComponent = GetInstance(SUCCESS, element);
                dataComponent = GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

                GeographicIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                GeographicIdentifier dataComponent = GetInstance(SUCCESS, null, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAMES, null, CountryCodeTest.Fixture, subCode, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, subCode, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("4.0.1"))
                {
                    dataComponent = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, null, null);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }

                dataComponent = GetInstance(SUCCESS, null, null, null, null, FacilityIdentifierTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

                GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_HTMLFacIdOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GeographicIdentifier component = FacIdBasedFixture;
                StringBuilder facIdOutput = new StringBuilder();
                facIdOutput.Append("<meta name=\"geographicIdentifier.facilityIdentifier.beNumber\" content=\"1234DD56789\" />\n");
                facIdOutput.Append("<meta name=\"geographicIdentifier.facilityIdentifier.osuffix\" content=\"DD123\" />\n");
                Assert.AreEqual(facIdOutput.ToString(), component.ToHTML());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_TextFacIdOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GeographicIdentifier component = FacIdBasedFixture;
                StringBuilder facIdOutput = new StringBuilder();
                facIdOutput.Append("geographicIdentifier.facilityIdentifier.beNumber: 1234DD56789\n");
                facIdOutput.Append("geographicIdentifier.facilityIdentifier.osuffix: DD123\n");
                Assert.AreEqual(facIdOutput.ToString(), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SubDivisionCode subCode = SubDivisionCodeTest.Fixture;

                GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, CountryCodeTest.Fixture, subCode, null);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        
        public virtual void Summary_GeographicIdentifier_CountryCodeReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode code = CountryCodeTest.Fixture;
                GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, code, null, null);
                GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, code, null, null);
            }
        }

        
        public virtual void Summary_GeographicIdentifier_SubDivisionCodeReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                if (!version.IsAtLeast("4.0.1"))
                {
                    continue;
                }

                SubDivisionCode code = SubDivisionCodeTest.Fixture;
                GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, code, null);
                GetInstance(SUCCESS, TEST_NAMES, TEST_REGIONS, null, code, null);
            }
        }

        
        public virtual void Summary_GeographicIdentifier_FacilityIdentifierReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                FacilityIdentifier facId = FacilityIdentifierTest.Fixture;
                GetInstance(SUCCESS, null, null, null, null, facId);
                GetInstance(SUCCESS, null, null, null, null, facId);
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeographicIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // Equality after Building (CountryCode-based)
                GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // Equality after Building (FacID-based)
                FacilityIdentifier facId = FacilityIdentifierTest.Fixture;
                component = new GeographicIdentifier(facId);
                builder = new GeographicIdentifier.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Names = TEST_NAMES;
                Assert.IsFalse(builder.Empty);

            }
        }

        //TODO - Figure out how to run this test
        [TestMethod]
        public virtual void Summary_GeographicIdentifier_BuilderValidation()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
            //    builder.FacilityIdentifier.BeNumber = "1234DD56789";
            //    try
            //    {
            //        builder.Commit();
            //        Assert.Fail("Builder allowed invalid data.");
            //    }
            //    catch (InvalidDDMSException e)
            //    {
            //        ExpectMessage(e, "osuffix is required.");
            //    }
            //    builder.FacilityIdentifier.Osuffix = "osuffix";
            //    builder.Commit();

            //    // Non-FacID-based
            //    builder = new GeographicIdentifier.Builder();
            //    builder.Names.Add("Name");
            //    builder.Regions.Add("Region");
            //    CountryCode countryCode = CountryCodeTest.Fixture;
            //    builder.CountryCode.Qualifier = countryCode.Qualifier;
            //    builder.CountryCode.Value = countryCode.Value;
            //    builder.Commit();
            //}
        }

        [TestMethod]
        public virtual void Summary_GeographicIdentifier_BuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GeographicIdentifier.Builder builder = new GeographicIdentifier.Builder();
                Assert.IsNotNull(builder.Names[1]);
                Assert.IsNotNull(builder.Regions[1]);
            }
        }
    }

}