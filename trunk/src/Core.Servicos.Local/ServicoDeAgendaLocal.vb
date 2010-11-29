Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeAgendaLocal
    Inherits Servico
    Implements IServicoDeAgenda

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long Implements IServicoDeAgenda.InsiraCompromisso
        Dim Mapeador As IMapeadorDeAgenda
        Dim ID As Long

        Compromisso.EstaConsistente()
        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()
        ServerUtils.BeginTransaction()

        Try
            ID = Mapeador.InsiraCompromisso(Compromisso)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return ID
    End Function

    Public Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso) Implements IServicoDeAgenda.ModifiqueCompromisso
        Dim Mapeador As IMapeadorDeAgenda

        Compromisso.EstaConsistente()
        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueCompromisso(Compromisso)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso Implements IServicoDeAgenda.ObtenhaCompromisso
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaCompromisso(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long) As IList(Of ICompromisso) Implements IServicoDeAgenda.ObtenhaCompromissos
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaCompromissos(IDProprietario)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RemovaCompromisso(ByVal ID As Long) Implements IServicoDeAgenda.RemovaCompromisso
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            GerenciadorDeGatilhos.GetInstancia.DispareGatilhoAntes(Me.GetType.FullName, "RemovaCompromisso", New Object() {ID})
            Mapeador.RemovaCompromisso(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long Implements IServicoDeAgenda.InsiraTarefa
        Dim Mapeador As IMapeadorDeAgenda
        Dim ID As Long

        Tarefa.EstaConsistente()
        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            ID = Mapeador.InsiraTarefa(Tarefa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return ID
    End Function

    Public Sub ModifiqueTarefa(ByVal Tarefa As ITarefa) Implements IServicoDeAgenda.ModifiqueTarefa
        Dim Mapeador As IMapeadorDeAgenda

        Tarefa.EstaConsistente()
        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueTarefa(Tarefa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaTarefa(ByVal ID As Long) As ITarefa Implements IServicoDeAgenda.ObtenhaTarefa
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaTarefa(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa) Implements IServicoDeAgenda.ObtenhaTarefas
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaTarefas(IDProprietario)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RemovaTarefa(ByVal ID As Long) Implements IServicoDeAgenda.RemovaTarefa
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            GerenciadorDeGatilhos.GetInstancia.DispareGatilhoAntes(Me.GetType.FullName, "RemovaTarefa", New Object() {ID})
            Mapeador.RemovaTarefa(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long, _
                                        ByVal DataInicio As Date, _
                                        ByVal DataFim As Date?) As IList(Of ICompromisso) Implements IServicoDeAgenda.ObtenhaCompromissos
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaCompromissos(IDProprietario, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long, _
                                   ByVal DataInicio As Date, _
                                   ByVal DataFim As Date?) As IList(Of ITarefa) Implements IServicoDeAgenda.ObtenhaTarefas
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaTarefas(IDProprietario, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function InsiraLembrete(ByVal Lembrete As ILembrete) As Long Implements IServicoDeAgenda.InsiraLembrete
        Dim Mapeador As IMapeadorDeAgenda
        Dim ID As Long

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            ID = Mapeador.InsiraLembrete(Lembrete)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return ID
    End Function

    Public Sub ModifiqueLembrete(ByVal Lembrete As ILembrete) Implements IServicoDeAgenda.ModifiqueLembrete
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueLembrete(Lembrete)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaLembrete(ByVal ID As Long) As ILembrete Implements IServicoDeAgenda.ObtenhaLembrete
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaLembrete(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long) As IList(Of ILembrete) Implements IServicoDeAgenda.ObtenhaLembretes
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaLembretes(IDProprietario)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Date?) As IList(Of ILembrete) Implements IServicoDeAgenda.ObtenhaLembretes
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaLembretes(IDProprietario, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RemovaLembrete(ByVal ID As Long) Implements IServicoDeAgenda.RemovaLembrete
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            GerenciadorDeGatilhos.GetInstancia.DispareGatilhoAntes(Me.GetType.FullName, "RemovaLembrete", New Object() {ID})
            Mapeador.RemovaLembrete(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub ModifiqueConfiguracao(ByVal ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario) Implements IServicoDeAgenda.ModifiqueConfiguracao
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueConfiguracao(ConfiguracaoDaAgenda)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaConfiguracao(ByVal Pessoa As IPessoa) As IConfiguracaoDeAgendaDoUsuario Implements IServicoDeAgenda.ObtenhaConfiguracao
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaConfiguracao(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Function

    Public Function ObtenhaConfiguracao(ByVal IDPessoa As Long) As IConfiguracaoDeAgendaDoUsuario Implements IServicoDeAgenda.ObtenhaConfiguracao
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        Try
            Return Mapeador.ObtenhaConfiguracao(IDPessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RemovaConfiguracao(ByVal ID As Long) Implements IServicoDeAgenda.RemovaConfiguracao
        Dim Mapeador As IMapeadorDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAgenda)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaConfiguracao(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class