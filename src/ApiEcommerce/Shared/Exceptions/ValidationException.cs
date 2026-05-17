namespace ApiEcommerce.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        // Exceção personalizada para indicar que houve um erro de validação, resultando em uma resposta HTTP 400 Bad Request.
        public ValidationException(string message) : base(message)
        {
        }
    }
}
