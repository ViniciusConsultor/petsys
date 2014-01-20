Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class frmVermifugos
    Inherits SuperPagina

    Private Const CHAVE_VERMIFUGOS As String = "CHAVE_FRMVERMIFUGOS_VERMIFUGOS"
    Private Const CHAVE_ID_ANIMAL As String = "CHAVE_FRMVERMIFUGOS_ID_ANIMAL"

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
        Dim Vermifugos As IList(Of IVermifugo)

        ViewState(CHAVE_ID_ANIMAL) = IDAnimal
        Dim Animal As IAnimal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(IDAnimal)

        crtlAnimalResumido1.ApresentaDadosResumidosDoAnimal(Animal)

        Using Servico As IServicoDeVermifugo = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVermifugo)()
            Vermifugos = Servico.ObtenhaVermifugosDoAnimal(IDAnimal)
        End Using

        ViewState(CHAVE_VERMIFUGOS) = Vermifugos
        grdVermifugos.DataSource = Vermifugos
        grdVermifugos.DataBind()
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

    Private Sub btnNovo_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/cdVermifugo.aspx", "?Id=", CLng(ViewState(CHAVE_ID_ANIMAL)))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Novo vermífugo", 500, 300), False)
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
        End Select
    End Sub

    Private Sub grdVermifugos_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdVermifugos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdVermifugos, ViewState(CHAVE_VERMIFUGOS), e)
    End Sub

End Class