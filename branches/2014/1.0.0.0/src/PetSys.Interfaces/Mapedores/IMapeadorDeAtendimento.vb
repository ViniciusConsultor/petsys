Imports PetSys.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAtendimento

        Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimento)
        Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimento
        Sub Insira(ByVal Atendimento As IAtendimento)
        Sub Modificar(ByVal Atendimento As IAtendimento)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace