using System;
using System.Collections.Generic;
using System.Text;
using DDMSSense.Extensions;
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
namespace DDMSSense.DDMS.SecurityElements.Ism {



	using Element = System.Xml.Linq.XElement;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.Xml.Linq;

	/// <summary>
	/// Attribute group for the ISM markings used throughout DDMS.
	/// 
	/// <para> When validating this attribute group, the required/optional nature of the classification and
	/// ownerProducer attributes are not checked. Because that limitation depends on the parent element (for example,
	/// ddms:title requires them, but ddms:creator does not), the parent element should be responsible for checking, via
	/// <code>requireClassification()</code>. </para>
	/// 
	/// <para> At this time, logical validation is only done on the data types of the various attributes, and the controlled
	/// vocabulary enumerations behind some of the attributes. Any further validation would require integration
	/// with ISM Schematron files as discussed in the Schematron Validation Power Tip on the website.</para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ISM:atomicEnergyMarkings</u>: (optional, starting in DDMS 3.1)<br />
	/// <u>ISM:classification</u>: (optional)<br />
	/// <u>ISM:classificationReason</u>: (optional)<br />
	/// <u>ISM:classifiedBy</u>: (optional)<br />
	/// <u>ISM:compilationReason</u>: (optional, starting in DDMS 3.0)<br />
	/// <u>ISM:dateOfExemptedSource</u>: (optional, DDMS 2.0 and 3.0 only)<br />
	/// <u>ISM:declassDate</u>: (optional)<br />
	/// <u>ISM:declassEvent</u>: (optional)<br />
	/// <u>ISM:declassException</u>: (optional)<br />
	/// <u>ISM:declassManualReview</u>: (optional, DDMS 2.0 only)<br />
	/// <u>ISM:derivativelyClassifiedBy</u>: (optional)<br />
	/// <u>ISM:derivedFrom</u>: (optional)<br />
	/// <u>ISM:displayOnlyTo</u>: (optional, starting in DDMS 3.1)<br />
	/// <u>ISM:disseminationControls</u>: (optional)<br />
	/// <u>ISM:FGIsourceOpen</u>: (optional)<br />
	/// <u>ISM:FGIsourceProtected</u>: (optional)<br />
	/// <u>ISM:nonICmarkings</u>: (optional)<br />
	/// <u>ISM:nonUSControls</u>: (optional, starting in DDMS 3.1)<br />
	/// <u>ISM:ownerProducer</u>: (optional)<br />
	/// <u>ISM:releasableTo</u>: (optional)<br />
	/// <u>ISM:SARIdentifier</u>: (optional)<br />
	/// <u>ISM:SCIcontrols</u>: (optional)<br />
	/// <u>ISM:typeOfExemptedSource</u>: (optional, DDMS 2.0 and 3.0 only)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class SecurityAttributes : AbstractAttributeGroup {
		private List<string> _atomicEnergyMarkings = null;
		private string _classification = null;
		private string _classificationReason = null;
		private string _classifiedBy = null;
		private string _compilationReason = null;
		private DateTime? _dateOfExemptedSource = null;
		private DateTime? _declassDate = null;
		private string _declassEvent = null;
		private string _declassException = null;
		private bool? _declassManualReview = null;
		private string _derivativelyClassifiedBy = null;
		private string _derivedFrom = null;
		private List<string> _displayOnlyTo = null;
		private List<string> _disseminationControls = null;
		private List<string> _FGIsourceOpen = null;
		private List<string> _FGIsourceProtected = null;
		private List<string> _nonICmarkings = null;
		private List<string> _nonUSControls = null;
		private List<string> _ownerProducers = null;
		private List<string> _releasableTo = null;
		private List<string> _SARIdentifier = null;
		private List<string> _SCIcontrols = null;
		private string _typeOfExemptedSource = null;

		/// <summary>
		/// Attribute name </summary>
		public const string ATOMIC_ENERGY_MARKINGS_NAME = "atomicEnergyMarkings";

		/// <summary>
		/// Attribute name </summary>
		public const string CLASSIFICATION_NAME = "classification";

		/// <summary>
		/// Attribute name </summary>
		public const string CLASSIFICATION_REASON_NAME = "classificationReason";

		/// <summary>
		/// Attribute name </summary>
		public const string CLASSIFIED_BY_NAME = "classifiedBy";

		/// <summary>
		/// Attribute name </summary>
		public const string COMPILATION_REASON_NAME = "compilationReason";

		/// <summary>
		/// Attribute name </summary>
		public const string DATE_OF_EXEMPTED_SOURCE_NAME = "dateOfExemptedSource";

		/// <summary>
		/// Attribute name </summary>
		public const string DECLASS_DATE_NAME = "declassDate";

		/// <summary>
		/// Attribute name </summary>
		public const string DECLASS_EVENT_NAME = "declassEvent";

		/// <summary>
		/// Attribute name </summary>
		public const string DECLASS_EXCEPTION_NAME = "declassException";

		/// <summary>
		/// Attribute name </summary>
		public const string DECLASS_MANUAL_REVIEW_NAME = "declassManualReview";

		/// <summary>
		/// Attribute name </summary>
		public const string DERIVATIVELY_CLASSIFIED_BY_NAME = "derivativelyClassifiedBy";

		/// <summary>
		/// Attribute name </summary>
		public const string DERIVED_FROM_NAME = "derivedFrom";

		/// <summary>
		/// Attribute name </summary>
		public const string DISPLAY_ONLY_TO_NAME = "displayOnlyTo";

		/// <summary>
		/// Attribute name </summary>
		public const string DISSEMINATION_CONTROLS_NAME = "disseminationControls";

		/// <summary>
		/// Attribute name </summary>
		public const string FGI_SOURCE_OPEN_NAME = "FGIsourceOpen";

		/// <summary>
		/// Attribute name </summary>
		public const string FGI_SOURCE_PROTECTED_NAME = "FGIsourceProtected";

		/// <summary>
		/// Attribute name </summary>
		public const string NON_IC_MARKINGS_NAME = "nonICmarkings";

		/// <summary>
		/// Attribute name </summary>
		public const string NON_US_CONTROLS_NAME = "nonUSControls";

		/// <summary>
		/// Attribute name </summary>
		public const string OWNER_PRODUCER_NAME = "ownerProducer";

		/// <summary>
		/// Attribute name </summary>
		public const string RELEASABLE_TO_NAME = "releasableTo";

		/// <summary>
		/// Attribute name </summary>
		public const string SAR_IDENTIFIER_NAME = "SARIdentifier";

		/// <summary>
		/// Attribute name </summary>
		public const string SCI_CONTROLS_NAME = "SCIcontrols";

		/// <summary>
		/// Attribute name </summary>
		public const string TYPE_OF_EXEMPTED_SOURCE_NAME = "typeOfExemptedSource";

		private static readonly List<string> ALL_NAMES = new List<string>();
		static SecurityAttributes() {
			ALL_NAMES.Add(ATOMIC_ENERGY_MARKINGS_NAME);
			ALL_NAMES.Add(CLASSIFICATION_NAME);
			ALL_NAMES.Add(CLASSIFICATION_REASON_NAME);
			ALL_NAMES.Add(CLASSIFIED_BY_NAME);
			ALL_NAMES.Add(COMPILATION_REASON_NAME);
			ALL_NAMES.Add(DATE_OF_EXEMPTED_SOURCE_NAME);
			ALL_NAMES.Add(DECLASS_DATE_NAME);
			ALL_NAMES.Add(DECLASS_EVENT_NAME);
			ALL_NAMES.Add(DECLASS_EXCEPTION_NAME);
			ALL_NAMES.Add(DECLASS_MANUAL_REVIEW_NAME);
			ALL_NAMES.Add(DERIVATIVELY_CLASSIFIED_BY_NAME);
			ALL_NAMES.Add(DERIVED_FROM_NAME);
			ALL_NAMES.Add(DISPLAY_ONLY_TO_NAME);
			ALL_NAMES.Add(DISSEMINATION_CONTROLS_NAME);
			ALL_NAMES.Add(FGI_SOURCE_OPEN_NAME);
			ALL_NAMES.Add(FGI_SOURCE_PROTECTED_NAME);
			ALL_NAMES.Add(NON_IC_MARKINGS_NAME);
			ALL_NAMES.Add(NON_US_CONTROLS_NAME);
			ALL_NAMES.Add(OWNER_PRODUCER_NAME);
			ALL_NAMES.Add(RELEASABLE_TO_NAME);
			ALL_NAMES.Add(SAR_IDENTIFIER_NAME);
			ALL_NAMES.Add(SCI_CONTROLS_NAME);
			ALL_NAMES.Add(TYPE_OF_EXEMPTED_SOURCE_NAME);
		}

		/// <summary>
		/// A set of all SecurityAttribute names which should not be converted into ExtensibleAttributes </summary>
		public static readonly List<string> NON_EXTENSIBLE_NAMES = ALL_NAMES;

		/// <summary>
		/// Returns a non-null instance of security attributes. If the instance passed in is not null, it will be returned.
		/// </summary>
		/// <param name="securityAttributes"> the attributes to return by default </param>
		/// <returns> a non-null attributes instance </returns>
		/// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>


		public static SecurityAttributes GetNonNullInstance(SecurityAttributes securityAttributes) {
			return (securityAttributes == null ? new SecurityAttributes(null, null, null) : securityAttributes);
		}

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="element"> the XOM element which is decorated with these attributes. </param>


		public SecurityAttributes(Element element) : base(element.Name.NamespaceName) {
			string icNamespace = DDMSVersion.IsmNamespace;
			_atomicEnergyMarkings = Util.GetXsListAsList(element.Attribute(XName.Get(ATOMIC_ENERGY_MARKINGS_NAME, icNamespace)).Value);
			_classification = element.Attribute(XName.Get(CLASSIFICATION_NAME, icNamespace)).Value;
			_classificationReason = element.Attribute(XName.Get(CLASSIFICATION_REASON_NAME, icNamespace)).Value;
			_classifiedBy = element.Attribute(XName.Get(CLASSIFIED_BY_NAME, icNamespace)).Value;
			_compilationReason = element.Attribute(XName.Get(COMPILATION_REASON_NAME, icNamespace)).Value;
			string dateOfExemptedSource = element.Attribute(XName.Get(DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace)).Value;
			if (!String.IsNullOrEmpty(dateOfExemptedSource)) {
				_dateOfExemptedSource = DateTime.Parse(dateOfExemptedSource);
			}
			string declassDate = element.Attribute(XName.Get(DECLASS_DATE_NAME, icNamespace)).Value;
			if (!String.IsNullOrEmpty(declassDate)) {
				_declassDate = DateTime.Parse(declassDate);
			}
			_declassEvent = element.Attribute(XName.Get(DECLASS_EVENT_NAME, icNamespace)).Value;
			_declassException = element.Attribute(XName.Get(DECLASS_EXCEPTION_NAME, icNamespace)).Value;
			string manualReview = element.Attribute(XName.Get(DECLASS_MANUAL_REVIEW_NAME, icNamespace)).Value;
			if (!String.IsNullOrEmpty(manualReview)) {
				_declassManualReview = Convert.ToBoolean(manualReview);
			}
			_derivativelyClassifiedBy = element.Attribute(XName.Get(DERIVATIVELY_CLASSIFIED_BY_NAME, icNamespace)).Value;
			_derivedFrom = element.Attribute(XName.Get(DERIVED_FROM_NAME, icNamespace)).Value;
            _displayOnlyTo = Util.GetXsListAsList(element.Attribute(XName.Get(DISPLAY_ONLY_TO_NAME, icNamespace)).Value);
            _disseminationControls = Util.GetXsListAsList(element.Attribute(XName.Get(DISSEMINATION_CONTROLS_NAME, icNamespace)).Value);
            _FGIsourceOpen = Util.GetXsListAsList(element.Attribute(XName.Get(FGI_SOURCE_OPEN_NAME, icNamespace)).Value);
            _FGIsourceProtected = Util.GetXsListAsList(element.Attribute(XName.Get(FGI_SOURCE_PROTECTED_NAME, icNamespace)).Value);
            _nonICmarkings = Util.GetXsListAsList(element.Attribute(XName.Get(NON_IC_MARKINGS_NAME, icNamespace)).Value);
            _nonUSControls = Util.GetXsListAsList(element.Attribute(XName.Get(NON_US_CONTROLS_NAME, icNamespace)).Value);
            _ownerProducers = Util.GetXsListAsList(element.Attribute(XName.Get(OWNER_PRODUCER_NAME, icNamespace)).Value);
            _releasableTo = Util.GetXsListAsList(element.Attribute(XName.Get(RELEASABLE_TO_NAME, icNamespace)).Value);
            _SARIdentifier = Util.GetXsListAsList(element.Attribute(XName.Get(SAR_IDENTIFIER_NAME, icNamespace)).Value);
            _SCIcontrols = Util.GetXsListAsList(element.Attribute(XName.Get(SCI_CONTROLS_NAME, icNamespace)).Value);
			_typeOfExemptedSource = element.Attribute(XName.Get(TYPE_OF_EXEMPTED_SOURCE_NAME, icNamespace)).Value;
			Validate();
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// 
		/// <para> The classification and ownerProducer exist as parameters, and any other security markings are passed in as a
		/// mapping of local attribute names to String values. This approach is a compromise between a constructor with over
		/// seventeen parameters, and the added complexity of a step-by-step factory/builder approach. If any name-value
		/// pairing does not correlate with a valid ISM attribute, it will be ignored. </para>
		/// 
		/// <para> If an attribute mapping appears more than once, the last one in the list will be the one used. If
		/// classification and ownerProducer are included in the Map of other attributes, they will be ignored. </para>
		/// </summary>
		/// <param name="classification"> the classification level, which must be a legal classification type (optional) </param>
		/// <param name="ownerProducers"> a list of ownerProducers (optional) </param>
		/// <param name="otherAttributes"> a name/value mapping of other ISM attributes. The value will be a String value, as it
		/// appears in XML. </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public SecurityAttributes(string classification, List<string> ownerProducers, IDictionary<string, string> otherAttributes) : base(DDMSVersion.GetCurrentVersion().Namespace) {
			if (ownerProducers == null) {
				ownerProducers = new List<string>();
			}
			if (otherAttributes == null) {
				otherAttributes = new Dictionary<string,string>();
			}

			_atomicEnergyMarkings = Util.GetXsListAsList(otherAttributes.GetValueOrNull(ATOMIC_ENERGY_MARKINGS_NAME));
			_classification = classification;
			_classificationReason = otherAttributes.GetValueOrNull(CLASSIFICATION_REASON_NAME);
			_classifiedBy = otherAttributes.GetValueOrNull(CLASSIFIED_BY_NAME);
			_compilationReason = otherAttributes.GetValueOrNull(COMPILATION_REASON_NAME);
			string dateOfExemptedSource = otherAttributes.GetValueOrNull(DATE_OF_EXEMPTED_SOURCE_NAME);
			if (!String.IsNullOrEmpty(dateOfExemptedSource)) {
				try {
					_dateOfExemptedSource = DateTime.Parse(dateOfExemptedSource);
				} catch (System.ArgumentException) {
					throw new InvalidDDMSException("The ISM:dateOfExemptedSource attribute is not in a valid date format.");
				}
			}
			string declassDate = otherAttributes.GetValueOrNull(DECLASS_DATE_NAME);
			if (!String.IsNullOrEmpty(declassDate)) {
				try {
					_declassDate = DateTime.Parse(declassDate);
				} catch (System.ArgumentException) {
					throw new InvalidDDMSException("The ISM:declassDate attribute is not in a valid date format.");
				}
			}
			_declassEvent = otherAttributes.GetValueOrNull(DECLASS_EVENT_NAME);
			_declassException = otherAttributes.GetValueOrNull(DECLASS_EXCEPTION_NAME);
			string manualReview = otherAttributes.GetValueOrNull(DECLASS_MANUAL_REVIEW_NAME);
			if (!String.IsNullOrEmpty(manualReview)) {
				_declassManualReview = Convert.ToBoolean(manualReview);
			}
			_derivativelyClassifiedBy = otherAttributes.GetValueOrNull(DERIVATIVELY_CLASSIFIED_BY_NAME);
			_derivedFrom = otherAttributes.GetValueOrNull(DERIVED_FROM_NAME);
			_displayOnlyTo = Util.GetXsListAsList(otherAttributes.GetValueOrNull(DISPLAY_ONLY_TO_NAME));
			_disseminationControls = Util.GetXsListAsList(otherAttributes.GetValueOrNull(DISSEMINATION_CONTROLS_NAME));
			_FGIsourceOpen = Util.GetXsListAsList(otherAttributes.GetValueOrNull(FGI_SOURCE_OPEN_NAME));
			_FGIsourceProtected = Util.GetXsListAsList(otherAttributes.GetValueOrNull(FGI_SOURCE_PROTECTED_NAME));
			_nonICmarkings = Util.GetXsListAsList(otherAttributes.GetValueOrNull(NON_IC_MARKINGS_NAME));
			_nonUSControls = Util.GetXsListAsList(otherAttributes.GetValueOrNull(NON_US_CONTROLS_NAME));
			_ownerProducers = ownerProducers;
			_releasableTo = Util.GetXsListAsList(otherAttributes.GetValueOrNull(RELEASABLE_TO_NAME));
			_SARIdentifier = Util.GetXsListAsList(otherAttributes.GetValueOrNull(SAR_IDENTIFIER_NAME));
			_SCIcontrols = Util.GetXsListAsList(otherAttributes.GetValueOrNull(SCI_CONTROLS_NAME));
			_typeOfExemptedSource = otherAttributes.GetValueOrNull(TYPE_OF_EXEMPTED_SOURCE_NAME);
			Validate();
		}

		/// <summary>
		/// Convenience method to add these attributes onto an existing XOM Element
		/// </summary>
		/// <param name="element"> the element to decorate </param>


		public void AddTo(Element element) {
			DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
			ValidateSameVersion(elementVersion);
			string icNamespace = elementVersion.IsmNamespace;
			string icPrefix = PropertyReader.GetPrefix("ism");

			Util.AddAttribute(element, icPrefix, ATOMIC_ENERGY_MARKINGS_NAME, icNamespace, Util.GetXsList(AtomicEnergyMarkings));
			Util.AddAttribute(element, icPrefix, CLASSIFICATION_NAME, icNamespace, Classification);
			Util.AddAttribute(element, icPrefix, CLASSIFICATION_REASON_NAME, icNamespace, ClassificationReason);
			Util.AddAttribute(element, icPrefix, CLASSIFIED_BY_NAME, icNamespace, ClassifiedBy);
			Util.AddAttribute(element, icPrefix, COMPILATION_REASON_NAME, icNamespace, CompilationReason);
			if (DateOfExemptedSource != null) {
				Util.AddAttribute(element, icPrefix, DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace, DateOfExemptedSource.ToString("o"));
			}
			if (DeclassDate != null) {
				Util.AddAttribute(element, icPrefix, DECLASS_DATE_NAME, icNamespace, DeclassDate.ToString("o"));
			}
			Util.AddAttribute(element, icPrefix, DECLASS_EVENT_NAME, icNamespace, DeclassEvent);
			Util.AddAttribute(element, icPrefix, DECLASS_EXCEPTION_NAME, icNamespace, DeclassException);
			if (DeclassManualReview != null) {
				Util.AddAttribute(element, icPrefix, DECLASS_MANUAL_REVIEW_NAME, icNamespace, DeclassManualReview.ToString());
			}
			Util.AddAttribute(element, icPrefix, DERIVATIVELY_CLASSIFIED_BY_NAME, icNamespace, DerivativelyClassifiedBy);
			Util.AddAttribute(element, icPrefix, DERIVED_FROM_NAME, icNamespace, DerivedFrom);
			Util.AddAttribute(element, icPrefix, DISPLAY_ONLY_TO_NAME, icNamespace, Util.GetXsList(DisplayOnlyTo));
			Util.AddAttribute(element, icPrefix, DISSEMINATION_CONTROLS_NAME, icNamespace, Util.GetXsList(DisseminationControls));
			Util.AddAttribute(element, icPrefix, FGI_SOURCE_OPEN_NAME, icNamespace, Util.GetXsList(FGIsourceOpen));
			Util.AddAttribute(element, icPrefix, FGI_SOURCE_PROTECTED_NAME, icNamespace, Util.GetXsList(FGIsourceProtected));
			Util.AddAttribute(element, icPrefix, NON_IC_MARKINGS_NAME, icNamespace, Util.GetXsList(NonICmarkings));
			Util.AddAttribute(element, icPrefix, NON_US_CONTROLS_NAME, icNamespace, Util.GetXsList(NonUSControls));
			Util.AddAttribute(element, icPrefix, OWNER_PRODUCER_NAME, icNamespace, Util.GetXsList(OwnerProducers));
			Util.AddAttribute(element, icPrefix, RELEASABLE_TO_NAME, icNamespace, Util.GetXsList(ReleasableTo));
			Util.AddAttribute(element, icPrefix, SAR_IDENTIFIER_NAME, icNamespace, Util.GetXsList(SARIdentifier));
			Util.AddAttribute(element, icPrefix, SCI_CONTROLS_NAME, icNamespace, Util.GetXsList(SCIcontrols));
			Util.AddAttribute(element, icPrefix, TYPE_OF_EXEMPTED_SOURCE_NAME, icNamespace, TypeOfExemptedSource);
		}

		/// <summary>
		/// Checks if any attributes have been set.
		/// </summary>
		/// <returns> true if no attributes have values, false otherwise </returns>
		public bool Empty {
			get {
				return (AtomicEnergyMarkings.Count == 0 && String.IsNullOrEmpty(Classification) && String.IsNullOrEmpty(ClassificationReason) && String.IsNullOrEmpty(ClassifiedBy) && String.IsNullOrEmpty(CompilationReason) && DateOfExemptedSource == null && DeclassDate == null && String.IsNullOrEmpty(DeclassEvent) && String.IsNullOrEmpty(DeclassException) && DeclassManualReview == null && String.IsNullOrEmpty(DerivativelyClassifiedBy) && String.IsNullOrEmpty(DerivedFrom) && DisplayOnlyTo.Count == 0 && DisseminationControls.Count == 0 && FGIsourceOpen.Count == 0 && FGIsourceProtected.Count == 0 && NonICmarkings.Count == 0 && NonUSControls.Count == 0 && OwnerProducers.Count == 0 && ReleasableTo.Count == 0 && SARIdentifier.Count == 0 && SCIcontrols.Count == 0 && String.IsNullOrEmpty(TypeOfExemptedSource));
			}
		}

		/// <summary>
		/// Validates the attribute group. Where appropriate the <seealso cref="ISMVocabulary"/> enumerations are validated.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody"> 
		/// <li>The atomicEnergyMarkings cannot be used until DDMS 3.1 or later.</li>
		/// <li>If set, the atomicEnergyMarkings attribute must be valid tokens.</li>
		/// <li>If set, the classification attribute must be a valid token.</li>
		/// <li>The compilationReason attribute cannot be used until DDMS 3.0 or later.</li>
		/// <li>The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0</li>
		/// <li>If set, the dateOfExemptedSource attribute is a valid xs:date value.</li>
		/// <li>If set, the declassDate attribute is a valid xs:date value.</li>
		/// <li>If set, the declassException attribute must be a valid token.</li>
		/// <li>The declassManualReview attribute cannot be used after DDMS 2.0.</li>
		/// <li>The displayOnlyTo attribute cannot be used until DDMS 3.1 or later.</li>
		/// <li>If set, the displayOnlyTo attribute must be valid tokens.</li>
		/// <li>If set, the disseminationControls attribute must be valid tokens.</li>
		/// <li>If set, the FGIsourceOpen attribute must be valid tokens.</li>
		/// <li>If set, the FGIsourceProtected attribute must be valid tokens.</li>
		/// <li>If set, the nonICmarkings attribute must be valid tokens.</li>
		/// <li>The nonUSControls attribute cannot be used until DDMS 3.1 or later.</li>
		/// <li>If set, the nonUSControls attribute must be valid tokens.</li>
		/// <li>If set, the ownerProducers attribute must be valid tokens.</li>
		/// <li>If set, the releasableTo attribute must be valid tokens.</li>
		/// <li>If set, the SARIdentifiers attribute must be valid tokens.</li>
		/// <li>If set, the SCIcontrols attribute must be valid tokens.</li>
		/// <li>The typeOfExemptedSource attribute can only be used in DDMS 2.0 or DDMS 3.0.</li>
		/// <li>If set, the typeOfExemptedSource attribute must be a valid token.</li>
		/// <li>Does NOT do any validation on the constraints described in the DES ISM specification.</li>
		/// </td></tr></table>
		/// </summary>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			// Should be reviewed as additional versions of DDMS are supported.
			DDMSVersion version = DDMSVersion;
			bool isDDMS20 = "2.0".Equals(version.Version);

			if (!version.IsAtLeast("3.1") && AtomicEnergyMarkings.Count > 0) {
				throw new InvalidDDMSException("The atomicEnergyMarkings attribute cannot be used until DDMS 3.1 or later.");
			}
			foreach (string atomic in AtomicEnergyMarkings) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_ATOMIC_ENERGY_MARKINGS, atomic);
			}
			if (!String.IsNullOrEmpty(Classification)) {
				if (version.IsAtLeast("3.0") || !ISMVocabulary.UsingOldClassification(Classification)) {
					ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_ALL_CLASSIFICATIONS, Classification);
				}
			}
			if (!version.IsAtLeast("3.0") && !String.IsNullOrEmpty(CompilationReason)) {
				throw new InvalidDDMSException("The compilationReason attribute cannot be used until DDMS 3.0 or later.");
			}
			if (version.IsAtLeast("3.1") && DateOfExemptedSource != null) {
				throw new InvalidDDMSException("The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0.");
			}
			if (DateOfExemptedSource != null) {
				throw new InvalidDDMSException("The dateOfExemptedSource attribute must be in the xs:date format (YYYY-MM-DD).");
			}
			if (DeclassDate != null) {
				throw new InvalidDDMSException("The declassDate must be in the xs:date format (YYYY-MM-DD).");
			}
			if (!String.IsNullOrEmpty(DeclassException)) {
				if (isDDMS20) {
					// In DDMS 2.0, this can be a list of tokens.
					foreach (string value in Util.GetXsListAsList(DeclassException)) {
						ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DECLASS_EXCEPTION, value);
					}
				} else {
					ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DECLASS_EXCEPTION, DeclassException);
				}
			}
			if (!isDDMS20 && DeclassManualReview != null) {
				throw new InvalidDDMSException("The declassManualReview attribute can only be used in DDMS 2.0.");
			}
			if (!version.IsAtLeast("3.1") && DisplayOnlyTo.Count > 0) {
				throw new InvalidDDMSException("The displayOnlyTo attribute cannot be used until DDMS 3.1 or later.");
			}
			foreach (string display in DisplayOnlyTo) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DISPLAY_ONLY_TO, display);
			}
			foreach (string dissemination in DisseminationControls) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, dissemination);
			}
			foreach (string fgiSourceOpen in FGIsourceOpen) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_FGI_SOURCE_OPEN, fgiSourceOpen);
			}
			foreach (string fgiSourceProtected in FGIsourceProtected) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_FGI_SOURCE_PROTECTED, fgiSourceProtected);
			}
			foreach (string nonIC in NonICmarkings) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NON_IC_MARKINGS, nonIC);
			}
			if (!version.IsAtLeast("3.1") && NonUSControls.Count > 0) {
				throw new InvalidDDMSException("The nonUSControls attribute cannot be used until DDMS 3.1 or later.");
			}
			foreach (string nonUS in NonUSControls) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NON_US_CONTROLS, nonUS);
			}
			foreach (string op in OwnerProducers) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_OWNER_PRODUCERS, op);
			}
			foreach (string releasableTo in ReleasableTo) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_RELEASABLE_TO, releasableTo);
			}
			foreach (string sarId in SARIdentifier) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_SAR_IDENTIFIER, sarId);
			}
			foreach (string sciControls in SCIcontrols) {
				ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_SCI_CONTROLS, sciControls);
			}
			if (!String.IsNullOrEmpty(TypeOfExemptedSource)) {
				if (isDDMS20) {
					// In DDMS 2.0, this can be a list of tokens.
					foreach (string value in Util.GetXsListAsList(TypeOfExemptedSource)) {
						ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, value);
					}
				} else if ("3.0".Equals(version.Version)) {
					ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, TypeOfExemptedSource);
				} else {
					throw new InvalidDDMSException("The typeOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0.");
				}
			}
			base.Validate();
		}

		/// <summary>
		/// Standalone validation method for components which require a classification and ownerProducer.
		/// </summary>
		/// <exception cref="InvalidDDMSException"> if there is no classification. </exception>


		public void RequireClassification() {
			Util.RequireDDMSValue(CLASSIFICATION_NAME, Classification);
			if (OwnerProducers.Count == 0) {
				throw new InvalidDDMSException("At least 1 ownerProducer must be set.");
			}
		}

		/// <seealso cref= AbstractAttributeGroup#getOutput(boolean, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix) {
			string localPrefix = Util.GetNonNullString(prefix);
			StringBuilder text = new StringBuilder();
			text.Append(Resource.BuildOutput(isHTML, localPrefix + ATOMIC_ENERGY_MARKINGS_NAME, Util.GetXsList(AtomicEnergyMarkings)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + CLASSIFICATION_NAME, Classification));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + CLASSIFICATION_REASON_NAME, ClassificationReason));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + CLASSIFIED_BY_NAME, ClassifiedBy));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + COMPILATION_REASON_NAME, CompilationReason));
			if (DateOfExemptedSource != null) {
				text.Append(Resource.BuildOutput(isHTML, localPrefix + DATE_OF_EXEMPTED_SOURCE_NAME, DateOfExemptedSource.ToString("o")));
			}
			if (DeclassDate != null) {
				text.Append(Resource.BuildOutput(isHTML, localPrefix + DECLASS_DATE_NAME, DeclassDate.ToString("o")));
			}
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DECLASS_EVENT_NAME, DeclassEvent));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DECLASS_EXCEPTION_NAME, DeclassException));
			if (DeclassManualReview != null) {
				text.Append(Resource.BuildOutput(isHTML, localPrefix + DECLASS_MANUAL_REVIEW_NAME, DeclassManualReview.ToString()));
			}
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DERIVATIVELY_CLASSIFIED_BY_NAME, DerivativelyClassifiedBy));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DERIVED_FROM_NAME, DerivedFrom));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DISPLAY_ONLY_TO_NAME, Util.GetXsList(DisplayOnlyTo)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + DISSEMINATION_CONTROLS_NAME, Util.GetXsList(DisseminationControls)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + FGI_SOURCE_OPEN_NAME, Util.GetXsList(FGIsourceOpen)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + FGI_SOURCE_PROTECTED_NAME, Util.GetXsList(FGIsourceProtected)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + NON_IC_MARKINGS_NAME, Util.GetXsList(NonICmarkings)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + NON_US_CONTROLS_NAME, Util.GetXsList(NonUSControls)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + OWNER_PRODUCER_NAME, Util.GetXsList(OwnerProducers)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + RELEASABLE_TO_NAME, Util.GetXsList(ReleasableTo)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + SAR_IDENTIFIER_NAME, Util.GetXsList(SARIdentifier)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + SCI_CONTROLS_NAME, Util.GetXsList(SCIcontrols)));
			text.Append(Resource.BuildOutput(isHTML, localPrefix + TYPE_OF_EXEMPTED_SOURCE_NAME, TypeOfExemptedSource));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!(obj is SecurityAttributes)) {
				return (false);
			}
			SecurityAttributes test = (SecurityAttributes) obj;
			return (Util.ListEquals(AtomicEnergyMarkings, test.AtomicEnergyMarkings) && Classification.Equals(test.Classification) && ClassificationReason.Equals(test.ClassificationReason) && ClassifiedBy.Equals(test.ClassifiedBy) && CompilationReason.Equals(test.CompilationReason) && Util.NullEquals(DateOfExemptedSource, test.DateOfExemptedSource) && Util.NullEquals(DeclassDate, test.DeclassDate) && DeclassEvent.Equals(test.DeclassEvent) && DeclassException.Equals(test.DeclassException) && Util.NullEquals(DeclassManualReview, test.DeclassManualReview) && DerivativelyClassifiedBy.Equals(test.DerivativelyClassifiedBy) && DerivedFrom.Equals(test.DerivedFrom) && Util.ListEquals(DisplayOnlyTo, test.DisplayOnlyTo) && Util.ListEquals(DisseminationControls, test.DisseminationControls) && Util.ListEquals(FGIsourceOpen, test.FGIsourceOpen) && Util.ListEquals(FGIsourceProtected, test.FGIsourceProtected) && Util.ListEquals(NonICmarkings, test.NonICmarkings) && Util.ListEquals(NonUSControls, test.NonUSControls) && Util.ListEquals(OwnerProducers, test.OwnerProducers) && Util.ListEquals(ReleasableTo, test.ReleasableTo) && Util.ListEquals(SARIdentifier, test.SARIdentifier) && Util.ListEquals(SCIcontrols, test.SCIcontrols) && TypeOfExemptedSource.Equals(test.TypeOfExemptedSource));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = 0;
			result = 7 * result + AtomicEnergyMarkings.GetHashCode();
			result = 7 * result + Classification.GetHashCode();
			result = 7 * result + ClassificationReason.GetHashCode();
			result = 7 * result + ClassifiedBy.GetHashCode();
			result = 7 * result + CompilationReason.GetHashCode();
			if (DateOfExemptedSource != null) {
				result = 7 * result + DateOfExemptedSource.GetHashCode();
			}
			if (DeclassDate != null) {
				result = 7 * result + DeclassDate.GetHashCode();
			}
			result = 7 * result + DeclassEvent.GetHashCode();
			result = 7 * result + DeclassException.GetHashCode();
			if (DeclassManualReview != null) {
				result = 7 * result + DeclassManualReview.GetHashCode();
			}
			result = 7 * result + DerivativelyClassifiedBy.GetHashCode();
			result = 7 * result + DerivedFrom.GetHashCode();
			result = 7 * result + DisplayOnlyTo.GetHashCode();
			result = 7 * result + DisseminationControls.GetHashCode();
			result = 7 * result + FGIsourceOpen.GetHashCode();
			result = 7 * result + FGIsourceProtected.GetHashCode();
			result = 7 * result + NonICmarkings.GetHashCode();
			result = 7 * result + NonUSControls.GetHashCode();
			result = 7 * result + OwnerProducers.GetHashCode();
			result = 7 * result + ReleasableTo.GetHashCode();
			result = 7 * result + SARIdentifier.GetHashCode();
			result = 7 * result + SCIcontrols.GetHashCode();
			result = 7 * result + TypeOfExemptedSource.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the atomicEnergyMarkings attribute. Returns a copy.
		/// </summary>
		public List<string> AtomicEnergyMarkings {
			get {
				return _atomicEnergyMarkings;
			}
			set {
                _atomicEnergyMarkings = value; ;
			}
		}

		/// <summary>
		/// Accessor for the classification attribute.
		/// </summary>
		public string Classification {
			get {
				return (Util.GetNonNullString(_classification));
			}
			set {
                _classification = value;
			}
		}

		/// <summary>
		/// Accessor for the classificationReason attribute.
		/// </summary>
		public string ClassificationReason {
			get {
				return (Util.GetNonNullString(_classificationReason));
			}
			set {
                _classificationReason = value;
			}
		}

		/// <summary>
		/// Accessor for the classifiedBy attribute.
		/// </summary>
		public string ClassifiedBy {
			get {
				return (Util.GetNonNullString(_classifiedBy));
			}
			set {
                _classifiedBy = value;
			}
		}

		/// <summary>
		/// Accessor for the compilationReason attribute.
		/// </summary>
		public string CompilationReason {
			get {
				return (Util.GetNonNullString(_compilationReason));
			}
			set {
                _compilationReason = value;
			}
		}

		/// <summary>
		/// Accessor for the dateOfExemptedSource attribute. May return null if not set.
		/// </summary>
		public DateTime DateOfExemptedSource {
			get {
				return DateTime.Parse(_dateOfExemptedSource.Value.ToString("o"));
			}
			set {
                _dateOfExemptedSource = value;
			}
		}

		/// <summary>
		/// Accessor for the declassDate attribute. May return null if not set.
		/// </summary>
		public DateTime DeclassDate {
			get {
				return DateTime.Parse(_declassDate.Value.ToString("o"));
			}
			set {
                _declassDate = value;
			}
		}

		/// <summary>
		/// Accessor for the declassEvent attribute.
		/// </summary>
		public string DeclassEvent {
			get {
				return (Util.GetNonNullString(_declassEvent));
			}
			set {
                _declassEvent = value;
			}
		}

		/// <summary>
		/// Accessor for the declassException attribute. In DDMS 2.0, this could be a list of tokens. This is represented
		/// here as a space-delimited string.
		/// </summary>
		public string DeclassException {
			get {
				return (Util.GetNonNullString(_declassException));
			}
			set {
                _declassException = value;
			}
		}

		/// <summary>
		/// Accessor for the declassManualReview attribute. Will be null in DDMS 3.0.
		/// </summary>
		public bool? DeclassManualReview {
			get {
				return (_declassManualReview);
			}
			set {
					_declassManualReview = value;
			}
		}

		/// <summary>
		/// Accessor for the derivativelyClassifiedBy attribute.
		/// </summary>
		public string DerivativelyClassifiedBy {
			get {
				return (Util.GetNonNullString(_derivativelyClassifiedBy));
			}
			set {
                _derivativelyClassifiedBy = value;
			}
		}

		/// <summary>
		/// Accessor for the derivedFrom attribute.
		/// </summary>
		public string DerivedFrom {
			get {
				return (Util.GetNonNullString(_derivedFrom));
			}
			set {
                _derivedFrom = value;
			}
		}

		/// <summary>
		/// Accessor for the displayOnlyTo attribute. Returns a copy.
		/// </summary>
		public List<string> DisplayOnlyTo {
			get {
				return _displayOnlyTo;
			}
			set {
                _displayOnlyTo = value;
			}
		}

		/// <summary>
		/// Accessor for the disseminationControls attribute. Returns a copy.
		/// </summary>
		public List<string> DisseminationControls {
			get {
				return _disseminationControls;
			}
			set {
                _disseminationControls = value;
			}
		}

		/// <summary>
		/// Accessor for the FGIsourceOpen attribute. Returns a copy.
		/// </summary>
		public List<string> FGIsourceOpen {
			get {
				return _FGIsourceOpen;
			}
			set {
                _FGIsourceOpen = value;
			}
		}

		/// <summary>
		/// Accessor for the FGIsourceProtected attribute. Returns a copy.
		/// </summary>
		public List<string> FGIsourceProtected {
			get {
				return _FGIsourceProtected;
			}
			set {
                _FGIsourceProtected = value;
			}
		}

		/// <summary>
		/// Accessor for the nonICmarkings attribute. Returns a copy.
		/// </summary>
		public List<string> NonICmarkings {
			get {
				return _nonICmarkings;
			}
			set {
                _nonICmarkings = value;
			}
		}

		/// <summary>
		/// Accessor for the nonUSControls attribute. Returns a copy.
		/// </summary>
		public List<string> NonUSControls {
			get {
				return _nonUSControls;
			}
			set {
                _nonUSControls = value;
			}
		}

		/// <summary>
		/// Accessor for the ownerProducers attribute. Returns a copy.
		/// </summary>
		public List<string> OwnerProducers {
			get {
				return _ownerProducers;
			}
			set {
                _ownerProducers = value;
			}
		}

		/// <summary>
		/// Accessor for the releasableTo attribute. Returns a copy.
		/// </summary>
		public List<string> ReleasableTo {
			get {
				return _releasableTo;
			}
			set {
                _releasableTo = value;
			}
		}

		/// <summary>
		/// Accessor for the SARIdentifier attribute. Returns a copy.
		/// </summary>
		public List<string> SARIdentifier {
			get {
				return _SARIdentifier;
			}
			set {
                _SARIdentifier = value;
			}
		}

		/// <summary>
		/// Accessor for the SCIcontrols attribute. Returns a copy.
		/// </summary>
		public List<string> SCIcontrols {
			get {
				return _SCIcontrols;
			}
			set {
                _SCIcontrols = value;
			}
		}

		/// <summary>
		/// Accessor for the typeOfExemptedSource attribute. In DDMS 2.0, this could be a list of tokens. This is represented
		/// here as a space-delimited string.
		/// </summary>
		public string TypeOfExemptedSource {
			get {
				return (Util.GetNonNullString(_typeOfExemptedSource));
			}
			set {
                _typeOfExemptedSource = value;
			}
		}

		/// <summary>
		/// Builder for these attributes.
		/// 
		/// <para>This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
		/// standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
		/// null.
		/// 
		/// </para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder {
			internal const long SerialVersionUID = 279072341662308051L;

			internal IDictionary<string, string> _stringAttributes = new Dictionary<string, string>();
			internal IDictionary<string, List<string>> _listAttributes = new Dictionary<string, List<string>>();
			internal bool? _declassManualReview = null;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(SecurityAttributes attributes) {
				AtomicEnergyMarkings = attributes.AtomicEnergyMarkings;
				Classification = attributes.Classification;
				ClassificationReason = attributes.ClassificationReason;
				ClassifiedBy = attributes.ClassifiedBy;
				CompilationReason = attributes.CompilationReason;
				if (attributes.DateOfExemptedSource != null) {
					DateOfExemptedSource = attributes.DateOfExemptedSource.ToString("o");
				}
				if (attributes.DeclassDate != null) {
					DeclassDate = attributes.DeclassDate.ToString("o");
				}
				DeclassEvent = attributes.DeclassEvent;
				DeclassException = attributes.DeclassException;
				if (attributes.DeclassManualReview != null) {
					DeclassManualReview = attributes.DeclassManualReview;
				}
				DerivativelyClassifiedBy = attributes.DerivativelyClassifiedBy;
				DerivedFrom = attributes.DerivedFrom;
				DisplayOnlyTo = attributes.DisplayOnlyTo;
				DisseminationControls = attributes.DisseminationControls;
				FGIsourceOpen = attributes.FGIsourceOpen;
				FGIsourceProtected = attributes.FGIsourceProtected;
				NonICmarkings = attributes.NonICmarkings;
				NonUSControls = attributes.NonUSControls;
				OwnerProducers = attributes.OwnerProducers;
				ReleasableTo = attributes.ReleasableTo;
				SARIdentifier = attributes.SARIdentifier;
				SCIcontrols = attributes.SCIcontrols;
				TypeOfExemptedSource = attributes.TypeOfExemptedSource;
			}

			/// <summary>
			/// Finalizes the data gathered for this builder instance. Will always return an empty instance instead of a null
			/// one.
			/// </summary>
			/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


			public virtual SecurityAttributes Commit() {
				IDictionary<string, string> otherAttributes = new Dictionary<string, string>();
				otherAttributes[ATOMIC_ENERGY_MARKINGS_NAME] = Util.GetXsList(AtomicEnergyMarkings);
				otherAttributes[CLASSIFICATION_REASON_NAME] = ClassificationReason;
				otherAttributes[CLASSIFIED_BY_NAME] = ClassifiedBy;
				otherAttributes[COMPILATION_REASON_NAME] = CompilationReason;
				otherAttributes[DATE_OF_EXEMPTED_SOURCE_NAME] = DateOfExemptedSource;
				otherAttributes[DECLASS_DATE_NAME] = DeclassDate;
				otherAttributes[DECLASS_EVENT_NAME] = DeclassEvent;
				otherAttributes[DECLASS_EXCEPTION_NAME] = DeclassException;
				if (DeclassManualReview != null) {
					otherAttributes[DECLASS_MANUAL_REVIEW_NAME] = DeclassManualReview.ToString();
				}
				otherAttributes[DERIVATIVELY_CLASSIFIED_BY_NAME] = DerivativelyClassifiedBy;
				otherAttributes[DERIVED_FROM_NAME] = DerivedFrom;
				otherAttributes[DISPLAY_ONLY_TO_NAME] = Util.GetXsList(DisplayOnlyTo);
				otherAttributes[DISSEMINATION_CONTROLS_NAME] = Util.GetXsList(DisseminationControls);
				otherAttributes[FGI_SOURCE_OPEN_NAME] = Util.GetXsList(FGIsourceOpen);
				otherAttributes[FGI_SOURCE_PROTECTED_NAME] = Util.GetXsList(FGIsourceProtected);
				otherAttributes[NON_IC_MARKINGS_NAME] = Util.GetXsList(NonICmarkings);
				otherAttributes[NON_US_CONTROLS_NAME] = Util.GetXsList(NonUSControls);
				otherAttributes[RELEASABLE_TO_NAME] = Util.GetXsList(ReleasableTo);
				otherAttributes[SAR_IDENTIFIER_NAME] = Util.GetXsList(SARIdentifier);
				otherAttributes[SCI_CONTROLS_NAME] = Util.GetXsList(SCIcontrols);
				otherAttributes[TYPE_OF_EXEMPTED_SOURCE_NAME] = TypeOfExemptedSource;
				return (new SecurityAttributes(Classification, OwnerProducers, otherAttributes));
			}

			/// <summary>
			/// Checks if any values have been provided for this Builder.
			/// </summary>
			/// <returns> true if every field is empty </returns>
			public virtual bool Empty {
				get {
					bool isEmpty = true;
					foreach (string value in StringAttributes.Values) {
						isEmpty = isEmpty && String.IsNullOrEmpty(value);
					}
					foreach (List<string> list in ListAttributes.Values) {
						isEmpty = isEmpty && Util.ContainsOnlyEmptyValues(list);
					}
					return (isEmpty && DeclassManualReview == null);
				}
			}

			/// <summary>
			/// Builder accessor for the atomicEnergyMarkings attribute
			/// </summary>
			public virtual List<string> AtomicEnergyMarkings {
				get {
					return (GetListAttribute(ATOMIC_ENERGY_MARKINGS_NAME));
				}
                set { SetListAttribute(ATOMIC_ENERGY_MARKINGS_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the classification attribute
			/// </summary>
			public virtual string Classification {
				get {
					return (StringAttributes.GetValueOrNull(CLASSIFICATION_NAME));
				}
                set { StringAttributes[CLASSIFICATION_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the classificationReason attribute
			/// </summary>
			public virtual string ClassificationReason {
				get {
					return (StringAttributes.GetValueOrNull(CLASSIFICATION_REASON_NAME));
				}
                set { StringAttributes[CLASSIFICATION_REASON_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the classifiedBy attribute
			/// </summary>
			public virtual string ClassifiedBy {
				get {
					return (StringAttributes.GetValueOrNull(CLASSIFIED_BY_NAME));
				}
                set { StringAttributes[CLASSIFIED_BY_NAME] =value; }
			}


			/// <summary>
			/// Builder accessor for the compilationReason attribute
			/// </summary>
			public virtual string CompilationReason {
				get {
					return (StringAttributes.GetValueOrNull(COMPILATION_REASON_NAME));
				}
                set { StringAttributes[COMPILATION_REASON_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the dateOfExemptedSource attribute
			/// </summary>
			public virtual string DateOfExemptedSource {
				get {
					return (StringAttributes.GetValueOrNull(DATE_OF_EXEMPTED_SOURCE_NAME));
				}
                set { StringAttributes[DATE_OF_EXEMPTED_SOURCE_NAME]=value; }
			}


			/// <summary>
			/// Builder accessor for the declassDate attribute
			/// </summary>
			public virtual string DeclassDate {
				get {
					return (StringAttributes.GetValueOrNull(DECLASS_DATE_NAME));
				}
                set { StringAttributes[DECLASS_DATE_NAME]=value; }
			}


			/// <summary>
			/// Builder accessor for the declassEvent attribute
			/// </summary>
			public virtual string DeclassEvent {
				get {
					return (StringAttributes.GetValueOrNull(DECLASS_EVENT_NAME));
				}
                set { StringAttributes[DECLASS_EVENT_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the declassException attribute
			/// </summary>
			public virtual string DeclassException {
				get {
					return (StringAttributes.GetValueOrNull(DECLASS_EXCEPTION_NAME));
				}
                set { StringAttributes[DECLASS_EXCEPTION_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the declassManualReview attribute
			/// </summary>
			public virtual bool? DeclassManualReview {
				get {
					return _declassManualReview;
				}
                set { _declassManualReview = value; }
			}


			/// <summary>
			/// Builder accessor for the derivativelyClassifiedBy attribute
			/// </summary>
			public virtual string DerivativelyClassifiedBy {
				get {
					return (StringAttributes.GetValueOrNull(DERIVATIVELY_CLASSIFIED_BY_NAME));
				}
                set { StringAttributes[DERIVATIVELY_CLASSIFIED_BY_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the derivedFrom attribute
			/// </summary>
			public virtual string DerivedFrom {
				get {
					return (StringAttributes.GetValueOrNull(DERIVED_FROM_NAME));
				}
                set { StringAttributes[DERIVED_FROM_NAME] = value; }
			}


			/// <summary>
			/// Builder accessor for the displayOnlyTo attribute
			/// </summary>
			public virtual List<string> DisplayOnlyTo {
				get {
					return (GetListAttribute(DISPLAY_ONLY_TO_NAME));
				}
                set { SetListAttribute(DISPLAY_ONLY_TO_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the disseminationControls attribute
			/// </summary>
			public virtual List<string> DisseminationControls {
				get {
					return (GetListAttribute(DISSEMINATION_CONTROLS_NAME));
				}
                set { SetListAttribute(DISSEMINATION_CONTROLS_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the FGIsourceOpen attribute
			/// </summary>
			public virtual List<string> FGIsourceOpen {
				get {
					return (GetListAttribute(FGI_SOURCE_OPEN_NAME));
				}
                set { SetListAttribute(FGI_SOURCE_OPEN_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the FGIsourceProtected attribute
			/// </summary>
			public virtual List<string> FGIsourceProtected {
				get {
					return (GetListAttribute(FGI_SOURCE_PROTECTED_NAME));
				}
                set { SetListAttribute(FGI_SOURCE_PROTECTED_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the nonICmarkings attribute
			/// </summary>
			public virtual List<string> NonICmarkings {
				get {
					return (GetListAttribute(NON_IC_MARKINGS_NAME));
				}
                set { SetListAttribute(NON_IC_MARKINGS_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the nonUSControls attribute
			/// </summary>
			public virtual List<string> NonUSControls {
				get {
					return (GetListAttribute(NON_US_CONTROLS_NAME));
				}
                set { SetListAttribute(NON_US_CONTROLS_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the ownerProducers attribute
			/// </summary>
			public virtual List<string> OwnerProducers {
				get {
					return (GetListAttribute(OWNER_PRODUCER_NAME));
				}
                set { SetListAttribute(OWNER_PRODUCER_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the releasableTo attribute
			/// </summary>
			public virtual List<string> ReleasableTo {
				get {
					return (GetListAttribute(RELEASABLE_TO_NAME));
				}
                set { SetListAttribute(RELEASABLE_TO_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the SARIdentifier attribute
			/// </summary>
			public virtual List<string> SARIdentifier {
				get {
					return (GetListAttribute(SAR_IDENTIFIER_NAME));
				}
                set { SetListAttribute(SAR_IDENTIFIER_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the SCIcontrols attribute
			/// </summary>
			public virtual List<string> SCIcontrols {
				get {
					return (GetListAttribute(SCI_CONTROLS_NAME));
				}
                set { SetListAttribute(SCI_CONTROLS_NAME, value); }
			}


			/// <summary>
			/// Builder accessor for the typeOfExemptedSource attribute
			/// </summary>
			public virtual string TypeOfExemptedSource {
				get {
					return (StringAttributes.GetValueOrNull(TYPE_OF_EXEMPTED_SOURCE_NAME));
				}
                set { StringAttributes[TYPE_OF_EXEMPTED_SOURCE_NAME] = value; }
			}


			/// <summary>
			/// Helper method to look up a key in the map of attribute. Lazily creates a new list if null.
			/// </summary>
			/// <param name="key"> the attribute name </param>
			/// <returns> the list of strings mapped to that attribute name </returns>
			internal virtual List<string> GetListAttribute(string key) {
				if (ListAttributes.GetValueOrNull(key) == null) {
                    ListAttributes[key] = new List<string>();
				}
				return (ListAttributes.GetValueOrNull(key));
			}

			/// <summary>
			/// Helper method to initialize a new lazy list for some attribute.
			/// </summary>
			/// <param name="key"> the attribute name </param>
			/// <param name="value"> the list to save, which will be wrapped in a lazy list </param>
			internal virtual void SetListAttribute(string key, List<string> value) {
                ListAttributes[key] = value;
			}

			/// <summary>
			/// Accessor for the map of attribute names to list values
			/// </summary>
			internal virtual IDictionary<string, List<string>> ListAttributes {
				get {
					return (_listAttributes);
				}
			}

			/// <summary>
			/// Accessor for the map of attribute names to string values
			/// </summary>
			internal virtual IDictionary<string, string> StringAttributes {
				get {
					return (_stringAttributes);
				}
			}
		}
	}
}