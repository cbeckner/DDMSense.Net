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


	
//    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using Description = DDMSense.DDMS.Summary.Description;
//    using DescriptionTest = DDMSense.Test.DDMS.Summary.DescriptionTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.ResourceElements;
//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:taskingInfo elements </para>
//    /// 
//    /// <para> Because a ddms:taskingInfo is a local component, we cannot load a valid document from a unit test data file. We
//    /// have to build the well-formed XElement ourselves. </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class TaskingInfoTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public TaskingInfoTest() : base(null) {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static XElement FixtureElement {
//            get {
//                try {
//                    DDMSVersion version = DDMSVersion.CurrentVersion;
//                    XElement element = Util.buildDDMSElement(TaskingInfo.getName(version), null);
//                    element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
//                    element.addNamespaceDeclaration(PropertyReader.getPrefix("ism"), version.IsmNamespace);
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    element.appendChild(RequesterInfoTest.GetFixtureElement(true));
//                    element.appendChild(AddresseeTest.GetFixtureElement(true));
//                    element.appendChild(DescriptionTest.Fixture.ElementCopy);
//                    element.appendChild(TaskIDTest.FixtureElement);
//                    return (element);
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static List<TaskingInfo> FixtureList {
//            get {
//                try {
//                    List<TaskingInfo> infos = new List<TaskingInfo>();
//                    infos.Add(new TaskingInfo(FixtureElement));
//                    return (infos);
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
//        private TaskingInfo GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            TaskingInfo component = null;
//            try {
//                component = new TaskingInfo(element);
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
//        /// <param name="requesterInfos"> list of requestors (optional) </param>
//        /// <param name="addressees"> list of addressee (optional) </param>
//        /// <param name="description"> description of tasking (optional) </param>
//        /// <param name="taskID"> taskID for tasking (required) </param>
//        private TaskingInfo GetInstance(string message, List<RequesterInfo> requesterInfos, List<Addressee> addressees, Description description, TaskID taskID) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            TaskingInfo component = null;
//            try {
//                component = new TaskingInfo(requesterInfos, addressees, description, taskID, SecurityAttributesTest.Fixture);
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
//            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.entityType", "organization"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.name", "DISA"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.classification", "U"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.requesterInfo.ownerProducer", "USA"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.entityType", "organization"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.name", "DISA"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.classification", "U"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.addressee.ownerProducer", "USA"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.description", "A transformation service."));
//            text.Append(BuildOutput(isHTML, "taskingInfo.description.classification", "U"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.description.ownerProducer", "USA"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID", "Task #12345"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.taskingSystem", "MDR"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.network", "NIPRNet"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.otherNetwork", "PBS"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.type", "simple"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.href", "http://en.wikipedia.org/wiki/Tank"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.role", "tank"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.title", "Tank Page"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.arcrole", "arcrole"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.show", "new"));
//            text.Append(BuildOutput(isHTML, "taskingInfo.taskID.actuate", "onLoad"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:taskingInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:requesterInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
//                xml.Append("</ddms:requesterInfo>");
//                xml.Append("<ddms:addressee ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization>");
//                xml.Append("</ddms:addressee>");
//                xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">A transformation service.</ddms:description>");
//                xml.Append("<ddms:taskID xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
//                xml.Append("ddms:taskingSystem=\"MDR\" network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
//                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:arcrole=\"arcrole\" ");
//                xml.Append("xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
//                xml.Append("</ddms:taskingInfo>");
//                return (xml.ToString());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, TaskingInfo.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, FixtureElement);

//                // No optional fields
//                XElement element = Util.buildDDMSElement(TaskingInfo.getName(version), null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                element.appendChild(TaskIDTest.Fixture.ElementCopy);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);

//                // No optional fields
//                GetInstance(SUCCESS, null, null, null, TaskIDTest.Fixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Missing taskID
//                XElement element = Util.buildDDMSElement(TaskingInfo.getName(version), null);
//                SecurityAttributesTest.Fixture.addTo(element);
//                GetInstance("taskID is required.", element);

//                // Missing security attributes
//                element = Util.buildDDMSElement(TaskingInfo.getName(version), null);
//                element.appendChild(TaskIDTest.Fixture.ElementCopy);
//                GetInstance("classification is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // Missing taskID
//                GetInstance("taskID is required.", null, null, null, null);

//                // Missing security attributes
//                try {
//                    new TaskingInfo(null, null, null, new TaskID("test", null, null, null, null), null);
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
//                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo elementComponent = GetInstance(SUCCESS, FixtureElement);
//                TaskingInfo dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo elementComponent = GetInstance(SUCCESS, FixtureElement);
//                TaskingInfo dataComponent = GetInstance(SUCCESS, null, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, null, DescriptionTest.Fixture, TaskIDTest.Fixture);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, null, TaskIDTest.Fixture);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                TaskID taskID = new TaskID("Test", null, null, null, null);
//                dataComponent = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, taskID);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, RequesterInfoTest.FixtureList, AddresseeTest.FixtureList, DescriptionTest.Fixture, TaskIDTest.Fixture);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            try {
//                DDMSVersion.CurrentVersion = "4.0.1";
//                SecurityAttributes attr = SecurityAttributesTest.Fixture;
//                DDMSVersion.SetCurrentVersion("2.0");
//                new TaskingInfo(null, null, null, new TaskID("test", null, null, null, null), attr);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "These attributes cannot decorate");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo component = GetInstance(SUCCESS, FixtureElement);
//                TaskingInfo.Builder builder = new TaskingInfo.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo.Builder builder = new TaskingInfo.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.RequesterInfos.get(1).SecurityAttributes.Classification = "U";
//                Assert.IsFalse(builder.Empty);
//                builder = new TaskingInfo.Builder();
//                builder.Addressees.get(1).SecurityAttributes.Classification = "U";
//                Assert.IsFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                TaskingInfo.Builder builder = new TaskingInfo.Builder();
//                builder.SecurityAttributes.Classification = "U";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "taskID is required.");
//                }
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.TaskID.Value = "test";
//                builder.commit();
//            }
//        }
//    }

//}