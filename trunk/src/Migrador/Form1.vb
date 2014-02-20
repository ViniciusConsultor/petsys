Imports System.Data.OleDb
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports System.Text
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio


Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao
        End Using

        Me.Cursor = Cursors.Default
        FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Conexao
    End Sub

    Private Function ObtenhaMunicipioCorrespondente(ByVal Leitor As DataRow) As IMunicipio
        Dim MunicipioSTR As String

        MunicipioSTR = UtilidadesDePersistencia.GetValor(Leitor, "CIDADE").Trim.ToUpper

        If MunicipioSTR.Contains("GOIANIA") Or MunicipioSTR.Contains("GOIÂNIA") Then
            MunicipioSTR = "GOIÂNIA"
        End If

        If MunicipioSTR.Contains("FLORES DE GOIAS") Then
            MunicipioSTR = "FLORES DE GOIÁS"
        End If

        If MunicipioSTR.Contains("GOIANAPOLIS") Then
            MunicipioSTR = "GOIANÁPOLIS"
        End If

        If MunicipioSTR.Contains("NIQUELANDIA") Then
            MunicipioSTR = "NIQUELÂNDIA"
        End If

        If MunicipioSTR.Contains("TRINTADE") Then
            MunicipioSTR = "TRINDADE"
        End If

        If MunicipioSTR.Contains("AMORINOPOLIS") Then
            MunicipioSTR = "AMORINÓPOLIS"
        End If

        If MunicipioSTR.Contains("UTAUÇÚ") Then
            MunicipioSTR = "ITAUÇU"
        End If

        If MunicipioSTR.Contains("SANTA FÉ DE GOIAS") Then
            MunicipioSTR = "SANTA FÉ DE GOIÁS"
        End If

        If MunicipioSTR.Contains("AGUA FRIA") Then
            MunicipioSTR = "ÁGUA FRIA"
        End If

        If MunicipioSTR.Contains("DIVINÓPOLIS DE GOIOÁS") Then
            MunicipioSTR = "DIVINÓPOLIS DE GOIÁS"
        End If

        If MunicipioSTR.Contains("QUIRINOPOLIS") Then
            MunicipioSTR = "QUIRINÓPOLIS"
        End If

        If MunicipioSTR.Contains("NOVA AMERICA") Then
            MunicipioSTR = "NOVA AMÉRICA"
        End If

        If MunicipioSTR.Contains("PALMEIRAS DE GOIAS") Then
            MunicipioSTR = "PALMEIRAS DE GOIÁS"
        End If

        If MunicipioSTR.Contains("SÃO LUIS DE MONTES BELOS") Then
            MunicipioSTR = "SÃO LUÍS DE MONTES BELOS"
        End If

        If MunicipioSTR.Contains("SÃO FRANCISCO DE GOIAS") Then
            MunicipioSTR = "SÃO FRANCISCO DE GOIÁS"
        End If

        If MunicipioSTR.Equals("SANTA ROSA DE GOIAS") Then
            MunicipioSTR = "SANTA ROSA DE GOIÁS"
        End If

        If MunicipioSTR.Equals("ALTO PARAISO DE GOIAS") Then
            MunicipioSTR = "ALTO PARAÍSO DE GOIÁS"
        End If

        If MunicipioSTR.Equals("PETROLINA DE GOIAS") Then
            MunicipioSTR = "PETROLINA DE GOIÁS"
        End If

        If MunicipioSTR.Equals("MONTIVIDIU DO NORTE") Then
            MunicipioSTR = "MONTIVIDIU"
        End If

        If MunicipioSTR.Equals("PALESTINA DE GOIAS") Then
            MunicipioSTR = "PALESTINA DE GOIÁS"
        End If

        If MunicipioSTR.Equals("BRASILIA") Or MunicipioSTR.Equals("BRASÍIA") Then
            MunicipioSTR = "BRASÍLIA"
        End If

        If MunicipioSTR.Equals("GIOIÂNIA") Then
            MunicipioSTR = "GOIÂNIA"
        End If

        If MunicipioSTR.Equals("CAMPOS BELOS DE GOIÁS") Then
            MunicipioSTR = "CAMPOS BELOS"
        End If

        If MunicipioSTR.Equals("SANTA CRUZ DE GOIAS") Then
            MunicipioSTR = "SANTA CRUZ DE GOIÁS"
        End If

        If MunicipioSTR.Equals("CAMPINAÇÚ") Then
            MunicipioSTR = "CAMPINAÇU"
        End If

        If MunicipioSTR.Equals("COCALZINHO DE GOIAS") Then
            MunicipioSTR = "COCALZINHO DE GOIÁS"
        End If

        If MunicipioSTR.Equals("APARECIDA DE GOIANIA") Then
            MunicipioSTR = "APARECIDA DE GOIÂNIA"
        End If

        If MunicipioSTR.Equals("ANAPOLIS") Then
            MunicipioSTR = "ANÁPOLIS"
        End If

        If MunicipioSTR.Equals("ARACAJÚ") Then
            MunicipioSTR = "ARACAJU"
        End If

        Dim Municipios As IList(Of IMunicipio)

        Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
            Municipios = Servico.ObtenhaMunicipiosPorNomeComoFiltro(MunicipioSTR.ToString, 5)
        End Using

        Return Municipios(0)
    End Function

    Private Sub btnMigrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMigrar.Click
        Dim DadosLegados As DataTable = ObtenhaDataTableSistemaLegado()

        For Each Linha As DataRow In DadosLegados.Rows
            Dim Pessoa As IPessoaFisica

            If Not IsDBNull(Linha("PREFEITO")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "PREFEITO")) Then
                Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Pessoa.Nome = UtilidadesDePersistencia.GetValor(Linha, "PREFEITO")

                If Pessoa.Nome.Length > 100 Then
                    Pessoa.Nome = Pessoa.Nome.Remove(99, Pessoa.Nome.Length - 100)
                End If

                Pessoa.EstadoCivil = EstadoCivil.Ignorado
                Pessoa.Nacionalidade = Nacionalidade.Brasileira

                If Not IsDBNull(Linha("EMAIL")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "EMAIL")) Then
                    Pessoa.EnderecoDeEmail = UtilidadesDePersistencia.GetValor(Linha, "EMAIL")

                    If Pessoa.EnderecoDeEmail.ToString.Equals("[NENHUM ENDEREÇO DE EMAIL FOI ENCONTRADO]", StringComparison.InvariantCultureIgnoreCase) Then
                        Pessoa.EnderecoDeEmail = Nothing
                    End If
                End If

                If Not IsDBNull(Linha("SEXO")) Then
                    Pessoa.Sexo = Sexo.ObtenhaSexo(UtilidadesDePersistencia.getValorChar(Linha, "SEXO"))
                    If Pessoa.Sexo Is Nothing Then
                        Pessoa.Sexo = Sexo.Masculino
                    End If
                Else
                    Pessoa.Sexo = Sexo.Masculino
                End If

                If Not IsDBNull(Linha("DTNASC")) Then
                    Pessoa.DataDeNascimento = CDate(Linha("DTNASC"))
                End If

                'If Not IsDBNull(Linha("ENDERECO")) Then
                '    Dim Endereco As IEndereco

                '    Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()
                '    Endereco.Logradouro = UtilidadesDePersistencia.GetValor(Linha, "ENDERECO")

                '    If Not IsDBNull(Linha("BAIRRO")) Then
                '        Endereco.Bairro = UtilidadesDePersistencia.GetValor(Linha, "BAIRRO")
                '    End If

                '    If Not IsDBNull(Linha("CIDADE")) Then
                '        Try
                '            Endereco.Municipio = ObtenhaMunicipioCorrespondente(Linha)
                '            Endereco.CEP = Endereco.Municipio.CEP

                '        Catch ex As Exception
                '            Pessoa.Endereco = Nothing
                '        End Try
                '    End If

                '    Pessoa.Endereco = Endereco
                'End If

                If Not IsDBNull(Linha("FONE")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "FONE")) Then
                    Dim TelefonesStr As IList(Of String)

                    TelefonesStr = ObtenhaApenasNumerosDoTelefone(UtilidadesDePersistencia.GetValor(Linha, "FONE"))
                    Me.ObtenhaTelefones(TelefonesStr, TipoDeTelefone.Comercial, Pessoa)
                End If

                If Not IsDBNull(Linha("FONE_COMERCIAL")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "FONE_COMERCIAL")) Then
                    Dim TelefonesStr As IList(Of String)

                    TelefonesStr = ObtenhaApenasNumerosDoTelefone(UtilidadesDePersistencia.GetValor(Linha, "FONE_COMERCIAL"))
                    Me.ObtenhaTelefones(TelefonesStr, TipoDeTelefone.Comercial, Pessoa)
                End If

                If Not IsDBNull(Linha("FONE_RESIDENCIAL")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "FONE_COMERCIAL")) Then
                    Dim TelefonesStr As IList(Of String)

                    TelefonesStr = ObtenhaApenasNumerosDoTelefone(UtilidadesDePersistencia.GetValor(Linha, "FONE_RESIDENCIAL"))
                    Me.ObtenhaTelefones(TelefonesStr, TipoDeTelefone.Residencial, Pessoa)
                End If

                If Not IsDBNull(Linha("FONE_CELULAR")) AndAlso Not String.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(Linha, "FONE_CELULAR")) Then
                    Dim TelefonesStr As IList(Of String)

                    TelefonesStr = ObtenhaApenasNumerosDoTelefone(UtilidadesDePersistencia.GetValor(Linha, "FONE_CELULAR"))
                    Me.ObtenhaTelefones(TelefonesStr, TipoDeTelefone.Celular, Pessoa)
                End If

                Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
                    Servico.Inserir(Pessoa)

                    Dim Contato As IContato

                    Contato = FabricaGenerica.GetInstancia.CrieObjeto(Of IContato)(New Object() {Pessoa})

                    If Not IsDBNull(Linha("FUNCAO")) Then
                        Contato.Cargo = UtilidadesDePersistencia.GetValor(Linha, "FUNCAO")
                    End If

                    If Not IsDBNull(Linha("OBS")) Then
                        Contato.Cargo = UtilidadesDePersistencia.GetValor(Linha, "OBS")
                    End If

                    Using ServicoDeContato As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
                        ServicoDeContato.Inserir(Contato)
                    End Using

                End Using

            End If
        Next

    End Sub

    Private Sub ObtenhaTelefones(ByVal TelefonesStr As IList(Of String), ByVal Tipo As TipoDeTelefone, ByRef Pessoa As IPessoaFisica)
        Dim Numero As Long
        Dim DDD As Short

        For Each TelefoneStr As String In TelefonesStr
            If TelefoneStr.Count = 10 Then
                DDD = CShort(Mid(TelefoneStr, 1, 2))
                Numero = CLng(Mid(TelefoneStr, 3))
            Else
                Numero = CLng(TelefoneStr)
            End If

            Dim Telefone As ITelefone

            Telefone = FabricaGenerica.GetInstancia.CrieObjeto(Of ITelefone)()

            Telefone.DDD = DDD
            Telefone.Numero = Numero
            Telefone.Tipo = Tipo

            If Not Pessoa.Telefones.Contains(Telefone) Then
                Pessoa.AdicioneTelefone(Telefone)
            End If
        Next
    End Sub

    Private Function ObtenhaApenasNumerosDoTelefone(ByVal NumeroStrDesformatado As String) As IList(Of String)
        Dim Telefones As IList(Of String) = New List(Of String)
        Dim NumeroAux As New StringBuilder

        For Each Caracter As Char In NumeroStrDesformatado
            If Char.IsNumber(Caracter) Then
                NumeroAux.Append(Caracter)
            End If

            If NumeroAux.Length = 10 OrElse (NumeroAux.Length = 8 And Telefones.Count > 0) Then
                Telefones.Add(NumeroAux.ToString)
                NumeroAux = New StringBuilder
            End If

        Next

        Return Telefones
    End Function

    Private Function ObtenhaDataTableSistemaLegado() As DataTable
        Dim DataSetDadosLegados As DataSet = New DataSet

        Using ConexaoAcess As OleDbConnection = New OleDbConnection(txtStringConexao.Text)
            ConexaoAcess.Open()

            Using Data As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM DADOS", ConexaoAcess)
                Data.Fill(DataSetDadosLegados)
            End Using

            ConexaoAcess.Close()
        End Using


        Return DataSetDadosLegados.Tables(0)
    End Function

End Class
