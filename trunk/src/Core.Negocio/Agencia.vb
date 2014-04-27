Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Agencia
    Implements IAgencia
    
    Private _Numero As String
    Public Property Numero() As String Implements IAgencia.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As String)
            _Numero = value
        End Set
    End Property

    Private _Banco As Banco
    Public Property Banco As Banco Implements IAgencia.Banco
        Get
            Return _Banco
        End Get
        Set(ByVal value As Banco)
            _Banco = value
        End Set
    End Property

    Private _ID As Long?
    Public Property ID As Long? Implements IAgencia.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String Implements IAgencia.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class