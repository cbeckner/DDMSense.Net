using System.Collections.Generic;

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
	/// An immutable implementation of ddms:contributor.
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Nested Elements</th></tr><tr><td class="infoBody">
	/// <u>ddms:organization</u>: The organization who is in this role (0-1, optional), implemented as an <seealso cref="Organization"/><br />
	/// <u>ddms:person</u>: the person who is in this role (0-1, optional), implemented as a <seealso cref="Person"/><br />
	/// <u>ddms:service</u>: The web service who is in this role (0-1, optional), implemented as a <seealso cref="Service"/><br />
	/// <u>ddms:unknown</u>: The unknown entity who is in this role (0-1, optional), implemented as an <seealso cref="Unknown"/><br />
	/// Only one of the nested entities can appear in this element.
	/// </td></tr></table>
	/// 
	/// <table class="info"><tr class="infoHeader"><th>Attributes</th></tr><tr><td class="infoBody">
	/// <u><seealso cref="SecurityAttributes"/></u>:  The classification and ownerProducer attributes are optional.
	/// </td></tr></table>
	/// 
	/// @author Brian Uri!
	/// @since 2.0.0
	/// </summary>
	public class Contributor : AbstractProducerRole {

		/// <summary>
		/// Constructor for creating a component from a XOM Element
		/// </summary>
		/// <param name="element"> the XOM element representing this </param>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		public Contributor(Element element) : base(element) {
		}

		/// <summary>
		/// Constructor which builds from raw data.
		/// </summary>
		/// <param name="entity"> the actual entity fulfilling this role </param>
		/// <param name="pocTypes"> the ISM pocType for this producer (optional, starting in DDMS 4.0.1) </param>
		/// <param name="securityAttributes"> any security attributes (optional) </param>


		public Contributor(IRoleEntity entity, List<string> pocTypes, SecurityAttributes securityAttributes) : base(Contributor.GetName(DDMSVersion.GetCurrentVersion()), entity, pocTypes, securityAttributes) {
		}

		/// <summary>
		/// Validates the component.
		/// 
		/// <table class="info"><tr class="infoHeader"><th>Rules</th></tr><tr><td class="infoBody">
		/// <li>The qualified name of the element is correct.</li>
		/// </td></tr></table>
		/// </summary>
		/// <seealso cref= AbstractProducerRole#validate() </seealso>
		/// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>


		protected internal override void Validate() {
			Util.RequireDDMSQName(Element, Contributor.GetName(DDMSVersion));
			base.Validate();
		}

		/// <seealso cref= Object#equals(Object) </seealso>
		public override bool Equals(object obj) {
			return (base.Equals(obj) && (obj is Contributor));
		}

		/// <summary>
		/// Accessor for the element name of this component, based on the version of DDMS used
		/// </summary>
		/// <param name="version"> the DDMSVersion </param>
		/// <returns> an element name </returns>
		public static string GetName(DDMSVersion version) {
			Util.RequireValue("version", version);
			return ("contributor");
		}

		/// <summary>
		/// Builder for this DDMS component.
		/// </summary>
		/// <seealso cref= IBuilder
		/// @author Brian Uri!
		/// @since 2.0.0 </seealso>
		public class Builder : AbstractProducerRole.Builder {
			internal const long SerialVersionUID = 4565840434345629470L;

			/// <summary>
			/// Empty constructor
			/// </summary>
			public Builder() : base() {
			}

			/// <summary>
			/// Constructor which starts from an existing component.
			/// </summary>
			public Builder(Contributor producer) : base(producer) {
			}

			/// <seealso cref= IBuilder#commit() </seealso>


			public override Contributor Commit() {
				return (Empty ? null : new Contributor(CommitSelectedEntity(), PocTypes, SecurityAttributes.Commit()));
			}
		}
	}

}