Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Fabricas

Public Class MapeadorDeAtendimentoDoAnimal
    Implements IMapeadorDeAtendimentoDoAnimal

    Public Sub Insira(ByVal Atendimento As IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.Insira

    End Sub

    Public Sub Modificar(ByVal Atendimento As IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.Modificar

    End Sub

    Public Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimentoDoAnimal Implements IMapeadorDeAtendimentoDoAnimal.ObtenhaAtendimento
        Return Nothing
    End Function

    Public Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.ObtenhaAtendimentos
        Return Nothing
    End Function

End Class