#region usings

using System;

#endregion

namespace DDMSSense.DDMS
{
    /// <summary>
    ///     Exception class for attempts to use a version of DDMS which is not supported by this library.
    
    
    /// </summary>
    public class UnsupportedVersionException : Exception
    {
        /// <see cref="Exception#Exception(String)"></see>
        public UnsupportedVersionException(string version) : base("DDMS Version " + version + " is not yet supported.") { }
    }
}