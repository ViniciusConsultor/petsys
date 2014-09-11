Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento

<Serializable()> _
Public Class PessoaJuridica
    Inherits Pessoa
    Implements IPessoaJuridica
    
    Private _NomeFantasia As String

    Public Overrides ReadOnly Property Tipo() As TipoDePessoa
        Get
            Return TipoDePessoa.Juridica
        End Get
    End Property

    Public Property NomeFantasia() As String Implements IPessoaJuridica.NomeFantasia
        Get
            Return _NomeFantasia
        End Get
        Set(ByVal value As String)
            _NomeFantasia = value
        End Set
    End Property

    Private _Logomarca As String
    Public Property Logomarca As String Implements IPessoaJuridica.Logomarca
        Get
            Return _Logomarca
        End Get
        Set(value As String)
            _Logomarca = value
        End Set
    End Property

End Class