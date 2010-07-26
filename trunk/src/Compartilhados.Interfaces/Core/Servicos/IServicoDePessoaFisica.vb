Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDePessoaFisica
        Inherits IServico

        Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica)
        Function ObtenhaPessoa(ByVal ID As Long) As IPessoaFisica
        Sub Inserir(ByVal Pessoa As IPessoaFisica)
        Sub Modificar(ByVal Pessoa As IPessoaFisica)
        Sub Remover(ByVal Pessoa As IPessoaFisica)

    End Interface

End Namespace