Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class CedenteLazyLoad
        Implements ICedenteLazyLoad
        
        Private _ID As Long
        Private _CedenteReal As ICedente

        Public Sub New(ByVal ID As Long)
            _ID = ID
        End Sub

        Public ReadOnly Property Pessoa As IPessoa Implements IPapelPessoa.Pessoa
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.Pessoa
            End Get
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using servico As IServicoDeCedente = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeCedente)()
                _CedenteReal = servico.Obtenha(_ID)
            End Using
        End Sub

        Public Property ImagemDeCabecalhoDoReciboDoSacado() As String Implements ICedente.ImagemDeCabecalhoDoReciboDoSacado
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.ImagemDeCabecalhoDoReciboDoSacado
            End Get
            Set(ByVal value As String)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.ImagemDeCabecalhoDoReciboDoSacado = value
            End Set
        End Property

        Public Property TipoDeCarteira() As TipoDeCarteira Implements ICedente.TipoDeCarteira
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.TipoDeCarteira
            End Get
            Set(ByVal value As TipoDeCarteira)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.TipoDeCarteira = value
            End Set
        End Property

        Public Property InicioNossoNumero() As Long Implements ICedente.InicioNossoNumero
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.InicioNossoNumero
            End Get
            Set(ByVal value As Long)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.InicioNossoNumero = value
            End Set
        End Property

        Public Property NumeroDaAgencia() As String Implements ICedente.NumeroDaAgencia
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.NumeroDaAgencia
            End Get
            Set (ByVal value As String)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.NumeroDaAgencia = value
            End Set
        End Property

        Public Property NumeroDaConta() As String Implements ICedente.NumeroDaConta
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.NumeroDaConta
            End Get
            Set(ByVal value As String)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.NumeroDaConta = value
            End Set
        End Property

        Public Property TipoDaConta() As Integer Implements ICedente.TipoDaConta
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.TipoDaConta
            End Get
            Set (ByVal value As Integer)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.TipoDaConta = value
            End Set
        End Property

        Public Property Padrao() As Boolean Implements ICedente.Padrao
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.Padrao
            End Get
            Set (ByVal value As Boolean)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.Padrao = value
            End Set
        End Property

        Public Property NumeroDoBanco() As String Implements ICedente.NumeroDoBanco
            Get
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                Return _CedenteReal.NumeroDoBanco
            End Get
            Set(ByVal value As String)
                If _CedenteReal Is Nothing Then CarregueObjetoReal()
                _CedenteReal.NumeroDoBanco = value
            End Set
        End Property
    End Class

End Namespace
