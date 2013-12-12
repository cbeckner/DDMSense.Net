#region usings

using System.Text;

#endregion

namespace DDMSSense.DDMS
{
    /// <summary>
    ///     Simple class representing a validation message.
    ///     <para>
    ///         DDMS components are validated during instantiation, and a component is either valid or does not exist. These
    ///         messages are either embedded in a thrown InvalidDDMSException (in which case they are errors), or stored as
    ///         informational messages on the component itself (warnings).
    ///     </para>
    ///     <para>
    ///         The locator string provides additional information about the context of the error or warning. This string is
    ///         implemented as an XPath string right now: if the error was coming from a ddms:identifier component, the locator
    ///         would
    ///         be "/ddms:identifier". Should a parent component choose to report the warnings/errors of a child component, the
    ///         locator string could be expanded with parent information, such as "/ddms:resource/ddms:identifier".
    ///     </para>
    ///     <para>
    ///         Please note that the XPath string is not intended to drill all the way down to the offending element or
    ///     </para>
    ///     attribute. It should merely provide enough context so that the source of the message can be discovered.
    ///     <para>
    ///         @author Brian Uri!
    ///         @since 0.9.c
    ///     </para>
    /// </summary>
    public sealed class ValidationMessage
    {
        /// <summary>
        ///     Constant type for a warning.
        /// </summary>
        public const string WarningType = "Warning";

        /// <summary>
        ///     Constant type for an error.
        /// </summary>
        public const string ErrorType = "Error";

        /// <summary>
        ///     XPath prefix to separate elements
        /// </summary>
        public const string ElementPrefix = "/";

        private string _locator;

        /// <summary>
        ///     Private constructor. Use factory methods to instantiate.
        /// </summary>
        /// <param name="type"> the type of this message </param>
        /// <param name="text"> the description text' </param>
        /// <param name="locator">
        ///     a locator string, in XPath format. For attributes, use empty string. The parent element will claim
        ///     the attributes.
        /// </param>
        private ValidationMessage(string type, string text, string locator)
        {
            Util.Util.RequireValue("text", text);
            Type = type;
            Text = text;
            Locator = locator;
        }

        /// <summary>
        ///     Accessor for the type
        /// </summary>
        public string Type {get; private set;}

        /// <summary>
        ///     Accessor for the text
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     Accessor for the locator
        /// </summary>
        public string Locator
        {
            get { return _locator; }
            set { _locator = (value == null ? "" : ElementPrefix + value); }
        }

        /// <summary>
        ///     Factory method to create a warning
        /// </summary>
        /// <param name="text"> the description text </param>
        /// <param name="locator">
        ///     a locator string, in XPath format. For attributes, use empty string. The parent element will claim
        ///     the attributes.
        /// </param>
        /// <returns> a new warning message </returns>
        public static ValidationMessage NewWarning(string text, string locator)
        {
            return (new ValidationMessage(WarningType, text, locator));
        }

        /// <summary>
        ///     Factory method to create an error
        /// </summary>
        /// <param name="text"> the description text </param>
        /// <param name="locator">
        ///     a locator string, in XPath format. For attributes, use empty string. The parent element will claim
        ///     the attributes.
        /// </param>
        /// <returns> a new error message </returns>
        public static ValidationMessage NewError(string text, string locator)
        {
            return (new ValidationMessage(ErrorType, text, locator));
        }

        public override string ToString()
        {
            var text = new StringBuilder();
            text.Append(Type).Append(": ").Append(Text);
            return (text.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return (true);
            
            if (!(obj is ValidationMessage))
                return (false);
            
            var test = (ValidationMessage) obj;
            return (Type.Equals(test.Type) && Text.Equals(test.Text) && Locator.Equals(test.Locator));
        }

        public override int GetHashCode()
        {
            int result = Type.GetHashCode();
            result = 7*result + Text.GetHashCode();
            result = 7*result + Locator.GetHashCode();
            return (result);
        }
    }
}