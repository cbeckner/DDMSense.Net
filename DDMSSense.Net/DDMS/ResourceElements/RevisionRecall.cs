#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.DDMS.Summary;
using DDMSense.DDMS.Summary.Xlink;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.ResourceElements
{
    /// <summary>
    ///     An immutable implementation of ddms:revisionRecall.
    ///     <para>
    ///         A revisionRecall element will either contain free child text describing the recall, or a set of link and
    ///         details
    ///         elements.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>A valid component cannot have both non-empty child text and nested elements.</li>
    ///                 </ul>
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Nested Elements</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:link</u>: Links to further information about the recall (0-many optional), implemented as a
    ///                 <see cref="Link" /><br />
    ///                 <u>ddms:details</u>: Further details about the recall (0-many optional), implemented as a
    ///                 <see cref="Details" /><br />
    ///             </td>
    ///         </tr>
    ///     </table>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>ddms:revisionID</u>: a sequential integer for the recall (required)<br />
    ///                 <u>ddms:revisionType</u>: an enumerated type for the recall (required)<br />
    ///                 <u>network</u>: the name of the network, taken from a token list (optional)<br />
    ///                 <u>otherNetwork</u>: an alternate network name (optional)<br />
    ///                 <u>
    ///                     <see cref="XLinkAttributes" />
    ///                 </u>
    ///                 : If set, the xlink:type attribute must have a fixed value of "resource".<br />
    ///                 <u>
    ///                     <see cref="SecurityAttributes" />
    ///                 </u>
    ///                 :  The classification and ownerProducer attributes are required.
    ///             </td>
    ///         </tr>
    ///     </table>
    

    /// </summary>
    public sealed class RevisionRecall : AbstractBaseComponent
    {
        private const string FIXED_TYPE = "resource";

        /// <summary>
        ///     The prefix of the network attributes
        /// </summary>
        public const string NO_PREFIX = "";

        /// <summary>
        ///     The namespace of the network attributes
        /// </summary>
        public const string NO_NAMESPACE = "";

        private const string REVISION_ID_NAME = "revisionID";
        private const string REVISION_TYPE_NAME = "revisionType";
        private const string NETWORK_NAME = "network";
        private const string OTHER_NETWORK_NAME = "otherNetwork";

        private readonly List<string> RevisionTypeTypes;

        public RevisionRecall()
        {
            RevisionTypeTypes = new List<string>();
            RevisionTypeTypes.Add("ADMINISTRATIVE RECALL");
            RevisionTypeTypes.Add("ADMINISTRATIVE REVISION");
            RevisionTypeTypes.Add("SUBSTANTIVE RECALL");
            RevisionTypeTypes.Add("SUBSTANTIVE REVISION");

            Details = new List<Details>();
            Links = new List<Link>();
            XLinkAttributes = new XLinkAttributes();
        }

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public RevisionRecall(XElement element)
            : this()
        {
            try
            {
                SetElement(element, false);
                Links = new List<Link>();
                IEnumerable<XElement> links = element.Elements(XName.Get(Link.GetName(DDMSVersion), Namespace));
                foreach (var link in links)
                    Links.Add(new Link(link));

                Details = new List<Details>();
                IEnumerable<XElement> details = element.Elements(XName.Get(ResourceElements.Details.GetName(DDMSVersion), Namespace));
                foreach (var detail in details)
                    Details.Add(new Details(detail));

                string revisionID = element.Attribute(XName.Get(REVISION_ID_NAME, Namespace)).Value;
                if (!String.IsNullOrEmpty(revisionID))
                {
                    try
                    {
                        RevisionID = Convert.ToInt32(revisionID);
                    }
                    catch (FormatException)
                    {
                        //FormatException
                        // 	This will be thrown as an InvalidDDMSException during validation
                    }
                }
                XLinkAttributes = new XLinkAttributes(element);
                SecurityAttributes = new SecurityAttributes(element);
                Validate();
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, based on links and details.
        /// </summary>
        /// <param name="links"> associated links (optional) </param>
        /// <param name="details"> associated details (optional) </param>
        /// <param name="revisionID"> integer ID for this recall (required) </param>
        /// <param name="revisionType"> type of revision (required) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public RevisionRecall(List<Link> links, List<Details> details, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes, SecurityAttributes securityAttributes)
            : this(null, links, details, revisionID, revisionType, network, otherNetwork, xlinkAttributes, securityAttributes)
        {
        }

        /// <summary>
        ///     Constructor for creating a component from raw data, based on child text.
        /// </summary>
        /// <param name="value"> the child text describing this revision (required) </param>
        /// <param name="revisionID"> integer ID for this recall (required) </param>
        /// <param name="revisionType"> type of revision (required) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public RevisionRecall(string value, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes, SecurityAttributes securityAttributes)
            : this(value, null, null, revisionID, revisionType, network, otherNetwork, xlinkAttributes, securityAttributes)
        {
        }

        /// <summary>
        ///     Private constructor for creating a component from raw data.
        /// </summary>
        /// <param name="value"> the child text describing this revision (required) </param>
        /// <param name="links"> associated links (optional) </param>
        /// <param name="details"> associated details (optional) </param>
        /// <param name="revisionID"> integer ID for this recall (required) </param>
        /// <param name="revisionType"> type of revision (required) </param>
        /// <param name="network"> the network (optional) </param>
        /// <param name="otherNetwork"> another network (optional) </param>
        /// <param name="xlinkAttributes"> simple xlink attributes (optional) </param>
        /// <param name="securityAttributes"> security attributes (required) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        private RevisionRecall(string value, List<Link> links, List<Details> details, int? revisionID, string revisionType, string network, string otherNetwork, XLinkAttributes xlinkAttributes, SecurityAttributes securityAttributes)
        {
            RevisionTypeTypes = new List<string>();
            RevisionTypeTypes.Add("ADMINISTRATIVE RECALL");
            RevisionTypeTypes.Add("ADMINISTRATIVE REVISION");
            RevisionTypeTypes.Add("SUBSTANTIVE RECALL");
            RevisionTypeTypes.Add("SUBSTANTIVE REVISION");

            try
            {
                if (links == null)
                    links = new List<Link>();

                if (details == null)
                    details = new List<Details>();

                XElement element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.CurrentVersion), value);
                foreach (var link in links)
                    element.Add(link.ElementCopy);

                foreach (var detail in details)
                    element.Add(detail.ElementCopy);

                if (revisionID != null)
                {
                    RevisionID = revisionID;
                    Util.Util.AddDDMSAttribute(element, REVISION_ID_NAME, revisionID.ToString());
                }
                Util.Util.AddDDMSAttribute(element, REVISION_TYPE_NAME, revisionType);
                Util.Util.AddAttribute(element, NO_PREFIX, NETWORK_NAME, NO_NAMESPACE, network);
                Util.Util.AddAttribute(element, NO_PREFIX, OTHER_NETWORK_NAME, NO_NAMESPACE, otherNetwork);
                Links = links;
                Details = details;
                XLinkAttributes = XLinkAttributes.GetNonNullInstance(xlinkAttributes);
                XLinkAttributes.AddTo(element);
                SecurityAttributes = SecurityAttributes.GetNonNullInstance(securityAttributes);
                SecurityAttributes.AddTo(element);
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.AddRange(Links);
                list.AddRange(Details);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the list of Links.
        /// </summary>
        public List<Link> Links { get; private set; }

        /// <summary>
        ///     Accessor for the list of Details.
        /// </summary>
        public List<Details> Details { get; private set; }

        /// <summary>
        ///     Accessor for the value of the child text.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Accessor for the revisionID attribute.
        /// </summary>
        public int? RevisionID { get; set; }

        /// <summary>
        ///     Accessor for the revisionType attribute.
        /// </summary>
        public string RevisionType
        {
            get { return (GetAttributeValue(REVISION_TYPE_NAME)); }
        }

        /// <summary>
        ///     Accessor for the network attribute.
        /// </summary>
        public string Network
        {
            get { return (GetAttributeValue(NETWORK_NAME, NO_NAMESPACE)); }
        }

        /// <summary>
        ///     Accessor for the otherNetwork attribute.
        /// </summary>
        public string OtherNetwork
        {
            get { return (GetAttributeValue(OTHER_NETWORK_NAME, NO_NAMESPACE)); }
        }

        /// <summary>
        ///     Accessor for the XDetails Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public XLinkAttributes XLinkAttributes { get; set; }

        /// <summary>
        ///     Accessor for the Security Attributes. Will always be non-null, even if it has no values set.
        /// </summary>
        public override SecurityAttributes SecurityAttributes { get; set; }

        /// <summary>
        ///     Validates the component.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>The qualified name of the element is correct.</li>
        ///                 <li>A valid component cannot have both non-empty child text and nested elements.</li>
        ///                 <li>Any links should have security attributes.</li>
        ///                 <li>The revisionID must be a valid Integer.</li>
        ///                 <li>The revisionType must be a valid type token.</li>
        ///                 <li>If set, the xlink:type attribute has a value of "resource".</li>
        ///                 <li>If set, the network attribute must be a valid network token.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));

            bool hasChildText = false;
            foreach (var child in Element.Descendants())
                hasChildText = (hasChildText || (!String.IsNullOrEmpty(child.Value.Trim())));

            bool hasNestedElements = (Links.Count > 0 || Details.Count > 0);

            if (hasChildText && hasNestedElements)
                throw new InvalidDDMSException("A ddms:revisionRecall element cannot have both child text and nested elements.");

            foreach (var link in Links)
            {
                Util.Util.RequireDDMSValue("link security attributes", link.SecurityAttributes);
                link.SecurityAttributes.RequireClassification();
            }
            Util.Util.RequireDDMSValue("revision ID", RevisionID);
            if (!RevisionTypeTypes.Contains(RevisionType))
                throw new InvalidDDMSException("The revisionType attribute must be one of " + RevisionTypeTypes);

            if (!String.IsNullOrEmpty(XLinkAttributes.Type) && !XLinkAttributes.Type.Equals(FIXED_TYPE))
                throw new InvalidDDMSException("The type attribute must have a fixed value of \"" + FIXED_TYPE + "\".");

            if (!String.IsNullOrEmpty(Network))
                ISMVocabulary.RequireValidNetwork(Network);

            base.Validate();
        }

        /// <summary>
        ///     Validates any conditions that might result in a warning.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>Include any warnings from the XLink attributes.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            if (XLinkAttributes != null)
                AddWarnings(XLinkAttributes.ValidationWarnings, true);

            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            bool hasNestedElements = (Links.Count > 0 || Details.Count > 0);
            string localPrefix = BuildPrefix(prefix, Name, suffix);
            var text = new StringBuilder();
            if (!hasNestedElements)
                text.Append(BuildOutput(isHtml, localPrefix, Value));

            text.Append(BuildOutput(isHtml, localPrefix + "." + REVISION_ID_NAME, Convert.ToString(RevisionID)));
            text.Append(BuildOutput(isHtml, localPrefix + "." + REVISION_TYPE_NAME, RevisionType));
            text.Append(BuildOutput(isHtml, localPrefix + "." + NETWORK_NAME, Network));
            text.Append(BuildOutput(isHtml, localPrefix + "." + OTHER_NETWORK_NAME, OtherNetwork));
            text.Append(BuildOutput(isHtml, localPrefix + ".", Links));
            text.Append(BuildOutput(isHtml, localPrefix + ".", Details));
            text.Append(XLinkAttributes.GetOutput(isHtml, localPrefix + "."));
            text.Append(SecurityAttributes.GetOutput(isHtml, localPrefix + "."));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is RevisionRecall))
                return (false);

            var test = (RevisionRecall)obj;
            return (Value.Equals(test.Value) && Util.Util.NullEquals(RevisionID, test.RevisionID) &&
                    RevisionType.Equals(test.RevisionType) && Network.Equals(test.Network) &&
                    OtherNetwork.Equals(test.OtherNetwork) && XLinkAttributes.Equals(test.XLinkAttributes));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7 * result + Value.GetHashCode();
            result = 7 * result + RevisionID.GetHashCode();
            result = 7 * result + RevisionType.GetHashCode();
            result = 7 * result + Network.GetHashCode();
            result = 7 * result + OtherNetwork.GetHashCode();
            result = 7 * result + XLinkAttributes.GetHashCode();
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
            return ("revisionRecall");
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
                Details = new List<Details.Builder>();
                Links = new List<Link.Builder>();
                SecurityAttributes = new SecurityAttributes.Builder();
                XLinkAttributes = new XLinkAttributes.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(RevisionRecall recall)
                : this()
            {
                foreach (var link in recall.Links)
                    Links.Add(new Link.Builder(link));

                foreach (var detail in recall.Details)
                    Details.Add(new Details.Builder(detail));

                if (recall.Links.Count == 0 && recall.Details.Count == 0)
                    Value = recall.Value;

                RevisionID = recall.RevisionID;
                RevisionType = recall.RevisionType;
                Network = recall.Network;
                OtherNetwork = recall.OtherNetwork;
                XLinkAttributes = new XLinkAttributes.Builder(recall.XLinkAttributes);
                SecurityAttributes = new SecurityAttributes.Builder(recall.SecurityAttributes);
            }

            /// <summary>
            ///     Builder accessor for the value
            /// </summary>
            public virtual string Value { get; set; }

            /// <summary>
            ///     Builder accessor for the links
            /// </summary>
            public virtual List<Link.Builder> Links { get; set; }

            /// <summary>
            ///     Builder accessor for the details
            /// </summary>
            public virtual List<Details.Builder> Details { get; set; }

            /// <summary>
            ///     Builder accessor for the revisionID
            /// </summary>
            public virtual int? RevisionID { get; set; }

            /// <summary>
            ///     Builder accessor for the revisionType
            /// </summary>
            public virtual string RevisionType { get; set; }

            /// <summary>
            ///     Builder accessor for the network
            /// </summary>
            public virtual string Network { get; set; }

            /// <summary>
            ///     Builder accessor for the otherNetwork
            /// </summary>
            public virtual string OtherNetwork { get; set; }

            /// <summary>
            ///     Builder accessor for the XLink Attributes
            /// </summary>
            public virtual XLinkAttributes.Builder XLinkAttributes { get; set; }

            /// <summary>
            ///     Builder accessor for the Security Attributes
            /// </summary>
            public virtual SecurityAttributes.Builder SecurityAttributes { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                if (Empty)
                    return (null);

                var links = new List<Link>();
                foreach (IBuilder builder in Links)
                {
                    var component = (Link)builder.Commit();
                    if (component != null)
                        links.Add(component);
                    }
                var details = new List<Details>();
                foreach (IBuilder builder in Details)
                {
                    var component = (Details)builder.Commit();
                    if (component != null)
                        details.Add(component);
                    }
                return (new RevisionRecall(Value, links, details, RevisionID, RevisionType, Network, OtherNetwork, XLinkAttributes.Commit(), SecurityAttributes.Commit()));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get
                {
                    bool hasValueInList = false;
                    foreach (IBuilder builder in Links)
                        hasValueInList = hasValueInList || !builder.Empty;

                    foreach (IBuilder builder in Details)
                        hasValueInList = hasValueInList || !builder.Empty;

                    return (!hasValueInList &&
                            String.IsNullOrEmpty(Value) &&
                            RevisionID == null &&
                            String.IsNullOrEmpty(RevisionType) &&
                            String.IsNullOrEmpty(Network) &&
                            String.IsNullOrEmpty(OtherNetwork) &&
                            XLinkAttributes.Empty &&
                            SecurityAttributes.Empty);
                }
            }
        }
    }
}