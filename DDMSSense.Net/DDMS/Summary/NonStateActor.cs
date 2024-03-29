#region usings

using System;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

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

namespace DDMSense.DDMS.Summary
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     An immutable implementation of ddms:nonStateActor.
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>
    ///                     DDMSence allows the
    ///                     following legal, but nonsensical constructs:
    ///                 </para>
    ///                 <ul>
    ///                     <li>
    ///                         A nonStateActor element can be used without any child
    ///                         text.
    ///                     </li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:order</u>:
    ///                 specifies a user-defined order of an element within the given document (optional)<br />
    ///                 <u>ddms:qualifier</u>: A
    ///                 URI-based qualifier (optional, starting in DDMS 4.1)<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 : The classification and
    ///                 ownerProducer attributes are optional.
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class NonStateActor : AbstractSimpleString
    {
        private const string ORDER_NAME = "order";
        private const string QUALIFIER_NAME = "qualifier";

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NonStateActor(Element element) : base(element, true)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data.
        /// </summary>
        /// @deprecated A new constructor was added for DDMS 4.1 to support ddms:qualifier. This constructor is preserved for 
        /// backwards compatibility, but may disappear in the next major release.
        /// <param name="value"> the value of the description child text </param>
        /// <param name="order"> the order of this actor </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NonStateActor(string value, int? order, SecurityAttributes securityAttributes)
            : this(value, order, null, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="value"> the value of the description child text </param>
        /// <param name="order"> the order of this actor </param>
        /// <param name="qualifier"> the qualifier (optional) </param>
        /// <param name="securityAttributes"> any security attributes (classification and ownerProducer are optional) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public NonStateActor(string value, int? order, string qualifier, SecurityAttributes securityAttributes)
            : base(GetName(DDMSVersion.CurrentVersion), value, securityAttributes, false)
        {
            try
            {
                if (order != null)
                {
                    Util.Util.AddDDMSAttribute(Element, ORDER_NAME, order.ToString());
                }
                if (!String.IsNullOrEmpty(qualifier))
                {
                    Util.Util.AddDDMSAttribute(Element, QUALIFIER_NAME, qualifier);
                }
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the order attribute.
        /// </summary>
        public int? Order
        {
            get
            {
                string order = GetAttributeValue(ORDER_NAME);
                return (String.IsNullOrEmpty(order) ? null : new int?(Convert.ToInt32(order)));
            }
        }

        /// <summary>
        ///     Accessor for the qualifier attribute.
        /// </summary>
        public string Qualifier
        {
            get { return (GetAttributeValue(QUALIFIER_NAME)); }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>If a qualifier exists, it is a valid URI.</li>
        ///                 <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        ///                 <li>Does not validate the value of the order attribute (this is done at the Resource level).</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            // Do not call super.validate(), because securityAttributes are optional.
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            if (!String.IsNullOrEmpty(Qualifier))
            {
                Util.Util.RequireDDMSValidUri(Qualifier);
            }
            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            ValidateWarnings();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>A ddms:nonStateActor element was found with no value.</li>
        ///                 <li>A qualifier attribute may cause issues for DDMS 4.0 records.</li>
        ///                 <li>Include any validation warnings from the security attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (String.IsNullOrEmpty(Value))
            {
                AddWarning("A ddms:" + Name + " element was found with no value.");
            }
            if (!String.IsNullOrEmpty(Qualifier))
            {
                AddDdms40Warning("ddms:qualifier attribute");
            }
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + "value", Value));
            text.Append(BuildOutput(isHtml, localPrefix + ORDER_NAME, Convert.ToString(Order)));
            text.Append(BuildOutput(isHtml, localPrefix + QUALIFIER_NAME, Qualifier));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is NonStateActor))
            {
                return (false);
            }
            var test = (NonStateActor) obj;
            return (Order.Equals(test.Order) && Qualifier.Equals(test.Qualifier));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Order.GetHashCode();
            result = 7*result + Qualifier.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Accessor for the element name of this component, based on the version of DDMS used
        /// </summary>
        /// <param name="version"> the DDMSVersion </param>
        /// <returns> an element name </returns>
        public static string GetName(DDMSVersion version)
        {
            Util.Util.RequireValue("version", version);
            return ("nonStateActor");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        public class Builder : AbstractSimpleString.Builder
        {
            
            internal int? _order;
            internal string _qualifier;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(NonStateActor actor) : base(actor)
            {
                Order = actor.Order;
                Qualifier = actor.Qualifier;
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public override bool Empty
            {
                get { return (base.Empty && Order == null && String.IsNullOrEmpty(Qualifier)); }
            }

            /// <summary>
            ///     Builder accessor for the order
            /// </summary>
            public virtual int? Order
            {
                get { return _order; }
                set { _order = value; }
            }

            /// <summary>
            ///     Builder accessor for the qualifier attribute
            /// </summary>
            public virtual string Qualifier
            {
                get { return _qualifier; }
                set { _qualifier = value; }
            }

            /// <see cref="IBuilder#commit()"></see>
            public override IDDMSComponent Commit()
            {
                return (Empty ? null : new NonStateActor(Value, Order, Qualifier, SecurityAttributes.Commit()));
            }
        }
    }
}