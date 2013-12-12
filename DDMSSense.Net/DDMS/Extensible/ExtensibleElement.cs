#region usings

using System;
using System.Xml;
using System.Xml.Linq;
using DDMSSense.Util;

#endregion

namespace DDMSSense.DDMS.Extensible
{
    /// <summary>
    ///     An immutable implementation of an element which might fulfill the xs:any space in the Extensible Layer.
    ///     <para>
    ///         Starting in DDMS 3.0, zero to many of these elements may appear in a ddms:resource and can live in any other
    ///         namespace besides the DDMS namespace. In DDMS 2.0, only one of these is allowed.
    ///     </para>
    ///     <para>
    ///         No validation or processing of any kind is performed by DDMSence on extensible attributes, other than the base
    ///         validation used when loading attributes from an XML file. This class merely exposes a
    ///         <code>getXOMElementCopy()</code>
    ///         method which returns a XOM Element that can be manipulated in business-specific ways.
    ///     </para>
    ///     <para>XOM elements can be created as follows:</para>
    ///     <ul>
    ///         <code>
    /// Element element = new Element("ddmsence:extension", "http://ddmsence.urizone.net/");<br />
    /// element.Add("This will be the child text.");
    /// </code>
    ///     </ul>
    ///     <para>
    ///         Because it is impossible to cover all of the HTML/Text output cases for ExtensibleElements, DDMSence
    ///         will simply print out the existence of extensible elements:
    ///     </para>
    ///     <ul>
    ///         <code>
    /// Extensible Layer: true<br />
    /// &lt;meta name="extensible.layer" content="true" /&gt;<br />
    /// </code>
    ///     </ul></p>
    ///     <para>
    ///         Details about the XOM Element class can be found at:
    ///         <i>http://www.xom.nu/apidocs/index.html?nu/xom/Element.html</i>
    ///     </para>
    
    ///     @since 1.1.0
    /// </summary>
    public sealed class ExtensibleElement : AbstractBaseComponent
    {
        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public ExtensibleElement(XElement element) : base(element)
        {
        }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The namespace cannot be the DDMS namespace.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            if (DDMSVersion.IsSupportedDDMSNamespace(Namespace))
                throw new InvalidDDMSException("Extensible elements cannot be defined in the DDMS namespace.");
            base.Validate();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            return ("");
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is ExtensibleElement))
                return (false);
            
            var test = (ExtensibleElement) obj;
            return (Element.ToString().Equals(test.Element.ToString()));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + Element.ToString().GetHashCode();
            return (result);
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(ExtensibleElement element)
            {
                Xml = element.ToString();
            }

            /// <summary>
            ///     Builder accessor for the XML string representing the element.
            /// </summary>
            public virtual string Xml { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                    return (null);
                try
                {
                    XDocument doc = XDocument.Parse(Xml);
                    return (new ExtensibleElement(doc.Root));
                }
                catch (Exception e)
                {
                    throw new InvalidDDMSException("Could not create a valid element from XML string: " + e.Message);
                }
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(Xml)); }
            }
        }
    }
}