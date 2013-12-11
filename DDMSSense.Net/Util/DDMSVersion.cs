#region usings

using System.Collections.Generic;
using DDMSSense.DDMS;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Extensions;

#endregion

namespace DDMSSense.Util
{
    /// <summary>
    ///     Manages the supported versions of DDMS.
    ///     <para>
    ///         This class is the extension point for supporting new DDMS versions in the future. DDMSVersion maintains a
    ///         static
    ///         currentVersion variable which can be set at runtime. All DDMS component constructors which build components
    ///         from
    ///         scratch can then call <code>DDMSVersion.getCurrentVersion()</code> to access various details such as schema
    ///         locations and namespace URIs. If no currentVersion has been set, a default will be used, which maps to
    ///         <code>DDMSSense.DDMS.defaultVersion</code> in the properties file. This defaults to 4.1 right now.
    ///     </para>
    ///     <para>
    ///         The ddmsence.properties file has a property, <code>ddms.supportedVersions</code> which can be a comma-separated
    ///         list of version
    ///         numbers. Each of these token values then has a set of properties which identify the namespace and schema
    ///         locations
    ///         for each DDMS version:
    ///     </para>
    ///     <li><code>&lt;versionNumber&gt;.ddms.xmlNamespace</code>: i.e. "urn:us:mil:ces:metadata:ddms:4"</li>
    ///     <li><code>&lt;versionNumber&gt;.ddms.xsdLocation</code>: i.e. "/schemas/4.1/DDMS/ddms.xsd"</li>
    ///     <li><code>&lt;versionNumber&gt;.gml.xmlNamespace</code>: i.e. "http://www.opengis.net/gml/3.2"</li>
    ///     <li><code>&lt;versionNumber&gt;.gml.xsdLocation</code>: i.e. "/schemas/4.1/DDMS/gml.xsd"</li>
    ///     <li><code>&lt;versionNumber&gt;.ism.cveLocation</code>: i.e. "/schemas/4.1/ISM/CVE/"</li>
    ///     <li><code>&lt;versionNumber&gt;.ism.xmlNamespace</code>: i.e. "urn:us:gov:ic:ism"</li>
    ///     <li><code>&lt;versionNumber&gt;.ntk.xmlNamespace</code>: i.e. "urn:us:gov:ic:ntk"</li>
    ///     <li><code>&lt;versionNumber&gt;.ntk.xsdLocation</code>: i.e. "/schemas/4.1/NTK/IC-NTK.xsd"</li>
    ///     <li><code>&lt;versionNumber&gt;.xlink.xmlNamespace</code>: i.e. "http://www.w3.org/1999/xlink"</li>
    ///     <para>
    ///         The format of an xsdLocation should generally follow
    ///         <code>/schemas/&lt;versionNumber&gt;/schemaLocationInDataDirectory</code>.
    ///     </para>
    ///     <para>
    ///         <u>Version-specific Notes:</u>
    ///     </para>
    ///     <para>
    ///         Because DDMS 3.0.1 is syntactically identical to DDMS 3.0, requests for version 3.0.1
    ///         will simply alias to DDMS 3.0. DDMS 3.0.1 is not set up as a separate batch of schemas and namespaces,
    ///         since none of the technical artifacts changed (3.0.1 was a documentation release).
    ///     </para>
    ///     <para>
    ///         Because DDMS 4.1 uses the same XML namespace as DDMS 4.0, resolving the XML namespace to a version
    ///         will always return 4.1 (because it is newer). 4.0.1 is now an alias for 4.1, and warnings will appear when
    ///         using new 4.1 components.
    ///     </para>
    ///     <para>
    ///         This class is intended for use in a single-threaded environment.
    ///     </para>
    
    
    /// </summary>
    public class DDMSVersion
    {
        private static DDMSVersion _currentVersion;

        private static readonly IDictionary<string, DDMSVersion> VERSIONS_TO_DETAILS = new SortedDictionary<string, DDMSVersion>();

        static DDMSVersion()
        {
            foreach (var version in PropertyReader.GetListProperty("ddms.supportedVersions"))
                VERSIONS_TO_DETAILS[version] = new DDMSVersion(version);
            
            _currentVersion = GetVersionFor(PropertyReader.GetProperty("ddms.defaultVersion"));
        }

        /// <summary>
        ///     Private to prevent instantiation
        /// </summary>
        /// <param name="version"> the number as shown in ddms.supportedVersions. </param>
        private DDMSVersion(string version)
        {
            int index = SupportedVersionsProperty.IndexOf(version);
            Version = version;
            Namespace = SupportedDDMSNamespacesProperty[index];
            Schema = PropertyReader.GetProperty(version + ".ddms.xsdLocation");
            GmlNamespace = PropertyReader.GetProperty(version + ".gml.xmlNamespace");
            GmlSchema = PropertyReader.GetProperty(version + ".gml.xsdLocation");
            IsmCveLocation = PropertyReader.GetProperty(version + ".ism.cveLocation");
            IsmNamespace = PropertyReader.GetProperty(version + ".ism.xmlNamespace");
            NtkNamespace = PropertyReader.GetProperty(version + ".ntk.xmlNamespace");
            NtkSchema = PropertyReader.GetProperty(version + ".ntk.xsdLocation");
            XlinkNamespace = PropertyReader.GetProperty(version + ".xlink.xmlNamespace");
        }


        /// <summary>
        ///     Returns a list of supported DDMS versions
        /// </summary>
        /// <returns> List of string version numbers </returns>
        public static List<string> SupportedVersions
        {
            get { return SupportedVersionsProperty; }
        }

        /// <summary>
        ///     Private accessor for the property containing the supported versions list
        /// </summary>
        /// <returns> List of String version numbers </returns>
        private static List<string> SupportedVersionsProperty
        {
            get { return (PropertyReader.GetListProperty("ddms.supportedVersions")); }
        }

        /// <summary>
        ///     Private accessor for the property containing the supported DDMS XML namespace list
        /// </summary>
        /// <returns> List of String version numbers </returns>
        private static List<string> SupportedDDMSNamespacesProperty
        {
            get
            {
                var supportedNamespaces = new List<string>();
                foreach (var version in SupportedVersionsProperty)
                    supportedNamespaces.Add(PropertyReader.GetProperty(version + ".ddms.xmlNamespace"));
                
                return (supportedNamespaces);
            }
        }

        /// <summary>
        ///     Accessor for the version number
        /// </summary>
        public virtual string Version {get; private set;}

        /// <summary>
        ///     Accessor for the DDMS namespace
        /// </summary>
        public virtual string Namespace {get; private set;}

        /// <summary>
        ///     Accessor for the DDMS schema location
        /// </summary>
        public virtual string Schema {get; private set;}

        /// <summary>
        ///     Accessor for the gml namespace
        /// </summary>
        public virtual string GmlNamespace {get; private set;}

        /// <summary>
        ///     Accessor for the gml schema location
        /// </summary>
        public virtual string GmlSchema {get; private set;}

        /// <summary>
        ///     Accessor for the ISM CVE location
        /// </summary>
        public virtual string IsmCveLocation {get; private set;}

        /// <summary>
        ///     Accessor for the ISM namespace
        /// </summary>
        public virtual string IsmNamespace {get; private set;}

        /// <summary>
        ///     Accessor for the NTK namespace
        /// </summary>
        public virtual string NtkNamespace {get; private set;}

        /// <summary>
        ///     Accessor for the NTK schema location
        /// </summary>
        public virtual string NtkSchema {get; private set;}

        /// <summary>
        ///     Accessor for the xlink namespace
        /// </summary>
        public virtual string XlinkNamespace {get; private set;}

        /// <summary>
        ///     Convenience method to check if a DDMS version number is equal to or higher that some
        ///     test number. An example of where this might be used is to determine the capitalization
        ///     of element names, many of which changed in DDMS 4.0.1.
        /// </summary>
        /// <param name="version"> the version number to check </param>
        /// <returns> true if the version is equal to or greater than the test version </returns>
        public virtual bool IsAtLeast(string version)
        {
            version = AliasVersion(version);
            if (!SupportedVersionsProperty.Contains(version))
                throw new UnsupportedVersionException(version);
            
            int index = SupportedVersionsProperty.IndexOf(Version);
            int testIndex = SupportedVersionsProperty.IndexOf(version);
            return (index >= testIndex);
        }

        /// <summary>
        ///     Checks if an XML namespace is included in the list of supported XML namespaces for DDMS
        /// </summary>
        /// <param name="xmlNamespace"> the namespace to test </param>
        /// <returns> true if the namespace is supported </returns>
        public static bool IsSupportedDDMSNamespace(string xmlNamespace)
        {
            return (SupportedDDMSNamespacesProperty.Contains(xmlNamespace));
        }

        /// <summary>
        ///     Returns the DDMSVersion instance mapped to a particular version number.
        /// </summary>
        /// <param name="version"> a version number </param>
        /// <returns> the instance </returns>
        /// <exception cref="UnsupportedVersionException"> if the version number is not supported </exception>
        public static DDMSVersion GetVersionFor(string version)
        {
            version = AliasVersion(version);
            if (!SupportedVersionsProperty.Contains(version))
                throw new UnsupportedVersionException(version);
            
            return (VERSIONS_TO_DETAILS.GetValueOrNull(version));
        }

        /// <summary>
        ///     Returns the DDMSVersion instance mapped to a particular XML namespace. If the
        ///     namespace is shared by multiple versions of DDMS, the most recent will be
        ///     returned.
        /// </summary>
        /// <param name="namespace"> the XML namespace </param>
        /// <returns> the instance </returns>
        /// <exception cref="UnsupportedVersionException"> if the version number is not supported </exception>
        public static DDMSVersion GetVersionForNamespace(string @namespace)
        {
            var versions = new List<DDMSVersion>(VERSIONS_TO_DETAILS.Values);
            versions.Reverse();
            foreach (var version in versions)
            {
                if (version.Namespace.Equals(@namespace) || 
                    version.IsmNamespace.Equals(@namespace) ||
                    version.NtkNamespace.Equals(@namespace) || 
                    version.GmlNamespace.Equals(@namespace))
                    return (version);
            }
            throw new UnsupportedVersionException("for XML namespace " + @namespace);
        }

        /// <summary>
        ///     Sets the currentVersion which will be used for by DDMS component constructors to determine the namespace and
        ///     schema to use. Also updates the ISMVersion on the ISMVocabulary class, which is used to determine
        ///     which set of IC CVEs to validate with.
        /// </summary>
        /// <param name="version"> the new version, which must be supported by DDMSence </param>
        /// <returns> the version which was just set, as a full-fledged DDMSVersion object </returns>
        /// <exception cref="UnsupportedVersionException"> if the version is not supported </exception>
        public static DDMSVersion SetCurrentVersion(string version)
        {
            lock (typeof(DDMSVersion))
            {
                version = AliasVersion(version);
                if (!SupportedVersionsProperty.Contains(version))
                    throw new UnsupportedVersionException(version);
                
                _currentVersion = GetVersionFor(version);
                ISMVocabulary.DDMSVersion = GetCurrentVersion();
                return (GetCurrentVersion());
            }
        }

        /// <summary>
        ///     Treats version 3.0.1 of DDMS as an alias for DDMS 3.0, and treats version 4.0.1 as an alias for DDMS 4.1.
        ///     3.0.1 is syntactically identical, and has the same namespaces and schemas. 4.0.1 shares the same
        ///     XML namespace as 4.1.
        /// </summary>
        /// <param name="version"> the raw version </param>
        /// <returns> the aliased version </returns>
        private static string AliasVersion(string version)
        {
            if ("3.0.1".Equals(version))
                return ("3.0");
            
            if ("4.0.1".Equals(version))
                return ("4.1");
            
            return (version);
        }

        /// <summary>
        ///     Accessor for the current version. If not set, returns the default from the properties file.
        /// </summary>
        public static DDMSVersion GetCurrentVersion()
        {
            return (_currentVersion);
        }

        /// <summary>
        ///     Resets the current version to the default value.
        /// </summary>
        public static void ClearCurrentVersion()
        {
            SetCurrentVersion(PropertyReader.GetProperty("ddms.defaultVersion"));
        }

        /// <see cref="Object#toString()"></see>
        public override string ToString()
        {
            return (Version);
        }
    }
}