Imports Diary.Interfaces.Negocio
Imports Compartilhados.Componentes.Web
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Partial Public Class cdSolicitacaoDeAudiencia
    Inherits System.Web.UI.Page

    Private Const CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_SOLICITACAO_AUDIENCIA"
    Private Const CHAVE_ID As String = "CHAVE_ID_SOLICITACAO_DE_AUDIENCIA"
    Private Const CHAVE_DATA As String = "CHAVE_DATA_SOLICITACAO_DE_AUDIENCIA"
    Private Const CHAVE_STATUS As String = "CHAVE_STATUS_SOLICITACAO_DE_AUDIENCIA"
    Private Const CHAVE_ID_CONTATO As String = "CHAVE_ID_CONTATO_SOLICITACAO_DE_AUDIENCIA"
    Private Const CHAVE_USUARIO_CADASTROU As String = "CHAVE_USUARIO_CADASTROU_SOLICITACAO_DE_AUDIENCIA"

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
            End If

            If Id Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes(Id.Value)
            End If
        End If
    End Sub

    Private Sub ExibaTelaNovo()
        ViewState(CHAVE_ESTADO) = Estado.Novo
        LimpaDados()
        cboContato.Enabled = True
    End Sub

    Private Sub LimpaDados()
        ViewState(CHAVE_DATA) = Nothing
        ViewState(CHAVE_STATUS) = Nothing
        ViewState(CHAVE_ID_CONTATO) = Nothing
        ViewState(CHAVE_ID) = Nothing
        ViewState(CHAVE_USUARIO_CADASTROU) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaSolicitacao, Control))
        UtilidadesWeb.LimparComponente(CType(pnlContato, Control))
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        LimpaDados()

        Dim Solicitacao As ISolicitacaoDeAudiencia

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Solicitacao = Servico.ObtenhaSolicitacaoDeAudiencia(Id)
        End Using

        If Solicitacao Is Nothing Then Exit Sub

        txtAssunto.Text = Solicitacao.Assunto
        txtDescricao.Text = Solicitacao.Descricao
        cboContato.Text = Solicitacao.Contato.Pessoa.Nome

        ViewState(CHAVE_DATA) = Solicitacao.DataDaSolicitacao
        ViewState(CHAVE_STATUS) = Solicitacao.Ativa
        ViewState(CHAVE_ID_CONTATO) = Solicitacao.Contato.Pessoa.ID
        ViewState(CHAVE_ID) = Solicitacao.ID
        ViewState(CHAVE_USUARIO_CADASTROU) = Solicitacao.UsuarioQueCadastrou
        cboContato.Enabled = False
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Private Sub btnSalva_Click()
        Dim Mensagem As String = ""
        Dim Solicitacao As ISolicitacaoDeAudiencia
        Dim Inconsistencia As String

        Inconsistencia = ValidaLancamento()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Solicitacao = MontaObjeto()

        Try
            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Solicitacao)
                    Mensagem = "Solicitação de audiência cadastrada com sucesso."
                Else
                    Servico.Modificar(Solicitacao)
                    Mensagem = "Solicitação de audiência modificada com sucesso."
                End If

            End Using

            ExibaTelaNovo()

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As ISolicitacaoDeAudiencia
        Dim Solicitacao As ISolicitacaoDeAudiencia
        Dim Contato As IContato
        Dim DataDaSolicitacao As Date

        Solicitacao = FabricaGenerica.GetInstancia.CrieObjeto(Of ISolicitacaoDeAudiencia)()

        Using ServicoDeContato As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contato = ServicoDeContato.Obtenha(CLng(ViewState(CHAVE_ID_CONTATO)))
        End Using

        Solicitacao.Assunto = txtAssunto.Text
        Solicitacao.Descricao = txtDescricao.Text
        Solicitacao.Contato = Contato

        If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
            DataDaSolicitacao = Now
            Solicitacao.Ativa = True
            Solicitacao.UsuarioQueCadastrou = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Else
            DataDaSolicitacao = CDate(ViewState(CHAVE_DATA))
            Solicitacao.ID = CType(ViewState(CHAVE_ID), Long?)
            Solicitacao.Ativa = CBool(ViewState(CHAVE_STATUS))
            Solicitacao.UsuarioQueCadastrou = CType(ViewState(CHAVE_USUARIO_CADASTROU), Usuario)
        End If

        Solicitacao.DataDaSolicitacao = DataDaSolicitacao

        Return Solicitacao
    End Function

    Private Function ValidaLancamento() As String
        If String.IsNullOrEmpty(CStr(ViewState(CHAVE_ID_CONTATO))) Then Return "O contato deve ser informado."
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto da solicitação de audiência deve ser informado."
        If String.IsNullOrEmpty(txtDescricao.Text) Then Return "A descrição da solicitação de audiência deve ser informada."
        Return Nothing
    End Function

    Private Sub cboContato_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboContato.ItemsRequested
        Dim Contatos As IList(Of IContato)

        Using Servico As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contatos = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Contatos Is Nothing Then
            For Each Contato As IContato In Contatos
                Dim Item As New RadComboBoxItem(Contato.Pessoa.Nome, Contato.Pessoa.ID.ToString)

                Dim TelefonesResidencial As IList(Of ITelefone)
                Dim TelefonesCelular As IList(Of ITelefone)
                Dim TelefonesComercial As IList(Of ITelefone)

                TelefonesResidencial = Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Residencial)
                TelefonesCelular = Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Celular)
                TelefonesComercial = Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Comercial)

                Dim TelefonesSTR As New StringBuilder

                For Each Telefone As ITelefone In TelefonesResidencial
                    TelefonesSTR.AppendLine(Telefone.ToString)
                Next

                For Each Telefone As ITelefone In TelefonesComercial
                    TelefonesSTR.AppendLine(Telefone.ToString)
                Next

                Item.Attributes.Add("Telefone", TelefonesSTR.ToString)

                Dim CelularesSTR As New StringBuilder

                For Each Celular As ITelefone In TelefonesCelular
                    CelularesSTR.AppendLine(Celular.ToString)
                Next

                Item.Attributes.Add("Celular", CelularesSTR.ToString)

                If Not String.IsNullOrEmpty(Contato.Cargo) Then
                    Item.Attributes.Add("Cargo", Contato.Cargo)
                Else
                    Item.Attributes.Add("Cargo", "")
                End If

                cboContato.Items.Add(Item)
                Item.DataBind()
            Next
        End If


    End Sub

    Private Sub cboContato_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContato.SelectedIndexChanged
        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then Exit Sub

        ViewState(CHAVE_ID_CONTATO) = CLng(DirectCast(o, RadComboBox).SelectedValue)
    End Sub

End Class