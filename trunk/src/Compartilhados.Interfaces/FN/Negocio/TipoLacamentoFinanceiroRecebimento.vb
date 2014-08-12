Namespace FN.Negocio

    <Serializable()> _
    Public Class TipoLacamentoFinanceiroRecebimento

        Private _ID As Short
        Private _Descricao As String

        Public Shared BoletoAvulso As TipoLacamentoFinanceiroRecebimento = New TipoLacamentoFinanceiroRecebimento(1S, "Boleto avulso")
        Public Shared RecebimentoDeManutencao As TipoLacamentoFinanceiroRecebimento = New TipoLacamentoFinanceiroRecebimento(2S, "Recebimento de manutenção")
        Public Shared Outros As TipoLacamentoFinanceiroRecebimento = New TipoLacamentoFinanceiroRecebimento(3S, "Outros")

        Private Shared Lista As TipoLacamentoFinanceiroRecebimento() = {BoletoAvulso, RecebimentoDeManutencao, Outros}

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

        Public Shared Function Obtenha(ByVal ID As Short) As TipoLacamentoFinanceiroRecebimento
            For Each Tipo As TipoLacamentoFinanceiroRecebimento In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoLacamentoFinanceiroRecebimento)
            Return New List(Of TipoLacamentoFinanceiroRecebimento)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoLacamentoFinanceiroRecebimento).ID = Me.ID
        End Function
    End Class

End Namespace
