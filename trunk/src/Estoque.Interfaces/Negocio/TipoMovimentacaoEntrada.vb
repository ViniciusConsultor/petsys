Namespace Negocio

    <Serializable()> _
    Public Class TipoMovimentacaoEntrada
        Private _ID As Char
        Private _Descricao As String

        Public Shared AlimentacaoEstoque As TipoMovimentacaoEntrada = New TipoMovimentacaoEntrada("0"c, "Alimentação de estoque")
        Public Shared DevolucaoDeProduto As TipoMovimentacaoEntrada = New TipoMovimentacaoEntrada("1"c, "Devolução de produto")

        Private Shared ListaDeTipos As TipoMovimentacaoEntrada() = {AlimentacaoEstoque, DevolucaoDeProduto}

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

        Public Shared Function ObtenhaTipoMovimentacaoEntrada(ByVal ID As Char) As TipoMovimentacaoEntrada
            For Each Tipo As TipoMovimentacaoEntrada In ListaDeTipos
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoMovimentacaoEntrada)
            Return New List(Of TipoMovimentacaoEntrada)(ListaDeTipos)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoMovimentacaoEntrada).ID = Me.ID
        End Function

    End Class

End Namespace