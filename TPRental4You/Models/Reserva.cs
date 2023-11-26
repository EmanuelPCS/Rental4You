using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public enum estadoReserva
    {
        Por_Confirmar, Confirmada, Rejeitada, Finalizada, Cancelada
    }

    public enum situacaoReserva
    {
        Por_Pagar, Vencido, Cliente_Entregou, Reembolsado
    }
    public class Reserva
    {
        public int Id { get; set; }

        [Display(Name = "Estado da Reserva", Prompt = "Confirme a reserva")]
        //public estadoReserva? Confirmada { get; set; }
        public estadoReserva? EstadoReserva { get; set; }
        public float Preco { get; set; }
        public DateTime DataDeLevantamento { get; set; }
        public DateTime DataDeEntrega { get; set; }
        public DateTime DataDeReserva { get; set; }
        public string ClienteId { get; set; }
        public ApplicationUser cliente { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo veiculo { get; set; }
        public int EmpresaId { get; set; }
        public Empresa empresa { get; set; }
        public ICollection<Estado> estados { get; set; }
        public situacaoReserva? Situacao { get; set; }
        [Range(0, 5, ErrorMessage = "Mínimo: 0 , máximo: 5")]
        public float? Classificacao { get; set; }
        public Lucro? Lucro { get; set; }
        public Avaliacao? Avaliacao { get; set; }
    }
}
