Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class cdCompromisso
    Inherits System.Web.UI.Page

    Private Const CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_COMPROMISSO"
    Private Const CHAVE_ID_COMPROMISSO As String = "CHAVE_ID_COMPROMISSO"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO_COMPROMISSO"

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim IdCompromisso As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("IdProprietario")) Then
                Session(CHAVE_ID_PROPRIETARIO) = CLng(Request.QueryString("IdProprietario"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("IdCompromisso")) Then
                IdCompromisso = CLng(Request.QueryString("IdCompromisso"))
            End If

            If IdCompromisso Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes(IdCompromisso.Value)
            End If
        End If
    End Sub

    Private Sub ExibaTelaNovo()
        Session(CHAVE_ESTADO) = Estado.Novo
        LimpaDados()
    End Sub

    Private Sub LimpaDados()
        Session(CHAVE_ID_COMPROMISSO) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoCompromisso, Control))
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        Session(CHAVE_ESTADO) = Estado.Modifica
        LimpaDados()

        Dim Compromisso As ICompromisso

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromisso = Servico.ObtenhaCompromisso(Id)
        End Using

        If Compromisso Is Nothing Then Exit Sub
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Private Sub btnSalva_Click()
        Dim Mensagem As String = ""
        Dim Compromisso As ICompromisso
        Dim Inconsistencia As String

        Inconsistencia = ConsisteDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Compromisso = MontaObjeto()

        Try
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.InsiraCompromisso(Compromisso)
                    Mensagem = "Compromisso cadastrado com sucesso."
                Else
                    Servico.ModifiqueCompromisso(Compromisso)
                    Mensagem = "Compromisso modificado com sucesso."
                End If

            End Using

            ExibaTelaNovo()

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As ICompromisso
        Dim Compromisso As ICompromisso

        Compromisso = FabricaGenerica.GetInstancia.CrieObjeto(Of ICompromisso)()
        Compromisso.Assunto = txtAssunto.Text
        Compromisso.Descricao = txtDescricao.Text
        Compromisso.Fim = txtDataHorarioFim.SelectedDate.Value
        Compromisso.Inicio = txtDataHorarioInicio.SelectedDate.Value
        Compromisso.Local = txtLocal.Text
        Compromisso.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(CLng(Session(CHAVE_ID_PROPRIETARIO)))

        If CByte(Session(CHAVE_ESTADO)) = Estado.Modifica Then
            Compromisso.ID = CLng(Session(CHAVE_ID_COMPROMISSO))
        End If

        Return Compromisso
    End Function

    Private Function ConsisteDados() As String
        If String.IsNullOrEmpty(txtAssunto.Text) Then Return "O assunto do compromisso deve ser informado."
        If Not txtDataHorarioInicio.SelectedDate.HasValue Then Return "A data e horário de início do compromisso devem ser informados."
        If Not txtDataHorarioFim.SelectedDate.HasValue Then Return "A data e horário de fim do compromisso devem ser informados."
        Return Nothing
    End Function

End Class