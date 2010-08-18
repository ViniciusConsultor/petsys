Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Public MustInherit Class MapeadorDePessoa(Of T As IPessoa)
    Implements IMapeadorDePessoa(Of T)

    Protected MustOverride Sub Insira(ByVal Pessoa As T)
    Protected MustOverride Sub Atualize(ByVal Pessoa As T)
    Protected MustOverride Sub Remova(ByVal ID As Long)
    Protected MustOverride Function Carregue(ByVal Id As Long) As T
    Protected MustOverride Function CarreguePorNome(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of T)

    Public Function Inserir(ByVal Pessoa As T) As Long Implements IMapeadorDePessoa(Of T).Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Pessoa.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_PESSOA (ID, NOME, TIPO, ENDEMAIL,")
        SQL.Append(" LOGRADOURO, COMPLEMENTO, IDMUNICIPIO, CEP,")
        SQL.Append(" BAIRRO, SITE)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Pessoa.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Nome), "', "))
        SQL.Append(String.Concat("'", Pessoa.Tipo.ID.ToString, "', "))

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.EnderecoDeEmail.ToString), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not Pessoa.Endereco Is Nothing Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Logradouro), "', "))

            If Not String.IsNullOrEmpty(Pessoa.Endereco.Complemento) Then
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Complemento), "', "))
            Else
                SQL.Append("NULL, ")
            End If

            If Not Pessoa.Endereco.Municipio Is Nothing Then
                SQL.Append(String.Concat(Pessoa.Endereco.Municipio.ID.Value, ", "))
            Else
                SQL.Append("NULL, ")
            End If

            If Not Pessoa.Endereco.CEP Is Nothing Then
                SQL.Append(String.Concat(Pessoa.Endereco.CEP.Numero.Value, ", "))
            Else
                SQL.Append("NULL, ")
            End If

            If Not String.IsNullOrEmpty(Pessoa.Endereco.Bairro) Then
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Bairro), "',"))
            Else
                SQL.Append("NULL, ")
            End If

        Else
            SQL.Append("NULL, NULL, NULL, NULL, NULL,")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Site) Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Site), "')"))
        Else
            SQL.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(SQL.ToString)

        InsiraTelefones(Pessoa)
        Me.Insira(Pessoa)
        Return Pessoa.ID.Value
    End Function

    Private Sub InsiraTelefones(ByVal Pessoa As IPessoa)
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        RemovaTelefones(Pessoa.ID.Value)

        If Not Pessoa.Telefones Is Nothing AndAlso Not Pessoa.Telefones.Count = 0 Then
            For Each Telefone As ITelefone In Pessoa.Telefones
                SQL = New StringBuilder
                SQL.Append("INSERT INTO NCL_PESSOATELEFONE (IDPESSOA, DDD, NUMERO, TIPO, INDICE)")
                SQL.Append(" VALUES ( ")
                SQL.Append(String.Concat(Pessoa.ID, ", "))
                SQL.Append(String.Concat(Telefone.DDD, ", "))
                SQL.Append(String.Concat(Telefone.Numero, ", "))
                SQL.Append(String.Concat(Telefone.Tipo.ID, ", "))
                SQL.Append(String.Concat(Pessoa.Telefones.IndexOf(Telefone), ") "))
                DBHelper.ExecuteNonQuery(SQL.ToString)
            Next
        End If
    End Sub

    Private Sub RemovaTelefones(ByVal IDPessoa As Long)
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_PESSOATELEFONE WHERE IDPESSOA = " & IDPessoa)
    End Sub

    Public Function Obtenha(ByVal ID As Long) As T Implements IMapeadorDePessoa(Of T).Obtenha
        Return Me.Carregue(ID)
    End Function

    Public Sub Atualizar(ByVal Pessoa As T) Implements IMapeadorDePessoa(Of T).Atualizar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper

        Me.InsiraTelefones(Pessoa)
        Me.Atualize(Pessoa)
        DBHelper = ServerUtils.getDBHelper
        SQL.Append(String.Concat("UPDATE NCL_PESSOA SET NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Nome), "', "))

        SQL.Append(" ENDEMAIL = ")

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.EnderecoDeEmail.ToString), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not Pessoa.Endereco Is Nothing Then
            SQL.Append(String.Concat("LOGRADOURO = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Logradouro), "', "))

            SQL.Append(" COMPLEMENTO = ")

            If Not String.IsNullOrEmpty(Pessoa.Endereco.Complemento) Then
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Complemento), "', "))
            Else
                SQL.Append("NULL, ")
            End If

            SQL.Append(" IDMUNICIPIO = ")

            If Not Pessoa.Endereco.Municipio Is Nothing Then
                SQL.Append(String.Concat(Pessoa.Endereco.Municipio.ID.Value, ", "))
            Else
                SQL.Append("NULL, ")
            End If

            SQL.Append(" CEP = ")

            If Not Pessoa.Endereco.CEP Is Nothing Then
                SQL.Append(String.Concat(Pessoa.Endereco.CEP.Numero.Value, ", "))
            Else
                SQL.Append("NULL, ")
            End If

            SQL.Append(" BAIRRO = ")

            If Not String.IsNullOrEmpty(Pessoa.Endereco.Bairro) Then
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Bairro), "',"))
            Else
                SQL.Append("NULL, ")
            End If

        Else
            SQL.Append("LOGRADOURO = NULL, COMPLEMENTO = NULL, IDMUNICIPIO = NULL, CEP = NULL, BAIRRO = NULL,")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Site) Then
            SQL.Append(String.Concat("SITE = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Site), "'"))
        Else
            SQL.Append("SITE = NULL")
        End If

        SQL.Append(String.Concat(" WHERE ID = ", Pessoa.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDePessoa(Of T).Remover
        Dim SQL As String
        Dim DBHelper As IDBHelper

        Me.Remova(ID)
        Me.RemovaTelefones(ID)
        DBHelper = ServerUtils.getDBHelper
        SQL = String.Concat("DELETE FROM NCL_PESSOA WHERE ID = ", ID.ToString)
        DBHelper.ExecuteNonQuery(SQL)
    End Sub

    Public Overloads Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                              ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of T) Implements IMapeadorDePessoa(Of T).ObtenhaPessoasPorNomeComoFiltro
        Return Me.CarreguePorNome(Nome, _
                                  QuantidadeMaximaDeRegistros)
    End Function

    Protected Sub PreencheDados(ByRef Pessoa As T, ByVal Leitor As IDataReader)
        Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "ENDEMAIL") Then
            Pessoa.EnderecoDeEmail = UtilidadesDePersistencia.GetValorString(Leitor, "ENDEMAIL")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOGRADOURO") Then
            Dim Endereco As IEndereco
            Dim MapeadorDeMunicipio As IMapeadorDeMunicipio

            Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()
            Endereco.Bairro = UtilidadesDePersistencia.GetValorString(Leitor, "BAIRRO")
            Endereco.CEP = New CEP(UtilidadesDePersistencia.GetValorLong(Leitor, "CEP"))
            Endereco.Complemento = UtilidadesDePersistencia.GetValorString(Leitor, "COMPLEMENTO")
            Endereco.Logradouro = UtilidadesDePersistencia.GetValorString(Leitor, "LOGRADOURO")

            MapeadorDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()
            Endereco.Municipio = MapeadorDeMunicipio.ObtenhaMunicipio(UtilidadesDePersistencia.GetValorLong(Leitor, "IDMUNICIPIO"))
            Pessoa.Endereco = Endereco
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "SITE") Then
            Pessoa.Site = UtilidadesDePersistencia.GetValorString(Leitor, "SITE")
        End If

        ObtenhaTelefones(Pessoa)
    End Sub

    Private Sub ObtenhaTelefones(ByVal Pessoa As IPessoa)
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper

        SQL = New StringBuilder

        DBHelper = ServerUtils.criarNovoDbHelper

        SQL.Append("SELECT IDPESSOA, DDD, NUMERO, TIPO, INDICE FROM NCL_PESSOATELEFONE")
        SQL.Append(" WHERE IDPESSOA = " & Pessoa.ID.Value.ToString)
        SQL.Append(" ORDER BY INDICE")

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read
                Dim Telefone As ITelefone

                Telefone = FabricaGenerica.GetInstancia.CrieObjeto(Of ITelefone)()
                Telefone.DDD = UtilidadesDePersistencia.getValorShort(Leitor, "DDD")
                Telefone.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")
                Telefone.Tipo = TipoDeTelefone.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))
                Pessoa.AdicioneTelefone(Telefone)
            End While
        End Using
    End Sub

End Class