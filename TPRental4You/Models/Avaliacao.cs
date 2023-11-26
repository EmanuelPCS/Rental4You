namespace TPRental4You.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public float classificacao { get; set; }
        public int EmpresaId { get; set; }
        public Empresa empresa { get; set; }
        public string ClienteId { get; set; }
        public int ReservaId { get; set; }
        public Reserva reserva { get; set; }
        public ApplicationUser cliente { get; set; }
    }
}
