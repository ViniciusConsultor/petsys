Imports T13.Interfaces.Mapeadores
Imports T13.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores

Public Class MapeadorDeLancamentoDeServicos
    Implements IMapeadorDeLancamentoDeServicos

    Public Sub Inserir(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IMapeadorDeLancamentoDeServicos.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Lancamento.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO T13_LANCAMENTODESERVICOS (ID, IDCLIENTE, DATA, ALIQUOTA, NATUREZADAOPERACAO, OBSERVACOES, NUMERO ) ")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Lancamento.ID.Value, ", "))
        Sql.Append(String.Concat(Lancamento.Cliente.Pessoa.ID.Value, ", "))
        Sql.Append(String.Concat(Lancamento.DataDeLancamento.ToString("yyyyMMdd"), ", "))

        If Not String.IsNullOrEmpty(Lancamento.Aliquota) Then
            Sql.Append(String.Concat("'", Lancamento.Aliquota, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lancamento.NaturezaDaOperacao) Then
            Sql.Append(String.Concat("'", Lancamento.NaturezaDaOperacao, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lancamento.Observacoes) Then
            Sql.Append(String.Concat("'", Lancamento.Observacoes, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat(Lancamento.Numero.Value, ")"))


        DBHelper.ExecuteNonQuery(Sql.ToString)

        For Each Item As IItemDeLancamento In Lancamento.ObtenhaItensDeLancamento
            Sql = New StringBuilder

            Sql.Append("INSERT INTO T13_ITEMDELANCAMENTO (IDLANCAMENTO, IDSERVICOPRESTADO, VALOR, UNIDADE, QUANTIDADE, OBSERVACAO) ")
            Sql.Append("VALUES (")
            Sql.Append(String.Concat(Lancamento.ID.Value, ", "))
            Sql.Append(String.Concat(Item.Servico.ID.Value, ", "))
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Item.Valor), ", "))

            If Not String.IsNullOrEmpty(Item.Unidade) Then
                Sql.Append(String.Concat("'", Item.Unidade, "', "))
            Else
                Sql.Append("NULL, ")
            End If

            If Not Item.Quantidade Is Nothing Then
                Sql.Append(String.Concat(Item.Quantidade.Value, ", "))
            Else
                Sql.Append("NULL, ")
            End If

            If Not String.IsNullOrEmpty(Item.Observacao) Then
                Sql.Append(String.Concat("'", Item.Observacao, "')"))
            Else
                Sql.Append("NULL)")
            End If

            DBHelper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

    Public Sub Modificar(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IMapeadorDeLancamentoDeServicos.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE T13_LANCAMENTODESERVICOS SET ")
        Sql.Append(String.Concat("DATA = ", Lancamento.DataDeLancamento.ToString("yyyyMMdd"), ", "))

        If Not String.IsNullOrEmpty(Lancamento.Aliquota) Then
            Sql.Append(String.Concat("ALIQUOTA = '", Lancamento.Aliquota, "', "))
        Else
            Sql.Append("ALIQUOTA = NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lancamento.NaturezaDaOperacao) Then
            Sql.Append(String.Concat("NATUREZADAOPERACAO = '", Lancamento.NaturezaDaOperacao, "', "))
        Else
            Sql.Append("NATUREZADAOPERACAO = NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lancamento.Observacoes) Then
            Sql.Append(String.Concat("OBSERVACOES = '", Lancamento.Observacoes, "', "))
        Else
            Sql.Append("OBSERVACOES = NULL, ")
        End If

        Sql.Append(String.Concat("NUMERO = ", Lancamento.Numero.Value))

        DBHelper.ExecuteNonQuery(Sql.ToString)

        Sql = New StringBuilder

        Sql.Append("DELETE FROM T13_ITEMDELANCAMENTO ")
        Sql.Append(String.Concat("WHERE IDLANCAMENTO = ", Lancamento.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)

        For Each Item As IItemDeLancamento In Lancamento.ObtenhaItensDeLancamento
            Sql = New StringBuilder

            Sql.Append("INSERT INTO T13_ITEMDELANCAMENTO (IDLANCAMENTO, IDSERVICOPRESTADO, VALOR, UNIDADE, QUANTIDADE, OBSERVACAO) ")
            Sql.Append("VALUES (")
            Sql.Append(String.Concat(Lancamento.ID.Value, ", "))
            Sql.Append(String.Concat(Item.Servico.ID.Value, ", "))
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Item.Valor), ", "))

            If Not String.IsNullOrEmpty(Item.Unidade) Then
                Sql.Append(String.Concat("'", Item.Unidade, "', "))
            Else
                Sql.Append("NULL, ")
            End If

            If Not Item.Quantidade Is Nothing Then
                Sql.Append(String.Concat(Item.Quantidade.Value, ", "))
            Else
                Sql.Append("NULL, ")
            End If

            If Not String.IsNullOrEmpty(Item.Observacao) Then
                Sql.Append(String.Concat("'", Item.Observacao, "')"))
            Else
                Sql.Append("NULL)")
            End If

            DBHelper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeLancamentoDeServicos.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM T13_ITEMDELANCAMENTO")
        Sql.Append(String.Concat(" WHERE IDLANCAMENTO = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)

        Sql = New StringBuilder
        Sql.Append("DELETE FROM T13_LANCAMENTODESERVICOS")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaLancamentosTardio(ByVal IDCliente As Long) As IList(Of ILacamentoDeServicosPrestados) Implements IMapeadorDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Lancamentos As IList(Of ILacamentoDeServicosPrestados)
        Dim Lancamento As ILacamentoDeServicosPrestados

        Sql.Append("SELECT ID, DATA, IDCLIENTE, NUMERO ")
        Sql.Append("FROM T13_LANCAMENTODESERVICOS ")
        Sql.Append(String.Concat("WHERE IDCLIENTE = ", IDCliente.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper
        Lancamentos = New List(Of ILacamentoDeServicosPrestados)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Lancamento = FabricaGenerica.GetInstancia.CrieObjeto(Of ILacamentoDeServicosPrestados)()
                Lancamento.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Lancamento.DataDeLancamento = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value
                Lancamento.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")
                Lancamentos.Add(Lancamento)
            End While
        End Using

        Return Lancamentos
    End Function

    Public Function ObtenhaLancamento(ByVal ID As Long) As ILacamentoDeServicosPrestados Implements IMapeadorDeLancamentoDeServicos.ObtenhaLancamento
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Lancamento As ILacamentoDeServicosPrestados = Nothing

        Sql.Append("SELECT ID, IDCLIENTE, DATA, ALIQUOTA, NATUREZADAOPERACAO, OBSERVACOES, NUMERO ")
        Sql.Append("FROM T13_LANCAMENTODESERVICOS ")
        Sql.Append(String.Concat("WHERE ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Lancamento = FabricaGenerica.GetInstancia.CrieObjeto(Of ILacamentoDeServicosPrestados)()
                Lancamento.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Lancamento.DataDeLancamento = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value
                Lancamento.Aliquota = UtilidadesDePersistencia.GetValorString(Leitor, "ALIQUOTA")
                Lancamento.NaturezaDaOperacao = UtilidadesDePersistencia.GetValorString(Leitor, "NATUREZADAOPERACAO")
                Lancamento.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")
                Lancamento.Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente).Obtenha(UtilidadesDePersistencia.GetValorLong(Leitor, "IDCLIENTE"))
                Lancamento.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")
            End If
        End Using

        If Not Lancamento Is Nothing Then
            Dim ItemDeLancamento As IItemDeLancamento

            Sql = New StringBuilder

            Sql.Append("SELECT IDLANCAMENTO, IDSERVICOPRESTADO, VALOR, UNIDADE, QUANTIDADE, OBSERVACAO ")
            Sql.Append("FROM T13_ITEMDELANCAMENTO ")
            Sql.Append(String.Concat("WHERE IDLANCAMENTO =", Lancamento.ID.Value))

            Dim MapeadorDeServicos As IMapeadorDeServicosPrestados

            MapeadorDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

            Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
                While Leitor.Read
                    ItemDeLancamento = FabricaGenerica.GetInstancia.CrieObjeto(Of IItemDeLancamento)()

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "QUANTIDADE") Then
                        ItemDeLancamento.Quantidade = UtilidadesDePersistencia.getValorShort(Leitor, "QUANTIDADE")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "UNIDADE") Then
                        ItemDeLancamento.Unidade = UtilidadesDePersistencia.GetValorString(Leitor, "UNIDADE")
                    End If

                    ItemDeLancamento.Valor = UtilidadesDePersistencia.getValorDouble(Leitor, "VALOR")
                    ItemDeLancamento.Servico = MapeadorDeServicos.ObtenhaServico(UtilidadesDePersistencia.GetValorLong(Leitor, "IDSERVICOPRESTADO"))

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACAO") Then
                        ItemDeLancamento.Observacao = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACAO")
                    End If

                    Lancamento.AdicionaItemDeLancamento(ItemDeLancamento)
                End While
            End Using
        End If

        Return Lancamento
    End Function

    Public Function ObtenhaProximoNumeroDisponivel() As Long Implements IMapeadorDeLancamentoDeServicos.ObtenhaProximoNumeroDisponivel
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim NumeroMaximo As Long

        Sql.Append("SELECT MAX(NUMERO) AS NUMEROMAXIMO ")
        Sql.Append("FROM T13_LANCAMENTODESERVICOS ")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                NumeroMaximo = UtilidadesDePersistencia.GetValorLong(Leitor, "NUMEROMAXIMO") + 1
            End If
        End Using

        Return NumeroMaximo
    End Function

    Public Function NumeroEstaSendoUtilizando(ByVal Numero As Long) As Boolean Implements IMapeadorDeLancamentoDeServicos.NumeroEstaSendoUtilizando
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append("SELECT NUMERO ")
        Sql.Append("FROM T13_LANCAMENTODESERVICOS ")
        Sql.Append(String.Concat("WHERE NUMERO = ", Numero.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Return Leitor.Read
        End Using

    End Function

    Public Function ObtenhaLancamentosTardio(ByVal DataInicio As Date, _
                                             ByVal DataFim As Date) As IList(Of ILacamentoDeServicosPrestados) Implements IMapeadorDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Lancamento As ILacamentoDeServicosPrestados = Nothing
        Dim Lancamentos As IList(Of ILacamentoDeServicosPrestados)

        Sql.Append("SELECT ID, IDCLIENTE, DATA, NUMERO ")
        Sql.Append("FROM T13_LANCAMENTODESERVICOS ")
        Sql.Append(String.Concat("WHERE DATA >= ", DataInicio.ToString("yyyyMMdd")))
        Sql.Append(String.Concat(" AND DATA <= ", DataFim.ToString("yyyyMMdd")))
        Sql.Append(" ORDER BY IDCLIENTE, DATA")

        DBHelper = ServerUtils.criarNovoDbHelper
        Lancamentos = New List(Of ILacamentoDeServicosPrestados)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Lancamento = FabricaGenerica.GetInstancia.CrieObjeto(Of ILacamentoDeServicosPrestados)()
                Lancamento.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Lancamento.DataDeLancamento = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value
                Lancamento.Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCliente).Obtenha(UtilidadesDePersistencia.GetValorLong(Leitor, "IDCLIENTE"))
                Lancamento.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")
                Lancamentos.Add(Lancamento)
            End While
        End Using

        Return Lancamentos
    End Function

End Class