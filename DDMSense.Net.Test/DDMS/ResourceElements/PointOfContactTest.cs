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
//    using DDMSense.DDMS;
//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:pointOfContact elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class PointOfContactTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public PointOfContactTest() : base("pointOfContact.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static PointOfContact Fixture {
//            get {
//                try {
//                    return (new PointOfContact(DDMSVersion.CurrentVersion.isAtLeast("3.0") ? UnknownTest.Fixture : PersonTest.Fixture, null, null));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing. organization to act as an entity
//        /// </summary>
//        private IRoleEntity EntityFixture {
//            get {
//                if ("2.0".Equals(DDMSVersion.CurrentVersion.Version)) {
//                    return (ServiceTest.Fixture);
//                }
//                return (UnknownTest.Fixture);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private PointOfContact GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            PointOfContact component = null;
//            try {
//                SecurityAttributesTest.Fixture.addTo(element);
//                component = new PointOfContact(element);
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
//        private PointOfContact GetInstance(string message, IRoleEntity entity, List<string> pocTypes) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            PointOfContact component = null;
//            try {
//                component = new PointOfContact(entity, pocTypes, SecurityAttributesTest.Fixture);
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
//            text.Append(((AbstractBaseComponent) EntityFixture).getOutput(isHTML, "pointOfContact.", ""));
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "pointOfContact.pocType", "DoD-Dist-B"));
//            }
//            text.Append(BuildOutput(isHTML, "pointOfContact.classification", "U"));
//            text.Append(BuildOutput(isHTML, "pointOfContact.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:pointOfContact ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM);
//            if (version.isAtLeast("4.0.1")) {
//                xml.Append(" ISM:pocType=\"DoD-Dist-B\"");
//            }
//            xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n\t");
//            if ("2.0".Equals(version.Version)) {
//                xml.Append("<ddms:").Append(Service.getName(version)).Append(">\n");
//                xml.Append("\t\t<ddms:name>https://metadata.dod.mil/ebxmlquery/soap</ddms:name>\n");
//                xml.Append("\t</ddms:").Append(Service.getName(version)).Append(">");
//            } else {
//                xml.Append("<ddms:").Append(Unknown.getName(version)).Append(">\n");
//                xml.Append("\t\t<ddms:name>UnknownEntity</ddms:name>\n");
//                xml.Append("\t</ddms:").Append(Unknown.getName(version)).Append(">");
//            }
//            xml.Append("\n</ddms:pointOfContact>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, PointOfContact.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(PointOfContact.getName(version), null);
//                element.appendChild(EntityFixture.ElementCopy);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, EntityFixture, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ismPrefix = PropertyReader.getPrefix("ism");

//                // Missing entity
//                XElement element = Util.buildDDMSElement(PointOfContact.getName(version), null);
//                GetInstance("entity is required.", element);

//                if (version.isAtLeast("4.0.1")) {
//                    // Invalid pocType
//                    element = Util.buildDDMSElement(PointOfContact.getName(version), null);
//                    element.appendChild(EntityFixture.ElementCopy);
//                    Util.addAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "Unknown");
//                    GetInstance("Unknown is not a valid enumeration token", element);

//                    // Partial Invalid pocType
//                    element = Util.buildDDMSElement(PointOfContact.getName(version), null);
//                    element.appendChild(EntityFixture.ElementCopy);
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
//                    GetInstance("Unknown is not a valid enumeration token", EntityFixture, Util.getXsListAsList("Unknown"));

//                    // Partial Invalid pocType
//                    GetInstance("Unknown is not a valid enumeration token", EntityFixture, Util.getXsListAsList("DoD-Dist-B Unknown"));
//                }
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // No warnings
//                PointOfContact component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                PointOfContact elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                PointOfContact dataComponent = GetInstance(SUCCESS, EntityFixture, RoleEntityTest.PocTypes);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                PointOfContact elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                PointOfContact dataComponent = GetInstance(SUCCESS, new Service(Util.getXsListAsList("DISA PEO-GES"), Util.getXsListAsList("703-882-1000 703-885-1000"), Util.getXsListAsList("ddms@fgm.com")), null);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                PointOfContact component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, EntityFixture, RoleEntityTest.PocTypes);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                PointOfContact component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, EntityFixture, RoleEntityTest.PocTypes);
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestSecurityAttributes() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                PointOfContact component = new PointOfContact(EntityFixture, null, SecurityAttributesTest.Fixture);
//                Assert.Equals(SecurityAttributesTest.Fixture, component.SecurityAttributes);
//            }
//        }

//        public virtual void TestWrongVersionPocType() {
//            DDMSVersion.CurrentVersion = "3.1";
//            try {
//                new PointOfContact(EntityFixture, Util.getXsListAsList("DoD-Dist-B"), SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot have a pocType");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                PointOfContact component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                PointOfContact.Builder builder = new PointOfContact.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                PointOfContact.Builder builder = new PointOfContact.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.PocTypes = Util.getXsListAsList("DoD-Dist-B");
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                PointOfContact.Builder builder = new PointOfContact.Builder();
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