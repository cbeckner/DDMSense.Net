#region usings

using System;

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