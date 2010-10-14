Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class frmAtendimentoAnimal
    Inherits SuperPagina

    Private Const CHAVE_HISTORICO_ATENDIMENTOS As String = "CHAVE_HISTORICO_ATENDIMENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler crtlAnimal1.AnimalFoiSelecionado, AddressOf AnimalFoiSelecionado

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True

        crtlAnimal1.Inicializa()
        crtlAnimal1.BotaoDetalharEhVisivel = False
        crtlAnimal1.BotaoNovoEhVisivel = True
        crtlAnimal1.EnableLoadOnDemand = True
        crtlAnimal1.ShowDropDownOnTextboxClick = True
        crtlAnimal1.AutoPostBack = True
        crtlAnimal1.EhObrigatorio = False

        Dim Atendimentos As IList(Of IAtendimentoDoAnimal) = New List(Of IAtendimentoDoAnimal)

        ExibaAtendimentosHistoricos(Atendimentos)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.PET.003"
    End Function

    Private Sub AnimalFoiSelecionado(ByVal Animal As IAnimal)
        Dim Atendimentos As IList(Of IAtendimentoDoAnimal)

        Using Servico As IServicoDeAtendimentoDoAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimentoDoAnimal)()
            Atendimentos = Servico.ObtenhaAtendimentos(Animal)
        End Using

        crtlAnimal1.BotaoDetalharEhVisivel = True
        ExibaAtendimentosHistoricos(Atendimentos)
    End Sub

    Private Sub ExibaAtendimentosHistoricos(ByVal Atendimentos As IList(Of IAtendimentoDoAnimal))
        UtilidadesWeb.LimparComponente(CType(pnlHistoricoDeAtendimentos, Control))

        ViewState(CHAVE_HISTORICO_ATENDIMENTOS) = Atendimentos
        grdAtendimentos.DataSource = Atendimentos
        grdAtendimentos.DataBind()
    End Sub

End Class