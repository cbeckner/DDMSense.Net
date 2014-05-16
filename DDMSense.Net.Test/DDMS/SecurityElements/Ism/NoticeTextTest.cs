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
    using System;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ISM:NoticeText elements </para>
    /// 
    /// <para> The valid instance of ISM:NoticeText is generated, rather than relying on the ISM schemas to validate an XML
    /// file. </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    public class NoticeTextTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "noticeText";
        private static readonly List<string> TEST_POC_TYPES = Util.GetXsListAsList("DoD-Dist-B");

        /// <summary>
        /// Constructor
        /// </summary>
        public NoticeTextTest()
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

                    XElement element = Util.BuildElement(ismPrefix, NoticeText.GetName(version), ismNs, TEST_VALUE);
                    element.Add(ismPrefix, version.IsmNamespace);
                    SecurityAttributesTest.Fixture.AddTo(element);
                    Util.AddAttribute(element, ismPrefix, "pocType", ismNs, "DoD-Dist-B");
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
        public static List<NoticeText> FixtureList
        {
            get
            {
                try
                {
                    List<NoticeText> list = new List<NoticeText>();
                    list.Add(new NoticeText(FixtureElement));
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
        private NoticeText GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeText component = null;
            try
            {
                component = new NoticeText(element);
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
        /// <param name="value"> the value (optional) </param>
        /// <param name="pocTypes"> the poc types (optional) </param>
        /// <returns> a valid object </returns>
        private NoticeText GetInstance(string message, string value, List<string> pocTypes)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            NoticeText component = null;
            try
            {
                component = new NoticeText(value, pocTypes, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "noticeText", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "noticeText.pocType", Util.GetXsList(TEST_POC_TYPES)));
            text.Append(BuildOutput(isHTML, "noticeText.classification", "U"));
            text.Append(BuildOutput(isHTML, "noticeText.ownerProducer", "USA"));
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
                xml.Append("<ISM:NoticeText ").Append(XmlnsISM).Append(" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ISM:pocType=\"DoD-Dist-B\"");
                xml.Append(">").Append(TEST_VALUE).Append("</ISM:NoticeText>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, FixtureElement), DEFAULT_ISM_PREFIX, NoticeText.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");

                // All fields
                GetInstance(SUCCESS, FixtureElement);

                // No optional fields
                XElement element = Util.BuildElement(ismPrefix, NoticeText.GetName(version), version.IsmNamespace, null);
                SecurityAttributesTest.Fixture.AddTo(element);
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
                GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);

                // No optional fields
                GetInstance(SUCCESS, null, null);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");

                // Invalid pocType
                XElement element = Util.BuildElement(ismPrefix, NoticeText.GetName(version), version.IsmNamespace, null);
                Util.AddAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "Unknown");
                GetInstance("Unknown is not a valid enumeration token", element);

                // Partial Invalid pocType
                element = Util.BuildElement(ismPrefix, NoticeText.GetName(version), version.IsmNamespace, null);
                Util.AddAttribute(element, ismPrefix, "pocType", version.IsmNamespace, "DoD-Dist-B Unknown");
                GetInstance("Unknown is not a valid enumeration token", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Invalid pocType
                GetInstance("Unknown is not a valid enumeration token", TEST_VALUE, Util.GetXsListAsList("Unknown"));

                // Partial Invalid pocType
                GetInstance("Unknown is not a valid enumeration token", TEST_VALUE, Util.GetXsListAsList("DoD-Dist-B Unknown"));
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string ismPrefix = PropertyReader.GetPrefix("ism");

                // No warnings
                NoticeText component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(0, component.ValidationWarnings.Count);

                // Empty value
                XElement element = Util.BuildElement(ismPrefix, NoticeText.GetName(version), version.IsmNamespace, null);
                SecurityAttributesTest.Fixture.AddTo(element);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count);
                string text = "An ISM:NoticeText element was found with no value.";
                string locator = "ISM:NoticeText";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText elementComponent = GetInstance(SUCCESS, FixtureElement);
                NoticeText dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);

                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText elementComponent = GetInstance(SUCCESS, FixtureElement);
                NoticeText dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_POC_TYPES);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText component = GetInstance(SUCCESS, FixtureElement);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_POC_TYPES);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new NoticeText(TEST_VALUE, TEST_POC_TYPES, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The NoticeText element cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText component = GetInstance(SUCCESS, FixtureElement);
                NoticeText.Builder builder = new NoticeText.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText.Builder builder = new NoticeText.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Value = TEST_VALUE;
                Assert.IsFalse(builder.Empty);

            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                NoticeText.Builder builder = new NoticeText.Builder();
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                try
                {
                    builder.Commit();
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
                builder.SecurityAttributes.Classification = "U";
                builder.Commit();
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                NoticeText.Builder builder = new NoticeText.Builder();
                Assert.IsNotNull(builder.PocTypes[1]);
            }
        }
    }

}