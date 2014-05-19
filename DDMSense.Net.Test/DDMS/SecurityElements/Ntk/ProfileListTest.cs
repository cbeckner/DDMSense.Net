//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

///* Copyright 2010 - 2013 by Brian Uri!
   
//   This file is part of DDMSence.
   
//   This library is free software; you can redistribute it and/or modify
//   it under the terms of version 3.0 of the GNU Lesser General Public 
//   License as published by the Free Software Foundation.
   
//   This library is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
//   GNU Lesser General Public License for more details.
   
//   You should have received a copy of the GNU Lesser General Public 
//   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

//   You can contact the author at ddmsence@urizone.net. The DDMSence
//   home page is located at http://ddmsence.urizone.net/
// */
//namespace DDMSense.Test.DDMS.SecurityElements.Ntk {



//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.SecurityElements.Ntk;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ntk:AccessProfileList elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class ProfileListTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public ProfileListTest() : base("accessProfileList.xml") {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static ProfileList Fixture {
//            get {
//                try {
//                    return (new ProfileList(ProfileTest.FixtureList, SecurityAttributesTest.Fixture));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private ProfileList GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            ProfileList component = null;
//            try {
//                component = new ProfileList(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="profiles"> the profiles in this list (required) </param>
//        private ProfileList GetInstance(string message, IList<Profile> profiles) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            ProfileList component = null;
//            try {
//                component = new ProfileList(profiles, SecurityAttributesTest.Fixture);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            StringBuilder text = new StringBuilder();
//            text.Append(ProfileTest.FixtureList[0].getOutput(isHTML, "profileList.", ""));
//            text.Append(BuildOutput(isHTML, "profileList.classification", "U"));
//            text.Append(BuildOutput(isHTML, "profileList.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ntk:AccessProfileList ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
//            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t<ntk:AccessProfile ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
//            xml.Append("\t\t<ntk:AccessProfileValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">profile</ntk:AccessProfileValue>\n");
//            xml.Append("\t</ntk:AccessProfile>\n");
//            xml.Append("</ntk:AccessProfileList>\n");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, ProfileList.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, ProfileTest.FixtureList);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ntkPrefix = PropertyReader.getPrefix("ntk");

//                // Missing profile
//                XElement element = Util.buildElement(ntkPrefix, ProfileList.getName(version), version.NtkNamespace, null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                GetInstance("At least one profile is required.", element);

//                // Missing security attributes
//                element = Util.buildElement(ntkPrefix, ProfileList.getName(version), version.NtkNamespace, null);
//                foreach (Profile profile in ProfileTest.FixtureList) {
//                    element.appendChild(profile.ElementCopy);
//                }
//                GetInstance("classification is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // Missing profile
//                GetInstance("At least one profile is required.", (IList) null);

//                // Missing security attributes
//                try {
//                    new ProfileList(ProfileTest.FixtureList, null);
//                    fail("Allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "classification is required.");
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // No warnings
//                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                ProfileList dataComponent = GetInstance(SUCCESS, ProfileTest.FixtureList);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                IList<Profile> profiles = ProfileTest.FixtureList;
//                IList<ProfileValue> valueList = new List<ProfileValue>();
//                valueList.Add(ProfileValueTest.GetFixture("profile2"));
//                profiles.Add(new Profile(SystemNameTest.Fixture, valueList, SecurityAttributesTest.Fixture));
//                ProfileList dataComponent = GetInstance(SUCCESS, profiles);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, ProfileTest.FixtureList);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());

//                component = GetInstance(SUCCESS, ProfileTest.FixtureList);
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                ProfileList.Builder builder = new ProfileList.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList.Builder builder = new ProfileList.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Profiles[0];
//                Assert.IsTrue(builder.Empty);
//                builder.Profiles.get(1).SecurityAttributes.Classification = "U";
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                ProfileList.Builder builder = new ProfileList.Builder();
//                builder.SecurityAttributes.Classification = "U";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "At least one profile is required.");
//                }
//                builder.Profiles[0].SystemName.Value = "TEST";
//                builder.Profiles[0].ProfileValues[0].Vocabulary = "vocab";
//                builder.Profiles[0].ProfileValues[0].Value = "TEST";
//                builder.Profiles[0].SystemName.SecurityAttributes.Classification = "U";
//                builder.Profiles[0].SystemName.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.Profiles[0].ProfileValues[0].SecurityAttributes.Classification = "U";
//                builder.Profiles[0].ProfileValues[0].SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.Profiles[0].SecurityAttributes.Classification = "U";
//                builder.Profiles[0].SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.SecurityAttributes.Classification = "U";
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                ProfileList.Builder builder = new ProfileList.Builder();
//                assertNotNull(builder.Profiles.get(1));
//            }
//        }
//    }

//}