namespace Artelio.MVC.Exceptions.Common
{
    public class FileNotSuitableException : Exception, IBaseException
    {
        public string ErrorMessage { get; }

        public int StatusCode => StatusCodes.Status400BadRequest;


        public FileNotSuitableException(string message)
        {
            ErrorMessage = message;
        }
    }
}
