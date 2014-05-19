using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

/* Copyright 2010 - 2013 by Brian Uri!
   
   This file is part of DDMSence.
   
   This library is free software; you can redistribute it and/or modify
   it under the terms of version 3.0 of the GNU Lesser General Public 
   License as published by the Free Software Foundation.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
   GNU Lesser General Public License for more processingInfo.
   
   You should have received a copy of the GNU Lesser General Public 
   License along with DDMSence. If not, see <http://www.gnu.org/licenses/>.

   You can contact the author at ddmsence@urizone.net. The DDMSence
   home page is located at http://ddmsence.urizone.net/
 */
namespace DDMSense.Test.DDMS.ResourceElements
{



    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.ResourceElements;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <para> Tests related to ddms:processingInfo elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    public class ProcessingInfoTest : AbstractBaseTestCase
    {

        private const string TEST_VALUE = "XSLT Transformation to convert DDMS 2.0 to DDMS 3.1.";
        private const string TEST_DATE_PROCESSED = "2011-08-19";

        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingInfoTest()
            : base("processingInfo.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static ProcessingInfo Fixture
        {
            get
            {
                try
                {
                    return (new ProcessingInfo(TEST_VALUE, TEST_DATE_PROCESSED, SecurityAttributesTest.Fixture));
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
        public static List<ProcessingInfo> FixtureList
        {
            get
            {
                List<ProcessingInfo> infos = new List<ProcessingInfo>();
                infos.Add(Fixture);
                return (infos);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private ProcessingInfo GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProcessingInfo component = null;
            try
            {
                component = new ProcessingInfo(element);
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
        /// <param name="value"> the child text </param>
        /// <param name="dateProcessed"> the processing date </param>
        /// <returns> a valid object </returns>
        private ProcessingInfo GetInstance(string message, string value, string dateProcessed)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProcessingInfo component = null;
            try
            {
                component = new ProcessingInfo(value, dateProcessed, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "processingInfo", TEST_VALUE));
            text.Append(BuildOutput(isHTML, "processingInfo.dateProcessed", TEST_DATE_PROCESSED));
            text.Append(BuildOutput(isHTML, "processingInfo.classification", "U"));
            text.Append(BuildOutput(isHTML, "processingInfo.ownerProducer", "USA"));
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
                xml.Append("<ddms:processingInfo ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM);
                xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\" ");
                xml.Append("ddms:dateProcessed=\"").Append(TEST_DATE_PROCESSED).Append("\">");
                xml.Append(TEST_VALUE).Append("</ddms:processingInfo>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, ProcessingInfo.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildDDMSElement(ProcessingInfo.GetName(version), null);
                Util.AddDDMSAttribute(element, "dateProcessed", TEST_DATE_PROCESSED);
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
                GetInstance(SUCCESS, TEST_VALUE, TEST_DATE_PROCESSED);

                // No optional fields
                GetInstance(SUCCESS, "", TEST_DATE_PROCESSED);
            }
        }

        [TestMethod]
        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing date
                XElement element = Util.BuildDDMSElement(ProcessingInfo.GetName(version), null);
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("dateProcessed is required.", element);

                // Wrong date format (using xs:gDay here)
                element = Util.BuildDDMSElement(ProcessingInfo.GetName(version), null);
                Util.AddDDMSAttribute(element, "dateProcessed", "---31");
                SecurityAttributesTest.Fixture.AddTo(element);
                GetInstance("The date datatype must be one of", element);
            }
        }

        [TestMethod]
        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing date
                GetInstance("dateProcessed is required.", TEST_VALUE, null);

                // Invalid date format
                GetInstance("The date datatype must be one of", TEST_VALUE, "baboon");

                // Wrong date format (using xs:gDay here)
                GetInstance("The date datatype must be one of", TEST_VALUE, "---31");

                // Bad security attributes
                try
                {
                    new ProcessingInfo(TEST_VALUE, TEST_DATE_PROCESSED, null);
                    Assert.Fail("Allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "classification is required.");
                }
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ProcessingInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());

                // No value
                XElement element = Util.BuildDDMSElement(ProcessingInfo.GetName(version), null);
                Util.AddDDMSAttribute(element, "dateProcessed", TEST_DATE_PROCESSED);
                SecurityAttributesTest.Fixture.AddTo(element);
                component = GetInstance(SUCCESS, element);
                Assert.Equals(1, component.ValidationWarnings.Count());
                string text = "A ddms:processingInfo element was found with no value.";
                string locator = "ddms:processingInfo";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void TestDeprecatedAccessors()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                ProcessingInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(TEST_DATE_PROCESSED, component.DateProcessed.GetValueOrDefault().ToString("o"));

                // Not compatible with XMLGregorianCalendar
                if (version.IsAtLeast("4.1"))
                {
                    component = new ProcessingInfo(TEST_VALUE, "2012-01-01T01:02Z", SecurityAttributesTest.Fixture);
                    Assert.IsNull(component.DateProcessed);
                }
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProcessingInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProcessingInfo dataComponent = GetInstance(SUCCESS, TEST_VALUE, TEST_DATE_PROCESSED);
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

                ProcessingInfo elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProcessingInfo dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_DATE_PROCESSED);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_VALUE, "2011");
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProcessingInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_DATE_PROCESSED);
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

                ProcessingInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(ExpectedXMLOutput, component.ToXML());

                component = GetInstance(SUCCESS, TEST_VALUE, TEST_DATE_PROCESSED);
                Assert.Equals(ExpectedXMLOutput, component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestWrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new ProcessingInfo(TEST_VALUE, TEST_DATE_PROCESSED, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The processingInfo element cannot be used");
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProcessingInfo component = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProcessingInfo.Builder builder = new ProcessingInfo.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProcessingInfo.Builder builder = new ProcessingInfo.Builder();
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

                ProcessingInfo.Builder builder = new ProcessingInfo.Builder();
                builder.Value = TEST_VALUE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "dateProcessed is required.");
                }
                builder.DateProcessed = TEST_DATE_PROCESSED;
                builder.SecurityAttributes.Classification = "U";
                builder.SecurityAttributes.OwnerProducers = Util.GetXsListAsList("USA");
                builder.Commit();
            }
        }
    }

}