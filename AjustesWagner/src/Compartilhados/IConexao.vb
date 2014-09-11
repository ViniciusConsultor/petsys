Public Interface IConexao

    Property StringDeConexao() As String
    ReadOnly Property Provider() As TipoDeProviderConexao
    Property SistemaUtilizaSQLUpperCase() As Boolean

End Interface