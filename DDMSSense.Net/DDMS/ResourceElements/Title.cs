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
namespace DDMSSense.DDMS.ResourceElements {

	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:title.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>The title child text must not be empty.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Title : AbstractSimpleString {

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Title(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public Title(Element element) : base(element, true) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="title"> the value of the title child text </param>
		/// <param name="securityAttributes"> any security attributes (classification and ownerProducer are required) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Title(String title, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public Title(string title, SecurityAttributes securityAttributes) : base(Title.GetName(DDMSVersion.GetCurrentVersion()), title, securityAttributes, true) {
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>The title exists and is not empty.</li>
		/// <li>A classification is required.</li>
		/// <li>At least 1 ownerProducer exists and is non-empty.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Title.GetName(DDMSVersion));
			Util.RequireDDMSValue("title value", Value);
			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix);
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, Value));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix + "."));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Title)) {
				return (false);
			}
			return (true);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("title");
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		public class Builder : AbstractSimpleString.Builder {
			internal const long SerialVersionUID = -7348511606867959470L;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Title title) : base(title) {
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Title commit() throws DDMSSense.DDMS.InvalidDDMSException
			public override Title Commit() {
				return (Empty ? null : new Title(Value, SecurityAttributes.Commit()));
			}
		}
	}
}