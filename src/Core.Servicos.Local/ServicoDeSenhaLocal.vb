Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Servicos

Public Class ServicoDeSenhaLocal
    Inherits Servico
    Implements IServicoDeSenha
    
    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaSenhaDoOperador(ByVal Operador As IOperador) As ISenha Implements IServicoDeSenha.ObtenhaSenhaDoOperador
        Dim Senha As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)

        Try
            Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()
            Senha = Mapeador.ObtenhaSenhaDoOperador(Operador.Pessoa.ID.Value)

            If Senha Is Nothing Then
                Throw New BussinesException("Não existe senha cadastrada para este operador.")
            End If

            Return Senha
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal SenhaAntigaInformada As ISenha, _
                      ByVal NovaSenha As ISenha, _
                      ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        Dim SenhaAntigaGravada As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            SenhaAntigaGravada = Mapeador.ObtenhaSenhaDoOperador(IDOperador)
            ValidaRegras(SenhaAntigaGravada, SenhaAntigaInformada, NovaSenha, ConfirmacaoNovaSenha)
            Mapeador.Altere(IDOperador, NovaSenha)
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Sub

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal NovaSenha As ISenha, _
                      ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        Dim SenhaAntigaGravada As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            SenhaAntigaGravada = Mapeador.ObtenhaSenhaDoOperador(IDOperador)
            ValidaRegras(SenhaAntigaGravada, SenhaAntigaGravada, NovaSenha, ConfirmacaoNovaSenha)
            Mapeador.Altere(IDOperador, NovaSenha)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub ValidaRegras(ByVal SenhaAntigaGravada As ISenha, _
                             ByVal SenhaAntigaInformada As ISenha, _
                             ByVal NovaSenha As ISenha, _
                             ByVal ConfirmacaoNovaSenha As ISenha)
        If Not SenhaAntigaGravada.ToString.Equals(SenhaAntigaInformada.ToString) Then
            Throw New BussinesException("A senha atual informada não está correta.")
        End If

        If Not NovaSenha.ToString.Equals(ConfirmacaoNovaSenha.ToString) Then
            Throw New BussinesException("A confirmação da nova senha não confere com a nova senha.")
        End If

        If SenhaAntigaGravada.ToString.Equals(NovaSenha.ToString) Then
            Throw New BussinesException("A nova senha não deve ser igual a senha antiga.")
        End If
    End Sub

    Public Sub RegistreDefinicaoDeNovaSenha(ByVal Operador As IOperador, _
                                            ByVal Link As String) Implements IServicoDeSenha.RegistreDefinicaoDeNovaSenha
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            ServerUtils.BeginTransaction()

            Dim id As Long = Mapeador.RegistreDefinicaoDeNovaSenha(Operador)
            EnviaEmailDeDefinicaoDaSenha(id, Link, Operador.Pessoa)
            ServerUtils.CommitTransaction()

        Catch ex As Exception
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub EnviaEmailDeDefinicaoDaSenha(ByVal Id As Long, ByVal Link As String, Pessoa As IPessoa)
        Dim Configuracao As IConfiguracaoDoSistema

        Using ServicoDeConfiguracao As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = ServicoDeConfiguracao.ObtenhaConfiguracaoDoSistema()
        End Using

        If Not Configuracao Is Nothing AndAlso Configuracao.NotificarErrosAutomaticamente Then
            Dim ConfiguracaoDeEmail As IConfiguracaoDeEmailDoSistema

            ConfiguracaoDeEmail = Configuracao.ConfiguracaoDeEmailDoSistema
            If Not Configuracao Is Nothing Then
                Dim CorpoDoEmail As String = Link & Id

                Dim destinarios = New List(Of String)

                For Each email As EnderecoDeEmail In Pessoa.EnderecosDeEmails
                    destinarios.Add(email.ToString)
                Next

                GerenciadorDeEmail.EnviaEmail("Redefinição de senha.", _
                                              destinarios, _
                                              Nothing,
                                              Nothing,
                                              CorpoDoEmail,
                                              Nothing, _
                                              "REDEFICAO_DE_SENHA", _
                                              True)
            End If
        End If
    End Sub

    Public Function ObtenhaIDOperadorParaRedifinirSenha(IDRedefinicaoDeSenha As Long) As Long? Implements IServicoDeSenha.ObtenhaIDOperadorParaRedifinirSenha
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            Return Mapeador.ObtenhaIDOperadorParaRedifinirSenha(IDRedefinicaoDeSenha)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RedefinaSenha(ByVal IDRedefinicaoDeSenha As Long,
                             ByVal IDOperador As Long, _
                             ByVal NovaSenha As ISenha, _
                             ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.RedefinaSenha
        Dim SenhaAntigaGravada As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)

        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            ServerUtils.BeginTransaction()

            SenhaAntigaGravada = Mapeador.ObtenhaSenhaDoOperador(IDOperador)
            ValidaRegras(SenhaAntigaGravada, SenhaAntigaGravada, NovaSenha, ConfirmacaoNovaSenha)
            Mapeador.Altere(IDOperador, NovaSenha)

            Mapeador.ExcluaRefinicaoDeSenha(IDRedefinicaoDeSenha)

            ServerUtils.CommitTransaction()

        Catch ex As Exception
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub
End Class