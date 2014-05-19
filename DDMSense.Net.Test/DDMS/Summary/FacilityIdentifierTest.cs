using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary;
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:facilityIdentifier elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class FacilityIdentifierTest : AbstractBaseTestCase
    {
        private const string TEST_BENUMBER = "1234DD56789";
        private const string TEST_OSUFFIX = "DD123";

        /// <summary>
        /// Constructor
        /// </summary>
        public FacilityIdentifierTest()
            : base("facilityIdentifier.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static FacilityIdentifier Fixture
        {
            get
            {
                try
                {
                    return (new FacilityIdentifier("1234DD56789", "DD123"));
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
        private FacilityIdentifier GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            FacilityIdentifier component = null;
            try
            {
                component = new FacilityIdentifier(element);
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
        /// <param name="beNumber"> the beNumber (required) </param>
        /// <param name="osuffix"> the Osuffix (required, because beNumber is required) </param>
        /// <returns> a valid object </returns>
        private FacilityIdentifier GetInstance(string message, string beNumber, string osuffix)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            FacilityIdentifier component = null;
            try
            {
                component = new FacilityIdentifier(beNumber, osuffix);
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
            text.Append(BuildOutput(isHTML, "facilityIdentifier.beNumber", TEST_BENUMBER));
            text.Append(BuildOutput(isHTML, "facilityIdentifier.osuffix", TEST_OSUFFIX));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:facilityIdentifier ").Append(XmlnsDDMS).Append(" ");
            xml.Append("ddms:beNumber=\"").Append(TEST_BENUMBER).Append("\" ");
            xml.Append("ddms:osuffix=\"").Append(TEST_OSUFFIX).Append("\" />");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, FacilityIdentifier.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Missing beNumber
                XElement element = Util.BuildDDMSElement(FacilityIdentifier.GetName(version), null);
                Util.AddDDMSAttribute(element, "osuffix", TEST_OSUFFIX);
                GetInstance("beNumber is required.", element);

                // Empty beNumber
                element = Util.BuildDDMSElement(FacilityIdentifier.GetName(version), null);
                Util.AddDDMSAttribute(element, "beNumber", "");
                Util.AddDDMSAttribute(element, "osuffix", TEST_OSUFFIX);
                GetInstance("beNumber is required.", element);

                // Missing osuffix
                element = Util.BuildDDMSElement(FacilityIdentifier.GetName(version), null);
                Util.AddDDMSAttribute(element, "beNumber", TEST_BENUMBER);
                GetInstance("osuffix is required.", element);

                // Empty osuffix
                element = Util.BuildDDMSElement(FacilityIdentifier.GetName(version), null);
                Util.AddDDMSAttribute(element, "beNumber", TEST_BENUMBER);
                Util.AddDDMSAttribute(element, "osuffix", "");
                GetInstance("osuffix is required.", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing beNumber
                GetInstance("beNumber is required.", null, TEST_OSUFFIX);

                // Empty beNumber
                GetInstance("beNumber is required.", "", TEST_OSUFFIX);

                // Missing osuffix
                GetInstance("osuffix is required.", TEST_BENUMBER, null);

                // Empty osuffix
                GetInstance("osuffix is required.", TEST_BENUMBER, "");
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                FacilityIdentifier dataComponent = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
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
                FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                FacilityIdentifier dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_OSUFFIX);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_BENUMBER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                FacilityIdentifier elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
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
                FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
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
                FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_BENUMBER, TEST_OSUFFIX);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                FacilityIdentifier component = GetInstance(SUCCESS, GetValidElement(sVersion));
                FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        //TODO - Find out how to run this test
        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder();
            //    Assert.IsNull(builder.Commit());
            //    Assert.IsTrue(builder.Empty);
            //    builder.BeNumber = TEST_BENUMBER;
            //    Assert.IsFalse(builder.Empty);

            //}
        }

        //TODO - Find out how to run this test
        public virtual void TestBuilderValidation()
        {
            //foreach (string sVersion in SupportedVersions)
            //{
            //    DDMSVersion.SetCurrentVersion(sVersion);

            //    FacilityIdentifier.Builder builder = new FacilityIdentifier.Builder();
            //    builder.BeNumber = TEST_BENUMBER;
            //    try
            //    {
            //        builder.Commit();
            //        fail("Builder allowed invalid data.");
            //    }
            //    catch (InvalidDDMSException e)
            //    {
            //        ExpectMessage(e, "osuffix is required.");
            //    }
            //    builder.Osuffix = TEST_OSUFFIX;
            //    builder.Commit();
            //}
        }
    }

}