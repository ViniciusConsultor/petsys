Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeCliente
    Implements IMapeadorDeCliente

    Public Sub Inserir(ByVal Cliente As ICliente) Implements IMapeadorDeCliente.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_CLIENTE (")
        Sql.Append("IDPESSOA, TIPOPESSOA)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Cliente.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Cliente.Pessoa.Tipo.ID, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Overloads Function Obtenha(ByVal Pessoa As IPessoa) As ICliente Implements IMapeadorDeCliente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA FROM NCL_CLIENTE WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString, " AND "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Pessoa.Tipo.ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICliente)(New Object() {Pessoa})
            End If
        End Using

        Return Cliente
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeCliente.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_CLIENTE")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente) Implements IMapeadorDeCliente.ObtenhaPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing
        Dim Pessoa As IPessoa = Nothing
        Dim Tipo As TipoDePessoa
        Dim Clientes As IList(Of ICliente) = New List(Of ICliente)

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,")
        Sql.Append("NCL_CLIENTE.IDPESSOA, NCL_CLIENTE.TIPOPESSOA ")
        Sql.Append("FROM NCL_PESSOA, NCL_CLIENTE ")
        Sql.Append("WHERE NCL_CLIENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CLIENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, QuantidadeMaximaDeRegistros)
            While Leitor.Read
                Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                If Tipo.Equals(TipoDePessoa.Fisica) Then
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Else
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                End If

                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICliente)(New Object() {Pessoa})

                Clientes.Add(Cliente)
            End While
        End Using

        Return Clientes
    End Function

    Public Overloads Function Obtenha(ByVal ID As Long) As ICliente Implements IMapeadorDeCliente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,")
        Sql.Append("NCL_CLIENTE.IDPESSOA, NCL_CLIENTE.TIPOPESSOA ")
        Sql.Append("FROM NCL_PESSOA, NCL_CLIENTE ")
        Sql.Append("WHERE NCL_CLIENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CLIENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")
        Sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                If Tipo.Equals(TipoDePessoa.Fisica) Then
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Else
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                End If

                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICliente)(New Object() {Pessoa})
            End If
        End Using

        Return Cliente
    End Function

End Class