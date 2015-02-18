using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SiteCHM.Utilidades
{
    public class EnviadorDeEmail
    {
        public static void EnvieEmail(string hostDeSaida, int porta, bool requerAutenticacao, string usuarioDeAutenticacaoDoServidorDeSaida, string senhaUsuarioServidorDeSaida,
                                      bool habilitarSsl, string assunto,  string mensagem, string emailDe, string nomeDe, IList<string>para)
        {
            using (var clienteDeEmail = new SmtpClient(hostDeSaida,porta))
            {
                if (requerAutenticacao)
                    clienteDeEmail.Credentials = new NetworkCredential(usuarioDeAutenticacaoDoServidorDeSaida,
                                                                       senhaUsuarioServidorDeSaida);

                clienteDeEmail.EnableSsl = habilitarSsl;

                var mensagemDeEmail = new MailMessage();

                mensagemDeEmail.From = new MailAddress(emailDe.ToLower(), nomeDe);
                
                mensagemDeEmail.IsBodyHtml = true;
                mensagemDeEmail.Subject = assunto;
                mensagemDeEmail.Body = mensagem;

                foreach (var email in para)
                    mensagemDeEmail.To.Add(new MailAddress(email));

            }
        }
    }
}