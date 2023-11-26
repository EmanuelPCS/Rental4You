// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TPRental4You.Models;

namespace TPRental4You.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            [Display(Name = "O meu Avatar")]
            public byte[]? Avatar { get; set; }
            public IFormFile AvatarFile { get; set; }

            [Display(Name = "Primeiro Nome")]
            public string PrimeiroNome { get; set; }

            [Display(Name = "Último Nome")]
            public string UltimoNome { get; set; }

            [Display(Name = "NIF")]
            public int NIF { get; set; }

            [Display(Name = "Data de Nascimento"), DataType(DataType.Date)]
            public DateTime DataNascimento { get; set; }

            public DateTime DataDeRegisto { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);

            
            Email = email;

            Input = new InputModel
            {
                DataNascimento = user.DataNascimento,
                PrimeiroNome = user.PrimeiroNome,
                UltimoNome = user.UltimoNome,
                NIF = user.NIF,
                Avatar = user.Avatar,
                DataDeRegisto = DateTime.Now
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public bool isValidFileType(string filename)
        {
            List<string> fileExtensions = new List<string>() { "png", "jpg", "jpeg" };
            List<string> filenameSeparated = filename.Split('.').Reverse().ToList<string>();

            foreach (var extension in fileExtensions)
                if (extension.Equals(filenameSeparated[0]))
                    return true;

            return false;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.AvatarFile != null)
            {
                if (Input.AvatarFile.Length > (200 * 1024))
                {
                    StatusMessage = "Error: Ficheiro demasiado grande";
                    return RedirectToPage();
                }
                // método a implementar – verifica se a extensão é .png,.jpg,.jpeg
                if (!isValidFileType(Input.AvatarFile.FileName))
                {
                    StatusMessage = "Error: Ficheiro não suportado";
                    return RedirectToPage();
                }
                using (var dataStream = new MemoryStream())
                {
                    await Input.AvatarFile.CopyToAsync(dataStream);
                    user.Avatar = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            if (user.PrimeiroNome != Input.PrimeiroNome)
            {
                user.PrimeiroNome = Input.PrimeiroNome;
                await _userManager.UpdateAsync(user);
            }


            if (user.UltimoNome != Input.UltimoNome)
            {
                user.UltimoNome = Input.UltimoNome;
                await _userManager.UpdateAsync(user);
            }

            if (user.DataNascimento != Input.DataNascimento)
            {
                user.DataNascimento = Input.DataNascimento;
                await _userManager.UpdateAsync(user);
            }

            if (user.NIF != Input.NIF)
            {
                user.NIF = Input.NIF;
                await _userManager.UpdateAsync(user);
            }

            //user.DataDeRegisto = DateTime.Now;

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
