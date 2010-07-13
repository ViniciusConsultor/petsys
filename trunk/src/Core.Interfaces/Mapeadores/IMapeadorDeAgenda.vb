Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAgenda

        Sub Insira(ByVal Agenda As IAgenda)
        Sub Modifique(ByVal Agenda As IAgenda)
        Sub Remova(ByVal ID As Long)

        Function ObtenhaAgenda(ByVal Pessoa As IPessoa) As IAgenda
        Function ObtenhaAgenda(ByVal IDPessoa As Long) As IAgenda

        Sub InsiraCompromisso(ByVal Compromisso As ICompromisso)
        Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso)
        Sub RemovaCompromisso(ByVal ID As Long)
        Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso
        Function ObtenhaCompromissos(ByVal IDProprieatario As Long) As IList(Of ICompromisso)

    End Interface

End Namespace