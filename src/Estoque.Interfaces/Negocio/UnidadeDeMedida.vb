Namespace Negocio

    <Serializable()> _
    Public Class UnidadeDeMedida
        Private _ID As Char
        Private _Descricao As String

        Public Shared Unidade As UnidadeDeMedida = New UnidadeDeMedida("0"c, "Unidade")
        Public Shared Metro As UnidadeDeMedida = New UnidadeDeMedida("1"c, "Metro")
        Public Shared MetroQuadrado As UnidadeDeMedida = New UnidadeDeMedida("2"c, "Metro quadrado")
        Public Shared MetroCubico As UnidadeDeMedida = New UnidadeDeMedida("3"c, "Metro cúbico")
        Public Shared Quilograma As UnidadeDeMedida = New UnidadeDeMedida("4"c, "Quilograma")
        Public Shared QuilogramaPorMetroCubico As UnidadeDeMedida = New UnidadeDeMedida("5"c, "Quilograma por metro cúbico")
        Public Shared Litro As UnidadeDeMedida = New UnidadeDeMedida("6"c, "Litro")
        Public Shared Tonelada As UnidadeDeMedida = New UnidadeDeMedida("7"c, "Tonelada")

        Private Shared ListaDeTiposDeUnidade As UnidadeDeMedida() = {Unidade, Metro, MetroQuadrado, MetroCubico, _
                                                                    Quilograma, QuilogramaPorMetroCubico, Litro, Tonelada}

        Private Sub New(ByVal ID As Char, _
                        ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Public ReadOnly Property ID() As Char
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function ObtenhaTipoDeUnidade(ByVal ID As Char) As UnidadeDeMedida
            For Each Tipo As UnidadeDeMedida In ListaDeTiposDeUnidade
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of UnidadeDeMedida)
            Return New List(Of UnidadeDeMedida)(ListaDeTiposDeUnidade)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, UnidadeDeMedida).ID = Me.ID
        End Function

    End Class

End Namespace
