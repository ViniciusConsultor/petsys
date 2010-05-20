Imports T13.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports T13.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

Public Class MapeadorDeServicosPrestados
    Implements IMapeadorDeServicosPrestados

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeServicosPrestados.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append("DELETE FROM T13_SERVICOSPRESTADOS")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByVal Servico As IServicoPrestado) Implements IMapeadorDeServicosPrestados.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Servico.ID = GeradorDeID.getInstancia.getProximoID
        Sql.Append("INSERT INTO T13_SERVICOSPRESTADOS (ID, NOME, VALOR, CARACTEDESCONTO) ")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Servico.ID.ToString, ", "))
        Sql.Append(String.Concat("'", Servico.Nome, "', "))

        If Not Servico.Valor Is Nothing Then
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Servico.Valor.Value), ", "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat("'", IIf(Servico.CaracterizaDesconto, "S", "F"), "')"))

        DBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Sevico As IServicoPrestado) Implements IMapeadorDeServicosPrestados.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append("UPDATE T13_SERVICOSPRESTADOS SET ")
        Sql.Append(String.Concat("NOME = '", Sevico.Nome, "', "))

        If Not Sevico.Valor Is Nothing Then
            Sql.Append(String.Concat("VALOR = ", UtilidadesDePersistencia.TPVd(Sevico.Valor.Value), ", "))
        Else
            Sql.Append("VALOR = NULL, ")
        End If

        Sql.Append(String.Concat("CARACTEDESCONTO = '", IIf(Sevico.CaracterizaDesconto, "S", "F"), "'"))
        Sql.Append(String.Concat(" WHERE ID = ", Sevico.ID.Value.ToString))

        DBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaServico(ByVal ID As Long) As IServicoPrestado Implements IMapeadorDeServicosPrestados.ObtenhaServico
        Dim Sql As New StringBuilder
        Dim ListaDeServicos As IList(Of IServicoPrestado)
        Dim ServicoPrestado As IServicoPrestado = Nothing

        Sql.Append("SELECT ID, NOME, VALOR, CARACTEDESCONTO FROM T13_SERVICOSPRESTADOS WHERE ")
        Sql.Append(String.Concat("ID = ", ID.ToString))

        ListaDeServicos = ObtenhaServicosPrestados(Sql.ToString)

        If Not ListaDeServicos.Count = 0 Then
            ServicoPrestado = ListaDeServicos(0)
        End If

        Return ServicoPrestado
    End Function

    Public Function ObtenhaServicoPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IServicoPrestado) Implements IMapeadorDeServicosPrestados.ObtenhaServicoPorNomeComoFiltro
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, VALOR, CARACTEDESCONTO FROM T13_SERVICOSPRESTADOS WHERE ")
        Sql.Append(String.Concat("NOME LIKE '", Nome, "%'"))

        Return ObtenhaServicosPrestados(Sql.ToString)
    End Function

    Private Function ObtenhaServicosPrestados(ByVal Sql As String) As IList(Of IServicoPrestado)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim ServicosPrestados As IList(Of IServicoPrestado)
        Dim ServicoPrestado As IServicoPrestado

        ServicosPrestados = New List(Of IServicoPrestado)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                ServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoPrestado)()
                ServicoPrestado.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                ServicoPrestado.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                ServicoPrestado.Valor = UtilidadesDePersistencia.getValorDouble(Leitor, "VALOR")
                ServicoPrestado.CaracterizaDesconto = UtilidadesDePersistencia.GetValorBooleano(Leitor, "CARACTEDESCONTO")

                ServicosPrestados.Add(ServicoPrestado)
            End While
        End Using

        Return ServicosPrestados
    End Function

End Class
