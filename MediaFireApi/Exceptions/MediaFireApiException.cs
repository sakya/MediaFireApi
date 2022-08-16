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

        public int? ErrorCode { get; private set; }
    }
}