Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Fabricas

Public Class MapeadorDeVisibilidadePorEmpresa
    Implements IMapeadorDeVisibilidadePorEmpresa

    Public Sub Inserir(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa)) Implements IMapeadorDeVisibilidadePorEmpresa.Inserir
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        For Each Empresa As IEmpresa In EmpresasVisiveis
            Dim SQL As New StringBuilder
            SQL.Append("INSERT INTO NCL_VISOPEEMP (IDOPERADOR, IDEMPRESA)")
            SQL.Append(" VALUES ( " & IDOperador)
            SQL.Append("," & Empresa.Pessoa.ID & ")")
            DBHelper.ExecuteNonQuery(SQL.ToString)
        Next
    End Sub

    Public Sub Modifique(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa)) Implements IMapeadorDeVisibilidadePorEmpresa.Modifique
        Remova(IDOperador)
        Inserir(IDOperador, EmpresasVisiveis)
    End Sub

    Public Function Obtenha(IDOperador As Long) As IList(Of IEmpresa) Implements IMapeadorDeVisibilidadePorEmpresa.Obtenha
        Dim SQL As New StringBuilder

        SQL.Append("SELECT IDEMPRESA, DTATIVACAO FROM NCL_VISOPEEMP, NCL_EMPRESA WHERE IDEMPRESA= IDPESSOA AND IDOPERADOR = " & IDOperador)

        Dim DBHelper As IDBHelper
        Dim Empresas As IList(Of IEmpresa) = New List(Of IEmpresa)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read
                    Dim Empresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IEmpresa)(New Object() {FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDEMPRESA"))})
                    Empresa.DataDaAtivacao = UtilidadesDePersistencia.getValorDate(Leitor, "DTATIVACAO").Value

                    Empresas.Add(Empresa)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Empresas

    End Function

    Public Sub Remova(IDOperador As Long) Implements IMapeadorDeVisibilidadePorEmpresa.Remova
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_VISOPEEMP WHERE IDOPERADOR = " & IDOperador, False)
    End Sub

End Class