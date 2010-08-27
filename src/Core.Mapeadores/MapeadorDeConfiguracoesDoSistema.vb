Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeConfiguracoesDoSistema
    Implements IMapeadorDeConfiguracoesDoSistema

    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IMapeadorDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim ConfiguracaoDoSistema As IConfiguracaoDoSistema = Nothing
        Dim ConfiguracaoDeEmail As IConfiguracaoDeEmailDoSistema = Nothing

        Sql.Append("SELECT NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL,")
        Sql.Append("PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA,")
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA")
        Sql.Append(" FROM NCL_CNFGERAL")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                ConfiguracaoDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDoSistema)()
                ConfiguracaoDoSistema.NotificarErrosAutomaticamente = UtilidadesDePersistencia.GetValorBooleano(Leitor, "NOTIFERROSREMAIL")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "") Then
                    ConfiguracaoDoSistema.RemetenteDaNotificaoDeErros = UtilidadesDePersistencia.GetValorString(Leitor, "EMAILREMETNOTIFERROS")
                End If

                'OU SEJA TEM CONFIGURACAO DE E-MAIL CADASTRADO
                If Not UtilidadesDePersistencia.EhNulo(Leitor, "REMETENTEPADRAO") Then
                    ConfiguracaoDeEmail = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeEmailDoSistema)()
                    ConfiguracaoDeEmail.EmailRemetente = UtilidadesDePersistencia.GetValorString(Leitor, "REMETENTEPADRAO")
                    ConfiguracaoDeEmail.HabilitarSSL = UtilidadesDePersistencia.GetValorBooleano(Leitor, "HABILITARSSL")
                    ConfiguracaoDeEmail.Porta = UtilidadesDePersistencia.getValorInteger(Leitor, "PORTA")
                    ConfiguracaoDeEmail.RequerAutenticacao = UtilidadesDePersistencia.GetValorBooleano(Leitor, "REQUERAUTENTICACAO")

                    If ConfiguracaoDeEmail.RequerAutenticacao Then
                        ConfiguracaoDeEmail.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida = UtilidadesDePersistencia.GetValorString(Leitor, "SHNUSUSERVSAIDA")
                        ConfiguracaoDeEmail.UsuarioDeAutenticacaoDoServidorDeSaida = UtilidadesDePersistencia.GetValorString(Leitor, "USUSERVSAIDA")
                    End If

                    ConfiguracaoDeEmail.ServidorDeSaidaDeEmail = UtilidadesDePersistencia.GetValorString(Leitor, "SERVSAIDA")
                    ConfiguracaoDeEmail.TipoDoServidor = TipoDeServidorDeEmail.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "TIPOSERVSAIDA"))
                End If

                ConfiguracaoDoSistema.ConfiguracaoDeEmailDoSistema = ConfiguracaoDeEmail
            End If
        End Using

        Return ConfiguracaoDoSistema
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IMapeadorDeConfiguracoesDoSistema.Salve
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_CNFGERAL")

        Sql.Append("INSERT INTO NCL_CNFGERAL (")
        Sql.Append("NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL,")
        Sql.Append("PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA,")
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat("'", IIf(Configuracao.NotificarErrosAutomaticamente, "S", "N"), "', "))

        If String.IsNullOrEmpty(Configuracao.RemetenteDaNotificaoDeErros) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.RemetenteDaNotificaoDeErros), "', "))
        End If

        If Not Configuracao.ConfiguracaoDeEmailDoSistema Is Nothing Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.EmailRemetente), "', "))
            Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeEmailDoSistema.HabilitarSSL, "S", "N"), "', "))
            Sql.Append(String.Concat(Configuracao.ConfiguracaoDeEmailDoSistema.Porta.ToString, ", "))
            Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao, "S", "N"), "', "))

            If Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao Then
                Sql.Append(String.Concat("'", Configuracao.ConfiguracaoDeEmailDoSistema.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida, "', "))
                Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.UsuarioDeAutenticacaoDoServidorDeSaida), "', "))
            Else
                Sql.Append("NULL, NULL, ")
            End If

            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.ServidorDeSaidaDeEmail), "', "))
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.TipoDoServidor.ID.ToString), "')"))
        Else
            Sql.Append("NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class
