using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> GetAllExceptions(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx;
            }
        }

        public static IEnumerable<string> GetAllExceptionAsString(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx.ToString();
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx.ToString();
            }
        }

        public static IEnumerable<string> GetAllExceptionMessages(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx.Message;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx.Message;
            }
        }

        public static string ToMessageAndCompleteStacktrace(this Exception exception)
        {
            var e = exception;
            var s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine("Exception type: " + e.GetType().FullName);
                s.AppendLine("Message       : " + e.Message);
                s.AppendLine("Stacktrace:");
                s.AppendLine(e.StackTrace);
                s.AppendLine();
                e = e.InnerException;
            }
            return s.ToString();
        }

        public static string GetAllMessages(this Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            var sb = new StringBuilder();
            while (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    if (sb.Length > 0)
                        sb.Append(" ");
                    sb.Append(ex.Message);
                }
                ex = ex.InnerException;
            }
            return sb.ToString();
        }

        public static void ExecuteFunction(Action method, string errorMessage)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

    }
}
