Namespace Core.Negocio

    Public Interface IBanco
        Inherits IPapelPessoa

        Property Numero() As Short
        ReadOnly Property Agencias() As IList(Of IAgencia)

    End Interface

End Namespace