Public Interface ICredencial

    ReadOnly Property Usuario() As Usuario
    ReadOnly Property Conexao() As IConexao
    ReadOnly Property EmpresaLogada As EmpresaVisivel

End Interface