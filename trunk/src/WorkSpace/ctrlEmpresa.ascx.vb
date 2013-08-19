Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports Telerik.Web.UI

Public Class ctrlEmpresa
    Inherits System.Web.UI.UserControl

    Public Event EmpresaFoiSelecionada(ByVal Empresa As IEmpresa)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlEmpresa, Control))
        EmpresaSelecionada = Nothing
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresa, Control), Habilitar)
    End Sub

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboEmpresa.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboEmpresa.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public Property NomeDaEmpresa() As String
        Get
            Return cboEmpresa.Text
        End Get
        Set(ByVal value As String)
            cboEmpresa.Text = value
        End Set
    End Property

    Public Property EmpresaSelecionada() As IEmpresa
        Get
            Return CType(ViewState(Me.ClientID), IEmpresa)
        End Get
        Set(ByVal value As IEmpresa)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Protected Sub cboEmpresa_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboEmpresa.ItemsRequested
        Dim Empresas As IList(Of IEmpresa)

        Using Servico As IServicoDeEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeEmpresa)()
            Empresas = Servico.ObtenhaPorNome(e.Text, 50)
        End Using

        If Not Empresas Is Nothing Then
            For Each Empresa As IEmpresa In Empresas
                Dim Item As New RadComboBoxItem(Empresa.Pessoa.Nome, Empresa.Pessoa.ID.ToString)

                Item.Attributes.Add("NomeFantasia", DirectCast(Empresa.Pessoa, IPessoaJuridica).NomeFantasia)
                cboEmpresa.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboEmpresa_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboEmpresa.SelectedIndexChanged
        Dim Empresa As IEmpresa
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeEmpresa)()
            Empresa = Servico.Obtenha(CLng(Valor))
        End Using

        EmpresaSelecionada = Empresa
        RaiseEvent EmpresaFoiSelecionada(Empresa)
    End Sub

End Class