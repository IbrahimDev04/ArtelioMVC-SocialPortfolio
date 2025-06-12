using Artelio.MVC.Exceptions.Common;

namespace Artelio.MVC.Exceptions.Auth
{
    public class EmailNotConfirmedException : Exception, IBaseException
    {
        public string ErrorMessage { get; }

        public int StatusCode => StatusCodes.Status400BadRequest;


        public EmailNotConfirmedException(string message)
        {
            ErrorMessage = message;
        }
    }
}
