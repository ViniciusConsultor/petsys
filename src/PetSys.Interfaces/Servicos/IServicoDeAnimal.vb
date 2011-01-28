Imports Compartilhados.Interfaces.Core.Negocio
Imports PetSys.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeAnimal
        Inherits IServico

        Function ObtenhaAnimaisPorNomeComoFiltro(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IAnimal)
        Function ObtenhaAnimal(ByVal ID As Long) As IAnimal
        Sub Inserir(ByVal Animal As IAnimal)
        Sub Modificar(ByVal Animal As IAnimal)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace