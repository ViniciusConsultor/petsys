Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Telerik.Web.UI
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad

Partial Public Class cdVacina
    Inherits SuperPagina

    Private Const CHAVE_ID_ANIMAL As String = "CHAVE_CD_VACINA_ID_ANIMAL"

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
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaVacina, Control))

        txtData.SelectedDate = Now

        Dim UsuarioLogadoEhVeterinario As Boolean

        Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
            UsuarioLogadoEhVeterinario = Servico.VerificaSePessoaEhVeterinario(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        End Using

        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = UsuarioLogadoEhVeterinario

        If UsuarioLogadoEhVeterinario Then
            lblVeterinarioResponsavel.Text = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.Nome
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Para utilizar a funcionalidade de vacinas o usuário precisa ser um veterinário."), False)
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
            Using Servico As IServicoDeVacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVacina)()
                Servico.Inserir(ObtenhaObjetoVacina)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Vacina cadastrada com sucesso."), False)
            End Using

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try

    End Sub

    Private Function ObtenhaObjetoVacina() As IVacina
        Dim Vacina As IVacina

        Vacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IVacina)()
        Vacina.Nome = txtNome.Text
        Vacina.Observacao = txtObservacao.Text
        Vacina.DataDaVacinacao = txtData.SelectedDate.Value
        Vacina.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL)))
        Vacina.RevacinarEm = txtRevacinar.SelectedDate

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Vacina.VeterinarioQueAplicou = VeterinarioLazyLoad

        Return Vacina
    End Function

    Private Function ValidaDados() As String
        If Not txtData.SelectedDate.HasValue Then Return "A data da vacina deve ser informada."
        If String.IsNullOrEmpty(txtNome.Text) Then Return "O nome da vacina deve ser informado."
        Return Nothing
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalvar_Click()
        End Select
    End Sub


End Class