//using System;
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
//    using DDMSense.DDMS.SecurityElements.Ntk;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ntk:Access elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class AccessTest : AbstractBaseTestCase {

//        private static readonly bool? TEST_EXTERNAL = true;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public AccessTest() : base("access.xml") {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Access Fixture {
//            get {
//                try {
//                    return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? new Access(IndividualTest.FixtureList, null, null, SecurityAttributesTest.Fixture) : null);
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a dummy value for the externalReference attribute, based upon the current DDMS version.
//        /// </summary>
//        private static bool? ExternalReference {
//            get {
//                return (DDMSVersion.CurrentVersion.isAtLeast("4.1") ? TEST_EXTERNAL : null);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private Access GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Access component = null;
//            try {
//                component = new Access(element);
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
//        /// <param name="individuals"> the individuals </param>
//        /// <param name="groups"> the groups </param>
//        /// <param name="profileList"> the profilesprofiles the profiles in this list (required) </param>
//        /// <param name="externalReference"> the external reference attribute </param>
//        private Access GetInstance(string message, IList<Individual> individuals, IList<Group> groups, ProfileList profileList, bool? externalReference) {
//            bool expectFailure = !Util.isEmpty(message);
//            Access component = null;
//            try {
//                component = new Access(individuals, groups, profileList, externalReference, SecurityAttributesTest.Fixture);
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
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder text = new StringBuilder();
//            text.Append(IndividualTest.Fixture.getOutput(isHTML, "access.individualList.", ""));
//            text.Append(GroupTest.Fixture.getOutput(isHTML, "access.groupList.", ""));
//            text.Append(ProfileListTest.Fixture.getOutput(isHTML, "access.", ""));
//            if (version.isAtLeast("4.1")) {
//                text.Append(BuildOutput(isHTML, "access.externalReference", Convert.ToString(TEST_EXTERNAL)));
//            }
//            text.Append(BuildOutput(isHTML, "access.classification", "U"));
//            text.Append(BuildOutput(isHTML, "access.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ntk:Access ").Append(XmlnsNTK).Append(" ").Append(XmlnsISM).Append(" ");
//            if (version.isAtLeast("4.1")) {
//                xml.Append("ntk:externalReference=\"true\" ");
//            }
//            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t<ntk:AccessIndividualList>\n");
//            xml.Append("\t\t<ntk:AccessIndividual ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
//            xml.Append("\t\t\t<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">user_2321889:Doe_John_H</ntk:AccessIndividualValue>\n");
//            xml.Append("\t\t</ntk:AccessIndividual>\n");
//            xml.Append("\t</ntk:AccessIndividualList>\n");
//            xml.Append("\t<ntk:AccessGroupList>\n");
//            xml.Append("\t\t<ntk:AccessGroup ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
//            xml.Append("\t\t\t<ntk:AccessGroupValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">WISE/RODCA</ntk:AccessGroupValue>\n");
//            xml.Append("\t\t</ntk:AccessGroup>\n");
//            xml.Append("\t</ntk:AccessGroupList>\n");
//            xml.Append("\t<ntk:AccessProfileList ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t\t<ntk:AccessProfile ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//            xml.Append("\t\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
//            xml.Append("\t\t\t<ntk:AccessProfileValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ntk:vocabulary=\"vocabulary\">profile</ntk:AccessProfileValue>\n");
//            xml.Append("\t\t</ntk:AccessProfile>\n");
//            xml.Append("\t</ntk:AccessProfileList>\n");
//            xml.Append("</ntk:Access>");

//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_NTK_PREFIX, Access.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ntkPrefix = PropertyReader.getPrefix("ntk");

//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildElement(ntkPrefix, Access.getName(version), version.NtkNamespace, null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields
//                GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, ProfileListTest.Fixture, ExternalReference);

//                // No optional fields
//                GetInstance(SUCCESS, null, null, null, null);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ntkPrefix = PropertyReader.getPrefix("ntk");

//                // Missing security attributes
//                XElement element = Util.buildElement(ntkPrefix, Access.getName(version), version.NtkNamespace, null);
//                GetInstance("classification is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Missing security attributes
//                try {
//                    new Access(null, null, null, null);
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
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Access component = GetInstance(SUCCESS, GetValidElement(sVersion));

//                // 4.1 ntk:externalReference used
//                if (version.isAtLeast("4.1")) {
//                    assertEquals(1, component.ValidationWarnings.size());
//                    string text = "The ntk:externalReference attribute in this DDMS component";
//                    string locator = "ntk:Access";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//                }
//                // No warnings 
//                else {
//                    assertEquals(0, component.ValidationWarnings.size());
//                }

//                // Empty
//                component = GetInstance(SUCCESS, null, null, null, null);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "An ntk:Access element was found with no";
//                string locator = "ntk:Access";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDeprecatedConstructor() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDeprecatedConstructor() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access component = new Access(IndividualTest.FixtureList, null, null, SecurityAttributesTest.Fixture);
//                assertNull(component.ExternalReference);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Access dataComponent = GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, ProfileListTest.Fixture, ExternalReference);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Access elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Access dataComponent = GetInstance(SUCCESS, null, GroupTest.FixtureList, ProfileListTest.Fixture, ExternalReference);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, IndividualTest.FixtureList, null, ProfileListTest.Fixture, ExternalReference);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, null, ExternalReference);
//                assertFalse(elementComponent.Equals(dataComponent));

//                if (version.isAtLeast("4.1")) {
//                    dataComponent = GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, ProfileListTest.Fixture, false);
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, ProfileListTest.Fixture, ExternalReference);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, IndividualTest.FixtureList, GroupTest.FixtureList, ProfileListTest.Fixture, ExternalReference);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Access.Builder builder = new Access.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access.Builder builder = new Access.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Individuals.get(0);
//                assertTrue(builder.Empty);
//                builder.Groups.get(1).SecurityAttributes.Classification = "U";
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Access.Builder builder = new Access.Builder();
//                builder.SecurityAttributes.Classification = "U";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "At least 1 ownerProducer must be set.");
//                }
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }

//        public virtual void TestWrongVersion() {
//            // Implicit, since the NTK namespace does not exist before DDMS 4.0.1.
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Access.Builder builder = new Access.Builder();
//                assertNotNull(builder.Individuals.get(1));
//                assertNotNull(builder.Groups.get(1));
//            }
//        }
//    }

//}