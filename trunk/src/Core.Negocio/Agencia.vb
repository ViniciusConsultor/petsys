Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Agencia
    Inherits PapelPessoa
    Implements IAgencia

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Private _Banco As IBanco
    Public Property Banco() As IBanco Implements IAgencia.Banco
        Get
            Return _Banco
        End Get
        Set(ByVal value As IBanco)
            _Banco = value
        End Set
    End Property

    Private _Numero As String
    Public Property Numero() As String Implements IAgencia.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As String)
            _Numero = value
        End Set
    End Property

End Class