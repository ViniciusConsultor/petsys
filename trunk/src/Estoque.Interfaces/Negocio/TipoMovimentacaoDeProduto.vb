Namespace Negocio

    <Serializable()> _
    Public Class TipoMovimentacaoDeProduto
        Private _ID As Char
        Private _Descricao As String

        Public Shared Entrada As TipoMovimentacaoDeProduto = New TipoMovimentacaoDeProduto("0"c, "Entrada")
        Public Shared Saida As TipoMovimentacaoDeProduto = New TipoMovimentacaoDeProduto("1"c, "Saída")

        Private Shared ListaDeTipos As TipoMovimentacaoDeProduto() = {Entrada, Saida}

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

        Public Shared Function ObtenhaTipoMovimentacaoDeProduto(ByVal ID As Char) As TipoMovimentacaoDeProduto
            For Each Tipo As TipoMovimentacaoDeProduto In ListaDeTipos
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoMovimentacaoDeProduto)
            Return New List(Of TipoMovimentacaoDeProduto)(ListaDeTipos)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoMovimentacaoDeProduto).ID = Me.ID
        End Function

    End Class

End Namespace