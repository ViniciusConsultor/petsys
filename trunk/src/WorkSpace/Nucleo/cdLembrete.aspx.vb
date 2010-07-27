Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados

Partial Public Class cdLembrete
    Inherits SuperPagina

    Private Const CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_LEMBRETE"
    Private Const CHAVE_ID_LEMBRETE As String = "CHAVE_ID_LEMBRETE"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO_LEMBRETE"

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim IdLembrete As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("IdProprietario")) Then
                ViewState(CHAVE_ID_PROPRIETARIO) = CLng(Request.QueryString("IdProprietario"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("IdLembrete")) Then
                IdLembrete = CLng(Request.QueryString("IdLembrete"))
            End If

            If IdLembrete Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes(IdLembrete.Value)
            End If
        End If
    End Sub

    Private Sub ExibaTelaNovo()
        ViewState(CHAVE_ESTADO) = Estado.Novo
        LimpaDados()
    End Sub

    Private Sub LimpaDados()
        ViewState(CHAVE_ID_LEMBRETE) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoLembrete, Control))
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        LimpaDados()

        Dim Lembrete As ILembrete

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Lembrete = Servico.ObtenhaLembrete(Id)
        End Using

        If Lembrete Is Nothing Then Exit Sub

        txtAssunto.Text = Lembrete.Assunto
        txtDescricao.Text = Lembrete.Descricao
        txtDataHorarioFim.SelectedDate = Lembrete.Fim
        txtDataHorarioInicio.SelectedDate = Lembrete.Inicio
        txtLocal.Text = Lembrete.Local
        ViewState(CHAVE_ID_PROPRIETARIO) = Lembrete.Proprietario.ID
        ViewState(CHAVE_ID_LEMBRETE) = Lembrete.ID
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Private Sub btnSalva_Click()
        Dim Mensagem As String = ""
        Dim Lembrete As ILembrete
        Dim Inconsistencia As String

        Inconsistencia = ConsisteDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Lembrete = MontaObjeto()

        Try
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.InsiraLembrete(Lembrete)
                    Mensagem = "Lembrete cadastrado com sucesso."
                Else
                    Servico.ModifiqueLembrete(Lembrete)
                    Mensagem = "Lembrete modificado com sucesso."
                End If

            End Using

            ExibaTelaNovo()

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As ILembrete
        Dim Lembrete As ILembrete

        Lembrete = FabricaGenerica.GetInstancia.CrieObjeto(Of ILembrete)()
        Lembrete.Assunto = txtAssunto.Text
        Lembrete.Descricao = txtDescricao.Text
        Lembrete.Fim = txtDataHorarioFim.SelectedDate.Value
        Lembrete.Inicio = txtDataHorarioInicio.SelectedDate.Value
        Lembrete.Local = txtLocal.Text
        Lembrete.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_PROPRIETARIO)))

        If CByte(ViewState(CHAVE_ESTADO)) = Estado.Modifica Then
            Lembrete.ID = CLng(ViewState(CHAVE_ID_LEMBRETE))
        End If

        Return Lembrete
    End Function

    Private Function ConsisteDados() As String
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto do lembrete deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e horário de início do lembrete devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e horário de fim do lembrete devem ser informados."
        Return Nothing
    End Function

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0012")
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

End Class