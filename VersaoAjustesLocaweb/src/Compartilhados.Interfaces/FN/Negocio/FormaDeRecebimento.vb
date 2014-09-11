Namespace FN.Negocio

    <Serializable()> _
    Public Class FormaDeRecebimento

        Private _ID As Short
        Private _Descricao As String

        Public Shared Dinheiro As FormaDeRecebimento = New FormaDeRecebimento(1S, "Dinheiro")
        Public Shared Cheques As FormaDeRecebimento = New FormaDeRecebimento(2S, "Cheques")
        Public Shared Boleto As FormaDeRecebimento = New FormaDeRecebimento(3S, "Boleto")
        Public Shared DepositoBancario As FormaDeRecebimento = New FormaDeRecebimento(4S, "Depósito bancário")

        Private Shared Lista As FormaDeRecebimento() = {Dinheiro, Cheques, Boleto, DepositoBancario}

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

        Public Shared Function Obtenha(ByVal ID As Short) As FormaDeRecebimento
            For Each Tipo As FormaDeRecebimento In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of FormaDeRecebimento)
            Return New List(Of FormaDeRecebimento)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, FormaDeRecebimento).ID = Me.ID
        End Function

    End Class

End Namespace