Imports Compartilhados
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Public Class frmEscolhaDaEmpresa
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EmpresasViveis As IList(Of EmpresaVisivel)

        If Not IsPostBack Then
            EmpresasViveis = FabricaDeContexto.GetInstancia.GetContextoAtual().Usuario.EmpresasVisiveis

            If EmpresasViveis.Count = 0 Then
                Exit Sub
            End If

            If EmpresasViveis.Count = 1 Then
                TermineDeEntrarNoSistema(EmpresasViveis(0))
            Else
                CarregueEmpresasVisiveisParaOOperador(EmpresasViveis)
            End If
        End If

    End Sub

    Private Sub btnEntrar_Click(sender As Object, e As System.EventArgs) Handles btnEntrar.Click

        If cboEmpresas.Items.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Você não tem visibilidade para nenhuma empresa. Solicite acesso ao Adminstrador do Sistema."), False)
            Exit Sub
        End If

        FabricaDeContexto.GetInstancia.GetContextoAtual.EmpresaLogada = FabricaDeContexto.GetInstancia.GetContextoAtual().Usuario.ObtenhaEmpresaViveisPorID(CLng(cboEmpresas.SelectedValue))

        TermineDeEntrarNoSistema(FabricaDeContexto.GetInstancia.GetContextoAtual().Usuario.ObtenhaEmpresaViveisPorID(CLng(cboEmpresas.SelectedValue)))
    End Sub

    Private Sub TermineDeEntrarNoSistema(EmpresaVisivel As EmpresaVisivel)
        FabricaDeContexto.GetInstancia.GetContextoAtual.EmpresaLogada = EmpresaVisivel

        Response.Redirect("Desktop.aspx")
    End Sub

    Private Sub CarregueEmpresasVisiveisParaOOperador(EmpresasViveis As IList(Of EmpresaVisivel))
        cboEmpresas.Items.Clear()

        For Each Empresa In EmpresasViveis
            cboEmpresas.Items.Add(New RadComboBoxItem(Empresa.Nome, Empresa.ID.ToString()))
        Next
    End Sub

End Class