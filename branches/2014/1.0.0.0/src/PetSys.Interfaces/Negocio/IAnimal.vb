Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface IAnimal

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property DataDeNascimento() As Nullable(Of Date)
        Property Sexo() As SexoDoAnimal
        Property Raca() As String
        Property Foto() As String
        ReadOnly Property Idade() As String
        Property Proprietario() As IProprietarioDeAnimal
        Property Especie() As Especie

    End Interface

End Namespace