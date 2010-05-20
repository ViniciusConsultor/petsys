Imports PetSys.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeAtendimentoDoAnimal
        Inherits IServico

        Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimentoDoAnimal)
        Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimentoDoAnimal
        Sub Insira(ByVal Atendimento As IAtendimentoDoAnimal)
        Sub Modificar(ByVal Atendimento As IAtendimentoDoAnimal)

    End Interface

End Namespace