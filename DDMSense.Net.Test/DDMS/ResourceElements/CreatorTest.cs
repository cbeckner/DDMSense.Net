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
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:creator elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class CreatorTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreatorTest()
            : base("creator.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Creator Fixture
        {
            get
            {
                try
                {
                    return (new Creator(OrganizationTest.Fixture, null, null));
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
        private Creator GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Creator component = null;
            try
            {
                SecurityAttributesTest.Fixture.AddTo(element);
                component = new Creator(element);
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
        /// <param name="entity"> the producer entity </param>
        /// <param name="pocTypes"> the pocType (DDMS 4.0.1 or later) </param>
        private Creator GetInstance(string message, IRoleEntity entity, List<string> pocTypes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Creator component = null;
            try
            {
                component = new Creator(entity, pocTypes, SecurityAttributesTest.Fixture);
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
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder text = new StringBuilder();
            text.Append(PersonTest.Fixture.GetOutput(isHTML, "creator.", ""));
            if (version.IsAtLeast("4.0.1"))
            {
                text.Append(BuildOutput(isHTML, "creator.pocType", "DoD-Dist-B"));
            }
            text.Append(BuildOutput(isHTML, "creator.classification", "U"));
            text.Append(BuildOutput(isHTML, "creator.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:creator ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM);
            if (version.IsAtLeast("4.0.1"))
            {
                xml.Append(" ISM:pocType=\"DoD-Dist-B\"");
            }
            xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n\t<ddms:").Append(Person.GetName(version)).Append(">\n");
            xml.Append("\t\t<ddms:name>Brian</ddms:name>\n");
            xml.Append("\t\t<ddms:surname>Uri</ddms:surname>\n");
            xml.Append("\t</ddms:").Append(Person.GetName(version)).Append(">\n</ddms:creator>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Creator.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(Creator.GetName(version), null);
                element.Add(PersonTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, PersonTest.Fixture, null);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");

                // Missing entity
                XElement element = Util.BuildDDMSElement(Creator.GetName(version), null);
                GetInstance("entity is required.", element);

                if (version.IsAtLeast("4.0.1"))
                {
                    // Invalid pocType
                    element = Util.BuildDDMSElement(Creator.GetName(version), null);
                    element.Add(PersonTest.Fixture.ElementCopy);
                    Util.AddAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "Unknown");
                    GetInstance("Unknown is not a valid enumeration token", element);

                    // Partial Invalid pocType
                    element = Util.BuildDDMSElement(Creator.GetName(version), null);
                    element.Add(PersonTest.Fixture.ElementCopy);
                    Util.AddAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "DoD-Dist-B Unknown");
                    GetInstance("Unknown is not a valid enumeration token", element);
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing entity
                GetInstance("entity is required.", (IRoleEntity)null, null);

                if (version.IsAtLeast("4.0.1"))
                {
                    // Invalid pocType
                    GetInstance("Unknown is not a valid enumeration token", PersonTest.Fixture, Util.GetXsListAsList("Unknown"));

                    // Partial Invalid pocType
                    GetInstance("Unknown is not a valid enumeration token", PersonTest.Fixture, Util.GetXsListAsList("DoD-Dist-B Unknown"));
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Creator elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Creator dataComponent = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Creator elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Creator dataComponent = GetInstance(SUCCESS, new Service(Util.GetXsListAsList("DISA PEO-GES"), Util.GetXsListAsList("703-882-1000 703-885-1000"), Util.GetXsListAsList("ddms@fgm.com")), null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Creator component = new Creator(PersonTest.Fixture, null, SecurityAttributesTest.Fixture);
                Assert.AreEqual(SecurityAttributesTest.Fixture, component.SecurityAttributes);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDDMSException), "This component cannot have a PocType")]
        public virtual void ResourceElements_Creator_WrongVersionPocType()
        {
            DDMSVersion.SetCurrentVersion("3.1");
            new Creator(PersonTest.Fixture, Util.GetXsListAsList("DoD-Dist-B"), SecurityAttributesTest.Fixture);
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Creator.Builder builder = new Creator.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Creator.Builder builder = new Creator.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.PocTypes = Util.GetXsListAsList("DoD-Dist-B");
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Creator_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                Creator.Builder builder = new Creator.Builder();
                builder.EntityType = Person.GetName(version);
                builder.Person.Phones = Util.GetXsListAsList("703-885-1000");
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "surname is required.");
                }
                builder.Person.Surname = "Uri";
                builder.Person.Names = Util.GetXsListAsList("Brian");
                builder.Commit();
            }
        }
    }
}