#region usings

using System;
using System.Xml.Linq;
using DDMSSense.DDMS;
using DDMSSense.DDMS.SecurityElements.Ism;
using DDMSSense.Util;

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

namespace DDMSSense
{
    #region usings

    using Element = XElement;

    #endregion

    /// <summary>
    ///     Base class for NTK elements which consist of simple child text decorated with NTK attributes, and security
    ///     attributes.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before the component is used.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ntk:id</u>: A unique XML identifier (optional)<br />
    ///                 <u>ntk:IDReference</u>: A cross-reference to a unique identifier (optional)<br />
    ///                 <u>ntk:qualifier</u>: A user-defined property within an element for general purpose processing used
    ///                 with block
    ///                 objects to provide supplemental information over and above that conveyed by the element name (optional)
    ///                 <br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and
    ///                 ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     @author Brian Uri!
    ///     @since 2.0.0
    /// </summary>
    public abstract class AbstractNtkString : AbstractBaseComponent
    {
        private const string ID_NAME = "id";
        private const string ID_REFERENCE_NAME = "IDReference";
        private const string QUALIFIER_NAME = "qualifier";
        private readonly bool _tokenBased;
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Base constructor which works from a XOM element.
        /// </summary>
        /// <param name="tokenBased"> true if the child text is an NMTOKEN, false if it's just a string </param>
        /// <param name="element"> the XOM element </param>
        protected internal AbstractNtkString(bool tokenBased, Element element)
        {
            try
            {
                SetXOMElement(element, false);
                _tokenBased = tokenBased;
                _securityAttributes = new SecurityAttributes(element);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="tokenBased"> true if the child text is an NMTOKEN, false if it's just a string </param>
        /// <param name="name"> the name of the element without a prefix </param>
        /// <param name="value"> the value of the element's child text </param>
        /// <param name="id"> the NTK ID (optional) </param>
        /// <param name="idReference"> a reference to an NTK ID (optional) </param>
        /// <param name="qualifier"> an NTK qualifier (optional) </param>
        /// <param name="securityAttributes"> the security attributes </param>
        /// <param name="validateNow"> whether to validate immediately </param>
        protected internal AbstractNtkString(bool tokenBased, string name, string value, string id, string idReference,
            string qualifier, SecurityAttributes securityAttributes, bool validateNow)
        {
            try
            {
                string ntkPrefix = PropertyReader.GetPrefix("ntk");
                string ntkNamespace = DDMSVersion.GetCurrentVersion().NtkNamespace;
                Element element = Util.Util.BuildElement(ntkPrefix, name, ntkNamespace, value);
                Util.Util.AddAttribute(element, ntkPrefix, ID_NAME, ntkNamespace, id);
                Util.Util.AddAttribute(element, ntkPrefix, ID_REFERENCE_NAME, ntkNamespace, idReference);
                Util.Util.AddAttribute(element, ntkPrefix, QUALIFIER_NAME, ntkNamespace, qualifier);
                _tokenBased = tokenBased;
                _securityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                _securityAttributes.AddTo(element);
                SetXOMElement(element, validateNow);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the child text of the description. The underlying XOM method which retrieves the child text returns
        ///     an empty string if not found.
        /// </summary>
        public virtual string Value
        {
            get { return (Element.Value); }
            set { Element.Value = value; }
        }

        /// <summary>
        ///     Accessor for the ID
        /// </summary>
        public virtual string ID
        {
            get { return (GetAttributeValue(ID_NAME, DDMSVersion.NtkNamespace)); }
        }

        /// <summary>
        ///     Accessor for the IDReference
        /// </summary>
        public virtual string IDReference
        {
            get { return (GetAttributeValue(ID_REFERENCE_NAME, DDMSVersion.NtkNamespace)); }
        }

        /// <summary>
        ///     Accessor for the qualifier
        /// </summary>
        public virtual string Qualifier
        {
            get { return (GetAttributeValue(QUALIFIER_NAME, DDMSVersion.NtkNamespace)); }
        }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        ///     Accessor for whether this is an NMTOKEN-based string
        /// </summary>
        private bool TokenBased
        {
            get { return (_tokenBased); }
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>If this is an NMTOKEN-based string, and the child text is not empty, the child text is an NMTOKEN.</li>
        ///                 <li>A classification is required.</li>
        ///                 <li>At least 1 ownerProducer exists and is non-empty.</li>
        ///                 <li>This component cannot be used until DDMS 4.0.1 or later.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            if (TokenBased)
            {
                Util.Util.RequireValidNMToken(Value);
            }
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();

            // Should be reviewed as additional versions of DDMS are supported.
            RequireVersion("4.0.1");

            base.Validate();
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return (false);
            }
            var test = (AbstractNtkString) obj;
            return (Value.Equals(test.Value) && ID.Equals(test.ID) && IDReference.Equals(test.IDReference) &&
                    Qualifier.Equals(test.Qualifier));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Value.GetHashCode();
            result = 7*result + ID.GetHashCode();
            result = 7*result + IDReference.GetHashCode();
            result = 7*result + Qualifier.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Abstract Builder for this DDMS component.
        ///     <para>
        ///         Builders which are based upon this abstract class should implement the commit() method, returning the
        ///         appropriate concrete object type.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 2.0.0"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            internal const long SerialVersionUID = 7824644958681123708L;
            internal string _id;
            internal string _idReference;
            internal string _qualifier;
            internal SecurityAttributes.Builder _securityAttributes;
            internal string _value;


            /// <summary>
            ///     Empty constructor
            /// </summary>
            protected internal Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractNtkString @string)
            {
                Value = @string.Value;
                ID = @string.ID;
                IDReference = @string.IDReference;
                Qualifier = @string.Qualifier;
                SecurityAttributes = new SecurityAttributes.Builder(@string.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the child text.
            /// </summary>
            public virtual string Value
            {
                get { return _value; }
                set { _value = value; }
            }


            /// <summary>
            ///     Builder accessor for the id
            /// </summary>
            public virtual string ID
            {
                get { return _id; }
                set { _id = value; }
            }


            /// <summary>
            ///     Builder accessor for the idReference
            /// </summary>
            public virtual string IDReference
            {
                get { return _idReference; }
                set { _idReference = value; }
            }


            /// <summary>
            ///     Builder accessor for the qualifier
            /// </summary>
            public virtual string Qualifier
            {
                get { return _qualifier; }
                set { _qualifier = value; }
            }


            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes
            {
                get
                {
                    if (_securityAttributes == null)
                    {
                        _securityAttributes = new SecurityAttributes.Builder();
                    }
                    return _securityAttributes;
                }
                set { _securityAttributes = value; }
            }

            public abstract IDDMSComponent Commit();

            /// <summary>
            ///     Helper method to determine if any values have been entered for this producer.
            /// </summary>
            /// <returns> true if all values are empty </returns>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(Value) && String.IsNullOrEmpty(ID) && String.IsNullOrEmpty(IDReference) &&
                            String.IsNullOrEmpty(Qualifier) && SecurityAttributes.Empty);
                }
            }
        }
    }
}