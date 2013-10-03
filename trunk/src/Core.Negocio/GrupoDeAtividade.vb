Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class GrupoDeAtividade
    Implements IGrupoDeAtividade

    Private _ID As Nullable(Of Long)
    Public Property ID As Long? Implements IGrupoDeAtividade.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String Implements IGrupoDeAtividade.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class