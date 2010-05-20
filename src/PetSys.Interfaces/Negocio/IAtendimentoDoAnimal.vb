Namespace Negocio

    Public Interface IAtendimentoDoAnimal

        Property ID() As Nullable(Of Long)
        Property Animal() As IAnimal
        Property Veterinario() As IVeterinario
        Property DataEHoraDoAtendimento() As Date

    End Interface

End Namespace