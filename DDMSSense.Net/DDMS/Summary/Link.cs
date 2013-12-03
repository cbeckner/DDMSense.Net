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
    using RevisionRecall = DDMSSense.DDMS.ResourceElements.RevisionRecall;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using XLinkAttributes = DDMSSense.DDMS.Summary.Xlink.XLinkAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:link.
	/// 
	/// <para>This element is not a global component, but is being implemented because it has attributes.</para>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>The href value must not be empty.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="XLinkAttributes"/></u>: The xlink:type attribute is required and must have a fixed
	/// value of "locator". The xlink:href attribute is also required.<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: Only allowed when used in the context of a <seealso cref="RevisionRecall"/> 
	/// (starting in DDMS 4.0.1). The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Link : AbstractBaseComponent {

		private XLinkAttributes _xlinkAttributes = null;
		private SecurityAttributes _securityAttributes = null;

		private const string FIXED_TYPE = "locator";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Link(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public Link(Element element) {
			try {
				_xlinkAttributes = new XLinkAttributes(element);
				_securityAttributes = new SecurityAttributes(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="xlinkAttributes"> the xlink attributes </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Link(DDMSSense.DDMS.Summary.Xlink.XLinkAttributes xlinkAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public Link(XLinkAttributes xlinkAttributes) : this(xlinkAttributes, null) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="xlinkAttributes"> the xlink attributes </param>
		/// <param name="securityAttributes"> attributes, which are only allowed on links within a ddms:revisionRecall </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Link(DDMSSense.DDMS.Summary.Xlink.XLinkAttributes xlinkAttributes, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public Link(XLinkAttributes xlinkAttributes, SecurityAttributes securityAttributes) {
			try {
				Element element = Util.BuildDDMSElement(Link.GetName(DDMSVersion.GetCurrentVersion()), null);
				_xlinkAttributes = XLinkAttributes.GetNonNullInstance(xlinkAttributes);
				_xlinkAttributes.AddTo(element);
				_securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
				_securityAttributes.AddTo(element);
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
		/// <li>The xlink:type is set and has a value of "locator".</li>
		/// <li>The xlink:href is set and non-empty.</li>
		/// <li>Does not validate the security attributes. It is the parent class' responsibility
		/// to do that.
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Link.GetName(DDMSVersion));
			Util.RequireDDMSValue("type attribute", XLinkAttributes.Type);
			Util.RequireDDMSValue("href attribute", XLinkAttributes.Href);

			if (!XLinkAttributes.Type.Equals(FIXED_TYPE)) {
				throw new InvalidDDMSException("The type attribute must have a fixed value of \"" + FIXED_TYPE + "\".");
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>Include any warnings from the XLink attributes.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (XLinkAttributes != null) {
				AddWarnings(XLinkAttributes.ValidationWarnings, true);
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(XLinkAttributes.GetOutput(isHTML, localPrefix));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Link)) {
				return (false);
			}
			Link test = (Link) obj;
			return (XLinkAttributes.Equals(test.XLinkAttributes));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + XLinkAttributes.HashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("link");
		}

		/// <summary>
		/// Accessor for the XLink Attributes. Will always be non-null, even if it has no values set.
		/// </summary>
		public XLinkAttributes XLinkAttributes {
			get {
				return (_xlinkAttributes);
			}
			set {
					_xlinkAttributes = value;
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
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = 4325950371570699184L;
			internal XLinkAttributes.Builder _xlinkAttributes;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Link link) {
				XLinkAttributes = new XLinkAttributes.Builder(link.XLinkAttributes);
				SecurityAttributes = new SecurityAttributes.Builder(link.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Link commit() throws DDMSSense.DDMS.InvalidDDMSException
			public virtual Link Commit() {
				return (Empty ? null : new Link(XLinkAttributes.Commit(), SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (XLinkAttributes.Empty && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the XLink Attributes
			/// </summary>
			public virtual XLinkAttributes.Builder XLinkAttributes {
				get {
					if (_xlinkAttributes == null) {
						_xlinkAttributes = new XLinkAttributes.Builder();
					}
					return _xlinkAttributes;
				}
                set { _xlinkAttributes = value; }
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