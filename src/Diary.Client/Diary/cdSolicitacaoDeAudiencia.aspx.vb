Imports Diary.Interfaces.Negocio
Imports Compartilhados.Componentes.Web
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados

Partial Public Class cdSolicitacaoDeAudiencia
    Inherits System.Web.UI.Page

    Private Const CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_SOLICITACAO_AUDIENCIA"
    Private Const CHAVE_ID As String = "CHAVE_ID_SOLICITACAO_DE_AUDIENCIA"
    Private Const CHAVE_DATA As String = "CHAVE_DATA_SOLICITACAO_DE_AUDIENCIA"

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Solicitacao)
                    Mensagem = "Solicitação de audiência cadastrada com sucesso."
                Else
                    Servico.Modificar(Solicitacao)
                    Mensagem = "Solicitação de audiência modificada com sucesso."
                End If
            End Using

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
            Contato = ServicoDeContato.Obtenha(CLng(cboContato.SelectedValue))
        End Using

        Solicitacao.Assunto = txtAssunto.Text
        Solicitacao.Descricao = txtDescricao.Text
        Solicitacao.Contato = Contato

        If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
            DataDaSolicitacao = Now
        Else
            DataDaSolicitacao = CDate(Session(CHAVE_DATA))
            Solicitacao.ID = CType(Session(CHAVE_ID), Long?)
        End If

        Solicitacao.DataDaSolicitacao = DataDaSolicitacao

        Return Solicitacao
    End Function

    Private Function ValidaLancamento() As String
        If String.IsNullOrEmpty(cboContato.SelectedValue) Then Return "O contato deve ser informado."
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
                cboContato.Items.Add(New RadComboBoxItem(Contato.Pessoa.Nome, Contato.Pessoa.ID.Value.ToString))
            Next
        End If
    End Sub

End Class