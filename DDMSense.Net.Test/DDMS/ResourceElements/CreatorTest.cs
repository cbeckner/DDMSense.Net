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
//namespace DDMSense.Test.DDMS.ResourceElements {

	
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.ResourceElements;
//    using DDMSense.DDMS.ResourceElements;
//    using DDMSense.DDMS;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:creator elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class CreatorTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public CreatorTest() : base("creator.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Creator Fixture {
//            get {
//                try {
//                    return (new Creator(OrganizationTest.Fixture, null, null));
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
//        private Creator GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            Creator component = null;
//            try {
//                SecurityAttributesTest.Fixture.addTo(element);
//                component = new Creator(element);
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
//        /// <param name="entity"> the producer entity </param>
//        /// <param name="pocTypes"> the pocType (DDMS 4.0.1 or later) </param>
//        private Creator GetInstance(string message, IRoleEntity entity, IList<string> pocTypes) {
//            bool expectFailure = !Util.isEmpty(message);
//            Creator component = null;
//            try {
//                component = new Creator(entity, pocTypes, SecurityAttributesTest.Fixture);
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
//            text.Append(PersonTest.Fixture.getOutput(isHTML, "creator.", ""));
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "creator.pocType", "DoD-Dist-B"));
//            }
//            text.Append(BuildOutput(isHTML, "creator.classification", "U"));
//            text.Append(BuildOutput(isHTML, "creator.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:creator ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM);
//            if (version.isAtLeast("4.0.1")) {
//                xml.Append(" ISM:pocType=\"DoD-Dist-B\"");
//            }
//            xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n\t<ddms:").Append(Person.getName(version)).Append(">\n");
//            xml.Append("\t\t<ddms:name>Brian</ddms:name>\n");
//            xml.Append("\t\t<ddms:surname>Uri</ddms:surname>\n");
//            xml.Append("\t</ddms:").Append(Person.getName(version)).Append(">\n</ddms:creator>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Creator.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Creator.getName(version), null);
//                element.appendChild(PersonTest.Fixture.XOMElementCopy);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // All fields
//                GetInstance(SUCCESS, PersonTest.Fixture, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ismPrefix = PropertyReader.getPrefix("ism");

//                // Missing entity
//                XElement element = Util.buildDDMSElement(Creator.getName(version), null);
//                GetInstance("entity is required.", element);

//                if (version.isAtLeast("4.0.1")) {
//                    // Invalid pocType
//                    element = Util.buildDDMSElement(Creator.getName(version), null);
//                    element.appendChild(PersonTest.Fixture.XOMElementCopy);
//                    Util.addAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "Unknown");
//                    GetInstance("Unknown is not a valid enumeration token", element);

//                    // Partial Invalid pocType
//                    element = Util.buildDDMSElement(Creator.getName(version), null);
//                    element.appendChild(PersonTest.Fixture.XOMElementCopy);
//                    Util.addAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "DoD-Dist-B Unknown");
//                    GetInstance("Unknown is not a valid enumeration token", element);
//                }
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Missing entity
//                GetInstance("entity is required.", (IRoleEntity) null, null);

//                if (version.isAtLeast("4.0.1")) {
//                    // Invalid pocType
//                    GetInstance("Unknown is not a valid enumeration token", PersonTest.Fixture, Util.getXsListAsList("Unknown"));

//                    // Partial Invalid pocType
//                    GetInstance("Unknown is not a valid enumeration token", PersonTest.Fixture, Util.getXsListAsList("DoD-Dist-B Unknown"));
//                }
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No warnings
//                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Creator elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Creator dataComponent = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Creator elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Creator dataComponent = GetInstance(SUCCESS, new Service(Util.getXsListAsList("DISA PEO-GES"), Util.getXsListAsList("703-882-1000 703-885-1000"), Util.getXsListAsList("ddms@fgm.com")), null);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, PersonTest.Fixture, RoleEntityTest.PocTypes);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestSecurityAttributes() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Creator component = new Creator(PersonTest.Fixture, null, SecurityAttributesTest.Fixture);
//                assertEquals(SecurityAttributesTest.Fixture, component.SecurityAttributes);
//            }
//        }

//        public virtual void TestWrongVersionPocType() {
//            DDMSVersion.CurrentVersion = "3.1";
//            try {
//                new Creator(PersonTest.Fixture, Util.getXsListAsList("DoD-Dist-B"), SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot have a pocType");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Creator component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Creator.Builder builder = new Creator.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Creator.Builder builder = new Creator.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.PocTypes = Util.getXsListAsList("DoD-Dist-B");
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Creator.Builder builder = new Creator.Builder();
//                builder.EntityType = Person.getName(version);
//                builder.Person.Phones = Util.getXsListAsList("703-885-1000");
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "surname is required.");
//                }
//                builder.Person.Surname = "Uri";
//                builder.Person.Names = Util.getXsListAsList("Brian");
//                builder.commit();
//            }
//        }
//    }

//}