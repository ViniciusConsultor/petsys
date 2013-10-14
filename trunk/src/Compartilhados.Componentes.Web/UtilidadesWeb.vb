Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.Web
Imports Telerik.Web
Imports Telerik.Web.UI
Imports System.Web.SessionState
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression

Public Class UtilidadesWeb

    Public Const URL_IMAGEM_SEM_FOTO As String = "~/Loads/Imagens/sem_foto_g.gif"
    Public Const URL_FOTO_ANIMAL As String = "~/Loads/Fotos/Animais"
    Public Const URL_FOTO_PESSOA As String = "~/Loads/Fotos/Pessoas"
    Public Const URL_PAPEIS_DE_PAREDE As String = "~/Loads/Imagens/PapeisDeParede"
    Public Const URL_ATALHOS As String = "~/Loads/Imagens/Atalhos"
    
    Public Const PASTA_LOADS As String = "Loads"

    Public Shared Sub HabilitaComponentes(ByRef Componente As Control, _
                                          ByVal Habilitar As Boolean)
        If TypeOf Componente Is WebControl Then
            CType(Componente, WebControl).Enabled = Habilitar
        End If

        If Componente.HasControls Then
            For Each ComponenteFilho As Control In Componente.Controls
                HabilitaComponentes(ComponenteFilho, Habilitar)
            Next
        End If
    End Sub

    Public Shared Sub SetaSkinNaPagina(ByVal Pagina As Page)
        If Not TypeOf Pagina Is SuperPagina Then Exit Sub

        Dim Skin As String

        Skin = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.Skin

        For Each Componente As Control In Pagina.Controls
            SetaSkinNoComponente(Componente, Skin)
        Next

        CType(Pagina, SuperPagina).Skin = Skin
    End Sub

    Private Shared Sub SetaSkinNoComponente(ByVal Componente As Control, _
                                            ByVal Skin As String)
        If TypeOf Componente Is ISkinnableControl Then
            CType(Componente, ISkinnableControl).Skin = Skin
        End If

        If Componente.HasControls Then
            For Each ComponenteFilho As Control In Componente.Controls
                SetaSkinNoComponente(ComponenteFilho, Skin)
            Next
        End If

    End Sub

    Public Shared Function MostraArquivoParaDownload(ByVal URL As String, ByVal Titulo As String) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")

        Js.AppendLine("window.open(""" & URL & """, """ & Titulo & """, """")")
        Js.AppendLine("</script>")

        Return Js.ToString

    End Function

    Public Shared Function MostraMensagemDeInformacao(ByVal mensagem As String) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("Ext.MessageBox.show({ ")
        Js.AppendLine("title:'Informação', ")
        Js.AppendLine(String.Concat("msg:'", mensagem, "', "))
        Js.AppendLine(String.Concat("buttons: Ext.MessageBox.OK,"))
        Js.AppendLine(String.Concat("icon: Ext.MessageBox.INFO});"))

        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Function MostraMensagemDeInconsistencias(ByVal Inconsistencias As IList(Of String)) As String
        Dim Js As New StringBuilder
        Dim Mensagem As New StringBuilder

        If Not Inconsistencias Is Nothing Then
            For Each Inconsistencia As String In Inconsistencias
                Mensagem.Append(String.Concat(Inconsistencia, "</br>"))
            Next
        End If

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("Ext.MessageBox.show({ ")
        Js.AppendLine("title:'Inconsistências', ")
        Js.AppendLine(String.Concat("msg:'", Mensagem.ToString, "', "))
        Js.AppendLine(String.Concat("buttons: Ext.MessageBox.OK,"))
        Js.AppendLine(String.Concat("icon: Ext.MessageBox.WARNING});"))
        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Function ExibeJanelaModal(ByVal URL As String, _
                                            ByVal TituloDaJanela As String, _
                                            ByVal Width As Integer, _
                                            ByVal Height As Integer) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("var win; ")
        Js.AppendLine(" win = new Ext.Window({ ")
        Js.AppendLine(String.Concat(" id: '", Guid.NewGuid.ToString, "',"))
        Js.AppendLine(String.Concat(" title: '", TituloDaJanela, "',"))
        Js.AppendLine("layout:  'fit',")
        Js.AppendLine("modal: true,")
        Js.AppendLine(String.Concat("width : ", Width.ToString, ","))
        Js.AppendLine(String.Concat("height : ", Height.ToString, ","))
        Js.AppendLine(String.Concat("html:'<iframe src =""", URL, """ width=""100%"" height=""100%""></iframe>',"))
        Js.AppendLine("});")
        Js.AppendLine("win.show();")
        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Function ExibeJanela(ByVal URL As String, _
                                       ByVal TituloDaJanela As String, _
                                       ByVal Width As Integer, _
                                       ByVal Height As Integer) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("var win; ")
        Js.AppendLine(" win = new Ext.Window({ ")
        Js.AppendLine(String.Concat(" id: '", Guid.NewGuid.ToString, "',"))
        Js.AppendLine(String.Concat(" title: '", TituloDaJanela, "',"))
        Js.AppendLine("layout:  'fit',")
        Js.AppendLine("modal: false,")
        Js.AppendLine(String.Concat("width : ", Width.ToString, ","))
        Js.AppendLine(String.Concat("height : ", Height.ToString, ","))
        Js.AppendLine(String.Concat("html:'<iframe src =""", URL, """ width=""100%"" height=""100%""></iframe>',"))
        Js.AppendLine("});")
        Js.AppendLine("win.show();")
        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Function MostraMensagemDeInconsitencia(ByVal Mensagem As String) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("Ext.MessageBox.show({ ")
        Js.AppendLine("title:'Inconsistências', ")
        Js.AppendLine(String.Concat("msg:'", Mensagem, "', "))
        Js.AppendLine(String.Concat("buttons: Ext.MessageBox.OK,"))
        Js.AppendLine(String.Concat("icon: Ext.MessageBox.WARNING});"))
        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Sub LimparComponente(ByRef Componente As Control)
        If TypeOf Componente Is TextBox Then
            CType(Componente, TextBox).Text = ""
        End If

        If TypeOf Componente Is DropDownList Then
            CType(Componente, DropDownList).Items.Clear()
        End If

        If TypeOf Componente Is ListBox Then
            CType(Componente, ListBox).Items.Clear()
        End If

        If TypeOf Componente Is RadioButton Then
            CType(Componente, RadioButton).Checked = False
        End If

        If TypeOf Componente Is CheckBox Then
            CType(Componente, CheckBox).Checked = False
        End If

        If Componente.HasControls Then
            For Each ComponenteFilho As Control In Componente.Controls
                LimparComponente(ComponenteFilho)
            Next
        End If

        If TypeOf Componente Is RadComboBox Then
            DirectCast(Componente, RadComboBox).Text = String.Empty
            DirectCast(Componente, RadComboBox).Items.Clear()
        End If

        If TypeOf Componente Is RadMaskedTextBox Then
            DirectCast(Componente, RadMaskedTextBox).Text = String.Empty
        End If

        If TypeOf Componente Is RadInputControl Then
            DirectCast(Componente, RadInputControl).Text = String.Empty
        End If

        If TypeOf Componente Is RadDatePicker Then
            DirectCast(Componente, RadDatePicker).SelectedDate = Nothing
            DirectCast(Componente, RadDatePicker).DateInput.MinDate = CDate("01/01/1900 00:00:00")
            DirectCast(Componente, RadDatePicker).MinDate = CDate("01/01/1900 00:00:00")
            DirectCast(Componente, RadDatePicker).Calendar.RangeMinDate = CDate("01/01/1900 00:00:00")
            DirectCast(Componente, RadDatePicker).Calendar.CultureInfo = CultureInfo.GetCultureInfo("pt-BR")
        End If

        If TypeOf Componente Is RadGrid Then
            DirectCast(Componente, RadGrid).MasterTableView.NoMasterRecordsText = "Sem registros"
            DirectCast(Componente, RadGrid).PagerStyle.PagerTextFormat = "Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, itens <strong>{2}</strong> a <strong>{3}</strong> de <strong>{5}</strong>."
            DirectCast(Componente, RadGrid).PagerStyle.AlwaysVisible = True
            DirectCast(Componente, RadGrid).PagerStyle.Mode = GridPagerMode.NumericPages
        End If

        If TypeOf Componente Is RadScheduler Then
            SetaGlobalizacaoPortuguesNoComponenteScheduler(CType(Componente, RadScheduler))
        End If

    End Sub

    Public Shared Sub SetaGlobalizacaoPortuguesNoComponenteScheduler(ByVal Componente As RadScheduler)
        With Componente
            .OverflowBehavior = OverflowBehavior.Scroll
            .StartEditingInAdvancedForm = False
            .StartInsertingInAdvancedForm = False
            .AllowDelete = True
            .AllowInsert = False
            .AllowEdit = False
            .Culture = CultureInfo.GetCultureInfo("pt-BR")
            .DisplayDeleteConfirmation = False
            .AdvancedForm.Enabled = False
            .HoursPanelTimeFormat = "HH:mm"
            .ShowFullTime = False
            .FirstDayOfWeek = DayOfWeek.Monday
            .LastDayOfWeek = DayOfWeek.Friday
            .Localization.AdvancedAllDayEvent = "Todo dia"
            .Localization.AdvancedCalendarCancel = "Cancelar"
            .Localization.AdvancedCalendarToday = "Hoje"
            .Localization.AdvancedClose = "Fechar"
            .Localization.AdvancedDaily = "Diariamente"
            .Localization.AdvancedDay = "Dia"
            .Localization.AdvancedDays = "dia(s)"
            .Localization.AdvancedDescription = "Descrição"
            .Localization.AdvancedEditAppointment = "Editar compromisso"
            .Localization.AdvancedEvery = "Toda"
            .Localization.AdvancedEveryWeekday = "Toda semana"
            .Localization.AdvancedFirst = "primeiro"
            .Localization.AdvancedFourth = "quarto"
            .Localization.AdvancedFrom = "Hora de início"
            .Localization.AdvancedHourly = "De hora em hora"
            .Localization.AdvancedHours = "hora(s)"
            .Localization.AdvancedInvalidNumber = "Número inválido"
            .Localization.AdvancedLast = "último"
            .Localization.AdvancedMaskDay = "dia"
            .Localization.AdvancedMaskWeekday = "dia da semana"
            .Localization.AdvancedMaskWeekendDay = "dia de semana"
            .Localization.AdvancedMonthly = "Mensal"
            .Localization.AdvancedMonths = "mês(es)"
            .Localization.AdvancedNewAppointment = "Novo compromisso"
            .Localization.AdvancedNoEndDate = "Sem data final"
            .Localization.AdvancedOccurrences = "ocorrências"
            .Localization.AdvancedOf = "de"
            .Localization.AdvancedOfEvery = "de cada"
            .Localization.AdvancedRecurEvery = "Repetir todos os"
            .Localization.AdvancedSecond = "segundo"
            .Localization.AdvancedSubject = "Assunto"
            .Localization.AdvancedThe = "O"
            .Localization.AdvancedThird = "terceiro"
            .Localization.AdvancedWeekly = "Semanal"
            .Localization.AdvancedWeeks = "semana(s) em"
            .Localization.AdvancedWorking = "Trabalho..."
            .Localization.AdvancedYearly = "Anual"
            .Localization.AllDay = "Horários"
            .Localization.Cancel = "Cancelar"
            .Localization.ConfirmCancel = "Cancelar"
            .Localization.ContextMenuDelete = "Apagar"
            .Localization.ContextMenuGoToToday = "Ir para hoje"
            .Localization.HeaderDay = "Dia"
            .Localization.HeaderMonth = "Mês"
            .Localization.HeaderNextDay = "próximo dia"
            .Localization.HeaderPrevDay = "dia anterior"
            .Localization.HeaderTimeline = "Linha do tempo"
            .Localization.HeaderToday = "hoje"
            .Localization.HeaderWeek = "Semana"
            .Localization.Show24Hours = "Mostrar as 24 horas..."
            .Localization.ShowBusinessHours = "Mostrar as horas de trabalho..."
            .Localization.ShowMore = "mais..."
        End With

    End Sub

    Public Shared Sub RedirecionaPaginaPorJScript(ByRef PaginaOrigem As Page, _
                                                  ByVal NomePaginaDestino As String)
        Dim Script As New StringBuilder

        Script.Append("<script language='javascript'>")
        Script.Append("parent.location.href = '" & NomePaginaDestino & "'")
        Script.Append("</script>")

        PaginaOrigem.Response.Write(Script.ToString)
        PaginaOrigem.Response.Flush()
    End Sub

    Public Shared Function ObtenhaURLCorrente() As String
        Dim URL As String

        URL = HttpContext.Current.Request.Url.AbsoluteUri

        Return URL.Substring(0, URL.LastIndexOf("/") + 1)
    End Function

    Public Shared Function ObtenhaURLHostDiretorioVirtual() As String
        Dim URL As String

        URL = String.Concat(HttpContext.Current.Request.Url.Scheme, "://", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.ApplicationPath, "/")

        Return URL.Substring(0, URL.LastIndexOf("/") + 1)
    End Function

    Public Shared Sub redimensionaImagem(ByVal diretorio As String, _
                                         ByVal nomeImagem As String, _
                                         ByVal novoHeight As Integer, _
                                         ByVal novoWidth As Integer)
        Dim arquivo As System.Drawing.Image
        Dim arquivoNovo As System.Drawing.Image
        'Deve ser passado o arquivo como o caminho completo. Ex.: "C:\Pasta1\"
        If Not diretorio.Trim.Substring(diretorio.Trim.Length - 1, 1).Equals("\") Then diretorio = diretorio.Trim & "\"

        If IO.File.Exists(diretorio & nomeImagem) Then
            arquivo = System.Drawing.Image.FromFile(diretorio & nomeImagem)


            ' Calcula a nova dimensão proporcionalmente
            Dim razao As Double
            Dim newHeight As Integer = arquivo.Height
            Dim newWidth As Integer = arquivo.Width

            If arquivo.Height > novoHeight Or arquivo.Width > novoWidth Then
                If arquivo.Height / novoHeight > arquivo.Width / novoWidth Then
                    razao = arquivo.Height / novoHeight
                    newHeight = novoHeight
                    newWidth = CInt(arquivo.Width / razao)
                Else
                    razao = arquivo.Width / novoWidth
                    newWidth = novoWidth
                    newHeight = CInt(arquivo.Height / razao)
                End If

                ' Redimensiona o arquivo para o padrão passado
                arquivoNovo = arquivo.GetThumbnailImage(newWidth, newHeight, Nothing, System.IntPtr.Zero)
                'Salva um arquivo temporário com o tamanho correto
                arquivoNovo.Save(diretorio & "_" & nomeImagem)
                arquivo.Dispose()
                'Carrega o novo arquivo temporario para poder deletar o antigo(pq ele ñ consegue deletar se estiver usando.
                arquivo = System.Drawing.Image.FromFile(diretorio & "_" & nomeImagem)
                'deleta o arquivo oringinal para poder salvar o redimensionado
                Kill(diretorio & nomeImagem)
                'Salva o arquivo com o nome original
                arquivo.Save(diretorio & nomeImagem)
                arquivo.Dispose()
                'Deleta o arquivo temporário
                Kill(diretorio & "_" & nomeImagem)
            End If
            arquivo.Dispose()
        End If
    End Sub

    Public Shared Sub PaginacaoDataGrid(ByRef Grid As RadGrid, ByVal Dados As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs)
        If e.NewPageIndex >= 0 Then
            Grid.CurrentPageIndex = e.NewPageIndex
            Grid.DataSource = Dados
            Grid.DataBind()
        End If
    End Sub

    Public Shared Sub ExibaUltimaPaginaDoDataGrid(ByVal DataGrid As DataGrid)
        Dim UltimaPagina As Integer = DataGrid.PageCount - 1

        If UltimaPagina = -1 Then Exit Sub

        DataGrid.CurrentPageIndex = UltimaPagina
        DataGrid.DataBind()
    End Sub

    Public Shared Function CompactarViewState(ByVal bytes As Byte()) As Byte()
        Dim MSsaida As MemoryStream = New MemoryStream()
        Dim gzip As GZipStream = New GZipStream(MSsaida, CompressionMode.Compress, True)

        gzip.Write(bytes, 0, bytes.Length)
        gzip.Close()
        Return MSsaida.ToArray()
    End Function

    Public Shared Function DescompactarViewState(ByVal bytes As Byte()) As Byte()
        Dim MSentrada As MemoryStream = New MemoryStream()

        MSentrada.Write(bytes, 0, bytes.Length)
        MSentrada.Position = 0

        Dim gzip As GZipStream = New GZipStream(MSentrada, CompressionMode.Decompress, True)

        Dim MSsaida As MemoryStream = New MemoryStream()

        Dim buffer As Byte() = New Byte(64) {}
        Dim leitura As Integer = -1

        leitura = gzip.Read(buffer, 0, buffer.Length)

        While (leitura > 0)
            MSsaida.Write(buffer, 0, leitura)
            leitura = gzip.Read(buffer, 0, buffer.Length)
        End While

        gzip.Close()
        Return MSsaida.ToArray()

    End Function
End Class
