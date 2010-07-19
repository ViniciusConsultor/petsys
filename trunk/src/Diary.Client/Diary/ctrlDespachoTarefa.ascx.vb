Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Telerik.Web.UI

Partial Public Class ctrlDespachoTarefa
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA As String = "CHAVE_ID_PROPRIETARIO_DESPACHO_TAREFA"
    Private Const CHAVE_SOLICITACAO_DESPACHO_AGENDA As String = "CHAVE_SOLICITACAO_DESPACHO_TAREFA"

    Public Event SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            AlimentaComponentes()
        End If
    End Sub

    Private Sub AlimentaComponentes()
        cboPrioridade.Items.Clear()

        For Each Prioridade As PrioridadeDaTarefa In PrioridadeDaTarefa.ObtenhaTodos
            cboPrioridade.Items.Add(New RadComboBoxItem(Prioridade.Descricao, Prioridade.ID.ToString))
        Next
    End Sub

    Private Sub btnAdicionarDespacho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarDespacho.Click
        Dim IDTarefa As Long
        Dim Despacho As IDespachoTarefa

        Despacho = MontaDespacho()

        Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            IDTarefa = ServicoDeAgenda.InsiraTarefa(Despacho.Tarefa)
        End Using

        Despacho.Tarefa.ID = IDTarefa

        Using ServicoDeDespacho As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
            ServicoDeDespacho.Inserir(Despacho)
        End Using

        RaiseEvent SolicitacaoFoiDespachada(Despacho)
    End Sub

    Private Function MontaDespacho() As IDespachoTarefa
        Dim Despacho As IDespachoTarefa
        Dim Tarefa As ITarefa
        Dim UsuarioLogado As Usuario

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Tarefa = FabricaGenerica.GetInstancia.CrieObjeto(Of ITarefa)()
        Tarefa.Prioridade = PrioridadeDaTarefa.Obtenha(CChar(cboPrioridade.SelectedValue))
        Tarefa.Assunto = txtAssunto.Text
        Tarefa.Descricao = txtDescricao.Text
        Tarefa.DataDeConclusao = txtDataHorarioFim.SelectedDate.Value
        Tarefa.DataDeInicio = txtDataHorarioInicio.SelectedDate.Value
        Tarefa.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA)))

        Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoTarefa)()
        Despacho.Tarefa = Tarefa
        Despacho.Responsavel = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)
        Despacho.Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA), ISolicitacao)
        Despacho.DataDoDespacho = Now

        Return Despacho
    End Function

    Private Function ValidaDespacho() As String
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e hora de inicio devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e hora final devem ser informados."

        Return Nothing
    End Function

    Public WriteOnly Property IDProprietario() As Long
        Set(ByVal value As Long)
            ViewState(CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA) = value
        End Set
    End Property

    Public WriteOnly Property Solicitacao() As ISolicitacao
        Set(ByVal value As ISolicitacao)
            ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA) = value
        End Set
    End Property

End Class