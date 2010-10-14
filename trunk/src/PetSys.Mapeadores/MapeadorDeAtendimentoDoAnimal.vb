Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio.LazyLoad

Public Class MapeadorDeAtendimentoDoAnimal
    Implements IMapeadorDeAtendimentoDoAnimal

    Public Sub Insira(ByVal Atendimento As IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.Insira
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Atendimento.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO PET_ATENDANIMAL")
        Sql.Append(" (ID, IDANIMAL, IDVETERINARIO, DATAEHORA)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Atendimento.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.Animal.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.Veterinario.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Atendimento.DataEHoraDoAtendimento.ToString("yyyyMMddhhmmss"), ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Atendimento As IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.Modificar

    End Sub

    Public Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimentoDoAnimal Implements IMapeadorDeAtendimentoDoAnimal.ObtenhaAtendimento
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, IDVETERINARIO, DATAEHORA ")
        Sql.Append("FROM PET_ATENDANIMAL ")
        Sql.Append(String.Concat("WHERE ID = ", ID.ToString))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Atendimento As IAtendimentoDoAnimal = Nothing

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Atendimento = MontaObjeto(Leitor)
            End If
        End Using

        Return Atendimento
    End Function

    Public Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimentoDoAnimal) Implements IMapeadorDeAtendimentoDoAnimal.ObtenhaAtendimentos
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, IDVETERINARIO, DATAEHORA ")
        Sql.Append("FROM PET_ATENDANIMAL ")
        Sql.Append(String.Concat("WHERE IDANIMAL = ", Animal.ID.Value))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Atendimentos As IList(Of IAtendimentoDoAnimal) = New List(Of IAtendimentoDoAnimal)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Atendimentos.Add(MontaObjeto(Leitor))
            End While
        End Using

        Return Atendimentos
    End Function

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeAtendimentoDoAnimal.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_ATENDANIMAL")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function MontaObjeto(ByVal Leitor As IDataReader) As IAtendimentoDoAnimal
        Dim Atendimento As IAtendimentoDoAnimal

        Atendimento = FabricaGenerica.GetInstancia.CrieObjeto(Of IAtendimentoDoAnimal)()
        Atendimento.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Atendimento.DataEHoraDoAtendimento = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATAEHORA").Value

        Dim PessoaLazyLoad As IPessoaFisicaLazyLoad
        Dim VeterinarioLazyLoad As IVeterinarioLazyLoad

        PessoaLazyLoad = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDVETERINARIO"))
        VeterinarioLazyLoad = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinarioLazyLoad)(New Object() {PessoaLazyLoad})
        Atendimento.Veterinario = VeterinarioLazyLoad

        Atendimento.Animal = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimalLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDANIMAL"))

        Return Atendimento
    End Function

End Class