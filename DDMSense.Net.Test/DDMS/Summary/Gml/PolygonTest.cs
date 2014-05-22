using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
    using DDMSVersion = DDMSense.Util.DDMSVersion;
    using PropertyReader = DDMSense.Util.PropertyReader;
    using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary.Gml;
    using System.Xml.Linq;
    using DDMSense.DDMS;
    using DDMSense.DDMS.ResourceElements;

    /// <summary>
    /// <para> Tests related to gml:Polygon elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.0
    /// </summary>
    [TestClass]
    public class PolygonTest : AbstractBaseTestCase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public PolygonTest()
            : base("polygon.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static List<Polygon> FixtureList
        {
            get
            {
                try
                {
                    List<Polygon> polygons = new List<Polygon>();
                    polygons.Add(new Polygon(PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID));
                    return (polygons);
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
        private Polygon GetInstance(string message, XElement element)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Polygon component = null;
            try
            {
                component = new Polygon(element);
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
        /// <param name="positions"> the positions (required) </param>
        /// <param name="srsAttributes"> the srs attributes (required) </param>
        /// <param name="id"> the id (required) </param>
        /// <returns> a valid object </returns>
        private Polygon GetInstance(string message, List<Position> positions, SRSAttributes srsAttributes, string id)
        {
            bool expectFailure = !String.IsNullOrEmpty(message);
            Polygon component = null;
            try
            {
                component = new Polygon(positions, srsAttributes, id);
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
        /// Wraps a list of positions into the nested elements needed for a valid construct
        /// </summary>
        /// <param name="positions"> the positions </param>
        /// <returns> an exterior element containing a LinearRing element containing the positions </returns>
        private XElement WrapPositions(List<Position> positions)
        {
            string gmlNamespace = DDMSVersion.CurrentVersion.GmlNamespace;
            XElement ringElement = Util.BuildElement(PropertyReader.GetPrefix("gml"), "LinearRing", gmlNamespace, null);
            foreach (Position pos in positions)
            {
                ringElement.Add(pos.ElementCopy);
            }
            XElement extElement = Util.BuildElement(PropertyReader.GetPrefix("gml"), "exterior", gmlNamespace, null);
            extElement.Add(ringElement);
            return (extElement);
        }

        /// <summary>
        /// Returns the expected HTML or Text output for this unit test
        /// </summary>
        private string GetExpectedOutput(bool isHTML)
        {
            StringBuilder text = new StringBuilder();
            text.Append(BuildOutput(isHTML, "Polygon.id", TEST_ID));
            text.Append(SRSAttributesTest.Fixture.GetOutput(isHTML, "Polygon."));
            foreach (Position pos in PositionTest.FixtureList)
            {
                text.Append(pos.GetOutput(isHTML, "Polygon.", ""));
            }
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
            xml.Append("<gml:Polygon ").Append(XmlnsGML).Append(" ");
            xml.Append("gml:id=\"").Append(TEST_ID).Append("\" ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">\n\t");
            xml.Append("<gml:exterior>\n\t\t");
            xml.Append("<gml:LinearRing>\n\t\t\t");
            xml.Append("<gml:pos ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
            xml.Append(Util.GetXsList(PositionTest.TEST_COORDS)).Append("</gml:pos>\n\t\t\t");
            xml.Append("<gml:pos ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
            xml.Append(Util.GetXsList(PositionTest.TEST_COORDS_2)).Append("</gml:pos>\n\t\t\t");
            xml.Append("<gml:pos ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
            xml.Append(Util.GetXsList(PositionTest.TEST_COORDS_3)).Append("</gml:pos>\n\t\t\t");
            xml.Append("<gml:pos ");
            xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
            xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
            xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
            xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
            xml.Append(Util.GetXsList(PositionTest.TEST_COORDS)).Append("</gml:pos>\n\t\t");
            xml.Append("</gml:LinearRing>\n\t");
            xml.Append("</gml:exterior>\n");
            xml.Append("</gml:Polygon>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }


        [TestMethod]
        public virtual void Summary_Gml_Polygon_NameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Polygon.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }


        [TestMethod]
        public virtual void Summary_Gml_Polygon_ElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                Util.AddAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, SRSAttributesTest.Fixture.SrsName);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance(SUCCESS, element);

                // First position matches last position but has extra whitespace.
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                List<Position> newPositions = new List<Position>(PositionTest.FixtureList);
                newPositions.Add(PositionTest.FixtureList[1]);
                XElement posElement = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, "32.1         40.1");
                SRSAttributesTest.Fixture.AddTo(posElement);
                Position positionWhitespace = new Position(posElement);
                newPositions.Add(positionWhitespace);
                element.Add(WrapPositions(newPositions));
                GetInstance(SUCCESS, element);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_DataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_ElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;
                // Missing SRS Name
                XElement element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("srsName is required.", element);

                // Empty SRS Name
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("srsName is required.", element);

                // Polygon SRS Name doesn't match pos SRS Name
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                attr.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("The srsName of each position", element);

                // Missing ID
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("id is required.", element);

                // Empty ID
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, "");
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("id is required.", element);

                // ID not NCName
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, "1TEST");
                element.Add(WrapPositions(PositionTest.FixtureList));
                GetInstance("\"1TEST\" is not a valid NCName.", element);

                // Missing Positions
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                element.Add(WrapPositions(new List<Position>()));
                GetInstance("At least 4 positions are required", element);

                // First position doesn't match last position.
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                List<Position> newPositions = new List<Position>(PositionTest.FixtureList);
                newPositions.Add(PositionTest.FixtureList[1]);
                element.Add(WrapPositions(newPositions));
                GetInstance("The first and last position", element);

                // Not enough positions
                element = Util.BuildElement(gmlPrefix, Polygon.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                Util.AddAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
                newPositions = new List<Position>();
                newPositions.Add(PositionTest.FixtureList[0]);
                element.Add(WrapPositions(newPositions));
                GetInstance("At least 4 positions are required", element);

                // Tests on shared attributes are done in the PositionTest.
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_DataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing SRS Name
                SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
                GetInstance("srsName is required.", PositionTest.FixtureList, attr, TEST_ID);

                // Empty SRS Name
                attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
                GetInstance("srsName is required.", PositionTest.FixtureList, attr, TEST_ID);

                // Polygon SRS Name doesn't match pos SRS Name
                attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                GetInstance("The srsName of each position", PositionTest.FixtureList, attr, TEST_ID);

                // Missing ID
                GetInstance("id is required.", PositionTest.FixtureList, SRSAttributesTest.Fixture, null);

                // Empty ID
                GetInstance("id is required.", PositionTest.FixtureList, SRSAttributesTest.Fixture, "");

                // ID not NCName
                GetInstance("\"1TEST\" is not a valid NCName.", PositionTest.FixtureList, SRSAttributesTest.Fixture, "1TEST");

                // Missing Positions
                GetInstance("At least 4 positions are required", null, SRSAttributesTest.Fixture, TEST_ID);

                // First position doesn't match last position.
                List<Position> newPositions = new List<Position>(PositionTest.FixtureList);
                newPositions.Add(PositionTest.FixtureList[1]);
                GetInstance("The first and last position", newPositions, SRSAttributesTest.Fixture, TEST_ID);

                // Not enough positions
                newPositions = new List<Position>();
                newPositions.Add(PositionTest.FixtureList[0]);
                GetInstance("At least 4 positions are required", newPositions, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_Warnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_ConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Polygon dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
                Assert.AreEqual(elementComponent, dataComponent);
                Assert.AreEqual(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_ConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                SRSAttributes attr = new SRSAttributes(SRSAttributesTest.Fixture.SrsName, Convert.ToInt32(11), SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
                Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Polygon dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, attr, TEST_ID);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<Position> newPositions = new List<Position>(PositionTest.FixtureList);
                newPositions.Add(PositionTest.FixtureList[1]);
                newPositions.Add(PositionTest.FixtureList[0]);
                dataComponent = GetInstance(SUCCESS, newPositions, SRSAttributesTest.Fixture, TEST_ID);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, DIFFERENT_VALUE);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_ConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_HTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
                Assert.AreEqual(GetExpectedOutput(true), component.ToHTML());
                Assert.AreEqual(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_XMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.AreEqual(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
                Assert.AreEqual(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        
        public virtual void Summary_Gml_Polygon_PositionReuse()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                List<Position> positions = PositionTest.FixtureList;
                GetInstance(SUCCESS, positions, SRSAttributesTest.Fixture, TEST_ID);
                GetInstance(SUCCESS, positions, SRSAttributesTest.Fixture, TEST_ID);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_GetLocatorSuffix()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Because Positions don't have any ValidationWarnings, no existing code uses this locator method right now.
                Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
                PrivateObject po = new PrivateObject(component, new PrivateType(typeof(Polygon))); //Required to access protected internal properties
                Assert.AreEqual("/gml:exterior/gml:LinearRing", po.GetFieldOrProperty("LocatorSuffix").ToString());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_BuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Polygon.Builder builder = new Polygon.Builder(component);
                Assert.AreEqual(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_BuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Polygon.Builder builder = new Polygon.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Id = TEST_ID;
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void Summary_Gml_Polygon_BuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Polygon.Builder builder = new Polygon.Builder();
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
                builder.SrsAttributes.SrsName = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
                builder.Positions[0].Coordinates[0].Value = Convert.ToDouble(1);
                builder.Positions[0].Coordinates[1].Value = Convert.ToDouble(1);
                builder.Positions[1].Coordinates[0].Value = Convert.ToDouble(2);
                builder.Positions[1].Coordinates[1].Value = Convert.ToDouble(2);
                builder.Positions[2].Coordinates[0].Value = Convert.ToDouble(3);
                builder.Positions[2].Coordinates[1].Value = Convert.ToDouble(3);
                builder.Positions[3].Coordinates[0].Value = Convert.ToDouble(1);
                builder.Positions[3].Coordinates[1].Value = Convert.ToDouble(1);
                builder.Commit();

                // Skip empty Positions
                builder = new Polygon.Builder();
                builder.Id = TEST_ID;
                Position.Builder emptyBuilder = new Position.Builder();
                Position.Builder fullBuilder1 = new Position.Builder();
                fullBuilder1.Coordinates[0].Value = Convert.ToDouble(0);
                fullBuilder1.Coordinates[1].Value = Convert.ToDouble(0);
                Position.Builder fullBuilder2 = new Position.Builder();
                fullBuilder2.Coordinates[0].Value = Convert.ToDouble(0);
                fullBuilder2.Coordinates[1].Value = Convert.ToDouble(1);
                Position.Builder fullBuilder3 = new Position.Builder();
                fullBuilder3.Coordinates[0].Value = Convert.ToDouble(1);
                fullBuilder3.Coordinates[1].Value = Convert.ToDouble(1);
                builder.Positions.Add(emptyBuilder);
                builder.Positions.Add(fullBuilder1);
                builder.Positions.Add(fullBuilder2);
                builder.Positions.Add(fullBuilder3);
                builder.Positions.Add(fullBuilder1);
                builder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
                builder.Commit();
                Assert.AreEqual(4, builder.Positions.Count());
            }
        }
    }

}