Imports PetSys.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeVacina
        Inherits IServico

        Function ObtenhaVacinasDoAnimal(ByVal IdAnimal As Long) As IList(Of IVacina)
        Function ObtenhaVacina(ByVal ID As Long) As IVacina
        Function ObtenhaVacinas(ByVal IDs As IList(Of Long)) As IList(Of IVacina)
        Sub Inserir(ByVal Vacina As IVacina)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace