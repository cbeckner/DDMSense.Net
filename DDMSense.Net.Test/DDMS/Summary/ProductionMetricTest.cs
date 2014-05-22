using System;
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

namespace DDMSense.Test.DDMS.Summary
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:productionMetric elements </para>
    ///
    /// @author Brian Uri!
    /// @since 2.0.0
    /// </summary>
    [TestClass]
    public class ProductionMetricTest : AbstractBaseTestCase
    {
        private const string TEST_SUBJECT = "FOOD";
        private const string TEST_COVERAGE = "AFG";

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductionMetricTest()
            : base("productionMetric.xml")
        {
            RemoveSupportedVersions("2.0 3.0 3.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<ProductionMetric> FixtureList
        {
            get
            {
                try
                {
                    DDMSVersion version = DDMSVersion.CurrentVersion;
                    List<ProductionMetric> metrics = new List<ProductionMetric>();
                    if (version.IsAtLeast("4.0.1"))
                    {
                        metrics.Add(new ProductionMetric("FOOD", "AFG", SecurityAttributesTest.Fixture));
                    }
                    return (metrics);
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
        private ProductionMetric GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProductionMetric component = null;
            try
            {
                component = new ProductionMetric(element);
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
        /// <param name="subject"> a method of categorizing the subject of a document in a fashion understandable by DDNI-A
        /// (required) </param>
        /// <param name="coverage"> a method of categorizing the coverage of a document in a fashion understandable by DDNI-A
        /// (required) </param>
        /// <param name="label"> the label (required) </param>
        /// <returns> a valid object </returns>
        private ProductionMetric GetInstance(string message, string subject, string coverage)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            ProductionMetric component = null;
            try
            {
                component = new ProductionMetric(subject, coverage, SecurityAttributesTest.Fixture);
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
            text.Append(BuildOutput(isHTML, "productionMetric.subject", TEST_SUBJECT));
            text.Append(BuildOutput(isHTML, "productionMetric.coverage", TEST_COVERAGE));
            text.Append(BuildOutput(isHTML, "productionMetric.classification", "U"));
            text.Append(BuildOutput(isHTML, "productionMetric.ownerProducer", "USA"));
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
                xml.Append("<ddms:productionMetric ").Append(XmlnsDDMS).Append(" ").Append(XmlnsISM).Append(" ");
                xml.Append("ddms:subject=\"").Append(TEST_SUBJECT).Append("\" ").Append("ddms:coverage=\"").Append(TEST_COVERAGE).Append("\" ");
                xml.Append("ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
                xml.Append(" />");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, ProductionMetric.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // Missing subject
                XElement element = Util.BuildDDMSElement(ProductionMetric.GetName(version), null);
                element.Add(Util.BuildDDMSAttribute("coverage", TEST_COVERAGE));
                GetInstance("subject attribute is required.", element);

                // Missing coverage
                element = Util.BuildDDMSElement(ProductionMetric.GetName(version), null);
                element.Add(Util.BuildDDMSAttribute("subject", TEST_SUBJECT));
                GetInstance("coverage attribute is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Missing subject
                GetInstance("subject attribute is required.", null, TEST_COVERAGE);

                // Missing coverage
                GetInstance("coverage attribute is required.", TEST_SUBJECT, null);
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProductionMetric dataComponent = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProductionMetric dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_COVERAGE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_SUBJECT, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(ExpectedXMLOutput);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_SUBJECT, TEST_COVERAGE);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_WrongVersion()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("2.0");
                new ProductionMetric(TEST_SUBJECT, TEST_COVERAGE, null);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "The productionMetric element cannot be used");
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric component = GetInstance(SUCCESS, GetValidElement(sVersion));
                ProductionMetric.Builder builder = new ProductionMetric.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric.Builder builder = new ProductionMetric.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Coverage = TEST_COVERAGE;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_ProductionMetric_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ProductionMetric.Builder builder = new ProductionMetric.Builder();
                Assert.IsNull(builder.Commit());
                builder.Coverage = TEST_COVERAGE;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "subject attribute is required.");
                }
                builder.Subject = TEST_SUBJECT;
                builder.Commit();
            }
        }
    }
}