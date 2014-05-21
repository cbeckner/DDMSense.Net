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
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:addressee elements </para>
    ///
    /// <para> Because a ddms:addressee is a local component, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class AddresseeTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AddresseeTest()
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
                XElement element = Util.BuildDDMSElement(Addressee.GetName(version), null);
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
        public static List<Addressee> FixtureList
        {
            get
            {
                try
                {
                    List<Addressee> list = new List<Addressee>();
                    list.Add(new Addressee(GetFixtureElement(true)));
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
        private Addressee GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Addressee component = null;
            try
            {
                component = new Addressee(element);
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
        private Addressee GetInstance(string message, IRoleEntity entity)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Addressee component = null;
            try
            {
                component = new Addressee(entity, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "addressee.entityType", "organization"));
            text.Append(BuildOutput(isHTML, "addressee.name", "DISA"));
            text.Append(BuildOutput(isHTML, "addressee.classification", "U"));
            text.Append(BuildOutput(isHTML, "addressee.ownerProducer", "USA"));
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
                xml.Append("<ddms:addressee ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
                xml.Append("</ddms:addressee>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(true)), DEFAULT_DDMS_PREFIX, Addressee.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_ElementConstructorValid()
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
        public virtual void ResourceElements_Adressee_DataConstructorValid()
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
        public virtual void ResourceElements_Adressee_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing entity
                XElement element = Util.BuildDDMSElement(Addressee.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("entity is required.", element);

                // Missing security attributes
                element = Util.BuildDDMSElement(Addressee.GetName(version), null);
                element.Add(OrganizationTest.Fixture.ElementCopy);
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing entity
                GetInstance("entity is required.", (IRoleEntity)null);

                // Wrong entity
                GetInstance("The entity must be a person or an organization.", new Service(Util.GetXsListAsList("Name"), null, null));

                // Missing security attributes
                try
                {
                    new Addressee(OrganizationTest.Fixture, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
                Addressee dataComponent = GetInstance(SUCCESS, OrganizationTest.Fixture);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
                Addressee dataComponent = GetInstance(SUCCESS, PersonTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new Addressee(OrganizationTest.Fixture, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The addressee element cannot be used");
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Equality after Building, organization
                Addressee component = GetInstance(SUCCESS, GetFixtureElement(true));
                Addressee.Builder builder = new Addressee.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // Equality after Building, person
                component = GetInstance(SUCCESS, GetFixtureElement(false));
                builder = new Addressee.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee.Builder builder = new Addressee.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Person.Names = Util.GetXsListAsList("Name");
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void ResourceElements_Adressee_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Addressee.Builder builder = new Addressee.Builder();
                builder.Person.Names = Util.GetXsListAsList("Name");
                builder.Person.Surname = "Surname";
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