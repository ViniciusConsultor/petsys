Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Public Class MapeadorDePessoaJuridica
    Inherits MapeadorDePessoa(Of IPessoaJuridica)
    Implements IMapeadorDePessoaJuridica

    Protected Overrides Sub Atualize(ByVal Pessoa As IPessoaJuridica)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_PESSOAJURIDICA SET ")

        If Not String.IsNullOrEmpty(Pessoa.NomeFantasia) Then
            Sql.Append(String.Concat("NOMEFANTASIA='", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeFantasia), "', "))
        End If

        Dim CNPJ As ICNPJ = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ), ICNPJ)

        If Not CNPJ Is Nothing Then
            Sql.Append(String.Concat("CNPJ='", CNPJ.Numero, "', "))
        Else
            Sql.Append("CNPJ= NULL, ")
        End If

        Dim InscricaoEstadual As IInscricaoEstadual = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IE), IInscricaoEstadual)

        If Not InscricaoEstadual Is Nothing Then
            Sql.Append(String.Concat("IE='", InscricaoEstadual.Numero, "', "))
        Else
            Sql.Append("IE=NULL, ")
        End If

        Dim InscricaoMunicipal As IInscricaoMunicipal = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IM), IInscricaoMunicipal)

        If Not InscricaoMunicipal Is Nothing Then
            Sql.Append(String.Concat("IM='", InscricaoMunicipal.Numero, "', "))
        Else
            Sql.Append("IM=NULL, ")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Logomarca) Then
            Sql.Append(String.Concat("LOGOMARCA='", Pessoa.Logomarca, "' "))
        Else
            Sql.Append("LOGOMARCA=NULL ")
        End If


        Sql.Append(String.Concat("WHERE IDPESSOA = ", Pessoa.ID.Value))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Protected Overrides Function Carregue(ByVal Id As Long) As IPessoaJuridica
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, TIPO, ENDEMAIL, ")
        Sql.Append("NOMEFANTASIA, CNPJ, IE, IM, SITE, IDBANCO, IDAGENCIA, CNTACORRENTE, TIPOCNTACORRENTE, LOGOMARCA ")
        Sql.Append("FROM NCL_PESSOA, NCL_PESSOAJURIDICA")
        Sql.Append(" WHERE ID = IDPESSOA ")
        Sql.Append(String.Concat("AND ID = ", Id.ToString))

        Return ObtenhaPessoa(Sql.ToString)
    End Function

    Private Function ObtenhaPessoa(ByVal SQL As String) As IPessoaJuridica
        Dim Pessoas As IList(Of IPessoaJuridica)
        Dim Pessoa As IPessoaJuridica = Nothing

        Pessoas = ObtenhaPessoas(SQL, Integer.MaxValue)

        If Not Pessoas.Count = 0 Then
            Pessoa = Pessoas.Item(0)
        End If

        Return Pessoa
    End Function

    Protected Overrides Function CarreguePorNome(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaJuridica)
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, TIPO, ENDEMAIL, ")
        Sql.Append("IDPESSOA, NOMEFANTASIA, CNPJ, IE, IM, SITE, IDBANCO, IDAGENCIA, CNTACORRENTE, TIPOCNTACORRENTE, LOGOMARCA ")
        Sql.Append("FROM NCL_PESSOA, NCL_PESSOAJURIDICA")
        Sql.Append(" WHERE ID = IDPESSOA ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Sql.AppendLine(" ORDER BY NOME")

        Return ObtenhaPessoas(Sql.ToString, QuantidadeMaximaDeRegistros)
    End Function

    Private Function ObtenhaPessoas(ByVal Sql As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaJuridica)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Pessoa As IPessoaJuridica
        Dim Pessoas As IList(Of IPessoaJuridica)

        Pessoas = New List(Of IPessoaJuridica)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                    MyBase.PreencheDados(Pessoa, Leitor)

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEFANTASIA") Then
                        Pessoa.NomeFantasia = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEFANTASIA")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "CNPJ") Then
                        Pessoa.AdicioneDocumento(FabricaGenerica.GetInstancia.CrieObjeto(Of ICNPJ)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "CNPJ")}))
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "IE") Then
                        Pessoa.AdicioneDocumento(FabricaGenerica.GetInstancia.CrieObjeto(Of IInscricaoEstadual)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "IE")}))
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "IM") Then
                        Pessoa.AdicioneDocumento(FabricaGenerica.GetInstancia.CrieObjeto(Of IInscricaoMunicipal)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "IM")}))
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOGOMARCA") Then
                        Pessoa.Logomarca = UtilidadesDePersistencia.GetValorString(Leitor, "LOGOMARCA")
                    End If

                    Pessoas.Add(Pessoa)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Pessoas
    End Function

    Protected Overrides Sub Insira(ByVal Pessoa As IPessoaJuridica)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_PESSOAJURIDICA (IDPESSOA, NOMEFANTASIA, CNPJ, IE, IM, LOGOMARCA) ")
        Sql.Append("VALUES (")
        Sql.Append(String.Concat(Pessoa.ID.Value, ", "))

        If Not String.IsNullOrEmpty(Pessoa.NomeFantasia) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeFantasia), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Dim CNPJ As ICNPJ = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ), ICNPJ)

        If Not CNPJ Is Nothing Then
            Sql.Append(String.Concat("'", CNPJ.Numero, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Dim InscricaoEstadual As IInscricaoEstadual = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IE), IInscricaoEstadual)

        If Not InscricaoEstadual Is Nothing Then
            Sql.Append(String.Concat("'", InscricaoEstadual.Numero, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Dim InscricaoMunicipal As IInscricaoMunicipal = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IM), IInscricaoMunicipal)

        If Not InscricaoMunicipal Is Nothing Then
            Sql.Append(String.Concat("'", InscricaoMunicipal.Numero, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Logomarca) Then
            Sql.Append(String.Concat("'", Pessoa.Logomarca, "'"))
        Else
            Sql.Append("NULL")
        End If

        Sql.Append(")")

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Protected Overrides Sub Remova(ByVal ID As Long)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_PESSOAJURIDICA ")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class