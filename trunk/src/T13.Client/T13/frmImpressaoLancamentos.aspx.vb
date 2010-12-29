Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports T13.Interfaces.Negocio
Imports T13.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio

Partial Public Class frmImpressaoLancamentos
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        UtilidadesWeb.LimparComponente(CType(pnlLancamentos, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlFiltro, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlLancamentos, Control), True)
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.T13.003"
    End Function

    Private Sub ImprimirLancamento(ByVal IDLancamento As Long)
        Dim NomeDoArquivo As String
        Dim Gerador As CriadorDeRelatorio
        Dim URL As String
        Dim Lancamento As ILacamentoDeServicosPrestados

        Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            Lancamento = Servico.ObtenhaLancamento(IDLancamento)
        End Using

        Dim ListaDeLancamentos As New List(Of ILacamentoDeServicosPrestados)

        ListaDeLancamentos.Add(Lancamento)

        Gerador = New CriadorDeRelatorio(ListaDeLancamentos)

        NomeDoArquivo = Gerador.GereNotaFiscal()
        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraArquivoParaDownload(URL, "Imprimir"), False)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        Dim Lancamentos As IList(Of ILacamentoDeServicosPrestados)
        Dim Inconsistencia As String

        Inconsistencia = VerificaInconsistencias()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            Lancamentos = Servico.ObtenhaLancamentosTardio(Me.txtDataInicial.SelectedDate.Value, Me.txtDataFinal.SelectedDate.Value)
        End Using

        ExibaLancamentos(Lancamentos)
    End Sub

    Private Function VerificaInconsistencias() As String
        Dim Inconsistencia As String = Nothing

        If txtDataInicial.SelectedDate Is Nothing Then
            Inconsistencia = "A data de início do lançamento deve ser informada."
            Return Inconsistencia
        End If

        If txtDataInicial.SelectedDate Is Nothing Then
            Inconsistencia = "A data de início do lançamento deve ser informada."
            Return Inconsistencia
        End If

        Return Inconsistencia
    End Function

    Private Function MontaDicionario(ByVal Lancamentos As IList(Of ILacamentoDeServicosPrestados)) As IDictionary(Of ICliente, IList(Of ILacamentoDeServicosPrestados))
        Dim Dicionario As IDictionary(Of ICliente, IList(Of ILacamentoDeServicosPrestados))

        Dicionario = New Dictionary(Of ICliente, IList(Of ILacamentoDeServicosPrestados))

        For Each Lancamento As ILacamentoDeServicosPrestados In Lancamentos
            If Not Dicionario.ContainsKey(Lancamento.Cliente) Then
                Dicionario.Add(Lancamento.Cliente, New List(Of ILacamentoDeServicosPrestados))
            End If

            Dicionario(Lancamento.Cliente).Add(Lancamento)
        Next

        Return Dicionario
    End Function

    Private Sub ExibaLancamentos(ByVal Lancamentos As IList(Of ILacamentoDeServicosPrestados))
        Dim Dicionario As IDictionary(Of ICliente, IList(Of ILacamentoDeServicosPrestados))

        Dicionario = MontaDicionario(Lancamentos)
        treLancamentos.Nodes.Clear()

        For Each Item As KeyValuePair(Of ICliente, IList(Of ILacamentoDeServicosPrestados)) In Dicionario
            Dim NoCliente As RadTreeNode

            NoCliente = New RadTreeNode(Item.Key.Pessoa.Nome, Item.Key.Pessoa.ID.Value.ToString)

            For Each Lancamento As ILacamentoDeServicosPrestados In Item.Value
                NoCliente.Nodes.Add(New RadTreeNode(Lancamento.DataDeLancamento.ToString("dd/MM/yyyy"), Lancamento.ID.Value.ToString))
            Next

            treLancamentos.Nodes.Add(NoCliente)
        Next
    End Sub

    Private Sub treLancamentos_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treLancamentos.NodeClick
        If e.Node.Nodes Is Nothing OrElse e.Node.Nodes.Count = 0 Then
            ImprimirLancamento(CLng(e.Node.Value))
        End If
    End Sub

End Class