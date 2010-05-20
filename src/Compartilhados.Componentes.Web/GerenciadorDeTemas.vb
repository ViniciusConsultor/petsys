Imports System.Web
Imports System.IO
Imports System.Web.Configuration

Public Class GerenciadorDeTemas

    Private Sub New()
    End Sub

    Public Shared Function ObtenhaImagemAplicacao(ByVal Imagem As String) As String
        Return GerenciadorDeTemas.ObtenhaPastaImagensAplicacao & "/" & Imagem
    End Function

    Public Shared Function ObtenhaPastaImagensAplicacao() As String
        Return GerenciadorDeTemas.ObtenhaPastaTemaAplicacao & "/imagens"
    End Function

    Public Shared Function ObtenhaPastaTemaAplicacao() As String
        Dim tema As String = GerenciadorDeTemas.ObtenhaTemaPadrao
    
        If (Not HttpContext.Current Is Nothing) Then
            Return (HttpContext.Current.Request.ApplicationPath & "/App_Themes/" & tema).Replace("//", "/")
        End If
        Return ("~/App_Themes/" & tema)
    End Function

    Friend Shared Function ObtenhaTemaPadrao() As String
        Dim str As String = WebConfigurationManager.AppSettings.Item("temaPadrao")

        If Not String.IsNullOrEmpty(str) Then
            Return str
        End If

        'Dim strArray As String() = GerenciadorDeTemas.ObtenhaTemasInstalados
        'If Not strArray Is Nothing AndAlso strArray.Length > 0 Then
        '    Return strArray(0)
        'End If
        Return "prata"
    End Function

    'Public Shared Function ObtenhaTemasInstalados() As String()
    '    If (HttpContext.Current Is Nothing) Then
    '        Return Nothing
    '    End If

    '    Dim path As String = String.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Themes")

    '    If Not Directory.Exists(path) Then
    '        Return Nothing
    '    End If
    '    Dim strArray As String() = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly)
    '    Dim strArray2 As String() = New String(strArray.Length - 1) {}
    '    Dim i As Integer
    '    For i = 0 To strArray.Length - 1
    '        strArray2(i) = path.GetFileNameWithoutExtension(strArray(i))
    '    Next i
    '    Return strArray2
    'End Function

End Class
