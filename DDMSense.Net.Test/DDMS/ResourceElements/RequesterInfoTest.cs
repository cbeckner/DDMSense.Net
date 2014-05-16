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
//    using DDMSense.DDMS;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:requesterInfo elements </para>
//    /// 
//    /// <para> Because a ddms:requesterInfo is a local component, we cannot load a valid document from a unit test data file. We
//    /// have to build the well-formed XElement ourselves. </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class RequesterInfoTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public RequesterInfoTest() : base(null) {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        /// <param name="useOrg"> true to put an organization in, false for a person </param>
//        public static XElement GetFixtureElement(bool useOrg) {
//            try {
//                DDMSVersion version = DDMSVersion.CurrentVersion;
//                XElement element = Util.buildDDMSElement(RequesterInfo.getName(version), null);
//                element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
//                element.appendChild(useOrg ? OrganizationTest.Fixture.XOMElementCopy : PersonTest.Fixture.XOMElementCopy);
//                SecurityAttributesTest.Fixture.addTo(element);
//                return (element);
//            } catch (InvalidDDMSException e) {
//                fail("Could not create fixture: " + e.Message);
//            }
//            return (null);
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static IList<RequesterInfo> FixtureList {
//            get {
//                try {
//                    IList<RequesterInfo> list = new List<RequesterInfo>();
//                    list.Add(new RequesterInfo(RequesterInfoTest.GetFixtureElement(true)));
//                    return (list);
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
//        private RequesterInfo GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            RequesterInfo component = null;
//            try {
//                component = new RequesterInfo(element);
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
//        /// <param name="entity"> the person or organization in this role </param>
//        /// <param name="org"> the organization </param>
//        private RequesterInfo GetInstance(string message, IRoleEntity entity) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            RequesterInfo component = null;
//            try {
//                component = new RequesterInfo(entity, SecurityAttributesTest.Fixture);
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
//            text.Append(BuildOutput(isHTML, "requesterInfo.entityType", "organization"));
//            text.Append(BuildOutput(isHTML, "requesterInfo.name", "DISA"));
//            text.Append(BuildOutput(isHTML, "requesterInfo.classification", "U"));
//            text.Append(BuildOutput(isHTML, "requesterInfo.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:requesterInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
//                xml.Append("</ddms:requesterInfo>");
//                return (xml.ToString());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(true)), DEFAULT_DDMS_PREFIX, RequesterInfo.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields, organization
//                GetInstance(SUCCESS, GetFixtureElement(true));

//                // All fields, person
//                GetInstance(SUCCESS, GetFixtureElement(false));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields, organization
//                GetInstance(SUCCESS, OrganizationTest.Fixture);

//                // All fields, person
//                GetInstance(SUCCESS, PersonTest.Fixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Missing entity
//                XElement element = Util.buildDDMSElement(RequesterInfo.getName(version), null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                GetInstance("entity is required.", element);

//                // Missing security attributes
//                element = Util.buildDDMSElement(RequesterInfo.getName(version), null);
//                element.appendChild(OrganizationTest.Fixture.XOMElementCopy);
//                GetInstance("classification is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // Missing entity
//                GetInstance("entity is required.", (IRoleEntity) null);

//                // Wrong entity
//                GetInstance("The entity must be a person or an organization.", new Service(Util.getXsListAsList("Service"), null, null));

//                // Missing security attributes
//                try {
//                    new RequesterInfo(OrganizationTest.Fixture, null);
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
//                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
//                RequesterInfo dataComponent = GetInstance(SUCCESS, OrganizationTest.Fixture);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
//                RequesterInfo dataComponent = GetInstance(SUCCESS, PersonTest.Fixture);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo elementComponent = GetInstance(SUCCESS, GetFixtureElement(true));
//                Rights wrongComponent = new Rights(true, true, true);
//                Assert.IsFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, OrganizationTest.Fixture);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            try {
//                DDMSVersion.SetCurrentVersion("2.0");
//                new RequesterInfo(OrganizationTest.Fixture, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The requesterInfo element cannot be used");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // Equality after Building, organization
//                RequesterInfo component = GetInstance(SUCCESS, GetFixtureElement(true));
//                RequesterInfo.Builder builder = new RequesterInfo.Builder(component);
//                Assert.Equals(component, builder.commit());

//                // Equality after Building, person
//                component = GetInstance(SUCCESS, GetFixtureElement(false));
//                builder = new RequesterInfo.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo.Builder builder = new RequesterInfo.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Person.Surname = "surname";
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                RequesterInfo.Builder builder = new RequesterInfo.Builder();
//                builder.Person.Names = Util.getXsListAsList("Brian");
//                builder.Person.Surname = "Uri";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "classification is required.");
//                }
//                builder.SecurityAttributes.Classification = "U";
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }
//    }

//}