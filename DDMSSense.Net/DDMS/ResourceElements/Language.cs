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
	/// An immutable implementation of ddms:language.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>A non-empty qualifier value is required when the value attribute is set.</li>
	/// </ul>
	///  
	/// <para>DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A qualifier can be set with no value.</li>
	/// <li>A language can be set without a qualifier or value.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:qualifier</u>: a URI-based vocabulary (required if value is set)<br />
	/// <u>ddms:value</u>: the identification of the content language (optional)<br />
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 0.9.b
	/// </summary>
	public sealed class Language : AbstractQualifierValue {

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Language(Element element) : base(element) {
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="qualifier">	the value of the qualifier attribute </param>
		/// <param name="value">	the value of the value attribute </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Language(string qualifier, string value) : base(Language.GetName(DDMSVersion.GetCurrentVersion()), qualifier, value, true) {
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// <li>If a value is set, a qualifier must exist and be non-empty.</li>
		/// <li>Does not validate that the value is valid against the qualifier's vocabulary.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Language.GetName(DDMSVersion));
			if (!String.IsNullOrEmpty(Value)) {
				Util.RequireDDMSValue("qualifier attribute", Qualifier);
			}

			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>A qualifier has been set without an accompanying value attribute.</li>
		/// <li>Neither a qualifier nor a value was set on this language.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (!String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value)) {
				AddWarning("A qualifier has been set without an accompanying value attribute.");
			}
			if (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value)) {
				AddWarning("Neither a qualifier nor a value was set on this language.");
			}
			base.ValidateWarnings();
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
			return (base.Equals(obj) && (obj is Language));
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("language");
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 1.8.0 </seealso>
		public class Builder : AbstractQualifierValue.Builder {
			internal const long SerialVersionUID = -6370530125138751078L;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Language language) : base(language) {
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public override IDDMSComponent Commit()
            {
				return (Empty ? null : new Language(Qualifier, Value));
			}
		}
	}
}