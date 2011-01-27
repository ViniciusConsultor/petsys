Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Telerik.Web.UI

Partial Public Class cdAtendimento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim IdAnimal As Nullable(Of Long)
            Dim IdAtendimento As Nullable(Of Long) = Nothing

            If Not String.IsNullOrEmpty(Request.QueryString("IdAnimal")) Then
                IdAnimal = CLng(Request.QueryString("IdAnimal"))
            End If

            If IdAtendimento Is Nothing Then
                Me.ExibaTelaNovo(IdAnimal.Value)
            Else
                Me.ExibaTelaDetalhes(IdAnimal.Value)
            End If
        End If
    End Sub

    Private Sub ExibaTelaNovo(ByVal IdAnimal As Long)
        'CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        'CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        'CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        'UtilidadesWeb.LimparComponente(CType(rdkDadosPessoa, Control)
        UtilidadesWeb.LimparComponente(CType(pnlDadosGerais, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoAnimal, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProntuario, Control))

        Dim Animal As IAnimal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(IdAnimal)

        crtlAnimalResumido1.ApresentaDadosResumidosDoAnimal(Animal)

        lblDataEHora.Text = Now.ToString("dd/MM/yyyy HH:mm")

        Dim UsuarioLogadoEhVeterinario As Boolean

        Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
            UsuarioLogadoEhVeterinario = Servico.VerificaSePessoaEhVeterinario(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        End Using

        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = UsuarioLogadoEhVeterinario

        If UsuarioLogadoEhVeterinario Then
            lblVeterinario.Text = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.Nome
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Para utilizar a funcionalidade de atendimento o usuário precisa ser um veterinário."), False)
        End If
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