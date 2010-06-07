Public Class Dezena

    Private _Numero As Short
    Public ReadOnly Property Numero() As Short
        Get
            Return _Numero
        End Get
    End Property

    Public Overrides Function GetHashCode() As Integer
        Return _Numero.GetHashCode
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return CType(obj, Dezena).Numero = Numero
    End Function

    Public Sub New(ByVal Numero As Short)
        _Numero = Numero
    End Sub

End Class