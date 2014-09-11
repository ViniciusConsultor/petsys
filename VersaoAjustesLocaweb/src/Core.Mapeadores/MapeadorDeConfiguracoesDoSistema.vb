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
        Dim ConfiguracaoDeAgenda As IConfiguracaoDeAgendaDoSistema = Nothing

        Sql.Append("SELECT NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL, ")
        Sql.Append("PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA, ")
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA, TXTCOMPRO, TXTCOMPROENTRELNH, TXTLEMBRE, ")
        Sql.Append("TXTLEMBREENTRELNH, TXTTARE, TXTTAREENTRELNH, TXTCABAGEN, APRELNHCABAGEN, APRELNHRODAGEN")
        Sql.Append(" FROM NCL_CNFGERAL")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    ConfiguracaoDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDoSistema)()
                    ConfiguracaoDoSistema.NotificarErrosAutomaticamente = UtilidadesDePersistencia.GetValorBooleano(Leitor, "NOTIFERROSREMAIL")

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "EMAILREMETNOTIFERROS") Then
                        ConfiguracaoDoSistema.DestinatarioDaNotificaoDeErros = UtilidadesDePersistencia.GetValorString(Leitor, "EMAILREMETNOTIFERROS")
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

                    ConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgendaDoSistema)()
                    ConfiguracaoDeAgenda.ApresentarLinhasNoCabecalhoDaAgenda = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHCABAGEN")
                    ConfiguracaoDeAgenda.ApresentarLinhasNoRodapeDaAgenda = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHRODAGEN")
                    ConfiguracaoDeAgenda.TextoDoCompromissoEntreLinhas = UtilidadesDePersistencia.GetValorBooleano(Leitor, "TXTCOMPROENTRELNH")
                    ConfiguracaoDeAgenda.TextoDeLembretesEntreLinhas = UtilidadesDePersistencia.GetValorBooleano(Leitor, "TXTLEMBREENTRELNH")
                    ConfiguracaoDeAgenda.TextoDeTarefasEntreLinhas = UtilidadesDePersistencia.GetValorBooleano(Leitor, "TXTTAREENTRELNH")

                    ConfiguracaoDeAgenda.TextoCabecalhoDaAgenda = UtilidadesDePersistencia.GetValorString(Leitor, "TXTCABAGEN")
                    ConfiguracaoDeAgenda.TextoCompromissos = UtilidadesDePersistencia.GetValorString(Leitor, "TXTCOMPRO")
                    ConfiguracaoDeAgenda.TextoLembretes = UtilidadesDePersistencia.GetValorString(Leitor, "TXTLEMBRE")
                    ConfiguracaoDeAgenda.TextoTarefas = UtilidadesDePersistencia.GetValorString(Leitor, "TXTTARE")

                    ConfiguracaoDoSistema.ConfiguracaoDeAgendaDoSistema = ConfiguracaoDeAgenda
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return ConfiguracaoDoSistema
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IMapeadorDeConfiguracoesDoSistema.Salve
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_CNFGERAL")

        Sql.Append("INSERT INTO NCL_CNFGERAL (")
        Sql.Append("NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL, ")
        Sql.Append("PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA, ")
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA, TXTCOMPRO, TXTCOMPROENTRELNH, TXTLEMBRE, ")
        Sql.Append("TXTLEMBREENTRELNH, TXTTARE, TXTTAREENTRELNH, TXTCABAGEN, APRELNHCABAGEN, APRELNHRODAGEN)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat("'", IIf(Configuracao.NotificarErrosAutomaticamente, "S", "N"), "', "))

        If String.IsNullOrEmpty(Configuracao.DestinatarioDaNotificaoDeErros) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.DestinatarioDaNotificaoDeErros), "', "))
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
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.TipoDoServidor.ID.ToString), "',"))
        Else
            Sql.Append("NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,")
        End If

        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoCompromissos), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoDoCompromissoEntreLinhas, "S", "N"), "', "))

        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoLembretes), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoDeLembretesEntreLinhas, "S", "N"), "', "))

        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoTarefas), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoDeTarefasEntreLinhas, "S", "N"), "', "))

        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoCabecalhoDaAgenda), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDaAgenda, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDaAgenda, "S", "N"), "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString, False)
    End Sub

End Class
