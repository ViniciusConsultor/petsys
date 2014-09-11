Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports System.IO
Imports Telerik.Web.UI

Public Class cdCedente
    Inherits SuperPagina

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_CEDENTE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ObtenhaCedente

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.019"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
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
        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ViewState(CHAVE_ESTADO) = Estado.Inicial
        imgImagem.ImageUrl = String.Empty
        ctrlTipoDeCarteira1.Inicializa()
        txtIniNossoNumero.Text = String.Empty
        chkCedentePadrao.Checked = False
        txtNumeroAgencia.Text = String.Empty
        txtNumeroDaConta.Text = String.Empty
        txtTipoConta.Text = String.Empty
        cboBanco.ClearSelection()
        cboBanco.Text = ""
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
        ViewState(CHAVE_ESTADO) = Estado.Novo
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO) = Estado.Modifica
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO) = Estado.Remove
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

    Private Function MontaObjetoCedente() As ICedente
        Dim Cedente As ICedente
        Dim Pessoa As IPessoa

        Pessoa = ctrlPessoa1.PessoaSelecionada
        Cedente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICedente)((New Object() {Pessoa}))

        If Not String.IsNullOrEmpty(imgImagem.ImageUrl) Then

            Cedente.ImagemDeCabecalhoDoReciboDoSacado = imgImagem.ImageUrl

        End If

        If Not ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada Is Nothing Then
            Cedente.TipoDeCarteira = ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada
        End If

        If Not String.IsNullOrEmpty(txtIniNossoNumero.Text) Then
            Cedente.InicioNossoNumero = CType(txtIniNossoNumero.Text, Long)
        End If

        If (Not String.IsNullOrEmpty(txtNumeroAgencia.Text)) Then
            Cedente.NumeroDaAgencia = txtNumeroAgencia.Text
        End If

        If (Not String.IsNullOrEmpty(txtNumeroDaConta.Text)) Then
            Cedente.NumeroDaConta = txtNumeroDaConta.Text
        End If

        If (Not String.IsNullOrEmpty(txtTipoConta.Text)) Then
            Cedente.TipoDaConta = CType(txtTipoConta.Text, Integer)
        End If

        Cedente.Padrao = chkCedentePadrao.Checked
        
        If (Not String.IsNullOrEmpty(cboBanco.SelectedValue)) Then
            Cedente.NumeroDoBanco = cboBanco.SelectedValue
        End If

        Return Cedente
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String

        Dim listaComErrosDePreenchimento As IList(Of String) = ListaErrosDePreenchimento()

        If listaComErrosDePreenchimento.Count > 0 Then

            Dim mensagemDePreenchimento As StringBuilder = New StringBuilder()

            mensagemDePreenchimento.Append("Preencher o(s) campo(s): ")

            For Each campo As String In listaComErrosDePreenchimento
                mensagemDePreenchimento.Append(campo)
                mensagemDePreenchimento.Append(" , ")
            Next

            'Mensagem = listaComErrosDePreenchimento.Aggregate("Preencher o(s) campo(s): ", Function(current, campo) current + campo)

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString,
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia(mensagemDePreenchimento.ToString().Remove(mensagemDePreenchimento.ToString().Count() - 1)), False)

            Return
        End If

        Dim Cedente As ICedente = MontaObjetoCedente()

        Try
            If (Cedente.Padrao) Then

                Using servicoCedente As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()

                    Dim cedentePadrao = servicoCedente.ObtenhaCedentePadrao()

                    If (cedentePadrao > 0) Then
                        Mensagem = "Já existe um cedente padrão cadastrado na base de dados."
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Mensagem), False)
                        Return
                    End If

                End Using

            End If

            Using Servico As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()
                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Cedente)
                    Mensagem = "Cedente cadastrado com sucesso."
                Else
                    Servico.Modificar(Cedente)
                    Mensagem = "Cedente modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function ListaErrosDePreenchimento() As IList(Of String)
        Dim listaDeCampos As IList(Of String) = New List(Of String)()

        If (ctrlPessoa1 Is Nothing Or ctrlPessoa1.PessoaSelecionada Is Nothing) Then
            listaDeCampos.Add("Nome")
        End If
        If (ctrlTipoDeCarteira1 Is Nothing Or ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada Is Nothing) Then
            listaDeCampos.Add("Tipo de carteira")
        End If
        If (String.IsNullOrEmpty(txtIniNossoNumero.Text)) Then
            listaDeCampos.Add("Inicio do nosso numero")
        End If
        If (String.IsNullOrEmpty(txtNumeroAgencia.Text)) Then
            listaDeCampos.Add("Numero da agencia")
        End If
        If (String.IsNullOrEmpty(txtNumeroDaConta.Text)) Then
            listaDeCampos.Add("Numero da conta")
        End If
        If (String.IsNullOrEmpty(txtTipoConta.Text)) Then
            listaDeCampos.Add("Tipo da conta")
        End If
        If (String.IsNullOrEmpty(cboBanco.SelectedValue)) Then
            listaDeCampos.Add("Numero do banco")
        End If

        Return listaDeCampos
    End Function

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
            Using Servico As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()
                Servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Cedente excluido com sucesso."), False)
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

    Private Sub ObtenhaCedente(ByVal Pessoa As IPessoa)
        Dim Cedente As ICedente

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()
            Cedente = Servico.Obtenha(Pessoa)
        End Using

        If Cedente Is Nothing Then
            CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
            Exit Sub
        End If

        MostreCedente(Cedente)
        ExibaTelaConsultar()
    End Sub

    Private Sub MostreCedente(ByVal Cedente As ICedente)

        If Not String.IsNullOrEmpty(Cedente.ImagemDeCabecalhoDoReciboDoSacado) Then

            imgImagem.ImageUrl = Cedente.ImagemDeCabecalhoDoReciboDoSacado

        End If

        If Not Cedente.TipoDeCarteira Is Nothing Then ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada = Cedente.TipoDeCarteira

        If Cedente.InicioNossoNumero > 0 Then
            txtIniNossoNumero.Text = Cedente.InicioNossoNumero.ToString()
        End If

        If Not String.IsNullOrEmpty(Cedente.NumeroDaAgencia) Then
            txtNumeroAgencia.Text = Cedente.NumeroDaAgencia
        End If

        If Not String.IsNullOrEmpty(Cedente.NumeroDaConta) Then
            txtNumeroDaConta.Text = Cedente.NumeroDaConta
        End If


        If Cedente.TipoDaConta > 0 Then
            txtTipoConta.Text = Cedente.TipoDaConta.ToString()
        Else
            txtTipoConta.Text = String.Empty
        End If

        If Cedente.Padrao Then
            chkCedentePadrao.Checked = True
        Else
            chkCedentePadrao.Checked = False
        End If

        If Not String.IsNullOrEmpty(Cedente.NumeroDoBanco) Then

            Dim bancoSelecionado As Banco
            bancoSelecionado = Banco.Obtenha(Cedente.NumeroDoBanco)

            Dim Item As New RadComboBoxItem(bancoSelecionado.Nome, bancoSelecionado.ID.ToString)

            Item.Attributes.Add("Numero", bancoSelecionado.ID)
            cboBanco.Items.Add(Item)
            Item.Selected = True
            Item.DataBind()


        End If


    End Sub

    Protected Sub uplImagem_OnFileUploaded(ByVal sender As Object, ByVal e As FileUploadedEventArgs)
        Try
            If uplImagem.UploadedFiles.Count > 0 Then
                Dim arquivo = uplImagem.UploadedFiles(0)
                Dim pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_IMAGEM_CABECALHO_BOLETO)

                Util.CrieDiretorio(pastaDeDestino)

                Dim caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension())

                arquivo.SaveAs(caminhoArquivo)
                imgImagem.ImageUrl = String.Concat(UtilidadesWeb.URL_IMAGEM_CABECALHO_BOLETO, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension())
            End If
        Catch ex As Exception
            Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex)
        End Try
    End Sub

    Private Sub cboBanco_ItemsRequested(sender As Object, e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboBanco.ItemsRequested
        Dim bancos As IList(Of Banco) = Banco.ObtenhaTodos()

        cboBanco.Items.Clear()

        For Each banco As Banco In bancos
            Dim Item As New RadComboBoxItem(banco.Nome, banco.ID.ToString)

            Item.Attributes.Add("Numero", banco.ID)
            cboBanco.Items.Add(Item)
            Item.DataBind()
        Next

    End Sub

    Private Sub cboBanco_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboBanco.SelectedIndexChanged

    End Sub
End Class