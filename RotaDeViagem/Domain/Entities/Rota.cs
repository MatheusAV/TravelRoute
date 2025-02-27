namespace RotaDeViagem.Domain.Entities
{
    /// <summary>
    /// Modelo de Rota
    /// </summary>
    public class Rota
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Custo { get; set; }
    }
}
