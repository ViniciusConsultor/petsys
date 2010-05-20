Namespace Visual

    <Serializable()> _
    Public Class AtalhoExterno
        Inherits Atalho

        Public Sub New(ByVal Nome As String, _
                       ByVal URL As String, _
                       ByVal Imagem As String)
            MyBase.New(Guid.NewGuid.ToString, Nome, URL, Imagem)
        End Sub

        Public Overrides ReadOnly Property Tipo() As TipoAtalho
            Get
                Return TipoAtalho.Externo
            End Get
        End Property

        Public Overrides Function ObtenhaURLCompleta(ByVal ParteDaURL As String) As String
            Return MyBase.URL
        End Function

        Public Overrides Function ObtenhaURLImagemCompleta() As String
            Return MyBase.Imagem
        End Function

    End Class

End Namespace