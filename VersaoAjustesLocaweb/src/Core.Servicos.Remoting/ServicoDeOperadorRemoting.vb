Imports Core.Servicos.Local
Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Public Class ServicoDeOperadorRemoting
    Inherits ServicoRemoto
    Implements IServicoDeOperador

    Private _ServicoLocal As ServicoDeOperadorLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeOperadorLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Operador As IOperador, ByVal Senha As ISenha) Implements IServicoDeOperador.Inserir
        _ServicoLocal.Inserir(Operador, Senha)
    End Sub

    Public Sub Modificar(ByVal Operador As IOperador) Implements IServicoDeOperador.Modificar
        _ServicoLocal.Modificar(Operador)
    End Sub

    Public Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador Implements IServicoDeOperador.ObtenhaOperador
        Return _ServicoLocal.ObtenhaOperador(Pessoa)
    End Function

    Public Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador Implements IServicoDeOperador.ObtenhaOperadorPorLogin
        Return _ServicoLocal.ObtenhaOperadorPorLogin(Login)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeOperador.Remover
        _ServicoLocal.Remover(ID)
    End Sub

    Public Function ObtenhaOperadores(ByVal Nome As String, ByVal Quantidade As Integer) As IList(Of IOperador) Implements IServicoDeOperador.ObtenhaOperadores
        Return _ServicoLocal.ObtenhaOperadores(Nome, Quantidade)
    End Function

End Class