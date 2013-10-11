Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    Public Class ClienteLazyLoad
        Implements IClienteLazyLoad

        Private _ID As Long
        Private _ClienteReal As ICliente

        Public Sub New(ByVal ID As Long)
            _ID = ID
        End Sub

        Public Property DataDoCadastro As Date? Implements ICliente.DataDoCadastro
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.DataDoCadastro
            End Get
            Set(value As Date?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.DataDoCadastro = value
            End Set
        End Property

        Public Property DataDoRegistro As Date? Implements ICliente.DataDoRegistro
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.DataDoRegistro
            End Get
            Set(value As Date?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.DataDoRegistro = value
            End Set
        End Property

        Public Property FaixaSalarial As Double? Implements ICliente.FaixaSalarial
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.FaixaSalarial
            End Get
            Set(value As Double?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.FaixaSalarial = value
            End Set
        End Property

        Public Property GrupoDeAtividade As IGrupoDeAtividade Implements ICliente.GrupoDeAtividade
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.GrupoDeAtividade
            End Get
            Set(value As IGrupoDeAtividade)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.GrupoDeAtividade = value
            End Set
        End Property

        Public Property InformacoesAdicionais As String Implements ICliente.InformacoesAdicionais
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.InformacoesAdicionais
            End Get
            Set(value As String)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.InformacoesAdicionais = value
            End Set
        End Property

        Public Property NumeroDoRegistro As String Implements ICliente.NumeroDoRegistro
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.NumeroDoRegistro
            End Get
            Set(value As String)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.NumeroDoRegistro = value
            End Set
        End Property

        Public Property PorcentagemDeDescontoAutomatico As Double? Implements ICliente.PorcentagemDeDescontoAutomatico
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.PorcentagemDeDescontoAutomatico
            End Get
            Set(value As Double?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.PorcentagemDeDescontoAutomatico = value
            End Set
        End Property

        Public Property SaldoParaCompras As Double? Implements ICliente.SaldoParaCompras
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.SaldoParaCompras
            End Get
            Set(value As Double?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.SaldoParaCompras = value
            End Set
        End Property

        Public Property ValorMaximoParaCompras As Double? Implements ICliente.ValorMaximoParaCompras
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.ValorMaximoParaCompras
            End Get
            Set(value As Double?)
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                _ClienteReal.ValorMaximoParaCompras = value
            End Set
        End Property

        Public ReadOnly Property Pessoa As IPessoa Implements IPapelPessoa.Pessoa
            Get
                If _ClienteReal Is Nothing Then CarregueObjetoReal()
                Return _ClienteReal.Pessoa
            End Get
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeCliente)()
                _ClienteReal = Servico.Obtenha(_ID)
            End Using
        End Sub

    End Class

End Namespace