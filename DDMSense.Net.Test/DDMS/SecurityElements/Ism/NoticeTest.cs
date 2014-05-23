using System.Collections;
using System.Collections.Generic;
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
    using DDMSense.DDMS;
    using DDMSense.DDMS.SecurityElements.Ism;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ISM:Notice elements </para>
    ///
    /// <para> The valid instance of ISM:Notice is generated, rather than relying on the ISM schemas to validate an XML file.
    /// </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class NoticeTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NoticeTest()
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
                    string ismPrefix = PropertyReader.GetPrefix("ism");
                    string ismNs = version.IsmNamespace;

                    XElement element = Util.BuildElement(ismPrefix, Notice.GetName(version), ismNs, null);
                    NoticeAttributesTest.Fixture.AddTo(element);
                    SecurityAttributesTest.Fixture.AddTo(element);
                    element.Add(NoticeTextTest.FixtureElement);
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
        public static List<Notice> FixtureList
        {
            get
            {
                try
                {
                    List<Notice> list = new List<Notice>();
                    list.Add(new Notice(NoticeTest.FixtureElement));
                    return (list);
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
        private Notice GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Notice component = null;
            try
            {
                component = new Notice(element);
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
        /// <param name="noticeTexts"> the notice texts (at least 1 required) </param>
        /// <returns> a valid object </returns>
        private Notice GetInstance(string message, List<NoticeText> noticeTexts)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Notice component = null;
            try
            {
                component = new Notice(noticeTexts, SecurityAttributesTest.Fixture, NoticeAttributesTest.Fixture);
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
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "notice.noticeText", "noticeText"));
            text.Append(BuildOutput(isHTML, "notice.noticeText.pocType", "DoD-Dist-B"));
            text.Append(BuildOutput(isHTML, "notice.noticeText.classification", "U"));
            text.Append(BuildOutput(isHTML, "notice.noticeText.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "notice.classification", "U"));
            text.Append(BuildOutput(isHTML, "notice.ownerProducer", "USA"));
            text.Append(BuildOutput(isHTML, "notice.noticeType", "DoD-Dist-B"));
            text.Append(BuildOutput(isHTML, "notice.noticeReason", "noticeReason"));
            text.Append(BuildOutput(isHTML, "notice.noticeDate", "2011-09-15"));
            text.Append(BuildOutput(isHTML, "notice.unregisteredNoticeType", "unregisteredNoticeType"));
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
            {
                text.Append(BuildOutput(isHTML, "notice.externalNotice", "false"));
            }
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
                xml.Append("<ISM:Notice ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:noticeType=\"DoD-Dist-B\" ISM:noticeReason=\"noticeReason\" ISM:noticeDate=\"2011-09-15\" ");
                xml.Append("ISM:unregisteredNoticeType=\"unregisteredNoticeType\"");
                if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
                {
                    xml.Append(" ISM:externalNotice=\"false\"");
                }
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\">");
                xml.Append("<ISM:NoticeText ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\">noticeText</ISM:NoticeText></ISM:Notice>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_ISM_PREFIX, Notice.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, FixtureElement);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, NoticeTextTest.FixtureList);

                // No attributes
                try
                {
                    new Notice(NoticeTextTest.FixtureList, null, null);
                }
                catch (InvalidDDMSException)
                {
                    Assert.Fail("Prevented valid data.");
                }
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No NoticeTexts
                XElement element = new XElement(FixtureElement);
                element.RemoveNodes();
                GetInstance("At least one ISM:NoticeText", element);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No NoticeTexts
                GetInstance("At least one ISM:NoticeText", (List<NoticeText>)null);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                Notice component = GetInstance(SUCCESS, FixtureElement);

                // 4.1 ism:Notice used
                if (version.IsAtLeast("4.1"))
                {
                    Assert.AreEqual(1, component.ValidationWarnings.Count());
                    string text = "The ISM:externalNotice attribute in this DDMS component";
                    string locator = "ISM:Notice";
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
        public virtual void SecurityElements_Ism_Notice_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice elementComponent = GetInstance(SUCCESS, FixtureElement);
                Notice dataComponent = GetInstance(SUCCESS, NoticeTextTest.FixtureList);

                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                List<NoticeText> list = NoticeTextTest.FixtureList;
                list.Add(new NoticeText(NoticeTextTest.FixtureElement));
                Notice elementComponent = GetInstance(SUCCESS, FixtureElement);
                Notice dataComponent = GetInstance(SUCCESS, list);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice component = GetInstance(SUCCESS, FixtureElement);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, NoticeTextTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice component = GetInstance(SUCCESS, FixtureElement);
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, NoticeTextTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_WrongVersion()
        {
            // Implicit, since 1 NoticeText is required and that requires DDMS 4.0.1 or greater.
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice component = GetInstance(SUCCESS, FixtureElement);
                Notice.Builder builder = new Notice.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice.Builder builder = new Notice.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.NoticeTexts.Add(new NoticeText.Builder());
                builder.NoticeTexts[0].Value = "TEST";
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void SecurityElements_Ism_Notice_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Notice.Builder builder = new Notice.Builder();
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                try
                {
                    builder.Commit();
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "At least one ISM:NoticeText");
                }
                builder.NoticeTexts.Add(new NoticeText.Builder());
                builder.NoticeTexts[0].Value = "TEST";
                builder.NoticeTexts[0].SecurityAttributes.Classification = "U";
                builder.NoticeTexts[0].SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }
}