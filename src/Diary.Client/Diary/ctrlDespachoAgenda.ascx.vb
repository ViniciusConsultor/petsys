Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class ctrlDespachoAgenda
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_ID_ALVO_DESPACHO_AGENDA As String = "CHAVE_ID_ALVO_DESPACHO_AGENDA"
    Private Const CHAVE_SOLICITACAO_DESPACHO_AGENDA As String = "CHAVE_SOLICITACAO_DESPACHO_AGENDA"
    Private Const CHAVE_TIPO_DESPACHO_AGENDA As String = "CHAVE_TIPO_DESPACHO_AGENDA"

    Public Event SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            AlimentaDados()
        End If
    End Sub

    Private Sub AlimentaDados()
        Dim Solicitacao As ISolicitacao

        Solicitacao = CType(ViewState(CHAVE_SOLICITACAO_DESPACHO_AGENDA), ISolicitacao)

        If Solicitacao Is Nothing Then Exit Sub

        If Solicitacao.Tipo.Equals(TipoDeSolicitacao.Audiencia) Then
            txtAssunto.Text = CType(Solicitacao, ISolicitacaoDeAudiencia).Assunto
        ElseIf Solicitacao.Tipo.Equals(TipoDeSolicitacao.Convite) Then
            txtAssunto.Text = "Convite"
            txtLocal.Text = CType(Solicitacao, ISolicitacaoDeConvite).Local
            txtDataHorarioInicio.SelectedDate = CType(Solicitacao, ISolicitacaoDeConvite).DataEHorario
        End If

        Dim DescricaoDaSoliticao As New StringBuilder
        'Nome do contato da solicitação
        DescricaoDaSoliticao.AppendLine(Solicitacao.Contato.Pessoa.Nome)

        'Cargo do contato
        If Not String.IsNullOrEmpty(Solicitacao.Contato.Cargo) Then
            DescricaoDaSoliticao.AppendLine(Solicitacao.Contato.Cargo)
        End If

        'Telefones do contato
        Dim TelefonesSTR As New StringBuilder

        Dim TelefonesResidencial As IList(Of ITelefone)
        Dim TelefonesComercial As IList(Of ITelefone)
        Dim TelefonesCelular As IList(Of ITelefone)

        TelefonesResidencial = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Residencial)
        TelefonesComercial = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Comercial)
        TelefonesCelular = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Celular)

        For Each Telefone As ITelefone In TelefonesResidencial
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        For Each Telefone As ITelefone In TelefonesComercial
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        For Each Telefone As ITelefone In TelefonesCelular
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        'Se tiver telefones
        If Not TelefonesSTR.Length = 0 Then
            DescricaoDaSoliticao.AppendLine(TelefonesSTR.ToString)
        End If

        'Descrição da solicitação
        If Not String.IsNullOrEmpty(Solicitacao.Descricao) Then
            DescricaoDaSoliticao.AppendLine(Solicitacao.Descricao)
        End If

        'Observação da solicitação de convite
        If Solicitacao.Tipo.Equals(TipoDeSolicitacao.Convite) Then
            If String.IsNullOrEmpty(CType(Solicitacao, ISolicitacaoDeConvite).Observacao) Then
                DescricaoDaSoliticao.AppendLine(CType(Solicitacao, ISolicitacaoDeConvite).Observacao)
            End If
        End If

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

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Solicitação despachada com sucesso."), False)

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