Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Public Class ctrlGrupoDeAtividade
    Inherits System.Web.UI.UserControl

    Public Event GrupoFoiSelecionado(ByVal Grupo As IGrupoDeAtividade)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboGrupos, Control))
        GrupoSelecionado = Nothing
        cboGrupos.ClearSelection()
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(cboGrupos, Control), Habilitar)
    End Sub

    Public Property NomeDoGrupo() As String
        Get
            Return cboGrupos.Text
        End Get
        Set(ByVal value As String)
            cboGrupos.Text = value
        End Set
    End Property

    Public Property GrupoSelecionado() As IGrupoDeAtividade
        Get
            Return CType(ViewState(Me.ClientID), IGrupoDeAtividade)
        End Get
        Set(ByVal value As IGrupoDeAtividade)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Protected Sub cboGrupos_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboGrupos.ItemsRequested
        Dim Grupos As IList(Of IGrupoDeAtividade)

        Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
            Grupos = Servico.ObtenhaPorNome(e.Text, 50)
        End Using

        If Not Grupos Is Nothing Then
            For Each Grupo As IGrupoDeAtividade In Grupos
                cboGrupos.Items.Add(New RadComboBoxItem(Grupo.Nome, Grupo.ID.ToString))
            Next
        End If

    End Sub

    Private Sub cboGrupos_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGrupos.SelectedIndexChanged
        Dim Grupo As IGrupoDeAtividade
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            GrupoSelecionado = Nothing
            Exit Sub
        End If

        Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
            Grupo = Servico.Obtenha(CLng(Valor))
        End Using

        GrupoSelecionado = Grupo
        RaiseEvent GrupoFoiSelecionado(Grupo)
    End Sub

    Public WriteOnly Property ExibeTituloParaSelecionarUmItem() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                cboGrupos.EmptyMessage = "Selecione um grupo de atividade"
                Exit Property
            End If
            cboGrupos.EmptyMessage = ""
        End Set
    End Property

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            cboGrupos.AutoPostBack = value
        End Set
    End Property

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

End Class