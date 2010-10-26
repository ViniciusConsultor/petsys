Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeClienteRemoting
    Inherits ServicoRemoto
    Implements IServicoDeCliente

    Private _ServicoLocal As ServicoDeClienteLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeClienteLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Cliente As ICliente) Implements IServicoDeCliente.Inserir
        _ServicoLocal.Inserir(Cliente)
    End Sub

    Public Sub Modificar(ByVal Cliente As ICliente) Implements IServicoDeCliente.Modificar
        _ServicoLocal.Modificar(Cliente)
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As ICliente Implements IServicoDeCliente.Obtenha
        Return _ServicoLocal.Obtenha(Pessoa)
    End Function

    Public Function Obtenha(ByVal ID As Long) As ICliente Implements IServicoDeCliente.Obtenha
        Return _ServicoLocal.Obtenha(ID)
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente) Implements IServicoDeCliente.ObtenhaPorNomeComoFiltro
        Return _ServicoLocal.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeCliente.Remover
        _ServicoLocal.Remover(ID)
    End Sub

End Class
