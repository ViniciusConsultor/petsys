Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeFornecedor
    Implements IMapeadorDeFornecedor

    Private Const SQL_FORNECEDOR As String = " SELECT IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL, IDPESSOACONTATO, P1.NOME AS NOMEFORNECEDOR," & _
                                             " P2.NOME AS NOMECONTATO FROM NCL_PESSOA P1, NCL_FORNECEDOR " & _
                                             " LEFT JOIN NCL_CONTATOFORNECEDOR ON IDPESSOA = IDPFORNECEDOR " & _
                                             " LEFT JOIN NCL_PESSOA P2 ON P2.ID = IDPESSOACONTATO " & _
                                             " WHERE P1.ID = IDPESSOA AND P1.TIPO = TIPOPESSOA "

    Public Sub Inserir(ByVal Fornecedor As IFornecedor) Implements IMapeadorDeFornecedor.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_FORNECEDOR (")
        Sql.Append("IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Fornecedor.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Fornecedor.Pessoa.Tipo.ID, ", "))
        Sql.Append(String.Concat(Fornecedor.DataDoCadastro.ToString("yyyyMMdd"), ", "))

        If String.IsNullOrEmpty(Fornecedor.InformacoesAdicionais) Then
            Sql.Append("NULL)")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Fornecedor.InformacoesAdicionais), "')"))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
        InsiraContatosDoFornecedor(Fornecedor, DBHelper)
    End Sub

    Private Sub ApagaContatosDoFornecedor(ByVal IDFornecedor As Long, ByRef Helper As IDBHelper)
        Dim Sql As New StringBuilder

        Sql.Append("DELETE FROM NCL_CONTATOFORNECEDOR WHERE IDPFORNECEDOR = " & IDFornecedor.ToString)

        Helper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Sub InsiraContatosDoFornecedor(ByVal Fornecedor As IFornecedor, ByRef Helper As IDBHelper)
        Dim Sql As StringBuilder

        For Each Contato As IPessoaFisica In Fornecedor.Contatos
            Sql = New StringBuilder

            Sql.Append("INSERT INTO NCL_CONTATOFORNECEDOR (")
            Sql.Append("IDPFORNECEDOR, IDPESSOACONTATO) ")
            Sql.Append("VALUES (")
            Sql.Append(String.Concat(Fornecedor.Pessoa.ID.Value.ToString, ", "))
            Sql.Append(String.Concat(Contato.ID.Value.ToString, ")"))

            Helper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

    Public Sub Modificar(ByVal Fornecedor As IFornecedor) Implements IMapeadorDeFornecedor.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_FORNECEDOR SET ")

        If String.IsNullOrEmpty(Fornecedor.InformacoesAdicionais) Then
            Sql.Append("INFOADICIONAL = NULL")
        Else
            Sql.Append(String.Concat("INFOADICIONAL = '", UtilidadesDePersistencia.FiltraApostrofe(Fornecedor.InformacoesAdicionais), "'"))
        End If

        Sql.Append(" WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Fornecedor.Pessoa.ID.ToString, "AND "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Fornecedor.Pessoa.Tipo.ID))

        If String.IsNullOrEmpty(Fornecedor.InformacoesAdicionais) Then
            Sql.Append("NULL)")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Fornecedor.InformacoesAdicionais), "')"))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
        ApagaContatosDoFornecedor(Fornecedor.Pessoa.ID.Value, DBHelper)
        InsiraContatosDoFornecedor(Fornecedor, DBHelper)
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IFornecedor Implements IMapeadorDeFornecedor.Obtenha
        Dim Sql As New StringBuilder
        Dim Fornecedor As IFornecedor = Nothing

        Sql.Append(SQL_FORNECEDOR)
        Sql.Append(" AND IDPESSOA = " & Pessoa.ID.Value.ToString)

        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                Dim Fornecedores As IList(Of IFornecedor) = MonteObjetos(Leitor)

                If Fornecedores.Count > 0 Then Fornecedor = Fornecedores.Item(0)
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Fornecedor
    End Function

    Public Function Obtenha(ByVal ID As Long) As IFornecedor Implements IMapeadorDeFornecedor.Obtenha
        Dim Sql As New StringBuilder
        Dim Fornecedor As IFornecedor = Nothing

        Sql.Append(SQL_FORNECEDOR)
        Sql.Append(" AND IDPESSOA = " & ID.ToString)

        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                Dim Fornecedores As IList(Of IFornecedor) = MonteObjetos(Leitor)

                If Fornecedores.Count > 0 Then Fornecedor = Fornecedores.Item(0)
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Fornecedor
    End Function

    Private Function MonteObjetos(ByVal Leitor As IDataReader) As IList(Of IFornecedor)
        Dim Fornecedor As IFornecedor = Nothing
        Dim Fornecedores As IList(Of IFornecedor) = New List(Of IFornecedor)
        Dim IdCorrente As String = ""

        While Leitor.Read
            Try
                If IdCorrente <> UtilidadesDePersistencia.GetValorString(Leitor, "IDPESSOA") Then
                    Dim Pessoa As IPessoa
                    Dim TipoPessoa As TipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPOPESSOA"))

                    If TipoPessoa.Equals(TipoDePessoa.Fisica) Then
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
                    Else
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
                    End If

                    Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEFORNECEDOR")

                    Fornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IFornecedor)(New Object() {Pessoa})
                    Fornecedor.DataDoCadastro = UtilidadesDePersistencia.getValorDate(Leitor, "DTCADASTRO").Value

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "INFOADICIONAL") Then
                        Fornecedor.InformacoesAdicionais = UtilidadesDePersistencia.GetValorString(Leitor, "INFOADICIONAL")
                    End If
                    Fornecedores.Add(Fornecedor)
                    IdCorrente = UtilidadesDePersistencia.GetValorString(Leitor, "IDPESSOA")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDPESSOACONTATO") Then
                    Dim Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOACONTATO"))

                    Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMECONTATO")
                    Fornecedor.AdicionaContato(Pessoa)
                End If
            Finally
                Leitor.Close()
            End Try
        End While

        Return Fornecedores
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IFornecedor) Implements IMapeadorDeFornecedor.ObtenhaPorNomeComoFiltro
        Dim Sql As New StringBuilder

        Sql.Append(SQL_FORNECEDOR)

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("AND P1.NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                Return MonteObjetos(Leitor)
            Finally
                Leitor.Close()
            End Try
        End Using
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeFornecedor.Remover
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        ApagaContatosDoFornecedor(ID, DBHelper)

        Dim Sql As New StringBuilder

        Sql.Append("DELETE FROM NCL_FORNECEDOR WHERE IDPESSOA = " & ID.ToString)
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class