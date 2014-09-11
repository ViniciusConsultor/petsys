Namespace Visual

    <Serializable()> _
    Public Class AtalhoSistema
        Inherits Atalho

        Public Sub New(ByVal ID As String, _
                       ByVal Nome As String, _
                       ByVal URL As String, _
                       ByVal Imagem As String)
            MyBase.New(ID, Nome, URL, Imagem)
        End Sub

        Public Overrides ReadOnly Property Tipo() As TipoAtalho
            Get
                Return TipoAtalho.Sistema
            End Get
        End Property

        Public Overrides Function ObtenhaURLCompleta(ByVal ParteDaURL As String) As String
            Return String.Concat(ParteDaURL, MyBase.URL)
        End Function

        Public Overrides Function ObtenhaURLImagemCompleta() As String
            Return String.Concat("imagens/", MyBase.Imagem, ".png")
        End Function
    End Class

End Namespace