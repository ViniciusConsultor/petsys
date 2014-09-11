Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Visual
Imports Compartilhados
Imports System.Text
Imports Compartilhados.DBHelper

Public Class MapeadorDePerfil
    Implements IMapeadorDePerfil

    Public Function Obtenha(ByVal Usuario As Usuario) As Perfil Implements IMapeadorDePerfil.Obtenha
        Dim SQL As New StringBuilder
        Dim Perfil As Perfil = Nothing

        SQL.Append("SELECT IDUSUARIO, IMAGEMDESKTOP, TEMA")
        SQL.Append(" FROM NCL_PERFIL")
        SQL.Append(String.Concat(" WHERE IDUSUARIO = ", Usuario.ID))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                If Leitor.Read() Then
                    Perfil = New PerfilUsuario(UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEMDESKTOP"), _
                                               UtilidadesDePersistencia.GetValorString(Leitor, "TEMA"))
                End If
            Finally
                Leitor.Close()
            End Try
            
        End Using

        If Not Perfil Is Nothing Then
            Dim Atalhos As IList(Of Atalho) = ObtenhaAtalhos(Usuario)

            For Each Atalho As Atalho In Atalhos
                Perfil.AdicioneAtalho(Atalho)
            Next
        End If

        Return Perfil
    End Function

    Public Sub Remova(ByVal Usuario As Usuario) Implements IMapeadorDePerfil.Remova
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_PERFIL")
        SQL.Append(String.Concat(" WHERE IDUSUARIO = ", Usuario.ID))
        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil) Implements IMapeadorDePerfil.Salve
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("INSERT INTO NCL_PERFIL (IDUSUARIO, IMAGEMDESKTOP, TEMA)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Usuario.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Perfil.ImagemDesktop), "', "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Perfil.Skin), "') "))

        DBHelper.ExecuteNonQuery(SQL.ToString, False)
    End Sub

    Public Sub SalveAtalhos(ByVal Usuario As Usuario, ByVal Atalhos As IList(Of Atalho)) Implements IMapeadorDePerfil.SalveAtalhos
        Dim SQL As New StringBuilder
        Dim Perfil As Perfil = Nothing
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_ATALHO WHERE IDUSUARIO = " & Usuario.ID.ToString)
        DBHelper.ExecuteNonQuery(SQL.ToString)

        If Not Atalhos Is Nothing AndAlso Not Atalhos.Count = 0 Then
            For Each Atalho As Atalho In Atalhos
                SQL = New StringBuilder

                SQL.Append("INSERT INTO NCL_ATALHO (ID, NOME, TIPO, URL, IMAGEM, IDUSUARIO)")
                SQL.Append(" VALUES ( ")

                SQL.Append(String.Concat("'", Atalho.ID, "', "))
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atalho.Nome), "', "))
                SQL.Append(String.Concat("'", Atalho.Tipo.ID, "', "))
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atalho.URL), "', "))
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atalho.Imagem), "', "))
                SQL.Append(String.Concat(Usuario.ID, ") "))

                DBHelper.ExecuteNonQuery(SQL.ToString, False)
            Next
        End If

    End Sub

    Public Function ObtenhaAtalhos(ByVal Usuario As Usuario) As IList(Of Atalho) Implements IMapeadorDePerfil.ObtenhaAtalhos
        Dim SQL As New StringBuilder
        Dim Atalho As Atalho = Nothing
        Dim Atalhos As IList(Of Atalho)
        Dim TipoDeAtalho As TipoAtalho

        Atalhos = New List(Of Atalho)

        SQL.Append("SELECT ID, NOME, TIPO, URL, IMAGEM, IDUSUARIO")
        SQL.Append(" FROM NCL_ATALHO")
        SQL.Append(String.Concat(" WHERE IDUSUARIO = ", Usuario.ID))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read
                    TipoDeAtalho = TipoAtalho.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "TIPO"))

                    If TipoDeAtalho.Equals(TipoAtalho.Externo) Then
                        Atalho = New AtalhoExterno(UtilidadesDePersistencia.GetValorString(Leitor, "NOME"), _
                                                   UtilidadesDePersistencia.GetValorString(Leitor, "URL"), _
                                                   UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEM"))
                        Atalho.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID")
                    Else
                        Atalho = New AtalhoSistema(UtilidadesDePersistencia.GetValorString(Leitor, "ID"), _
                                                    UtilidadesDePersistencia.GetValorString(Leitor, "NOME"), _
                                                   UtilidadesDePersistencia.GetValorString(Leitor, "URL"), _
                                                   UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEM"))
                    End If

                    Atalhos.Add(Atalho)
                End While
            Finally
                Leitor.Close()
            End Try
           
        End Using

        Return Atalhos
    End Function

End Class