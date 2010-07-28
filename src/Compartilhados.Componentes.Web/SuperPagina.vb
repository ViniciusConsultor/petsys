Imports System.Web.UI
Imports Compartilhados
Imports System.IO
Imports Telerik.Web.UI

Public MustInherit Class SuperPagina
    Inherits Page

    Public Sub New()
        AddHandler MyBase.PreInit, New EventHandler(AddressOf Me.Page_PreInit)
        AddHandler MyBase.Load, New EventHandler(AddressOf Me.SuperPagina_Load)
        AddHandler MyBase.PreRender, New EventHandler(AddressOf Me.SuperPagina_PreRender)
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
        If Not Me.IsPostBack Then
            MostraOperacoesAutorizadas()
        End If
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

    Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Dim viewState As String = Request.Form("__VSTATE")
        Dim bytes As Byte() = Convert.FromBase64String(viewState)

        ' DESCOMPACTAR VIEWSTATE   

        bytes = UtilidadesWeb.DescompactarViewState(bytes)

        Dim formatter As LosFormatter = New LosFormatter()

        Return formatter.Deserialize(Convert.ToBase64String(bytes))

    End Function

End Class