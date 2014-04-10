Namespace Core.Negocio

    <Serializable()> _
    Public Class TipoDeCarteira

        Private _ID As Short
        Private _Nome As String
        Private _Sigla As String

        Public Shared SR As TipoDeCarteira = New TipoDeCarteira(1, "Carteira sem registro", "SR")

        Private Shared Lista As TipoDeCarteira() = {SR}

        Private Sub New(ByVal ID As Short, _
                            ByVal Nome As String, _
                            ByVal Sigla As String)
            _ID = ID
            _Nome = Nome
            _Sigla = Sigla
        End Sub

        Public ReadOnly Property ID() As Short
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public ReadOnly Property Sigla() As String
            Get
                Return _Sigla
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Short) As TipoDeCarteira
            For Each item As TipoDeCarteira In From item1 In Lista Where item1.ID = ID
                Return item
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDeCarteira)
            Return New List(Of TipoDeCarteira)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDeCarteira).ID = Me.ID
        End Function

    End Class

End Namespace
