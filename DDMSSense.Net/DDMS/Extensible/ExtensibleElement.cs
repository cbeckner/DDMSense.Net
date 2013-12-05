using System;

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
namespace DDMSSense.DDMS.Extensible {


	using Document = System.Xml.Linq.XDocument;
	using Element = System.Xml.Linq.XElement;
    
	using XMLReader = System.Xml.XmlReader;
	using XMLReaderFactory = org.xml.sax.helpers.XMLReaderFactory;

	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using System.IO;
    using DDMSSense.DDMS;

	/// <summary>
	/// An immutable implementation of an element which might fulfill the xs:any space in the Extensible Layer.
	/// 
	/// <para>Starting in DDMS 3.0, zero to many of these elements may appear in a ddms:resource and can live in any other
	/// namespace besides the DDMS namespace. In DDMS 2.0, only one of these is allowed.</para>
	/// 
	/// <para>No validation or processing of any kind is performed by DDMSence on extensible attributes, other than the base
	/// validation used when loading attributes from an XML file. This class merely exposes a <code>getXOMElementCopy()</code> 
	/// method which returns a XOM Element that can be manipulated in business-specific ways.</para>
	/// 
	/// <para>XOM elements can be created as follows:</para>
	/// 
	/// <ul><code>
	/// Element element = new Element("ddmsence:extension", "http://ddmsence.urizone.net/");<br />
	/// element.Add("This will be the child text.");
	/// </code></ul>
	/// 
	/// <para>Because it is impossible to cover all of the HTML/Text output cases for ExtensibleElements, DDMSence
	/// will simply print out the existence of extensible elements:</para>
	/// <ul><code>
	/// Extensible Layer: true<br />
	/// &lt;meta name="extensible.layer" content="true" /&gt;<br />
	/// </code></ul></p>
	/// 
	/// <para>Details about the XOM Element class can be found at:
	/// <i>http://www.xom.nu/apidocs/index.html?nu/xom/Element.html</i></para>
	/// 
	/// @author Brian Uri!
	/// @since 1.1.0
	/// </summary>
	public sealed class ExtensibleElement : AbstractBaseComponent {

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public ExtensibleElement(Element element) : base(element) {
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The namespace cannot be the DDMS namespace.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			if (DDMSVersion.IsSupportedDDMSNamespace(Namespace)) {
				throw new InvalidDDMSException("Extensible elements cannot be defined in the DDMS namespace.");
			}
			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			return ("");
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is ExtensibleElement)) {
				return (false);
			}
			ExtensibleElement test = (ExtensibleElement) obj;
            return (Element.ToString().Equals(test.Element.ToString()));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
            result = 7 * result + Element.ToString().GetHashCode();
			return (result);
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 7276942157278555643L;
			internal string _xml;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(ExtensibleElement element) {
				Xml = element.ToString();
			}

			/// <seealso cref= IBuilder#commit() </seealso>


			public virtual ExtensibleElement Commit() {
				if (Empty) {
					return (null);
				}
				try {
					XMLReader reader = XMLReaderFactory.createXMLReader(PropertyReader.GetProperty("xml.reader.class"));
					nu.xom.Builder builder = new nu.xom.Builder(reader, false);
					Document doc = builder.build(new StringReader(Xml));
					return (new ExtensibleElement(doc.Root));
				} catch (Exception e) {
					throw new InvalidDDMSException("Could not create a valid element from XML string: " + e.Message);
				}
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Xml));
				}
			}

			/// <summary>
			/// Builder accessor for the XML string representing the element.
			/// </summary>
			public virtual string Xml {
				get {
					return _xml;
				}
				set {
					_xml = value;
				}
			}

		}
	}
}