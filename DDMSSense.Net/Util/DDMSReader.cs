#region usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DDMSense.DDMS;

#endregion

namespace DDMSense.Util
{
    /// <summary>
    ///     Reader class which loads an XML file containing DDMS information and converts it into XOM elements.
    ///     <para>
    ///         This parsing performs schema validation against a local set of DDMS/ISM schemas.
    ///     </para>
    
    
    /// </summary>
    public class DDMSReader
    {
        private const string PROP_XERCES_VALIDATION = "http://xml.org/sax/features/validation";
        private const string PROP_XERCES_SCHEMA_VALIDATION = "http://apache.org/xml/features/validation/schema";
        private const string PROP_XERCES_EXTERNAL_LOCATION = "http://apache.org/xml/properties/schema/external-schemaLocation";

        private readonly XmlReader _reader;

        /// <summary>
        ///     Constructor
        ///     <para>Schemas are loaded in reverse order, so the latest, greatest copy is always first to be looked for.</para>
        ///     Creates a DDMSReader which can process various versions of DDMS and GML
        /// </summary>
        public DDMSReader()
        {
            _reader = null;
            var schemas = new StringBuilder();
            var versions = new List<string>(DDMSVersion.SupportedVersions);
            versions.Reverse();
            var processedNamespaces = new List<string>();
            foreach (var versionString in versions)
            {
                DDMSVersion version = DDMSVersion.GetVersionFor(versionString);
                LoadSchema(version.Namespace, version.Schema, schemas, processedNamespaces);
                LoadSchema(version.GmlNamespace, version.GmlSchema, schemas, processedNamespaces);
                LoadSchema(version.NtkNamespace, version.NtkSchema, schemas, processedNamespaces);
            }
            //TODO: Find alternative for SetFeature and SetProperty
            //Reader.setFeature(PROP_XERCES_VALIDATION, true);
            //Reader.setFeature(PROP_XERCES_SCHEMA_VALIDATION, true);
            //Reader.setProperty(PROP_XERCES_EXTERNAL_LOCATION, schemas.ToString().Trim());
        }

        /// <summary>
        ///     Returns the external schema locations for debugging. The returned string will contain a space-delimited set
        ///     of XMLNamespace/SchemaLocation pairs.
        /// </summary>
        /// <returns> the string containing all schema locations </returns>
        public virtual string ExternalSchemaLocations
        {
            get
            {
                try
                {
                    return Reader.GetAttribute(PROP_XERCES_EXTERNAL_LOCATION);
                }
                catch (Exception)
                {
                    throw new XmlException(PROP_XERCES_EXTERNAL_LOCATION +
                                           " is not supported or recognized for this XMLReader.");
                }
            }
        }

        /// <summary>
        ///     Accessor for the reader
        /// </summary>
        private XmlReader Reader
        {
            get { return _reader; }
        }

        /// <summary>
        ///     Helper method to load schemas into the property for the XML Reader
        /// </summary>
        /// <param name="namespace"> the XML namespace </param>
        /// <param name="schemaLocation"> the schema location </param>
        /// <param name="schemas"> the buffer to add the schema location to </param>
        /// <param name="processedNamespaces"> namespaces which have already been loaded </param>
        private void LoadSchema(string @namespace, string schemaLocation, StringBuilder schemas,List<string> processedNamespaces)
        {
            if (processedNamespaces.Contains(@namespace)) return;
            if (!String.IsNullOrEmpty(schemaLocation))
            {
                string xsd = GetLocalSchemaLocation(schemaLocation);
                schemas.Append(@namespace).Append(" ").Append(xsd).Append(" ");
            }
            processedNamespaces.Add(@namespace);
        }

        /// <summary>
        ///     Returns the full path to a local schema copy, based on the relative location from the
        ///     properties file. The full path will have spaces escaped as %20, to resolve Issue 50
        ///     in the DDMSence Issue Tracker.
        /// </summary>
        /// <param name="schemaLocation"> the relative schema location as specified in the properties file </param>
        /// <returns> the full path to the schema (generally this is in the JAR file) </returns>
        /// <exception cref="ArgumentException"> if the schema could not be found. </exception>
        private string GetLocalSchemaLocation(string schemaLocation)
        {
            string xsd = ConfigurationManager.AppSettings[schemaLocation];
            if (xsd == null)
                throw new ArgumentException("Unable to load a local copy of the schema for validation.");

            return xsd;
        }

        /// <summary>
        ///     Attempts to build an Element from a Resource XML string. Element-based constructors
        ///     for a Resource are automatically validated against a schema when the XML parser
        ///     loads it. This method allows the data-driven constructors for a Resource to do
        ///     a final confirmation that none of the data breaks any schema rules.
        /// </summary>
        /// <param name="resourceXml"> the XML of the resource to check </param>
        /// <exception cref="InvalidDDMSException"> if the resource is invalid </exception>
        public static void ValidateWithSchema(string resourceXml)
        {
            try
            {
                (new DDMSReader()).GetElement(resourceXml);
            }
            catch (IOException e)
            {
                throw new InvalidDDMSException(e);
            }
            catch (Exception e)
            {
                throw new InvalidDDMSException(e);
            }
        }

        /// <summary>
        ///     Creates a XOM element representing the root XML element in the file.
        ///     <para>The implementation of this method delegates to the Reader-based overloaded method.</para>
        /// </summary>
        /// <param name="path">the file containing the XML document </param>
        /// <returns> a XOM element representing the root node in the document </returns>
        protected virtual XElement GetElementFromFile(string path)
        {
            Util.RequireValue("path", path);
            try
            {
                XDocument doc = XDocument.Load(path);
                return doc.Root;
            }
            catch (XmlException e)
            {
                throw new InvalidDDMSException(e);
            }
        }

        /// <summary>
        ///     Creates a XOM element representing the root XML element in a string representation of an XML document.
        ///     <para>The implementation of this method delegates to the Reader-based overloaded method.</para>
        /// </summary>
        /// <param name="xml"> a string containing the XML document </param>
        /// <returns> a XOM element representing the root node in the document  </returns>
        public virtual XElement GetElement(string xml)
        {
            Util.RequireValue("XML string", xml);
            return (GetElement(xml));
        }

        /// <summary>
        ///     Creates a XOM element representing the root XML element in a reader.
        /// </summary>
        /// <param name="reader"> a reader mapping to an XML document </param>
        /// <returns> a XOM element representing the root node in the document </returns>
        public virtual XElement GetElement(Stream reader)
        {
            Util.RequireValue("reader", reader);
            try
            {
                XDocument doc = XDocument.Load(reader);
                return (doc.Root);
            }
            catch (XmlException e)
            {
                throw new InvalidDDMSException(e);
            }
        }

        /// <summary>
        ///     Creates a DDMS resource based on the contents of a file, and also sets the DDMSVersion based on the namespace
        ///     URIs in the file.
        /// </summary>
        /// <param name="path"> the file containing the DDMS Resource. </param>
        /// <returns> a DDMS Resource </returns>
        /// <exception cref="InvalidDDMSException"> if the component could not be built </exception>
        public virtual Resource GetDDMSResourceFromFile(string path)
        {
            return (BuildResource(GetElementFromFile(path)));
        }

        /// <summary>
        ///     Creates a DDMS resource based on the contents of a string representation of an XML document, and also sets the
        ///     DDMSVersion based on the namespace URIs in the document.
        /// </summary>
        /// <param name="xml"> the string representation of the XML DDMS Resource </param>
        /// <returns> a DDMS Resource </returns>
        /// <exception cref="InvalidDDMSException"> if the component could not be built </exception>
        public virtual Resource GetDDMSResource(string xml)
        {
            return (BuildResource(GetElement(xml)));
        }

        /// <summary>
        ///     Creates a DDMS resource based on the contents of an input stream, and also sets the DDMSVersion based on the
        ///     namespace URIs in the document.
        /// </summary>
        /// <param name="inputStream"> the input stream wrapped around an XML DDMS Resource </param>
        /// <returns> a DDMS Resource </returns>
        /// <exception cref="InvalidDDMSException"> if the component could not be built </exception>
        public virtual Resource GetDDMSResource(Stream inputStream)
        {
            return (BuildResource(GetElement(inputStream)));
        }

        /// <summary>
        ///     Shared helper method to build a DDMS Resource from a XOM Element
        /// </summary>
        /// <param name="element"> </param>
        /// <returns> a DDMS Resource </returns>
        /// <exception cref="InvalidDDMSException"> if the component could not be built </exception>
        protected virtual Resource BuildResource(XElement element)
        {
            DDMSVersion.SetCurrentVersion(DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName).Version);
            return (new Resource(element));
        }
    }
}