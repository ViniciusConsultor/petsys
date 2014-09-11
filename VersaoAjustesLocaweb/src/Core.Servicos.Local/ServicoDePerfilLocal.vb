Imports Compartilhados
Imports Compartilhados.Visual
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDePerfilLocal
    Inherits Servico
    Implements IServicoDePerfil

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function Obtenha(ByVal Usuario As Usuario) As Perfil Implements IServicoDePerfil.Obtenha
        Dim Mapeador As IMapeadorDePerfil
        Dim PerfilUsuario As Perfil

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePerfil)()

        Try
            PerfilUsuario = Mapeador.Obtenha(Usuario)
        Finally
            ServerUtils.libereRecursos()
        End Try

        If PerfilUsuario Is Nothing Then
            PerfilUsuario = New PerfilPadrao()
            Me.Salve(Usuario, PerfilUsuario)
        End If

        Return PerfilUsuario
    End Function

    Public Sub Remova(ByVal Usuario As Usuario) Implements IServicoDePerfil.Remova
        Dim Mapeador As IMapeadorDePerfil

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePerfil)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remova(Usuario)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil) Implements IServicoDePerfil.Salve
        Dim Mapeador As IMapeadorDePerfil

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePerfil)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remova(Usuario)
            Mapeador.Salve(Usuario, Perfil)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub SalveAtalhos(ByVal Usuario As Usuario, ByVal Atalhos As IList(Of Atalho)) Implements IServicoDePerfil.SalveAtalhos
        Dim Mapeador As IMapeadorDePerfil

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePerfil)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.SalveAtalhos(Usuario, Atalhos)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class