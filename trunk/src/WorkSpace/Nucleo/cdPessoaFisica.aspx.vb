﻿Imports Compartilhados.Componentes.Web
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
        CarregueComponentes()
        ViewState(CHAVE_ESTADO) = Estado.Novo
        ViewState(CHAVE_ID) = Nothing
        ViewState(CHAVE_TELEFONES) = Nothing
        imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), False)
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

            Pessoa.Endereco = Endereco
        End If

        If Not String.IsNullOrEmpty(txtEmail.Text) Then
            Pessoa.EnderecoDeEmail = txtEmail.Text
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
        Pessoa.Foto = imgFoto.ImageUrl
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

        If Not Pessoa.Endereco Is Nothing Then
            txtLogradouro.Text = Pessoa.Endereco.Logradouro
            txtComplemento.Text = Pessoa.Endereco.Complemento
            txtBairro.Text = Pessoa.Endereco.Bairro

            If Not Pessoa.Endereco.Municipio Is Nothing Then
                ctrlMunicipios2.MunicipioSelecionado = Pessoa.Endereco.Municipio
                ctrlMunicipios2.NomeDoMunicipio = Pessoa.Endereco.Municipio.Nome
                cboUFEndereco.SelectedValue = Pessoa.Endereco.Municipio.UF.ID.ToString
            End If

            If Not Pessoa.Endereco.CEP Is Nothing Then
                txtCEPEndereco.Text = Pessoa.Endereco.CEP.Numero.Value.ToString
            End If

        End If

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

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            txtEmail.Text = Pessoa.EnderecoDeEmail.ToString
        End If

        txtSite.Text = Pessoa.Site
        ExibaTelefones(Pessoa.Telefones)
        imgFoto.ImageUrl = Pessoa.Foto
        ViewState(CHAVE_ID) = Pessoa.ID.Value
    End Sub

    Private Sub ExibaTelefones(ByVal Telefones As IList(Of ITelefone))
        grdTelefones.DataSource = Telefones
        grdTelefones.DataBind()
        ViewState(CHAVE_TELEFONES) = Telefones
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
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        ViewState(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub btnSim_Click()
        Dim Pessoa As IPessoaFisica = Nothing

        Pessoa = MontaObjeto()

        Try
            Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
                Servico.Remover(Pessoa)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Pessoa removida com sucesso."), False)
            Me.ExibaTelaNovo()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSubmit.Click
        If uplFoto.UploadedFiles.Count > 0 Then
            For Each validFile As UploadedFile In uplFoto.UploadedFiles
                Dim PastaDeDestino As String = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_FOTO_PESSOA)
                validFile.SaveAs(Path.Combine(PastaDeDestino, validFile.GetName()), True)
                UtilidadesWeb.redimensionaImagem(PastaDeDestino, _
                                                  validFile.GetName(), _
                                                  200, _
                                                  200)
                imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_FOTO_PESSOA, "/" & validFile.GetName())
            Next
        End If
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
    End Sub

    Private Function ObtenhaObjetoTelefone() As ITelefone
        Dim Telefone As ITelefone
        Telefone = FabricaGenerica.GetInstancia.CrieObjeto(Of ITelefone)()
        Telefone.DDD = CShort(txtDDDTelefone.Text)
        Telefone.Numero = CLng(txtNumeroTelefone.Text)
        Telefone.Tipo = TipoDeTelefone.Obtenha(CShort(cboTipoTelefone.SelectedValue))

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

        If e.CommandName = "Excluir" Then
            Dim Telefones As IList(Of ITelefone)
            Telefones = CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone))
            Telefones.RemoveAt(IndiceSelecionado)
            ExibaTelefones(Telefones)
        End If
    End Sub
End Class