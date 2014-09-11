Namespace Visual

    <Serializable()> _
    Public Class PerfilUsuario
        Inherits Perfil

        Public Sub New(ByVal ImagemDesktop As String, _
                       ByVal Skin As String)
            MyBase.New(ImagemDesktop, Skin)
        End Sub

        Public Overrides ReadOnly Property Tipo() As TipoDePerfil
            Get
                Return TipoDePerfil.Usuario
            End Get
        End Property
    End Class

End Namespace