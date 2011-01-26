Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports PetSys.Interfaces.Servicos
Imports Compartilhados
Imports System.IO

Partial Public Class cdAnimal
    Inherits SuperPagina

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_ANIMAL"
    Private ID_OBJETO As String = "ID_OBJETO_CD_ANIMAL"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler crtlAnimal1.AnimalFoiSelecionado, AddressOf MostreAnimal

        If Not IsPostBack Then
            Me.ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.PET.002"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        Dim Animal As IAnimal

        Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
            Animal = Servico.ObtenhaAnimal(Id)
        End Using

        If Not Animal Is Nothing Then
            Me.MostreAnimal(Animal)
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False

        CType(toolBarRodape.FindButtonByCommandName("btnVacinas"), RadToolBarButton).Visible = False
        CType(toolBarRodape.FindButtonByCommandName("btnAtendimentos"), RadToolBarButton).Visible = False

        UtilidadesWeb.LimparComponente(CType(pnlDadosDoAnimal, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAnimal, Control), False)

        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = False
        ctrlPessoa1.EhEditavel = False
        ctrlPessoa1.EhObrigatorio = False

        crtlAnimal1.Inicializa()
        crtlAnimal1.BotaoDetalharEhVisivel = False
        crtlAnimal1.BotaoNovoEhVisivel = False
        crtlAnimal1.EnableLoadOnDemand = True
        crtlAnimal1.ShowDropDownOnTextboxClick = True
        crtlAnimal1.AutoPostBack = True
        crtlAnimal1.EhObrigatorio = False

        imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO

        Session(CHAVE_ESTADO) = Estado.Inicial
        Session(ID_OBJETO) = Nothing
        CarregaSexo()
        CarregaEspecie()
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub CarregaSexo()
        Me.rblSexo.Items.Clear()

        For Each Sexo As SexoDoAnimal In SexoDoAnimal.ObtenhaTodos
            rblSexo.Items.Add(New ListItem(Sexo.Descricao, Sexo.ID.ToString))
        Next
    End Sub

    Private Sub CarregaEspecie()
        Me.cboEspecie.Items.Clear()

        For Each Especie As Especie In Especie.ObtenhaTodos
            Me.cboEspecie.Items.Add(New RadComboBoxItem(Especie.Descricao, Especie.ID.ToString))
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

        CType(toolBarRodape.FindButtonByCommandName("btnVacinas"), RadToolBarButton).Visible = False
        CType(toolBarRodape.FindButtonByCommandName("btnAtendimentos"), RadToolBarButton).Visible = False

        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAnimal, Control), True)
        Session(CHAVE_ESTADO) = Estado.Novo

        crtlAnimal1.Inicializa()
        crtlAnimal1.EnableLoadOnDemand = False
        crtlAnimal1.ShowDropDownOnTextboxClick = False
        crtlAnimal1.AutoPostBack = False
        crtlAnimal1.EhObrigatorio = True
        crtlAnimal1.TextoItemVazio = String.Empty

        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ctrlPessoa1.EhEditavel = True
        ctrlPessoa1.EhObrigatorio = True
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False

        CType(toolBarRodape.FindButtonByCommandName("btnVacinas"), RadToolBarButton).Visible = True
        CType(toolBarRodape.FindButtonByCommandName("btnAtendimentos"), RadToolBarButton).Visible = True

        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAnimal, Control), True)
        Session(CHAVE_ESTADO) = Estado.Modifica
        crtlAnimal1.EnableLoadOnDemand = False
        crtlAnimal1.ShowDropDownOnTextboxClick = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True

        CType(toolBarRodape.FindButtonByCommandName("btnVacinas"), RadToolBarButton).Visible = True
        CType(toolBarRodape.FindButtonByCommandName("btnAtendimentos"), RadToolBarButton).Visible = True

        Session(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAnimal, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False

        CType(toolBarRodape.FindButtonByCommandName("btnVacinas"), RadToolBarButton).Visible = True
        CType(toolBarRodape.FindButtonByCommandName("btnAtendimentos"), RadToolBarButton).Visible = True
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function MontaObjetoAnimal() As IAnimal
        Dim Animal As IAnimal
        Dim Proprietario As IProprietarioDeAnimal

        Animal = FabricaGenerica.GetInstancia.CrieObjeto(Of IAnimal)()

        If CByte(Session(CHAVE_ESTADO)) <> Estado.Novo Then
            Animal.ID = CLng(Session(ID_OBJETO))
        End If

        Animal.Nome = crtlAnimal1.NomeDoAnimal
        Animal.Raca = txtRaca.Text
        Animal.DataDeNascimento = txtDataDeNascimento.SelectedDate
        Animal.Especie = Especie.Obtenha(CChar(cboEspecie.SelectedValue))
        Animal.Sexo = SexoDoAnimal.Obtenha(CChar(rblSexo.SelectedValue))
        Animal.Foto = imgFoto.ImageUrl
        Proprietario = FabricaGenerica.GetInstancia.CrieObjeto(Of IProprietarioDeAnimal)(New Object() {ctrlPessoa1.PessoaSelecionada})
        Animal.Proprietario = Proprietario

        Return Animal
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String
        Dim Animal As IAnimal = MontaObjetoAnimal()

        Try
            Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Animal)
                    Mensagem = "Animal cadastrado com sucesso."
                Else
                    Servico.Modificar(Animal)
                    Mensagem = "Animal modificado com sucesso."
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
            Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
                Servico.Excluir(CLng(Session(ID_OBJETO)))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Animal excluido com sucesso."), False)
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

    Private Sub MostreAnimal(ByVal Animal As IAnimal)
        Session(ID_OBJETO) = Animal.ID
        Me.txtDataDeNascimento.SelectedDate = Animal.DataDeNascimento
        Me.cboEspecie.SelectedValue = Animal.Especie.ID.ToString
        crtlAnimal1.NomeDoAnimal = Animal.Nome
        txtRaca.Text = Animal.Raca
        rblSexo.SelectedValue = Animal.Sexo.ID.ToString
        ctrlPessoa1.PessoaSelecionada = Animal.Proprietario.Pessoa
        imgFoto.ImageUrl = Animal.Foto
        ExibaTelaConsultar()
    End Sub

    Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSubmit.Click
        If uplFoto.UploadedFiles.Count > 0 Then
            For Each validFile As UploadedFile In uplFoto.UploadedFiles
                Dim PastaDeDestino As String = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_FOTO_ANIMAL)
                validFile.SaveAs(Path.Combine(PastaDeDestino, validFile.GetName()), True)
                UtilidadesWeb.redimensionaImagem(PastaDeDestino, _
                                                  validFile.GetName(), _
                                                  200, _
                                                  200)
                imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_FOTO_ANIMAL, "/" & validFile.GetName())
            Next
        End If
    End Sub

    Private Sub toolBarRodape_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles toolBarRodape.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnVacinas"
                Call btnVacinas_Click()
            Case "btnAtendimentos"
                Call btnAtendimentos_Click()
        End Select
    End Sub

    Private Sub btnVacinas_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/frmVacinas.aspx", "?Id=", CLng(Session(ID_OBJETO)))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Vacinas do animal", 650, 450), False)
    End Sub

    Private Sub btnAtendimentos_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "PetSys/frmAtendimentoAnimal.aspx", "?Id=", CLng(Session(ID_OBJETO)))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Atendimentos do animal", 650, 450), False)
    End Sub

End Class