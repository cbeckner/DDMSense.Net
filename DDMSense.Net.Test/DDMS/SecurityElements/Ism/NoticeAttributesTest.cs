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
//namespace DDMSense.Test.DDMS.SecurityElements.Ism {

	
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.SecurityElements.Ism;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to the ISM notice attributes </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 2.0.0
//    /// </summary>
//    public class NoticeAttributesTest : AbstractBaseTestCase {

//        private const string TEST_NOTICE_TYPE = "DoD-Dist-B";
//        private const string TEST_NOTICE_REASON = "noticeReason";
//        private const string TEST_NOTICE_DATE = "2011-09-15";
//        private const string TEST_UNREGISTERED_NOTICE_TYPE = "unregisteredNoticeType";
//        private static readonly bool? TEST_EXTERNAL = false;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public NoticeAttributesTest() : base(null) {
//            RemoveSupportedVersions("2.0 3.0 3.1");
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static NoticeAttributes Fixture {
//            get {
//                try {
//                    DDMSVersion version = DDMSVersion.CurrentVersion;
//                    bool? externalNotice = version.isAtLeast("4.1") ? false : null;
//                    return (new NoticeAttributes(TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, externalNotice));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a dummy value for the externalNotice attribute, based upon the current DDMS version.
//        /// </summary>
//        private static bool? ExternalNotice {
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
//        private NoticeAttributes GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            NoticeAttributes attributes = null;
//            try {
//                attributes = new NoticeAttributes(element);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="noticeType"> the notice type (with a value from the CVE) </param>
//        /// <param name="noticeReason"> the reason associated with a notice </param>
//        /// <param name="noticeDate"> the date associated with a notice </param>
//        /// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
//        /// <param name="externalNotice"> the external notice attribute </param>
//        /// <returns> a valid object </returns>
//        private NoticeAttributes GetInstance(string message, string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType, bool? externalNotice) {
//            bool expectFailure = !Util.isEmpty(message);
//            NoticeAttributes attributes = null;
//            try {
//                attributes = new NoticeAttributes(noticeType, noticeReason, noticeDate, unregisteredNoticeType, externalNotice);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (attributes);
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // All fields
//                XElement element = Util.buildDDMSElement(Resource.getName(version), null);
//                Fixture.addTo(element);
//                GetInstance(SUCCESS, element);

//                // No optional fields
//                element = Util.buildDDMSElement(Resource.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields
//                GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                // No optional fields
//                GetInstance(SUCCESS, null, null, null, null, ExternalNotice);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string ismPrefix = PropertyReader.getPrefix("ism");
//                string icNamespace = version.IsmNamespace;

//                // invalid noticeType
//                XElement element = Util.buildDDMSElement(Resource.getName(version), null);
//                Util.addAttribute(element, ismPrefix, NoticeAttributes.NOTICE_TYPE_NAME, icNamespace, "Unknown");
//                GetInstance("Unknown is not a valid enumeration token", element);

//                // invalid noticeDate
//                element = Util.buildDDMSElement(Resource.getName(version), null);
//                Util.addAttribute(element, ismPrefix, NoticeAttributes.NOTICE_DATE_NAME, icNamespace, "2001");
//                GetInstance("The noticeDate attribute must be in the xs:date format", element);

//                StringBuilder longString = new StringBuilder();
//                for (int i = 0; i < NoticeAttributes.MAX_LENGTH / 10 + 1; i++) {
//                    longString.Append("0123456789");
//                }

//                // too long noticeReason
//                element = Util.buildDDMSElement(Resource.getName(version), null);
//                Util.addAttribute(element, ismPrefix, NoticeAttributes.NOTICE_REASON_NAME, icNamespace, longString.ToString());
//                GetInstance("The noticeReason attribute must be shorter", element);

//                // too long unregisteredNoticeType
//                element = Util.buildDDMSElement(Resource.getName(version), null);
//                Util.addAttribute(element, ismPrefix, NoticeAttributes.UNREGISTERED_NOTICE_TYPE_NAME, icNamespace, longString.ToString());
//                GetInstance("The unregisteredNoticeType attribute must be shorter", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // invalid noticeType
//                GetInstance("Unknown is not a valid enumeration token", "Unknown", TEST_NOTICE_REASON, "2001", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                // horribly invalid noticeDate
//                GetInstance("The ISM:noticeDate attribute is not in a valid date format.", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "baboon", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                // invalid noticeDate
//                GetInstance("The noticeDate attribute must be in the xs:date format", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "2001", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                StringBuilder longString = new StringBuilder();
//                for (int i = 0; i < NoticeAttributes.MAX_LENGTH / 10 + 1; i++) {
//                    longString.Append("0123456789");
//                }

//                // too long noticeReason
//                GetInstance("The noticeReason attribute must be shorter", TEST_NOTICE_TYPE, longString.ToString(), TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                // too long unregisteredNoticeType
//                GetInstance("The unregisteredNoticeType attribute must be shorter", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, longString.ToString(), ExternalNotice);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // No warnings
//                XElement element = Util.buildDDMSElement(Resource.getName(version), null);

//                Fixture.addTo(element);
//                NoticeAttributes attr = GetInstance(SUCCESS, element);
//                assertEquals(0, attr.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDeprecatedConstructor() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDeprecatedConstructor() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes attr = new NoticeAttributes(TEST_NOTICE_TYPE, null, null, null);
//                assertNull(attr.ExternalReference);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string icNamespace = version.IsmNamespace;

//                XElement element = Util.buildDDMSElement(Resource.getName(version), null);
//                Util.addAttribute(element, PropertyReader.getPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
//                Fixture.addTo(element);
//                NoticeAttributes elementAttributes = GetInstance(SUCCESS, element);
//                NoticeAttributes dataAttributes = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

//                assertEquals(elementAttributes, elementAttributes);
//                assertEquals(elementAttributes, dataAttributes);
//                assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                XElement element = Util.buildDDMSElement(Resource.getName(version), null);
//                Fixture.addTo(element);
//                NoticeAttributes expected = GetInstance(SUCCESS, element);

//                NoticeAttributes test = GetInstance(SUCCESS, "DoD-Dist-C", TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
//                assertFalse(expected.Equals(test));

//                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, DIFFERENT_VALUE, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
//                assertFalse(expected.Equals(test));

//                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "2011-08-15", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
//                assertFalse(expected.Equals(test));

//                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, DIFFERENT_VALUE, ExternalNotice);
//                assertFalse(expected.Equals(test));

//                if (version.isAtLeast("4.1")) {
//                    test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, null);
//                    assertFalse(expected.Equals(test));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes elementAttributes = Fixture;
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(elementAttributes.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testAddTo() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestAddTo() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                NoticeAttributes component = Fixture;

//                XElement element = Util.buildDDMSElement("sample", null);
//                component.addTo(element);
//                NoticeAttributes output = new NoticeAttributes(element);
//                assertEquals(component, output);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testGetNonNull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestGetNonNull() {
//            NoticeAttributes component = new NoticeAttributes(null, null, null, null);
//            NoticeAttributes output = NoticeAttributes.getNonNullInstance(null);
//            assertEquals(component, output);
//            assertTrue(output.Empty);

//            output = NoticeAttributes.getNonNullInstance(Fixture);
//            assertEquals(Fixture, output);
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersion() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersion() {
//            DDMSVersion version = DDMSVersion.setCurrentVersion("2.0");
//            XElement element = Util.buildDDMSElement(Resource.getName(version), null);
//            Util.addAttribute(element, PropertyReader.getPrefix("ism"), NoticeAttributes.NOTICE_DATE_NAME, version.IsmNamespace, "2011-09-15");
//            GetInstance("Notice attributes cannot be used", element);

//            DDMSVersion.CurrentVersion = "4.0.1";
//            NoticeAttributes attributes = Fixture;
//            try {
//                attributes.addTo(element);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "These attributes cannot decorate");
//            }
//        }

//        public virtual void TestIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes dataAttributes = GetInstance(SUCCESS, null, null, null, null, null);
//                assertTrue(dataAttributes.Empty);
//                dataAttributes = GetInstance(SUCCESS, null, null, null, TEST_UNREGISTERED_NOTICE_TYPE, null);
//                assertFalse(dataAttributes.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDateOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDateOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes dataAttributes = GetInstance(SUCCESS, null, null, "2005-10-10", null, null);
//                assertEquals(BuildOutput(true, "noticeDate", "2005-10-10"), dataAttributes.getOutput(true, ""));
//                assertEquals(BuildOutput(false, "noticeDate", "2005-10-10"), dataAttributes.getOutput(false, ""));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes component = Fixture;
//                NoticeAttributes.Builder builder = new NoticeAttributes.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes.Builder builder = new NoticeAttributes.Builder();
//                assertNotNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.NoticeType = "";
//                assertTrue(builder.Empty);
//                builder.NoticeReason = TEST_NOTICE_REASON;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                NoticeAttributes.Builder builder = new NoticeAttributes.Builder();
//                builder.NoticeDate = "2001";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "The noticeDate attribute must be in the xs:date format");
//                }
//                builder.NoticeDate = "2011-01-20";
//                builder.commit();
//            }
//        }
//    }

//}