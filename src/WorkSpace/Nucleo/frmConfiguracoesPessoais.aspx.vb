Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Visual
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Servicos
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class frmConfiguracoesPessoais
    Inherits SuperPagina

    Private CHAVE_ATALHOS_EXTERNOS As String = "CHAVE_ATALHOS_EXTERNOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        'Atalhos
        ViewState(CHAVE_ATALHOS_EXTERNOS) = New List(Of Atalho)
        ApresenteMenuParaEscolhaDosAtalhos()
        LimpaCamposDoAtalhoExterno()
        ExibaAtalhosExternos(New List(Of Atalho))

        If FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.UsuarioTemAtalhos Then
            ExibaAtalhos()
        End If

        'Papel de parede e tema
        Dim Perfil As Perfil
        Dim Usuario As Usuario

        Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Perfil = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil

        RadSkinManager1.Skin = Perfil.Skin
        imgPapelDeParede.ImageUrl = String.Concat("~/", Perfil.ImagemDesktop)

        'agenda
        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ctrlPessoa1.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa1.SetaTipoDePessoaPadrao(TipoDePessoa.Fisica)
        ctrlPessoa1.BotaoNovoEhVisivel = False

        Dim ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario

        Using ServicoDeAgenda As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            ConfiguracaoDaAgenda = ServicoDeAgenda.ObtenhaConfiguracao(Usuario.ID)
        End Using

        UtilidadesWeb.LimparComponente(CType(pnlDadosDaAgenda, Control))

        If ConfiguracaoDaAgenda Is Nothing Then
            chkHabilitaAgenda.Checked = False
            ctrlPessoa1.PessoaSelecionada = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(Usuario.ID)
            UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAgenda, Control), False)
            UtilidadesWeb.HabilitaComponentes(CType(pnlPessoaPadraoDaAgenda, Control), False)
        Else
            chkHabilitaAgenda.Checked = True
            ctrlPessoa1.PessoaSelecionada = ConfiguracaoDaAgenda.PessoaPadraoAoAcessarAAgenda
            UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAgenda, Control), True)
            UtilidadesWeb.HabilitaComponentes(CType(pnlPessoaPadraoDaAgenda, Control), True)
            txtHorarioDeInicio.SelectedDate = ConfiguracaoDaAgenda.HorarioDeInicio
            txtHorarioFinal.SelectedDate = ConfiguracaoDaAgenda.HorarioDeTermino
            txtIntervaloEntreCompromissos.SelectedDate = ConfiguracaoDaAgenda.IntervaloEntreOsCompromissos
        End If
    End Sub

    Private Sub ApresenteMenuParaEscolhaDosAtalhos()
        Dim Menu As IMenuComposto
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        Using Servico As IServicoDeMenu = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMenu)()
            Menu = Servico.ObtenhaMenu
        End Using

        Me.trwMenuDoUsuario.Nodes.Clear()

        For Each MenuModulo As IMenuComposto In Menu.ObtenhaItens
            If Principal.EstaAutorizado(MenuModulo.ID) Then
                Dim NodeModulo As RadTreeNode

                NodeModulo = New RadTreeNode(MenuModulo.Nome, MenuModulo.ID.ToString)
                PreenchaMenuFuncionalidadesDoModulo(MenuModulo, Principal, NodeModulo)
                Me.trwMenuDoUsuario.Nodes.Add(NodeModulo)
            End If
        Next
    End Sub

    Private Sub ExibaAtalhos()
        ExibaAtalhosExternos(FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ObtenhaAtalhosExternos)
        ExibaAtalhosDoSistema(FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ObtenhaAtalhosDoSistema)
    End Sub

    Private Sub ExibaAtalhosDoSistema(ByVal AtalhosDoSistema As IList(Of Atalho))
        For Each Atalho As Atalho In AtalhosDoSistema
            Dim No As RadTreeNode
            Dim IDAtalho As String = String.Concat(Atalho.ID.ToString, "|", Atalho.Imagem, "|", Atalho.URL)

            No = trwMenuDoUsuario.FindNodeByValue(IDAtalho)
            No.Expanded = True

            If Not No.ParentNode Is Nothing AndAlso Not No.Nodes Is Nothing AndAlso No.Nodes.Count = 0 Then
                No.Checked = True
            End If
        Next
    End Sub

    Private Sub PreenchaMenuFuncionalidadesDoModulo(ByVal MenuModulo As IMenuComposto, _
                                                    ByVal Principal As Principal, _
                                                    ByRef NodeModulo As RadTreeNode)
        For Each MenuFuncao As IMenuFolha In MenuModulo.ObtenhaItens

            If Principal.EstaAutorizado(MenuFuncao.ID) Then
                Dim NodeFuncao As RadTreeNode

                NodeFuncao = New RadTreeNode(MenuFuncao.Nome, String.Concat(MenuFuncao.ID.ToString, "|", MenuFuncao.Imagem, "|", MenuFuncao.URL))
                NodeModulo.Nodes.Add(NodeFuncao)
            End If
        Next
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.011"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnSalva_Click()
        Try
            SalveTemaEPapelDeParede()
            SalveAtalhos()
            SalveAgenda()
            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Configurações pessoais modificadas com sucesso."), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub SalveAtalhos()
        Dim Atalhos As New List(Of Atalho)
        Dim AtalhosExternos As IList(Of Atalho)

        AtalhosExternos = DirectCast(ViewState(CHAVE_ATALHOS_EXTERNOS), IList(Of Atalho))

        If Not AtalhosExternos.Count = 0 Then
            Atalhos.AddRange(AtalhosExternos)
        End If

        Dim AtalhosDoSistema As IList(Of Atalho) = New List(Of Atalho)
        Dim ItensSelecionados As IList(Of RadTreeNode)

        ItensSelecionados = trwMenuDoUsuario.CheckedNodes

        For Each No As RadTreeNode In ItensSelecionados
            If Not No.Value.Contains("MOD") Then
                Dim Atalho As Atalho
                Dim URL As String
                Dim ID As String
                Dim Imagem As String
                Dim Chave() As String

                Chave = Split(No.Value, "|")
                ID = Chave(0)
                Imagem = Chave(1)
                URL = Chave(2)

                Atalho = New AtalhoSistema(ID, No.Text, URL, Imagem)
                AtalhosDoSistema.Add(Atalho)
            End If
        Next

        If Not AtalhosDoSistema.Count = 0 Then
            Atalhos.AddRange(AtalhosDoSistema)
        End If

        Using Servico As IServicoDePerfil = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePerfil)()
            Servico.SalveAtalhos(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario, Atalhos)
        End Using
    End Sub

    Private Sub SalveTemaEPapelDeParede()
        Dim Perfil As Perfil
        Dim Usuario As Usuario

        Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Perfil = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil

        Perfil.Skin = RadSkinManager1.Skin
        Perfil.ImagemDesktop = imgPapelDeParede.ImageUrl.Remove(0, 2)

        Using Servico As IServicoDePerfil = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePerfil)()
            Servico.Salve(Usuario, Perfil)
        End Using
    End Sub

    Private Sub btnAdicionarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarItem.Click
        Dim AtalhoASerInserido As AtalhoExterno
        Dim Inconsistencias As IList(Of String)
        Dim AtalhosExternos As IList(Of Atalho)

        Inconsistencias = ValidaInsercaoDeAtalhosExternos()

        If Not Inconsistencias.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsistencias(Inconsistencias), False)
            Exit Sub
        End If

        AtalhosExternos = CType(ViewState(CHAVE_ATALHOS_EXTERNOS), IList(Of Atalho))
        AtalhoASerInserido = CriaAtalhoExterno()
        AtalhosExternos.Add(AtalhoASerInserido)
        ExibaAtalhosExternos(AtalhosExternos)
        LimpaCamposDoAtalhoExterno()
    End Sub

    Private Sub ExibaAtalhosExternos(ByVal Atalhos As IList(Of Atalho))
        ViewState(CHAVE_ATALHOS_EXTERNOS) = Atalhos
        grdAtalhosExternos.DataSource = Atalhos
        grdAtalhosExternos.DataBind()
    End Sub

    Private Function CriaAtalhoExterno() As AtalhoExterno
        Dim Atalho As AtalhoExterno

        Dim imagem As String = Nothing

        If Not String.IsNullOrEmpty(imgFoto.ImageUrl) Then
            imagem = imgFoto.ImageUrl.Remove(0, 2)
        End If
        
        Atalho = New AtalhoExterno(txtNomeAtalhoExterno.Text, txtURLAtalhoExterno.Text, imagem)

        Return Atalho
    End Function

    Private Function ValidaInsercaoDeAtalhosExternos() As IList(Of String)
        Dim Inconsistencias As New List(Of String)

        If String.IsNullOrEmpty(txtNomeAtalhoExterno.Text) Then
            Inconsistencias.Add("O nome do atalho deve ser informado.")
        End If

        If String.IsNullOrEmpty(txtURLAtalhoExterno.Text) Then
            Inconsistencias.Add("A URL do atalho deve ser informada.")
        End If

        Return Inconsistencias
    End Function

    Private Sub LimpaCamposDoAtalhoExterno()
        Me.txtNomeAtalhoExterno.Text = ""
        Me.txtURLAtalhoExterno.Text = ""
        Me.imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
        UtilidadesWeb.LimparComponente(CType(grdAtalhosExternos, Control))
    End Sub

   Private Function ConsisteDados() As String
        If ctrlPessoa1.PessoaSelecionada Is Nothing Then Return "Proprietário da agenda deve ser selecionado."
        If Not txtHorarioDeInicio.SelectedDate.HasValue Then Return "O horário de início deve ser informado."
        If Not txtHorarioFinal.SelectedDate.HasValue Then Return "O horário final deve ser informado."
        If Not txtIntervaloEntreCompromissos.SelectedDate.HasValue Then Return "O intervalo entre os compromissos deve ser informado."
        Return String.Empty
    End Function

    Private Function MontaObjeto() As IConfiguracaoDeAgendaDoUsuario
        Dim Pessoa As IPessoaFisica
        Dim PessoaPadrao As IPessoaFisica
        Dim ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario

        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        PessoaPadrao = CType(ctrlPessoa1.PessoaSelecionada, IPessoaFisica)
        ConfiguracaoDaAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgendaDoUsuario)()
        ConfiguracaoDaAgenda.Pessoa = Pessoa
        ConfiguracaoDaAgenda.HorarioDeInicio = txtHorarioDeInicio.SelectedDate.Value
        ConfiguracaoDaAgenda.HorarioDeTermino = txtHorarioFinal.SelectedDate.Value
        ConfiguracaoDaAgenda.IntervaloEntreOsCompromissos = txtIntervaloEntreCompromissos.SelectedDate.Value
        ConfiguracaoDaAgenda.PessoaPadraoAoAcessarAAgenda = PessoaPadrao
        Return ConfiguracaoDaAgenda
    End Function

    Private Sub SalveAgenda()

        If chkHabilitaAgenda.Checked Then
            Dim ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario
            Dim Inconsistencia As String

            Inconsistencia = ConsisteDados()

            If Not String.IsNullOrEmpty(Inconsistencia) Then
                Throw New BussinesException(Inconsistencia)
            End If

            ConfiguracaoDaAgenda = MontaObjeto()

            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                Servico.ModifiqueConfiguracao(ConfiguracaoDaAgenda)
            End Using
        Else
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                Servico.RemovaConfiguracao(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
            End Using
        End If
    End Sub

    Private Sub Mostre(ByVal Configuracao As IConfiguracaoDeAgendaDoUsuario)
        txtHorarioDeInicio.SelectedDate = Configuracao.HorarioDeInicio
        txtHorarioFinal.SelectedDate = Configuracao.HorarioDeTermino
        txtIntervaloEntreCompromissos.SelectedDate = Configuracao.IntervaloEntreOsCompromissos
        ctrlPessoa1.PessoaSelecionada = Configuracao.PessoaPadraoAoAcessarAAgenda
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Atalhos
        tabStrip.Tabs(0).Visible = FabricaDeContexto.GetInstancia.GetContextoAtual.EstaAutorizado("OPE.NCL.011.0001")
        'Temas
        tabStrip.Tabs(1).Visible = FabricaDeContexto.GetInstancia.GetContextoAtual.EstaAutorizado("OPE.NCL.011.0002")
        'Papel de parede
        tabStrip.Tabs(2).Visible = FabricaDeContexto.GetInstancia.GetContextoAtual.EstaAutorizado("OPE.NCL.011.0003")
        'Agenda
        tabStrip.Tabs(3).Visible = FabricaDeContexto.GetInstancia.GetContextoAtual.EstaAutorizado("OPE.NCL.011.0004")

        'Verifica se usuário logado tem permissão para visualizar outras agendas
        pnlPessoaPadraoDaAgenda.Visible = FabricaDeContexto.GetInstancia.GetContextoAtual.EstaAutorizado("OPE.NCL.012.0007")
    End Sub

    Private Sub chkHabilitaAgenda_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHabilitaAgenda.CheckedChanged
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaAgenda, Control))

        If Not chkHabilitaAgenda.Checked Then
            UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAgenda, Control), False)
            UtilidadesWeb.HabilitaComponentes(CType(pnlPessoaPadraoDaAgenda, Control), False)
            Exit Sub
        End If

        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAgenda, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlPessoaPadraoDaAgenda, Control), True)
    End Sub

    Private Sub uplPapelParede_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uplPapelParede.FileUploaded
        If uplPapelParede.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = uplPapelParede.UploadedFiles(0)
            Dim targetFolder As String = Server.MapPath(UtilidadesWeb.URL_PAPEIS_DE_PAREDE)
            UtilidadesWeb.CrieDiretorio(targetFolder)
            Dim targetFileName As String = Path.Combine(targetFolder, file.GetNameWithoutExtension() + file.GetExtension())
            file.SaveAs(targetFileName)
            imgPapelDeParede.ImageUrl = String.Concat(UtilidadesWeb.URL_PAPEIS_DE_PAREDE, "/", file.GetNameWithoutExtension() + file.GetExtension())
        End If
    End Sub

    Private Sub uplAtalho_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uplAtalho.FileUploaded
        If uplAtalho.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = uplAtalho.UploadedFiles(0)
            Dim targetFolder As String = Server.MapPath(UtilidadesWeb.URL_ATALHOS)
            UtilidadesWeb.CrieDiretorio(targetFolder)
            Dim targetFileName As String = Path.Combine(targetFolder, file.GetNameWithoutExtension() + file.GetExtension())
            file.SaveAs(targetFileName)
            imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_ATALHOS, "/", file.GetNameWithoutExtension() + file.GetExtension())
        End If
    End Sub

    Private Sub grdAtalhosExternos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdAtalhosExternos.ItemCommand
        Dim IndiceSelecionado As Integer

        IndiceSelecionado = e.Item().ItemIndex

        If e.CommandName = "Excluir" Then
            Dim AtalhosExternos = CType(ViewState(CHAVE_ATALHOS_EXTERNOS), IList(Of Atalho))

            AtalhosExternos.RemoveAt(IndiceSelecionado)
            ExibaAtalhosExternos(AtalhosExternos)
        End If
    End Sub

    Private Sub grdAtalhosExternos_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles grdAtalhosExternos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdAtalhosExternos, ViewState(CHAVE_ATALHOS_EXTERNOS), e)
    End Sub

End Class