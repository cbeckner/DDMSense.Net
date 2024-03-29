using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace DDMSense.Test.DDMS.SecurityElements
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.SecurityElements;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Notice = DDMSense.DDMS.SecurityElements.Ism.Notice;
    using NoticeTest = DDMSense.Test.DDMS.SecurityElements.Ism.NoticeTest;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:noticeList elements </para>
    ///
    /// <para> Because a ddms:noticeList is a local component, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class NoticeListTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NoticeListTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static XElement FixtureElement
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;

                    XElement element = Util.BuildDDMSElement(NoticeList.GetName(version), null);
                    //element.Add(PropertyReader.GetPrefix("ddms"), version.Namespace);
                    SecurityAttributesTest.Fixture.AddTo(element);
                    element.Add(NoticeTest.FixtureElement);
                    return (element);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static NoticeList Fixture
        {
            get
            {
                try
                {
                    return (DDMSVersion.CurrentVersion.IsAtLeast("4.0.1") ? new NoticeList(FixtureElement) : null);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private NoticeList GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeList component = null;
            try
            {
                component = new NoticeList(element);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="notices"> the notices (at least 1 required) </param>
        /// <returns> a valid object </returns>
        private NoticeList GetInstance(string message, List<Notice> notices)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeList component = null;
            try
            {
                component = new NoticeList(notices, SecurityAttributesTest.Fixture);
                CheckConstructorSuccess(expectFailure);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectFailure, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText", "noticeText"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.pocType", "DoD-Dist-B"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.classification", "U"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeText.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.classification", "U"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeType", "DoD-Dist-B"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeReason", "noticeReason"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.noticeDate", "2011-09-15"));
            text.Append(BuildOutput(isHTML, "noticeList.notice.unregisteredNoticeType", "unregisteredNoticeType"));
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
            {
                text.Append(BuildOutput(isHTML, "noticeList.notice.externalNotice", "false"));
            }
            text.Append(BuildOutput(isHTML, "noticeList.classification", "U"));
            text.Append(BuildOutput(isHTML, "noticeList.ownerProducer", "USA"));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string ExpectedXMLOutput
        {
            get
            {
                StringBuilder xml = new StringBuilder();
                xml.Append("<ddms:noticeList ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ISM:Notice ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
                xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
                if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
                {
                    xml.Append(" ISM:externalNotice=\"false\"");
                }
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText>");
                xml.Append("</ISM:Notice>");
                xml.Append("</ddms:noticeList>");
                return (xml.ToString());
            }
        }

        public virtual void SecurityElements_NoticeList_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_DDMS_PREFIX, NoticeList.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        public virtual void SecurityElements_NoticeList_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);
            }
        }

        public virtual void SecurityElements_NoticeList_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, NoticeTest.FixtureList);
            }
        }

        public virtual void SecurityElements_NoticeList_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No Notices
                XElement element = new XElement(FixtureElement);
                element.RemoveAll();
                GetInstance("At least one ISM:Notice", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No Notices
                GetInstance("At least one ISM:Notice", (List<Notice>)null);

                // No attributes
                try
                {
                    new NoticeList(NoticeTest.FixtureList, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList component = GetInstance(SUCCESS, FixtureElement);

                // 4.1 ISM:externalNotice used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.AreEqual(1, component.ValidationWarnings.Count());
                    string text = "The ISM:externalNotice XAttribute in this DDMS component";
                    string locator = "ddms:noticeList/ISM:Notice";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
                // No warnings
                else
                {
                    Assert.AreEqual(0, component.ValidationWarnings.Count());
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList elementComponent = GetInstance(SUCCESS, FixtureElement);
                NoticeList dataComponent = GetInstance(SUCCESS, NoticeTest.FixtureList);

                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                List<Notice> list = NoticeTest.FixtureList;
                list.Add(new Notice(NoticeTest.FixtureElement));
                NoticeList elementComponent = GetInstance(SUCCESS, FixtureElement);
                NoticeList dataComponent = GetInstance(SUCCESS, list);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, NoticeTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList component = GetInstance(SUCCESS, FixtureElement);
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, NoticeTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        public virtual void SecurityElements_NoticeList_WrongVersion()
        {
            // Implicit, since 1 Notice is required and that requires DDMS 4.0.1 or greater.
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList component = GetInstance(SUCCESS, FixtureElement);
                NoticeList.Builder builder = new NoticeList.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_NoticeList_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList.Builder builder = new NoticeList.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Notices.Add(new Notice.Builder());
                builder.Notices[0].NoticeTexts.Add(new DDMSense.DDMS.SecurityElements.Ism.NoticeText.Builder());
                builder.Notices[0].NoticeTexts[0].Value = "TEST";
                Assert.IsFalse(builder.Empty);
            }
        }

        public virtual void SecurityElements_NoticeList_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeList.Builder builder = new NoticeList.Builder();
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one ISM:Notice");
                }
                builder.Notices[0].NoticeTexts[0].Value = "test";
                builder.Notices[0].SecurityAttributes.Classification = "U";
                builder.Notices[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Notices[0].NoticeTexts[0].SecurityAttributes.Classification = "U";
                builder.Notices[0].NoticeTexts[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }
}