Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.Web
Imports Telerik.Web
Imports Telerik.Web.UI
Imports System.Web.SessionState

Public Class UtilidadesWeb

    Public Const URL_IMAGEM_SEM_FOTO As String = "~/Loads/Imagens/sem_foto_g.gif"
    Public Const URL_FOTO_ANIMAL As String = "~/Loads/Fotos/Animais"
    Public Const URL_FOTO_PESSOA As String = "~/Loads/Fotos/Pessoas"

    Public Const PASTA_FOTO_ANIMAL As String = "Loads\Fotos\Animais"
    Public Const PASTA_FOTO_PESSOA As String = "Loads\Fotos\Pessoas"
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
                Mensagem.AppendLine(String.Concat(Inconsistencia, "</br>"))
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
                                            ByVal TituloDaJanela As String) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("var win; ")
        Js.AppendLine(" win = new Ext.Window({ ")
        Js.AppendLine(String.Concat(" id: '", Guid.NewGuid.ToString, "',"))
        Js.AppendLine(String.Concat(" title: '", TituloDaJanela, "',"))
        Js.AppendLine("layout:  'fit',")
        Js.AppendLine("modal: true,")
        Js.AppendLine("width : 640,")
        Js.AppendLine("height : 480,")
        Js.AppendLine(String.Concat("html:'<iframe src =""", URL, """ width=""100%"" height=""100%""></iframe>',"))
        Js.AppendLine("});")
        Js.AppendLine("win.show();")
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
        Js.AppendLine(String.Concat("width : ", Width.ToString))
        Js.AppendLine(String.Concat("height : ", Height.ToString))
        Js.AppendLine(String.Concat("html:'<iframe src =""", URL, """ width=""100%"" height=""100%""></iframe>',"))
        Js.AppendLine("});")
        Js.AppendLine("win.show();")
        Js.AppendLine("</script>")

        Return Js.ToString
    End Function

    Public Shared Function ExibeJanela(ByVal URL As String, _
                                       ByVal TituloDaJanela As String) As String
        Dim Js As New StringBuilder

        Js.AppendLine("<script language='javascript' type='text/javascript'>")
        Js.AppendLine("var win; ")
        Js.AppendLine(" win = new Ext.Window({ ")
        Js.AppendLine(String.Concat(" id: '", Guid.NewGuid.ToString, "',"))
        Js.AppendLine(String.Concat(" title: '", TituloDaJanela, "',"))
        Js.AppendLine("layout:  'fit',")
        Js.AppendLine("modal: false,")
        Js.AppendLine("width : 640,")
        Js.AppendLine("height : 480,")
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
        End If

        If TypeOf Componente Is RadGrid Then
            DirectCast(Componente, RadGrid).MasterTableView.NoMasterRecordsText = "Sem registros"
        End If

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

End Class
