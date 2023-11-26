using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public enum Marcas
    {
        Audi,
        BMW,
        Citroën,
        Ferrari,
        Fiat,
        Ford,
        Honda,
        Hyundai,
        Jaguar,
        Jeepe,
        Lamborghini,
        Mercedes,
        Opel,
        Peugeot,
        Porshe,
        Renault,
        Seat,
        Toyota,
        Volkswagen
    }

    public enum tipoMudancas
    {
        Manuais, Automáticas
    }

    public class Veiculo
    {
        public int Id { get; set; }
        [Display(Name = "Matricula", Prompt = "Introduza a matricula do carro")]
        public string Matricula { get; set; }

        [Display(Name = "Marca", Prompt = "Introduza a marca do carro")]
        public string Marca { get; set; }

        [Display(Name = "Nome", Prompt = "Introduza o nome do veiculo")]
        public string Nome { get; set; }

        [Display(Name = "Km", Prompt = "Introduza o numero de km do carro")]
        public float Km { get; set; }

        [Display(Name = "Preço", Prompt = "Introduza o preço por hora do veiculo")]
        public float Preco { get; set; }

        [Display(Name = "Carro ativo")]
        public bool Ativo { get; set; }

        //[Display(Name = "Descrição", Prompt = "Introduza a descrição do carro")]
        //public string Descricao { get; set; }

        [Display(Name = "Lugares", Prompt = "Introduza o numero de lugares do carro")]
        public int Lugares { get; set; }

        [Display(Name = "Malas", Prompt = "Introduza o numero de malas do carro")]
        public int Malas { get; set; }

        [Display(Name = "Mudanças", Prompt = "Introduza o tipo de mudanças do carro")]
        public string Mudancas { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFim { get; set; }
        public int LocalizacaoId { get; set; }
        public Localizacao localizacao { get; set; }
        public int TiposDeVeiculoId { get; set; }
        public TiposDeVeiculo tiposDeVeiculo { get; set; }
        public int EmpresaId { get; set; }
        public Empresa empresa { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        //Foto
        public byte[]? Foto { get; set; }

        [NotMapped]
        public IFormFile? FotoFile { get; set; }
    }
}
