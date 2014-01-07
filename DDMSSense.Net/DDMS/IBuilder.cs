namespace DDMSense.DDMS
{
    /// <summary>
    ///     Identifying interface for a mutable Builder of components
    ///     <para>
    ///         The builder should be used when a DDMS record needs to be built up over time, but validation should not occur
    ///         until the end. The commit() method attempts to finalize the immutable object based on the values gathered.
    ///     </para>
    ///     <para>
    ///         The builder approach differs from calling the immutable constructor directly because it treats a Builder
    ///         instance
    ///         with no values provided as "no component" instead of "a component with missing values". For example, calling a
    ///         constructor directly with an empty string for a required parameter might throw an InvalidDDMSException, while
    ///         calling
    ///         commit() on a Builder without setting any values would just return null.
    ///     </para>
    
    ///     @since 1.8.0
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        ///     Checks if any values have been provided for this Builder.
        /// </summary>
        /// <returns> true if every field is empty </returns>
        bool Empty { get; }

        /// <summary>
        ///     Finalizes the data gathered for this builder instance. If no values have been provided, a null instance will be
        ///     returned instead of a possibly invalid one or an empty one.
        /// </summary>
        /// <exception cref="InvalidDDMSException"> if any required information is missing or malformed </exception>
        IDDMSComponent Commit();
    }
}