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
    /// <para> Tests related to ddms:subDivisionCode elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class SubDivisionCodeTest : AbstractBaseTestCase
    {

        private const string TEST_QUALIFIER = "ISO-3166";
        private const string TEST_VALUE = "USA";

        /// <summary>
        /// Constructor
        /// </summary>
        public SubDivisionCodeTest()
            : base("subDivisionCode.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static SubDivisionCode Fixture
        {
            get
            {
                try
                {
                    return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? new SubDivisionCode("ISO-3166", "USA") : null);
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
        private SubDivisionCode GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubDivisionCode component = null;
            try
            {
                component = new SubDivisionCode(element);
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
        /// <param name="qualifier"> the qualifier value </param>
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private SubDivisionCode GetInstance(string message, string qualifier, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubDivisionCode component = null;
            try
            {
                component = new SubDivisionCode(qualifier, value);
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
            text.Append(BuildOutput(isHTML, "subDivisionCode.qualifier", TEST_QUALIFIER));
            text.Append(BuildOutput(isHTML, "subDivisionCode.value", TEST_VALUE));
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
                xml.Append("<ddms:subDivisionCode ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, SubDivisionCode.GetName(version));
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

                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string subCode = SubDivisionCode.GetName(version);

                // Missing qualifier
                XElement element = Util.BuildDDMSElement(subCode, null);
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);

                // Empty qualifier
                element = Util.BuildDDMSElement(subCode, null);
                Util.AddDDMSAttribute(element, "qualifier", "");
                Util.AddDDMSAttribute(element, "value", TEST_VALUE);
                GetInstance("qualifier attribute is required.", element);

                // Missing value
                element = Util.BuildDDMSElement(subCode, null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                GetInstance("value attribute is required.", element);

                // Empty value
                element = Util.BuildDDMSElement(subCode, null);
                Util.AddDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
                Util.AddDDMSAttribute(element, "value", "");
                GetInstance("value attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
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
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                SubDivisionCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubDivisionCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubDivisionCode dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
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

                SubDivisionCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubDivisionCode dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubDivisionCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
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

                SubDivisionCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new SubDivisionCode(TEST_QUALIFIER, TEST_VALUE);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The subDivisionCode element cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubDivisionCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
                SubDivisionCode.Builder builder = new SubDivisionCode.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubDivisionCode.Builder builder = new SubDivisionCode.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubDivisionCode.Builder builder = new SubDivisionCode.Builder();
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