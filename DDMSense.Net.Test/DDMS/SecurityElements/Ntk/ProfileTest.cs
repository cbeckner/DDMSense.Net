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

namespace DDMSense.Test.DDMS.SecurityElements.Ntk
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ntk:AccessProfile elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ProfileTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileTest()
            : base("accessProfile.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Profile Fixture
        {
            get
            {
                try
                {
                    return (new Profile(SystemNameTest.Fixture, ProfileValueTest.FixtureList, SecurityAttributesTest.Fixture));
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
        public static List<Profile> FixtureList
        {
            get
            {
                List<Profile> profiles = new List<Profile>();
                profiles.Add(ProfileTest.Fixture);
                return (profiles);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private Profile GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Profile component = null;
            try
            {
                component = new Profile(element);
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
        private Profile GetInstance(string message, SystemName systemName, List<ProfileValue> values)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Profile component = null;
            try
            {
                component = new Profile(systemName, values, SecurityAttributesTest.Fixture);
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
            text.Append(SystemNameTest.Fixture.GetOutput(isHTML, "profile.", ""));
            text.Append(ProfileValueTest.FixtureList[0].GetOutput(isHTML, "profile.", ""));
            text.Append(BuildOutput(isHTML, "profile.classification", "U"));
            text.Append(BuildOutput(isHTML, "profile.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ntk:AccessProfile ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
            xml.Append("\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
            xml.Append("\t<ntk:AccessProfileValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">profile</ntk:AccessProfileValue>\n");
            xml.Append("</ntk:AccessProfile>\n");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, Profile.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, SystemNameTest.Fixture, ProfileValueTest.FixtureList);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing systemName
                XElement element = Util.BuildElement(ntkPrefix, Profile.GetName(version), version.NtkNamespace, null);
                foreach (ProfileValue value in ProfileValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("systemName is required.", element);

                // Missing profileValue
                element = Util.BuildElement(ntkPrefix, Profile.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("At least one profile value is required.", element);

                // Missing security attributes
                element = Util.BuildElement(ntkPrefix, Profile.GetName(version), version.NtkNamespace, null);
                element.Add(SystemNameTest.Fixture.ElementCopy);
                foreach (ProfileValue value in ProfileValueTest.FixtureList)
                {
                    element.Add(value.ElementCopy);
                }
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing systemName
                GetInstance("systemName is required.", null, ProfileValueTest.FixtureList);

                // Missing profileValue
                GetInstance("At least one profile value is required.", SystemNameTest.Fixture, null);

                // Missing security attributes
                try
                {
                    new Profile(SystemNameTest.Fixture, ProfileValueTest.FixtureList, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                Profile component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Profile dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, ProfileValueTest.FixtureList);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Profile dataComponent = GetInstance(SUCCESS, new SystemName("MDR", null, null, null, SecurityAttributesTest.Fixture), ProfileValueTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<ProfileValue> list = new List<ProfileValue>();
                list.Add(ProfileValueTest.GetFixture("newProfile"));
                dataComponent = GetInstance(SUCCESS, SystemNameTest.Fixture, list);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, ProfileValueTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, SystemNameTest.Fixture, ProfileValueTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Profile.Builder builder = new Profile.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile.Builder builder = new Profile.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.ProfileValues.Add(new ProfileValue.Builder());
                Assert.IsTrue(builder.Empty);
                builder.ProfileValues.Add(new ProfileValue.Builder());
                builder.ProfileValues[1].Value = "TEST";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_Profile_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Profile.Builder builder = new Profile.Builder();
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
                    ExpectMessage(e, "At least one profile value is required.");
                }
                builder.ProfileValues.Add(new ProfileValue.Builder());
                builder.ProfileValues[0].Qualifier = "test";
                builder.ProfileValues[0].Value = "test";
                builder.ProfileValues[0].Vocabulary = "vocab";
                builder.ProfileValues[0].SecurityAttributes.Classification = "U";
                builder.ProfileValues[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }
}