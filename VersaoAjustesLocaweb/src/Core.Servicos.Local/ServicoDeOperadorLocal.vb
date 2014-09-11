Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio

Public Class ServicoDeOperadorLocal
    Inherits Servico
    Implements IServicoDeOperador

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Modificar(ByVal Operador As IOperador) Implements IServicoDeOperador.Modificar
        Dim Mapeador As IMapeadorDeOperador

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Operador)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeOperador.Remover
        Dim MapeadorDeOperador As IMapeadorDeOperador
        Dim MapeadorDeSenha As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        MapeadorDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()
        MapeadorDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        ServerUtils.BeginTransaction()

        Try
            MapeadorDeSenha.Remova(ID)
            MapeadorDeOperador.Remover(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        End Try
    End Sub

    Public Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador Implements IServicoDeOperador.ObtenhaOperadorPorLogin
        Dim Mapeador As IMapeadorDeOperador

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()

        Try
            Return Mapeador.ObtenhaOperadorPorLogin(Login)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador Implements IServicoDeOperador.ObtenhaOperador
        Dim Mapeador As IMapeadorDeOperador

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()

        Try
            Return Mapeador.ObtenhaOperador(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Inserir(ByVal Operador As IOperador, _
                       ByVal Senha As ISenha) Implements IServicoDeOperador.Inserir
        Dim MapeadorDeOperador As IMapeadorDeOperador
        Dim MapeadorDeSenha As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        MapeadorDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()
        MapeadorDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            ServerUtils.BeginTransaction()
            MapeadorDeOperador.Inserir(Operador)
            MapeadorDeSenha.Insira(Operador.Pessoa.ID.Value, Senha)
            ServerUtils.CommitTransaction()

        Catch ex As Exception
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaOperadores(ByVal Nome As String, _
                                      ByVal Quantidade As Integer) As IList(Of IOperador) Implements IServicoDeOperador.ObtenhaOperadores
        Dim Mapeador As IMapeadorDeOperador

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeOperador)()

        Try
            Return Mapeador.ObtenhaOperadores(Nome, Quantidade)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class