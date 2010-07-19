Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class ctrlDespachoAgenda
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA As String = "CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA"
    Private Const CHAVE_SOLICITACAO_DESPACHO_AGENDA As String = "CHAVE_SOLICITACAO_DESPACHO_AGENDA"

    Public Event SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnAdicionarDespacho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarDespacho.Click
        Dim IDCompromisso As Long
        Dim Despacho As IDespachoAgenda

        Despacho = MontaDespacho()

        Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            IDCompromisso = ServicoDeAgenda.InsiraCompromisso(Despacho.Compromisso)
        End Using

        Despacho.Compromisso.ID = IDCompromisso

        Using ServicoDeDespacho As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
            ServicoDeDespacho.Inserir(Despacho)
        End Using

        RaiseEvent SolicitacaoFoiDespachada(Despacho)
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
        Compromisso.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(ViewState(CHAVE_ID_PROPRIETARIO_DESPACHO_AGENDA)))
        Compromisso.Local = txtLocal.Text

        Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoAgenda)()
        Despacho.Compromisso = Compromisso
        Despacho.Responsavel = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)
        Despacho.Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA), ISolicitacao)

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