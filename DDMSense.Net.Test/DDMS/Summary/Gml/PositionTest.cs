//using System;
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
//namespace DDMSense.Test.DDMS.Summary.Gml {


	
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using PropertyReader = DDMSense.Util.PropertyReader;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary.Gml;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to gml:pos elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class PositionTest : AbstractBaseTestCase {

//        public static readonly IList<double?> TEST_COORDS = new List<double?>();
//        static PositionTest() {
//            TEST_COORDS.Add(new double?(32.1));
//            TEST_COORDS.Add(new double?(40.1));
//            TEST_COORDS_2.Add(new double?(42.1));
//            TEST_COORDS_2.Add(new double?(40.1));
//            TEST_COORDS_3.Add(new double?(42.1));
//            TEST_COORDS_3.Add(new double?(50.1));
//        }
//        protected internal static readonly string TEST_XS_LIST = Util.getXsList(TEST_COORDS);

//        public static readonly IList<double?> TEST_COORDS_2 = new List<double?>();
//        public static readonly IList<double?> TEST_COORDS_3 = new List<double?>();

//        /// <summary>
//        /// Constructor
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public PositionTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public PositionTest() : base("position.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static Position Fixture {
//            get {
//                try {
//                    return (new Position(PositionTest.TEST_COORDS, SRSAttributesTest.Fixture));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Returns a fixture object for testing. This list of positions represents a closed polygon.
//        /// </summary>
//        public static IList<Position> FixtureList {
//            get {
//                try {
//                    IList<Position> positions = new List<Position>();
//                    positions.Add(new Position(TEST_COORDS, SRSAttributesTest.Fixture));
//                    positions.Add(new Position(TEST_COORDS_2, SRSAttributesTest.Fixture));
//                    positions.Add(new Position(TEST_COORDS_3, SRSAttributesTest.Fixture));
//                    positions.Add(new Position(TEST_COORDS, SRSAttributesTest.Fixture));
//                    return (positions);
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
//        private Position GetInstance(string message, XElement element) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Position component = null;
//            try {
//                component = new Position(element);
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
//        /// <param name="coordinates"> the coordinates </param>
//        /// <param name="srsAttributes"> the srs attributes (optional) </param>
//        /// <returns> a valid object </returns>
//        private Position GetInstance(string message, IList<double?> coordinates, SRSAttributes srsAttributes) {
//            bool expectFailure = !String.IsNullOrEmpty(message);
//            Position component = null;
//            try {
//                component = new Position(coordinates, srsAttributes);
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
//            text.Append(BuildOutput(isHTML, "pos", TEST_XS_LIST));
//            text.Append(SRSAttributesTest.Fixture.getOutput(isHTML, "pos."));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<gml:pos ").Append(XmlnsGML).Append(" ");
//            xml.Append("srsName=\"").Append(SRSAttributesTest.TEST_SRS_NAME).Append("\" ");
//            xml.Append("srsDimension=\"").Append(SRSAttributesTest.TEST_SRS_DIMENSION).Append("\" ");
//            xml.Append("axisLabels=\"").Append(Util.getXsList(SRSAttributesTest.TEST_AXIS_LABELS)).Append("\" ");
//            xml.Append("uomLabels=\"").Append(Util.getXsList(SRSAttributesTest.TEST_UOM_LABELS)).Append("\">");
//            xml.Append(TEST_XS_LIST).Append("</gml:pos>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Position.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string gmlPrefix = PropertyReader.getPrefix("gml");
//                string gmlNamespace = version.GmlNamespace;

//                // All fields
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // No optional fields
//                XElement element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST);
//                GetInstance(SUCCESS, element);

//                // Empty coordinate
//                element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, "25.0    26.0");
//                GetInstance(SUCCESS, element);

//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // All fields
//                GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);

//                // No optional fields
//                GetInstance(SUCCESS, TEST_COORDS, null);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string gmlPrefix = PropertyReader.getPrefix("gml");
//                string gmlNamespace = version.GmlNamespace;
//                // Missing coordinates
//                XElement element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, null);
//                SRSAttributesTest.Fixture.addTo(element);
//                GetInstance("A position must be represented by", element);

//                // At least 2 coordinates
//                element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, "25.0");
//                SRSAttributesTest.Fixture.addTo(element);
//                GetInstance("A position must be represented by", element);

//                // No more than 3 coordinates
//                element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST + " 25.0 35.0");
//                SRSAttributesTest.Fixture.addTo(element);
//                GetInstance("A position must be represented by", element);

//                // Each coordinate is a Double
//                element = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, "25.0 Dog");
//                SRSAttributesTest.Fixture.addTo(element);
//                GetInstance("coordinate is required.", element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // Missing coordinates
//                GetInstance("A position must be represented by", null, SRSAttributesTest.Fixture);

//                // At least 2 coordinates
//                IList<double?> newCoords = new List<double?>();
//                newCoords.Add(new double?(12.3));
//                GetInstance("A position must be represented by", newCoords, SRSAttributesTest.Fixture);

//                // No more than 3 coordinates
//                newCoords = new List<double?>();
//                newCoords.Add(new double?(12.3));
//                newCoords.Add(new double?(12.3));
//                newCoords.Add(new double?(12.3));
//                newCoords.Add(new double?(12.3));
//                GetInstance("A position must be represented by", newCoords, SRSAttributesTest.Fixture);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                // No warnings
//                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Position dataComponent = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
//                Assert.Equals(elementComponent, dataComponent);
//                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testEqualityWhitespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestEqualityWhitespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                string gmlPrefix = PropertyReader.getPrefix("gml");
//                string gmlNamespace = version.GmlNamespace;
//                Position position = new Position(Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST));
//                Position positionEqual = new Position(Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST));
//                Position positionEqualWhitespace = new Position(Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST + "   "));
//                Position positionUnequal2d = new Position(Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, "32.1 40.0"));
//                Position positionUnequal3d = new Position(Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, TEST_XS_LIST + " 40.0"));
//                Assert.Equals(position, positionEqual);
//                Assert.Equals(position, positionEqualWhitespace);
//                Assert.IsFalse(position.Equals(positionUnequal2d));
//                Assert.IsFalse(position.Equals(positionUnequal3d));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Position dataComponent = GetInstance(SUCCESS, TEST_COORDS, null);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));

//                IList<double?> newCoords = new List<double?>(TEST_COORDS);
//                newCoords.Add(new double?(100.0));
//                dataComponent = GetInstance(SUCCESS, newCoords, SRSAttributesTest.Fixture);
//                Assert.IsFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityWrongClass() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Rights wrongComponent = new Rights(true, true, true);
//                Assert.IsFalse(elementComponent.Equals(wrongComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
//                Assert.Equals(GetExpectedOutput(true), component.toHTML());
//                Assert.Equals(GetExpectedOutput(false), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Assert.Equals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
//                Assert.Equals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                Position.Builder builder = new Position.Builder(component);
//                Assert.Equals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Position.Builder builder = new Position.Builder();
//                Assert.IsNull(builder.commit());
//                Assert.IsTrue(builder.Empty);
//                builder.Coordinates[0].Value = Convert.ToDouble(0);
//                Assert.IsFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);

//                Position.Builder builder = new Position.Builder();
//                builder.Coordinates[0].Value = Convert.ToDouble(0);
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "A position must be represented by");
//                }
//                builder.Coordinates.get(1).Value = Convert.ToDouble(0);
//                builder.commit();

//                // Skip empty Coordinates
//                builder = new Position.Builder();
//                builder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
//                builder.Coordinates[0].Value = null;
//                builder.Coordinates.get(1).Value = Convert.ToDouble(0);
//                builder.Coordinates.get(2).Value = Convert.ToDouble(1);
//                Assert.Equals(2, builder.commit().Coordinates.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.SetCurrentVersion(sVersion);
//                Position.Builder builder = new Position.Builder();
//                assertNotNull(builder.Coordinates.get(1));
//            }
//        }
//    }
//}