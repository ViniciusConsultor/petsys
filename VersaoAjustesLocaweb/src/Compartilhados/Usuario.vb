<Serializable()> _
Public Class Usuario

    Private _ID As Long
    Private _Nome As String
    Private _Autorizacoes As IList(Of String)
    Private _Sexo As Char
    Private _EmpresasVisiveis As IList(Of EmpresaVisivel)

    Public Sub New(ByVal ID As Long, ByVal Nome As String)
        _ID = ID
        _Nome = Nome
    End Sub

    Public Sub New(ByVal ID As Long, ByVal Nome As String, ByVal Autorizacoes As IList(Of String), ByVal Sexo As Char, EmpresasVisiveis As IList(Of EmpresaVisivel))
        Me.New(ID, Nome)
        'garante que a lista de autorizações não está nula
        If Autorizacoes Is Nothing Then Autorizacoes = New List(Of String)
        _Autorizacoes = Autorizacoes
        _Sexo = Sexo
        _EmpresasVisiveis = EmpresasVisiveis
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

    Public ReadOnly Property EmpresasVisiveis As IList(Of EmpresaVisivel)
        Get
            Return _EmpresasVisiveis
        End Get
    End Property

    Public Function ObtenhaEmpresaViveisPorID(ID As Long) As EmpresaVisivel
        If Not _EmpresasVisiveis Is Nothing Then

            For Each Empresa In From empresa1 In _EmpresasVisiveis Where empresa1.ID.Equals(ID)
                Return Empresa
            Next
        End If

        Return Nothing
    End Function

End Class