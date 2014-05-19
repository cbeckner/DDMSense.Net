using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    using System;

    /// <summary>
    /// <para> Tests related to ddms:countryCode elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class CountryCodeTest : AbstractBaseTestCase
    {

        private const string TEST_QUALIFIER = "ISO-3166";
        private const string TEST_VALUE = "USA";

        /// <summary>
        /// Constructor
        /// </summary>
        public CountryCodeTest()
            : base("countryCode.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static CountryCode Fixture
        {
            get
            {
                try
                {
                    return (new CountryCode("ISO-3166", "USA"));
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
        private CountryCode GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            CountryCode component = null;
            try
            {
                component = new CountryCode(element);
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
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private CountryCode GetInstance(string message, string qualifier, string value)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            CountryCode component = null;
            try
            {
                component = new CountryCode(qualifier, value);
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
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "countryCode.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "countryCode.value", TEST_VALUE));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:countryCode ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, CountryCode.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string countryCodeName = CountryCode.GetName(version);
                // Missing qualifier
                XElement element = Util.BuildDDMSElement(countryCodeName, null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);

                // Empty qualifier
                element = Util.BuildDDMSElement(countryCodeName, null);
                Util.AddDDMSAttribute(element, "qualifier", "");
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);

                // Missing value
                element = Util.BuildDDMSElement(countryCodeName, null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                GetInstance("value attribute is required.", element);

                // Empty value
                element = Util.BuildDDMSElement(countryCodeName, null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(element, "value", "");
                GetInstance("value attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing qualifier
                GetInstance("qualifier attribute is required.", null, TEST_VALUE);

                // Empty qualifier
                GetInstance("qualifier attribute is required.", "", TEST_VALUE);

                // Missing value
                GetInstance("value attribute is required.", TEST_QUALIFIER, null);

                // Empty value
                GetInstance("value attribute is required.", TEST_QUALIFIER, "");
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                CountryCode dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                CountryCode dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                CountryCode.Builder builder = new CountryCode.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                CountryCode.Builder builder = new CountryCode.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void Summary_CountryCode_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                CountryCode.Builder builder = new CountryCode.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "qualifier attribute is required.");
                }
                builder.Qualifier = TEST_QUALIFIER;
                builder.Commit();
            }
        }
    }

}