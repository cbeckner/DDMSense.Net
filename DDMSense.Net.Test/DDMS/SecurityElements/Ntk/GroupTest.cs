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

namespace DDMSense.Test.DDMS.SecurityElements.Ntk
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ntk:AccessGroup elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class GroupTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GroupTest()
            : base("accessGroup.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Group Fixture
        {
            get
            {
                try
                {
                    return (new Group(SystemNameTest.Fixture, GroupValueTest.FixtureList, SecurityAttributesTest.Fixture));
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
        public static List<Group> FixtureList
        {
            get
            {
                List<Group> list = new List<Group>();
                list.Add(GroupTest.Fixture);
                return (list);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private Group GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Group component = null;
            try
            {
                component = new Group(element);
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
        /// <param name="systemName"> the system (required) </param>
        /// <param name="values"> the values (1 required) </param>
        private Group GetInstance(string message, SystemName systemName, List<GroupValue> values)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Group component = null;
            try
            {
                component = new Group(systemName, values, SecurityAttributesTest.Fixture);
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
            text.Append(SystemNameTest.Fixture.GetOutput(isHTML, "group.", ""));
            text.Append(GroupValueTest.FixtureList[0].GetOutput(isHTML, "group.", ""));
            text.Append(BuildOutput(isHTML, "group.classification", "U"));
            text.Append(BuildOutput(isHTML, "group.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ntk:AccessGroup ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
            xml.Append("\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
            xml.Append("\t<ntk:AccessGroupValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">WISE/RODCA</ntk:AccessGroupValue>\n");
            xml.Append("</ntk:AccessGroup>\n");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, Group.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, SystemNameTest.Fixture, GroupValueTest.FixtureList);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing systemName
                XElement element = Util.BuildElement(ntkPrefix, Group.GetName(version), version.NtkNamespace, null);
                foreach (GroupValue value in GroupValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("systemName is required.", element);

                // Missing groupValue
                element = Util.BuildElement(ntkPrefix, Group.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("At least one group value is required.", element);

                // Missing security attributes
                element = Util.BuildElement(ntkPrefix, Group.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                foreach (GroupValue value in GroupValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing systemName
                GetInstance("systemName is required.", null, GroupValueTest.FixtureList);

                // Missing groupValue
                GetInstance("At least one group value is required.", SystemNameTest.Fixture, null);

                // Missing security attributes
                try
                {
                    new Group(SystemNameTest.Fixture, GroupValueTest.FixtureList, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Group component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Group dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, GroupValueTest.FixtureList);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Group dataComponent = GetInstance(SUCCESS, new SystemName("MDR", null, null, null, SecurityAttributesTest.Fixture), GroupValueTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<GroupValue> list = new List<GroupValue>();
                list.Add(new GroupValue("newUser", null, null, null, SecurityAttributesTest.Fixture));
                dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, list);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, GroupValueTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, GroupValueTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Group.Builder builder = new Group.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group.Builder builder = new Group.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.GroupValues.Add(new GroupValue.Builder());
                builder.GroupValues[0].Value = "TEST";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void SecurityELements_Ntk_Group_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Group.Builder builder = new Group.Builder();
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.SystemName.Value = "value";
                builder.SystemName.SecurityAttributes.Classification = "U";
                builder.SystemName.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");

                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one group value is required.");
                }
                builder.GroupValues.Add(new GroupValue.Builder());
                builder.GroupValues[0].Qualifier = "test";
                builder.GroupValues[0].Value = "test";
                builder.GroupValues[0].SecurityAttributes.Classification = "U";
                builder.GroupValues[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }
}