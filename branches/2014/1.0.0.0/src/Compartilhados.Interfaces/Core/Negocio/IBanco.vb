Namespace Core.Negocio

    Public Interface IBanco
        Inherits IPapelPessoa

        Property Numero() As Integer
        ReadOnly Property Agencias() As IList(Of IAgencia)

    End Interface

End Namespace