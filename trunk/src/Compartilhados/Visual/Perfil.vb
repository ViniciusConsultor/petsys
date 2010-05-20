Namespace Visual

    <Serializable()> _
    Public MustInherit Class Perfil

        Private _ImagemDesktop As String
        Private _Skin As String
        Private _Atalhos As IList(Of Atalho)

        Protected Sub New(ByVal ImagemDesktop As String, _
                          ByVal Skin As String)
            _ImagemDesktop = ImagemDesktop
            _Skin = Skin
            _Atalhos = New List(Of Atalho)
        End Sub

        Public Property ImagemDesktop() As String
            Get
                Return _ImagemDesktop
            End Get
            Set(ByVal value As String)
                _ImagemDesktop = value
            End Set
        End Property

        Public Property Skin() As String
            Get
                Return _Skin
            End Get
            Set(ByVal value As String)
                _Skin = value
            End Set
        End Property

        Public ReadOnly Property Atalhos() As IList(Of Atalho)
            Get
                Return _Atalhos
            End Get
        End Property

        Public Sub AdicioneAtalho(ByVal Atalho As Atalho)
            If Not Atalhos.Contains(Atalho) Then
                Atalhos.Add(Atalho)
            End If
        End Sub

        Public MustOverride ReadOnly Property Tipo() As TipoDePerfil

    End Class

End Namespace