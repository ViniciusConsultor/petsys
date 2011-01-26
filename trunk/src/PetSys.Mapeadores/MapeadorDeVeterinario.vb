Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class MapeadorDeVeterinario
    Implements IMapeadorDeVeterinario

    Public Sub Inserir(ByVal Veterinario As IVeterinario) Implements IMapeadorDeVeterinario.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO PET_VETERINARIO (")
        Sql.Append("IDPESSOA, CRMV, UF)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Veterinario.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Veterinario.CRMV), "', "))
        Sql.Append(String.Concat(Veterinario.UF.ID, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Veterinario As IVeterinario) Implements IMapeadorDeVeterinario.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE PET_VETERINARIO SET ")
        Sql.Append(String.Concat("CRMV = '", UtilidadesDePersistencia.FiltraApostrofe(Veterinario.CRMV), "', "))
        Sql.Append(String.Concat("UF = ", Veterinario.UF.ID))
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Veterinario.Pessoa.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IVeterinario Implements IMapeadorDeVeterinario.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Veterinario As IVeterinario = Nothing

        Sql.Append("SELECT IDPESSOA, CRMV, UF")
        Sql.Append(" FROM PET_VETERINARIO")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Pessoa.ID.Value.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Veterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IVeterinario)(New Object() {Pessoa})
                Veterinario.CRMV = UtilidadesDePersistencia.GetValorString(Leitor, "CRMV")
                Veterinario.UF = UF.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "UF"))
            End If
        End Using

        Return Veterinario
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeVeterinario.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_VETERINARIO")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function VerificaSePessoaEhVeterinario(ByVal IdPessoa As Long) As Boolean Implements IMapeadorDeVeterinario.VerificaSePessoaEhVeterinario
        Dim Sql As New StringBuilder

        Sql.Append("SELECT IDPESSOA FROM PET_VETERINARIO")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", IdPessoa.ToString))

        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Return Leitor.Read
        End Using

    End Function

End Class