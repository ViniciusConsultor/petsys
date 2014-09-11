Imports System.Web.UI
Imports Compartilhados
Imports System.IO
Imports Telerik.Web.UI
Imports System.IO.Compression
Imports System.Text
Imports System.Web.UI.WebControls

Public MustInherit Class SuperPagina
    Inherits Page

    Private Const CHAVE_VIEW_STATE_SESSAO As String = "###__SIMPLE_VSSESSION_SUPER_PAGINA__"
    Private escritor As System.IO.StringWriter = Nothing
    Private los As LosFormatter = Nothing
    Private nomePagina As String = Nothing

    Public Sub New()
        AddHandler MyBase.PreInit, New EventHandler(AddressOf Me.Page_PreInit)
        AddHandler MyBase.Load, New EventHandler(AddressOf Me.SuperPagina_Load)
        AddHandler MyBase.PreRender, New EventHandler(AddressOf Me.SuperPagina_PreRender)
        Me.nomePagina = Me.Page.GetType.FullName
        Me.escritor = New System.IO.StringWriter
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs)
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        If Principal Is Nothing OrElse Not Principal.EstaAutenticado Then
            Me.RedirecionePaginaUsuarioNaoAutenticado()
        End If

        If Not Principal.EstaAutorizado(Me.ObtenhaIdFuncao) Then
            Me.RedirecionePaginaUsuarioNaoAutorizado()
        End If
    End Sub

    Protected Sub SuperPagina_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        MostraOperacoesAutorizadas()
    End Sub

    Private Sub MostraOperacoesAutorizadas()
        Dim ToolBar As RadToolBar
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        ToolBar = Me.ObtenhaBarraDeFerramentas

        If Not ToolBar Is Nothing Then
            For Each Item As RadToolBarItem In ToolBar.Items
                If TypeOf Item Is RadToolBarButton Then
                    If Not String.IsNullOrEmpty(CType(Item, RadToolBarButton).CommandArgument) Then
                        'Vamos fazer as verificações apenas nos botoes que tiverem visiveis
                        If Item.Visible Then
                            Item.Visible = Principal.EstaAutorizado(CType(Item, RadToolBarButton).CommandArgument)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub SuperPagina_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.IsPostBack Then
            UtilidadesWeb.SetaSkinNaPagina(Me)
        End If
    End Sub

    Protected Overridable Sub RedirecionePaginaUsuarioNaoAutenticado()
        Me.Redirecione("~/naoautenticado.html", "Usuário não autenticado.")
    End Sub

    Protected Overridable Sub RedirecionePaginaUsuarioNaoAutorizado()
        Me.Redirecione("~/naoautorizado.html", "Usuário não autorizado.")
    End Sub

    Private Sub Redirecione(ByVal Pagina As String, ByVal Msg As String)
        If Not File.Exists(MyBase.Request.MapPath(Pagina)) Then
            Throw New ApplicationException(Msg)
        End If
        MyBase.Response.Redirect(MyBase.ResolveUrl(Pagina))
    End Sub

    Protected MustOverride Function ObtenhaIdFuncao() As String
    Protected MustOverride Function ObtenhaBarraDeFerramentas() As RadToolBar

    Public Property Skin() As String
        Get
            Return CStr(Session("SuperTipoSkin"))
        End Get
        Set(ByVal value As String)
            Session("SuperTipoSkin") = value
        End Set
    End Property

    Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Me.los = New LosFormatter
        Dim hashtable As Hashtable = DirectCast(Me.Session.Item(CHAVE_VIEW_STATE_SESSAO), Hashtable)
        If (hashtable Is Nothing) Then
            Return Nothing
        End If
        If Not hashtable.Contains(Me.nomePagina) Then
            Return Nothing
        End If
        Return Me.los.Deserialize(descompacte(CType(hashtable.Item(Me.nomePagina), Byte())))
    End Function

    Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        Try
            Me.los = New LosFormatter
            Me.los.Serialize(Me.escritor, viewState)

            Dim hashtable As Hashtable = DirectCast(Me.Session.Item(CHAVE_VIEW_STATE_SESSAO), Hashtable)
            If hashtable Is Nothing Then hashtable = New Hashtable

            hashtable(Me.nomePagina) = compacte(Me.escritor.ToString())
            Me.Session.Item(CHAVE_VIEW_STATE_SESSAO) = hashtable
        Finally
            Me.los = Nothing
            Me.escritor = Nothing
        End Try
    End Sub

    Private Function compacte(ByVal texto As String) As Byte()
        Dim dados As Byte() = System.Text.Encoding.Unicode.GetBytes(texto)
        Dim buffer As New System.IO.MemoryStream
        Dim compactador As New DeflateStream(buffer, CompressionMode.Compress, True)

        compactador.Write(dados, 0, dados.Length)
        compactador.Flush()
        compactador.Dispose()

        Return buffer.GetBuffer()
    End Function

    Private Function descompacte(ByVal texto As Byte()) As String
        Dim retorno As New System.Text.StringBuilder
        Dim buffer As New System.IO.MemoryStream(texto)
        Dim descompactador As New DeflateStream(buffer, CompressionMode.Decompress, True)
        Dim bufferDeLeitura As Byte()
        ReDim bufferDeLeitura(1024 * 1024) '1024 KB
        Dim totalLidos As Integer = descompactador.Read(bufferDeLeitura, 0, bufferDeLeitura.Length - 1)

        While totalLidos > 0
            retorno.Append(System.Text.Encoding.Unicode.GetString(bufferDeLeitura, 0, totalLidos))
            totalLidos = descompactador.Read(bufferDeLeitura, 0, bufferDeLeitura.Length - 1)
        End While

        Return retorno.ToString
    End Function

End Class