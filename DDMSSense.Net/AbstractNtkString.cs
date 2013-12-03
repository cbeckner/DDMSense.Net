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
namespace DDMSSense {

	using Element = System.Xml.Linq.XElement;
	using IBuilder = DDMSSense.DDMS.IBuilder;
	using InvalidDDMSException = DDMSSense.DDMS.InvalidDDMSException;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;
    using DDMSSense.DDMS;

	/// <summary>
	/// Base class for NTK elements which consist of simple child text decorated with NTK attributes, and security attributes.
	/// 
	/// <para> Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
	/// before the component is used. </para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ntk:id</u>: A unique XML identifier (optional)<br />
	/// <u>ntk:IDReference</u>: A cross-reference to a unique identifier (optional)<br />
	/// <u>ntk:qualifier</u>: A user-defined property within an element for general purpose processing used with block 
	/// objects to provide supplemental information over and above that conveyed by the element name (optional)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and
	/// ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public abstract class AbstractNtkString : AbstractBaseComponent {

		private bool _tokenBased = false;
		private SecurityAttributes _securityAttributes = null;

		private const string ID_NAME = "id";
		private const string ID_REFERENCE_NAME = "IDReference";
		private const string QUALIFIER_NAME = "qualifier";

		/// <summary>
		/// Base constructor which works from a XOM element.
		/// </summary>
		/// <param name="tokenBased"> true if the child text is an NMTOKEN, false if it's just a string </param>
		/// <param name="element"> the XOM element </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected AbstractNtkString(boolean tokenBased, nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		protected internal AbstractNtkString(bool tokenBased, Element element) {
			try {
				SetXOMElement(element, false);
				_tokenBased = tokenBased;
				_securityAttributes = new SecurityAttributes(element);
				Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="tokenBased"> true if the child text is an NMTOKEN, false if it's just a string </param>
		/// <param name="name"> the name of the element without a prefix </param>
		/// <param name="value"> the value of the element's child text </param>
		/// <param name="id"> the NTK ID (optional) </param>
		/// <param name="idReference"> a reference to an NTK ID (optional) </param>
		/// <param name="qualifier"> an NTK qualifier (optional) </param>
		/// <param name="securityAttributes"> the security attributes </param>
		/// <param name="validateNow"> whether to validate immediately </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected AbstractNtkString(boolean tokenBased, String name, String value, String id, String idReference, String qualifier, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes, boolean validateNow) throws DDMSSense.DDMS.InvalidDDMSException
		protected internal AbstractNtkString(bool tokenBased, string name, string value, string id, string idReference, string qualifier, SecurityAttributes securityAttributes, bool validateNow) {
			try {
				string ntkPrefix = PropertyReader.GetPrefix("ntk");
				string ntkNamespace = DDMSVersion.GetCurrentVersion().NtkNamespace;
				Element element = Util.BuildElement(ntkPrefix, name, ntkNamespace, value);
				Util.AddAttribute(element, ntkPrefix, ID_NAME, ntkNamespace, id);
				Util.AddAttribute(element, ntkPrefix, ID_REFERENCE_NAME, ntkNamespace, idReference);
				Util.AddAttribute(element, ntkPrefix, QUALIFIER_NAME, ntkNamespace, qualifier);
				_tokenBased = tokenBased;
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
				SetXOMElement(element, validateNow);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>If this is an NMTOKEN-based string, and the child text is not empty, the child text is an NMTOKEN.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// <li>This component cannot be used until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			if (TokenBased) {
				Util.RequireValidNMToken(Value);
			}
			Util.RequireDDMSValue("security attributes", SecurityAttributes);
			SecurityAttributes.RequireClassification();

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj)) {
				return (false);
			}
			AbstractNtkString test = (AbstractNtkString) obj;
			return (Value.Equals(test.Value) && ID.Equals(test.ID) && IDReference.Equals(test.IDReference) && Qualifier.Equals(test.Qualifier));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Value.GetHashCode();
			result = 7 * result + ID.GetHashCode();
			result = 7 * result + IDReference.GetHashCode();
			result = 7 * result + Qualifier.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the child text of the description. The underlying XOM method which retrieves the child text returns
		/// an empty string if not found.
		/// </summary>
		public virtual string Value {
			get {
                return (Element.Value);
			}
			set {
                Element.Value = value;
			}
		}

		/// <summary>
		/// Accessor for the ID
		/// </summary>
		public virtual string ID {
			get {
				return (GetAttributeValue(ID_NAME, DDMSVersion.NtkNamespace));
			}
			set {
					_id = value;
			}
		}

		/// <summary>
		/// Accessor for the IDReference
		/// </summary>
		public virtual string IDReference {
			get {
				return (GetAttributeValue(ID_REFERENCE_NAME, DDMSVersion.NtkNamespace));
			}
			set {
					_idReference = value;
			}
		}

		/// <summary>
		/// Accessor for the qualifier
		/// </summary>
		public virtual string Qualifier {
			get {
				return (GetAttributeValue(QUALIFIER_NAME, DDMSVersion.NtkNamespace));
			}
			set {
					_qualifier = value;
			}
		}

		/// <summary>
		/// Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
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
		/// Accessor for whether this is an NMTOKEN-based string
		/// </summary>
		private bool TokenBased {
			get {
				return (_tokenBased);
			}
		}

		/// <summary>
		/// Abstract Builder for this DDMS component.
		/// 
		/// <para>Builders which are based upon this abstract class should implement the commit() method, returning the
		/// appropriate concrete object type.</para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		[Serializable]
		public abstract class Builder : IBuilder {
			public abstract IDDMSComponent Commit();
			internal const long SerialVersionUID = 7824644958681123708L;
			internal string _value;
			internal string _id;
			internal string _idReference;
			internal string _qualifier;
			internal SecurityAttributes.Builder _securityAttributes;


			/// <summary>
			/// Empty constructor
			/// </summary>
			protected internal Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			protected internal Builder(AbstractNtkString @string) {
				Value = @string.Value;
				ID = @string.ID;
				IDReference = @string.IDReference;
				Qualifier = @string.Qualifier;
				SecurityAttributes = new SecurityAttributes.Builder(@string.SecurityAttributes);
			}

			/// <summary>
			/// Helper method to determine if any values have been entered for this producer.
			/// </summary>
			/// <returns> true if all values are empty </returns>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Value) && String.IsNullOrEmpty(ID) && String.IsNullOrEmpty(IDReference) && String.IsNullOrEmpty(Qualifier) && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the child text.
			/// </summary>
			public virtual string Value {
				get {
					return _value;
				}
                set { _value = value; }
			}


			/// <summary>
			/// Builder accessor for the id
			/// </summary>
			public virtual string ID {
				get {
					return _id;
				}
                set { _id = value; }
			}


			/// <summary>
			/// Builder accessor for the idReference
			/// </summary>
			public virtual string IDReference {
				get {
					return _idReference;
				}
                set { _idReference = value; }
			}


			/// <summary>
			/// Builder accessor for the qualifier
			/// </summary>
			public virtual string Qualifier {
				get {
					return _qualifier;
				}
                set { _qualifier = value; }
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
                set { _securityAttributes = value; }
			}

		}
	}
}