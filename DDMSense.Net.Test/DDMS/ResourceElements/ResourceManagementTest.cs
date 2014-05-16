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
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.ResourceElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:resourceManagement elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class ResourceManagementTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public ResourceManagementTest() : base("resourceManagement.xml") {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static ResourceManagement Fixture {
//            get {
//                try {
//                    return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? new ResourceManagement(null, null, null, ProcessingInfoTest.FixtureList, SecurityAttributesTest.Fixture) : null);
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
//        private ResourceManagement GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            ResourceManagement component = null;
//            try {
//                component = new ResourceManagement(element);
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
//        /// <param name="recordsManagementInfo"> records management info (optional) </param>
//        /// <param name="revisionRecall"> information about revision recalls (optional) </param>
//        /// <param name="taskingInfos"> list of tasking info (optional) </param>
//        /// <param name="processingInfos"> list of processing info (optional) </param>
//        private ResourceManagement GetInstance(string message, RecordsManagementInfo recordsManagementInfo, RevisionRecall revisionRecall, IList<TaskingInfo> taskingInfos, IList<ProcessingInfo> processingInfos) {
//            bool expectFailure = !Util.isEmpty(message);
//            ResourceManagement component = null;
//            try {
//                component = new ResourceManagement(recordsManagementInfo, revisionRecall, taskingInfos, processingInfos, SecurityAttributesTest.Fixture);
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
//            text.Append(RecordsManagementInfoTest.Fixture.getOutput(isHTML, "resourceManagement.", ""));
//            text.Append(RevisionRecallTest.TextFixture.getOutput(isHTML, "resourceManagement.", ""));
//            text.Append(TaskingInfoTest.FixtureList[0].getOutput(isHTML, "resourceManagement.", ""));
//            text.Append(ProcessingInfoTest.FixtureList[0].getOutput(isHTML, "resourceManagement.", ""));
//            text.Append(BuildOutput(isHTML, "resourceManagement.classification", "U"));
//            text.Append(BuildOutput(isHTML, "resourceManagement.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:resourceManagement ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:recordsManagementInfo ddms:vitalRecordIndicator=\"true\">");
//                xml.Append("<ddms:recordKeeper><ddms:recordKeeperID>#289-99202.9</ddms:recordKeeperID>");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:recordKeeper>");
//                xml.Append("<ddms:applicationSoftware ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("IRM Generator 2L-9</ddms:applicationSoftware>");
//                xml.Append("</ddms:recordsManagementInfo>");
//                xml.Append("<ddms:revisionRecall xmlns:xlink=\"http://www.w3.org/1999/xlink\" ddms:revisionID=\"1\" ");
//                xml.Append("ddms:revisionType=\"ADMINISTRATIVE RECALL\" network=\"NIPRNet\" otherNetwork=\"PBS\" ");
//                xml.Append("xlink:type=\"resource\" xlink:role=\"tank\" xlink:title=\"Tank Page\" xlink:label=\"tank\" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">Description of Recall</ddms:revisionRecall>");
//                xml.Append("<ddms:taskingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\"><ddms:requesterInfo ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:requesterInfo>");
//                xml.Append("<ddms:addressee ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ddms:organization><ddms:name>DISA</ddms:name></ddms:organization></ddms:addressee>");
//                xml.Append("<ddms:description ISM:classification=\"U\" ISM:ownerProducer=\"USA\">A transformation service.");
//                xml.Append("</ddms:description><ddms:taskID xmlns:xlink=\"http://www.w3.org/1999/xlink\" ");
//                xml.Append("ddms:taskingSystem=\"MDR\" network=\"NIPRNet\" otherNetwork=\"PBS\" xlink:type=\"simple\" ");
//                xml.Append("xlink:href=\"http://en.wikipedia.org/wiki/Tank\" xlink:role=\"tank\" xlink:title=\"Tank Page\" ");
//                xml.Append("xlink:arcrole=\"arcrole\" xlink:show=\"new\" xlink:actuate=\"onLoad\">Task #12345</ddms:taskID>");
//                xml.Append("</ddms:taskingInfo><ddms:processingInfo ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
//                xml.Append("ddms:dateProcessed=\"2011-08-19\">XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.");
//                xml.Append("</ddms:processingInfo></ddms:resourceManagement>");
//                return (xml.ToString());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, ResourceManagement.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(ResourceManagement.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields
//                GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);

//                // No optional fields
//                GetInstance(SUCCESS, null, null, null, null);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Too many recordsManagementInfo elements
//                XElement element = Util.buildDDMSElement(ResourceManagement.getName(version), null);
//                element.appendChild(RecordsManagementInfoTest.Fixture.XOMElementCopy);
//                element.appendChild(RecordsManagementInfoTest.Fixture.XOMElementCopy);
//                GetInstance("No more than 1 recordsManagementInfo", element);

//                // Too many revisionRecall elements
//                element = Util.buildDDMSElement(ResourceManagement.getName(version), null);
//                element.appendChild(RevisionRecallTest.TextFixtureElement);
//                element.appendChild(RevisionRecallTest.TextFixtureElement);
//                GetInstance("No more than 1 revisionRecall", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Incorrect version of security attributes
//                DDMSVersion.CurrentVersion = "2.0";
//                SecurityAttributes attributes = SecurityAttributesTest.Fixture;
//                DDMSVersion.CurrentVersion = sVersion;
//                try {
//                    new ResourceManagement(null, null, null, null, attributes);
//                    fail("Allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "These attributes cannot decorate");
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // No warnings
//                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                ResourceManagement dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                ResourceManagement dataComponent = GetInstance(SUCCESS, null, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, null, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, null, ProcessingInfoTest.FixtureList);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, null);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, RecordsManagementInfoTest.Fixture, RevisionRecallTest.TextFixture, TaskingInfoTest.FixtureList, ProcessingInfoTest.FixtureList);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            try {
//                DDMSVersion.CurrentVersion = "2.0";
//                new ResourceManagement(null, null, null, null, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The resourceManagement element cannot be used");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                ResourceManagement.Builder builder = new ResourceManagement.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement.Builder builder = new ResourceManagement.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.TaskingInfos.get(1).SecurityAttributes.Classification = "U";
//                assertFalse(builder.Empty);
//                builder = new ResourceManagement.Builder();
//                builder.ProcessingInfos.get(1).SecurityAttributes.Classification = "U";
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                ResourceManagement.Builder builder = new ResourceManagement.Builder();
//                builder.SecurityAttributes.Classification = "COW";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "COW is not a valid enumeration token for this attribute");
//                }
//                builder.SecurityAttributes.Classification = "U";
//                builder.commit();
//            }
//        }
//    }

//}