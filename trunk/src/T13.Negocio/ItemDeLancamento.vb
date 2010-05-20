Imports T13.Interfaces.Negocio

<Serializable()> _
Public Class ItemDeLancamento
    Implements IItemDeLancamento

    Private _Quantidade As Nullable(Of Short)
    Private _ServicoPrestado As IServicoPrestado
    Private _Valor As Double
    Private _Observacao As String
    Private _Unidade As String

    Public Property Servico() As IServicoPrestado Implements IItemDeLancamento.Servico
        Get
            Return _ServicoPrestado
        End Get
        Set(ByVal value As IServicoPrestado)
            _ServicoPrestado = value
        End Set
    End Property

    Public Property Unidade() As String Implements IItemDeLancamento.Unidade
        Get
            Return _Unidade
        End Get
        Set(ByVal value As String)
            _Unidade = value
        End Set
    End Property

    Public Property Valor() As Double Implements IItemDeLancamento.Valor
        Get
            Return _Valor
        End Get
        Set(ByVal value As Double)
            _Valor = value
        End Set
    End Property

    Public Property Quantidade() As Short? Implements IItemDeLancamento.Quantidade
        Get
            Return _Quantidade
        End Get
        Set(ByVal value As Short?)
            _Quantidade = value
        End Set
    End Property

    Public ReadOnly Property Total() As Double Implements IItemDeLancamento.Total
        Get
            'Verifica se é necessário multiplicar o quantidade x valor
            If Not Quantidade Is Nothing AndAlso Not Quantidade.Value = 0 Then
                Return Valor * Quantidade.Value
            End If

            Return Valor
        End Get
    End Property

    Public Property Observacao() As String Implements IItemDeLancamento.Observacao
        Get
            Return _Observacao
        End Get
        Set(ByVal value As String)
            _Observacao = value
        End Set
    End Property

End Class