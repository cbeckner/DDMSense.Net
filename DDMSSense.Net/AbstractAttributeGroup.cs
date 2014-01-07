#region usings

using System.Collections.Generic;
using DDMSense.DDMS;
using DDMSense.DDMS.SecurityElements.Ism;
using DDMSense.Util;

#endregion

namespace DDMSense
{
    /// <summary>
    ///     Top-level base class for attribute groups, such as <see cref="SecurityAttributes" />.
    ///     <para>
    ///         Extensions of this class are generally expected to be immutable. It is assumed that after the constructor on
    ///         a component has been called, the component will be well-formed and valid.
    ///     </para>
    
    ///     @since 1.1.0
    /// </summary>
    public abstract class AbstractAttributeGroup
    {
        private readonly string _xmlNamespace;
        private List<ValidationMessage> _warnings;

        /// <summary>
        ///     Constructor which stores the XML namespace of the enclosing element
        /// </summary>
        public AbstractAttributeGroup(string xmlNamespace)
        {
            _xmlNamespace = xmlNamespace;
        }

        /// <summary>
        ///     Accessor for the DDMS namespace on the enclosing element.
        /// </summary>
        public virtual DDMSVersion DDMSVersion
        {
            get { return (DDMSVersion.GetVersionForNamespace(_xmlNamespace)); }
        }

        /// <summary>
        ///     Returns a list of any warning messages that occurred during validation. Warnings do not prevent a valid component
        ///     from being formed.
        /// </summary>
        /// <returns> a list of warnings </returns>
        public virtual List<ValidationMessage> ValidationWarnings
        {
            get { return Warnings; }
        }

        /// <summary>
        ///     Accessor for the list of validation warnings.
        ///     <para>
        ///         This is the private copy that should be manipulated during validation. Lazy initialization.
        ///     </para>
        /// </summary>
        /// <returns> an editable list of warnings </returns>
        protected internal virtual List<ValidationMessage> Warnings
        {
            get
            {
                if (_warnings == null)
                    _warnings = new List<ValidationMessage>();
                
                return (_warnings);
            }
        }

        /// <summary>
        ///     Base validation case for attribute groups.
        ///     <table class="info">
        ///         <tr class="infoHeader">
        ///             <th>Rules</th>
        ///         </tr>
        ///         <tr>
        ///             <td class="infoBody">
        ///                 <li>No rules are validated at this level. Extending classes may have additional rules.</li>
        ///             </td>
        ///         </tr>
        ///     </table>
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        protected internal virtual void Validate()
        {
        }

        /// <summary>
        ///     Compares the DDMS version of these attributes to another DDMS version
        /// </summary>
        /// <param name="version"> the version to test </param>
        /// <exception cref="InvalidDDMSException"> if the versions do not match </exception>
        protected internal virtual void ValidateSameVersion(DDMSVersion version)
        {
            if (!DDMSVersion.Equals(version))
            {
                throw new InvalidDDMSException(
                    "These attributes cannot decorate a component with a different DDMS version.");
            }
        }

        /// <summary>
        ///     Outputs to HTML or Text with a prefix at the beginning of each meta tag or line.
        /// </summary>
        /// <param name="isHtml"> true for HTML, false for Text </param>
        /// <param name="prefix"> the prefix to add </param>
        /// <returns> the HTML or Text output </returns>
        public abstract string GetOutput(bool isHtml, string prefix);
    }
}