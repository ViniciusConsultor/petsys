Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

<Serializable()> _
Public Class PessoaFisicaLazyLoad
    Implements IPessoaFisicaLazyLoad

    Private _Pessoa As IPessoaFisica

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
            Return TipoDePessoa.Fisica
        End Get
    End Property

    Public Property DataDeNascimento() As Nullable(Of Date) Implements IPessoaFisica.DataDeNascimento
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.DataDeNascimento
        End Get
        Set(ByVal value As Nullable(Of Date))
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.DataDeNascimento = value
        End Set
    End Property

    Public Property EstadoCivil() As EstadoCivil Implements IPessoaFisica.EstadoCivil
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.EstadoCivil
        End Get
        Set(ByVal value As EstadoCivil)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.EstadoCivil = value
        End Set
    End Property

    Public Property Foto() As String Implements IPessoaFisica.Foto
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Foto
        End Get
        Set(ByVal value As String)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Foto = value
        End Set
    End Property

    Public Property GrauDeInstrucao() As GrauDeInstrucao Implements IPessoaFisica.GrauDeInstrucao
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.GrauDeInstrucao
        End Get
        Set(ByVal value As GrauDeInstrucao)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.GrauDeInstrucao = value
        End Set
    End Property

    Public Property Nacionalidade() As Nacionalidade Implements IPessoaFisica.Nacionalidade
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Nacionalidade
        End Get
        Set(ByVal value As Nacionalidade)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Nacionalidade = value
        End Set
    End Property

    Public Property Naturalidade() As IMunicipio Implements IPessoaFisica.Naturalidade
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Naturalidade
        End Get
        Set(ByVal value As IMunicipio)
            _Pessoa.Naturalidade = value
        End Set
    End Property

    Public Property NomeDaMae() As String Implements IPessoaFisica.NomeDaMae
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.NomeDaMae
        End Get
        Set(ByVal value As String)
            _Pessoa.NomeDaMae = value
        End Set
    End Property

    Public Property NomeDoPai() As String Implements IPessoaFisica.NomeDoPai
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.NomeDoPai
        End Get
        Set(ByVal value As String)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.NomeDoPai = value
        End Set
    End Property

    Public Property Raca() As Raca Implements IPessoaFisica.Raca
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Raca
        End Get
        Set(ByVal value As Raca)
            _Pessoa.Raca = value
        End Set
    End Property

    Public Property Sexo() As Sexo Implements IPessoaFisica.Sexo
        Get
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            Return _Pessoa.Sexo
        End Get
        Set(ByVal value As Sexo)
            If _Pessoa Is Nothing Then CarregueObjetoReal()
            _Pessoa.Sexo = value
        End Set
    End Property

    Public Sub AdicioneTelefones(ByVal Telefones As IList(Of ITelefone)) Implements IPessoa.AdicioneTelefones
        If _Pessoa Is Nothing Then CarregueObjetoReal()

        _Pessoa.AdicioneTelefones(Telefones)
    End Sub

    Public Sub CarregueObjetoReal() Implements Compartilhados.Interfaces.Core.Negocio.LazyLoad.IObjetoLazyLoad.CarregueObjetoReal
        Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            _Pessoa = Servico.ObtenhaPessoa(Me._ID.Value)
        End Using
    End Sub

    Public Function ObtenhaTelelefones(ByVal TipoTelefone As TipoDeTelefone) As IList(Of ITelefone) Implements IPessoa.ObtenhaTelelefones
        If _Pessoa Is Nothing Then CarregueObjetoReal()
        Return _Pessoa.ObtenhaTelelefones(TipoTelefone)
    End Function

End Class
