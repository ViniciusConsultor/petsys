Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class RG
    Inherits Documento
    Implements IRG

    Private _DataDeEmissao As Nullable(Of Date)
    Private _OrgaoExpeditor As String
    Private _UF As UF

    Public Sub New(ByVal Numero As String)
        MyBase.New(Numero)
    End Sub

    Public Overrides Function EhValido() As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property Tipo() As TipoDeDocumento
        Get
            Return TipoDeDocumento.RG
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Numero
    End Function

    Public Property DataDeEmissao() As Date? Implements IRG.DataDeEmissao
        Get
            Return _DataDeEmissao
        End Get
        Set(ByVal value As Date?)
            _DataDeEmissao = value
        End Set
    End Property

    Public Property OrgaoExpeditor() As String Implements IRG.OrgaoExpeditor
        Get
            Return _OrgaoExpeditor
        End Get
        Set(ByVal value As String)
            _OrgaoExpeditor = Value
        End Set
    End Property

    Public Property UF() As UF Implements IRG.UF
        Get
            Return _UF
        End Get
        Set(ByVal value As UF)
            _UF = value
        End Set
    End Property

End Class
