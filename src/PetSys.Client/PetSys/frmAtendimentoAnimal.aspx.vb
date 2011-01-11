﻿Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class frmAtendimentoAnimal
    Inherits SuperPagina

    Private Const CHAVE_HISTORICO_ATENDIMENTOS As String = "CHAVE_HISTORICO_ATENDIMENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler crtlAnimal1.AnimalFoiSelecionado, AddressOf AnimalFoiSelecionado

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True

        crtlAnimal1.Inicializa()
        crtlAnimal1.BotaoDetalharEhVisivel = False
        crtlAnimal1.BotaoNovoEhVisivel = True
        crtlAnimal1.EnableLoadOnDemand = True
        crtlAnimal1.ShowDropDownOnTextboxClick = True
        crtlAnimal1.AutoPostBack = True
        crtlAnimal1.EhObrigatorio = False

        Dim Atendimentos As IList(Of IAtendimento) = New List(Of IAtendimento)

        ExibaAtendimentosHistoricos(Atendimentos)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.PET.003"
    End Function

    Private Sub AnimalFoiSelecionado(ByVal Animal As IAnimal)
        Dim Atendimentos As IList(Of IAtendimento)

        Using Servico As IServicoDeAtendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimento)()
            Atendimentos = Servico.ObtenhaAtendimentos(Animal)
        End Using

        crtlAnimal1.BotaoDetalharEhVisivel = True
        ExibaAtendimentosHistoricos(Atendimentos)
    End Sub

    Private Sub ExibaAtendimentosHistoricos(ByVal Atendimentos As IList(Of IAtendimento))
        UtilidadesWeb.LimparComponente(CType(pnlHistoricoDeAtendimentos, Control))

        ViewState(CHAVE_HISTORICO_ATENDIMENTOS) = Atendimentos
        grdAtendimentos.DataSource = Atendimentos
        grdAtendimentos.DataBind()
    End Sub

    Private Sub grdAtendimentos_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdAtendimentos.ItemCommand
        Dim ID As Long
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            ID = CLng(e.Item.Cells(4).Text)
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Atendimentos As IList(Of IAtendimento)
            Atendimentos = CType(ViewState(CHAVE_HISTORICO_ATENDIMENTOS), IList(Of IAtendimento))
            Atendimentos.RemoveAt(IndiceSelecionado)
            ExibaAtendimentosHistoricos(Atendimentos)

            Using Servico As IServicoDeAtendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimento)()
                Servico.Excluir(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/cdAtendimento.aspx", "?Id=", ID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastrar atendimento", 650, 450), False)
        End If
    End Sub

    Protected Sub btnNovo_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/cdAtendimento.aspx")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastrar atendimento", 650, 450), False)
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
        End Select
    End Sub

    Private Sub grdAtendimentos_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdAtendimentos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdAtendimentos, ViewState(CHAVE_HISTORICO_ATENDIMENTOS), e)
    End Sub
End Class