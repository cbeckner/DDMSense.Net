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
namespace buri.ddmsence.ddms.summary.gml {


	using Element = nu.xom.Element;
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to gml:Point elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class PointTest : AbstractBaseTestCase {

		/// <summary>
		/// Constructor
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public PointTest() throws buri.ddmsence.ddms.InvalidDDMSException
		public PointTest() : base("point.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static IList<Point> FixtureList {
			get {
				try {
					IList<Point> points = new List<Point>();
					points.Add(new Point(new Position(PositionTest.TEST_COORDS, null), SRSAttributesTest.Fixture, TEST_ID));
					return (points);
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
		private Point GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			Point component = null;
			try {
				component = new Point(element);
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
		/// <param name="position"> the position (required) </param>
		/// <param name="srsAttributes"> the srs attributes (required) </param>
		/// <param name="id"> the id (required) </param>
		/// <returns> a valid object </returns>
		private Point GetInstance(string message, Position position, SRSAttributes srsAttributes, string id) {
			bool expectFailure = !Util.isEmpty(message);
			Point component = null;
			try {
				component = new Point(position, srsAttributes, id);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "Point.id", TEST_ID));
			text.Append(SRSAttributesTest.Fixture.getOutput(isHTML, "Point."));
			text.Append(PositionTest.Fixture.getOutput(isHTML, "Point.", ""));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedXMLOutput(boolean preserveFormatting) throws buri.ddmsence.ddms.InvalidDDMSException
		private string GetExpectedXMLOutput(bool preserveFormatting) {
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testNameAndNamespace() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_GML_PREFIX, Point.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// All fields
				GetInstance(SUCCESS, GetValidElement(sVersion));

				// No optional fields
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Point.getName(version), version.GmlNamespace, null);
				Util.addAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, SRSAttributesTest.Fixture.SrsName);
				Util.addAttribute(element, PropertyReader.getPrefix("gml"), "id", version.GmlNamespace, TEST_ID);
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance(SUCCESS, element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string gmlPrefix = PropertyReader.getPrefix("gml");
				string gmlNamespace = version.GmlNamespace;

				// Missing SRS Name
				Element element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				SRSAttributes attr = new SRSAttributes(null, SRSAttributesTest.Fixture.SrsDimension, null, null);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("srsName is required.", element);

				// Empty SRS Name
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				attr = new SRSAttributes("", SRSAttributesTest.Fixture.SrsDimension, null, null);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("srsName is required.", element);

				// Point SRS Name doesn't match pos SRS Name
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				attr = new SRSAttributes(DIFFERENT_VALUE, SRSAttributesTest.Fixture.SrsDimension, SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
				attr.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("The srsName of the position must match", element);

				// Missing ID
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("id is required.", element);

				// Empty ID
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, "");
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("id is required.", element);

				// ID not NCName
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, "1TEST");
				element.appendChild(PositionTest.Fixture.XOMElementCopy);
				GetInstance("\"1TEST\" is not a valid NCName.", element);

				// Missing position
				element = Util.buildElement(gmlPrefix, Point.getName(version), gmlNamespace, null);
				SRSAttributesTest.Fixture.addTo(element);
				Util.addAttribute(element, gmlPrefix, "id", gmlNamespace, TEST_ID);
				GetInstance("position is required.", element);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDataConstructorInvalid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
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
				GetInstance("\"1TEST\" is not a valid NCName.", PositionTest.Fixture, SRSAttributesTest.Fixture, "1TEST");

				// Missing position
				GetInstance("position is required.", null, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Point dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SRSAttributes attr = new SRSAttributes(SRSAttributesTest.Fixture.SrsName, Convert.ToInt32(11), SRSAttributesTest.Fixture.AxisLabels, SRSAttributesTest.Fixture.UomLabels);
				Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Point dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, attr, TEST_ID);
				assertFalse(elementComponent.Equals(dataComponent));

				IList<double?> newCoords = new List<double?>();
				newCoords.Add(new double?(56.0));
				newCoords.Add(new double?(150.0));
				Position newPosition = new Position(newCoords, SRSAttributesTest.Fixture);

				dataComponent = GetInstance(SUCCESS, newPosition, SRSAttributesTest.Fixture, TEST_ID);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, DIFFERENT_VALUE);
				assertFalse(elementComponent.Equals(dataComponent));

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Point elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testXMLOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, PositionTest.Fixture, SRSAttributesTest.Fixture, TEST_ID);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testPositionReuse() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestPositionReuse() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				Position pos = PositionTest.Fixture;
				GetInstance(SUCCESS, pos, SRSAttributesTest.Fixture, TEST_ID);
				GetInstance(SUCCESS, pos, SRSAttributesTest.Fixture, TEST_ID);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Point component = GetInstance(SUCCESS, GetValidElement(sVersion));
				Point.Builder builder = new Point.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Point.Builder builder = new Point.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.Id = TEST_ID;
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				Point.Builder builder = new Point.Builder();
				builder.Id = TEST_ID;
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "srsName is required.");
				}
				builder.Position.Coordinates.get(0).Value = new double?(32.1);
				builder.Position.Coordinates.get(1).Value = new double?(42.1);
				builder.Id = "IDValue";
				builder.SrsAttributes.SrsName = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
				builder.commit();
			}
		}
	}

}