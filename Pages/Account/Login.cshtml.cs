using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _configuration;

        public string Titulo
        {
            get => _configuration["Projeto:Titulo"] ?? "";
        }
        public string Subtitulo
        {
            get => _configuration["Projeto:Subtitulo"] ?? "";
        }
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signInManager.PasswordSignInAsync(Email, Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToPage("/Index"); // Redireciona após o login bem-sucedido
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Trata erro de login
                return Page();
            }
        }
    }
}
