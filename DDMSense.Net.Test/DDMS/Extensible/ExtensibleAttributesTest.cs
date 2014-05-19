using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
namespace DDMSense.Test.DDMS.Extensible {


	
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using Util = DDMSense.Util.Util;
	using DDMSense.DDMS.Extensible;
	using System.Xml.Linq;
	using DDMSense.DDMS;
	using System.Xml;
	using DDMSense.DDMS.Summary;
	using DDMSense.DDMS.ResourceElements;

	/// <summary>
	/// <para> Tests related to the extensible attributes themselves. How they interact with parent classes is tested in those
	/// classes. </para>
	/// 
	/// @author Brian Uri!
	/// @since 1.1.0
	/// </summary>
	public class ExtensibleAttributesTest : AbstractBaseTestCase {

		private const string TEST_NAMESPACE = "http://ddmsence.urizone.net/";

		private static readonly XAttribute TEST_ATTRIBUTE = new XAttribute(XName.Get("ddmsence:relevance", TEST_NAMESPACE), "95");

		/// <summary>
		/// Constructor
		/// </summary>
		public ExtensibleAttributesTest() : base(null) {
		}

		/// <summary>
		/// Returns a fixture object for testing.
		/// </summary>
		public static ExtensibleAttributes Fixture {
			get {
				try {
					List<XAttribute> attributes = new List<XAttribute>();
					attributes.Add(new XAttribute(TEST_ATTRIBUTE));
					return (new ExtensibleAttributes(attributes));
				} catch (InvalidDDMSException e) {
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
		private ExtensibleAttributes GetInstance(string message, XElement element) {
			bool expectFailure = !string.IsNullOrEmpty(message);
			ExtensibleAttributes attributes = null;
			try {
				attributes = new ExtensibleAttributes(element);
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
		/// <param name="attributes"> a list of attributes (optional) </param>
		/// <returns> a valid object </returns>
		private ExtensibleAttributes GetInstance(string message, List<XAttribute> attributes) {
			bool expectFailure = !string.IsNullOrEmpty(message);
			ExtensibleAttributes exAttributes = null;
			try {
				exAttributes = new ExtensibleAttributes(attributes);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (exAttributes);
		}

		/// <summary>
		/// Returns the expected HTML or Text output for this unit test
		/// </summary>
		private string GetExpectedOutput(bool isHTML) {
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, "ddmsence.relevance", "95"));
			return (text.ToString());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				// All fields
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				GetInstance(SUCCESS, element);

				// No optional fields
				element = (new Keyword("testValue", null)).ElementCopy;
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				// All fields
				List<XAttribute> attributes = new List<XAttribute>();
				attributes.Add(new XAttribute(TEST_ATTRIBUTE));
				GetInstance(SUCCESS, attributes);

				// No optional fields
				GetInstance(SUCCESS, (List<XAttribute>) null);
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				// No invalid cases right now, since reserved names are silently skipped.
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				// No invalid cases right now. The validation occurs when the attributes are added to some component.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				// No warnings
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				GetInstance(SUCCESS, element);
				ExtensibleAttributes component = GetInstance(SUCCESS, element);
				Assert.Equals(0, component.ValidationWarnings.Count);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);

				List<XAttribute> attributes = new List<XAttribute>();
				attributes.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, attributes);

				Assert.Equals(elementAttributes, dataAttributes);
				Assert.Equals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
				List<XAttribute> attributes = new List<XAttribute>();
				attributes.Add(new XAttribute(XName.Get("essence:confidence", "http://essence/"), "test"));
				ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, attributes);
				Assert.Fail(elementAttributes.Equals(dataAttributes).ToString());

				dataAttributes = GetInstance(SUCCESS, (List<XAttribute>) null);
				Assert.Fail(elementAttributes.Equals(dataAttributes).ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
				Rights wrongComponent = new Rights(true, true, true);
				Assert.Fail(elementAttributes.Equals(wrongComponent).ToString());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testHTMLTextOutput() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestHTMLTextOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
				Assert.Equals(GetExpectedOutput(true), elementAttributes.GetOutput(true, ""));
				Assert.Equals(GetExpectedOutput(false), elementAttributes.GetOutput(false, ""));

				List<XAttribute> attributes = new List<XAttribute>();
				attributes.Add(new XAttribute(TEST_ATTRIBUTE));
				elementAttributes = GetInstance(SUCCESS, attributes);
				Assert.Equals(GetExpectedOutput(true), elementAttributes.GetOutput(true, ""));
				Assert.Equals(GetExpectedOutput(false), elementAttributes.GetOutput(false, ""));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testAddTo() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestAddTo() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				ExtensibleAttributes component = Fixture;

				XElement element = Util.BuildDDMSElement("sample", null);
				component.AddTo(element);
				ExtensibleAttributes output = new ExtensibleAttributes(element);
				Assert.Equals(component, output);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetNonNull() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		//public virtual void TestGetNonNull() {
		//    //TODO - figure out correct instantiation for this
		//    ExtensibleAttributes component = new ExtensibleAttributes((List) null); 
		//    ExtensibleAttributes output = ExtensibleAttributes.GetNonNullInstance(null);
		//    Assert.Equals(component, output);

		//    output = ExtensibleAttributes.GetNonNullInstance(Fixture);
		//    Assert.Equals(Fixture, output);
		//}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes elementAttributes = GetInstance(SUCCESS, element);
				Assert.Fail(elementAttributes.Empty.ToString());

				ExtensibleAttributes dataAttributes = GetInstance(SUCCESS, (List<XAttribute>) null);
				Assert.IsTrue(dataAttributes.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				XElement element = (new Keyword("testValue", null)).ElementCopy;
				element.Add(new XAttribute(TEST_ATTRIBUTE));
				ExtensibleAttributes component = GetInstance(SUCCESS, element);
				ExtensibleAttributes.Builder builder = new ExtensibleAttributes.Builder(component);
				Assert.Equals(component, builder.Commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				ExtensibleAttributes.Builder builder = new ExtensibleAttributes.Builder();
				Assert.IsNotNull(builder.Commit());
				Assert.IsTrue(builder.Empty);
				builder.Attributes.Add(new ExtensibleAttributes.AttributeBuilder(TEST_ATTRIBUTE));
				Assert.Fail(builder.Empty.ToString());

			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);

				// No invalid cases right now, because validation cannot occur until these attributes are attached to something.
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws DDMSense.Net.Test.DDMS.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.SetCurrentVersion(sVersion);
				ExtensibleAttributes.Builder builder = new ExtensibleAttributes.Builder();
				Assert.IsNotNull(builder.Attributes[1]);
				Assert.IsTrue(builder.Commit().Attributes.Count == 0);
			}
		}
	}

}