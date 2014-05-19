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

	
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using Access = DDMSense.DDMS.SecurityElements.Ntk.Access;
//    using AccessTest = DDMSense.Test.DDMS.SecurityElements.Ntk.AccessTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.SecurityElements;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:security elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class SecurityTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public SecurityTest() : base("security.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Security Fixture {
//            get {
//                try {
//                    return (new Security(null, null, SecurityAttributesTest.Fixture));
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
//        private Security GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Security component = null;
//            try {
//                component = new Security(element);
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
//        /// <param name="noticeList"> the notice list (optional) </param>
//        /// <param name="access"> NTK access information (optional) </param>
//        /// <returns> a valid object </returns>
//        private Security GetInstance(string message, NoticeList noticeList, Access access) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Security component = null;
//            try {
//                component = new Security(noticeList, access, SecurityAttributesTest.Fixture);
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
//            string prefix = "security.";
//            StringBuilder text = new StringBuilder();
//            if (version.isAtLeast("3.0")) {
//                text.Append(BuildOutput(isHTML, prefix + "excludeFromRollup", "true"));
//            }
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeText", "noticeText"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeText.pocType", "DoD-Dist-B"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeText.classification", "U"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeText.ownerProducer", "USA"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.classification", "U"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.ownerProducer", "USA"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeType", "DoD-Dist-B"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeReason", "noticeReason"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.noticeDate", "2011-09-15"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.unregisteredNoticeType", "unregisteredNoticeType"));
//                if (version.isAtLeast("4.1")) {
//                    text.Append(BuildOutput(isHTML, prefix + "noticeList.notice.externalNotice", "false"));
//                }
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.classification", "U"));
//                text.Append(BuildOutput(isHTML, prefix + "noticeList.ownerProducer", "USA"));
//                text.Append(AccessTest.Fixture.getOutput(isHTML, "security.", ""));
//            }
//            text.Append(SecurityAttributesTest.Fixture.getOutput(isHTML, prefix));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:security ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
//            if (version.isAtLeast("3.0")) {
//                xml.Append("ISM:excludeFromRollup=\"true\" ");
//            }
//            xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
//            if (!version.isAtLeast("4.0.1")) {
//                xml.Append(" />");
//            } else {
//                xml.Append(">\n");
//                xml.Append("\t<ddms:noticeList ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t\t<ISM:Notice ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
//                if (version.isAtLeast("4.1")) {
//                    xml.Append(" ISM:externalNotice=\"false\"");
//                }
//                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t\t\t<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText>\n");
//                xml.Append("\t\t</ISM:Notice>\n");
//                xml.Append("\t</ddms:noticeList>\n");
//                xml.Append("\t<ntk:Access xmlns:ntk=\"urn:us:gov:ic:ntk\" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t\t<ntk:AccessIndividualList>\n");
//                xml.Append("\t\t\t<ntk:AccessIndividual ISM:classification=\"U\" ISM:ownerProducer=\"USA\">\n");
//                xml.Append("\t\t\t\t<ntk:AccessSystemName ISM:classification=\"U\" ISM:ownerProducer=\"USA\">DIAS</ntk:AccessSystemName>\n");
//                xml.Append("\t\t\t\t<ntk:AccessIndividualValue ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
//                xml.Append("user_2321889:Doe_John_H</ntk:AccessIndividualValue>\n");
//                xml.Append("\t\t\t</ntk:AccessIndividual>\n");
//                xml.Append("\t\t</ntk:AccessIndividualList>\n");
//                xml.Append("\t</ntk:Access>\n");
//                xml.Append("</ddms:security>");
//            }
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Security.getName(version));
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
//                if (version.isAtLeast("4.0.1")) {
//                    XElement element = Util.buildDDMSElement(Security.getName(version), null);
//                    Util.addAttribute(element, PropertyReader.getPrefix("ism"), "excludeFromRollup", version.IsmNamespace, "true");
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    GetInstance(SUCCESS, element);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, NoticeListTest.Fixture, AccessTest.Fixture);

//                // No optional fields
//                GetInstance(SUCCESS, null, null);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Missing excludeFromRollup
//                if (version.isAtLeast("3.0")) {
//                    XElement element = Util.buildDDMSElement(Security.getName(version), null);
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    GetInstance("The excludeFromRollup attribute is required.", element);

//                    // Incorrect excludeFromRollup
//                    element = Util.buildDDMSElement(Security.getName(version), null);
//                    Util.addAttribute(element, PropertyReader.getPrefix("ism"), "excludeFromRollup", version.IsmNamespace, "false");
//                    GetInstance("The excludeFromRollup attribute must have a fixed value", element);

//                    // Invalid excludeFromRollup
//                    element = Util.buildDDMSElement(Security.getName(version), null);
//                    Util.addAttribute(element, PropertyReader.getPrefix("ism"), "excludeFromRollup", version.IsmNamespace, "aardvark");
//                    GetInstance("The excludeFromRollup attribute is required.", element);
//                }
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Bad security attributes
//                try {
//                    new Security(null, null, null);
//                    fail("Allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "security attributes is required.");
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Security component = GetInstance(SUCCESS, GetValidElement(sVersion));

//                // 4.1 ISM:externalNotice used
//                if (version.isAtLeast("4.1")) {
//                    Assert.Equals(1, component.ValidationWarnings.size());
//                    string text = "The ISM:externalNotice attribute in this DDMS component";
//                    string locator = "ddms:security/ddms:noticeList/ISM:Notice";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
//                }
//                // No warnings 
//                else {
//                    Assert.Equals(0, component.ValidationWarnings.size());
//                }

//                // Nested warnings
//                if (version.isAtLeast("4.0.1")) {
//                    XElement element = Util.buildDDMSElement(Security.getName(version), null);
//                    Util.addAttribute(element, PropertyReader.getPrefix("ism"), "excludeFromRollup", version.IsmNamespace, "true");
//                    XElement accessElement = Util.buildElement(PropertyReader.getPrefix("ntk"), Access.getName(version), version.NtkNamespace, null);
//                    SecurityAttributesTest.Fixture.addTo(accessElement);
//                    element.appendChild(accessElement);
//                    SecurityAttributesTest.Fixture.addTo(element);
//                    component = GetInstance(SUCCESS, element);
//                    Assert.Equals(1, component.ValidationWarnings.size());
//                    string text = "An ntk:Access element was found with no";
//                    string locator = "ddms:security/ntk:Access";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Security elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Security dataComponent = GetInstance(SUCCESS, NoticeListTest.Fixture, AccessTest.Fixture);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                if (version.isAtLeast("4.0.1")) {
//                    Security elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                    Security dataComponent = GetInstance(SUCCESS, NoticeListTest.Fixture, null);
//                    Assert.IsFalse(elementComponent.Equals(dataComponent));

//                    dataComponent = GetInstance(SUCCESS, null, AccessTest.Fixture);
//                    Assert.IsFalse(elementComponent.Equals(dataComponent));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Security component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, NoticeListTest.Fixture, AccessTest.Fixture);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Security component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, NoticeListTest.Fixture, AccessTest.Fixture);
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionExcludeFromRollup() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionExcludeFromRollup() {
//            DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
//            string icPrefix = PropertyReader.getPrefix("ism");
//            string icNamespace = version.IsmNamespace;

//            XElement element = Util.buildDDMSElement("security", null);
//            Util.addAttribute(element, icPrefix, "classification", icNamespace, "U");
//            Util.addAttribute(element, icPrefix, "ownerProducer", icNamespace, "USA");
//            Util.addAttribute(element, icPrefix, "excludeFromRollup", icNamespace, "true");
//            try {
//                new Security(element);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The excludeFromRollup attribute cannot be used");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Security component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Security.Builder builder = new Security.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Security.Builder builder = new Security.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.SecurityAttributes.Classification = "U";
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Security.Builder builder = new Security.Builder();
//                builder.SecurityAttributes.Classification = "SuperSecret";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "SuperSecret is not a valid enumeration token");
//                }
//                builder.SecurityAttributes.Classification = "U";
//                builder.SecurityAttributes.OwnerProducers = Util.getXsListAsList("USA");
//                builder.commit();
//            }
//        }
//    }

//}