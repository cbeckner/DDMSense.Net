using System.Text;
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
namespace DDMSense.Test.DDMS.ResourceElements
{


    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// <para> Tests related to ddms:applicationSoftware elements </para>
    /// 
    /// <para> Because a ddms:applicationSoftware is a local component, we cannot load a valid document from a unit test data
    /// file. We have to build the well-formed XElement ourselves. </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ApplicationSoftwareTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "IRM Generator 2L-9";

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationSoftwareTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
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
                    XElement element = Util.BuildDDMSElement(ApplicationSoftware.GetName(version), TEST_VALUE);
                    element.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + element.Name.LocalName;
                    element.Name = XName.Get(PropertyReader.GetPrefix("ism"), version.IsmNamespace) + element.Name.LocalName; 
                    SecurityAttributesTest.Fixture.AddTo(element);
                    return (element);
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
        public static ApplicationSoftware Fixture
        {
            get
            {
                try
                {
                    return (new ApplicationSoftware(FixtureElement));
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
        private ApplicationSoftware GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ApplicationSoftware component = null;
            try
            {
                component = new ApplicationSoftware(element);
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
        /// <param name="value"> the child text </param>
        /// <returns> a valid object </returns>
        private ApplicationSoftware GetInstance(string message, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ApplicationSoftware component = null;
            try
            {
                component = new ApplicationSoftware(value, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "applicationSoftware", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "applicationSoftware.classification", "U"));
            text.Append(BuildOutput(isHTML, "applicationSoftware.ownerProducer", "USA"));
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
                xml.Append("<ddms:applicationSoftware ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append(TEST_VALUE).Append("</ddms:applicationSoftware>");
                return (xml.ToString());
            }
        }


        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, ApplicationSoftware.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);

                // No optional fields
                XElement element = Util.BuildDDMSElement(ApplicationSoftware.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_DataConstructorValid()
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

       [TestMethod]
       public virtual void ResourceElements_ApplicationSoftware_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Bad security attributes
                XElement element = Util.BuildDDMSElement(ApplicationSoftware.GetName(version), null);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Bad security attributes
                try
                {
                    new ApplicationSoftware(TEST_VALUE, (SecurityAttributes)null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(0, component.ValidationWarnings.Count());

                // No value
                XElement element = Util.BuildDDMSElement(ApplicationSoftware.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count());
                string text = "A ddms:applicationSoftware element was found with no value.";
                string locator = "ddms:applicationSoftware";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware elementComponent = GetInstance(SUCCESS, FixtureElement);
                ApplicationSoftware dataComponent = GetInstance(SUCCESS, TEST_VALUE);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware elementComponent = GetInstance(SUCCESS, FixtureElement);
                ApplicationSoftware dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new ApplicationSoftware(TEST_VALUE, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The applicationSoftware element cannot be used");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware component = GetInstance(SUCCESS, FixtureElement);
                ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void ResourceElements_ApplicationSoftware_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApplicationSoftware.Builder builder = new ApplicationSoftware.Builder();
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