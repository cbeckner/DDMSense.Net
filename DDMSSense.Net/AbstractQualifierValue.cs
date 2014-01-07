#region usings

using System;
using System.Xml.Linq;
using DDMSense.DDMS;

#endregion

namespace DDMSense
{
    /// <summary>
    ///     Base class for DDMS elements which have a qualifier/value attribute, such as ddms:Identifier and ddms:source.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable, and the underlying XOM element MUST be set
    ///         before the component is used.
    ///     </para>
    /// </summary>
    public abstract class AbstractQualifierValue : AbstractBaseComponent
    {
        protected internal const string QUALIFIER_NAME = "qualifier";
        protected internal const string VALUE_NAME = "value";

        /// <summary>
        ///     This implicit superconstructor does nothing.
        /// </summary>
        protected internal AbstractQualifierValue()
        {
        }

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element representing this component </param>
        protected internal AbstractQualifierValue(XElement element)
            : base(element)
        {
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="name"> the name of the element without a prefix </param>
        /// <param name="qualifier"> the value of the qualifier attribute </param>
        /// <param name="value"> the value of the value attribute </param>
        /// <param name="validateNow">
        ///     true to validate the component immediately. Because Source entities have additional fields
        ///     they should not be validated in the superconstructor.
        /// </param>
        protected internal AbstractQualifierValue(string name, string qualifier, string value, bool validateNow)
        {
            try
            {
                XElement element = Util.Util.BuildDDMSElement(name, null);
                Util.Util.AddDDMSAttribute(element, QUALIFIER_NAME, qualifier);
                Util.Util.AddDDMSAttribute(element, VALUE_NAME, value);
                SetElement(element, validateNow);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Accessor for the value of the qualifier attribute
        /// </summary>
        public virtual string Qualifier
        {
            get { return (GetAttributeValue(QUALIFIER_NAME)); }
        }

        /// <summary>
        ///     Accessor for the value of the value attribute
        /// </summary>
        public virtual string Value
        {
            get { return (GetAttributeValue(VALUE_NAME)); }
            set { Element.Value = value; }
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is AbstractQualifierValue))
                return (false);

            var test = (AbstractQualifierValue)obj;
            return (Qualifier.Equals(test.Qualifier) && Value.Equals(test.Value));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Qualifier.GetHashCode();
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
            internal const long SerialVersionUID = 5630463057657652800L;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            protected internal Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            protected internal Builder(AbstractQualifierValue qualifierValue)
            {
                Qualifier = qualifierValue.Qualifier;
                Value = qualifierValue.Value;
            }

            /// <summary>
            ///     Builder accessor for the qualifier attribute.
            /// </summary>
            public virtual string Qualifier { get; set; }


            /// <summary>
            ///     Builder accessor for the value attribute.
            /// </summary>
            public virtual string Value { get; set; }

            public abstract IDDMSComponent Commit();


            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(Qualifier) && String.IsNullOrEmpty(Value)); }
            }
        }
    }
}