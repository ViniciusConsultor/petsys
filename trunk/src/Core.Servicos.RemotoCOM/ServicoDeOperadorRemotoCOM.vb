Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Core.Interfaces.Servicos

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeOperadorRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeOperador, IServicoRemoto

    Private ServicoLocal As ServicoDeOperadorLocal

    Public Sub Modificar(ByVal Operador As IOperador) Implements IServicoDeOperador.Modificar
        ServicoLocal.Modificar(Operador)
    End Sub

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeOperador.Remover
        ServicoLocal.Remover(ID)
    End Sub

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeOperadorLocal(Credencial)
    End Sub

    Public Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador Implements IServicoDeOperador.ObtenhaOperadorPorLogin
        Return ServicoLocal.ObtenhaOperadorPorLogin(Login)
    End Function

    Public Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador Implements IServicoDeOperador.ObtenhaOperador
        Return ServicoLocal.ObtenhaOperador(Pessoa)
    End Function

    Public Sub Inserir(ByVal Operador As Compartilhados.Interfaces.Core.Negocio.IOperador, ByVal Senha As Interfaces.Negocio.ISenha) Implements Interfaces.Servicos.IServicoDeOperador.Inserir
        ServicoLocal.Inserir(Operador, Senha)
    End Sub
End Class