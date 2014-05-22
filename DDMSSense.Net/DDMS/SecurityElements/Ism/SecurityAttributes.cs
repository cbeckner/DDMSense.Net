#region usings

using DDMSense.Extensions;
using DDMSense.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

#endregion usings

namespace DDMSense.DDMS.SecurityElements.Ism
{
    /// <summary>
    ///     Attribute group for the ISM markings used throughout DDMS.
    ///     <para>
    ///         When validating this attribute group, the required/optional nature of the classification and
    ///         ownerProducer attributes are not checked. Because that limitation depends on the parent element (for example,
    ///         ddms:title requires them, but ddms:creator does not), the parent element should be responsible for checking,
    ///         via
    ///         <code>requireClassification()</code>.
    ///     </para>
    ///     <para>
    ///         At this time, logical validation is only done on the data types of the various attributes, and the controlled
    ///         vocabulary enumerations behind some of the attributes. Any further validation would require integration
    ///         with ISM Schematron files as discussed in the Schematron Validation Power Tip on the website.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ISM:atomicEnergyMarkings</u>: (optional, starting in DDMS 3.1)<br />
    ///                 <u>ISM:classification</u>: (optional)<br />
    ///                 <u>ISM:classificationReason</u>: (optional)<br />
    ///                 <u>ISM:classifiedBy</u>: (optional)<br />
    ///                 <u>ISM:compilationReason</u>: (optional, starting in DDMS 3.0)<br />
    ///                 <u>ISM:dateOfExemptedSource</u>: (optional, DDMS 2.0 and 3.0 only)<br />
    ///                 <u>ISM:declassDate</u>: (optional)<br />
    ///                 <u>ISM:declassEvent</u>: (optional)<br />
    ///                 <u>ISM:declassException</u>: (optional)<br />
    ///                 <u>ISM:declassManualReview</u>: (optional, DDMS 2.0 only)<br />
    ///                 <u>ISM:derivativelyClassifiedBy</u>: (optional)<br />
    ///                 <u>ISM:derivedFrom</u>: (optional)<br />
    ///                 <u>ISM:displayOnlyTo</u>: (optional, starting in DDMS 3.1)<br />
    ///                 <u>ISM:disseminationControls</u>: (optional)<br />
    ///                 <u>ISM:FGIsourceOpen</u>: (optional)<br />
    ///                 <u>ISM:FGIsourceProtected</u>: (optional)<br />
    ///                 <u>ISM:nonICmarkings</u>: (optional)<br />
    ///                 <u>ISM:nonUSControls</u>: (optional, starting in DDMS 3.1)<br />
    ///                 <u>ISM:ownerProducer</u>: (optional)<br />
    ///                 <u>ISM:releasableTo</u>: (optional)<br />
    ///                 <u>ISM:SARIdentifier</u>: (optional)<br />
    ///                 <u>ISM:SCIcontrols</u>: (optional)<br />
    ///                 <u>ISM:typeOfExemptedSource</u>: (optional, DDMS 2.0 and 3.0 only)<br />
    ///             </td>
    ///         </tr>
    ///     </table>

    /// </summary>
    public sealed class SecurityAttributes : AbstractAttributeGroup
    {
        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string ATOMIC_ENERGY_MARKINGS_NAME = "atomicEnergyMarkings";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string CLASSIFICATION_NAME = "classification";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string CLASSIFICATION_REASON_NAME = "classificationReason";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string CLASSIFIED_BY_NAME = "classifiedBy";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string COMPILATION_REASON_NAME = "compilationReason";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DATE_OF_EXEMPTED_SOURCE_NAME = "dateOfExemptedSource";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DECLASS_DATE_NAME = "declassDate";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DECLASS_EVENT_NAME = "declassEvent";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DECLASS_EXCEPTION_NAME = "declassException";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DECLASS_MANUAL_REVIEW_NAME = "declassManualReview";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DERIVATIVELY_CLASSIFIED_BY_NAME = "derivativelyClassifiedBy";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DERIVED_FROM_NAME = "derivedFrom";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DISPLAY_ONLY_TO_NAME = "displayOnlyTo";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string DISSEMINATION_CONTROLS_NAME = "disseminationControls";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string FGI_SOURCE_OPEN_NAME = "FGIsourceOpen";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string FGI_SOURCE_PROTECTED_NAME = "FGIsourceProtected";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string NON_IC_MARKINGS_NAME = "nonICmarkings";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string NON_US_CONTROLS_NAME = "nonUSControls";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string OWNER_PRODUCER_NAME = "ownerProducer";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string RELEASABLE_TO_NAME = "releasableTo";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string SAR_IDENTIFIER_NAME = "SARIdentifier";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string SCI_CONTROLS_NAME = "SCIcontrols";

        /// <summary>
        ///     Attribute name
        /// </summary>
        public const string TYPE_OF_EXEMPTED_SOURCE_NAME = "typeOfExemptedSource";

        private static readonly List<string> ALL_NAMES = new List<string>();

        /// <summary>
        ///     A set of all SecurityAttribute names which should not be converted into ExtensibleAttributes
        /// </summary>
        public static readonly List<string> NON_EXTENSIBLE_NAMES = ALL_NAMES;

        private void Initialize()
        {
            AtomicEnergyMarkings = new List<string>();
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
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element which is decorated with these attributes. </param>
        public SecurityAttributes(XElement element)
            : base(element.Name.NamespaceName)
        {
            Initialize();
            string icNamespace = DDMSVersion.IsmNamespace;
            AtomicEnergyMarkings =
                Util.Util.GetXsListAsList(element.Attribute(XName.Get(ATOMIC_ENERGY_MARKINGS_NAME, icNamespace)).ToNonNullString());
            Classification = element.Attribute(XName.Get(CLASSIFICATION_NAME, icNamespace)).ToNonNullString();
            ClassificationReason = element.Attribute(XName.Get(CLASSIFICATION_REASON_NAME, icNamespace)).ToNonNullString();
            ClassifiedBy = element.Attribute(XName.Get(CLASSIFIED_BY_NAME, icNamespace)).ToNonNullString();
            CompilationReason = element.Attribute(XName.Get(COMPILATION_REASON_NAME, icNamespace)).ToNonNullString();
            string dateOfExemptedSource = element.Attribute(XName.Get(DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace)).ToNonNullString();
            if (!String.IsNullOrEmpty(dateOfExemptedSource))
                DateOfExemptedSource = DateTime.Parse(dateOfExemptedSource);

            string declassDate = element.Attribute(XName.Get(DECLASS_DATE_NAME, icNamespace)).ToNonNullString();
            if (!String.IsNullOrEmpty(declassDate))
                DeclassDate = DateTime.Parse(declassDate);

            DeclassEvent = element.Attribute(XName.Get(DECLASS_EVENT_NAME, icNamespace)).ToNonNullString();
            DeclassException = element.Attribute(XName.Get(DECLASS_EXCEPTION_NAME, icNamespace)).ToNonNullString();
            string manualReview = element.Attribute(XName.Get(DECLASS_MANUAL_REVIEW_NAME, icNamespace)).ToNonNullString();
            if (!String.IsNullOrEmpty(manualReview))
                DeclassManualReview = Convert.ToBoolean(manualReview);

            DerivativelyClassifiedBy = element.Attribute(XName.Get(DERIVATIVELY_CLASSIFIED_BY_NAME, icNamespace)).ToNonNullString();
            DerivedFrom = element.Attribute(XName.Get(DERIVED_FROM_NAME, icNamespace)).ToNonNullString();
            DisplayOnlyTo = Util.Util.GetXsListAsList(element.Attribute(XName.Get(DISPLAY_ONLY_TO_NAME, icNamespace)).ToNonNullString());
            DisseminationControls = Util.Util.GetXsListAsList(element.Attribute(XName.Get(DISSEMINATION_CONTROLS_NAME, icNamespace)).ToNonNullString());
            FGIsourceOpen = Util.Util.GetXsListAsList(element.Attribute(XName.Get(FGI_SOURCE_OPEN_NAME, icNamespace)).ToNonNullString());
            FGIsourceProtected = Util.Util.GetXsListAsList(element.Attribute(XName.Get(FGI_SOURCE_PROTECTED_NAME, icNamespace)).ToNonNullString());
            NonICmarkings = Util.Util.GetXsListAsList(element.Attribute(XName.Get(NON_IC_MARKINGS_NAME, icNamespace)).ToNonNullString());
            NonUSControls = Util.Util.GetXsListAsList(element.Attribute(XName.Get(NON_US_CONTROLS_NAME, icNamespace)).ToNonNullString());
            OwnerProducers = Util.Util.GetXsListAsList(element.Attribute(XName.Get(OWNER_PRODUCER_NAME, icNamespace)).ToNonNullString());
            ReleasableTo = Util.Util.GetXsListAsList(element.Attribute(XName.Get(RELEASABLE_TO_NAME, icNamespace)).ToNonNullString());
            SARIdentifier = Util.Util.GetXsListAsList(element.Attribute(XName.Get(SAR_IDENTIFIER_NAME, icNamespace)).ToNonNullString());
            SCIcontrols = Util.Util.GetXsListAsList(element.Attribute(XName.Get(SCI_CONTROLS_NAME, icNamespace)).ToNonNullString());
            TypeOfExemptedSource = element.Attribute(XName.Get(TYPE_OF_EXEMPTED_SOURCE_NAME, icNamespace)).ToNonNullString();
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        ///     <para>
        ///         The classification and ownerProducer exist as parameters, and any other security markings are passed in as a
        ///         mapping of local attribute names to String values. This approach is a compromise between a constructor with
        ///         over
        ///         seventeen parameters, and the added complexity of a step-by-step factory/builder approach. If any name-value
        ///         pairing does not correlate with a valid ISM attribute, it will be ignored.
        ///     </para>
        ///     <para>
        ///         If an attribute mapping appears more than once, the last one in the list will be the one used. If
        ///         classification and ownerProducer are included in the Map of other attributes, they will be ignored.
        ///     </para>
        /// </summary>
        /// <param name="classification"> the classification level, which must be a legal classification type (optional) </param>
        /// <param name="ownerProducers"> a list of ownerProducers (optional) </param>
        /// <param name="otherAttributes">
        ///     a name/value mapping of other ISM attributes. The value will be a String value, as it
        ///     appears in XML.
        /// </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public SecurityAttributes(string classification, List<string> ownerProducers, IDictionary<string, string> otherAttributes)
            : base(DDMSVersion.CurrentVersion.Namespace)
        {
            Initialize();
            SCIcontrols = null;
            SARIdentifier = null;
            ReleasableTo = null;
            OwnerProducers = null;
            NonUSControls = null;
            NonICmarkings = null;
            FGIsourceProtected = null;
            FGIsourceOpen = null;
            DisseminationControls = null;
            DisplayOnlyTo = null;
            if (ownerProducers == null)
                ownerProducers = new List<string>();

            if (otherAttributes == null)
                otherAttributes = new Dictionary<string, string>();

            AtomicEnergyMarkings = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(ATOMIC_ENERGY_MARKINGS_NAME));
            Classification = classification;
            ClassificationReason = otherAttributes.GetValueOrEmpty(CLASSIFICATION_REASON_NAME);
            ClassifiedBy = otherAttributes.GetValueOrEmpty(CLASSIFIED_BY_NAME);
            CompilationReason = otherAttributes.GetValueOrEmpty(COMPILATION_REASON_NAME);
            string dateOfExemptedSource = otherAttributes.GetValueOrEmpty(DATE_OF_EXEMPTED_SOURCE_NAME);
            if (!String.IsNullOrEmpty(dateOfExemptedSource))
            {
                try
                {
                    DateOfExemptedSource = DateTime.Parse(dateOfExemptedSource);
                }
                catch (ArgumentException)
                {
                    throw new InvalidDDMSException("The ISM:dateOfExemptedSource attribute is not in a valid date format.");
                }
            }
            string declassDate = otherAttributes.GetValueOrEmpty(DECLASS_DATE_NAME);
            if (!String.IsNullOrEmpty(declassDate))
            {
                try
                {
                    DeclassDate = DateTime.Parse(declassDate);
                }
                catch (ArgumentException)
                {
                    throw new InvalidDDMSException("The ISM:declassDate attribute is not in a valid date format.");
                }
            }
            DeclassEvent = otherAttributes.GetValueOrEmpty(DECLASS_EVENT_NAME);
            DeclassException = otherAttributes.GetValueOrEmpty(DECLASS_EXCEPTION_NAME);
            string manualReview = otherAttributes.GetValueOrEmpty(DECLASS_MANUAL_REVIEW_NAME);
            if (!String.IsNullOrEmpty(manualReview))
                DeclassManualReview = Convert.ToBoolean(manualReview);

            DerivativelyClassifiedBy = otherAttributes.GetValueOrEmpty(DERIVATIVELY_CLASSIFIED_BY_NAME);
            DerivedFrom = otherAttributes.GetValueOrEmpty(DERIVED_FROM_NAME);
            DisplayOnlyTo = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(DISPLAY_ONLY_TO_NAME));
            DisseminationControls = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(DISSEMINATION_CONTROLS_NAME));
            FGIsourceOpen = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(FGI_SOURCE_OPEN_NAME));
            FGIsourceProtected = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(FGI_SOURCE_PROTECTED_NAME));
            NonICmarkings = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(NON_IC_MARKINGS_NAME));
            NonUSControls = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(NON_US_CONTROLS_NAME));
            OwnerProducers = ownerProducers;
            ReleasableTo = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(RELEASABLE_TO_NAME));
            SARIdentifier = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(SAR_IDENTIFIER_NAME));
            SCIcontrols = Util.Util.GetXsListAsList(otherAttributes.GetValueOrEmpty(SCI_CONTROLS_NAME));
            TypeOfExemptedSource = otherAttributes.GetValueOrEmpty(TYPE_OF_EXEMPTED_SOURCE_NAME);
            Validate();
        }

        /// <summary>
        ///     Checks if any attributes have been set.
        /// </summary>
        /// <returns> true if no attributes have values, false otherwise </returns>
        public bool Empty
        {
            get
            {
                return (AtomicEnergyMarkings.Count == 0 &&
                        String.IsNullOrEmpty(Classification) &&
                        String.IsNullOrEmpty(ClassificationReason) &&
                        String.IsNullOrEmpty(ClassifiedBy) &&
                        String.IsNullOrEmpty(CompilationReason) &&
                        DateOfExemptedSource == null &&
                        DeclassDate == null &&
                        String.IsNullOrEmpty(DeclassEvent) &&
                        String.IsNullOrEmpty(DeclassException) &&
                        DeclassManualReview == null &&
                        String.IsNullOrEmpty(DerivativelyClassifiedBy) &&
                        String.IsNullOrEmpty(DerivedFrom) &&
                        DisplayOnlyTo.Count == 0 &&
                        DisseminationControls.Count == 0 &&
                        FGIsourceOpen.Count == 0 &&
                        FGIsourceProtected.Count == 0 &&
                        NonICmarkings.Count == 0 &&
                        NonUSControls.Count == 0 &&
                        OwnerProducers.Count == 0 &&
                        ReleasableTo.Count == 0 &&
                        SARIdentifier.Count == 0 &&
                        SCIcontrols.Count == 0 &&
                        String.IsNullOrEmpty(TypeOfExemptedSource));
            }
        }

        /// <summary>
        ///     Accessor for the atomicEnergyMarkings attribute. Returns a copy.
        /// </summary>
        public List<string> AtomicEnergyMarkings { get; set; }

        /// <summary>
        ///     Accessor for the classification attribute.
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        ///     Accessor for the classificationReason attribute.
        /// </summary>
        public string ClassificationReason { get; set; }

        /// <summary>
        ///     Accessor for the classifiedBy attribute.
        /// </summary>
        public string ClassifiedBy { get; set; }

        /// <summary>
        ///     Accessor for the compilationReason attribute.
        /// </summary>
        public string CompilationReason { get; set; }

        /// <summary>
        ///     Accessor for the dateOfExemptedSource attribute. May return null if not set.
        /// </summary>
        public DateTime? DateOfExemptedSource { get; set; }

        /// <summary>
        ///     Accessor for the declassDate attribute. May return null if not set.
        /// </summary>
        public DateTime? DeclassDate { get; set; }

        /// <summary>
        ///     Accessor for the declassEvent attribute.
        /// </summary>
        public string DeclassEvent { get; set; }

        /// <summary>
        ///     Accessor for the declassException attribute. In DDMS 2.0, this could be a list of tokens. This is represented
        ///     here as a space-delimited string.
        /// </summary>
        public string DeclassException { get; set; }

        /// <summary>
        ///     Accessor for the declassManualReview attribute. Will be null in DDMS 3.0.
        /// </summary>
        public bool? DeclassManualReview { get; set; }

        /// <summary>
        ///     Accessor for the derivativelyClassifiedBy attribute.
        /// </summary>
        public string DerivativelyClassifiedBy { get; set; }

        /// <summary>
        ///     Accessor for the derivedFrom attribute.
        /// </summary>
        public string DerivedFrom { get; set; }

        /// <summary>
        ///     Accessor for the displayOnlyTo attribute. Returns a copy.
        /// </summary>
        public List<string> DisplayOnlyTo { get; set; }

        /// <summary>
        ///     Accessor for the disseminationControls attribute. Returns a copy.
        /// </summary>
        public List<string> DisseminationControls { get; set; }

        /// <summary>
        ///     Accessor for the FGIsourceOpen attribute. Returns a copy.
        /// </summary>
        public List<string> FGIsourceOpen { get; set; }

        /// <summary>
        ///     Accessor for the FGIsourceProtected attribute. Returns a copy.
        /// </summary>
        public List<string> FGIsourceProtected { get; set; }

        /// <summary>
        ///     Accessor for the nonICmarkings attribute. Returns a copy.
        /// </summary>
        public List<string> NonICmarkings { get; set; }

        /// <summary>
        ///     Accessor for the nonUSControls attribute. Returns a copy.
        /// </summary>
        public List<string> NonUSControls { get; set; }

        /// <summary>
        ///     Accessor for the ownerProducers attribute. Returns a copy.
        /// </summary>
        public List<string> OwnerProducers { get; set; }

        /// <summary>
        ///     Accessor for the releasableTo attribute. Returns a copy.
        /// </summary>
        public List<string> ReleasableTo { get; set; }

        /// <summary>
        ///     Accessor for the SARIdentifier attribute. Returns a copy.
        /// </summary>
        public List<string> SARIdentifier { get; set; }

        /// <summary>
        ///     Accessor for the SCIcontrols attribute. Returns a copy.
        /// </summary>
        public List<string> SCIcontrols { get; set; }

        /// <summary>
        ///     Accessor for the typeOfExemptedSource attribute. In DDMS 2.0, this could be a list of tokens. This is represented
        ///     here as a space-delimited string.
        /// </summary>
        public string TypeOfExemptedSource { get; set; }

        /// <summary>
        ///     Returns a non-null instance of security attributes. If the instance passed in is not null, it will be returned.
        /// </summary>
        /// <param name="securityAttributes"> the attributes to return by default </param>
        /// <returns> a non-null attributes instance </returns>
        /// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
        public static SecurityAttributes GetNonNullInstance(SecurityAttributes securityAttributes)
        {
            return (securityAttributes == null ? new SecurityAttributes(null, null, null) : securityAttributes);
        }

        /// <summary>
        ///     Convenience method to add these attributes onto an existing XOM Element
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        public void AddTo(XElement element)
        {
            DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
            ValidateSameVersion(elementVersion);
            string icNamespace = elementVersion.IsmNamespace;
            string icPrefix = PropertyReader.GetPrefix("ism");

            Util.Util.AddAttribute(element, icPrefix, ATOMIC_ENERGY_MARKINGS_NAME, icNamespace, Util.Util.GetXsList(AtomicEnergyMarkings));
            Util.Util.AddAttribute(element, icPrefix, CLASSIFICATION_NAME, icNamespace, Classification);
            Util.Util.AddAttribute(element, icPrefix, CLASSIFICATION_REASON_NAME, icNamespace, ClassificationReason);
            Util.Util.AddAttribute(element, icPrefix, CLASSIFIED_BY_NAME, icNamespace, ClassifiedBy);
            Util.Util.AddAttribute(element, icPrefix, COMPILATION_REASON_NAME, icNamespace, CompilationReason);
            if (DateOfExemptedSource.HasValue)
                Util.Util.AddAttribute(element, icPrefix, DATE_OF_EXEMPTED_SOURCE_NAME, icNamespace, DateOfExemptedSource.GetValueOrDefault().ToString("o"));

            if (DeclassDate.HasValue)
                Util.Util.AddAttribute(element, icPrefix, DECLASS_DATE_NAME, icNamespace, DeclassDate.GetValueOrDefault().ToString("o"));

            Util.Util.AddAttribute(element, icPrefix, DECLASS_EVENT_NAME, icNamespace, DeclassEvent);
            Util.Util.AddAttribute(element, icPrefix, DECLASS_EXCEPTION_NAME, icNamespace, DeclassException);
            if (DeclassManualReview != null)
                Util.Util.AddAttribute(element, icPrefix, DECLASS_MANUAL_REVIEW_NAME, icNamespace, DeclassManualReview.ToString());

            Util.Util.AddAttribute(element, icPrefix, DERIVATIVELY_CLASSIFIED_BY_NAME, icNamespace,
                DerivativelyClassifiedBy);
            Util.Util.AddAttribute(element, icPrefix, DERIVED_FROM_NAME, icNamespace, DerivedFrom);
            Util.Util.AddAttribute(element, icPrefix, DISPLAY_ONLY_TO_NAME, icNamespace,
                Util.Util.GetXsList(DisplayOnlyTo));
            Util.Util.AddAttribute(element, icPrefix, DISSEMINATION_CONTROLS_NAME, icNamespace,
                Util.Util.GetXsList(DisseminationControls));
            Util.Util.AddAttribute(element, icPrefix, FGI_SOURCE_OPEN_NAME, icNamespace,
                Util.Util.GetXsList(FGIsourceOpen));
            Util.Util.AddAttribute(element, icPrefix, FGI_SOURCE_PROTECTED_NAME, icNamespace,
                Util.Util.GetXsList(FGIsourceProtected));
            Util.Util.AddAttribute(element, icPrefix, NON_IC_MARKINGS_NAME, icNamespace,
                Util.Util.GetXsList(NonICmarkings));
            Util.Util.AddAttribute(element, icPrefix, NON_US_CONTROLS_NAME, icNamespace,
                Util.Util.GetXsList(NonUSControls));
            Util.Util.AddAttribute(element, icPrefix, OWNER_PRODUCER_NAME, icNamespace,
                Util.Util.GetXsList(OwnerProducers));
            Util.Util.AddAttribute(element, icPrefix, RELEASABLE_TO_NAME, icNamespace, Util.Util.GetXsList(ReleasableTo));
            Util.Util.AddAttribute(element, icPrefix, SAR_IDENTIFIER_NAME, icNamespace,
                Util.Util.GetXsList(SARIdentifier));
            Util.Util.AddAttribute(element, icPrefix, SCI_CONTROLS_NAME, icNamespace, Util.Util.GetXsList(SCIcontrols));
            Util.Util.AddAttribute(element, icPrefix, TYPE_OF_EXEMPTED_SOURCE_NAME, icNamespace, TypeOfExemptedSource);
        }

        /// <summary>
        ///     Validates the attribute group. Where appropriate the <see cref="ISMVocabulary" /> enumerations are validated.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The atomicEnergyMarkings cannot be used until DDMS 3.1 or later.</li>
        ///                 <li>If set, the atomicEnergyMarkings attribute must be valid tokens.</li>
        ///                 <li>If set, the classification attribute must be a valid token.</li>
        ///                 <li>The compilationReason attribute cannot be used until DDMS 3.0 or later.</li>
        ///                 <li>The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0</li>
        ///                 <li>If set, the dateOfExemptedSource attribute is a valid xs:date value.</li>
        ///                 <li>If set, the declassDate attribute is a valid xs:date value.</li>
        ///                 <li>If set, the declassException attribute must be a valid token.</li>
        ///                 <li>The declassManualReview attribute cannot be used after DDMS 2.0.</li>
        ///                 <li>The displayOnlyTo attribute cannot be used until DDMS 3.1 or later.</li>
        ///                 <li>If set, the displayOnlyTo attribute must be valid tokens.</li>
        ///                 <li>If set, the disseminationControls attribute must be valid tokens.</li>
        ///                 <li>If set, the FGIsourceOpen attribute must be valid tokens.</li>
        ///                 <li>If set, the FGIsourceProtected attribute must be valid tokens.</li>
        ///                 <li>If set, the nonICmarkings attribute must be valid tokens.</li>
        ///                 <li>The nonUSControls attribute cannot be used until DDMS 3.1 or later.</li>
        ///                 <li>If set, the nonUSControls attribute must be valid tokens.</li>
        ///                 <li>If set, the ownerProducers attribute must be valid tokens.</li>
        ///                 <li>If set, the releasableTo attribute must be valid tokens.</li>
        ///                 <li>If set, the SARIdentifiers attribute must be valid tokens.</li>
        ///                 <li>If set, the SCIcontrols attribute must be valid tokens.</li>
        ///                 <li>The typeOfExemptedSource attribute can only be used in DDMS 2.0 or DDMS 3.0.</li>
        ///                 <li>If set, the typeOfExemptedSource attribute must be a valid token.</li>
        ///                 <li>Does NOT do any validation on the constraints described in the DES ISM specification.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            // Should be reviewed as additional versions of DDMS are supported.
            DDMSVersion version = DDMSVersion;
            bool isDDMS20 = "2.0".Equals(version.Version);

            if (!version.IsAtLeast("3.1") && AtomicEnergyMarkings.Count > 0)
                throw new InvalidDDMSException("The atomicEnergyMarkings attribute cannot be used until DDMS 3.1 or later.");

            foreach (var atomic in AtomicEnergyMarkings)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_ATOMIC_ENERGY_MARKINGS, atomic);

            if (!String.IsNullOrEmpty(Classification))
            {
                if (version.IsAtLeast("3.0") || !ISMVocabulary.UsingOldClassification(Classification))
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_ALL_CLASSIFICATIONS, Classification);
            }
            if (!version.IsAtLeast("3.0") && !String.IsNullOrEmpty(CompilationReason))
                throw new InvalidDDMSException("The compilationReason attribute cannot be used until DDMS 3.0 or later.");

            if (version.IsAtLeast("3.1") && DateOfExemptedSource != null)
                throw new InvalidDDMSException("The dateOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0.");

            if (DateOfExemptedSource != null && !DateOfExemptedSource.Value.TimeOfDay.TotalSeconds.Equals(0))
                throw new InvalidDDMSException("The dateOfExemptedSource attribute must be in the xs:date format (YYYY-MM-DD).");

            if (DeclassDate != null && !DeclassDate.Value.TimeOfDay.TotalSeconds.Equals(0))
                throw new InvalidDDMSException("The declassDate must be in the xs:date format (YYYY-MM-DD).");

            if (!String.IsNullOrEmpty(DeclassException))
            {
                if (isDDMS20)
                {
                    // In DDMS 2.0, this can be a list of tokens.
                    foreach (var value in Util.Util.GetXsListAsList(DeclassException))
                        ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DECLASS_EXCEPTION, value);
                }
                else
                {
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DECLASS_EXCEPTION, DeclassException);
                }
            }
            if (!isDDMS20 && DeclassManualReview != null)
                throw new InvalidDDMSException("The declassManualReview attribute can only be used in DDMS 2.0.");

            if (!version.IsAtLeast("3.1") && DisplayOnlyTo.Count > 0)
                throw new InvalidDDMSException("The displayOnlyTo attribute cannot be used until DDMS 3.1 or later.");

            foreach (var display in DisplayOnlyTo)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DISPLAY_ONLY_TO, display);

            foreach (var dissemination in DisseminationControls)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_DISSEMINATION_CONTROLS, dissemination);

            foreach (var fgiSourceOpen in FGIsourceOpen)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_FGI_SOURCE_OPEN, fgiSourceOpen);

            foreach (var fgiSourceProtected in FGIsourceProtected)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_FGI_SOURCE_PROTECTED, fgiSourceProtected);

            foreach (var nonIC in NonICmarkings)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NON_IC_MARKINGS, nonIC);

            if (!version.IsAtLeast("3.1") && NonUSControls.Count > 0)
                throw new InvalidDDMSException("The nonUSControls attribute cannot be used until DDMS 3.1 or later.");

            foreach (var nonUS in NonUSControls)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_NON_US_CONTROLS, nonUS);

            foreach (var op in OwnerProducers)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_OWNER_PRODUCERS, op);

            foreach (var releasableTo in ReleasableTo)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_RELEASABLE_TO, releasableTo);

            foreach (var sarId in SARIdentifier)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_SAR_IDENTIFIER, sarId);

            foreach (var sciControls in SCIcontrols)
                ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_SCI_CONTROLS, sciControls);

            if (!String.IsNullOrEmpty(TypeOfExemptedSource))
            {
                if (isDDMS20)
                {
                    // In DDMS 2.0, this can be a list of tokens.
                    foreach (var value in Util.Util.GetXsListAsList(TypeOfExemptedSource))
                        ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, value);
                }
                else if ("3.0".Equals(version.Version))
                {
                    ISMVocabulary.ValidateEnumeration(ISMVocabulary.CVE_TYPE_EXEMPTED_SOURCE, TypeOfExemptedSource);
                }
                else
                {
                    throw new InvalidDDMSException(
                        "The typeOfExemptedSource attribute can only be used in DDMS 2.0 or 3.0.");
                }
            }
            base.Validate();
        }

        /// <summary>
        ///     Standalone validation method for components which require a classification and ownerProducer.
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if there is no classification. </exception>
        public void RequireClassification()
        {
            Util.Util.RequireDDMSValue(CLASSIFICATION_NAME, Classification);
            if (OwnerProducers.Count == 0)
                throw new InvalidDDMSException("At least 1 ownerProducer must be set.");
        }

        /// <see cref="AbstractAttributeGroup#getOutput(boolean, String)"></see>
        public override string GetOutput(bool isHtml, string prefix)
        {
            string localPrefix = prefix.ToNonNullString();
            var text = new StringBuilder();
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + ATOMIC_ENERGY_MARKINGS_NAME, Util.Util.GetXsList(AtomicEnergyMarkings)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + CLASSIFICATION_NAME, Classification));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + CLASSIFICATION_REASON_NAME, ClassificationReason));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + CLASSIFIED_BY_NAME, ClassifiedBy));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + COMPILATION_REASON_NAME, CompilationReason));
            if (DateOfExemptedSource.HasValue)
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DATE_OF_EXEMPTED_SOURCE_NAME, DateOfExemptedSource.GetValueOrDefault().ToString("o")));

            if (DeclassDate.HasValue)
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DECLASS_DATE_NAME, DeclassDate.GetValueOrDefault().ToString("o")));

            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DECLASS_EVENT_NAME, DeclassEvent));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DECLASS_EXCEPTION_NAME, DeclassException));
            if (DeclassManualReview != null)
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DECLASS_MANUAL_REVIEW_NAME, DeclassManualReview.ToString()));

            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DERIVATIVELY_CLASSIFIED_BY_NAME, DerivativelyClassifiedBy));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DERIVED_FROM_NAME, DerivedFrom));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DISPLAY_ONLY_TO_NAME, Util.Util.GetXsList(DisplayOnlyTo)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + DISSEMINATION_CONTROLS_NAME, Util.Util.GetXsList(DisseminationControls)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + FGI_SOURCE_OPEN_NAME, Util.Util.GetXsList(FGIsourceOpen)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + FGI_SOURCE_PROTECTED_NAME, Util.Util.GetXsList(FGIsourceProtected)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + NON_IC_MARKINGS_NAME, Util.Util.GetXsList(NonICmarkings)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + NON_US_CONTROLS_NAME, Util.Util.GetXsList(NonUSControls)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + OWNER_PRODUCER_NAME, Util.Util.GetXsList(OwnerProducers)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + RELEASABLE_TO_NAME, Util.Util.GetXsList(ReleasableTo)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + SAR_IDENTIFIER_NAME, Util.Util.GetXsList(SARIdentifier)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + SCI_CONTROLS_NAME, Util.Util.GetXsList(SCIcontrols)));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + TYPE_OF_EXEMPTED_SOURCE_NAME, TypeOfExemptedSource));
            return (text.ToString());
        }

        /// <see cref="Object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!(obj is SecurityAttributes))
                return (false);

            var test = (SecurityAttributes)obj;

            return (Util.Util.ListEquals(AtomicEnergyMarkings, test.AtomicEnergyMarkings) &&
                    Classification == test.Classification &&
                    ClassificationReason == test.ClassificationReason &&
                    ClassifiedBy == test.ClassifiedBy &&
                    CompilationReason == test.CompilationReason &&
                    Util.Util.NullEquals(DateOfExemptedSource, test.DateOfExemptedSource) &&
                    Util.Util.NullEquals(DeclassDate, test.DeclassDate) &&
                    DeclassEvent == test.DeclassEvent &&
                    DeclassException == test.DeclassException &&
                    Util.Util.NullEquals(DeclassManualReview, test.DeclassManualReview) &&
                    DerivativelyClassifiedBy == test.DerivativelyClassifiedBy &&
                    DerivedFrom == test.DerivedFrom &&
                    Util.Util.ListEquals(DisplayOnlyTo, test.DisplayOnlyTo) &&
                    Util.Util.ListEquals(DisseminationControls, test.DisseminationControls) &&
                    Util.Util.ListEquals(FGIsourceOpen, test.FGIsourceOpen) &&
                    Util.Util.ListEquals(FGIsourceProtected, test.FGIsourceProtected) &&
                    Util.Util.ListEquals(NonICmarkings, test.NonICmarkings) &&
                    Util.Util.ListEquals(NonUSControls, test.NonUSControls) &&
                    Util.Util.ListEquals(OwnerProducers, test.OwnerProducers) &&
                    Util.Util.ListEquals(ReleasableTo, test.ReleasableTo) &&
                    Util.Util.ListEquals(SARIdentifier, test.SARIdentifier) &&
                    Util.Util.ListEquals(SCIcontrols, test.SCIcontrols) &&
                    TypeOfExemptedSource == test.TypeOfExemptedSource);
        }

        /// <see cref="Object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = 0;
            result = 7 * result + AtomicEnergyMarkings.GetOrderIndependentHashCode();
            result = 7 * result + Classification.GetHashCode();
            result = 7 * result + ClassificationReason.GetHashCode();
            result = 7 * result + ClassifiedBy.GetHashCode();
            result = 7 * result + CompilationReason.GetHashCode();
            if (DateOfExemptedSource != null)
                result = 7 * result + DateOfExemptedSource.GetHashCode();

            if (DeclassDate != null)
                result = 7 * result + DeclassDate.GetHashCode();

            result = 7 * result + DeclassEvent.GetHashCode();
            result = 7 * result + DeclassException.GetHashCode();
            if (DeclassManualReview != null)
                result = 7 * result + DeclassManualReview.GetHashCode();

            result = 7 * result + DerivativelyClassifiedBy.GetHashCode();
            result = 7 * result + DerivedFrom.GetHashCode();
            result = 7 * result + DisplayOnlyTo.GetOrderIndependentHashCode();
            result = 7 * result + DisseminationControls.GetOrderIndependentHashCode();
            result = 7 * result + FGIsourceOpen.GetOrderIndependentHashCode();
            result = 7 * result + FGIsourceProtected.GetOrderIndependentHashCode();
            result = 7 * result + NonICmarkings.GetOrderIndependentHashCode();
            result = 7 * result + NonUSControls.GetOrderIndependentHashCode();
            result = 7 * result + OwnerProducers.GetOrderIndependentHashCode();
            result = 7 * result + ReleasableTo.GetOrderIndependentHashCode();
            result = 7 * result + SARIdentifier.GetOrderIndependentHashCode();
            result = 7 * result + SCIcontrols.GetOrderIndependentHashCode();
            result = 7 * result + TypeOfExemptedSource.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Builder for these attributes.
        ///     <para>
        ///         This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
        ///         standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
        ///         null.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                ListAttributes = new Dictionary<string, List<string>>();
                StringAttributes = new Dictionary<string, string>();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(SecurityAttributes attributes)
                : this()
            {
                AtomicEnergyMarkings = attributes.AtomicEnergyMarkings;
                Classification = attributes.Classification;
                ClassificationReason = attributes.ClassificationReason;
                ClassifiedBy = attributes.ClassifiedBy;
                CompilationReason = attributes.CompilationReason;
                if (attributes.DateOfExemptedSource.HasValue)
                    DateOfExemptedSource = attributes.DateOfExemptedSource.GetValueOrDefault().ToString("o");

                if (attributes.DeclassDate.HasValue)
                    DeclassDate = attributes.DeclassDate.GetValueOrDefault().ToString("o");

                DeclassEvent = attributes.DeclassEvent;
                DeclassException = attributes.DeclassException;
                if (attributes.DeclassManualReview != null)
                    DeclassManualReview = attributes.DeclassManualReview;

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
            ///     Checks if any values have been provided for this Builder.
            /// </summary>
            /// <returns> true if every field is empty </returns>
            public virtual bool Empty
            {
                get
                {
                    bool isEmpty = true;
                    foreach (var value in StringAttributes.Values)
                        isEmpty = isEmpty && String.IsNullOrEmpty(value);

                    foreach (var list in ListAttributes.Values)
                        isEmpty = isEmpty && Util.Util.ContainsOnlyEmptyValues(list);

                    return (isEmpty && DeclassManualReview == null);
                }
            }

            /// <summary>
            ///     Builder accessor for the atomicEnergyMarkings attribute
            /// </summary>
            public virtual List<string> AtomicEnergyMarkings
            {
                get { return (GetListAttribute(ATOMIC_ENERGY_MARKINGS_NAME)); }
                set { SetListAttribute(ATOMIC_ENERGY_MARKINGS_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the classification attribute
            /// </summary>
            public virtual string Classification
            {
                get { return (StringAttributes.GetValueOrNull(CLASSIFICATION_NAME)); }
                set { StringAttributes[CLASSIFICATION_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the classificationReason attribute
            /// </summary>
            public virtual string ClassificationReason
            {
                get { return (StringAttributes.GetValueOrNull(CLASSIFICATION_REASON_NAME)); }
                set { StringAttributes[CLASSIFICATION_REASON_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the classifiedBy attribute
            /// </summary>
            public virtual string ClassifiedBy
            {
                get { return (StringAttributes.GetValueOrNull(CLASSIFIED_BY_NAME)); }
                set { StringAttributes[CLASSIFIED_BY_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the compilationReason attribute
            /// </summary>
            public virtual string CompilationReason
            {
                get { return (StringAttributes.GetValueOrNull(COMPILATION_REASON_NAME)); }
                set { StringAttributes[COMPILATION_REASON_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the dateOfExemptedSource attribute
            /// </summary>
            public virtual string DateOfExemptedSource
            {
                get { return (StringAttributes.GetValueOrNull(DATE_OF_EXEMPTED_SOURCE_NAME)); }
                set { StringAttributes[DATE_OF_EXEMPTED_SOURCE_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the declassDate attribute
            /// </summary>
            public virtual string DeclassDate
            {
                get { return (StringAttributes.GetValueOrNull(DECLASS_DATE_NAME)); }
                set { StringAttributes[DECLASS_DATE_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the declassEvent attribute
            /// </summary>
            public virtual string DeclassEvent
            {
                get { return (StringAttributes.GetValueOrNull(DECLASS_EVENT_NAME)); }
                set { StringAttributes[DECLASS_EVENT_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the declassException attribute
            /// </summary>
            public virtual string DeclassException
            {
                get { return (StringAttributes.GetValueOrNull(DECLASS_EXCEPTION_NAME)); }
                set { StringAttributes[DECLASS_EXCEPTION_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the declassManualReview attribute
            /// </summary>
            public virtual bool? DeclassManualReview { get; set; }

            /// <summary>
            ///     Builder accessor for the derivativelyClassifiedBy attribute
            /// </summary>
            public virtual string DerivativelyClassifiedBy
            {
                get { return (StringAttributes.GetValueOrNull(DERIVATIVELY_CLASSIFIED_BY_NAME)); }
                set { StringAttributes[DERIVATIVELY_CLASSIFIED_BY_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the derivedFrom attribute
            /// </summary>
            public virtual string DerivedFrom
            {
                get { return (StringAttributes.GetValueOrNull(DERIVED_FROM_NAME)); }
                set { StringAttributes[DERIVED_FROM_NAME] = value; }
            }

            /// <summary>
            ///     Builder accessor for the displayOnlyTo attribute
            /// </summary>
            public virtual List<string> DisplayOnlyTo
            {
                get { return (GetListAttribute(DISPLAY_ONLY_TO_NAME)); }
                set { SetListAttribute(DISPLAY_ONLY_TO_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the disseminationControls attribute
            /// </summary>
            public virtual List<string> DisseminationControls
            {
                get { return (GetListAttribute(DISSEMINATION_CONTROLS_NAME)); }
                set { SetListAttribute(DISSEMINATION_CONTROLS_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the FGIsourceOpen attribute
            /// </summary>
            public virtual List<string> FGIsourceOpen
            {
                get { return (GetListAttribute(FGI_SOURCE_OPEN_NAME)); }
                set { SetListAttribute(FGI_SOURCE_OPEN_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the FGIsourceProtected attribute
            /// </summary>
            public virtual List<string> FGIsourceProtected
            {
                get { return (GetListAttribute(FGI_SOURCE_PROTECTED_NAME)); }
                set { SetListAttribute(FGI_SOURCE_PROTECTED_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the nonICmarkings attribute
            /// </summary>
            public virtual List<string> NonICmarkings
            {
                get { return (GetListAttribute(NON_IC_MARKINGS_NAME)); }
                set { SetListAttribute(NON_IC_MARKINGS_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the nonUSControls attribute
            /// </summary>
            public virtual List<string> NonUSControls
            {
                get { return (GetListAttribute(NON_US_CONTROLS_NAME)); }
                set { SetListAttribute(NON_US_CONTROLS_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the ownerProducers attribute
            /// </summary>
            public virtual List<string> OwnerProducers
            {
                get { return (GetListAttribute(OWNER_PRODUCER_NAME)); }
                set { SetListAttribute(OWNER_PRODUCER_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the releasableTo attribute
            /// </summary>
            public virtual List<string> ReleasableTo
            {
                get { return (GetListAttribute(RELEASABLE_TO_NAME)); }
                set { SetListAttribute(RELEASABLE_TO_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the SARIdentifier attribute
            /// </summary>
            public virtual List<string> SARIdentifier
            {
                get { return (GetListAttribute(SAR_IDENTIFIER_NAME)); }
                set { SetListAttribute(SAR_IDENTIFIER_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the SCIcontrols attribute
            /// </summary>
            public virtual List<string> SCIcontrols
            {
                get { return (GetListAttribute(SCI_CONTROLS_NAME)); }
                set { SetListAttribute(SCI_CONTROLS_NAME, value); }
            }

            /// <summary>
            ///     Builder accessor for the typeOfExemptedSource attribute
            /// </summary>
            public virtual string TypeOfExemptedSource
            {
                get { return (StringAttributes.GetValueOrNull(TYPE_OF_EXEMPTED_SOURCE_NAME)); }
                set { StringAttributes[TYPE_OF_EXEMPTED_SOURCE_NAME] = value; }
            }

            /// <summary>
            ///     Accessor for the map of attribute names to list values
            /// </summary>
            internal virtual IDictionary<string, List<string>> ListAttributes { get; private set; }

            /// <summary>
            ///     Accessor for the map of attribute names to string values
            /// </summary>
            internal virtual IDictionary<string, string> StringAttributes { get; private set; }

            /// <summary>
            ///     Finalizes the data gathered for this builder instance. Will always return an empty instance instead of a null
            ///     one.
            /// </summary>
            /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
            public virtual SecurityAttributes Commit()
            {
                IDictionary<string, string> otherAttributes = new Dictionary<string, string>();
                otherAttributes[ATOMIC_ENERGY_MARKINGS_NAME] = Util.Util.GetXsList(AtomicEnergyMarkings);
                otherAttributes[CLASSIFICATION_REASON_NAME] = ClassificationReason;
                otherAttributes[CLASSIFIED_BY_NAME] = ClassifiedBy;
                otherAttributes[COMPILATION_REASON_NAME] = CompilationReason;
                otherAttributes[DATE_OF_EXEMPTED_SOURCE_NAME] = DateOfExemptedSource;
                otherAttributes[DECLASS_DATE_NAME] = DeclassDate;
                otherAttributes[DECLASS_EVENT_NAME] = DeclassEvent;
                otherAttributes[DECLASS_EXCEPTION_NAME] = DeclassException;
                if (DeclassManualReview != null)
                    otherAttributes[DECLASS_MANUAL_REVIEW_NAME] = DeclassManualReview.ToString();

                otherAttributes[DERIVATIVELY_CLASSIFIED_BY_NAME] = DerivativelyClassifiedBy;
                otherAttributes[DERIVED_FROM_NAME] = DerivedFrom;
                otherAttributes[DISPLAY_ONLY_TO_NAME] = Util.Util.GetXsList(DisplayOnlyTo);
                otherAttributes[DISSEMINATION_CONTROLS_NAME] = Util.Util.GetXsList(DisseminationControls);
                otherAttributes[FGI_SOURCE_OPEN_NAME] = Util.Util.GetXsList(FGIsourceOpen);
                otherAttributes[FGI_SOURCE_PROTECTED_NAME] = Util.Util.GetXsList(FGIsourceProtected);
                otherAttributes[NON_IC_MARKINGS_NAME] = Util.Util.GetXsList(NonICmarkings);
                otherAttributes[NON_US_CONTROLS_NAME] = Util.Util.GetXsList(NonUSControls);
                otherAttributes[RELEASABLE_TO_NAME] = Util.Util.GetXsList(ReleasableTo);
                otherAttributes[SAR_IDENTIFIER_NAME] = Util.Util.GetXsList(SARIdentifier);
                otherAttributes[SCI_CONTROLS_NAME] = Util.Util.GetXsList(SCIcontrols);
                otherAttributes[TYPE_OF_EXEMPTED_SOURCE_NAME] = TypeOfExemptedSource;
                return (new SecurityAttributes(Classification, OwnerProducers, otherAttributes));
            }

            /// <summary>
            ///     Helper method to look up a key in the map of attribute. Lazily creates a new list if null.
            /// </summary>
            /// <param name="key"> the attribute name </param>
            /// <returns> the list of strings mapped to that attribute name </returns>
            internal virtual List<string> GetListAttribute(string key)
            {
                if (ListAttributes.GetValueOrNull(key) == null)
                    ListAttributes[key] = new List<string>();

                return (ListAttributes.GetValueOrNull(key));
            }

            /// <summary>
            ///     Helper method to initialize a new lazy list for some attribute.
            /// </summary>
            /// <param name="key"> the attribute name </param>
            /// <param name="value"> the list to save, which will be wrapped in a lazy list </param>
            internal virtual void SetListAttribute(string key, List<string> value)
            {
                ListAttributes[key] = value;
            }
        }
    }
}