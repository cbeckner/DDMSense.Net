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
namespace DDMSSense.DDMS.SecurityElements.Ntk {

	using Element = System.Xml.Linq.XElement;
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using PropertyReader = DDMSSense.Util.PropertyReader;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ntk:AccessProfileValue.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence allows the following legal, but nonsensical constructs:</para>
	/// <ul>
	/// <li>A profile value element can be used without any child text.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ntk:vocabulary</u>: A lexicon associated with the profile (required)<br />
	/// <u>ntk:id</u>: A unique XML identifier (optional)<br />
	/// <u>ntk:IDReference</u>: A cross-reference to a unique identifier (optional)<br />
	/// <u>ntk:qualifier</u>: A user-defined property within an element for general purpose processing used with block 
	/// objects to provide supplemental information over and above that conveyed by the element name (optional)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class ProfileValue : AbstractNtkString {

		private const string VOCABULARY_NAME = "vocabulary";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ProfileValue(nu.xom.Element element) throws DDMSSense.DDMS.InvalidDDMSException
		public ProfileValue(Element element) : base(false, element) {
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="value"> the value of the element's child text </param>
		/// <param name="vocabulary"> the lexicon (required) </param>
		/// <param name="id"> the NTK ID (optional) </param>
		/// <param name="idReference"> a reference to an NTK ID (optional) </param>
		/// <param name="qualifier"> an NTK qualifier (optional) </param>
		/// <param name="securityAttributes"> the security attributes </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ProfileValue(String value, String vocabulary, String id, String idReference, String qualifier, DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes securityAttributes) throws DDMSSense.DDMS.InvalidDDMSException
		public ProfileValue(string value, string vocabulary, string id, string idReference, string qualifier, SecurityAttributes securityAttributes) : base(false, ProfileValue.GetName(DDMSVersion.GetCurrentVersion()), value, id, idReference, qualifier, securityAttributes, false) {
			try {
				Util.AddAttribute(Element, PropertyReader.GetPrefix("ntk"), VOCABULARY_NAME, DDMSVersion.GetCurrentVersion().NtkNamespace, vocabulary);
				Validate();
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
		/// <li>The vocabulary attribute is set, and is a valid NMTOKEN.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void validate() throws DDMSSense.DDMS.InvalidDDMSException
		protected internal override void Validate() {
			Util.RequireQualifiedName(Element, Namespace, ProfileValue.GetName(DDMSVersion));
			Util.RequireValidNMToken(Vocabulary);
			base.Validate();
		}

		/// <summary>
		/// Validates any conditions that might result in a warning.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>An element was found with no child text.</li>
		/// </td></tr></table>
		/// </summary>
		protected internal override void ValidateWarnings() {
			if (String.IsNullOrEmpty(Value)) {
				AddWarning("A ntk:" + Name + " element was found with no value.");
			}
			base.ValidateWarnings();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, "profileValue", suffix);
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix, Value));
			text.Append(BuildOutput(isHTML, localPrefix + ".vocabulary", Vocabulary));
			text.Append(BuildOutput(isHTML, localPrefix + ".id", ID));
			text.Append(BuildOutput(isHTML, localPrefix + ".idReference", IDReference));
			text.Append(BuildOutput(isHTML, localPrefix + ".qualifier", Qualifier));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix + "."));
			return (text.ToString());
		}

		/// <summary>
		/// Builder for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("AccessProfileValue");
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is ProfileValue)) {
				return (false);
			}
			ProfileValue test = (ProfileValue) obj;
			return (Vocabulary.Equals(test.Vocabulary));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Vocabulary.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Builder for the vocabulary
		/// </summary>
		public string Vocabulary {
			get {
				return (GetAttributeValue(VOCABULARY_NAME, DDMSVersion.NtkNamespace));
			}
			set {                   
					_vocabulary = value;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		public class Builder : AbstractNtkString.Builder {
			internal const long SerialVersionUID = 7750664735441105296L;
			internal string _vocabulary;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(ProfileValue value) : base(value) {
				Vocabulary = value.Vocabulary;
			}

			/// <seealso cref= IBuilder#commit() </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ProfileValue commit() throws DDMSSense.DDMS.InvalidDDMSException
			public override ProfileValue Commit() {
				return (Empty ? null : new ProfileValue(Value, Vocabulary, ID, IDReference, Qualifier, SecurityAttributes.Commit()));
			}

			/// <summary>
			/// Builder accessor for the vocabulary
			/// </summary>
			public virtual string Vocabulary {
				get {
					return _vocabulary;
				}
                set { _vocabulary = value; }
			}

		}
	}
}