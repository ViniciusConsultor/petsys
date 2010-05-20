Imports Compartilhados.Interfaces.Core.Negocio
Imports PetSys.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeVeterinario

        Function Obtenha(ByVal Pessoa As IPessoa) As IVeterinario
        Sub Inserir(ByVal Veterinario As IVeterinario)
        Sub Modificar(ByVal Veterinario As IVeterinario)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace