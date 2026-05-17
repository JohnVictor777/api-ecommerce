namespace ApiEcommerce.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        // Exceção personalizada para indicar que um recurso não foi encontrado, resultando em uma resposta HTTP 404 Not Found.
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
