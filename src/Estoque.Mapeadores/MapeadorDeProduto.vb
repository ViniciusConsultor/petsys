Imports Estoque.Interfaces.Mapeadores
Imports Estoque.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas

Public Class MapeadorDeProduto
    Implements IMapeadorDeProduto

    Public Sub AtualizarMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IMapeadorDeProduto.AtualizarMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE ETQ_MARCAPRODUTO SET ")
        Sql.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Marca.Nome), "'"))
        Sql.Append(String.Concat(" WHERE ID = ", Marca.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub InserirMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IMapeadorDeProduto.InserirMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Marca.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO ETQ_MARCAPRODUTO (")
        Sql.Append("ID, NOME)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Marca.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Marca.Nome), "')"))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaMarcaDeProduto(ByVal ID As Long) As IMarcaDeProduto Implements IMapeadorDeProduto.ObtenhaMarcaDeProduto
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME")
        Sql.Append(" FROM ETQ_MARCAPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        Return ObtenhaMarcasDePodutos(Sql.ToString, 1)(0)
    End Function

    Private Function ObtenhaMarcasDePodutos(ByVal SQL As String, _
                                            ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Marcas As IList(Of IMarcaDeProduto)
        Dim Marca As IMarcaDeProduto

        Marcas = New List(Of IMarcaDeProduto)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read AndAlso Marcas.Count < QuantidadeDeRegistros
                    Marca = FabricaGenerica.GetInstancia.CrieObjeto(Of IMarcaDeProduto)()
                    Marca.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Marca.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                    Marcas.Add(Marca)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Marcas
    End Function

    Public Function ObtenhaMarcasDeProdutosPorNome(ByVal Nome As String, _
                                                   ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto) Implements IMapeadorDeProduto.ObtenhaMarcasDeProdutosPorNome
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME")
        Sql.Append(" FROM ETQ_MARCAPRODUTO")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat(" WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Return ObtenhaMarcasDePodutos(Sql.ToString, QuantidadeDeRegistros)
    End Function

    Public Sub RemoverMarcaDeProduto(ByVal ID As Long) Implements IMapeadorDeProduto.RemoverMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM ETQ_MARCAPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub AtualizarProduto(ByVal Produto As IProduto) Implements IMapeadorDeProduto.AtualizarProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE ETQ_PRODUTO SET")

        If String.IsNullOrEmpty(Produto.CodigoDeBarras) Then
            Sql.Append(" CODIGOBARRAS = NULL,")
        Else
            Sql.Append(String.Concat(" CODIGOBARRAS = '", UtilidadesDePersistencia.FiltraApostrofe(Produto.CodigoDeBarras), "',"))
        End If

        Sql.Append(String.Concat(" NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Produto.Nome), "',"))

        If Produto.UnidadeDeMedida Is Nothing Then
            Sql.Append(" UNIDADE = NULL,")
        Else
            Sql.Append(String.Concat(" UNIDADE = '", Produto.UnidadeDeMedida.ID.ToString, "',"))
        End If

        If Produto.Marca Is Nothing Then
            Sql.Append(" IDMARCA = NULL,")
        Else
            Sql.Append(String.Concat(" IDMARCA = ", Produto.Marca.ID.Value, ","))
        End If

        If Produto.QuantidadeMinimaEmEstoque Is Nothing Then
            Sql.Append(" QTDMINESTOQUE = NULL,")
        Else
            Sql.Append(String.Concat(" QTDMINESTOQUE = ", UtilidadesDePersistencia.TPVd(Produto.QuantidadeMinimaEmEstoque.Value), ","))
        End If

        If Produto.ValorDeCusto Is Nothing Then
            Sql.Append(" VALORDECUSTO = NULL,")
        Else
            Sql.Append(String.Concat(" VALORDECUSTO = ", UtilidadesDePersistencia.TPVd(Produto.ValorDeCusto.Value), ","))
        End If

        If Produto.ValorDeVendaMinimo Is Nothing Then
            Sql.Append(" VALORVENDAMIN = NULL,")
        Else
            Sql.Append(String.Concat(" VALORVENDAMIN = ", UtilidadesDePersistencia.TPVd(Produto.ValorDeVendaMinimo.Value), ","))
        End If

        If Produto.PorcentagemDeLucro Is Nothing Then
            Sql.Append(" PORCLUCRO = NULL,")
        Else
            Sql.Append(String.Concat(" PORCLUCRO = ", UtilidadesDePersistencia.TPVd(Produto.PorcentagemDeLucro.Value), ","))
        End If

        Sql.Append(String.Concat(" IDGRPPROD = ", Produto.GrupoDeProduto.ID.Value.ToString, ","))

        If String.IsNullOrEmpty(Produto.Observacoes) Then
            Sql.Append(" OBSERVACOES = NULL")
        Else
            Sql.Append(String.Concat(" OBSERVACOES = '", UtilidadesDePersistencia.FiltraApostrofe(Produto.Observacoes), "'"))
        End If
        Sql.Append(String.Concat(" WHERE ID = ", Produto.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub InserirProduto(ByVal Produto As IProduto) Implements IMapeadorDeProduto.InserirProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Produto.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO ETQ_PRODUTO (")
        Sql.Append("ID, CODIGOBARRAS, NOME, UNIDADE, IDMARCA, QTDMINESTOQUE, VALORDECUSTO, VALORVENDAMIN, PORCLUCRO, IDGRPPROD, OBSERVACOES)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Produto.ID.Value.ToString, ", "))

        If String.IsNullOrEmpty(Produto.CodigoDeBarras) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Produto.CodigoDeBarras), "', "))
        End If

        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Produto.Nome), "', "))

        If Produto.UnidadeDeMedida Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", Produto.UnidadeDeMedida.ID.ToString, "', "))
        End If

        If Produto.Marca Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(Produto.Marca.ID.Value, ", "))
        End If

        If Produto.QuantidadeMinimaEmEstoque Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Produto.QuantidadeMinimaEmEstoque.Value), ", "))
        End If

        If Produto.ValorDeCusto Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Produto.ValorDeCusto.Value), ", "))
        End If

        If Produto.ValorDeVendaMinimo Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Produto.ValorDeVendaMinimo.Value), ", "))
        End If

        If Produto.PorcentagemDeLucro Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Produto.PorcentagemDeLucro.Value), ", "))
        End If

        Sql.Append(String.Concat(Produto.GrupoDeProduto.ID.Value.ToString, ", "))

        If String.IsNullOrEmpty(Produto.Observacoes) Then
            Sql.Append("NULL)")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Produto.Observacoes), "')"))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaProduto(ByVal ID As Long) As IProduto Implements IMapeadorDeProduto.ObtenhaProduto
        Dim SQL As New StringBuilder
        Dim Produtos As IList(Of IProduto)

        SQL.Append("SELECT ETQ_PRODUTO.ID AS IDPRODUTO, CODIGOBARRAS, ETQ_PRODUTO.NOME AS NOMEPRODUTO, UNIDADE, IDMARCA, QTDMINESTOQUE, VALORDECUSTO, VALORVENDAMIN, PORCLUCRO, IDGRPPROD, OBSERVACOES,")
        SQL.Append(" ETQ_GRPPRODUTO.ID AS IDGRPPRODUTO, ETQ_GRPPRODUTO.NOME AS NOMEGRPPRODUTO, PRCCOMISSAO,")
        SQL.Append(" ETQ_MARCAPRODUTO.ID AS ID_MARCA, ETQ_MARCAPRODUTO.NOME AS NOMEMARCA")
        SQL.Append(" FROM ETQ_PRODUTO")
        SQL.Append(" INNER JOIN ETQ_GRPPRODUTO ON IDGRPPROD = ETQ_GRPPRODUTO.ID")
        SQL.Append(" LEFT JOIN ETQ_MARCAPRODUTO ON IDMARCA = ETQ_MARCAPRODUTO.ID")
        SQL.Append(String.Concat(" WHERE ETQ_PRODUTO.ID = ", ID.ToString))

        Produtos = ObtenhaEMonteProdutos(SQL.ToString, 1)

        If Not Produtos.Count = 0 Then Return Produtos.Item(0)

        Return Nothing
    End Function

    Public Function ObtenhaProduto(ByVal CodigoDeBarras As String) As IProduto Implements IMapeadorDeProduto.ObtenhaProduto
        Dim SQL As New StringBuilder
        Dim Produtos As IList(Of IProduto)

        SQL.Append("SELECT ETQ_PRODUTO.ID AS IDPRODUTO, CODIGOBARRAS, ETQ_PRODUTO.NOME AS NOMEPRODUTO, UNIDADE, IDMARCA, QTDMINESTOQUE, VALORDECUSTO, VALORVENDAMIN, PORCLUCRO, IDGRPPROD, OBSERVACOES,")
        SQL.Append(" ETQ_GRPPRODUTO.ID AS IDGRPPRODUTO, ETQ_GRPPRODUTO.NOME AS NOMEGRPPRODUTO, PRCCOMISSAO,")
        SQL.Append(" ETQ_MARCAPRODUTO.ID AS ID_MARCA, ETQ_MARCAPRODUTO.NOME AS NOMEMARCA")
        SQL.Append(" FROM ETQ_PRODUTO")
        SQL.Append(" INNER JOIN ETQ_GRPPRODUTO ON IDGRPPROD = ETQ_GRPPRODUTO.ID")
        SQL.Append(" LEFT JOIN ETQ_MARCAPRODUTO ON IDMARCA = ETQ_MARCAPRODUTO.ID")
        SQL.Append(String.Concat(" WHERE CODIGOBARRAS = '", UtilidadesDePersistencia.FiltraApostrofe(CodigoDeBarras), "'"))

        Produtos = ObtenhaEMonteProdutos(SQL.ToString, 1)

        If Not Produtos.Count = 0 Then Return Produtos.Item(0)

        Return Nothing
    End Function

    Public Function ObtenhaProdutos(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IProduto) Implements IMapeadorDeProduto.ObtenhaProdutos
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ETQ_PRODUTO.ID AS IDPRODUTO, CODIGOBARRAS, ETQ_PRODUTO.NOME AS NOMEPRODUTO, UNIDADE, IDMARCA, QTDMINESTOQUE, VALORDECUSTO, VALORVENDAMIN, PORCLUCRO, IDGRPPROD, OBSERVACOES,")
        SQL.Append(" ETQ_GRPPRODUTO.ID AS IDGRPPRODUTO, ETQ_GRPPRODUTO.NOME AS NOMEGRPPRODUTO, PRCCOMISSAO,")
        SQL.Append(" ETQ_MARCAPRODUTO.ID AS ID_MARCA, ETQ_MARCAPRODUTO.NOME AS NOMEMARCA")
        SQL.Append(" FROM ETQ_PRODUTO")
        SQL.Append(" INNER JOIN ETQ_GRPPRODUTO ON IDGRPPROD = ETQ_GRPPRODUTO.ID")
        SQL.Append(" LEFT JOIN ETQ_MARCAPRODUTO ON IDMARCA = ETQ_MARCAPRODUTO.ID")

        If Not String.IsNullOrEmpty(Nome) Then
            SQL.Append(String.Concat(" WHERE ETQ_PRODUTO.NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Return ObtenhaEMonteProdutos(SQL.ToString, QuantidadeDeRegistros)
    End Function

    Private Function ObtenhaEMonteGrupoDeProduto(ByVal Leitor As IDataReader) As IGrupoDeProduto
        Dim GrupoDeProduto As IGrupoDeProduto

        GrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeProduto)()

        GrupoDeProduto.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDGRPPRODUTO")
        GrupoDeProduto.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEGRPPRODUTO")
        GrupoDeProduto.PorcentagemDeComissao = UtilidadesDePersistencia.getValorDouble(Leitor, "PRCCOMISSAO")

        Return GrupoDeProduto
    End Function

    Private Function ObtenhaEMonteMarcaDeProduto(ByVal Leitor As IDataReader) As IMarcaDeProduto
        Dim MarcaDeProduto As IMarcaDeProduto = Nothing

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDMARCA") Then
            MarcaDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IMarcaDeProduto)()

            MarcaDeProduto.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID_MARCA")
            MarcaDeProduto.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEMARCA")
        End If

        Return MarcaDeProduto
    End Function

    Private Function ObtenhaEMonteProdutos(ByVal SQL As String, _
                                           ByVal QuantidadeDeRegistros As Integer) As IList(Of IProduto)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Produtos As IList(Of IProduto)
        Dim Produto As IProduto

        Produtos = New List(Of IProduto)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read AndAlso Produtos.Count < QuantidadeDeRegistros
                    Produto = FabricaGenerica.GetInstancia.CrieObjeto(Of IProduto)()

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "CODIGOBARRAS") Then Produto.CodigoDeBarras = UtilidadesDePersistencia.GetValorString(Leitor, "CODIGOBARRAS")

                    Produto.GrupoDeProduto = Me.ObtenhaEMonteGrupoDeProduto(Leitor)
                    Produto.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDPRODUTO")
                    Produto.Marca = Me.ObtenhaEMonteMarcaDeProduto(Leitor)
                    Produto.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPRODUTO")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then Produto.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "PORCLUCRO") Then Produto.PorcentagemDeLucro = UtilidadesDePersistencia.getValorDouble(Leitor, "PORCLUCRO")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "QTDMINESTOQUE") Then Produto.QuantidadeMinimaEmEstoque = UtilidadesDePersistencia.getValorDouble(Leitor, "QTDMINESTOQUE")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "UNIDADE") Then Produto.UnidadeDeMedida = UnidadeDeMedida.ObtenhaTipoDeUnidade(UtilidadesDePersistencia.getValorChar(Leitor, "UNIDADE"))

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "VALORDECUSTO") Then Produto.ValorDeCusto = UtilidadesDePersistencia.getValorDouble(Leitor, "VALORDECUSTO")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "VALORVENDAMIN") Then Produto.ValorDeVendaMinimo = UtilidadesDePersistencia.getValorDouble(Leitor, "VALORVENDAMIN")

                    Produtos.Add(Produto)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Produtos
    End Function

    Public Sub RemoverProduto(ByVal ID As Long) Implements IMapeadorDeProduto.RemoverProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM ETQ_PRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaQuantidadeEmEstoqueDoProduto(ByVal IDProduto As Long) As Double Implements IMapeadorDeProduto.ObtenhaQuantidadeEmEstoqueDoProduto
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim SqlEntrada As String = "SELECT SUM(QUANTIDADE) AS QUANTIDADEENTRADA FROM ETQ_PRODUTOMOV, ETQ_MOVPRODUTO WHERE IDPRODUTO = " & IDProduto.ToString & " AND IDMOVIMENTACAO = ID AND TIPO = '" & TipoMovimentacaoDeProduto.Entrada.ID & "'"
        Dim SqlSaida As String = "SELECT SUM(QUANTIDADE) AS QUANTIDADESAIDA FROM ETQ_PRODUTOMOV, ETQ_MOVPRODUTO WHERE IDPRODUTO = " & IDProduto.ToString & " AND IDMOVIMENTACAO = ID AND TIPO = '" & TipoMovimentacaoDeProduto.Saida.ID & "'"
        Dim QuantidadeEntrada As Double = 0
        Dim QuantidadeSaida As Double = 0

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SqlEntrada)
            If Leitor.Read Then
                QuantidadeEntrada = UtilidadesDePersistencia.getValorDouble(Leitor, "QUANTIDADEENTRADA")
            End If
        End Using

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SqlSaida)
            If Leitor.Read Then
                QuantidadeSaida = UtilidadesDePersistencia.getValorDouble(Leitor, "QUANTIDADESAIDA")
            End If
        End Using

        Return QuantidadeEntrada - QuantidadeSaida
    End Function

End Class