namespace TPRental4You.Models
{
    public class Lucro
    {
        public int Id { get; set; }
        public float valor { get; set; }
        public int ReservaId { get; set; }
        public Reserva reserva { get; set; }
        public int EmpresaId { get; set; }
        public Empresa empresa { get; set; }
        public DateTime diaHora { get; set; }

    }
}
