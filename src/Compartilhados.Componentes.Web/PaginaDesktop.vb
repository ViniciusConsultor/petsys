Imports System.Web.UI
Imports System.IO
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports System.Web.UI.WebControls
Imports Compartilhados.Visual

Public MustInherit Class PaginaDesktop
    Inherits Page

    Public Sub New()
        AddHandler MyBase.PreInit, New EventHandler(AddressOf Me.Page_PreInit)
        AddHandler MyBase.Load, New EventHandler(AddressOf Me.SuperPagina_Load)
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

        'Dim myCss As String = "body { background:#3d71b8 url(" & FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ImagemDesktop & ") no-repeat left top; }"

        Dim myCss As String = "body { background:#3d71b8 url(" & FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ImagemDesktop & ") repeat-x; }"

        Dim cssStyle As String = "<style type=""text/css"">" & myCss & "</style>"

        Dim cssLiteral As Literal = New Literal()

        cssLiteral.Text = cssStyle
        Page.Header.Controls.Add(cssLiteral)
    End Sub

    Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        Dim formatter As LosFormatter = New LosFormatter()
        Dim writer As StringWriter = New StringWriter()

        formatter.Serialize(writer, viewState)
        Dim viewStateString As String = writer.ToString()
        Dim bytes As Byte() = Convert.FromBase64String(viewStateString)

        ' COMPACTAR VIEWSTATE

        bytes = UtilidadesWeb.CompactarViewState(bytes)
        ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(bytes))
    End Sub


End Class