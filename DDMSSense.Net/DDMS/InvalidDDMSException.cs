#region usings

using System;

#endregion

namespace DDMSSense.DDMS
{
    /// <summary>
    ///     Exception class for attempts to generate invalid DDMS components.
    ///     <para>
    ///         The underlying data is stored as a ValidationMessage, which allows locator information to be set on it. Because
    ///         InvalidDDMSExceptions are singular (one is thrown) while validation warnings are gathered from subcomponents
    ///         and
    ///         merged into a master list, we modify the exception itself when adding parent locator information.
    ///     </para>
    ///     <para>
    ///         Since a component is not nested in another component at the time of instantiation, it has no parent when a
    ///         validation exception is thrown. Therefore, the locator info will always consist of the single element whose
    ///         constructor was called.
    ///         @author Brian Uri!
    ///         @since 0.9.b
    ///     </para>
    /// </summary>
    public class InvalidDDMSException : Exception
    {
        private const long SerialVersionUID = -183915550465140589L;
        private readonly ValidationMessage _message;

        /// <see cref="Exception#Exception(String)"></see>
        public InvalidDDMSException(string message) : base(message)
        {
            _message = ValidationMessage.NewError(Message, null);
        }

        /// <see cref="Exception#Exception(Throwable)"></see>
        public InvalidDDMSException(Exception nested) : base(nested.Message)
        {
            _message = ValidationMessage.NewError(Message, null);
        }

        /// <summary>
        ///     Handles nested URISyntaxExceptions
        /// </summary>
        /// <param name="e">	the exception </param>
        public InvalidDDMSException(UriFormatException e) : base("Invalid URI (" + e.Message + ")", e)
        {
            _message = ValidationMessage.NewError(Message, null);
        }

        /// <summary>
        ///     Accessor for the underlying ValidationMessage
        /// </summary>
        private ValidationMessage ValidationMessage
        {
            get { return _message; }
        }

        /// <summary>
        ///     Accessor for the locator
        /// </summary>
        public virtual string Locator
        {
            get { return ValidationMessage.Locator; }
            set { ValidationMessage.Locator = value; }
        }
    }
}