Imports PetSys.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeVermifugos

        Function ObtenhaVermifugosDoAnimal(ByVal IdAnimal As Long) As IList(Of IVermifugo)
        Function ObtenhaVermifugo(ByVal ID As Long) As IVermifugo
        Function ObtenhaVermifugos(ByVal IDs As IList(Of Long)) As IList(Of IVermifugo)
        Sub Inserir(ByRef Vermifugo As IVermifugo)
        Sub Modificar(ByVal Vermifugo As IVermifugo)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace