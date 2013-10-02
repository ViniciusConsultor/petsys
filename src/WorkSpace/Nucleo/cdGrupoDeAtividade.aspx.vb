Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados

Public Class cdGrupoDeAtividade
    Inherits SuperPagina

    Private CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE As String = "CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE"
    Private CHAVE_ID_GRUPO_ATIVIDADE As String = "CHAVE_ID_GRUPO_ATIVIDADE"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.016"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoGrupoDeAtividade, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoGrupoDeAtividade, Control), False)

        ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE) = Estado.Inicial
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE) = Estado.Novo
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoGrupoDeAtividade, Control), True)
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoGrupoDeAtividade, Control), True)
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE) = Estado.Remove
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoGrupoDeAtividade, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function MontaObjeto() As IGrupoDeAtividade
        Dim GrupoDeAtividade As IGrupoDeAtividade

        GrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeAtividade)()

        If CByte(ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE)) <> Estado.Novo Then
            GrupoDeAtividade.ID = CLng(ViewState(CHAVE_ID_GRUPO_ATIVIDADE))
        End If

        GrupoDeAtividade.Nome = ctrlGrupo1.NomeDoGrupo

        Return GrupoDeAtividade
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String
        Dim GrupoDeAtividade As IGrupoDeAtividade = MontaObjeto()

        Try
            Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
                If CByte(ViewState(CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE)) = Estado.Novo Then
                    Servico.Insira(GrupoDeAtividade)
                    Mensagem = "Grupo de atividade cadastrado com sucesso."
                Else
                    Servico.Modificar(GrupoDeAtividade)
                    Mensagem = "Grupo de atividade modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnModificar_Click()
        ExibaTelaModificar()
    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluir()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
                Servico.Remover(CLng(ViewState(CHAVE_ID_GRUPO_ATIVIDADE)))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Grupo de atividade excluido com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnModificar"
                Call btnModificar_Click()
            Case "btnExcluir"
                Call btnExclui_Click()
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
            Case "btnSim"
                Call btnSim_Click()
            Case "btnNao"
                Call btnNao_Click()
        End Select
    End Sub

    'Private Sub Obtenha(ByVal Pessoa As IPessoa)
    '    Dim Cliente As ICliente

    '    ctrlPessoa1.BotaoDetalharEhVisivel = True

    '    Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCliente)()
    '        Cliente = Servico.Obtenha(Pessoa)
    '    End Using

    '    If Cliente Is Nothing Then
    '        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
    '        Exit Sub
    '    End If

    '    MostreCliente(Cliente)
    '    ExibaTelaConsultar()
    'End Sub

    Private Sub Mostre(ByVal GrupoDeAtividade As IGrupoDeAtividade)
        'txtNome.Text = GrupoDeAtividade.Nome
        ViewState(CHAVE_ID_GRUPO_ATIVIDADE) = GrupoDeAtividade.ID
    End Sub

End Class