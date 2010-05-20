Imports Compartilhados
Imports T13.Interfaces.Servicos
Imports T13.Interfaces.Negocio
Imports T13.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeLancamentoDeServicosLocal
    Inherits Servico
    Implements IServicoDeLancamentoDeServicos

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeLancamentoDeServicos.Excluir
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Excluir(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Inserir(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.Inserir
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()
        ValidaRegrasDeNegocio(Lancamento, Mapeador)

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Lancamento)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.Modificar
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()
        ValidaRegrasDeNegocio(Lancamento, Mapeador)

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(Lancamento)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaLancamentosTardio(ByVal IDCliente As Long) As IList(Of ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()

        Try
            Return Mapeador.ObtenhaLancamentosTardio(IDCliente)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaLancamento(ByVal ID As Long) As ILacamentoDeServicosPrestados Implements IServicoDeLancamentoDeServicos.ObtenhaLancamento
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()

        Try
            Return Mapeador.ObtenhaLancamento(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaProximoNumeroDisponivel() As Long Implements IServicoDeLancamentoDeServicos.ObtenhaProximoNumeroDisponivel
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()

        Try
            Return Mapeador.ObtenhaProximoNumeroDisponivel
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Private Sub ValidaRegrasDeNegocio(ByVal Lancamento As ILacamentoDeServicosPrestados, ByVal Mapeador As IMapeadorDeLancamentoDeServicos)

        If Mapeador.NumeroEstaSendoUtilizando(Lancamento.Numero.Value) Then
            Throw New BussinesException("O número do lançamento informado já está sendo utilizado.")
        End If

    End Sub

    Public Function ObtenhaLancamentosTardio(ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Dim Mapeador As IMapeadorDeLancamentoDeServicos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeLancamentoDeServicos)()

        Try
            Return Mapeador.ObtenhaLancamentosTardio(DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class