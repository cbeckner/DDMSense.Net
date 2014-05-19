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
//namespace DDMSense.Test.DDMS.Summary {


//    using ExtensibleAttributes = DDMSense.DDMS.Extensible.ExtensibleAttributes;
//    using ExtensibleAttributesTest = DDMSense.Test.DDMS.Extensible.ExtensibleAttributesTest;
//    using SecurityAttributesTest = DDMSense.Test.DDMS.SecurityElements.Ism.SecurityAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:category elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class CategoryTest : AbstractBaseTestCase {

//        private const string TEST_QUALIFIER = "http://metadata.dod.mil/mdr/artifiact/MET/severeWeatherCode_enum/xml";
//        private const string TEST_CODE = "T";
//        private const string TEST_LABEL = "TORNADO";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public CategoryTest() : base("category.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static List<Category> FixtureList {
//            get {
//                try {
//                    List<Category> categories = new List<Category>();
//                    categories.Add(new Category("urn:buri:ddmsence:categories", "DDMS", "DDMS", null));
//                    return (categories);
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
//        private Category GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Category component = null;
//            try {
//                component = new Category(element);
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
//        /// <param name="qualifier"> the qualifier (optional) </param>
//        /// <param name="code"> the code (optional) </param>
//        /// <param name="label"> the label (required) </param>
//        /// <returns> a valid object </returns>
//        private Category GetInstance(string message, string qualifier, string code, string label) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            Category component = null;
//            try {
//                component = new Category(qualifier, code, label, version.isAtLeast("4.0.1") ? SecurityAttributesTest.Fixture : null);
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
//            DDMSVersion version = DDMSVersion.CurrentVersion;
//            StringBuilder text = new StringBuilder();
//            text.Append(BuildOutput(isHTML, "category.qualifier", TEST_QUALIFIER));
//            text.Append(BuildOutput(isHTML, "category.code", TEST_CODE));
//            text.Append(BuildOutput(isHTML, "category.label", TEST_LABEL));
//            if (version.isAtLeast("4.0.1")) {
//                text.Append(BuildOutput(isHTML, "category.classification", "U"));
//                text.Append(BuildOutput(isHTML, "category.ownerProducer", "USA"));
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
//                xml.Append("<ddms:category ").Append(XmlnsDDMS).Append(" ");
//                if (version.isAtLeast("4.0.1")) {
//                    xml.Append(XmlnsISM).Append(" ");
//                }
//                xml.Append("ddms:qualifier=\"").Append(TEST_QUALIFIER).Append("\" ");
//                xml.Append("ddms:code=\"").Append(TEST_CODE).Append("\" ");
//                xml.Append("ddms:label=\"").Append(TEST_LABEL).Append("\"");
//                if (version.isAtLeast("4.0.1")) {
//                    xml.Append(" ISM:classification=\"U\" ISM:ownerProducer=\"USA\"");
//                }
//                xml.Append(" />");
//                return (xml.ToString());
//            }
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, Category.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildDDMSElement(Category.getName(version), null);
//                Util.addDDMSAttribute(element, "label", TEST_LABEL);
//                GetInstance(SUCCESS, element);
//            }
//        }

//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);

//                // No optional fields
//                GetInstance(SUCCESS, "", "", TEST_LABEL);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Missing label
//                XElement element = Util.buildDDMSElement(Category.getName(version), null);
//                GetInstance("label attribute is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Missing label
//                GetInstance("label attribute is required.", TEST_QUALIFIER, TEST_CODE, null);

//                // Qualifier not URI
//                GetInstance("Invalid URI", INVALID_URI, TEST_CODE, TEST_LABEL);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // No warnings
//                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Category dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Category dataComponent = GetInstance(SUCCESS, "", TEST_CODE, TEST_LABEL);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, "", TEST_LABEL);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, DIFFERENT_VALUE);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Category elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                Assert.IsFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(ExpectedXMLOutput, component.toXML());

//                component = GetInstance(SUCCESS, TEST_QUALIFIER, TEST_CODE, TEST_LABEL);
//                Assert.Equals(ExpectedXMLOutput, component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testExtensibleSuccess() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestExtensibleSuccess() {
//            // Extensible attribute added
//            DDMSVersion.CurrentVersion = "3.0";
//            ExtensibleAttributes attr = ExtensibleAttributesTest.Fixture;
//            new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attr);
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testExtensibleFailure() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestExtensibleFailure() {
//            // Wrong DDMS Version
//            DDMSVersion.SetCurrentVersion("2.0");
//            ExtensibleAttributes attributes = ExtensibleAttributesTest.Fixture;
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "xs:anyAttribute cannot be applied");
//            }

//            DDMSVersion version = DDMSVersion.setCurrentVersion("3.0");

//            // Using ddms:qualifier as the extension (data)
//            List<Attribute> extAttributes = new List<Attribute>();
//            extAttributes.Add(new Attribute("ddms:qualifier", version.Namespace, "dog"));
//            attributes = new ExtensibleAttributes(extAttributes);
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The extensible attribute with the name, ddms:qualifier");
//            }

//            // Using ddms:code as the extension (data)
//            extAttributes = new List<Attribute>();
//            extAttributes.Add(new Attribute("ddms:code", version.Namespace, "dog"));
//            attributes = new ExtensibleAttributes(extAttributes);
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The extensible attribute with the name, ddms:code");
//            }

//            // Using ddms:label as the extension (data)
//            extAttributes = new List<Attribute>();
//            extAttributes.Add(new Attribute("ddms:label", version.Namespace, "dog"));
//            attributes = new ExtensibleAttributes(extAttributes);
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "The extensible attribute with the name, ddms:label");
//            }

//            // Using icism:classification as the extension (data)
//            extAttributes = new List<Attribute>();
//            extAttributes.Add(new Attribute("icism:classification", version.IsmNamespace, "U"));
//            attributes = new ExtensibleAttributes(extAttributes);
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, null, attributes);
//            } catch (InvalidDDMSException) {
//                fail("Prevented valid data.");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testWrongVersionSecurityAttributes() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestWrongVersionSecurityAttributes() {
//            DDMSVersion.CurrentVersion = "3.1";
//            try {
//                new Category(TEST_QUALIFIER, TEST_CODE, TEST_LABEL, SecurityAttributesTest.Fixture);
//                fail("Allowed invalid data.");
//            } catch (InvalidDDMSException e) {
//                ExpectMessage(e, "Security attributes cannot be applied");
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Category component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Category.Builder builder = new Category.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Category.Builder builder = new Category.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Label = TEST_LABEL;
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Category.Builder builder = new Category.Builder();
//                builder.Qualifier = TEST_QUALIFIER;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "label attribute is required.");
//                }
//                builder.Label = TEST_LABEL;
//                builder.commit();
//            }
//        }
//    }

//}