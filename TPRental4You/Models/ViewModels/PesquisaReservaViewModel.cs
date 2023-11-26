using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models.ViewModels
{
    public class PesquisaReservaViewModel
    {
        public DateTime DataDeLevantamento { get; set; }
        public DateTime DataDeEntrega { get; set; }
        public int TiposDeVeiculoId { get; set; }
        public int VeiculoId { get; set; }
        public string ClienteId { get; set; }
        public int EstadoReserva { get; set; }
    }
}
