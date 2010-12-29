Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Partial Public Class ctrlDespachoTarefa
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_ALVO_DESPACHO_TAREFA As String = "CHAVE_ID_ALVO_DESPACHO_TAREFA"
    Private Const CHAVE_SOLICITACAO_DESPACHO_TAREFA As String = "CHAVE_SOLICITACAO_DESPACHO_TAREFA"
    Private Const CHAVE_TIPO_DESPACHO_TAREFA As String = "CHAVE_TIPO_DESPACHO_TAREFA"

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
        Dim Inconsistencia As String

        Inconsistencia = ValidaDespacho()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Despacho = MontaDespacho()

        Try
            Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                IDTarefa = ServicoDeAgenda.InsiraTarefa(Despacho.Tarefa)
            End Using

            Despacho.Tarefa.ID = IDTarefa

            Using ServicoDeDespacho As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
                ServicoDeDespacho.Inserir(Despacho)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Solicitação despachada com sucesso."), False)
            RaiseEvent SolicitacaoFoiDespachada(Despacho)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaDespacho() As IDespachoTarefa
        Dim Despacho As IDespachoTarefa
        Dim Tarefa As ITarefa
        Dim UsuarioLogado As Usuario

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Tarefa = FabricaGenerica.GetInstancia.CrieObjeto(Of ITarefa)()
        Tarefa.Prioridade = PrioridadeDaTarefa.Obtenha(CChar(cboPrioridade.SelectedValue))
        Tarefa.Assunto = txtAssunto.Content
        Tarefa.Descricao = txtDescricao.Content
        Tarefa.DataDeConclusao = txtDataHorarioFim.SelectedDate.Value
        Tarefa.DataDeInicio = txtDataHorarioInicio.SelectedDate.Value
        Tarefa.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_TAREFA)))
        Tarefa.Status = StatusDaTarefa.NaoIniciada

        Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoTarefa)()
        Despacho.Tarefa = Tarefa
        Despacho.Solicitante = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)
        Despacho.Alvo = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_TAREFA)))
        Despacho.Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_TAREFA), ISolicitacao)
        Despacho.DataDoDespacho = Now
        Despacho.Tipo = CType(ViewState(CHAVE_TIPO_DESPACHO_TAREFA), TipoDeDespacho)

        Return Despacho
    End Function

    Private Function ValidaDespacho() As String
        If ViewState(CHAVE_ID_ALVO_DESPACHO_TAREFA) Is Nothing Then Return "O alvo do despacho deve ser informado."
        If String.IsNullOrEmpty(txtAssunto.Content) Then Return "O assunto deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e hora de inicio devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e hora final devem ser informados."

        Return Nothing
    End Function

    Public WriteOnly Property IDAlvo() As Long
        Set(ByVal value As Long)
            ViewState(CHAVE_ID_ALVO_DESPACHO_TAREFA) = value
        End Set
    End Property

    Public WriteOnly Property Solicitacao() As ISolicitacao
        Set(ByVal value As ISolicitacao)
            ViewState(CHAVE_SOLICITACAO_DESPACHO_TAREFA) = value
        End Set
    End Property

    Public WriteOnly Property TipoDespacho() As TipoDeDespacho
        Set(ByVal value As TipoDeDespacho)
            ViewState(CHAVE_TIPO_DESPACHO_TAREFA) = value
        End Set
    End Property

    Public WriteOnly Property Assunto() As String
        Set(ByVal value As String)
            txtAssunto.Content = value
        End Set
    End Property

    Public WriteOnly Property Descricao() As String
        Set(ByVal value As String)
            txtDescricao.Content = value
        End Set
    End Property

    Public WriteOnly Property Inicio() As Date
        Set(ByVal value As Date)
            txtDataHorarioInicio.SelectedDate = value
        End Set
    End Property

End Class