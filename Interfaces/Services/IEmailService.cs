namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, string nomeCliente,byte[] attachment = null);
    }
}
