namespace RotaDeViagem.Domain.MensageResponse
{
    /// <summary>
    /// Representa uma resposta padrão de serviço para operações da API.
    /// </summary>
    /// <typeparam name="T">O tipo de dado da propriedade <see cref="Data"/>.</typeparam>
    public class ServiceResponse<T>
    {        
        public T Data { get; set; }
       
        public bool Success { get; set; }
        
        public string Message { get; set; }
    }
}
