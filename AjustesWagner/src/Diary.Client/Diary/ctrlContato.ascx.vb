Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Partial Public Class ctrlContato
    Inherits System.Web.UI.UserControl

    Public Event ContatoFoiSelecionado(ByVal Contato As IContato)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public WriteOnly Property EstaAtivo() As Boolean
        Set(ByVal value As Boolean)
            cboContato.Enabled = value
        End Set
    End Property

    Private Sub cboContato_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboContato.ItemsRequested
        Dim Contatos As IList(Of IContato)

        Using Servico As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contatos = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Contatos Is Nothing Then
            For Each Contato As IContato In Contatos
                Dim Item As New RadComboBoxItem(Contato.Pessoa.Nome, Contato.Pessoa.ID.ToString)

                Dim TelefonesCelular As IList(Of ITelefone)
                Dim TelefonesComercial As IList(Of ITelefone)

                TelefonesCelular = Contato.Pessoa.ObtenhaTelefones(TipoDeTelefone.Celular)
                TelefonesComercial = Contato.Pessoa.ObtenhaTelefones(TipoDeTelefone.Comercial)

                Dim TelefonesSTR As New StringBuilder

                For Each Telefone As ITelefone In TelefonesComercial
                    TelefonesSTR.AppendLine(Telefone.ToString & "<BR>")
                Next

                Item.Attributes.Add("Telefone", TelefonesSTR.ToString)

                Dim CelularesSTR As New StringBuilder

                For Each Celular As ITelefone In TelefonesCelular
                    CelularesSTR.AppendLine(Celular.ToString & "<BR>")
                Next

                Item.Attributes.Add("Celular", CelularesSTR.ToString)

                If Not String.IsNullOrEmpty(Contato.Cargo) Then
                    Item.Attributes.Add("Cargo", Contato.Cargo)
                Else
                    Item.Attributes.Add("Cargo", " ")
                End If

                cboContato.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboContato_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContato.SelectedIndexChanged
        Dim Contato As IContato

        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then Exit Sub

        Using Servico As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contato = Servico.Obtenha(CLng(DirectCast(o, RadComboBox).SelectedValue))
        End Using

        ContatoSelecionado = Contato
        RaiseEvent ContatoFoiSelecionado(Contato)
    End Sub

    Public Property ContatoSelecionado() As IContato
        Get
            Return CType(ViewState(Me.ClientID), IContato)
        End Get
        Set(ByVal value As IContato)
            ViewState.Add(Me.ClientID, value)

            If Not value Is Nothing Then
                cboContato.Text = value.Pessoa.Nome
            End If
        End Set
    End Property

End Class