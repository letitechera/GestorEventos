using System;

namespace GestorEventos.Models.Responses
{
    public class ResetPasswordResult
    {
        public bool Success { get; set; }
        public ResetPasswordError? ErrorType { get; set; }
        public Exception ErrorException { get; set; }

        public ResetPasswordResult(bool success, ResetPasswordError? eType = null, Exception e = null)
        {
            Success = success;
            ErrorType = eType;
            ErrorException = e;
        }
    }

    public enum ResetPasswordError
    {
        InvalidEmail,
        Exception
    }
}
