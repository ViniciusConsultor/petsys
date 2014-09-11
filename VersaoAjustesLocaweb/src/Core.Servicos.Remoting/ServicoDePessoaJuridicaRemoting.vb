Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDePessoaJuridicaRemoting
    Inherits ServicoRemoto
    Implements IServicoDePessoaJuridica

    Private _ServicoLocal As ServicoDePessoaJuridicaLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDePessoaJuridicaLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Pessoa As IPessoaJuridica) Implements IServicoDePessoaJuridica.Inserir
        _ServicoLocal.Inserir(Pessoa)
    End Sub

    Public Sub Modificar(ByVal Pessoa As IPessoaJuridica) Implements IServicoDePessoaJuridica.Modificar
        _ServicoLocal.Modificar(Pessoa)
    End Sub

    Public Function ObtenhaPessoa(ByVal ID As Long) As IPessoaJuridica Implements IServicoDePessoaJuridica.ObtenhaPessoa
        Return _ServicoLocal.ObtenhaPessoa(ID)
    End Function

    Public Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaJuridica) Implements IServicoDePessoaJuridica.ObtenhaPessoasPorNomeComoFiltro
        Return _ServicoLocal.ObtenhaPessoasPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDePessoaJuridica.Remover
        _ServicoLocal.Remover(ID)
    End Sub

End Class