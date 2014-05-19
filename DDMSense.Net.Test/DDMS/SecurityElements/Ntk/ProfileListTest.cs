using System.Collections;
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



    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.SecurityElements.Ntk;
    using System.Xml.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DDMSense.DDMS;
    using System;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to ntk:AccessProfileList elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ProfileListTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileListTest()
            : base("accessProfileList.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static ProfileList Fixture
        {
            get
            {
                try
                {
                    return (new ProfileList(ProfileTest.FixtureList, SecurityAttributesTest.Fixture));
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
        private ProfileList GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProfileList component = null;
            try
            {
                component = new ProfileList(element);
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
        /// <param name="profiles"> the profiles in this list (required) </param>
        private ProfileList GetInstance(string message, List<Profile> profiles)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProfileList component = null;
            try
            {
                component = new ProfileList(profiles, SecurityAttributesTest.Fixture);
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
            text.Append(ProfileTest.FixtureList[0].GetOutput(isHTML, "profileList.", ""));
            text.Append(BuildOutput(isHTML, "profileList.classification", "U"));
            text.Append(BuildOutput(isHTML, "profileList.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ntk:AccessProfileList ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
            xml.Append("\t<ntk:AccessProfile ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
            xml.Append("\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
            xml.Append("\t\t<ntk:AccessProfileValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">profile</ntk:AccessProfileValue>\n");
            xml.Append("\t</ntk:AccessProfile>\n");
            xml.Append("</ntk:AccessProfileList>\n");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, ProfileList.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, ProfileTest.FixtureList);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ntkPrefix = PropertyReader.GetPrefix("ntk");

                // Missing profile
                XElement element = Util.BuildElement(ntkPrefix, ProfileList.GetName(version), version.NtkNamespace, null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("At least one profile is required.", element);

                // Missing security attributes
                element = Util.BuildElement(ntkPrefix, ProfileList.GetName(version), version.NtkNamespace, null);
                foreach (Profile profile in ProfileTest.FixtureList)
                {
                    element.Add(profile.ElementCopy);
                }
                GetInstance("classification is required.", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing profile
                GetInstance("At least one profile is required.", (List<Profile>)null);

                // Missing security attributes
                try
                {
                    new ProfileList(ProfileTest.FixtureList, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProfileList dataComponent = GetInstance(SUCCESS, ProfileTest.FixtureList);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                List<Profile> profiles = ProfileTest.FixtureList;
                List<ProfileValue> valueList = new List<ProfileValue>();
                valueList.Add(ProfileValueTest.GetFixture("profile2"));
                profiles.Add(new Profile(SystemNameTest.Fixture, valueList, SecurityAttributesTest.Fixture));
                ProfileList dataComponent = GetInstance(SUCCESS, profiles);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void SecurityElements_Ntk_ProfileList_HTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void SecurityElements_Ntk_ProfileList_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, ProfileTest.FixtureList);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());

                component = GetInstance(SUCCESS, ProfileTest.FixtureList);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_WrongVersion()
        {
            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void SecurityElements_Ntk_ProfileList_BuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        public virtual void SecurityElements_Ntk_ProfileList_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProfileList.Builder builder = new ProfileList.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList.Builder builder = new ProfileList.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                //TODO: Not sure what to do here.
                //builder.Profiles[0];
                Assert.Fail("TODO: builder.Profiles[0]");
                Assert.IsTrue(builder.Empty);
                builder.Profiles[1].SecurityAttributes.Classification = "U";
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProfileList.Builder builder = new ProfileList.Builder();
                builder.SecurityAttributes.Classification = "U";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one profile is required.");
                }
                builder.Profiles[0].SystemName.Value = "TEST";
                builder.Profiles[0].ProfileValues[0].Vocabulary = "vocab";
                builder.Profiles[0].ProfileValues[0].Value = "TEST";
                builder.Profiles[0].SystemName.SecurityAttributes.Classification = "U";
                builder.Profiles[0].SystemName.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Profiles[0].ProfileValues[0].SecurityAttributes.Classification = "U";
                builder.Profiles[0].ProfileValues[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Profiles[0].SecurityAttributes.Classification = "U";
                builder.Profiles[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ntk_ProfileList_BuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                ProfileList.Builder builder = new ProfileList.Builder();
                Assert.IsNotNull(builder.Profiles[1]);
            }
        }
    }

}