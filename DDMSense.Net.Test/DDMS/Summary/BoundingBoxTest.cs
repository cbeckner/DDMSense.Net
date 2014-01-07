using System;
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
namespace DDMSense.Test.DDMS.Summary {

	
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using Util = DDMSense.Util.Util;
    using DDMSense.DDMS.Summary;
    using DDMSense.DDMS.Summary;
    using System.Xml.Linq;

	/// <summary>
	/// <para> Tests related to ddms:boundingBox elements </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class BoundingBoxTest : AbstractBaseTestCase {

		private const double TEST_WEST = 12.3;
		private const double TEST_EAST = 23.4;
		private const double TEST_SOUTH = 34.5;
		private const double TEST_NORTH = 45.6;

		/// <summary>
		/// Constructor
		/// </summary>
		public BoundingBoxTest() : base("boundingBox.xml") {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static BoundingBox Fixture {
			get {
				try {
					return (new BoundingBox(1.1, 2.2, 3.3, 4.4));
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
		private BoundingBox GetInstance(string message, XElement element) {
			bool expectFailure = !Util.isEmpty(message);
			BoundingBox component = null;
			try {
				component = new BoundingBox(element);
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
		/// <param name="westBL"> the westbound longitude </param>
		/// <param name="eastBL"> the eastbound longitude </param>
		/// <param name="southBL"> the southbound latitude </param>
		/// <param name="northBL"> the northbound latitude </param>
		/// <returns> a valid object </returns>
		private BoundingBox GetInstance(string message, double westBL, double eastBL, double southBL, double northBL) {
			bool expectFailure = !Util.isEmpty(message);
			BoundingBox component = null;
			try {
				component = new BoundingBox(westBL, eastBL, southBL, northBL);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (component);
		}

		/// <summary>
		/// Helper method to get the name of the westbound longitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string WestBLName {
			get {
				return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "westBL" : "WestBL");
			}
		}

		/// <summary>
		/// Helper method to get the name of the eastbound longitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string EastBLName {
			get {
				return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "eastBL" : "EastBL");
			}
		}

		/// <summary>
		/// Helper method to get the name of the southbound latitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string SouthBLName {
			get {
				return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "southBL" : "SouthBL");
			}
		}

		/// <summary>
		/// Helper method to get the name of the northbound latitude element, which changed in DDMS 4.0.1.
		/// </summary>
		private string NorthBLName {
			get {
				return (DDMSVersion.CurrentVersion.isAtLeast("4.0.1") ? "northBL" : "NorthBL");
			}
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String getExpectedOutput(boolean isHTML) throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "boundingBox." + WestBLName, Convert.ToString(TEST_WEST)));
			text.Append(BuildOutput(isHTML, "boundingBox." + EastBLName, Convert.ToString(TEST_EAST)));
			text.Append(BuildOutput(isHTML, "boundingBox." + SouthBLName, Convert.ToString(TEST_SOUTH)));
			text.Append(BuildOutput(isHTML, "boundingBox." + NorthBLName, Convert.ToString(TEST_NORTH)));
			return (text.ToString());
		}

		/// <summary>
		/// Returns the expected XML output for this unit test
		/// </summary>
		/// <param name="preserveFormatting"> if true, include line breaks and tabs. </param>
		private string GetExpectedXMLOutput(bool preserveFormatting) {
			StringBuilder xml = new StringBuilder();
			xml.Append("<ddms:boundingBox ").Append(XmlnsDDMS).Append(">\n\t");
			xml.Append("<ddms:").Append(WestBLName).Append(">").Append(TEST_WEST).Append("</ddms:").Append(WestBLName).Append(">\n\t");
			xml.Append("<ddms:").Append(EastBLName).Append(">").Append(TEST_EAST).Append("</ddms:").Append(EastBLName).Append(">\n\t");
			xml.Append("<ddms:").Append(SouthBLName).Append(">").Append(TEST_SOUTH).Append("</ddms:").Append(SouthBLName).Append(">\n\t");
			xml.Append("<ddms:").Append(NorthBLName).Append(">").Append(TEST_NORTH).Append("</ddms:").Append(NorthBLName).Append(">\n");
			xml.Append("</ddms:boundingBox>");
			return (FormatXml(xml.ToString(), preserveFormatting));
		}

		/// <summary>
		/// Helper method to create a XOM element that can be used to test element constructors
		/// </summary>
		/// <param name="west"> westBL </param>
		/// <param name="east"> eastBL </param>
		/// <param name="south"> southBL </param>
		/// <param name="north"> northBL </param>
		/// <returns> XElement </returns>
		private XElement BuildComponentElement(string west, string east, string south, string north) {
			XElement element = Util.buildDDMSElement(BoundingBox.getName(DDMSVersion.CurrentVersion), null);
			element.appendChild(Util.buildDDMSElement(WestBLName, Convert.ToString(west)));
			element.appendChild(Util.buildDDMSElement(EastBLName, Convert.ToString(east)));
			element.appendChild(Util.buildDDMSElement(SouthBLName, Convert.ToString(south)));
			element.appendChild(Util.buildDDMSElement(NorthBLName, Convert.ToString(north)));
			return (element);
		}

		public virtual void TestNameAndNamespace() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				AssertNameAndNamespace(GetInstance(SUCCESS, GetValidElement(sVersion)), DEFAULT_DDMS_PREFIX, BoundingBox.getName(version));
				GetInstance(WRONG_NAME_MESSAGE, WrongNameElementFixture);
			}
		}

		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GetInstance(SUCCESS, GetValidElement(sVersion));
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				// Missing values
				XElement element = Util.buildDDMSElement(BoundingBox.getName(version), null);
				GetInstance("westbound longitude is required.", element);

				// Not Double
				element = BuildComponentElement("west", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
				GetInstance("westbound longitude is required.", element);

				// Longitude too small
				element = BuildComponentElement("-181", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
				GetInstance("A longitude value must be between", element);

				// Longitude too big
				element = BuildComponentElement("181", Convert.ToString(TEST_EAST), Convert.ToString(TEST_SOUTH), Convert.ToString(TEST_NORTH));
				GetInstance("A longitude value must be between", element);

				// Latitude too small
				element = BuildComponentElement(Convert.ToString(TEST_WEST), Convert.ToString(TEST_EAST), "-91", Convert.ToString(TEST_NORTH));
				GetInstance("A latitude value must be between", element);

				// Latitude too big
				element = BuildComponentElement(Convert.ToString(TEST_WEST), Convert.ToString(TEST_EAST), "91", Convert.ToString(TEST_NORTH));
				GetInstance("A latitude value must be between", element);
			}
		}

		public virtual void TestNorthboundLatitudeValiation() {
			// Issue #65
			GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, TEST_SOUTH, -91);
			GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, TEST_SOUTH, 91);
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// Longitude too small
				GetInstance("A longitude value must be between", -181, TEST_EAST, TEST_SOUTH, TEST_NORTH);

				// Longitude too big
				GetInstance("A longitude value must be between", 181, TEST_EAST, TEST_SOUTH, TEST_NORTH);

				// Latitude too small
				GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, -91, TEST_NORTH);

				// Latitude too big
				GetInstance("A latitude value must be between", TEST_WEST, TEST_EAST, 91, TEST_NORTH);
			}
		}

		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				// No warnings
				BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(0, component.ValidationWarnings.size());
			}
		}

		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				BoundingBox dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
				assertEquals(elementComponent, dataComponent);
				assertEquals(elementComponent.GetHashCode(), dataComponent.GetHashCode());
			}
		}

		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				BoundingBox dataComponent = GetInstance(SUCCESS, 10, TEST_EAST, TEST_SOUTH, TEST_NORTH);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_WEST, 10, TEST_SOUTH, TEST_NORTH);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, 10, TEST_NORTH);
				assertFalse(elementComponent.Equals(dataComponent));

				dataComponent = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, 10);
				assertFalse(elementComponent.Equals(dataComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox elementComponent = GetInstance(SUCCESS, GetValidElement(sVersion));
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementComponent.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());

				component = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
				assertEquals(GetExpectedOutput(true), component.toHTML());
				assertEquals(GetExpectedOutput(false), component.toText());
			}
		}

		public virtual void TestXMLOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(GetExpectedXMLOutput(true), component.toXML());

				component = GetInstance(SUCCESS, TEST_WEST, TEST_EAST, TEST_SOUTH, TEST_NORTH);
				assertEquals(GetExpectedXMLOutput(false), component.toXML());
			}
		}

		public virtual void TestDoubleEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
				assertEquals(component.WestBL, Convert.ToDouble(TEST_WEST));
				assertEquals(component.EastBL, Convert.ToDouble(TEST_EAST));
				assertEquals(component.SouthBL, Convert.ToDouble(TEST_SOUTH));
				assertEquals(component.NorthBL, Convert.ToDouble(TEST_NORTH));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				BoundingBox component = GetInstance(SUCCESS, GetValidElement(sVersion));
				BoundingBox.Builder builder = new BoundingBox.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				BoundingBox.Builder builder = new BoundingBox.Builder();
				assertNull(builder.commit());
				assertTrue(builder.Empty);
				builder.WestBL = TEST_WEST;
				assertFalse(builder.Empty);

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				BoundingBox.Builder builder = new BoundingBox.Builder();
				builder.EastBL = Convert.ToDouble(TEST_EAST);
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "A ddms:boundingBox requires");
				}
				builder.WestBL = Convert.ToDouble(TEST_WEST);
				builder.NorthBL = Convert.ToDouble(TEST_NORTH);
				builder.SouthBL = Convert.ToDouble(TEST_SOUTH);
				builder.commit();
			}
		}
	}

}