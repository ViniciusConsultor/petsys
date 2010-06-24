Imports System.IO
Imports Telerik.Web.UI
Imports Compartilhados
Imports Compartilhados.Visual
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos

Partial Public Class frmAlterarPapelDeParede
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Perfil As Perfil
            Dim Usuario As Usuario

            Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
            Perfil = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil

            RadSkinManager1.Skin = Perfil.Skin
            imgPapelDeParede.ImageUrl = String.Concat("~/", Perfil.ImagemDesktop)
        End If
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.004"
    End Function

    Private Sub btnSalvar_Click()
        Dim Perfil As Perfil
        Dim Usuario As Usuario

        Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Perfil = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil

        Perfil.Skin = RadSkinManager1.Skin
        Perfil.ImagemDesktop = imgPapelDeParede.ImageUrl.Remove(0, 2)

        Try
            Using Servico As IServicoDePerfil = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePerfil)()
                Servico.Salve(Usuario, Perfil)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Perfil alterado com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSubmit.Click
        If uplFoto.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = uplFoto.UploadedFiles(0)
            Dim targetFolder As String = Server.MapPath(uplFoto.TargetFolder)
            Dim targetFileName As String = Path.Combine(targetFolder, file.GetNameWithoutExtension() + file.GetExtension())
            file.SaveAs(targetFileName)

            imgPapelDeParede.ImageUrl = String.Concat(uplFoto.TargetFolder, "/", file.GetNameWithoutExtension() + file.GetExtension())
        End If
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalvar_Click()
        End Select
    End Sub

End Class