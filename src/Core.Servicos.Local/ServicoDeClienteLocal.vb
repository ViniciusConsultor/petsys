Imports Core.Interfaces.Servicos
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeClienteLocal
    Inherits Servico
    Implements IServicoDeCliente

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Cliente As ICliente) Implements IServicoDeCliente.Inserir
        Dim Mapeador As IMapeadorDeCliente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Cliente)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Cliente As ICliente) Implements IServicoDeCliente.Modificar
        'Dim Mapeador As IMapeadorDeCliente

        'ServerUtils.setCredencial(MyBase._Credencial)
        'Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        'ServerUtils.BeginTransaction()

        'Try
        '    Mapeador.Modificar(Cliente)
        '    ServerUtils.CommitTransaction()
        'Catch
        '    ServerUtils.RollbackTransaction()
        '    Throw
        'Finally
        '    ServerUtils.libereRecursos()
        'End Try
    End Sub

    Public Overloads Function Obtenha(ByVal Pessoa As IPessoa) As ICliente Implements IServicoDeCliente.Obtenha
        Dim Mapeador As IMapeadorDeCliente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        Try
            Return Mapeador.Obtenha(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeCliente.Remover
        Dim Mapeador As IMapeadorDeCliente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remover(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente) Implements IServicoDeCliente.ObtenhaPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeCliente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        Try
            Return Mapeador.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Overloads Function Obtenha(ByVal ID As Long) As ICliente Implements IServicoDeCliente.Obtenha
        Dim Mapeador As IMapeadorDeCliente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class