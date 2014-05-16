#region usings

using System;
using System.Xml.Linq;
using DDMSense.DDMS;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

namespace DDMSense
{
    /// <summary>
    ///     Base class for DDMS elements which consist of simple child text, possibly decorated with attributes, such as
    ///     ddms:description, ddms:title, and ddms:subtitle.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before the component is used.
    ///     </para>
    /// </summary>
    public abstract class AbstractSimpleString : AbstractBaseComponent
    {
        private SecurityAttributes _securityAttributes;

        /// <summary>
        ///     Base constructor which works from a XOM element.
        /// </summary>
        /// <param name="element"> the XOM element </param>
        /// <param name="validateNow"> true if the component should be validated here </param>
        protected internal AbstractSimpleString(XElement element, bool validateNow)
        {
            try
            {
                _securityAttributes = new SecurityAttributes(element);
                SetElement(element, validateNow);
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
        /// <param name="name"> the name of the element without a prefix </param>
        /// <param name="value"> the value of the element's child text </param>
        /// <param name="attributes"> the security attributes </param>
        /// <param name="validateNow"> true if the component should be validated here </param>
        protected internal AbstractSimpleString(string name, string value, SecurityAttributes attributes, bool validateNow)
            : this(PropertyReader.GetPrefix("ddms"), DDMSVersion.CurrentVersion.Namespace, name, value, attributes, validateNow)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="prefix"> the XML prefix </param>
        /// <param name="namespace"> the namespace of the element </param>
        /// <param name="name"> the name of the element without a prefix </param>
        /// <param name="value"> the value of the element's child text </param>
        /// <param name="attributes"> the security attributes </param>
        /// <param name="validateNow"> true if the component should be validated here </param>
        protected internal AbstractSimpleString(string prefix, string @namespace, string name, string value, SecurityAttributes attributes, bool validateNow)
        {
            try
            {
                XElement element = Util.Util.BuildElement(prefix, name, @namespace, value);
                _securityAttributes = SecurityAttributes.GetNonNullInstance(attributes);
                _securityAttributes.AddTo(element);
                SetElement(element, validateNow);
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
        ///     Accessor for the Security Attributes. Will always be non-null even if the attributes are not set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes
        {
            get { return (_securityAttributes); }
            set { _securityAttributes = value; }
        }

        /// <summary>
        ///     Validates the component.
        /// </summary>
        /// <remarks>
        /// A classification is required.
        /// At least 1 ownerProducer exists and is non-empty.
        /// </remarks>

        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSValue("security attributes", SecurityAttributes);
            SecurityAttributes.RequireClassification();
            base.Validate();
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractSimpleString))
                return (false);
            
            var test = (AbstractSimpleString)obj;
            return (Value.Equals(test.Value));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Value.GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Abstract Builder for this DDMS component.
        ///     <para>
        ///         Builders which are based upon this abstract class should implement the commit() method, returning the
        ///         appropriate concrete object type.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public abstract class Builder : IBuilder
        {
            
            internal SecurityAttributes.Builder _securityAttributes;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            protected internal Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractSimpleString simpleString)
            {
                Value = simpleString.Value;
                SecurityAttributes = new SecurityAttributes.Builder(simpleString.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the child text.
            /// </summary>
            public virtual string Value { get; set; }


            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes
            {
                get
                {
                    if (_securityAttributes == null)
                        _securityAttributes = new SecurityAttributes.Builder();
                    
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
                get { return (String.IsNullOrEmpty(Value) && SecurityAttributes.Empty); }
            }
        }
    }
}