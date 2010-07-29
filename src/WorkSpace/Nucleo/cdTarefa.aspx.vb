Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class cdTarefa
    Inherits SuperPagina

    Private Const CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_TAREFA"
    Private Const CHAVE_ID_TAREFA As String = "CHAVE_ID_TAREFA"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO_TAREFA"

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim IdTarefa As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("IdProprietario")) Then
                ViewState(CHAVE_ID_PROPRIETARIO) = CLng(Request.QueryString("IdProprietario"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("IdTarefa")) Then
                IdTarefa = CLng(Request.QueryString("IdTarefa"))
            End If

            If IdTarefa Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes(IdTarefa.Value)
            End If
        End If
    End Sub

    Private Sub CarregaDados()
        cboPrioridade.Items.Clear()

        For Each Prioridade As PrioridadeDaTarefa In PrioridadeDaTarefa.ObtenhaTodos
            cboPrioridade.Items.Add(New RadComboBoxItem(Prioridade.Descricao, Prioridade.ID))
        Next

        cboStatus.Items.Clear()
        For Each Status As StatusDaTarefa In StatusDaTarefa.ObtenhaTodos
            cboStatus.Items.Add(New RadComboBoxItem(Status.Descricao, Status.ID.ToString))
        Next

        cboStatus.SelectedValue = StatusDaTarefa.NaoIniciada.ID.ToString
    End Sub

    Private Sub ExibaTelaNovo()
        ViewState(CHAVE_ESTADO) = Estado.Novo
        LimpaDados()
    End Sub

    Private Sub LimpaDados()
        ViewState(CHAVE_ID_TAREFA) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoCompromisso, Control))
        CarregaDados()
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        LimpaDados()

        Dim Tarefa As ITarefa

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Tarefa = Servico.ObtenhaTarefa(Id)
        End Using

        If Tarefa Is Nothing Then Exit Sub

        txtAssunto.Text = Tarefa.Assunto
        txtDescricao.Text = Tarefa.Descricao
        txtDataHorarioInicio.SelectedDate = Tarefa.DataDeInicio
        txtDataHorarioFim.SelectedDate = Tarefa.DataDeConclusao
        cboPrioridade.SelectedValue = Tarefa.Prioridade.ID.ToString
        ViewState(CHAVE_ID_PROPRIETARIO) = Tarefa.Proprietario.ID
        ViewState(CHAVE_ID_TAREFA) = Tarefa.ID
        cboStatus.SelectedValue = Tarefa.Status.ID.ToString
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Private Sub btnSalva_Click()
        Dim Mensagem As String = ""
        Dim Tarefa As ITarefa
        Dim Inconsistencia As String

        Inconsistencia = ConsisteDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Tarefa = MontaObjeto()

        Try
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.InsiraTarefa(Tarefa)
                    Mensagem = "Tarefa cadastrada com sucesso."
                Else
                    Servico.ModifiqueTarefa(Tarefa)
                    Mensagem = "Tarefa modificada com sucesso."
                End If

            End Using

            ExibaTelaNovo()

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As ITarefa
        Dim Tarefa As ITarefa

        Tarefa = FabricaGenerica.GetInstancia.CrieObjeto(Of ITarefa)()
        Tarefa.Assunto = txtAssunto.Text
        Tarefa.Descricao = txtDescricao.Text
        Tarefa.DataDeInicio = txtDataHorarioInicio.SelectedDate.Value
        Tarefa.DataDeConclusao = txtDataHorarioFim.SelectedDate.Value
        Tarefa.Prioridade = PrioridadeDaTarefa.Obtenha(CChar(cboPrioridade.SelectedValue))
        Tarefa.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_PROPRIETARIO)))

        If CByte(ViewState(CHAVE_ESTADO)) = Estado.Modifica Then
            Tarefa.ID = CLng(ViewState(CHAVE_ID_TAREFA))
        End If

        Tarefa.Status = StatusDaTarefa.Obtenha(CChar(cboStatus.SelectedValue))

        Return Tarefa
    End Function

    Private Function ConsisteDados() As String
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto da tarefa deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e horário de início da tarefa devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e horário de fim da tarefa devem ser informados."
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

End Class