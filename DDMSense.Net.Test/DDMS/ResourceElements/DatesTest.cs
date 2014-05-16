//using System.Collections.Generic;
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
//namespace DDMSense.Test.DDMS.ResourceElements {



//    using DDMSense.DDMS;
//    using DDMSense.DDMS.ResourceElements;
//    using DDMSense.DDMS;
//    using DDMSense.DDMS.ResourceElements;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:source elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class DatesTest : AbstractBaseTestCase {

//        private const string TEST_CREATED = "2003";
//        private const string TEST_POSTED = "2003-02";
//        private const string TEST_VALID = "2003-02-15";
//        private const string TEST_CUTOFF = "2001-10-31T17:00:00Z";
//        private const string TEST_APPROVED = "2003-02-16";
//        private const string TEST_RECEIVED = "2003-02-17";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public DatesTest() : base("dates.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Dates Fixture {
//            get {
//                try {
//                    return (new Dates(null, "2003", null, null, null, null, null));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Attempts to build a component from a XOM element.
//        /// </summary>
//        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
//        /// <param name="element"> the element to build from
//        /// </param>
//        /// <returns> a valid object </returns>
//        private Dates GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Dates component = null;
//            try {
//                component = new Dates(element);
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
//        /// <param name="acquiredOns"> a list of acquisition dates (optional, starting in 4.1) </param>
//        /// <param name="created"> the creation date (optional) </param>
//        /// <param name="posted"> the posting date (optional) </param>
//        /// <param name="validTil"> the expiration date (optional) </param>
//        /// <param name="infoCutOff"> the info cutoff date (optional) </param>
//        /// <param name="approvedOn"> the approved on date (optional, starting in 3.1) </param>
//        /// <param name="receivedOn"> the received on date (optional, starting in 4.0.1) </param>
//        /// <returns> a valid object </returns>
//        private Dates GetInstance(string message, IList<ApproximableDate> acquiredOns, string created, string posted, string validTil, string infoCutOff, string approvedOn, string receivedOn) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Dates component = null;
//            try {
//                component = new Dates(acquiredOns, created, posted, validTil, infoCutOff, approvedOn, receivedOn);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Generates an getApprovedOn() Date for testing
//        /// </summary>
//        private string ApprovedOn {
//            get {
//                return (DDMSVersion.CurrentVersion.isAtLeast("3.1") ? TEST_APPROVED : "");
//            }
//        }

//        /// <summary>
//        /// Generates a receivedOn Date for testing
//        /// </summary>
//        private string ReceivedOn {
//            get {
//                return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? TEST_RECEIVED : "");
//            }
//        }

//        /// <summary>
//        /// Generates an acquiredOn Date for testing
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private java.util.List<DDMSense.Net.Test.DDMS.ApproximableDate> getAcquiredOns() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private IList<ApproximableDate> AcquiredOns {
//            get {
//                IList<ApproximableDate> list = new List<ApproximableDate>();
//                if (DDMSVersion.CurrentVersion.isAtLeast("4.1")) {
//                    list.Add(new ApproximableDate(ApproximableDateTest.GetFixtureElement("acquiredOn", true)));
//                }
//                return (list);
//            }
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder text = new StringBuilder();
//            if (version.isAtLeast("4.1")) {
//                foreach (ApproximableDate acquiredOn in AcquiredOns) {
//                    text.Append(acquiredOn.getOutput(isHTML, "dates.", ""));
//                }
//            }
//            text.Append(BuildOutput(isHTML, "dates.created", TEST_CREATED));
//            text.Append(BuildOutput(isHTML, "dates.posted", TEST_POSTED));
//            text.Append(BuildOutput(isHTML, "dates.validTil", TEST_VALID));
//            text.Append(BuildOutput(isHTML, "dates.infoCutOff", TEST_CUTOFF));
//            if (version.isAtLeast("3.1")) {
//                text.Append(BuildOutput(isHTML, "dates.approvedOn", TEST_APPROVED));
//            }
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "dates.receivedOn", TEST_RECEIVED));
//            }
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                DDMSVersion version = DDMSVersion.CurrentVersion;
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:dates ").Append(XmlnsDDMS).Append(" ");
//                xml.Append("ddms:created=\"").Append(TEST_CREATED).Append("\" ");
//                xml.Append("ddms:posted=\"").Append(TEST_POSTED).Append("\" ");
//                xml.Append("ddms:validTil=\"").Append(TEST_VALID).Append("\" ");
//                xml.Append("ddms:infoCutOff=\"").Append(TEST_CUTOFF).Append("\"");
//                if (version.isAtLeast("3.1")) {
//                    xml.Append(" ddms:approvedOn=\"").Append(TEST_APPROVED).Append("\"");
//                }
//                if (version.isAtLeast("4.0.1")) {
//                    xml.Append(" ddms:receivedOn=\"").Append(TEST_RECEIVED).Append("\"");
//                }
//                if (version.isAtLeast("4.1")) {
//                    xml.Append("><ddms:acquiredOn>");
//                    xml.Append("<ddms:description>description</ddms:description>");
//                    xml.Append("<ddms:approximableDate ddms:approximation=\"1st qtr\">2012</ddms:approximableDate>");
//                    xml.Append("<ddms:searchableDate><ddms:start>2012-01</ddms:start>");
//                    xml.Append("<ddms:end>2012-03-31</ddms:end></ddms:searchableDate>");
//                    xml.Append("</ddms:acquiredOn></ddms:dates>");
//                } else {
//                    xml.Append(" />");
//                }
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Dates.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Dates.getName(version), null);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                // All fields
//                GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);

//                // No optional fields
//                GetInstance(SUCCESS, null, "", "", "", "", "", "");
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Wrong date format (using xs:gDay here)
//                XElement element = Util.buildDDMSElement(Dates.getName(version), null);
//                Util.addDDMSAttribute(element, "created", "---31");
//                GetInstance("The date datatype must be one of", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                // Wrong date format (using xs:gDay here)
//                GetInstance("The date datatype must be one of", AcquiredOns, "---31", TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, "---31", TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, "---31", TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, "---31", ApprovedOn, ReceivedOn);
//                if (version.isAtLeast("3.1")) {
//                    GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, "---31", ReceivedOn);
//                }
//                if (version.isAtLeast("4.0.1")) {
//                    GetInstance("The date datatype must be one of", AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "---31");
//                }
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));

//                // 4.1 ddms:acquiredOn element used
//                if (version.isAtLeast("4.1")) {
//                    assertEquals(1, component.ValidationWarnings.size());
//                    string text = "The ddms:acquiredOn element in this DDMS component";
//                    string locator = "ddms:dates";
//                    AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//                }
//                // No warnings 
//                else {
//                    assertEquals(0, component.ValidationWarnings.size());
//                }

//                // Empty element
//                XElement element = Util.buildDDMSElement(Dates.getName(version), null);
//                component = GetInstance(SUCCESS, element);
//                assertEquals(1, component.ValidationWarnings.size());
//                string text = "A completely empty ddms:dates element was found.";
//                string locator = "ddms:dates";
//                AssertWarningEquality(text, locator, component.ValidationWarnings.get(0));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDeprecatedConstructor() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDeprecatedConstructor() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates component = new Dates(TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertTrue(component.AcquiredOns.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDeprecatedAccessors() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDeprecatedAccessors() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Dates component = new Dates(TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertEquals(TEST_CREATED, component.Created.toXMLFormat());
//                assertEquals(TEST_POSTED, component.Posted.toXMLFormat());
//                assertEquals(TEST_VALID, component.ValidTil.toXMLFormat());
//                assertEquals(TEST_CUTOFF, component.InfoCutOff.toXMLFormat());
//                if (version.isAtLeast("3.1")) {
//                    assertEquals(TEST_APPROVED, component.ApprovedOn.toXMLFormat());
//                }
//                if (version.isAtLeast("4.0.1")) {
//                    assertEquals(TEST_RECEIVED, component.ReceivedOn.toXMLFormat());
//                }

//                // Not compatible with XMLGregorianCalendar
//                if (version.isAtLeast("4.1")) {
//                    component = new Dates("2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z", "2012-01-01T01:02Z");
//                    assertNull(component.Created);
//                    assertNull(component.Posted);
//                    assertNull(component.ValidTil);
//                    assertNull(component.InfoCutOff);
//                    assertNull(component.ApprovedOn);
//                    assertNull(component.ReceivedOn);
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Dates dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Dates dataComponent = GetInstance(SUCCESS, AcquiredOns, "", TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, "", TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, "", TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, "", ApprovedOn, ReceivedOn);
//                assertFalse(elementComponent.Equals(dataComponent));

//                if (version.isAtLeast("3.1")) {
//                    dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, "", ReceivedOn);
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }

//                if (version.isAtLeast("4.0.1")) {
//                    dataComponent = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "");
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }

//                if (version.isAtLeast("4.1")) {
//                    dataComponent = GetInstance(SUCCESS, null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, "");
//                    assertFalse(elementComponent.Equals(dataComponent));
//                }
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                assertFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, AcquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, ApprovedOn, ReceivedOn);
//                assertEquals(ExpectedXMLOutput, component.toXML());
//            }
//        }

//        public virtual void TestWrongVersionApprovedOn() {
//            DDMSVersion.CurrentVersion = "3.0";
//            try {
//                new Dates(null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, TEST_APPROVED, null);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot have an approvedOn date ");
//            }
//        }

//        public virtual void TestWrongVersionReceivedOn() {
//            DDMSVersion.CurrentVersion = "3.0";
//            try {
//                new Dates(null, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, null, TEST_RECEIVED);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot have a receivedOn date ");
//            }
//        }

//        public virtual void TestWrongVersionAcquiredOn() {
//            try {
//                DDMSVersion.CurrentVersion = "4.1";
//                IList<ApproximableDate> acquiredOns = AcquiredOns;
//                DDMSVersion.CurrentVersion = "3.0";
//                new Dates(acquiredOns, TEST_CREATED, TEST_POSTED, TEST_VALID, TEST_CUTOFF, null, null);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "This component cannot have an acquiredOn date");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Dates.Builder builder = new Dates.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates.Builder builder = new Dates.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.AcquiredOns.get(0).Description = "";
//                assertTrue(builder.Empty);
//                builder.AcquiredOns.get(0).Description = "test";
//                assertFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                Dates.Builder builder = new Dates.Builder();
//                builder.Created = "notAnXmlDate";
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "The date datatype must be one of");
//                }
//                builder.Created = TEST_CREATED;
//                builder.commit();
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                Dates.Builder builder = new Dates.Builder();
//                assertNotNull(builder.AcquiredOns.get(1));
//            }
//        }
//    }
//}