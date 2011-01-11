Public Partial Class cdAtendimento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub ExibaTelaNovo()
        'CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        'CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        'UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        'UtilidadesWeb.HabilitaComponentes(CType(rdkDadosPessoa, Control), True)
        'CarregueComponentes()
        'ViewState(CHAVE_ESTADO) = Estado.Novo
        'ViewState(CHAVE_ID) = Nothing
        'ViewState(CHAVE_TELEFONES) = Nothing
        'imgFoto.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO
    End Sub

    Private Sub ExibaTelaDetalhes(ByVal Id As Long)
        'CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        'CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        'CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        'UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control))
        'UtilidadesWeb.HabilitaComponentes(CType(pnlDadosPessoais, Control), False)
        'UtilidadesWeb.HabilitaComponentes(CType(pnlDocumentos, Control), False)
        'UtilidadesWeb.HabilitaComponentes(CType(pnlEndereco, Control), False)
        'UtilidadesWeb.HabilitaComponentes(CType(pnlContatos, Control), False)
        'CarregueComponentes()

        'Dim Pessoa As IPessoaFisica

        'Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
        '    Pessoa = Servico.ObtenhaPessoa(Id)
        'End Using

        'Me.ExibaObjeto(Pessoa)
    End Sub

End Class