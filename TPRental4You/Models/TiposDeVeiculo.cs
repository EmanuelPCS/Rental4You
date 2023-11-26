using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public class TiposDeVeiculo
    {
        public int Id { get; set; }

        [Display(Name = "Tipo", Prompt = "Introduza o tipo de veiculo")]
        public string Nome { get; set; }

        /*[Display(Name = "Idade Minima", Prompt = "Introduza a idade minima")]
        public string IdadeMinima { get; set; }
        public bool Disponivel { get; set; }*/
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
