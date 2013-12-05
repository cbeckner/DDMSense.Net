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
	/// Base class for DDMS elements which consist of simple child text, possibly decorated with attributes, such as
	/// ddms:description, ddms:title, and ddms:subtitle.
	/// 
	/// <para> Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
	/// before the component is used. </para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and
	/// ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public abstract class AbstractSimpleString : AbstractBaseComponent {

		private SecurityAttributes _securityAttributes = null;

		/// <summary>
		/// Base constructor which works from a XOM element.
		/// </summary>
		/// <param name="element"> the XOM element </param>
		/// <param name="validateNow"> true if the component should be validated here </param>


		protected internal AbstractSimpleString(Element element, bool validateNow) {
			try {
				_securityAttributes = new SecurityAttributes(element);
				SetXOMElement(element, validateNow);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="name"> the name of the element without a prefix </param>
		/// <param name="value"> the value of the element's child text </param>
		/// <param name="attributes"> the security attributes </param>
		/// <param name="validateNow"> true if the component should be validated here </param>


		protected internal AbstractSimpleString(string name, string value, SecurityAttributes attributes, bool validateNow) : this(PropertyReader.GetPrefix("ddms"), DDMSVersion.GetCurrentVersion().Namespace, name, value, attributes, validateNow) {
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="prefix"> the XML prefix </param>
		/// <param name="namespace"> the namespace of the element </param>
		/// <param name="name"> the name of the element without a prefix </param>
		/// <param name="value"> the value of the element's child text </param>
		/// <param name="attributes"> the security attributes </param>
		/// <param name="validateNow"> true if the component should be validated here </param>


		protected internal AbstractSimpleString(string prefix, string @namespace, string name, string value, SecurityAttributes attributes, bool validateNow) {
			try {
				Element element = Util.BuildElement(prefix, name, @namespace, value);
				_securityAttributes = SecurityAttributes.GetNonNullInstance(attributes);
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
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSValue("security attributes", SecurityAttributes);
			SecurityAttributes.RequireClassification();
			base.Validate();
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is AbstractSimpleString)) {
				return (false);
			}
			AbstractSimpleString test = (AbstractSimpleString) obj;
			return (Value.Equals(test.Value));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode() {
			int result = base.GetHashCode();
			result = 7 * result + Value.GetHashCode();
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
		/// Abstract Builder for this DDMS component.
		/// 
		/// <para>Builders which are based upon this abstract class should implement the commit() method, returning the
		/// appropriate concrete object type.</para>
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public abstract class Builder : IBuilder {
			public abstract IDDMSComponent Commit();
			internal const long SerialVersionUID = 7824644958681123708L;
			internal string _value;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			protected internal Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			protected internal Builder(AbstractSimpleString simpleString) {
				Value = simpleString.Value;
				SecurityAttributes = new SecurityAttributes.Builder(simpleString.SecurityAttributes);
			}

			/// <summary>
			/// Helper method to determine if any values have been entered for this producer.
			/// </summary>
			/// <returns> true if all values are empty </returns>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Value) && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the child text.
			/// </summary>
			public virtual string Value {
				get {
					return _value;
				}
                set
                {
                    _value = value;
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
                set { _securityAttributes = value; }
			}

		}
	}
}