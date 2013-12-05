using System;
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
namespace DDMSSense.DDMS.Summary {

	using Element = System.Xml.Linq.XElement;
	using ExtensibleAttributes = DDMSSense.DDMS.Extensible.ExtensibleAttributes;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:category.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>The label value must be non-empty.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:qualifier</u>: A URI-based qualifier (optional)<br />
	/// <u>ddms:code</u>: The machine readable description of a concept represented within the scope of the category
	/// qualifier (optional)<br />
	/// <u>ddms:label</u>: The human readable representation of the concept that corresponds to the category qualifier and
	/// the category code, if they exist. (required)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional (starting in DDMS 
	/// 4.0.1).<br />
	/// <u><seealso cref="ExtensibleAttributes"/></u>: (optional, starting in DDMS 3.0).
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Category : AbstractBaseComponent {

		private SecurityAttributes _securityAttributes = null;
		private ExtensibleAttributes _extensibleAttributes = null;

		private const string QUALIFIER_NAME = "qualifier";
		private const string CODE_NAME = "code";
		private const string LABEL_NAME = "label";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Category(Element element) {
			try {
				_securityAttributes = new SecurityAttributes(element);
				_extensibleAttributes = new ExtensibleAttributes(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="qualifier"> the qualifier (optional) </param>
		/// <param name="code"> the code (optional) </param>
		/// <param name="label"> the label (required) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Category(string qualifier, string code, string label, SecurityAttributes securityAttributes) : this(qualifier, code, label, securityAttributes, null) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// 
		/// <para>This constructor will throw an InvalidDDMSException if the extensible attributes uses the reserved
		/// attributes, "ddms:qualifier", "ddms:code", or "ddms:label".</para>
		/// </summary>
		/// <param name="qualifier"> the qualifier (optional) </param>
		/// <param name="code"> the code (optional) </param>
		/// <param name="label"> the label (required) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>
		/// <param name="extensibleAttributes"> extensible attributes (optional) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Category(string qualifier, string code, string label, SecurityAttributes securityAttributes, ExtensibleAttributes extensibleAttributes) {
			try {
				Element element = Util.BuildDDMSElement(Category.GetName(DDMSVersion.GetCurrentVersion()), null);
				Util.AddDDMSAttribute(element, QUALIFIER_NAME, qualifier);
				Util.AddDDMSAttribute(element, CODE_NAME, code);
				Util.AddDDMSAttribute(element, LABEL_NAME, label);
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				_extensibleAttributes = ExtensibleAttributes.GetNonNullInstance(extensibleAttributes);
				_extensibleAttributes.AddTo(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>A label exists and is not empty.</li>
		/// <li>If a qualifier exists, it is a valid URI.</li>
		/// <li>The SecurityAttributes do not exist  until DDMS 4.0.1 or later.</li>
		/// <li>No extensible attributes can exist until DDMS 3.0 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Category.GetName(DDMSVersion));
			Util.RequireDDMSValue("label attribute", Label);
			if (!String.IsNullOrEmpty(Qualifier)) {
				Util.RequireDDMSValidURI(Qualifier);
			}
			// Should be reviewed as additional versions of DDMS are supported.
			if (!DDMSVersion.IsAtLeast("4.0.1") && !SecurityAttributes.Empty) {
				throw new InvalidDDMSException("Security attributes cannot be applied to this component until DDMS 4.0.1 or later.");
			}
			if (!DDMSVersion.IsAtLeast("3.0") && !ExtensibleAttributes.Empty) {
				throw new InvalidDDMSException("xs:anyAttribute cannot be applied to ddms:category until DDMS 3.0 or later.");
			}

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + QUALIFIER_NAME, Qualifier));
			text.Append(BuildOutput(isHTML, localPrefix + CODE_NAME, Code));
			text.Append(BuildOutput(isHTML, localPrefix + LABEL_NAME, Label));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			text.Append(ExtensibleAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Category)) {
				return (false);
			}
			Category test = (Category) obj;
			return (Qualifier.Equals(test.Qualifier) && Code.Equals(test.Code) && Label.Equals(test.Label) && ExtensibleAttributes.Equals(test.ExtensibleAttributes));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + Qualifier.GetHashCode();
			result = 7 * result + Code.GetHashCode();
			result = 7 * result + Label.GetHashCode();
			result = 7 * result + ExtensibleAttributes.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("category");
		}

		/// <summary>
		/// Accessor for the qualifier attribute.
		/// </summary>
		public string Qualifier {
			get {
				return (GetAttributeValue(QUALIFIER_NAME));
			}
			set {
					_qualifier = value;
			}
		}

		/// <summary>
		/// Accessor for the code attribute.
		/// </summary>
		public string Code {
			get {
				return (GetAttributeValue(CODE_NAME));
			}
			set {
					_code = value;
			}
		}

		/// <summary>
		/// Accessor for the label attribute.
		/// </summary>
		public string Label {
			get {
				return (GetAttributeValue(LABEL_NAME));
			}
			set {
					_label = value;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes. Will always be non-null, even if it has no values set.
		/// </summary>
		public override SecurityAttributes SecurityAttributes {
			get {
				return (_securityAttributes);
			}
			set {
					_securityAttributes = value;
			}
		}

		/// <summary>
		/// Accessor for the extensible attributes. Will always be non-null, even if not set.
		/// </summary>
		public ExtensibleAttributes ExtensibleAttributes {
			get {
				return (_extensibleAttributes);
			}
			set {
					_extensibleAttributes = value;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = -9012648230977148516L;
			internal string _qualifier;
			internal string _code;
			internal string _label;
			internal SecurityAttributes.Builder _securityAttributes;
			internal ExtensibleAttributes.Builder _extensibleAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Category category) {
				Qualifier = category.Qualifier;
				Code = category.Code;
				Label = category.Label;
				SecurityAttributes = new SecurityAttributes.Builder(category.SecurityAttributes);
				ExtensibleAttributes = new ExtensibleAttributes.Builder(category.ExtensibleAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public virtual IDDMSComponent Commit()
            {
				return (Empty ? null : new Category(Qualifier, Code, Label, SecurityAttributes.Commit(), ExtensibleAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Code) && String.IsNullOrEmpty(Label) && SecurityAttributes.Empty && ExtensibleAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the qualifier attribute
			/// </summary>
			public virtual string Qualifier {
				get {
					return _qualifier;
				}
			}


			/// <summary>
			/// Builder accessor for the code attribute
			/// </summary>
			public virtual string Code {
				get {
					return _code;
				}
			}


			/// <summary>
			/// Builder accessor for the label attribute
			/// </summary>
			public virtual string Label {
				get {
					return _label;
				}
			}


			/// <summary>
			/// Builder accessor for the Security Attributes
			/// </summary>
			public virtual SecurityAttributes.Builder SecurityAttributes {
				get {
					if (_securityAttributes == null) {
						_securityAttributes = new SecurityAttributes.Builder();
					}
					return _securityAttributes;
				}
			}


			/// <summary>
			/// Builder accessor for the Extensible Attributes
			/// </summary>
			public virtual ExtensibleAttributes.Builder ExtensibleAttributes {
				get {
					if (_extensibleAttributes == null) {
						_extensibleAttributes = new ExtensibleAttributes.Builder();
					}
					return _extensibleAttributes;
				}
			}

		}
	}
}