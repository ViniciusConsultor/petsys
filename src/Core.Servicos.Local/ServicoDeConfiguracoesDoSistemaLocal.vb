Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio

Public Class ServicoDeConfiguracoesDoSistemaLocal
    Inherits Servico
    Implements IServicoDeConfiguracoesDoSistema

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Private Shared Configuracao As IConfiguracaoDoSistema

    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IServicoDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Return Configuracao
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IServicoDeConfiguracoesDoSistema.Salve
        Configuracao = Configuracao
    End Sub

End Class