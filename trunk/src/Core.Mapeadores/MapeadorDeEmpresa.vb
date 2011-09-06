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

    Public Function Obtenha() As IEmpresa Implements IMapeadorDeEmpresa.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Empresa As IEmpresa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME,")
        Sql.Append("NCL_EMPRESA.IDPESSOA, NCL_EMPRESA.DTATIVACAO ")
        Sql.Append("FROM NCL_PESSOA, NCL_EMPRESA ")
        Sql.Append("WHERE NCL_EMPRESA.IDPESSOA = NCL_PESSOA.ID ")

        Dim Pessoa As IPessoa

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then

                    Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
                    Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                    Empresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IEmpresa)(New Object() {Pessoa})
                    Empresa.DataDaAtivacao = UtilidadesDePersistencia.getValorDate(Leitor, "DTATIVACAO").Value
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Empresa
    End Function

End Class
