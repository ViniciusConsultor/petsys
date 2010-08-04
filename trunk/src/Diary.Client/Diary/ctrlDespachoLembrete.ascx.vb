Imports Diary.Interfaces.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Diary.Interfaces.Servicos
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class ctrlDespachoLembrete
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_ALVO_DESPACHO_LEMBRETE As String = "CHAVE_ID_ALVO_DESPACHO_LEMBRETE"
    Private Const CHAVE_SOLICITACAO_DESPACHO_LEMBRETE As String = "CHAVE_SOLICITACAO_DESPACHO_LEMBRETE"
    Private Const CHAVE_TIPO_DESPACHO_LEMBRETE As String = "CHAVE_TIPO_DESPACHO_LEMBRETE"

    Public Event SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnAdicionarDespacho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarDespacho.Click
        Dim IDLembrete As Long
        Dim Despacho As IDespachoLembrete
        Dim Inconsistencia As String

        Inconsistencia = ValidaDespacho()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Despacho = MontaDespacho()

        Try
            Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                IDLembrete = ServicoDeAgenda.InsiraLembrete(Despacho.Lembrete)
            End Using

            Despacho.Lembrete.ID = IDLembrete

            Using ServicoDeDespacho As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
                ServicoDeDespacho.Inserir(Despacho)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Solicitação despachada com sucesso."), False)

            RaiseEvent SolicitacaoFoiDespachada(Despacho)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaDespacho() As IDespachoLembrete
        Dim Despacho As IDespachoLembrete
        Dim Lembrete As ILembrete
        Dim UsuarioLogado As Usuario

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Lembrete = FabricaGenerica.GetInstancia.CrieObjeto(Of ILembrete)()
        Lembrete.Assunto = txtAssunto.Text
        Lembrete.Descricao = txtDescricao.Text
        Lembrete.Fim = txtDataHorarioFim.SelectedDate.Value
        Lembrete.Inicio = txtDataHorarioInicio.SelectedDate.Value
        Lembrete.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_LEMBRETE)))
        Lembrete.Local = txtLocal.Text
        Lembrete.Status = StatusDoCompromisso.Pendente

        Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoLembrete)()
        Despacho.Lembrete = Lembrete
        Despacho.Solicitante = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)
        Despacho.Alvo = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_LEMBRETE)))
        Despacho.Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_LEMBRETE), ISolicitacao)
        Despacho.Tipo = CType(ViewState(CHAVE_TIPO_DESPACHO_LEMBRETE), TipoDeDespacho)
        Despacho.DataDoDespacho = Now
        Return Despacho
    End Function

    Private Function ValidaDespacho() As String
        If ViewState(CHAVE_ID_ALVO_DESPACHO_LEMBRETE) Is Nothing Then Return "O alvo do despacho deve ser informado."
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e hora de inicio devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e hora final devem ser informados."

        Return Nothing
    End Function

    Public WriteOnly Property IDAlvo() As Long
        Set(ByVal value As Long)
            ViewState(CHAVE_ID_ALVO_DESPACHO_LEMBRETE) = value
        End Set
    End Property

    Public WriteOnly Property Solicitacao() As ISolicitacao
        Set(ByVal value As ISolicitacao)
            ViewState(CHAVE_SOLICITACAO_DESPACHO_LEMBRETE) = value
        End Set
    End Property

    Public WriteOnly Property TipoDespacho() As TipoDeDespacho
        Set(ByVal value As TipoDeDespacho)
            ViewState(CHAVE_TIPO_DESPACHO_LEMBRETE) = value
        End Set
    End Property

    Public WriteOnly Property Local() As String
        Set(ByVal value As String)
            txtLocal.Text = value
        End Set
    End Property

    Public WriteOnly Property Assunto() As String
        Set(ByVal value As String)
            txtAssunto.Text = value
        End Set
    End Property

    Public WriteOnly Property Descricao() As String
        Set(ByVal value As String)
            txtDescricao.Text = value
        End Set
    End Property

    Public WriteOnly Property Inicio() As Date
        Set(ByVal value As Date)
            txtDataHorarioInicio.SelectedDate = value
        End Set
    End Property

End Class