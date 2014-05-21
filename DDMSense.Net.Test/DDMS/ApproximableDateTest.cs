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

namespace DDMSense.Test.DDMS
{
    using DDMSense.DDMS;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to elements of type ddms:ApproximableDateType (includes ddms:acquiredOn, and the ddms:start / ddms:end values in a ddms:temporalCoverage element.</para>
    ///
    /// <para> Because these are local components, we cannot load a valid document from a unit test data file. We
    /// have to build the well-formed XElement ourselves. </para>
    ///
    /// @author Brian Uri!
    /// @since 2.1.0
    /// </summary>
    [TestClass]
    public class ApproximableDateTest : AbstractBaseTestCase
    {
        private const string TEST_NAME = "acquiredOn";
        private const string TEST_DESCRIPTION = "description";
        private const string TEST_APPROXIMABLE_DATE = "2012";
        private const string TEST_APPROXIMATION = "1st qtr";
        private const string TEST_START_DATE = "2012-01";
        private const string TEST_END_DATE = "2012-03-31";

        /// <summary>
        /// Constructor
        /// </summary>
        public ApproximableDateTest()
            : base(null)
        {
            RemoveSupportedVersions("2.0 3.0 3.1 4.0.1");
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        /// <param name="name"> the element name </param>
        /// <param name="includeAllFields"> true to include optional fields </param>
        public static XElement GetFixtureElement(string name, bool includeAllFields)
        {
            DDMSVersion version = DDMSVersion.CurrentVersion;
            XElement element = Util.BuildDDMSElement(name, null);
            if (includeAllFields)
            {
                Util.AddDDMSChildElement(element, "description", TEST_DESCRIPTION);

                XElement approximableElment = Util.BuildDDMSElement("approximableDate", TEST_APPROXIMABLE_DATE, false);
                Util.AddDDMSAttribute(approximableElment, "approximation", TEST_APPROXIMATION);
                element.Add(approximableElment);

                XElement searchableElement = Util.BuildDDMSElement("searchableDate", null);
                Util.AddDDMSChildElement(searchableElement, "start", TEST_START_DATE);
                Util.AddDDMSChildElement(searchableElement, "end", TEST_END_DATE);
                element.Add(searchableElement);
            }
            return (element);
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private ApproximableDate GetInstance(string message, XElement element)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            ApproximableDate component = null;
            try
            {
                component = new ApproximableDate(element);
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
        /// <param name="name"> the name of the element </param>
        /// <param name="description"> the description of this approximable date (optional) </param>
        /// <param name="approximableDate"> the value of the approximable date (optional) </param>
        /// <param name="approximation"> an attribute that decorates the date (optional) </param>
        /// <param name="searchableStartDate"> the lower bound for this approximable date (optional) </param>
        /// <param name="searchableEndDate"> the upper bound for this approximable date (optional) </param>
        /// <param name="entity"> the person or organization in this role </param>
        /// <param name="org"> the organization </param>
        private ApproximableDate GetInstance(string message, string name, string description, string approximableDate, string approximation, string searchableStartDate, string searchableEndDate)
        {
            bool expectFailure = !string.IsNullOrEmpty(message);
            ApproximableDate component = null;
            try
            {
                component = new ApproximableDate(name, description, approximableDate, approximation, searchableStartDate, searchableEndDate);
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
            text.Append(BuildOutput(isHTML, "acquiredOn.description", TEST_DESCRIPTION));
            text.Append(BuildOutput(isHTML, "acquiredOn.approximableDate", TEST_APPROXIMABLE_DATE));
            text.Append(BuildOutput(isHTML, "acquiredOn.approximableDate.approximation", TEST_APPROXIMATION));
            text.Append(BuildOutput(isHTML, "acquiredOn.searchableDate.start", TEST_START_DATE));
            text.Append(BuildOutput(isHTML, "acquiredOn.searchableDate.end", TEST_END_DATE));
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
                xml.Append("<ddms:acquiredOn ").Append(XmlnsDDMS).Append(">");
                xml.Append("<ddms:description>").Append(TEST_DESCRIPTION).Append("</ddms:description>");
                xml.Append("<ddms:approximableDate ddms:approximation=\"").Append(TEST_APPROXIMATION).Append("\">");
                xml.Append(TEST_APPROXIMABLE_DATE).Append("</ddms:approximableDate>");
                xml.Append("<ddms:searchableDate><ddms:start>").Append(TEST_START_DATE).Append("</ddms:start>");
                xml.Append("<ddms:end>").Append(TEST_END_DATE).Append("</ddms:end></ddms:searchableDate>");
                xml.Append("</ddms:acquiredOn>");
                return (xml.ToString());
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true)), DEFAULT_DDMS_PREFIX, TEST_NAME);
                GetInstance("The element name must be one of", WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));

                // No fields
                GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, false));
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // All fields
                GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);

                // No fields
                GetInstance(SUCCESS, TEST_NAME, null, null, null, null, null);
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong date format: approximableDate
                XElement element = Util.BuildDDMSElement(TEST_NAME, null);
                XElement approximableElment = Util.BuildDDMSElement("approximableDate", "---31");
                element.Add(approximableElment);
                GetInstance("The date datatype", element);

                // Invalid approximation
                element = Util.BuildDDMSElement(TEST_NAME, null);
                approximableElment = Util.BuildDDMSElement("approximableDate", TEST_APPROXIMABLE_DATE);
                Util.AddDDMSAttribute(approximableElment, "approximation", "almost-nearly");
                element.Add(approximableElment);
                GetInstance("The approximation must be one of", element);

                // Wrong date format: start
                element = Util.BuildDDMSElement(TEST_NAME, null);
                XElement searchableElement = Util.BuildDDMSElement("searchableDate", null);
                Util.AddDDMSChildElement(searchableElement, "start", "---31");
                Util.AddDDMSChildElement(searchableElement, "end", TEST_END_DATE);
                element.Add(searchableElement);
                GetInstance("The date datatype", element);

                // Wrong date format: end
                element = Util.BuildDDMSElement(TEST_NAME, null);
                searchableElement = Util.BuildDDMSElement("searchableDate", null);
                Util.AddDDMSChildElement(searchableElement, "start", TEST_START_DATE);
                Util.AddDDMSChildElement(searchableElement, "end", "---31");
                element.Add(searchableElement);
                GetInstance("The date datatype", element);
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // Wrong date format: approximableDate
                GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, "---31", TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);

                // Invalid approximation
                GetInstance("The approximation", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, "almost-nearly", TEST_START_DATE, TEST_END_DATE);

                // Wrong date format: start
                GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, "---31", TEST_END_DATE);

                // Wrong date format: end
                GetInstance("The date datatype", TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, "---31");
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                // No warnings
                ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));

                Assert.AreEqual(0, component.ValidationWarnings.Count());

                // Empty element
                component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, false));
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                string text = "A completely empty ddms:acquiredOn";
                string locator = "ddms:acquiredOn";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);

                // Description element with no child text
                XElement element = Util.BuildDDMSElement(TEST_NAME, null);
                element.Add(Util.BuildDDMSElement("description", null));
                Util.AddDDMSChildElement(element, "description", null);
                Util.AddDDMSChildElement(element, "approximableDate", TEST_APPROXIMABLE_DATE);
                component = GetInstance(SUCCESS, element);
                Assert.AreEqual(1, component.ValidationWarnings.Count());
                text = "A completely empty ddms:description";
                locator = "ddms:acquiredOn";
                AssertWarningEquality(text, locator, component.ValidationWarnings[0]);
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate elementComponent = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
                ApproximableDate dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate elementComponent = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
                ApproximableDate dataComponent = GetInstance(SUCCESS, "approximableStart", TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, DIFFERENT_VALUE, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, "2000", TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, "2nd qtr", TEST_START_DATE, TEST_END_DATE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, "2000", TEST_END_DATE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, "2500");
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML(SaveOptions.DisableFormatting));

                component = GetInstance(SUCCESS, TEST_NAME, TEST_DESCRIPTION, TEST_APPROXIMABLE_DATE, TEST_APPROXIMATION, TEST_START_DATE, TEST_END_DATE);
                Assert.AreEqual(ExpectedXMLOutput, component.ToXML(SaveOptions.DisableFormatting));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDDMSException), "the AcquiredOn element cannot be used")]
        public virtual void ApproximableDate_WrongVersion()
        {
            DDMSVersion.SetCurrentVersion("2.0");
            new ApproximableDate(TEST_NAME, null, null, null, null, null);
        }

        [TestMethod]
        public virtual void ApproximableDate_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate component = GetInstance(SUCCESS, GetFixtureElement(TEST_NAME, true));
                ApproximableDate.Builder builder = new ApproximableDate.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void ApproximableDate_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate.Builder builder = new ApproximableDate.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Description = TEST_DESCRIPTION;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDDMSException), "The approximation element cannot be used")]
        public virtual void ApproximableDate_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                ApproximableDate.Builder builder = new ApproximableDate.Builder();
                builder.Name = TEST_NAME;
                builder.ApproximableDate = TEST_APPROXIMABLE_DATE;
                builder.Approximation = "almost-nearly";
                builder.Commit();
                builder.Approximation = TEST_APPROXIMATION;
                builder.Commit();
            }
        }
    }
}