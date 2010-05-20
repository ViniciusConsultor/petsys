Imports System.Web

Friend Class SessaoWeb
    Inherits Sessao

    Private _Contexto As Principal
    Private Const SESSIONCONTEXTO As String = "ContextoAtual"

    Public Sub New()
        Me.Contexto = New Principal
    End Sub

    Public Overrides Sub AtualizeContexto(ByVal ContextoAtual As Principal)
        Dim HttpContext As HttpContext = HttpContext.Current
        HttpContext.Session(SESSIONCONTEXTO) = ContextoAtual
    End Sub

    Public Overrides Property Contexto() As Principal
        Get
            Me._Contexto = Me.RecupereContexto()
            Return Me._Contexto
        End Get
        Set(ByVal value As Principal)
            Me._Contexto = value
            Me.AtualizeContexto(Me._Contexto)
        End Set
    End Property

    Public Overrides Function RecupereContexto() As Principal
        Dim HttpContext As HttpContext = HttpContext.Current

        If CType(HttpContext.Session(SESSIONCONTEXTO), Principal) Is Nothing Then
            Me.AtualizeContexto(New Principal)
        End If

        Return CType(HttpContext.Session(SESSIONCONTEXTO), Principal)
    End Function

End Class