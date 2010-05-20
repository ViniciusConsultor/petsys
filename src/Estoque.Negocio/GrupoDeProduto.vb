Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public Class GrupoDeProduto
    Implements IGrupoDeProduto

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IGrupoDeProduto.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IGrupoDeProduto.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _PorcentagemDeComissao As Double
    Public Property PorcentagemDeComissao() As Double Implements IGrupoDeProduto.PorcentagemDeComissao
        Get
            Return _PorcentagemDeComissao
        End Get
        Set(ByVal value As Double)
            _PorcentagemDeComissao = value
        End Set
    End Property

End Class