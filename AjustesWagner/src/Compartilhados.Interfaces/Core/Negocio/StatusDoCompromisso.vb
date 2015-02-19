﻿Namespace Core.Negocio

    <Serializable()> _
    Public Class StatusDoCompromisso

        Private _ID As Char
        Private _Descricao As String

        Public Shared Pendente As StatusDoCompromisso = New StatusDoCompromisso("P"c, "Pendente")
        Public Shared Cancelado As StatusDoCompromisso = New StatusDoCompromisso("A"c, "Cancelado")
        Public Shared Concluido As StatusDoCompromisso = New StatusDoCompromisso("C"c, "Concluído")

        Private Shared Lista As StatusDoCompromisso() = {Pendente, Concluido, Cancelado}

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

        Public Shared Function Obtenha(ByVal ID As Char) As StatusDoCompromisso
            For Each Item As StatusDoCompromisso In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of StatusDoCompromisso)
            Return New List(Of StatusDoCompromisso)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, StatusDoCompromisso).ID = Me.ID
        End Function

    End Class

End Namespace