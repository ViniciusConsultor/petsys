Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad

Public Class MapeadorDeVacina
    Implements IMapeadorDeVacina

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeVacina.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_VACINA")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByRef Vacina As IVacina) Implements IMapeadorDeVacina.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Vacina.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO PET_VACINA (")
        Sql.Append("ID, IDANIMAL, NOME, DATA, OBSERVACAO, ")
        Sql.Append("REVACINAREM, IDVETERINARIORESP) ")
        Sql.Append("VALUES (")
        Sql.Append(String.Concat(Vacina.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Vacina.AnimalQueRecebeu.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Vacina.Nome), "', "))
        Sql.Append(String.Concat(Vacina.DataDaVacinacao.ToString("yyyyMMdd"), ", "))

        If Not String.IsNullOrEmpty(Vacina.Observacao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Vacina.Observacao), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Vacina.RevacinarEm.HasValue Then
            Sql.Append(String.Concat(Vacina.RevacinarEm.Value.ToString("yyyyMMdd"), ", "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat(Vacina.VeterinarioQueAplicou.Pessoa.ID, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Vacina As IVacina) Implements IMapeadorDeVacina.Modificar

    End Sub

    Public Function ObtenhaVacina(ByVal ID As Long) As IVacina Implements IMapeadorDeVacina.ObtenhaVacina
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, REVACINAREM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VACINA ")
        Sql.Append(String.Concat("WHERE ID = ", ID.ToString))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vacina As IVacina = Nothing

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Vacina = Me.MontaObjetoVacina(Leitor)
            End If
        End Using

        Return Vacina
    End Function

    Public Function ObtenhaVacinasDoAnimal(ByVal IdAnimal As Long) As IList(Of IVacina) Implements IMapeadorDeVacina.ObtenhaVacinasDoAnimal
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, REVACINAREM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VACINA ")
        Sql.Append(String.Concat("WHERE IDANIMAL = ", IdAnimal.ToString))
        Sql.Append(" ORDER BY DATA")

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vacinas As IList(Of IVacina) = New List(Of IVacina)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Vacinas.Add(Me.MontaObjetoVacina(Leitor))
            End While
        End Using

        Return Vacinas
    End Function

    Private Function MontaObjetoVacina(ByVal Leitor As IDataReader) As IVacina
        Dim Vacina As IVacina

        Vacina = FabricaGenerica.GetInstancia.CrieObjeto(Of IVacina)()
        Vacina.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDANIMAL"))
        Vacina.DataDaVacinacao = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value
        Vacina.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Vacina.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACAO") Then
            Vacina.Observacao = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACAO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "REVACINAREM") Then
            Vacina.RevacinarEm = UtilidadesDePersistencia.getValorDate(Leitor, "REVACINAREM").Value
        End If

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDVETERINARIO"))
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Vacina.VeterinarioQueAplicou = VeterinarioLazyLoad

        Return Vacina
    End Function

    Public Function ObtenhaVacinas(ByVal IDs As IList(Of Long)) As IList(Of IVacina) Implements IMapeadorDeVacina.ObtenhaVacinas
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, REVACINAREM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VACINA ")
        Sql.Append("WHERE ID IN (")

        Dim IDsSTR As New StringBuilder

        For Each ID As Long In IDs
            IDsSTR.Append(String.Concat(ID.ToString, ","))
        Next

        IDsSTR = IDsSTR.Remove(IDsSTR.Length - 1, 1)

        Sql.Append(String.Concat(IDsSTR.ToString, ")"))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vacinas As IList(Of IVacina) = New List(Of IVacina)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Vacinas.Add(Me.MontaObjetoVacina(Leitor))
            End While
        End Using

        Return Vacinas
    End Function

End Class