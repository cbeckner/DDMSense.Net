#region usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using DDMSense.Extensions;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.SecurityElements.Ism
{
    /// <summary>
    ///     Representation of the Controlled Vocabulary enumerations used by ISM attributes.
    ///     <para>
    ///         Token values are read from the CVEnumISM.xml files accompanying the "XML Data Encoding Specification for
    ///         Information
    ///         Security Marking Metadata". They can then be used to validate the contents of the attributes
    ///         in a <see cref="SecurityAttributes" /> or <see cref="NoticeAttributes" /> instance.
    ///     </para>
    ///     <ul>
    ///         <li>CVEnumISM25X.xml: tokens allowed in the "declassException" attribute</li>
    ///         <li>
    ///             CVEnumISMAtomicEnergyMarkings.xml: tokens allowed in the "atomicEnergyMarkings" attribute (starting in DDMS
    ///             3.1)
    ///         </li>
    ///         <li>CVEnumISMClassificationAll.xml: tokens allowed in the "classification" attribute</li>
    ///         <li>CVEnumISMClassificationUS.xml: subset of the tokens allowed in the "classification" attribute</li>
    ///         <li>CVEnumISMCompliesWith.xml: tokens allowed in the "compliesWith" attribute (starting in DDMS 3.1)</li>
    ///         <li>CVEnumISMDissem.xml: tokens allowed in the "disseminationControls" attribute</li>
    ///         <li>CVEnumISMFGIOpen.xml: tokens allowed in the "FGIsourceOpen" attribute</li>
    ///         <li>CVEnumISMFGIProtected.xml: tokens allowed in the "FGIsourceProtected" attribute</li>
    ///         <li>CVEnumISMNonIC.xml: tokens allowed in the "nonICmarkings" attribute</li>
    ///         <li>CVEnumISMNonUSControls.xml: tokens allowed in the "nonUSControls" attribute (starting in DDMS 3.1)</li>
    ///         <li>CVEnumISMNotice.xml: tokens allowed in the "noticeType" attribute (starting in DDMS 4.0.1)</li>
    ///         <li>CVEnumISMOwnerProducer.xml: tokens allowed in the "ownerProducer" attribute</li>
    ///         <li>CVEnumISMPocType.xml: tokens allowed in the "pocType" attribute</li>
    ///         <li>
    ///             CVEnumISMRelTo.xml: tokens allowed in the "displayOnlyTo" (starting in DDMS 3.1) and "releasableTo"
    ///             attribute
    ///         </li>
    ///         <li>CVEnumISMSAR.xml: tokens allowed in the "SARIdentifier" attribute</li>
    ///         <li>CVEnumISMSCIControls.xml: tokens allowed in the "SCIcontrols" attribute</li>
    ///         <li>
    ///             CVEnumISMSourceMarked.xml: tokens allowed in the "typeOfExemptedSource" attribute (DDMS 2.0 and DDMS 3.0
    ///             only)
    ///         </li>
    ///     </ul>
    ///     <para>Some of these vocabularies include regular expression patterns.</para>
    /// </summary>
    public static class ISMVocabulary
    {
        /// <summary>
        ///     Filename for the enumerations allowed in a declassException attribute
        /// </summary>
        public const string CVE_DECLASS_EXCEPTION = "CVEnumISM25X.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in an atomicEnergyMarkings attribute
        /// </summary>
        public const string CVE_ATOMIC_ENERGY_MARKINGS = "CVEnumISMAtomicEnergyMarkings.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a classification attribute
        /// </summary>
        public const string CVE_ALL_CLASSIFICATIONS = "CVEnumISMClassificationAll.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a classification attribute (US only)
        /// </summary>
        public const string CVE_US_CLASSIFICATIONS = "CVEnumISMClassificationUS.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a compliesWith attribute
        /// </summary>
        public const string CVE_COMPLIES_WITH = "CVEnumISMCompliesWith.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a displayOnlyTo attribute
        /// </summary>
        public const string CVE_DISPLAY_ONLY_TO = "CVEnumISMRelTo.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a disseminationControls attribute
        /// </summary>
        public const string CVE_DISSEMINATION_CONTROLS = "CVEnumISMDissem.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a FGIsourceOpen attribute
        /// </summary>
        public const string CVE_FGI_SOURCE_OPEN = "CVEnumISMFGIOpen.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a FGIsourceProtected attribute
        /// </summary>
        public const string CVE_FGI_SOURCE_PROTECTED = "CVEnumISMFGIProtected.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a nonICmarkings attribute
        /// </summary>
        public const string CVE_NON_IC_MARKINGS = "CVEnumISMNonIC.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a nonUSControls attribute
        /// </summary>
        public const string CVE_NON_US_CONTROLS = "CVEnumISMNonUSControls.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a noticeType attribute
        /// </summary>
        public const string CVE_NOTICE_TYPE = "CVEnumISMNotice.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in an ownerProducer attribute
        /// </summary>
        public const string CVE_OWNER_PRODUCERS = "CVEnumISMOwnerProducer.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a pocType attribute
        /// </summary>
        public const string CVE_POC_TYPE = "CVEnumISMPocType.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a releasableTo attribute
        /// </summary>
        public const string CVE_RELEASABLE_TO = "CVEnumISMRelTo.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a SARIdentifier attribute
        /// </summary>
        public const string CVE_SAR_IDENTIFIER = "CVEnumISMSAR.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a SCIcontrols attribute
        /// </summary>
        public const string CVE_SCI_CONTROLS = "CVEnumISMSCIControls.xml";

        /// <summary>
        ///     Filename for the enumerations allowed in a typeOfExemptedSource attribute
        /// </summary>
        public const string CVE_TYPE_EXEMPTED_SOURCE = "CVEnumISMSourceMarked.xml";

        private const string ENUMERATION_NAME = "Enumeration";
        private const string TERM_NAME = "Term";
        private const string VALUE_NAME = "Value";
        private const string REG_EXP_NAME = "regularExpression";
        private static readonly List<string> COMMON_NETWORK_TYPES = new List<string>();
        private static readonly List<string> ALL_ENUMS = new List<string>();

        private static readonly IDictionary<string, IDictionary<string, List<string>>> LOCATION_TO_ENUM_TOKENS =
            new Dictionary<string, IDictionary<string, List<string>>>();

        private static readonly IDictionary<string, IDictionary<string, List<string>>> LOCATION_TO_ENUM_PATTERNS =
            new Dictionary<string, IDictionary<string, List<string>>>();

        private static string _lastEnumLocation;
        private static DDMSVersion _ddmsVersion;

        static ISMVocabulary()
        {
            COMMON_NETWORK_TYPES.Add("NIPRNet");
            COMMON_NETWORK_TYPES.Add("SIPRNet");
            COMMON_NETWORK_TYPES.Add("JWICS");
            COMMON_NETWORK_TYPES.Add("ADSN");
            COMMON_NETWORK_TYPES.Add("StoneGhost");
            COMMON_NETWORK_TYPES.Add("LOCE");
            COMMON_NETWORK_TYPES.Add("CRONOS");
            COMMON_NETWORK_TYPES.Add("other");
            ALL_ENUMS.Add(CVE_DECLASS_EXCEPTION);
            ALL_ENUMS.Add(CVE_ATOMIC_ENERGY_MARKINGS);
            ALL_ENUMS.Add(CVE_ALL_CLASSIFICATIONS);
            ALL_ENUMS.Add(CVE_US_CLASSIFICATIONS);
            ALL_ENUMS.Add(CVE_COMPLIES_WITH);
            ALL_ENUMS.Add(CVE_DISSEMINATION_CONTROLS);
            ALL_ENUMS.Add(CVE_DISPLAY_ONLY_TO);
            ALL_ENUMS.Add(CVE_FGI_SOURCE_OPEN);
            ALL_ENUMS.Add(CVE_FGI_SOURCE_PROTECTED);
            ALL_ENUMS.Add(CVE_NON_IC_MARKINGS);
            ALL_ENUMS.Add(CVE_NON_US_CONTROLS);
            ALL_ENUMS.Add(CVE_NOTICE_TYPE);
            ALL_ENUMS.Add(CVE_OWNER_PRODUCERS);
            ALL_ENUMS.Add(CVE_POC_TYPE);
            ALL_ENUMS.Add(CVE_RELEASABLE_TO);
            ALL_ENUMS.Add(CVE_SAR_IDENTIFIER);
            ALL_ENUMS.Add(CVE_SCI_CONTROLS);
            ALL_ENUMS.Add(CVE_TYPE_EXEMPTED_SOURCE);
            DDMSVersion = DDMSVersion.CurrentVersion;
        }

        /// <summary>
        ///     Maintains a DDMSVersion which will be used to look up the CVE files.
        /// </summary>
        /// <param name="version"> the DDMS version </param>
        public static DDMSVersion DDMSVersion
        {
            set
            {
                lock (typeof (ISMVocabulary))
                {
                    _ddmsVersion = value;
                }
            }
            get { return (_ddmsVersion); }
        }

        /// <summary>
        ///     Accessor for the last enum location.
        /// </summary>
        private static string LastEnumLocation
        {
            get { return (_lastEnumLocation); }
        }

        /// <summary>
        ///     Reloads CVEs if necessary.
        /// </summary>
        private static void UpdateEnumLocation()
        {
            lock (typeof (ISMVocabulary))
            {
                string enumLocation = PropertyReader.GetProperty(DDMSVersion.Version + ".ism.cveLocation");
                if (LastEnumLocation == null || !LastEnumLocation.Equals(enumLocation))
                {
                    _lastEnumLocation = enumLocation;
                    if (LOCATION_TO_ENUM_TOKENS.GetValueOrNull(LastEnumLocation) == null)
                    {
                        try
                        {
                            LOCATION_TO_ENUM_TOKENS[LastEnumLocation] = new Dictionary<string, List<string>>();
                            LOCATION_TO_ENUM_PATTERNS[LastEnumLocation] = new Dictionary<string, List<string>>();
                            foreach (var cve in ALL_ENUMS)
                            {
                                try
                                {
                                    LoadEnumeration(enumLocation, cve);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Could not load controlled vocabularies: " + e.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Opens the enumeration file and extracts a Set of String token values based on the Term elements in the file.
        ///     Stores them in the ENUM_TOKENS map with the key. If a pattern is discovered, it is stored in a separate mapping.
        /// </summary>
        /// <param name="enumLocation"> the classpath resource location for the enumeration files </param>
        /// <param name="builder"> the XOM Builder to read the file with </param>
        /// <param name="enumerationKey"> the key for the enumeration, which doubles as the filename. </param>
        private static void LoadEnumeration(string enumLocation, string enumerationKey)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, enumLocation);
            path = Path.Combine(path.Replace("/", "\\"), enumerationKey);

            XDocument doc = XDocument.Load(path);
            var tokens = new List<string>();
            var patterns = new List<string>();
            string cveNamespace = PropertyReader.GetProperty(DDMSVersion.Version + ".ism.cve.xmlNamespace");
            XElement enumerationElement = doc.Root.Element(XName.Get(ENUMERATION_NAME, cveNamespace));
            IEnumerable<XElement> terms = enumerationElement.Elements(XName.Get(TERM_NAME, cveNamespace));
            foreach (var term in terms)
            {
                XElement value = term.Element(XName.Get(VALUE_NAME, cveNamespace));
                bool isPattern = Convert.ToBoolean((string)value.Attribute(REG_EXP_NAME));
                if (value != null)
                {
                    if (isPattern)
                        patterns.Add(value.Value);
                    else
                        tokens.Add(value.Value);
                }
            }

            if(!LOCATION_TO_ENUM_TOKENS.GetValueOrNull(LastEnumLocation).ContainsKey(enumerationKey))
                LOCATION_TO_ENUM_TOKENS.GetValueOrNull(LastEnumLocation).Add(enumerationKey, tokens);

            if (!LOCATION_TO_ENUM_PATTERNS.GetValueOrNull(LastEnumLocation).ContainsKey(enumerationKey))
                LOCATION_TO_ENUM_PATTERNS.GetValueOrNull(LastEnumLocation).Add(enumerationKey, patterns);
        }

        /// <summary>
        ///     Returns an unmodifiable set of controlled vocabulary tokens. This method is publicly available
        ///     so that these tokens can be used as reference data (for example, a select box on a web form).
        ///     <para>
        ///         If you wish to use these tokens in that way, you must explicitly call <code>setISMVersion()</code>
        ///         in advance, to ensure that the appropriate set of CVE files is used to look up the tokens, OR
        ///         you may use the configurable property, <code>icism.cve.customEnumLocation</code>, to force the
        ///         use of a custom set of CVE files. If neither option is used, the default set of tokens returned
        ///         will be based on the current value of <code>DDMSVersion.CurrentVersion</code>.
        ///     </para>
        /// </summary>
        /// <param name="enumerationKey"> the key of the enumeration </param>
        /// <returns> an unmodifiable set of Strings </returns>
        /// <exception cref="ArgumentException"> if the key does not match a controlled vocabulary </exception>
        public static List<string> GetEnumerationTokens(string enumerationKey)
        {
            UpdateEnumLocation();
            List<string> vocabulary = LOCATION_TO_ENUM_TOKENS.GetValueOrNull(LastEnumLocation).GetValueOrNull(enumerationKey);
            if (vocabulary == null)
                throw new ArgumentException("No controlled vocabulary could be found for this key: " + enumerationKey);
            
            return (vocabulary);
        }

        /// <summary>
        ///     Returns an unmodifiable set of controlled vocabulary regular expression patterns.
        /// </summary>
        /// <param name="enumerationKey"> the key of the enumeration </param>
        /// <returns> an unmodifiable set of Strings </returns>
        private static List<string> GetEnumerationPatterns(string enumerationKey)
        {
            UpdateEnumLocation();
            List<string> vocabulary = LOCATION_TO_ENUM_PATTERNS.GetValueOrNull(LastEnumLocation)[enumerationKey];
            return vocabulary;
        }

        /// <summary>
        ///     Helper method to validate a value from a controlled vocabulary.
        /// </summary>
        /// <param name="enumerationKey"> the key of the enumeration </param>
        /// <param name="value"> the test value </param>
        /// <exception cref="InvalidDDMSException"> if the value is not and validation should result in errors </exception>
        public static void ValidateEnumeration(string enumerationKey, string value)
        {
            DDMSVersion = DDMSVersion;
            if (!EnumContains(enumerationKey, value))
            {
                string message = GetInvalidMessage(enumerationKey, value);
                throw new InvalidDDMSException(message);
            }
        }

        /// <summary>
        ///     Checks if a value exists in the controlled vocabulary identified by the key. If the value does not match the
        ///     tokens, but the CVE also contains patterns, the regular expression pattern is checked next. If neither tokens or
        ///     patterns returns a match, return false.
        /// </summary>
        /// <param name="enumerationKey"> the key of the enumeration </param>
        /// <param name="value"> the test value </param>
        /// <returns> true if the value exists in the enumeration, false otherwise </returns>
        /// <exception cref="ArgumentException"> on an invalid key </exception>
        public static bool EnumContains(string enumerationKey, string value)
        {
            Util.Util.RequireValue("key", enumerationKey);
            bool isValidToken = GetEnumerationTokens(enumerationKey).Contains(value);
            if (!isValidToken)
            {
                foreach (var patternString in GetEnumerationPatterns(enumerationKey))
                {
                    Match regex = Regex.Match(value, patternString);
                    if (regex.Success)
                    {
                        isValidToken = true;
                        break;
                    }
                }
            }
            return (isValidToken);
        }

        /// <summary>
        ///     Checks if one of the classifications that existed in DDMS 2.0 but was removed for DDMS 3.0 is being used.
        /// </summary>
        /// <param name="classification"> the classification to test </param>
        /// <returns> true if it is one of the removed enums, false otherwise </returns>
        public static bool UsingOldClassification(string classification)
        {
            return ("NS-S".Equals(classification) || "NS-A".Equals(classification));
        }

        /// <summary>
        ///     Generates a message for an invalid value.
        /// </summary>
        /// <param name="enumerationKey"> the key of the enumeration </param>
        /// <param name="value"> the test value which was invalid </param>
        /// <returns> a String </returns>
        public static string GetInvalidMessage(string enumerationKey, string value)
        {
            return (value + " is not a valid enumeration token for this attribute, as specified in " + enumerationKey +                    ".");
        }

        /// <summary>
        ///     Validates the value of a network attribute from the IC-COMMON schema.
        /// </summary>
        /// <param name="network"> the network token to test </param>
        /// <exception cref="InvalidDDMSException"> if the network is not a valid token </exception>
        public static void RequireValidNetwork(string network)
        {
            if (!COMMON_NETWORK_TYPES.Contains(network))
                throw new InvalidDDMSException("The network attribute must be one of " + COMMON_NETWORK_TYPES);
        }
    }
}