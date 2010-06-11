Imports Lotofacil.Interfaces.Negocio

<Serializable()> _
Public Class Dezena
    Implements IDezena

    Private _Numero As Byte

    Public Overrides Function GetHashCode() As Integer
        Return _Numero.GetHashCode
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return CType(obj, Dezena).Numero = Numero
    End Function

    Public Sub New(ByVal Numero As Byte)
        _Numero = Numero
    End Sub

    Public ReadOnly Property Numero() As Byte Implements IDezena.Numero
        Get
            Return _Numero
        End Get
    End Property

End Class