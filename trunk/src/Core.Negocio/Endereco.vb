Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Endereco
    Implements IEndereco

    Private _CEP As CEP
    Private _Complemento As String
    Private _Logradouro As String
    Private _Municipio As IMunicipio
    Private _Bairro As String

    Public Property CEP() As CEP Implements IEndereco.CEP
        Get
            Return _CEP
        End Get
        Set(ByVal value As CEP)
            _CEP = value
        End Set
    End Property

    Public Property Complemento() As String Implements IEndereco.Complemento
        Get
            Return _Complemento
        End Get
        Set(ByVal value As String)
            _Complemento = value
        End Set
    End Property

    Public Property Logradouro() As String Implements IEndereco.Logradouro
        Get
            Return _Logradouro
        End Get
        Set(ByVal value As String)
            _Logradouro = value
        End Set
    End Property

    Public Property Municipio() As IMunicipio Implements IEndereco.Municipio
        Get
            Return _Municipio
        End Get
        Set(ByVal value As IMunicipio)
            _Municipio = value
        End Set
    End Property

    Public Property Bairro() As String Implements IEndereco.Bairro
        Get
            Return _Bairro
        End Get
        Set(ByVal value As String)
            _Bairro = value
        End Set
    End Property

    Private _TipoDeEndereco As ITipoDeEndereco
    Public Property TipoDeEndereco As ITipoDeEndereco Implements IEndereco.TipoDeEndereco
        Get
            Return _TipoDeEndereco
        End Get
        Set(ByVal value As ITipoDeEndereco)
            _TipoDeEndereco = value
        End Set
    End Property

End Class