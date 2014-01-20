Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad

Public Class MapeadorDeAtendimento
    Implements IMapeadorDeAtendimento

    Public Sub Excluir(ByVal ID As Long) Implements Interfaces.Mapeadores.IMapeadorDeAtendimento.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_ATENDANIMAL")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Insira(ByVal Atendimento As IAtendimento) Implements IMapeadorDeAtendimento.Insira
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Atendimento.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO PET_ATENDANIMAL")
        Sql.Append(" (ID, IDANIMAL, IDVETERINARIO, DATAEHORA, DATAEHORARETORNO,")
        Sql.Append(" QUEIXA, SINCLINICO, PROGNOSTICO, TRATAMENTO, PESODOANIMAL)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Atendimento.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.Animal.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.Veterinario.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.DataEHoraDoAtendimento.ToString("yyyyMMddhhmmss"), ", "))

        If Atendimento.DataEHoraDoRetorno.HasValue Then
            Sql.Append(String.Concat(Atendimento.DataEHoraDoRetorno.Value.ToString("yyyyMMdd"), ", "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Atendimento.Queixa) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atendimento.Queixa), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Atendimento.SinaisClinicos) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atendimento.SinaisClinicos), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Atendimento.Prognostico) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atendimento.Prognostico), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Atendimento.Tratamento) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Atendimento.Tratamento), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Atendimento.Peso.HasValue Then
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Atendimento.Peso.Value), ")"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)

        InsiraVacinasDoAtendimento(DBHelper, Atendimento)
    End Sub

    Private Sub InsiraVacinasDoAtendimento(ByVal DBHelper As IDBHelper, ByVal Atendimento As IAtendimento)
        Dim Sql As New StringBuilder
        Dim MapeadorDeVacina As IMapeadorDeVacina

        MapeadorDeVacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

        For Each Vacina As IVacina In Atendimento.Vacinas
            MapeadorDeVacina.Inserir(Vacina)
            Sql.Append("INSERT PET_ATENDVAC (IDATENDIMENTO, IDVACINA) VALUES (")
            Sql.Append(Atendimento.ID.Value.ToString & Vacina.ID.Value.ToString & ")")
            DBHelper.ExecuteNonQuery(Sql.ToString)
            Sql = New StringBuilder
        Next
    End Sub

    Private Sub InsiraVermifugosDoAtendimento(ByVal DBHelper As IDBHelper, ByVal Atendimento As IAtendimento)
        Dim Sql As New StringBuilder
        Dim MapeadorDeVermifugos As IMapeadorDeVermifugos

        MapeadorDeVermifugos = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

        For Each Vermifugo As IVermifugo In Atendimento.Vermifugos
            MapeadorDeVermifugos.Inserir(Vermifugo)
            Sql.Append("INSERT PET_ATENDVERM (IDATENDIMENTO, IDVERMIFUGO) VALUES (")
            Sql.Append(Atendimento.ID.Value.ToString & Vermifugo.ID.Value.ToString & ")")
            DBHelper.ExecuteNonQuery(Sql.ToString)
            Sql = New StringBuilder
        Next
    End Sub

    Public Sub Modificar(ByVal Atendimento As IAtendimento) Implements IMapeadorDeAtendimento.Modificar

    End Sub

    Public Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimento Implements IMapeadorDeAtendimento.ObtenhaAtendimento
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, IDVETERINARIO, DATAEHORA, DATAEHORARETORNO, ")
        Sql.Append("QUEIXA, SINCLINICO, PROGNOSTICO, TRATAMENTO, PESODOANIMAL ")
        Sql.Append("FROM PET_ATENDANIMAL ")
        Sql.Append(String.Concat("WHERE ID = ", ID.ToString))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Atendimento As IAtendimento = Nothing

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Atendimento = MontaObjeto(Leitor)
            End If
        End Using

        Return Atendimento
    End Function

    Public Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimento) Implements IMapeadorDeAtendimento.ObtenhaAtendimentos
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, IDVETERINARIO, DATAEHORA, DATAEHORARETORNO, ")
        Sql.Append("QUEIXA, SINCLINICO, PROGNOSTICO, TRATAMENTO, PESODOANIMAL ")
        Sql.Append("FROM PET_ATENDANIMAL ")
        Sql.Append(String.Concat("WHERE IDANIMAL = ", Animal.ID.Value))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Atendimentos As IList(Of IAtendimento) = New List(Of IAtendimento)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Atendimentos.Add(MontaObjeto(Leitor))
            End While
        End Using

        Return Atendimentos
    End Function

    Private Function MontaObjeto(ByVal Leitor As IDataReader) As IAtendimento
        Dim Atendimento As IAtendimento

        Atendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IAtendimento)()
        Atendimento.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Atendimento.DataEHoraDoAtendimento = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATAEHORA").Value

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DATAEHORARETORNO") Then
            Atendimento.DataEHoraDoRetorno = UtilidadesDePersistencia.getValorDate(Leitor, "DATAEHORARETORNO").Value
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "QUEIXA") Then
            Atendimento.Queixa = UtilidadesDePersistencia.GetValorString(Leitor, "QUEIXA")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "SINCLINICO") Then
            Atendimento.SinaisClinicos = UtilidadesDePersistencia.GetValorString(Leitor, "SINCLINICO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "PROGNOSTICO") Then
            Atendimento.Prognostico = UtilidadesDePersistencia.GetValorString(Leitor, "PROGNOSTICO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "TRATAMENTO") Then
            Atendimento.Tratamento = UtilidadesDePersistencia.GetValorString(Leitor, "TRATAMENTO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "PESODOANIMAL") Then
            Atendimento.Peso = UtilidadesDePersistencia.getValorDouble(Leitor, "PESODOANIMAL")
        End If

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDVETERINARIO"))
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Atendimento.Veterinario = VeterinarioLazyLoad

        Atendimento.Animal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDANIMAL"))

        Atendimento.Vacinas = Me.ObtenhaVacinasDoAnimalNoAtendimento(Atendimento.ID.Value)
        Atendimento.Vermifugos = Me.ObtenhaVermifugosDoAnimalNoAtendimento(Atendimento.ID.Value)

        Return Atendimento
    End Function

    Private Function ObtenhaVacinasDoAnimalNoAtendimento(ByVal IDAtendimento As Long) As IList(Of IVacina)
        Dim Sql As New StringBuilder

        Sql.Append("SELECT IDATENDIMENTO, IDVACINA ")
        Sql.Append("FROM PET_ATENDVAC ")
        Sql.Append(String.Concat("WHERE IDATENDIMENTO = ", IDAtendimento))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim IDsVacina As IList(Of Long) = New List(Of Long)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                IDsVacina.Add(UtilidadesDePersistencia.GetValorLong(Leitor, "IDATENDIMENTO"))
            End While
        End Using

        If IDsVacina.Count > 0 Then
            Dim MapeadorDeVacina As IMapeadorDeVacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

            Return MapeadorDeVacina.ObtenhaVacinas(IDsVacina)
        End If

        Return New List(Of IVacina)
    End Function

    Private Function ObtenhaVermifugosDoAnimalNoAtendimento(ByVal IDAtendimento As Long) As IList(Of IVermifugo)
        Dim Sql As New StringBuilder

        Sql.Append("SELECT IDATENDIMENTO, IDVERMIFUGO ")
        Sql.Append("FROM PET_ATENDVERM ")
        Sql.Append(String.Concat("WHERE IDATENDIMENTO = ", IDAtendimento))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim IDsVermifugos As IList(Of Long) = New List(Of Long)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                IDsVermifugos.Add(UtilidadesDePersistencia.GetValorLong(Leitor, "IDATENDIMENTO"))
            End While
        End Using

        If IDsVermifugos.Count > 0 Then
            Dim MapeadorDeVermifugos As IMapeadorDeVermifugos = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

            Return MapeadorDeVermifugos.ObtenhaVermifugos(IDsVermifugos)
        End If

        Return New List(Of IVermifugo)
    End Function

End Class
