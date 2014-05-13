Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeCedente
    Implements IMapeadorDeCedente
    
    Public Sub Inserir(Cedente As ICedente) Implements IMapeadorDeCedente.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_CEDENTE (")
        Sql.Append("IDPESSOA, TIPOPESSOA, IMAGEMBOLETO, TIPODECARTEIRA, INICIONOSSONUMERO, NUMEROAGENCIA, NUMEROCONTA, TIPOCONTA, PADRAO, NUMERODOBANCO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Cedente.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Cedente.Pessoa.Tipo.ID, ", "))
        
        If String.IsNullOrEmpty(Cedente.ImagemDeCabecalhoDoReciboDoSacado) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cedente.ImagemDeCabecalhoDoReciboDoSacado), "', "))
        End If

        If Cedente.TipoDeCarteira Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", Cedente.TipoDeCarteira.ID.ToString(), "', "))
        End If

        If Cedente.InicioNossoNumero <= 0 Then
            Sql.Append("10001001, ")
        Else
            Sql.Append(String.Concat(Cedente.InicioNossoNumero, ", "))
        End If

        If String.IsNullOrEmpty(Cedente.NumeroDaAgencia) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDaAgencia), "', "))
        End If

        If String.IsNullOrEmpty(Cedente.NumeroDaConta) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDaConta), "', "))
        End If

        If Cedente.TipoDaConta <= 0 Then
            Sql.Append("0, ")
        Else
            Sql.Append(String.Concat(Cedente.TipoDaConta, ", "))
        End If

        If Cedente.Padrao Then
            Sql.Append(String.Concat("'1', "))
        Else
            Sql.Append(String.Concat("'0', "))
        End If

        If String.IsNullOrEmpty(Cedente.NumeroDoBanco) Then
            Sql.Append("NULL) ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDoBanco), "') "))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(Cedente As ICedente) Implements IMapeadorDeCedente.Modificar

        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_CEDENTE SET ")
        Sql.Append(String.Concat(" IMAGEMBOLETO = '", UtilidadesDePersistencia.FiltraApostrofe(Cedente.ImagemDeCabecalhoDoReciboDoSacado), "', "))
        Sql.Append(String.Concat(" TIPODECARTEIRA = '", UtilidadesDePersistencia.FiltraApostrofe(Cedente.TipoDeCarteira.ID.ToString()), "', "))
        Sql.Append(String.Concat(" INICIONOSSONUMERO = ", Cedente.InicioNossoNumero, ", "))
        Sql.Append(String.Concat(" NUMEROAGENCIA = '", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDaAgencia), "', "))
        Sql.Append(String.Concat(" NUMEROCONTA = '", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDaConta), "', "))
        Sql.Append(String.Concat(" TIPOCONTA = ", Cedente.TipoDaConta, ", "))

        If Cedente.Padrao Then
            Sql.Append(String.Concat(" PADRAO = '1', "))
        Else
            Sql.Append(String.Concat(" PADRAO = '0', "))
        End If

        Sql.Append(String.Concat(" NUMERODOBANCO = '", UtilidadesDePersistencia.FiltraApostrofe(Cedente.NumeroDoBanco), "' "))

        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Cedente.Pessoa.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)

    End Sub

    Public Function ObtenhaCedentePadrao() As Long Implements IMapeadorDeCedente.ObtenhaCedentePadrao

        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Dim idCedente As Long = 0

        Sql.Append("SELECT IDPESSOA FROM NCL_CEDENTE ")
        Sql.Append("WHERE PADRAO = '1' ")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    idCedente = UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA")
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return idCedente

    End Function

    Public Function Obtenha(Pessoa As IPessoa) As ICedente Implements IMapeadorDeCedente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cedente As ICedente = Nothing

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA, IMAGEMBOLETO, TIPODECARTEIRA, INICIONOSSONUMERO, NUMEROAGENCIA, NUMEROCONTA, TIPOCONTA, PADRAO, NUMERODOBANCO FROM NCL_CEDENTE ")
        Sql.Append("WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString, " AND "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Pessoa.Tipo.ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Cedente = MontaObjetoCedente(Leitor, Pessoa)
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Cedente
    End Function

    Private Function MontaObjetoCedente(ByVal Leitor As IDataReader, ByVal Pessoa As IPessoa) As ICedente
        Dim Cedente As ICedente = Nothing

        Cedente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICedente)(New Object() {Pessoa})
        
        Cedente.ImagemDeCabecalhoDoReciboDoSacado = UtilidadesDePersistencia.GetValorString(Leitor, "IMAGEMBOLETO")

        Cedente.TipoDeCarteira = TipoDeCarteira.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPODECARTEIRA"))

        Cedente.InicioNossoNumero = UtilidadesDePersistencia.GetValorLong(Leitor, "INICIONOSSONUMERO")

        Cedente.NumeroDaAgencia = UtilidadesDePersistencia.GetValorString(Leitor, "NUMEROAGENCIA")

        Cedente.NumeroDaConta = UtilidadesDePersistencia.GetValorString(Leitor, "NUMEROCONTA")

        Cedente.TipoDaConta = UtilidadesDePersistencia.getValorInteger(Leitor, "TIPOCONTA")

        Cedente.Padrao = UtilidadesDePersistencia.GetValorBooleano(Leitor, "PADRAO")

        Cedente.NumeroDoBanco = UtilidadesDePersistencia.GetValorString(Leitor, "NUMERODOBANCO")
        
        Return Cedente
    End Function

    Public Function Obtenha(ID As Long) As ICedente Implements IMapeadorDeCedente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cedente As ICedente = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID IDDAPESSOA, NCL_PESSOA.NOME NOMEPESSOA, NCL_PESSOA.TIPO, ")
        Sql.Append("NCL_CEDENTE.IDPESSOA, NCL_CEDENTE.TIPOPESSOA, NCL_CEDENTE.IMAGEMBOLETO, NCL_CEDENTE.TIPODECARTEIRA, NCL_CEDENTE.INICIONOSSONUMERO, ")
        Sql.Append("NCL_CEDENTE.NUMEROAGENCIA, NCL_CEDENTE.NUMEROCONTA, NCL_CEDENTE.TIPOCONTA, NCL_CEDENTE.PADRAO, NCL_CEDENTE.NUMERODOBANCO ")
        Sql.Append("FROM NCL_PESSOA, NCL_CEDENTE ")
        Sql.Append("WHERE NCL_CEDENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CEDENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")
        Sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                    If Tipo.Equals(TipoDePessoa.Fisica) Then
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA"))
                    Else
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA"))
                    End If

                    Cedente = MontaObjetoCedente(Leitor, Pessoa)
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Cedente
    End Function

    Public Function ObtenhaPorNomeComoFiltro(Nome As String, QuantidadeMaximaDeRegistros As Integer) As IList(Of ICedente) Implements IMapeadorDeCedente.ObtenhaPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cedente As ICedente = Nothing
        Dim Pessoa As IPessoa = Nothing
        Dim Tipo As TipoDePessoa
        Dim Cedentes As IList(Of ICedente) = New List(Of ICedente)

        Sql.Append("SELECT NCL_PESSOA.ID IDDAPESSOA, NCL_PESSOA.NOME NOMEPESSOA, NCL_PESSOA.TIPO,")
        Sql.Append("NCL_CEDENTE.IDPESSOA, NCL_CEDENTE.TIPOPESSOA, NCL_CEDENTE.IMAGEMBOLETO, NCL_CEDENTE.TIPODECARTEIRA, NCL_CEDENTE.INICIONOSSONUMERO, ")
        Sql.Append("NCL_CEDENTE.NUMEROAGENCIA, NCL_CEDENTE.NUMEROCONTA, NCL_CEDENTE.TIPOCONTA, NCL_CEDENTE.PADRAO, NCL_CEDENTE.NUMERODOBANCO ")
        Sql.Append("FROM NCL_PESSOA, NCL_CEDENTE ")
        Sql.Append("WHERE NCL_CEDENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CEDENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")
        
        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat(" AND NCL_PESSOA.NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                    If Tipo.Equals(TipoDePessoa.Fisica) Then
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA"))
                    Else
                        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA"))
                    End If

                    Cedente = MontaObjetoCedente(Leitor, Pessoa)
                    Cedentes.Add(Cedente)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Cedentes
    End Function

    Public Sub Remover(ID As Long) Implements IMapeadorDeCedente.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_CEDENTE")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class