using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models.ViewModels
{
    public class PesquisaVeiculoViewModel
    {
        public List<Veiculo> ListaDeVeiculos { get; set; }
        public int NumResultados { get; set; }
        [Display(Name = "Pesquisa de veiculos", Prompt = "introduza o texto a pesquisar")]
        //public string VeiculoAPesquisar { get; set; }
        public DateTime DataPesquisaInicial { get; set; }
        public DateTime DataPesquisaFinal { get; set; }
        public int TiposDeVeiculoId { get; set; }
        public int Ativo { get; set; }
        public int LocalizacaoId { get; set; }
        public int EmpresaId { get; set; }
        public int ordenarPreco { get; set; }
        public int ordenarClassificacao { get; set; }
        public int ordenarTipo { get; set; }
        public int ordenarEstado { get; set; }
    }
}
