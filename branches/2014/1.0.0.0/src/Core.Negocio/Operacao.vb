Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class Operacao
    Implements IOperacao

    Private _ID As String
    Private _Nome As String

    Public Property ID() As String Implements IOperacao.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IOperacao.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class