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
//namespace DDMSense.Test.DDMS.SecurityElements {

	
//    using Notice = DDMSense.DDMS.SecurityElements.Ism.Notice;
//    using NoticeTest = DDMSense.Test.DDMS.SecurityElements.Ism.NoticeTest;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.SecurityElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:noticeList elements </para>
//    /// 
//    /// <para> Because a ddms:noticeList is a local component, we cannot load a valid document from a unit test data file. We
//    /// have to build the well-formed XElement ourselves. </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class NoticeListTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public NoticeListTest() : base(null) {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static XElement FixtureElement {
//            get {
//                try {
//                    DDMSVersion version = DDMSVersion.CurrentVersion;
    
//                    XElement element = Util.buildDDMSElement(NoticeList.getName(version), null);
//                    element.addNamespaceDeclaration(PropertyReader.getPrefix("ddms"), version.Namespace);
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    element.appendChild(NoticeTest.FixtureElement);
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
//        public static NoticeList Fixture {
//            get {
//                try {
//                    return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? new NoticeList(FixtureElement) : null);
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
//        private NoticeList GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            NoticeList component = null;
//            try {
//                component = new NoticeList(element);
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
//        /// <param name="notices"> the notices (at least 1 required) </param>
//        /// <returns> a valid object </returns>
//        private NoticeList GetInstance(string message, List<Notice> notices) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            NoticeList component = null;
//            try {
//                component = new NoticeList(notices, SecurityAttributesTest.Fixture);
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
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText", "noticeText"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.pocType", "DoD-Dist-B"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.classification", "U"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.ownerProducer", "USA"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.classification", "U"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.ownerProducer", "USA"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeType", "DoD-Dist-B"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeReason", "noticeReason"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeDate", "2011-09-15"));
//            text.Append(BuildOutput(isHTML, "noticeList.notice.unregisteredNoticeType", "unregisteredNoticeType"));
//            if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
//                text.Append(BuildOutput(isHTML, "noticeList.notice.externalNotice", "false"));
//            }
//            text.Append(BuildOutput(isHTML, "noticeList.classification", "U"));
//            text.Append(BuildOutput(isHTML, "noticeList.ownerProducer", "USA"));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:noticeList ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ISM:Notice ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
//                xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
//                if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
//                    xml.Append(" ISM:externalNotice=\"false\"");
//                }
//                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText>");
//                xml.Append("</ISM:Notice>");
//                xml.Append("</ddms:noticeList>");
//                return (xml.ToString());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, NoticeList.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, FixtureElement);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // All fields
//                GetInstance(SUCCESS, NoticeTest.FixtureList);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // No Notices
//                XElement element = new XElement(FixtureElement);
//                element.removeChildren();
//                GetInstance("At least one ISM:Notice", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                // No Notices
//                GetInstance("At least one ISM:Notice", (List) null);

//                // No attributes
//                try {
//                    new NoticeList(NoticeTest.FixtureList, null);
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

//                NoticeList component = GetInstance(SUCCESS, FixtureElement);

//                // 4.1 ISM:externalNotice used
//                if (version.isAtLeast("4.1")) {
//                    Assert.Equals(1, component.ValidationWarnings.size());
//                    string text = "The ISM:externalNotice attribute in this DDMS component";
//                    string locator = "ddms:noticeList/ISM:Notice";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
//                }
//                // No warnings 
//                else {
//                    Assert.Equals(0, component.ValidationWarnings.size());
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList elementComponent = GetInstance(SUCCESS, FixtureElement);
//                NoticeList dataComponent = GetInstance(SUCCESS, NoticeTest.FixtureList);

//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                List<Notice> list = NoticeTest.FixtureList;
//                list.Add(new Notice(NoticeTest.FixtureElement));
//                NoticeList elementComponent = GetInstance(SUCCESS, FixtureElement);
//                NoticeList dataComponent = GetInstance(SUCCESS, list);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList component = GetInstance(SUCCESS, FixtureElement);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, NoticeTest.FixtureList);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList component = GetInstance(SUCCESS, FixtureElement);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, NoticeTest.FixtureList);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersion() {
//            // Implicit, since 1 Notice is required and that requires DDMS 4.0.1 or greater.
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList component = GetInstance(SUCCESS, FixtureElement);
//                NoticeList.Builder builder = new NoticeList.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList.Builder builder = new NoticeList.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Notices.get(1).NoticeTexts.get(1).Value = "TEST";
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                NoticeList.Builder builder = new NoticeList.Builder();
//                builder.SecurityAttributes.Classification = "U";
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "At least one ISM:Notice");
//                }
//                builder.Notices[0].NoticeTexts[0].Value = "test";
//                builder.Notices[0].SecurityAttributes.Classification = "U";
//                builder.Notices[0].SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.Notices[0].NoticeTexts[0].SecurityAttributes.Classification = "U";
//                builder.Notices[0].NoticeTexts[0].SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                NoticeList.Builder builder = new NoticeList.Builder();
//                assertNotNull(builder.Notices.get(1));
//            }
//        }
//    }

//}