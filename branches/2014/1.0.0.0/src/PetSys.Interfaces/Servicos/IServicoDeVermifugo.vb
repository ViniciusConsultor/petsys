Imports PetSys.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeVermifugo
        Inherits IServico

        Function ObtenhaVermifugosDoAnimal(ByVal IdAnimal As Long) As IList(Of IVermifugo)
        Function ObtenhaVermifugo(ByVal ID As Long) As IVermifugo
        Function ObtenhaVermifugos(ByVal IDs As IList(Of Long)) As IList(Of IVermifugo)
        Sub Inserir(ByRef Vermifugo As IVermifugo)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace