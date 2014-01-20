Imports Estoque.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class ProdutoLazyLoad
        Implements IProdutoLazyLoad

        Private _ProdutoReal As IProduto

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
                _ProdutoReal = Servico.ObtenhaProduto(ID.Value)
            End Using
        End Sub

        Public Property CodigoDeBarras() As String Implements IProduto.CodigoDeBarras
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.CodigoDeBarras
            End Get
            Set(ByVal value As String)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.CodigoDeBarras = value
            End Set
        End Property

        Public Property GrupoDeProduto() As IGrupoDeProduto Implements IProduto.GrupoDeProduto
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.GrupoDeProduto
            End Get
            Set(ByVal value As IGrupoDeProduto)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.GrupoDeProduto = value
            End Set
        End Property

        Private _ID As Long?
        Public Property ID() As Long? Implements IProduto.ID
            Get
                Return _ID
            End Get
            Set(ByVal value As Long?)
                _ID = value
            End Set
        End Property

        Public Property Marca() As IMarcaDeProduto Implements IProduto.Marca
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.Marca
            End Get
            Set(ByVal value As IMarcaDeProduto)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.Marca = value
            End Set
        End Property

        Public Property Nome() As String Implements IProduto.Nome
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.Nome
            End Get
            Set(ByVal value As String)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.Nome = value
            End Set
        End Property

        Public Property Observacoes() As String Implements IProduto.Observacoes
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.Observacoes
            End Get
            Set(ByVal value As String)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.Observacoes = value
            End Set
        End Property

        Public Property PorcentagemDeLucro() As Double? Implements IProduto.PorcentagemDeLucro
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.PorcentagemDeLucro
            End Get
            Set(ByVal value As Double?)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.PorcentagemDeLucro = value
            End Set
        End Property

        Public ReadOnly Property QuantidadeEmEstoque() As Double Implements IProduto.QuantidadeEmEstoque
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.QuantidadeEmEstoque
            End Get
        End Property

        Public Property QuantidadeMinimaEmEstoque() As Double? Implements IProduto.QuantidadeMinimaEmEstoque
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.QuantidadeMinimaEmEstoque
            End Get
            Set(ByVal value As Double?)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.QuantidadeMinimaEmEstoque = value
            End Set
        End Property

        Public Property UnidadeDeMedida() As UnidadeDeMedida Implements IProduto.UnidadeDeMedida
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.UnidadeDeMedida
            End Get
            Set(ByVal value As UnidadeDeMedida)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.UnidadeDeMedida = value
            End Set
        End Property

        Public Property ValorDeCusto() As Double? Implements IProduto.ValorDeCusto
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.ValorDeCusto
            End Get
            Set(ByVal value As Double?)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.ValorDeCusto = value
            End Set
        End Property

        Public ReadOnly Property ValorDeVenda() As Double Implements IProduto.ValorDeVenda
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.ValorDeVenda
            End Get
        End Property

        Public Property ValorDeVendaMinimo() As Double? Implements IProduto.ValorDeVendaMinimo
            Get
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                Return _ProdutoReal.ValorDeVendaMinimo
            End Get
            Set(ByVal value As Double?)
                If _ProdutoReal Is Nothing Then CarregueObjetoReal()
                _ProdutoReal.ValorDeVendaMinimo = value
            End Set
        End Property

    End Class

End Namespace