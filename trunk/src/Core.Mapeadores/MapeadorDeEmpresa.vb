Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Fabricas

Public Class MapeadorDeEmpresa
    Implements IMapeadorDeEmpresa
    
    Public Sub Insira(ByVal Empresa As IEmpresa) Implements IMapeadorDeEmpresa.Insira
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_EMPRESA (")
        Sql.Append("IDPESSOA, DTATIVACAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Empresa.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Empresa.DataDaAtivacao.ToString("yyyyMMdd"), ") "))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(Empresa As IEmpresa) Implements IMapeadorDeEmpresa.Modificar

    End Sub
    
    Public Sub Remover(ID As Long) Implements IMapeadorDeEmpresa.Remover
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery("DELETE FROM NCL_EMPRESA WHERE IDPESSOA = " & ID.ToString())
    End Sub

    Public Function Obtenha(ID As Long) As Compartilhados.Interfaces.Core.Negocio.IEmpresa Implements IMapeadorDeEmpresa.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Empresa As IEmpresa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME,")
        Sql.Append("NCL_EMPRESA.IDPESSOA, NCL_EMPRESA.DTATIVACAO ")
        Sql.Append("FROM NCL_PESSOA, NCL_EMPRESA ")
        Sql.Append("WHERE NCL_EMPRESA.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND IDPESSOA = " & ID)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then

                    Empresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IEmpresa)(New Object() {FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))})
                    Empresa.DataDaAtivacao = UtilidadesDePersistencia.getValorDate(Leitor, "DTATIVACAO").Value
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Empresa
    End Function

    Public Function ObtenhaPorNome(Filtro As String, Quantidade As Integer) As IList(Of IEmpresa) Implements IMapeadorDeEmpresa.ObtenhaPorNome
        Dim Sql As New StringBuilder

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME,")
        Sql.Append("NCL_EMPRESA.IDPESSOA, NCL_EMPRESA.DTATIVACAO ")
        Sql.Append("FROM NCL_PESSOA, NCL_EMPRESA ")
        Sql.Append("WHERE NCL_EMPRESA.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append(" AND TIPO = " & TipoDePessoa.Juridica.ID)

        If Not String.IsNullOrEmpty(Filtro) Then
            Sql.Append(String.Concat(" AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Filtro), "%'"))
        End If

        Dim DBHelper As IDBHelper
        Dim Empresas As IList(Of IEmpresa) = New List(Of IEmpresa)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, Quantidade)
            Try
                While Leitor.Read
                    Dim Empresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IEmpresa)(New Object() {FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))})
                    Empresa.DataDaAtivacao = UtilidadesDePersistencia.getValorDate(Leitor, "DTATIVACAO").Value

                    Empresas.Add(Empresa)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Empresas
    End Function

End Class