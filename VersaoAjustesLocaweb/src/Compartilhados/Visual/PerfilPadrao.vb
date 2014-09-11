Namespace Visual

    <Serializable()> _
    Public Class PerfilPadrao
        Inherits Perfil

        Public Sub New()
            MyBase.New(Util.ObtenhaImagemPadrao, Util.ObtenhaSkinPadrao)
        End Sub

        Public Overrides ReadOnly Property Tipo() As TipoDePerfil
            Get
                Return TipoDePerfil.Padrao
            End Get
        End Property

    End Class

End Namespace