using System;
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
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using XLinkAttributes = DDMSense.DDMS.Summary.Xlink.XLinkAttributes;
    using XLinkAttributesTest = DDMSense.Test.DDMS.Summary.Xlink.XLinkAttributesTest;

    /// <summary>
    /// <para> Tests related to ddms:taskID elements </para>
    ///
    /// <para> Because a ddms:taskID is a local component, we cannot load a valid document from a unit test data file. We have
    /// to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class TaskIDTest : AbstractBaseTestCase
    {
        private const string TEST_TASKING_SYSTEM = "MDR";
        private const string TEST_VALUE = "Task #12345";
        private const string TEST_NETWORK = "NIPRNet";
        private const string TEST_OTHER_NETWORK = "PBS";

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskIDTest()
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

                    XElement element = Util.BuildDDMSElement(TaskID.GetName(version), TEST_VALUE);
                    ////element.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + element.Name.LocalName;
                    //element.Name = XName.Get(PropertyReader.GetPrefix("xlink"), version.XlinkNamespace) + element.Name.LocalName;
                    Util.AddDDMSAttribute(element, "taskingSystem", TEST_TASKING_SYSTEM);
                    Util.AddAttribute(element, "", "network", "", TEST_NETWORK);
                    Util.AddAttribute(element, "", "otherNetwork", "", TEST_OTHER_NETWORK);
                    XLinkAttributesTest.SimpleFixture.AddTo(element);
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
        public static TaskID Fixture
        {
            get
            {
                try
                {
                    return (new TaskID(TaskIDTest.FixtureElement));
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
        private TaskID GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TaskID component = null;
            try
            {
                component = new TaskID(element);
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
        /// <param name="value"> the child text (optional) link href (required) </param>
        /// <param name="taskingSystem"> the tasking system (optional) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="attributes"> the xlink attributes (optional) </param>
        private TaskID GetInstance(string message, string value, string taskingSystem, string network, string otherNetwork, XLinkAttributes attributes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TaskID component = null;
            try
            {
                component = new TaskID(value, taskingSystem, network, otherNetwork, attributes);
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
            text.Append(BuildOutput(isHTML, "taskID", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "taskID.taskingSystem", TEST_TASKING_SYSTEM));
            text.Append(BuildOutput(isHTML, "taskID.network", TEST_NETWORK));
            text.Append(BuildOutput(isHTML, "taskID.otherNetwork", TEST_OTHER_NETWORK));
            text.Append(XLinkAttributesTest.SimpleFixture.GetOutput(isHTML, "taskID."));
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
                xml.Append("<ddms:taskID ").Append(XmlnsDDMS).Append(" ");
                xml.Append("xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:taskingSystem=\"MDR\" ");
                xml.Append("network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" ");
                xml.Append("xlink:arcrole=\"arcrole\" xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, TaskID.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);

                // No optional fields
                XElement element = Util.BuildDDMSElement(TaskID.GetName(version), TEST_VALUE);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);

                // No optional fields
                GetInstance(SUCCESS, TEST_VALUE, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong type of XLinkAttributes (locator)
                XElement element = Util.BuildDDMSElement(TaskID.GetName(version), TEST_VALUE);
                XLinkAttributesTest.LocatorFixture.AddTo(element);
                GetInstance("The type attribute must have a fixed value", element);

                // Missing value
                element = Util.BuildDDMSElement(TaskID.GetName(version), null);
                GetInstance("value is required.", element);

                // Bad network
                element = Util.BuildDDMSElement(TaskID.GetName(version), TEST_VALUE);
                Util.AddAttribute(element, "", "network", "", "PBS");
                GetInstance("The network attribute must be one of", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong type of XLinkAttributes
                GetInstance("The type attribute must have a fixed value", TEST_VALUE, null, null, null, XLinkAttributesTest.LocatorFixture);

                // Missing value
                GetInstance("value is required.", null, null, null, null, null);

                // Bad network
                GetInstance("The network attribute must be one of", TEST_VALUE, null, "PBS", null, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                TaskID component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
                TaskID dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
                TaskID dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, DIFFERENT_VALUE, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, "SIPRNet", TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, DIFFERENT_VALUE, XLinkAttributesTest.SimpleFixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID elementComponent = GetInstance(SUCCESS, FixtureElement);
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID component = GetInstance(SUCCESS, FixtureElement);
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, XLinkAttributesTest.SimpleFixture);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("4.0.1");
                XLinkAttributes attr = XLinkAttributesTest.SimpleFixture;
                DDMSVersion.SetCurrentVersion("2.0");
                new TaskID(TEST_VALUE, TEST_TASKING_SYSTEM, TEST_NETWORK, TEST_OTHER_NETWORK, attr);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID component = GetInstance(SUCCESS, FixtureElement);
                TaskID.Builder builder = new TaskID.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID.Builder builder = new TaskID.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_TaskID_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TaskID.Builder builder = new TaskID.Builder();
                builder.TaskingSystem = TEST_TASKING_SYSTEM;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "value is required.");
                }
                builder.Value = TEST_VALUE;
                builder.Commit();
            }
        }
    }
}