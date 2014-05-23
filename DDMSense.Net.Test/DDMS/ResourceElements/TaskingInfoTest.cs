using System;
using System.Collections.Generic;
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

namespace DDMSense.Test.DDMS.ResourceElements
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Description = DDMSense.DDMS.Summary.Description;
    using DescriptionTest = DDMSense.Test.DDMS.Summary.DescriptionTest;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:taskingInfo elements </para>
    ///
    /// <para> Because a ddms:taskingInfo is a local component, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class TaskingInfoTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskingInfoTest()
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
                    XElement element = Util.BuildDDMSElement(TaskingInfo.GetName(version), null);
                    //element.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + element.Name.LocalName;
                    //element.Name = XName.Get(PropertyReader.GetPrefix("ism"), version.IsmNamespace) + element.Name.LocalName;
                    SecurityAttributesTest.Fixture.AddTo(element);
                    element.Add(RequesterInfoTest.GetFixtureElement(true));
                    element.Add(AddresseeTest.GetFixtureElement(true));
                    element.Add(DescriptionTest.Fixture.ElementCopy);
                    element.Add(TaskIDTest.FixtureElement);
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
        public static List<TaskingInfo> FixtureList
        {
            get
            {
                try
                {
                    List<TaskingInfo> infos = new List<TaskingInfo>();
                    infos.Add(new TaskingInfo(FixtureElement));
                    return (infos);
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
        private TaskingInfo GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TaskingInfo component = null;
            try
            {
                component = new TaskingInfo(element);
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
        /// <param name="requesterInfos"> list of requestors (optional) </param>
        /// <param name="addressees"> list of addressee (optional) </param>
        /// <param name="description"> description of tasking (optional) </param>
        /// <param name="taskID"> taskID for tasking (required) </param>
        private TaskingInfo GetInstance(string message, List<RequesterInfo> requesterInfos, List<Addressee> addressees, Description description, TaskID taskID)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TaskingInfo component = null;
            try
            {
                component = new TaskingInfo(requesterInfos, addressees, description, taskID, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.entityType", "organization"));
            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.name", "DISA"));
            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.classification", "U"));
            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.entityType", "organization"));
            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.name", "DISA"));
            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.classification", "U"));
            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "taskingInfo.description", "A transformation service."));
            text.Append(BuildOutput(isHTML, "taskingInfo.description.classification", "U"));
            text.Append(BuildOutput(isHTML, "taskingInfo.description.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID", "Task #12345"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.taskingSystem", "MDR"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.network", "NIPRNet"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.otherNetwork", "PBS"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.type", "simple"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.href", "http://en.wikipedia.org/wiki/Tank"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.role", "tank"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.title", "Tank Page"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.arcrole", "arcrole"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.show", "new"));
            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.actuate", "onLoad"));
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
                xml.Append("<ddms:taskingInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:requesterInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
                xml.Append("</ddms:requesterInfo>");
                xml.Append("<ddms:addressee ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
                xml.Append("</ddms:addressee>");
                xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">A transformation service.</ddms:description>");
                xml.Append("<ddms:taskID xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
                xml.Append("ddms:taskingSystem=\"MDR\" network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:arcrole=\"arcrole\" ");
                xml.Append("xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
                xml.Append("</ddms:taskingInfo>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, TaskingInfo.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);

                // No optional fields
                XElement element = Util.BuildDDMSElement(TaskingInfo.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                element.Add(TaskIDTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);

                // No optional fields
                GetInstance(SUCCESS, null, null, null, TaskIDTest.Fixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing taskID
                XElement element = Util.BuildDDMSElement(TaskingInfo.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("taskID is required.", element);

                // Missing security attributes
                element = Util.BuildDDMSElement(TaskingInfo.GetName(version), null);
                element.Add(TaskIDTest.Fixture.ElementCopy);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing taskID
                GetInstance("taskID is required.", null, null, null, null);

                // Missing security attributes
                try
                {
                    new TaskingInfo(null, null, null, new TaskID("test", null, null, null, null), null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo elementComponent = GetInstance(SUCCESS, FixtureElement);
                TaskingInfo dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo elementComponent = GetInstance(SUCCESS, FixtureElement);
                TaskingInfo dataComponent = GetInstance(SUCCESS, null, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, null, DescriptionTest.Fixture, TaskIDTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, null, TaskIDTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                TaskID taskID = new TaskID("Test", null, null, null, null);
                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, taskID);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("4.0.1");
                SecurityAttributes attr = SecurityAttributesTest.Fixture;
                DDMSVersion.SetCurrentVersion("2.0");
                new TaskingInfo(null, null, null, new TaskID("test", null, null, null, null), attr);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
                TaskingInfo.Builder builder = new TaskingInfo.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo.Builder builder = new TaskingInfo.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.RequesterInfos.Add(new RequesterInfo.Builder());
                builder.RequesterInfos[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);
                builder = new TaskingInfo.Builder();
                builder.Addressees.Add(new Addressee.Builder());
                builder.Addressees[0].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskingInfo_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskingInfo.Builder builder = new TaskingInfo.Builder();
                builder.SecurityAttributes.Classification = "U";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "taskID is required.");
                }
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.TaskID.Value = "test";
                builder.Commit();
            }
        }
    }
}