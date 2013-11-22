Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeMenu
    Implements IMapeadorDeMenu

    Public Function ObtenhaMenu() As IMenuComposto Implements IMapeadorDeMenu.ObtenhaMenu
        Dim SQL As New StringBuilder
        Dim Menu As IMenuComposto
        Dim DBHelper As IDBHelper

        SQL.AppendLine("SELECT NCL_MODULO.NOME AS NOME_MODULO, NCL_MENUMODULO.IDMODULO AS ID_MODULO, NCL_MENUMODULO.IMAGEM AS IMAGEM_MODULO, ")
        SQL.AppendLine("NCL_MENUFUNCAO.IDFUNCAO AS ID_FUNCAO, NCL_FUNCAO.NOME AS NOME_FUNCAO, NCL_MENUFUNCAO.IMAGEM AS IMAGEM_FUNCAO, NCL_MENUFUNCAO.URL, NCL_MENUFUNCAO.AGRUPADOR")
        SQL.AppendLine(" FROM NCL_MODULO, NCL_FUNCAO, NCL_MENUMODULO, NCL_MENUFUNCAO")
        SQL.AppendLine(" WHERE NCL_MENUMODULO.IDMODULO = NCL_MODULO.IDMODULO")
        SQL.AppendLine(" AND NCL_MENUFUNCAO.IDFUNCAO = NCL_FUNCAO.IDFUNCAO")
        SQL.AppendLine(" AND NCL_MENUFUNCAO.IDMODULO = NCL_MENUMODULO.IDMODULO")
        SQL.AppendLine(" ORDER BY NCL_MENUMODULO.IDMODULO, NCL_MENUFUNCAO.IDFUNCAO, NCL_MENUFUNCAO.AGRUPADOR")

        DBHelper = ServerUtils.criarNovoDbHelper
        Menu = FabricaGenerica.GetInstancia.CrieObjeto(Of IMenuComposto)()

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                Dim MenuComposto As IMenuComposto = Nothing
                Dim MenuFolha As IMenuFolha = Nothing

                Dim IdModuloCorrente As String = ""
                Dim IdFuncaoCorrente As String = ""
                
                While Leitor.Read

                    If IdModuloCorrente <> UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO") Then
                        MenuComposto = FabricaGenerica.GetInstancia.CrieObjeto(Of IMenuComposto)()
                        MenuComposto.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO")
                        MenuComposto.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_MODULO")
                        MenuComposto.Imagem = UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEM_MODULO")
                        Menu.AdicioneItem(MenuComposto)
                        IdModuloCorrente = UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO")
                    End If

                    If IdFuncaoCorrente <> UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO") Then
                        
                        MenuFolha = FabricaGenerica.GetInstancia.CrieObjeto(Of IMenuFolha)()
                        MenuFolha.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO")
                        MenuFolha.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_FUNCAO")
                        MenuFolha.URL = UtilidadesDePersistencia.GetValorString(Leitor, "URL")
                        MenuFolha.Imagem = UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEM_FUNCAO")

                        If UtilidadesDePersistencia.EhNulo(Leitor, "AGRUPADOR") Then
                            MenuComposto.AdicioneItem(MenuFolha)
                        Else
                            If Not MenuComposto.Agrupador.ContainsKey(UtilidadesDePersistencia.GetValorString(Leitor, "AGRUPADOR")) Then
                                MenuComposto.Agrupador.Add(UtilidadesDePersistencia.GetValorString(Leitor, "AGRUPADOR"), FabricaGenerica.GetInstancia.CrieObjeto(Of IMenuComposto)())
                            End If

                            MenuComposto.Agrupador(UtilidadesDePersistencia.GetValorString(Leitor, "AGRUPADOR")).AdicioneItem(MenuFolha)
                        End If

                        IdFuncaoCorrente = UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO")
                    End If

                End While
            Finally
                Leitor.Close()
            End Try
            
        End Using

        Return Menu
    End Function

End Class
