Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class ctrlGrupo
    Inherits System.Web.UI.UserControl

    Public Event GrupoFoiSelecionado(ByVal Grupo As IGrupo)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlGrupo, Control))
        GrupoSelecionado = Nothing
        EhObrigatorio = False
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGrupo, Control), Habilitar)
    End Sub

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboGrupos.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboGrupos.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public Property NomeDoGrupo() As String
        Get
            Return cboGrupos.Text
        End Get
        Set(ByVal value As String)
            cboGrupos.Text = value
        End Set
    End Property

    Public Property GrupoSelecionado() As IGrupo
        Get
            Return CType(Session(Me.ClientID), IGrupo)
        End Get
        Set(ByVal value As IGrupo)
            Session.Add(Me.ClientID, value)
        End Set
    End Property

    Protected Sub cboGrupos_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboGrupos.ItemsRequested
        Dim Grupos As IList(Of IGrupo)

        Using Servico As IServicoDeGrupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupo)()
            Grupos = Servico.ObtenhaGruposPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Grupos Is Nothing Then
            For Each Grupo As IGrupo In Grupos
                cboGrupos.Items.Add(New RadComboBoxItem(Grupo.Nome, Grupo.ID.ToString))
            Next
        End If

    End Sub

    Private Sub cboGrupos_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGrupos.SelectedIndexChanged
        Dim Grupo As IGrupo
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeGrupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupo)()
            Grupo = Servico.ObtenhaGrupo(CLng(Valor))
        End Using

        GrupoSelecionado = Grupo
        RaiseEvent GrupoFoiSelecionado(Grupo)
    End Sub

    Public WriteOnly Property EhObrigatorio() As Boolean
        Set(ByVal value As Boolean)
            cboGrupos.CausesValidation = value
        End Set
    End Property
End Class