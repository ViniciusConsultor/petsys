Imports T13.Interfaces.Negocio

<Serializable()> _
Public Class ServicoPrestadoLocal
    Implements IServicoPrestado

    Private _ID As Nullable(Of Long)
    Private _Nome As String
    Private _Valor As Nullable(Of Double)
    Private _CaracterizaDesconto As Boolean

    Public Property ID() As Long? Implements IServicoPrestado.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IServicoPrestado.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property CaracterizaDesconto() As Boolean Implements IServicoPrestado.CaracterizaDesconto
        Get
            Return _CaracterizaDesconto
        End Get
        Set(ByVal value As Boolean)
            _CaracterizaDesconto = value
        End Set
    End Property

    Public Property Valor() As Double? Implements IServicoPrestado.Valor
        Get
            Return _Valor
        End Get
        Set(ByVal value As Double?)
            _Valor = value
        End Set
    End Property

End Class