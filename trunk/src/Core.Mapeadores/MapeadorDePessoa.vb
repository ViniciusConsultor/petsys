Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

Public MustInherit Class MapeadorDePessoa(Of T As IPessoa)
    Implements IMapeadorDePessoa(Of T)

    Protected MustOverride Sub Insira(ByVal Pessoa As T)
    Protected MustOverride Sub Atualize(ByVal Pessoa As T)
    Protected MustOverride Sub Remova(ByVal Pessoa As T)
    Protected MustOverride Function Carregue(ByVal Id As Long) As T
    Protected MustOverride Function CarreguePorNome(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of T)

    Public Function Inserir(ByVal Pessoa As T) As Long Implements IMapeadorDePessoa(Of T).Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Pessoa.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_PESSOA (ID, NOME, TIPO, ENDEMAIL,")
        SQL.Append(" LOGRADOURO, COMPLEMENTO, IDMUNICIPIO, CEP,")
        SQL.Append(" BAIRRO)")
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
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Complemento), "', "))
            SQL.Append(String.Concat(Pessoa.Endereco.Municipio.ID.Value, ", "))
            SQL.Append(String.Concat(Pessoa.Endereco.CEP.Numero.Value, ", "))
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Bairro), "')"))
        Else
            SQL.Append("NULL, NULL, NULL, NULL, NULL)")
        End If

        DBHelper.ExecuteNonQuery(SQL.ToString)
        Me.Insira(Pessoa)
        Return Pessoa.ID.Value
    End Function

    Public Function Obtenha(ByVal ID As Long) As T Implements IMapeadorDePessoa(Of T).Obtenha
        Return Me.Carregue(ID)
    End Function

    Public Sub Atualizar(ByVal Pessoa As T) Implements IMapeadorDePessoa(Of T).Atualizar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper

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
            SQL.Append(String.Concat("COMPLEMENTO = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Complemento), "', "))
            SQL.Append(String.Concat("IDMUNICIPIO = ", Pessoa.Endereco.Municipio.ID.Value, ", "))
            SQL.Append(String.Concat("CEP = ", Pessoa.Endereco.CEP.Numero.Value, ", "))
            SQL.Append(String.Concat("BAIRRO = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.Endereco.Bairro), "'"))
        Else
            SQL.Append("LOGRADOURO = NULL, COMPLEMENTO = NULL, IDMUNICIPIO = NULL, CEP = NULL, BAIRRO = NULL")
        End If

        SQL.Append(String.Concat(" WHERE ID = ", Pessoa.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Remover(ByVal Pessoa As T) Implements IMapeadorDePessoa(Of T).Remover
        Dim SQL As String
        Dim DBHelper As IDBHelper

        Me.Remova(Pessoa)
        DBHelper = ServerUtils.getDBHelper
        SQL = String.Concat("DELETE FROM NCL_PESSOA WHERE ID = ", Pessoa.ID.Value.ToString)
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
    End Sub

End Class