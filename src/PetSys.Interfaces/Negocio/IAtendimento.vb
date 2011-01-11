Namespace Negocio

    Public Interface IAtendimento

        Property ID() As Nullable(Of Long)
        Property Animal() As IAnimal
        Property Veterinario() As IVeterinario
        Property DataEHoraDoAtendimento() As Date
        Property Vacinas() As IList(Of IVacina)

    End Interface

End Namespace