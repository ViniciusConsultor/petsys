Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class cdPessoaFisica
    Inherits SuperPagina

    Private Enum Estado As Byte
        Novo
        Modifica
        Exclui
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_PESSOA_FISICA"
    Private CHAVE_ID As String = "CHAVE_ID_CD_PESSOA_FISICA"
    Private CHAVE_TELEFONES As String = "CHAVE_TELEFONES_PESSOA_FISICA"
    Private CHAVE_ENDERECOS As String = "CHAVE_ENDERECOS_PESSOA_FISICA"
    Private CHAVE_CONTATOS As String = "CHAVE_CONTATOS_PESSOA_FISICA"
    Private CHAVE_EVENTOS As String = "CHAVE_EVENTOS_PESSOA_FISICA"
    Private CHAVE_EMAILS As String = "CHAVE_EMAILS_PESSOA_FISICA"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlMunicipios1.MunicipioFoiSelecionado, AddressOf MunicipioDeNascimentoFoiSelecionado
        AddHandler ctrlMunicipios2.MunicipioFoiSelecionado, AddressOf MunicipioDeEnderecoFoiSelecionado

        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
            End If

            If Id Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes(Id.Value)
            End If
        End If
    End Sub

    Private Sub MunicipioDeNascimentoFoiSelecionado(ByVal MunicipioDeNascimento As IMunicipio)
        cboUFNascimento.SelectedValue = MunicipioDeNascimento.UF.ID.ToString
    End Sub

    Private Sub MunicipioDeEnderecoFoiSelecionado(ByVal MunicipioDeEndereco As IMunicipio)
        cboUFEndereco.SelectedValue = MunicipioDeEndereco.UF.ID.ToString

        If String.IsNullOrEmpty(txtCEPEndereco.Text) AndAlso Not MunicipioDeEndereco.CEP Is Nothing Then
            txtCEPEndereco.Text = MunicipioDeEndereco.CEP.ToString
        End If
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.006"
    End Function

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        txtNome.Enabled = True
        CarregueComponentes()
        ViewState(CHAVE_ESTADO) = Estado.Novo
        ViewState(CHAVE_ID) = Nothing
        ViewState(CHAVE_TELEFONES) = Nothing
        ViewState(CHAVE_ENDERECOS) = Nothing
        ViewState(CHAVE_CONTATOS) = Nothing
        ViewState(CHAVE_EVENTOS) = Nothing
        imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        txtNome.Enabled = True
        ViewState(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosPessoais, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBancarios, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEventos, Control), False)
        txtNome.Enabled = False

        CarregueComponentes()

        Dim Pessoa As IPessoaFisica

        Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            Pessoa = Servico.ObtenhaPessoa(Id)
        End Using

        Me.ExibaObjeto(Pessoa)
    End Sub

    Private Sub CarregueComponentes()
        CarregueSexo()
        CarregueEstadoCivil()
        CarregueRaca()
        CarregueUFs()
        CarregaNacionalidade()
        CarregaTiposDeTelefone()
        ExibaTelefones(New List(Of ITelefone))
        ExibaContatos(New List(Of String))
        ExibaEnderecos(New List(Of IEndereco))
        ExibaEventos(New List(Of IEventoDeContato))
    End Sub

    Private Sub CarregaTiposDeTelefone()
        cboTipoTelefone.Items.Clear()

        For Each Tipo As TipoDeTelefone In TipoDeTelefone.ObtenhaTodos
            cboTipoTelefone.Items.Add(New RadComboBoxItem(Tipo.Descricao, Tipo.ID.ToString))
        Next
    End Sub

    Private Sub CarregueSexo()
        rblSexo.Items.Clear()

        For Each Item As Sexo In Sexo.ObtenhaTodosSexo
            rblSexo.Items.Add(New ListItem(Item.Descricao, Item.ID.ToString))
        Next

        rblSexo.SelectedValue = Sexo.Masculino.ID
    End Sub

    Private Sub CarregueEstadoCivil()
        cboEstadoCivil.Items.Clear()

        For Each Item As EstadoCivil In EstadoCivil.ObtenhaTodos
            cboEstadoCivil.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID))
        Next
    End Sub

    Private Sub CarregueRaca()
        cboRaca.Items.Clear()

        cboRaca.Items.Add(New RadComboBoxItem("", ""))

        For Each Item As Raca In Raca.ObtenhaTodos
            cboRaca.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID))
        Next
    End Sub

    Private Sub CarregueUFs()
        cboUFEndereco.Items.Clear()
        cboUFNascimento.Items.Clear()
        cboUFRG.Items.Clear()

        For Each Item As UF In UF.ObtenhaTodos
            cboUFEndereco.Items.Add(New RadComboBoxItem(Item.Nome, Item.ID.ToString))
            cboUFNascimento.Items.Add(New RadComboBoxItem(Item.Nome, Item.ID.ToString))
            cboUFRG.Items.Add(New RadComboBoxItem(Item.Nome, Item.ID.ToString))
        Next
    End Sub

    Private Sub CarregaNacionalidade()
        cboNacionalidade.Items.Clear()

        For Each Item As Nacionalidade In Nacionalidade.ObtenhaTodos
            cboNacionalidade.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID))
        Next
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnSalva_Click()
        Dim Pessoa As IPessoaFisica = Nothing
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Pessoa = MontaObjeto()

        Try
            Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()

                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Pessoa)
                Else
                    Servico.Modificar(Pessoa)
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Pessoa cadastrada com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function ValidaDados() As String
        If String.IsNullOrEmpty(txtNome.Text) Then Return "O nome da pessoa deve ser informado."

        Return Nothing
    End Function

    Private Function MontaObjeto() As IPessoaFisica
        Dim Pessoa As IPessoaFisica

        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()

        If CByte(ViewState(CHAVE_ESTADO)) = Estado.Modifica Then
            Pessoa.ID = CLng(ViewState(CHAVE_ID))
        End If

        Pessoa.Nome = txtNome.Text
        Pessoa.Sexo = Sexo.ObtenhaSexo(CChar(rblSexo.SelectedValue))
        Pessoa.DataDeNascimento = Me.txtDataDeNascimento.SelectedDate
        Pessoa.EstadoCivil = EstadoCivil.ObtenhaEstadoCivil(CChar(cboEstadoCivil.SelectedValue))
        Pessoa.NomeDaMae = txtNomeDaMae.Text
        Pessoa.NomeDoPai = txtNomeDoPai.Text
        Pessoa.Naturalidade = ctrlMunicipios1.MunicipioSelecionado
        Pessoa.Nacionalidade = Nacionalidade.Obtenha(CChar(cboNacionalidade.SelectedValue))

        If Not String.IsNullOrEmpty(cboRaca.SelectedValue) Then
            Pessoa.Raca = Raca.ObtenhaRaca(CChar(cboRaca.SelectedValue))
        End If

        If Not String.IsNullOrEmpty(txtLogradouro.Text) Then
            Dim Endereco As IEndereco

            Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()

            Endereco.Bairro = txtBairro.Text

            If Not String.IsNullOrEmpty(txtCEPEndereco.Text) Then
                Endereco.CEP = New CEP(CLng(txtCEPEndereco.Text))
            End If

            Endereco.Complemento = txtComplemento.Text
            Endereco.Logradouro = txtLogradouro.Text
            Endereco.Municipio = ctrlMunicipios2.MunicipioSelecionado


        End If

        If Not CType(ViewState(CHAVE_EMAILS), IList(Of EnderecoDeEmail)) Is Nothing Then
            Pessoa.EnderecosDeEmails = CType(ViewState(CHAVE_EMAILS), IList(Of EnderecoDeEmail))
        End If

        If Not String.IsNullOrEmpty(txtCPF.Text) Then
            Dim CPF As ICPF

            CPF = FabricaGenerica.GetInstancia.CrieObjeto(Of ICPF)(New Object() {txtCPF.Text})
            Pessoa.AdicioneDocumento(CPF)
        End If

        If Not String.IsNullOrEmpty(txtNumeroRG.Text) Then
            Dim RG As IRG

            RG = FabricaGenerica.GetInstancia.CrieObjeto(Of IRG)(New Object() {txtNumeroRG.Text})
            RG.OrgaoExpeditor = txtOrgaoExpeditorRG.Text
            RG.UF = UF.Obtenha(CShort(cboUFRG.SelectedValue))
            RG.DataDeEmissao = txtDataEmissaoRG.SelectedDate

            Pessoa.AdicioneDocumento(RG)
        End If

        Pessoa.Site = txtSite.Text
        Pessoa.AdicioneTelefones(CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone)))
        Pessoa.AdicioneEnderecos(CType(ViewState(CHAVE_ENDERECOS), IList(Of IEndereco)))
        Pessoa.Foto = imgFoto.ImageUrl

        If Not ctrlBancosEAgencias1.AgenciaSelecionada Is Nothing Then
            Pessoa.DadoBancario = FabricaGenerica.GetInstancia.CrieObjeto(Of IDadoBancario)()
            Pessoa.DadoBancario.Agencia = ctrlBancosEAgencias1.AgenciaSelecionada
            Pessoa.DadoBancario.Conta = FabricaGenerica.GetInstancia.CrieObjeto(Of IContaBancaria)()
            Pessoa.DadoBancario.Conta.Numero = ctrlBancosEAgencias1.NumeroDaConta

            If ctrlBancosEAgencias1.TipoDaConta.HasValue Then
                Pessoa.DadoBancario.Conta.Tipo = ctrlBancosEAgencias1.TipoDaConta.Value
            End If
        End If

        Pessoa.AdicioneContatos(CType(ViewState(CHAVE_CONTATOS), IList(Of String)))
        Pessoa.AdicioneEventosDeContato(CType(ViewState(CHAVE_EVENTOS), IList(Of IEventoDeContato)))
        Return Pessoa
    End Function

    Private Sub ExibaObjeto(ByVal Pessoa As IPessoaFisica)
        txtNome.Text = Pessoa.Nome
        txtDataDeNascimento.SelectedDate = Pessoa.DataDeNascimento
        cboEstadoCivil.SelectedValue = Pessoa.EstadoCivil.ID
        cboNacionalidade.SelectedValue = Pessoa.Nacionalidade.ID

        If Not Pessoa.Naturalidade Is Nothing Then
            ctrlMunicipios1.MunicipioSelecionado = Pessoa.Naturalidade
            ctrlMunicipios1.NomeDoMunicipio = Pessoa.Naturalidade.Nome
            cboUFNascimento.SelectedValue = Pessoa.Naturalidade.UF.ID.ToString
        End If
        
        txtNomeDaMae.Text = Pessoa.NomeDaMae
        txtNomeDoPai.Text = Pessoa.NomeDoPai

        If Not Pessoa.Raca Is Nothing Then
            cboRaca.SelectedValue = Pessoa.Raca.ID
        End If

        rblSexo.SelectedValue = Pessoa.Sexo.ID

        Dim CPF As ICPF

        CPF = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF), ICPF)

        If Not CPF Is Nothing Then
            txtCPF.Text = CPF.Numero
        End If

        Dim RG As IRG

        RG = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.RG), IRG)

        If Not RG Is Nothing Then
            txtNumeroRG.Text = RG.Numero

            If Not RG.DataDeEmissao Is Nothing Then
                txtDataEmissaoRG.SelectedDate = RG.DataDeEmissao.Value
            End If

            txtOrgaoExpeditorRG.Text = RG.OrgaoExpeditor
            cboUFRG.SelectedValue = RG.UF.ID.ToString
        End If

        If Not Pessoa.EnderecosDeEmails Is Nothing Then
            ViewState(CHAVE_EMAILS) = Pessoa.EnderecosDeEmails
        End If

        If Not Pessoa.DadoBancario Is Nothing Then
            ctrlBancosEAgencias1.BancoSelecionado = Pessoa.DadoBancario.Agencia.Banco
            ctrlBancosEAgencias1.AgenciaSelecionada = Pessoa.DadoBancario.Agencia

            If Not Pessoa.DadoBancario.Conta Is Nothing Then
                ctrlBancosEAgencias1.NumeroDaConta = Pessoa.DadoBancario.Conta.Numero
                ctrlBancosEAgencias1.TipoDaConta = Pessoa.DadoBancario.Conta.Tipo
            End If
        End If

        txtSite.Text = Pessoa.Site
        ExibaTelefones(Pessoa.Telefones)
        ExibaEnderecos(Pessoa.Enderecos)
        ExibaContatos(Pessoa.Contatos())
        ExibaEventos(Pessoa.EventosDeContato())
        ExibaEmails(Pessoa.EnderecosDeEmails)

        imgFoto.ImageUrl = Pessoa.Foto
        ViewState(CHAVE_ID) = Pessoa.ID.Value
    End Sub

    Private Sub ExibaEventos(ByVal Eventos As IList(Of IEventoDeContato))
        If Eventos Is Nothing Then Eventos = New List(Of IEventoDeContato)()
        grdEventos.DataSource = Eventos
        grdEventos.DataBind()
        ViewState(CHAVE_EVENTOS) = Eventos
    End Sub

    Private Sub ExibaTelefones(ByVal Telefones As IList(Of ITelefone))
        If Telefones Is Nothing Then Telefones = New List(Of ITelefone)()
        grdTelefones.DataSource = Telefones
        grdTelefones.DataBind()
        ViewState(CHAVE_TELEFONES) = Telefones
    End Sub

    Private Sub ExibaEnderecos(ByVal Enderecos As IList(Of IEndereco))
        If Enderecos Is Nothing Then Enderecos = New List(Of IEndereco)()
        grdEnderecos.DataSource = Enderecos
        grdEnderecos.DataBind()
        ViewState(CHAVE_ENDERECOS) = Enderecos
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                btnSalva_Click()
            Case "btnModificar"
                ExibaTelaModificar()
            Case "btnExcluir"
                ExibaTelaExcluir()
            Case "btnNao"
                ExibaTelaDetalhes(CLng(ViewState(CHAVE_ID)))
            Case "btnSim"
                btnSim_Click()
        End Select
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
                Servico.Remover(CLng(ViewState(CHAVE_ID)))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Pessoa removida com sucesso."), False)
            Me.ExibaTelaNovo()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnAdicionarTelefone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarTelefone.Click
        Dim Telefones As IList(Of ITelefone)
        Dim Inconsistencia As String

        Inconsistencia = ValidaDadosDoTelefone()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Telefones = CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone))

        If Telefones Is Nothing Then Telefones = New List(Of ITelefone)

        Dim Telefone As ITelefone

        Telefone = ObtenhaObjetoTelefone()

        If Telefones.Contains(Telefone) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O telefone informado já existe para esta pessoa."), False)
            Exit Sub
        End If
        Telefones.Add(Telefone)
        ExibaTelefones(Telefones)
        LimpaDadosDoTelefone()
    End Sub

    Private Sub LimpaDadosDoTelefone()
        cboTipoTelefone.SelectedValue = TipoDeTelefone.Residencial.ID.ToString
        txtDDDTelefone.Text = ""
        txtNumeroTelefone.Text = ""
        txtContatoTelefone.Text = ""
    End Sub

    Private Function ObtenhaObjetoTelefone() As ITelefone
        Dim Telefone As ITelefone
        Telefone = FabricaGenerica.GetInstancia.CrieObjeto(Of ITelefone)()
        Telefone.DDD = CShort(txtDDDTelefone.Text)
        Telefone.Numero = CLng(txtNumeroTelefone.Text)
        Telefone.Tipo = TipoDeTelefone.Obtenha(CShort(cboTipoTelefone.SelectedValue))
        Telefone.Contato = txtContatoTelefone.Text

        Return Telefone
    End Function

    Private Function ValidaDadosDoTelefone() As String
        If String.IsNullOrEmpty(txtDDDTelefone.Text) Then Return "O DDD do telefone deve ser informado."
        If String.IsNullOrEmpty(txtNumeroTelefone.Text) Then Return "O número do telefone deve ser informado."

        Return Nothing
    End Function

    Private Sub grdTelefones_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdTelefones.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Editar" Then
            Dim Telefones As IList(Of ITelefone)
            Telefones = CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone))
            Dim TelefoneASerEditado As ITelefone = Telefones(IndiceSelecionado)
            CarregueTelefoneEmEdicao(TelefoneASerEditado)
            Telefones.RemoveAt(IndiceSelecionado)
            ExibaTelefones(Telefones)
        End If

        If e.CommandName = "Excluir" Then
            Dim Telefones As IList(Of ITelefone)
            Telefones = CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone))
            Telefones.RemoveAt(IndiceSelecionado)
            ExibaTelefones(Telefones)
        End If
    End Sub

    Private Sub grdTelefones_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdTelefones.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdTelefones.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub uplFoto_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uplFoto.FileUploaded
        If uplFoto.UploadedFiles.Count > 0 Then
            Dim validFile As UploadedFile = uplFoto.UploadedFiles(0)
            Dim PastaDeDestino As String = Server.MapPath(UtilidadesWeb.URL_FOTO_PESSOA)

            UtilidadesWeb.CrieDiretorio(PastaDeDestino)
            validFile.SaveAs(Path.Combine(PastaDeDestino, validFile.GetName()), True)
            UtilidadesWeb.redimensionaImagem(PastaDeDestino, _
                                              validFile.GetName(), _
                                              200, _
                                              200)
            imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_FOTO_PESSOA, "/" & validFile.GetName())
        End If
    End Sub

    Private Sub btnAdicionarEndereco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarEndereco.Click
        Dim Enderecos As IList(Of IEndereco)
        Dim Inconsistencia As String

        Inconsistencia = ValidaDadosDoEndereco()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Enderecos = CType(ViewState(CHAVE_ENDERECOS), IList(Of IEndereco))

        If Enderecos Is Nothing Then Enderecos = New List(Of IEndereco)

        Dim Endereco As IEndereco

        Endereco = ObtenhaObjetoEndereco()

        If Enderecos.Contains(Endereco) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O endereço informado já existe para esta pessoa."), False)
            Exit Sub
        End If

        Enderecos.Add(Endereco)
        ExibaEnderecos(Enderecos)
        LimpaDadosDoEndereco()
    End Sub

    Private Sub LimpaDadosDoEndereco()
        ctrlTipoEndereco1.LimparControle()
        txtBairro.Text = ""
        txtComplemento.Text = ""
        txtCEPEndereco.Text = ""
        txtLogradouro.Text = ""
        ctrlMunicipios2.LimparControle()
    End Sub

    Private Function ObtenhaObjetoEndereco() As IEndereco
        Dim Endereco As IEndereco

        Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()
        Endereco.Bairro = txtBairro.Text
        Endereco.Complemento = txtComplemento.Text
        Endereco.Logradouro = txtLogradouro.Text
        Endereco.TipoDeEndereco = ctrlTipoEndereco1.TipoSelecionado

        If Not ctrlMunicipios2.MunicipioSelecionado Is Nothing Then
            Endereco.Municipio = ctrlMunicipios2.MunicipioSelecionado
        End If


        If Not String.IsNullOrEmpty(txtCEPEndereco.Text) Then
            Endereco.CEP = New CEP(CLng(txtCEPEndereco.Text))
        End If

        Return Endereco
    End Function

    Private Function ValidaDadosDoEndereco() As String
        If ctrlTipoEndereco1.TipoSelecionado Is Nothing Then Return "O tipo do endereço deve ser informado."

        Return Nothing
    End Function

    Private Sub grdEnderecos_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEnderecos.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Enderecos As IList(Of IEndereco)
            Enderecos = CType(ViewState(CHAVE_ENDERECOS), IList(Of IEndereco))
            Enderecos.RemoveAt(IndiceSelecionado)
            ExibaEnderecos(Enderecos)
        End If
    End Sub

    Private Sub grdEnderecos_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEnderecos.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdEnderecos.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub btnAdicionarContato_Click(sender As Object, e As System.EventArgs) Handles btnAdicionarContato.Click
        Dim Contatos As IList(Of String)


        If String.IsNullOrEmpty(txtNomeDoContato.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O nome do contato deve ser informado."), False)
            Exit Sub
        End If

        Contatos = CType(ViewState(CHAVE_CONTATOS), IList(Of String))

        If Contatos Is Nothing Then Contatos = New List(Of String)

        If Contatos.Contains(txtNomeDoContato.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O nome do contato informado já existe na lista de contatos."), False)
            Exit Sub
        End If

        Contatos.Add(txtNomeDoContato.Text)
        ExibaContatos(Contatos)
        txtNomeDoContato.Text = ""
    End Sub

    Private Sub ExibaContatos(ByVal Contatos As IList(Of String))
        If Contatos Is Nothing Then Contatos = New List(Of String)()
        grdContatos.DataSource = Contatos
        grdContatos.DataBind()
        ViewState(CHAVE_CONTATOS) = Contatos
    End Sub

    Private Sub grdContatos_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdContatos.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Contatos As IList(Of String)
            Contatos = CType(ViewState(CHAVE_CONTATOS), IList(Of String))
            Contatos.RemoveAt(IndiceSelecionado)
            ExibaContatos(Contatos)
        End If
    End Sub

    Private Sub btnAdicionarEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarEvento.Click
        Dim Eventos As IList(Of IEventoDeContato)

        If Not txtDataDoEvento.SelectedDate.HasValue Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("A data do evento deve ser informada."), False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtDescricaoEvento.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("A descrição do evento deve ser informada."), False)
            Exit Sub
        End If

        Eventos = CType(ViewState(CHAVE_EVENTOS), IList(Of IEventoDeContato))

        If Eventos Is Nothing Then Eventos = New List(Of IEventoDeContato)

        Dim Evento As IEventoDeContato = FabricaGenerica.GetInstancia().CrieObjeto(Of IEventoDeContato)()

        Evento.Data = txtDataDoEvento.SelectedDate.Value
        Evento.Descricao = txtDescricaoEvento.Text

        Eventos.Add(Evento)
        ExibaEventos(Eventos)
        txtDescricaoEvento.Text = ""
        txtDataDoEvento.SelectedDate = Nothing
    End Sub

    Private Sub grdEventos_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEventos.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Eventos As IList(Of IEventoDeContato)
            Eventos = CType(ViewState(CHAVE_EVENTOS), IList(Of IEventoDeContato))
            Eventos.RemoveAt(IndiceSelecionado)
            ExibaEventos(Eventos)
        End If
    End Sub

    Private Sub grdEventos_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdEventos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdEventos, ViewState(CHAVE_EVENTOS), e)
    End Sub

    Private Sub grdContatos_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdContatos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdContatos, ViewState(CHAVE_CONTATOS), e)
    End Sub

    Private Sub grdEnderecos_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdEnderecos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdEnderecos, ViewState(CHAVE_ENDERECOS), e)
    End Sub

    Private Sub grdTelefones_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdTelefones.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdTelefones, ViewState(CHAVE_TELEFONES), e)
    End Sub

    Private Sub CarregueTelefoneEmEdicao(ByVal Telefone As ITelefone)
        cboTipoTelefone.SelectedValue = Telefone.Tipo.ID.ToString()
        txtDDDTelefone.Text = Telefone.DDD.ToString()
        txtNumeroTelefone.Text = Telefone.Numero.ToString()
        txtContatoTelefone.Text = Telefone.Contato
    End Sub

    Private Sub btnAdicionarEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarEmail.Click
        Dim Emails As IList(Of EnderecoDeEmail)

        Emails = CType(ViewState(CHAVE_EMAILS), IList(Of EnderecoDeEmail))

        If Emails Is Nothing Then
            Emails = New List(Of EnderecoDeEmail)
        End If

        Dim Email As EnderecoDeEmail = txtEmail.Text

        If Emails.Contains(Email) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString,
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("O e-mail informado já foi adicionado."), False)
            Exit Sub
        End If

        If Not EmailValido(Email.ToString()) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString,
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("O e-mail informado é inválido."), False)
            Exit Sub
        End If

        Emails.Add(Email)
        ExibaEmails(Emails)
        txtEmail.Text = String.Empty
    End Sub

    Private Sub ExibaEmails(ByVal Emails As IList(Of EnderecoDeEmail))
        If Emails Is Nothing Then Emails = New List(Of EnderecoDeEmail)()
        grdEmails.DataSource = Emails
        grdEmails.DataBind()
        ViewState(CHAVE_EMAILS) = Emails
    End Sub

    Private Sub grdEmails_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEmails.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Editar" Then
            Dim enderecoDeEmails As IList(Of EnderecoDeEmail)
            enderecoDeEmails = CType(ViewState(CHAVE_EMAILS), IList(Of EnderecoDeEmail))
            Dim enderecoEmail As EnderecoDeEmail = enderecoDeEmails(IndiceSelecionado)
            CarregueEmailEmEdicao(enderecoEmail)
            enderecoDeEmails.RemoveAt(IndiceSelecionado)
            ExibaEmails(enderecoDeEmails)
        End If

        If e.CommandName = "Excluir" Then
            Dim enderecoDeEmails As IList(Of EnderecoDeEmail)
            enderecoDeEmails = CType(ViewState(CHAVE_EMAILS), IList(Of EnderecoDeEmail))
            enderecoDeEmails.RemoveAt(IndiceSelecionado)
            ExibaEmails(enderecoDeEmails)
        End If
    End Sub

    Private Sub grdEmails_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdEmails.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdEmails.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub CarregueEmailEmEdicao(ByVal email As EnderecoDeEmail)
        txtEmail.Text = email.ToString()
    End Sub

    Function EmailValido(ByVal email As String) As Boolean
        Static emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

        Return emailExpression.IsMatch(email)
    End Function

End Class