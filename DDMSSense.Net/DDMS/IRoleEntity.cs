#region usings

using System.Collections.Generic;
using DDMSSense.DDMS.Extensible;

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
    ///     Identifying interface for an entity element (person, organization, service, unknown) which may be used to fulfill
    ///     a producer role (creator, contributor, pointOfContact, publisher) or a tasking role (addressee, requesterInfo,
    ///     recordKeeper).
    ///     @author Brian Uri!
    ///     @since 2.0.0
    /// </summary>
    public interface IRoleEntity : IDDMSComponent
    {
        /// <summary>
        ///     Accessor for the names of the entity (1 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        List<string> Names { get; }

        /// <summary>
        ///     Accessor for the phone numbers of the entity (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        List<string> Phones { get; }

        /// <summary>
        ///     Accessor for the emails of the entity (0 to many).
        /// </summary>
        /// <returns> unmodifiable List </returns>
        List<string> Emails { get; }

        /// <summary>
        ///     Accessor for any extensible attributes on the producer
        /// </summary>
        /// <returns> the attributes </returns>
        ExtensibleAttributes ExtensibleAttributes { get; }
    }
}