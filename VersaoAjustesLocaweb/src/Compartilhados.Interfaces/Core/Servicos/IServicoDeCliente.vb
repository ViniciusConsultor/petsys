Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeCliente
        Inherits IServico

        Function Obtenha(ByVal Pessoa As IPessoa) As ICliente
        Function Obtenha(ByVal ID As Long) As ICliente
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente)
        Sub Inserir(ByVal Cliente As ICliente)
        Sub Modificar(ByVal Cliente As ICliente)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace