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
namespace DDMSense.Test.DDMS.ResourceElements
{


    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:resourceManagement elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ResourceManagementTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceManagementTest()
            : base("resourceManagement.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static ResourceManagement Fixture
        {
            get
            {
                try
                {
                    return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? new ResourceManagement(null, null, null, ProcessingInfoTest.FixtureList, SecurityAttributesTest.Fixture) : null);
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
        private ResourceManagement GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ResourceManagement component = null;
            try
            {
                component = new ResourceManagement(element);
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
        /// <param name="recordsManagementInfo"> records management info (optional) </param>
        /// <param name="revisionRecall"> information about revision recalls (optional) </param>
        /// <param name="taskingInfos"> list of tasking info (optional) </param>
        /// <param name="processingInfos"> list of processing info (optional) </param>
        private ResourceManagement GetInstance(string message, RecordsManagementInfo recordsManagementInfo, RevisionRecall revisionRecall, List<TaskingInfo> taskingInfos, List<ProcessingInfo> processingInfos)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ResourceManagement component = null;
            try
            {
                component = new ResourceManagement(recordsManagementInfo, revisionRecall, taskingInfos, processingInfos, SecurityAttributesTest.Fixture);
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
            text.Append(RecordsManagementInfoTest.Fixture.GetOutput(isHTML, "resourceManagement.", ""));
            text.Append(RevisionRecallTest.TextFixture.GetOutput(isHTML, "resourceManagement.", ""));
            text.Append(TaskingInfoTest.FixtureList[0].GetOutput(isHTML, "resourceManagement.", ""));
            text.Append(ProcessingInfoTest.FixtureList[0].GetOutput(isHTML, "resourceManagement.", ""));
            text.Append(BuildOutput(isHTML, "resourceManagement.classification", "U"));
            text.Append(BuildOutput(isHTML, "resourceManagement.ownerProducer", "USA"));
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
                xml.Append("<ddms:resourceManagement ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:recordsManagementInfo ddms:vitalRecordIndicator=\"true\">");
                xml.Append("<ddms:recordKeeper><ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:recordKeeper>");
                xml.Append("<ddms:applicationSoftware ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("IRM Generator 2L-9</ddms:applicationSoftware>");
                xml.Append("</ddms:recordsManagementInfo>");
                xml.Append("<ddms:revisionRecall xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:revisionID=\"1\" ");
                xml.Append("ddms:revisionType=\"ADMINISTRATIVE RECALL\" network=\"NIPRNet\" otherNetwork=\"PBS\" ");
                xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description of Recall</ddms:revisionRecall>");
                xml.Append("<ddms:taskingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\"><ddms:requesterInfo ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:requesterInfo>");
                xml.Append("<ddms:addressee ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:addressee>");
                xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">A transformation service.");
                xml.Append("</ddms:description><ddms:taskID xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
                xml.Append("ddms:taskingSystem=\"MDR\" network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" ");
                xml.Append("xlink:arcrole=\"arcrole\" xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
                xml.Append("</ddms:taskingInfo><ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
                xml.Append("ddms:dateProcessed=\"2011-08-19\">XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.");
                xml.Append("</ddms:processingInfo></ddms:resourceManagement>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, ResourceManagement.GetName(version));
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
                XElement element = Util.BuildDDMSElement(ResourceManagement.GetName(version), null);
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
                GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);

                // No optional fields
                GetInstance(SUCCESS, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Too many recordsManagementInfo elements
                XElement element = Util.BuildDDMSElement(ResourceManagement.GetName(version), null);
                element.Add(RecordsManagementInfoTest.Fixture.ElementCopy);
                element.Add(RecordsManagementInfoTest.Fixture.ElementCopy);
                GetInstance("No more than 1 recordsManagementInfo", element);

                // Too many revisionRecall elements
                element = Util.BuildDDMSElement(ResourceManagement.GetName(version), null);
                element.Add(RevisionRecallTest.TextFixtureElement);
                element.Add(RevisionRecallTest.TextFixtureElement);
                GetInstance("No more than 1 revisionRecall", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Incorrect version of security attributes
                DDMSVersion.SetCurrentVersion("2.0");
                SecurityAttributes attributes = SecurityAttributesTest.Fixture;
                DDMSVersion.SetCurrentVersion(sVersion);
                try
                {
                    new ResourceManagement(null, null, null, null, attributes);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "These attributes cannot decorate");
                }
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ResourceManagement elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ResourceManagement dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
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

                ResourceManagement elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ResourceManagement dataComponent = GetInstance(SUCCESS, null, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, null, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, null, ProcessingInfoTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
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

                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new ResourceManagement(null, null, null, null, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The resourceManagement element cannot be used");
            }
        }
       
        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
                ResourceManagement.Builder builder = new ResourceManagement.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ResourceManagement.Builder builder = new ResourceManagement.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.TaskingInfos[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);
                builder = new ResourceManagement.Builder();
                builder.ProcessingInfos[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ResourceManagement.Builder builder = new ResourceManagement.Builder();
                builder.SecurityAttributes.Classification = "COW";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "COW is not a valid enumeration token for this attribute");
                }
                builder.SecurityAttributes.Classification = "U";
                builder.Commit();
            }
        }
    }

}