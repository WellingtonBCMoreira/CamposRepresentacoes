namespace CamposRepresentacoes
{
    public class MensagemAlerta
    {
        private static Dictionary<string, string> _mensagens = new Dictionary<string, string>();

        public static void SetMensagem(string chave, string mensagem)
        {
            _mensagens[chave] = mensagem;
        }

        public static string GetMensagem(string chave)
        {
            string mensagem;
            if(_mensagens.TryGetValue(chave, out mensagem))
            {
                _mensagens.Remove(chave);

            }
            return mensagem;
        }
    }
}
