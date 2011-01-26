Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos

Partial Public Class cdVeterinario
    Inherits SuperPagina

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_VETERINARIO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ObtenhaVeterinario

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.PET.001"
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
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoVeterinario, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), False)

        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ctrlPessoa1.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa1.SetaTipoDePessoaPadrao(TipoDePessoa.Fisica)
        Session(CHAVE_ESTADO) = Estado.Inicial
        CarregaUF()
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub CarregaUF()
        Me.cboUF.Items.Clear()

        For Each UF As UF In UF.ObtenhaTodos
            cboUF.Items.Add(New RadComboBoxItem(UF.Nome, UF.ID.ToString))
        Next
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), True)
        Session(CHAVE_ESTADO) = Estado.Novo
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), True)
        Session(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        Session(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoVeterinario, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function MontaObjetoVeterinario() As IVeterinario
        Dim Veterinario As IVeterinario
        Dim Pessoa As IPessoa

        Pessoa = ctrlPessoa1.PessoaSelecionada
        Veterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinario)((New Object() {Pessoa}))
        Veterinario.CRMV = txtCRMV.Text
        Veterinario.UF = UF.Obtenha(CShort(cboUF.SelectedValue))

        Return Veterinario
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String
        Dim Veterinario As IVeterinario = MontaObjetoVeterinario()

        Try
            Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Veterinario)
                    Mensagem = "Veterinário cadastrado com sucesso."
                Else
                    Servico.Modificar(Veterinario)
                    Mensagem = "Veterinário modificado com sucesso."
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
            Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
                Servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Veterinário excluido com sucesso."), False)
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

    Private Sub ObtenhaVeterinario(ByVal Pessoa As IPessoa)
        Dim Veterinario As IVeterinario

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
            Veterinario = Servico.Obtenha(Pessoa)
        End Using

        If Veterinario Is Nothing Then
            CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
            Exit Sub
        End If

        MostreVeterinario(Veterinario)
        ExibaTelaConsultar()
    End Sub

    Private Sub MostreVeterinario(ByVal Veterinario As IVeterinario)
        Me.txtCRMV.Text = Veterinario.CRMV
        Me.cboUF.SelectedValue = Veterinario.UF.ID.ToString
    End Sub

End Class