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
namespace DDMSense.Test {


	using IDDMSComponent = DDMSense.DDMS.IDDMSComponent;
	using InvalidDDMSException = DDMSense.DDMS.InvalidDDMSException;
	using ValidationMessage = DDMSense.DDMS.ValidationMessage;
	using DDMSReader = DDMSense.Util.DDMSReader;
	using DDMSVersion = DDMSense.Util.DDMSVersion;
	using PropertyReader = DDMSense.Util.PropertyReader;
	using Util = DDMSense.Util.Util;
    using System.Xml.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

	/// <summary>
	/// Base class for DDMSence test cases.
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
    [TestClass]
	public abstract class AbstractBaseTestCase {

		private string _type;
		private List<string> _supportedVersions = new List<string>(DDMSVersion.SupportedVersions);

		private static IDictionary<string, XElement> _elementMap = new Dictionary<string, XElement>();

		protected internal const string TEST_ID = "IDValue";

		protected internal const string SUCCESS = "";
		protected internal const string INVALID_URI = ":::::";
		protected internal const string DIFFERENT_VALUE = "Different";
		protected internal const string WRONG_NAME_MESSAGE = "Unexpected namespace URI and local name encountered:";
		protected internal static readonly string DEFAULT_DDMS_PREFIX = PropertyReader.GetPrefix("ddms");
		protected internal static readonly string DEFAULT_GML_PREFIX = PropertyReader.GetPrefix("gml");
		protected internal static readonly string DEFAULT_ISM_PREFIX = PropertyReader.GetPrefix("ism");
		protected internal static readonly string DEFAULT_NTK_PREFIX = PropertyReader.GetPrefix("ntk");

		/// <summary>
		/// Resets the in-use version of DDMS.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void setUp() throws Exception
		[TestInitialize]
        protected internal virtual void SetUp() {
			DDMSVersion.ClearCurrentVersion();
		}

		/// <summary>
		/// Resets the in-use version of DDMS.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void tearDown() throws Exception
		[TestCleanup]
        protected internal virtual void TearDown() {
			DDMSVersion.ClearCurrentVersion();
			PropertyReader.SetProperty("output.indexLevel", "0");
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="validDocumentFile"> the filename to load. One of these will be loaded for each supporting version. </param>
		public AbstractBaseTestCase(string validDocumentFile) {
			_type = validDocumentFile;
			if (validDocumentFile == null) {
				return;
			}
			try {
				DDMSReader reader = null;
				foreach (string sVersion in SupportedVersions) {
					if (GetValidElement(sVersion) == null) {
						if (reader == null) {
							reader = new DDMSReader();
						}
						FileInfo file = new FileInfo(Path.Combine(PropertyReader.GetProperty("test.unit.data") + sVersion, validDocumentFile));
						if (file.Exists) {
                            string text;
                            using (StreamReader streamReader = new StreamReader(file.ToString(), Encoding.UTF8))
                            {
                                text = streamReader.ReadToEnd();
                            }
							XElement element = reader.GetElement(text);
							lock (_elementMap) {
								_elementMap[Type + ":" + sVersion] = element;
							}
						}
					}
				}
			} catch (Exception e) {
				throw new Exception("Cannot run tests without valid DDMSReader and valid unit test object.", e);
			}

		}

		/// <summary>
		/// Convenience method to fail a test if the wrong error message comes back. This addresses cases where a test is no
		/// longer failing for the reason we expect it to be failing.
		/// </summary>
		/// <param name="e"> the exception </param>
		/// <param name="message"> the beginning of the expected message (enough to confirm its accuracy). </param>
		protected internal virtual void ExpectMessage(Exception e, string message) {
			if (!e.Message.StartsWith(message)) {
				Console.WriteLine(DDMSVersion.CurrentVersion);
				Console.WriteLine(e.Message);
				Assert.Fail("Test failed for the wrong reason.");
			}
		}

		/// <summary>
		/// Convenience method to create a XOM element which is not a valid DDMS component because of an incorrect name.
		/// </summary>
		protected internal static XElement WrongNameElementFixture {
			get {
				return (DDMSense.Util.Util.BuildDDMSElement("wrongName", null));
			}
		}

		/// <summary>
		/// Should be called after a successful constructor execution. If the constructor was expected to fail, but it
		/// succeeded, the test will fail.
		/// </summary>
		/// <param name="expectFailure"> true if the constructor was expected to fail. </param>
        protected internal static void CheckConstructorSuccess(bool expectFailure) {
			if (expectFailure) {
				Assert.Fail("Constructor allowed invalid data.");
			}
		}

		/// <summary>
		/// Should be called after a failed constructor execution. If the constructor was expected to succeed, but it failed,
		/// the test will fail.
		/// </summary>
		/// <param name="expectFailure"> true if the constructor was expected to fail. </param>
		/// <param name="exception"> the exception that occurred </param>
        protected internal static void CheckConstructorFailure(bool expectFailure, InvalidDDMSException exception) {
			if (!expectFailure) {
				Assert.Fail("Constructor failed on valid data: " + exception.Message);
			}
		}

		/// <summary>
		/// Helper method to confirm that a warning message is correct.
		/// </summary>
		/// <param name="text"> the text of the message </param>
		/// <param name="locator"> the locator text of the message </param>
		/// <param name="message"> the ValidationMessage to test </param>
        protected internal virtual void AssertWarningEquality(string text, string locator, ValidationMessage message) {
			if (locator != "") {
				locator = "/" + locator;
			}
			Assert.IsTrue(ValidationMessage.WarningType.Equals(message.Type));
			Assert.IsTrue(locator.Equals(message.Locator));
			Assert.IsTrue(message.Text.StartsWith(text));
		}

		/// <summary>
		/// Helper method to confirm that a warning message is correct.
		/// </summary>
		/// <param name="text"> the text of the message </param>
		/// <param name="locator"> the locator text of the message </param>
		/// <param name="message"> the ValidationMessage to test </param>
        protected internal virtual void AssertErrorEquality(string text, string locator, ValidationMessage message) {
			if (locator != "") {
				locator = "/" + locator;
			}
			Assert.IsTrue(ValidationMessage.ErrorType.Equals(message.Type));
			Assert.IsTrue(locator.Equals(message.Locator));
			Assert.IsTrue(message.Text.StartsWith(text));
		}

		/// <summary>
		/// Shared method for testing the name and namespace of a created component.
		/// </summary>
		/// <param name="component"> the component to test </param>
		/// <param name="prefix"> the expected XML prefix </param>
		/// <param name="name"> the expected XML local name </param>
		
        protected internal virtual void AssertNameAndNamespace(IDDMSComponent component, string prefix, string name) {
			Assert.AreEqual(name, component.Name);
			Assert.AreEqual(prefix, component.Prefix);
			Assert.AreEqual(prefix + ":" + name, component.QualifiedName);
		}

		/// <summary>
		/// Convenience method to build a meta tag for HTML output or a text line for Text output.
		/// </summary>
		/// <param name="isHTML"> true for HTML, false for Text </param>
		/// <param name="name"> the name value of the meta tag (will be escaped in HTML) </param>
		/// <param name="content"> the content value of the meta tag (will be escaped in HTML) </param>
		/// <returns> a string containing the output </returns>
		public static string BuildOutput(bool isHTML, string name, string content) {
			StringBuilder tag = new StringBuilder();
			tag.Append(isHTML ? "<meta name=\"" : "");
			tag.Append(isHTML ? DDMSense.Util.Util.XmlEscape(name) : name);
			tag.Append(isHTML ? "\" content=\"" : ": ");
            tag.Append(isHTML ? DDMSense.Util.Util.XmlEscape(content) : content);
			tag.Append(isHTML ? "\" />\n" : "\n");
			return (tag.ToString());
		}

		/// <summary>
		/// Strips tabs and new lines from XML output where appropriate. The unit test samples in the XML files have tabs and
		/// new lines, but the default implementation of XOM toXML() returns XML on a single line.
		/// </summary>
		/// <param name="str"> the original string </param>
		/// <param name="preserveFormatting"> true to retain tabs and new lines, false to strip them. </param>
		/// <returns> the modified string </returns>
		protected internal static string FormatXml(string str, bool preserveFormatting) {
			if (!preserveFormatting) {
				str = str.Replace("\t", "");
				str = str.Replace("\n", "");
			}
			return (str);
		}

		/// <summary>
		/// Returns a namespace declaration for DDMS
		/// </summary>
		protected internal static string XmlnsDDMS {
			get {
				return ("xmlns:ddms=\"" + DDMSVersion.CurrentVersion.Namespace + "\"");
			}
		}

		/// <summary>
		/// Returns a namespace declaration for ISM
		/// </summary>
		protected internal static string XmlnsISM {
			get {
				return ("xmlns:ISM=\"" + DDMSVersion.CurrentVersion.IsmNamespace + "\"");
			}
		}

		/// <summary>
		/// Returns a namespace declaration for GML
		/// </summary>
		protected internal static string XmlnsGML {
			get {
				return ("xmlns:gml=\"" + DDMSVersion.CurrentVersion.GmlNamespace + "\"");
			}
		}

		/// <summary>
		/// Returns a namespace declaration for NTK
		/// </summary>
		protected internal static string XmlnsNTK {
			get {
				return ("xmlns:ntk=\"" + DDMSVersion.CurrentVersion.NtkNamespace + "\"");
			}
		}

		/// <summary>
		/// Accessor for a valid DDMS XOM XElement constructed from the root element of an XML file, which can be used in
		/// testing as a "correct base case".
		/// </summary>
		public virtual XElement GetValidElement(string version) {
			return (_elementMap.GetValueOrNull(Type + ":" + version));
		}

		/// <summary>
		/// Removes specific versions, so that components will only be tested in supported versions.
		/// </summary>
		/// <param name="xsList"> an xs:list containing the unsupported version numbers </param>
		protected internal virtual void RemoveSupportedVersions(string xsList) {
			List<string> unsupportedVersions = DDMSense.Util.Util.GetXsListAsList(xsList);
            unsupportedVersions.ForEach(x => SupportedVersions.Remove(x));
		}

		/// <summary>
		/// Accessor for the local identifier for the type of component being tested
		/// </summary>
		private string Type {
			get {
				return (_type);
			}
		}

		/// <summary>
		/// Accessor for the supported versions for this specific component
		/// </summary>
		protected internal virtual List<string> SupportedVersions {
			get {
				return (_supportedVersions);
			}
		}
	}

}