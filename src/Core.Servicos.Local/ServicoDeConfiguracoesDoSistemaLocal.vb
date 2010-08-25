Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos

Public Class ServicoDeConfiguracoesDoSistemaLocal
    Inherits Servico
    Implements IServicoDeConfiguracoesDoSistema

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub


    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IServicoDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Dim ConfiguracaoDeEmail As IConfiguracaoDeEmailDoSistema
        Dim ConfiguracaoGeral As IConfiguracaoDoSistema

        ConfiguracaoGeral = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDoSistema)()

        ConfiguracaoGeral.NotificarErrosAutomaticamente = True

        ConfiguracaoDeEmail = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeEmailDoSistema)()
        ConfiguracaoDeEmail.EmailRemetente = "hermes@lggo.com.br"
        ConfiguracaoDeEmail.HabilitarSSL = False
        ConfiguracaoDeEmail.Porta = 25
        ConfiguracaoDeEmail.RequerAutenticacao = True
        ConfiguracaoDeEmail.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida = "53841"
        ConfiguracaoDeEmail.UsuarioDeAutenticacaoDoServidorDeSaida = "hermes@lggo.com.br"
        ConfiguracaoDeEmail.TipoDoServidor = TipoDeServidorDeEmail.SMTP
        ConfiguracaoDeEmail.ServidorDeSaidaDeEmail = "mail.lggo.com.br"

        ConfiguracaoGeral.ConfiguracaoDeEmailDoSistema = ConfiguracaoDeEmail
        Return ConfiguracaoGeral
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IServicoDeConfiguracoesDoSistema.Salve
    End Sub

End Class