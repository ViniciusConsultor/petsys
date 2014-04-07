Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Public Class MapeadorDePessoaFisica
    Inherits MapeadorDePessoa(Of IPessoaFisica)
    Implements IMapeadorDePessoaFisica

    Private Function ObtenhaQueryBasica() As String
        Dim Sql As New StringBuilder

        Sql.Append("SELECT NCL_PESSOA.NOME NOMEPESSOA, TIPO, ENDEMAIL, ")
        Sql.Append("NCL_PESSOAFISICA.IDPESSOA, DATANASCIMENTO, ESTADOCIVIL, ")
        Sql.Append("NACIONALIDADE, RACA, SEXO, NOMEMAE, ")
        Sql.Append("NOMEPAI, NUMERORG, ORGEXPEDITOR, DATAEXP, ")
        Sql.Append("UFEXP, CPF, NATURALIDADE, FOTO, SITE, NCL_PESSOA.IDBANCO, IDAGENCIA, CNTACORRENTE, TIPOCNTACORRENTE, NCL_BANCO.NUMERO  NUMEROBANCO, NCL_BANCO.NOME NOMEDOBANCO, NCL_AGENCIABANCO.NUMERO  NUMEROAGENCIA ")
        Sql.Append("FROM NCL_PESSOA ")
        Sql.Append("INNER JOIN NCL_PESSOAFISICA ON NCL_PESSOA.ID = NCL_PESSOAFISICA.IDPESSOA ")
        Sql.Append("LEFT JOIN NCL_BANCO ON NCL_BANCO.ID = NCL_PESSOA.IDBANCO ")
        Sql.Append("LEFT JOIN NCL_AGENCIABANCO ON  NCL_AGENCIABANCO.IDBANCO =  NCL_PESSOA.IDBANCO AND NCL_AGENCIABANCO.IDPESSOA = NCL_PESSOA.IDAGENCIA ")

        Return Sql.ToString
    End Function

    Protected Overrides Sub Atualize(ByVal Pessoa As IPessoaFisica)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_PESSOAFISICA SET")

        If Pessoa.DataDeNascimento.HasValue Then
            Sql.Append(String.Concat(" DATANASCIMENTO = ", Pessoa.DataDeNascimento.Value.ToString("yyyyMMdd"), ","))
        Else
            Sql.Append(" DATANASCIMENTO = NULL,")
        End If

        Sql.Append(String.Concat(" ESTADOCIVIL = '", Pessoa.EstadoCivil.ID, "',"))

        If Not Pessoa.Nacionalidade Is Nothing Then
            Sql.Append(String.Concat(" NACIONALIDADE = '", Pessoa.Nacionalidade.ID, "',"))
        Else
            Sql.Append(" NACIONALIDADE = NULL,")
        End If

        If Not Pessoa.Raca Is Nothing Then
            Sql.Append(String.Concat(" RACA = '", Pessoa.Raca.ID, "',"))
        Else
            Sql.Append(" RACA = NULL,")
        End If

        Sql.Append(String.Concat(" SEXO = '", Pessoa.Sexo.ID, "',"))

        If Not String.IsNullOrEmpty(Pessoa.NomeDaMae) Then
            Sql.Append(String.Concat(" NOMEMAE = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDaMae), "',"))
        Else
            Sql.Append(" NOMEMAE = NULL,")
        End If


        If Not String.IsNullOrEmpty(Pessoa.NomeDoPai) Then
            Sql.Append(String.Concat(" NOMEPAI = '", UtilidadesDePersistencia.FiltraApostrofe(Pessoa.NomeDoPai), "',"))
        Else
            Sql.Append(" NOMEPAI = NULL,")
        End If

        If Not Pessoa.ObtenhaDocumento(TipoDeDocumento.RG) Is Nothing Then
            Dim RG As IRG

            RG = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.RG), IRG)
            Sql.Append(String.Concat(" NUMERORG = '", RG.Numero, "',"))
            Sql.Append(String.Concat(" ORGEXPEDITOR = '", RG.OrgaoExpeditor, "',"))
            Sql.Append(String.Concat(" UFEXP = ", RG.UF.ID, ","))

            If Not RG.DataDeEmissao Is Nothing Then
                Sql.Append(String.Concat(" DATAEXP = ", RG.DataDeEmissao.Value.ToString("yyyyMMdd"), ","))
            Else
                Sql.Append(" DATAEXP = NULL,")
            End If
        Else
            Sql.Append(" NUMERORG = NULL, ORGEXPEDITOR = NULL, UFEXP = NULL, DATAEXP= NULL,")
        End If

        If Not Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF) Is Nothing Then
            Sql.Append(String.Concat(" CPF = '", Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF).Numero, "',"))
        Else
            Sql.Append(" CPF = NULL,")
        End If

        If Not Pessoa.Naturalidade Is Nothing Then
            Sql.Append(String.Concat(" NATURALIDADE = ", Pessoa.Naturalidade.ID.Value.ToString, ","))
        Else
            Sql.Append(" NATURALIDADE = NULL,")
        End If

        Sql.Append(String.Concat(" FOTO = '", Pessoa.Foto, "'"))
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Pessoa.ID))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Protected Overrides Function Carregue(ByVal Id As Long) As IPessoaFisica
        Dim Sql As String

        Sql = Me.ObtenhaQueryBasica
        Sql &= String.Concat(" WHERE NCL_PESSOA.ID = ", Id.ToString)

        Return ObtenhaPessoa(Sql.ToString)
    End Function

    Private Function ObtenhaPessoa(ByVal SQL As String) As IPessoaFisica
        Dim Pessoas As IList(Of IPessoaFisica)
        Dim Pessoa As IPessoaFisica = Nothing

        Pessoas = ObtenhaPessoas(SQL, Integer.MaxValue, 1)

        If Not Pessoas.Count = 0 Then
            Pessoa = Pessoas.Item(0)
        End If

        Return Pessoa
    End Function

    Protected Overrides Function CarreguePorNome(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer, _
                                                 NivelDeRetardo As Integer) As IList(Of IPessoaFisica)
        Dim Sql As String

        Sql = Me.ObtenhaQueryBasica

        If Not String.IsNullOrEmpty(Nome) Then
            Sql &= String.Concat(" WHERE NCL_PESSOA.NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'")
        End If

        Sql &= " ORDER BY NCL_PESSOA.NOME"

        Return ObtenhaPessoas(Sql.ToString, QuantidadeMaximaDeRegistros, NivelDeRetardo)
    End Function

    Private Function ObtenhaPessoas(ByVal Sql As String, ByVal QuantidadeMaximaDeRegistros As Integer, NivelDeRetardo As Integer) As IList(Of IPessoaFisica)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Pessoa As IPessoaFisica
        Dim Pessoas As IList(Of IPessoaFisica)

        Pessoas = New List(Of IPessoaFisica)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                    MyBase.PreencheDados(Pessoa, Leitor, NivelDeRetardo)

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "DATANASCIMENTO") Then
                        Pessoa.DataDeNascimento = UtilidadesDePersistencia.getValorDate(Leitor, "DATANASCIMENTO").Value
                    End If

                    Pessoa.EstadoCivil = EstadoCivil.ObtenhaEstadoCivil(UtilidadesDePersistencia.getValorChar(Leitor, "ESTADOCIVIL"))
                    Pessoa.Nacionalidade = Nacionalidade.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "NACIONALIDADE"))
                    Pessoa.Raca = Raca.ObtenhaRaca(UtilidadesDePersistencia.getValorChar(Leitor, "RACA"))
                    Pessoa.Sexo = Sexo.ObtenhaSexo(UtilidadesDePersistencia.getValorChar(Leitor, "SEXO"))

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEMAE") Then
                        Pessoa.NomeDaMae = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEMAE")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEPAI") Then
                        Pessoa.NomeDoPai = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPAI")
                    End If
                    
                    If NivelDeRetardo > 0 Then
                        Dim MapeadorDeMunicipio As IMapeadorDeMunicipio

                        MapeadorDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMunicipio)()

                        If Not UtilidadesDePersistencia.EhNulo(Leitor, "NATURALIDADE") Then
                            Pessoa.Naturalidade = MapeadorDeMunicipio.ObtenhaMunicipio(UtilidadesDePersistencia.GetValorLong(Leitor, "NATURALIDADE"))
                        End If
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
            Finally
                Leitor.Close()
            End Try

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

    Protected Overrides Sub Remova(ByVal ID As Long)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append(String.Concat("DELETE FROM NCL_PESSOAFISICA WHERE IDPESSOA = ", ID))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class