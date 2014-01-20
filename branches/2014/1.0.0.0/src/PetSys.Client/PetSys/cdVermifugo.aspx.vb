Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Telerik.Web.UI
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad

Partial Public Class cdVermifugo
    Inherits SuperPagina

    Private Const CHAVE_ID_ANIMAL As String = "CHAVE_CD_VERMIFUGO_ID_ANIMAL"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
                ViewState(CHAVE_ID_ANIMAL) = Id
            End If

            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoVermifugo, Control))

        txtData.SelectedDate = Now

        Dim UsuarioLogadoEhVeterinario As Boolean

        Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
            UsuarioLogadoEhVeterinario = Servico.VerificaSePessoaEhVeterinario(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        End Using

        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = UsuarioLogadoEhVeterinario

        If UsuarioLogadoEhVeterinario Then
            lblVeterinarioResponsavel.Text = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.Nome
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Para utilizar a funcionalidade de vermífugos o usuário precisa ser um veterinário."), False)
        End If

    End Sub

    Protected Sub btnSalvar_Click()
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Try
            Using Servico As IServicoDeVermifugo = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVermifugo)()
                Servico.Inserir(ObtenhaObjetoVermifugo)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Vermífugo cadastrado com sucesso."), False)
            End Using

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try

    End Sub

    Private Function ObtenhaObjetoVermifugo() As IVermifugo
        Dim Vermifugo As IVermifugo

        Vermifugo = FabricaGenerica.GetInstancia.CrieObjeto(Of IVermifugo)()
        Vermifugo.Nome = txtNome.Text
        Vermifugo.Observacao = txtObservacao.Text
        Vermifugo.Data = txtData.SelectedDate.Value
        Vermifugo.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL)))
        Vermifugo.ProximaDoseEm = txtProximaDose.SelectedDate

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Vermifugo.VeterinarioQueReceitou = VeterinarioLazyLoad

        Return Vermifugo
    End Function

    Private Function ValidaDados() As String
        If Not txtData.SelectedDate.HasValue Then Return "A data do vermífugo deve ser informada."
        If String.IsNullOrEmpty(txtNome.Text) Then Return "O nome do vermífugo deve ser informado."
        Return Nothing
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalvar_Click()
        End Select
    End Sub

End Class