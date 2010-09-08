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

        Sql.Append("SELECT NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL,")
        Sql.Append("PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA,")
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA, TXTCABCOMPRO,	APRELNHCABCOMPRO,")
        Sql.Append("APRELNHRODCOMPRO, TXTCABLEMBRE, APRELNHCABLEMBRE, APRELNHRODLEMBRE,")
        Sql.Append("TXTCABTARE,	APRELNHCABTARE, APRELNHRODTARE")
        Sql.Append(" FROM NCL_CNFGERAL")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                ConfiguracaoDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDoSistema)()
                ConfiguracaoDoSistema.NotificarErrosAutomaticamente = UtilidadesDePersistencia.GetValorBooleano(Leitor, "NOTIFERROSREMAIL")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "EMAILREMETNOTIFERROS") Then
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

                ConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgendaDoSistema)()
                ConfiguracaoDeAgenda.ApresentarLinhasNoCabecalhoDeCompromissos = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHCABCOMPRO")
                ConfiguracaoDeAgenda.ApresentarLinhasNoCabecalhoDeLembretes = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHCABLEMBRE")
                ConfiguracaoDeAgenda.ApresentarLinhasNoCabecalhoDeTarefas = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHCABTARE")
                ConfiguracaoDeAgenda.ApresentarLinhasNoRodapeDeCompromissos = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHRODCOMPRO")
                ConfiguracaoDeAgenda.ApresentarLinhasNoRodapeDeLembretes = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHRODLEMBRE")
                ConfiguracaoDeAgenda.ApresentarLinhasNoRodapeDeTarefas = UtilidadesDePersistencia.GetValorBooleano(Leitor, "APRELNHRODTARE")
                ConfiguracaoDeAgenda.TextoCabecalhoDeCompromissos = UtilidadesDePersistencia.GetValorString(Leitor, "TXTCABCOMPRO")
                ConfiguracaoDeAgenda.TextoCabecalhoDeTarefas = UtilidadesDePersistencia.GetValorString(Leitor, "TXTCABTARE")
                ConfiguracaoDeAgenda.TextoCabelhoDeLembretes = UtilidadesDePersistencia.GetValorString(Leitor, "TXTCABLEMBRE")

                ConfiguracaoDoSistema.ConfiguracaoDeAgendaDoSistema = ConfiguracaoDeAgenda
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
        Sql.Append("SERVSAIDA, TIPOSERVSAIDA, APRELNHCABCOMPRO, APRELNHCABLEMBRE,")
        Sql.Append("APRELNHCABTARE, APRELNHRODCOMPRO, APRELNHRODLEMBRE, APRELNHRODTARE,")
        Sql.Append("TXTCABCOMPRO, TXTCABTARE, TXTCABLEMBRE)")
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
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeEmailDoSistema.TipoDoServidor.ID.ToString), "',"))
        Else
            Sql.Append("NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,")
        End If

        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeCompromissos, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeLembretes, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeTarefas, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeCompromissos, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeLembretes, "S", "N"), "', "))
        Sql.Append(String.Concat("'", IIf(Configuracao.ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeTarefas, "S", "N"), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeCompromissos), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeTarefas), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Configuracao.ConfiguracaoDeAgendaDoSistema.TextoCabelhoDeLembretes), "') "))

        DBHelper.ExecuteNonQuery(Sql.ToString, False)
    End Sub

End Class
