Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados

Partial Public Class frmAgendaPessoal
    Inherits SuperPagina

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_AGENDAPESSOAL"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.012"
    End Function

    Private Sub ExibaTelaInicial()
        MontaAgenda()
    End Sub

    Private Sub MontaAgenda()
        Dim Agenda As IAgenda
        Dim UsuarioLogado As Usuario

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Agenda = Servico.ObtenhaAgenda(UsuarioLogado.ID)
        End Using

        If Agenda Is Nothing Then
            ctrlAgenda1.Visible = False
            lblInconsistencia.Visible = True

            lblInconsistencia.Text = "Agenda ainda não configurada. Para configurar a agenda acesse o cadastro de agenda."
            Exit Sub
        End If
        lblInconsistencia.Visible = False
        ctrlAgenda1.Visible = True
        ctrlAgenda1.IDProprietario = UsuarioLogado.ID
        ctrlAgenda1.HoraInicio = Agenda.HorarioDeInicio
        ctrlAgenda1.HoraFim = Agenda.HorarioDeTermino
        ctrlAgenda1.IntervaloEntreCompromissos = Agenda.IntervaloEntreOsCompromissos
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ctrlAgenda1.CarregaAgenda()
    End Sub

End Class