﻿Namespace FN.Negocio

    <Serializable()> _
    Public Class Situacao

        Private _ID As Short
        Private _Descricao As String

        Public Shared CobrancaEmAberto As Situacao = New Situacao(1S, "Cobrança em aberto")
        Public Shared Paga As Situacao = New Situacao(2S, "Paga")
        Public Shared Cancelada As Situacao = New Situacao(3S, "Cancelada")
        Public Shared AguardandoCobranca As Situacao = New Situacao(4S, "Aguardando cobrança")
        Public Shared CobrancaGerada As Situacao = New Situacao(5S, "Cobrança gerada")

        Private Shared Lista As Situacao() = {CobrancaEmAberto, Paga, Cancelada, AguardandoCobranca, CobrancaGerada}

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

        Public Shared Function Obtenha(ByVal ID As Short) As Situacao
            For Each Tipo As Situacao In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of Situacao)
            Return New List(Of Situacao)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, Situacao).ID = Me.ID
        End Function

    End Class

End Namespace
