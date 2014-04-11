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
        txtInicioNossoNumero.Text = String.Empty
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
            Cedente.TipoDeCarteira = ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada.ID.ToString()
        End If

        If Not String.IsNullOrEmpty(txtInicioNossoNumero.Text) Then
            Cedente.InicioNossoNumero = CType(txtInicioNossoNumero.Text, Long)
        End If

        Return Cedente
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String
        Dim Cedente As ICedente = MontaObjetoCedente()

        Try
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

        If Not String.IsNullOrEmpty(Cedente.TipoDeCarteira) Then
            Dim tipoDeCarteira = Interfaces.Core.Negocio.TipoDeCarteira.Obtenha(CType(Cedente.TipoDeCarteira, Short))

            ctrlTipoDeCarteira1.TipoDeCarteiraSelecionada = tipoDeCarteira

        End If

        If Cedente.InicioNossoNumero > 0 Then
            txtInicioNossoNumero.Text = Cedente.InicioNossoNumero.ToString()
        End If

    End Sub

    Protected Sub uplImagem_OnFileUploaded(ByVal sender As Object, ByVal e As FileUploadedEventArgs)
        Try
            If uplImagem.UploadedFiles.Count > 0 Then
                Dim arquivo = uplImagem.UploadedFiles(0)
                Dim pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_IMAGEM_CABECALHO_BOLETO)

                UtilidadesWeb.CrieDiretorio(pastaDeDestino)

                Dim caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension())

                arquivo.SaveAs(caminhoArquivo)
                imgImagem.ImageUrl = String.Concat(UtilidadesWeb.URL_IMAGEM_CABECALHO_BOLETO, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension())
            End If
        Catch ex As Exception
            Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex)
        End Try
    End Sub
End Class