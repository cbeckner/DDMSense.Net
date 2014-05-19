using System.Text;

/* Copyright 2010 - 2013 by Brian Uri!
   
   This file is part of DDMSence.
   
   This library is free software; you can redistribute it and/or modify
   it under the terms of version 3.0 of the GNU Lesser General Public 
   License as published by the Free Software Foundation.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
   GNU Lesser General Public License for more details.
   
   You should have received a copy of the GNU Lesser General Public 
   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

   You can contact the author at ddmsence@urizone.net. The DDMSence
   home page is located at http://ddmsence.urizone.net/
 */
namespace DDMSense.Test.DDMS.SecurityElements.Ism
{


    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.SecurityElements.Ism;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using DDMSense.DDMS.SecurityElements;
    using DDMSense.DDMS.ResourceElements;
    using System.Linq;

    /// <summary>
    /// <para> Tests related to the ISM notice attributes </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class NoticeAttributesTest : AbstractBaseTestCase
    {

        private const string TEST_NOTICE_TYPE = "DoD-Dist-B";
        private const string TEST_NOTICE_REASON = "noticeReason";
        private const string TEST_NOTICE_DATE = "2011-09-15";
        private const string TEST_UNREGISTERED_NOTICE_TYPE = "unregisteredNoticeType";
        private static readonly bool? TEST_EXTERNAL = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public NoticeAttributesTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static NoticeAttributes Fixture
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;
                    bool? externalNotice = version.IsAtLeast("4.1") ? false : (bool?)null;
                    return (new NoticeAttributes(TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, externalNotice));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a dummy value for the externalNotice attribute, based upon the current DDMS version.
        /// </summary>
        private static bool? ExternalNotice
        {
            get
            {
                return (DDMSVersion.CurrentVersion.IsAtLeast("4.1") ? TEST_EXTERNAL : null);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private NoticeAttributes GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeAttributes attributes = null;
            try
            {
                attributes = new NoticeAttributes(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="noticeType"> the notice type (with a value from the CVE) </param>
        /// <param name="noticeReason"> the reason associated with a notice </param>
        /// <param name="noticeDate"> the date associated with a notice </param>
        /// <param name="unregisteredNoticeType"> a notice type not in the CVE </param>
        /// <param name="externalNotice"> the external notice attribute </param>
        /// <returns> a valid object </returns>
        private NoticeAttributes GetInstance(string message, string noticeType, string noticeReason, string noticeDate, string unregisteredNoticeType, bool? externalNotice)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeAttributes attributes = null;
            try
            {
                attributes = new NoticeAttributes(noticeType, noticeReason, noticeDate, unregisteredNoticeType, externalNotice);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (attributes);
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Fixture.AddTo(element);
                GetInstance(SUCCESS, element);

                // No optional fields
                element = Util.BuildDDMSElement(Resource.GetName(version), null);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                // No optional fields
                GetInstance(SUCCESS, null, null, null, null, ExternalNotice);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");
                string icNamespace = version.IsmNamespace;

                // invalid noticeType
                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, NoticeAttributes.NOTICE_TYPE_NAME, icNamespace, "Unknown");
                GetInstance("Unknown is not a valid enumeration token", element);

                // invalid noticeDate
                element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, NoticeAttributes.NOTICE_DATE_NAME, icNamespace, "2001");
                GetInstance("The noticeDate attribute must be in the xs:date format", element);

                StringBuilder longString = new StringBuilder();
                for (int i = 0; i < NoticeAttributes.MAX_LENGTH / 10 + 1; i++)
                {
                    longString.Append("0123456789");
                }

                // too long noticeReason
                element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, NoticeAttributes.NOTICE_REASON_NAME, icNamespace, longString.ToString());
                GetInstance("The noticeReason attribute must be shorter", element);

                // too long unregisteredNoticeType
                element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, ismPrefix, NoticeAttributes.UNREGISTERED_NOTICE_TYPE_NAME, icNamespace, longString.ToString());
                GetInstance("The unregisteredNoticeType attribute must be shorter", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // invalid noticeType
                GetInstance("Unknown is not a valid enumeration token", "Unknown", TEST_NOTICE_REASON, "2001", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                // horribly invalid noticeDate
                GetInstance("The ISM:noticeDate attribute is not in a valid date format.", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "baboon", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                // invalid noticeDate
                GetInstance("The noticeDate attribute must be in the xs:date format", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "2001", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                StringBuilder longString = new StringBuilder();
                for (int i = 0; i < NoticeAttributes.MAX_LENGTH / 10 + 1; i++)
                {
                    longString.Append("0123456789");
                }

                // too long noticeReason
                GetInstance("The noticeReason attribute must be shorter", TEST_NOTICE_TYPE, longString.ToString(), TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                // too long unregisteredNoticeType
                GetInstance("The unregisteredNoticeType attribute must be shorter", TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, longString.ToString(), ExternalNotice);
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);

                Fixture.AddTo(element);
                NoticeAttributes attr = GetInstance(SUCCESS, element);
                Assert.Equals(0, attr.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestDeprecatedConstructor()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes attr = new NoticeAttributes(TEST_NOTICE_TYPE, null, null, null);
                Assert.IsNull(attr.ExternalReference);
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string icNamespace = version.IsmNamespace;

                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
                Fixture.AddTo(element);
                NoticeAttributes elementAttributes = GetInstance(SUCCESS, element);
                NoticeAttributes dataAttributes = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);

                Assert.Equals(elementAttributes, elementAttributes);
                Assert.Equals(elementAttributes, dataAttributes);
                Assert.Equals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
                Fixture.AddTo(element);
                NoticeAttributes expected = GetInstance(SUCCESS, element);

                NoticeAttributes test = GetInstance(SUCCESS, "DoD-Dist-C", TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
                Assert.IsFalse(expected.Equals(test));

                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, DIFFERENT_VALUE, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
                Assert.IsFalse(expected.Equals(test));

                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, "2011-08-15", TEST_UNREGISTERED_NOTICE_TYPE, ExternalNotice);
                Assert.IsFalse(expected.Equals(test));

                test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, DIFFERENT_VALUE, ExternalNotice);
                Assert.IsFalse(expected.Equals(test));

                if (version.IsAtLeast("4.1"))
                {
                    test = GetInstance(SUCCESS, TEST_NOTICE_TYPE, TEST_NOTICE_REASON, TEST_NOTICE_DATE, TEST_UNREGISTERED_NOTICE_TYPE, null);
                    Assert.IsFalse(expected.Equals(test));
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes elementAttributes = Fixture;
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementAttributes.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void TestAddTo()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                NoticeAttributes component = Fixture;

                XElement element = Util.BuildDDMSElement("sample", null);
                component.AddTo(element);
                NoticeAttributes output = new NoticeAttributes(element);
                Assert.Equals(component, output);
            }
        }

        [TestMethod]
        public virtual void TestGetNonNull()
        {
            NoticeAttributes component = new NoticeAttributes(null, null, null, null);
            NoticeAttributes output = NoticeAttributes.GetNonNullInstance(null);
            Assert.Equals(component, output);
            Assert.IsTrue(output.Empty);

            output = NoticeAttributes.GetNonNullInstance(Fixture);
            Assert.Equals(Fixture, output);
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            DDMSVersion version = DDMSVersion.SetCurrentVersion("2.0");
            XElement element = Util.BuildDDMSElement(Resource.GetName(version), null);
            Util.AddAttribute(element, PropertyReader.GetPrefix("ism"), NoticeAttributes.NOTICE_DATE_NAME, version.IsmNamespace, "2011-09-15");
            GetInstance("Notice attributes cannot be used", element);

            DDMSVersion.SetCurrentVersion("4.0.1");
            NoticeAttributes attributes = Fixture;
            try
            {
                attributes.AddTo(element);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "These attributes cannot decorate");
            }
        }

        [TestMethod]
        public virtual void TestIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes dataAttributes = GetInstance(SUCCESS, null, null, null, null, null);
                Assert.IsTrue(dataAttributes.Empty);
                dataAttributes = GetInstance(SUCCESS, null, null, null, TEST_UNREGISTERED_NOTICE_TYPE, null);
                Assert.IsFalse(dataAttributes.Empty);
            }
        }

        [TestMethod]
        public virtual void TestDateOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes dataAttributes = GetInstance(SUCCESS, null, null, "2005-10-10", null, null);
                Assert.Equals(BuildOutput(true, "noticeDate", "2005-10-10"), dataAttributes.GetOutput(true, ""));
                Assert.Equals(BuildOutput(false, "noticeDate", "2005-10-10"), dataAttributes.GetOutput(false, ""));
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes component = Fixture;
                NoticeAttributes.Builder builder = new NoticeAttributes.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes.Builder builder = new NoticeAttributes.Builder();
                Assert.IsNotNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.NoticeType = "";
                Assert.IsTrue(builder.Empty);
                builder.NoticeReason = TEST_NOTICE_REASON;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeAttributes.Builder builder = new NoticeAttributes.Builder();
                builder.NoticeDate = "2001";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The noticeDate attribute must be in the xs:date format");
                }
                builder.NoticeDate = "2011-01-20";
                builder.Commit();
            }
        }
    }

}