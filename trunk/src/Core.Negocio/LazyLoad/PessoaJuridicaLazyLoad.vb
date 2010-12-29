Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

<Serializable()> _
Public Class PessoaJuridicaLazyLoad
    Implements IPessoaJuridicaLazyLoad

    Private _Pessoa As IPessoaJuridica

    Public Sub New(ByVal ID As Long)
        Me.ID = ID
    End Sub

    Public Sub AdicioneDadoBancario(ByVal DadoBancario As IDadoBancario) Implements IPessoa.AdicioneDadoBancario
        If _Pessoa Is Nothing Then CarregueObjetoReal()
        _Pessoa.AdicioneDadoBancario(DadoBancario)
    End Sub

    Public Sub AdicioneDocumento(ByVal Documento As IDocumento) Implements IPessoa.AdicioneDocumento
        If _Pessoa Is Nothing Then CarregueObjetoReal()
        _Pessoa.AdicioneDocumento(Documento)
    End Sub

    Public Sub AdicioneTelefone(ByVal Telefone As ITelefone) Implements IPessoa.AdicioneTelefone
        If _Pessoa Is Nothing Then CarregueObjetoReal()
        _Pessoa.AdicioneTelefone(Telefone)
    End Sub

    Public Property Endereco() As IEndereco Implements IPessoa.Endereco
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Endereco
        End Get
        Set(ByVal value As IEndereco)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Endereco = value
        End Set
    End Property

    Public Property EnderecoDeEmail() As EnderecoDeEmail Implements IPessoa.EnderecoDeEmail
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.EnderecoDeEmail
        End Get
        Set(ByVal value As EnderecoDeEmail)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.EnderecoDeEmail = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IPessoa.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IPessoa.Nome
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Nome
        End Get
        Set(ByVal value As String)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Nome = value
        End Set
    End Property

    Public Function ObtenhaDadosBancarios() As IList(Of IDadoBancario) Implements IPessoa.ObtenhaDadosBancarios
        If _Pessoa Is Nothing Then CarregueObjetoReal()

        Return _Pessoa.ObtenhaDadosBancarios
    End Function

    Public Function ObtenhaDocumento(ByVal TipoDocumento As TipoDeDocumento) As IDocumento Implements IPessoa.ObtenhaDocumento
        If _Pessoa Is Nothing Then CarregueObjetoReal()

        Return _Pessoa.ObtenhaDocumento(TipoDocumento)
    End Function

    Public Property Site() As String Implements IPessoa.Site
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()

            Return _Pessoa.Site
        End Get
        Set(ByVal value As String)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Site = value
        End Set
    End Property

    Public ReadOnly Property Telefones() As IList(Of ITelefone) Implements IPessoa.Telefones
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Telefones
        End Get
    End Property

    Public ReadOnly Property Tipo() As TipoDePessoa Implements IPessoa.Tipo
        Get
            Return TipoDePessoa.Juridica
        End Get
    End Property

    Public Property NomeFantasia() As String Implements IPessoaJuridica.NomeFantasia
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()

            Return _Pessoa.NomeFantasia
        End Get
        Set(ByVal value As String)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.NomeFantasia = value
        End Set
    End Property

    Public Sub AdicioneTelefones(ByVal Telefones As IList(Of ITelefone)) Implements IPessoa.AdicioneTelefones
        If _Pessoa Is Nothing Then CarregueObjetoReal()

        _Pessoa.AdicioneTelefones(Telefones)
    End Sub

    Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
        Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
            _Pessoa = Servico.ObtenhaPessoa(Me._ID.Value)
        End Using
    End Sub

    Public Function ObtenhaTelelefones(ByVal TipoTelefone As TipoDeTelefone) As IList(Of ITelefone) Implements IPessoa.ObtenhaTelelefones
        If _Pessoa Is Nothing Then CarregueObjetoReal()
        Return _Pessoa.ObtenhaTelelefones(TipoTelefone)
    End Function

End Class