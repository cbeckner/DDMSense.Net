using System.Collections.Generic;
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
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:subOrganization elements </para>
    ///
    /// <para> Because a ddms:subOrganization is a local component, we cannot load a valid document from a unit test data file.
    /// We have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class SubOrganizationTest : AbstractBaseTestCase
    {
        private const string TEST_VALUE = "PEO-GES";

        /// <summary>
        /// Constructor
        /// </summary>
        public SubOrganizationTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<SubOrganization> FixtureList
        {
            get
            {
                try
                {
                    if (!DDMSVersion.CurrentVersion.IsAtLeast("4.0.1"))
                    {
                        return (null);
                    }
                    List<SubOrganization> subOrgs = new List<SubOrganization>();
                    subOrgs.Add(new SubOrganization("sub1", SecurityAttributesTest.Fixture));
                    subOrgs.Add(new SubOrganization("sub2", SecurityAttributesTest.Fixture));
                    return (subOrgs);
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
        public static XElement FixtureElement
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;
                    XElement element = Util.BuildDDMSElement(SubOrganization.GetName(version), TEST_VALUE);
                    //element.Add(PropertyReader.GetPrefix("ddms"), version.Namespace);
                    //element.Add(PropertyReader.GetPrefix("ism"), version.IsmNamespace);
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
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private SubOrganization GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubOrganization component = null;
            try
            {
                component = new SubOrganization(element);
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
        /// <param name="value"> the value </param>
        /// <returns> a valid object </returns>
        private SubOrganization GetInstance(string message, string value)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            SubOrganization component = null;
            try
            {
                component = new SubOrganization(value, SecurityAttributesTest.Fixture);
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
        //ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "subOrganization", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "subOrganization.classification", "U"));
            text.Append(BuildOutput(isHTML, "subOrganization.ownerProducer", "USA"));
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
                xml.Append("<ddms:subOrganization ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append(TEST_VALUE);
                xml.Append("</ddms:subOrganization>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, SubOrganization.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GetInstance(SUCCESS, FixtureElement);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                GetInstance(SUCCESS, TEST_VALUE);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing child text
                XElement element = Util.BuildDDMSElement(SubOrganization.GetName(version), null);
                GetInstance("subOrganization value is required.", element);

                // Empty child text
                element = Util.BuildDDMSElement(SubOrganization.GetName(version), "");
                GetInstance("subOrganization value is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing child text
                GetInstance("subOrganization value is required.", (string)null);

                // Empty child text
                GetInstance("subOrganization value is required.", "");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization elementComponent = GetInstance(SUCCESS, FixtureElement);
                SubOrganization dataComponent = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization elementComponent = GetInstance(SUCCESS, FixtureElement);
                SubOrganization dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new SubOrganization(TEST_VALUE, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The subOrganization element cannot be used");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization component = GetInstance(SUCCESS, FixtureElement);
                SubOrganization.Builder builder = new SubOrganization.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization.Builder builder = new SubOrganization.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_SubOrganization_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                SubOrganization.Builder builder = new SubOrganization.Builder();
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