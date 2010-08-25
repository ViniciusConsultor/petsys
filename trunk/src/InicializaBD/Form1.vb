Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao
        End Using

        Me.Cursor = Cursors.Default
        FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Conexao
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Function ValidaDados() As String
        If String.IsNullOrEmpty(txtNome.Text) Then Return "O nome do operador deve ser informado."
        If String.IsNullOrEmpty(txtLogin.Text) Then Return "O login do operador deve ser informado."
        If String.IsNullOrEmpty(txtSenha.Text) Then Return "A senha do operador deve ser informada."

        Return Nothing
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInicializar.Click
        Dim Inconsistencia As String = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            MsgBox(Inconsistencia, MsgBoxStyle.Exclamation, "Inconsistências")
            Exit Sub
        End If

        Try
            InicializaMunicipios()
            IniciaOperador()

            MsgBox("Base de dados inicializada com sucesso.", MsgBoxStyle.Information, "Inicialização de base de dados.")
        Catch ex As Exception
            MsgBox("Ocorreu um erro durante a inicialização do banco de dados." & vbLf & ex.Message, MsgBoxStyle.Critical, "Inicialização de base de dados.")
        End Try
    End Sub

    Private Sub IniciaOperador()
        Dim Pessoa As IPessoaFisica
        Dim Operador As IOperador
        Dim Senha As ISenha
        Dim Grupo As IGrupo

        Me.Cursor = Cursors.WaitCursor
        ToolStripProgressBar1.Visible = True
        ToolStripProgressBar1.Maximum = 8
        Me.Activate()

        ToolStripStatusLabel1.Text = "Preparando Grupo de Usuário ADMINISTRADOR..."

        Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupo)()
        Grupo.Nome = "GRUPO ADMINISTRADOR"
        Grupo.Status = StatusDoGrupo.Ativo

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Inserindo Grupo de Usuário ADMINISTRADOR..."

        Using ServicoDeGrupo As IServicoDeGrupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupo)()
            ServicoDeGrupo.Inserir(Grupo)
        End Using

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Preparando Pessoa ADMINISTRADOR..."

        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
        Pessoa.Nome = txtNome.Text
        Pessoa.Sexo = Sexo.Masculino
        Pessoa.EstadoCivil = EstadoCivil.Ignorado
        Pessoa.Nacionalidade = Nacionalidade.Outros
        Pessoa.Raca = Raca.Branca

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Inserindo Pessoa ADMINISTRADOR..."

        Using ServicoDePessoa As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
            ServicoDePessoa.Inserir(Pessoa)
        End Using

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Preparando Operador ADMINISTRADOR..."
        Operador = FabricaGenerica.GetInstancia.CrieObjeto(Of IOperador)(New Object() {Pessoa})
        Operador.Login = txtLogin.Text
        Operador.Status = StatusDoOperador.Ativo
        Operador.AdicioneGrupo(Grupo)

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Preparando a Senha do Operador ADMINISTRADOR..."
        Senha = ObtenhaSenha()

        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Inserindo o Operador ADMINISTRADOR..."
        Using ServicoDeOperador As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
            ServicoDeOperador.Inserir(Operador, senha)
        End Using
        ToolStripProgressBar1.Increment(1)

        ToolStripStatusLabel1.Text = "Inserindo as Autorizações do Operador ADMINISTRADOR..."
        Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
            Servico.Modifique(Grupo.ID.Value, ObtenhaDiretivas)
        End Using
        ToolStripProgressBar1.Increment(1)

        Me.Cursor = Cursors.Default
        Me.ToolStripProgressBar1.Value = 0
        ToolStripProgressBar1.Visible = False
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Function ObtenhaDiretivas() As IList(Of IDiretivaDeSeguranca)
        Dim Modulos As IList(Of IModulo)
        Dim Diretiva As IDiretivaDeSeguranca
        Dim Diretivas As IList(Of IDiretivaDeSeguranca)

        Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
            Modulos = Servico.ObtenhaModulosDisponiveis
        End Using

        Diretivas = New List(Of IDiretivaDeSeguranca)
        Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()

        For Each Modulo As IModulo In Modulos
            Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()
            Diretiva.ID = Modulo.ID.ToString
            Diretivas.Add(Diretiva)

            For Each Funcao As IFuncao In Modulo.ObtenhaFuncoes
                Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()
                Diretiva.ID = Funcao.ID.ToString
                Diretivas.Add(Diretiva)

                For Each Operacao As IOperacao In Funcao.ObtenhaOperacoes
                    Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()
                    Diretiva.ID = Operacao.ID.ToString
                    Diretivas.Add(Diretiva)
                Next
            Next
        Next

        Return Diretivas
    End Function

    Private Function ObtenhaSenha() As ISenha
        Dim Senha As ISenha
        Dim SenhaTXTCript As String

        SenhaTXTCript = AjudanteDeCriptografia.CriptografeMaoUnicao(txtSenha.Text)

        Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {SenhaTXTCript, Now})
        Return Senha
    End Function

    Private Sub InicializaMunicipios()
        Me.Cursor = Cursors.WaitCursor
        ToolStripStatusLabel1.Text = "Abrindo arquivo de municípios..."

        Dim Diretorio As String = Environment.CurrentDirectory

        Dim Arquivo As New StreamReader(Diretorio & "\municipios.sql")
        Dim Municipios As IList(Of IMunicipio) = New List(Of IMunicipio)

        ToolStripStatusLabel1.Text = "Iniciando a leitura do arquivo..."
        While Not Arquivo.EndOfStream
            Dim Linha As String

            Linha = Arquivo.ReadLine()

            If Not String.IsNullOrEmpty(Linha) AndAlso Not Linha.Equals(";") Then
                Municipios.Add(CriaMunicipio(Linha))
            End If
        End While

        ToolStripStatusLabel1.Text = "Dados carregados com sucesso.."
        ToolStripStatusLabel1.Text = "Iniciando processamento.."
        ToolStripProgressBar1.Visible = True
        ToolStripProgressBar1.Maximum = Municipios.Count
        Me.Activate()
        ToolStripStatusLabel1.Text = "Inserindo municípios"

        Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
            For Each Municipio As IMunicipio In Municipios
                Application.DoEvents()
                Servico.Inserir(Municipio)
                ToolStripStatusLabel1.Text = "Município - " & Municipio.Nome & " inserido com sucesso..."
                ToolStripProgressBar1.Increment(1)
            Next
        End Using

        ToolStripStatusLabel1.Text = "Total de municípios inseridos: " & Municipios.Count

        Me.Cursor = Cursors.Default
        Me.ToolStripProgressBar1.Value = 0
        ToolStripProgressBar1.Visible = False
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Function CriaMunicipio(ByVal LinhaMunicipio As String) As IMunicipio
        Dim Municipio As IMunicipio = Nothing
        Dim VetorA() As String
        Dim VetorB() As String

        VetorA = Split(LinhaMunicipio, "VALUES")
        VetorB = Split(VetorA(1), ",")

        Municipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMunicipio)()
        Municipio.Nome = ObtenhaValor(VetorB(1))
        Municipio.UF = UF.Obtenha(CShort(ObtenhaValor(VetorB(2))))
        Municipio.CEP = New CEP(CLng(ObtenhaValor(VetorB(3))))
        Return Municipio
    End Function

    Private Function ObtenhaValor(ByVal Valor As String) As String
        If Valor.Contains("(") Then
            Valor = Valor.Replace("(", "").Trim
        End If

        If Valor.Contains("'") Then
            Valor = Valor.Replace("'", "").Trim
        End If

        If Valor.Contains(")") Then
            Valor = Valor.Replace(")", "").Trim
        End If

        Return Valor.Trim
    End Function

End Class