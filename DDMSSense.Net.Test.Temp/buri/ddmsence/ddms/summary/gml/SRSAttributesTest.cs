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
	/// <para> Tests related to the SRS attributes on gml: elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class SRSAttributesTest : AbstractBaseTestCase {

		protected internal const string TEST_SRS_NAME = "http://metadata.dod.mil/mdr/ns/GSIP/crs/WGS84E_2D";
		protected internal const int? TEST_SRS_DIMENSION = 10;
		protected internal static readonly IList<string> TEST_AXIS_LABELS = new List<string>();
		static SRSAttributesTest() {
			TEST_AXIS_LABELS.Add("A");
			TEST_AXIS_LABELS.Add("B");
			TEST_AXIS_LABELS.Add("C");
			TEST_UOM_LABELS.Add("Meter");
			TEST_UOM_LABELS.Add("Meter");
			TEST_UOM_LABELS.Add("Meter");
		}
		protected internal static readonly IList<string> TEST_UOM_LABELS = new List<string>();

		/// <summary>
		/// Constructor
		/// </summary>
		public SRSAttributesTest() : base(null) {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static SRSAttributes Fixture {
			get {
				try {
					return (new SRSAttributes(TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS));
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
		private SRSAttributes GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			SRSAttributes attributes = null;
			try {
				attributes = new SRSAttributes(element);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (attributes);
		}

		/// <summary>
		/// Helper method to create an object which is expected to be valid.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="srsName"> the srsName (optional) </param>
		/// <param name="srsDimension"> the srsDimension (optional) </param>
		/// <param name="axisLabels"> the axis labels (optional, but should be omitted if no srsName is set) </param>
		/// <param name="uomLabels"> the labels for UOM (required when axisLabels is set) </param>
		/// <returns> a valid object </returns>
		private SRSAttributes GetInstance(string message, string srsName, int? srsDimension, IList<string> axisLabels, IList<string> uomLabels) {
			bool expectFailure = !Util.isEmpty(message);
			SRSAttributes attributes = null;
			try {
				attributes = new SRSAttributes(srsName, srsDimension, axisLabels, uomLabels);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (attributes);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "srsName", TEST_SRS_NAME));
			text.Append(BuildOutput(isHTML, "srsDimension", Convert.ToString(TEST_SRS_DIMENSION)));
			text.Append(BuildOutput(isHTML, "axisLabels", Util.getXsList(TEST_AXIS_LABELS)));
			text.Append(BuildOutput(isHTML, "uomLabels", Util.getXsList(TEST_UOM_LABELS)));
			return (text.ToString());
		}

		/// <summary>
		/// Helper method to add srs attributes to a XOM element. The element is not validated.
		/// </summary>
		/// <param name="element"> element </param>
		/// <param name="srsName"> the srsName (optional) </param>
		/// <param name="srsDimension"> the srsDimension (optional) </param>
		/// <param name="axisLabels"> the axis labels (optional, but should be omitted if no srsName is set) </param>
		/// <param name="uomLabels"> the labels for UOM (required when axisLabels is set) </param>
		private void AddAttributes(Element element, string srsName, int? srsDimension, string axisLabels, string uomLabels) {
			Util.addAttribute(element, SRSAttributes.NO_PREFIX, "srsName", SRSAttributes.NO_NAMESPACE, srsName);
			if (srsDimension != null) {
				Util.addAttribute(element, SRSAttributes.NO_PREFIX, "srsDimension", SRSAttributes.NO_NAMESPACE, Convert.ToString(srsDimension));
			}
			Util.addAttribute(element, SRSAttributes.NO_PREFIX, "axisLabels", SRSAttributes.NO_NAMESPACE, axisLabels);
			Util.addAttribute(element, SRSAttributes.NO_PREFIX, "uomLabels", SRSAttributes.NO_NAMESPACE, uomLabels);
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// All fields
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				GetInstance(SUCCESS, element);

				// No optional fields
				element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// All fields
				GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

				// No optional fields
				GetInstance(SUCCESS, null, null, null, null);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// srsName not a URI
				Element element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, INVALID_URI, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				GetInstance("Invalid URI", element);

				// axisLabels without srsName
				element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, null, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				GetInstance("The axisLabels attribute can only be used", element);

				// uomLabels without axisLabels
				element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, null, Util.getXsList(TEST_UOM_LABELS));
				GetInstance("The uomLabels attribute can only be used", element);

				// Non-NCNames in axisLabels
				IList<string> newLabels = new List<string>(TEST_AXIS_LABELS);
				newLabels.Add("1TEST");
				element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(newLabels), Util.getXsList(TEST_UOM_LABELS));
				GetInstance("\"1TEST\" is not a valid NCName.", element);

				// Non-NCNames in uomLabels
				newLabels = new List<string>(TEST_UOM_LABELS);
				newLabels.Add("TEST:TEST");
				element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(newLabels));
				GetInstance("\"TEST:TEST\" is not a valid NCName.", element);

				// Dimension is a positive integer
				element = Util.buildDDMSElement(Position.getName(version), null);
				AddAttributes(element, TEST_SRS_NAME, Convert.ToInt32(-1), null, Util.getXsList(TEST_UOM_LABELS));
				GetInstance("The srsDimension must be a positive integer.", element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// srsName not a URI
				GetInstance("Invalid URI", INVALID_URI, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

				// axisLabels without srsName
				GetInstance("The axisLabels attribute can only be used", null, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

				// uomLabels without axisLabels
				GetInstance("The uomLabels attribute can only be used", TEST_SRS_NAME, TEST_SRS_DIMENSION, null, TEST_UOM_LABELS);

				// Non-NCNames in axisLabels
				IList<string> newLabels = new List<string>(TEST_AXIS_LABELS);
				newLabels.Add("TEST:TEST");
				GetInstance("\"TEST:TEST\" is not a valid NCName.", TEST_SRS_NAME, TEST_SRS_DIMENSION, newLabels, TEST_UOM_LABELS);

				// Non-NCNames in uomLabels
				newLabels = new List<string>(TEST_UOM_LABELS);
				newLabels.Add("TEST:TEST");
				GetInstance("\"TEST:TEST\" is not a valid NCName.", TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, newLabels);

				// Dimension is a positive integer
				GetInstance("The srsDimension must be a positive integer.", TEST_SRS_NAME, Convert.ToInt32(-1), TEST_AXIS_LABELS, TEST_UOM_LABELS);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// No warnings
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				SRSAttributes component = GetInstance(SUCCESS, element);
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				SRSAttributes elementAttributes = GetInstance(SUCCESS, element);
				SRSAttributes dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);

				assertEquals(elementAttributes, dataAttributes);
				assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				SRSAttributes elementAttributes = GetInstance(SUCCESS, element);
				SRSAttributes dataAttributes = GetInstance(SUCCESS, DIFFERENT_VALUE, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);
				assertFalse(elementAttributes.Equals(dataAttributes));

				dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, null, TEST_AXIS_LABELS, TEST_UOM_LABELS);
				assertFalse(elementAttributes.Equals(dataAttributes));

				IList<string> newLabels = new List<string>(TEST_AXIS_LABELS);
				newLabels.Add("NewLabel");
				dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, newLabels, TEST_UOM_LABELS);
				assertFalse(elementAttributes.Equals(dataAttributes));

				dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, null);
				assertFalse(elementAttributes.Equals(dataAttributes));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				SRSAttributes attributes = new SRSAttributes(element);
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(attributes.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), Position.getName(version), version.GmlNamespace, null);
				AddAttributes(element, TEST_SRS_NAME, TEST_SRS_DIMENSION, Util.getXsList(TEST_AXIS_LABELS), Util.getXsList(TEST_UOM_LABELS));
				SRSAttributes attributes = new SRSAttributes(element);
				assertEquals(GetExpectedOutput(true), attributes.getOutput(true, ""));
				assertEquals(GetExpectedOutput(false), attributes.getOutput(false, ""));

				SRSAttributes dataAttributes = GetInstance(SUCCESS, TEST_SRS_NAME, TEST_SRS_DIMENSION, TEST_AXIS_LABELS, TEST_UOM_LABELS);
				assertEquals(GetExpectedOutput(true), dataAttributes.getOutput(true, ""));
				assertEquals(GetExpectedOutput(false), dataAttributes.getOutput(false, ""));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testAddTo() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestAddTo() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SRSAttributes component = Fixture;

				Element element = Util.buildElement(PropertyReader.getPrefix("gml"), "sample", version.GmlNamespace, null);
				component.addTo(element);
				SRSAttributes output = new SRSAttributes(element);
				assertEquals(component, output);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetNonNull() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetNonNull() {
			SRSAttributes component = new SRSAttributes(null, null, null, null);
			SRSAttributes output = SRSAttributes.getNonNullInstance(null);
			assertEquals(component, output);

			output = SRSAttributes.getNonNullInstance(Fixture);
			assertEquals(Fixture, output);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionAttributes() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWrongVersionAttributes() {
			DDMSVersion.CurrentVersion = "3.0";
			SRSAttributes attr = Fixture;
			DDMSVersion.CurrentVersion = "2.0";
			try {
				new Position(PositionTest.TEST_COORDS, attr);
				fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "These attributes cannot decorate");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SRSAttributes component = Fixture;
				SRSAttributes.Builder builder = new SRSAttributes.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SRSAttributes.Builder builder = new SRSAttributes.Builder();
				assertNotNull(builder.commit());
				assertTrue(builder.Empty);
				builder.SrsName = TEST_SRS_NAME;
				assertFalse(builder.Empty);

				builder = new SRSAttributes.Builder();
				builder.SrsDimension = TEST_SRS_DIMENSION;
				assertFalse(builder.Empty);

				builder = new SRSAttributes.Builder();
				builder.UomLabels.add(null);
				builder.UomLabels.add("label");
				assertFalse(builder.Empty);

				builder = new SRSAttributes.Builder();
				builder.AxisLabels.add(null);
				builder.AxisLabels.add("label");
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SRSAttributes.Builder builder = new SRSAttributes.Builder();
				builder.SrsDimension = Convert.ToInt32(-1);
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The srsDimension must be a positive integer.");
				}
				builder.SrsDimension = Convert.ToInt32(1);
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SRSAttributes.Builder builder = new SRSAttributes.Builder();
				assertNotNull(builder.UomLabels.get(1));
				assertNotNull(builder.AxisLabels.get(1));
			}
		}
	}

}