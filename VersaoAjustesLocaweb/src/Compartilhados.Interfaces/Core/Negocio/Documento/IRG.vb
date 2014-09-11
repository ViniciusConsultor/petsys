Namespace Core.Negocio.Documento

    Public Interface IRG
        Inherits IDocumento

        Property DataDeEmissao() As Nullable(Of Date)
        Property OrgaoExpeditor() As String
        Property UF() As UF

    End Interface

End Namespace