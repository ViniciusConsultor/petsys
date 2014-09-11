Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class Modulo
    Implements IModulo

    Private _Funcoes As IList(Of IFuncao)
    Private _ID As String
    Private _Nome As String

    Public Sub New()
        _Funcoes = New List(Of IFuncao)
    End Sub

    Public Sub AdicioneFuncao(ByVal Funcao As IFuncao) Implements IModulo.AdicioneFuncao
        _Funcoes.Add(Funcao)
    End Sub

    Public Function ObtenhaFuncoes() As IList(Of IFuncao) Implements IModulo.ObtenhaFuncoes
        Return _Funcoes
    End Function

    Public Property ID() As String Implements IModulo.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IModulo.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class