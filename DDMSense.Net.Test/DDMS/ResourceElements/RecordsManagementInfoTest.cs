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
    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:recordsManagementInfo elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    public class RecordsManagementInfoTest : AbstractBaseTestCase
    {

        private static readonly bool? TEST_VITAL = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public RecordsManagementInfoTest()
            : base("recordsManagementInfo.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static RecordsManagementInfo Fixture
        {
            get
            {
                try
                {
                    return (new RecordsManagementInfo(RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL));
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
        private RecordsManagementInfo GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RecordsManagementInfo component = null;
            try
            {
                component = new RecordsManagementInfo(element);
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
        /// <param name="keeper"> the record keeper (optional) </param>
        /// <param name="software"> the software (optional) </param>
        /// <param name="vitalRecord"> whether this is a vital record (optional, defaults to false) </param>
        /// <param name="org"> the organization </param>
        private RecordsManagementInfo GetInstance(string message, RecordKeeper keeper, ApplicationSoftware software, bool? vitalRecord)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RecordsManagementInfo component = null;
            try
            {
                component = new RecordsManagementInfo(keeper, software, vitalRecord);
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
            text.Append(RecordKeeperTest.Fixture.GetOutput(isHTML, "recordsManagementInfo.", ""));
            text.Append(ApplicationSoftwareTest.Fixture.GetOutput(isHTML, "recordsManagementInfo.", ""));
            text.Append(BuildOutput(isHTML, "recordsManagementInfo.vitalRecordIndicator", "true"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:recordsManagementInfo ").Append(XmlnsDDMS);
            xml.Append(" ddms:vitalRecordIndicator=\"true\">\n");
            xml.Append("\t<ddms:recordKeeper>\n");
            xml.Append("\t\t<ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>\n");
            xml.Append("\t\t<ddms:organization>\n");
            xml.Append("\t\t\t<ddms:name>DISA</ddms:name>\n");
            xml.Append("\t\t</ddms:organization>\n");
            xml.Append("\t</ddms:recordKeeper>\n");
            xml.Append("\t<ddms:applicationSoftware ").Append(XmlnsISM);
            xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">IRM Generator 2L-9</ddms:applicationSoftware>\n");
            xml.Append("</ddms:recordsManagementInfo>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, RecordsManagementInfo.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(RecordsManagementInfo.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);

                // No optional fields
                GetInstance(SUCCESS, null, null, null);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No invalid cases.
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No invalid cases.
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordsManagementInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RecordsManagementInfo dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
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

                RecordsManagementInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                RecordsManagementInfo dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RecordKeeperTest.Fixture, null, TEST_VITAL);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, null, ApplicationSoftwareTest.Fixture, TEST_VITAL);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
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

                RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, RecordKeeperTest.Fixture, ApplicationSoftwareTest.Fixture, TEST_VITAL);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new RecordsManagementInfo(null, null, TEST_VITAL);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The recordsManagementInfo element cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordsManagementInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.VitalRecordIndicator = TEST_VITAL;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RecordsManagementInfo.Builder builder = new RecordsManagementInfo.Builder();
                builder.ApplicationSoftware.Value = "value";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
                builder.ApplicationSoftware.SecurityAttributes.Classification = "U";
                builder.ApplicationSoftware.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }

}