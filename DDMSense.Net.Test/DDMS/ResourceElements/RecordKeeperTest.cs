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
namespace DDMSense.Test.DDMS.ResourceElements
{


    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:recordKeeper elements </para>
    /// 
    /// <para> Because a ddms:recordKeeper is a local component, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class RecordKeeperTest : AbstractBaseTestCase
    {

        private new const string TEST_ID = "#289-99202.9";
        private const string TEST_NAME = "DISA";

        /// <summary>
        /// Constructor
        /// </summary>
        public RecordKeeperTest()
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
                DDMSVersion version = DDMSVersion.CurrentVersion;
                XElement element = Util.BuildDDMSElement(RecordKeeper.GetName(version), null);
                element.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + element.Name.LocalName;
                XElement idElement = Util.BuildDDMSElement("recordKeeperID", TEST_ID);
                element.Add(idElement);
                element.Add(OrganizationTest.Fixture.ElementCopy);
                return (element);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static RecordKeeper Fixture
        {
            get
            {
                try
                {
                    return (new RecordKeeper(FixtureElement));
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
        private RecordKeeper GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RecordKeeper component = null;
            try
            {
                component = new RecordKeeper(element);
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
        /// <param name="recordKeeperID"> ID value </param>
        /// <param name="org"> the organization </param>
        private RecordKeeper GetInstance(string message, string recordKeeperID, Organization org)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RecordKeeper component = null;
            try
            {
                component = new RecordKeeper(recordKeeperID, org);
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
            text.Append(BuildOutput(isHTML, "recordKeeper.recordKeeperID", TEST_ID));
            text.Append(BuildOutput(isHTML, "recordKeeper.entityType", "organization"));
            text.Append(BuildOutput(isHTML, "recordKeeper.name", TEST_NAME));
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
                xml.Append("<ddms:recordKeeper ").Append(XmlnsDDMS).Append(">");
                xml.Append("<ddms:recordKeeperID>").Append(TEST_ID).Append("</ddms:recordKeeperID>");
                xml.Append("<ddms:organization>");
                xml.Append("<ddms:name>").Append(TEST_NAME).Append("</ddms:name>");
                xml.Append("</ddms:organization>");
                xml.Append("</ddms:recordKeeper>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, RecordKeeper.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing recordKeeperID
                XElement element = Util.BuildDDMSElement(RecordKeeper.GetName(version), null);
                element.Add(OrganizationTest.Fixture.ElementCopy);
                GetInstance("record keeper ID is required.", element);

                // Empty recordKeeperID
                element = Util.BuildDDMSElement(RecordKeeper.GetName(version), null);
                element.Add(Util.BuildDDMSElement("recordKeeperID", null));
                element.Add(OrganizationTest.Fixture.ElementCopy);
                GetInstance("record keeper ID is required.", element);

                // Missing organization
                element = Util.BuildDDMSElement(RecordKeeper.GetName(version), null);
                element.Add(Util.BuildDDMSElement("recordKeeperID", TEST_ID));
                GetInstance("organization is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing recordKeeperID
                GetInstance("record keeper ID is required.", null, OrganizationTest.Fixture);

                // Missing organization
                GetInstance("organization is required.", TEST_ID, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper elementComponent = GetInstance(SUCCESS, FixtureElement);
                RecordKeeper dataComponent = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper elementComponent = GetInstance(SUCCESS, FixtureElement);
                RecordKeeper dataComponent = GetInstance(SUCCESS, "newID", OrganizationTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_ID, new Organization(Util.GetXsListAsList("DARPA"), null, null, null, null));
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_ID, OrganizationTest.Fixture);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new RecordKeeper(TEST_ID, OrganizationTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The recordKeeper element cannot be used");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper component = GetInstance(SUCCESS, FixtureElement);
                RecordKeeper.Builder builder = new RecordKeeper.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper.Builder builder = new RecordKeeper.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.RecordKeeperID = TEST_ID;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void ResourceElements_RecordKeeper_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordKeeper.Builder builder = new RecordKeeper.Builder();
                builder.RecordKeeperID = TEST_ID;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "organization is required.");
                }
                builder.Organization.Names = Util.GetXsListAsList(TEST_NAME);
                builder.Commit();
            }
        }
    }

}