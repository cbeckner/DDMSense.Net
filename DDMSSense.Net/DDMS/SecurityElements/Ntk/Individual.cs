using System.Collections.Generic;
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
	
	using Util = DDMSSense.Util.Util;
    using System.Xml;
    using System.Xml.Linq;

	/// <summary>
	/// An immutable implementation of ntk:AccessIndividual.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ntk:AccessSystemName</u>: The system described by this access record (exactly 1 required), implemented as a 
	/// <seealso cref="SystemName"/><br />
	/// <u>ntk:AccessIndividualValue</u>: The value used to describe the individual (1-to-many required), implemented as an 
	/// <seealso cref="IndividualValue"/><br />
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are required.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public sealed class Individual : AbstractAccessEntity {

		private List<IndividualValue> _individualValues = null;

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Individual(Element element) : base(element) {
			try {
				IEnumerable<Element> values = element.Elements(XName.Get(IndividualValue.GetName(DDMSVersion), Namespace));
				_individualValues = new List<IndividualValue>();
				foreach(var value in values)
					_individualValues.Add(new IndividualValue(value));
				
                Validate();
			} catch (InvalidDDMSException e) {
				e.Locator = QualifiedName;
				throw (e);
			}
		}

		/// <summary>
		/// Constructor for creating a component from raw data
		/// </summary>
		/// <param name="systemName"> the system name (required) </param>
		/// <param name="individualValues"> the list of values (at least 1 required) </param>
		/// <param name="securityAttributes"> security attributes (required) </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Individual(SystemName systemName, List<IndividualValue> individualValues, SecurityAttributes securityAttributes) : base(Individual.GetName(DDMSVersion.GetCurrentVersion()), systemName, securityAttributes) {
			try {
				if (individualValues == null) {
					individualValues = new List<IndividualValue>();
				}
				foreach (IndividualValue value in individualValues) {
                    Element.Add(value.XOMElementCopy);
				}
				_individualValues = individualValues;
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
		/// <li>At least 1 individual value is required.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractBaseComponent#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireQualifiedName(Element, Namespace, Individual.GetName(DDMSVersion));
			if (IndividualValues.Count == 0) {
				throw new InvalidDDMSException("At least one individual value is required.");
			}

			base.Validate();
		}

		/// <seealso cref= AbstractBaseComponent#getOutput(boolean, String, String) </seealso>
		public override string GetOutput(bool isHTML, string prefix, string suffix) {
			string localPrefix = BuildPrefix(prefix, "individual", suffix) + ".";
			StringBuilder text = new StringBuilder();
			if (SystemName != null) {
				text.Append(SystemName.GetOutput(isHTML, localPrefix, ""));
			}
			text.Append(BuildOutput(isHTML, localPrefix, IndividualValues));
			text.Append(SecurityAttributes.GetOutput(isHTML, localPrefix));
			return (text.ToString());
		}

		/// <seealso cref= AbstractBaseComponent#getNestedComponents() </seealso>
		protected internal override List<IDDMSComponent> NestedComponents {
			get {
				List<IDDMSComponent> list = base.NestedComponents;
				list.AddRange(IndividualValues);
				return (list);
			}
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			if (!base.Equals(obj) || !(obj is Individual)) {
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
			return ("AccessIndividual");
		}

		/// <summary>
		/// Accessor for the list of individual values (1-many)
		/// </summary>
		public List<IndividualValue> IndividualValues {
			get {
				return _individualValues;
			}
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		public class Builder : AbstractAccessEntity.Builder {
			internal const long SerialVersionUID = 7851044806424206976L;
			internal List<IndividualValue.Builder> _individualValues;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Individual individual) : base(individual) {
				foreach (IndividualValue value in individual.IndividualValues) {
					IndividualValues.Add(new IndividualValue.Builder(value));
				}
			}

			/// <seealso cref= IBuilder#commit() </seealso>


            public override IDDMSComponent Commit()
            {
				if (Empty) {
					return (null);
				}
				List<IndividualValue> values = new List<IndividualValue>();
				foreach (IBuilder builder in IndividualValues) {
					IndividualValue component = (IndividualValue) builder.Commit();
					if (component != null) {
						values.Add(component);
					}
				}
				return (new Individual(SystemName.Commit(), values, SecurityAttributes.Commit()));
			}

			/// <seealso cref= IBuilder#isEmpty() </seealso>
			public override bool Empty {
				get {
					bool hasValueInList = false;
					foreach (IBuilder builder in IndividualValues) {
						hasValueInList = hasValueInList || !builder.Empty;
					}
					return (!hasValueInList && base.Empty);
				}
			}

			/// <summary>
			/// Builder accessor for the values
			/// </summary>
			public virtual List<IndividualValue.Builder> IndividualValues {
				get {
					if (_individualValues == null) {
                        _individualValues = new List<IndividualValue.Builder>();
					}
					return _individualValues;
				}
			}
		}
	}
}