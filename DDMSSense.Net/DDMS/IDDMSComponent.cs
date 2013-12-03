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
namespace DDMSSense.DDMS {

    using DDMSSense.DDMS;
    using Element = System.Xml.Linq.XElement;
    using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;

	/// <summary>
	/// Interface for a single DDMS element.
	/// 
	/// <para>
	/// All DDMS components should be valid after instantiation and can output themselves in various formats. Implementation
	/// classes are divided up into packages based on the five Core sets: Metacard Info, Security, Resource, Summary Content, 
	/// and Format.
	/// </para>
	/// 
	/// <para>
	/// Accessors which return string-based data will always favour an empty string over a null value.
	/// </para>
	/// 
	/// <para>
	/// In general, Java classes are implemented for:
	/// <ul>
	/// <li>Elements which are explicitly declared as DDMS Categories in the DDMS documentation are always implemented
	/// (ddms:identifier).</li>
	/// <li>Elements which merely enclose important data AND which have no special attributes are never implemented
	/// (ddms:Media).</li>
	/// <li>Data which can be represented as a simple Java type AND which has no special attributes is represented as a
	/// simple Java type (ddms:email).</li>
	/// <li>Attributes are generally implemented as properties on an Object. The exception to this is the ISM
	/// AttributeGroup, which decorates many DDMS components.</li>
	/// </ul>
	/// </para>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public interface IDDMSComponent {

		/// <summary>
		/// Accessor for the prefix of the component, without a trailing colon
		/// </summary>
		/// <returns> the prefix </returns>
		string Prefix { get; }

		/// <summary>
		/// Accessor for the name of the component
		/// </summary>
		/// <returns> the name, without prefix </returns>
		string Name { get; }

		/// <summary>
		/// Accessor for the XML namespace of the component
		/// </summary>
		/// <returns> the namespace </returns>
		string Namespace { get; }

		/// <summary>
		/// Accessor for the name of the component, including the prefix
		/// </summary>
		/// <returns> the name, with prefix if available </returns>
		string QualifiedName { get; }

		/// <summary>
		/// Returns any security attributes attached to this component. If the component is not allowed to have attributes,
		/// this will be null. Otherwise, a valid SecurityAttributes object will be returned, even if none of its properties
		/// are set.
		/// </summary>
		SecurityAttributes SecurityAttributes { get; }

		/// <summary>
		/// Returns a list of any warning messages that occurred during validation. Warnings do not prevent a valid component
		/// from being formed. A parent component should also claim the warnings of its child elements.
		/// </summary>
		/// <returns> a list of warnings </returns>
		List<ValidationMessage> ValidationWarnings { get; }

		/// <summary>
		/// Renders this component as HTML.
		/// </summary>
		/// <returns> the HTML representation of this component </returns>
		string ToHTML();

		/// <summary>
		/// Renders this component as Text.
		/// </summary>
		/// <returns> the text-based representation of this component </returns>
		string ToText();

		/// <summary>
		/// Renders this component as XML.
		/// </summary>
		/// <returns> the XML representation of this component </returns>
		string ToXML();

		/// <summary>
		/// Accessor for a copy of the underlying XOM element. This allows a XOM tree to be built from DDMS data when
		/// traversing a list of IDDMSComponents.
		/// </summary>
		Element XOMElementCopy { get; }
	}

}