Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Servicos

Public Class MapeadorDePessoaFisica
    Inherits MapeadorDePessoa(Of IPessoaFisica)
    Implements IMapeadorDePessoaFisica

    Private Function ObtenhaQueryBasica() As String
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, TIPO, ENDEMAIL, ")
        Sql.Append("LOGRADOURO, COMPLEMENTO, IDMUNICIPIO, CEP, ")
        Sql.Append("BAIRRO, IDPESSOA, DATANASCIMENTO, ESTADOCIVIL, ")
        Sql.Append("NACIONALIDADE, RACA, SEXO, NOMEMAE, ")
        Sql.Append("NOMEPAI, NUMERORG, ORGEXPEDITOR, DATAEXP, ")
        Sql.Append("UFEXP, CPF, NATURALIDADE, FOTO ")
        Sql.Append("FROM NCL_PESSOA, NCL_PESSOAFISICA")
        Sql.Append(" WHERE ID = IDPESSOA ")

        Return Sql.ToString
    End Function

    Protected Overrides Sub Atualize(ByVal Pessoa As IPessoaFisica)
        Dim ListaQuery As New List(Of String)

        If Pessoa.DataDeNascimento.HasValue Then
            ListaQuery.Add(String.Concat(" DATANASCIMENTO = ", Pessoa.DataDeNascimento.Value.ToString("yyyyMMdd")))
        Else
            ListaQuery.Add(" DATANASCIMENTO = NULL")
        End If


        ListaQuery.Add(String.Concat(" ESTADOCIVIL = '", Pessoa.EstadoCivil.ID, "'"))

        If Not Pessoa.Nacionalidade Is Nothing Then
            ListaQuery.Add(String.Concat(" NACIONALIDADE = '", Pessoa.Nacionalidade.ID, "'"))
        Else
            ListaQuery.Add(" NACIONALIDADE = NULL")
        End If

        If Not Pessoa.Raca Is Nothing Then
            ListaQuery.Add(String.Concat(" RACA = '", Pessoa.Raca.ID, "'"))
        End If

        ListaQuery.Add(String.Concat(" SEXO = '", Pessoa.Sexo.ID, "'"))

        If Not String.IsNullOrEmpty(Pessoa.NomeDaMae) Then
            ListaQuery.Add(String.Concat(" NOMEMAE = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDaMae), "'"))
        Else
            ListaQuery.Add(" NOMEMAE = NULL")
        End If


        If Not String.IsNullOrEmpty(Pessoa.NomeDoPai) Then
            ListaQuery.Add(String.Concat(" NOMEPAI = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDoPai), "'"))
        Else
            ListaQuery.Add(" NOMEPAI = NULL")
        End If

        If Not Pessoa.ObtenhaDocumento(TipoDeDocumento.RG) Is Nothing Then
            Dim RG As IRG

            RG = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.RG), IRG)
            ListaQuery.Add(String.Concat(" NUMERORG = '", RG.Numero, "'"))
            ListaQuery.Add(String.Concat(" ORGEXPEDITOR = '", RG.OrgaoExpeditor, "'"))
            ListaQuery.Add(String.Concat(" UFEXP = ", RG.UF.ID))

            If Not RG.DataDeEmissao Is Nothing Then
                ListaQuery.Add(String.Concat(" DATAEXP = ", RG.DataDeEmissao.Value.ToString("yyyyMMdd")))
            End If
        End If

        If Not Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF) Is Nothing Then
            ListaQuery.Add(String.Concat(" CPF = '", Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF).Numero, "'"))
        End If

        ListaQuery.Add(String.Concat(" NATURALIDADE = ", Pessoa.Naturalidade.ID.Value.ToString))
        ListaQuery.Add(String.Concat(" FOTO = '", Pessoa.Foto, "'"))


        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_PESSOAFISICA SET")

        For Each Item As String In ListaQuery
            Sql.Append(String.Concat(Item, ","))
        Next

        Sql.Remove(Sql.Length - 1, 1)
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Pessoa.ID))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Protected Overrides Function Carregue(ByVal Id As Long) As IPessoaFisica
        Dim Sql As String

        Sql = Me.ObtenhaQueryBasica
        Sql &= String.Concat("AND ID = ", Id.ToString)

        Return ObtenhaPessoa(Sql.ToString)
    End Function

    Private Function ObtenhaPessoa(ByVal SQL As String) As IPessoaFisica
        Dim Pessoas As IList(Of IPessoaFisica)
        Dim Pessoa As IPessoaFisica = Nothing

        Pessoas = ObtenhaPessoas(SQL, Integer.MaxValue)

        If Not Pessoas.Count = 0 Then
            Pessoa = Pessoas.Item(0)
        End If

        Return Pessoa
    End Function

    Protected Overrides Function CarreguePorNome(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica)
        Dim Sql As String

        Sql = Me.ObtenhaQueryBasica

        If Not String.IsNullOrEmpty(Nome) Then
            Sql &= String.Concat("AND NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome).ToUpper, "%'")
        End If

        Return ObtenhaPessoas(Sql.ToString, QuantidadeMaximaDeRegistros)
    End Function

    Private Function ObtenhaPessoas(ByVal Sql As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Pessoa As IPessoaFisica
        Dim Pessoas As IList(Of IPessoaFisica)

        Pessoas = New List(Of IPessoaFisica)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql)
            While Leitor.Read AndAlso Pessoas.Count < QuantidadeMaximaDeRegistros
                Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                MyBase.PreencheDados(Pessoa, Leitor)

                Pessoa.DataDeNascimento = UtilidadesDePersistencia.getValorDate(Leitor, "DATANASCIMENTO").Value
                Pessoa.EstadoCivil = EstadoCivil.ObtenhaEstadoCivil(UtilidadesDePersistencia.getValorChar(Leitor, "ESTADOCIVIL"))
                Pessoa.Nacionalidade = Nacionalidade.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "NACIONALIDADE"))
                Pessoa.Raca = Raca.ObtenhaRaca(UtilidadesDePersistencia.getValorChar(Leitor, "RACA"))
                Pessoa.Sexo = Sexo.ObtenhaSexo(UtilidadesDePersistencia.getValorChar(Leitor, "SEXO"))
                Pessoa.NomeDaMae = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEMAE")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEPAI") Then
                    Pessoa.NomeDoPai = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPAI")
                End If

                Dim MapeadorDeMunicipio As IMapeadorDeMunicipio

                MapeadorDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NATURALIDADE") Then
                    Pessoa.Naturalidade = MapeadorDeMunicipio.ObtenhaMunicipio(UtilidadesDePersistencia.GetValorLong(Leitor, "NATURALIDADE"))
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "FOTO") Then
                    Pessoa.Foto = UtilidadesDePersistencia.GetValorString(Leitor, "FOTO")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "CPF") Then
                    Dim CPF As ICPF

                    CPF = FabricaGenerica.GetInstancia.CrieObjeto(Of ICPF)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "CPF")})
                    Pessoa.AdicioneDocumento(CPF)
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NUMERORG") Then
                    Dim RG As IRG

                    RG = FabricaGenerica.GetInstancia.CrieObjeto(Of IRG)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "NUMERORG")})

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "DATAEXP") Then
                        RG.DataDeEmissao = UtilidadesDePersistencia.getValorDate(Leitor, "DATAEXP")
                    End If

                    RG.OrgaoExpeditor = UtilidadesDePersistencia.GetValorString(Leitor, "ORGEXPEDITOR")
                    RG.UF = UF.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "UFEXP"))
                    Pessoa.AdicioneDocumento(RG)
                End If

                Pessoas.Add(Pessoa)
            End While
        End Using

        Return Pessoas
    End Function

    Protected Overrides Sub Insira(ByVal Pessoa As IPessoaFisica)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_PESSOAFISICA(IDPESSOA, DATANASCIMENTO, ESTADOCIVIL, NACIONALIDADE,")
        Sql.Append("RACA, SEXO, NOMEMAE, NOMEPAI,")
        Sql.Append("NUMERORG, ORGEXPEDITOR, DATAEXP, UFEXP,")
        Sql.Append("CPF, NATURALIDADE, FOTO)")
        Sql.Append("VALUES (")
        Sql.Append(Pessoa.ID.Value)

        If Pessoa.DataDeNascimento.HasValue Then
            Sql.Append(String.Concat(", ", Pessoa.DataDeNascimento.Value.ToString("yyyyMMdd")))
        Else
            Sql.Append(", NULL ")
        End If

        Sql.Append(String.Concat(", '", Pessoa.EstadoCivil.ID, "'"))
        Sql.Append(String.Concat(", '", Pessoa.Nacionalidade.ID, "'"))

        If Not Pessoa.Raca Is Nothing Then
            Sql.Append(String.Concat(", '", Pessoa.Raca.ID, "'"))
        Else
            Sql.Append(", NULL ")
        End If

        Sql.Append(String.Concat(", '", Pessoa.Sexo.ID, "'"))

        If Not String.IsNullOrEmpty(Pessoa.NomeDaMae) Then
            Sql.Append(String.Concat(", '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDaMae), "'"))
        Else
            Sql.Append(String.Concat(", NULL"))
        End If

        If Not String.IsNullOrEmpty(Pessoa.NomeDoPai) Then
            Sql.Append(String.Concat(", '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDoPai), "'"))
        Else
            Sql.Append(String.Concat(", NULL"))
        End If

        Dim RG As IRG = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.RG), IRG)

        If Not RG Is Nothing Then
            Sql.Append(String.Concat(", '", RG.Numero, "'"))
            Sql.Append(String.Concat(", '", RG.OrgaoExpeditor, "'"))

            If Not RG.DataDeEmissao Is Nothing Then
                Sql.Append(String.Concat(", ", RG.DataDeEmissao.Value.ToString("yyyyMMdd")))
            Else
                Sql.Append(String.Concat(", NULL"))
            End If

            Sql.Append(String.Concat(", ", RG.UF.ID))
        Else
            Sql.Append(", NULL, NULL, NULL, NULL")
        End If

        Dim CPF As ICPF = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF), ICPF)

        If Not CPF Is Nothing Then
            Sql.Append(String.Concat(", '", CPF.Numero, "'"))
        Else
            Sql.Append(String.Concat(", NULL"))
        End If

        If Not Pessoa.Naturalidade Is Nothing Then
            Sql.Append(String.Concat(", '", Pessoa.Naturalidade.ID.Value, "'"))
        Else
            Sql.Append(String.Concat(", NULL"))
        End If

        If Not String.IsNullOrEmpty(Pessoa.Foto) Then
            Sql.Append(String.Concat(", '", Pessoa.Foto, "'"))
        Else
            Sql.Append(String.Concat(", NULL"))
        End If

        Sql.Append(")")

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Protected Overrides Sub Remova(ByVal Pessoa As IPessoaFisica)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append(String.Concat("DELETE FROM NCL_PESSOAFISICA WHERE IDPESSOA = ", Pessoa.ID.Value))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class