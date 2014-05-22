using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    using DDMSense.DDMS.Summary;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using Point = DDMSense.DDMS.Summary.Gml.Point;
    using PointTest = DDMSense.Test.DDMS.Summary.Gml.PointTest;
    using Polygon = DDMSense.DDMS.Summary.Gml.Polygon;
    using PolygonTest = DDMSense.Test.DDMS.Summary.Gml.PolygonTest;
    using Position = DDMSense.DDMS.Summary.Gml.Position;
    using PositionTest = DDMSense.Test.DDMS.Summary.Gml.PositionTest;
    using SRSAttributes = DDMSense.DDMS.Summary.Gml.SRSAttributes;
    using SRSAttributesTest = DDMSense.Test.DDMS.Summary.Gml.SRSAttributesTest;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to ddms:subjectCoverage elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class BoundingGeometryTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BoundingGeometryTest()
            : base("boundingGeometry.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static BoundingGeometry Fixture
        {
            get
            {
                try
                {
                    return (new BoundingGeometry(null, PointTest.FixtureList));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="element"> the element to build from </param>
        /// <returns> a valid object </returns>
        private BoundingGeometry GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            BoundingGeometry component = null;
            try
            {
                component = new BoundingGeometry(element);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="polygons"> an ordered list of the polygons used in this geometry </param>
        /// <param name="points"> an ordered list of the points used in this geometry </param>
        /// <returns> a valid object </returns>
        private BoundingGeometry GetInstance(string message, List<Polygon> polygons, List<Point> points)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            BoundingGeometry component = null;
            try
            {
                component = new BoundingGeometry(polygons, points);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorFailure(expectAssert, e);
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
            text.Append(PointTest.FixtureList[0].GetOutput(isHTML, "boundingGeometry.", ""));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ddms:boundingGeometry ").Append(XmlnsDDMS).Append(">\n\t");
            xml.Append("<gml:Point ").Append(XmlnsGML).Append(" ");
            xml.Append("gml:id=\"IDValue\" srsName=\"http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D\" srsDimension=\"10\" ").Append("axisLabels=\"A B C\" uomLabels=\"Meter Meter Meter\">\n\t\t");
            xml.Append("<gml:pos>32.1 40.1</gml:pos>\n\t");
            xml.Append("</gml:Point>\n");
            xml.Append("</ddms:boundingGeometry>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, BoundingGeometry.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // Point
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // Polygon
                XElement element = Util.BuildDDMSElement(BoundingGeometry.GetName(version), null);
                element.Add(PolygonTest.FixtureList[0].ElementCopy);
                GetInstance(SUCCESS, element);

                // Both
                element = Util.BuildDDMSElement(BoundingGeometry.GetName(version), null);
                element.Add(PolygonTest.FixtureList[0].ElementCopy);
                element.Add(PointTest.FixtureList[0].ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Point
                GetInstance(SUCCESS, null, PointTest.FixtureList);

                // Polygon
                GetInstance(SUCCESS, PolygonTest.FixtureList, null);

                // Both
                GetInstance(SUCCESS, PolygonTest.FixtureList, PointTest.FixtureList);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // No polygons or points
                XElement element = Util.BuildDDMSElement(BoundingGeometry.GetName(version), null);
                GetInstance("At least 1 of ", element);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No polygons or points
                GetInstance("At least 1 of ", null, null);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingGeometry elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                BoundingGeometry dataComponent = GetInstance(SUCCESS, null, PointTest.FixtureList);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingGeometry elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                BoundingGeometry dataComponent = GetInstance(SUCCESS, PolygonTest.FixtureList, PointTest.FixtureList);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, PolygonTest.FixtureList, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, null, PointTest.FixtureList);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, PolygonTest.FixtureList, null);
                Assert.AreEqual(PolygonTest.FixtureList[0].GetOutput(true, "boundingGeometry.", ""), component.ToHTML());
                Assert.AreEqual(PolygonTest.FixtureList[0].GetOutput(false, "boundingGeometry.", ""), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, null, PointTest.FixtureList);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingGeometry component = GetInstance(SUCCESS, GetValidElement(sVersion));

                // Equality after Building (Point-based)
                BoundingGeometry.Builder builder = new BoundingGeometry.Builder(component);
                Assert.AreEqual(component, builder.Commit());

                // Equality after Building (Polygon-based)
                component = new BoundingGeometry(PolygonTest.FixtureList, null);
                builder = new BoundingGeometry.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingGeometry.Builder builder = new BoundingGeometry.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Points[0].Id = TEST_ID;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_BoundingGeometry_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                BoundingGeometry.Builder builder = new BoundingGeometry.Builder();
                foreach (Point point in PointTest.FixtureList)
                {
                    Point.Builder pointBuilder = new Point.Builder(point);
                    pointBuilder.Id = "";
                    builder.Points.Add(pointBuilder);
                }
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "id is required.");
                }
                builder = new BoundingGeometry.Builder();
                foreach (Polygon polygon in PolygonTest.FixtureList)
                {
                    builder.Polygons.Add(new Polygon.Builder(polygon));
                }
                builder.Commit();

                // Skip empty Points
                builder = new BoundingGeometry.Builder();
                Point.Builder emptyBuilder = new Point.Builder();
                Point.Builder fullBuilder = new Point.Builder();
                fullBuilder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
                fullBuilder.Id = TEST_ID;
                fullBuilder.Position.Coordinates[0].Value = Convert.ToDouble(0);
                fullBuilder.Position.Coordinates[1].Value = Convert.ToDouble(0);
                builder.Points.Add(emptyBuilder);
                builder.Points.Add(fullBuilder);
                builder.Commit();
                Assert.AreEqual(1, builder.Points.Count());

                // Skip empty Polygons
                builder = new BoundingGeometry.Builder();
                Polygon.Builder emptyPolygonBuilder = new Polygon.Builder();
                Polygon.Builder fullPolygonBuilder = new Polygon.Builder();
                fullPolygonBuilder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
                fullPolygonBuilder.Id = TEST_ID;
                fullPolygonBuilder.Positions.Add(new Position.Builder());
                fullPolygonBuilder.Positions.Add(new Position.Builder());
                fullPolygonBuilder.Positions.Add(new Position.Builder());
                fullPolygonBuilder.Positions.Add(new Position.Builder());
                fullPolygonBuilder.Positions[0].Coordinates[0].Value = PositionTest.TEST_COORDS[0];
                fullPolygonBuilder.Positions[0].Coordinates[1].Value = PositionTest.TEST_COORDS[1];

                fullPolygonBuilder.Positions[1].Coordinates[0].Value = PositionTest.TEST_COORDS_2[0];
                fullPolygonBuilder.Positions[1].Coordinates[1].Value = PositionTest.TEST_COORDS_2[1];

                fullPolygonBuilder.Positions[2].Coordinates[0].Value = PositionTest.TEST_COORDS_3[0];
                fullPolygonBuilder.Positions[2].Coordinates[1].Value = PositionTest.TEST_COORDS_3[1];

                fullPolygonBuilder.Positions[3].Coordinates[0].Value = PositionTest.TEST_COORDS[0];
                fullPolygonBuilder.Positions[3].Coordinates[1].Value = PositionTest.TEST_COORDS[1];

                builder.Polygons.Add(emptyPolygonBuilder);
                builder.Polygons.Add(fullPolygonBuilder);
                builder.Commit();
                Assert.AreEqual(1, builder.Polygons.Count());
            }
        }
    }
}