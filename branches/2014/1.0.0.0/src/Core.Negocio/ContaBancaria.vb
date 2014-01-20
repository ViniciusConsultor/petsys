Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ContaBancaria
    Implements IContaBancaria

    Private _Numero As String
    Public Property Numero() As String Implements IContaBancaria.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As String)
            _Numero = value
        End Set
    End Property

    Private _Tipo As Integer?
    Public Property Tipo() As Integer? Implements IContaBancaria.Tipo
        Get
            Return _Tipo
        End Get
        Set(ByVal value As Integer?)
            _Tipo = value
        End Set
    End Property

End Class
