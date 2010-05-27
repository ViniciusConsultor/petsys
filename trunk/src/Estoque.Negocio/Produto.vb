Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public Class Produto
    Implements IProduto

    Private _CodigoDeBarras As String
    Public Property CodigoDeBarras() As String Implements IProduto.CodigoDeBarras
        Get
            Return _CodigoDeBarras
        End Get
        Set(ByVal value As String)
            _CodigoDeBarras = value
        End Set
    End Property

    Private _GrupoDeProduto As IGrupoDeProduto
    Public Property GrupoDeProduto() As IGrupoDeProduto Implements IProduto.GrupoDeProduto
        Get
            Return _GrupoDeProduto
        End Get
        Set(ByVal value As IGrupoDeProduto)
            _GrupoDeProduto = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IProduto.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Marca As IMarcaDeProduto
    Public Property Marca() As IMarcaDeProduto Implements IProduto.Marca
        Get
            Return _Marca
        End Get
        Set(ByVal value As IMarcaDeProduto)
            _Marca = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IProduto.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _Observacoes As String
    Public Property Observacoes() As String Implements IProduto.Observacoes
        Get
            Return _Observacoes
        End Get
        Set(ByVal value As String)
            _Observacoes = value
        End Set
    End Property

    Public ReadOnly Property QuantidadeEmEstoque() As Integer Implements IProduto.QuantidadeEmEstoque
        Get

        End Get
    End Property

    Private _Unidade As String
    Public Property Unidade() As String Implements IProduto.Unidade
        Get
            Return _Unidade
        End Get
        Set(ByVal value As String)
            _Unidade = value
        End Set
    End Property

    ''' <summary>
    ''' Calcula o valor de venda do produto
    ''' </summary>
    ''' <value></value>
    ''' <returns>Valor de venda dado o valor de custo e porcentagem de lucro</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ValorDeVenda() As Double Implements IProduto.ValorDeVenda
        Get
            Dim Valor As Double = 0

            If Not Me.ValorDeCusto Is Nothing AndAlso Me.PorcentagemDeLucro Is Nothing Then
                Return ValorDeCusto.Value
            End If

            If Not Me.ValorDeCusto Is Nothing AndAlso Not Me.PorcentagemDeLucro Is Nothing Then
                Valor = (ValorDeCusto.Value * (Me.PorcentagemDeLucro.Value / 100)) + ValorDeCusto.Value
            End If

            Return Valor
        End Get
    End Property

    Private _ValorDeCusto As Double?
    Public Property ValorDeCusto() As Double? Implements IProduto.ValorDeCusto
        Get
            Return _ValorDeCusto
        End Get
        Set(ByVal value As Double?)
            _ValorDeCusto = value
        End Set
    End Property

    Private _PorcentagemDeLucro As Double?
    Public Property PorcentagemDeLucro() As Double? Implements IProduto.PorcentagemDeLucro
        Get
            Return _PorcentagemDeLucro
        End Get
        Set(ByVal value As Double?)
            _PorcentagemDeLucro = value
        End Set
    End Property

    Private _QuantidadeMinimaEmEstoque As Double?
    Public Property QuantidadeMinimaEmEstoque() As Double? Implements IProduto.QuantidadeMinimaEmEstoque
        Get
            Return _QuantidadeMinimaEmEstoque
        End Get
        Set(ByVal value As Double?)
            _QuantidadeMinimaEmEstoque = value
        End Set
    End Property

    Private _ValorDeVendaMinimo As Double?
    Public Property ValorDeVendaMinimo() As Double? Implements IProduto.ValorDeVendaMinimo
        Get
            Return _ValorDeVendaMinimo
        End Get
        Set(ByVal value As Double?)
            _ValorDeVendaMinimo = value
        End Set
    End Property

End Class