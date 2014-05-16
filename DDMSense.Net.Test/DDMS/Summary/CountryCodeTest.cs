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


//    using DDMSense.DDMS.Summary;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;

//    /// <summary>
//    /// <para> Tests related to ddms:countryCode elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class CountryCodeTest : AbstractBaseTestCase {

//        private const string TEST_QUALIFIER = "ISO-3166";
//        private const string TEST_VALUE = "USA";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public CountryCodeTest() : base("countryCode.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static CountryCode Fixture {
//            get {
//                try {
//                    return (new CountryCode("ISO-3166", "USA"));
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
//        private CountryCode GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            CountryCode component = null;
//            try {
//                component = new CountryCode(element);
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
//        /// <param name="qualifier"> the qualifier value </param>
//        /// <param name="value"> the value </param>
//        /// <returns> a valid object </returns>
//        private CountryCode GetInstance(string message, string qualifier, string value) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            CountryCode component = null;
//            try {
//                component = new CountryCode(qualifier, value);
//                CheckConstructorSuccess(expectFailure);
//            } catch (InvalidDDMSException e) {
//                CheckConstructorFailure(expectFailure, e);
//                ExpectMessage(e, message);
//            }
//            return (component);
//        }

//        /// <summary>
//        /// Returns the expected HTML or Text output for this unit test
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        private string GetExpectedOutput(bool isHTML) {
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, "countryCode.qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "countryCode.value", TEST_VALUE));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        private string ExpectedXMLOutput {
//            get {
//                StringBuilder xml = new StringBuilder();
//                xml.Append("<ddms:countryCode ").Append(XmlnsDDMS).Append(" ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ddms:value=\"").Append(TEST_VALUE).Append("\" />");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, CountryCode.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                GetInstance(SUCCESS, GetValidElement(sVersion));
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string countryCodeName = CountryCode.getName(version);
//                // Missing qualifier
//                XElement element = Util.buildDDMSElement(countryCodeName, null);
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("qualifier attribute is required.", element);

//                // Empty qualifier
//                element = Util.buildDDMSElement(countryCodeName, null);
//                Util.addDDMSAttribute(element, "qualifier", "");
//                Util.addDDMSAttribute(element, "value", TEST_VALUE);
//                GetInstance("qualifier attribute is required.", element);

//                // Missing value
//                element = Util.buildDDMSElement(countryCodeName, null);
//                Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
//                GetInstance("value attribute is required.", element);

//                // Empty value
//                element = Util.buildDDMSElement(countryCodeName, null);
//                Util.addDDMSAttribute(element, "qualifier", TEST_QUALIFIER);
//                Util.addDDMSAttribute(element, "value", "");
//                GetInstance("value attribute is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Missing qualifier
//                GetInstance("qualifier attribute is required.", null, TEST_VALUE);

//                // Empty qualifier
//                GetInstance("qualifier attribute is required.", "", TEST_VALUE);

//                // Missing value
//                GetInstance("value attribute is required.", TEST_QUALIFIER, null);

//                // Empty value
//                GetInstance("value attribute is required.", TEST_QUALIFIER, "");
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // No warnings
//                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                CountryCode dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                CountryCode elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                CountryCode dataComponent = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_VALUE);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, DIFFERENT_VALUE);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_VALUE);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                CountryCode component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                CountryCode.Builder builder = new CountryCode.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                CountryCode.Builder builder = new CountryCode.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Value = TEST_VALUE;
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                CountryCode.Builder builder = new CountryCode.Builder();
//                builder.Value = TEST_VALUE;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "qualifier attribute is required.");
//                }
//                builder.Qualifier = TEST_QUALIFIER;
//                builder.commit();
//            }
//        }
//    }

//}