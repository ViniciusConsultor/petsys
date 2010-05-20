Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports System.IO

Partial Public Class cdPessoaJuridica
    Inherits SuperPagina

    Private Enum Estado As Byte
        Novo
        Modifica
        Neutro
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_PESSOA_JURIDICA"
    Private CHAVE_ID As String = "CHAVE_ID_CD_PESSOA_JURIDICA"

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
        Session(CHAVE_ESTADO) = Estado.Novo
        CarregueUFs()
        'imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub ExibaTelaAposSalvar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Neutro
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlPessoa, Control), False)
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlPessoa, Control), False)

        Dim Pessoa As IPessoaJuridica

        Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
            Pessoa = Servico.ObtenhaPessoa(Id)
        End Using

        CarregueUFs()
        Me.ExibaObjeto(Pessoa)
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlPessoa, Control), True)
        Session(CHAVE_ESTADO) = Estado.Modifica
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

                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
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

        If CByte(Session(CHAVE_ESTADO)) = Estado.Modifica Then
            Pessoa.ID = CLng(Session(CHAVE_ID))
        End If

        Pessoa.Nome = txtNome.Text
        Pessoa.NomeFantasia = txtNomeFantasia.Text

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

        Return Pessoa
    End Function

    Private Sub ExibaObjeto(ByVal Pessoa As IPessoaJuridica)
        txtNome.Text = Pessoa.Nome
        txtNomeFantasia.Text = Pessoa.NomeFantasia

        If Not Pessoa.Endereco Is Nothing Then
            txtLogradouro.Text = Pessoa.Endereco.Logradouro
            txtComplemento.Text = Pessoa.Endereco.Complemento
            txtBairro.Text = Pessoa.Endereco.Bairro
            ctrlMunicipios2.MunicipioSelecionado = Pessoa.Endereco.Municipio
            ctrlMunicipios2.NomeDoMunicipio = Pessoa.Endereco.Municipio.Nome
            cboUFEndereco.SelectedValue = Pessoa.Endereco.Municipio.UF.ID.ToString
            txtCEPEndereco.Text = Pessoa.Endereco.CEP.Numero.Value.ToString
        End If

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

        'imgFoto.ImageUrl = Pessoa.Foto

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

    'Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSubmit.Click
    '    If uplFoto.UploadedFiles.Count > 0 Then
    '        For Each validFile As UploadedFile In uplFoto.UploadedFiles
    '            Dim PastaDeDestino As String = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_FOTO_PESSOA)
    '            validFile.SaveAs(Path.Combine(PastaDeDestino, validFile.GetName()), True)
    '            UtilidadesWeb.redimensionaImagem(PastaDeDestino, _
    '                                              validFile.GetName(), _
    '                                              200, _
    '                                              200)
    '            imgFoto.ImageUrl = String.Concat(UtilidadesWeb.URL_FOTO_PESSOA, "/" & validFile.GetName())
    '        Next
    '    End If
    'End Sub

    Private Sub CarregueUFs()
        cboUFEndereco.Items.Clear()

        For Each Item As UF In UF.ObtenhaTodos
            cboUFEndereco.Items.Add(New RadComboBoxItem(Item.Nome, Item.ID.ToString))
        Next
    End Sub

End Class