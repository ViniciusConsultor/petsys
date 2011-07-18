Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Banco
    Inherits PapelPessoa
    Implements IBanco

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Public ReadOnly Property Agencias() As IList(Of IAgencia) Implements IBanco.Agencias
        Get
            Return Nothing
        End Get
    End Property

    Private _Numero As Integer
    Public Property Numero() As Integer Implements IBanco.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As Integer)
            _Numero = value
        End Set
    End Property

End Class