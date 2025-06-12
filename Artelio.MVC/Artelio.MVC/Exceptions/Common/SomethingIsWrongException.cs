namespace Artelio.MVC.Exceptions.Common
{
    public class SomethingIsWrongException : Exception, IBaseException
    {
        public string ErrorMessage { get; }

        public int StatusCode => StatusCodes.Status400BadRequest;


        public SomethingIsWrongException(string message)
        {
            ErrorMessage = message;
        }
    }
}
