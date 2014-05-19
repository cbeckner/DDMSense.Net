//using System;
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

	
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:verticalExtent elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class VerticalExtentTest : AbstractBaseTestCase {

//        private const string TEST_UOM = "Meter";
//        private const string TEST_DATUM = "AGL";
//        private const double TEST_MIN = 0.1;
//        private const double TEST_MAX = 100.1;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public VerticalExtentTest() : base("verticalExtent.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static VerticalExtent Fixture {
//            get {
//                try {
//                    return (new VerticalExtent(1.1, 2.2, "Meter", "HAE"));
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
//        private VerticalExtent GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            VerticalExtent component = null;
//            try {
//                component = new VerticalExtent(element);
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
//        /// <param name="minVerticalExtent"> the minimum (required) </param>
//        /// <param name="maxVerticalExtent"> the maximum (required) </param>
//        /// <param name="unitOfMeasure"> the unit of measure (required) </param>
//        /// <param name="datum"> the datum (required) </param>
//        /// <returns> a valid object </returns>
//        private VerticalExtent GetInstance(string message, double minVerticalExtent, double maxVerticalExtent, string unitOfMeasure, string datum) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            VerticalExtent component = null;
//            try {
//                component = new VerticalExtent(minVerticalExtent, maxVerticalExtent, unitOfMeasure, datum);
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
//            text.Append(BuildOutput(isHTML, "verticalExtent.unitOfMeasure", Convert.ToString(TEST_UOM)));
//            text.Append(BuildOutput(isHTML, "verticalExtent.datum", Convert.ToString(TEST_DATUM)));
//            text.Append(BuildOutput(isHTML, "verticalExtent.minimum", Convert.ToString(TEST_MIN)));
//            text.Append(BuildOutput(isHTML, "verticalExtent.maximum", Convert.ToString(TEST_MAX)));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Helper method to get the correct-case of the minVerticalExtent eleemnt.
//        /// </summary>
//        private string MinVerticalExtentName {
//            get {
//                return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "minVerticalExtent" : "MinVerticalExtent");
//            }
//        }

//        /// <summary>
//        /// Helper method to get the correct-case of the maxVerticalExtent eleemnt.
//        /// </summary>
//        private string MaxVerticalExtentName {
//            get {
//                return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "maxVerticalExtent" : "MaxVerticalExtent");
//            }
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:verticalExtent ").Append(XmlnsDDMS).Append(" ");
//            xml.Append("ddms:unitOfMeasure=\"").Append(TEST_UOM).Append("\" ");
//            xml.Append("ddms:datum=\"").Append(TEST_DATUM).Append("\">\n\t");
//            xml.Append("<ddms:").Append(MinVerticalExtentName).Append(">").Append(TEST_MIN).Append("</ddms:").Append(MinVerticalExtentName).Append(">\n\t");
//            xml.Append("<ddms:").Append(MaxVerticalExtentName).Append(">").Append(TEST_MAX).Append("</ddms:").Append(MaxVerticalExtentName).Append(">\n");
//            xml.Append("</ddms:verticalExtent>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, VerticalExtent.getName(version));
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
//                GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string extentName = VerticalExtent.getName(version);
//                // Missing UOM
//                XElement element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("unitOfMeasure is required.", element);

//                // Invalid UOM
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", "furlong");
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("The length measure type must be one of", element);

//                // Missing Datum
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("datum is required.", element);

//                // Invalid Datum
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", "PDQ");
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("The vertical datum type must be one of", element);

//                // Missing MinVerticalExtent
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance(MinVerticalExtentName + " is required.", element);

//                // Missing MaxVerticalExtent
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                GetInstance(MaxVerticalExtentName + " is required.", element);

//                // MinVerticalExtent UOM doesn't match parent
//                XElement minElement = Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN));
//                Util.addDDMSAttribute(minElement, "unitOfMeasure", "Inch");
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(minElement);
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("The unitOfMeasure on the", element);

//                // MinVerticalExtent Datum doesn't match parent
//                minElement = Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN));
//                Util.addDDMSAttribute(minElement, "datum", "PDQ");
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(minElement);
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX)));
//                GetInstance("The datum on the", element);

//                // MaxVerticalExtent UOM doesn't match parent
//                XElement maxElement = Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX));
//                Util.addDDMSAttribute(maxElement, "unitOfMeasure", "Inch");
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(maxElement);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                GetInstance("The unitOfMeasure on the", element);

//                // MaxVerticalExtent Datum doesn't match parent
//                maxElement = Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MAX));
//                Util.addDDMSAttribute(maxElement, "datum", "PDQ");
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(maxElement);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                GetInstance("The datum on the", element);

//                // MinVerticalExtent is not less than MaxVerticalExtent
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MAX)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString(TEST_MIN)));
//                GetInstance("Minimum vertical extent must be less", element);

//                // Not Double
//                element = Util.buildDDMSElement(extentName, null);
//                Util.addDDMSAttribute(element, "unitOfMeasure", TEST_UOM);
//                Util.addDDMSAttribute(element, "datum", TEST_DATUM);
//                element.appendChild(Util.buildDDMSElement(MinVerticalExtentName, Convert.ToString(TEST_MIN)));
//                element.appendChild(Util.buildDDMSElement(MaxVerticalExtentName, Convert.ToString("ground-level")));
//                GetInstance(MaxVerticalExtentName + " is required.", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Missing UOM
//                GetInstance("unitOfMeasure is required.", TEST_MIN, TEST_MAX, null, TEST_DATUM);

//                // Invalid UOM
//                GetInstance("The length measure type must be one of", TEST_MIN, TEST_MAX, "furlong", TEST_DATUM);

//                // Missing Datum
//                GetInstance("datum is required.", TEST_MIN, TEST_MAX, TEST_UOM, null);

//                // Invalid Datum
//                GetInstance("The vertical datum type must be one of", TEST_MIN, TEST_MAX, TEST_UOM, "PDQ");

//                // MinVerticalExtent is not less than MaxVerticalExtent
//                GetInstance("Minimum vertical extent must be less", TEST_MAX, TEST_MIN, TEST_UOM, TEST_DATUM);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // No warnings
//                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VerticalExtent dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VerticalExtent dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, "Inch", TEST_DATUM);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, "HAE");
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, 1, TEST_MAX, TEST_UOM, TEST_DATUM);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, TEST_MIN, 101, TEST_UOM, TEST_DATUM);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                Assert.IsFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, TEST_MIN, TEST_MAX, TEST_UOM, TEST_DATUM);
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

//        public virtual void TestDoubleEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(component.MinVerticalExtent, Convert.ToDouble(TEST_MIN));
//                Assert.Equals(component.MaxVerticalExtent, Convert.ToDouble(TEST_MAX));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                VerticalExtent component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                VerticalExtent.Builder builder = new VerticalExtent.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                VerticalExtent.Builder builder = new VerticalExtent.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Datum = TEST_DATUM;
//                Assert.IsFalse(builder.Empty);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                VerticalExtent.Builder builder = new VerticalExtent.Builder();
//                builder.UnitOfMeasure = TEST_UOM;
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "A ddms:verticalExtent requires");
//                }
//                builder.Datum = TEST_DATUM;
//                builder.MaxVerticalExtent = TEST_MAX;
//                builder.MinVerticalExtent = TEST_MIN;
//            }
//        }
//    }

//}