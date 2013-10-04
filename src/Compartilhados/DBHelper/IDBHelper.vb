Namespace DBHelper

    Public Interface IDBHelper

        'Inicializa a transação no banco de dados
        Sub BeginTransaction(ByVal eTipoTransacao As IsolationLevel)
        Sub BeginTransaction()
        Sub Dispose()

        'Finaliza uma transação bem sucedida no banco de dados
        Sub CommitTransaction()

        'Defaz todas as operações efetuadas no banco de dados devido alguma
        ' um operação mal sucedida no banco de dados
        Sub RollbackTransaction()

        Function existeTransacaoAberta() As Boolean

        ' Executa um comando sql que não retorna dados (INSERT, UPDATE, DELETE)
        Function ExecuteNonQuery(ByVal sSQL As String) As Integer
        Function ExecuteNonQuery(ByVal sSQL As String, ByVal bSaveUpperCase As Boolean) As Integer

        ' Executa uma query sql e retorna um DataReader
        Function obtenhaReader(ByVal sQuery As String) As IDataReader
        Function obtenhaReader(ByVal Query As String, ByVal QuantidadeDeRegistros As Integer) As IDataReader
        Function obtenhaReader(ByVal Query As String, ByVal QuantidadeDeRegistros As Integer, OffSet As Integer) As IDataReader

        Function SuporteALimite() As Boolean
        Function SuporteAOffSet() As Boolean
        Function ObtenhaQueryComLimite(ByVal QueryOriginal As String, ByVal QuantidadeDeRegistros As Integer) As String
        Function ObtenhaQueryComLimiteEOffset(QueryOriginal As String, QuantidadeDeRegistros As Integer, OffSet As Integer) As String
        Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As Exception) As String

    End Interface

End Namespace