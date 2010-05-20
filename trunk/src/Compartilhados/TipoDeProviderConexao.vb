<Serializable()> _
Public Class TipoDeProviderConexao

    Private _ID As Char
    Private _Descricao As String

    Public Shared OLEBD As TipoDeProviderConexao = New TipoDeProviderConexao("0"c, "OLEBD")
    Public Shared ODBC As TipoDeProviderConexao = New TipoDeProviderConexao("1"c, "ODBC")
    Public Shared SQLSERVER As TipoDeProviderConexao = New TipoDeProviderConexao("2"c, "MS SQL SERVER")
    Public Shared ORACLE As TipoDeProviderConexao = New TipoDeProviderConexao("3"c, "ORACLE")
    Public Shared SQLITE As TipoDeProviderConexao = New TipoDeProviderConexao("4"c, "SQLITE")

    Private Shared Lista As TipoDeProviderConexao() = {OLEBD, ODBC, SQLSERVER, ORACLE, SQLITE}

    Private Sub New(ByVal ID As Char, _
                    ByVal Descricao As String)
        _ID = ID
        _Descricao = Descricao
    End Sub

    Public ReadOnly Property ID() As Char
        Get
            Return _ID
        End Get
    End Property

    Public ReadOnly Property Descricao() As String
        Get
            Return _Descricao
        End Get
    End Property

    Public Shared Function Obtenha(ByVal ID As Char) As TipoDeProviderConexao
        For Each Tipo As TipoDeProviderConexao In Lista
            If Tipo.ID = ID Then
                Return Tipo
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function ObtenhaTodos() As IList(Of TipoDeProviderConexao)
        Return New List(Of TipoDeProviderConexao)(Lista)
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return CType(obj, TipoDeProviderConexao).ID = Me.ID
    End Function

End Class