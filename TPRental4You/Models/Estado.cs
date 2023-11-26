using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public enum Tipo
    {
        Levantamento,Entrega
    }
    public class Estado
    {
        public int Id { get; set; }
        public Tipo Tipo { get; set; }
        public float Km { get; set; }
        public bool DanosDoVeiculo { get; set; }
        public string Observacoes { get; set; }
        public int ReservaId { get; set; }
        public Reserva reserva { get; set; }
        public string FuncionarioId { get; set; }
        public ApplicationUser funcionario { get; set; }
        //Foto
        //public byte[] Foto { get; set; }
    }
}
