using TPRental4You.Models.ViewModels;
using TPRental4You.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TPRental4You.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace TPRental4You.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UtilizadoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
       RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userAtualId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFuncionario = await _context.Users.Include(u => u.Empresa).Include(u => u.Reservas)
                .FirstOrDefaultAsync(u => u.Id == userAtualId);

            if (User.IsInRole("Gestor"))
            {
                users = await _userManager.Users.Where(u => u.EmpresaId == userFuncionario.EmpresaId).ToListAsync();
            }

            List<UserRolesViewModel> userRolesManagerViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                UserRolesViewModel userRolesViewModel = new UserRolesViewModel();

                userRolesViewModel.Avatar = user.Avatar;
                userRolesViewModel.UserId = user.Id;
                userRolesViewModel.UserName = user.UserName;
                userRolesViewModel.PrimeiroNome = user.PrimeiroNome;
                userRolesViewModel.UltimoNome = user.UltimoNome;
                userRolesViewModel.Ativo = user.EmailConfirmed;
                userRolesViewModel.Elimina = true;
                userRolesViewModel.Desativa = true;

                if (user.Id.Equals(userAtualId))
                {
                    userRolesViewModel.Elimina = false;
                    userRolesViewModel.Desativa = false;
                }
                else
                {
                    foreach (var item in _context.Estados.ToList())
                    {
                        if (item.funcionario != null)
                        {
                            if (item.funcionario.Email.Equals(user.Email))
                                userRolesViewModel.Elimina = false;
                        }
                    }

                    if (_context.Reservas.Where(r => r.cliente.Email.Equals(user.Email)).Count() != 0)
                        userRolesViewModel.Elimina = false;
                }

                userRolesViewModel.Roles = await _userManager.GetRolesAsync(user);

                userRolesManagerViewModel.Add(userRolesViewModel);
            }
            return View(userRolesManagerViewModel);
        }

        // GET: Empresas/Create
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Create()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFuncionario = await _context.Users.Include(u => u.Empresa).Include(u => u.Reservas)
                .FirstOrDefaultAsync(u => u.Id == id);

            //ApplicationUser user = new ApplicationUser();
            //user.EmpresaId = userFuncionario.EmpresaId;

            ViewData["EmpresaId"] = userFuncionario.EmpresaId;

            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Create([Bind("Email,PasswordHash,PrimeiroNome,UltimoNome" +
            ",DataNascimento,NIF,EmpresaId")] ApplicationUser applicationUser, int Role)
        {
            ModelState.Remove(nameof(applicationUser.Empresa));
            ModelState.Remove(nameof(applicationUser.Reservas));
            ModelState.Remove(nameof(applicationUser.Avaliacoes));

            var user = await _userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {

                if (ModelState.IsValid)
                {
                    applicationUser.EmailConfirmed = true;
                    applicationUser.PhoneNumberConfirmed = true;
                    applicationUser.UserName = applicationUser.Email;

                    await _userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);
                    if (Role == 0)
                    {
                        await _userManager.AddToRoleAsync(applicationUser,
                        Roles.Funcionario.ToString());
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(applicationUser,
                        Roles.Gestor.ToString());
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["EmpresaId"] = applicationUser.EmpresaId;

            return View(applicationUser);
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Edit(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            return View(user);
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public bool isValidFileType(string filename)
        {
            List<string> fileExtensions = new List<string>() { "png", "jpg", "jpeg" };
            List<string> filenameSeparated = filename.Split('.').Reverse().ToList<string>();

            foreach (var extension in fileExtensions)
                if (extension.Equals(filenameSeparated[0]))
                    return true;

            return false;
        }

        [HttpPost]
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AvatarFile,Email,PrimeiroNome,UltimoNome,NIF,DataNascimento")]
            ApplicationUser? applicationUser)
        {
            var user = await _userManager.FindByIdAsync(id);

            ModelState.Remove(nameof(applicationUser.Empresa));
            ModelState.Remove(nameof(applicationUser.Reservas));
            ModelState.Remove(nameof(applicationUser.Avaliacoes));
            if (id != applicationUser.Id && user != null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (applicationUser.AvatarFile != null)
                {
                    if (applicationUser.AvatarFile.Length > (200 * 1024))
                    {
                        return NotFound();
                    }
                    // método a implementar – verifica se a extensão é .png,.jpg,.jpeg
                    if (!isValidFileType(applicationUser.AvatarFile.FileName))
                    {
                        return NotFound();
                    }
                    using (var dataStream = new MemoryStream())
                    {
                        await applicationUser.AvatarFile.CopyToAsync(dataStream);
                        user.Avatar = dataStream.ToArray();
                    }
                    await _userManager.UpdateAsync(user);
                }


                user.Email = applicationUser.Email;
                user.UserName = applicationUser.Email;
                user.PrimeiroNome = applicationUser.PrimeiroNome;
                user.UltimoNome = applicationUser.UltimoNome;
                user.NIF = applicationUser.NIF;
                user.DataNascimento = applicationUser.DataNascimento;

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Ativar(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.EmailConfirmed = true;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Desativar(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.EmailConfirmed = false;

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!user.Id.Equals(id))
            {
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Eliminar(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!userId.Equals(id))
            {

                foreach (var item in _context.Estados.ToList())
                {
                    if (item.funcionario != null)
                    {
                        if (item.funcionario.Id.Equals(userId))
                            return RedirectToAction(nameof(Index));
                    }
                }

                if (_context.Reservas.Where(r => r.cliente.Email.Equals(user.Email)).Count() != 0)
                    return RedirectToAction(nameof(Index));

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Gestor,Administrador")]
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Details(string UserId)
        {

            var user = await _userManager.FindByIdAsync(UserId);

            return View(user);
        }
    }
}
