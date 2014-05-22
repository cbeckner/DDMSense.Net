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



    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:postalAddress elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class PostalAddressTest : AbstractBaseTestCase
    {

        private static readonly List<string> TEST_STREETS = new List<string>() {"1600 Pennsylvania Avenue, NW"};
        private const string TEST_CITY = "Washington";
        private const string TEST_STATE = "DC";
        private const string TEST_PROVINCE = "Alberta";
        private const string TEST_POSTAL_CODE = "20500";

        /// <summary>
        /// Constructor
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public PostalAddressTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public PostalAddressTest()
            : base("postalAddress.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static PostalAddress Fixture
        {
            get
            {
                try
                {
                    return (new PostalAddress(null, null, "VA", null, null, true));
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
        private PostalAddress GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            PostalAddress component = null;
            try
            {
                component = new PostalAddress(element);
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
        /// <param name="streets"> the street address lines (0-6) </param>
        /// <param name="city"> the city (optional) </param>
        /// <param name="stateOrProvince"> the state or province (optional) </param>
        /// <param name="postalCode"> the postal code (optional) </param>
        /// <param name="countryCode"> the country code (optional) </param>
        /// <param name="hasState"> true if the stateOrProvince is a state, false if it is a province (only 1 of state or province
        /// can exist in a postalAddress) </param>
        /// <returns> a valid object </returns>
        private PostalAddress GetInstance(string message, List<string> streets, string city, string stateOrProvince, string postalCode, CountryCode countryCode, bool hasState)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            PostalAddress component = null;
            try
            {
                component = new PostalAddress(streets, city, stateOrProvince, postalCode, countryCode, hasState);
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
        private string GetExpectedOutput(bool isHTML, bool hasState)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "postalAddress.street", TEST_STREETS[0]));
            text.Append(BuildOutput(isHTML, "postalAddress.city", TEST_CITY));
            if (hasState)
            {
                text.Append(BuildOutput(isHTML, "postalAddress.state", TEST_STATE));
            }
            else
            {
                text.Append(BuildOutput(isHTML, "postalAddress.province", TEST_PROVINCE));
            }
            text.Append(BuildOutput(isHTML, "postalAddress.postalCode", TEST_POSTAL_CODE));
            text.Append(BuildOutput(isHTML, "postalAddress.countryCode.qualifier", "ISO-3166"));
            text.Append(BuildOutput(isHTML, "postalAddress.countryCode.value", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs.
        /// @boolean whether this address has a state or a province </param>
        private string GetExpectedXMLOutput(bool preserveFormatting, bool hasState)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:postalAddress ").Append(XmlnsDDMS).Append(">\n\t");
            xml.Append("<ddms:street>1600 Pennsylvania Avenue, NW</ddms:street>\n\t");
            xml.Append("<ddms:city>Washington</ddms:city>\n\t");
            if (hasState)
            {
                xml.Append("<ddms:state>DC</ddms:state>\n\t");
            }
            else
            {
                xml.Append("<ddms:province>Alberta</ddms:province>\n\t");
            }
            xml.Append("<ddms:postalCode>20500</ddms:postalCode>\n\t");
            xml.Append("<ddms:countryCode ddms:qualifier=\"ISO-3166\" ddms:value=\"USA\" />\n");
            xml.Append("</ddms:postalAddress>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, PostalAddress.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(PostalAddress.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);

                // All fields with a province
                GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);

                // No optional fields
                GetInstance(SUCCESS, null, null, null, null, null, false);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string postalName = PostalAddress.GetName(version);

                // Either a state or a province but not both.
                XElement element = Util.BuildDDMSElement(postalName, null);
                element.Add(Util.BuildDDMSElement("state", TEST_STATE));
                element.Add(Util.BuildDDMSElement("province", TEST_PROVINCE));
                GetInstance("Only 1 of state or province can be used.", element);

                // Too many streets
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 7; i++)
                {
                    element.Add(Util.BuildDDMSElement("street", "street" + i));
                }
                GetInstance("No more than 6 street elements can exist.", element);

                // Too many cities
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 2; i++)
                {
                    element.Add(Util.BuildDDMSElement("city", "city" + i));
                }
                GetInstance("No more than 1 city element can exist.", element);

                // Too many states
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 2; i++)
                {
                    element.Add(Util.BuildDDMSElement("state", "state" + i));
                }
                GetInstance("No more than 1 state element can exist.", element);

                // Too many provinces
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 2; i++)
                {
                    element.Add(Util.BuildDDMSElement("province", "province" + i));
                }
                GetInstance("No more than 1 province element can exist.", element);

                // Too many postalCodes
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 2; i++)
                {
                    element.Add(Util.BuildDDMSElement("postalCode", "postalCode" + i));
                }
                GetInstance("No more than 1 postalCode element can exist.", element);

                // Too many country codes
                element = Util.BuildDDMSElement(postalName, null);
                for (int i = 0; i < 2; i++)
                {
                    element.Add((new CountryCode("ISO-123" + i, "US" + i)).ElementCopy);
                }
                GetInstance("No more than 1 countryCode element can exist.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Too many streets
                List<string> streets = new List<string>();
                for (int i = 0; i < 7; i++)
                {
                    streets.Add("Street" + i);
                }
                GetInstance("No more than 6 street elements can exist.", streets, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Empty element
                XElement element = Util.BuildDDMSElement(PostalAddress.GetName(version), null);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A completely empty ddms:postalAddress element was found.";
                string locator = "ddms:postalAddress";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                PostalAddress elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                PostalAddress dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                PostalAddress dataComponent = GetInstance(SUCCESS, null, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_STREETS, null, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, null, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, null, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, null, CountryCodeTest.Fixture, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, null, true);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, true), component.ToText());

                component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, true), component.ToText());

                component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
                Assert.AreEqual(GetExpectedOutput(true, false), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true, true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, true);
                Assert.AreEqual(GetExpectedXMLOutput(false, true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_PROVINCE, TEST_POSTAL_CODE, CountryCodeTest.Fixture, false);
                Assert.AreEqual(GetExpectedXMLOutput(false, false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_CountryCodeReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode code = CountryCodeTest.Fixture;
                GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, code, true);
                GetInstance(SUCCESS, TEST_STREETS, TEST_CITY, TEST_STATE, TEST_POSTAL_CODE, code, true);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress component = GetInstance(SUCCESS, GetValidElement(sVersion));
                PostalAddress.Builder builder = new PostalAddress.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // No country code
                builder = new PostalAddress.Builder(component);
                builder.CountryCode = new CountryCode.Builder();
                PostalAddress address = builder.Commit() as PostalAddress;
                Assert.IsNull(address.CountryCode);

                // Country code
                CountryCode countryCode = CountryCodeTest.Fixture;
                builder = new PostalAddress.Builder();
                builder.CountryCode.Qualifier = countryCode.Qualifier;
                builder.CountryCode.Value = countryCode.Value;
                builder.Streets.Add("1600 Pennsylvania Avenue, NW");
                address = builder.Commit() as PostalAddress;
                Assert.AreEqual(countryCode, address.CountryCode);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress.Builder builder = new PostalAddress.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.City = TEST_CITY;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_PostalAddress_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                PostalAddress.Builder builder = new PostalAddress.Builder();
                builder.State = TEST_STATE;
                builder.Province = TEST_PROVINCE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "Only 1 of state or province can be used.");
                }
                builder.Province = "";
                builder.Commit();
            }
        }
    }

}