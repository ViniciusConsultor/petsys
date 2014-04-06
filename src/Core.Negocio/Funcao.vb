Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class Funcao
    Implements IFuncao

    Private _Operacoes As IList(Of IOperacao)
    Private _ID As String
    Private _Nome As String

    Public Sub New()
        _Operacoes = New List(Of IOperacao)
    End Sub

    Public Sub AdicioneOperacao(ByVal Operacao As IOperacao) Implements IFuncao.AdicioneOperacao
        _Operacoes.Add(Operacao)
    End Sub

    Public Function ObtenhaOperacoes() As IList(Of IOperacao) Implements IFuncao.ObtenhaOperacoes
        Return _Operacoes
    End Function

    Public Property ID() As String Implements IFuncao.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IFuncao.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class