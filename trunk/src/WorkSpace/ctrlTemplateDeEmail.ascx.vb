Public Class ctrlTemplateDeEmail
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Property TextoDoTemplate() As String
        Get
            Return txtTextoDoTemplate.Content
        End Get
        Set(value As String)
            txtTextoDoTemplate.Content = value
        End Set
    End Property


    Public Sub AdicionaTextoNoTemplate(texto As String)
        txtTextoDoTemplate.Content = txtTextoDoTemplate.Content & " [" & texto & "]"
    End Sub

    Public Sub Inicializa()
        txtTextoDoTemplate.Content = ""
    End Sub

End Class