Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados
Imports Core.Interfaces.Negocio

Partial Public Class frmAlterarSenhaDoOperador
    Inherits SuperPagina

    Private Const CHAVE_OPERADOR As String = "CHAVE_OPERADOR_ALTERAR_SENHA_DO_OPERADOR"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        ViewState(CHAVE_OPERADOR) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlOperador, Control))
        UtilidadesWeb.LimparComponente(CType(pnlSenha, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlSenha, Control), False)
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.004"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return rtbToolBar
    End Function

    Private Sub cboOperador_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboOperador.ItemsRequested
        Dim Operadores As IList(Of IOperador)

        Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
            Operadores = Servico.ObtenhaOperadores(e.Text, 50)
        End Using

        If Not Operadores Is Nothing Then
            For Each Operador As IOperador In Operadores
                Dim Item As New RadComboBoxItem(Operador.Pessoa.Nome.Trim, Operador.Pessoa.ID.Value.ToString)

                Item.Attributes.Add("Login", Operador.Login)
                cboOperador.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboOperador_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboOperador.SelectedIndexChanged
        ExibaTelaModificar()
        ViewState(CHAVE_OPERADOR) = e.Value
    End Sub

    Private Function ValidaDados() As String
        If String.IsNullOrEmpty(txtNovaSenha.Text) Then Return "A nova senha deve ser informada."
        If String.IsNullOrEmpty(txtConfirmacaoNovaSenha.Text) Then Return "A confirmação da nova senha deve ser informada."

        Return Nothing
    End Function

    Private Function ObtenhaSenha(ByVal SenhaDescriptografada As String) As ISenha
        Dim Senha As ISenha
        Dim SenhaTXTCript As String

        SenhaTXTCript = AjudanteDeCriptografia.CriptografeMaoUnicao(SenhaDescriptografada)

        Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {SenhaTXTCript, Now})
        Return Senha
    End Function

    Private Sub btnModifica_Click()
        Dim Senha As IPessoaFisica = Nothing
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Dim NovaSenha As ISenha
        Dim ConfirmacaoDaNovaSenha As ISenha

        NovaSenha = ObtenhaSenha(txtNovaSenha.Text)
        ConfirmacaoDaNovaSenha = ObtenhaSenha(txtConfirmacaoNovaSenha.Text)

        Try
            Using Servico As IServicoDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSenha)()
                Servico.Altere(CLng(ViewState(CHAVE_OPERADOR)), NovaSenha, ConfirmacaoDaNovaSenha)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Senha do operador modificada com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub ExibaTelaModificar()
        UtilidadesWeb.LimparComponente(CType(pnlSenha, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlSenha, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnModificar"
                btnModifica_Click()
        End Select
    End Sub

End Class