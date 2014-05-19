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


    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using System;

    /// <summary>
    /// <para> Tests related to ddms:description elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class DescriptionTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "A transformation service.";

        /// <summary>
        /// Constructor
        /// </summary>
        public DescriptionTest()
            : base("description.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Description Fixture
        {
            get
            {
                try
                {
                    return (new Description(TEST_VALUE, SecurityAttributesTest.Fixture));
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
        private Description GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Description component = null;
            try
            {
                component = new Description(element);
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
        /// <param name="value"> the description </param>
        /// <returns> a valid object </returns>
        private Description GetInstance(string message, string value)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Description component = null;
            try
            {
                component = new Description(value, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "description", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "description.classification", "U"));
            text.Append(BuildOutput(isHTML, "description.ownerProducer", "USA"));
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
                xml.Append("<ddms:description ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append(TEST_VALUE).Append("</ddms:description>");
                return (xml.ToString());
            }
        }

        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Description.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Description.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance(SUCCESS, element);
            }
        }

        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_VALUE);

                // No optional fields
                GetInstance(SUCCESS, "");
            }
        }

        
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing security attributes
                XElement element = Util.BuildDDMSElement(Description.GetName(version), null);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing security attributes
                try
                {
                    new Description(TEST_VALUE, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
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
                Description component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());

                // No value
                XElement element = Util.BuildDDMSElement(Description.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count());
                string text = "A ddms:description element was found with no description value.";
                string locator = "ddms:description";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Description elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Description dataComponent = GetInstance(SUCCESS, TEST_VALUE);
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
                Description elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Description dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Description component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE);
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
                Description component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Description component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Description.Builder builder = new Description.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Description.Builder builder = new Description.Builder();
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

                Description.Builder builder = new Description.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }

}