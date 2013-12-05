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
namespace DDMSSense.Util {
	using Document = System.Xml.Linq.XDocument;
	using Element = System.Xml.Linq.XElement;
	using ParsingException = System.Xml.XmlException;
	using XMLReader = System.Xml.XmlReader;
	using InvalidDDMSException = DDMSSense.DDMS.InvalidDDMSException;
	using Resource = DDMSSense.DDMS.Resource;
    using System.IO;
    using System;
    using System.Xml;

	/// <summary>
	/// Reader class which loads an XML file containing DDMS information and converts it into XOM elements.
	/// 
	/// <para>
	/// This parsing performs schema validation against a local set of DDMS/ISM schemas.
	/// </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public class DDMSReader {

		private XMLReader _reader;

		private const string PROP_XERCES_VALIDATION = "http://xml.org/sax/features/validation";
		private const string PROP_XERCES_SCHEMA_VALIDATION = "http://apache.org/xml/features/validation/schema";
		private const string PROP_XERCES_EXTERNAL_LOCATION = "http://apache.org/xml/properties/schema/external-schemaLocation";

		/// <summary>
		/// Constructor
		/// 
		/// <para>Schemas are loaded in reverse order, so the latest, greatest copy is always first to be looked for.</para>
		/// 
		/// Creates a DDMSReader which can process various versions of DDMS and GML
		/// </summary>


		public DDMSReader() {
			_reader = XMLReaderFactory.createXMLReader(PropertyReader.GetProperty("xml.reader.class"));
			StringBuilder schemas = new StringBuilder();
			List<string> versions = new List<string>(DDMSVersion.SupportedVersions);
			versions.Reverse();
			List<string> processedNamespaces = new List<string>();
			foreach (string versionString in versions) {
				DDMSVersion version = DDMSVersion.GetVersionFor(versionString);
				LoadSchema(version.Namespace, version.Schema, schemas, processedNamespaces);
				LoadSchema(version.GmlNamespace, version.GmlSchema, schemas, processedNamespaces);
				LoadSchema(version.NtkNamespace, version.NtkSchema, schemas, processedNamespaces);
			}
			Reader.setFeature(PROP_XERCES_VALIDATION, true);
			Reader.setFeature(PROP_XERCES_SCHEMA_VALIDATION, true);
			Reader.setProperty(PROP_XERCES_EXTERNAL_LOCATION, schemas.ToString().Trim());
		}

		/// <summary>
		/// Helper method to load schemas into the property for the XML Reader
		/// </summary>
		/// <param name="namespace"> the XML namespace </param>
		/// <param name="schemaLocation"> the schema location </param>
		/// <param name="schemas"> the buffer to add the schema location to </param>
		/// <param name="processedNamespaces"> namespaces which have already been loaded </param>
		private void LoadSchema(string @namespace, string schemaLocation, StringBuilder schemas, List<string> processedNamespaces) {
			if (!processedNamespaces.Contains(@namespace)) {
				if (!String.IsNullOrEmpty(schemaLocation)) {
					string xsd = GetLocalSchemaLocation(schemaLocation);
					schemas.Append(@namespace).Append(" ").Append(xsd).Append(" ");
				}
				processedNamespaces.Add(@namespace);
			}
		}

		/// <summary>
		/// Returns the full path to a local schema copy, based on the relative location from the
		/// properties file. The full path will have spaces escaped as %20, to resolve Issue 50
		/// in the DDMSence Issue Tracker.
		/// </summary>
		/// <param name="schemaLocation"> the relative schema location as specified in the properties file </param>
		/// <returns> the full path to the schema (generally this is in the JAR file) </returns>
		/// <exception cref="IllegalArgumentException"> if the schema could not be found. </exception>
		private string GetLocalSchemaLocation(string schemaLocation) {
			Uri xsd = this.GetType().getResource(schemaLocation);
			if (xsd == null) {
				throw new System.ArgumentException("Unable to load a local copy of the schema for validation.");
			}
			string fullPath = xsd.toExternalForm().replaceAll(" ", "%20");
			return (fullPath);
		}

		/// <summary>
		/// Attempts to build an Element from a Resource XML string. Element-based constructors
		/// for a Resource are automatically validated against a schema when the XML parser
		/// loads it. This method allows the data-driven constructors for a Resource to do
		/// a final confirmation that none of the data breaks any schema rules.
		/// </summary>
		/// <param name="resourceXML"> the XML of the resource to check </param>
		/// <exception cref="InvalidDDMSException"> if the resource is invalid </exception>


		public static void ValidateWithSchema(string resourceXML) {
			try {
				(new DDMSReader()).GetElement(resourceXML);
			} catch (SAXException e) {
				throw new InvalidDDMSException(e);
			} catch (IOException e) {
				throw new InvalidDDMSException(e);
			}
		}

		/// <summary>
		/// Creates a XOM element representing the root XML element in the file.
		/// 
		/// <para>The implementation of this method delegates to the Reader-based overloaded method.</para>
		/// </summary>
		/// <param name="file"> the file containing the XML document </param>
		/// <returns> a XOM element representing the root node in the document </returns>


		public virtual Element GetElement(FileStream file) {
			Util.RequireValue("file", file);
			return (GetElement(new FileReader(file)));
		}

		/// <summary>
		/// Creates a XOM element representing the root XML element in a string representation of an XML document.
		/// 
		/// <para>The implementation of this method delegates to the Reader-based overloaded method.</para>
		/// </summary>
		/// <param name="xml"> a string containing the XML document </param>
		/// <returns> a XOM element representing the root node in the document  </returns>


		public virtual Element GetElement(string xml) {
			Util.RequireValue("XML string", xml);
			return (GetElement(new StringReader(xml)));
		}

		
		/// <summary>
		/// Creates a XOM element representing the root XML element in a reader.
		/// </summary>
		/// <param name="reader"> a reader mapping to an XML document </param>
		/// <returns> a XOM element representing the root node in the document </returns>


		public virtual Element GetElement(Stream reader) {
			Util.RequireValue("reader", reader);
			try {
				Builder builder = new Builder(Reader, true);
				Document doc = builder.build(reader);
				return (doc.Root);
			} catch (ParsingException e) {
				throw new InvalidDDMSException(e);
			}
		}

		/// <summary>
		/// Creates a DDMS resource based on the contents of a file, and also sets the DDMSVersion based on the namespace
		/// URIs in the file.
		/// </summary>
		/// <param name="file"> the file containing the DDMS Resource. </param>
		/// <returns> a DDMS Resource </returns>
		/// <exception cref="InvalidDDMSException"> if the component could not be built </exception>


		public virtual Resource GetDDMSResource(FileStream file) {
			return (BuildResource(GetElement(file)));
		}

		/// <summary>
		/// Creates a DDMS resource based on the contents of a string representation of an XML document, and also sets the
		/// DDMSVersion based on the namespace URIs in the document.
		/// </summary>
		/// <param name="xml"> the string representation of the XML DDMS Resource </param>
		/// <returns> a DDMS Resource </returns>
		/// <exception cref="InvalidDDMSException"> if the component could not be built </exception>


		public virtual Resource GetDDMSResource(string xml) {
			return (BuildResource(GetElement(xml)));
		}

		/// <summary>
		/// Creates a DDMS resource based on the contents of an input stream, and also sets the DDMSVersion based on the
		/// namespace URIs in the document.
		/// </summary>
		/// <param name="inputStream"> the input stream wrapped around an XML DDMS Resource </param>
		/// <returns> a DDMS Resource </returns>
		/// <exception cref="InvalidDDMSException"> if the component could not be built </exception>


		public virtual Resource GetDDMSResource(Stream inputStream) {
			return (BuildResource(GetElement(inputStream)));
		}

		/// <summary>
		/// Creates a DDMS resource based on the contents of a reader, and also sets the DDMSVersion based on the namespace
		/// URIs in the document.
		/// </summary>
		/// <param name="reader"> the reader wrapped around an XML DDMS Resource </param>
		/// <returns> a DDMS Resource </returns>
		/// <exception cref="InvalidDDMSException"> if the component could not be built </exception>


		public virtual Resource GetDDMSResource(StreamReader reader) {
			return (BuildResource(GetElement(reader)));
		}

		/// <summary>
		/// Shared helper method to build a DDMS Resource from a XOM Element
		/// </summary>
		/// <param name="xomElement"> </param>
		/// <returns> a DDMS Resource </returns>
		/// <exception cref="InvalidDDMSException"> if the component could not be built </exception>


		protected internal virtual Resource BuildResource(Element xomElement) {
            DDMSVersion.SetCurrentVersion(DDMSVersion.GetVersionForNamespace(xomElement.Name.NamespaceName).Version);
			return (new Resource(xomElement));
		}

		/// <summary>
		/// Returns the external schema locations for debugging. The returned string will contain a space-delimited set
		/// of XMLNamespace/SchemaLocation pairs.
		/// </summary>
		/// <returns> the string containing all schema locations </returns>
		public virtual string ExternalSchemaLocations {
			get {
				try {
					return ((string) Reader.getProperty(PROP_XERCES_EXTERNAL_LOCATION));
				} catch (SAXException) {
					throw new XmlException(PROP_XERCES_EXTERNAL_LOCATION + " is not supported or recognized for this XMLReader.");
				}
			}
		}

		/// <summary>
		/// Accessor for the reader
		/// </summary>
		private XMLReader Reader {
			get {
				return _reader;
			}
		}
	}
}