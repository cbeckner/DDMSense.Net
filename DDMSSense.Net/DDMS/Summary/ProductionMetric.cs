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
	using SecurityAttributes = DDMSSense.DDMS.SecurityElements.Ism.SecurityAttributes;
	using DDMSVersion = DDMSSense.Util.DDMSVersion;
	using Util = DDMSSense.Util.Util;

	/// <summary>
	/// An immutable implementation of ddms:productionMetric.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Strictness</th></tr><tr><td class="infoBody">
	/// <para>DDMSence is stricter than the specification in the following ways:</para>
	/// <ul>
	/// <li>The subject and coverage attributes must be non-empty.</li>
	/// </ul>
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u>ddms:subject</u>: A method of categorizing the subject of a document in a fashion understandable by DDNI-A. (required)<br />
	/// <u>ddms:coverage</u>: A method of categorizing the coverage of a document in a fashion understandable by DDNI-A (required)<br />
	/// <u><seealso cref="SecurityAttributes"/></u>: The classification and ownerProducer attributes are optional.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class ProductionMetric : AbstractBaseComponent {

		private SecurityAttributes _securityAttributes = null;

		private const string SUBJECT_NAME = "subject";
		private const string COVERAGE_NAME = "coverage";

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public ProductionMetric(Element element) {
			try {
				_securityAttributes = new SecurityAttributes(element);
				SetXOMElement(element, true);
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data.
		/// </summary>
		/// <param name="subject"> a method of categorizing the subject of a document in a fashion understandable by DDNI-A
		/// (required) </param>
		/// <param name="coverage"> a method of categorizing the coverage of a document in a fashion understandable by DDNI-A
		/// (required) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public ProductionMetric(string subject, string coverage, SecurityAttributes securityAttributes) {
			try {
				Element element = Util.BuildDDMSElement(ProductionMetric.GetName(DDMSVersion.GetCurrentVersion()), null);
				Util.AddDDMSAttribute(element, SUBJECT_NAME, subject);
				Util.AddDDMSAttribute(element, COVERAGE_NAME, coverage);
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
		/// <li>A subject exists and is not empty.</li>
		/// <li>A coverage exists and is not empty.</li>
		/// <li>This component cannot be used until DDMS 4.0.1 or later.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, ProductionMetric.GetName(DDMSVersion));
			Util.RequireDDMSValue("subject attribute", Subject);
			Util.RequireDDMSValue("coverage attribute", Coverage);

			// Should be reviewed as additional versions of DDMS are supported.
			RequireVersion("4.0.1");

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
			StringBuilder text = new StringBuilder();
			text.Append(BuildOutput(isHTML, localPrefix + SUBJECT_NAME, Subject));
			text.Append(BuildOutput(isHTML, localPrefix + COVERAGE_NAME, Coverage));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is ProductionMetric)) {
				return (false);
			}
			ProductionMetric test = (ProductionMetric) obj;
			return (Subject.Equals(test.Subject) && Coverage.Equals(test.Coverage));
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int HashCode() {
			int result = base.HashCode();
			result = 7 * result + Subject.GetHashCode();
			result = 7 * result + Coverage.GetHashCode();
			return (result);
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("productionMetric");
		}

		/// <summary>
		/// Accessor for the subject attribute.
		/// </summary>
		public string Subject {
			get {
				return (GetAttributeValue(SUBJECT_NAME));
			}
			set {
					_subject = value;
			}
		}

		/// <summary>
		/// Accessor for the coverage attribute.
		/// </summary>
		public string Coverage {
			get {
				return (GetAttributeValue(COVERAGE_NAME));
			}
			set {
					_coverage = value;
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
		/// @since 2.0.0 </seealso>
		[Serializable]
		public class Builder : IBuilder {
			internal const long SerialVersionUID = -9012648230977148516L;
			internal string _subject;
			internal string _coverage;
			internal SecurityAttributes.Builder _securityAttributes;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(ProductionMetric metric) {
				Subject = metric.Subject;
				Coverage = metric.Coverage;
				SecurityAttributes = new SecurityAttributes.Builder(metric.SecurityAttributes);
			}

			/// <seealso cref= IBuilder#commit() </seealso>


			public virtual ProductionMetric Commit() {
				return (Empty ? null : new ProductionMetric(Subject, Coverage, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public virtual bool Empty {
				get {
					return (String.IsNullOrEmpty(Subject) && String.IsNullOrEmpty(Coverage) && SecurityAttributes.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the subject attribute
			/// </summary>
			public virtual string Subject {
				get {
					return _subject;
				}
			}


			/// <summary>
			/// Builder accessor for the coverage attribute
			/// </summary>
			public virtual string Coverage {
				get {
					return _coverage;
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

		}
	}
}