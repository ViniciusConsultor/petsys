Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad
Imports Telerik.Web.UI

Partial Public Class frmVacinas
    Inherits SuperPagina

    Private Const CHAVE_VACINAS As String = "CHAVE_FRMVACINAS_VACINAS"
    Private Const CHAVE_ID_ANIMAL As String = "CHAVE_FRMVACINAS_ID_ANIMAL"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
                ExibaTelaInicial(Id.Value)
            End If

        End If
    End Sub

    Private Sub ExibaTelaInicial(ByVal IDAnimal As Long)
        Dim Vacinas As IList(Of IVacina)

        ViewState(CHAVE_ID_ANIMAL) = IDAnimal
        Dim Animal As IAnimal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(IDAnimal)

        crtlAnimalResumido1.ApresentaDadosResumidosDoAnimal(Animal)

        Using Servico As IServicoDeVacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVacina)()
            Vacinas = Servico.ObtenhaVacinasDoAnimal(IDAnimal)
        End Using

        ViewState(CHAVE_VACINAS) = Vacinas
        grdVacinas.DataSource = Vacinas
        grdVacinas.DataBind()
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

    Private Sub btnNovo_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/cdVacina.aspx", "?Id=", CLng(ViewState(CHAVE_ID_ANIMAL)))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Nova vacina", 500, 300), False)
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
        End Select
    End Sub

    Private Sub grdVacinas_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdVacinas.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdVacinas, ViewState(CHAVE_VACINAS), e)
    End Sub

End Class