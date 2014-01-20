Imports Core.Interfaces.Servicos
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local

Public Class ServicoDeAutenticacaoRemoting
    Inherits ServicoRemoto
    Implements IServicoDeAutenticacao

    Private _ServicoLocal As ServicoDeAutenticacaoLocal

    Public Function FacaLogon(ByVal LoginInformado As String, ByVal SenhaInformada As String) As IOperador Implements IServicoDeAutenticacao.FacaLogon
        Return _ServicoLocal.FacaLogon(LoginInformado, SenhaInformada)
    End Function

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeAutenticacaoLocal(Credencial)
    End Sub

End Class