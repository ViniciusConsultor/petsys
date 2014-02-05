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

    End Class

End Namespace
