namespace TPRental4You.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public byte[]? Avatar { get; set; }
        public string UserId { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string UserName { get; set; }
        public bool Ativo { get; set; }
        public bool Elimina { get; set; }
        public bool Desativa { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
