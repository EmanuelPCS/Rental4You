using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models.ViewModels
{
    public class PesquisaEmpresaViewModel
    {
        public List<Veiculo> ListaDeEmpresas { get; set; }
        public int NumResultados { get; set; }
        [Display(Name = "Pesquisa de veiculos", Prompt = "introduza o texto a pesquisar")]
        //public string VeiculoAPesquisar { get; set; }
        public int EmpresaId { get; set; }
        public int Ativa { get; set; }
        public int ordenarEmpresa { get; set; }
        public int ordenarEstado { get; set; }
    }
}
