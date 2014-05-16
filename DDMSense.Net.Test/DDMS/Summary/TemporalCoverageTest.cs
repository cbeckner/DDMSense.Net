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
//namespace DDMSense.Test.DDMS.Summary {

	
//    using SecurityAttributes = DDMSense.DDMS.SecurityElements.Ism.SecurityAttributes;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS;
//    using DDMSense.DDMS;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:temporalCoverage elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class TemporalCoverageTest : AbstractBaseTestCase {

//        private const string TEST_NAME = "My Time Period";
//        private const string TEST_START = "1979-09-15";
//        private const string TEST_END = "Not Applicable";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public TemporalCoverageTest() : base("temporalCoverage.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static TemporalCoverage Fixture {
//            get {
//                try {
//                    return (new TemporalCoverage(null, "1979-09-15", "Not Applicable", null));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Generates an approximableDate for testing
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private DDMSense.Net.Test.DDMS.ApproximableDate getApproximableStart(boolean includeAllFields) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private ApproximableDate GetApproximableStart(bool includeAllFields) {
//            if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
//                return (new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableStart", includeAllFields)));
//            }
//            return (null);
//        }

//        /// <summary>
//        /// Generates an approximableDate for testing
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private DDMSense.Net.Test.DDMS.ApproximableDate getApproximableEnd(boolean includeAllFields) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private ApproximableDate GetApproximableEnd(bool includeAllFields) {
//            if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
//                return (new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableEnd", includeAllFields)));
//            }
//            return (null);
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="element"> the element to build from </param>
//        /// <returns> a valid object </returns>
//        private TemporalCoverage GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            TemporalCoverage component = null;
//            try {
//                if (DDMSVersion.CurrentVersion.isAtLeast("3.0")) {
//                    SecurityAttributesTest.Fixture.addTo(element);
//                }
//                component = new TemporalCoverage(element);
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
//        /// <param name="timePeriodName"> the time period name (optional) </param>
//        /// <param name="startString"> a string representation of the date (optional) (if empty, defaults to Unknown) </param>
//        /// <param name="approximableStart"> the start date, as an approximable date (optional) </param>
//        /// <param name="endString"> a string representation of the end date (optional) (if empty, defaults to Unknown) </param>
//        /// <param name="approximableEnd"> the end date, as an approximable date (optional) </param>
//        /// <returns> a valid object </returns>
//        private TemporalCoverage GetInstance(string message, string timePeriodName, string startString, ApproximableDate approximableStart, string endString, ApproximableDate approximableEnd) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            TemporalCoverage component = null;
//            try {
//                SecurityAttributes attr = (!DDMSVersion.CurrentVersion.isAtLeast("3.0")) ? null : SecurityAttributesTest.Fixture;
//                if (approximableStart != null && approximableEnd != null) {
//                    component = new TemporalCoverage(timePeriodName, approximableStart, approximableEnd, attr);
//                } else if (approximableStart != null && approximableEnd == null) {
//                    component = new TemporalCoverage(timePeriodName, approximableStart, endString, attr);
//                } else if (approximableStart == null && approximableEnd != null) {
//                    component = new TemporalCoverage(timePeriodName, startString, approximableEnd, attr);
//                } else {
//                    component = new TemporalCoverage(timePeriodName, startString, endString, attr);
//                }
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Helper method to manage the deprecated TimePeriod wrapper element
//        /// </summary>
//        /// <param name="innerElement"> the element containing the guts of this component </param>
//        /// <returns> the element itself in DDMS 4.0.1 or later, or the element wrapped in another element </returns>
//        private XElement WrapInnerElement(XElement innerElement) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            string name = TemporalCoverage.getName(version);
//            if (version.isAtLeast("4.0.1")) {
//                innerElement.LocalName = name;
//                return (innerElement);
//            }
//            XElement element = Util.buildDDMSElement(name, null);
//            element.appendChild(innerElement);
//            return (element);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML, boolean isApproximable) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML, bool isApproximable) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            string prefix = "temporalCoverage.";
//            if (!version.isAtLeast("4.0.1")) {
//                prefix += "TimePeriod.";
//            }
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, prefix + "name", TEST_NAME));
//            if (isApproximable) {
//                ApproximableDate start = new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableStart", true));
//                ApproximableDate end = new ApproximableDate(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
//                text.Append(start.getOutput(isHTML, prefix, ""));
//                text.Append(end.getOutput(isHTML, prefix, ""));
//            } else {
//                text.Append(BuildOutput(isHTML, prefix + "start", TEST_START));
//                text.Append(BuildOutput(isHTML, prefix + "end", TEST_END));
//            }
//            if (version.isAtLeast("3.0")) {
//                text.Append(SecurityAttributesTest.Fixture.getOutput(isHTML, prefix));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string GetExpectedXMLOutput(bool isApproximable) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:temporalCoverage ").Append(XmlnsDDMS);
//            if (version.isAtLeast("3.0")) {
//                xml.Append(" ").Append(XmlnsISM).Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
//            }
//            xml.Append(">");
//            if (!version.isAtLeast("4.0.1")) {
//                xml.Append("<ddms:TimePeriod>");
//            }
//            xml.Append("<ddms:name>").Append(TEST_NAME).Append("</ddms:name>");
//            if (isApproximable) {
//                xml.Append("<ddms:approximableStart>");
//                xml.Append("<ddms:description>description</ddms:description>");
//                xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
//                xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
//                xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
//                xml.Append("</ddms:approximableStart>");
//                xml.Append("<ddms:approximableEnd>");
//                xml.Append("<ddms:description>description</ddms:description>");
//                xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
//                xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
//                xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
//                xml.Append("</ddms:approximableEnd>");
//            } else {
//                xml.Append("<ddms:start>").Append(TEST_START).Append("</ddms:start>");
//                xml.Append("<ddms:end>").Append(TEST_END).Append("</ddms:end>");
//            }
//            if (!version.isAtLeast("4.0.1")) {
//                xml.Append("</ddms:TimePeriod>");
//            }
//            xml.Append("</ddms:temporalCoverage>");
//            return (xml.ToString());
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, TemporalCoverage.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // All fields, exact-exact
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("start", TEST_START));
//                periodElement.appendChild(Util.buildDDMSElement("end", TEST_END));
//                GetInstance(SUCCESS, WrapInnerElement(periodElement));

//                // All fields, approx-approx
//                if (version.isAtLeast("4.1")) {
//                    periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", false));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
//                    GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                }

//                // No optional fields, empty name element rather than no name element
//                periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("name", ""));
//                periodElement.appendChild(Util.buildDDMSElement("start", TEST_START));
//                periodElement.appendChild(Util.buildDDMSElement("end", TEST_END));
//                GetInstance(SUCCESS, WrapInnerElement(periodElement));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields, exact-exact
//                GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);

//                if (version.isAtLeast("4.1")) {
//                    // All fields, exact-approx
//                    GetInstance(SUCCESS, TEST_NAME, TEST_START, null, null, GetApproximableEnd(false));

//                    // All fields, approx-exact
//                    GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), TEST_END, null);

//                    // All fields, approx-approx
//                    GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), null, GetApproximableEnd(false));
//                }

//                // No optional fields
//                GetInstance(SUCCESS, "", TEST_START, null, TEST_END, null);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // Wrong date format (using xs:gDay here)
//                XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("start", "---31"));
//                periodElement.appendChild(Util.buildDDMSElement("end", TEST_END));
//                GetInstance("The date datatype must be one of", WrapInnerElement(periodElement));
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Wrong date format (using xs:gDay here)
//                GetInstance("The date datatype must be one of", TEST_NAME, "---31", null, TEST_END, null);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // No warnings, exact-exact
//                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());

//                // Empty name element
//                XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("name", null));
//                periodElement.appendChild(Util.buildDDMSElement("start", TEST_START));
//                periodElement.appendChild(Util.buildDDMSElement("end", TEST_END));
//                component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A ddms:name element was found with no value.";
//                string locator = version.isAtLeast("4.0.1") ? "ddms:temporalCoverage" : "ddms:temporalCoverage/ddms:TimePeriod";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));

//                // 4.1 ddms:approximableStart/End element used
//                if (version.isAtLeast("4.1")) {
//                    periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", true));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
//                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    assertEquals(1, component.ValidationWarnings.size());
//                    text = "The ddms:approximableStart or ddms:approximableEnd element";
//                    locator = "ddms:temporalCoverage";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // exact-exact
//                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                TemporalCoverage dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());

//                // approx-approx
//                if (version.isAtLeast("4.1")) {
//                    XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", false));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
//                    elementComponent = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    dataComponent = GetInstance(SUCCESS, null, null, GetApproximableStart(false), null, GetApproximableEnd(false));
//                    assertEquals(elementComponent, dataComponent);
//                    assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                TemporalCoverage dataComponent = GetInstance(SUCCESS, "", TEST_START, null, TEST_END, null);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_NAME, "Not Applicable", null, TEST_END, null);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, "2050", null);
//                assertFalse(elementComponent.Equals(dataComponent));

//                if (version.isAtLeast("4.1")) {
//                    XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", false));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", false));
//                    elementComponent = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    dataComponent = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(false), null, GetApproximableEnd(false));
//                    assertFalse(elementComponent.Equals(dataComponent));

//                    dataComponent = GetInstance(SUCCESS, null, null, GetApproximableStart(false), TEST_END, null);
//                    assertFalse(elementComponent.Equals(dataComponent));

//                    dataComponent = GetInstance(SUCCESS, null, TEST_START, null, null, GetApproximableEnd(false));
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                TemporalCoverage elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true, false), component.toHTML());
//                assertEquals(GetExpectedOutput(false, false), component.toText());

//                component = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
//                assertEquals(GetExpectedOutput(true, false), component.toHTML());
//                assertEquals(GetExpectedOutput(false, false), component.toText());

//                if (version.isAtLeast("4.1")) {
//                    XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(Util.buildDDMSElement("name", TEST_NAME));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", true));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
//                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    assertEquals(GetExpectedOutput(true, true), component.toHTML());
//                    assertEquals(GetExpectedOutput(false, true), component.toText());

//                    component = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(true), null, GetApproximableEnd(true));
//                    assertEquals(GetExpectedOutput(true, true), component.toHTML());
//                    assertEquals(GetExpectedOutput(false, true), component.toText());
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());

//                component = GetInstance(SUCCESS, TEST_NAME, TEST_START, null, TEST_END, null);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());

//                if (version.isAtLeast("4.1")) {
//                    XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(Util.buildDDMSElement("name", TEST_NAME));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", true));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
//                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                    component = GetInstance(SUCCESS, TEST_NAME, null, GetApproximableStart(true), null, GetApproximableEnd(true));
//                    assertEquals(GetExpectedXMLOutput(true), component.toXML());
//                }
//            }
//        }

//        public virtual void TestDefaultValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("start", ""));
//                periodElement.appendChild(Util.buildDDMSElement("end", ""));
//                TemporalCoverage component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                assertEquals("Unknown", component.StartString);
//                assertEquals("Unknown", component.EndString);

//                component = GetInstance(SUCCESS, "", "", null, "", null);
//                assertEquals("Unknown", component.TimePeriodName);
//                assertEquals("Unknown", component.StartString);
//                assertEquals("Unknown", component.EndString);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDeprecatedAccessors() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDeprecatedAccessors() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                periodElement.appendChild(Util.buildDDMSElement("start", ""));
//                periodElement.appendChild(Util.buildDDMSElement("end", ""));
//                TemporalCoverage component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                assertNull(component.Start);
//                assertNull(component.End);

//                component = GetInstance(SUCCESS, "", TEST_START, null, TEST_START, null);
//                assertEquals(TEST_START, component.Start.toXMLFormat());
//                assertEquals(TEST_START, component.End.toXMLFormat());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestSecurityAttributes() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                SecurityAttributes attr = (!version.isAtLeast("3.0") ? null : SecurityAttributesTest.Fixture);
//                TemporalCoverage component = new TemporalCoverage(TEST_NAME, TEST_START, TEST_END, attr);
//                if (!version.isAtLeast("3.0")) {
//                    assertTrue(component.SecurityAttributes.Empty);
//                } else {
//                    assertEquals(attr, component.SecurityAttributes);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionSecurityAttributes() {
//            DDMSVersion.CurrentVersion = "2.0";
//            try {
//                new TemporalCoverage(TEST_NAME, TEST_START, TEST_END, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Security attributes cannot be applied");
//            }
//        }

//        public virtual void TestWrongVersionApproximable() {
//            try {
//                DDMSVersion.CurrentVersion = "4.1";
//                ApproximableDate start = GetApproximableStart(false);
//                DDMSVersion.CurrentVersion = "3.0";
//                new TemporalCoverage(null, start, TEST_END, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Approximable dates cannot be used until DDMS 4.1 or later.");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                TemporalCoverage component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                TemporalCoverage.Builder builder = new TemporalCoverage.Builder(component);
//                assertEquals(component, builder.commit());

//                if (version.isAtLeast("4.1")) {
//                    XElement periodElement = Util.buildDDMSElement("TimePeriod", null);
//                    periodElement.appendChild(Util.buildDDMSElement("name", TEST_NAME));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableStart", true));
//                    periodElement.appendChild(ApproximableDateTest.GetFixtureElement("approximableEnd", true));
//                    component = GetInstance(SUCCESS, WrapInnerElement(periodElement));
//                    builder = new TemporalCoverage.Builder(component);
//                    assertEquals(component, builder.commit());
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                TemporalCoverage.Builder builder = new TemporalCoverage.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.StartString = TEST_START;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                TemporalCoverage.Builder builder = new TemporalCoverage.Builder();
//                builder.StartString = "Invalid";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "The date datatype");
//                }
//                builder.StartString = "2001";
//                builder.EndString = "2002";
//                builder.commit();

//                if (version.isAtLeast("4.1")) {
//                    builder.ApproximableStart.Description = "test";
//                    try {
//                        builder.commit();
//                        fail("Builder allowed invalid data.");
//                    } catch (InvalidDDMSException e) {
//                        ExpectMessage(e, "Only 1 of start or approximableStart");
//                    }

//                    builder.ApproximableStart.Description = null;
//                    builder.ApproximableEnd.Description = "test";
//                    try {
//                        builder.commit();
//                        fail("Builder allowed invalid data.");
//                    } catch (InvalidDDMSException e) {
//                        ExpectMessage(e, "Only 1 of end or approximableEnd");
//                    }

//                    builder.ApproximableStart.Description = "test";
//                    builder.StartString = null;
//                    builder.EndString = null;
//                    builder.commit();
//                }
//            }
//        }
//    }
//}