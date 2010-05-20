<Serializable()> _
Public Class Usuario

    Private _ID As Long
    Private _Nome As String
    Private _Autorizacoes As IList(Of String)
    Private _Sexo As Char

    Public Sub New(ByVal ID As Long, ByVal Nome As String, ByVal Autorizacoes As IList(Of String), ByVal Sexo As Char)
        _ID = ID
        _Nome = Nome
        'garante que a lista de autorizações não está nula
        If Autorizacoes Is Nothing Then Autorizacoes = New List(Of String)
        _Autorizacoes = Autorizacoes
        _Sexo = Sexo
    End Sub

    Public ReadOnly Property ID() As Long
        Get
            Return _ID
        End Get
    End Property

    Public ReadOnly Property Nome() As String
        Get
            Return _Nome
        End Get
    End Property

    Public Function ContemItem(ByVal Diretiva As String) As Boolean
        Return _Autorizacoes.Contains(Diretiva)
    End Function

    Public ReadOnly Property Sexo() As Char
        Get
            Return _Sexo
        End Get
    End Property

    Public ReadOnly Property Autorizacoes() As IList(Of String)
        Get
            Return _Autorizacoes
        End Get
    End Property

End Class