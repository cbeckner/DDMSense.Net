#region usings

using System.Collections.Generic;
using DDMSSense.DDMS.Extensible;

#endregion

namespace DDMSSense.DDMS
{
    /// <summary>
    ///     Identifying interface for an entity element (person, organization, service, unknown) which may be used to fulfill
    ///     a producer role (creator, contributor, pointOfContact, publisher) or a tasking role (addressee, requesterInfo,
    ///     recordKeeper).
    
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