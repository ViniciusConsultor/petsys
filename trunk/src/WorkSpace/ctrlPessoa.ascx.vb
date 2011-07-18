Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class ctrlPessoa
    Inherits System.Web.UI.UserControl

    Public Event PessoaFoiSelecionada(ByVal Pessoa As IPessoa)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
        CarregaTipos()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlPessoa, Control))
        PessoaSelecionada = Nothing
    End Sub

    Private WriteOnly Property HabilitaControles() As Boolean
        Set(ByVal value As Boolean)
            UtilidadesWeb.HabilitaComponentes(CType(pnlPessoa, Control), value)
        End Set
    End Property

    Private Sub CarregaTipos()
        rblTipo.Items.Clear()

        For Each Tipo As TipoDePessoa In TipoDePessoa.ObtenhaTodos
            rblTipo.Items.Add(New ListItem(Tipo.Descricao, Tipo.ID.ToString))
        Next

        TipoDaPessoa = TipoDePessoa.Fisica
    End Sub

    Public WriteOnly Property OpcaoTipoDaPessoaEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            pnlTipoDePessoa.Visible = value
        End Set
    End Property

    Private Function ObtenhaComboAtiva() As RadComboBox
        If TipoDaPessoa.Equals(TipoDePessoa.Fisica) Then
            Return cboPessoaFisica
        End If

        Return cboPessoaJuridica
    End Function

    Private Property NomeDaPessoa() As String
        Get
            Return ObtenhaComboAtiva.Text
        End Get
        Set(ByVal value As String)
            ObtenhaComboAtiva.Text = value
        End Set
    End Property

    Private Property TipoDaPessoa() As TipoDePessoa
        Get
            Return TipoDePessoa.Obtenha(CShort(rblTipo.SelectedValue))
        End Get
        Set(ByVal value As TipoDePessoa)
            rblTipo.SelectedValue = value.ID.ToString
            rblTipo_SelectedIndexChanged(Nothing, Nothing)
        End Set
    End Property

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de pessoas", 650, 480), False)
    End Sub

    Private Sub btnDetalhar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDetalhar.Click
        Dim URL As String

        URL = ObtenhaURL()
        URL = String.Concat(URL, "?Id=", PessoaSelecionada.ID.Value.ToString)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de pessoas", 650, 480), False)
    End Sub

    Private Function ObtenhaURL() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual

        If TipoDaPessoa.Equals(TipoDePessoa.Fisica) Then
            URL = String.Concat(URL, "Nucleo/cdPessoaFisica.aspx")
        ElseIf TipoDaPessoa.Equals(TipoDePessoa.Juridica) Then
            URL = String.Concat(URL, "Nucleo/cdPessoaJuridica.aspx")
        End If

        Return URL
    End Function

    Protected Sub cboPessoaFisica_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboPessoaFisica.ItemsRequested
        Dim Pessoas As IList(Of IPessoaFisica)

        Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            Pessoas = Servico.ObtenhaPessoasPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Pessoas Is Nothing Then
            For Each Pessoa As IPessoaFisica In Pessoas
                Dim Item As New RadComboBoxItem(Pessoa.Nome.Trim, Pessoa.ID.ToString)

                If Pessoa.DataDeNascimento.HasValue Then
                    Item.Attributes.Add("DataNascimento", Pessoa.DataDeNascimento.Value.ToString("dd/MM/yyyy"))
                Else
                    Item.Attributes.Add("DataNascimento", "")
                End If

                Dim TelefonesComercial As IList(Of ITelefone)
                Dim TelefonesCelular As IList(Of ITelefone)

                TelefonesComercial = Pessoa.ObtenhaTelefones(TipoDeTelefone.Comercial)
                TelefonesCelular = Pessoa.ObtenhaTelefones(TipoDeTelefone.Celular)

                If Not TelefonesComercial Is Nothing Then
                    Dim TelefonesSTR As New StringBuilder

                    For Each Telefone As ITelefone In TelefonesComercial
                        TelefonesSTR.Append(Telefone.ToString & "<br>")
                    Next

                    Item.Attributes.Add("TelefoneComercial", TelefonesSTR.ToString.Trim)
                Else
                    Item.Attributes.Add("TelefoneComercial", "")
                End If

                If Not TelefonesCelular Is Nothing Then
                    Dim TelefonesSTR As New StringBuilder

                    For Each Telefone As ITelefone In TelefonesCelular
                        TelefonesSTR.Append(Telefone.ToString & "<br>")
                    Next
                    Item.Attributes.Add("TelefoneCelular", TelefonesSTR.ToString.Trim)
                Else
                    Item.Attributes.Add("TelefoneCelular", "")
                End If

                cboPessoaFisica.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Protected Sub cboPessoaFisica_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPessoaFisica.SelectedIndexChanged
        Dim Pessoa As IPessoa

        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then Exit Sub

        Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            Pessoa = Servico.ObtenhaPessoa(CLng(DirectCast(o, RadComboBox).SelectedValue))
        End Using

        PessoaSelecionada = Pessoa
        RaiseEvent PessoaFoiSelecionada(Pessoa)
    End Sub

    Public Property PessoaSelecionada() As IPessoa
        Get
            Return CType(ViewState(Me.ClientID), IPessoa)
        End Get
        Set(ByVal value As IPessoa)
            ViewState.Add(Me.ClientID, value)

            If Not value Is Nothing Then
                TipoDaPessoa = value.Tipo
                NomeDaPessoa = value.Nome

            End If
        End Set
    End Property

    Private Sub rblTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblTipo.SelectedIndexChanged

        If TipoDaPessoa.Equals(TipoDePessoa.Fisica) Then
            cboPessoaFisica.Visible = True
            cboPessoaJuridica.Visible = False
        ElseIf TipoDaPessoa.Equals(TipoDePessoa.Juridica) Then
            cboPessoaFisica.Visible = False
            cboPessoaJuridica.Visible = True
        End If
    End Sub

    Public WriteOnly Property BotaoNovoEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            btnNovo.Visible = value
        End Set
    End Property

    Public WriteOnly Property BotaoDetalharEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            btnDetalhar.Visible = value
        End Set
    End Property

    Public WriteOnly Property TextoItemVazio() As String
        Set(ByVal value As String)
            cboPessoaFisica.EmptyMessage = value
        End Set
    End Property

    'Verifica se o usuário tem permissão para cadastrar nova pessoa e se tem permissão para detalhar (ver os dados)
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnDetalhar.Visible Then
            Principal.EstaAutorizado(btnDetalhar.CommandArgument)
        End If
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnNovo.Visible Then
            Principal.EstaAutorizado(btnNovo.CommandArgument)
        End If
    End Sub

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboPessoaFisica.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboPessoaFisica.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public WriteOnly Property EhEditavel() As Boolean
        Set(ByVal value As Boolean)
            UtilidadesWeb.HabilitaComponentes(CType(pnlPessoa, Control), value)
        End Set
    End Property

    Private Sub cboPessoaJuridica_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboPessoaJuridica.ItemsRequested
        Dim Pessoas As IList(Of IPessoaJuridica)

        Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
            Pessoas = Servico.ObtenhaPessoasPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Pessoas Is Nothing Then
            For Each Pessoa As IPessoaJuridica In Pessoas
                Dim Item As New RadComboBoxItem(Pessoa.Nome, Pessoa.ID.ToString)

                Item.Attributes.Add("NomeFantasia", Pessoa.NomeFantasia)
                cboPessoaJuridica.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboPessoaJuridica_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPessoaJuridica.SelectedIndexChanged
        Dim Pessoa As IPessoa

        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then Exit Sub

        Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
            Pessoa = Servico.ObtenhaPessoa(CLng(DirectCast(o, RadComboBox).SelectedValue))
        End Using

        PessoaSelecionada = Pessoa
        RaiseEvent PessoaFoiSelecionada(Pessoa)
    End Sub

    Public Sub SetaTipoDePessoaPadrao(ByVal Tipo As TipoDePessoa)
        TipoDaPessoa = Tipo
    End Sub

End Class