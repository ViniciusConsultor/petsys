Imports System.Data.OleDb
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports System.Text

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao
        End Using

        Me.Cursor = Cursors.Default
        FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Conexao
    End Sub

    'OBS
    'FUNCAO

    Private Function ObtenhaMunicipioCorrespondente(ByVal Leitor As DataRow) As IMunicipio
        Dim MunicipioSTR As String
        Dim StringDeParametro As New StringBuilder

        MunicipioSTR = UtilidadesDePersistencia.GetValor(Leitor, "CIDADE").Trim.ToUpper

        If MunicipioSTR.Contains("GOIANIA") Then
            MunicipioSTR = "GOIÂNIA"
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

        For Each Caracter As Char In MunicipioSTR
            If Char.IsPunctuation(Caracter) Then Exit For
            StringDeParametro.Append(Caracter)
        Next

        Dim Municipios As IList(Of IMunicipio)

        Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
            Municipios = Servico.ObtenhaMunicipiosPorNomeComoFiltro(StringDeParametro.ToString, 1)
        End Using

        Return Municipios(0)
    End Function

    Private Sub btnMigrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMigrar.Click
        Dim DadosLegados As DataTable = ObtenhaDataTableSistemaLegado()
        Dim PessoasMigradas As IList(Of IPessoa) = New List(Of IPessoa)

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

                    If Pessoa.EnderecoDeEmail.ToString.Contains("[NENHUM ENDEREÇO DE EMAIL FOI ENCONTRADO]") Then
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

                If Not IsDBNull(Linha("ENDERECO")) Then
                    Dim Endereco As IEndereco

                    Endereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IEndereco)()
                    Endereco.Logradouro = UtilidadesDePersistencia.GetValor(Linha, "ENDERECO")

                    If Not IsDBNull(Linha("BAIRRO")) Then
                        Endereco.Bairro = UtilidadesDePersistencia.GetValor(Linha, "BAIRRO")
                    End If

                    If Not IsDBNull(Linha("CIDADE")) Then
                        Endereco.Municipio = ObtenhaMunicipioCorrespondente(Linha)
                    End If

                    Pessoa.Endereco = Endereco
                End If

                If Not IsDBNull(Linha("FONE")) Then
                    Dim FoneSTR As String = UtilidadesDePersistencia.GetValor(Linha, "FONE")

                End If

                If Not IsDBNull(Linha("FONE_COMERCIAL")) Then
                    Dim FoneSTR As String = UtilidadesDePersistencia.GetValor(Linha, "FONE_COMERCIAL")


                End If

                If Not IsDBNull(Linha("FONE_RESIDENCIAL")) Then
                    Dim FoneSTR As String = UtilidadesDePersistencia.GetValor(Linha, "FONE_RESIDENCIAL")


                End If

                If Not IsDBNull(Linha("FONE_CELULAR")) Then
                    Dim FoneSTR As String = UtilidadesDePersistencia.GetValor(Linha, "FONE_CELULAR")


                End If

                PessoasMigradas.Add(Pessoa)
            End If
        Next

        Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            For Each Pessoa As IPessoaFisica In PessoasMigradas
                Servico.Inserir(Pessoa)
            Next
        End Using

    End Sub

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
