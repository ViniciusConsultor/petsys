﻿Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

<Serializable()> _
Public Class ServicoDeEmpresaLocal
    Inherits Servico
    Implements IServicoDeEmpresa

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Insira(ByVal Empresa As IEmpresa) Implements IServicoDeEmpresa.Insira
        Dim Mapeador As IMapeadorDeEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeEmpresa)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Insira(Empresa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha() As IEmpresa Implements IServicoDeEmpresa.Obtenha
        Dim Mapeador As IMapeadorDeEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeEmpresa)()

        Try
            Return Mapeador.Obtenha()
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class