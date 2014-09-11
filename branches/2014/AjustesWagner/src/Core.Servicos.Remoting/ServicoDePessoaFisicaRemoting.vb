Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDePessoaFisicaRemoting
    Inherits ServicoRemoto
    Implements IServicoDePessoaFisica

    Private _ServicoLocal As ServicoDePessoaFisicaLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDePessoaFisicaLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Inserir
        _ServicoLocal.Inserir(Pessoa)
    End Sub

    Public Sub Modificar(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Modificar
        _ServicoLocal.Modificar(Pessoa)
    End Sub

    Public Function ObtenhaPessoa(ByVal ID As Long) As IPessoaFisica Implements IServicoDePessoaFisica.ObtenhaPessoa
        Return _ServicoLocal.ObtenhaPessoa(ID)
    End Function

    Public Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica) Implements IServicoDePessoaFisica.ObtenhaPessoasPorNomeComoFiltro
        Return _ServicoLocal.ObtenhaPessoasPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDePessoaFisica.Remover
        _ServicoLocal.Remover(ID)
    End Sub

End Class