Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class InscricaoMunicipal
    Inherits Documento
    Implements IInscricaoMunicipal

    Public Sub New(ByVal Numero As String)
        MyBase.New(Numero)
    End Sub

    Public Overrides Function EhValido() As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property Tipo() As TipoDeDocumento
        Get
            Return TipoDeDocumento.IM
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Format(CLng(Me.Numero), "000\.###\-##")
    End Function

End Class
