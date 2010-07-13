Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeAgenda
        Inherits IServico

        'Cadastro de agenda
        Sub Insira(ByVal Agenda As IAgenda)
        Sub Modifique(ByVal Agenda As IAgenda)
        Sub Remova(ByVal ID As Long)
        Function ObtenhaAgenda(ByVal Pessoa As IPessoa) As IAgenda
        Function ObtenhaAgenda(ByVal IDPessoa As Long) As IAgenda

        'Compromissos
        Sub InsiraCompromisso(ByVal Compromisso As ICompromisso)
        Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso)
        Sub RemovaCompromisso(ByVal ID As Long)
        Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso
        Function ObtenhaCompromissos(ByVal IDProprietario As Long) As IList(Of ICompromisso)

    End Interface

End Namespace