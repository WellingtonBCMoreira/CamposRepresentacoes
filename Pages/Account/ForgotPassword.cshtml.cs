using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Interfaces.Services;

namespace CamposRepresentacoes.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Não revelar que o usuário não existe ou não está confirmado
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // Gerar token de redefinição de senha e enviar por e-mail
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Autenticacao/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Account", token, email = Input.Email },
                    protocol: Request.Scheme);

                //Enviar e-mail com o link de redefinição de senha(implemente o envio de e - mail no seu serviço de e - mail)
                 await _emailService.SendEmail(
                    Input.Email,
                    "Redefinir Senha",
                    $"Por favor redefina sua senha <a href='{callbackUrl}'>clicando aqui</a>.", user.UserName);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
