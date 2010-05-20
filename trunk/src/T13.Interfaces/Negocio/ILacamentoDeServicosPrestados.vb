Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface ILacamentoDeServicosPrestados
        Inherits ICloneable

        Sub AdicionaItemDeLancamento(ByVal Item As IItemDeLancamento)
        Function ObtenhaItensDeLancamento() As IList(Of IItemDeLancamento)
        Function ObtenhaTotalDosItensLancados() As Double
        Function ObtenhaValorDoISSQN() As Double
        Property DataDeLancamento() As Date
        Property NaturezaDaOperacao() As String
        Property Aliquota() As String
        Property Observacoes() As String
        Property Cliente() As ICliente
        Property ID() As Nullable(Of Long)
        Property Numero() As Nullable(Of Long)

    End Interface

End Namespace