Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public MustInherit Class MapeadorDePessoa(Of T As IPessoa)
    Implements IMapeadorDePessoa(Of T)

    Protected MustOverride Sub Insira(ByVal Pessoa As T)
    Protected MustOverride Sub Atualize(ByVal Pessoa As T)
    Protected MustOverride Sub Remova(ByVal ID As Long)
    Protected MustOverride Function Carregue(ByVal Id As Long) As T
    Protected MustOverride Function CarreguePorNome(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer, NivelDeRetardo As Integer) As IList(Of T)

    Public Function Inserir(ByVal Pessoa As T) As Long Implements IMapeadorDePessoa(Of T).Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Pessoa.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_PESSOA (ID, NOME, TIPO, ENDEMAIL,")
        SQL.Append(" SITE, IDBANCO, IDAGENCIA, CNTACORRENTE, TIPOCNTACORRENTE)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Pessoa.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Nome), "', "))
        SQL.Append(String.Concat("'", Pessoa.Tipo.ID.ToString, "', "))

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.EnderecoDeEmail.ToString), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Site) Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Site), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not Pessoa.DadoBancario Is Nothing Then
            SQL.Append(String.Concat(Pessoa.DadoBancario.Agencia.Banco.Pessoa.ID.Value.ToString, ", "))
            SQL.Append(String.Concat(Pessoa.DadoBancario.Agencia.Pessoa.ID.Value.ToString, ", "))

            If String.IsNullOrEmpty(Pessoa.DadoBancario.Conta.Numero) Then
                SQL.Append("NULL, ")
            Else
                SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.DadoBancario.Conta.Numero), "', "))
            End If

            If Pessoa.DadoBancario.Conta.Tipo.HasValue Then
                SQL.Append(String.Concat(Pessoa.DadoBancario.Conta.Tipo.Value, ")"))
            Else
                SQL.Append("NULL)")
            End If
        Else
            SQL.Append("NULL, NULL, NULL, NULL)")
        End If

        DBHelper.ExecuteNonQuery(SQL.ToString)

        InsiraTelefones(Pessoa)
        InsiraEnderecos(Pessoa)
        Me.Insira(Pessoa)
        Return Pessoa.ID.Value
    End Function

    Private Sub InsiraEnderecos(ByVal Pessoa As IPessoa)
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        RemovaEnderecos(Pessoa.ID.Value)

        If Not Pessoa.Enderecos Is Nothing AndAlso Not Pessoa.Enderecos.Count = 0 Then
            For Each Endereco As IEndereco In Pessoa.Enderecos
                SQL = New StringBuilder
                SQL.Append("INSERT INTO NCL_PESSOAENDERECO (IDPESSOA, TIPO, INDICE, LOGRADOURO, COMPLEMENTO, IDMUNICIPIO, CEP, BAIRRO)")
                SQL.Append(" VALUES ( ")
                SQL.Append(String.Concat(Pessoa.ID, ", "))
                SQL.Append(String.Concat(Endereco.TipoDeEndereco.ID, ", "))
                SQL.Append(String.Concat(Pessoa.Enderecos.IndexOf(Endereco), ", "))

                If String.IsNullOrEmpty(Endereco.Logradouro) Then
                    SQL.Append("NULL, ")
                Else
                    SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Endereco.Logradouro), "', "))
                End If

                If String.IsNullOrEmpty(Endereco.Complemento) Then
                    SQL.Append("NULL, ")
                Else
                    SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Endereco.Complemento), "', "))
                End If

                If Endereco.Municipio Is Nothing Then
                    SQL.Append("NULL, ")
                Else
                    SQL.Append(String.Concat(Endereco.Municipio.ID, ", "))
                End If

                If Endereco.CEP Is Nothing Then
                    SQL.Append("NULL, ")
                Else
                    SQL.Append(String.Concat(Endereco.CEP.Numero.Value, ", "))
                End If

                If String.IsNullOrEmpty(Endereco.Bairro) Then
                    SQL.Append("NULL, ")
                Else
                    SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Endereco.Bairro), "') "))
                End If

                DBHelper.ExecuteNonQuery(SQL.ToString)
            Next
        End If
    End Sub

    Private Sub RemovaEnderecos(ByVal IDPessoa As Long)
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_PESSOAENDERECO WHERE IDPESSOA = " & IDPessoa)
    End Sub

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
        Me.InsiraEnderecos(Pessoa)
        Me.Atualize(Pessoa)
        DBHelper = ServerUtils.getDBHelper
        SQL.Append(String.Concat("UPDATE NCL_PESSOA SET NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Nome), "', "))

        SQL.Append(" ENDEMAIL = ")

        If Not Pessoa.EnderecoDeEmail Is Nothing Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.EnderecoDeEmail.ToString), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Pessoa.Site) Then
            SQL.Append(String.Concat("SITE = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Site), "',"))
        Else
            SQL.Append("SITE = NULL,")
        End If

        If Not Pessoa.DadoBancario Is Nothing Then
            SQL.Append(String.Concat("IDBANCO  = ", Pessoa.DadoBancario.Agencia.Banco.Pessoa.ID.Value.ToString, ", "))
            SQL.Append(String.Concat("IDAGENCIA = ", Pessoa.DadoBancario.Agencia.Pessoa.ID.Value.ToString, ", "))

            If String.IsNullOrEmpty(Pessoa.DadoBancario.Conta.Numero) Then
                SQL.Append("CNTACORRENTE = NULL, ")
            Else
                SQL.Append(String.Concat("CNTACORRENTE = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.DadoBancario.Conta.Numero), "', "))
            End If

            If Pessoa.DadoBancario.Conta.Tipo.HasValue Then
                SQL.Append(String.Concat("TIPOCNTACORRENTE = ", Pessoa.DadoBancario.Conta.Tipo.Value))
            Else
                SQL.Append("TIPOCNTACORRENTE = NULL")
            End If
        Else
            SQL.Append("IDBANCO = NULL, IDAGENCIA = NULL, CNTACORRENTE = NULL, TIPOCNTACORRENTE = NULL")
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
                                                              ByVal QuantidadeMaximaDeRegistros As Integer,
                                                              NivelDeRetardo As Integer) As IList(Of T) Implements IMapeadorDePessoa(Of T).ObtenhaPessoasPorNomeComoFiltro
        Return Me.CarreguePorNome(Nome, _
                                  QuantidadeMaximaDeRegistros, NivelDeRetardo)
    End Function

    Protected Sub PreencheDados(ByRef Pessoa As T, ByVal Leitor As IDataReader, NivelDeRetardo As Integer)
        Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "ENDEMAIL") Then
            Pessoa.EnderecoDeEmail = UtilidadesDePersistencia.GetValorString(Leitor, "ENDEMAIL")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "SITE") Then
            Pessoa.Site = UtilidadesDePersistencia.GetValorString(Leitor, "SITE")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDAGENCIA") Then
            Dim PessoaBanco As IPessoa
            Dim Banco As IBanco
            Dim Agencia As IAgencia
            Dim PessoaAgencia As IPessoa

            PessoaBanco = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDBANCO"))
            Banco = FabricaGenerica.GetInstancia.CrieObjeto(Of IBanco)(New Object() {PessoaBanco})
            'TODO: BUSCAR O NUMERO DO BANCO

            PessoaAgencia = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDAGENCIA"))
            Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)(New Object() {PessoaAgencia})
            Agencia.Banco = Banco
            'TODO: BUSCAR O NUMERO DA AGENCIA

            Dim DadosBancarios = FabricaGenerica.GetInstancia.CrieObjeto(Of IDadoBancario)()
            DadosBancarios.Agencia = Agencia

            Dim Conta As IContaBancaria

            Conta = FabricaGenerica.GetInstancia.CrieObjeto(Of IContaBancaria)()
            Conta.Numero = UtilidadesDePersistencia.GetValorString(Leitor, "CNTACORRENTE")

            If Not UtilidadesDePersistencia.EhNulo(Leitor, "TIPOCNTACORRENTE") Then
                Conta.Tipo = UtilidadesDePersistencia.getValorInteger(Leitor, "TIPOCNTACORRENTE")
            End If

            DadosBancarios.Conta = Conta
            Pessoa.DadoBancario = DadosBancarios
        End If

        If (NivelDeRetardo > 0) Then
            ObtenhaTelefones(Pessoa)
            ObtenhaEnderecos(Pessoa)
        End If
    End Sub

    Private Sub ObtenhaEnderecos(ByVal Pessoa As IPessoa)
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper

        SQL = New StringBuilder

        DBHelper = ServerUtils.criarNovoDbHelper

        SQL.Append("SELECT IDPESSOA, TIPO, INDICE, LOGRADOURO, COMPLEMENTO, IDMUNICIPIO, CEP, BAIRRO, ID, NOME FROM NCL_PESSOAENDERECO, NCL_TIPO_ENDERECO")
        SQL.Append(" WHERE IDPESSOA = " & Pessoa.ID.Value.ToString)
        SQL.Append(" AND TIPO = NCL_TIPO_ENDERECO.ID ")
        SQL.Append(" ORDER BY INDICE")

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read
                    Dim Endereco As IEndereco
                    Dim MapeadorDeMunicipio As IMapeadorDeMunicipio
                    Dim Tipo As ITipoDeEndereco = FabricaGenerica.GetInstancia.CrieObjeto(Of ITipoDeEndereco)()

                    Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()

                    Tipo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Tipo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                    Endereco.TipoDeEndereco = Tipo

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "BAIRRO") Then
                        Endereco.Bairro = UtilidadesDePersistencia.GetValorString(Leitor, "BAIRRO")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "CEP") Then
                        Endereco.CEP = New CEP(UtilidadesDePersistencia.GetValorLong(Leitor, "CEP"))
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "COMPLEMENTO") Then
                        Endereco.Complemento = UtilidadesDePersistencia.GetValorString(Leitor, "COMPLEMENTO")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOGRADOURO") Then
                        Endereco.Logradouro = UtilidadesDePersistencia.GetValorString(Leitor, "LOGRADOURO")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDMUNICIPIO") Then
                        MapeadorDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()
                        Endereco.Municipio = MapeadorDeMunicipio.ObtenhaMunicipio(UtilidadesDePersistencia.GetValorLong(Leitor, "IDMUNICIPIO"))
                    End If

                    Pessoa.AdicioneEndereco(Endereco)
                End While
            Finally
                Leitor.Close()
            End Try

        End Using

        
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
            Try
                While Leitor.Read
                    Dim Telefone As ITelefone

                    Telefone = FabricaGenerica.GetInstancia.CrieObjeto(Of ITelefone)()
                    Telefone.DDD = UtilidadesDePersistencia.getValorShort(Leitor, "DDD")
                    Telefone.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")
                    Telefone.Tipo = TipoDeTelefone.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))
                    Pessoa.AdicioneTelefone(Telefone)
                End While
            Finally
                Leitor.Close()
            End Try
            
        End Using
    End Sub

End Class