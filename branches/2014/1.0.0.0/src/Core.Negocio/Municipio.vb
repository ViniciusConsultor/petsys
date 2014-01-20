Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Municipio
    Implements IMunicipio

    Private _CEP As CEP
    Private _ID As Nullable(Of Long)
    Private _Nome As String
    Private _UF As UF

    Public Property CEP() As CEP Implements IMunicipio.CEP
        Get
            Return _CEP
        End Get
        Set(ByVal value As CEP)
            _CEP = value
        End Set
    End Property

    Public Property Nome() As String Implements IMunicipio.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property UF() As UF Implements IMunicipio.UF
        Get
            Return _UF
        End Get
        Set(ByVal value As UF)
            _UF = value
        End Set
    End Property

    Public Property ID() As Long? Implements IMunicipio.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Concat(Nome, " - ", UF.Sigla)
    End Function

End Class