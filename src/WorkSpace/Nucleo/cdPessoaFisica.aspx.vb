Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports System.IO

Partial Public Class cdPessoaFisica
    Inherits SuperPagina

    Private Enum Estado As Byte
        Novo
        Modifica
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_PESSOA_FISICA"
    Private CHAVE_ID As String = "CHAVE_ID_CD_PESSOA_FISICA"

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
        UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        CarregueComponentes()
        Session(CHAVE_ESTADO) = Estado.Novo
        imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        Session(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
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

        'If Not ValidaDadosObrigatorios() Then Exit Sub

        Pessoa = MontaObjeto()

        Try
            Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()

                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
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

    Private Function MontaObjeto() As IPessoaFisica
        Dim Pessoa As IPessoaFisica

        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()

        If CByte(Session(CHAVE_ESTADO)) = Estado.Modifica Then
            Pessoa.ID = CLng(Session(CHAVE_ID))
        End If

        Pessoa.Nome = txtNome.Text
        Pessoa.Sexo = Sexo.ObtenhaSexo(CChar(rblSexo.SelectedValue))
        Pessoa.DataDeNascimento = Me.txtDataDeNascimento.SelectedDate.Value
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
            Endereco.CEP = New CEP(CLng(txtCEPEndereco.Text))
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

        Pessoa.Foto = imgFoto.ImageUrl
        Return Pessoa
    End Function

    Private Sub ExibaObjeto(ByVal Pessoa As IPessoaFisica)
        txtNome.Text = Pessoa.Nome
        txtDataDeNascimento.SelectedDate = Pessoa.DataDeNascimento
        cboEstadoCivil.SelectedValue = Pessoa.EstadoCivil.ID
        cboNacionalidade.SelectedValue = Pessoa.Nacionalidade.ID
        ctrlMunicipios1.MunicipioSelecionado = Pessoa.Naturalidade
        ctrlMunicipios1.NomeDoMunicipio = Pessoa.Naturalidade.Nome
        cboUFNascimento.SelectedValue = Pessoa.Naturalidade.UF.ID.ToString
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
            ctrlMunicipios2.MunicipioSelecionado = Pessoa.Endereco.Municipio
            ctrlMunicipios2.NomeDoMunicipio = Pessoa.Endereco.Municipio.Nome
            cboUFEndereco.SelectedValue = Pessoa.Endereco.Municipio.UF.ID.ToString
            txtCEPEndereco.Text = Pessoa.Endereco.CEP.Numero.Value.ToString
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

        imgFoto.ImageUrl = Pessoa.Foto

        Session(CHAVE_ID) = Pessoa.ID.Value
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnModificar"
                Call ExibaTelaModificar()
        End Select
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

End Class