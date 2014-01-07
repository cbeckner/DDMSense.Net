#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDMSense.Extensions;
using DDMSense.Util;

#endregion

namespace DDMSense.DDMS.FormatElements
{
    /// <summary>
    ///     An immutable implementation of ddms:format.
    ///     <para>
    ///         Before DDMS 4.0.1, a format element contains a locally defined Media construct.
    ///         This Media construct is a container for the mimeType,extent, and medium of a resource.
    ///         It exists only inside of a ddms:format parent, so it is not implemented as a Java
    ///         object. Starting in DDMS 4.0.1, the Media wrapper has been removed.
    ///     </para>
    ///     <table class="info">
    ///         <tr class="infoHeader">
    ///             <th>Strictness</th>
    ///         </tr>
    ///         <tr>
    ///             <td class="infoBody">
    ///                 <para>DDMSence is stricter than the specification in the following ways:</para>
    ///                 <ul>
    ///                     <li>A non-empty mimeType value is required.</li>
    ///                 </ul>
    ///                 <para>DDMSence allows the following legal, but nonsensical constructs:</para>
    ///                 <ul>
    ///                     <li>A medium element can be used with no child text.</li>
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
    ///                 <u>ddms:mimeType</u>: the MIME type (exactly 1 required)<br />
    ///                 <u>ddms:extent</u>: the format extent (0-1 optional), implemented as a <see cref="Extent" /><br />
    ///                 <u>ddms:medium</u>: the physical medium (0-1 optional)<br />
    ///             </td>
    ///         </tr>
    ///     </table>
    
    
    /// </summary>
    public sealed class Format : AbstractBaseComponent
    {
        private const string MediaName = "Media";
        private const string MimeTypeName = "mimeType";
        private const string MediumName = "medium";
        private string _medium;
        private string _mimeType;

        /// <summary>
        ///     Constructor for creating a component from a XOM Element
        /// </summary>
        /// <param name="element"> the XOM element representing this </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Format(XElement element)
        {
            try
            {
                SetElement(element, false);
                XElement mediaElement = MediaElement;
                if (mediaElement != null)
                {
                    XElement mimeTypeElement = mediaElement.Element(XName.Get(MimeTypeName, Namespace));
                    if (mimeTypeElement != null)
                        _mimeType = mimeTypeElement.Value;
                    
                    XElement extentElement = mediaElement.Element(XName.Get(Extent.GetName(DDMSVersion), Namespace));
                    if (extentElement != null)
                        Extent = new Extent(extentElement);
                    
                    XElement mediumElement = mediaElement.Element(XName.Get(MediumName, Namespace));
                    if (mediumElement != null)
                        _medium = mediumElement.Value;
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
        ///     Constructor for creating a component from raw data
        /// </summary>
        /// <param name="mimeType"> the mimeType element (required) </param>
        /// <param name="extent"> the extent element (may be null) </param>
        /// <param name="medium"> the medium element (may be null) </param>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        public Format(string mimeType, Extent extent, string medium)
        {
            try
            {
                XElement element = Util.Util.BuildDDMSElement(GetName(DDMSVersion.GetCurrentVersion()), null);
                XElement mediaElement = DDMSVersion.GetCurrentVersion().IsAtLeast("4.0.1")
                    ? element
                    : Util.Util.BuildDDMSElement(MediaName, null);
                Util.Util.AddDDMSChildElement(mediaElement, MimeTypeName, mimeType);
                if (extent != null)
                    mediaElement.Add(extent.ElementCopy);
                Util.Util.AddDDMSChildElement(mediaElement, MediumName, medium);

                if (!DDMSVersion.GetCurrentVersion().IsAtLeast("4.0.1"))
                    element.Add(mediaElement);

                _mimeType = mimeType;
                Extent = extent;
                _medium = medium;
                SetElement(element, true);
            }
            catch (InvalidDDMSException e)
            {
                e.Locator = QualifiedName;
                throw (e);
            }
        }

        /// <see cref="AbstractBaseComponent#getLocatorSuffix()"></see>
        protected internal override string LocatorSuffix
        {
            get
            {
                return (DDMSVersion.IsAtLeast("4.0.1")
                    ? ""
                    : ValidationMessage.ElementPrefix + Element.GetPrefix() + ":" + MediaName);
            }
        }

        /// <see cref="AbstractBaseComponent#getNestedComponents()"></see>
        protected internal override List<IDDMSComponent> NestedComponents
        {
            get
            {
                var list = new List<IDDMSComponent>();
                list.Add(Extent);
                return (list);
            }
        }

        /// <summary>
        ///     Accessor for the element which contains the mimeType, medium, and extent. Before DDMS 4.0.1,
        ///     this is a wrapper element called ddms:Media. Starting in DDMS 4.0.1, it is the ddms:format
        ///     element itself.
        /// </summary>
        private XElement MediaElement
        {
            get { return (DDMSVersion.IsAtLeast("4.0.1") ? Element : GetChild(MediaName)); }
        }

        /// <summary>
        ///     Accessor for the mimeType element child text. Will return an empty string if not set, but that cannot occur after
        ///     instantiation.
        /// </summary>
        public string MimeType
        {
            get { return (Util.Util.GetNonNullString(_mimeType)); }
            set { _mimeType = value; }
        }

        /// <summary>
        ///     Accessor for the extent
        /// </summary>
        public Extent Extent { get; set; }

        /// <summary>
        ///     Convenience accessor for the extent qualifier. Returns an empty string if there is not extent.
        /// </summary>
        public string ExtentQualifier
        {
            get { return (Extent == null ? "" : Extent.Qualifier); }
        }

        /// <summary>
        ///     Convenience accessor for the extent value. Returns an empty string if there is not extent.
        /// </summary>
        public string ExtentValue
        {
            get { return (Extent == null ? "" : Extent.Value); }
        }

        /// <summary>
        ///     Accessor for the medium element child text
        /// </summary>
        public string Medium
        {
            get { return (Util.Util.GetNonNullString(_medium)); }
            set { _medium = value; }
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
        ///                 <li>A mimeType exists, and is not empty.</li>
        ///                 <li>Exactly 1 mimeType, 0-1 extents, and 0-1 mediums exist.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <see cref="AbstractBaseComponent#validate()"></see>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal override void Validate()
        {
            Util.Util.RequireDDMSQualifiedName(Element, GetName(DDMSVersion));
            XElement mediaElement = MediaElement;
            Util.Util.RequireDDMSValue("Media element", mediaElement);
            Util.Util.RequireDDMSValue(MimeTypeName, MimeType);
            Util.Util.RequireBoundedChildCount(mediaElement, MimeTypeName, 1, 1);
            Util.Util.RequireBoundedChildCount(mediaElement, Extent.GetName(DDMSVersion), 0, 1);
            Util.Util.RequireBoundedChildCount(mediaElement, MediumName, 0, 1);

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
        ///                 <li>A ddms:medium element was found with no value.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        protected internal override void ValidateWarnings()
        {
            XElement mediaElement = MediaElement;
            if (String.IsNullOrEmpty(Medium) && mediaElement.Elements(XName.Get(MediumName, mediaElement.Name.NamespaceName)).Count() == 1)
                AddWarning("A ddms:medium element was found with no value.");
            
            base.ValidateWarnings();
        }

        /// <see cref="AbstractBaseComponent#getOutput(boolean, String, String)"></see>
        public override string GetOutput(bool isHtml, string prefix, string suffix)
        {
            string localPrefix = BuildPrefix(prefix, Name, suffix + ".");
            if (!DDMSVersion.IsAtLeast("4.0.1"))
                localPrefix += MediaName + ".";
            
            var text = new StringBuilder();
            text.Append(BuildOutput(isHtml, localPrefix + MimeTypeName, MimeType));
            if (Extent != null)
                text.Append(Extent.GetOutput(isHtml, localPrefix, ""));
            
            text.Append(BuildOutput(isHtml, localPrefix + MediumName, Medium));
            return (text.ToString());
        }

        /// <see cref="object#equals(Object)"></see>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is Format))
                return (false);
            
            var test = (Format) obj;
            bool isEqual = MimeType.Equals(test.MimeType) && Medium.Equals(test.Medium);
            return (isEqual);
        }

        /// <see cref="object#hashCode()"></see>
        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 7*result + MimeType.GetHashCode();
            result = 7*result + Medium.GetHashCode();
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
            return ("format");
        }

        /// <summary>
        ///     Builder for this DDMS component.
        /// </summary>
        /// <see cref="IBuilder
        /// @author Brian Uri!
        /// @since 1.8.0"></see>
        [Serializable]
        public class Builder : IBuilder
        {
            /// <summary>
            ///     Empty constructor
            /// </summary>
            public Builder()
            {
                Extent = new Extent.Builder();
            }

            /// <summary>
            ///     Constructor which starts from an existing component.
            /// </summary>
            public Builder(Format format)
            {
                MimeType = format.MimeType;
                if (format.Extent != null)
                    Extent = new Extent.Builder(format.Extent);
                Medium = format.Medium;
            }

            /// <summary>
            ///     Builder accessor for the mimeType element child text.
            /// </summary>
            public virtual string MimeType { get; set; }

            /// <summary>
            ///     Builder accessor for the mediaExtent element.
            /// </summary>
            public virtual Extent.Builder Extent { get; set; }

            /// <summary>
            ///     Builder accessor for the medium element child text.
            /// </summary>
            public virtual string Medium { get; set; }

            /// <see cref="IBuilder#commit()"></see>
            public virtual IDDMSComponent Commit()
            {
                return (Empty ? null : new Format(MimeType, (Extent) Extent.Commit(), Medium));
            }

            /// <see cref="IBuilder#isEmpty()"></see>
            public virtual bool Empty
            {
                get { return (String.IsNullOrEmpty(MimeType) && String.IsNullOrEmpty(Medium) && Extent.Empty); }
            }
        }
    }
}