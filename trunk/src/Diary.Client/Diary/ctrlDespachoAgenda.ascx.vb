Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Componentes.Web

Partial Public Class ctrlDespachoAgenda
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_ALVO_DESPACHO_AGENDA As String = "CHAVE_ID_ALVO_DESPACHO_AGENDA"
    Private Const CHAVE_SOLICITACAO_DESPACHO_AGENDA As String = "CHAVE_SOLICITACAO_DESPACHO_AGENDA"
    Private Const CHAVE_TIPO_DESPACHO_AGENDA As String = "CHAVE_TIPO_DESPACHO_AGENDA"

    Public Event SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnAdicionarDespacho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarDespacho.Click
        Dim IDCompromisso As Long
        Dim Despacho As IDespachoAgenda
        Dim Inconsistencia As String

        Inconsistencia = ValidaDespacho()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Despacho = MontaDespacho()

        Try
            Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                IDCompromisso = ServicoDeAgenda.InsiraCompromisso(Despacho.Compromisso)
            End Using

            Despacho.Compromisso.ID = IDCompromisso

            Using ServicoDeDespacho As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
                ServicoDeDespacho.Inserir(Despacho)
            End Using

            RaiseEvent SolicitacaoFoiDespachada(Despacho)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaDespacho() As IDespachoAgenda
        Dim Despacho As IDespachoAgenda
        Dim Compromisso As ICompromisso
        Dim UsuarioLogado As Usuario

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Compromisso = FabricaGenerica.GetInstancia.CrieObjeto(Of ICompromisso)()
        Compromisso.Assunto = txtAssunto.Text
        Compromisso.Descricao = txtDescricao.Text
        Compromisso.Fim = txtDataHorarioFim.SelectedDate.Value
        Compromisso.Inicio = txtDataHorarioInicio.SelectedDate.Value
        Compromisso.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_AGENDA)))
        Compromisso.Local = txtLocal.Text

        Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoAgenda)()
        Despacho.Compromisso = Compromisso
        Despacho.Solicitante = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)
        Despacho.Alvo = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_ALVO_DESPACHO_AGENDA)))
        Despacho.Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA), ISolicitacao)
        Despacho.Tipo = CType(ViewState(CHAVE_TIPO_DESPACHO_AGENDA), TipoDeDespacho)
        Despacho.DataDoDespacho = Now
        Return Despacho
    End Function

    Private Function ValidaDespacho() As String
        If ViewState(CHAVE_ID_ALVO_DESPACHO_AGENDA) Is Nothing Then Return "O alvo do despacho deve ser informado."
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e hora de inicio devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e hora final devem ser informados."

        Return Nothing
    End Function

    Public WriteOnly Property IDAlvo() As Long
        Set(ByVal value As Long)
            ViewState(CHAVE_ID_ALVO_DESPACHO_AGENDA) = value
        End Set
    End Property

    Public WriteOnly Property Solicitacao() As ISolicitacao
        Set(ByVal value As ISolicitacao)
            ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA) = value
        End Set
    End Property

    Public WriteOnly Property TipoDespacho() As TipoDeDespacho
        Set(ByVal value As TipoDeDespacho)
            ViewState(CHAVE_TIPO_DESPACHO_AGENDA) = value
        End Set
    End Property

End Class