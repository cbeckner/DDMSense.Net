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

namespace DDMSense.Test.DDMS.Summary.Gml
{
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;
    using DDMSense.DDMS.Summary.Gml;
    using Microsoft.XmlDiffPatch;
    using System.Xml;
    using System.Xml.Linq;
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;

    /// <summary>
    /// <para> Tests related to gml:Point elements </para>
    ///
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    [TestClass]
    public class PointTest : AbstractBaseTestCase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PointTest()
            : base("point.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<Point> FixtureList
        {
            get
            {
                try
                {
                    List<Point> points = new List<Point>();
                    points.Add(new Point(new Position(PositionTest.TEST_COORDS, null), SRSAttributesTest.Fixture, TEST_ID));
                    return (points);
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Attempts to build a component from a XOM element.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="element"> the element to build from
        /// </param>
        /// <returns> a valid object </returns>
        private Point GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Point component = null;
            try
            {
                component = new Point(element);
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
        /// <param name="position"> the position (required) </param>
        /// <param name="srsAttributes"> the srs attributes (required) </param>
        /// <param name="id"> the id (required) </param>
        /// <returns> a valid object </returns>
        private Point GetInstance(string message, Position position, SRSAttributes srsAttributes, string id)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Point component = null;
            try
            {
                component = new Point(position, srsAttributes, id);
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
            text.Append(BuildOutput(isHTML, "Point.id", TEST_ID));
            text.Append(SRSAttributesTest.Fixture.GetOutput(isHTML, "Point."));
            text.Append(PositionTest.Fixture.GetOutput(isHTML, "Point.", ""));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            SRSAttributes attr = SRSAttributesTest.Fixture;
            StringBuilder xml = new StringBuilder();
            xml.Append("<gml:Point ").Append(XmlnsGML).Append(" ");
            xml.Append("gml:id=\"").Append(TEST_ID).Append("\" ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">\n\t");
            xml.Append("<gml:pos ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
            xml.Append(PositionTest.TEST_XS_LIST).Append("</gml:pos>\n");
            xml.Append("</gml:Point>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Point.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildElement(PropertyReader.GetPrefix("gml"), Point.GetName(version), version.GmlNamespace, null);
                Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, SRSAttributesTest.Fixture.SrsName);
                Util.AddAttribute(element, PropertyReader.GetPrefix("gml"), "id", version.GmlNamespace, TEST_ID);
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;

                // Missing SRS Name
                XElement element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("srsName is required.", element);

                // Empty SRS Name
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("srsName is required.", element);

                // Point SRS Name doesn't match pos SRS Name
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("The srsName of the position must match", element);

                // Missing ID
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("id is required.", element);

                // Empty ID
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, "");
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("id is required.", element);

                // ID not NCName
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, "1TEST");
                element.Add(PositionTest.Fixture.ElementCopy);
                GetInstance("Name cannot begin with the '1' character", element);

                // Missing position
                element = Util.BuildElement(gmlPrefix, Point.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                GetInstance("position is required.", element);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing SRS Name
                SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
                GetInstance("srsName is required.", PositionTest.Fixture, attr, TEST_ID);

                // Empty SRS Name
                attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
                GetInstance("srsName is required.", PositionTest.Fixture, attr, TEST_ID);

                // Polygon SRS Name doesn't match pos SRS Name
                attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                GetInstance("The srsName of the position must match", PositionTest.Fixture, attr, TEST_ID);

                // Missing ID
                GetInstance("id is required.", PositionTest.Fixture, SRSAttributesTest.Fixture, null);

                // Empty ID
                GetInstance("id is required.", PositionTest.Fixture, SRSAttributesTest.Fixture, "");

                // ID not NCName
                GetInstance("Name cannot begin with the '1' character", PositionTest.Fixture, SRSAttributesTest.Fixture, "1TEST");

                // Missing position
                GetInstance("position is required.", null, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Point dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SRSAttributes attr = new SRSAttributes(SRSAttributesTest.Fixture.SrsName, Convert.ToInt32(11), SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Point dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, attr, TEST_ID);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<double?> newCoords = new List<double?>();
                newCoords.Add(new double?(56.0));
                newCoords.Add(new double?(150.0));
                Position newPosition = new Position(newCoords, SRSAttributesTest.Fixture);

                dataComponent = GetInstance(SUCCESS, newPosition, SRSAttributesTest.Fixture, TEST_ID);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_XMLOutput()
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreWhitespace);
            XmlDocument expected = new XmlDocument();
            XmlDocument actual = new XmlDocument();
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
                expected.LoadXml(GetExpectedXMLOutput(false));
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));

                component = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
                actual.LoadXml(component.ToXML());
                Assert.IsTrue(diff.Compare(expected.DocumentElement, actual.DocumentElement));
            }
        }

        public virtual void Summary_Gml_Point_PositionReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position pos = PositionTest.Fixture;
                GetInstance(SUCCESS, pos, SRSAttributesTest.Fixture, TEST_ID);
                GetInstance(SUCCESS, pos, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Point.Builder builder = new Point.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Point.Builder builder = new Point.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Id = TEST_ID;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Point_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Point.Builder builder = new Point.Builder();
                builder.Id = TEST_ID;
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "srsName is required.");
                }
                builder.Position.Coordinates.Add(new Position.DoubleBuilder());
                builder.Position.Coordinates[0].Value = new double?(32.1);
                builder.Position.Coordinates.Add(new Position.DoubleBuilder());
                builder.Position.Coordinates[1].Value = new double?(42.1);
                builder.Id = "IDValue";
                builder.SrsAttributes.SrsName = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
                builder.Commit();
            }
        }
    }
}