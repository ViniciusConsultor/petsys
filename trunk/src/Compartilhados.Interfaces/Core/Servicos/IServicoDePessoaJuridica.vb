Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDePessoaJuridica
        Inherits IServico

        Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaJuridica)
        Function ObtenhaPessoa(ByVal ID As Long) As IPessoaJuridica
        Sub Inserir(ByVal Pessoa As IPessoaJuridica)
        Sub Modificar(ByVal Pessoa As IPessoaJuridica)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace