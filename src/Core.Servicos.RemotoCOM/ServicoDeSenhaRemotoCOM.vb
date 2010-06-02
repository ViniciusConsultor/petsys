Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados
Imports Core.Interfaces.Negocio

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeSenhaRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeSenha, IServicoRemoto

    Private ServicoLocal As ServicoDeSenhaLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeSenhaLocal(Credencial)
    End Sub

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal SenhaAntigaInformada As ISenha, _
                      ByVal NovaSenha As ISenha, _
                      ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        ServicoLocal.Altere(IDOperador, SenhaAntigaInformada, NovaSenha, ConfirmacaoNovaSenha)
    End Sub

    Public Function ObtenhaSenhaDoOperador(ByVal Operador As IOperador) As ISenha Implements IServicoDeSenha.ObtenhaSenhaDoOperador
        Return ServicoLocal.ObtenhaSenhaDoOperador(Operador)
    End Function

End Class