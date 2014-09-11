Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos

Namespace LazyLoad

    <Serializable()> _
    Public Class FornecedorLazyLoad
        Implements IFornecedorLazyLoad

        Private _ID As Long
        Private _FornecedorReal As IFornecedor

        Public Sub New(ByVal ID As Long)
            _ID = ID
        End Sub

        Public Sub AdicionaContato(ByVal Contato As IPessoaFisica) Implements IFornecedor.AdicionaContato
            If _FornecedorReal Is Nothing Then CarregueObjetoReal()
            _FornecedorReal.AdicionaContato(Contato)
        End Sub

        Public ReadOnly Property Contatos() As IList(Of IPessoaFisica) Implements IFornecedor.Contatos
            Get
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                Return _FornecedorReal.Contatos
            End Get
        End Property

        Public Property DataDoCadastro() As Date Implements IFornecedor.DataDoCadastro
            Get
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                Return _FornecedorReal.DataDoCadastro
            End Get
            Set(ByVal value As Date)
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                _FornecedorReal.DataDoCadastro = value
            End Set
        End Property

        Public Property InformacoesAdicionais() As String Implements IFornecedor.InformacoesAdicionais
            Get
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                Return _FornecedorReal.InformacoesAdicionais
            End Get
            Set(ByVal value As String)
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                _FornecedorReal.InformacoesAdicionais = value
            End Set
        End Property

        Public ReadOnly Property Pessoa() As IPessoa Implements IPapelPessoa.Pessoa
            Get
                If _FornecedorReal Is Nothing Then CarregueObjetoReal()
                Return _FornecedorReal.Pessoa
            End Get
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
                _FornecedorReal = Servico.Obtenha(_ID)
            End Using
        End Sub

    End Class

End Namespace