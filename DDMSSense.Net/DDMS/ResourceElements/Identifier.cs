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
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:identifier.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>A non-empty qualifier value is required.</li>
	/// <li>A non-empty value attribute is required.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:qualifier</u>: a URI-based qualifier (required)<br />
	/// <u>ddms:value</u>: an unambiguous reference to the resource (required)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Identifier : AbstractQualifierValue {

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Identifier(Element element) : base(element) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="qualifier">	the value of the qualifier attribute </param>
		/// <param name="value">	the value of the value attribute </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Identifier(string qualifier, string value) : base(Identifier.GetName(DDMSVersion.GetCurrentVersion()), qualifier, value, true) {
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>The qualifier exists and is not empty.</li>
		/// <li>The value exists and is not empty.</li>
		/// <li>The qualifier is a valid URI.</li>
		/// <li>Does NOT validate that the value is valid against the qualifier's vocabulary.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Identifier.GetName(DDMSVersion));
			Util.RequireDDMSValue("qualifier attribute", Qualifier);
			Util.RequireDDMSValue("value attribute", Value);
			Util.RequireDDMSValidURI(Qualifier);
			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + QUALIFIER_NAME, Qualifier));
			text.Append(BuildOutput(isHTML, localPrefix + VALUE_NAME, Value));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			return (base.Equals(obj) && (obj is Identifier));
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("identifier");
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		public class Builder : AbstractQualifierValue.Builder {
			internal const long SerialVersionUID = -1105410940799401080L;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Identifier identifier) : base(identifier) {
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public override IDDMSComponent Commit()
            {
				return (Empty ? null : new Identifier(Qualifier, Value));
			}
		}
	}
}