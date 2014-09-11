Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeConfiguracoesDoSistemaRemoting
    Inherits ServicoRemoto
    Implements IServicoDeConfiguracoesDoSistema

    Private _ServicoLocal As ServicoDeConfiguracoesDoSistemaLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeConfiguracoesDoSistemaLocal(Credencial)
    End Sub

    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IServicoDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Return _ServicoLocal.ObtenhaConfiguracaoDoSistema
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IServicoDeConfiguracoesDoSistema.Salve
        _ServicoLocal.Salve(Configuracao)
    End Sub

End Class