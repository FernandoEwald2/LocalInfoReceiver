namespace Aplicacao.Exceptions
{
    public class LocalException : Exception
    {       
        public ExceptionMessage Exception { get; set; }
        public LocalException(ExceptionEnum exceptionType, string message) : base(message)
        {
            this.Exception = new ExceptionMessage()
            {
                Codigo = ((int)exceptionType).ToString(),
                Mensagem = message
            };
        }

        public LocalException(string message) : base(message)
        {
            this.Exception = new ExceptionMessage()
            {
                Codigo = ((int)ExceptionEnum.InternalServerError).ToString(),
                Mensagem = message
            };
        }
        public LocalException(string message, Exception innerException) : base(message, innerException)
        {
            this.Exception = new ExceptionMessage()
            {
                Codigo = innerException.GetType().ToString(),
                Mensagem = message
            };
        }
    }
}
