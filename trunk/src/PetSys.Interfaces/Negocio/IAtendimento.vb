Namespace Negocio

    Public Interface IAtendimento

        Property ID() As Nullable(Of Long)
        Property Animal() As IAnimal
        Property Veterinario() As IVeterinario
        Property DataEHoraDoAtendimento() As Date
        Property DataEHoraDoRetorno() As Nullable(Of Date)
        Property Vacinas() As IList(Of IVacina)
        Property Vermifugos() As IList(Of IVermifugo)
        Property Queixa() As String
        Property SinaisClinicos() As String
        Property Prognostico() As String
        Property Tratamento() As String
        Property Peso() As Nullable(Of Double)

    End Interface

End Namespace