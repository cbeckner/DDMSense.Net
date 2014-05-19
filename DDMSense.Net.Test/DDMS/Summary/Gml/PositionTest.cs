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
    /// <para> Tests related to gml:pos elements </para>
    /// 
    /// @author Brian Uri!
    /// @since 0.9.b
    /// </summary>
    public class PositionTest : AbstractBaseTestCase
    {

        public static readonly List<double?> TEST_COORDS = new List<double?>();
        static PositionTest()
        {
            TEST_COORDS.Add(new double?(32.1));
            TEST_COORDS.Add(new double?(40.1));
            TEST_COORDS_2.Add(new double?(42.1));
            TEST_COORDS_2.Add(new double?(40.1));
            TEST_COORDS_3.Add(new double?(42.1));
            TEST_COORDS_3.Add(new double?(50.1));
        }
        protected internal static readonly string TEST_XS_LIST = Util.GetXsList(TEST_COORDS);

        public static readonly List<double?> TEST_COORDS_2 = new List<double?>();
        public static readonly List<double?> TEST_COORDS_3 = new List<double?>();

        /// <summary>
        /// Constructor
        /// </summary>
        public PositionTest()
            : base("position.xml")
        {
        }

        /// <summary>
        /// Returns a fixture object for testing.
        /// </summary>
        public static Position Fixture
        {
            get
            {
                try
                {
                    return (new Position(PositionTest.TEST_COORDS, SRSAttributesTest.Fixture));
                }
                catch (InvalidDDMSException e)
                {
                    Assert.Fail("Could not create fixture: " + e.Message);
                }
                return (null);
            }
        }

        /// <summary>
        /// Returns a fixture object for testing. This list of positions represents a closed polygon.
        /// </summary>
        public static List<Position> FixtureList
        {
            get
            {
                try
                {
                    List<Position> positions = new List<Position>();
                    positions.Add(new Position(TEST_COORDS, SRSAttributesTest.Fixture));
                    positions.Add(new Position(TEST_COORDS_2, SRSAttributesTest.Fixture));
                    positions.Add(new Position(TEST_COORDS_3, SRSAttributesTest.Fixture));
                    positions.Add(new Position(TEST_COORDS, SRSAttributesTest.Fixture));
                    return (positions);
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
        private Position GetInstance(string message, XElement element)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Position component = null;
            try
            {
                component = new Position(element);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorAssert.Failure(expectAssert, e);
                ExpectMessage(e, message);
            }
            return (component);
        }

        /// <summary>
        /// Helper method to create an object which is expected to be valid.
        /// </summary>
        /// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
        /// <param name="coordinates"> the coordinates </param>
        /// <param name="srsAttributes"> the srs attributes (optional) </param>
        /// <returns> a valid object </returns>
        private Position GetInstance(string message, List<double?> coordinates, SRSAttributes srsAttributes)
        {
            bool expectAssert = !String.IsNullOrEmpty(message);
            Position component = null;
            try
            {
                component = new Position(coordinates, srsAttributes);
                CheckConstructorSuccess(expectAssert);
            }
            catch (InvalidDDMSException e)
            {
                CheckConstructorAssert.Failure(expectAssert, e);
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
            text.Append(BuildOutput(isHTML, "pos", TEST_XS_LIST));
            text.Append(SRSAttributesTest.Fixture.GetOutput(isHTML, "pos."));
            return (text.ToString());
        }

        /// <summary>
        /// Returns the expected XML output for this unit test
        /// </summary>
        /// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
        private string GetExpectedXMLOutput(bool preserveFormatting)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<gml:pos ").Append(XmlnsGML).Append(" ");
            xml.Append("srsName=\"").Append(SRSAttributesTest.TEST_SRS_NAME).Append("\" ");
            xml.Append("srsDimension=\"").Append(SRSAttributesTest.TEST_SRS_DIMENSION).Append("\" ");
            xml.Append("axisLabels=\"").Append(Util.GetXsList(SRSAttributesTest.TEST_AXIS_LABELS)).Append("\" ");
            xml.Append("uomLabels=\"").Append(Util.GetXsList(SRSAttributesTest.TEST_UOM_LABELS)).Append("\">");
            xml.Append(TEST_XS_LIST).Append("</gml:pos>");
            return (FormatXml(xml.ToString(), preserveFormatting));
        }

        public virtual void TestNameAndNamespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

                AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Position.GetName(version));
                GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
            }
        }

        public virtual void TestElementConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;

                // All fields
                GetInstance(SUCCESS, GetValidElement(sVersion));

                // No optional fields
                XElement element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST);
                GetInstance(SUCCESS, element);

                // Empty coordinate
                element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, "25.0    26.0");
                GetInstance(SUCCESS, element);

            }
        }

        public virtual void TestDataConstructorValid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // All fields
                GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);

                // No optional fields
                GetInstance(SUCCESS, TEST_COORDS, null);
            }
        }

        public virtual void TestElementConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;
                // Missing coordinates
                XElement element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, null);
                SRSAttributesTest.Fixture.AddTo(element);
                GetInstance("A position must be represented by", element);

                // At least 2 coordinates
                element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, "25.0");
                SRSAttributesTest.Fixture.AddTo(element);
                GetInstance("A position must be represented by", element);

                // No more than 3 coordinates
                element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST + " 25.0 35.0");
                SRSAttributesTest.Fixture.AddTo(element);
                GetInstance("A position must be represented by", element);

                // Each coordinate is a Double
                element = Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, "25.0 Dog");
                SRSAttributesTest.Fixture.AddTo(element);
                GetInstance("coordinate is required.", element);
            }
        }

        public virtual void TestDataConstructorInvalid()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // Missing coordinates
                GetInstance("A position must be represented by", null, SRSAttributesTest.Fixture);

                // At least 2 coordinates
                List<double?> newCoords = new List<double?>();
                newCoords.Add(new double?(12.3));
                GetInstance("A position must be represented by", newCoords, SRSAttributesTest.Fixture);

                // No more than 3 coordinates
                newCoords = new List<double?>();
                newCoords.Add(new double?(12.3));
                newCoords.Add(new double?(12.3));
                newCoords.Add(new double?(12.3));
                newCoords.Add(new double?(12.3));
                GetInstance("A position must be represented by", newCoords, SRSAttributesTest.Fixture);
            }
        }

        [TestMethod]
        public virtual void TestWarnings()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                // No warnings
                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(0, component.ValidationWarnings.Count());
            }
        }

        [TestMethod]
        public virtual void TestConstructorEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Position dataComponent = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
                Assert.Equals(elementComponent, dataComponent);
                Assert.Equals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
            }
        }

        [TestMethod]
        public virtual void TestEqualityWhitespace()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
                string gmlPrefix = PropertyReader.GetPrefix("gml");
                string gmlNamespace = version.GmlNamespace;
                Position position = new Position(Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST));
                Position positionEqual = new Position(Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST));
                Position positionEqualWhitespace = new Position(Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST + "   "));
                Position positionUnequal2d = new Position(Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, "32.1 40.0"));
                Position positionUnequal3d = new Position(Util.BuildElement(gmlPrefix, Position.GetName(version), gmlNamespace, TEST_XS_LIST + " 40.0"));
                Assert.Equals(position, positionEqual);
                Assert.Equals(position, positionEqualWhitespace);
                Assert.IsFalse(position.Equals(positionUnequal2d));
                Assert.IsFalse(position.Equals(positionUnequal3d));
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityDifferentValues()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Position dataComponent = GetInstance(SUCCESS, TEST_COORDS, null);
                Assert.IsFalse(elementComponent.Equals(dataComponent));

                List<double?> newCoords = new List<double?>(TEST_COORDS);
                newCoords.Add(new double?(100.0));
                dataComponent = GetInstance(SUCCESS, newCoords, SRSAttributesTest.Fixture);
                Assert.IsFalse(elementComponent.Equals(dataComponent));
            }
        }

        [TestMethod]
        public virtual void TestConstructorInequalityWrongClass()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
                Rights wrongComponent = new Rights(true, true, true);
                Assert.IsFalse(elementComponent.Equals(wrongComponent));
            }
        }

        [TestMethod]
        public virtual void TestHTMLTextOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());

                component = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
                Assert.Equals(GetExpectedOutput(true), component.ToHTML());
                Assert.Equals(GetExpectedOutput(false), component.ToText());
            }
        }

        [TestMethod]
        public virtual void TestXMLOutput()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Assert.Equals(GetExpectedXMLOutput(true), component.ToXML());

                component = GetInstance(SUCCESS, TEST_COORDS, SRSAttributesTest.Fixture);
                Assert.Equals(GetExpectedXMLOutput(false), component.ToXML());
            }
        }

        [TestMethod]
        public virtual void TestBuilderEquality()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Position component = GetInstance(SUCCESS, GetValidElement(sVersion));
                Position.Builder builder = new Position.Builder(component);
                Assert.Equals(component, builder.Commit());
            }
        }

        [TestMethod]
        public virtual void TestBuilderIsEmpty()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Position.Builder builder = new Position.Builder();
                Assert.IsNull(builder.Commit());
                Assert.IsTrue(builder.Empty);
                builder.Coordinates[0].Value = Convert.ToDouble(0);
                Assert.IsFalse(builder.Empty);
            }
        }

        [TestMethod]
        public virtual void TestBuilderValidation()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);

                Position.Builder builder = new Position.Builder();
                builder.Coordinates[0].Value = Convert.ToDouble(0);
                try
                {
                    builder.Commit();
                    Assert.Fail("Builder allowed invalid data.");
                }
                catch (InvalidDDMSException e)
                {
                    ExpectMessage(e, "A position must be represented by");
                }
                builder.Coordinates[1].Value = Convert.ToDouble(0);
                builder.Commit();

                // Skip empty Coordinates
                builder = new Position.Builder();
                builder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
                builder.Coordinates[0].Value = null;
                builder.Coordinates[1].Value = Convert.ToDouble(0);
                builder.Coordinates[2].Value = Convert.ToDouble(1);
                builder.Commit();
                Assert.Equals(2, builder.Coordinates.Count());
            }
        }

        [TestMethod]
        public virtual void TestBuilderLazyList()
        {
            foreach (string sVersion in SupportedVersions)
            {
                DDMSVersion.SetCurrentVersion(sVersion);
                Position.Builder builder = new Position.Builder();
                Assert.IsNotNull(builder.Coordinates[1]);
            }
        }
    }
}