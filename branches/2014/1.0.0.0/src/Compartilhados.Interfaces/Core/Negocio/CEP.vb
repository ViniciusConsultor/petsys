Namespace Core.Negocio

    <Serializable()> _
    Public Class CEP

        Private _Numero As Long

        Public Sub New(ByVal Numero As Long)
            _Numero = Numero
        End Sub

        Public ReadOnly Property Numero() As Nullable(Of Long)
            Get
                Return _Numero
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Format(Me._Numero, "00\.###\-###")
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return _Numero = DirectCast(obj, CEP)._Numero
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _Numero.GetHashCode
        End Function

    End Class

End Namespace