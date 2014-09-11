Imports System.Web.UI
Imports System.IO
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports System.Web.UI.WebControls
Imports Compartilhados.Visual
Imports System.IO.Compression

Public MustInherit Class PaginaDesktop
    Inherits Page

    Private Const CHAVE_VIEW_STATE_SESSAO As String = "###__SIMPLE_VSSESSION_PAGINA_DESKTOP__"
    Private escritor As System.IO.StringWriter = Nothing
    Private los As LosFormatter = Nothing
    Private nomePagina As String = Nothing

    Public Sub New()
        AddHandler MyBase.PreInit, New EventHandler(AddressOf Me.Page_PreInit)
        AddHandler MyBase.Load, New EventHandler(AddressOf Me.SuperPagina_Load)
        Me.nomePagina = Me.Page.GetType.FullName
        Me.escritor = New System.IO.StringWriter
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs)
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        If Principal Is Nothing OrElse Not Principal.EstaAutenticado Then
            Me.RedirecionePaginaUsuarioNaoAutenticado()
        End If
    End Sub

    Protected Overridable Sub RedirecionePaginaUsuarioNaoAutenticado()
        Me.Redirecione("~/naoautenticado.html", "Usuário não autenticado.")
    End Sub

    Private Sub Redirecione(ByVal Pagina As String, ByVal Msg As String)
        If Not File.Exists(MyBase.Request.MapPath(Pagina)) Then
            Throw New ApplicationException(Msg)
        End If
        MyBase.Response.Redirect(MyBase.ResolveUrl(Pagina))
    End Sub

    Protected Sub SuperPagina_Load(ByVal sender As Object, ByVal e As EventArgs)
        Using Servico As IServicoDePerfil = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePerfil)()
            FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil = Servico.Obtenha(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario)
        End Using

        Dim myCss As String = "body { background:#3d71b8 url(" & FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ImagemDesktop & ") repeat-x; }"

        Dim cssStyle As String = "<style type=""text/css"">" & myCss & "</style>"

        Dim cssLiteral As Literal = New Literal()

        cssLiteral.Text = cssStyle
        Page.Header.Controls.Add(cssLiteral)
    End Sub

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