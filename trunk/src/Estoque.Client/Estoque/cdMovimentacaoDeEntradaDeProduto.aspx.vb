Imports Compartilhados.Componentes.Web
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class cdMovimentacaoDeEntradaDeProduto
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ApliqueFiltroMovimentacoesInicial()
        End If
    End Sub

    'Traz as ultimas movimentacoes feitas no período de 3 meses
    Private Sub ApliqueFiltroMovimentacoesInicial()
        Dim DataFinal As Date = Now
        Dim DataInicio As Date = New Date(DataFinal.Year, DataFinal.Month - 3, DataFinal.Day)

        CarregueMovimentacoes(DataInicio, DataFinal)
    End Sub

    Private Sub CarregueMovimentacoes(ByVal DataInicio As Date, ByVal DataFim As Date)
        Dim Movimentacoes As IList(Of IMovimentacaoDeProdutoEntrada)

        Using Servico As IServicoDeMovimentacaoDeProdutoEntrada = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMovimentacaoDeProdutoEntrada)()
            Movimentacoes = Servico.ObtenhaMovimentacoes(DataInicio, DataFim)
        End Using

        ExibaMovimentacoes(Movimentacoes)
    End Sub

    Private Sub ExibaMovimentacoes(ByVal Movimentacoes As IList(Of IMovimentacaoDeProdutoEntrada))
        grdItensLancados.DataSource = Movimentacoes
        grdItensLancados.DataBind()
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.ETQ.005"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class