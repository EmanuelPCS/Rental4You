using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public class Localizacao
    {
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Localização")]
        public string Nome { get; set; }
        public string InfoAdicional { get; set; }
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
