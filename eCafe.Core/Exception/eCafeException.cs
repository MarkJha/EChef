using System;

namespace eCafe.Core
{
    [Serializable]
    public class ECafeException : Exception
    {
        public ECafeException(string message)
            : base(message)
        {

        }

        public ECafeException(string message, Exception innerException)
        : base(message, innerException)
        {

        }
    }
}
