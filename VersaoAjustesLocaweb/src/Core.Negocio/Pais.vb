Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Pais
    Implements IPais

    Private _ID As Long?
    Public Property ID As Long? Implements IPais.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String Implements IPais.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _Sigla As String
    Public Property Sigla As String Implements IPais.Sigla
        Get
            Return _Sigla
        End Get
        Set(ByVal value As String)
            _Sigla = value
        End Set
    End Property

End Class
