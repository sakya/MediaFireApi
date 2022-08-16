using System;

namespace MediaFireApi.Exceptions
{
    public class MediaFireApiException : Exception
    {
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