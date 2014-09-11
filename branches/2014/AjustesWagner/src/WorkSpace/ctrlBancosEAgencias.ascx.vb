Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Partial Public Class ctrlBancosEAgencias
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub cboBancos_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboBancos.ItemsRequested
        Dim Bancos As IList(Of Banco) = Banco.ObtenhaTodos()
        
        If Not Bancos Is Nothing Then
            For Each Banco As Banco In Bancos
                Dim Item As New RadComboBoxItem(Banco.Nome, Banco.ID.ToString)

                Item.Attributes.Add("Numero", Banco.ID.ToString)
                cboBancos.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Public Property BancoSelecionado() As Banco
        Get
            Return CType(ViewState(Me.ClientID & "_BANCO_"), Banco)
        End Get
        Set(ByVal value As Banco)
            ViewState.Add(Me.ClientID & "_BANCO_", value)
            cboBancos.Text = value.Nome
        End Set
    End Property

    Public Property AgenciaSelecionada() As IAgencia
        Get
            Return CType(ViewState(Me.ClientID & "_AGENCIA_"), IAgencia)
        End Get
        Set(ByVal value As IAgencia)
            ViewState.Add(Me.ClientID & "_AGENCIA_", value)
            cboAgencias.Text = value.Nome
        End Set
    End Property

    Private Sub cboBancos_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboBancos.SelectedIndexChanged
        Dim Banco As Banco
        Dim Valor As String
        
        Valor = DirectCast(sender, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            BancoSelecionado = Nothing
            Return
        End If

        Banco = Banco.Obtenha(Valor)
        BancoSelecionado = Banco
    End Sub

    Private Sub cboAgencias_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboAgencias.ItemsRequested
        If BancoSelecionado Is Nothing Then Exit Sub

        Dim Agencias As IList(Of IAgencia)

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
            Agencias = Servico.ObtenhaAgenciasPorNomeComoFiltro(BancoSelecionado, e.Text, 50)
        End Using

        If Not Agencias Is Nothing Then
            For Each Agencia As IAgencia In Agencias
                Dim Item As New RadComboBoxItem(Agencia.Nome, Agencia.ID.ToString)

                Item.Attributes.Add("Numero", Agencia.Numero)
                cboAgencias.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Public Property NumeroDaConta() As String
        Get
            Return txtNumeroDaConta.Text
        End Get
        Set(ByVal value As String)
            txtNumeroDaConta.Text = value
        End Set
    End Property

    Public Property TipoDaConta() As Nullable(Of Integer)
        Get
            Return CType(txtTipoConta.Value, Integer?)
        End Get
        Set(ByVal value As Nullable(Of Integer))
            txtTipoConta.Value = value
        End Set
    End Property

    Private Sub cboAgencias_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAgencias.SelectedIndexChanged
        Dim Agencia As IAgencia
        Dim Valor As String

        Valor = DirectCast(sender, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            AgenciaSelecionada = Nothing
            Return
        End If

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
            Agencia = Servico.ObtenhaAgencia(BancoSelecionado.ID, CLng(Valor))
        End Using

        AgenciaSelecionada = Agencia
    End Sub
End Class