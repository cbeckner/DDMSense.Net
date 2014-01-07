using System;
using System.Collections.Generic;
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
namespace DDMSense.Test.DDMS.Summary.Gml {


	
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using PropertyReader = DDMSense.Util.PropertyReader;
	using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary.Gml;
    using System.Xml.Linq;

	/// <summary>
	/// <para> Tests related to gml:Polygon elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.0
	/// </summary>
	public class PolygonTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public PolygonTest() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public PolygonTest() : base("polygon.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<Polygon> FixtureList {
			get {
				try {
					IList<Polygon> polygons = new List<Polygon>();
					polygons.Add(new Polygon(PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID));
					return (polygons);
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
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
		private Polygon GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			Polygon component = null;
			try {
				component = new Polygon(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
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
		private Polygon GetInstance(string message, IList<Position> positions, SRSAttributes srsAttributes, string id) {
			bool expectFailure = !Util.isEmpty(message);
			Polygon component = null;
			try {
				component = new Polygon(positions, srsAttributes, id);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
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
		private XElement WrapPositions(IList<Position> positions) {
			string gmlNamespace = DDMSVersion.CurrentVersion.GmlNamespace;
			XElement ringElement = Util.buildElement(PropertyReader.getPrefix("gml"), "LinearRing", gmlNamespace, null);
			foreach (Position pos in positions) {
				ringElement.appendChild(pos.XOMElementCopy);
			}
			XElement extElement = Util.buildElement(PropertyReader.getPrefix("gml"), "exterior", gmlNamespace, null);
			extElement.appendChild(ringElement);
			return (extElement);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "Polygon.id", TEST_ID));
			text.Append(SRSAttributesTest.Fixture.getOutput(isHTML, "Polygon."));
			foreach (Position pos in PositionTest.FixtureList) {
				text.Append(pos.getOutput(isHTML, "Polygon.", ""));
			}
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedXMLOutput(boolean preserveFormatting) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedXMLOutput(bool preserveFormatting) {
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
			xml.Append(Util.getXsList(PositionTest.TEST_COORDS)).Append("</gml:pos>\n\t\t\t");
			xml.Append("<gml:pos ");
			xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
			xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
			xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
			xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
			xml.Append(Util.getXsList(PositionTest.TEST_COORDS_2)).Append("</gml:pos>\n\t\t\t");
			xml.Append("<gml:pos ");
			xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
			xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
			xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
			xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
			xml.Append(Util.getXsList(PositionTest.TEST_COORDS_3)).Append("</gml:pos>\n\t\t\t");
			xml.Append("<gml:pos ");
			xml.Append("srsName=\"").Append(attr.SrsName).Append("\" ");
			xml.Append("srsDimension=\"").Append(attr.SrsDimension).Append("\" ");
			xml.Append("axisLabels=\"").Append(attr.AxisLabelsAsXsList).Append("\" ");
			xml.Append("uomLabels=\"").Append(attr.UomLabelsAsXsList).Append("\">");
			xml.Append(Util.getXsList(PositionTest.TEST_COORDS)).Append("</gml:pos>\n\t\t");
			xml.Append("</gml:LinearRing>\n\t");
			xml.Append("</gml:exterior>\n");
			xml.Append("</gml:Polygon>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Polygon.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string gmlPrefix = PropertyReader.getPrefix("gml");
				string gmlNamespace = version.GmlNamespace;

				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				XElement element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				Util.addAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, SRSAttributesTest.Fixture.SrsName);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance(SUCCESS, element);

				// First position matches last position but has extra whitespace.
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				IList<Position> newPositions = new List<Position>(PositionTest.FixtureList);
				newPositions.Add(PositionTest.FixtureList[1]);
				XElement posElement = Util.buildElement(gmlPrefix, Position.getName(version), gmlNamespace, "32.1         40.1");
				SRSAttributesTest.Fixture.addTo(posElement);
				Position positionWhitespace = new Position(posElement);
				newPositions.Add(positionWhitespace);
				element.appendChild(WrapPositions(newPositions));
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string gmlPrefix = PropertyReader.getPrefix("gml");
				string gmlNamespace = version.GmlNamespace;
				// Missing SRS Name
				XElement element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("srsName is required.", element);

				// Empty SRS Name
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("srsName is required.", element);

				// Polygon SRS Name doesn't match pos SRS Name
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("The srsName of each position", element);

				// Missing ID
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("id is required.", element);

				// Empty ID
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, "");
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("id is required.", element);

				// ID not NCName
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, "1TEST");
				element.appendChild(WrapPositions(PositionTest.FixtureList));
				GetInstance("\"1TEST\" is not a valid NCName.", element);

				// Missing Positions
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(WrapPositions(new List<Position>()));
				GetInstance("At least 4 positions are required", element);

				// First position doesn't match last position.
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				IList<Position> newPositions = new List<Position>(PositionTest.FixtureList);
				newPositions.Add(PositionTest.FixtureList[1]);
				element.appendChild(WrapPositions(newPositions));
				GetInstance("The first and last position", element);

				// Not enough positions
				element = Util.buildElement(gmlPrefix, Polygon.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				newPositions = new List<Position>();
				newPositions.Add(PositionTest.FixtureList[0]);
				element.appendChild(WrapPositions(newPositions));
				GetInstance("At least 4 positions are required", element);

				// Tests on shared attributes are done in the PositionTest.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
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
				IList<Position> newPositions = new List<Position>(PositionTest.FixtureList);
				newPositions.Add(PositionTest.FixtureList[1]);
				GetInstance("The first and last position", newPositions, SRSAttributesTest.Fixture, TEST_ID);

				// Not enough positions
				newPositions = new List<Position>();
				newPositions.Add(PositionTest.FixtureList[0]);
				GetInstance("At least 4 positions are required", newPositions, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Polygon dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SRSAttributes attr = new SRSAttributes(SRSAttributesTest.Fixture.SrsName, Convert.ToInt32(11), SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
				Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Polygon dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, attr, TEST_ID);
				assertFalse(elementComponent.Equals(dataComponent));

				IList<Position> newPositions = new List<Position>(PositionTest.FixtureList);
				newPositions.Add(PositionTest.FixtureList[1]);
				newPositions.Add(PositionTest.FixtureList[0]);
				dataComponent = GetInstance(SUCCESS, newPositions, SRSAttributesTest.Fixture, TEST_ID);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Polygon elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, PositionTest.FixtureList, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testPositionReuse() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestPositionReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IList<Position> positions = PositionTest.FixtureList;
				GetInstance(SUCCESS, positions, SRSAttributesTest.Fixture, TEST_ID);
				GetInstance(SUCCESS, positions, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

		public virtual void TestGetLocatorSuffix() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Because Positions don't have any ValidationWarnings, no existing code uses this locator method right now.
				Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals("/gml:exterior/gml:LinearRing", component.LocatorSuffix);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Polygon component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Polygon.Builder builder = new Polygon.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Polygon.Builder builder = new Polygon.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Id = TEST_ID;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Polygon.Builder builder = new Polygon.Builder();
				builder.Id = TEST_ID;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "srsName is required.");
				}
				builder.SrsAttributes.SrsName = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
				builder.Positions.get(0).Coordinates.get(0).Value = Convert.ToDouble(1);
				builder.Positions.get(0).Coordinates.get(1).Value = Convert.ToDouble(1);
				builder.Positions.get(1).Coordinates.get(0).Value = Convert.ToDouble(2);
				builder.Positions.get(1).Coordinates.get(1).Value = Convert.ToDouble(2);
				builder.Positions.get(2).Coordinates.get(0).Value = Convert.ToDouble(3);
				builder.Positions.get(2).Coordinates.get(1).Value = Convert.ToDouble(3);
				builder.Positions.get(3).Coordinates.get(0).Value = Convert.ToDouble(1);
				builder.Positions.get(3).Coordinates.get(1).Value = Convert.ToDouble(1);
				builder.commit();

				// Skip empty Positions
				builder = new Polygon.Builder();
				builder.Id = TEST_ID;
				Position.Builder emptyBuilder = new Position.Builder();
				Position.Builder fullBuilder1 = new Position.Builder();
				fullBuilder1.Coordinates.get(0).Value = Convert.ToDouble(0);
				fullBuilder1.Coordinates.get(1).Value = Convert.ToDouble(0);
				Position.Builder fullBuilder2 = new Position.Builder();
				fullBuilder2.Coordinates.get(0).Value = Convert.ToDouble(0);
				fullBuilder2.Coordinates.get(1).Value = Convert.ToDouble(1);
				Position.Builder fullBuilder3 = new Position.Builder();
				fullBuilder3.Coordinates.get(0).Value = Convert.ToDouble(1);
				fullBuilder3.Coordinates.get(1).Value = Convert.ToDouble(1);
				builder.Positions.add(emptyBuilder);
				builder.Positions.add(fullBuilder1);
				builder.Positions.add(fullBuilder2);
				builder.Positions.add(fullBuilder3);
				builder.Positions.add(fullBuilder1);
				builder.SrsAttributes = new SRSAttributes.Builder(SRSAttributesTest.Fixture);
				assertEquals(4, builder.commit().Positions.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Polygon.Builder builder = new Polygon.Builder();
				assertNotNull(builder.Positions.get(1));
			}
		}
	}

}