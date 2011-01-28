Imports PetSys.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeVacina

        Function ObtenhaVacinasDoAnimal(ByVal IdAnimal As Long) As IList(Of IVacina)
        Function ObtenhaVacina(ByVal ID As Long) As IVacina
        Function ObtenhaVacinas(ByVal IDs As IList(Of Long)) As IList(Of IVacina)
        Sub Inserir(ByRef Vacina As IVacina)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace