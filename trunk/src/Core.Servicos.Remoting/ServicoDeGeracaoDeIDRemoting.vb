Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local

Public Class ServicoDeGeracaoDeIDRemoting
    Inherits ServicoRemoto
    Implements IServicoDeGeracaoDeID

    Private _ServicoLocal As ServicoDeGeracaoDeIDLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeGeracaoDeIDLocal(Credencial)
    End Sub

    Public Sub ArmazeneNumeroHigh(ByVal NumeroHigh As Integer) Implements IServicoDeGeracaoDeID.ArmazeneNumeroHigh
        _ServicoLocal.ArmazeneNumeroHigh(NumeroHigh)
    End Sub

    Public Function ObtenhaNumeroHigh() As Integer Implements IServicoDeGeracaoDeID.ObtenhaNumeroHigh
        Return _ServicoLocal.ObtenhaNumeroHigh
    End Function

End Class
