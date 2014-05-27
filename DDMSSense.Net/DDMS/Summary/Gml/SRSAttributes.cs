#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DDMSense.Util;

#endregion usings

using DDMSense.Extensions;

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

namespace DDMSense.DDMS.Summary.Gml
{
    #region usings

    using Element = XElement;

    #endregion usings

    /// <summary>
    ///     Attribute group for the four SRS attributes used in the GML profile.
    ///     <para>
    ///         Because the GML-Profile defines these attributes locally inside of attribute groups, they are not in any
    ///         namespace.
    ///         Some older examples on the DDMS website inaccurately display the attributes with the gml: prefix.
    ///     </para>
    ///     <para>
    ///         When validating this attribute group, the required/optional nature of the srsName attribute is not checked.
    ///         Because
    ///         that limitation depends on the parent element (for example, gml:Point and gml:Polygon require an srsName, but
    ///         gml:pos
    ///         does not), the parent element should be responsible for checking.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Attributes</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <u>srsName</u>: A URI-based name (optional on gml:pos, required everywhere else)<br />
    ///                 <u>srsDimension</u>: A positive integer dimension (optional)<br />
    ///                 <u>axisLabels</u>: Ordered list of labels for the axes, as a space-delimited list of NCNames (valid XML
    ///                 names without
    ///                 colons) (optional, but if no srsName is set, this should be omitted too)<br />
    ///                 <u>uomLabels</u>: Ordered list of unit of measure (uom) labels for all the axes, as a space-delimited
    ///                 list of NCNames
    ///                 (valid XML names without colons) (required when axisLabels is set)<br />
    ///             </td>
    ///         </tr>
    ///     </table>

    /// </summary>
    public sealed class SRSAttributes : AbstractAttributeGroup
    {
        /// <summary>
        ///     The prefix of the shared attributes
        /// </summary>
        public const string NO_PREFIX = "";

        /// <summary>
        ///     The namespace of the shared attributes
        /// </summary>
        public const string NO_NAMESPACE = "";

        private const string SRS_NAME_NAME = "srsName";
        private const string SRS_DIMENSION_NAME = "srsDimension";
        private const string AXIS_LABELS_NAME = "axisLabels";
        private const string UOM_LABELS_NAME = "uomLabels";
        private List<string> _axisLabels;
        private int? _srsDimension;
        private string _srsName;
        private List<string> _uomLabels;

        /// <summary>
        ///     Base constructor
        /// </summary>
        /// <param name="element"> the XOM element which is decorated with these attributes. </param>
        public SRSAttributes(Element element)
            : base(element.Name.NamespaceName)
        {
            _srsName = element.Attribute(XName.Get(SRS_NAME_NAME, NO_NAMESPACE)).ToNonNullString();

            string srsDimension = element.Attribute(XName.Get(SRS_DIMENSION_NAME, NO_NAMESPACE)).ToNonNullString();
            if (!String.IsNullOrEmpty(srsDimension))
            {
                _srsDimension = Convert.ToInt32(srsDimension);
            }
            string axisLabels = element.Attribute(XName.Get(AXIS_LABELS_NAME, NO_NAMESPACE)).ToNonNullString();
            _axisLabels = new List<string>();
            if (!String.IsNullOrEmpty(axisLabels))
            {
                _axisLabels.AddRange(Util.Util.GetXsListAsList(axisLabels));
            }
            string uomLabels = element.Attribute(XName.Get(UOM_LABELS_NAME, NO_NAMESPACE)).ToNonNullString();
            _uomLabels = new List<string>();
            if (!String.IsNullOrEmpty(uomLabels))
            {
                _uomLabels.AddRange(Util.Util.GetXsListAsList(uomLabels));
            }
            Validate();
        }

        /// <summary>
        ///     Constructor which builds from raw data.
        /// </summary>
        /// <param name="srsName">	the srsName (required if the name is not "pos") </param>
        /// <param name="srsDimension"> the srsDimension (optional) </param>
        /// <param name="axisLabels"> the axis labels (optional, but should be omitted if no srsName is set) </param>
        /// <param name="uomLabels"> the labels for UOM (required when axisLabels is set) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public SRSAttributes(string srsName, int? srsDimension, List<string> axisLabels, List<string> uomLabels)
            : base(DDMSVersion.CurrentVersion.GmlNamespace)
        {
            if (axisLabels == null)
            {
                axisLabels = new List<string>();
            }
            if (uomLabels == null)
            {
                uomLabels = new List<string>();
            }
            _srsName = srsName;
            _srsDimension = srsDimension;
            _axisLabels = axisLabels;
            _uomLabels = uomLabels;
            Validate();
        }

        /// <summary>
        ///     Accessor for the srsName.
        /// </summary>
        public string SrsName
        {
            get { return (_srsName.ToNonNullString()); }
            set { _srsName = value; }
        }

        /// <summary>
        ///     Accessor for the srsDimension. May return null if not set.
        /// </summary>
        public int? SrsDimension
        {
            get { return (_srsDimension); }
            set { _srsDimension = value; }
        }

        /// <summary>
        ///     Accessor for the axisLabels. Will return an empty list if not set.
        /// </summary>
        public List<string> AxisLabels
        {
            get { return _axisLabels; }
            set { _axisLabels = value; }
        }

        /// <summary>
        ///     Accessor for the String representation of the axisLabels
        /// </summary>
        public string AxisLabelsAsXsList
        {
            get { return (Util.Util.GetXsList(AxisLabels)); }
        }

        /// <summary>
        ///     Accessor for the uomLabels. Will return an empty list if not set.
        /// </summary>
        public List<string> UomLabels
        {
            get { return _uomLabels; }
            set { _uomLabels = value; }
        }

        /// <summary>
        ///     Accessor for the String representation of the uomLabels
        /// </summary>
        public string UomLabelsAsXsList
        {
            get { return (Util.Util.GetXsList(UomLabels)); }
        }

        /// <summary>
        ///     Returns a non-null instance of SRS attributes. If the instance passed in is not null, it will be returned.
        /// </summary>
        /// <param name="srsAttributes"> the attributes to return by default </param>
        /// <returns> a non-null attributes instance </returns>
        /// <exception cref="InvalidDDMSException"> if there are problems creating the empty attributes instance </exception>
        public static SRSAttributes GetNonNullInstance(SRSAttributes srsAttributes)
        {
            return (srsAttributes == null ? new SRSAttributes(null, null, null, null) : srsAttributes);
        }

        /// <summary>
        ///     Convenience method to add these attributes onto an existing XOM Element
        /// </summary>
        /// <param name="element"> the element to decorate </param>
        /// <exception cref="InvalidDDMSException"> if the DDMS version of the element is different </exception>
        public void AddTo(Element element)
        {
            DDMSVersion elementVersion = DDMSVersion.GetVersionForNamespace(element.Name.NamespaceName);
            ValidateSameVersion(elementVersion);
            Util.Util.AddAttribute(element, NO_PREFIX, SRS_NAME_NAME, NO_NAMESPACE, SrsName);
            if (SrsDimension != null)
            {
                Util.Util.AddAttribute(element, NO_PREFIX, SRS_DIMENSION_NAME, NO_NAMESPACE,
                    Convert.ToString(SrsDimension));
            }
            Util.Util.AddAttribute(element, NO_PREFIX, AXIS_LABELS_NAME, NO_NAMESPACE, AxisLabelsAsXsList);
            Util.Util.AddAttribute(element, NO_PREFIX, UOM_LABELS_NAME, NO_NAMESPACE, UomLabelsAsXsList);
        }

        /// <summary>
        ///     Validates the attribute group.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>If the srsName is set, it must be a valid URI.</li>
        ///                 <li>If the srsDimension is set, it must be positive.</li>
        ///                 <li>If the srsName is not set, the axisLabels must be not set or empty.</li>
        ///                 <li>If the axisLabels are not set or empty, the uomLabels must be not set or empty.</li>
        ///                 <li>Each axisLabel must be a NCName.</li>
        ///                 <li>Each uomLabel must be a NCName.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            if (!String.IsNullOrEmpty(SrsName))
            {
                Util.Util.RequireDDMSValidUri(SrsName);
            }
            if (SrsDimension != null && (int)SrsDimension < 0)
            {
                throw new InvalidDDMSException("The srsDimension must be a positive integer.");
            }
            if (String.IsNullOrEmpty(SrsName) && AxisLabels.Count > 0)
            {
                throw new InvalidDDMSException("The axisLabels attribute can only be used in tandem with an srsName.");
            }
            if (AxisLabels.Count == 0 && UomLabels.Count > 0)
            {
                throw new InvalidDDMSException("The uomLabels attribute can only be used in tandem with axisLabels.");
            }
            Util.Util.RequireValidNCNames(AxisLabels);
            Util.Util.RequireValidNCNames(UomLabels);
            base.Validate();
        }

        /// <see cref="AbstractAttributeGroup#getOutput(boolean, String)"></see>
        public override string GetOutput(bool isHtml, string prefix)
        {
            string localPrefix = prefix.ToNonNullString();
            var text = new StringBuilder();
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + "srsName", SrsName));
            if (SrsDimension != null)
            {
                text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + "srsDimension",
                    Convert.ToString(SrsDimension)));
            }
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + "axisLabels", AxisLabelsAsXsList));
            text.Append(AbstractBaseComponent.BuildOutput(isHtml, localPrefix + "uomLabels", UomLabelsAsXsList));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!(obj is SRSAttributes))
            {
                return (false);
            }
            var test = (SRSAttributes)obj;
            return (SrsName.Equals(test.SrsName) && Util.Util.NullEquals(SrsDimension, test.SrsDimension) &&
                    Util.Util.ListEquals(AxisLabels, test.AxisLabels) && Util.Util.ListEquals(UomLabels, test.UomLabels));
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = 0;
            result = 7 * result + SrsName.GetHashCode();
            if (SrsDimension != null)
            {
                result = 7 * result + SrsDimension.GetHashCode();
            }
            result = 7 * result + AxisLabels.GetOrderIndependentHashCode();
            result = 7 * result + UomLabels.GetOrderIndependentHashCode();
            return (result);
        }

        /// <summary>
        ///     Builder for these attributes.
        ///     <para>
        ///         This class does not implement the IBuilder interface, because the behavior of commit() is at odds with the
        ///         standard commit() method. As an attribute group, an empty attribute group will always be returned instead of
        ///         null.
        ///     </para>
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder
        {
            internal List<string> _axisLabels;
            internal int? _srsDimension;
            internal string _srsName;
            internal List<string> _uomLabels;

            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(SRSAttributes attributes)
            {
                SrsName = attributes.SrsName;
                SrsDimension = attributes.SrsDimension;
                AxisLabels = attributes.AxisLabels;
                UomLabels = attributes.UomLabels;
            }

            /// <summary>
            ///     Checks if any values have been provided for this Builder.
            /// </summary>
            /// <returns> true if every field is empty </returns>
            public virtual bool Empty
            {
                get
                {
                    return (String.IsNullOrEmpty(SrsName) && SrsDimension == null &&
                            Util.Util.ContainsOnlyEmptyValues(AxisLabels) &&
                            Util.Util.ContainsOnlyEmptyValues(UomLabels));
                }
            }

            /// <summary>
            ///     Builder accessor for the srsName
            /// </summary>
            public virtual string SrsName
            {
                get { return _srsName; }
                set { _srsName = value; }
            }

            /// <summary>
            ///     Builder accessor for the srsDimension
            /// </summary>
            public virtual int? SrsDimension
            {
                get { return _srsDimension; }
                set { _srsDimension = value; }
            }

            /// <summary>
            ///     Builder accessor for the axisLabels
            /// </summary>
            public virtual List<string> AxisLabels
            {
                get
                {
                    if (_axisLabels == null)
                    {
                        _axisLabels = new List<string>();
                    }
                    return _axisLabels;
                }
                set { _axisLabels = value; }
            }

            /// <summary>
            ///     Builder accessor for the uomLabels
            /// </summary>
            public virtual List<string> UomLabels
            {
                get
                {
                    if (_uomLabels == null)
                    {
                        _uomLabels = new List<string>();
                    }
                    return _uomLabels;
                }
                set { _uomLabels = value; }
            }

            /// <summary>
            ///     Finalizes the data gathered for this builder instance. Will always return an empty instance instead of
            ///     a null one.
            /// </summary>
            /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
            public virtual SRSAttributes Commit()
            {
                return (new SRSAttributes(SrsName, SrsDimension, AxisLabels, UomLabels));
            }
        }
    }
}