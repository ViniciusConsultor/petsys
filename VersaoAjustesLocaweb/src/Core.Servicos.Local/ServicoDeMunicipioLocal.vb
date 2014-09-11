Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

Public Class ServicoDeMunicipioLocal
    Inherits Servico
    Implements IServicoDeMunicipio

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal Id As Long) Implements IServicoDeMunicipio.Excluir
        Dim Mapeador As IMapeadorDeMunicipio

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Excluir(Id)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Inserir(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Inserir
        Dim Mapeador As IMapeadorDeMunicipio

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()
        ValidaRegras(Municipio, Mapeador)

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Municipio)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Sub

    Public Sub Modificar(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Modificar
        Dim Mapeador As IMapeadorDeMunicipio

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()
        ValidaRegras(Municipio, Mapeador)

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Municipio)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Sub

    Public Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio Implements IServicoDeMunicipio.ObtenhaMunicipio
        Dim Mapeador As IMapeadorDeMunicipio
        Dim Municipio As IMunicipio

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()

        Try
            Municipio = Mapeador.ObtenhaMunicipio(Id)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Municipio
    End Function

    Private Sub ValidaRegras(ByVal Municipio As IMunicipio, _
                             ByVal Mapeador As IMapeadorDeMunicipio)

        If Mapeador.Existe(Municipio) Then
            Throw New BussinesException("Já existe um municipio cadastrado com este nome na UF selecionada.")
        End If

    End Sub

    Public Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, _
                                                   ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio) Implements IServicoDeMunicipio.ObtenhaMunicipiosPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeMunicipio
        Dim Municipios As IList(Of IMunicipio)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()

        Try
            Municipios = Mapeador.ObtenhaMunicipiosPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Municipios
    End Function

End Class