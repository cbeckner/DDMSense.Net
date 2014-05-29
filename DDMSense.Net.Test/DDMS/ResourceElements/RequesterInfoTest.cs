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
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:requesterInfo elements </para>
    ///
    /// <para> Because a ddms:requesterInfo is a local component, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class RequesterInfoTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RequesterInfoTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="useOrg"> true to put an organization in, false for a person </param>
        public static XElement GetFixtureElement(bool useOrg)
        {
            try
            {
                DDMSVersion version = DDMSVersion.CurrentVersion;
                XElement element = Util.BuildDDMSElement(RequesterInfo.GetName(version), null);
                //element.Name = XName.Get(PropertyReader.GetPrefix("ddms"), version.Namespace) + element.Name.LocalName;
                element.Add(useOrg ? OrganizationTest.Fixture.ElementCopy : PersonTest.Fixture.ElementCopy);
                SecurityAttributesTest.Fixture.AddTo(element);
                return (element);
            }
            catch (InvalidDDMSException e)
            {
                Assert.Fail("Could not create fixture: " + e.Message);
            }
            return (null);
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<RequesterInfo> FixtureList
        {
            get
            {
                try
                {
                    List<RequesterInfo> list = new List<RequesterInfo>();
                    list.Add(new RequesterInfo(RequesterInfoTest.GetFixtureElement(true)));
                    return (list);
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
        private RequesterInfo GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RequesterInfo component = null;
            try
            {
                component = new RequesterInfo(element);
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
        /// <param name="entity"> the person or organization in this role </param>
        /// <param name="org"> the organization </param>
        private RequesterInfo GetInstance(string message, IRoleEntity entity)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            RequesterInfo component = null;
            try
            {
                component = new RequesterInfo(entity, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "requesterInfo.entityType", "organization"));
            text.Append(BuildOutput(isHTML, "requesterInfo.name", "DISA"));
            text.Append(BuildOutput(isHTML, "requesterInfo.classification", "U"));
            text.Append(BuildOutput(isHTML, "requesterInfo.ownerProducer", "USA"));
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
                xml.Append("<ddms:requesterInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
                xml.Append("</ddms:requesterInfo>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(true)), DEFAULT_DDMS_PREFIX, RequesterInfo.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields, organization
                GetInstance(SUCCESS, GetFixtureElement(true));

                // All fields, person
                GetInstance(SUCCESS, GetFixtureElement(false));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields, organization
                GetInstance(SUCCESS, OrganizationTest.Fixture);

                // All fields, person
                GetInstance(SUCCESS, PersonTest.Fixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing entity
                XElement element = Util.BuildDDMSElement(RequesterInfo.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("entity is required.", element);

                // Missing security attributes
                element = Util.BuildDDMSElement(RequesterInfo.GetName(version), null);
                element.Add(OrganizationTest.Fixture.ElementCopy);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing entity
                GetInstance("entity is required.", (IRoleEntity)null);

                // Wrong entity
                GetInstance("The entity must be a person or an organization.", new Service(Util.GetXsListAsList("Service"), null, null));

                // Missing security attributes
                try
                {
                    new RequesterInfo(OrganizationTest.Fixture, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
                RequesterInfo dataComponent = GetInstance(SUCCESS, OrganizationTest.Fixture);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
                RequesterInfo dataComponent = GetInstance(SUCCESS, PersonTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new RequesterInfo(OrganizationTest.Fixture, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The requesterInfo element cannot be used");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Equality after Building, organization
                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
                RequesterInfo.Builder builder = new RequesterInfo.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // Equality after Building, person
                component = GetInstance(SUCCESS, GetFixtureElement(false));
                builder = new RequesterInfo.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo.Builder builder = new RequesterInfo.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Person.Surname = "surname";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Requester_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                RequesterInfo.Builder builder = new RequesterInfo.Builder();
                builder.Person.Names = Util.GetXsListAsList("Brian");
                builder.Person.Surname = "Uri";
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