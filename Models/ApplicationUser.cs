using Microsoft.AspNetCore.Identity;

namespace CamposRepresentacoes.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
