Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class PessoaFisica
    Inherits Pessoa
    Implements IPessoaFisica

    Private _DataDeNascimento As Nullable(Of Date)
    Private _EstadoCivil As EstadoCivil
    Private _GrauDeInstrucao As GrauDeInstrucao
    Private _Nacionalidade As Nacionalidade
    Private _Raca As Raca
    Private _Sexo As Sexo
    Private _Naturalidade As IMunicipio
    Private _NomeDaMae As String
    Private _NomeDoPai As String
    Private _Foto As String

    Public Property EstadoCivil() As EstadoCivil Implements IPessoaFisica.EstadoCivil
        Get
            Return _EstadoCivil
        End Get
        Set(ByVal value As EstadoCivil)
            _EstadoCivil = value
        End Set
    End Property

    Public Property GrauDeInstrucao() As GrauDeInstrucao Implements IPessoaFisica.GrauDeInstrucao
        Get
            Return _GrauDeInstrucao
        End Get
        Set(ByVal value As GrauDeInstrucao)
            _GrauDeInstrucao = value
        End Set
    End Property

    Public Property Nacionalidade() As Nacionalidade Implements IPessoaFisica.Nacionalidade
        Get
            Return _Nacionalidade
        End Get
        Set(ByVal value As Nacionalidade)
            _Nacionalidade = value
        End Set
    End Property

    Public Property Raca() As Raca Implements IPessoaFisica.Raca
        Get
            Return _Raca
        End Get
        Set(ByVal value As Raca)
            _Raca = value
        End Set
    End Property

    Public Property Sexo() As Sexo Implements IPessoaFisica.Sexo
        Get
            Return _Sexo
        End Get
        Set(ByVal value As Sexo)
            _Sexo = value
        End Set
    End Property

    Public Overrides ReadOnly Property Tipo() As TipoDePessoa
        Get
            Return TipoDePessoa.Fisica
        End Get
    End Property

    Public Property DataDeNascimento() As Nullable(Of Date) Implements IPessoaFisica.DataDeNascimento
        Get
            Return _DataDeNascimento
        End Get
        Set(ByVal value As Nullable(Of Date))
            _DataDeNascimento = value
        End Set
    End Property

    Public Property Naturalidade() As IMunicipio Implements IPessoaFisica.Naturalidade
        Get
            Return _Naturalidade
        End Get
        Set(ByVal value As IMunicipio)
            _Naturalidade = value
        End Set
    End Property

    Public Property NomeDaMae() As String Implements IPessoaFisica.NomeDaMae
        Get
            Return _NomeDaMae
        End Get
        Set(ByVal value As String)
            _NomeDaMae = value
        End Set
    End Property

    Public Property NomeDoPai() As String Implements IPessoaFisica.NomeDoPai
        Get
            Return _NomeDoPai
        End Get
        Set(ByVal value As String)
            _NomeDoPai = value
        End Set
    End Property

    Public Property Foto() As String Implements IPessoaFisica.Foto
        Get
            Return _Foto
        End Get
        Set(ByVal value As String)
            _Foto = value
        End Set
    End Property

End Class