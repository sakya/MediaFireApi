using System;

namespace MediaFireApi.Exceptions
{
    /// <summary>
    /// MediaFire API exception
    /// </summary>
    public class MediaFireApiException : Exception
    {
        /// <summary>
        /// Create a new <see cref="MediaFireApiException"/>
        /// </summary>
        /// <param name="errorCode">The error code</param>
        /// <param name="message">The error message</param>
        public MediaFireApiException(int? errorCode, string message) :
            base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The error code.
        /// This should be one of <see cref="ErrorCodes"/>
        /// </summary>
        public int? ErrorCode { get; private set; }
    }
}