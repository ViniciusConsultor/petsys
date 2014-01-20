Namespace Core.Negocio

    <Serializable()> _
    Public Class TipoDePessoa

        Private _ID As Short
        Private _Descricao As String

        Public Shared Fisica As TipoDePessoa = New TipoDePessoa(1S, "Física")
        Public Shared Juridica As TipoDePessoa = New TipoDePessoa(2S, "Jurídica")
        
        Private Shared Lista As TipoDePessoa() = {Fisica, Juridica}

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

        Public Shared Function Obtenha(ByVal ID As Short) As TipoDePessoa
            For Each Tipo As TipoDePessoa In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDePessoa)
            Return New List(Of TipoDePessoa)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDePessoa).ID = Me.ID
        End Function

    End Class

End Namespace