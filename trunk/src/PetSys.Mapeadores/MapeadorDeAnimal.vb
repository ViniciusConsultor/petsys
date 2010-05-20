Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports System.Text
Imports Compartilhados
Imports Compartilhados.DBHelper
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores

Public Class MapeadorDeAnimal
    Implements IMapeadorDeAnimal

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeAnimal.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_ANIMAL")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByVal Animal As IAnimal) Implements IMapeadorDeAnimal.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Animal.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO PET_ANIMAL (")
        Sql.Append("ID, NOME, DATADENASCIMENTO, SEXO, ")
        Sql.Append("RACA, FOTO, IDPROPRIETARIO, TIPOPESSOA, ESPECIE) ")
        Sql.Append("VALUES (")
        Sql.Append(String.Concat(Animal.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Animal.Nome), "', "))

        If Not Animal.DataDeNascimento Is Nothing Then
            Sql.Append(String.Concat(Animal.DataDeNascimento.Value.ToString("yyyyMMdd"), ", "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat("'", Animal.Sexo.ID.ToString, "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Animal.Raca), "', "))

        If Not String.IsNullOrEmpty(Animal.Foto) Then
            Sql.Append(String.Concat("'", Animal.Foto, "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat(Animal.Proprietario.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Animal.Proprietario.Pessoa.Tipo.ID, ", "))
        Sql.Append(String.Concat("'", Animal.Especie.ID.ToString, "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Animal As IAnimal) Implements IMapeadorDeAnimal.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE PET_ANIMAL SET ")
        Sql.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Animal.Nome), "', "))

        If Not Animal.DataDeNascimento Is Nothing Then
            Sql.Append(String.Concat("DATADENASCIMENTO = ", Animal.DataDeNascimento.Value.ToString("yyyyMMdd"), ", "))
        Else
            Sql.Append("DATADENASCIMENTO = NULL, ")
        End If

        Sql.Append(String.Concat("SEXO = '", Animal.Sexo.ID.ToString, "', "))
        Sql.Append(String.Concat("RACA = '", UtilidadesDePersistencia.FiltraApostrofe(Animal.Raca), "', "))

        If Not String.IsNullOrEmpty(Animal.Foto) Then
            Sql.Append(String.Concat("FOTO = '", Animal.Foto, "', "))
        Else
            Sql.Append("FOTO = NULL, ")
        End If

        Sql.Append(String.Concat("IDPROPRIETARIO = ", Animal.Proprietario.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Animal.Proprietario.Pessoa.Tipo.ID, ", "))
        Sql.Append(String.Concat("ESPECIE = '", Animal.Especie.ID.ToString, "' "))
        Sql.Append(String.Concat("WHERE ID = ", Animal.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAnimaisPorNomeComoFiltro(ByVal Nome As String, _
                                                    ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IAnimal) Implements IMapeadorDeAnimal.ObtenhaAnimaisPorNomeComoFiltro
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, DATADENASCIMENTO, SEXO, ")
        Sql.Append("RACA, FOTO, IDPROPRIETARIO, TIPOPESSOA, ESPECIE ")
        Sql.Append("FROM PET_ANIMAL ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Return ObtenhaAnimais(Sql, QuantidadeMaximaDeRegistros)
    End Function

    Public Function ObtenhaAnimal(ByVal ID As Long) As IAnimal Implements IMapeadorDeAnimal.ObtenhaAnimal
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, DATADENASCIMENTO, SEXO, ")
        Sql.Append("RACA, FOTO, IDPROPRIETARIO, TIPOPESSOA, ESPECIE ")
        Sql.Append("FROM PET_ANIMAL ")
        Sql.Append("WHERE ID = " & ID.ToString)

        Dim Animal As IAnimal = Nothing
        Dim Animais As IList(Of IAnimal)

        Animais = ObtenhaAnimais(Sql, Integer.MaxValue)

        If Not Animais.Count = 0 Then
            Animal = Animais.Item(0)
        End If

        Return Animal
    End Function

    Private Function ObtenhaAnimais(ByVal SQL As StringBuilder, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IAnimal)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Animais As IList(Of IAnimal)
        Dim Animal As IAnimal

        Animais = New List(Of IAnimal)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read AndAlso Animais.Count < QuantidadeMaximaDeRegistros
                Animal = FabricaGenerica.GetInstancia.CrieObjeto(Of IAnimal)()
                Animal.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Animal.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "DATADENASCIMENTO") Then
                    Animal.DataDeNascimento = UtilidadesDePersistencia.getValorDate(Leitor, "DATADENASCIMENTO")
                End If

                Animal.Sexo = SexoDoAnimal.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "SEXO"))
                Animal.Raca = UtilidadesDePersistencia.GetValorString(Leitor, "RACA")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "FOTO") Then
                    Animal.Foto = UtilidadesDePersistencia.GetValorString(Leitor, "FOTO")
                End If

                Dim Pessoa As IPessoa = Nothing
                Dim Tipo As TipoDePessoa

                Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPOPESSOA"))

                If Tipo.Equals(TipoDePessoa.Fisica) Then
                    Dim Mapeador As IMapeadorDePessoa(Of IPessoaFisica)

                    Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()
                    Pessoa = Mapeador.Obtenha(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPROPRIETARIO"))
                Else
                    Dim Mapeador As IMapeadorDePessoa(Of IPessoaJuridica)

                    Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaJuridica)()
                    Pessoa = Mapeador.Obtenha(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPROPRIETARIO"))
                End If

                Animal.Proprietario = FabricaGenerica.GetInstancia.CrieObjeto(Of IProprietarioDeAnimal)(New Object() {Pessoa})
                Animal.Especie = Especie.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "ESPECIE"))

                Animais.Add(Animal)
            End While
        End Using

        Return Animais
    End Function

End Class
