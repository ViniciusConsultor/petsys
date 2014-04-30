Imports System.Net.Mail
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports System.Net
Imports System.IO

Public Class GerenciadorDeEmail

    Public Shared Sub EnviaEmail(ByVal Assunto As String, _
                                 ByVal Remetente As String, _
                                 ByVal DestinatariosEmCopia As IList(Of String), _
                                 ByVal DestinatariosEmCopiaOculta As IList(Of String), _
                                 ByVal Mensagem As String,
                                 ByVal Anexos As IDictionary(Of String, Stream), _
                                 ByVal Contexto As String, _
                                 ByVal GravaHistorico As Boolean)
        Dim Configuracao As IConfiguracaoDoSistema

        Using ServicoDeConfiguracao As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = ServicoDeConfiguracao.ObtenhaConfiguracaoDoSistema()
        End Using

        Using Servico As IServicoDeEnvioDeEmail = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeEnvioDeEmail)()
            Servico.EnviaEmail(Configuracao, Assunto, Remetente, DestinatariosEmCopia, DestinatariosEmCopiaOculta, Mensagem, Anexos, Contexto, GravaHistorico)
        End Using
    End Sub

End Class
