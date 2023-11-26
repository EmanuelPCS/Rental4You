using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float? Avaliacao { get; set; }
        public bool Ativa { get; set; }
        //Adicionar logo
        public ICollection<Veiculo> Veiculos { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public ICollection<Lucro> Lucro { get; set; }
    }
}
