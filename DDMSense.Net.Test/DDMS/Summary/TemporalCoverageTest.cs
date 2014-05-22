using System;
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
    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:temporalCoverage elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class TemporalCoverageTest : AbstractBaseTestCase
    {
        private const string TEST_NAME = "My Time Period";
        private const string TEST_START = "1979-09-15";
        private const string TEST_END = "Not Applicable";

        /// <summary>
        /// Constructor
        /// </summary>
        public TemporalCoverageTest()
            : base("temporalCoverage.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static TemporalCoverage Fixture
        {
            get
            {
                try
                {
                    return (new TemporalCoverage(null, "1979-09-15", "Not Applicable", null));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Generates an approximableDate for testing
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private DDMSense.Net.Test.DDMS.ApproximableDate getApproximableStart(boolean includeAllFields) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private ApproximableDate GetApproximableStart(bool includeAllFields)
        {
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
            {
                return (new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableStart", includeAllFields)));
            }
            return (null);
        }

        /// <summary>
        /// Generates an approximableDate for testing
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private DDMSense.Net.Test.DDMS.ApproximableDate getApproximableEnd(boolean includeAllFields) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        private ApproximableDate GetApproximableEnd(bool includeAllFields)
        {
            if (DDMSVersion.CurrentVersion.IsAtLeast("4.1"))
            {
                return (new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableEnd", includeAllFields)));
            }
            return (null);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private TemporalCoverage GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TemporalCoverage component = null;
            try
            {
                if (DDMSVersion.CurrentVersion.IsAtLeast("3.0"))
                {
                    SecurityAttributesTest.Fixture.AddTo(element);
                }
                component = new TemporalCoverage(element);
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
        /// <param name="timePeriodName"> the time period name (optional) </param>
        /// <param name="startString"> a string representation of the date (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableStart"> the start date, as an approximable date (optional) </param>
        /// <param name="endString"> a string representation of the end date (optional) (if empty, defaults to Unknown) </param>
        /// <param name="approximableEnd"> the end date, as an approximable date (optional) </param>
        /// <returns> a valid object </returns>
        private TemporalCoverage GetInstance(string message, string timePeriodName, string startString, ApproximableDate approximableStart, string endString, ApproximableDate approximableEnd)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            TemporalCoverage component = null;
            try
            {
                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.IsAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
                if (approximableStart != null && approximableEnd != null)
                {
                    component = new TemporalCoverage(timePeriodName, approximableStart, approximableEnd, attr);
                }
                else if (approximableStart != null && approximableEnd == null)
                {
                    component = new TemporalCoverage(timePeriodName, approximableStart, endString, attr);
                }
                else if (approximableStart == null && approximableEnd != null)
                {
                    component = new TemporalCoverage(timePeriodName, startString, approximableEnd, attr);
                }
                else
                {
                    component = new TemporalCoverage(timePeriodName, startString, endString, attr);
                }
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
        /// Helper method to manage the deprecated TimePeriod wrapper element
        /// </summary>
        /// <param name="innerElement"> the element containing the guts of this component </param>
        /// <returns> the element itself in DDMS 4.0.1 or later, or the element wrapped in another element </returns>
        private XElement WrapInnerElement(XElement innerElement)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string name = TemporalCoverage.GetName(version);
            if (version.IsAtLeast("4.0.1"))
            {
                innerElement.Name = XName.Get(name);
                return (innerElement);
            }
            XElement element = Util.BuildDDMSElement(name, null);
            element.Add(innerElement);
            return (element);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML, bool isApproximable)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            string prefix = "temporalCoverage.";
            if (!version.IsAtLeast("4.0.1"))
            {
                prefix += "TimePeriod.";
            }
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, prefix + "name", TEST_NAME));
            if (isApproximable)
            {
                ApproximableDate start = new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableStart", true));
                ApproximableDate end = new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
                text.Append(start.GetOutput(isHTML, prefix, ""));
                text.Append(end.GetOutput(isHTML, prefix, ""));
            }
            else
            {
                text.Append(BuildOutput(isHTML, prefix + "start", TEST_START));
                text.Append(BuildOutput(isHTML, prefix + "end", TEST_END));
            }
            if (version.IsAtLeast("3.0"))
            {
                text.Append(SecurityAttributesTest.Fixture.GetOutput(isHTML, prefix));
            }
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        private string GetExpectedXMLOutput(bool isApproximable)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:temporalCoverage ").Append(XmlnsDDMS);
            if (version.IsAtLeast("3.0"))
            {
                xml.Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
            }
            xml.Append(">");
            if (!version.IsAtLeast("4.0.1"))
            {
                xml.Append("<ddms:TimePeriod>");
            }
            xml.Append("<ddms:name>").Append(TEST_NAME).Append("</ddms:name>");
            if (isApproximable)
            {
                xml.Append("<ddms:approximableStart>");
                xml.Append("<ddms:description>description</ddms:description>");
                xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
                xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
                xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
                xml.Append("</ddms:approximableStart>");
                xml.Append("<ddms:approximableEnd>");
                xml.Append("<ddms:description>description</ddms:description>");
                xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
                xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
                xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
                xml.Append("</ddms:approximableEnd>");
            }
            else
            {
                xml.Append("<ddms:start>").Append(TEST_START).Append("</ddms:start>");
                xml.Append("<ddms:end>").Append(TEST_END).Append("</ddms:end>");
            }
            if (!version.IsAtLeast("4.0.1"))
            {
                xml.Append("</ddms:TimePeriod>");
            }
            xml.Append("</ddms:temporalCoverage>");
            return (xml.ToString());
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, TemporalCoverage.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // All fields, exact-exact
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("start", TEST_START));
                periodElement.Add(Util.BuildDDMSElement("end", TEST_END));
                GetInstance(SUCCESS, WrapInnerElement(periodElement));

                // All fields, approx-approx
                if (version.IsAtLeast("4.1"))
                {
                    periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", false));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
                    GetInstance(SUCCESS, WrapInnerElement(periodElement));
                }

                // No optional fields, empty name element rather than no name element
                periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("name", ""));
                periodElement.Add(Util.BuildDDMSElement("start", TEST_START));
                periodElement.Add(Util.BuildDDMSElement("end", TEST_END));
                GetInstance(SUCCESS, WrapInnerElement(periodElement));
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields, exact-exact
                GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);

                if (version.IsAtLeast("4.1"))
                {
                    // All fields, exact-approx
                    GetInstance(SUCCESS, TEST_NAME, TEST_START, null, null, GetApproximableEnd(false));

                    // All fields, approx-exact
                    GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), TEST_END, null);

                    // All fields, approx-approx
                    GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), null, GetApproximableEnd(false));
                }

                // No optional fields
                GetInstance(SUCCESS, "", TEST_START, null, TEST_END, null);
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong date format (using xs:gDay here)
                XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("start", "---31"));
                periodElement.Add(Util.BuildDDMSElement("end", TEST_END));
                GetInstance("The date datatype must be one of", WrapInnerElement(periodElement));
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Wrong date format (using xs:gDay here)
                GetInstance("The date datatype must be one of", TEST_NAME, "---31", null, TEST_END, null);
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings, exact-exact
                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Empty name element
                XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("name", null));
                periodElement.Add(Util.BuildDDMSElement("start", TEST_START));
                periodElement.Add(Util.BuildDDMSElement("end", TEST_END));
                component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A ddms:name element was found with no value.";
                string locator = version.IsAtLeast("4.0.1") ? "ddms:temporalCoverage" : "ddms:temporalCoverage/ddms:TimePeriod";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // 4.1 ddms:approximableStart/End element used
                if (version.IsAtLeast("4.1"))
                {
                    periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", true));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    Assert.AreEqual(1, component.ValidationWarnings.Count());
                    text = "The ddms:approximableStart or ddms:approximableEnd element";
                    locator = "ddms:temporalCoverage";
                    AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                // exact-exact
                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                TemporalCoverage dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());

                // approx-approx
                if (version.IsAtLeast("4.1"))
                {
                    XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", false));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
                    elementComponent = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    dataComponent = GetInstance(SUCCESS, null, null, GetApproximableStart(false), null, GetApproximableEnd(false));
                    Assert.AreEqual(elementComponent, dataComponent);
                    Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                TemporalCoverage dataComponent = GetInstance(SUCCESS, "", TEST_START, null, TEST_END, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, "Not Applicable", null, TEST_END, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, "2050", null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                if (version.IsAtLeast("4.1"))
                {
                    XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", false));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
                    elementComponent = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    dataComponent = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), null, GetApproximableEnd(false));
                    Assert.IsFalse(elementComponent.Equals(dataComponent));

                    dataComponent = GetInstance(SUCCESS, null, null, GetApproximableStart(false), TEST_END, null);
                    Assert.IsFalse(elementComponent.Equals(dataComponent));

                    dataComponent = GetInstance(SUCCESS, null, TEST_START, null, null, GetApproximableEnd(false));
                    Assert.IsFalse(elementComponent.Equals(dataComponent));
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true, false), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
                Assert.AreEqual(GetExpectedOutput(true, false), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false, false), component.ToText());

                if (version.IsAtLeast("4.1"))
                {
                    XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(Util.BuildDDMSElement("name", TEST_NAME));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", true));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                    Assert.AreEqual(GetExpectedOutput(false, true), component.ToText());

                    component = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(true), null, GetApproximableEnd(true));
                    Assert.AreEqual(GetExpectedOutput(true, true), component.ToHTML());
                    Assert.AreEqual(GetExpectedOutput(false, true), component.ToText());
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                if (version.IsAtLeast("4.1"))
                {
                    XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(Util.BuildDDMSElement("name", TEST_NAME));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", true));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    actual.LoadXml(component.ToXML());
                    Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                    component = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(true), null, GetApproximableEnd(true));
                    actual.LoadXml(component.ToXML());
                    Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_DefaultValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("start", ""));
                periodElement.Add(Util.BuildDDMSElement("end", ""));
                TemporalCoverage component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                Assert.AreEqual("Unknown", component.StartString);
                Assert.AreEqual("Unknown", component.EndString);

                component = GetInstance(SUCCESS, "", "", null, "", null);
                Assert.AreEqual("Unknown", component.TimePeriodName);
                Assert.AreEqual("Unknown", component.StartString);
                Assert.AreEqual("Unknown", component.EndString);
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_DeprecatedAccessors()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                periodElement.Add(Util.BuildDDMSElement("start", ""));
                periodElement.Add(Util.BuildDDMSElement("end", ""));
                TemporalCoverage component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                Assert.IsNull(component.Start);
                Assert.IsNull(component.End);

                component = GetInstance(SUCCESS, "", TEST_START, null, TEST_START, null);
                Assert.AreEqual(TEST_START, component.Start.GetValueOrDefault().ToString("o"));
                Assert.AreEqual(TEST_START, component.End.GetValueOrDefault().ToString("o"));
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_SecurityAttributes()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                SecurityAttributes attr = (!version.IsAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
                TemporalCoverage component = new TemporalCoverage(TEST_NAME, TEST_START, TEST_END, attr);
                if (!version.IsAtLeast("3.0"))
                {
                    Assert.IsTrue(component.SecurityAttributes.Empty);
                }
                else
                {
                    Assert.AreEqual(attr, component.SecurityAttributes);
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_WrongVersionSecurityAttributes()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            try
            {
                new TemporalCoverage(TEST_NAME, TEST_START, TEST_END, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Security attributes cannot be applied");
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_WrongVersionApproximable()
        {
            try
            {
                DDMSVersion.SetCurrentVersion("4.1");
                ApproximableDate start = GetApproximableStart(false);
                DDMSVersion.SetCurrentVersion("3.0");
                new TemporalCoverage(null, start, TEST_END, SecurityAttributesTest.Fixture);
                Assert.Fail("Allowed invalid data.");
            }
            catch (InvalidDDMSException e)
            {
                ExpectMessage(e, "Approximable dates cannot be used until DDMS 4.1 or later.");
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
                TemporalCoverage.Builder builder = new TemporalCoverage.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                if (version.IsAtLeast("4.1"))
                {
                    XElement periodElement = Util.BuildDDMSElement("TimePeriod", null);
                    periodElement.Add(Util.BuildDDMSElement("name", TEST_NAME));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableStart", true));
                    periodElement.Add(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
                    builder = new TemporalCoverage.Builder(component);
                    Assert.AreEqual(component, builder.Commit());
                }
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                TemporalCoverage.Builder builder = new TemporalCoverage.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.StartString = TEST_START;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_TemporalCoverage_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                TemporalCoverage.Builder builder = new TemporalCoverage.Builder();
                builder.StartString = "Invalid";
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "The date datatype");
                }
                builder.StartString = "2001";
                builder.EndString = "2002";
                builder.Commit();

                if (version.IsAtLeast("4.1"))
                {
                    builder.ApproximableStart.Description = "test";
                    try
                    {
                        builder.Commit();
                        Assert.Fail("Builder allowed invalid data.");
                    }
                    catch (InvalidDDMSException e)
                    {
                        ExpectMessage(e, "Only 1 of start or approximableStart");
                    }

                    builder.ApproximableStart.Description = null;
                    builder.ApproximableEnd.Description = "test";
                    try
                    {
                        builder.Commit();
                        Assert.Fail("Builder allowed invalid data.");
                    }
                    catch (InvalidDDMSException e)
                    {
                        ExpectMessage(e, "Only 1 of end or approximableEnd");
                    }

                    builder.ApproximableStart.Description = "test";
                    builder.StartString = null;
                    builder.EndString = null;
                    builder.Commit();
                }
            }
        }
    }
}