Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public MustInherit Class MovimentacaoDeProduto
    Implements IMovimentacaoDeProduto

    Private _ProdutosMovimentados As IList(Of IProdutoMovimentado)

    Public Sub New()
        _ProdutosMovimentados = New List(Of IProdutoMovimentado)
    End Sub

    Public Sub AdicioneProdutoMovimentado(ByVal ProdutoMovimentado As IProdutoMovimentado) Implements IMovimentacaoDeProduto.AdicioneProdutoMovimentado
        _ProdutosMovimentados.Add(ProdutoMovimentado)
    End Sub

    Private _Data As Date
    Public Property Data() As Date Implements IMovimentacaoDeProduto.Data
        Get
            Return _Data
        End Get
        Set(ByVal value As Date)
            _Data = value
        End Set
    End Property

    Private _Historico As String
    Public Property Historico() As String Implements IMovimentacaoDeProduto.Historico
        Get
            Return _Historico
        End Get
        Set(ByVal value As String)
            _Historico = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IMovimentacaoDeProduto.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Function ObtenhaProdutosMovimentados() As IList(Of IProdutoMovimentado) Implements IMovimentacaoDeProduto.ObtenhaProdutosMovimentados
        Return _ProdutosMovimentados
    End Function

    Public Function ObtenhaTotalDaMovimentacao() As Double Implements IMovimentacaoDeProduto.ObtenhaTotalDaMovimentacao
        If _ProdutosMovimentados.Count = 0 Then Return 0

        Dim Total As Double

        For Each ProdutoMovimentado As IProdutoMovimentado In _ProdutosMovimentados
            Total += ProdutoMovimentado.PrecoTotal
        Next

        Return Total
    End Function

    Public MustOverride ReadOnly Property Tipo() As TipoMovimentacaoDeProduto Implements IMovimentacaoDeProduto.Tipo

    Private _NumeroDocumento As String
    Public Property NumeroDocumento() As String Implements IMovimentacaoDeProduto.NumeroDocumento
        Get
            Return _NumeroDocumento
        End Get
        Set(ByVal value As String)
            _NumeroDocumento = value
        End Set
    End Property

    Public Sub AdicioneProdutosMovimentados(ByVal ProdutosMovimentados As IList(Of IProdutoMovimentado)) Implements IMovimentacaoDeProduto.AdicioneProdutosMovimentados
        _ProdutosMovimentados = ProdutosMovimentados
    End Sub

End Class