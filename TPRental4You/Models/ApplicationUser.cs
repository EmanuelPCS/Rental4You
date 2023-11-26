using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TPRental4You.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "O meu Avatar")]
        public byte[]? Avatar { get; set; }
        [NotMapped]
        public IFormFile? AvatarFile { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        [PersonalData]
        public DateTime DataNascimento { get; set; }
        [PersonalData]
        public int NIF { get; set; }
        public DateTime DataDeRegisto { get; set; }
        public int? EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
    }
}
