using System.Collections;
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
namespace buri.ddmsence.ddms.security.ism {


	using Element = nu.xom.Element;
	using Rights = buri.ddmsence.ddms.resource.Rights;
	using Title = buri.ddmsence.ddms.resource.Title;
	using DDMSVersion = buri.ddmsence.util.DDMSVersion;
	using PropertyReader = buri.ddmsence.util.PropertyReader;
	using Util = buri.ddmsence.util.Util;

	/// <summary>
	/// <para> Tests related to the ISM attributes </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class SecurityAttributesTest : AbstractBaseTestCase {

		private const string TEST_CLASS = "U";
		private static readonly IList<string> TEST_OWNERS = Util.getXsListAsList("USA");

		private static readonly IDictionary<string, string> TEST_OTHERS_41 = new Dictionary<string, string>();
		static SecurityAttributesTest() {
			TEST_OTHERS_41[SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME] = "RD";
			TEST_OTHERS_41[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
			TEST_OTHERS_41[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
			TEST_OTHERS_41[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
			TEST_OTHERS_41[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
			TEST_OTHERS_41[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
			TEST_OTHERS_41[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
			TEST_OTHERS_41[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
			TEST_OTHERS_41[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
			TEST_OTHERS_41[SecurityAttributes.DISPLAY_ONLY_TO_NAME] = "AIA";
			TEST_OTHERS_41[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
			TEST_OTHERS_41[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
			TEST_OTHERS_41[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
			TEST_OTHERS_41[SecurityAttributes.NON_IC_MARKINGS_NAME] = "DS";
			TEST_OTHERS_41[SecurityAttributes.NON_US_CONTROLS_NAME] = "ATOMAL";
			TEST_OTHERS_41[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
			TEST_OTHERS_41[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
			TEST_OTHERS_41[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
			TEST_OTHERS_31[SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME] = "RD";
			TEST_OTHERS_31[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
			TEST_OTHERS_31[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
			TEST_OTHERS_31[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
			TEST_OTHERS_31[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
			TEST_OTHERS_31[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
			TEST_OTHERS_31[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
			TEST_OTHERS_31[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
			TEST_OTHERS_31[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
			TEST_OTHERS_31[SecurityAttributes.DISPLAY_ONLY_TO_NAME] = "AIA";
			TEST_OTHERS_31[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
			TEST_OTHERS_31[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
			TEST_OTHERS_31[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
			TEST_OTHERS_31[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
			TEST_OTHERS_31[SecurityAttributes.NON_US_CONTROLS_NAME] = "ATOMAL";
			TEST_OTHERS_31[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
			TEST_OTHERS_31[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
			TEST_OTHERS_31[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
			TEST_OTHERS_30[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
			TEST_OTHERS_30[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
			TEST_OTHERS_30[SecurityAttributes.COMPILATION_REASON_NAME] = "NO";
			TEST_OTHERS_30[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-11";
			TEST_OTHERS_30[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
			TEST_OTHERS_30[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
			TEST_OTHERS_30[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
			TEST_OTHERS_30[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
			TEST_OTHERS_30[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
			TEST_OTHERS_30[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
			TEST_OTHERS_30[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
			TEST_OTHERS_30[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
			TEST_OTHERS_30[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
			TEST_OTHERS_30[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
			TEST_OTHERS_30[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
			TEST_OTHERS_30[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
			TEST_OTHERS_30[SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME] = "OADR";
			TEST_OTHERS_20[SecurityAttributes.CLASSIFICATION_REASON_NAME] = "PQ";
			TEST_OTHERS_20[SecurityAttributes.CLASSIFIED_BY_NAME] = " MN";
			TEST_OTHERS_20[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-11";
			TEST_OTHERS_20[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
			TEST_OTHERS_20[SecurityAttributes.DECLASS_EVENT_NAME] = "RS";
			TEST_OTHERS_20[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "25X1";
			TEST_OTHERS_20[SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME] = "true";
			TEST_OTHERS_20[SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME] = "OP";
			TEST_OTHERS_20[SecurityAttributes.DERIVED_FROM_NAME] = "QR";
			TEST_OTHERS_20[SecurityAttributes.DISSEMINATION_CONTROLS_NAME] = "FOUO";
			TEST_OTHERS_20[SecurityAttributes.FGI_SOURCE_OPEN_NAME] = "ALA";
			TEST_OTHERS_20[SecurityAttributes.FGI_SOURCE_PROTECTED_NAME] = "FGI";
			TEST_OTHERS_20[SecurityAttributes.NON_IC_MARKINGS_NAME] = "SINFO";
			TEST_OTHERS_20[SecurityAttributes.RELEASABLE_TO_NAME] = "AIA";
			TEST_OTHERS_20[SecurityAttributes.SAR_IDENTIFIER_NAME] = "SAR-USA";
			TEST_OTHERS_20[SecurityAttributes.SCI_CONTROLS_NAME] = "HCS";
			TEST_OTHERS_20[SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME] = "OADR";
		}
		private static readonly IDictionary<string, string> TEST_OTHERS_31 = new Dictionary<string, string>();
		private static readonly IDictionary<string, string> TEST_OTHERS_30 = new Dictionary<string, string>();
		private static readonly IDictionary<string, string> TEST_OTHERS_20 = new Dictionary<string, string>();

		/// <summary>
		/// Constructor
		/// </summary>
		public SecurityAttributesTest() : base(null) {
		}

		/// <summary>
		/// Resets the validation property.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void tearDown() throws Exception
		protected internal override void TearDown() {
			base.TearDown();
		}

		/// <summary>
		/// Returns a fixture object for testing. These attributes will only contain the basic required attributes
		/// (classification and ownerProducer).
		/// </summary>
		public static SecurityAttributes Fixture {
			get {
				try {
					return (new SecurityAttributes(TEST_CLASS, TEST_OWNERS, null));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a fixture object for testing. These attributes will be a full set, including optional attributes.
		/// </summary>
		public static SecurityAttributes FullFixture {
			get {
				try {
					return (new SecurityAttributes(TEST_CLASS, TEST_OWNERS, OtherAttributes));
				} catch (InvalidDDMSException e) {
					fail("Could not create fixture: " + e.Message);
				}
				return (null);
			}
		}

		/// <summary>
		/// Returns a set of attributes for a specific version of DDMS.
		/// </summary>
		/// <returns> an attribute group </returns>
		private static IDictionary<string, string> OtherAttributes {
			get {
				string version = DDMSVersion.CurrentVersion.Version;
				if ("2.0".Equals(version)) {
					return (new Dictionary<string, string>(TEST_OTHERS_20));
				}
				if ("3.0".Equals(version)) {
					return (new Dictionary<string, string>(TEST_OTHERS_30));
				}
				if ("3.1".Equals(version)) {
					return (new Dictionary<string, string>(TEST_OTHERS_31));
				}
				return (new Dictionary<string, string>(TEST_OTHERS_41));
			}
		}

		/// <summary>
		/// Returns a set of attributes for a specific version of DDMS, with a single attribute replaced by a custom value.
		/// </summary>
		/// <param name="key"> the key of the attribute to replace </param>
		/// <param name="value"> the new value to set for that attribute </param>
		/// <returns> an attribute group </returns>
		private static IDictionary<string, string> GetOtherAttributes(string key, string value) {
			IDictionary<string, string> baseAttributes = new Hashtable(OtherAttributes);
			baseAttributes[key] = value;
			return (baseAttributes);
		}

		/// <summary>
		/// Helper method to confirm that changing a single attribute correctly affects equality of two instances
		/// </summary>
		/// <param name="expected"> the base set of attributes </param>
		/// <param name="key"> the key of the attribute that will change </param>
		/// <param name="value"> the value of the attribute that will change </param>
		private void AssertAttributeChangeAffectsEquality(SecurityAttributes expected, string key, string value) {
			IDictionary<string, string> others = GetOtherAttributes(key, value);
			assertFalse(expected.Equals(GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, others)));
		}

		/// <summary>
		/// Attempts to build a component from a XOM element.
		/// </summary>
		/// <param name="message"> an expected error message. If empty, the constructor is expected to succeed. </param>
		/// <param name="element"> the element to build from
		/// </param>
		/// <returns> a valid object </returns>
		private SecurityAttributes GetInstance(string message, Element element) {
			bool expectFailure = !Util.isEmpty(message);
			SecurityAttributes attributes = null;
			try {
				attributes = new SecurityAttributes(element);
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
		/// <param name="classification"> the classification level, which must be a legal classification type (optional) </param>
		/// <param name="ownerProducers"> a list of ownerProducers (optional) </param>
		/// <param name="otherAttributes"> a name/value mapping of other ISM attributes. The value will be a String value, as it
		/// appears in XML. </param>
		/// <returns> a valid object </returns>
		private SecurityAttributes GetInstance(string message, string classification, IList<string> ownerProducers, IDictionary<string, string> otherAttributes) {
			bool expectFailure = !Util.isEmpty(message);
			SecurityAttributes attributes = null;
			try {
				attributes = new SecurityAttributes(classification, ownerProducers, otherAttributes);
				CheckConstructorSuccess(expectFailure);
			} catch (InvalidDDMSException e) {
				CheckConstructorFailure(expectFailure, e);
				ExpectMessage(e, message);
			}
			return (attributes);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testElementConstructorValid() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestElementConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ismPrefix = PropertyReader.getPrefix("ism");
				string icNamespace = version.IsmNamespace;

				// All fields
				Element element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				FullFixture.addTo(element);
				GetInstance(SUCCESS, element);

				// No optional fields
				element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				Util.addAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
				Util.addAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.getXsList(TEST_OWNERS));
				GetInstance(SUCCESS, element);
			}
		}

		public virtual void TestDataConstructorValid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				// All fields
				GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);

				// No optional fields
				GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, null);

				// Extra fields
				GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, GetOtherAttributes("notAnAttribute", "test"));
			}
		}

		public virtual void TestElementConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string ismPrefix = PropertyReader.getPrefix("ism");
				string icNamespace = version.IsmNamespace;

				// invalid declassDate
				Element element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				Util.addAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
				Util.addAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.getXsList(TEST_OWNERS));
				Util.addAttribute(element, ismPrefix, SecurityAttributes.DECLASS_DATE_NAME, icNamespace, "2001");
				GetInstance("The declassDate must be in the xs:date format", element);

				// invalid dateOfExemptedSource
				element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, ismPrefix, Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				Util.addAttribute(element, ismPrefix, SecurityAttributes.CLASSIFICATION_NAME, icNamespace, TEST_CLASS);
				Util.addAttribute(element, ismPrefix, SecurityAttributes.OWNER_PRODUCER_NAME, icNamespace, Util.getXsList(TEST_OWNERS));
				Util.addAttribute(element, ismPrefix, SecurityAttributes.DECLASS_DATE_NAME, icNamespace, "2001");
				Util.addAttribute(element, ismPrefix, SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace, "2001");
				string message = (version.isAtLeast("3.1") ? "The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0." : "The dateOfExemptedSource attribute must be in the xs:date format");
				GetInstance(message, element);
			}
		}

		public virtual void TestDataConstructorInvalid() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);

				// invalid declassDate
				GetInstance("The declassDate must be in the xs:date format", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DECLASS_DATE_NAME, "2004"));

				// nonsensical declassDate
				GetInstance("The ISM:declassDate attribute is not in a valid date format.", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DECLASS_DATE_NAME, "notAnXmlDate"));

				// invalid dateOfExemptedSource
				string message = (version.isAtLeast("3.1") ? "The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0." : "The dateOfExemptedSource attribute must be in the xs:date format");
				GetInstance(message, TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2004"));

				// nonsensical dateOfExemptedSource
				GetInstance("The ISM:dateOfExemptedSource attribute is not in a valid date format.", TEST_CLASS, TEST_OWNERS, GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "notAnXmlDate"));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWarnings() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWarnings() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string icNamespace = version.IsmNamespace;

				// No warnings
				Element element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, PropertyReader.getPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				FullFixture.addTo(element);
				SecurityAttributes attr = GetInstance(SUCCESS, element);
				assertEquals(0, attr.ValidationWarnings.size());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string icNamespace = version.IsmNamespace;

				Element element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, PropertyReader.getPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				FullFixture.addTo(element);
				SecurityAttributes elementAttributes = GetInstance(SUCCESS, element);
				SecurityAttributes dataAttributes = GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);

				assertEquals(elementAttributes, elementAttributes);
				assertEquals(elementAttributes, dataAttributes);
				assertEquals(elementAttributes.GetHashCode(), dataAttributes.GetHashCode());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityDifferentValues() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityDifferentValues() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				string icNamespace = version.IsmNamespace;

				Element element = Util.buildDDMSElement(Security.getName(version), null);
				Util.addAttribute(element, PropertyReader.getPrefix("ism"), Security.EXCLUDE_FROM_ROLLUP_NAME, icNamespace, "true");
				FullFixture.addTo(element);
				SecurityAttributes expected = GetInstance(SUCCESS, element);

				if (version.isAtLeast("3.1")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME, "FRD");
				}
				assertFalse(expected.Equals(GetInstance(SUCCESS, "C", TEST_OWNERS, OtherAttributes))); // Classification
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.CLASSIFIED_BY_NAME, DIFFERENT_VALUE);
				if (version.isAtLeast("3.0")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.COMPILATION_REASON_NAME, DIFFERENT_VALUE);
				}
				if (!version.isAtLeast("3.1")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2001-10-10");
				}
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_DATE_NAME, "2001-10-10");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_EVENT_NAME, DIFFERENT_VALUE);
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_EXCEPTION_NAME, "25X4");
				if (!version.isAtLeast("3.0")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, "false");
				}
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DERIVATIVELY_CLASSIFIED_BY_NAME, DIFFERENT_VALUE);
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DERIVED_FROM_NAME, DIFFERENT_VALUE);
				if (version.isAtLeast("3.1")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DISPLAY_ONLY_TO_NAME, "USA");
				}
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.DISSEMINATION_CONTROLS_NAME, "EYES");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.FGI_SOURCE_OPEN_NAME, "BGR");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.FGI_SOURCE_PROTECTED_NAME, "BGR");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.NON_IC_MARKINGS_NAME, "SBU");
				if (version.isAtLeast("3.1")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.NON_US_CONTROLS_NAME, "BALK");
				}
				assertFalse(expected.Equals(GetInstance(SUCCESS, TEST_CLASS, Util.getXsListAsList("AUS"), OtherAttributes))); // OwnerProducer
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.RELEASABLE_TO_NAME, "BGR");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.SAR_IDENTIFIER_NAME, "SAR-AIA");
				AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.SCI_CONTROLS_NAME, "TK");
				if (!version.isAtLeast("3.1")) {
					AssertAttributeChangeAffectsEquality(expected, SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "X4");
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testConstructorInequalityWrongClass() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestConstructorInequalityWrongClass() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SecurityAttributes elementAttributes = FullFixture;
				Rights wrongComponent = new Rights(true, true, true);
				assertFalse(elementAttributes.Equals(wrongComponent));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testAddTo() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestAddTo() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				SecurityAttributes component = Fixture;

				Element element = Util.buildDDMSElement("sample", null);
				component.addTo(element);
				SecurityAttributes output = new SecurityAttributes(element);
				assertEquals(component, output);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testGetNonNull() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestGetNonNull() {
			SecurityAttributes component = new SecurityAttributes(null, null, null);
			SecurityAttributes output = SecurityAttributes.getNonNullInstance(null);
			assertEquals(component, output);

			output = SecurityAttributes.getNonNullInstance(Fixture);
			assertEquals(Fixture, output);
		}

		public virtual void TestIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, null, null);
				assertTrue(dataAttributes.Empty);
				dataAttributes = GetInstance(SUCCESS, TEST_CLASS, null, null);
				assertFalse(dataAttributes.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testWrongVersionAttributes() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestWrongVersionAttributes() {
			DDMSVersion.CurrentVersion = "3.0";
			SecurityAttributes attr = GetInstance(SUCCESS, TEST_CLASS, TEST_OWNERS, OtherAttributes);
			DDMSVersion.CurrentVersion = "2.0";
			try {
				new Title("Wrong Version Title", attr);
				fail("Allowed different versions.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "These attributes cannot decorate");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test30AttributesIn31() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void Test30AttributesIn31() {
			DDMSVersion.CurrentVersion = "3.1";
			IDictionary<string, string> others = GetOtherAttributes(SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "OADR");
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed 3.0 attributes in 3.1.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The typeOfExemptedSource attribute can only be used");
			}

			others = GetOtherAttributes(SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME, "2010-01-01");
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed 3.0 attributes in 3.1.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The dateOfExemptedSource attribute can only be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test31AttributesIn30() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void Test31AttributesIn30() {
			DDMSVersion.CurrentVersion = "3.0";
			IDictionary<string, string> others = GetOtherAttributes(SecurityAttributes.ATOMIC_ENERGY_MARKINGS_NAME, "RD");
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed 3.1 attributes in 3.0.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The atomicEnergyMarkings attribute cannot be used");
			}

			others = GetOtherAttributes(SecurityAttributes.DISPLAY_ONLY_TO_NAME, "AIA");
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed 3.1 attributes in 3.0.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The displayOnlyTo attribute cannot be used");
			}

			others = GetOtherAttributes(SecurityAttributes.NON_US_CONTROLS_NAME, "ATOMAL");
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed 3.1 attributes in 3.0.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The nonUSControls attribute cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testClassificationValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestClassificationValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;
				IDictionary<string, string> others = OtherAttributes;

				// Missing classification
				SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, TEST_OWNERS, others);
				try {
					dataAttributes.requireClassification();
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}

				// Empty classification
				dataAttributes = GetInstance(SUCCESS, "", TEST_OWNERS, others);
				try {
					dataAttributes.requireClassification();
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "classification is required.");
				}

				// Invalid classification
				GetInstance("ZOO is not a valid enumeration token", "ZOO", TEST_OWNERS, others);

				// No ownerProducers
				dataAttributes = GetInstance(SUCCESS, TEST_CLASS, null, others);
				try {
					dataAttributes.requireClassification();
					fail("Allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "At least 1 ownerProducer must be set.");
				}

				// No non-empty ownerProducers
				IList<string> ownerProducers = new List<string>();
				ownerProducers.Add("");
				dataAttributes = GetInstance(" is not a valid enumeration token", TEST_CLASS, ownerProducers, others);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDateOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDateOutput() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				IDictionary<string, string> others = new Dictionary<string, string>();
				others[SecurityAttributes.DECLASS_DATE_NAME] = "2005-10-10";
				SecurityAttributes dataAttributes = GetInstance(SUCCESS, null, null, others);
				assertEquals(BuildOutput(true, "declassDate", "2005-10-10"), dataAttributes.getOutput(true, ""));
				assertEquals(BuildOutput(false, "declassDate", "2005-10-10"), dataAttributes.getOutput(false, ""));

				if (!version.isAtLeast("3.1")) {
					others = new Dictionary<string, string>();
					others[SecurityAttributes.DATE_OF_EXEMPTED_SOURCE_NAME] = "2005-10-10";
					dataAttributes = GetInstance(SUCCESS, null, null, others);
					assertEquals(BuildOutput(true, "dateOfExemptedSource", "2005-10-10"), dataAttributes.getOutput(true, ""));
					assertEquals(BuildOutput(false, "dateOfExemptedSource", "2005-10-10"), dataAttributes.getOutput(false, ""));
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testOldClassifications() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestOldClassifications() {
			DDMSVersion.CurrentVersion = "2.0";
			GetInstance(SUCCESS, "NS-S", TEST_OWNERS, null);
			GetInstance(SUCCESS, "NS-A", TEST_OWNERS, null);
			DDMSVersion.CurrentVersion = "3.0";
			GetInstance("NS-S is not a valid enumeration token", "NS-S", TEST_OWNERS, null);
			GetInstance("NS-A is not a valid enumeration token for this attribute", "NS-A", TEST_OWNERS, null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test30AttributesIn20() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void Test30AttributesIn20() {
			try {
				DDMSVersion.CurrentVersion = "3.0";
				IDictionary<string, string> others = OtherAttributes;
				DDMSVersion.CurrentVersion = "2.0";
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, others);
				fail("Allowed DDMS 3.0 attributes to be used in DDMS 2.0.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The compilationReason attribute cannot be used");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void test20AttributesIn30() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void Test20AttributesIn30() {
			DDMSVersion.CurrentVersion = "2.0";
			IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME, "true");
			try {
				DDMSVersion.CurrentVersion = "3.0";
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
				fail("Allowed DDMS 2.0 attributes to be used in DDMS 3.0.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "The declassManualReview attribute can only be used in DDMS 2.0.");
			}

			DDMSVersion.CurrentVersion = "2.0";
			map.Remove(SecurityAttributes.COMPILATION_REASON_NAME);
			SecurityAttributes attr = new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
			assertTrue(attr.getOutput(true, "").contains(BuildOutput(true, "declassManualReview", "true")));
			assertTrue(attr.getOutput(false, "").contains(BuildOutput(false, "declassManualReview", "true")));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testMultipleDeclassException() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestMultipleDeclassException() {
			DDMSVersion.CurrentVersion = "2.0";
			IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.DECLASS_EXCEPTION_NAME, "25X1 25X2");
			new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testMultipleTypeExempted() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestMultipleTypeExempted() {
			DDMSVersion.CurrentVersion = "2.0";
			IDictionary<string, string> map = GetOtherAttributes(SecurityAttributes.TYPE_OF_EXEMPTED_SOURCE_NAME, "X1 X2");
			new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testDeclassManualReviewHtmlOutput() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestDeclassManualReviewHtmlOutput() {
			DDMSVersion.CurrentVersion = "2.0";
			IDictionary<string, string> map = new Dictionary<string, string>();
			map[SecurityAttributes.DECLASS_MANUAL_REVIEW_NAME] = "true";
			SecurityAttributes attributes = new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
			assertEquals(BuildOutput(true, "classification", "U") + BuildOutput(true, "declassManualReview", "true") + BuildOutput(true, "ownerProducer", "USA"), attributes.getOutput(true, ""));
		}

		public virtual void TestCVEErrorsByDefault() {
			IDictionary<string, string> map = new Dictionary<string, string>();
			map[SecurityAttributes.DECLASS_EXCEPTION_NAME] = "UnknownValue";
			try {
				new SecurityAttributes(TEST_CLASS, TEST_OWNERS, map);
				fail("Allowed invalid CVE value without throwing an Exception.");
			} catch (InvalidDDMSException e) {
				ExpectMessage(e, "UnknownValue is not a valid enumeration token");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderEquality() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderEquality() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SecurityAttributes component = FullFixture;
				SecurityAttributes.Builder builder = new SecurityAttributes.Builder(component);
				assertEquals(component, builder.commit());
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderIsEmpty() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderIsEmpty() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
				assertNotNull(builder.commit());
				assertTrue(builder.Empty);
				builder.AtomicEnergyMarkings = Util.getXsListAsList("");
				assertTrue(builder.Empty);
				builder.AtomicEnergyMarkings = Util.getXsListAsList("RD FRD");
				assertFalse(builder.Empty);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderValidation() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderValidation() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion.CurrentVersion = sVersion;

				SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
				builder.Classification = "SuperSecret";
				try {
					builder.commit();
					fail("Builder allowed invalid data.");
				} catch (InvalidDDMSException e) {
					ExpectMessage(e, "SuperSecret is not a valid enumeration token");
				}
				builder.Classification = "U";
				builder.commit();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testBuilderLazyList() throws buri.ddmsence.ddms.InvalidDDMSException
		public virtual void TestBuilderLazyList() {
			foreach (string sVersion in SupportedVersions) {
				DDMSVersion version = DDMSVersion.setCurrentVersion(sVersion);
				SecurityAttributes.Builder builder = new SecurityAttributes.Builder();
				assertNotNull(builder.OwnerProducers.get(1));
				assertNotNull(builder.SCIcontrols.get(1));
				assertNotNull(builder.SARIdentifier.get(1));
				assertNotNull(builder.DisseminationControls.get(1));
				assertNotNull(builder.FGIsourceOpen.get(1));
				assertNotNull(builder.FGIsourceProtected.get(1));
				assertNotNull(builder.ReleasableTo.get(1));
				assertNotNull(builder.NonICmarkings.get(1));

				if (version.isAtLeast("3.1")) {
					assertNotNull(builder.AtomicEnergyMarkings.get(1));
					assertNotNull(builder.DisplayOnlyTo.get(1));
					assertNotNull(builder.NonUSControls.get(1));
				}
			}
		}
	}

}