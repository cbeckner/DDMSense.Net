using System.Collections.Generic;
using System.Linq;
using DDMSense.Extensions;

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


    using DDMSense.DDMS.ResourceElements;
    using DDMSense.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Xml.Linq;
    using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;
    using System.Text;

	/// <summary>
	/// A collection of Util tests.
	/// </summary>
	public class UtilTest : AbstractBaseTestCase {

		protected internal static readonly string TEST_NAMESPACE = DDMSVersion.CurrentVersion.Namespace;

		public UtilTest() : base(null) {
		}

        [TestMethod]
		public virtual void TestGetNonNullStringNull() {
            Assert.AreEqual("",Util.GetNonNullString(null));
		}

        [TestMethod]
		public virtual void TestGetNonNullStringValue() {
			Assert.AreEqual("Test", Util.GetNonNullString("Test"));
		}

        [TestMethod]
		public virtual void TestGetXsList() {
			List<string> list = new List<string>();
			list.Add("Test");
			list.Add("Dog");
			Assert.AreEqual("Test Dog", Util.GetXsList(list));
		}

        [TestMethod]
		public virtual void TestGetXsListNull() {
			Assert.AreEqual("", Util.GetXsList((List<string>)null));
		}

        [TestMethod]
		public virtual void TestBooleanHashCodeTrue() {
			Assert.AreEqual(1, Util.BooleanHashCode(true));
		}

        [TestMethod]
		public virtual void TestBooleanHashCodeFalse() {
			Assert.AreEqual(0, Util.BooleanHashCode(false));
		}

        [TestMethod]
		public virtual void TestContainsEmptyStringFalse() {
			Assert.IsFalse(Util.ContainsOnlyEmptyValues(null));
			List<string> list = new List<string>();
			list.Add("dog");
			list.Add("");
			list.Add(null);
			Assert.IsFalse(Util.ContainsOnlyEmptyValues(list));
		}

        [TestMethod]
		public virtual void TestContainsEmptyStringTrue() {
			List<string> list = new List<string>();
			Assert.IsTrue(Util.ContainsOnlyEmptyValues(list));
			list.Add("");
			Assert.IsTrue(Util.ContainsOnlyEmptyValues(list));
			list.Clear();
			list.Add(null);
			Assert.IsTrue(Util.ContainsOnlyEmptyValues(list));
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueNullParent() {
			try {
				Util.GetFirstDDMSChildValue(null, "test");
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueNullChild() {
			try {
				Util.GetFirstDDMSChildValue(Util.BuildDDMSElement("test", null), null);
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueWrongNamespace() {
			XElement element = Util.BuildElement("ddmsence", "test", "http://ddmsence.urizone.net/", null);
			try {
				Util.GetFirstDDMSChildValue(element, "child");
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "This method should only be called");
			}
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueIndependentOfCurrentVersion() {
            DDMSVersion.SetCurrentVersion("3.0");
			XElement element = Util.BuildDDMSElement("test", null);
            element.Add(Util.BuildDDMSElement("child", "childText1"));
			element.Add(Util.BuildDDMSElement("child", "childText2"));
            DDMSVersion.SetCurrentVersion("2.0");
			string value = Util.GetFirstDDMSChildValue(element, "child");
			Assert.AreEqual("childText1", value);
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueNoValue() {
			string value = Util.GetFirstDDMSChildValue(Util.BuildDDMSElement("test", null), "unknown");
			Assert.AreEqual("", value);
		}

        [TestMethod]
		public virtual void TestGetFirstDDMSChildValueWithValue() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("child", "childText1"));
			element.Add(Util.BuildDDMSElement("child", "childText2"));
			string value = Util.GetFirstDDMSChildValue(element, "child");
			Assert.AreEqual("childText1", value);
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesNullParent() {
			try {
				Util.GetDDMSChildValues(null, "test");
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesNullChild() {
			try {
				Util.GetDDMSChildValues(Util.BuildDDMSElement("test", null), null);
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesWrongNamespace() {
			XElement element = Util.BuildElement("ddmsence", "test", "http://ddmsence.urizone.net/", null);
			element.Add(Util.BuildDDMSElement("child", "child1"));
			element.Add(Util.BuildDDMSElement("child", "child2"));
			try {
				Util.GetDDMSChildValues(element, "child");
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "This method should only be called");
			}
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesIndependentOfCurrentVersion() {
			DDMSVersion.SetCurrentVersion("3.0");
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("child", "child1"));
			element.Add(Util.BuildDDMSElement("child", "child2"));
			DDMSVersion.SetCurrentVersion("2.0");
			List<string> values = Util.GetDDMSChildValues(element, "child");
			Assert.IsNotNull(values);
			Assert.AreEqual(2, values.Count);
			Assert.AreEqual("child1", values[0]);
			Assert.AreEqual("child2", values[1]);
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesNoValues() {
			XElement element = Util.BuildDDMSElement("test", null);
			List<string> values = Util.GetDDMSChildValues(element, "unknown");
			Assert.IsNotNull(values);
			Assert.IsTrue(values.Count == 0);
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesEmptyValues() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("child", ""));
			List<string> values = Util.GetDDMSChildValues(element, "child");
			Assert.IsNotNull(values);
			Assert.AreEqual(1, values.Count);
			Assert.AreEqual("", values[0]);
		}

        [TestMethod]
		public virtual void TestGetDDMSChildValuesWithValues() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("child", "child1"));
			element.Add(Util.BuildDDMSElement("child", "child2"));
			List<string> values = Util.GetDDMSChildValues(element, "child");
			Assert.IsNotNull(values);
			Assert.AreEqual(2, values.Count);
			Assert.AreEqual("child1", values[0]);
			Assert.AreEqual("child2", values[1]);
		}

        [TestMethod]
		public virtual void TestRequireDDMSValueNull() {
			try {
				Util.RequireDDMSValue("description", null);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "description is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSValueEmpty() {
			try {
				Util.RequireDDMSValue("description", "");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "description is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSValueNotEmpty() {
			try {
				Util.RequireDDMSValue("description", "DDMSence");
			} catch (InvalidDDMSException) {
				Assert.Fail("Did not allow valid data.");
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSDateFormatSuccess() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				try {
				   Util.RequireDDMSDateFormat("2012", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01-01", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01-01T01:02:03Z", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01-01T01:02:03+05:00", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01-01T01:02:03.4Z", version.Namespace);
				   Util.RequireDDMSDateFormat("2012-01-01T01:02:03.4+05:00", version.Namespace);
					if (version.IsAtLeast("4.1")) {
						Util.RequireDDMSDateFormat("2012-01-01T01:02Z", version.Namespace);
						Util.RequireDDMSDateFormat("2012-01-01T01:02+05:00", version.Namespace);
					}
				} catch (InvalidDDMSException) {
					Assert.Fail("Did not allow valid data.");
				}
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSDateFormatFailure() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				// Another XSD date type
				try {
				   Util.RequireDDMSDateFormat("---31", version.Namespace);
				   Assert.Fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The date datatype must be");
				}

				// Not a date
				try {
				   Util.RequireDDMSDateFormat("baboon", version.Namespace);
				   Assert.Fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "The date datatype must be");
				}

				// ddms:DateHourMinType (specifying 4.0.1 here, since you can't tell if it's 4.0.1 or 4.1 by XML namespace)
				if (!version.IsAtLeast("4.0.1")) {
					try {
					   Util.RequireDDMSDateFormat("2012-01-01T01:02Z", version.Namespace);
					   Assert.Fail("Allowed invalid data.");
					} catch (InvalidDDMSException e) {
						ExpectMessage(e, "The date datatype must be");
					}
					try {
					   Util.RequireDDMSDateFormat("2012-01-01T01:02+05:00", version.Namespace);
					   Assert.Fail("Allowed invalid data.");
					} catch (InvalidDDMSException e) {
						ExpectMessage(e, "The date datatype must be");
					}
				}
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSQNameSuccess() {
			try {
				XElement element = Util.BuildDDMSElement("name", null);
				Util.RequireDDMSQualifiedName(element, "name");
			} catch (InvalidDDMSException) {
				Assert.Fail("Did not allow valid data.");
			}
		}

        [TestMethod]
		public virtual void TestRequireDDMSQNameFailure() {
			// Bad URI
			try {
				XElement element = Util.BuildElement("ic", "name", "urn:us:gov:ic:ism", null);
				Util.RequireDDMSQualifiedName(element, "name");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}

			// Bad Name
			try {
				XElement element = Util.BuildDDMSElement("name", null);
				Util.RequireDDMSQualifiedName(element, "newName");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}
        
        [TestMethod]
		public virtual void TestRequireQNameSuccess() {
			try {
				XElement element = Util.BuildDDMSElement("name", null);
				Util.RequireQualifiedName(element, DDMSVersion.CurrentVersion.Namespace, "name");
			} catch (InvalidDDMSException) {
				Assert.Fail("Did not allow valid data.");
			}

			// Empty URI
			try {
				XElement element = Util.BuildElement("", "name", "", null);
				Util.RequireQualifiedName(element, null, "name");
			} catch (InvalidDDMSException) {
				Assert.Fail("Did not allow valid data.");
			}
		}

        [TestMethod]
		public virtual void TestRequireQNameFailure() {
			// Bad URI
			try {
				XElement element = Util.BuildElement("ic", "name", "urn:us:gov:ic:ism", null);
				Util.RequireQualifiedName(element, "", "name");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}

			// Bad Name
			try {
				XElement element = Util.BuildDDMSElement("name", null);
				Util.RequireQualifiedName(element, DDMSVersion.CurrentVersion.Namespace, "newName");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Unexpected namespace URI and local name encountered");
			}
		}

        [TestMethod]
		public virtual void TestRequireValueNull() {
			try {
				Util.RequireValue("description", null);
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "description is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireValueEmpty() {
			try {
				Util.RequireValue("description", "");
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "description is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireValueNotEmpty() {
			try {
				Util.RequireValue("description", "DDMSence");
			} catch (System.ArgumentException) {
				Assert.Fail("Did not allow valid data.");
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountNullParent() {
			try {
				Util.RequireBoundedChildCount(null, "test", 0, 1);
				Assert.Fail("Allowed illegal argument data.");
			} catch (InvalidDDMSException) {
				Assert.Fail("Allowed illegal argument data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "parent element is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountNullChild() {
			try {
				Util.RequireBoundedChildCount(Util.BuildDDMSElement("test", null), null, 0, 1);
				Assert.Fail("Allowed illegal argument data.");
			} catch (InvalidDDMSException) {
				Assert.Fail("Allowed illegal argument data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "child name is required.");
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountBounded() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("name", "nameValue"));
			try {
				Util.RequireBoundedChildCount(element, "name", 0, 2);
			} catch (InvalidDDMSException) {
				Assert.Fail("Prevented valid data.");
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountExactly1() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("name", "nameValue"));
			try {
				Util.RequireBoundedChildCount(element, "phone", 1, 1);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				Assert.AreEqual("Exactly 1 phone element must exist.", e.Message);
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountExactlyX() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("name", "nameValue"));
			try {
				Util.RequireBoundedChildCount(element, "phone", 2, 2);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				Assert.AreEqual("Exactly 2 phone elements must exist.", e.Message);
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountNoMoreThan1() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			try {
				Util.RequireBoundedChildCount(element, "phone", 0, 1);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				Assert.AreEqual("No more than 1 phone element can exist.", e.Message);
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountNoMoreThanX() {
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			try {
				Util.RequireBoundedChildCount(element, "phone", 0, 2);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				Assert.AreEqual("No more than 2 phone elements can exist.", e.Message);
			}
		}

        [TestMethod]
		public virtual void TestRequireBoundedDDMSChildCountGenericUnbounded() {
			XElement element = Util.BuildDDMSElement("test", null);
			try {
				Util.RequireBoundedChildCount(element, "phone", 1, 5);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				Assert.AreEqual("The number of phone elements must be between 1 and 5.", e.Message);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireBoundedChildCountIndependentOfCurrentVersion() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireBoundedChildCountIndependentOfCurrentVersion() {
			DDMSVersion.SetCurrentVersion("3.0");
			XElement element = Util.BuildDDMSElement("test", null);
			element.Add(Util.BuildDDMSElement("phone", "phoneValue"));
			DDMSVersion.SetCurrentVersion("2.0");
			Util.RequireBoundedChildCount(element, "phone", 1, 1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesNull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNCNamesNull() {
			Util.RequireValidNCNames(null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNCNamesValid() {
			List<string> names = new List<string>();
			names.Add("test");
			Util.RequireValidNCNames(names);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNamesInvalid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNCNamesInvalid() {
			List<string> names = new List<string>();
			names.Add("1test");
			try {
				Util.RequireValidNCNames(names);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"1test\" is not a valid NCName.");
			}
		}

        [TestMethod]
		public virtual void TestRequireValidNCNameNull() {
			try {
				Util.RequireValidNCName(null);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"null\" is not a valid NCName.");
			}
		}

        [TestMethod]
		public virtual void TestRequireValidNCNameInvalidName() {
			try {
				Util.RequireValidNCName("1TEST");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"1TEST\" is not a valid NCName.");
			}
		}

        [TestMethod]
		public virtual void TestRequireValidNCNameInvalidNamespace() {
			try {
				Util.RequireValidNCName("xmlns:TEST");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"xmlns:TEST\" is not a valid NCName.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNCNameValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNCNameValid() {
			Util.RequireValidNCName("name");
		}

        [TestMethod]
		public virtual void TestRequireValidNMTokenNull() {
			try {
				Util.RequireValidNMToken(null);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "\"null\" is not a valid NMTOKEN.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValidName() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNMTokenValidName() {
			Util.RequireValidNMToken("1TEST");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValidNamespace() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNMTokenValidNamespace() {
			Util.RequireValidNMToken("xmlns:TEST");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidNMTokenValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidNMTokenValid() {
			Util.RequireValidNMToken("name");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireDDMSValidURIValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireDDMSValidURIValid() {
			Util.RequireDDMSValidUri("test");
		}

        [TestMethod]
		public virtual void TestRequireDDMSValidURIInvalid() {
			try {
				Util.RequireDDMSValidUri(":::::");
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "Invalid URI");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireDDMSValidURINull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireDDMSValidURINull() {
			try {
				Util.RequireDDMSValidUri(null);
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "uri is required.");
			}
		}

        [TestMethod]
        public virtual void TestRequireValidLongitudeNull() {
			try {
				Util.RequireValidLongitude(null);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
		}

        [TestMethod]
		public virtual void TestRequireValidLongitudeOutOfBounds() {
			try {
				Util.RequireValidLongitude(new double?(-181));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
			try {
				Util.RequireValidLongitude(new double?(181));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A longitude value must be between");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidLongitudeValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidLongitudeValid() {
			Util.RequireValidLongitude(new double?(0));
		}

        [TestMethod]
		public virtual void TestRequireValidLatitudeNull() {
			try {
				Util.RequireValidLatitude(null);
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
		}

        [TestMethod]
		public virtual void TestRequireValidLatitudeOutOfBounds() {
			try {
				Util.RequireValidLatitude(new double?(-91));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
			try {
				Util.RequireValidLatitude(new double?(91));
				Assert.Fail("Allowed invalid data.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A latitude value must be between");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireValidLatitudeValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireValidLatitudeValid() {
			Util.RequireValidLatitude(new double?(0));
		}

        [TestMethod]
        public virtual void TestIsBoundedBadRange() {
			try {
				Util.IsBounded(0, 10, 0);
				Assert.Fail("Did not stop on bad range.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Invalid number range: 10 to 0");
			}
		}

        [TestMethod]
		public virtual void TestIsBoundedLow() {
			Assert.IsFalse(Util.IsBounded(0, 1, 3));
		}

        [TestMethod]
		public virtual void TestIsBoundedHigh() {
			Assert.IsFalse(Util.IsBounded(4, 1, 3));
		}

        [TestMethod]
		public virtual void TestIsBoundedMiddle() {
			Assert.IsTrue(Util.IsBounded(1, 0, 2));
		}

        [TestMethod]
		public virtual void TestIsBoundedLowEdge() {
			Assert.IsTrue(Util.IsBounded(1, 1, 3));
		}

        [TestMethod]
		public virtual void TestIsBoundedHighEdge() {
			Assert.IsTrue(Util.IsBounded(3, 1, 3));
		}

        [TestMethod]
		public virtual void TestIsBoundedOnlyOne() {
			Assert.IsTrue(Util.IsBounded(1, 1, 1));
		}

        [TestMethod]
		public virtual void TestListEqualsNullLists() {
			try {
                Util.ListEquals((List<object>)null, (List<object>)null);
				Assert.Fail("Did not stop on bad data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "Null lists cannot be compared.");
			}
		}

        [TestMethod]
		public virtual void TestListEqualsSameList() {
			List<string> list = new List<string>();
			list.Add("Test");
			Assert.IsTrue(Util.ListEquals(list, list));
		}

        [TestMethod]
		public virtual void TestListEqualsEmptyLists() {
			List<string> list1 = new List<string>();
			List<string> list2 = new List<string>();
			Assert.IsTrue(Util.ListEquals(list1, list2));
		}

        [TestMethod]
		public virtual void TestListEqualsSizes() {
			List<string> list1 = new List<string>();
			list1.Add("Test");
			List<string> list2 = new List<string>();
			list2.Add("Test");
			list2.Add("Test2");
			Assert.IsFalse(Util.ListEquals(list1, list2));
		}

        [TestMethod]
		public virtual void TestListEqualsNullValues() {
			List<string> list1 = new List<string>();
			list1.Add(null);
			list1.Add("Test2");
			List<string> list2 = new List<string>();
			list2.Add(null);
			list2.Add("Test2");
			Assert.IsTrue(Util.ListEquals(list1, list2));
		}

        [TestMethod]
		public virtual void TestListEqualsNullValue() {
			List<string> list1 = new List<string>();
			list1.Add(null);
			list1.Add("Test2");
			List<string> list2 = new List<string>();
			list2.Add("Test");
			list2.Add("Test2");
			Assert.IsFalse(Util.ListEquals(list1, list2));
		}

        [TestMethod]
		public virtual void TestListEqualsDifferentValue() {
			List<string> list1 = new List<string>();
			list1.Add("Test1");
			List<string> list2 = new List<string>();
			list2.Add("Test2");
			Assert.IsFalse(Util.ListEquals(list1, list2));
		}

        [TestMethod]
		public virtual void TestXmlEscape() {
			string testString = "<test>\"Brian's DDMSense & DDMS\"</test>";
			Assert.AreEqual("&lt;test&gt;&quot;Brian&apos;s DDMSense &amp; DDMS&quot;&lt;/test&gt;", Util.XmlEscape(testString));
		}

        [TestMethod]
		public virtual void TestCapitalizeEmpty() {
			Assert.AreEqual(null, Util.Capitalize(null));
		}

        [TestMethod]
		public virtual void TestCapitalizeOneChar() {
			Assert.AreEqual("A", Util.Capitalize("a"));
		}

        [TestMethod]
		public virtual void TestCapitalizeNotLetter() {
			Assert.AreEqual("123", Util.Capitalize("123"));
		}

        [TestMethod]
		public virtual void TestCapitalizeSuccess() {
			Assert.AreEqual("Ddms", Util.Capitalize("ddms"));
		}

        [TestMethod]
		public virtual void TestBuildElementEmptyPrefix() {
			XElement element = Util.BuildElement(null, "test", "", null);
			Assert.IsNotNull(element);
			Assert.AreEqual("test", element.Name.LocalName);
			Assert.AreEqual("", element.Name.NamespaceName);
            Assert.AreEqual("", element.GetPrefix());
			//Assert.AreEqual("test", element.QualifiedName);

		}

        [TestMethod]
		public virtual void TestBuildDDMSElement() {
			XElement element = Util.BuildDDMSElement("test", null);
			Assert.IsNotNull(element);
			Assert.AreEqual("test", element.Name.LocalName);
			Assert.AreEqual(TEST_NAMESPACE, element.Name.NamespaceName);
            Assert.AreEqual(PropertyReader.GetPrefix("ddms"), element.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildDDMSElementNullName() {
			try {
				Util.BuildDDMSElement(null, null);
				Assert.Fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "name is required.");
			}
		}

        [TestMethod]
		public virtual void TestBuildDDMSElementChildText() {
			XElement element = Util.BuildDDMSElement("test", "testValue");
			Assert.IsNotNull(element);
			Assert.AreEqual("test", element.Name.LocalName);
			Assert.AreEqual("testValue", element.Value);
			Assert.AreEqual(TEST_NAMESPACE, element.Name.NamespaceName);
            Assert.AreEqual(PropertyReader.GetPrefix("ddms"), element.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildDDMSElementNoChildText() {
			XElement element = Util.BuildDDMSElement("test", null);
			Assert.IsNotNull(element);
			Assert.AreEqual("test", element.Name.LocalName);
			Assert.AreEqual("", element.Value);
			Assert.AreEqual(TEST_NAMESPACE, element.Name.NamespaceName);
			Assert.AreEqual(PropertyReader.GetPrefix("ddms"), element.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildAttribute() {
			XAttribute attribute = Util.BuildAttribute("ddms", "test", DDMSVersion.CurrentVersion.Namespace, "testValue");
			Assert.IsNotNull(attribute);
			Assert.AreEqual("test", attribute.Name.LocalName);
			Assert.AreEqual("testValue", attribute.Value);
			Assert.AreEqual(TEST_NAMESPACE, attribute.Name.NamespaceName);
			Assert.AreEqual(PropertyReader.GetPrefix("ddms"), attribute.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildAttributeEmptyValues() {
			XAttribute attribute = Util.BuildAttribute(null, "test", null, "testValue");
			Assert.IsNotNull(attribute);
			Assert.AreEqual("test", attribute.Name.LocalName);
			Assert.AreEqual("testValue", attribute.Value);
			Assert.AreEqual("", attribute.Name.NamespaceName);
			Assert.AreEqual("", attribute.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildDDMSAttribute() {
			XAttribute attribute = Util.BuildDDMSAttribute("test", "testValue");
			Assert.IsNotNull(attribute);
			Assert.AreEqual("test", attribute.Name.LocalName);
			Assert.AreEqual("testValue", attribute.Value);
			Assert.AreEqual(TEST_NAMESPACE, attribute.Name.NamespaceName);
			Assert.AreEqual(PropertyReader.GetPrefix("ddms"), attribute.GetPrefix());
		}

        [TestMethod]
		public virtual void TestBuildDDMSAttributeNullName() {
			try {
				Util.BuildDDMSAttribute(null, "testValue");
				Assert.Fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "name is required.");
			}
		}

        [TestMethod]
		public virtual void TestBuildDDMSAttributeNullValue() {
			try {
				Util.BuildDDMSAttribute("test", null);
				Assert.Fail("Method allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "value is required.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testRequireSameVersionSuccess() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
        [TestMethod]
        public virtual void TestRequireSameVersionSuccess() {
			DDMSVersion.SetCurrentVersion("2.0");
			Identifier identifier = new Identifier("Test", "Value");
			Identifier identifier2 = new Identifier("Test", "Value");
			Util.RequireCompatibleVersion(identifier, identifier2);
		}

        [TestMethod]
		public virtual void TestRequireSameVersionFailure() {
			try {
				DDMSVersion.SetCurrentVersion("2.0");
				Identifier identifier = new Identifier("Test", "Value");
				DDMSVersion.SetCurrentVersion("3.0");
				Identifier identifier2 = new Identifier("Test", "Value");
				Util.RequireCompatibleVersion(identifier, identifier2);
				Assert.Fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "A child component, ddms:identifier");
			}
		}

        [TestMethod]
		public virtual void TestAddDdmsAttributeEmptyValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.AddDDMSAttribute(element, "testAttribute", null);
			Assert.IsNull(element.Attribute(XName.Get("testAttribute", DDMSVersion.CurrentVersion.Namespace)));
		}

        [TestMethod]
		public virtual void TestAddDdmsAttributeValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.AddDDMSAttribute(element, "testAttribute", "dog");
            XAttribute attr = element.Attribute(XName.Get("testAttribute", DDMSVersion.CurrentVersion.Namespace));
			Assert.AreEqual("ddms", attr.GetPrefix());
			Assert.AreEqual(DDMSVersion.CurrentVersion.Namespace, attr.Name.NamespaceName);
			Assert.AreEqual("testAttribute", attr.Name.LocalName);
			Assert.AreEqual("dog", element.Attribute(XName.Get("testAttribute", DDMSVersion.CurrentVersion.Namespace)));
		}

        [TestMethod]
		public virtual void TestAddDdmsChildElementEmptyValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.AddDDMSChildElement(element, "child", null);
			Assert.AreEqual(0, element.Elements().Count());
		}

        [TestMethod]
		public virtual void TestAddDdmsChildElementValue() {
			XElement element = new XElement("test", "http://test.com");
			Util.AddDDMSChildElement(element, "child", "dog");
			Assert.AreEqual(1, element.Elements().Count());
			XElement child = element.Element(XName.Get("child", DDMSVersion.CurrentVersion.Namespace));
            Assert.AreEqual("ddms", child.GetPrefix());
			Assert.AreEqual(DDMSVersion.CurrentVersion.Namespace, child.Name.NamespaceName);
			Assert.AreEqual("child", child.Name.LocalName);
			Assert.AreEqual("dog", child.Value);
		}
        
        [TestMethod]
		public virtual void TestGetAsList() {
			Assert.IsTrue(!Util.GetXsListAsList(null).Any());
			Assert.IsTrue(!Util.GetXsListAsList("").Any());
			List<string> list = Util.GetXsListAsList("a b");
			Assert.AreEqual(2, list.Count);
			Assert.AreEqual("a", list[0]);
			Assert.AreEqual("b", list[1]);
			list = Util.GetXsListAsList("a      b");
			Assert.AreEqual(2, list.Count);
			Assert.AreEqual("a", list[0]);
			Assert.AreEqual("b", list[1]);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildXmlDocument() throws Exception
        [TestMethod]
        public virtual void TestBuildXmlDocument() {
            //File testFile = new File(PropertyReader.getProperty("test.unit.data") + "3.0/", "resource.xml");
            //string expectedXmlOutput = (new DDMSReader()).GetDDMSResource(testFile).toXML();
            //Assert.AreEqual(expectedXmlOutput, Util.BuildXmlDocument(new FileStream(testFile)).RootElement.toXML());
            Assert.Fail("Not Implemented");
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuildXmlDocumentBadFile() throws Exception
        [TestMethod]
        public virtual void TestBuildXmlDocumentBadFile() {
			try {
				Util.BuildXmlDocument(null);
				Assert.Fail("Allowed invalid data.");
			} catch (System.ArgumentException e) {
				ExpectMessage(e, "input stream is required.");
			}

			try {
				Util.BuildXmlDocument(new MemoryStream(Encoding.ASCII.GetBytes("Not an XML File")));
				Assert.Fail("Allowed invalid data.");
			} catch (IOException e) {
				ExpectMessage(e, "Content is not allowed in prolog.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testSchematronQueryBinding() throws Exception
        [TestMethod]
        public virtual void TestSchematronQueryBinding() {
			XDocument schDocument = Util.BuildXmlDocument(new FileStream("data/sample/schematron/testPublisherValueXslt1.sch",FileMode.Open));
			Assert.AreEqual("xslt", Util.GetSchematronQueryBinding(schDocument));
			schDocument = Util.BuildXmlDocument(new FileStream("data/sample/schematron/testPositionValuesXslt2.sch",FileMode.Open));
			Assert.AreEqual("xslt2", Util.GetSchematronQueryBinding(schDocument));
		}
	}


//Bill's AWESOME date tests!
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DDMSense.Util;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DDMSense.DDMS;

//namespace DDMSense.Util.Tests
//{
//    [TestClass()]
//    public class UtilTests
//    {
//        [TestMethod()]
//        public void RequireDDMSDateFormatTest()
//        {
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy"), "urn:us:mil:ces:metadata:ddms:4");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM"), "urn:us:mil:ces:metadata:ddms:4");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-dd"), "urn:us:mil:ces:metadata:ddms:4");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHHK"), "urn:us:mil:ces:metadata:ddms:4");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//        }

//        [TestMethod()]
//        [ExpectedException(typeof(InvalidDDMSException))]
//        public void Invalid_RequireDDMSDateFormat()
//        {
//            //Util.RequireDDMSDateFormat(DateTime.Now.ToString(), "urn:us:mil:ces:metadata:ddms:4");
//            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy/MM/ddTHHK"), "urn:us:mil:ces:metadata:ddms:4");
//            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.f"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//            //Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffK"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//            Util.RequireDDMSDateFormat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), @"http://metadata.dod.mil/mdr/ns/DDMS/3.1/");
//        }
//    }
//}
//    /*  YYYY
//        YYYY-MM
//        YYYY-MM-DD
//        YYYY-MM-DDThhTZD
//        YYYY-MM-DDThh:mmTZD
//        YYYY-MM-DDThh:mm.ssTZD
//        YYYY-MM-DDThh:mm:ss.sTZD
//        Where:
//        YYYY	0000 through current year
//        MM	01 through 12  (month)
//        DD	01 through 31  (day)
//        hh	00 through 24  (hour)
//        mm	00 through 59  (minute)
//        ss	00 through 60  (second)
//        .s	.0 through 999 (fractional second)
//        TZD  = time zone designator (Z or +hh:mm or -hh:mm)
//    */
}