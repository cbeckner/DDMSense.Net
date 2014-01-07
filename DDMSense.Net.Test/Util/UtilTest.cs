using System.Collections.Generic;

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
namespace DDMSense.Test.Util {


	using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;

	/// <summary>
	/// A collection of Util tests.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class UtilTest : AbstractBaseTestCase {

		protected internal static readonly string TEST_NAMESPACE = DDMSVersion.CurrentVersion.Namespace;

		public UtilTest() : base(null) {
		}

		public virtual void TestGetNonNullStringNull() {
			assertEquals("", Util.getNonNullString(null));
		}

		public virtual void TestGetNonNullStringValue() {
			assertEquals("Test", Util.getNonNullString("Test"));
		}

		public virtual void TestGetXsList() {
			IList<string> list = new List<string>();
			list.Add("Test");
			list.Add("Dog");
			assertEquals("Test Dog", Util.getXsList(list));
		}

		public virtual void TestGetXsListNull() {
			assertEquals("", Util.getXsList(null));
		}

		public virtual void TestBooleanHashCodeTrue() {
			assertEquals(1, Util.booleanHashCode(true));
		}

		public virtual void TestBooleanHashCodeFalse() {
			assertEquals(0, Util.booleanHashCode(false));
		}

		public virtual void TestIsEmptyNull() {
			assertTrue(Util.isEmpty(null));
		}

		public virtual void TestIsEmptyWhitespace() {
			assertTrue(Util.isEmpty(" "));
		}

		public virtual void TestIsEmptyEmptyString() {
			assertTrue(Util.isEmpty(""));
		}

		public virtual void TestIsEmptyNot() {
			assertFalse(Util.isEmpty("DDMSence"));
		}

		public virtual void TestContainsEmptyStringFalse() {
			assertFalse(Util.containsOnlyEmptyValues(null));
			IList<string> list = new List<string>();
			list.Add("dog");
			list.Add("");
			list.Add(null);
			assertFalse(Util.containsOnlyEmptyValues(list));
		}

		public virtual void TestContainsEmptyStringTrue() {
			IList<string> list = new List<string>();
			assertTrue(Util.containsOnlyEmptyValues(list));
			list.Add("");
			assertTrue(Util.containsOnlyEmptyValues(list));
			list.Clear();
			list.Add(null);
			assertTrue(Util.containsOnlyEmptyValues(list));
		}

		public virtual void TestGetFirstDDMSChildValueNullParent() {
			try {
				Util.getFirstDDMSChildValue(null, "test");
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

		public virtual void TestGetFirstDDMSChildValueNullChild() {
			try {
				Util.getFirstDDMSChildValue(Util.buildDDMSElement("test", null), null);
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

		public virtual void TestGetFirstDDMSChildValueWrongNamespace() {
			XElement element = Util.buildElement("ddmsence", "test", "http://ddmsence.urizone.net/", null);
			try {
				Util.getFirstDDMSChildValue(element, "child");
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "This method should only be called");
			}
		}

		public virtual void TestGetFirstDDMSChildValueIndependentOfCurrentVersion() {
			DDMSVersion.CurrentVersion = "3.0";
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("child", "childText1"));
			element.appendChild(Util.buildDDMSElement("child", "childText2"));
			DDMSVersion.CurrentVersion = "2.0";
			string value = Util.getFirstDDMSChildValue(element, "child");
			assertEquals("childText1", value);
		}

		public virtual void TestGetFirstDDMSChildValueNoValue() {
			string value = Util.getFirstDDMSChildValue(Util.buildDDMSElement("test", null), "unknown");
			assertEquals("", value);
		}

		public virtual void TestGetFirstDDMSChildValueWithValue() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("child", "childText1"));
			element.appendChild(Util.buildDDMSElement("child", "childText2"));
			string value = Util.getFirstDDMSChildValue(element, "child");
			assertEquals("childText1", value);
		}

		public virtual void TestGetDDMSChildValuesNullParent() {
			try {
				Util.getDDMSChildValues(null, "test");
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

		public virtual void TestGetDDMSChildValuesNullChild() {
			try {
				Util.getDDMSChildValues(Util.buildDDMSElement("test", null), null);
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

		public virtual void TestGetDDMSChildValuesWrongNamespace() {
			XElement element = Util.buildElement("ddmsence", "test", "http://ddmsence.urizone.net/", null);
			element.appendChild(Util.buildDDMSElement("child", "child1"));
			element.appendChild(Util.buildDDMSElement("child", "child2"));
			try {
				Util.getDDMSChildValues(element, "child");
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "This method should only be called");
			}
		}

		public virtual void TestGetDDMSChildValuesIndependentOfCurrentVersion() {
			DDMSVersion.CurrentVersion = "3.0";
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("child", "child1"));
			element.appendChild(Util.buildDDMSElement("child", "child2"));
			DDMSVersion.CurrentVersion = "2.0";
			IList<string> values = Util.getDDMSChildValues(element, "child");
			assertNotNull(values);
			assertEquals(2, values.Count);
			assertEquals("child1", values[0]);
			assertEquals("child2", values[1]);
		}

		public virtual void TestGetDDMSChildValuesNoValues() {
			XElement element = Util.buildDDMSElement("test", null);
			IList<string> values = Util.getDDMSChildValues(element, "unknown");
			assertNotNull(values);
			assertTrue(values.Count == 0);
		}

		public virtual void TestGetDDMSChildValuesEmptyValues() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("child", ""));
			IList<string> values = Util.getDDMSChildValues(element, "child");
			assertNotNull(values);
			assertEquals(1, values.Count);
			assertEquals("", values[0]);
		}

		public virtual void TestGetDDMSChildValuesWithValues() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("child", "child1"));
			element.appendChild(Util.buildDDMSElement("child", "child2"));
			IList<string> values = Util.getDDMSChildValues(element, "child");
			assertNotNull(values);
			assertEquals(2, values.Count);
			assertEquals("child1", values[0]);
			assertEquals("child2", values[1]);
		}

		public virtual void TestRequireDDMSValueNull() {
			try {
				Util.requireDDMSValue("description", null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "description is required.");
			}
		}

		public virtual void TestRequireDDMSValueEmpty() {
			try {
				Util.requireDDMSValue("description", "");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "description is required.");
			}
		}

		public virtual void TestRequireDDMSValueNotEmpty() {
			try {
				Util.requireDDMSValue("description", "DDMSence");
			} catch (InvalidDDMSException) {
				fail("Did not allow valid data.");
			}
		}

		public virtual void TestRequireDDMSDateFormatSuccess() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				try {
				   Util.requireDDMSDateFormat("2012", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01-01", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01-01T01:02:03Z", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01-01T01:02:03+05:00", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01-01T01:02:03.4Z", version.Namespace);
				   Util.requireDDMSDateFormat("2012-01-01T01:02:03.4+05:00", version.Namespace);
					if (version.isAtLeast("4.1")) {
						Util.requireDDMSDateFormat("2012-01-01T01:02Z", version.Namespace);
						Util.requireDDMSDateFormat("2012-01-01T01:02+05:00", version.Namespace);
					}
				} catch (InvalidDDMSException) {
					fail("Did not allow valid data.");
				}
			}
		}

		public virtual void TestRequireDDMSDateFormatFailure() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// Another XSD date type
				try {
				   Util.requireDDMSDateFormat("---31", version.Namespace);
				   fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The date datatype must be");
				}

				// Not a date
				try {
				   Util.requireDDMSDateFormat("baboon", version.Namespace);
				   fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The date datatype must be");
				}

				// ddms:DateHourMinType (specifying 4.0.1 here, since you can't tell if it's 4.0.1 or 4.1 by XML namespace)
				if (!version.isAtLeast("4.0.1")) {
					try {
					   Util.requireDDMSDateFormat("2012-01-01T01:02Z", version.Namespace);
					   fail("Allowed invalid data.");
					} catch (InvalidDDMSException e) {
						ExpectMessage(e, "The date datatype must be");
					}
					try {
					   Util.requireDDMSDateFormat("2012-01-01T01:02+05:00", version.Namespace);
					   fail("Allowed invalid data.");
					} catch (InvalidDDMSException e) {
						ExpectMessage(e, "The date datatype must be");
					}
				}
			}
		}

		public virtual void TestRequireDDMSQNameSuccess() {
			try {
				XElement element = Util.buildDDMSElement("name", null);
				Util.requireDDMSQName(element, "name");
			} catch (InvalidDDMSException) {
				fail("Did not allow valid data.");
			}
		}

		public virtual void TestRequireDDMSQNameFailure() {
			// Bad URI
			try {
				XElement element = Util.buildElement("ic", "name", "urn:us:gov:ic:ism", null);
				Util.requireDDMSQName(element, "name");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}

			// Bad Name
			try {
				XElement element = Util.buildDDMSElement("name", null);
				Util.requireDDMSQName(element, "newName");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

		public virtual void TestRequireQNameSuccess() {
			try {
				XElement element = Util.buildDDMSElement("name", null);
				Util.requireQName(element, DDMSVersion.CurrentVersion.Namespace, "name");
			} catch (InvalidDDMSException) {
				fail("Did not allow valid data.");
			}

			// Empty URI
			try {
				XElement element = Util.buildElement("", "name", "", null);
				Util.requireQName(element, null, "name");
			} catch (InvalidDDMSException) {
				fail("Did not allow valid data.");
			}
		}

		public virtual void TestRequireQNameFailure() {
			// Bad URI
			try {
				XElement element = Util.buildElement("ic", "name", "urn:us:gov:ic:ism", null);
				Util.requireQName(element, "", "name");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}

			// Bad Name
			try {
				XElement element = Util.buildDDMSElement("name", null);
				Util.requireQName(element, DDMSVersion.CurrentVersion.Namespace, "newName");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

		public virtual void TestRequireValueNull() {
			try {
				Util.requireValue("description", null);
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "description is required.");
			}
		}

		public virtual void TestRequireValueEmpty() {
			try {
				Util.requireValue("description", "");
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "description is required.");
			}
		}

		public virtual void TestRequireValueNotEmpty() {
			try {
				Util.requireValue("description", "DDMSence");
			} catch (System.ArgumentException) {
				fail("Did not allow valid data.");
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountNullParent() {
			try {
				Util.requireBoundedChildCount(null, "test", 0, 1);
				fail("Allowed illegal argument data.");
			} catch (InvalidDDMSException) {
				fail("Allowed illegal argument data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountNullChild() {
			try {
				Util.requireBoundedChildCount(Util.buildDDMSElement("test", null), null, 0, 1);
				fail("Allowed illegal argument data.");
			} catch (InvalidDDMSException) {
				fail("Allowed illegal argument data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountBounded() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("name", "nameValue"));
			try {
				Util.requireBoundedChildCount(element, "name", 0, 2);
			} catch (InvalidDDMSException) {
				fail("Prevented valid data.");
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountExactly1() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("name", "nameValue"));
			try {
				Util.requireBoundedChildCount(element, "phone", 1, 1);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				assertEquals("Exactly 1 phone element must exist.", e.Message);
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountExactlyX() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("name", "nameValue"));
			try {
				Util.requireBoundedChildCount(element, "phone", 2, 2);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				assertEquals("Exactly 2 phone elements must exist.", e.Message);
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountNoMoreThan1() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			try {
				Util.requireBoundedChildCount(element, "phone", 0, 1);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				assertEquals("No more than 1 phone element can exist.", e.Message);
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountNoMoreThanX() {
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			try {
				Util.requireBoundedChildCount(element, "phone", 0, 2);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				assertEquals("No more than 2 phone elements can exist.", e.Message);
			}
		}

		public virtual void TestRequireBoundedDDMSChildCountGenericUnbounded() {
			XElement element = Util.buildDDMSElement("test", null);
			try {
				Util.requireBoundedChildCount(element, "phone", 1, 5);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				assertEquals("The number of phone elements must be between 1 and 5.", e.Message);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireBoundedChildCountIndependentOfCurrentVersion() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireBoundedChildCountIndependentOfCurrentVersion() {
			DDMSVersion.CurrentVersion = "3.0";
			XElement element = Util.buildDDMSElement("test", null);
			element.appendChild(Util.buildDDMSElement("phone", "phoneValue"));
			DDMSVersion.CurrentVersion = "2.0";
			Util.requireBoundedChildCount(element, "phone", 1, 1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesNull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNCNamesNull() {
			Util.requireValidNCNames(null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNCNamesValid() {
			IList<string> names = new List<string>();
			names.Add("test");
			Util.requireValidNCNames(names);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNCNamesInvalid() {
			IList<string> names = new List<string>();
			names.Add("1test");
			try {
				Util.requireValidNCNames(names);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"1test\" is not a valid NCName.");
			}
		}

		public virtual void TestRequireValidNCNameNull() {
			try {
				Util.requireValidNCName(null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"null\" is not a valid NCName.");
			}
		}

		public virtual void TestRequireValidNCNameInvalidName() {
			try {
				Util.requireValidNCName("1TEST");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"1TEST\" is not a valid NCName.");
			}
		}

		public virtual void TestRequireValidNCNameInvalidNamespace() {
			try {
				Util.requireValidNCName("xmlns:TEST");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"xmlns:TEST\" is not a valid NCName.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNameValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNCNameValid() {
			Util.requireValidNCName("name");
		}

		public virtual void TestRequireValidNMTokenNull() {
			try {
				Util.requireValidNMToken(null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"null\" is not a valid NMTOKEN.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValidName() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNMTokenValidName() {
			Util.requireValidNMToken("1TEST");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValidNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNMTokenValidNamespace() {
			Util.requireValidNMToken("xmlns:TEST");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidNMTokenValid() {
			Util.requireValidNMToken("name");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireDDMSValidURIValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireDDMSValidURIValid() {
			Util.requireDDMSValidURI("test");
		}

		public virtual void TestRequireDDMSValidURIInvalid() {
			try {
				Util.requireDDMSValidURI(":::::");
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Invalid URI");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireDDMSValidURINull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireDDMSValidURINull() {
			try {
				Util.requireDDMSValidURI(null);
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "uri is required.");
			}
		}

		public virtual void TestRequireValidLongitudeNull() {
			try {
				Util.requireValidLongitude(null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
		}

		public virtual void TestRequireValidLongitudeOutOfBounds() {
			try {
				Util.requireValidLongitude(new double?(-181));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
			try {
				Util.requireValidLongitude(new double?(181));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidLongitudeValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidLongitudeValid() {
			Util.requireValidLongitude(new double?(0));
		}

		public virtual void TestRequireValidLatitudeNull() {
			try {
				Util.requireValidLatitude(null);
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
		}

		public virtual void TestRequireValidLatitudeOutOfBounds() {
			try {
				Util.requireValidLatitude(new double?(-91));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
			try {
				Util.requireValidLatitude(new double?(91));
				fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidLatitudeValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireValidLatitudeValid() {
			Util.requireValidLatitude(new double?(0));
		}

		public virtual void TestIsBoundedBadRange() {
			try {
				Util.isBounded(0, 10, 0);
				fail("Did not stop on bad range.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Invalid number range: 10 to 0");
			}
		}

		public virtual void TestIsBoundedLow() {
			assertFalse(Util.isBounded(0, 1, 3));
		}

		public virtual void TestIsBoundedHigh() {
			assertFalse(Util.isBounded(4, 1, 3));
		}

		public virtual void TestIsBoundedMiddle() {
			assertTrue(Util.isBounded(1, 0, 2));
		}

		public virtual void TestIsBoundedLowEdge() {
			assertTrue(Util.isBounded(1, 1, 3));
		}

		public virtual void TestIsBoundedHighEdge() {
			assertTrue(Util.isBounded(3, 1, 3));
		}

		public virtual void TestIsBoundedOnlyOne() {
			assertTrue(Util.isBounded(1, 1, 1));
		}

		public virtual void TestListEqualsNullLists() {
			try {
				Util.listEquals(null, null);
				fail("Did not stop on bad data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Null lists cannot be compared.");
			}
		}

		public virtual void TestListEqualsSameList() {
			IList<string> list = new List<string>();
			list.Add("Test");
			assertTrue(Util.listEquals(list, list));
		}

		public virtual void TestListEqualsEmptyLists() {
			IList<string> list1 = new List<string>();
			IList<string> list2 = new List<string>();
			assertTrue(Util.listEquals(list1, list2));
		}

		public virtual void TestListEqualsSizes() {
			IList<string> list1 = new List<string>();
			list1.Add("Test");
			IList<string> list2 = new List<string>();
			list2.Add("Test");
			list2.Add("Test2");
			assertFalse(Util.listEquals(list1, list2));
		}

		public virtual void TestListEqualsNullValues() {
			IList<string> list1 = new List<string>();
			list1.Add(null);
			list1.Add("Test2");
			IList<string> list2 = new List<string>();
			list2.Add(null);
			list2.Add("Test2");
			assertTrue(Util.listEquals(list1, list2));
		}

		public virtual void TestListEqualsNullValue() {
			IList<string> list1 = new List<string>();
			list1.Add(null);
			list1.Add("Test2");
			IList<string> list2 = new List<string>();
			list2.Add("Test");
			list2.Add("Test2");
			assertFalse(Util.listEquals(list1, list2));
		}

		public virtual void TestListEqualsDifferentValue() {
			IList<string> list1 = new List<string>();
			list1.Add("Test1");
			IList<string> list2 = new List<string>();
			list2.Add("Test2");
			assertFalse(Util.listEquals(list1, list2));
		}

		public virtual void TestXmlEscape() {
			string testString = "<test>\"Brian's DDMSense & DDMS\"</test>";
			assertEquals("&lt;test&gt;&quot;Brian&apos;s DDMSense &amp; DDMS&quot;&lt;/test&gt;", Util.xmlEscape(testString));
		}

		public virtual void TestCapitalizeEmpty() {
			assertEquals(null, Util.capitalize(null));
		}

		public virtual void TestCapitalizeOneChar() {
			assertEquals("A", Util.capitalize("a"));
		}

		public virtual void TestCapitalizeNotLetter() {
			assertEquals("123", Util.capitalize("123"));
		}

		public virtual void TestCapitalizeSuccess() {
			assertEquals("Ddms", Util.capitalize("ddms"));
		}

		public virtual void TestBuildElementEmptyPrefix() {
			XElement element = Util.buildElement(null, "test", "", null);
			assertNotNull(element);
			assertEquals("test", element.LocalName);
			assertEquals("", element.NamespaceURI);
			assertEquals("", element.NamespacePrefix);
			assertEquals("test", element.QualifiedName);

		}

		public virtual void TestBuildDDMSElement() {
			XElement element = Util.buildDDMSElement("test", null);
			assertNotNull(element);
			assertEquals("test", element.LocalName);
			assertEquals(TEST_NAMESPACE, element.NamespaceURI);
			assertEquals(PropertyReader.getPrefix("ddms"), element.NamespacePrefix);
		}

		public virtual void TestBuildDDMSElementNullName() {
			try {
				Util.buildDDMSElement(null, null);
				fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "name is required.");
			}
		}

		public virtual void TestBuildDDMSElementChildText() {
			XElement element = Util.buildDDMSElement("test", "testValue");
			assertNotNull(element);
			assertEquals("test", element.LocalName);
			assertEquals("testValue", element.Value);
			assertEquals(TEST_NAMESPACE, element.NamespaceURI);
			assertEquals(PropertyReader.getPrefix("ddms"), element.NamespacePrefix);
		}

		public virtual void TestBuildDDMSElementNoChildText() {
			XElement element = Util.buildDDMSElement("test", null);
			assertNotNull(element);
			assertEquals("test", element.LocalName);
			assertEquals("", element.Value);
			assertEquals(TEST_NAMESPACE, element.NamespaceURI);
			assertEquals(PropertyReader.getPrefix("ddms"), element.NamespacePrefix);
		}

		public virtual void TestBuildAttribute() {
			Attribute attribute = Util.buildAttribute("ddms", "test", DDMSVersion.CurrentVersion.Namespace, "testValue");
			assertNotNull(attribute);
			assertEquals("test", attribute.LocalName);
			assertEquals("testValue", attribute.Value);
			assertEquals(TEST_NAMESPACE, attribute.NamespaceURI);
			assertEquals(PropertyReader.getPrefix("ddms"), attribute.NamespacePrefix);
		}

		public virtual void TestBuildAttributeEmptyValues() {
			Attribute attribute = Util.buildAttribute(null, "test", null, "testValue");
			assertNotNull(attribute);
			assertEquals("test", attribute.LocalName);
			assertEquals("testValue", attribute.Value);
			assertEquals("", attribute.NamespaceURI);
			assertEquals("", attribute.NamespacePrefix);
		}

		public virtual void TestBuildDDMSAttribute() {
			Attribute attribute = Util.buildDDMSAttribute("test", "testValue");
			assertNotNull(attribute);
			assertEquals("test", attribute.LocalName);
			assertEquals("testValue", attribute.Value);
			assertEquals(TEST_NAMESPACE, attribute.NamespaceURI);
			assertEquals(PropertyReader.getPrefix("ddms"), attribute.NamespacePrefix);
		}

		public virtual void TestBuildDDMSAttributeNullName() {
			try {
				Util.buildDDMSAttribute(null, "testValue");
				fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "name is required.");
			}
		}

		public virtual void TestBuildDDMSAttributeNullValue() {
			try {
				Util.buildDDMSAttribute("test", null);
				fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "value is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireSameVersionSuccess() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestRequireSameVersionSuccess() {
			DDMSVersion.CurrentVersion = "2.0";
			Identifier identifier = new Identifier("Test", "Value");
			Identifier identifier2 = new Identifier("Test", "Value");
			Util.requireCompatibleVersion(identifier, identifier2);
		}

		public virtual void TestRequireSameVersionFailure() {
			try {
				DDMSVersion.CurrentVersion = "2.0";
				Identifier identifier = new Identifier("Test", "Value");
				DDMSVersion.CurrentVersion = "3.0";
				Identifier identifier2 = new Identifier("Test", "Value");
				Util.requireCompatibleVersion(identifier, identifier2);
				fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A child component, ddms:identifier");
			}
		}

		public virtual void TestAddDdmsAttributeEmptyValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.addDDMSAttribute(element, "testAttribute", null);
			assertNull(element.getAttribute("testAttribute", DDMSVersion.CurrentVersion.Namespace));
		}

		public virtual void TestAddDdmsAttributeValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.addDDMSAttribute(element, "testAttribute", "dog");
			Attribute attr = element.getAttribute("testAttribute", DDMSVersion.CurrentVersion.Namespace);
			assertEquals("ddms", attr.NamespacePrefix);
			assertEquals(DDMSVersion.CurrentVersion.Namespace, attr.NamespaceURI);
			assertEquals("testAttribute", attr.LocalName);
			assertEquals("dog", element.getAttributeValue("testAttribute", DDMSVersion.CurrentVersion.Namespace));
		}

		public virtual void TestAddDdmsChildElementEmptyValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.addDDMSChildElement(element, "child", null);
			assertEquals(0, element.ChildElements.size());
		}

		public virtual void TestAddDdmsChildElementValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.addDDMSChildElement(element, "child", "dog");
			assertEquals(1, element.ChildElements.size());
			XElement child = element.getFirstChildElement("child", DDMSVersion.CurrentVersion.Namespace);
			assertEquals("ddms", child.NamespacePrefix);
			assertEquals(DDMSVersion.CurrentVersion.Namespace, child.NamespaceURI);
			assertEquals("child", child.LocalName);
			assertEquals("dog", child.Value);
		}

		public virtual void TestGetDatatypeFactory() {
			assertNotNull(Util.DataTypeFactory);
		}

		public virtual void TestGetAsList() {
			assertTrue(Util.getXsListAsList(null).Empty);
			assertTrue(Util.getXsListAsList("").Empty);
			IList<string> list = Util.getXsListAsList("a b");
			assertEquals(2, list.Count);
			assertEquals("a", list[0]);
			assertEquals("b", list[1]);
			list = Util.getXsListAsList("a      b");
			assertEquals(2, list.Count);
			assertEquals("a", list[0]);
			assertEquals("b", list[1]);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildXmlDocument() throws Exception
		public virtual void TestBuildXmlDocument() {
			File testFile = new File(PropertyReader.getProperty("test.unit.data") + "3.0/", "resource.xml");
			string expectedXmlOutput = (new DDMSReader()).getDDMSResource(testFile).toXML();
			assertEquals(expectedXmlOutput, Util.buildXmlDocument(new FileInputStream(testFile)).RootElement.toXML());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildXmlDocumentBadFile() throws Exception
		public virtual void TestBuildXmlDocumentBadFile() {
			try {
				Util.buildXmlDocument(null);
				fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "input stream is required.");
			}

			try {
				Util.buildXmlDocument(new ByteArrayInputStream("Not an XML File".GetBytes()));
				fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "Content is not allowed in prolog.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSchematronQueryBinding() throws Exception
		public virtual void TestSchematronQueryBinding() {
			Document schDocument = Util.buildXmlDocument(new FileInputStream("data/sample/schematron/testPublisherValueXslt1.sch"));
			assertEquals("xslt", Util.getSchematronQueryBinding(schDocument));
			schDocument = Util.buildXmlDocument(new FileInputStream("data/sample/schematron/testPositionValuesXslt2.sch"));
			assertEquals("xslt2", Util.getSchematronQueryBinding(schDocument));
		}
	}

}