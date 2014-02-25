Namespace FN.Negocio

    <Serializable()> _
    Public Class TipoLacamentoFinanceiro

        Private _ID As Short
        Private _Descricao As String

        Public Shared Recebimento As TipoLacamentoFinanceiro = New TipoLacamentoFinanceiro(1S, "Recebimento")
        Public Shared Pagamento As TipoLacamentoFinanceiro = New TipoLacamentoFinanceiro(2S, "Pagamento")

        Private Shared Lista As TipoLacamentoFinanceiro() = {Recebimento, Pagamento}

        Private Sub New(ByVal ID As Short, _
                        ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Public ReadOnly Property ID() As Short
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Short) As TipoLacamentoFinanceiro
            For Each Tipo As TipoLacamentoFinanceiro In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoLacamentoFinanceiro)
            Return New List(Of TipoLacamentoFinanceiro)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoLacamentoFinanceiro).ID = Me.ID
        End Function

    End Class

End Namespace

