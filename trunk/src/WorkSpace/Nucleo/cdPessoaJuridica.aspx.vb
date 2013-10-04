Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class cdPessoaJuridica
    Inherits SuperPagina

    Private Enum Estado As Byte
        Novo
        Modifica
        Neutro
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_PESSOA_JURIDICA"
    Private CHAVE_ID As String = "CHAVE_ID_CD_PESSOA_JURIDICA"
    Private CHAVE_TELEFONES As String = "CHAVE_TELEFONES_PESSOA_JURIDICA"
    Private CHAVE_ENDERECOS As String = "CHAVE_ENDERECOS_PESSOA_JURIDICA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        txtNome.Enabled = True
        ViewState(CHAVE_ESTADO) = Estado.Novo
        ViewState(CHAVE_TELEFONES) = Nothing
        CarregueUFs()
        CarregaTiposDeTelefone()
        cboUFEndereco.Enabled = False
        imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub CarregaTiposDeTelefone()
        cboTipoTelefone.Items.Clear()

        For Each Tipo As TipoDeTelefone In TipoDeTelefone.ObtenhaTodos
            cboTipoTelefone.Items.Add(New RadComboBoxItem(Tipo.Descricao, Tipo.ID.ToString))
        Next
    End Sub

    Private Sub ExibaTelaAposSalvar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO) = Estado.Neutro
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), False)
        txtNome.Enabled = False
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), False)
        txtNome.Enabled = False

        Dim Pessoa As IPessoaJuridica

        Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
            Pessoa = Servico.ObtenhaPessoa(Id)
        End Using

        CarregueUFs()
        CarregaTiposDeTelefone()
        Me.ExibaObjeto(Pessoa)
        cboUFEndereco.Enabled = False
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        txtNome.Enabled = True
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        cboUFEndereco.Enabled = False
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Function ValidaDadosObrigatorios() As String
        If String.IsNullOrEmpty(txtNome.Text) Then
            Return "Nome deve ser informado."
        End If

        If String.IsNullOrEmpty(txtCNPJ.Text) Then
            Return "CNPJ deve ser informado."
        End If

        If Not String.IsNullOrEmpty(txtLogradouro.Text) OrElse _
           Not String.IsNullOrEmpty(txtBairro.Text) OrElse _
           Not ctrlMunicipios2.MunicipioSelecionado Is Nothing OrElse _
           Not String.IsNullOrEmpty(txtCEPEndereco.Text) Then

            If String.IsNullOrEmpty(txtLogradouro.Text) Then Return "Logradouro deve ser informado."
            If String.IsNullOrEmpty(txtBairro.Text) Then Return "Bairro deve ser informado."
            If ctrlMunicipios2.MunicipioSelecionado Is Nothing Then Return "Municipio do endereço deve ser informado."
            If String.IsNullOrEmpty(txtCEPEndereco.Text) Then Return "CEP deve ser informado."

        End If

        Return String.Empty
    End Function

    Private Sub btnSalva_Click()
        Dim Pessoa As IPessoaJuridica = Nothing
        Dim Inconsistencia As String

        Inconsistencia = ValidaDadosObrigatorios()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Pessoa = MontaObjeto()

        Try
            Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()

                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Pessoa)
                Else
                    Servico.Modificar(Pessoa)
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Pessoa cadastrada com sucesso."), False)
            ExibaTelaAposSalvar()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IPessoaJuridica
        Dim Pessoa As IPessoaJuridica

        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()

        If CByte(ViewState(CHAVE_ESTADO)) = Estado.Modifica Then
            Pessoa.ID = CLng(ViewState(CHAVE_ID))
        End If

        Pessoa.Nome = txtNome.Text
        Pessoa.NomeFantasia = txtNomeFantasia.Text

        Pessoa.AdicioneEnderecos(CType(ViewState(CHAVE_ENDERECOS), IList(Of IEndereco)))

        If Not String.IsNullOrEmpty(txtEmail.Text) Then
            Pessoa.EnderecoDeEmail = txtEmail.Text
        End If

        If Not String.IsNullOrEmpty(txtCNPJ.Text) Then
            Dim CNPJ As ICNPJ

            CNPJ = FabricaGenerica.GetInstancia.CrieObjeto(Of ICNPJ)(New Object() {txtCNPJ.Text})
            Pessoa.AdicioneDocumento(CNPJ)
        End If

        If Not String.IsNullOrEmpty(txtInscricaoEstadual.Text) Then
            Dim InscricaoEstadual As IInscricaoEstadual

            InscricaoEstadual = FabricaGenerica.GetInstancia.CrieObjeto(Of IInscricaoEstadual)(New Object() {txtInscricaoEstadual.Text})
            Pessoa.AdicioneDocumento(InscricaoEstadual)
        End If

        If Not String.IsNullOrEmpty(txtInstricaoMunicipal.Text) Then
            Dim InscricaoMunicipal As IInscricaoMunicipal

            InscricaoMunicipal = FabricaGenerica.GetInstancia.CrieObjeto(Of IInscricaoMunicipal)(New Object() {txtInstricaoMunicipal.Text})
            Pessoa.AdicioneDocumento(InscricaoMunicipal)
        End If

        Pessoa.Site = txtSite.Text
        Pessoa.AdicioneTelefones(CType(ViewState(CHAVE_TELEFONES), IList(Of ITelefone)))

        If Not ctrlBancosEAgencias1.AgenciaSelecionada Is Nothing Then
            Pessoa.DadoBancario = FabricaGenerica.GetInstancia.CrieObjeto(Of IDadoBancario)()
            Pessoa.DadoBancario.Agencia = ctrlBancosEAgencias1.AgenciaSelecionada
            Pessoa.DadoBancario.Conta = FabricaGenerica.GetInstancia.CrieObjeto(Of IContaBancaria)()
            Pessoa.DadoBancario.Conta.Numero = ctrlBancosEAgencias1.NumeroDaConta

            If ctrlBancosEAgencias1.TipoDaConta.HasValue Then
                Pessoa.DadoBancario.Conta.Tipo = ctrlBancosEAgencias1.TipoDaConta.Value
            End If
        End If

        Pessoa.Logomarca = imgFoto.ImageUrl

        Return Pessoa
    End Function

    Private Sub ExibaObjeto(ByVal Pessoa As IPessoaJuridica)
        txtNome.Text = Pessoa.Nome
        txtNomeFantasia.Text = Pessoa.NomeFantasia

        Dim CNPJ As ICNPJ

        CNPJ = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ), ICNPJ)

        If Not CNPJ Is Nothing Then
            txtCNPJ.Text = CNPJ.Numero
        End If

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            txtEmail.Text = Pessoa.EnderecoDeEmail.ToString
        End If

        Dim InscricaoEstadual As IInscricaoEstadual

        InscricaoEstadual = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IE), IInscricaoEstadual)

        If Not InscricaoEstadual Is Nothing Then
            txtInscricaoEstadual.Text = InscricaoEstadual.Numero
        End If

        Dim InscricaoMunicipal As IInscricaoMunicipal

        InscricaoMunicipal = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IM), IInscricaoMunicipal)

        If Not InscricaoMunicipal Is Nothing Then
            txtInstricaoMunicipal.Text = InscricaoMunicipal.Numero
        End If

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            txtEmail.Text = Pessoa.EnderecoDeEmail.ToString
        End If

        If Not Pessoa.DadoBancario Is Nothing Then
            ctrlBancosEAgencias1.BancoSelecionado = Pessoa.DadoBancario.Agencia.Banco
            ctrlBancosEAgencias1.AgenciaSelecionada = Pessoa.DadoBancario.Agencia

            If Not Pessoa.DadoBancario.Conta Is Nothing Then
                ctrlBancosEAgencias1.NumeroDaConta = Pessoa.DadoBancario.Conta.Numero
                ctrlBancosEAgencias1.TipoDaConta = Pessoa.DadoBancario.Conta.Tipo
            End If
        End If

        imgFoto.ImageUrl = Pessoa.Logomarca

        txtSite.Text = Pessoa.Site
        ExibaTelefones(Pessoa.Telefones)

        ViewState(CHAVE_ID) = Pessoa.ID.Value
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnModificar"
                Call ExibaTelaModificar()
        End Select
    End Sub

    Private Sub CarregueUFs()
        cboUFEndereco.Items.Clear()

        For Each Item As UF In UF.ObtenhaTodos
            cboUFEndereco.Items.Add(New RadComboBoxItem(Item.Nome, Item.ID.ToString))
        Next
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

    Private Sub grdTelefones_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdTelefones.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdTelefones.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub ExibaTelefones(ByVal Telefones As IList(Of ITelefone))
        grdTelefones.DataSource = Telefones
        grdTelefones.DataBind()
        ViewState(CHAVE_TELEFONES) = Telefones
    End Sub

    Private Sub uplFoto_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles uplFoto.FileUploaded
        If uplFoto.UploadedFiles.Count > 0 Then
            Dim validFile As UploadedFile = uplFoto.UploadedFiles(0)
            Dim PastaDeDestino As String = Server.MapPath(UtilidadesWeb.URL_FOTO_PESSOA)
            validFile.SaveAs(Path.Combine(PastaDeDestino, validFile.GetName()), True)
            UtilidadesWeb.redimensionaImagem(PastaDeDestino, _
                                              validFile.GetName(), _
                                              200, _
                                              200)
            imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_FOTO_PESSOA, "/" & validFile.GetName())
        End If
    End Sub
End Class