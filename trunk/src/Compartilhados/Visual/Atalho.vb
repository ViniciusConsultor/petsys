Namespace Visual

    <Serializable()> _
    Public MustInherit Class Atalho

        Private _Nome As String
        Private _Imagem As String
        Private _URL As String
        Private _ID As String

        Protected Sub New(ByVal ID As String, ByVal Nome As String, ByVal URL As String, ByVal Imagem As String)
            _ID = ID
            _Nome = Nome
            _URL = URL
            _Imagem = Imagem
        End Sub

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public ReadOnly Property Imagem() As String
            Get
                Return _Imagem
            End Get
        End Property

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Public ReadOnly Property URL() As String
            Get
                Return _URL
            End Get
        End Property

        Public MustOverride ReadOnly Property Tipo() As TipoAtalho
        Public MustOverride Function ObtenhaURLCompleta(ByVal ParteDaURL As String) As String
        Public MustOverride Function ObtenhaURLImagemCompleta() As String

    End Class

End Namespace