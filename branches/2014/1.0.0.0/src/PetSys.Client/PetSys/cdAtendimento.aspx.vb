Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Telerik.Web.UI

Partial Public Class cdAtendimento
    Inherits SuperPagina

    Private Const CHAVE_VACINAS_DO_ATENDIMENTO As String = "CHAVE_VACINAS_DO_ATENDIMENTO"
    Private Const CHAVE_VERMIFUGOS_DO_ATENDIMENTO As String = "CHAVE_VERMIFUGOS_DO_ATENDIMENTO"
    Private Const CHAVE_ID_ANIMAL_ATENDIDO As String = "CHAVE_ID_ANIMAL_ATENDIDO"
    Private Const CHAVE_ID_ATENDIMENTO As String = "CHAVE_ID_ATENDIMENTO_CDATENDIMENTO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim IdAnimal As Nullable(Of Long)
            Dim IdAtendimento As Nullable(Of Long) = Nothing

            If Not String.IsNullOrEmpty(Request.QueryString("IdAnimal")) Then
                IdAnimal = CLng(Request.QueryString("IdAnimal"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("IdAtendimento")) Then
                IdAtendimento = CLng(Request.QueryString("IdAtendimento"))
            End If

            ViewState(CHAVE_ID_ANIMAL_ATENDIDO) = IdAnimal
            ViewState(CHAVE_ID_ATENDIMENTO) = IdAtendimento

            If IdAtendimento Is Nothing Then
                Me.ExibaTelaNovo()
            Else
                Me.ExibaTelaDetalhes()
            End If
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlDadosGerais, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaVacina, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoVermifugo, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoAnimal, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProntuario, Control))

        Dim Animal As IAnimal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL_ATENDIDO)))

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

    Private Sub ExibaTelaNovo()
        ExibaTelaInicial()
        Dim Vacinas As IList(Of IVacina) = New List(Of IVacina)
        ViewState(CHAVE_VACINAS_DO_ATENDIMENTO) = Vacinas

        Dim Vermifugos As IList(Of IVermifugo) = New List(Of IVermifugo)
        ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO) = Vermifugos
        ExibaVacinasDoAtendimento()
        ExibaVermifugosDoAtendimento()
    End Sub

    Private Sub ExibaVacinasDoAtendimento()
        grdVacinas.DataSource = ViewState(CHAVE_VACINAS_DO_ATENDIMENTO)
        grdVacinas.DataBind()
    End Sub

    Private Sub ExibaVermifugosDoAtendimento()
        grdVermifugos.DataSource = ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO)
        grdVermifugos.DataBind()
    End Sub

    Private Sub ExibaTelaDetalhes()
        ExibaTelaInicial()

        Dim Atendimento As IAtendimento

        Using Servico As IServicoDeAtendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimento)()
            Atendimento = Servico.ObtenhaAtendimento(CLng(ViewState(CHAVE_ID_ATENDIMENTO)))
        End Using

        ExibaObjetoAtendimento(Atendimento)
    End Sub

    Private Sub ExibaObjetoAtendimento(ByVal Atendimento As IAtendimento)
        txtDataDoRetorno.SelectedDate = Atendimento.DataEHoraDoRetorno

        If Atendimento.Peso.HasValue Then txtPesoDoAnimal.Text = Atendimento.Peso.Value.ToString
        txtPrognostico.Text = Atendimento.Prognostico
        txtQueixas.Text = Atendimento.Queixa
        txtSinaisClinicos.Text = Atendimento.SinaisClinicos
        txtTratamento.Text = Atendimento.Tratamento
        ViewState(CHAVE_VACINAS_DO_ATENDIMENTO) = Atendimento.Vacinas
        ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO) = Atendimento.Vermifugos
        ExibaVacinasDoAtendimento()
        ExibaVermifugosDoAtendimento()
    End Sub

    Private Function ValidaNovaVacina() As String
        If Not txtDataDaVacinacao.SelectedDate.HasValue Then Return "A data da vacina deve ser informada."
        If String.IsNullOrEmpty(txtNomeDaVacina.Text) Then Return "O nome da vacina deve ser informado."
        Return Nothing
    End Function

    Private Function ObtenhaObjetoVacina() As IVacina
        Dim Vacina As IVacina

        Vacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IVacina)()
        Vacina.Nome = txtNomeDaVacina.Text
        Vacina.Observacao = txtObservacaoDaVacina.Text
        Vacina.DataDaVacinacao = txtDataDaVacinacao.SelectedDate.Value
        Vacina.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL_ATENDIDO)))
        Vacina.RevacinarEm = txtRevacinar.SelectedDate

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Vacina.VeterinarioQueAplicou = VeterinarioLazyLoad

        Return Vacina
    End Function

    Private Sub btnAdicionarVacina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdicionarVacina.Click
        Dim Inconsistencia As String

        Inconsistencia = ValidaNovaVacina()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Dim Vacinas As IList(Of IVacina) = CType(Me.ViewState(CHAVE_VACINAS_DO_ATENDIMENTO), IList(Of IVacina))

        Vacinas.Add(ObtenhaObjetoVacina)
        Me.ViewState(CHAVE_VACINAS_DO_ATENDIMENTO) = Vacinas
        ExibaVacinasDoAtendimento()
    End Sub

    Private Function ObtenhaObjetoVermifugo() As IVermifugo
        Dim Vermifugo As IVermifugo

        Vermifugo = FabricaGenerica.GetInstancia.CrieObjeto(Of IVermifugo)()
        Vermifugo.Nome = txtNomeVermifugo.Text
        Vermifugo.Observacao = txtObservacaoVermifugo.Text
        Vermifugo.Data = txtDataVermifugo.SelectedDate.Value
        Vermifugo.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL_ATENDIDO)))
        Vermifugo.ProximaDoseEm = txtProximaDose.SelectedDate

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Vermifugo.VeterinarioQueReceitou = VeterinarioLazyLoad

        Return Vermifugo
    End Function

    Private Function ValidaNovoVermifugo() As String
        If Not txtDataVermifugo.SelectedDate.HasValue Then Return "A data do vermífugo deve ser informada."
        If String.IsNullOrEmpty(txtNomeVermifugo.Text) Then Return "O nome do vermífugo deve ser informado."
        Return Nothing
    End Function

    Private Sub btnAdicionarVermifugo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdicionarVermifugo.Click
        Dim Inconsistencia As String

        Inconsistencia = ValidaNovoVermifugo()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Dim Vermifugos As IList(Of IVermifugo) = CType(Me.ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO), IList(Of IVermifugo))

        Vermifugos.Add(ObtenhaObjetoVermifugo)
        Me.ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO) = Vermifugos
        ExibaVermifugosDoAtendimento()
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalvar_Click()
        End Select
    End Sub

    Private Sub btnSalvar_Click()
        Try
            Using Servico As IServicoDeAtendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimento)()
                Servico.Insira(MontaObjetoAtendimento)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Atendimento cadastrado com sucesso."), False)
            End Using

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try

    End Sub

    Private Function MontaObjetoAtendimento() As IAtendimento
        Dim Atendimento As IAtendimento

        Atendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IAtendimento)()
        Atendimento.Animal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(CLng(ViewState(CHAVE_ID_ANIMAL_ATENDIDO)))
        Atendimento.DataEHoraDoAtendimento = Now
        If txtDataDoRetorno.SelectedDate.HasValue Then Atendimento.DataEHoraDoRetorno = txtDataDoRetorno.SelectedDate

        If Not ViewState(CHAVE_ID_ATENDIMENTO) Is Nothing Then
            Atendimento.ID = CLng(ViewState(CHAVE_ID_ATENDIMENTO))
        End If

        If Not String.IsNullOrEmpty(txtPesoDoAnimal.Text) Then Atendimento.Peso = CDbl(txtPesoDoAnimal.Text)
        Atendimento.Prognostico = txtPrognostico.Text
        Atendimento.Queixa = txtQueixas.Text
        Atendimento.SinaisClinicos = txtSinaisClinicos.Text
        Atendimento.Tratamento = txtTratamento.Text
        Atendimento.Vermifugos = CType(ViewState(CHAVE_VERMIFUGOS_DO_ATENDIMENTO), IList(Of IVermifugo))
        Atendimento.Vacinas = CType(ViewState(CHAVE_VACINAS_DO_ATENDIMENTO), IList(Of IVacina))

        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad
        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID)
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Atendimento.Veterinario = VeterinarioLazyLoad

        Return Atendimento
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

End Class