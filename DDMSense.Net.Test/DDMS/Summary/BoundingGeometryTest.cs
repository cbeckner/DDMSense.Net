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
//namespace DDMSense.Test.DDMS.Summary {

	
//    using Point = DDMSense.DDMS.Summary.Gml.Point;
//    using PointTest = DDMSense.Test.DDMS.Summary.Gml.PointTest;
//    using Polygon = DDMSense.DDMS.Summary.Gml.Polygon;
//    using PolygonTest = DDMSense.Test.DDMS.Summary.Gml.PolygonTest;
//    using Position = DDMSense.DDMS.Summary.Gml.Position;
//    using PositionTest = DDMSense.Test.DDMS.Summary.Gml.PositionTest;
//    using SRSAttributes = DDMSense.DDMS.Summary.Gml.SRSAttributes;
//    using SRSAttributesTest = DDMSense.Test.DDMS.Summary.Gml.SRSAttributesTest;
//    using DDMSVersion = DDMSense.Util.DDMSVersion;
//    using Util = DDMSense.Util.Util;
//    using DDMSense.DDMS.Summary;
//    using DDMSense.DDMS.Summary;
//    using System.Xml.Linq;

//    /// <summary>
//    /// <para> Tests related to ddms:subjectCoverage elements </para>
//    /// 
//    /// @author Brian Uri!
//    /// @since 0.9.b
//    /// </summary>
//    public class BoundingGeometryTest : AbstractBaseTestCase {

//        /// <summary>
//        /// Constructor
//        /// </summary>
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public BoundingGeometryTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public BoundingGeometryTest() : base("boundingGeometry.xml") {
//        }

//        /// <summary>
//        /// Returns a fixture object for testing.
//        /// </summary>
//        public static BoundingGeometry Fixture {
//            get {
//                try {
//                    return (new BoundingGeometry(null, PointTest.FixtureList));
//                } catch (InvalidDDMSException e) {
//                    fail("Could not create fixture: " + e.Message);
//                }
//                return (null);
//            }
//        }

//        /// <summary>
//        /// Helper method to create an object which is expected to be valid.
//        /// </summary>
//        /// <param name="element"> the element to build from </param>
//        /// <returns> a valid object </returns>
//        private BoundingGeometry GetInstance(string message, XElement element) {
//            bool expectFailure = !Util.isEmpty(message);
//            BoundingGeometry component = null;
//            try {
//                component = new BoundingGeometry(element);
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
//        /// <param name="polygons"> an ordered list of the polygons used in this geometry </param>
//        /// <param name="points"> an ordered list of the points used in this geometry </param>
//        /// <returns> a valid object </returns>
//        private BoundingGeometry GetInstance(string message, IList<Polygon> polygons, IList<Point> points) {
//            bool expectFailure = !Util.isEmpty(message);
//            BoundingGeometry component = null;
//            try {
//                component = new BoundingGeometry(polygons, points);
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
//            text.Append(PointTest.FixtureList[0].getOutput(isHTML, "boundingGeometry.", ""));
//            return (text.ToString());
//        }

//        /// <summary>
//        /// Returns the expected XML output for this unit test
//        /// </summary>
//        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//        private string GetExpectedXMLOutput(bool preserveFormatting) {
//            StringBuilder xml = new StringBuilder();
//            xml.Append("<ddms:boundingGeometry ").Append(XmlnsDDMS).Append(">\n\t");
//            xml.Append("<gml:Point ").Append(XmlnsGML).Append(" ");
//            xml.Append("gml:id=\"IDValue\" srsName=\"http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D\" srsDimension=\"10\" ").Append("axisLabels=\"A B C\" uomLabels=\"Meter Meter Meter\">\n\t\t");
//            xml.Append("<gml:pos>32.1 40.1</gml:pos>\n\t");
//            xml.Append("</gml:Point>\n");
//            xml.Append("</ddms:boundingGeometry>");
//            return (FormatXml(xml.ToString(), preserveFormatting));
//        }

//        public virtual void TestNameAndNamespace() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

//                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, BoundingGeometry.getName(version));
//                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestElementConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // Point
//                GetInstance(SUCCESS, GetValidElement(sVersion));

//                // Polygon
//                XElement element = Util.buildDDMSElement(BoundingGeometry.getName(version), null);
//                element.appendChild(PolygonTest.FixtureList[0].XOMElementCopy);
//                GetInstance(SUCCESS, element);

//                // Both
//                element = Util.buildDDMSElement(BoundingGeometry.getName(version), null);
//                element.appendChild(PolygonTest.FixtureList[0].XOMElementCopy);
//                element.appendChild(PointTest.FixtureList[0].XOMElementCopy);
//                GetInstance(SUCCESS, element);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestDataConstructorValid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // Point
//                GetInstance(SUCCESS, null, PointTest.FixtureList);

//                // Polygon
//                GetInstance(SUCCESS, PolygonTest.FixtureList, null);

//                // Both
//                GetInstance(SUCCESS, PolygonTest.FixtureList, PointTest.FixtureList);
//            }
//        }

//        public virtual void TestElementConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
//                // No polygons or points
//                XElement element = Util.buildDDMSElement(BoundingGeometry.getName(version), null);
//                GetInstance("At least 1 of ", element);
//            }
//        }

//        public virtual void TestDataConstructorInvalid() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No polygons or points
//                GetInstance("At least 1 of ", null, null);
//            }
//        }

//        public virtual void TestWarnings() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                // No warnings
//                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(0, component.ValidationWarnings.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                BoundingGeometry elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                BoundingGeometry dataComponent = GetInstance(SUCCESS, null, PointTest.FixtureList);
//                assertEquals(elementComponent, dataComponent);
//                assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestConstructorInequalityDifferentValues() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                BoundingGeometry elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
//                BoundingGeometry dataComponent = GetInstance(SUCCESS, PolygonTest.FixtureList, PointTest.FixtureList);
//                assertFalse(elementComponent.Equals(dataComponent));

//                dataComponent = GetInstance(SUCCESS, PolygonTest.FixtureList, null);
//                assertFalse(elementComponent.Equals(dataComponent));
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestHTMLTextOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;


//                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, null, PointTest.FixtureList);
//                assertEquals(GetExpectedOutput(true), component.toHTML());
//                assertEquals(GetExpectedOutput(false), component.toText());

//                component = GetInstance(SUCCESS, PolygonTest.FixtureList, null);
//                assertEquals(PolygonTest.FixtureList[0].getOutput(true, "boundingGeometry.", ""), component.toHTML());
//                assertEquals(PolygonTest.FixtureList[0].getOutput(false, "boundingGeometry.", ""), component.toText());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestXMLOutput() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
//                assertEquals(GetExpectedXMLOutput(true), component.toXML());

//                component = GetInstance(SUCCESS, null, PointTest.FixtureList);
//                assertEquals(GetExpectedXMLOutput(false), component.toXML());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderEquality() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));

//                // Equality after Building (Point-based)
//                BoundingGeometry.Builder builder = new BoundingGeometry.Builder(component);
//                assertEquals(component, builder.commit());

//                // Equality after Building (Polygon-based)
//                component = new BoundingGeometry(PolygonTest.FixtureList, null);
//                builder = new BoundingGeometry.Builder(component);
//                assertEquals(component, builder.commit());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderIsEmpty() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                BoundingGeometry.Builder builder = new BoundingGeometry.Builder();
//                assertNull(builder.commit());
//                assertTrue(builder.Empty);
//                builder.Points.get(0).Id = TEST_ID;
//                assertFalse(builder.Empty);
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderValidation() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;

//                BoundingGeometry.Builder builder = new BoundingGeometry.Builder();
//                foreach (Point point in PointTest.FixtureList) {
//                    Point.Builder pointBuilder = new Point.Builder(point);
//                    pointBuilder.Id = "";
//                    builder.Points.add(pointBuilder);
//                }
//                try {
//                    builder.commit();
//                    fail("Builder allowed invalid data.");
//                } catch (InvalidDDMSException e) {
//                    ExpectMessage(e, "id is required.");
//                }
//                builder = new BoundingGeometry.Builder();
//                foreach (Polygon polygon in PolygonTest.FixtureList) {
//                    builder.Polygons.add(new Polygon.Builder(polygon));
//                }
//                builder.commit();

//                // Skip empty Points
//                builder = new BoundingGeometry.Builder();
//                Point.Builder emptyBuilder = new Point.Builder();
//                Point.Builder fullBuilder = new Point.Builder();
//                fullBuilder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
//                fullBuilder.Id = TEST_ID;
//                fullBuilder.Position.Coordinates.get(0).Value = Convert.ToDouble(0);
//                fullBuilder.Position.Coordinates.get(1).Value = Convert.ToDouble(0);
//                builder.Points.add(emptyBuilder);
//                builder.Points.add(fullBuilder);
//                assertEquals(1, builder.commit().Points.size());

//                // Skip empty Polygons
//                builder = new BoundingGeometry.Builder();
//                Polygon.Builder emptyPolygonBuilder = new Polygon.Builder();
//                Polygon.Builder fullPolygonBuilder = new Polygon.Builder();
//                fullPolygonBuilder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
//                fullPolygonBuilder.Id = TEST_ID;
//                fullPolygonBuilder.Positions.add(new Position.Builder());
//                fullPolygonBuilder.Positions.add(new Position.Builder());
//                fullPolygonBuilder.Positions.add(new Position.Builder());
//                fullPolygonBuilder.Positions.add(new Position.Builder());
//                fullPolygonBuilder.Positions.get(0).Coordinates.get(0).Value = PositionTest.TEST_COORDS[0];
//                fullPolygonBuilder.Positions.get(0).Coordinates.get(1).Value = PositionTest.TEST_COORDS[1];

//                fullPolygonBuilder.Positions.get(1).Coordinates.get(0).Value = PositionTest.TEST_COORDS_2[0];
//                fullPolygonBuilder.Positions.get(1).Coordinates.get(1).Value = PositionTest.TEST_COORDS_2[1];

//                fullPolygonBuilder.Positions.get(2).Coordinates.get(0).Value = PositionTest.TEST_COORDS_3[0];
//                fullPolygonBuilder.Positions.get(2).Coordinates.get(1).Value = PositionTest.TEST_COORDS_3[1];

//                fullPolygonBuilder.Positions.get(3).Coordinates.get(0).Value = PositionTest.TEST_COORDS[0];
//                fullPolygonBuilder.Positions.get(3).Coordinates.get(1).Value = PositionTest.TEST_COORDS[1];

//                builder.Polygons.add(emptyPolygonBuilder);
//                builder.Polygons.add(fullPolygonBuilder);
//                assertEquals(1, builder.commit().Polygons.size());
//            }
//        }

////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
//        public virtual void TestBuilderLazyList() {
//            foreach (string sVersion in SupportedVersions) {
//                DDMSVersion.CurrentVersion = sVersion;
//                BoundingGeometry.Builder builder = new BoundingGeometry.Builder();
//                assertNotNull(builder.Points.get(1));
//                assertNotNull(builder.Polygons.get(1));
//            }
//        }
//    }

//}